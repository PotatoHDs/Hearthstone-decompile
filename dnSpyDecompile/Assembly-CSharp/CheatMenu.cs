using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B41 RID: 2881
[CustomEditClass]
public class CheatMenu : MonoBehaviour
{
	// Token: 0x0600989D RID: 39069 RVA: 0x00316624 File Offset: 0x00314824
	private void Start()
	{
		if (this.ActiveTabGroupIndex > 0)
		{
			this.LeftArrow.SetActive(true);
		}
		else
		{
			this.LeftArrow.SetActive(false);
		}
		if (this.ActiveTabGroupIndex < this.groups.Count - 1)
		{
			this.RightArrow.SetActive(true);
		}
		else
		{
			this.RightArrow.SetActive(false);
		}
		this.ActiveTabContents = this.contents[this.ActiveTabIndex];
		for (int i = 0; i < this.contents.Count; i++)
		{
			if (i == this.ActiveTabIndex)
			{
				ColorBlock colors = this.tabs[this.ActiveTabIndex].GetComponentInChildren<Button>().colors;
				colors.normalColor = Color.white;
				this.tabs[this.ActiveTabIndex].GetComponentInChildren<Button>().colors = colors;
				this.contents[this.ActiveTabIndex].SetActive(true);
			}
			else
			{
				ColorBlock colors2 = this.tabs[this.ActiveTabIndex].GetComponentInChildren<Button>().colors;
				colors2.normalColor = Color.clear;
				this.tabs[this.ActiveTabIndex].GetComponentInChildren<Button>().colors = colors2;
				this.contents[i].SetActive(false);
			}
		}
		this.ActiveTabContents.SetActive(true);
	}

	// Token: 0x0600989E RID: 39070 RVA: 0x00316780 File Offset: 0x00314980
	private void OnEnable()
	{
		Debug.Log("Enabled");
		this.m_allCardRecords = new Dictionary<string, CardDbfRecord>();
		foreach (string text in GameUtils.GetAllCardIds())
		{
			this.m_allCardRecords[text] = GameUtils.GetCardRecord(text);
		}
	}

	// Token: 0x0600989F RID: 39071 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void OnDisable()
	{
	}

	// Token: 0x060098A0 RID: 39072 RVA: 0x003167F4 File Offset: 0x003149F4
	public void SetAsActiveTab(int tabIndex)
	{
		Debug.Log("Tab Index " + tabIndex);
		ColorBlock colors = this.tabs[this.ActiveTabIndex].GetComponentInChildren<Button>().colors;
		colors.normalColor = Color.clear;
		this.tabs[this.ActiveTabIndex].GetComponentInChildren<Button>().colors = colors;
		if (this.ActiveTabContents != null)
		{
			this.ActiveTabContents.SetActive(false);
		}
		this.ActiveTabIndex = tabIndex;
		this.ActiveTabContents = this.contents[this.ActiveTabIndex];
		this.ActiveTabContents.SetActive(true);
		colors = this.tabs[this.ActiveTabIndex].GetComponentInChildren<Button>().colors;
		colors.normalColor = Color.white;
		this.tabs[this.ActiveTabIndex].GetComponentInChildren<Button>().colors = colors;
	}

	// Token: 0x060098A1 RID: 39073 RVA: 0x003168E4 File Offset: 0x00314AE4
	public void ShiftGroup(int indexChange)
	{
		this.groups[this.ActiveTabGroupIndex].SetActive(false);
		this.ActiveTabGroupIndex += indexChange;
		this.groups[this.ActiveTabGroupIndex].SetActive(true);
		if (this.ActiveTabGroupIndex > 0)
		{
			this.LeftArrow.SetActive(true);
		}
		else
		{
			this.LeftArrow.SetActive(false);
		}
		if (this.ActiveTabGroupIndex < this.groups.Count - 1)
		{
			this.RightArrow.SetActive(true);
			return;
		}
		this.RightArrow.SetActive(false);
	}

	// Token: 0x060098A2 RID: 39074 RVA: 0x00316980 File Offset: 0x00314B80
	public void MaxMana()
	{
		if (!Network.IsRunning())
		{
			return;
		}
		string command = "maxmana friendly";
		Network.Get().SendDebugConsoleCommand(command);
	}

