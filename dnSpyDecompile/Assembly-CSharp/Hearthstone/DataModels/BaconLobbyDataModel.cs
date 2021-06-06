using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010A8 RID: 4264
	public class BaconLobbyDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x0600B9A7 RID: 47527 RVA: 0x0038D535 File Offset: 0x0038B735
		public int DataModelId
		{
			get
			{
				return 43;
			}
		}

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x0600B9A8 RID: 47528 RVA: 0x0038D539 File Offset: 0x0038B739
		public string DataModelDisplayName
		{
			get
			{
				return "baconlobby";
			}
		}

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x0600B9AA RID: 47530 RVA: 0x0038D566 File Offset: 0x0038B766
		// (set) Token: 0x0600B9A9 RID: 47529 RVA: 0x0038D540 File Offset: 0x0038B740
		public int Rating
		{
			get
			{
				return this.m_Rating;
			}
			set
			{
				if (this.m_Rating == value)
				{
					return;
				}
				this.m_Rating = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x0600B9AC RID: 47532 RVA: 0x0038D594 File Offset: 0x0038B794
		// (set) Token: 0x0600B9AB RID: 47531 RVA: 0x0038D56E File Offset: 0x0038B76E
		public int FirstPlaceFinishes
		{
			get
			{
				return this.m_FirstPlaceFinishes;
			}
			set
			{
				if (this.m_FirstPlaceFinishes == value)
				{
					return;
				}
				this.m_FirstPlaceFinishes = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x0600B9AE RID: 47534 RVA: 0x0038D5C2 File Offset: 0x0038B7C2
		// (set) Token: 0x0600B9AD RID: 47533 RVA: 0x0038D59C File Offset: 0x0038B79C
		public int Top4Finishes
		{
			get
			{
				return this.m_Top4Finishes;
			}
			set
			{
				if (this.m_Top4Finishes == value)
				{
					return;
				}
				this.m_Top4Finishes = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x0600B9B0 RID: 47536 RVA: 0x0038D5F0 File Offset: 0x0038B7F0
		// (set) Token: 0x0600B9AF RID: 47535 RVA: 0x0038D5CA File Offset: 0x0038B7CA
		public BoosterDbId PremiumPackType
		{
			get
			{
				return this.m_PremiumPackType;
			}
			set
			{
				if (this.m_PremiumPackType == value)
				{
					return;
				}
				this.m_PremiumPackType = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x0600B9B2 RID: 47538 RVA: 0x0038D61E File Offset: 0x0038B81E
		// (set) Token: 0x0600B9B1 RID: 47537 RVA: 0x0038D5F8 File Offset: 0x0038B7F8
		public int PremiumPackOwnedCount
		{
			get
			{
				return this.m_PremiumPackOwnedCount;
			}
			set
			{
				if (this.m_PremiumPackOwnedCount == value)
				{
					return;
				}
				this.m_PremiumPackOwnedCount = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x0600B9B4 RID: 47540 RVA: 0x0038D64C File Offset: 0x0038B84C
		// (set) Token: 0x0600B9B3 RID: 47539 RVA: 0x0038D626 File Offset: 0x0038B826
		public bool BonusesLicenseOwned
		{
			get
			{
				return this.m_BonusesLicenseOwned;
			}
			set
			{
				if (this.m_BonusesLicenseOwned == value)
				{
					return;
				}
				this.m_BonusesLicenseOwned = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x0600B9B5 RID: 47541 RVA: 0x0038D654 File Offset: 0x0038B854
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600B9B6 RID: 47542 RVA: 0x0038D65C File Offset: 0x0038B85C
		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			int rating = this.m_Rating;
			int num2 = (num + this.m_Rating.GetHashCode()) * 31;
			int firstPlaceFinishes = this.m_FirstPlaceFinishes;
			int num3 = (num2 + this.m_FirstPlaceFinishes.GetHashCode()) * 31;
			int top4Finishes = this.m_Top4Finishes;
			int num4 = (num3 + this.m_Top4Finishes.GetHashCode()) * 31;
			BoosterDbId premiumPackType = this.m_PremiumPackType;
			int num5 = (num4 + this.m_PremiumPackType.GetHashCode()) * 31;
			int premiumPackOwnedCount = this.m_PremiumPackOwnedCount;
			int num6 = (num5 + this.m_PremiumPackOwnedCount.GetHashCode()) * 31;
			bool bonusesLicenseOwned = this.m_BonusesLicenseOwned;
			return num6 + this.m_BonusesLicenseOwned.GetHashCode();
		}

		// Token: 0x0600B9B7 RID: 47543 RVA: 0x0038D6F8 File Offset: 0x0038B8F8
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = this.m_Rating;
				return true;
			case 1:
				value = this.m_FirstPlaceFinishes;
				return true;
			case 2:
				value = this.m_Top4Finishes;
				return true;
			case 3:
				value = this.m_PremiumPackType;
				return true;
			case 4:
				value = this.m_PremiumPackOwnedCount;
				return true;
			case 5:
				value = this.m_BonusesLicenseOwned;
				return true;
			default:
				value = null;
				return false;
			}
		}

		// Token: 0x0600B9B8 RID: 47544 RVA: 0x0038D784 File Offset: 0x0038B984
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				this.Rating = ((value != null) ? ((int)value) : 0);
				return true;
			case 1:
				this.FirstPlaceFinishes = ((value != null) ? ((int)value) : 0);
				return true;
			case 2:
				this.Top4Finishes = ((value != null) ? ((int)value) : 0);
				return true;
			case 3:
				this.PremiumPackType = ((value != null) ? ((BoosterDbId)value) : BoosterDbId.INVALID);
				return true;
			case 4:
				this.PremiumPackOwnedCount = ((value != null) ? ((int)value) : 0);
				return true;
			case 5:
				this.BonusesLicenseOwned = (value != null && (bool)value);
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600B9B9 RID: 47545 RVA: 0x0038D82C File Offset: 0x0038BA2C
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
			default:
				info = default(DataModelProperty);
				return false;
			}
		}

		// Token: 0x040098FD RID: 39165
		public const int ModelId = 43;

		// Token: 0x040098FE RID: 39166
		private int m_Rating;

		// Token: 0x040098FF RID: 39167
		private int m_FirstPlaceFinishes;

		// Token: 0x04009900 RID: 39168
		private int m_Top4Finishes;

		// Token: 0x04009901 RID: 39169
		private BoosterDbId m_PremiumPackType;

		// Token: 0x04009902 RID: 39170
		private int m_PremiumPackOwnedCount;

		// Token: 0x04009903 RID: 39171
		private bool m_BonusesLicenseOwned;

		// Token: 0x04009904 RID: 39172
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "rating",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "first_place_finishes",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "top_4_finishes",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "premium_pack_type",
				Type = typeof(BoosterDbId)
			},
			new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "premium_pack_owned_count",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "bonuses_license_owned",
				Type = typeof(bool)
			}
		};
	}
}
