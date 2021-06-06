using System;
using System.Collections.Generic;
using System.Linq;

namespace Hearthstone.InGameMessage
{
	// Token: 0x02001151 RID: 4433
	public class GameAttributes
	{
		// Token: 0x0600C24D RID: 49741 RVA: 0x003AE148 File Offset: 0x003AC348
		public bool Create(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return false;
			}
			bool flag;
			if (this.m_attrs.TryGetValue(name, out flag))
			{
				return false;
			}
			this.m_attrs.Add(name, false);
			return true;
		}

		// Token: 0x0600C24E RID: 49742 RVA: 0x003AE17F File Offset: 0x003AC37F
		public bool Delete(string name)
		{
			return this.Exist(name) && this.m_attrs.Remove(name);
		}

		// Token: 0x0600C24F RID: 49743 RVA: 0x003AE198 File Offset: 0x003AC398
		public bool Activate(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return false;
			}
			bool flag;
			if (this.m_attrs.TryGetValue(name, out flag))
			{
				flag = true;
			}
			else
			{
				this.m_attrs.Add(name, true);
			}
			return true;
		}

		// Token: 0x0600C250 RID: 49744 RVA: 0x003AE1D1 File Offset: 0x003AC3D1
		public bool Exist(string name)
		{
			return !string.IsNullOrEmpty(name) && this.m_attrs.ContainsKey(name);
		}

		// Token: 0x0600C251 RID: 49745 RVA: 0x003AE1EC File Offset: 0x003AC3EC
		public IEnumerable<string> GetAttributions(bool activeOnly)
		{
			if (!activeOnly)
			{
				return this.m_attrs.Keys;
			}
			return from attr in this.m_attrs
			where attr.Value
			select attr.Key;
		}

		// Token: 0x0600C252 RID: 49746 RVA: 0x003AE258 File Offset: 0x003AC458
		public static GameAttributes Get()
		{
			if (GameAttributes.s_instance == null)
			{
				GameAttributes.s_instance = new GameAttributes();
			}
			return GameAttributes.s_instance;
		}

		// Token: 0x04009C95 RID: 40085
		private Dictionary<string, bool> m_attrs = new Dictionary<string, bool>();

		// Token: 0x04009C96 RID: 40086
		private static GameAttributes s_instance;
	}
}
