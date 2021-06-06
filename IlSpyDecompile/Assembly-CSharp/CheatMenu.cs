using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CustomEditClass]
public class CheatMenu : MonoBehaviour
{
	[CustomEditField(Sections = "TabGroups")]
	public List<GameObject> groups = new List<GameObject>();

	private int ActiveTabGroupIndex;

	[CustomEditField(Sections = "Arrows")]
	public GameObject LeftArrow;

	[CustomEditField(Sections = "Arrows")]
	public GameObject RightArrow;

	[CustomEditField(Sections = "Tabs")]
	public List<GameObject> tabs = new List<GameObject>();

	[CustomEditField(Sections = "Tabs")]
	public List<GameObject> contents = new List<GameObject>();

	private int ActiveTabIndex;

	private GameObject ActiveTabContents;

	[CustomEditField(Sections = "Tab_00_Contents")]
	public GameObject m_maxManaButton;

	[CustomEditField(Sections = "Tab_00_Contents")]
	public GameObject m_fullHealthButton;

	[CustomEditField(Sections = "Tab_00_Contents")]
	public GameObject m_SetHealthToOneButton;

	[CustomEditField(Sections = "Tab_00_Contents")]
	public GameObject m_ImmuneCheckMark;

	[CustomEditField(Sections = "Tab_00_Contents")]
	public GameObject m_ClearMinionsButton;

	[CustomEditField(Sections = "Tab_00_Contents")]
	public GameObject m_ClearHandButton;

	[CustomEditField(Sections = "Tab_00_Contents")]
	public GameObject m_destroyButton;

	[CustomEditField(Sections = "Tab_01_Contents")]
	public GameObject SearchTab;

	[CustomEditField(Sections = "Tab_01_Contents")]
	public GameObject PinnedTab;

	[CustomEditField(Sections = "Tab_01_Contents")]
	public GameObject SearchTabContents;

	[CustomEditField(Sections = "Tab_01_Contents")]
	public GameObject PinnedTabContents;

	[CustomEditField(Sections = "Tab_01_Contents")]
	public GameObject SearchInputField;

	[CustomEditField(Sections = "Tab_01_Contents")]
	public GameObject PinnedInputField;

	[CustomEditField(Sections = "Tab_01_Contents")]
	public GameObject exportCardButton;

	[CustomEditField(Sections = "Tab_01_Contents")]
	public GameObject m_GoldenCheckMark;

	[CustomEditField(Sections = "Tab_01_Contents")]
	public GameObject m_PinItCheckMark;

	[CustomEditField(Sections = "Tab_01_Contents")]
	public GameObject m_SearchResultItem;

	[CustomEditField(Sections = "Tab_01_Contents")]
	public GameObject m_PreviewCard;

	private TAG_PREMIUM m_premiumType;

	[CustomEditField(Sections = "Tab_02_Contents")]
	public GameObject m_runConsoleButton;

	[CustomEditField(Sections = "Tab_02_Contents")]
	public InputField m_scriptContent;

	private int tutorialProgress;

	private int DustInput;

	private int GoldInput;

	private int TicketsInput;

	[CustomEditField(Sections = "Tab_04_General")]
	public GameObject m_HUDcheckMark;

	private bool isHUDactive = true;

	[CustomEditField(Sections = "Tab_04_General")]
	public GameObject m_HideHistorycheckMark;

	private bool isHistoryactive = true;

	[CustomEditField(Sections = "Tab_04_General")]
	public GameObject m_SetboardInputField;

	private Dictionary<string, CardDbfRecord> m_allCardRecords;

	private string m_selectedCard;

	private GameObject m_cardPreview;

