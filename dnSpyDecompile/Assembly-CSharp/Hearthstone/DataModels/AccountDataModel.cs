using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x02001097 RID: 4247
	public class AccountDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x0600B825 RID: 47141 RVA: 0x00386A64 File Offset: 0x00384C64
		public int DataModelId
		{
			get
			{
				return 153;
			}
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x0600B826 RID: 47142 RVA: 0x00386A6B File Offset: 0x00384C6B
		public string DataModelDisplayName
		{
			get
			{
				return "account";
			}
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x0600B828 RID: 47144 RVA: 0x00386A98 File Offset: 0x00384C98
		// (set) Token: 0x0600B827 RID: 47143 RVA: 0x00386A72 File Offset: 0x00384C72
		public Locale Language
		{
			get
			{
				return this.m_Language;
			}
			set
			{
				if (this.m_Language == value)
				{
					return;
				}
				this.m_Language = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x0600B829 RID: 47145 RVA: 0x00386AA0 File Offset: 0x00384CA0
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600B82A RID: 47146 RVA: 0x00386AA8 File Offset: 0x00384CA8
		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			Locale language = this.m_Language;
			return num + this.m_Language.GetHashCode();
		}

		// Token: 0x0600B82B RID: 47147 RVA: 0x00386AC8 File Offset: 0x00384CC8
		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 0)
			{
				value = this.m_Language;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600B82C RID: 47148 RVA: 0x00386AE0 File Offset: 0x00384CE0
		public bool SetPropertyValue(int id, object value)
		{
			if (id == 0)
			{
				this.Language = ((value != null) ? ((Locale)value) : Locale.enUS);
				return true;
			}
			return false;
		}

		// Token: 0x0600B82D RID: 47149 RVA: 0x00386AFA File Offset: 0x00384CFA
		public bool GetPropertyInfo(int id, out DataModelProperty info)
		{
			if (id == 0)
			{
				info = this.Properties[0];
				return true;
			}
			info = default(DataModelProperty);
			return false;
		}

		// Token: 0x0400985E RID: 39006
		public const int ModelId = 153;

		// Token: 0x0400985F RID: 39007
		private Locale m_Language;

		// Token: 0x04009860 RID: 39008
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "language",
				Type = typeof(Locale)
			}
		};
	}
}
