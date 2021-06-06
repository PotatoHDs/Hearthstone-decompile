using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x02001009 RID: 4105
	public class ParticleSystemDynamicPropertyResolverProxy : IDynamicPropertyResolverProxy, IDynamicPropertyResolver
	{
		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x0600B2BA RID: 45754 RVA: 0x003708AE File Offset: 0x0036EAAE
		public ICollection<DynamicPropertyInfo> DynamicProperties
		{
			get
			{
				return this.m_properties.Values;
			}
		}

		// Token: 0x0600B2BB RID: 45755 RVA: 0x003708BB File Offset: 0x0036EABB
		public void SetTarget(object target)
		{
			this.m_particleSystem = (ParticleSystem)target;
			this.m_properties.Clear();
			this.GeneratePropertyData(target);
		}

		// Token: 0x0600B2BC RID: 45756 RVA: 0x003708DC File Offset: 0x0036EADC
		private void GeneratePropertyData(object target)
		{
			if (target == null)
			{
				return;
			}
			foreach (string text in ParticleSystemDynamicPropertyResolverProxy.s_propertyPathStrs)
			{
				string formattedPathId = this.GetFormattedPathId(text);
				ParticleSystemDynamicPropertyResolverProxy.PropertyTargetInfo[] targetsFromPathId = this.GetTargetsFromPathId(formattedPathId);
				PropertyInfo propertyInfo = null;
				object obj = target;
				if (targetsFromPathId.Length != 0)
				{
					propertyInfo = targetsFromPathId.Last<ParticleSystemDynamicPropertyResolverProxy.PropertyTargetInfo>().m_property;
					obj = targetsFromPathId.Last<ParticleSystemDynamicPropertyResolverProxy.PropertyTargetInfo>().m_target;
				}
				if (!(propertyInfo == null) && NestedReferenceUtils.IsSupportedType(propertyInfo.PropertyType))
				{
					DynamicPropertyInfo value = new DynamicPropertyInfo
					{
						Id = formattedPathId,
						Name = text,
						Type = propertyInfo.PropertyType,
						Value = (propertyInfo.PropertyType.IsEnum ? Convert.ToInt32(propertyInfo.GetValue(obj)) : propertyInfo.GetValue(obj))
					};
					this.m_properties.Add(formattedPathId, value);
				}
			}
		}

		// Token: 0x0600B2BD RID: 45757 RVA: 0x003709C0 File Offset: 0x0036EBC0
		private ParticleSystemDynamicPropertyResolverProxy.PropertyTargetInfo[] GetTargetsFromPathId(string path)
		{
			PropertyInfo propertyInfo = null;
			object obj = this.m_particleSystem;
			string[] array = path.Split(new char[]
			{
				'.'
			});
			ParticleSystemDynamicPropertyResolverProxy.PropertyTargetInfo[] array2 = new ParticleSystemDynamicPropertyResolverProxy.PropertyTargetInfo[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				string name = array[i];
				if (propertyInfo != null)
				{
					bool flag = propertyInfo.PropertyType.IsValueType && !propertyInfo.PropertyType.IsEnum;
					bool isClass = propertyInfo.PropertyType.IsClass;
					if (obj == null || (!flag && !isClass))
					{
						return new ParticleSystemDynamicPropertyResolverProxy.PropertyTargetInfo[0];
					}
					obj = propertyInfo.GetValue(obj);
				}
				propertyInfo = ((obj != null) ? obj.GetType().GetProperty(name) : null);
				if (propertyInfo == null || propertyInfo.IsDefined(ParticleSystemDynamicPropertyResolverProxy.s_obsoleteAttributeType))
				{
					return new ParticleSystemDynamicPropertyResolverProxy.PropertyTargetInfo[0];
				}
				array2[i].m_target = obj;
				array2[i].m_property = propertyInfo;
			}
			return array2;
		}

		// Token: 0x0600B2BE RID: 45758 RVA: 0x00370AB0 File Offset: 0x0036ECB0
		private string GetFormattedPathId(string pathStr)
		{
			string[] array = pathStr.Split(new char[]
			{
				'.'
			});
			string[] array2 = new string[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = char.ToLowerInvariant(array[i][0]).ToString() + array[i].Substring(1);
			}
			return string.Join('.'.ToString(), array2);
		}

		// Token: 0x0600B2BF RID: 45759 RVA: 0x00370B20 File Offset: 0x0036ED20
		public bool GetDynamicPropertyValue(string id, out object value)
		{
			value = null;
			DynamicPropertyInfo dynamicPropertyInfo = null;
			if (this.m_properties.TryGetValue(id, out dynamicPropertyInfo))
			{
				value = dynamicPropertyInfo.Value;
				return true;
			}
			return false;
		}

		// Token: 0x0600B2C0 RID: 45760 RVA: 0x00370B50 File Offset: 0x0036ED50
		public bool SetDynamicPropertyValue(string id, object value)
		{
			DynamicPropertyInfo dynamicPropertyInfo;
			if (this.m_properties.TryGetValue(id, out dynamicPropertyInfo))
			{
				dynamicPropertyInfo.Value = (dynamicPropertyInfo.Type.IsEnum ? Convert.ToInt32(value) : value);
				ParticleSystemDynamicPropertyResolverProxy.PropertyTargetInfo[] targetsFromPathId = this.GetTargetsFromPathId(id);
				object value2 = dynamicPropertyInfo.Value;
				for (int i = targetsFromPathId.Length - 1; i >= 0; i--)
				{
					object target = targetsFromPathId[i].m_target;
					targetsFromPathId[i].m_property.SetValue(target, value2);
					value2 = target;
				}
				return true;
			}
			return false;
		}

		// Token: 0x04009639 RID: 38457
		private const char PathDelimiter = '.';

		// Token: 0x0400963A RID: 38458
		private static readonly string[] s_propertyPathStrs = new string[]
		{
			"Main.StartColor.Color",
			"Shape.Mesh"
		};

		// Token: 0x0400963B RID: 38459
		private static Type s_obsoleteAttributeType = typeof(ObsoleteAttribute);

		// Token: 0x0400963C RID: 38460
		private Map<string, DynamicPropertyInfo> m_properties = new Map<string, DynamicPropertyInfo>();

		// Token: 0x0400963D RID: 38461
		private ParticleSystem m_particleSystem;

		// Token: 0x0200283B RID: 10299
		private struct PropertyTargetInfo
		{
			// Token: 0x0400F8DD RID: 63709
			public object m_target;

			// Token: 0x0400F8DE RID: 63710
			public PropertyInfo m_property;
		}
	}
}
