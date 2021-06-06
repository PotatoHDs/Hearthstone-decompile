using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x0200109D RID: 4253
	public class AchievementSectionListDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600B89E RID: 47262 RVA: 0x00388960 File Offset: 0x00386B60
		public AchievementSectionListDataModel()
		{
			base.RegisterNestedDataModel(this.m_Sections);
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x0600B89F RID: 47263 RVA: 0x003889CB File Offset: 0x00386BCB
		public int DataModelId
		{
			get
			{
				return 228;
			}
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x0600B8A0 RID: 47264 RVA: 0x003889D2 File Offset: 0x00386BD2
		public string DataModelDisplayName
		{
			get
			{
				return "achievement_section_list";
			}
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x0600B8A2 RID: 47266 RVA: 0x00388A12 File Offset: 0x00386C12
		// (set) Token: 0x0600B8A1 RID: 47265 RVA: 0x003889D9 File Offset: 0x00386BD9
		public DataModelList<AchievementSectionDataModel> Sections
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

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x0600B8A3 RID: 47267 RVA: 0x00388A1A File Offset: 0x00386C1A
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600B8A4 RID: 47268 RVA: 0x00388A22 File Offset: 0x00386C22
		public int GetPropertiesHashCode()
		{
			return 17 * 31 + ((this.m_Sections != null) ? this.m_Sections.GetPropertiesHashCode() : 0);
		}

		// Token: 0x0600B8A5 RID: 47269 RVA: 0x00388A40 File Offset: 0x00386C40
		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 0)
			{
				value = this.m_Sections;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600B8A6 RID: 47270 RVA: 0x00388A53 File Offset: 0x00386C53
		public bool SetPropertyValue(int id, object value)
		{
			if (id == 0)
			{
				this.Sections = ((value != null) ? ((DataModelList<AchievementSectionDataModel>)value) : null);
				return true;
			}
			return false;
		}

		// Token: 0x0600B8A7 RID: 47271 RVA: 0x00388A6D File Offset: 0x00386C6D
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

		// Token: 0x0400988F RID: 39055
		public const int ModelId = 228;

		// Token: 0x04009890 RID: 39056
		private DataModelList<AchievementSectionDataModel> m_Sections = new DataModelList<AchievementSectionDataModel>();

		// Token: 0x04009891 RID: 39057
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "sections",
				Type = typeof(DataModelList<AchievementSectionDataModel>)
			}
		};
	}
}
