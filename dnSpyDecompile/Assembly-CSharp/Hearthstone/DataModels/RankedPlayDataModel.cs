using System;
using Hearthstone.UI;
using PegasusShared;
using UnityEngine;

namespace Hearthstone.DataModels
{
	// Token: 0x020010D4 RID: 4308
	public class RankedPlayDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x0600BCB9 RID: 48313 RVA: 0x00399654 File Offset: 0x00397854
		public int DataModelId
		{
			get
			{
				return 123;
			}
		}

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x0600BCBA RID: 48314 RVA: 0x00399658 File Offset: 0x00397858
		public string DataModelDisplayName
		{
			get
			{
				return "rankedplay";
			}
		}

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x0600BCBC RID: 48316 RVA: 0x00399685 File Offset: 0x00397885
		// (set) Token: 0x0600BCBB RID: 48315 RVA: 0x0039965F File Offset: 0x0039785F
		public int StarMultiplier
		{
			get
			{
				return this.m_StarMultiplier;
			}
			set
			{
				if (this.m_StarMultiplier == value)
				{
					return;
				}
				this.m_StarMultiplier = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x0600BCBE RID: 48318 RVA: 0x003996B3 File Offset: 0x003978B3
		// (set) Token: 0x0600BCBD RID: 48317 RVA: 0x0039968D File Offset: 0x0039788D
		public int Stars
		{
			get
			{
				return this.m_Stars;
			}
			set
			{
				if (this.m_Stars == value)
				{
					return;
				}
				this.m_Stars = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x0600BCC0 RID: 48320 RVA: 0x003996E1 File Offset: 0x003978E1
		// (set) Token: 0x0600BCBF RID: 48319 RVA: 0x003996BB File Offset: 0x003978BB
		public int MaxStars
		{
			get
			{
				return this.m_MaxStars;
			}
			set
			{
				if (this.m_MaxStars == value)
				{
					return;
				}
				this.m_MaxStars = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x0600BCC2 RID: 48322 RVA: 0x00399714 File Offset: 0x00397914
		// (set) Token: 0x0600BCC1 RID: 48321 RVA: 0x003996E9 File Offset: 0x003978E9
		public Texture MedalTexture
		{
			get
			{
				return this.m_MedalTexture;
			}
			set
			{
				if (this.m_MedalTexture == value)
				{
					return;
				}
				this.m_MedalTexture = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x0600BCC4 RID: 48324 RVA: 0x00399747 File Offset: 0x00397947
		// (set) Token: 0x0600BCC3 RID: 48323 RVA: 0x0039971C File Offset: 0x0039791C
		public string MedalText
		{
			get
			{
				return this.m_MedalText;
			}
			set
			{
				if (this.m_MedalText == value)
				{
					return;
				}
				this.m_MedalText = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x0600BCC6 RID: 48326 RVA: 0x00399775 File Offset: 0x00397975
		// (set) Token: 0x0600BCC5 RID: 48325 RVA: 0x0039974F File Offset: 0x0039794F
		public bool IsLegend
		{
			get
			{
				return this.m_IsLegend;
			}
			set
			{
				if (this.m_IsLegend == value)
				{
					return;
				}
				this.m_IsLegend = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x0600BCC8 RID: 48328 RVA: 0x003997A3 File Offset: 0x003979A3
		// (set) Token: 0x0600BCC7 RID: 48327 RVA: 0x0039977D File Offset: 0x0039797D
		public int LegendRank
		{
			get
			{
				return this.m_LegendRank;
			}
			set
			{
				if (this.m_LegendRank == value)
				{
					return;
				}
				this.m_LegendRank = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x0600BCCA RID: 48330 RVA: 0x003997D6 File Offset: 0x003979D6
		// (set) Token: 0x0600BCC9 RID: 48329 RVA: 0x003997AB File Offset: 0x003979AB
		public string RankName
		{
			get
			{
				return this.m_RankName;
			}
			set
			{
				if (this.m_RankName == value)
				{
					return;
				}
				this.m_RankName = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x0600BCCC RID: 48332 RVA: 0x00399804 File Offset: 0x00397A04
		// (set) Token: 0x0600BCCB RID: 48331 RVA: 0x003997DE File Offset: 0x003979DE
		public int StarLevel
		{
			get
			{
				return this.m_StarLevel;
			}
			set
			{
				if (this.m_StarLevel == value)
				{
					return;
				}
				this.m_StarLevel = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x0600BCCE RID: 48334 RVA: 0x00399832 File Offset: 0x00397A32
		// (set) Token: 0x0600BCCD RID: 48333 RVA: 0x0039980C File Offset: 0x00397A0C
		public bool IsNewPlayer
		{
			get
			{
				return this.m_IsNewPlayer;
			}
			set
			{
				if (this.m_IsNewPlayer == value)
				{
					return;
				}
				this.m_IsNewPlayer = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x0600BCD0 RID: 48336 RVA: 0x00399860 File Offset: 0x00397A60
		// (set) Token: 0x0600BCCF RID: 48335 RVA: 0x0039983A File Offset: 0x00397A3A
		public RankedMedal.DisplayMode DisplayMode
		{
			get
			{
				return this.m_DisplayMode;
			}
			set
			{
				if (this.m_DisplayMode == value)
				{
					return;
				}
				this.m_DisplayMode = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x0600BCD2 RID: 48338 RVA: 0x0039988E File Offset: 0x00397A8E
		// (set) Token: 0x0600BCD1 RID: 48337 RVA: 0x00399868 File Offset: 0x00397A68
		public bool IsTooltipEnabled
		{
			get
			{
				return this.m_IsTooltipEnabled;
			}
			set
			{
				if (this.m_IsTooltipEnabled == value)
				{
					return;
				}
				this.m_IsTooltipEnabled = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x0600BCD4 RID: 48340 RVA: 0x003998C1 File Offset: 0x00397AC1
		// (set) Token: 0x0600BCD3 RID: 48339 RVA: 0x00399896 File Offset: 0x00397A96
		public Material MedalMaterial
		{
			get
			{
				return this.m_MedalMaterial;
			}
			set
			{
				if (this.m_MedalMaterial == value)
				{
					return;
				}
				this.m_MedalMaterial = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x0600BCD6 RID: 48342 RVA: 0x003998EF File Offset: 0x00397AEF
		// (set) Token: 0x0600BCD5 RID: 48341 RVA: 0x003998C9 File Offset: 0x00397AC9
		public bool HasEarnedCardBack
		{
			get
			{
				return this.m_HasEarnedCardBack;
			}
			set
			{
				if (this.m_HasEarnedCardBack == value)
				{
					return;
				}
				this.m_HasEarnedCardBack = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x0600BCD8 RID: 48344 RVA: 0x0039991D File Offset: 0x00397B1D
		// (set) Token: 0x0600BCD7 RID: 48343 RVA: 0x003998F7 File Offset: 0x00397AF7
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

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x0600BCD9 RID: 48345 RVA: 0x00399925 File Offset: 0x00397B25
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BCDA RID: 48346 RVA: 0x00399930 File Offset: 0x00397B30
		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			int starMultiplier = this.m_StarMultiplier;
			int num2 = (num + this.m_StarMultiplier.GetHashCode()) * 31;
			int stars = this.m_Stars;
			int num3 = (num2 + this.m_Stars.GetHashCode()) * 31;
			int maxStars = this.m_MaxStars;
			int num4 = (((num3 + this.m_MaxStars.GetHashCode()) * 31 + ((this.m_MedalTexture != null) ? this.m_MedalTexture.GetHashCode() : 0)) * 31 + ((this.m_MedalText != null) ? this.m_MedalText.GetHashCode() : 0)) * 31;
			bool isLegend = this.m_IsLegend;
			int num5 = (num4 + this.m_IsLegend.GetHashCode()) * 31;
			int legendRank = this.m_LegendRank;
			int num6 = ((num5 + this.m_LegendRank.GetHashCode()) * 31 + ((this.m_RankName != null) ? this.m_RankName.GetHashCode() : 0)) * 31;
			int starLevel = this.m_StarLevel;
			int num7 = (num6 + this.m_StarLevel.GetHashCode()) * 31;
			bool isNewPlayer = this.m_IsNewPlayer;
			int num8 = (num7 + this.m_IsNewPlayer.GetHashCode()) * 31;
			RankedMedal.DisplayMode displayMode = this.m_DisplayMode;
			int num9 = (num8 + this.m_DisplayMode.GetHashCode()) * 31;
			bool isTooltipEnabled = this.m_IsTooltipEnabled;
			int num10 = ((num9 + this.m_IsTooltipEnabled.GetHashCode()) * 31 + ((this.m_MedalMaterial != null) ? this.m_MedalMaterial.GetHashCode() : 0)) * 31;
			bool hasEarnedCardBack = this.m_HasEarnedCardBack;
			int num11 = (num10 + this.m_HasEarnedCardBack.GetHashCode()) * 31;
			FormatType formatType = this.m_FormatType;
			return num11 + this.m_FormatType.GetHashCode();
		}

		// Token: 0x0600BCDB RID: 48347 RVA: 0x00399AB4 File Offset: 0x00397CB4
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = this.m_StarMultiplier;
				return true;
			case 1:
				value = this.m_Stars;
				return true;
			case 2:
				value = this.m_MaxStars;
				return true;
			case 3:
				value = this.m_MedalTexture;
				return true;
			case 4:
				value = this.m_MedalText;
				return true;
			case 5:
				value = this.m_IsLegend;
				return true;
			case 6:
				value = this.m_LegendRank;
				return true;
			case 8:
				value = this.m_RankName;
				return true;
			case 9:
				value = this.m_StarLevel;
				return true;
			case 10:
				value = this.m_IsNewPlayer;
				return true;
			case 11:
				value = this.m_DisplayMode;
				return true;
			case 12:
				value = this.m_IsTooltipEnabled;
				return true;
			case 13:
				value = this.m_MedalMaterial;
				return true;
			case 14:
				value = this.m_HasEarnedCardBack;
				return true;
			case 15:
				value = this.m_FormatType;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600BCDC RID: 48348 RVA: 0x00399BE0 File Offset: 0x00397DE0
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				this.StarMultiplier = ((value != null) ? ((int)value) : 0);
				return true;
			case 1:
				this.Stars = ((value != null) ? ((int)value) : 0);
				return true;
			case 2:
				this.MaxStars = ((value != null) ? ((int)value) : 0);
				return true;
			case 3:
				this.MedalTexture = ((value != null) ? ((Texture)value) : null);
				return true;
			case 4:
				this.MedalText = ((value != null) ? ((string)value) : null);
				return true;
			case 5:
				this.IsLegend = (value != null && (bool)value);
				return true;
			case 6:
				this.LegendRank = ((value != null) ? ((int)value) : 0);
				return true;
			case 8:
				this.RankName = ((value != null) ? ((string)value) : null);
				return true;
			case 9:
				this.StarLevel = ((value != null) ? ((int)value) : 0);
				return true;
			case 10:
				this.IsNewPlayer = (value != null && (bool)value);
				return true;
			case 11:
				this.DisplayMode = ((value != null) ? ((RankedMedal.DisplayMode)value) : RankedMedal.DisplayMode.Default);
				return true;
			case 12:
				this.IsTooltipEnabled = (value != null && (bool)value);
				return true;
			case 13:
				this.MedalMaterial = ((value != null) ? ((Material)value) : null);
				return true;
			case 14:
				this.HasEarnedCardBack = (value != null && (bool)value);
				return true;
			case 15:
				this.FormatType = ((value != null) ? ((FormatType)value) : FormatType.FT_UNKNOWN);
				return true;
			}
			return false;
		}

		// Token: 0x0600BCDD RID: 48349 RVA: 0x00399D68 File Offset: 0x00397F68
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
			case 8:
				info = this.Properties[7];
				return true;
			case 9:
				info = this.Properties[8];
				return true;
			case 10:
				info = this.Properties[9];
				return true;
			case 11:
				info = this.Properties[10];
				return true;
			case 12:
				info = this.Properties[11];
				return true;
			case 13:
				info = this.Properties[12];
				return true;
			case 14:
				info = this.Properties[13];
				return true;
			case 15:
				info = this.Properties[14];
				return true;
			}
			info = default(DataModelProperty);
			return false;
		}

		// Token: 0x04009A2E RID: 39470
		public const int ModelId = 123;

		// Token: 0x04009A2F RID: 39471
		private int m_StarMultiplier;

		// Token: 0x04009A30 RID: 39472
		private int m_Stars;

		// Token: 0x04009A31 RID: 39473
		private int m_MaxStars;

		// Token: 0x04009A32 RID: 39474
		private Texture m_MedalTexture;

		// Token: 0x04009A33 RID: 39475
		private string m_MedalText;

		// Token: 0x04009A34 RID: 39476
		private bool m_IsLegend;

		// Token: 0x04009A35 RID: 39477
		private int m_LegendRank;

		// Token: 0x04009A36 RID: 39478
		private string m_RankName;

		// Token: 0x04009A37 RID: 39479
		private int m_StarLevel;

		// Token: 0x04009A38 RID: 39480
		private bool m_IsNewPlayer;

		// Token: 0x04009A39 RID: 39481
		private RankedMedal.DisplayMode m_DisplayMode;

		// Token: 0x04009A3A RID: 39482
		private bool m_IsTooltipEnabled;

		// Token: 0x04009A3B RID: 39483
		private Material m_MedalMaterial;

		// Token: 0x04009A3C RID: 39484
		private bool m_HasEarnedCardBack;

		// Token: 0x04009A3D RID: 39485
		private FormatType m_FormatType;

		// Token: 0x04009A3E RID: 39486
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "star_multiplier",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "stars",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "max_stars",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "medal_texture",
				Type = typeof(Texture)
			},
			new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "medal_text",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "is_legend",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 6,
				PropertyDisplayName = "legend_rank",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 8,
				PropertyDisplayName = "rank_name",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 9,
				PropertyDisplayName = "star_level",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 10,
				PropertyDisplayName = "is_new_player",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 11,
				PropertyDisplayName = "display_mode",
				Type = typeof(RankedMedal.DisplayMode)
			},
			new DataModelProperty
			{
				PropertyId = 12,
				PropertyDisplayName = "is_tooltip_enabled",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 13,
				PropertyDisplayName = "medal_material",
				Type = typeof(Material)
			},
			new DataModelProperty
			{
				PropertyId = 14,
				PropertyDisplayName = "has_earned_cardback",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 15,
				PropertyDisplayName = "format_type",
				Type = typeof(FormatType)
			}
		};
	}
}
