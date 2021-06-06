using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x02001099 RID: 4249
	public class AchievementCategoryListDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600B842 RID: 47170 RVA: 0x00387080 File Offset: 0x00385280
		public AchievementCategoryListDataModel()
		{
			base.RegisterNestedDataModel(this.m_Categories);
			base.RegisterNestedDataModel(this.m_Stats);
		}

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x0600B843 RID: 47171 RVA: 0x0038712C File Offset: 0x0038532C
		public int DataModelId
		{
			get
			{
				return 239;
			}
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x0600B844 RID: 47172 RVA: 0x00387133 File Offset: 0x00385333
		public string DataModelDisplayName
		{
			get
			{
				return "achievement_category_list";
			}
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x0600B846 RID: 47174 RVA: 0x00387173 File Offset: 0x00385373
		// (set) Token: 0x0600B845 RID: 47173 RVA: 0x0038713A File Offset: 0x0038533A
		public DataModelList<AchievementCategoryDataModel> Categories
		{
			get
			{
				return this.m_Categories;
			}
			set
			{
				if (this.m_Categories == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_Categories);
				base.RegisterNestedDataModel(value);
				this.m_Categories = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x0600B848 RID: 47176 RVA: 0x003871B4 File Offset: 0x003853B4
		// (set) Token: 0x0600B847 RID: 47175 RVA: 0x0038717B File Offset: 0x0038537B
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

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x0600B849 RID: 47177 RVA: 0x003871BC File Offset: 0x003853BC
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600B84A RID: 47178 RVA: 0x003871C4 File Offset: 0x003853C4
		public int GetPropertiesHashCode()
		{
			return (17 * 31 + ((this.m_Categories != null) ? this.m_Categories.GetPropertiesHashCode() : 0)) * 31 + ((this.m_Stats != null) ? this.m_Stats.GetPropertiesHashCode() : 0);
		}

		// Token: 0x0600B84B RID: 47179 RVA: 0x003871FC File Offset: 0x003853FC
		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 0)
			{
				value = this.m_Categories;
				return true;
			}
			if (id != 1)
			{
				value = null;
				return false;
			}
			value = this.m_Stats;
			return true;
		}

		// Token: 0x0600B84C RID: 47180 RVA: 0x0038721F File Offset: 0x0038541F
		public bool SetPropertyValue(int id, object value)
		{
			if (id == 0)
			{
				this.Categories = ((value != null) ? ((DataModelList<AchievementCategoryDataModel>)value) : null);
				return true;
			}
			if (id != 1)
			{
				return false;
			}
			this.Stats = ((value != null) ? ((AchievementStatsDataModel)value) : null);
			return true;
		}

		// Token: 0x0600B84D RID: 47181 RVA: 0x00387253 File Offset: 0x00385453
		public bool GetPropertyInfo(int id, out DataModelProperty info)
		{
			if (id == 0)
			{
				info = this.Properties[0];
				return true;
			}
			if (id != 1)
			{
				info = default(DataModelProperty);
				return false;
			}
			info = this.Properties[1];
			return true;
		}

		// Token: 0x04009869 RID: 39017
		public const int ModelId = 239;

		// Token: 0x0400986A RID: 39018
		private DataModelList<AchievementCategoryDataModel> m_Categories = new DataModelList<AchievementCategoryDataModel>();

		// Token: 0x0400986B RID: 39019
		private AchievementStatsDataModel m_Stats;

		// Token: 0x0400986C RID: 39020
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "categories",
				Type = typeof(DataModelList<AchievementCategoryDataModel>)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "stats",
				Type = typeof(AchievementStatsDataModel)
			}
		};
	}
}
