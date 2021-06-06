using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010B6 RID: 4278
	public class GameModeButtonDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x0600BAB3 RID: 47795 RVA: 0x0039195A File Offset: 0x0038FB5A
		public int DataModelId
		{
			get
			{
				return 172;
			}
		}

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x0600BAB4 RID: 47796 RVA: 0x00391961 File Offset: 0x0038FB61
		public string DataModelDisplayName
		{
			get
			{
				return "gamemodebutton";
			}
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x0600BAB6 RID: 47798 RVA: 0x00391993 File Offset: 0x0038FB93
		// (set) Token: 0x0600BAB5 RID: 47797 RVA: 0x00391968 File Offset: 0x0038FB68
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

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x0600BAB8 RID: 47800 RVA: 0x003919C6 File Offset: 0x0038FBC6
		// (set) Token: 0x0600BAB7 RID: 47799 RVA: 0x0039199B File Offset: 0x0038FB9B
		public string Description
		{
			get
			{
				return this.m_Description;
			}
			set
			{
				if (this.m_Description == value)
				{
					return;
				}
				this.m_Description = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x0600BABA RID: 47802 RVA: 0x003919F9 File Offset: 0x0038FBF9
		// (set) Token: 0x0600BAB9 RID: 47801 RVA: 0x003919CE File Offset: 0x0038FBCE
		public string ButtonState
		{
			get
			{
				return this.m_ButtonState;
			}
			set
			{
				if (this.m_ButtonState == value)
				{
					return;
				}
				this.m_ButtonState = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x0600BABC RID: 47804 RVA: 0x00391A27 File Offset: 0x0038FC27
		// (set) Token: 0x0600BABB RID: 47803 RVA: 0x00391A01 File Offset: 0x0038FC01
		public int GameModeRecordId
		{
			get
			{
				return this.m_GameModeRecordId;
			}
			set
			{
				if (this.m_GameModeRecordId == value)
				{
					return;
				}
				this.m_GameModeRecordId = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x0600BABE RID: 47806 RVA: 0x00391A55 File Offset: 0x0038FC55
		// (set) Token: 0x0600BABD RID: 47805 RVA: 0x00391A2F File Offset: 0x0038FC2F
		public bool IsNew
		{
			get
			{
				return this.m_IsNew;
			}
			set
			{
				if (this.m_IsNew == value)
				{
					return;
				}
				this.m_IsNew = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x0600BAC0 RID: 47808 RVA: 0x00391A83 File Offset: 0x0038FC83
		// (set) Token: 0x0600BABF RID: 47807 RVA: 0x00391A5D File Offset: 0x0038FC5D
		public bool IsEarlyAccess
		{
			get
			{
				return this.m_IsEarlyAccess;
			}
			set
			{
				if (this.m_IsEarlyAccess == value)
				{
					return;
				}
				this.m_IsEarlyAccess = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x0600BAC2 RID: 47810 RVA: 0x00391AB1 File Offset: 0x0038FCB1
		// (set) Token: 0x0600BAC1 RID: 47809 RVA: 0x00391A8B File Offset: 0x0038FC8B
		public bool IsBeta
		{
			get
			{
				return this.m_IsBeta;
			}
			set
			{
				if (this.m_IsBeta == value)
				{
					return;
				}
				this.m_IsBeta = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x0600BAC3 RID: 47811 RVA: 0x00391AB9 File Offset: 0x0038FCB9
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BAC4 RID: 47812 RVA: 0x00391AC4 File Offset: 0x0038FCC4
		public int GetPropertiesHashCode()
		{
			int num = (((17 * 31 + ((this.m_Name != null) ? this.m_Name.GetHashCode() : 0)) * 31 + ((this.m_Description != null) ? this.m_Description.GetHashCode() : 0)) * 31 + ((this.m_ButtonState != null) ? this.m_ButtonState.GetHashCode() : 0)) * 31;
			int gameModeRecordId = this.m_GameModeRecordId;
			int num2 = (num + this.m_GameModeRecordId.GetHashCode()) * 31;
			bool isNew = this.m_IsNew;
			int num3 = (num2 + this.m_IsNew.GetHashCode()) * 31;
			bool isEarlyAccess = this.m_IsEarlyAccess;
			int num4 = (num3 + this.m_IsEarlyAccess.GetHashCode()) * 31;
			bool isBeta = this.m_IsBeta;
			return num4 + this.m_IsBeta.GetHashCode();
		}

		// Token: 0x0600BAC5 RID: 47813 RVA: 0x00391B7C File Offset: 0x0038FD7C
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = this.m_Name;
				return true;
			case 1:
				value = this.m_Description;
				return true;
			case 2:
				value = this.m_ButtonState;
				return true;
			case 3:
				value = this.m_GameModeRecordId;
				return true;
			case 4:
				value = this.m_IsNew;
				return true;
			case 5:
				value = this.m_IsBeta;
				return true;
			default:
				if (id != 234)
				{
					value = null;
					return false;
				}
				value = this.m_IsEarlyAccess;
				return true;
			}
		}

		// Token: 0x0600BAC6 RID: 47814 RVA: 0x00391C10 File Offset: 0x0038FE10
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				this.Name = ((value != null) ? ((string)value) : null);
				return true;
			case 1:
				this.Description = ((value != null) ? ((string)value) : null);
				return true;
			case 2:
				this.ButtonState = ((value != null) ? ((string)value) : null);
				return true;
			case 3:
				this.GameModeRecordId = ((value != null) ? ((int)value) : 0);
				return true;
			case 4:
				this.IsNew = (value != null && (bool)value);
				return true;
			case 5:
				this.IsBeta = (value != null && (bool)value);
				return true;
			default:
				if (id != 234)
				{
					return false;
				}
				this.IsEarlyAccess = (value != null && (bool)value);
				return true;
			}
		}

		// Token: 0x0600BAC7 RID: 47815 RVA: 0x00391CD8 File Offset: 0x0038FED8
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
				info = this.Properties[6];
				return true;
			default:
				if (id != 234)
				{
					info = default(DataModelProperty);
					return false;
				}
				info = this.Properties[5];
				return true;
			}
		}

		// Token: 0x04009967 RID: 39271
		public const int ModelId = 172;

		// Token: 0x04009968 RID: 39272
		private string m_Name;

		// Token: 0x04009969 RID: 39273
		private string m_Description;

		// Token: 0x0400996A RID: 39274
		private string m_ButtonState;

		// Token: 0x0400996B RID: 39275
		private int m_GameModeRecordId;

		// Token: 0x0400996C RID: 39276
		private bool m_IsNew;

		// Token: 0x0400996D RID: 39277
		private bool m_IsEarlyAccess;

		// Token: 0x0400996E RID: 39278
		private bool m_IsBeta;

		// Token: 0x0400996F RID: 39279
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
				PropertyDisplayName = "description",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "buttonstate",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "gamemoderecordid",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "isnew",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 234,
				PropertyDisplayName = "isearlyaccess",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "isbeta",
				Type = typeof(bool)
			}
		};
	}
}
