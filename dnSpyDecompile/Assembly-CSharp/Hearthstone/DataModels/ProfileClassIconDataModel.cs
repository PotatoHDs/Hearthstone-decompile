using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010C8 RID: 4296
	public class ProfileClassIconDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x0600BBBF RID: 48063 RVA: 0x00395482 File Offset: 0x00393682
		public int DataModelId
		{
			get
			{
				return 232;
			}
		}

		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x0600BBC0 RID: 48064 RVA: 0x00395489 File Offset: 0x00393689
		public string DataModelDisplayName
		{
			get
			{
				return "profile_class_icon";
			}
		}

		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x0600BBC2 RID: 48066 RVA: 0x003954B6 File Offset: 0x003936B6
		// (set) Token: 0x0600BBC1 RID: 48065 RVA: 0x00395490 File Offset: 0x00393690
		public TAG_CLASS TagClass
		{
			get
			{
				return this.m_TagClass;
			}
			set
			{
				if (this.m_TagClass == value)
				{
					return;
				}
				this.m_TagClass = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x0600BBC4 RID: 48068 RVA: 0x003954E4 File Offset: 0x003936E4
		// (set) Token: 0x0600BBC3 RID: 48067 RVA: 0x003954BE File Offset: 0x003936BE
		public bool IsUnlocked
		{
			get
			{
				return this.m_IsUnlocked;
			}
			set
			{
				if (this.m_IsUnlocked == value)
				{
					return;
				}
				this.m_IsUnlocked = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x0600BBC6 RID: 48070 RVA: 0x00395512 File Offset: 0x00393712
		// (set) Token: 0x0600BBC5 RID: 48069 RVA: 0x003954EC File Offset: 0x003936EC
		public bool IsGolden
		{
			get
			{
				return this.m_IsGolden;
			}
			set
			{
				if (this.m_IsGolden == value)
				{
					return;
				}
				this.m_IsGolden = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x0600BBC8 RID: 48072 RVA: 0x00395540 File Offset: 0x00393740
		// (set) Token: 0x0600BBC7 RID: 48071 RVA: 0x0039551A File Offset: 0x0039371A
		public int CurrentLevel
		{
			get
			{
				return this.m_CurrentLevel;
			}
			set
			{
				if (this.m_CurrentLevel == value)
				{
					return;
				}
				this.m_CurrentLevel = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x0600BBCA RID: 48074 RVA: 0x0039556E File Offset: 0x0039376E
		// (set) Token: 0x0600BBC9 RID: 48073 RVA: 0x00395548 File Offset: 0x00393748
		public int MaxLevel
		{
			get
			{
				return this.m_MaxLevel;
			}
			set
			{
				if (this.m_MaxLevel == value)
				{
					return;
				}
				this.m_MaxLevel = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x0600BBCC RID: 48076 RVA: 0x0039559C File Offset: 0x0039379C
		// (set) Token: 0x0600BBCB RID: 48075 RVA: 0x00395576 File Offset: 0x00393776
		public long CurrentLevelXP
		{
			get
			{
				return this.m_CurrentLevelXP;
			}
			set
			{
				if (this.m_CurrentLevelXP == value)
				{
					return;
				}
				this.m_CurrentLevelXP = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x0600BBCE RID: 48078 RVA: 0x003955CA File Offset: 0x003937CA
		// (set) Token: 0x0600BBCD RID: 48077 RVA: 0x003955A4 File Offset: 0x003937A4
		public long CurrentLevelXPMax
		{
			get
			{
				return this.m_CurrentLevelXPMax;
			}
			set
			{
				if (this.m_CurrentLevelXPMax == value)
				{
					return;
				}
				this.m_CurrentLevelXPMax = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x0600BBD0 RID: 48080 RVA: 0x003955FD File Offset: 0x003937FD
		// (set) Token: 0x0600BBCF RID: 48079 RVA: 0x003955D2 File Offset: 0x003937D2
		public string WinsText
		{
			get
			{
				return this.m_WinsText;
			}
			set
			{
				if (this.m_WinsText == value)
				{
					return;
				}
				this.m_WinsText = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x0600BBD2 RID: 48082 RVA: 0x0039562B File Offset: 0x0039382B
		// (set) Token: 0x0600BBD1 RID: 48081 RVA: 0x00395605 File Offset: 0x00393805
		public bool IsMaxLevel
		{
			get
			{
				return this.m_IsMaxLevel;
			}
			set
			{
				if (this.m_IsMaxLevel == value)
				{
					return;
				}
				this.m_IsMaxLevel = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x0600BBD4 RID: 48084 RVA: 0x0039565E File Offset: 0x0039385E
		// (set) Token: 0x0600BBD3 RID: 48083 RVA: 0x00395633 File Offset: 0x00393833
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

		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x0600BBD6 RID: 48086 RVA: 0x00395691 File Offset: 0x00393891
		// (set) Token: 0x0600BBD5 RID: 48085 RVA: 0x00395666 File Offset: 0x00393866
		public string TooltipTitle
		{
			get
			{
				return this.m_TooltipTitle;
			}
			set
			{
				if (this.m_TooltipTitle == value)
				{
					return;
				}
				this.m_TooltipTitle = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x0600BBD8 RID: 48088 RVA: 0x003956C4 File Offset: 0x003938C4
		// (set) Token: 0x0600BBD7 RID: 48087 RVA: 0x00395699 File Offset: 0x00393899
		public string TooltipDesc
		{
			get
			{
				return this.m_TooltipDesc;
			}
			set
			{
				if (this.m_TooltipDesc == value)
				{
					return;
				}
				this.m_TooltipDesc = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x0600BBDA RID: 48090 RVA: 0x003956F2 File Offset: 0x003938F2
		// (set) Token: 0x0600BBD9 RID: 48089 RVA: 0x003956CC File Offset: 0x003938CC
		public bool IsPremium
		{
			get
			{
				return this.m_IsPremium;
			}
			set
			{
				if (this.m_IsPremium == value)
				{
					return;
				}
				this.m_IsPremium = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x0600BBDC RID: 48092 RVA: 0x00395720 File Offset: 0x00393920
		// (set) Token: 0x0600BBDB RID: 48091 RVA: 0x003956FA File Offset: 0x003938FA
		public int GoldWinsReq
		{
			get
			{
				return this.m_GoldWinsReq;
			}
			set
			{
				if (this.m_GoldWinsReq == value)
				{
					return;
				}
				this.m_GoldWinsReq = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x0600BBDE RID: 48094 RVA: 0x0039574E File Offset: 0x0039394E
		// (set) Token: 0x0600BBDD RID: 48093 RVA: 0x00395728 File Offset: 0x00393928
		public int PremiumWinsReq
		{
			get
			{
				return this.m_PremiumWinsReq;
			}
			set
			{
				if (this.m_PremiumWinsReq == value)
				{
					return;
				}
				this.m_PremiumWinsReq = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x0600BBE0 RID: 48096 RVA: 0x0039577C File Offset: 0x0039397C
		// (set) Token: 0x0600BBDF RID: 48095 RVA: 0x00395756 File Offset: 0x00393956
		public int Wins
		{
			get
			{
				return this.m_Wins;
			}
			set
			{
				if (this.m_Wins == value)
				{
					return;
				}
				this.m_Wins = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x0600BBE1 RID: 48097 RVA: 0x00395784 File Offset: 0x00393984
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BBE2 RID: 48098 RVA: 0x0039578C File Offset: 0x0039398C
		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			TAG_CLASS tagClass = this.m_TagClass;
			int num2 = (num + this.m_TagClass.GetHashCode()) * 31;
			bool isUnlocked = this.m_IsUnlocked;
			int num3 = (num2 + this.m_IsUnlocked.GetHashCode()) * 31;
			bool isGolden = this.m_IsGolden;
			int num4 = (num3 + this.m_IsGolden.GetHashCode()) * 31;
			int currentLevel = this.m_CurrentLevel;
			int num5 = (num4 + this.m_CurrentLevel.GetHashCode()) * 31;
			int maxLevel = this.m_MaxLevel;
			int num6 = (num5 + this.m_MaxLevel.GetHashCode()) * 31;
			long currentLevelXP = this.m_CurrentLevelXP;
			int num7 = (num6 + this.m_CurrentLevelXP.GetHashCode()) * 31;
			long currentLevelXPMax = this.m_CurrentLevelXPMax;
			int num8 = ((num7 + this.m_CurrentLevelXPMax.GetHashCode()) * 31 + ((this.m_WinsText != null) ? this.m_WinsText.GetHashCode() : 0)) * 31;
			bool isMaxLevel = this.m_IsMaxLevel;
			int num9 = ((((num8 + this.m_IsMaxLevel.GetHashCode()) * 31 + ((this.m_Name != null) ? this.m_Name.GetHashCode() : 0)) * 31 + ((this.m_TooltipTitle != null) ? this.m_TooltipTitle.GetHashCode() : 0)) * 31 + ((this.m_TooltipDesc != null) ? this.m_TooltipDesc.GetHashCode() : 0)) * 31;
			bool isPremium = this.m_IsPremium;
			int num10 = (num9 + this.m_IsPremium.GetHashCode()) * 31;
			int goldWinsReq = this.m_GoldWinsReq;
			int num11 = (num10 + this.m_GoldWinsReq.GetHashCode()) * 31;
			int premiumWinsReq = this.m_PremiumWinsReq;
			int num12 = (num11 + this.m_PremiumWinsReq.GetHashCode()) * 31;
			int wins = this.m_Wins;
			return num12 + this.m_Wins.GetHashCode();
		}

		// Token: 0x0600BBE3 RID: 48099 RVA: 0x00395914 File Offset: 0x00393B14
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = this.m_TagClass;
				return true;
			case 1:
				value = this.m_IsUnlocked;
				return true;
			case 2:
				value = this.m_IsGolden;
				return true;
			case 3:
				value = this.m_CurrentLevel;
				return true;
			case 4:
				value = this.m_MaxLevel;
				return true;
			case 5:
				value = this.m_CurrentLevelXP;
				return true;
			case 6:
				value = this.m_CurrentLevelXPMax;
				return true;
			case 7:
				value = this.m_WinsText;
				return true;
			case 8:
				value = this.m_IsMaxLevel;
				return true;
			case 9:
				value = this.m_Name;
				return true;
			case 10:
				value = this.m_TooltipTitle;
				return true;
			case 11:
				value = this.m_TooltipDesc;
				return true;
			case 12:
				value = this.m_IsPremium;
				return true;
			case 13:
				value = this.m_GoldWinsReq;
				return true;
			case 14:
				value = this.m_PremiumWinsReq;
				return true;
			case 15:
				value = this.m_Wins;
				return true;
			default:
				value = null;
				return false;
			}
		}

		// Token: 0x0600BBE4 RID: 48100 RVA: 0x00395A4C File Offset: 0x00393C4C
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				this.TagClass = ((value != null) ? ((TAG_CLASS)value) : TAG_CLASS.INVALID);
				return true;
			case 1:
				this.IsUnlocked = (value != null && (bool)value);
				return true;
			case 2:
				this.IsGolden = (value != null && (bool)value);
				return true;
			case 3:
				this.CurrentLevel = ((value != null) ? ((int)value) : 0);
				return true;
			case 4:
				this.MaxLevel = ((value != null) ? ((int)value) : 0);
				return true;
			case 5:
				this.CurrentLevelXP = ((value != null) ? ((long)value) : 0L);
				return true;
			case 6:
				this.CurrentLevelXPMax = ((value != null) ? ((long)value) : 0L);
				return true;
			case 7:
				this.WinsText = ((value != null) ? ((string)value) : null);
				return true;
			case 8:
				this.IsMaxLevel = (value != null && (bool)value);
				return true;
			case 9:
				this.Name = ((value != null) ? ((string)value) : null);
				return true;
			case 10:
				this.TooltipTitle = ((value != null) ? ((string)value) : null);
				return true;
			case 11:
				this.TooltipDesc = ((value != null) ? ((string)value) : null);
				return true;
			case 12:
				this.IsPremium = (value != null && (bool)value);
				return true;
			case 13:
				this.GoldWinsReq = ((value != null) ? ((int)value) : 0);
				return true;
			case 14:
				this.PremiumWinsReq = ((value != null) ? ((int)value) : 0);
				return true;
			case 15:
				this.Wins = ((value != null) ? ((int)value) : 0);
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600BBE5 RID: 48101 RVA: 0x00395BE8 File Offset: 0x00393DE8
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
				info = this.Properties[5];
				return true;
			case 6:
				info = this.Properties[6];
				return true;
			case 7:
				info = this.Properties[7];
				return true;
			case 8:
				info = this.Properties[8];
				return true;
			case 9:
				info = this.Properties[9];
				return true;
			case 10:
				info = this.Properties[10];
				return true;
			case 11:
				info = this.Properties[11];
				return true;
			case 12:
				info = this.Properties[12];
				return true;
			case 13:
				info = this.Properties[13];
				return true;
			case 14:
				info = this.Properties[14];
				return true;
			case 15:
				info = this.Properties[15];
				return true;
			default:
				info = default(DataModelProperty);
				return false;
			}
		}

		// Token: 0x040099C9 RID: 39369
		public const int ModelId = 232;

		// Token: 0x040099CA RID: 39370
		private TAG_CLASS m_TagClass;

		// Token: 0x040099CB RID: 39371
		private bool m_IsUnlocked;

		// Token: 0x040099CC RID: 39372
		private bool m_IsGolden;

		// Token: 0x040099CD RID: 39373
		private int m_CurrentLevel;

		// Token: 0x040099CE RID: 39374
		private int m_MaxLevel;

		// Token: 0x040099CF RID: 39375
		private long m_CurrentLevelXP;

		// Token: 0x040099D0 RID: 39376
		private long m_CurrentLevelXPMax;

		// Token: 0x040099D1 RID: 39377
		private string m_WinsText;

		// Token: 0x040099D2 RID: 39378
		private bool m_IsMaxLevel;

		// Token: 0x040099D3 RID: 39379
		private string m_Name;

		// Token: 0x040099D4 RID: 39380
		private string m_TooltipTitle;

		// Token: 0x040099D5 RID: 39381
		private string m_TooltipDesc;

		// Token: 0x040099D6 RID: 39382
		private bool m_IsPremium;

		// Token: 0x040099D7 RID: 39383
		private int m_GoldWinsReq;

		// Token: 0x040099D8 RID: 39384
		private int m_PremiumWinsReq;

		// Token: 0x040099D9 RID: 39385
		private int m_Wins;

		// Token: 0x040099DA RID: 39386
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "tag_class",
				Type = typeof(TAG_CLASS)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "is_unlocked",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "is_golden",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "current_level",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "max_level",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "current_level_xp",
				Type = typeof(long)
			},
			new DataModelProperty
			{
				PropertyId = 6,
				PropertyDisplayName = "current_level_xp_max",
				Type = typeof(long)
			},
			new DataModelProperty
			{
				PropertyId = 7,
				PropertyDisplayName = "wins_text",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 8,
				PropertyDisplayName = "is_max_level",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 9,
				PropertyDisplayName = "name",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 10,
				PropertyDisplayName = "tooltip_title",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 11,
				PropertyDisplayName = "tooltip_desc",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 12,
				PropertyDisplayName = "is_premium",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 13,
				PropertyDisplayName = "gold_wins_req",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 14,
				PropertyDisplayName = "premium_wins_req",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 15,
				PropertyDisplayName = "wins",
				Type = typeof(int)
			}
		};
	}
}
