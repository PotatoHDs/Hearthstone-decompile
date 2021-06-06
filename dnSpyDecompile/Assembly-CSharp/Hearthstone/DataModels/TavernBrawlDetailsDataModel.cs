using System;
using Hearthstone.UI;
using PegasusShared;

namespace Hearthstone.DataModels
{
	// Token: 0x020010E1 RID: 4321
	public class TavernBrawlDetailsDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x0600BDBF RID: 48575 RVA: 0x0039DA19 File Offset: 0x0039BC19
		public int DataModelId
		{
			get
			{
				return 158;
			}
		}

		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x0600BDC0 RID: 48576 RVA: 0x0039DA20 File Offset: 0x0039BC20
		public string DataModelDisplayName
		{
			get
			{
				return "tavern_brawl_details";
			}
		}

		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x0600BDC2 RID: 48578 RVA: 0x0039DA4D File Offset: 0x0039BC4D
		// (set) Token: 0x0600BDC1 RID: 48577 RVA: 0x0039DA27 File Offset: 0x0039BC27
		public BrawlType BrawlType
		{
			get
			{
				return this.m_BrawlType;
			}
			set
			{
				if (this.m_BrawlType == value)
				{
					return;
				}
				this.m_BrawlType = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x0600BDC4 RID: 48580 RVA: 0x0039DA7B File Offset: 0x0039BC7B
		// (set) Token: 0x0600BDC3 RID: 48579 RVA: 0x0039DA55 File Offset: 0x0039BC55
		public TavernBrawlMode BrawlMode
		{
			get
			{
				return this.m_BrawlMode;
			}
			set
			{
				if (this.m_BrawlMode == value)
				{
					return;
				}
				this.m_BrawlMode = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x0600BDC6 RID: 48582 RVA: 0x0039DAA9 File Offset: 0x0039BCA9
		// (set) Token: 0x0600BDC5 RID: 48581 RVA: 0x0039DA83 File Offset: 0x0039BC83
		public FormatType FormatType
		{
			get
			{
				return this.m_FormatType;
			}
			set
			{
				if (this.m_FormatType == value)
				{
					return;
				}
				this.m_FormatType = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x0600BDC8 RID: 48584 RVA: 0x0039DAD7 File Offset: 0x0039BCD7
		// (set) Token: 0x0600BDC7 RID: 48583 RVA: 0x0039DAB1 File Offset: 0x0039BCB1
		public int TicketType
		{
			get
			{
				return this.m_TicketType;
			}
			set
			{
				if (this.m_TicketType == value)
				{
					return;
				}
				this.m_TicketType = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x0600BDCA RID: 48586 RVA: 0x0039DB05 File Offset: 0x0039BD05
		// (set) Token: 0x0600BDC9 RID: 48585 RVA: 0x0039DADF File Offset: 0x0039BCDF
		public int MaxWins
		{
			get
			{
				return this.m_MaxWins;
			}
			set
			{
				if (this.m_MaxWins == value)
				{
					return;
				}
				this.m_MaxWins = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x0600BDCC RID: 48588 RVA: 0x0039DB33 File Offset: 0x0039BD33
		// (set) Token: 0x0600BDCB RID: 48587 RVA: 0x0039DB0D File Offset: 0x0039BD0D
		public int MaxLosses
		{
			get
			{
				return this.m_MaxLosses;
			}
			set
			{
				if (this.m_MaxLosses == value)
				{
					return;
				}
				this.m_MaxLosses = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x0600BDCE RID: 48590 RVA: 0x0039DB66 File Offset: 0x0039BD66
		// (set) Token: 0x0600BDCD RID: 48589 RVA: 0x0039DB3B File Offset: 0x0039BD3B
		public string Title
		{
			get
			{
				return this.m_Title;
			}
			set
			{
				if (this.m_Title == value)
				{
					return;
				}
				this.m_Title = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x0600BDD0 RID: 48592 RVA: 0x0039DB99 File Offset: 0x0039BD99
		// (set) Token: 0x0600BDCF RID: 48591 RVA: 0x0039DB6E File Offset: 0x0039BD6E
		public string RulesDesc
		{
			get
			{
				return this.m_RulesDesc;
			}
			set
			{
				if (this.m_RulesDesc == value)
				{
					return;
				}
				this.m_RulesDesc = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x0600BDD2 RID: 48594 RVA: 0x0039DBC7 File Offset: 0x0039BDC7
		// (set) Token: 0x0600BDD1 RID: 48593 RVA: 0x0039DBA1 File Offset: 0x0039BDA1
		public TavernBrawlPopupType PopupType
		{
			get
			{
				return this.m_PopupType;
			}
			set
			{
				if (this.m_PopupType == value)
				{
					return;
				}
				this.m_PopupType = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x0600BDD4 RID: 48596 RVA: 0x0039DBFA File Offset: 0x0039BDFA
		// (set) Token: 0x0600BDD3 RID: 48595 RVA: 0x0039DBCF File Offset: 0x0039BDCF
		public string RewardDesc
		{
			get
			{
				return this.m_RewardDesc;
			}
			set
			{
				if (this.m_RewardDesc == value)
				{
					return;
				}
				this.m_RewardDesc = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x0600BDD6 RID: 48598 RVA: 0x0039DC2D File Offset: 0x0039BE2D
		// (set) Token: 0x0600BDD5 RID: 48597 RVA: 0x0039DC02 File Offset: 0x0039BE02
		public string MinRewardDesc
		{
			get
			{
				return this.m_MinRewardDesc;
			}
			set
			{
				if (this.m_MinRewardDesc == value)
				{
					return;
				}
				this.m_MinRewardDesc = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x0600BDD8 RID: 48600 RVA: 0x0039DC60 File Offset: 0x0039BE60
		// (set) Token: 0x0600BDD7 RID: 48599 RVA: 0x0039DC35 File Offset: 0x0039BE35
		public string MaxRewardDesc
		{
			get
			{
				return this.m_MaxRewardDesc;
			}
			set
			{
				if (this.m_MaxRewardDesc == value)
				{
					return;
				}
				this.m_MaxRewardDesc = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x0600BDDA RID: 48602 RVA: 0x0039DC93 File Offset: 0x0039BE93
		// (set) Token: 0x0600BDD9 RID: 48601 RVA: 0x0039DC68 File Offset: 0x0039BE68
		public string EndConditionDesc
		{
			get
			{
				return this.m_EndConditionDesc;
			}
			set
			{
				if (this.m_EndConditionDesc == value)
				{
					return;
				}
				this.m_EndConditionDesc = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x0600BDDB RID: 48603 RVA: 0x0039DC9B File Offset: 0x0039BE9B
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BDDC RID: 48604 RVA: 0x0039DCA4 File Offset: 0x0039BEA4
		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			BrawlType brawlType = this.m_BrawlType;
			int num2 = (num + this.m_BrawlType.GetHashCode()) * 31;
			TavernBrawlMode brawlMode = this.m_BrawlMode;
			int num3 = (num2 + this.m_BrawlMode.GetHashCode()) * 31;
			FormatType formatType = this.m_FormatType;
			int num4 = (num3 + this.m_FormatType.GetHashCode()) * 31;
			int ticketType = this.m_TicketType;
			int num5 = (num4 + this.m_TicketType.GetHashCode()) * 31;
			int maxWins = this.m_MaxWins;
			int num6 = (num5 + this.m_MaxWins.GetHashCode()) * 31;
			int maxLosses = this.m_MaxLosses;
			int num7 = (((num6 + this.m_MaxLosses.GetHashCode()) * 31 + ((this.m_Title != null) ? this.m_Title.GetHashCode() : 0)) * 31 + ((this.m_RulesDesc != null) ? this.m_RulesDesc.GetHashCode() : 0)) * 31;
			TavernBrawlPopupType popupType = this.m_PopupType;
			return ((((num7 + this.m_PopupType.GetHashCode()) * 31 + ((this.m_RewardDesc != null) ? this.m_RewardDesc.GetHashCode() : 0)) * 31 + ((this.m_MinRewardDesc != null) ? this.m_MinRewardDesc.GetHashCode() : 0)) * 31 + ((this.m_MaxRewardDesc != null) ? this.m_MaxRewardDesc.GetHashCode() : 0)) * 31 + ((this.m_EndConditionDesc != null) ? this.m_EndConditionDesc.GetHashCode() : 0);
		}

		// Token: 0x0600BDDD RID: 48605 RVA: 0x0039DE04 File Offset: 0x0039C004
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = this.m_BrawlType;
				return true;
			case 1:
				value = this.m_BrawlMode;
				return true;
			case 2:
				value = this.m_FormatType;
				return true;
			case 3:
				value = this.m_TicketType;
				return true;
			case 4:
				value = this.m_MaxWins;
				return true;
			case 5:
				value = this.m_MaxLosses;
				return true;
			case 6:
				value = this.m_Title;
				return true;
			case 7:
				value = this.m_RulesDesc;
				return true;
			case 8:
				value = this.m_PopupType;
				return true;
			case 9:
				value = this.m_RewardDesc;
				return true;
			case 10:
				value = this.m_MinRewardDesc;
				return true;
			case 11:
				value = this.m_MaxRewardDesc;
				return true;
			case 12:
				value = this.m_EndConditionDesc;
				return true;
			default:
				value = null;
				return false;
			}
		}

		// Token: 0x0600BDDE RID: 48606 RVA: 0x0039DEFC File Offset: 0x0039C0FC
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				this.BrawlType = ((value != null) ? ((BrawlType)value) : BrawlType.BRAWL_TYPE_UNKNOWN);
				return true;
			case 1:
				this.BrawlMode = ((value != null) ? ((TavernBrawlMode)value) : TavernBrawlMode.TB_MODE_NORMAL);
				return true;
			case 2:
				this.FormatType = ((value != null) ? ((FormatType)value) : FormatType.FT_UNKNOWN);
				return true;
			case 3:
				this.TicketType = ((value != null) ? ((int)value) : 0);
				return true;
			case 4:
				this.MaxWins = ((value != null) ? ((int)value) : 0);
				return true;
			case 5:
				this.MaxLosses = ((value != null) ? ((int)value) : 0);
				return true;
			case 6:
				this.Title = ((value != null) ? ((string)value) : null);
				return true;
			case 7:
				this.RulesDesc = ((value != null) ? ((string)value) : null);
				return true;
			case 8:
				this.PopupType = ((value != null) ? ((TavernBrawlPopupType)value) : TavernBrawlPopupType.POPUP_TYPE_NONE);
				return true;
			case 9:
				this.RewardDesc = ((value != null) ? ((string)value) : null);
				return true;
			case 10:
				this.MinRewardDesc = ((value != null) ? ((string)value) : null);
				return true;
			case 11:
				this.MaxRewardDesc = ((value != null) ? ((string)value) : null);
				return true;
			case 12:
				this.EndConditionDesc = ((value != null) ? ((string)value) : null);
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600BDDF RID: 48607 RVA: 0x0039E050 File Offset: 0x0039C250
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
			default:
				info = default(DataModelProperty);
				return false;
			}
		}

		// Token: 0x04009A97 RID: 39575
		public const int ModelId = 158;

		// Token: 0x04009A98 RID: 39576
		private BrawlType m_BrawlType;

		// Token: 0x04009A99 RID: 39577
		private TavernBrawlMode m_BrawlMode;

		// Token: 0x04009A9A RID: 39578
		private FormatType m_FormatType;

		// Token: 0x04009A9B RID: 39579
		private int m_TicketType;

		// Token: 0x04009A9C RID: 39580
		private int m_MaxWins;

		// Token: 0x04009A9D RID: 39581
		private int m_MaxLosses;

		// Token: 0x04009A9E RID: 39582
		private string m_Title;

		// Token: 0x04009A9F RID: 39583
		private string m_RulesDesc;

		// Token: 0x04009AA0 RID: 39584
		private TavernBrawlPopupType m_PopupType;

		// Token: 0x04009AA1 RID: 39585
		private string m_RewardDesc;

		// Token: 0x04009AA2 RID: 39586
		private string m_MinRewardDesc;

		// Token: 0x04009AA3 RID: 39587
		private string m_MaxRewardDesc;

		// Token: 0x04009AA4 RID: 39588
		private string m_EndConditionDesc;

		// Token: 0x04009AA5 RID: 39589
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "brawl_type",
				Type = typeof(BrawlType)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "brawl_mode",
				Type = typeof(TavernBrawlMode)
			},
			new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "format_type",
				Type = typeof(FormatType)
			},
			new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "ticket_type",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "max_wins",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "max_losses",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 6,
				PropertyDisplayName = "title",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 7,
				PropertyDisplayName = "rules_desc",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 8,
				PropertyDisplayName = "popup_type",
				Type = typeof(TavernBrawlPopupType)
			},
			new DataModelProperty
			{
				PropertyId = 9,
				PropertyDisplayName = "reward_desc",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 10,
				PropertyDisplayName = "min_reward_desc",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 11,
				PropertyDisplayName = "max_reward_desc",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 12,
				PropertyDisplayName = "end_condition_desc",
				Type = typeof(string)
			}
		};
	}
}
