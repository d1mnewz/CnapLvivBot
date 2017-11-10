using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CnapLvivBot.Core.Caching
{
	[Serializable]
	public class VersionedCacheKeyGenerator : ICacheKeyGenerator
	{
		protected const string ReplaceExpression = @"\s+";

		protected readonly ConcurrentDictionary<Type, uint> TypeHash = new ConcurrentDictionary<Type, uint>();
		public string Prefix { get; set; }

		public virtual string Generate<T>(string id)
		{
			var type = typeof(T);
			return Prefix + type.Name + "#" + GetTypeHash(type) + "#" + Regex.Replace(id, ReplaceExpression, "-");
		}

		protected uint GetTypeHash(Type t)
		{
			return TypeHash.GetOrAdd(t, CalcTypeHash);
		}

		protected static uint StringHash(string s)
		{
			return s.Aggregate<char, uint>(0, (current, c) => current * 37 + c);
		}

		protected virtual uint CalcTypeHash(Type t)
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
			{
				result = result * 2017 + GetTypeHash(field.FieldType);
				result = result * 2017 + StringHash(field.Name);
			}
			foreach (var property in ti.DeclaredProperties
				.Where(p => p.GetMethod != null && !p.GetMethod.IsStatic && p.SetMethod != null).OrderBy(x => x.Name))
			{
				result = result * 2017 + GetTypeHash(property.PropertyType);
				result = result * 2017 + StringHash(property.Name);
			}
			return result * 2017 + GetTypeHash(ti.BaseType);
		}
	}
}