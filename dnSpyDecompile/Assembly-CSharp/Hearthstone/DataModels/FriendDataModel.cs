using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010B5 RID: 4277
	public class FriendDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x0600BA99 RID: 47769 RVA: 0x00391261 File Offset: 0x0038F461
		public int DataModelId
		{
			get
			{
				return 159;
			}
		}

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x0600BA9A RID: 47770 RVA: 0x00391268 File Offset: 0x0038F468
		public string DataModelDisplayName
		{
			get
			{
				return "friend";
			}
		}

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x0600BA9C RID: 47772 RVA: 0x0039129A File Offset: 0x0038F49A
		// (set) Token: 0x0600BA9B RID: 47771 RVA: 0x0039126F File Offset: 0x0038F46F
		public string PlayerName
		{
			get
			{
				return this.m_PlayerName;
			}
			set
			{
				if (this.m_PlayerName == value)
				{
					return;
				}
				this.m_PlayerName = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x0600BA9E RID: 47774 RVA: 0x003912CD File Offset: 0x0038F4CD
		// (set) Token: 0x0600BA9D RID: 47773 RVA: 0x003912A2 File Offset: 0x0038F4A2
		public string PlayerStatus
		{
			get
			{
				return this.m_PlayerStatus;
			}
			set
			{
				if (this.m_PlayerStatus == value)
				{
					return;
				}
				this.m_PlayerStatus = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x0600BAA0 RID: 47776 RVA: 0x003912FB File Offset: 0x0038F4FB
		// (set) Token: 0x0600BA9F RID: 47775 RVA: 0x003912D5 File Offset: 0x0038F4D5
		public bool IsOnline
		{
			get
			{
				return this.m_IsOnline;
			}
			set
			{
				if (this.m_IsOnline == value)
				{
					return;
				}
				this.m_IsOnline = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x0600BAA2 RID: 47778 RVA: 0x00391329 File Offset: 0x0038F529
		// (set) Token: 0x0600BAA1 RID: 47777 RVA: 0x00391303 File Offset: 0x0038F503
		public bool IsAway
		{
			get
			{
				return this.m_IsAway;
			}
			set
			{
				if (this.m_IsAway == value)
				{
					return;
				}
				this.m_IsAway = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x0600BAA4 RID: 47780 RVA: 0x00391357 File Offset: 0x0038F557
		// (set) Token: 0x0600BAA3 RID: 47779 RVA: 0x00391331 File Offset: 0x0038F531
		public bool IsBusy
		{
			get
			{
				return this.m_IsBusy;
			}
			set
			{
				if (this.m_IsBusy == value)
				{
					return;
				}
				this.m_IsBusy = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x0600BAA6 RID: 47782 RVA: 0x00391385 File Offset: 0x0038F585
		// (set) Token: 0x0600BAA5 RID: 47781 RVA: 0x0039135F File Offset: 0x0038F55F
		public bool IsFSGPatron
		{
			get
			{
				return this.m_IsFSGPatron;
			}
			set
			{
				if (this.m_IsFSGPatron == value)
				{
					return;
				}
				this.m_IsFSGPatron = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x0600BAA8 RID: 47784 RVA: 0x003913B3 File Offset: 0x0038F5B3
		// (set) Token: 0x0600BAA7 RID: 47783 RVA: 0x0039138D File Offset: 0x0038F58D
		public bool IsFSGInnkeeper
		{
			get
			{
				return this.m_IsFSGInnkeeper;
			}
			set
			{
				if (this.m_IsFSGInnkeeper == value)
				{
					return;
				}
				this.m_IsFSGInnkeeper = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x0600BAAA RID: 47786 RVA: 0x003913E1 File Offset: 0x0038F5E1
		// (set) Token: 0x0600BAA9 RID: 47785 RVA: 0x003913BB File Offset: 0x0038F5BB
		public bool IsInEditMode
		{
			get
			{
				return this.m_IsInEditMode;
			}
			set
			{
				if (this.m_IsInEditMode == value)
				{
					return;
				}
				this.m_IsInEditMode = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x0600BAAC RID: 47788 RVA: 0x0039140F File Offset: 0x0038F60F
		// (set) Token: 0x0600BAAB RID: 47787 RVA: 0x003913E9 File Offset: 0x0038F5E9
		public bool IsSelected
		{
			get
			{
				return this.m_IsSelected;
			}
			set
			{
				if (this.m_IsSelected == value)
				{
					return;
				}
				this.m_IsSelected = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x0600BAAD RID: 47789 RVA: 0x00391417 File Offset: 0x0038F617
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BAAE RID: 47790 RVA: 0x00391420 File Offset: 0x0038F620
		public int GetPropertiesHashCode()
		{
			int num = ((17 * 31 + ((this.m_PlayerName != null) ? this.m_PlayerName.GetHashCode() : 0)) * 31 + ((this.m_PlayerStatus != null) ? this.m_PlayerStatus.GetHashCode() : 0)) * 31;
			bool isOnline = this.m_IsOnline;
			int num2 = (num + this.m_IsOnline.GetHashCode()) * 31;
			bool isAway = this.m_IsAway;
			int num3 = (num2 + this.m_IsAway.GetHashCode()) * 31;
			bool isBusy = this.m_IsBusy;
			int num4 = (num3 + this.m_IsBusy.GetHashCode()) * 31;
			bool isFSGPatron = this.m_IsFSGPatron;
			int num5 = (num4 + this.m_IsFSGPatron.GetHashCode()) * 31;
			bool isFSGInnkeeper = this.m_IsFSGInnkeeper;
			int num6 = (num5 + this.m_IsFSGInnkeeper.GetHashCode()) * 31;
			bool isInEditMode = this.m_IsInEditMode;
			int num7 = (num6 + this.m_IsInEditMode.GetHashCode()) * 31;
			bool isSelected = this.m_IsSelected;
			return num7 + this.m_IsSelected.GetHashCode();
		}

		// Token: 0x0600BAAF RID: 47791 RVA: 0x00391500 File Offset: 0x0038F700
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 160:
				value = this.m_PlayerName;
				return true;
			case 161:
				value = this.m_PlayerStatus;
				return true;
			case 162:
				value = this.m_IsOnline;
				return true;
			case 163:
				value = this.m_IsAway;
				return true;
			case 164:
				value = this.m_IsBusy;
				return true;
			case 165:
				value = this.m_IsFSGPatron;
				return true;
			case 166:
				value = this.m_IsFSGInnkeeper;
				return true;
			case 167:
				value = this.m_IsInEditMode;
				return true;
			case 169:
				value = this.m_IsSelected;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600BAB0 RID: 47792 RVA: 0x003915C4 File Offset: 0x0038F7C4
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 160:
				this.PlayerName = ((value != null) ? ((string)value) : null);
				return true;
			case 161:
				this.PlayerStatus = ((value != null) ? ((string)value) : null);
				return true;
			case 162:
				this.IsOnline = (value != null && (bool)value);
				return true;
			case 163:
				this.IsAway = (value != null && (bool)value);
				return true;
			case 164:
				this.IsBusy = (value != null && (bool)value);
				return true;
			case 165:
				this.IsFSGPatron = (value != null && (bool)value);
				return true;
			case 166:
				this.IsFSGInnkeeper = (value != null && (bool)value);
				return true;
			case 167:
				this.IsInEditMode = (value != null && (bool)value);
				return true;
			case 169:
				this.IsSelected = (value != null && (bool)value);
				return true;
			}
			return false;
		}

		// Token: 0x0600BAB1 RID: 47793 RVA: 0x003916C0 File Offset: 0x0038F8C0
		public bool GetPropertyInfo(int id, out DataModelProperty info)
		{
			switch (id)
			{
			case 160:
				info = this.Properties[0];
				return true;
			case 161:
				info = this.Properties[1];
				return true;
			case 162:
				info = this.Properties[2];
				return true;
			case 163:
				info = this.Properties[3];
				return true;
			case 164:
				info = this.Properties[4];
				return true;
			case 165:
				info = this.Properties[5];
				return true;
			case 166:
				info = this.Properties[6];
				return true;
			case 167:
				info = this.Properties[7];
				return true;
			case 169:
				info = this.Properties[8];
				return true;
			}
			info = default(DataModelProperty);
			return false;
		}

		// Token: 0x0400995C RID: 39260
		public const int ModelId = 159;

		// Token: 0x0400995D RID: 39261
		private string m_PlayerName;

		// Token: 0x0400995E RID: 39262
		private string m_PlayerStatus;

		// Token: 0x0400995F RID: 39263
		private bool m_IsOnline;

		// Token: 0x04009960 RID: 39264
		private bool m_IsAway;

		// Token: 0x04009961 RID: 39265
		private bool m_IsBusy;

		// Token: 0x04009962 RID: 39266
		private bool m_IsFSGPatron;

		// Token: 0x04009963 RID: 39267
		private bool m_IsFSGInnkeeper;

		// Token: 0x04009964 RID: 39268
		private bool m_IsInEditMode;

		// Token: 0x04009965 RID: 39269
		private bool m_IsSelected;

		// Token: 0x04009966 RID: 39270
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 160,
				PropertyDisplayName = "player_name",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 161,
				PropertyDisplayName = "player_status",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 162,
				PropertyDisplayName = "is_online",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 163,
				PropertyDisplayName = "is_away",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 164,
				PropertyDisplayName = "is_busy",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 165,
				PropertyDisplayName = "is_fsgpatron",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 166,
				PropertyDisplayName = "is_fsginnkeeper",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 167,
				PropertyDisplayName = "is_in_edit_mode",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 169,
				PropertyDisplayName = "is_selected",
				Type = typeof(bool)
			}
		};
	}
}
