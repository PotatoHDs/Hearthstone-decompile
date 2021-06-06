using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010D2 RID: 4306
	public class QuestListDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600BCA0 RID: 48288 RVA: 0x00398F04 File Offset: 0x00397104
		public QuestListDataModel()
		{
			base.RegisterNestedDataModel(this.m_Quests);
		}

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x0600BCA1 RID: 48289 RVA: 0x00398F6F File Offset: 0x0039716F
		public int DataModelId
		{
			get
			{
				return 208;
			}
		}

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x0600BCA2 RID: 48290 RVA: 0x00398F76 File Offset: 0x00397176
		public string DataModelDisplayName
		{
			get
			{
				return "quest_list";
			}
		}

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x0600BCA4 RID: 48292 RVA: 0x00398FB6 File Offset: 0x003971B6
		// (set) Token: 0x0600BCA3 RID: 48291 RVA: 0x00398F7D File Offset: 0x0039717D
		public DataModelList<QuestDataModel> Quests
		{
			get
			{
				return this.m_Quests;
			}
			set
			{
				if (this.m_Quests == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_Quests);
				base.RegisterNestedDataModel(value);
				this.m_Quests = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x0600BCA5 RID: 48293 RVA: 0x00398FBE File Offset: 0x003971BE
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BCA6 RID: 48294 RVA: 0x00398FC6 File Offset: 0x003971C6
		public int GetPropertiesHashCode()
		{
			return 17 * 31 + ((this.m_Quests != null) ? this.m_Quests.GetPropertiesHashCode() : 0);
		}

		// Token: 0x0600BCA7 RID: 48295 RVA: 0x00398FE4 File Offset: 0x003971E4
		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 0)
			{
				value = this.m_Quests;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600BCA8 RID: 48296 RVA: 0x00398FF7 File Offset: 0x003971F7
		public bool SetPropertyValue(int id, object value)
		{
			if (id == 0)
			{
				this.Quests = ((value != null) ? ((DataModelList<QuestDataModel>)value) : null);
				return true;
			}
			return false;
		}

		// Token: 0x0600BCA9 RID: 48297 RVA: 0x00399011 File Offset: 0x00397211
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

		// Token: 0x04009A26 RID: 39462
		public const int ModelId = 208;

		// Token: 0x04009A27 RID: 39463
		private DataModelList<QuestDataModel> m_Quests = new DataModelList<QuestDataModel>();

		// Token: 0x04009A28 RID: 39464
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "quests",
				Type = typeof(DataModelList<QuestDataModel>)
			}
		};
	}
}