	private void Start()
	{
		if (ActiveTabGroupIndex > 0)
		{
			LeftArrow.SetActive(value: true);
		}
		else
		{
			LeftArrow.SetActive(value: false);
		}
		if (ActiveTabGroupIndex < groups.Count - 1)
		{
			RightArrow.SetActive(value: true);
		}
		else
		{
			RightArrow.SetActive(value: false);
		}
		ActiveTabContents = contents[ActiveTabIndex];
		for (int i = 0; i < contents.Count; i++)
		{
			if (i == ActiveTabIndex)
			{
				ColorBlock colors = tabs[ActiveTabIndex].GetComponentInChildren<Button>().colors;
				colors.normalColor = Color.white;
				tabs[ActiveTabIndex].GetComponentInChildren<Button>().colors = colors;
				contents[ActiveTabIndex].SetActive(value: true);
			}
			else
			{
				ColorBlock colors2 = tabs[ActiveTabIndex].GetComponentInChildren<Button>().colors;
				colors2.normalColor = Color.clear;
				tabs[ActiveTabIndex].GetComponentInChildren<Button>().colors = colors2;
				contents[i].SetActive(value: false);
			}
		}
		ActiveTabContents.SetActive(value: true);
	}

	private void OnEnable()
	{
		Debug.Log("Enabled");
		m_allCardRecords = new Dictionary<string, CardDbfRecord>();
		foreach (string allCardId in GameUtils.GetAllCardIds())
		{
			m_allCardRecords[allCardId] = GameUtils.GetCardRecord(allCardId);
		}
	}

	private void OnDisable()
	{
	}

	public void SetAsActiveTab(int tabIndex)
	{
		Debug.Log("Tab Index " + tabIndex);
		ColorBlock colors = tabs[ActiveTabIndex].GetComponentInChildren<Button>().colors;
		colors.normalColor = Color.clear;
		tabs[ActiveTabIndex].GetComponentInChildren<Button>().colors = colors;
		if (ActiveTabContents != null)
		{
			ActiveTabContents.SetActive(value: false);
		}
		ActiveTabIndex = tabIndex;
		ActiveTabContents = contents[ActiveTabIndex];
		ActiveTabContents.SetActive(value: true);
		colors = tabs[ActiveTabIndex].GetComponentInChildren<Button>().colors;
		colors.normalColor = Color.white;
		tabs[ActiveTabIndex].GetComponentInChildren<Button>().colors = colors;
	}

	public void ShiftGroup(int indexChange)
	{
		groups[ActiveTabGroupIndex].SetActive(value: false);
		ActiveTabGroupIndex += indexChange;
		groups[ActiveTabGroupIndex].SetActive(value: true);
		if (ActiveTabGroupIndex > 0)
		{
			LeftArrow.SetActive(value: true);
		}
		else
		{
			LeftArrow.SetActive(value: false);
		}
		if (ActiveTabGroupIndex < groups.Count - 1)
		{
			RightArrow.SetActive(value: true);
		}
		else
		{
			RightArrow.SetActive(value: false);
		}
	}

	public void MaxMana()
	{
		if (Network.IsRunning())
		{
			string command = "maxmana friendly";
			Network.Get().SendDebugConsoleCommand(command);
		}
	}

	public void FullHealth()
	{
		if (Network.IsRunning())
		{
			string command = "healhero friendly";
			Network.Get().SendDebugConsoleCommand(command);
		}
	}

	public void SetHealthToOne()
	{
		if (Network.IsRunning())
		{
			string command = "spawncard XXX_107 friendly hand 0";
			Network.Get().SendDebugConsoleCommand(command);
		}
	}

	public void SetImmune()
	{
		Debug.Log("Cheat: SetImmune function called");
	}

	public void ClearMinions()
	{
		if (Network.IsRunning())
		{
			string command = "spawncard XXX_018 friendly hand 0";
			Network.Get().SendDebugConsoleCommand(command);
		}
	}

	public void Discard()
	{
		if (Network.IsRunning())
		{
			string command = "cyclehand friendly";
			Network.Get().SendDebugConsoleCommand(command);
		}
	}

	public void DrawCard()
	{
		if (Network.IsRunning())
		{
			string command = "drawcard friendly";
			Network.Get().SendDebugConsoleCommand(command);
		}
	}

	public void Destroy()
	{
		Debug.Log("Cheat: Destroy function called");
	}

	public void SearchOnValueChanged()
	{
		string text = SearchInputField.GetComponent<InputField>().text;
		Debug.Log("Search keyword changed to: " + text);
	}

