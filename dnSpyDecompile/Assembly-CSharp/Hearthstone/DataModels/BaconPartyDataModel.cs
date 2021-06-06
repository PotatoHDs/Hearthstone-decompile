using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010A9 RID: 4265
	public class BaconPartyDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x0600B9BB RID: 47547 RVA: 0x0038D9CF File Offset: 0x0038BBCF
		public int DataModelId
		{
			get
			{
				return 154;
			}
		}

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x0600B9BC RID: 47548 RVA: 0x0038D9D6 File Offset: 0x0038BBD6
		public string DataModelDisplayName
		{
			get
			{
				return "baconparty";
			}
		}

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x0600B9BE RID: 47550 RVA: 0x0038DA03 File Offset: 0x0038BC03
		// (set) Token: 0x0600B9BD RID: 47549 RVA: 0x0038D9DD File Offset: 0x0038BBDD
		public bool Active
		{
			get
			{
				return this.m_Active;
			}
			set
			{
				if (this.m_Active == value)
				{
					return;
				}
				this.m_Active = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x0600B9C0 RID: 47552 RVA: 0x0038DA31 File Offset: 0x0038BC31
		// (set) Token: 0x0600B9BF RID: 47551 RVA: 0x0038DA0B File Offset: 0x0038BC0B
		public int Size
		{
			get
			{
				return this.m_Size;
			}
			set
			{
				if (this.m_Size == value)
				{
					return;
				}
				this.m_Size = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x0600B9C2 RID: 47554 RVA: 0x0038DA5F File Offset: 0x0038BC5F
		// (set) Token: 0x0600B9C1 RID: 47553 RVA: 0x0038DA39 File Offset: 0x0038BC39
		public int Ready
		{
			get
			{
				return this.m_Ready;
			}
			set
			{
				if (this.m_Ready == value)
				{
					return;
				}
				this.m_Ready = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x0600B9C4 RID: 47556 RVA: 0x0038DA8D File Offset: 0x0038BC8D
		// (set) Token: 0x0600B9C3 RID: 47555 RVA: 0x0038DA67 File Offset: 0x0038BC67
		public bool PrivateGame
		{
			get
			{
				return this.m_PrivateGame;
			}
			set
			{
				if (this.m_PrivateGame == value)
				{
					return;
				}
				this.m_PrivateGame = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x0600B9C5 RID: 47557 RVA: 0x0038DA95 File Offset: 0x0038BC95
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600B9C6 RID: 47558 RVA: 0x0038DAA0 File Offset: 0x0038BCA0
		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			bool active = this.m_Active;
			int num2 = (num + this.m_Active.GetHashCode()) * 31;
			int size = this.m_Size;
			int num3 = (num2 + this.m_Size.GetHashCode()) * 31;
			int ready = this.m_Ready;
			int num4 = (num3 + this.m_Ready.GetHashCode()) * 31;
			bool privateGame = this.m_PrivateGame;
			return num4 + this.m_PrivateGame.GetHashCode();
		}

		// Token: 0x0600B9C7 RID: 47559 RVA: 0x0038DB08 File Offset: 0x0038BD08
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = this.m_Active;
				return true;
			case 1:
				value = this.m_Size;
				return true;
			case 2:
				value = this.m_Ready;
				return true;
			case 3:
				value = this.m_PrivateGame;
				return true;
			default:
				value = null;
				return false;
			}
		}

		// Token: 0x0600B9C8 RID: 47560 RVA: 0x0038DB70 File Offset: 0x0038BD70
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				this.Active = (value != null && (bool)value);
				return true;
			case 1:
				this.Size = ((value != null) ? ((int)value) : 0);
				return true;
			case 2:
				this.Ready = ((value != null) ? ((int)value) : 0);
				return true;
			case 3:
				this.PrivateGame = (value != null && (bool)value);
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600B9C9 RID: 47561 RVA: 0x0038DBE8 File Offset: 0x0038BDE8
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
			default:
				info = default(DataModelProperty);
				return false;
			}
		}

		// Token: 0x04009905 RID: 39173
		public const int ModelId = 154;

		// Token: 0x04009906 RID: 39174
		private bool m_Active;

		// Token: 0x04009907 RID: 39175
		private int m_Size;

		// Token: 0x04009908 RID: 39176
		private int m_Ready;

		// Token: 0x04009909 RID: 39177
		private bool m_PrivateGame;

		// Token: 0x0400990A RID: 39178
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "active",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "size",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "ready",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "private_game",
				Type = typeof(bool)
			}
		};
	}
}
