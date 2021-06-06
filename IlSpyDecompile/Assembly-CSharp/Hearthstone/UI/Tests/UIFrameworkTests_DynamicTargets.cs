using System.Collections.Generic;
using UnityEngine;

namespace Hearthstone.UI.Tests
{
	[AddComponentMenu("")]
	public class UIFrameworkTests_DynamicTargets : MonoBehaviour, IDynamicPropertyResolver
	{
		private string m_value;

		public ICollection<DynamicPropertyInfo> DynamicProperties => new List<DynamicPropertyInfo>
		{
			new DynamicPropertyInfo
			{
				Id = "property",
				Name = "Property",
				Type = typeof(string),
				Value = m_value
			}
		};

		public bool GetDynamicPropertyValue(string id, out object value)
		{
			value = m_value;
			return true;
		}

		public bool SetDynamicPropertyValue(string id, object value)
		{
			m_value = value as string;
			return true;
		}
	}
}
