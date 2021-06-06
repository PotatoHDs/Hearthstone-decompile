using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Hearthstone.UI
{
	public class ParticleSystemDynamicPropertyResolverProxy : IDynamicPropertyResolverProxy, IDynamicPropertyResolver
	{
		private struct PropertyTargetInfo
		{
			public object m_target;

			public PropertyInfo m_property;
		}

		private const char PathDelimiter = '.';

		private static readonly string[] s_propertyPathStrs = new string[2] { "Main.StartColor.Color", "Shape.Mesh" };

		private static Type s_obsoleteAttributeType = typeof(ObsoleteAttribute);

		private Map<string, DynamicPropertyInfo> m_properties = new Map<string, DynamicPropertyInfo>();

		private ParticleSystem m_particleSystem;

		public ICollection<DynamicPropertyInfo> DynamicProperties => m_properties.Values;

		public void SetTarget(object target)
		{
			m_particleSystem = (ParticleSystem)target;
			m_properties.Clear();
			GeneratePropertyData(target);
		}

		private void GeneratePropertyData(object target)
		{
			if (target == null)
			{
				return;
			}
			string[] array = s_propertyPathStrs;
			foreach (string text in array)
			{
				string formattedPathId = GetFormattedPathId(text);
				PropertyTargetInfo[] targetsFromPathId = GetTargetsFromPathId(formattedPathId);
				PropertyInfo propertyInfo = null;
				object obj = target;
				if (targetsFromPathId.Length != 0)
				{
					propertyInfo = targetsFromPathId.Last().m_property;
					obj = targetsFromPathId.Last().m_target;
				}
				if (!(propertyInfo == null) && NestedReferenceUtils.IsSupportedType(propertyInfo.PropertyType))
				{
					DynamicPropertyInfo value = new DynamicPropertyInfo
					{
						Id = formattedPathId,
						Name = text,
						Type = propertyInfo.PropertyType,
						Value = (propertyInfo.PropertyType.IsEnum ? ((object)Convert.ToInt32(propertyInfo.GetValue(obj))) : propertyInfo.GetValue(obj))
					};
					m_properties.Add(formattedPathId, value);
				}
			}
		}

		private PropertyTargetInfo[] GetTargetsFromPathId(string path)
		{
			PropertyInfo propertyInfo = null;
			object obj = m_particleSystem;
			string[] array = path.Split('.');
			PropertyTargetInfo[] array2 = new PropertyTargetInfo[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				string name = array[i];
				if (propertyInfo != null)
				{
					bool flag = propertyInfo.PropertyType.IsValueType && !propertyInfo.PropertyType.IsEnum;
					bool isClass = propertyInfo.PropertyType.IsClass;
					if (obj == null || (!flag && !isClass))
					{
						return new PropertyTargetInfo[0];
					}
					obj = propertyInfo.GetValue(obj);
				}
				propertyInfo = obj?.GetType().GetProperty(name);
				if (propertyInfo == null || propertyInfo.IsDefined(s_obsoleteAttributeType))
				{
					return new PropertyTargetInfo[0];
				}
				array2[i].m_target = obj;
				array2[i].m_property = propertyInfo;
			}
			return array2;
		}

		private string GetFormattedPathId(string pathStr)
		{
			string[] array = pathStr.Split('.');
			string[] array2 = new string[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = char.ToLowerInvariant(array[i][0]) + array[i].Substring(1);
			}
			return string.Join('.'.ToString(), array2);
		}

		public bool GetDynamicPropertyValue(string id, out object value)
		{
			value = null;
			DynamicPropertyInfo value2 = null;
			if (m_properties.TryGetValue(id, out value2))
			{
				value = value2.Value;
				return true;
			}
			return false;
		}

		public bool SetDynamicPropertyValue(string id, object value)
		{
			if (m_properties.TryGetValue(id, out var value2))
			{
				value2.Value = (value2.Type.IsEnum ? ((object)Convert.ToInt32(value)) : value);
				PropertyTargetInfo[] targetsFromPathId = GetTargetsFromPathId(id);
				object value3 = value2.Value;
				object obj = null;
				for (int num = targetsFromPathId.Length - 1; num >= 0; num--)
				{
					obj = targetsFromPathId[num].m_target;
					targetsFromPathId[num].m_property.SetValue(obj, value3);
					value3 = obj;
				}
				return true;
			}
			return false;
		}
	}
}