	public void SearchOnEndEdit()
	{
		if (m_allCardRecords.Count == 0)
		{
			DefLoader.Get().Clear();
			Localization.SetLocale(Locale.enUS);
			GameDbf.Load();
			GameStrings.ReloadAll();
			foreach (string allCardId in GameUtils.GetAllCardIds())
			{
				m_allCardRecords[allCardId] = GameUtils.GetCardRecord(allCardId);
			}
			DefLoader.Get().LoadAllEntityDefs();
		}
		string text = SearchInputField.GetComponent<InputField>().text.ToLower();
		Debug.Log("User pressed 'enter'. Keyword: " + text);
		Transform transform = SearchTabContents.transform.Find("Search Results List").transform.Find("Search Result Items").transform;
		for (int i = 0; i < transform.childCount; i++)
		{
			Object.Destroy(transform.GetChild(i).gameObject);
		}
		if (string.IsNullOrEmpty(text) || m_allCardRecords.Count <= 0)
		{
			return;
		}
		Vector3 localPosition = new Vector3(0f, 0f, -73f);
		Vector3 one = Vector3.one;
		foreach (KeyValuePair<string, CardDbfRecord> allCardRecord in m_allCardRecords)
		{
			if ((allCardRecord.Key + allCardRecord.Value.Name.GetString(Locale.enUS).ToLower()).Contains(text))
			{
				GameObject gameObject = Object.Instantiate(m_SearchResultItem);
				SearchResultItem result = gameObject.GetComponent<SearchResultItem>();
				result.m_text = allCardRecord.Value.Name.GetString(Locale.enUS);
				result.m_card = allCardRecord.Key;
				gameObject.name = "Item";
				gameObject.transform.SetParent(transform);
				gameObject.transform.localPosition = localPosition;
				gameObject.transform.localRotation = Quaternion.identity;
				gameObject.transform.localScale = one;
				gameObject.GetComponent<Button>().onClick.AddListener(delegate
				{
					CardSelectedHandler(result);
				});
			}
		}
	}

	public void CardSelectedHandler(SearchResultItem item)
	{
		Debug.Log(item.m_text);
		m_selectedCard = item.m_card;
		PreviewCard();
	}

	private void PreviewCard()
	{
		if (m_cardPreview != null)
		{
			Object.Destroy(m_cardPreview);
		}
		m_cardPreview = LoadCard(m_selectedCard, m_premiumType);
	}

