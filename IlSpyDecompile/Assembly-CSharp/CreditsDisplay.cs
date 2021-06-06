using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Hearthstone;
using UnityEngine;

public class CreditsDisplay : MonoBehaviour
{
	public GameObject m_creditsRoot;

	public UberText m_creditsText1;

	public UberText m_creditsText2;

	private UberText m_currentText;

	public Transform m_offscreenCardBone;

	public Transform m_cardBone;

	public UIBButton m_doneButton;

	public UIBButton m_yearButton1;

	public UIBButton m_yearButton2;

	public UIBButton m_fasterButton;

	public UIBButton m_slowerButton;

	public Transform m_flopPoint;

	public GameObject m_doneArrowInButton;

	private const string CREDITS_TEXT_CARD_CUTOFF_LINE = "<!-- no more cards displayed after this line -->";

	private float m_creditsScrollSpeed = 3.5f;

	private const float CREDITS_SCROLL_SPEED_DEFAULT = 3.5f;

	private const float CREDITS_SCROLL_SPEED_STEP = 0.75f;

	private const int CREDITS_SCROLL_STEP_LIMIT = 3;

	private int m_creditsScrollSpeedCurrentStep;

	private const int MAX_LINES_PER_CHUNK = 70;

	private static CreditsDisplay s_instance;

	private string[] m_creditLines;

	private int m_currentLine;

	private List<Actor> m_fakeCards;

	private List<DefLoader.DisposableFullDef> m_creditsDefs = new List<DefLoader.DisposableFullDef>();

	private bool started;

	private bool m_creditsTextLoaded;

	private bool m_creditsTextLoadSucceeded;

	private bool m_creditsDone;

	private Actor m_shownCreditsCard;

	private Vector3 creditsRootStartLocalPosition;

	private Vector3 creditsText1StartLocalPosition;

	private Vector3 creditsText2StartLocalPosition;

	private int m_lastCard = 1;

	private List<string> m_cardsToLoad = new List<string>();

	private bool m_sortedCards;

	private Map<string, string> m_creditsCardsByName;

	private Coroutine END_CREDITS_COROUTINE;

	private Coroutine START_CREDITS_COROUTINE;

	private Coroutine SHOW_NEW_CARD_COROUTINE;

	private AssetReference[] s_credits_card_embers;

	private AssetReference[] s_credits_card_enter;

	private AssetReference[] s_tavern_crowd_play_reaction_positive;

	private int m_creditsYearIndex = -1;

	private CreditsYearDbfRecord[] m_creditsYearsAvailable = new CreditsYearDbfRecord[0];

	public static CreditsDisplay Get()
	{
		return s_instance;
	}

