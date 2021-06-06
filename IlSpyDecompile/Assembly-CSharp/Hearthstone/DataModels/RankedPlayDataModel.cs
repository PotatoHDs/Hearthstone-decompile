using Hearthstone.UI;
using PegasusShared;
using UnityEngine;

namespace Hearthstone.DataModels
{
	public class RankedPlayDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 123;

		private int m_StarMultiplier;

		private int m_Stars;

		private int m_MaxStars;

		private Texture m_MedalTexture;

		private string m_MedalText;

		private bool m_IsLegend;

		private int m_LegendRank;

		private string m_RankName;

		private int m_StarLevel;

		private bool m_IsNewPlayer;

		private RankedMedal.DisplayMode m_DisplayMode;

		private bool m_IsTooltipEnabled;

		private Material m_MedalMaterial;

		private bool m_HasEarnedCardBack;

		private FormatType m_FormatType;

		private DataModelProperty[] m_properties;

		public int DataModelId => 123;

		public string DataModelDisplayName => "rankedplay";

		public int StarMultiplier
		{
			get
			{
				return m_StarMultiplier;
			}
			set
			{
				if (m_StarMultiplier != value)
				{
					m_StarMultiplier = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int Stars
		{
			get
			{
				return m_Stars;
			}
			set
			{
				if (m_Stars != value)
				{
					m_Stars = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int MaxStars
		{
			get
			{
				return m_MaxStars;
			}
			set
			{
				if (m_MaxStars != value)
				{
					m_MaxStars = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public Texture MedalTexture
		{
			get
			{
				return m_MedalTexture;
			}
			set
			{
				if (!(m_MedalTexture == value))
				{
					m_MedalTexture = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string MedalText
		{
			get
			{
				return m_MedalText;
			}
			set
			{
				if (!(m_MedalText == value))
				{
					m_MedalText = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool IsLegend
		{
			get
			{
				return m_IsLegend;
			}
			set
			{
				if (m_IsLegend != value)
				{
					m_IsLegend = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int LegendRank
		{
			get
			{
				return m_LegendRank;
			}
			set
			{
				if (m_LegendRank != value)
				{
					m_LegendRank = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string RankName
		{
			get
			{
				return m_RankName;
			}
			set
			{
				if (!(m_RankName == value))
				{
					m_RankName = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int StarLevel
		{
			get
			{
				return m_StarLevel;
			}
			set
			{
				if (m_StarLevel != value)
				{
					m_StarLevel = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool IsNewPlayer
		{
			get
			{
				return m_IsNewPlayer;
			}
			set
			{
				if (m_IsNewPlayer != value)
				{
					m_IsNewPlayer = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public RankedMedal.DisplayMode DisplayMode
		{
			get
			{
				return m_DisplayMode;
			}
			set
			{
				if (m_DisplayMode != value)
				{
					m_DisplayMode = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool IsTooltipEnabled
		{
			get
			{
				return m_IsTooltipEnabled;
			}
			set
			{
				if (m_IsTooltipEnabled != value)
				{
					m_IsTooltipEnabled = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public Material MedalMaterial
		{
			get
			{
				return m_MedalMaterial;
			}
			set
			{
				if (!(m_MedalMaterial == value))
				{
					m_MedalMaterial = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool HasEarnedCardBack
		{
			get
			{
				return m_HasEarnedCardBack;
			}
			set
			{
				if (m_HasEarnedCardBack != value)
				{
					m_HasEarnedCardBack = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public FormatType FormatType
		{
			get
			{
				return m_FormatType;
			}
			set
			{
				if (m_FormatType != value)
				{
					m_FormatType = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public RankedPlayDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[15];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "star_multiplier",
				Type = typeof(int)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "stars",
				Type = typeof(int)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "max_stars",
				Type = typeof(int)
			};
			array[2] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "medal_texture",
				Type = typeof(Texture)
			};
			array[3] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "medal_text",
				Type = typeof(string)
			};
			array[4] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "is_legend",
				Type = typeof(bool)
			};
			array[5] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 6,
				PropertyDisplayName = "legend_rank",
				Type = typeof(int)
			};
			array[6] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 8,
				PropertyDisplayName = "rank_name",
				Type = typeof(string)
			};
			array[7] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 9,
				PropertyDisplayName = "star_level",
				Type = typeof(int)
			};
			array[8] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 10,
				PropertyDisplayName = "is_new_player",
				Type = typeof(bool)
			};
			array[9] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 11,
				PropertyDisplayName = "display_mode",
				Type = typeof(RankedMedal.DisplayMode)
			};
			array[10] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 12,
				PropertyDisplayName = "is_tooltip_enabled",
				Type = typeof(bool)
			};
			array[11] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 13,
				PropertyDisplayName = "medal_material",
				Type = typeof(Material)
			};
			array[12] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 14,
				PropertyDisplayName = "has_earned_cardback",
				Type = typeof(bool)
			};
			array[13] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 15,
				PropertyDisplayName = "format_type",
				Type = typeof(FormatType)
			};
			array[14] = dataModelProperty;
			m_properties = array;
			base._002Ector();
		}

		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			_ = m_StarMultiplier;
			int num2 = (num + m_StarMultiplier.GetHashCode()) * 31;
			_ = m_Stars;
			int num3 = (num2 + m_Stars.GetHashCode()) * 31;
			_ = m_MaxStars;
			int num4 = (((num3 + m_MaxStars.GetHashCode()) * 31 + ((m_MedalTexture != null) ? m_MedalTexture.GetHashCode() : 0)) * 31 + ((m_MedalText != null) ? m_MedalText.GetHashCode() : 0)) * 31;
			_ = m_IsLegend;
			int num5 = (num4 + m_IsLegend.GetHashCode()) * 31;
			_ = m_LegendRank;
			int num6 = ((num5 + m_LegendRank.GetHashCode()) * 31 + ((m_RankName != null) ? m_RankName.GetHashCode() : 0)) * 31;
			_ = m_StarLevel;
			int num7 = (num6 + m_StarLevel.GetHashCode()) * 31;
			_ = m_IsNewPlayer;
			int num8 = (num7 + m_IsNewPlayer.GetHashCode()) * 31;
			_ = m_DisplayMode;
			int num9 = (num8 + m_DisplayMode.GetHashCode()) * 31;
			_ = m_IsTooltipEnabled;
			int num10 = ((num9 + m_IsTooltipEnabled.GetHashCode()) * 31 + ((m_MedalMaterial != null) ? m_MedalMaterial.GetHashCode() : 0)) * 31;
			_ = m_HasEarnedCardBack;
			int num11 = (num10 + m_HasEarnedCardBack.GetHashCode()) * 31;
			_ = m_FormatType;
			return num11 + m_FormatType.GetHashCode();
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_StarMultiplier;
				return true;
			case 1:
				value = m_Stars;
				return true;
			case 2:
				value = m_MaxStars;
				return true;
			case 3:
				value = m_MedalTexture;
				return true;
			case 4:
				value = m_MedalText;
				return true;
			case 5:
				value = m_IsLegend;
				return true;
			case 6:
				value = m_LegendRank;
				return true;
			case 8:
				value = m_RankName;
				return true;
			case 9:
				value = m_StarLevel;
				return true;
			case 10:
				value = m_IsNewPlayer;
				return true;
			case 11:
				value = m_DisplayMode;
				return true;
			case 12:
				value = m_IsTooltipEnabled;
				return true;
			case 13:
				value = m_MedalMaterial;
				return true;
			case 14:
				value = m_HasEarnedCardBack;
				return true;
			case 15:
				value = m_FormatType;
				return true;
			default:
				value = null;
				return false;
			}
		}

		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				StarMultiplier = ((value != null) ? ((int)value) : 0);
				return true;
			case 1:
				Stars = ((value != null) ? ((int)value) : 0);
				return true;
			case 2:
				MaxStars = ((value != null) ? ((int)value) : 0);
				return true;
			case 3:
				MedalTexture = ((value != null) ? ((Texture)value) : null);
				return true;
			case 4:
				MedalText = ((value != null) ? ((string)value) : null);
				return true;
			case 5:
				IsLegend = value != null && (bool)value;
				return true;
			case 6:
				LegendRank = ((value != null) ? ((int)value) : 0);
				return true;
			case 8:
				RankName = ((value != null) ? ((string)value) : null);
				return true;
			case 9:
				StarLevel = ((value != null) ? ((int)value) : 0);
				return true;
			case 10:
				IsNewPlayer = value != null && (bool)value;
				return true;
			case 11:
				DisplayMode = ((value != null) ? ((RankedMedal.DisplayMode)value) : RankedMedal.DisplayMode.Default);
				return true;
			case 12:
				IsTooltipEnabled = value != null && (bool)value;
				return true;
			case 13:
				MedalMaterial = ((value != null) ? ((Material)value) : null);
				return true;
			case 14:
				HasEarnedCardBack = value != null && (bool)value;
				return true;
			case 15:
				FormatType = ((value != null) ? ((FormatType)value) : FormatType.FT_UNKNOWN);
				return true;
			default:
				return false;
			}
		}

		public bool GetPropertyInfo(int id, out DataModelProperty info)
		{
			switch (id)
			{
			case 0:
				info = Properties[0];
				return true;
			case 1:
				info = Properties[1];
				return true;
			case 2:
				info = Properties[2];
				return true;
			case 3:
				info = Properties[3];
				return true;
			case 4:
				info = Properties[4];
				return true;
			case 5:
				info = Properties[5];
				return true;
			case 6:
				info = Properties[6];
				return true;
			case 8:
				info = Properties[7];
				return true;
			case 9:
				info = Properties[8];
				return true;
			case 10:
				info = Properties[9];
				return true;
			case 11:
				info = Properties[10];
				return true;
			case 12:
				info = Properties[11];
				return true;
			case 13:
				info = Properties[12];
				return true;
			case 14:
				info = Properties[13];
				return true;
			case 15:
				info = Properties[14];
				return true;
			default:
				info = default(DataModelProperty);
				return false;
			}
		}
	}
}
