using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x0200109B RID: 4251
	public class AchievementListDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600B880 RID: 47232 RVA: 0x00388300 File Offset: 0x00386500
		public AchievementListDataModel()
		{
			base.RegisterNestedDataModel(this.m_Achievements);
		}

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x0600B881 RID: 47233 RVA: 0x0038836B File Offset: 0x0038656B
		public int DataModelId
		{
			get
			{
				return 223;
			}
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x0600B882 RID: 47234 RVA: 0x00388372 File Offset: 0x00386572
		public string DataModelDisplayName
		{
			get
			{
				return "achievement_list";
			}
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x0600B884 RID: 47236 RVA: 0x003883B2 File Offset: 0x003865B2
		// (set) Token: 0x0600B883 RID: 47235 RVA: 0x00388379 File Offset: 0x00386579
		public DataModelList<AchievementDataModel> Achievements
		{
			get
			{
				return this.m_Achievements;
			}
			set
			{
				if (this.m_Achievements == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_Achievements);
				base.RegisterNestedDataModel(value);
				this.m_Achievements = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x0600B885 RID: 47237 RVA: 0x003883BA File Offset: 0x003865BA
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600B886 RID: 47238 RVA: 0x003883C2 File Offset: 0x003865C2
		public int GetPropertiesHashCode()
		{
			return 17 * 31 + ((this.m_Achievements != null) ? this.m_Achievements.GetPropertiesHashCode() : 0);
		}

		// Token: 0x0600B887 RID: 47239 RVA: 0x003883E0 File Offset: 0x003865E0
		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 0)
			{
				value = this.m_Achievements;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600B888 RID: 47240 RVA: 0x003883F3 File Offset: 0x003865F3
		public bool SetPropertyValue(int id, object value)
		{
			if (id == 0)
			{
				this.Achievements = ((value != null) ? ((DataModelList<AchievementDataModel>)value) : null);
				return true;
			}
			return false;
		}

		// Token: 0x0600B889 RID: 47241 RVA: 0x0038840D File Offset: 0x0038660D
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

		// Token: 0x04009884 RID: 39044
		public const int ModelId = 223;

		// Token: 0x04009885 RID: 39045
		private DataModelList<AchievementDataModel> m_Achievements = new DataModelList<AchievementDataModel>();

		// Token: 0x04009886 RID: 39046
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "achievements",
				Type = typeof(DataModelList<AchievementDataModel>)
			}
		};
	}
}
