using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hearthstone.UI.Tests
{
	// Token: 0x02001036 RID: 4150
	[AddComponentMenu("")]
	public class UIFrameworkTests_DynamicTargets : MonoBehaviour, IDynamicPropertyResolver
	{
		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x0600B472 RID: 46194 RVA: 0x00377EAC File Offset: 0x003760AC
		public ICollection<DynamicPropertyInfo> DynamicProperties
		{
			get
			{
				return new List<DynamicPropertyInfo>
				{
					new DynamicPropertyInfo
					{
						Id = "property",
						Name = "Property",
						Type = typeof(string),
						Value = this.m_value
					}
				};
			}
		}

		// Token: 0x0600B473 RID: 46195 RVA: 0x00377EFB File Offset: 0x003760FB
		public bool GetDynamicPropertyValue(string id, out object value)
		{
			value = this.m_value;
			return true;
		}

		// Token: 0x0600B474 RID: 46196 RVA: 0x00377F06 File Offset: 0x00376106
		public bool SetDynamicPropertyValue(string id, object value)
		{
			this.m_value = (value as string);
			return true;
		}

		// Token: 0x040096DE RID: 38622
		private string m_value;
	}
}
