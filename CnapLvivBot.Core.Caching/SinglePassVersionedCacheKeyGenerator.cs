using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CnapLvivBot.Core.Caching
{
	public class SinglePassVersionedCacheKeyGenerator : VersionedCacheKeyGenerator
	{
		protected override uint CalcTypeHash(Type t)
		{
			var result = StringHash(t.FullName);

			if (t.Namespace.StartsWith("System") && !t.IsGenericType)
				return result;
			if (t.IsInterface)
				return result;
			if (t.IsArray)
				return result * 2017 + GetTypeHash(t.GetElementType());
			if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Dictionary<,>))
			{
				var keyType = t.GetGenericArguments()[0];
				var valueType = t.GetGenericArguments()[1];
				result = result * 2017 + GetTypeHash(keyType);
				return result * 2017 + GetTypeHash(valueType);
			}
			if (t.IsEnum)
			{
				foreach (var name in t.GetEnumNames())
					result = result * 2017 + StringHash(name);
				foreach (var value in t.GetEnumValues())
					result = result * 2017 + StringHash(value.ToString());
				return result;
			}
			var ti = t.GetTypeInfo();
			foreach (var field in ti.DeclaredFields.Where(f => !f.IsStatic && !f.IsInitOnly).OrderBy(x => x.Name))
				result = result * 2017 + StringHash(field.FieldType + field.Name);
			foreach (var property in ti.DeclaredProperties
				.Where(p => p.GetMethod != null && !p.GetMethod.IsStatic && p.SetMethod != null).OrderBy(x => x.Name))
				result = result * 2017 + StringHash(property.PropertyType + property.Name);
			return result * 2017 + GetTypeHash(ti.BaseType);
		}
	}
}