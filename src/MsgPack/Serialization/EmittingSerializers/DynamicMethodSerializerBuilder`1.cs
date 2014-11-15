#region -- License Terms --
//
// MessagePack for CLI
//
// Copyright (C) 2010-2013 FUJIWARA, Yusuke
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

namespace MsgPack.Serialization.EmittingSerializers
{
	/// <summary>
	///		An implementation of <see cref="MsgPack.Serialization.AbstractSerializers.SerializerBuilder{TContext,TConstruct,TObject}"/> using dynamic methods.
	/// </summary>
	/// <typeparam name="TObject">The type of the serializing object.</typeparam>
	internal sealed class DynamicMethodSerializerBuilder<TObject> : ILEmittingSerializerBuilder<DynamicMethodEmittingContext, TObject>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DynamicMethodSerializerBuilder{TObject}"/> class.
		/// </summary>
		public DynamicMethodSerializerBuilder() { }

		protected override DynamicMethodEmittingContext CreateCodeGenerationContextForSerializerCreation( SerializationContext context )
		{
			return
				new DynamicMethodEmittingContext(
					context,
					typeof( TObject ),
					() => SerializationMethodGeneratorManager.Get().CreateEmitter( typeof( TObject ), EmitterFlavor.ContextBased ),
					() => SerializationMethodGeneratorManager.Get().CreateEnumEmitter( typeof( TObject ), EmitterFlavor.ContextBased )
				);
		}

		protected override ILConstruct EmitThisReferenceExpression( DynamicMethodEmittingContext context )
		{
			throw new NotSupportedException();
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "0", Justification = "Asserted internally" )]
		protected override ILConstruct EmitInvokeUnpackTo( DynamicMethodEmittingContext context, ILConstruct unpacker, ILConstruct collection )
		{
			return
				this.EmitInvokeVoidMethod(
					context,
					this.EmitInvokeMethodExpression(
						context,
						context.Context,
						Metadata._SerializationContext.GetSerializer1_Method.MakeGenericMethod( typeof( TObject ) )
					),
					typeof( MessagePackSerializer<TObject> ).GetMethod( "UnpackTo" ),
					unpacker,
					collection
				);
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "0", Justification = "Asserted internally" )]
		protected override ILConstruct EmitGetSerializerExpression( DynamicMethodEmittingContext context, Type targetType, SerializingMember? memberInfo )
		{
			return
				memberInfo == null || !targetType.GetIsEnum()
					? this.EmitInvokeMethodExpression(
						context,
						context.Context,
						Metadata._SerializationContext.GetSerializer1_Method.MakeGenericMethod( targetType )
					)
					: this.EmitInvokeMethodExpression(
						context,
						context.Context,
						Metadata._SerializationContext.GetSerializer1_Parameter_Method.MakeGenericMethod( targetType ),
						this.EmitBoxExpression( 
							context,
							typeof( EnumSerializationMethod ),
							this.EmitInvokeMethodExpression( 
								context,
								null,
								Metadata._EnumMessagePackSerializerHelpers.DetermineEnumSerializationMethodMethod,
								context.Context,
								this.EmitTypeOfExpression( context, targetType ),
								this.MakeEnumLiteral(
									context,
									typeof( EnumMemberSerializationMethod ),
									memberInfo.Value.GetEnumMemberSerializationMethod()
								)
							)
						)
					);
		}
	}
}