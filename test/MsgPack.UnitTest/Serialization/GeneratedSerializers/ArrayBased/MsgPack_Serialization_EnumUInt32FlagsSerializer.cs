﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.34014
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MsgPack.Serialization.GeneratedSerializers.ArrayBased {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("MsgPack.Serialization.CodeDomSerializers.CodeDomSerializerBuilder", "0.5.0.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public class MsgPack_Serialization_EnumUInt32FlagsSerializer : MsgPack.Serialization.EnumMessagePackSerializer<MsgPack.Serialization.EnumUInt32Flags> {
        
        private System.Reflection.MethodBase _methodBaseComplexType_get_History0;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_AddOnlyCollection_DateTimeField1;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_AddOnlyCollection_MessagePackObjectField2;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_AddOnlyCollection_ObjectField3;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_ArrayListField4;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_Collection_MessagePackObjectField5;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_CollectionDateTimeField6;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_CollectionObjectField7;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_Dictionary_MessagePackObject_MessagePackObjectField8;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_DictionaryObjectObjectField9;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_DictionaryStringDateTimeField10;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_HashSet_MessagePackObjectField11;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_HashSetDateTimeField12;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_HashSetObjectField13;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_HashtableField14;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_ICollection_MessagePackObjectField15;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_ICollectionDateTimeField16;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_ICollectionObjectField17;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_IDictionary_MessagePackObject_MessagePackObjectField18;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_IDictionaryObjectObjectField19;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_IDictionaryStringDateTimeField20;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_IList_MessagePackObjectField21;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_IListDateTimeField22;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_IListObjectField23;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_ISet_MessagePackObjectField24;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_ISetDateTimeField25;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_ISetObjectField26;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_List_MessagePackObjectField27;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_ListDateTimeField28;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_ListObjectField29;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_ObservableCollection_MessagePackObjectField30;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_ObservableCollectionDateTimeField31;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_ObservableCollectionObjectField32;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_StringKeyedCollection_DateTimeField33;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_StringKeyedCollection_MessagePackObjectField34;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_StringKeyedCollection_ObjectField35;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeWithDataContract_get_History36;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeWithDataContractWithOrder_get_History37;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeWithNonSerialized_get_History38;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeWithoutAnyAttribute_get_History39;
        
        public MsgPack_Serialization_EnumUInt32FlagsSerializer(MsgPack.Serialization.SerializationContext context) : 
                base(context, MsgPack.Serialization.EnumSerializationMethod.ByName) {
            this._methodBaseComplexType_get_History0 = typeof(MsgPack.Serialization.ComplexType).GetMethod("get_History", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_AddOnlyCollection_DateTimeField1 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_AddOnlyCollection_DateTimeField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_AddOnlyCollection_MessagePackObjectField2 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_AddOnlyCollection_MessagePackObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_AddOnlyCollection_ObjectField3 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_AddOnlyCollection_ObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_ArrayListField4 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_ArrayListField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_Collection_MessagePackObjectField5 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_Collection_MessagePackObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_CollectionDateTimeField6 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_CollectionDateTimeField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_CollectionObjectField7 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_CollectionObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_Dictionary_MessagePackObject_MessagePackObjectField8 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_Dictionary_MessagePackObject_MessagePackObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_DictionaryObjectObjectField9 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_DictionaryObjectObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_DictionaryStringDateTimeField10 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_DictionaryStringDateTimeField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_HashSet_MessagePackObjectField11 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_HashSet_MessagePackObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_HashSetDateTimeField12 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_HashSetDateTimeField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_HashSetObjectField13 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_HashSetObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_HashtableField14 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_HashtableField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_ICollection_MessagePackObjectField15 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_ICollection_MessagePackObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_ICollectionDateTimeField16 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_ICollectionDateTimeField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_ICollectionObjectField17 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_ICollectionObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_IDictionary_MessagePackObject_MessagePackObjectField18 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_IDictionary_MessagePackObject_MessagePackObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_IDictionaryObjectObjectField19 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_IDictionaryObjectObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_IDictionaryStringDateTimeField20 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_IDictionaryStringDateTimeField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_IList_MessagePackObjectField21 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_IList_MessagePackObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_IListDateTimeField22 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_IListDateTimeField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_IListObjectField23 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_IListObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_ISet_MessagePackObjectField24 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_ISet_MessagePackObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_ISetDateTimeField25 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_ISetDateTimeField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_ISetObjectField26 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_ISetObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_List_MessagePackObjectField27 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_List_MessagePackObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_ListDateTimeField28 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_ListDateTimeField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_ListObjectField29 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_ListObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_ObservableCollection_MessagePackObjectField30 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_ObservableCollection_MessagePackObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_ObservableCollectionDateTimeField31 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_ObservableCollectionDateTimeField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_ObservableCollectionObjectField32 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_ObservableCollectionObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_StringKeyedCollection_DateTimeField33 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_StringKeyedCollection_DateTimeField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_StringKeyedCollection_MessagePackObjectField34 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_StringKeyedCollection_MessagePackObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_StringKeyedCollection_ObjectField35 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_StringKeyedCollection_ObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeWithDataContract_get_History36 = typeof(MsgPack.Serialization.ComplexTypeWithDataContract).GetMethod("get_History", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeWithDataContractWithOrder_get_History37 = typeof(MsgPack.Serialization.ComplexTypeWithDataContractWithOrder).GetMethod("get_History", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeWithNonSerialized_get_History38 = typeof(MsgPack.Serialization.ComplexTypeWithNonSerialized).GetMethod("get_History", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeWithoutAnyAttribute_get_History39 = typeof(MsgPack.Serialization.ComplexTypeWithoutAnyAttribute).GetMethod("get_History", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
        }
        
        protected internal override void PackUnderlyingValueTo(MsgPack.Packer packer, MsgPack.Serialization.EnumUInt32Flags enumValue) {
            packer.Pack(((uint)(enumValue)));
        }
        
        protected internal override MsgPack.Serialization.EnumUInt32Flags UnpackFromUnderlyingValue(MsgPack.MessagePackObject messagePackObject) {
            return ((MsgPack.Serialization.EnumUInt32Flags)(messagePackObject.AsUInt32()));
        }
        
        private static T @__Conditional<T>(bool condition, T whenTrue, T whenFalse)
         {
            if (condition) {
                return whenTrue;
            }
            else {
                return whenFalse;
            }
        }
    }
}
