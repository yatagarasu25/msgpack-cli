#region -- License Terms --
//
// MessagePack for CLI
//
// Copyright (C) 2010-2014 FUJIWARA, Yusuke
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

namespace MsgPack.Serialization.AbstractSerializers
{
	/// <summary>
	///		Defines common interfaces and features for context objects for serializer generation.
	/// </summary>
	/// <typeparam name="TConstruct">The contextType of the code construct for serializer builder.</typeparam>
	internal abstract class SerializerGenerationContext<TConstruct>
	{
		/// <summary>
		///		Gets the serialization context which holds various serialization configuration.
		/// </summary>
		/// <value>
		///		The serialization context. This value will not be <c>null</c>.
		/// </value>
		public SerializationContext SerializationContext { get; private set; }

		/// <summary>
		///		Gets the code construct which represents the argument for the packer.
		/// </summary>
		/// <value>
		///		The code construct which represents the argument for the packer.
		///		This value will not be <c>null</c>.
		/// </value>
		public TConstruct Packer { get; protected set; }

		/// <summary>
		///		Gets the code construct which represents the argument for the packing target object tree root.
		/// </summary>
		/// <returns>
		///		The code construct which represents the argument for the packing target object tree root.
		///		This value will not be <c>null</c>.
		/// </returns>
		public TConstruct PackToTarget { get; protected set; }

		/// <summary>
		///		Gets the code construct which represents the argument for the unpacker.
		/// </summary>
		/// <value>
		///		The code construct which represents the argument for the unpacker.
		///		This value will not be <c>null</c>.
		/// </value>
		public TConstruct Unpacker { get; protected set; }

		/// <summary>
		///		Gets the code construct which represents the argument for the collection which will hold unpacked items.
		/// </summary>
		/// <returns>
		///		The code construct which represents the argument for the collection which will hold unpacked items.
		///		This value will not be <c>null</c>.
		/// </returns>
		public TConstruct UnpackToTarget { get; protected set; }

		/// <summary>
		///		Gets the configured nil-implication for collection items.
		/// </summary>
		/// <value>
		///		The configured nil-implication for collection items.
		/// </value>
		public NilImplication CollectionItemNilImplication { get; private set; }

		/// <summary>
		///		Gets the configured nil-implication for dictionary keys.
		/// </summary>
		/// <value>
		///		The configured nil-implication for dictionary keys.
		/// </value>
		public NilImplication DictionaryKeyNilImplication { get; private set; }

		// NOTE: Missing map value is MemberDefault

		/// <summary>
		///		Gets the configured nil-implication for tuple items.
		/// </summary>
		/// <value>
		///		The configured nil-implication for tuple items.
		/// </value>
		public NilImplication TupleItemNilImplication { get; private set; }

		/// <summary>
		///		Initializes a new instance of the <see cref="SerializerGenerationContext{TConstruct}"/> class.
		/// </summary>
		/// <param name="context">The serialization context.</param>
		protected SerializerGenerationContext( SerializationContext context )
		{
			this.SerializationContext = context;
			this.CollectionItemNilImplication = NilImplication.Null;
			this.DictionaryKeyNilImplication = NilImplication.Prohibit;
			this.TupleItemNilImplication = NilImplication.Null;
		}

		/// <summary>
		///		Resets internal states for specified target type.
		/// </summary>
		/// <param name="targetType">Type of the serialization target.</param>
		public void Reset( Type targetType )
		{
			this.ResetCore( targetType );
		}

		/// <summary>
		///		Resets internal states for specified target type.
		/// </summary>
		/// <param name="targetType">Type of the serialization target.</param>
		protected abstract void ResetCore( Type targetType );
	}
}