using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x02001098 RID: 4248
	public class AchievementCategoryDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600B82E RID: 47150 RVA: 0x00386B1C File Offset: 0x00384D1C
		public AchievementCategoryDataModel()
		{
			base.RegisterNestedDataModel(this.m_Subcategories);
			base.RegisterNestedDataModel(this.m_Stats);
			base.RegisterNestedDataModel(this.m_SelectedSubcategory);
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x0600B82F RID: 47151 RVA: 0x00386C9D File Offset: 0x00384E9D
		public int DataModelId
		{
			get
			{
				return 225;
			}
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x0600B830 RID: 47152 RVA: 0x00386CA4 File Offset: 0x00384EA4
		public string DataModelDisplayName
		{
			get
			{
				return "achievement_category";
			}
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x0600B832 RID: 47154 RVA: 0x00386CD6 File Offset: 0x00384ED6
		// (set) Token: 0x0600B831 RID: 47153 RVA: 0x00386CAB File Offset: 0x00384EAB
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

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x0600B834 RID: 47156 RVA: 0x00386D09 File Offset: 0x00384F09
		// (set) Token: 0x0600B833 RID: 47155 RVA: 0x00386CDE File Offset: 0x00384EDE
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

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x0600B836 RID: 47158 RVA: 0x00386D4A File Offset: 0x00384F4A
		// (set) Token: 0x0600B835 RID: 47157 RVA: 0x00386D11 File Offset: 0x00384F11
		public AchievementSubcategoryListDataModel Subcategories
		{
			get
			{
				return this.m_Subcategories;
			}
			set
			{
				if (this.m_Subcategories == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_Subcategories);
				base.RegisterNestedDataModel(value);
				this.m_Subcategories = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x0600B838 RID: 47160 RVA: 0x00386D8B File Offset: 0x00384F8B
		// (set) Token: 0x0600B837 RID: 47159 RVA: 0x00386D52 File Offset: 0x00384F52
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

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x0600B83A RID: 47162 RVA: 0x00386DCC File Offset: 0x00384FCC
		// (set) Token: 0x0600B839 RID: 47161 RVA: 0x00386D93 File Offset: 0x00384F93
		public AchievementSubcategoryDataModel SelectedSubcategory
		{
			get
			{
				return this.m_SelectedSubcategory;
			}
			set
			{
				if (this.m_SelectedSubcategory == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_SelectedSubcategory);
				base.RegisterNestedDataModel(value);
				this.m_SelectedSubcategory = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x0600B83C RID: 47164 RVA: 0x00386DFA File Offset: 0x00384FFA
		// (set) Token: 0x0600B83B RID: 47163 RVA: 0x00386DD4 File Offset: 0x00384FD4
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

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x0600B83D RID: 47165 RVA: 0x00386E02 File Offset: 0x00385002
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600B83E RID: 47166 RVA: 0x00386E0C File Offset: 0x0038500C
		public int GetPropertiesHashCode()
		{
			int num = (((((17 * 31 + ((this.m_Name != null) ? this.m_Name.GetHashCode() : 0)) * 31 + ((this.m_Icon != null) ? this.m_Icon.GetHashCode() : 0)) * 31 + ((this.m_Subcategories != null) ? this.m_Subcategories.GetPropertiesHashCode() : 0)) * 31 + ((this.m_Stats != null) ? this.m_Stats.GetPropertiesHashCode() : 0)) * 31 + ((this.m_SelectedSubcategory != null) ? this.m_SelectedSubcategory.GetPropertiesHashCode() : 0)) * 31;
			int id = this.m_ID;
			return num + this.m_ID.GetHashCode();
		}

		// Token: 0x0600B83F RID: 47167 RVA: 0x00386EB4 File Offset: 0x003850B4
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = this.m_Name;
				return true;
			case 1:
				value = this.m_Icon;
				return true;
			case 2:
				value = this.m_Subcategories;
				return true;
			case 3:
				value = this.m_Stats;
				return true;
			case 4:
				value = this.m_SelectedSubcategory;
				return true;
			case 5:
				value = this.m_ID;
				return true;
			default:
				value = null;
				return false;
			}
		}

		// Token: 0x0600B840 RID: 47168 RVA: 0x00386F28 File Offset: 0x00385128
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				this.Name = ((value != null) ? ((string)value) : null);
				return true;
			case 1:
				this.Icon = ((value != null) ? ((string)value) : null);
				return true;
			case 2:
				this.Subcategories = ((value != null) ? ((AchievementSubcategoryListDataModel)value) : null);
				return true;
			case 3:
				this.Stats = ((value != null) ? ((AchievementStatsDataModel)value) : null);
				return true;
			case 4:
				this.SelectedSubcategory = ((value != null) ? ((AchievementSubcategoryDataModel)value) : null);
				return true;
			case 5:
				this.ID = ((value != null) ? ((int)value) : 0);
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600B841 RID: 47169 RVA: 0x00386FD0 File Offset: 0x003851D0
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

		// Token: 0x04009861 RID: 39009
		public const int ModelId = 225;

		// Token: 0x04009862 RID: 39010
		private string m_Name;

		// Token: 0x04009863 RID: 39011
		private string m_Icon;

		// Token: 0x04009864 RID: 39012
		private AchievementSubcategoryListDataModel m_Subcategories;

		// Token: 0x04009865 RID: 39013
		private AchievementStatsDataModel m_Stats;

		// Token: 0x04009866 RID: 39014
		private AchievementSubcategoryDataModel m_SelectedSubcategory;

		// Token: 0x04009867 RID: 39015
		private int m_ID;

		// Token: 0x04009868 RID: 39016
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
				PropertyDisplayName = "icon",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "subcategories",
				Type = typeof(AchievementSubcategoryListDataModel)
			},
			new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "stats",
				Type = typeof(AchievementStatsDataModel)
			},
			new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "selected_subcategory",
				Type = typeof(AchievementSubcategoryDataModel)
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
