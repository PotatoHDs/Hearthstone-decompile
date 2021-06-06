using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class AdventureBookPageDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 2;

		private AdventureBookPageType m_PageType;

		private AdventureChapterDataModel m_ChapterData;

		private string m_NumChaptersCompletedText;

		private AdventureBookPageMoralAlignment m_MoralAlignment;

		private DataModelList<AdventureChapterDataModel> m_AllChaptersData = new DataModelList<AdventureChapterDataModel>();

		private string m_NumBossesDefeatedText;

		private string m_NumCardsCollectedText;

		private DataModelList<string> m_NumChaptersOwnedText = new DataModelList<string>();

		private bool m_AllChaptersCompletedInCurrentSection;

		private DataModelProperty[] m_properties;

		public int DataModelId => 2;

		public string DataModelDisplayName => "adventure_book_page";

		public AdventureBookPageType PageType
		{
			get
			{
				return m_PageType;
			}
			set
			{
				if (m_PageType != value)
				{
					m_PageType = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public AdventureChapterDataModel ChapterData
		{
			get
			{
				return m_ChapterData;
			}
			set
			{
				if (m_ChapterData != value)
				{
					RemoveNestedDataModel(m_ChapterData);
					RegisterNestedDataModel(value);
					m_ChapterData = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string NumChaptersCompletedText
		{
			get
			{
				return m_NumChaptersCompletedText;
			}
			set
			{
				if (!(m_NumChaptersCompletedText == value))
				{
					m_NumChaptersCompletedText = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public AdventureBookPageMoralAlignment MoralAlignment
		{
			get
			{
				return m_MoralAlignment;
			}
			set
			{
				if (m_MoralAlignment != value)
				{
					m_MoralAlignment = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelList<AdventureChapterDataModel> AllChaptersData
		{
			get
			{
				return m_AllChaptersData;
			}
			set
			{
				if (m_AllChaptersData != value)
				{
					RemoveNestedDataModel(m_AllChaptersData);
					RegisterNestedDataModel(value);
					m_AllChaptersData = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string NumBossesDefeatedText
		{
			get
			{
				return m_NumBossesDefeatedText;
			}
			set
			{
				if (!(m_NumBossesDefeatedText == value))
				{
					m_NumBossesDefeatedText = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string NumCardsCollectedText
		{
			get
			{
				return m_NumCardsCollectedText;
			}
			set
			{
				if (!(m_NumCardsCollectedText == value))
				{
					m_NumCardsCollectedText = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelList<string> NumChaptersOwnedText
		{
			get
			{
				return m_NumChaptersOwnedText;
			}
			set
			{
				if (m_NumChaptersOwnedText != value)
				{
					RemoveNestedDataModel(m_NumChaptersOwnedText);
					RegisterNestedDataModel(value);
					m_NumChaptersOwnedText = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool AllChaptersCompletedInCurrentSection
		{
			get
			{
				return m_AllChaptersCompletedInCurrentSection;
			}
			set
			{
				if (m_AllChaptersCompletedInCurrentSection != value)
				{
					m_AllChaptersCompletedInCurrentSection = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public AdventureBookPageDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[9];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "page_type",
				Type = typeof(AdventureBookPageType)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "current_chapter",
				Type = typeof(AdventureChapterDataModel)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "num_chapters_completed_text",
				Type = typeof(string)
			};
			array[2] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 128,
				PropertyDisplayName = "moral_alignment",
				Type = typeof(AdventureBookPageMoralAlignment)
			};
			array[3] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 129,
				PropertyDisplayName = "all_chapters",
				Type = typeof(DataModelList<AdventureChapterDataModel>)
			};
			array[4] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 131,
				PropertyDisplayName = "num_bosses_defeated_text",
				Type = typeof(string)
			};
			array[5] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 132,
				PropertyDisplayName = "num_cards_collected_text",
				Type = typeof(string)
			};
			array[6] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 134,
				PropertyDisplayName = "num_chapters_owned_text",
				Type = typeof(DataModelList<string>)
			};
			array[7] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 135,
				PropertyDisplayName = "all_chapters_completed_in_current_section",
				Type = typeof(bool)
			};
			array[8] = dataModelProperty;
			m_properties = array;
			base._002Ector();
			RegisterNestedDataModel(m_ChapterData);
			RegisterNestedDataModel(m_AllChaptersData);
			RegisterNestedDataModel(m_NumChaptersOwnedText);
		}

		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			_ = m_PageType;
			int num2 = (((num + m_PageType.GetHashCode()) * 31 + ((m_ChapterData != null) ? m_ChapterData.GetPropertiesHashCode() : 0)) * 31 + ((m_NumChaptersCompletedText != null) ? m_NumChaptersCompletedText.GetHashCode() : 0)) * 31;
			_ = m_MoralAlignment;
			int num3 = (((((num2 + m_MoralAlignment.GetHashCode()) * 31 + ((m_AllChaptersData != null) ? m_AllChaptersData.GetPropertiesHashCode() : 0)) * 31 + ((m_NumBossesDefeatedText != null) ? m_NumBossesDefeatedText.GetHashCode() : 0)) * 31 + ((m_NumCardsCollectedText != null) ? m_NumCardsCollectedText.GetHashCode() : 0)) * 31 + ((m_NumChaptersOwnedText != null) ? m_NumChaptersOwnedText.GetPropertiesHashCode() : 0)) * 31;
			_ = m_AllChaptersCompletedInCurrentSection;
			return num3 + m_AllChaptersCompletedInCurrentSection.GetHashCode();
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_PageType;
				return true;
			case 1:
				value = m_ChapterData;
				return true;
			case 2:
				value = m_NumChaptersCompletedText;
				return true;
			case 128:
				value = m_MoralAlignment;
				return true;
			case 129:
				value = m_AllChaptersData;
				return true;
			case 131:
				value = m_NumBossesDefeatedText;
				return true;
			case 132:
				value = m_NumCardsCollectedText;
				return true;
			case 134:
				value = m_NumChaptersOwnedText;
				return true;
			case 135:
				value = m_AllChaptersCompletedInCurrentSection;
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
				PageType = ((value != null) ? ((AdventureBookPageType)value) : AdventureBookPageType.INVALID);
				return true;
			case 1:
				ChapterData = ((value != null) ? ((AdventureChapterDataModel)value) : null);
				return true;
			case 2:
				NumChaptersCompletedText = ((value != null) ? ((string)value) : null);
				return true;
			case 128:
				MoralAlignment = ((value != null) ? ((AdventureBookPageMoralAlignment)value) : AdventureBookPageMoralAlignment.GOOD);
				return true;
			case 129:
				AllChaptersData = ((value != null) ? ((DataModelList<AdventureChapterDataModel>)value) : null);
				return true;
			case 131:
				NumBossesDefeatedText = ((value != null) ? ((string)value) : null);
				return true;
			case 132:
				NumCardsCollectedText = ((value != null) ? ((string)value) : null);
				return true;
			case 134:
				NumChaptersOwnedText = ((value != null) ? ((DataModelList<string>)value) : null);
				return true;
			case 135:
				AllChaptersCompletedInCurrentSection = value != null && (bool)value;
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
			case 128:
				info = Properties[3];
				return true;
			case 129:
				info = Properties[4];
				return true;
			case 131:
				info = Properties[5];
				return true;
			case 132:
				info = Properties[6];
				return true;
			case 134:
				info = Properties[7];
				return true;
			case 135:
				info = Properties[8];
				return true;
			default:
				info = default(DataModelProperty);
				return false;
			}
		}
	}
}
