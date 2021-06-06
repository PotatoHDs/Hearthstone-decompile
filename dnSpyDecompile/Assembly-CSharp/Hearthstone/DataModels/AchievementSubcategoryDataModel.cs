using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x0200109F RID: 4255
	public class AchievementSubcategoryDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600B8BC RID: 47292 RVA: 0x00388F94 File Offset: 0x00387194
		public AchievementSubcategoryDataModel()
		{
			base.RegisterNestedDataModel(this.m_Sections);
			base.RegisterNestedDataModel(this.m_Stats);
		}

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x0600B8BD RID: 47293 RVA: 0x00389109 File Offset: 0x00387309
		public int DataModelId
		{
			get
			{
				return 227;
			}
		}

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x0600B8BE RID: 47294 RVA: 0x00389110 File Offset: 0x00387310
		public string DataModelDisplayName
		{
			get
			{
				return "achievement_subcategory";
			}
		}

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x0600B8C0 RID: 47296 RVA: 0x00389142 File Offset: 0x00387342
		// (set) Token: 0x0600B8BF RID: 47295 RVA: 0x00389117 File Offset: 0x00387317
		public string Name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				if (this.m_Name == value)
				{
					return;
				}
				this.m_Name = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x0600B8C2 RID: 47298 RVA: 0x00389175 File Offset: 0x00387375
		// (set) Token: 0x0600B8C1 RID: 47297 RVA: 0x0038914A File Offset: 0x0038734A
		public string FullName
		{
			get
			{
				return this.m_FullName;
			}
			set
			{
				if (this.m_FullName == value)
				{
					return;
				}
				this.m_FullName = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x0600B8C4 RID: 47300 RVA: 0x003891A8 File Offset: 0x003873A8
		// (set) Token: 0x0600B8C3 RID: 47299 RVA: 0x0038917D File Offset: 0x0038737D
		public string Icon
		{
			get
			{
				return this.m_Icon;
			}
			set
			{
				if (this.m_Icon == value)
				{
					return;
				}
				this.m_Icon = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x0600B8C6 RID: 47302 RVA: 0x003891E9 File Offset: 0x003873E9
		// (set) Token: 0x0600B8C5 RID: 47301 RVA: 0x003891B0 File Offset: 0x003873B0
		public AchievementSectionListDataModel Sections
		{
			get
			{
				return this.m_Sections;
			}
			set
			{
				if (this.m_Sections == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_Sections);
				base.RegisterNestedDataModel(value);
				this.m_Sections = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x0600B8C8 RID: 47304 RVA: 0x0038922A File Offset: 0x0038742A
		// (set) Token: 0x0600B8C7 RID: 47303 RVA: 0x003891F1 File Offset: 0x003873F1
		public AchievementStatsDataModel Stats
		{
			get
			{
				return this.m_Stats;
			}
			set
			{
				if (this.m_Stats == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_Stats);
				base.RegisterNestedDataModel(value);
				this.m_Stats = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x0600B8CA RID: 47306 RVA: 0x00389258 File Offset: 0x00387458
		// (set) Token: 0x0600B8C9 RID: 47305 RVA: 0x00389232 File Offset: 0x00387432
		public int ID
		{
			get
			{
				return this.m_ID;
			}
			set
			{
				if (this.m_ID == value)
				{
					return;
				}
				this.m_ID = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x0600B8CB RID: 47307 RVA: 0x00389260 File Offset: 0x00387460
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600B8CC RID: 47308 RVA: 0x00389268 File Offset: 0x00387468
		public int GetPropertiesHashCode()
		{
			int num = (((((17 * 31 + ((this.m_Name != null) ? this.m_Name.GetHashCode() : 0)) * 31 + ((this.m_FullName != null) ? this.m_FullName.GetHashCode() : 0)) * 31 + ((this.m_Icon != null) ? this.m_Icon.GetHashCode() : 0)) * 31 + ((this.m_Sections != null) ? this.m_Sections.GetPropertiesHashCode() : 0)) * 31 + ((this.m_Stats != null) ? this.m_Stats.GetPropertiesHashCode() : 0)) * 31;
			int id = this.m_ID;
			return num + this.m_ID.GetHashCode();
		}

		// Token: 0x0600B8CD RID: 47309 RVA: 0x00389310 File Offset: 0x00387510
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = this.m_Name;
				return true;
			case 1:
				value = this.m_FullName;
				return true;
			case 2:
				value = this.m_Icon;
				return true;
			case 3:
				value = this.m_Sections;
				return true;
			case 4:
				value = this.m_Stats;
				return true;
			case 5:
				value = this.m_ID;
				return true;
			default:
				value = null;
				return false;
			}
		}

		// Token: 0x0600B8CE RID: 47310 RVA: 0x00389384 File Offset: 0x00387584
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				this.Name = ((value != null) ? ((string)value) : null);
				return true;
			case 1:
				this.FullName = ((value != null) ? ((string)value) : null);
				return true;
			case 2:
				this.Icon = ((value != null) ? ((string)value) : null);
				return true;
			case 3:
				this.Sections = ((value != null) ? ((AchievementSectionListDataModel)value) : null);
				return true;
			case 4:
				this.Stats = ((value != null) ? ((AchievementStatsDataModel)value) : null);
				return true;
			case 5:
				this.ID = ((value != null) ? ((int)value) : 0);
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600B8CF RID: 47311 RVA: 0x0038942C File Offset: 0x0038762C
		public bool GetPropertyInfo(int id, out DataModelProperty info)
		{
			switch (id)
			{
			case 0:
				info = this.Properties[0];
				return true;
			case 1:
				info = this.Properties[1];
				return true;
			case 2:
				info = this.Properties[2];
				return true;
			case 3:
				info = this.Properties[3];
				return true;
			case 4:
				info = this.Properties[4];
				return true;
			case 5:
				info = this.Properties[5];
				return true;
			default:
				info = default(DataModelProperty);
				return false;
			}
		}

		// Token: 0x0400989A RID: 39066
		public const int ModelId = 227;

		// Token: 0x0400989B RID: 39067
		private string m_Name;

		// Token: 0x0400989C RID: 39068
		private string m_FullName;

		// Token: 0x0400989D RID: 39069
		private string m_Icon;

		// Token: 0x0400989E RID: 39070
		private AchievementSectionListDataModel m_Sections;

		// Token: 0x0400989F RID: 39071
		private AchievementStatsDataModel m_Stats;

		// Token: 0x040098A0 RID: 39072
		private int m_ID;

		// Token: 0x040098A1 RID: 39073
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "name",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "full_name",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "icon",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "sections",
				Type = typeof(AchievementSectionListDataModel)
			},
			new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "stats",
				Type = typeof(AchievementStatsDataModel)
			},
			new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "id",
				Type = typeof(int)
			}
		};
	}
}