	// Token: 0x060098A3 RID: 39075 RVA: 0x003169A8 File Offset: 0x00314BA8
	public void FullHealth()
	{
		if (!Network.IsRunning())
		{
			return;
		}
		string command = "healhero friendly";
		Network.Get().SendDebugConsoleCommand(command);
	}

	// Token: 0x060098A4 RID: 39076 RVA: 0x003169D0 File Offset: 0x00314BD0
	public void SetHealthToOne()
	{
		if (!Network.IsRunning())
		{
			return;
		}
		string command = "spawncard XXX_107 friendly hand 0";
		Network.Get().SendDebugConsoleCommand(command);
	}

	// Token: 0x060098A5 RID: 39077 RVA: 0x003169F7 File Offset: 0x00314BF7
	public void SetImmune()
	{
		Debug.Log("Cheat: SetImmune function called");
	}

	// Token: 0x060098A6 RID: 39078 RVA: 0x00316A04 File Offset: 0x00314C04
	public void ClearMinions()
	{
		if (!Network.IsRunning())
		{
			return;
		}
		string command = "spawncard XXX_018 friendly hand 0";
		Network.Get().SendDebugConsoleCommand(command);
	}

	// Token: 0x060098A7 RID: 39079 RVA: 0x00316A2C File Offset: 0x00314C2C
	public void Discard()
	{
		if (!Network.IsRunning())
		{
			return;
		}
		string command = "cyclehand friendly";
		Network.Get().SendDebugConsoleCommand(command);
	}

	// Token: 0x060098A8 RID: 39080 RVA: 0x00316A54 File Offset: 0x00314C54
	public void DrawCard()
	{
		if (!Network.IsRunning())
		{
			return;
		}
		string command = "drawcard friendly";
		Network.Get().SendDebugConsoleCommand(command);
	}

	// Token: 0x060098A9 RID: 39081 RVA: 0x00316A7B File Offset: 0x00314C7B
	public void Destroy()
	{
		Debug.Log("Cheat: Destroy function called");
	}

	// Token: 0x060098AA RID: 39082 RVA: 0x00316A88 File Offset: 0x00314C88
	public void SearchOnValueChanged()
	{
		string text = this.SearchInputField.GetComponent<InputField>().text;
		Debug.Log("Search keyword changed to: " + text);
	}