	private void Awake()
	{
		s_credits_card_embers = new AssetReference[3]
		{
			new AssetReference("credits_card_embers_1.prefab:4648803a81d87474796231e996fb0d13"),
			new AssetReference("credits_card_embers_2.prefab:4078df663ba798940b2421bcbc3158b4"),
			new AssetReference("credits_card_embers_3.prefab:bd7299f68ec58234e907a520a605c2ec")
		};
		s_credits_card_enter = new AssetReference[3]
		{
			new AssetReference("credits_card_enter_1.prefab:d7f2bfe2038cc5b4db0d62d0583b00d5"),
			new AssetReference("credits_card_enter_2.prefab:e13890ae6bc727c438226f1f6097b7ee"),
			new AssetReference("credits_card_enter_3.prefab:5f352f8760ce4a346b9e6800cc1e8aac")
		};
		s_tavern_crowd_play_reaction_positive = new AssetReference[5]
		{
			new AssetReference("tavern_crowd_play_reaction_positive_1.prefab:83877aea3ad648a48929d10bd1c2241b"),
			new AssetReference("tavern_crowd_play_reaction_positive_2.prefab:f034e34549f86b44683db038fc04cb68"),
			new AssetReference("tavern_crowd_play_reaction_positive_3.prefab:d62c8a96c4fb6f14990d0f1dc089e50a"),
			new AssetReference("tavern_crowd_play_reaction_positive_4.prefab:ed271df67f20c6847833c88cee921c53"),
			new AssetReference("tavern_crowd_play_reaction_positive_5.prefab:cb3d351beea04f54fafc21eb44618108")
		};
		s_instance = this;
		m_fakeCards = new List<Actor>();
		creditsRootStartLocalPosition = m_creditsRoot.transform.localPosition;
		creditsText1StartLocalPosition = m_creditsText1.transform.localPosition;
		creditsText2StartLocalPosition = m_creditsText2.transform.localPosition;
		m_doneButton.SetText(GameStrings.Get("GLOBAL_BACK"));
		m_doneButton.AddEventListener(UIEventType.RELEASE, OnDonePressed);
		m_yearButton1.AddEventListener(UIEventType.RELEASE, OnYearPressed1);
		m_yearButton2.AddEventListener(UIEventType.RELEASE, OnYearPressed2);
		UpdateYearButtons();
		m_fasterButton.AddEventListener(UIEventType.RELEASE, OnFasterButtonPressed);
		m_slowerButton.AddEventListener(UIEventType.RELEASE, OnSlowerButtonPressed);
		m_fasterButton.SetText("Faster");
		m_fasterButton.gameObject.SetActive(value: false);
		m_slowerButton.SetText("Slower");
		m_slowerButton.gameObject.SetActive(value: false);
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			Box.Get().m_tableTop.SetActive(value: false);
			Box.Get().m_letterboxingContainer.SetActive(value: false);
			m_doneButton.SetText("");
			m_doneArrowInButton.SetActive(value: true);
		}
		AssetLoader.Get().InstantiatePrefab("Card_Hand_Ally.prefab:d00eb0f79080e0749993fe4619e9143d", ActorLoadedCallback, null, AssetLoadingOptions.IgnorePrefabPosition);
		AssetLoader.Get().InstantiatePrefab("Card_Hand_Ally.prefab:d00eb0f79080e0749993fe4619e9143d", ActorLoadedCallback, null, AssetLoadingOptions.IgnorePrefabPosition);
		m_creditsYearsAvailable = (from r in GameDbf.CreditsYear.GetRecords()
			orderby r.ID
			select r).ToArray();
		m_creditsYearIndex = m_creditsYearsAvailable.Length - 1;
		UpdateYearButtons();
		PopulateCreditsCardsByName();
		LoadCreditsText();
	}

	private void OnDestoy()
	{
		s_instance = null;
		ReleaseAllCreditsCards();
	}

	private void StopAndClearCoroutine(ref Coroutine co)
	{
		if (co != null)
		{
			StopCoroutine(co);
			co = null;
		}
	}

	private void PopulateCreditsCardsByName()
	{
		IEnumerable<CardDbfRecord> source = from c in GameDbf.Card.GetRecords()
			where GameDbf.CardSetTiming.GetRecords((CardSetTimingDbfRecord t) => t.CardId == c.ID).Any((CardSetTimingDbfRecord t) => t.CardSetId == 16)
			select c;
		IEnumerable<KeyValuePair<string, string>> first = from c in source
			where c.Name != null
			select new KeyValuePair<string, string>(c.Name.GetString().Trim(), c.NoteMiniGuid);
		IEnumerable<KeyValuePair<string, string>> second = from c in source
			where !string.IsNullOrEmpty(c.CreditsCardName)
			select new KeyValuePair<string, string>(c.CreditsCardName.Trim(), c.NoteMiniGuid);
		m_creditsCardsByName = new Map<string, string>(first.Union(second));
	}

	private void LoadAllCreditsCards()
	{
		ReleaseAllCreditsCards();
		foreach (string item in m_cardsToLoad)
		{
			DefLoader.Get().LoadFullDef(item, OnFullDefLoaded);
		}
	}

	private void ReleaseAllCreditsCards()
	{
		m_creditsDefs.DisposeValuesAndClear();
	}

	private void LoadCreditsText()
	{
		m_creditsTextLoadSucceeded = false;
		m_cardsToLoad.Clear();
		string text = null;
		if (m_creditsYearIndex >= 0 && m_creditsYearIndex < m_creditsYearsAvailable.Length)
		{
			text = m_creditsYearsAvailable[m_creditsYearIndex].ContentsFilename;
		}
		string filePath = GetFilePath(text);
		if (filePath == null)
		{
			Error.AddDevWarning("Credits Error", "CreditsDisplay.LoadCreditsText() - Failed to find file for CREDITS: {0}", text);
			m_creditsTextLoaded = true;
			return;
		}
		try
		{
			m_creditLines = File.ReadAllLines(filePath);
			m_creditsTextLoadSucceeded = true;
		}
		catch (Exception ex)
		{
			Error.AddDevWarning("Credits Error", "CreditsDisplay.LoadCreditsText() - Failed to read \"{0}\".\n\nException: {1}", filePath, ex.Message);
		}
		for (int i = 0; i < m_creditLines.Length; i++)
		{
			string text2 = m_creditLines[i].Trim();
			if (text2 == "<!-- no more cards displayed after this line -->")
			{
				m_creditLines[i] = string.Empty;
				break;
			}
			if (m_creditsCardsByName != null && m_creditsCardsByName.TryGetValue(text2, out var value) && !m_cardsToLoad.Contains(value))
			{
				m_cardsToLoad.Add(value);
			}
		}
		m_creditsTextLoaded = true;
		LoadAllCreditsCards();
	}

	private string GetFilePath(string creditsFilename)
	{
		if (creditsFilename == null)
		{
			return null;
		}
		Locale[] loadOrder = Localization.GetLoadOrder();
		for (int i = 0; i < loadOrder.Length; i++)
		{
			string assetPath = GameStrings.GetAssetPath(loadOrder[i], creditsFilename);
			if (assetPath != null && File.Exists(assetPath))
			{
				return assetPath;
			}
		}
		return null;
	}

	private void FlopCredits()
	{
		if (m_currentText == m_creditsText1)
		{
			m_currentText = m_creditsText2;
		}
		else
		{
			m_currentText = m_creditsText1;
		}
		m_currentText.Text = GetNextCreditsChunk();
		DropText();
	}

	private void DropText()
	{
		UberText uberText = m_creditsText1;
		if (m_currentText == m_creditsText1)
		{
			uberText = m_creditsText2;
		}
		float z = 1.8649f;
		TransformUtil.SetPoint(m_currentText.gameObject, Anchor.FRONT, uberText.gameObject, Anchor.BACK, new Vector3(0f, 0f, z));
	}

	private string GetNextCreditsChunk()
	{
		string text = "";
		int currentLine = m_currentLine;
		int num = 70;
		for (int i = 0; i < num; i++)
		{
			if (m_creditLines.Length < i + currentLine + 1)
			{
				m_creditsDone = true;
				StartEndCreditsTimer();
				return text;
			}
			string text2 = m_creditLines[i + currentLine];
			if (text2.Length > 38)
			{
				num -= Mathf.CeilToInt(text2.Length / 38);
				if (i > num && i > 60)
				{
					break;
				}
			}
			text += text2;
			text += Environment.NewLine;
			m_currentLine++;
		}
		return text;
	}

	private void ActorLoadedCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		m_fakeCards.Add(go.GetComponent<Actor>());
	}

	private void Start()
	{
		Navigation.Push(EndCredits);
		StartCoroutine(NotifySceneLoadedWhenReady());
	}

	private IEnumerator NotifySceneLoadedWhenReady()
	{
		while (m_fakeCards.Count < 2)
		{
			yield return null;
		}
		while (!m_creditsTextLoaded)
		{
			yield return null;
		}
		Box.Get().AddTransitionFinishedListener(OnBoxOpened);
		SceneMgr.Get().NotifySceneLoaded();
	}

	private void OnBoxOpened(object userData)
	{
		Box.Get().RemoveTransitionFinishedListener(OnBoxOpened);
		if (!m_creditsTextLoadSucceeded)
		{
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
			return;
		}
		MusicManager.Get().StartPlaylist(MusicPlaylistType.UI_Credits);
		START_CREDITS_COROUTINE = StartCoroutine(StartCredits());
	}

	private IEnumerator StartCredits()
	{
		m_creditsText2.Text = GetNextCreditsChunk();
		m_currentText = m_creditsText2;
		FlopCredits();
		started = true;
		m_creditsRoot.SetActive(value: true);
		yield return new WaitForSeconds(4f);
		SHOW_NEW_CARD_COROUTINE = StartCoroutine(ShowNewCard());
	}

	private IEnumerator ShowNewCard()
	{
		if (m_creditsDefs != null && m_creditsDefs.Count != 0)
		{
			float time = 1f;
			int num = 0;
			if (m_lastCard == 0)
			{
				num = 1;
			}
			m_lastCard = num;
			m_shownCreditsCard = m_fakeCards[num];
			int index = ((!m_sortedCards) ? UnityEngine.Random.Range(0, m_creditsDefs.Count) : 0);
			m_shownCreditsCard.SetCardDef(m_creditsDefs[index].DisposableCardDef);
			EntityDef entityDef = m_creditsDefs[index].EntityDef;
			bool num2 = entityDef.GetCardId() == "CRED_10";
			if (num2)
			{
				entityDef.SetTag(GAME_TAG.CARDRACE, TAG_RACE.PIRATE);
			}
			m_shownCreditsCard.SetEntityDef(entityDef);
			m_creditsDefs.DisposeAndRemoveAt(index);
			m_shownCreditsCard.UpdateAllComponents();
			m_shownCreditsCard.Show();
			if (num2)
			{
				m_shownCreditsCard.GetRaceText().Text = GameStrings.Get("GLUE_NINJA");
			}
			m_shownCreditsCard.transform.position = m_offscreenCardBone.position;
			m_shownCreditsCard.transform.localScale = m_offscreenCardBone.localScale;
			m_shownCreditsCard.transform.localEulerAngles = m_offscreenCardBone.localEulerAngles;
			SoundManager.Get().LoadAndPlay(s_credits_card_enter[UnityEngine.Random.Range(0, 2)]);
			iTween.MoveTo(m_shownCreditsCard.gameObject, m_cardBone.position, time);
			iTween.RotateTo(m_shownCreditsCard.gameObject, m_cardBone.localEulerAngles, time);
			Actor oldActor = m_shownCreditsCard;
			yield return new WaitForSeconds(0.5f);
			SoundManager.Get().LoadAndPlay(s_tavern_crowd_play_reaction_positive[UnityEngine.Random.Range(0, 4)]);
			yield return new WaitForSeconds(7.5f);
			m_shownCreditsCard.ActivateSpellBirthState(SpellType.BURN);
			SoundManager.Get().LoadAndPlay(s_credits_card_embers[UnityEngine.Random.Range(0, 2)]);
			if (m_shownCreditsCard == oldActor)
			{
				m_shownCreditsCard = null;
			}
			yield return new WaitForSeconds(11f);
			SHOW_NEW_CARD_COROUTINE = StartCoroutine(ShowNewCard());
		}
	}

	private void Update()
	{
		Network.Get().ProcessNetwork();
		if (!started)
		{
			return;
		}
		m_creditsRoot.transform.localPosition += new Vector3(0f, 0f, m_creditsScrollSpeed * Time.deltaTime);
		if (!m_creditsDone && !(m_currentText == null))
		{
			if (GetTopOfCurrentCredits() > m_flopPoint.position.z)
			{
				FlopCredits();
			}
			ReadKeyboardInput();
		}
	}

	private float GetTopOfCurrentCredits()
	{
		Bounds textWorldSpaceBounds = m_currentText.GetTextWorldSpaceBounds();
		return textWorldSpaceBounds.center.z + textWorldSpaceBounds.extents.z;
	}

	private void OnFullDefLoaded(string cardID, DefLoader.DisposableFullDef def, object userData)
	{
		m_creditsDefs.Add(def);
	}

	private void OnDonePressed(UIEvent e)
	{
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			Box.Get().m_letterboxingContainer.SetActive(value: true);
		}
		Navigation.GoBack();
	}

	private void OnYearPressed1(UIEvent e)
	{
		if (m_creditsYearIndex <= 0)
		{
			m_creditsYearIndex++;
		}
		else if (m_creditsYearIndex >= m_creditsYearsAvailable.Length - 1)
		{
			m_creditsYearIndex = m_creditsYearsAvailable.Length - 3;
		}
		else
		{
			m_creditsYearIndex--;
		}
		if (m_creditsYearIndex < 0 || m_creditsYearIndex >= m_creditsYearsAvailable.Length)
		{
			m_creditsYearIndex = -1;
		}
		OnYearPressed();
	}

	private void OnYearPressed2(UIEvent e)
	{
		if (m_creditsYearIndex <= 0)
		{
			m_creditsYearIndex += 2;
		}
		else if (m_creditsYearIndex >= m_creditsYearsAvailable.Length - 1)
		{
			m_creditsYearIndex = m_creditsYearsAvailable.Length - 2;
		}
		else
		{
			m_creditsYearIndex++;
		}
		if (m_creditsYearIndex < 0 || m_creditsYearIndex >= m_creditsYearsAvailable.Length)
		{
			m_creditsYearIndex = -1;
		}
		OnYearPressed();
	}

	private void OnYearPressed()
	{
		StopAndClearCoroutine(ref START_CREDITS_COROUTINE);
		StopAndClearCoroutine(ref SHOW_NEW_CARD_COROUTINE);
		if (m_shownCreditsCard != null)
		{
			m_shownCreditsCard.ActivateSpellBirthState(SpellType.BURN);
			SoundManager.Get().LoadAndPlay(s_credits_card_enter[UnityEngine.Random.Range(0, 2)]);
			m_shownCreditsCard = null;
		}
		StartCoroutine(ResetCredits());
	}

	private void OnFasterButtonPressed(UIEvent e)
	{
		m_creditsScrollSpeedCurrentStep++;
		if (m_creditsScrollSpeedCurrentStep == 3)
		{
			m_fasterButton.gameObject.SetActive(value: false);
		}
		m_slowerButton.gameObject.SetActive(value: true);
		m_creditsScrollSpeed = 3.5f + (float)m_creditsScrollSpeedCurrentStep * 0.75f;
	}

	private void OnSlowerButtonPressed(UIEvent e)
	{
		m_creditsScrollSpeedCurrentStep--;
		if (m_creditsScrollSpeedCurrentStep == -3)
		{
			m_slowerButton.gameObject.SetActive(value: false);
		}
		m_fasterButton.gameObject.SetActive(value: true);
		m_creditsScrollSpeed = 3.5f + (float)m_creditsScrollSpeedCurrentStep * 0.75f;
	}

	private void UpdateYearButtons()
	{
		int num = -1;
		int num2 = -1;
		if (m_creditsYearIndex <= 0)
		{
			num = 1;
			num2 = 2;
		}
		else if (m_creditsYearIndex >= m_creditsYearsAvailable.Length - 1)
		{
			num = m_creditsYearsAvailable.Length - 3;
			num2 = m_creditsYearsAvailable.Length - 2;
		}
		else
		{
			num = m_creditsYearIndex - 1;
			num2 = m_creditsYearIndex + 1;
		}
		if (num >= 0 && num < m_creditsYearsAvailable.Length)
		{
			m_yearButton1.gameObject.SetActive(value: true);
			string text = m_creditsYearsAvailable[num].ButtonLabel;
			if (string.IsNullOrEmpty(text))
			{
				text = m_creditsYearsAvailable[num].ID.ToString();
			}
			m_yearButton1.SetText(text);
		}
		else
		{
			m_yearButton1.gameObject.SetActive(value: false);
		}
		if (num2 >= 0 && num2 < m_creditsYearsAvailable.Length)
		{
			m_yearButton2.gameObject.SetActive(value: true);
			string text2 = m_creditsYearsAvailable[num2].ButtonLabel;
			if (string.IsNullOrEmpty(text2))
			{
				text2 = m_creditsYearsAvailable[num2].ID.ToString();
			}
			m_yearButton2.SetText(text2);
		}
		else
		{
			m_yearButton2.gameObject.SetActive(value: false);
		}
	}

	private IEnumerator ResetCredits()
	{
		m_currentText = null;
		m_creditsText1.Text = "";
		m_creditsText2.Text = "";
		started = false;
		m_creditsTextLoaded = false;
		m_creditsTextLoadSucceeded = false;
		m_creditsDone = false;
		m_currentLine = 0;
		m_creditLines = null;
		UpdateYearButtons();
		m_creditsText1.transform.localPosition = creditsText1StartLocalPosition;
		m_creditsText2.transform.localPosition = creditsText2StartLocalPosition;
		m_creditsRoot.transform.localPosition = creditsRootStartLocalPosition;
		m_lastCard = 1;
		ReleaseAllCreditsCards();
		LoadCreditsText();
		while (!m_creditsTextLoaded)
		{
			yield return null;
		}
		StopAndClearCoroutine(ref END_CREDITS_COROUTINE);
		StopAndClearCoroutine(ref START_CREDITS_COROUTINE);
		START_CREDITS_COROUTINE = StartCoroutine(StartCredits());
	}

	private bool EndCredits()
	{
		iTween.FadeTo(m_creditsText1.gameObject, 0f, 0.1f);
		iTween.FadeTo(m_creditsText2.gameObject, 0f, 0.1f);
		SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
		return true;
	}

	private void StartEndCreditsTimer()
	{
		END_CREDITS_COROUTINE = StartCoroutine(EndCreditsTimer());
	}

	private IEnumerator EndCreditsTimer()
	{
		yield return new WaitForSeconds(300f);
		if (!SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB) && SceneMgr.Get().GetMode() == SceneMgr.Mode.CREDITS)
		{
			Navigation.GoBack();
		}
	}

	private void ReadKeyboardInput()
	{
		if (HearthstoneApplication.IsPublic())
		{
			return;
		}
		int num = Input.touches.Count((Touch t) => t.phase == TouchPhase.Began);
		if (InputCollection.GetKeyDown(KeyCode.Plus) || InputCollection.GetKeyDown(KeyCode.KeypadPlus))
		{
			OnFasterButtonPressed(null);
			m_slowerButton.gameObject.SetActive(value: false);
			m_fasterButton.gameObject.SetActive(value: false);
		}
		else if (InputCollection.GetKeyDown(KeyCode.Minus) || InputCollection.GetKeyDown(KeyCode.KeypadMinus))
		{
			OnSlowerButtonPressed(null);
			m_slowerButton.gameObject.SetActive(value: false);
			m_fasterButton.gameObject.SetActive(value: false);
		}
		else if (InputCollection.GetKeyDown(KeyCode.Space))
		{
			LoadAllCreditsCards();
			UIStatus.Get().AddInfo($"Reset cards list: {m_creditsDefs.Count} to display");
		}
		else if (InputCollection.GetKeyDown(KeyCode.N) || num == 2)
		{
			if (m_creditsDefs == null || m_creditsDefs.Count == 0)
			{
				LoadAllCreditsCards();
				UIStatus.Get().AddInfo($"Reset cards list: {m_creditsDefs.Count} to display");
				return;
			}
			StopAndClearCoroutine(ref SHOW_NEW_CARD_COROUTINE);
			if (m_shownCreditsCard != null)
			{
				m_shownCreditsCard.ActivateSpellBirthState(SpellType.BURN);
				m_shownCreditsCard = null;
			}
			SHOW_NEW_CARD_COROUTINE = StartCoroutine(ShowNewCard());
		}
		else if (InputCollection.GetKeyDown(KeyCode.S) || num == 5)
		{
			if (m_creditsDefs == null)
			{
				return;
			}
			m_sortedCards = !m_sortedCards;
			if (m_sortedCards)
			{
				m_creditsDefs.Sort((DefLoader.DisposableFullDef a, DefLoader.DisposableFullDef b) => m_cardsToLoad.IndexOf(a.EntityDef.GetCardId()).CompareTo(m_cardsToLoad.IndexOf(b.EntityDef.GetCardId())));
			}
			UIStatus.Get().AddInfo(string.Format("{0} remaining cards to display {1}.", m_creditsDefs.Count, m_sortedCards ? "sorted" : "randomized"));
		}
		else if (InputCollection.GetKeyDown(KeyCode.D))
		{
			string[] array = (from d in m_creditsDefs
				let e = d.EntityDef
				let c = (e == null) ? null : GameDbf.GetIndex().GetCardRecord(e.GetCardId())
				select (e != null) ? $"{GameUtils.TranslateCardIdToDbId(e.GetCardId())}-{e.GetCardId()} {e.GetName()}{((c == null || string.IsNullOrEmpty(c.CreditsCardName)) ? string.Empty : $" ({c.CreditsCardName})")}" : null into n
				where !string.IsNullOrEmpty(n)
				select n).ToArray();
			Log.All.Print("Credits Cards to show:\n{0}", string.Join("\n", array));
			UIStatus.Get().AddInfo($"Dumped to log: {array.Length} remaining cards to display.");
		}
	}
}
