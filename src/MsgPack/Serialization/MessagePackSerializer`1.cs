﻿#region -- License Terms --
//
// MessagePack for CLI
//
// Copyright (C) 2010-2012 FUJIWARA, Yusuke
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
#endregion -- License Terms --

using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;

namespace MsgPack.Serialization
{
	// TODO: MessagePackEncoder/Decoder <|- ...NativeEncoder/Decoder, ...JsonEncoder/Decoder
	/// <summary>
	///		Defines base contract for object serialization.
	/// </summary>
	/// <typeparam name="T">Target type.</typeparam>
	/// <remarks>
	///		<para>
	///			This class implements strongly typed serialization and deserialization.
	///		</para>
	///		<para>
	///			When the underlying stream does not contain strongly typed or contains dynamically typed objects,
	///			you should use <see cref="Unpacker"/> directly and take advantage of <see cref="MessagePackObject"/>.
	///		</para>
	/// </remarks>
	/// <seealso cref="Unpacker"/>
	/// <seealso cref="Unpacking"/>
	public abstract class MessagePackSerializer<T> : IMessagePackSingleObjectSerializer
	{
		// ReSharper disable once StaticFieldInGenericType
		private static readonly bool _isNullable = JudgeNullable();

#if !XAMIOS && !UNITY_IPHONE
		// This field exists for each closed generic types.
		internal static readonly MethodInfo UnpackToCoreMethod =
			FromExpression.ToMethod( ( MessagePackSerializer<T> @this, Unpacker unpacker, T collection ) => @this.UnpackToCore( unpacker, collection ) );
#endif // if !XAMIOS && !UNITY_IPHONE

		private readonly PackerCompatibilityOptions _packerCompatibilityOptions;

		/// <summary>
		///		Gets the packer compatibility options for this instance.
		/// </summary>
		/// <value>
		///		The packer compatibility options for this instance
		/// </value>
		protected internal PackerCompatibilityOptions PackerCompatibilityOptions
		{
			get { return this._packerCompatibilityOptions; }
		}

		private readonly WeakReference _ownerContext;

		/// <summary>
		///		Gets a <see cref="SerializationContext"/> which owns this serializer.
		/// </summary>
		/// <value>
		///		A <see cref="SerializationContext"/> which owns this serializer.
		///		Or <see cref="SerializationContext.Default"/> when owner context is not known or is garbage-collected.
		/// </value>
		protected internal SerializationContext OwnerContext
		{
			get
			{
				try
				{
					return this._ownerContext.Target as SerializationContext ?? SerializationContext.Default;
				}
				catch ( InvalidOperationException )
				{
					// It should not be occurred as long as serializer is holded by out of context.
					// So continuous exception is OK.
					return SerializationContext.Default;
				}
			}
		}

		/// <summary>
		///		Initializes a new instance of the <see cref="MessagePackSerializer{T}"/> class with <see cref="T:PackerCompatibilityOptions.Classic"/>.
		/// </summary>
		/// <remarks>
		///		This method supports backword compatibility with 0.3.
		/// </remarks>
		[Obsolete( "Use MessagePackSerializer (SerlaizationContext) instead." )]
		protected MessagePackSerializer() : this( PackerCompatibilityOptions.Classic ) { }

		/// <summary>
		///		Initializes a new instance of the <see cref="MessagePackSerializer{T}"/> class with default context..
		/// </summary>
		/// <param name="packerCompatibilityOptions">The <see cref="PackerCompatibilityOptions"/> for new packer creation.</param>
		/// <remarks>
		///		This method supports backword compatibility with 0.4.
		/// </remarks>
		[Obsolete( "Use MessagePackSerializer (SerlaizationContext, PackerCompatibilityOptions) instead." )]
		protected MessagePackSerializer( PackerCompatibilityOptions packerCompatibilityOptions )
			: this( null, packerCompatibilityOptions ) { }

		/// <summary>
		///		Initializes a new instance of the <see cref="MessagePackSerializer{T}"/> class.
		/// </summary>
		/// <param name="ownerContext">A <see cref="SerializationContext"/> which owns this serializer.</param>
		/// <exception cref="ArgumentNullException"><paramref name="ownerContext"/> is <c>null</c>.</exception>
		protected MessagePackSerializer( SerializationContext ownerContext )
			: this( ownerContext, ( ownerContext ?? SerializationContext.Default ).CompatibilityOptions.PackerCompatibilityOptions ) { }

		/// <summary>
		///		Initializes a new instance of the <see cref="MessagePackSerializer{T}"/> class with explicitly specified compatibility option.
		/// </summary>
		/// <param name="ownerContext">A <see cref="SerializationContext"/> which owns this serializer.</param>
		/// <param name="packerCompatibilityOptions">The <see cref="PackerCompatibilityOptions"/> for new packer creation.</param>
		/// <exception cref="ArgumentNullException"><paramref name="ownerContext"/> is <c>null</c>.</exception>
		/// <remarks>
		///		This method also supports backword compatibility with 0.4.
		/// </remarks>
		protected MessagePackSerializer( SerializationContext ownerContext, PackerCompatibilityOptions packerCompatibilityOptions )
		{
			if ( ownerContext == null )
			{
				throw new ArgumentNullException( "ownerContext" );
			}
			
			this._packerCompatibilityOptions = packerCompatibilityOptions;
			this._ownerContext = new WeakReference( ownerContext );
		}

		private static bool JudgeNullable()
		{
			if ( !typeof( T ).GetIsValueType() )
			{
				// reference type.
				return true;
			}

			if ( typeof( T ) == typeof( MessagePackObject ) )
			{
				// can be MPO.Nil.
				return true;
			}

			if ( typeof( T ).GetIsGenericType() && typeof( T ).GetGenericTypeDefinition() == typeof( Nullable<> ) )
			{
				// Nullable<T>
				return true;
			}

			return false;
		}

		/// <summary>
		///		Serializes specified object to the <see cref="Stream"/>.
		/// </summary>
		/// <param name="stream">Destination <see cref="Stream"/>.</param>
		/// <param name="objectTree">Object to be serialized.</param>
		/// <exception cref="ArgumentNullException">
		///		<paramref name="stream"/> is <c>null</c>.
		/// </exception>
		/// <exception cref="SerializationException">
		///		<typeparamref name="T"/> is not serializable etc.
		/// </exception>
		public void Pack( Stream stream, T objectTree )
		{
			// Packer does not have finalizer, so just avoiding packer disposing prevents stream closing.
			this.PackTo( Packer.Create( stream, this._packerCompatibilityOptions ), objectTree );
		}

		/// <summary>
		///		Deserialize object from the <see cref="Stream"/>.
		/// </summary>
		/// <param name="stream">Source <see cref="Stream"/>.</param>
		/// <returns>Deserialized object.</returns>
		/// <exception cref="ArgumentNullException">
		///		<paramref name="stream"/> is <c>null</c>.
		/// </exception>
		/// <exception cref="SerializationException">
		///		<typeparamref name="T"/> is not serializable etc.
		/// </exception>
		public T Unpack( Stream stream )
		{
			// Unpacker does not have finalizer, so just avoiding unpacker disposing prevents stream closing.
			var unpacker = Unpacker.Create( stream );
			if ( !unpacker.Read() )
			{
				throw SerializationExceptions.NewUnexpectedEndOfStream();
			}

			return this.UnpackFrom( unpacker );
		}

		public object UnpackObject( Stream stream )
		{
			return Unpack( stream );
		}

		/// <summary>
		///		Serializes specified object with specified <see cref="Packer"/>.
		/// </summary>
		/// <param name="packer"><see cref="Packer"/> which packs values in <paramref name="objectTree"/>.</param>
		/// <param name="objectTree">Object to be serialized.</param>
		/// <exception cref="ArgumentNullException">
		///		<paramref name="packer"/> is <c>null</c>.
		/// </exception>
		/// <exception cref="SerializationException">
		///		<typeparamref name="T"/> is not serializable etc.
		/// </exception>
		public void PackTo( Packer packer, T objectTree )
		{
			// TODO: Hot-Path-Optimization
			if ( packer == null )
			{
				throw new ArgumentNullException( "packer" );
			}

			// ReSharper disable once CompareNonConstrainedGenericWithNull
			if ( objectTree == null )
			{
				packer.PackNull();
				return;
			}

			this.PackToCore( packer, objectTree );
		}

		/// <summary>
		///		Serializes specified object with specified <see cref="Packer"/>.
		/// </summary>
		/// <param name="packer"><see cref="Packer"/> which packs values in <paramref name="objectTree"/>. This value will not be <c>null</c>.</param>
		/// <param name="objectTree">Object to be serialized.</param>
		/// <exception cref="SerializationException">
		///		<typeparamref name="T"/> is not serializable etc.
		/// </exception>
		protected internal abstract void PackToCore( Packer packer, T objectTree );

		/// <summary>
		///		Deserializes object with specified <see cref="Unpacker"/>.
		/// </summary>
		/// <param name="unpacker"><see cref="Unpacker"/> which unpacks values of resulting object tree.</param>
		/// <returns>Deserialized object.</returns>
		/// <exception cref="ArgumentNullException">
		///		<paramref name="unpacker"/> is <c>null</c>.
		/// </exception>
		/// <exception cref="SerializationException">
		///		Failed to deserialize object due to invalid unpacker state, stream content, or so.
		/// </exception>
		/// <exception cref="MessageTypeException">
		///		Failed to deserialize object due to invalid unpacker state, stream content, or so.
		/// </exception>
		/// <exception cref="InvalidMessagePackStreamException">
		///		Failed to deserialize object due to invalid unpacker state, stream content, or so.
		/// </exception>
		/// <exception cref="NotSupportedException">
		///		<typeparamref name="T"/> is abstract type.
		/// </exception>
		public T UnpackFrom( Unpacker unpacker )
		{
			// TODO: Hot-Path-Optimization
			if ( unpacker == null )
			{
				throw new ArgumentNullException( "unpacker" );
			}

			if ( unpacker.LastReadData.IsNil )
			{
				if ( _isNullable )
				{
					// null
					return default( T );
				}
				// ReSharper disable once RedundantIfElseBlock
				else
				{
					throw SerializationExceptions.NewValueTypeCannotBeNull( typeof( T ) );
				}
			}

			return this.UnpackFromCore( unpacker );
		}

		/// <summary>
		///		Deserializes object with specified <see cref="Unpacker"/>.
		/// </summary>
		/// <param name="unpacker"><see cref="Unpacker"/> which unpacks values of resulting object tree. This value will not be <c>null</c>.</param>
		/// <returns>Deserialized object.</returns>
		/// <exception cref="SerializationException">
		///		Failed to deserialize object due to invalid unpacker state, stream content, or so.
		/// </exception>
		/// <exception cref="MessageTypeException">
		///		Failed to deserialize object due to invalid unpacker state, stream content, or so.
		/// </exception>
		/// <exception cref="InvalidMessagePackStreamException">
		///		Failed to deserialize object due to invalid unpacker state, stream content, or so.
		/// </exception>
		/// <exception cref="NotSupportedException">
		///		<typeparamref name="T"/> is abstract type.
		/// </exception>
		protected internal abstract T UnpackFromCore( Unpacker unpacker );

		/// <summary>
		///		Deserializes collection items with specified <see cref="Unpacker"/> and stores them to <paramref name="collection"/>.
		/// </summary>
		/// <param name="unpacker"><see cref="Unpacker"/> which unpacks values of resulting object tree.</param>
		/// <param name="collection">Collection that the items to be stored.</param>
		/// <exception cref="ArgumentNullException">
		///		<paramref name="unpacker"/> is <c>null</c>.
		///		Or <paramref name="collection"/> is <c>null</c>.
		/// </exception>
		/// <exception cref="SerializationException">
		///		Failed to deserialize object due to invalid unpacker state, stream content, or so.
		/// </exception>
		/// <exception cref="MessageTypeException">
		///		Failed to deserialize object due to invalid unpacker state, stream content, or so.
		/// </exception>
		/// <exception cref="InvalidMessagePackStreamException">
		///		Failed to deserialize object due to invalid unpacker state, stream content, or so.
		/// </exception>
		/// <exception cref="NotSupportedException">
		///		<typeparamref name="T"/> is not collection.
		/// </exception>
		public void UnpackTo( Unpacker unpacker, T collection )
		{
			// TODO: Hot-Path-Optimization
			if ( unpacker == null )
			{
				throw new ArgumentNullException( "unpacker" );
			}

			// ReSharper disable once CompareNonConstrainedGenericWithNull
			if ( collection == null )
			{
				throw new ArgumentNullException( "unpacker" );
			}

			if ( unpacker.LastReadData.IsNil )
			{
				return;
			}

			this.UnpackToCore( unpacker, collection );
		}

		/// <summary>
		///		Deserializes collection items with specified <see cref="Unpacker"/> and stores them to <paramref name="collection"/>.
		/// </summary>
		/// <param name="unpacker"><see cref="Unpacker"/> which unpacks values of resulting object tree. This value will not be <c>null</c>.</param>
		/// <param name="collection">Collection that the items to be stored. This value will not be <c>null</c>.</param>
		/// <exception cref="SerializationException">
		///		Failed to deserialize object due to invalid unpacker state, stream content, or so.
		/// </exception>
		/// <exception cref="NotSupportedException">
		///		<typeparamref name="T"/> is not collection.
		/// </exception>
		protected internal virtual void UnpackToCore( Unpacker unpacker, T collection )
		{
			throw new NotSupportedException( String.Format( CultureInfo.CurrentCulture, "This operation is not supported by '{0}'.", this.GetType() ) );
		}

		/// <summary>
		///		Serializes specified object to the array of <see cref="Byte"/>.
		/// </summary>
		/// <param name="objectTree">Object to be serialized.</param>
		/// <returns>An array of <see cref="Byte"/> which stores serialized value.</returns>
		/// <exception cref="SerializationException">
		///		<typeparamref name="T"/> is not serializable etc.
		/// </exception>
		public byte[] PackSingleObject( T objectTree )
		{
			using ( var buffer = new MemoryStream() )
			{
				this.Pack( buffer, objectTree );
				return buffer.ToArray();
			}
		}

		/// <summary>
		///		Deserializes a single object from the array of <see cref="Byte"/> which contains a serialized object.
		/// </summary>
		/// <param name="buffer">An array of <see cref="Byte"/> serialized value to be stored.</param>
		/// <returns>A bytes of serialized binary.</returns>
		/// <exception cref="ArgumentNullException">
		///		<paramref name="buffer"/> is <c>null</c>.
		/// </exception>
		/// <exception cref="SerializationException">
		///		Failed to deserialize object due to invalid unpacker state, stream content, or so.
		/// </exception>
		/// <exception cref="MessageTypeException">
		///		Failed to deserialize object due to invalid unpacker state, stream content, or so.
		/// </exception>
		/// <exception cref="InvalidMessagePackStreamException">
		///		Failed to deserialize object due to invalid unpacker state, stream content, or so.
		/// </exception>
		/// <remarks>
		///		<para>
		///			This method assumes that <paramref name="buffer"/> contains single serialized object dedicatedly,
		///			so this method does not return any information related to actual consumed bytes.
		///		</para>
		///		<para>
		///			This method is a counter part of <see cref="PackSingleObject"/>.
		///		</para>
		/// </remarks>
		public T UnpackSingleObject( byte[] buffer )
		{
			if ( buffer == null )
			{
				throw new ArgumentNullException( "buffer" );
			}

			using ( var stream = new MemoryStream( buffer ) )
			{
				return this.Unpack( stream );
			}
		}

		void IMessagePackSerializer.PackTo( Packer packer, object objectTree )
		{
			// TODO: Hot-Path-Optimization
			if ( packer == null )
			{
				throw new ArgumentNullException( "packer" );
			}

			if ( objectTree == null )
			{
				if ( typeof( T ).GetIsValueType() )
				{
					if ( !( typeof( T ).GetIsGenericType() && typeof( T ).GetGenericTypeDefinition() == typeof( Nullable<> ) ) )
					{
						throw SerializationExceptions.NewValueTypeCannotBeNull( typeof( T ) );
					}
				}

				packer.PackNull();
				return;
			}
			// ReSharper disable once RedundantIfElseBlock
			else
			{
				if ( !( objectTree is T ) )
				{
					throw new ArgumentException( String.Format( CultureInfo.CurrentCulture, "'{0}' is not compatible for '{1}'.", objectTree.GetType(), typeof( T ) ), "objectTree" );
				}
			}

			this.PackToCore( packer, ( T )objectTree );
		}

		object IMessagePackSerializer.UnpackFrom( Unpacker unpacker )
		{
			return this.UnpackFrom( unpacker );
		}

		void IMessagePackSerializer.UnpackTo( Unpacker unpacker, object collection )
		{
			// TODO: Hot-Path-Optimization
			if ( unpacker == null )
			{
				throw new ArgumentNullException( "unpacker" );
			}

			if ( collection == null )
			{
				throw new ArgumentNullException( "collection" );
			}

			if ( !( collection is T ) )
			{
				throw new ArgumentException( String.Format( CultureInfo.CurrentCulture, "'{0}' is not compatible for '{1}'.", collection.GetType(), typeof( T ) ), "collection" );
			}

			this.UnpackToCore( unpacker, ( T )collection );
		}

		byte[] IMessagePackSingleObjectSerializer.PackSingleObject( object objectTree )
		{
			if ( ( typeof( T ).GetIsValueType() && !( objectTree is T ) )
				|| ( ( objectTree != null && !( objectTree is T ) ) ) )
			{
				throw new ArgumentException( String.Format( CultureInfo.CurrentCulture, "'{0}' is not compatible for '{1}'.", objectTree == null ? "(null)" : objectTree.GetType().FullName, typeof( T ) ), "objectTree" );
			}

			return this.PackSingleObject( ( T )objectTree );
		}

		object IMessagePackSingleObjectSerializer.UnpackSingleObject( byte[] buffer )
		{
			return this.UnpackSingleObject( buffer );
		}
	}
}