	// Token: 0x060098AB RID: 39083 RVA: 0x00316AB8 File Offset: 0x00314CB8
	public void SearchOnEndEdit()
	{
		if (this.m_allCardRecords.Count == 0)
		{
			DefLoader.Get().Clear();
			Localization.SetLocale(Locale.enUS);
			GameDbf.Load();
			GameStrings.ReloadAll();
			foreach (string text in GameUtils.GetAllCardIds())
			{
				this.m_allCardRecords[text] = GameUtils.GetCardRecord(text);
			}
			DefLoader.Get().LoadAllEntityDefs();
		}
		string text2 = this.SearchInputField.GetComponent<InputField>().text.ToLower();
		Debug.Log("User pressed 'enter'. Keyword: " + text2);
		Transform transform = this.SearchTabContents.transform.Find("Search Results List").transform.Find("Search Result Items").transform;
		for (int i = 0; i < transform.childCount; i++)
		{
			UnityEngine.Object.Destroy(transform.GetChild(i).gameObject);
		}
		if (string.IsNullOrEmpty(text2))
		{
			return;
		}
		if (this.m_allCardRecords.Count > 0)
		{
			Vector3 localPosition = new Vector3(0f, 0f, -73f);
			Vector3 one = Vector3.one;
			foreach (KeyValuePair<string, CardDbfRecord> keyValuePair in this.m_allCardRecords)
			{
				if ((keyValuePair.Key + keyValuePair.Value.Name.GetString(Locale.enUS, true).ToLower()).Contains(text2))
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_SearchResultItem);
					SearchResultItem result = gameObject.GetComponent<SearchResultItem>();
					result.m_text = keyValuePair.Value.Name.GetString(Locale.enUS, true);
					result.m_card = keyValuePair.Key;
					gameObject.name = "Item";
					gameObject.transform.SetParent(transform);
					gameObject.transform.localPosition = localPosition;
					gameObject.transform.localRotation = Quaternion.identity;
					gameObject.transform.localScale = one;
					gameObject.GetComponent<Button>().onClick.AddListener(delegate()
					{
						this.CardSelectedHandler(result);
					});
				}
			}
		}
	}

	// Token: 0x060098AC RID: 39084 RVA: 0x00316D40 File Offset: 0x00314F40
	public void CardSelectedHandler(SearchResultItem item)
	{
		Debug.Log(item.m_text);
		this.m_selectedCard = item.m_card;
		this.PreviewCard();
	}

	// Token: 0x060098AD RID: 39085 RVA: 0x00316D5F File Offset: 0x00314F5F
	private void PreviewCard()
	{
		if (this.m_cardPreview != null)
		{
			UnityEngine.Object.Destroy(this.m_cardPreview);
		}
		this.m_cardPreview = this.LoadCard(this.m_selectedCard, this.m_premiumType);
	}

	// Token: 0x060098AE RID: 39086 RVA: 0x00316D94 File Offset: 0x00314F94
	private GameObject LoadCard(string cardID, TAG_PREMIUM premium)
	{
		GameObject result;
		using (DefLoader.DisposableFullDef fullDef = DefLoader.Get().GetFullDef(cardID, new CardPortraitQuality(3, premium)))
		{
			string handActor = ActorNames.GetHandActor(fullDef.EntityDef, premium);
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab(handActor, AssetLoadingOptions.IgnorePrefabPosition);
			Actor component = gameObject.GetComponent<Actor>();
			if (component == null)
			{
				Debug.LogWarning(string.Format("Error getting Actor for: {0}", cardID));
				result = null;
			}
			else
			{
				this.m_PreviewCard.SetActive(false);
				component.SetPremium(premium);
				component.SetEntityDef(fullDef.EntityDef);
				component.SetCardDef(fullDef.DisposableCardDef);
				component.UpdateAllComponents();
				component.SetUnlit();
				gameObject.transform.SetParent(this.contents[1].transform, false);
				gameObject.transform.localPosition = this.m_PreviewCard.transform.localPosition;
				gameObject.transform.localRotation = Quaternion.identity;
				gameObject.transform.localScale = this.m_PreviewCard.transform.localScale;
				Transform[] componentsInChildren = gameObject.GetComponentsInChildren<Transform>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].gameObject.layer = LayerMask.NameToLayer("UI");
				}
				gameObject.layer = LayerMask.NameToLayer("UI");
				result = gameObject;
			}
		}
		return result;
	}

	// Token: 0x060098AF RID: 39087 RVA: 0x00316F08 File Offset: 0x00315108
	public void PinnedOnValueChanged()
	{
		string text = this.PinnedInputField.GetComponent<InputField>().text;
		Debug.Log("Pinned keyword changed to: " + text);
	}

	// Token: 0x060098B0 RID: 39088 RVA: 0x00316F38 File Offset: 0x00315138
	public void PinnedOnEndEdit()
	{
		string text = this.PinnedInputField.GetComponent<InputField>().text;
		Debug.Log("User pressed 'enter'. Keyword: " + text);
	}

	// Token: 0x060098B1 RID: 39089 RVA: 0x00316F68 File Offset: 0x00315168
	public void ShowSearchTab()
	{
		Debug.Log("Showing Search");
		this.SearchTabContents.SetActive(true);
		this.PinnedTabContents.SetActive(false);
		Vector3 localPosition = this.SearchTab.GetComponent<RectTransform>().localPosition;
		this.SearchTab.GetComponent<RectTransform>().localPosition = new Vector3(localPosition.x, localPosition.y, 0.109f);
		Vector3 localPosition2 = this.PinnedTab.GetComponent<RectTransform>().localPosition;
		this.PinnedTab.GetComponent<RectTransform>().localPosition = new Vector3(localPosition2.x, localPosition2.y, 0.095f);
	}

	// Token: 0x060098B2 RID: 39090 RVA: 0x00317008 File Offset: 0x00315208
	public void ShowPinnedTab()
	{
		Debug.Log("Showing Pinned Items");
		this.SearchTabContents.SetActive(false);
		this.PinnedTabContents.SetActive(true);
		Vector3 localPosition = this.SearchTab.GetComponent<RectTransform>().localPosition;
		this.SearchTab.GetComponent<RectTransform>().localPosition = new Vector3(localPosition.x, localPosition.y, 0.095f);
		Vector3 localPosition2 = this.PinnedTab.GetComponent<RectTransform>().localPosition;
		this.PinnedTab.GetComponent<RectTransform>().localPosition = new Vector3(localPosition2.x, localPosition2.y, 0.109f);
	}

	// Token: 0x060098B3 RID: 39091 RVA: 0x003170A8 File Offset: 0x003152A8
	public void PreviewCard(GameObject textObj)
	{
		string text = textObj.GetComponent<Text>().text;
		Debug.Log("Search Result Click. Previewing: " + text);
	}

	// Token: 0x060098B4 RID: 39092 RVA: 0x003170D4 File Offset: 0x003152D4
	public void ToggleGolden()
	{
		Debug.Log("Cheat: ToggleGolden function called");
		this.m_GoldenCheckMark.SetActive(!this.m_GoldenCheckMark.activeSelf);
		if (this.m_premiumType == TAG_PREMIUM.GOLDEN || this.m_premiumType == TAG_PREMIUM.DIAMOND)
		{
			this.m_premiumType = TAG_PREMIUM.NORMAL;
		}
		else
		{
			this.m_premiumType = TAG_PREMIUM.GOLDEN;
		}
		this.PreviewCard();
	}

	// Token: 0x060098B5 RID: 39093 RVA: 0x0031712C File Offset: 0x0031532C
	public void ExportCard()
	{
		Debug.Log("Export Card function called");
	}

	// Token: 0x060098B6 RID: 39094 RVA: 0x00317138 File Offset: 0x00315338
	public void AddCardTo(string location)
	{
		if (!(location == "opponentHand"))
		{
			if (!(location == "opponentField"))
			{
				if (!(location == "opponentDeck"))
				{
					if (!(location == "yourField"))
					{
						if (!(location == "yourHand"))
						{
							if (!(location == "yourDeck"))
							{
								return;
							}
							Debug.Log("AddCardTo function called. Adding card to Your Deck");
							if (!Network.IsRunning())
							{
								return;
							}
							string command = string.Format("spawncard {0} friendly deck 0", this.m_selectedCard);
							Network.Get().SendDebugConsoleCommand(command);
							return;
						}
						else
						{
							Debug.Log("AddCardTo function called. Adding card to Your Hand");
							if (!Network.IsRunning())
							{
								return;
							}
							string command2 = string.Format("spawncard {0} friendly hand 0", this.m_selectedCard);
							Network.Get().SendDebugConsoleCommand(command2);
							return;
						}
					}
					else
					{
						Debug.Log("AddCardTo function called. Adding card to Your Field");
						if (!Network.IsRunning())
						{
							return;
						}
						string command3 = string.Format("spawncard {0} friendly play 0", this.m_selectedCard);
						Network.Get().SendDebugConsoleCommand(command3);
						return;
					}
				}
				else
				{
					Debug.Log("AddCardTo function called. Adding card to Opponent's Deck");
					if (!Network.IsRunning())
					{
						return;
					}
					string command4 = string.Format("spawncard {0} opponent deck 0", this.m_selectedCard);
					Network.Get().SendDebugConsoleCommand(command4);
					return;
				}
			}
			else
			{
				Debug.Log("AddCardTo function called. Adding card to Opponent's Field");
				if (!Network.IsRunning())
				{
					return;
				}
				string command5 = string.Format("spawncard {0} opponent play 0", this.m_selectedCard);
				Network.Get().SendDebugConsoleCommand(command5);
				return;
			}
		}
		else
		{
			Debug.Log("AddCardTo function called. Adding card to Opponent's Hand");
			if (!Network.IsRunning())
			{
				return;
			}
			string command6 = string.Format("spawncard {0} opponent hand 0", this.m_selectedCard);
			Network.Get().SendDebugConsoleCommand(command6);
			return;
		}
	}

	// Token: 0x060098B7 RID: 39095 RVA: 0x003172C3 File Offset: 0x003154C3
	public void RunConsole()
	{
		Debug.Log("Cheat: RunConsole function called");
	}

	// Token: 0x060098B8 RID: 39096 RVA: 0x003172CF File Offset: 0x003154CF
	public void ClearConsole()
	{
		this.m_scriptContent.text = "";
	}

	// Token: 0x060098B9 RID: 39097 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void CopyCheatLine()
	{
	}

	// Token: 0x060098BA RID: 39098 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void PasteCheatLine()
	{
	}

	// Token: 0x060098BB RID: 39099 RVA: 0x003172E1 File Offset: 0x003154E1
	public void DustValueInput(InputField input)
	{
		this.DustInput = int.Parse(input.text);
		Debug.Log("Arcane Dust input field changed to: " + this.DustInput);
	}

	// Token: 0x060098BC RID: 39100 RVA: 0x0031730E File Offset: 0x0031550E
	public void GoldValueInput(InputField input)
	{
		this.GoldInput = int.Parse(input.text);
		Debug.Log("Gold input field changed to: " + this.GoldInput);
	}

	// Token: 0x060098BD RID: 39101 RVA: 0x0031733B File Offset: 0x0031553B
	public void TicketValueInput(InputField input)
	{
		this.TicketsInput = int.Parse(input.text);
		Debug.Log("Tickets input field changed to: " + this.TicketsInput);
	}

	// Token: 0x060098BE RID: 39102 RVA: 0x00317368 File Offset: 0x00315568
	public void TutorialDropdownValueChanged(int value)
	{
		this.tutorialProgress = value;
		Debug.Log("Tut: " + this.tutorialProgress);
	}

	// Token: 0x060098BF RID: 39103 RVA: 0x0031738C File Offset: 0x0031558C
	public void SetTutorialProgress()
	{
		switch (this.tutorialProgress)
		{
		case 0:
			Debug.Log("Tutorial Progress set to: " + this.tutorialProgress + " : Hogger");
			return;
		case 1:
			Debug.Log("Tutorial Progress set to: " + this.tutorialProgress + " : Manastorm");
			return;
		case 2:
			Debug.Log("Tutorial Progress set to: " + this.tutorialProgress + " : Lorewalker");
			return;
		case 3:
			Debug.Log("Tutorial Progress set to: " + this.tutorialProgress + " : King Mukla");
			return;
		case 4:
			Debug.Log("Tutorial Progress set to: " + this.tutorialProgress + " : Nesingwary");
			return;
		case 5:
			Debug.Log("Tutorial Progress set to: " + this.tutorialProgress + " : Stormrage");
			return;
		case 6:
			Debug.Log("Tutorial Progress set to: " + this.tutorialProgress + " : Tutorial Complete");
			return;
		default:
			return;
		}
	}

	// Token: 0x060098C0 RID: 39104 RVA: 0x003174A2 File Offset: 0x003156A2
	public void SetArcaneDust()
	{
		Debug.Log("Cheat: SetArcaneDust function called to add " + this.DustInput + " Arcane Dust to account");
	}

	// Token: 0x060098C1 RID: 39105 RVA: 0x003174C3 File Offset: 0x003156C3
	public void SetGoldBalance()
	{
		Debug.Log("Cheat: SetGoldBalance function called to add " + this.GoldInput + " Gold to account");
	}

	// Token: 0x060098C2 RID: 39106 RVA: 0x003174E4 File Offset: 0x003156E4
	public void OpenArena()
	{
		Debug.Log("Cheat: OpenArena function called");
	}

	// Token: 0x060098C3 RID: 39107 RVA: 0x003174F0 File Offset: 0x003156F0
	public void SetTickets()
	{
		Debug.Log("Cheat: SetTickets function called to add " + this.TicketsInput + " Tickets to account");
	}

	// Token: 0x060098C4 RID: 39108 RVA: 0x00317511 File Offset: 0x00315711
	public void BuyAllAdventures()
	{
		Debug.Log("Cheat: BuyAllAdventures function called");
	}

	// Token: 0x060098C5 RID: 39109 RVA: 0x0031751D File Offset: 0x0031571D
	public void DefeatAllAdventures()
	{
		Debug.Log("Cheat: DefeatAllAdventures function called");
	}

	// Token: 0x060098C6 RID: 39110 RVA: 0x00317529 File Offset: 0x00315729
	public void MaxLevelAllHeroes()
	{
		Debug.Log("Cheat: MaxLevelAllHeroes function called");
	}

	// Token: 0x060098C7 RID: 39111 RVA: 0x00317535 File Offset: 0x00315735
	public void CloneAccount()
	{
		Debug.Log("Cheat: CloneAccount function called");
	}

	// Token: 0x060098C8 RID: 39112 RVA: 0x00317541 File Offset: 0x00315741
	public void ResetAccount()
	{
		Debug.Log("Cheat: ResetAccount function called");
	}

	// Token: 0x060098C9 RID: 39113 RVA: 0x0031754D File Offset: 0x0031574D
	public void GiveMeEverything()
	{
		Debug.Log("Cheat: GiveMeEverything function called");
	}

	// Token: 0x060098CA RID: 39114 RVA: 0x00317559 File Offset: 0x00315759
	public void ToggleHUD()
	{
		Debug.Log("Cheat: ToggleHUD function called");
		this.m_HUDcheckMark.SetActive(!this.m_HUDcheckMark.activeSelf);
		this.isHUDactive = !this.isHUDactive;
	}

	// Token: 0x060098CB RID: 39115 RVA: 0x0031758D File Offset: 0x0031578D
	public void ToggleHideHistory()
	{
		Debug.Log("Cheat: ToggleHideHistory function called");
		this.m_HideHistorycheckMark.SetActive(!this.m_HideHistorycheckMark.activeSelf);
		this.isHistoryactive = !this.isHistoryactive;
	}

	// Token: 0x060098CC RID: 39116 RVA: 0x003175C1 File Offset: 0x003157C1
	public void RenameInnkeeper(Text name)
	{
		Debug.Log("Cheat: RenameInnkeeper function called. Renaming to: " + name.GetComponent<Text>().text);
	}

	// Token: 0x060098CD RID: 39117 RVA: 0x003175DD File Offset: 0x003157DD
	public void ResetClient()
	{
		Debug.Log("Cheat: ResetClient function called");
	}

	// Token: 0x060098CE RID: 39118 RVA: 0x003175E9 File Offset: 0x003157E9
	public void ExportCardsTool()
	{
		Debug.Log("Cheat: ExportCardsTool function called");
	}

	// Token: 0x060098CF RID: 39119 RVA: 0x003175F8 File Offset: 0x003157F8
	public void BoardOnValueChanged()
	{
		string text = this.m_SetboardInputField.GetComponent<InputField>().text;
		Debug.Log("Pinned keyword changed to: " + text);
	}

	// Token: 0x060098D0 RID: 39120 RVA: 0x00317628 File Offset: 0x00315828
	public void BoardOnEndEdit()
	{
		string text = this.m_SetboardInputField.GetComponent<InputField>().text;
		Debug.Log("User pressed 'enter'. Keyword: " + text);
	}

	// Token: 0x04007FA8 RID: 32680
	[CustomEditField(Sections = "TabGroups")]
	public List<GameObject> groups = new List<GameObject>();

	// Token: 0x04007FA9 RID: 32681
	private int ActiveTabGroupIndex;

	// Token: 0x04007FAA RID: 32682
	[CustomEditField(Sections = "Arrows")]
	public GameObject LeftArrow;

	// Token: 0x04007FAB RID: 32683
	[CustomEditField(Sections = "Arrows")]
	public GameObject RightArrow;

	// Token: 0x04007FAC RID: 32684
	[CustomEditField(Sections = "Tabs")]
	public List<GameObject> tabs = new List<GameObject>();

	// Token: 0x04007FAD RID: 32685
	[CustomEditField(Sections = "Tabs")]
	public List<GameObject> contents = new List<GameObject>();

	// Token: 0x04007FAE RID: 32686
	private int ActiveTabIndex;

	// Token: 0x04007FAF RID: 32687
	private GameObject ActiveTabContents;

	// Token: 0x04007FB0 RID: 32688
	[CustomEditField(Sections = "Tab_00_Contents")]
	public GameObject m_maxManaButton;

	// Token: 0x04007FB1 RID: 32689
	[CustomEditField(Sections = "Tab_00_Contents")]
	public GameObject m_fullHealthButton;

	// Token: 0x04007FB2 RID: 32690
	[CustomEditField(Sections = "Tab_00_Contents")]
	public GameObject m_SetHealthToOneButton;

	// Token: 0x04007FB3 RID: 32691
	[CustomEditField(Sections = "Tab_00_Contents")]
	public GameObject m_ImmuneCheckMark;

	// Token: 0x04007FB4 RID: 32692
	[CustomEditField(Sections = "Tab_00_Contents")]
	public GameObject m_ClearMinionsButton;

	// Token: 0x04007FB5 RID: 32693
	[CustomEditField(Sections = "Tab_00_Contents")]
	public GameObject m_ClearHandButton;

	// Token: 0x04007FB6 RID: 32694
	[CustomEditField(Sections = "Tab_00_Contents")]
	public GameObject m_destroyButton;

	// Token: 0x04007FB7 RID: 32695
	[CustomEditField(Sections = "Tab_01_Contents")]
	public GameObject SearchTab;

	// Token: 0x04007FB8 RID: 32696
	[CustomEditField(Sections = "Tab_01_Contents")]
	public GameObject PinnedTab;

	// Token: 0x04007FB9 RID: 32697
	[CustomEditField(Sections = "Tab_01_Contents")]
	public GameObject SearchTabContents;

	// Token: 0x04007FBA RID: 32698
	[CustomEditField(Sections = "Tab_01_Contents")]
	public GameObject PinnedTabContents;

	// Token: 0x04007FBB RID: 32699
	[CustomEditField(Sections = "Tab_01_Contents")]
	public GameObject SearchInputField;

	// Token: 0x04007FBC RID: 32700
	[CustomEditField(Sections = "Tab_01_Contents")]
	public GameObject PinnedInputField;

	// Token: 0x04007FBD RID: 32701
	[CustomEditField(Sections = "Tab_01_Contents")]
	public GameObject exportCardButton;

	// Token: 0x04007FBE RID: 32702
	[CustomEditField(Sections = "Tab_01_Contents")]
	public GameObject m_GoldenCheckMark;

	// Token: 0x04007FBF RID: 32703
	[CustomEditField(Sections = "Tab_01_Contents")]
	public GameObject m_PinItCheckMark;

	// Token: 0x04007FC0 RID: 32704
	[CustomEditField(Sections = "Tab_01_Contents")]
	public GameObject m_SearchResultItem;

	// Token: 0x04007FC1 RID: 32705
	[CustomEditField(Sections = "Tab_01_Contents")]
	public GameObject m_PreviewCard;

	// Token: 0x04007FC2 RID: 32706
	private TAG_PREMIUM m_premiumType;

	// Token: 0x04007FC3 RID: 32707
	[CustomEditField(Sections = "Tab_02_Contents")]
	public GameObject m_runConsoleButton;

	// Token: 0x04007FC4 RID: 32708
	[CustomEditField(Sections = "Tab_02_Contents")]
	public InputField m_scriptContent;

	// Token: 0x04007FC5 RID: 32709
	private int tutorialProgress;

	// Token: 0x04007FC6 RID: 32710
	private int DustInput;

	// Token: 0x04007FC7 RID: 32711
	private int GoldInput;

	// Token: 0x04007FC8 RID: 32712
	private int TicketsInput;

	// Token: 0x04007FC9 RID: 32713
	[CustomEditField(Sections = "Tab_04_General")]
	public GameObject m_HUDcheckMark;

	// Token: 0x04007FCA RID: 32714
	private bool isHUDactive = true;

	// Token: 0x04007FCB RID: 32715
	[CustomEditField(Sections = "Tab_04_General")]
	public GameObject m_HideHistorycheckMark;

	// Token: 0x04007FCC RID: 32716
	private bool isHistoryactive = true;

	// Token: 0x04007FCD RID: 32717
	[CustomEditField(Sections = "Tab_04_General")]
	public GameObject m_SetboardInputField;

	// Token: 0x04007FCE RID: 32718
	private Dictionary<string, CardDbfRecord> m_allCardRecords;

	// Token: 0x04007FCF RID: 32719
	private string m_selectedCard;

	// Token: 0x04007FD0 RID: 32720
	private GameObject m_cardPreview;
}
