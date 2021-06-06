using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010CA RID: 4298
	public class ProfileGameModeStatDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600BBF0 RID: 48112 RVA: 0x00395EC0 File Offset: 0x003940C0
		public ProfileGameModeStatDataModel()
		{
			base.RegisterNestedDataModel(this.m_StatValue);
		}

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x0600BBF1 RID: 48113 RVA: 0x00395FFF File Offset: 0x003941FF
		public int DataModelId
		{
			get
			{
				return 214;
			}
		}

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x0600BBF2 RID: 48114 RVA: 0x00396006 File Offset: 0x00394206
		public string DataModelDisplayName
		{
			get
			{
				return "profile_game_mode_stat";
			}
		}

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x0600BBF4 RID: 48116 RVA: 0x00396038 File Offset: 0x00394238
		// (set) Token: 0x0600BBF3 RID: 48115 RVA: 0x0039600D File Offset: 0x0039420D
		public string ModeName
		{
			get
			{
				return this.m_ModeName;
			}
			set
			{
				if (this.m_ModeName == value)
				{
					return;
				}
				this.m_ModeName = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x0600BBF6 RID: 48118 RVA: 0x00396066 File Offset: 0x00394266
		// (set) Token: 0x0600BBF5 RID: 48117 RVA: 0x00396040 File Offset: 0x00394240
		public int ModeIcon
		{
			get
			{
				return this.m_ModeIcon;
			}
			set
			{
				if (this.m_ModeIcon == value)
				{
					return;
				}
				this.m_ModeIcon = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x0600BBF8 RID: 48120 RVA: 0x00396099 File Offset: 0x00394299
		// (set) Token: 0x0600BBF7 RID: 48119 RVA: 0x0039606E File Offset: 0x0039426E
		public string StatName
		{
			get
			{
				return this.m_StatName;
			}
			set
			{
				if (this.m_StatName == value)
				{
					return;
				}
				this.m_StatName = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x0600BBFA RID: 48122 RVA: 0x003960DA File Offset: 0x003942DA
		// (set) Token: 0x0600BBF9 RID: 48121 RVA: 0x003960A1 File Offset: 0x003942A1
		public DataModelList<int> StatValue
		{
			get
			{
				return this.m_StatValue;
			}
			set
			{
				if (this.m_StatValue == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_StatValue);
				base.RegisterNestedDataModel(value);
				this.m_StatValue = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x0600BBFC RID: 48124 RVA: 0x0039610D File Offset: 0x0039430D
		// (set) Token: 0x0600BBFB RID: 48123 RVA: 0x003960E2 File Offset: 0x003942E2
		public string StatDesc
		{
			get
			{
				return this.m_StatDesc;
			}
			set
			{
				if (this.m_StatDesc == value)
				{
					return;
				}
				this.m_StatDesc = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x0600BBFD RID: 48125 RVA: 0x00396115 File Offset: 0x00394315
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BBFE RID: 48126 RVA: 0x00396120 File Offset: 0x00394320
		public int GetPropertiesHashCode()
		{
			int num = (17 * 31 + ((this.m_ModeName != null) ? this.m_ModeName.GetHashCode() : 0)) * 31;
			int modeIcon = this.m_ModeIcon;
			return (((num + this.m_ModeIcon.GetHashCode()) * 31 + ((this.m_StatName != null) ? this.m_StatName.GetHashCode() : 0)) * 31 + ((this.m_StatValue != null) ? this.m_StatValue.GetPropertiesHashCode() : 0)) * 31 + ((this.m_StatDesc != null) ? this.m_StatDesc.GetHashCode() : 0);
		}

		// Token: 0x0600BBFF RID: 48127 RVA: 0x003961B0 File Offset: 0x003943B0
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = this.m_ModeName;
				return true;
			case 1:
				value = this.m_ModeIcon;
				return true;
			case 2:
				value = this.m_StatName;
				return true;
			case 3:
				value = this.m_StatValue;
				return true;
			case 4:
				value = this.m_StatDesc;
				return true;
			default:
				value = null;
				return false;
			}
		}

		// Token: 0x0600BC00 RID: 48128 RVA: 0x00396214 File Offset: 0x00394414
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				this.ModeName = ((value != null) ? ((string)value) : null);
				return true;
			case 1:
				this.ModeIcon = ((value != null) ? ((int)value) : 0);
				return true;
			case 2:
				this.StatName = ((value != null) ? ((string)value) : null);
				return true;
			case 3:
				this.StatValue = ((value != null) ? ((DataModelList<int>)value) : null);
				return true;
			case 4:
				this.StatDesc = ((value != null) ? ((string)value) : null);
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600BC01 RID: 48129 RVA: 0x003962A4 File Offset: 0x003944A4
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
			default:
				info = default(DataModelProperty);
				return false;
			}
		}

		// Token: 0x040099DE RID: 39390
		public const int ModelId = 214;

		// Token: 0x040099DF RID: 39391
		private string m_ModeName;

		// Token: 0x040099E0 RID: 39392
		private int m_ModeIcon;

		// Token: 0x040099E1 RID: 39393
		private string m_StatName;

		// Token: 0x040099E2 RID: 39394
		private DataModelList<int> m_StatValue = new DataModelList<int>();

		// Token: 0x040099E3 RID: 39395
		private string m_StatDesc;

		// Token: 0x040099E4 RID: 39396
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "mode_name",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "mode_icon",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "stat_name",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "stat_value",
				Type = typeof(DataModelList<int>)
			},
			new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "stat_desc",
				Type = typeof(string)
			}
		};
	}
}