	private GameObject LoadCard(string cardID, TAG_PREMIUM premium)
	{
		using DefLoader.DisposableFullDef disposableFullDef = DefLoader.Get().GetFullDef(cardID, new CardPortraitQuality(3, premium));
		string handActor = ActorNames.GetHandActor(disposableFullDef.EntityDef, premium);
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(handActor, AssetLoadingOptions.IgnorePrefabPosition);
		Actor component = gameObject.GetComponent<Actor>();
		if (component == null)
		{
			Debug.LogWarning($"Error getting Actor for: {cardID}");
			return null;
		}
		m_PreviewCard.SetActive(value: false);
		component.SetPremium(premium);
		component.SetEntityDef(disposableFullDef.EntityDef);
		component.SetCardDef(disposableFullDef.DisposableCardDef);
		component.UpdateAllComponents();
		component.SetUnlit();
		gameObject.transform.SetParent(contents[1].transform, worldPositionStays: false);
		gameObject.transform.localPosition = m_PreviewCard.transform.localPosition;
		gameObject.transform.localRotation = Quaternion.identity;
		gameObject.transform.localScale = m_PreviewCard.transform.localScale;
		Transform[] componentsInChildren = gameObject.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].gameObject.layer = LayerMask.NameToLayer("UI");
		}
		gameObject.layer = LayerMask.NameToLayer("UI");
		return gameObject;
	}

	public void PinnedOnValueChanged()
	{
		string text = PinnedInputField.GetComponent<InputField>().text;
		Debug.Log("Pinned keyword changed to: " + text);
	}

	public void PinnedOnEndEdit()
	{
		string text = PinnedInputField.GetComponent<InputField>().text;
		Debug.Log("User pressed 'enter'. Keyword: " + text);
	}

	public void ShowSearchTab()
	{
		Debug.Log("Showing Search");
		SearchTabContents.SetActive(value: true);
		PinnedTabContents.SetActive(value: false);
		Vector3 localPosition = SearchTab.GetComponent<RectTransform>().localPosition;
		SearchTab.GetComponent<RectTransform>().localPosition = new Vector3(localPosition.x, localPosition.y, 0.109f);
		Vector3 localPosition2 = PinnedTab.GetComponent<RectTransform>().localPosition;
		PinnedTab.GetComponent<RectTransform>().localPosition = new Vector3(localPosition2.x, localPosition2.y, 0.095f);
	}

	public void ShowPinnedTab()
	{
		Debug.Log("Showing Pinned Items");
		SearchTabContents.SetActive(value: false);
		PinnedTabContents.SetActive(value: true);
		Vector3 localPosition = SearchTab.GetComponent<RectTransform>().localPosition;
		SearchTab.GetComponent<RectTransform>().localPosition = new Vector3(localPosition.x, localPosition.y, 0.095f);
		Vector3 localPosition2 = PinnedTab.GetComponent<RectTransform>().localPosition;
		PinnedTab.GetComponent<RectTransform>().localPosition = new Vector3(localPosition2.x, localPosition2.y, 0.109f);
	}

	public void PreviewCard(GameObject textObj)
	{
		string text = textObj.GetComponent<Text>().text;
		Debug.Log("Search Result Click. Previewing: " + text);
	}

	public void ToggleGolden()
	{
		Debug.Log("Cheat: ToggleGolden function called");
		m_GoldenCheckMark.SetActive(!m_GoldenCheckMark.activeSelf);
		if (m_premiumType == TAG_PREMIUM.GOLDEN || m_premiumType == TAG_PREMIUM.DIAMOND)
		{
			m_premiumType = TAG_PREMIUM.NORMAL;
		}
		else
		{
			m_premiumType = TAG_PREMIUM.GOLDEN;
		}
		PreviewCard();
	}

	public void ExportCard()
	{
		Debug.Log("Export Card function called");
	}

	public void AddCardTo(string location)
	{
		switch (location)
		{
		case "opponentHand":
			Debug.Log("AddCardTo function called. Adding card to Opponent's Hand");
			if (Network.IsRunning())
			{
				string command4 = $"spawncard {m_selectedCard} opponent hand 0";
				Network.Get().SendDebugConsoleCommand(command4);
			}
			break;
		case "opponentField":
			Debug.Log("AddCardTo function called. Adding card to Opponent's Field");
			if (Network.IsRunning())
			{
				string command6 = $"spawncard {m_selectedCard} opponent play 0";
				Network.Get().SendDebugConsoleCommand(command6);
			}
			break;
		case "opponentDeck":
			Debug.Log("AddCardTo function called. Adding card to Opponent's Deck");
			if (Network.IsRunning())
			{
				string command2 = $"spawncard {m_selectedCard} opponent deck 0";
				Network.Get().SendDebugConsoleCommand(command2);
			}
			break;
		case "yourField":
			Debug.Log("AddCardTo function called. Adding card to Your Field");
			if (Network.IsRunning())
			{
				string command5 = $"spawncard {m_selectedCard} friendly play 0";
				Network.Get().SendDebugConsoleCommand(command5);
			}
			break;
		case "yourHand":
			Debug.Log("AddCardTo function called. Adding card to Your Hand");
			if (Network.IsRunning())
			{
				string command3 = $"spawncard {m_selectedCard} friendly hand 0";
				Network.Get().SendDebugConsoleCommand(command3);
			}
			break;
		case "yourDeck":
			Debug.Log("AddCardTo function called. Adding card to Your Deck");
			if (Network.IsRunning())
			{
				string command = $"spawncard {m_selectedCard} friendly deck 0";
				Network.Get().SendDebugConsoleCommand(command);
			}
			break;
		}
	}

	public void RunConsole()
	{
		Debug.Log("Cheat: RunConsole function called");
	}

	public void ClearConsole()
	{
		m_scriptContent.text = "";
	}

	public void CopyCheatLine()
	{
	}

	public void PasteCheatLine()
	{
	}

	public void DustValueInput(InputField input)
	{
		DustInput = int.Parse(input.text);
		Debug.Log("Arcane Dust input field changed to: " + DustInput);
	}

	public void GoldValueInput(InputField input)
	{
		GoldInput = int.Parse(input.text);
		Debug.Log("Gold input field changed to: " + GoldInput);
	}

	public void TicketValueInput(InputField input)
	{
		TicketsInput = int.Parse(input.text);
		Debug.Log("Tickets input field changed to: " + TicketsInput);
	}

	public void TutorialDropdownValueChanged(int value)
	{
		tutorialProgress = value;
		Debug.Log("Tut: " + tutorialProgress);
	}

	public void SetTutorialProgress()
	{
		switch (tutorialProgress)
		{
		case 0:
			Debug.Log("Tutorial Progress set to: " + tutorialProgress + " : Hogger");
			break;
		case 1:
			Debug.Log("Tutorial Progress set to: " + tutorialProgress + " : Manastorm");
			break;
		case 2:
			Debug.Log("Tutorial Progress set to: " + tutorialProgress + " : Lorewalker");
			break;
		case 3:
			Debug.Log("Tutorial Progress set to: " + tutorialProgress + " : King Mukla");
			break;
		case 4:
			Debug.Log("Tutorial Progress set to: " + tutorialProgress + " : Nesingwary");
			break;
		case 5:
			Debug.Log("Tutorial Progress set to: " + tutorialProgress + " : Stormrage");
			break;
		case 6:
			Debug.Log("Tutorial Progress set to: " + tutorialProgress + " : Tutorial Complete");
			break;
		}
	}

	public void SetArcaneDust()
	{
		Debug.Log("Cheat: SetArcaneDust function called to add " + DustInput + " Arcane Dust to account");
	}

	public void SetGoldBalance()
	{
		Debug.Log("Cheat: SetGoldBalance function called to add " + GoldInput + " Gold to account");
	}

	public void OpenArena()
	{
		Debug.Log("Cheat: OpenArena function called");
	}

	public void SetTickets()
	{
		Debug.Log("Cheat: SetTickets function called to add " + TicketsInput + " Tickets to account");
	}

	public void BuyAllAdventures()
	{
		Debug.Log("Cheat: BuyAllAdventures function called");
	}

	public void DefeatAllAdventures()
	{
		Debug.Log("Cheat: DefeatAllAdventures function called");
	}

	public void MaxLevelAllHeroes()
	{
		Debug.Log("Cheat: MaxLevelAllHeroes function called");
	}

	public void CloneAccount()
	{
		Debug.Log("Cheat: CloneAccount function called");
	}

	public void ResetAccount()
	{
		Debug.Log("Cheat: ResetAccount function called");
	}

	public void GiveMeEverything()
	{
		Debug.Log("Cheat: GiveMeEverything function called");
	}

	public void ToggleHUD()
	{
		Debug.Log("Cheat: ToggleHUD function called");
		m_HUDcheckMark.SetActive(!m_HUDcheckMark.activeSelf);
		isHUDactive = !isHUDactive;
	}

	public void ToggleHideHistory()
	{
		Debug.Log("Cheat: ToggleHideHistory function called");
		m_HideHistorycheckMark.SetActive(!m_HideHistorycheckMark.activeSelf);
		isHistoryactive = !isHistoryactive;
	}

	public void RenameInnkeeper(Text name)
	{
		Debug.Log("Cheat: RenameInnkeeper function called. Renaming to: " + name.GetComponent<Text>().text);
	}

	public void ResetClient()
	{
		Debug.Log("Cheat: ResetClient function called");
	}

	public void ExportCardsTool()
	{
		Debug.Log("Cheat: ExportCardsTool function called");
	}

	public void BoardOnValueChanged()
	{
		string text = m_SetboardInputField.GetComponent<InputField>().text;
		Debug.Log("Pinned keyword changed to: " + text);
	}

	public void BoardOnEndEdit()
	{
		string text = m_SetboardInputField.GetComponent<InputField>().text;
		Debug.Log("User pressed 'enter'. Keyword: " + text);
	}
}
