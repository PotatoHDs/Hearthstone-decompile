using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010A1 RID: 4257
	public class AdventureBookPageDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600B8DA RID: 47322 RVA: 0x0038960C File Offset: 0x0038780C
		public AdventureBookPageDataModel()
		{
			base.RegisterNestedDataModel(this.m_ChapterData);
			base.RegisterNestedDataModel(this.m_AllChaptersData);
			base.RegisterNestedDataModel(this.m_NumChaptersOwnedText);
		}

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x0600B8DB RID: 47323 RVA: 0x000052D6 File Offset: 0x000034D6
		public int DataModelId
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x0600B8DC RID: 47324 RVA: 0x0038985B File Offset: 0x00387A5B
		public string DataModelDisplayName
		{
			get
			{
				return "adventure_book_page";
			}
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x0600B8DE RID: 47326 RVA: 0x00389888 File Offset: 0x00387A88
		// (set) Token: 0x0600B8DD RID: 47325 RVA: 0x00389862 File Offset: 0x00387A62
		public AdventureBookPageType PageType
		{
			get
			{
				return this.m_PageType;
			}
			set
			{
				if (this.m_PageType == value)
				{
					return;
				}
				this.m_PageType = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x0600B8E0 RID: 47328 RVA: 0x003898C9 File Offset: 0x00387AC9
		// (set) Token: 0x0600B8DF RID: 47327 RVA: 0x00389890 File Offset: 0x00387A90
		public AdventureChapterDataModel ChapterData
		{
			get
			{
				return this.m_ChapterData;
			}
			set
			{
				if (this.m_ChapterData == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_ChapterData);
				base.RegisterNestedDataModel(value);
				this.m_ChapterData = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x0600B8E2 RID: 47330 RVA: 0x003898FC File Offset: 0x00387AFC
		// (set) Token: 0x0600B8E1 RID: 47329 RVA: 0x003898D1 File Offset: 0x00387AD1
		public string NumChaptersCompletedText
		{
			get
			{
				return this.m_NumChaptersCompletedText;
			}
			set
			{
				if (this.m_NumChaptersCompletedText == value)
				{
					return;
				}
				this.m_NumChaptersCompletedText = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x0600B8E4 RID: 47332 RVA: 0x0038992A File Offset: 0x00387B2A
		// (set) Token: 0x0600B8E3 RID: 47331 RVA: 0x00389904 File Offset: 0x00387B04
		public AdventureBookPageMoralAlignment MoralAlignment
		{
			get
			{
				return this.m_MoralAlignment;
			}
			set
			{
				if (this.m_MoralAlignment == value)
				{
					return;
				}
				this.m_MoralAlignment = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x0600B8E6 RID: 47334 RVA: 0x0038996B File Offset: 0x00387B6B
		// (set) Token: 0x0600B8E5 RID: 47333 RVA: 0x00389932 File Offset: 0x00387B32
		public DataModelList<AdventureChapterDataModel> AllChaptersData
		{
			get
			{
				return this.m_AllChaptersData;
			}
			set
			{
				if (this.m_AllChaptersData == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_AllChaptersData);
				base.RegisterNestedDataModel(value);
				this.m_AllChaptersData = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x0600B8E8 RID: 47336 RVA: 0x0038999E File Offset: 0x00387B9E
		// (set) Token: 0x0600B8E7 RID: 47335 RVA: 0x00389973 File Offset: 0x00387B73
		public string NumBossesDefeatedText
		{
			get
			{
				return this.m_NumBossesDefeatedText;
			}
			set
			{
				if (this.m_NumBossesDefeatedText == value)
				{
					return;
				}
				this.m_NumBossesDefeatedText = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x0600B8EA RID: 47338 RVA: 0x003899D1 File Offset: 0x00387BD1
		// (set) Token: 0x0600B8E9 RID: 47337 RVA: 0x003899A6 File Offset: 0x00387BA6
		public string NumCardsCollectedText
		{
			get
			{
				return this.m_NumCardsCollectedText;
			}
			set
			{
				if (this.m_NumCardsCollectedText == value)
				{
					return;
				}
				this.m_NumCardsCollectedText = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x0600B8EC RID: 47340 RVA: 0x00389A12 File Offset: 0x00387C12
		// (set) Token: 0x0600B8EB RID: 47339 RVA: 0x003899D9 File Offset: 0x00387BD9
		public DataModelList<string> NumChaptersOwnedText
		{
			get
			{
				return this.m_NumChaptersOwnedText;
			}
			set
			{
				if (this.m_NumChaptersOwnedText == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_NumChaptersOwnedText);
				base.RegisterNestedDataModel(value);
				this.m_NumChaptersOwnedText = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x0600B8EE RID: 47342 RVA: 0x00389A40 File Offset: 0x00387C40
		// (set) Token: 0x0600B8ED RID: 47341 RVA: 0x00389A1A File Offset: 0x00387C1A
		public bool AllChaptersCompletedInCurrentSection
		{
			get
			{
				return this.m_AllChaptersCompletedInCurrentSection;
			}
			set
			{
				if (this.m_AllChaptersCompletedInCurrentSection == value)
				{
					return;
				}
				this.m_AllChaptersCompletedInCurrentSection = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x0600B8EF RID: 47343 RVA: 0x00389A48 File Offset: 0x00387C48
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600B8F0 RID: 47344 RVA: 0x00389A50 File Offset: 0x00387C50
		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			AdventureBookPageType pageType = this.m_PageType;
			int num2 = (((num + this.m_PageType.GetHashCode()) * 31 + ((this.m_ChapterData != null) ? this.m_ChapterData.GetPropertiesHashCode() : 0)) * 31 + ((this.m_NumChaptersCompletedText != null) ? this.m_NumChaptersCompletedText.GetHashCode() : 0)) * 31;
			AdventureBookPageMoralAlignment moralAlignment = this.m_MoralAlignment;
			int num3 = (((((num2 + this.m_MoralAlignment.GetHashCode()) * 31 + ((this.m_AllChaptersData != null) ? this.m_AllChaptersData.GetPropertiesHashCode() : 0)) * 31 + ((this.m_NumBossesDefeatedText != null) ? this.m_NumBossesDefeatedText.GetHashCode() : 0)) * 31 + ((this.m_NumCardsCollectedText != null) ? this.m_NumCardsCollectedText.GetHashCode() : 0)) * 31 + ((this.m_NumChaptersOwnedText != null) ? this.m_NumChaptersOwnedText.GetPropertiesHashCode() : 0)) * 31;
			bool allChaptersCompletedInCurrentSection = this.m_AllChaptersCompletedInCurrentSection;
			return num3 + this.m_AllChaptersCompletedInCurrentSection.GetHashCode();
		}

		// Token: 0x0600B8F1 RID: 47345 RVA: 0x00389B4C File Offset: 0x00387D4C
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = this.m_PageType;
				return true;
			case 1:
				value = this.m_ChapterData;
				return true;
			case 2:
				value = this.m_NumChaptersCompletedText;
				return true;
			default:
				switch (id)
				{
				case 128:
					value = this.m_MoralAlignment;
					return true;
				case 129:
					value = this.m_AllChaptersData;
					return true;
				case 131:
					value = this.m_NumBossesDefeatedText;
					return true;
				case 132:
					value = this.m_NumCardsCollectedText;
					return true;
				case 134:
					value = this.m_NumChaptersOwnedText;
					return true;
				case 135:
					value = this.m_AllChaptersCompletedInCurrentSection;
					return true;
				}
				value = null;
				return false;
			}
		}

		// Token: 0x0600B8F2 RID: 47346 RVA: 0x00389C08 File Offset: 0x00387E08
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				this.PageType = ((value != null) ? ((AdventureBookPageType)value) : AdventureBookPageType.INVALID);
				return true;
			case 1:
				this.ChapterData = ((value != null) ? ((AdventureChapterDataModel)value) : null);
				return true;
			case 2:
				this.NumChaptersCompletedText = ((value != null) ? ((string)value) : null);
				return true;
			default:
				switch (id)
				{
				case 128:
					this.MoralAlignment = ((value != null) ? ((AdventureBookPageMoralAlignment)value) : AdventureBookPageMoralAlignment.GOOD);
					return true;
				case 129:
					this.AllChaptersData = ((value != null) ? ((DataModelList<AdventureChapterDataModel>)value) : null);
					return true;
				case 131:
					this.NumBossesDefeatedText = ((value != null) ? ((string)value) : null);
					return true;
				case 132:
					this.NumCardsCollectedText = ((value != null) ? ((string)value) : null);
					return true;
				case 134:
					this.NumChaptersOwnedText = ((value != null) ? ((DataModelList<string>)value) : null);
					return true;
				case 135:
					this.AllChaptersCompletedInCurrentSection = (value != null && (bool)value);
					return true;
				}
				return false;
			}
		}

		// Token: 0x0600B8F3 RID: 47347 RVA: 0x00389D10 File Offset: 0x00387F10
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
			default:
				switch (id)
				{
				case 128:
					info = this.Properties[3];
					return true;
				case 129:
					info = this.Properties[4];
					return true;
				case 131:
					info = this.Properties[5];
					return true;
				case 132:
					info = this.Properties[6];
					return true;
				case 134:
					info = this.Properties[7];
					return true;
				case 135:
					info = this.Properties[8];
					return true;
				}
				info = default(DataModelProperty);
				return false;
			}
		}

		// Token: 0x040098A5 RID: 39077
		public const int ModelId = 2;

		// Token: 0x040098A6 RID: 39078
		private AdventureBookPageType m_PageType;

		// Token: 0x040098A7 RID: 39079
		private AdventureChapterDataModel m_ChapterData;

		// Token: 0x040098A8 RID: 39080
		private string m_NumChaptersCompletedText;

		// Token: 0x040098A9 RID: 39081
		private AdventureBookPageMoralAlignment m_MoralAlignment;

		// Token: 0x040098AA RID: 39082
		private DataModelList<AdventureChapterDataModel> m_AllChaptersData = new DataModelList<AdventureChapterDataModel>();

		// Token: 0x040098AB RID: 39083
		private string m_NumBossesDefeatedText;

		// Token: 0x040098AC RID: 39084
		private string m_NumCardsCollectedText;

		// Token: 0x040098AD RID: 39085
		private DataModelList<string> m_NumChaptersOwnedText = new DataModelList<string>();

		// Token: 0x040098AE RID: 39086
		private bool m_AllChaptersCompletedInCurrentSection;

		// Token: 0x040098AF RID: 39087
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "page_type",
				Type = typeof(AdventureBookPageType)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "current_chapter",
				Type = typeof(AdventureChapterDataModel)
			},
			new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "num_chapters_completed_text",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 128,
				PropertyDisplayName = "moral_alignment",
				Type = typeof(AdventureBookPageMoralAlignment)
			},
			new DataModelProperty
			{
				PropertyId = 129,
				PropertyDisplayName = "all_chapters",
				Type = typeof(DataModelList<AdventureChapterDataModel>)
			},
			new DataModelProperty
			{
				PropertyId = 131,
				PropertyDisplayName = "num_bosses_defeated_text",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 132,
				PropertyDisplayName = "num_cards_collected_text",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 134,
				PropertyDisplayName = "num_chapters_owned_text",
				Type = typeof(DataModelList<string>)
			},
			new DataModelProperty
			{
				PropertyId = 135,
				PropertyDisplayName = "all_chapters_completed_in_current_section",
				Type = typeof(bool)
			}
		};
	}
}
