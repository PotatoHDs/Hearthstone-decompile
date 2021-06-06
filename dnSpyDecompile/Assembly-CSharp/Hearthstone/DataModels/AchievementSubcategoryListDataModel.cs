using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010A0 RID: 4256
	public class AchievementSubcategoryListDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600B8D0 RID: 47312 RVA: 0x003894DC File Offset: 0x003876DC
		public AchievementSubcategoryListDataModel()
		{
			base.RegisterNestedDataModel(this.m_Subcategories);
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x0600B8D1 RID: 47313 RVA: 0x00389547 File Offset: 0x00387747
		public int DataModelId
		{
			get
			{
				return 261;
			}
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x0600B8D2 RID: 47314 RVA: 0x0038954E File Offset: 0x0038774E
		public string DataModelDisplayName
		{
			get
			{
				return "achievement_subcategory_list";
			}
		}

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x0600B8D4 RID: 47316 RVA: 0x0038958E File Offset: 0x0038778E
		// (set) Token: 0x0600B8D3 RID: 47315 RVA: 0x00389555 File Offset: 0x00387755
		public DataModelList<AchievementSubcategoryDataModel> Subcategories
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

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x0600B8D5 RID: 47317 RVA: 0x00389596 File Offset: 0x00387796
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600B8D6 RID: 47318 RVA: 0x0038959E File Offset: 0x0038779E
		public int GetPropertiesHashCode()
		{
			return 17 * 31 + ((this.m_Subcategories != null) ? this.m_Subcategories.GetPropertiesHashCode() : 0);
		}

		// Token: 0x0600B8D7 RID: 47319 RVA: 0x003895BC File Offset: 0x003877BC
		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 0)
			{
				value = this.m_Subcategories;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600B8D8 RID: 47320 RVA: 0x003895CF File Offset: 0x003877CF
		public bool SetPropertyValue(int id, object value)
		{
			if (id == 0)
			{
				this.Subcategories = ((value != null) ? ((DataModelList<AchievementSubcategoryDataModel>)value) : null);
				return true;
			}
			return false;
		}

		// Token: 0x0600B8D9 RID: 47321 RVA: 0x003895E9 File Offset: 0x003877E9
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

		// Token: 0x040098A2 RID: 39074
		public const int ModelId = 261;

		// Token: 0x040098A3 RID: 39075
		private DataModelList<AchievementSubcategoryDataModel> m_Subcategories = new DataModelList<AchievementSubcategoryDataModel>();

		// Token: 0x040098A4 RID: 39076
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "subcategories",
				Type = typeof(DataModelList<AchievementSubcategoryDataModel>)
			}
		};
	}
}
