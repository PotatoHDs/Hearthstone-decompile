using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010BC RID: 4284
	public class JournalMetaDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x0600BB0D RID: 47885 RVA: 0x00392B0C File Offset: 0x00390D0C
		public int DataModelId
		{
			get
			{
				return 279;
			}
		}

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x0600BB0E RID: 47886 RVA: 0x00392B13 File Offset: 0x00390D13
		public string DataModelDisplayName
		{
			get
			{
				return "journal_meta";
			}
		}

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x0600BB10 RID: 47888 RVA: 0x00392B40 File Offset: 0x00390D40
		// (set) Token: 0x0600BB0F RID: 47887 RVA: 0x00392B1A File Offset: 0x00390D1A
		public int TabIndex
		{
			get
			{
				return this.m_TabIndex;
			}
			set
			{
				if (this.m_TabIndex == value)
				{
					return;
				}
				this.m_TabIndex = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x0600BB11 RID: 47889 RVA: 0x00392B48 File Offset: 0x00390D48
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BB12 RID: 47890 RVA: 0x00392B50 File Offset: 0x00390D50
		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			int tabIndex = this.m_TabIndex;
			return num + this.m_TabIndex.GetHashCode();
		}

		// Token: 0x0600BB13 RID: 47891 RVA: 0x00392B6A File Offset: 0x00390D6A
		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 0)
			{
				value = this.m_TabIndex;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600BB14 RID: 47892 RVA: 0x00392B82 File Offset: 0x00390D82
		public bool SetPropertyValue(int id, object value)
		{
			if (id == 0)
			{
				this.TabIndex = ((value != null) ? ((int)value) : 0);
				return true;
			}
			return false;
		}

		// Token: 0x0600BB15 RID: 47893 RVA: 0x00392B9C File Offset: 0x00390D9C
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

		// Token: 0x04009988 RID: 39304
		public const int ModelId = 279;

		// Token: 0x04009989 RID: 39305
		private int m_TabIndex;

		// Token: 0x0400998A RID: 39306
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "tab_index",
				Type = typeof(int)
			}
		};
	}
}
