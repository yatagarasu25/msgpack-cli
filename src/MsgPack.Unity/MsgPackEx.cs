using MsgPack.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MsgPack
{
	public static class MsgPackEx
	{
		public static void RegisterSerializers(string namespace_name)
		{
			RegisterSerializers(namespace_name, Assembly.GetCallingAssembly());
		}
		public static void RegisterSerializers(string namespace_name, Assembly assembly)
		{
			var types = new HashSet<Type>();
			foreach (var t in assembly.GetTypes())
				if (t.Namespace == namespace_name)
					types.Add(t);

			while (types.Count > 0) {
				var ntypes = new HashSet<Type>();
				foreach (var t in types) {
					try {
						SerializationContext.Default.Serializers.Register(t.BaseType.GetGenericArguments()[0], Activator.CreateInstance(t, SerializationContext.Default));
					}
					catch {
						ntypes.Add(t);
					}
				}
				types = ntypes;
			}
		}
	}
}
