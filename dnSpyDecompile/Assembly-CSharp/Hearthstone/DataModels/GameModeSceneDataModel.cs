using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010B7 RID: 4279
	public class GameModeSceneDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600BAC8 RID: 47816 RVA: 0x00391DA4 File Offset: 0x0038FFA4
		public GameModeSceneDataModel()
		{
			base.RegisterNestedDataModel(this.m_GameModeButtons);
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x0600BAC9 RID: 47817 RVA: 0x00391E44 File Offset: 0x00390044
		public int DataModelId
		{
			get
			{
				return 173;
			}
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x0600BACA RID: 47818 RVA: 0x00391E4B File Offset: 0x0039004B
		public string DataModelDisplayName
		{
			get
			{
				return "gamemodescene";
			}
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x0600BACC RID: 47820 RVA: 0x00391E8B File Offset: 0x0039008B
		// (set) Token: 0x0600BACB RID: 47819 RVA: 0x00391E52 File Offset: 0x00390052
		public DataModelList<GameModeButtonDataModel> GameModeButtons
		{
			get
			{
				return this.m_GameModeButtons;
			}
			set
			{
				if (this.m_GameModeButtons == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_GameModeButtons);
				base.RegisterNestedDataModel(value);
				this.m_GameModeButtons = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x0600BACE RID: 47822 RVA: 0x00391EB9 File Offset: 0x003900B9
		// (set) Token: 0x0600BACD RID: 47821 RVA: 0x00391E93 File Offset: 0x00390093
		public int LastSelectedGameModeRecordId
		{
			get
			{
				return this.m_LastSelectedGameModeRecordId;
			}
			set
			{
				if (this.m_LastSelectedGameModeRecordId == value)
				{
					return;
				}
				this.m_LastSelectedGameModeRecordId = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x0600BACF RID: 47823 RVA: 0x00391EC1 File Offset: 0x003900C1
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BAD0 RID: 47824 RVA: 0x00391EC9 File Offset: 0x003900C9
		public int GetPropertiesHashCode()
		{
			int num = (17 * 31 + ((this.m_GameModeButtons != null) ? this.m_GameModeButtons.GetPropertiesHashCode() : 0)) * 31;
			int lastSelectedGameModeRecordId = this.m_LastSelectedGameModeRecordId;
			return num + this.m_LastSelectedGameModeRecordId.GetHashCode();
		}

		// Token: 0x0600BAD1 RID: 47825 RVA: 0x00391EFD File Offset: 0x003900FD
		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 0)
			{
				value = this.m_GameModeButtons;
				return true;
			}
			if (id != 1)
			{
				value = null;
				return false;
			}
			value = this.m_LastSelectedGameModeRecordId;
			return true;
		}

		// Token: 0x0600BAD2 RID: 47826 RVA: 0x00391F25 File Offset: 0x00390125
		public bool SetPropertyValue(int id, object value)
		{
			if (id == 0)
			{
				this.GameModeButtons = ((value != null) ? ((DataModelList<GameModeButtonDataModel>)value) : null);
				return true;
			}
			if (id != 1)
			{
				return false;
			}
			this.LastSelectedGameModeRecordId = ((value != null) ? ((int)value) : 0);
			return true;
		}

		// Token: 0x0600BAD3 RID: 47827 RVA: 0x00391F59 File Offset: 0x00390159
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

		// Token: 0x04009970 RID: 39280
		public const int ModelId = 173;

		// Token: 0x04009971 RID: 39281
		private DataModelList<GameModeButtonDataModel> m_GameModeButtons = new DataModelList<GameModeButtonDataModel>();

		// Token: 0x04009972 RID: 39282
		private int m_LastSelectedGameModeRecordId;

		// Token: 0x04009973 RID: 39283
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "gamemodesbuttons",
				Type = typeof(DataModelList<GameModeButtonDataModel>)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "lastselectedgamemoderecordid",
				Type = typeof(int)
			}
		};
	}
}
