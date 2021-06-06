using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Hearthstone;
using UnityEngine;

// Token: 0x02000140 RID: 320
public class CreditsDisplay : MonoBehaviour
{
	// Token: 0x06001505 RID: 5381 RVA: 0x0007823F File Offset: 0x0007643F
	public static CreditsDisplay Get()
	{
		return CreditsDisplay.s_instance;
	}

	// Token: 0x06001506 RID: 5382 RVA: 0x00078248 File Offset: 0x00076448
	private void Awake()
	{
		this.s_credits_card_embers = new AssetReference[]
		{
			new AssetReference("credits_card_embers_1.prefab:4648803a81d87474796231e996fb0d13"),
			new AssetReference("credits_card_embers_2.prefab:4078df663ba798940b2421bcbc3158b4"),
			new AssetReference("credits_card_embers_3.prefab:bd7299f68ec58234e907a520a605c2ec")
		};
		this.s_credits_card_enter = new AssetReference[]
		{
			new AssetReference("credits_card_enter_1.prefab:d7f2bfe2038cc5b4db0d62d0583b00d5"),
			new AssetReference("credits_card_enter_2.prefab:e13890ae6bc727c438226f1f6097b7ee"),
			new AssetReference("credits_card_enter_3.prefab:5f352f8760ce4a346b9e6800cc1e8aac")
		};
		this.s_tavern_crowd_play_reaction_positive = new AssetReference[]
		{
			new AssetReference("tavern_crowd_play_reaction_positive_1.prefab:83877aea3ad648a48929d10bd1c2241b"),
			new AssetReference("tavern_crowd_play_reaction_positive_2.prefab:f034e34549f86b44683db038fc04cb68"),
			new AssetReference("tavern_crowd_play_reaction_positive_3.prefab:d62c8a96c4fb6f14990d0f1dc089e50a"),
			new AssetReference("tavern_crowd_play_reaction_positive_4.prefab:ed271df67f20c6847833c88cee921c53"),
			new AssetReference("tavern_crowd_play_reaction_positive_5.prefab:cb3d351beea04f54fafc21eb44618108")
		};
		CreditsDisplay.s_instance = this;
		this.m_fakeCards = new List<Actor>();
		this.creditsRootStartLocalPosition = this.m_creditsRoot.transform.localPosition;
		this.creditsText1StartLocalPosition = this.m_creditsText1.transform.localPosition;
		this.creditsText2StartLocalPosition = this.m_creditsText2.transform.localPosition;
		this.m_doneButton.SetText(GameStrings.Get("GLOBAL_BACK"));
		this.m_doneButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnDonePressed));
		this.m_yearButton1.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnYearPressed1));
		this.m_yearButton2.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnYearPressed2));
		this.UpdateYearButtons();
		this.m_fasterButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnFasterButtonPressed));
		this.m_slowerButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnSlowerButtonPressed));
		this.m_fasterButton.SetText("Faster");
		this.m_fasterButton.gameObject.SetActive(false);
		this.m_slowerButton.SetText("Slower");
		this.m_slowerButton.gameObject.SetActive(false);
		if (UniversalInputManager.UsePhoneUI)
		{
			Box.Get().m_tableTop.SetActive(false);
			Box.Get().m_letterboxingContainer.SetActive(false);
			this.m_doneButton.SetText("");
			this.m_doneArrowInButton.SetActive(true);
		}
		AssetLoader.Get().InstantiatePrefab("Card_Hand_Ally.prefab:d00eb0f79080e0749993fe4619e9143d", new PrefabCallback<GameObject>(this.ActorLoadedCallback), null, AssetLoadingOptions.IgnorePrefabPosition);
		AssetLoader.Get().InstantiatePrefab("Card_Hand_Ally.prefab:d00eb0f79080e0749993fe4619e9143d", new PrefabCallback<GameObject>(this.ActorLoadedCallback), null, AssetLoadingOptions.IgnorePrefabPosition);
		this.m_creditsYearsAvailable = (from r in GameDbf.CreditsYear.GetRecords()
		orderby r.ID
		select r).ToArray<CreditsYearDbfRecord>();
		this.m_creditsYearIndex = this.m_creditsYearsAvailable.Length - 1;
		this.UpdateYearButtons();
		this.PopulateCreditsCardsByName();
		this.LoadCreditsText();
	}

	// Token: 0x06001507 RID: 5383 RVA: 0x0007851E File Offset: 0x0007671E
	private void OnDestoy()
	{
		CreditsDisplay.s_instance = null;
		this.ReleaseAllCreditsCards();
	}

	// Token: 0x06001508 RID: 5384 RVA: 0x0007852C File Offset: 0x0007672C
	private void StopAndClearCoroutine(ref Coroutine co)
	{
		if (co != null)
		{
			base.StopCoroutine(co);
			co = null;
		}
	}

	// Token: 0x06001509 RID: 5385 RVA: 0x00078540 File Offset: 0x00076740
	private void PopulateCreditsCardsByName()
	{
		IEnumerable<CardDbfRecord> source = from c in GameDbf.Card.GetRecords()
		where GameDbf.CardSetTiming.GetRecords((CardSetTimingDbfRecord t) => t.CardId == c.ID, -1).Any((CardSetTimingDbfRecord t) => t.CardSetId == 16)
		select c;
		IEnumerable<KeyValuePair<string, string>> first = from c in source
		where c.Name != null
		select new KeyValuePair<string, string>(c.Name.GetString(true).Trim(), c.NoteMiniGuid);
		IEnumerable<KeyValuePair<string, string>> second = from c in source
		where !string.IsNullOrEmpty(c.CreditsCardName)
		select new KeyValuePair<string, string>(c.CreditsCardName.Trim(), c.NoteMiniGuid);
		this.m_creditsCardsByName = new Map<string, string>(first.Union(second));
	}

	// Token: 0x0600150A RID: 5386 RVA: 0x00078620 File Offset: 0x00076820
	private void LoadAllCreditsCards()
	{
		this.ReleaseAllCreditsCards();
		foreach (string cardId in this.m_cardsToLoad)
		{
			DefLoader.Get().LoadFullDef(cardId, new DefLoader.LoadDefCallback<DefLoader.DisposableFullDef>(this.OnFullDefLoaded), null, null);
		}
	}

	// Token: 0x0600150B RID: 5387 RVA: 0x0007868C File Offset: 0x0007688C
	private void ReleaseAllCreditsCards()
	{
		this.m_creditsDefs.DisposeValuesAndClear<DefLoader.DisposableFullDef>();
	}

	// Token: 0x0600150C RID: 5388 RVA: 0x0007869C File Offset: 0x0007689C
	private void LoadCreditsText()
	{
		this.m_creditsTextLoadSucceeded = false;
		this.m_cardsToLoad.Clear();
		string text = null;
		if (this.m_creditsYearIndex >= 0 && this.m_creditsYearIndex < this.m_creditsYearsAvailable.Length)
		{
			text = this.m_creditsYearsAvailable[this.m_creditsYearIndex].ContentsFilename;
		}
		string filePath = this.GetFilePath(text);
		if (filePath == null)
		{
			Error.AddDevWarning("Credits Error", "CreditsDisplay.LoadCreditsText() - Failed to find file for CREDITS: {0}", new object[]
			{
				text
			});
			this.m_creditsTextLoaded = true;
			return;
		}
		try
		{
			this.m_creditLines = File.ReadAllLines(filePath);
			this.m_creditsTextLoadSucceeded = true;
		}
		catch (Exception ex)
		{
			Error.AddDevWarning("Credits Error", "CreditsDisplay.LoadCreditsText() - Failed to read \"{0}\".\n\nException: {1}", new object[]
			{
				filePath,
				ex.Message
			});
		}
		for (int i = 0; i < this.m_creditLines.Length; i++)
		{
			string text2 = this.m_creditLines[i].Trim();
			if (text2 == "<!-- no more cards displayed after this line -->")
			{
				this.m_creditLines[i] = string.Empty;
				break;
			}
			string item;
			if (this.m_creditsCardsByName != null && this.m_creditsCardsByName.TryGetValue(text2, out item) && !this.m_cardsToLoad.Contains(item))
			{
				this.m_cardsToLoad.Add(item);
			}
		}
		this.m_creditsTextLoaded = true;
		this.LoadAllCreditsCards();
	}

	// Token: 0x0600150D RID: 5389 RVA: 0x000787E4 File Offset: 0x000769E4
	private string GetFilePath(string creditsFilename)
	{
		if (creditsFilename == null)
		{
			return null;
		}
		Locale[] loadOrder = Localization.GetLoadOrder(false);
		for (int i = 0; i < loadOrder.Length; i++)
		{
			string assetPath = GameStrings.GetAssetPath(loadOrder[i], creditsFilename, false);
			if (assetPath != null && File.Exists(assetPath))
			{
				return assetPath;
			}
		}
		return null;
	}

	// Token: 0x0600150E RID: 5390 RVA: 0x00078824 File Offset: 0x00076A24
	private void FlopCredits()
	{
		if (this.m_currentText == this.m_creditsText1)
		{
			this.m_currentText = this.m_creditsText2;
		}
		else
		{
			this.m_currentText = this.m_creditsText1;
		}
		this.m_currentText.Text = this.GetNextCreditsChunk();
		this.DropText();
	}

	// Token: 0x0600150F RID: 5391 RVA: 0x00078878 File Offset: 0x00076A78
	private void DropText()
	{
		UberText uberText = this.m_creditsText1;
		if (this.m_currentText == this.m_creditsText1)
		{
			uberText = this.m_creditsText2;
		}
		float z = 1.8649f;
		TransformUtil.SetPoint(this.m_currentText.gameObject, Anchor.FRONT, uberText.gameObject, Anchor.BACK, new Vector3(0f, 0f, z));
	}

	// Token: 0x06001510 RID: 5392 RVA: 0x000788D8 File Offset: 0x00076AD8
	private string GetNextCreditsChunk()
	{
		string text = "";
		int currentLine = this.m_currentLine;
		int num = 70;
		for (int i = 0; i < num; i++)
		{
			if (this.m_creditLines.Length < i + currentLine + 1)
			{
				this.m_creditsDone = true;
				this.StartEndCreditsTimer();
				return text;
			}
			string text2 = this.m_creditLines[i + currentLine];
			if (text2.Length > 38)
			{
				num -= Mathf.CeilToInt((float)(text2.Length / 38));
				if (i > num && i > 60)
				{
					break;
				}
			}
			text += text2;
			text += Environment.NewLine;
			this.m_currentLine++;
		}
		return text;
	}

	// Token: 0x06001511 RID: 5393 RVA: 0x00078976 File Offset: 0x00076B76
	private void ActorLoadedCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.m_fakeCards.Add(go.GetComponent<Actor>());
	}

	// Token: 0x06001512 RID: 5394 RVA: 0x00078989 File Offset: 0x00076B89
	private void Start()
	{
		Navigation.Push(new Navigation.NavigateBackHandler(this.EndCredits));
		base.StartCoroutine(this.NotifySceneLoadedWhenReady());
	}

	// Token: 0x06001513 RID: 5395 RVA: 0x000789A9 File Offset: 0x00076BA9
	private IEnumerator NotifySceneLoadedWhenReady()
	{
		while (this.m_fakeCards.Count < 2)
		{
			yield return null;
		}
		while (!this.m_creditsTextLoaded)
		{
			yield return null;
		}
		Box.Get().AddTransitionFinishedListener(new Box.TransitionFinishedCallback(this.OnBoxOpened));
		SceneMgr.Get().NotifySceneLoaded();
		yield break;
	}

	// Token: 0x06001514 RID: 5396 RVA: 0x000789B8 File Offset: 0x00076BB8
	private void OnBoxOpened(object userData)
	{
		Box.Get().RemoveTransitionFinishedListener(new Box.TransitionFinishedCallback(this.OnBoxOpened));
		if (!this.m_creditsTextLoadSucceeded)
		{
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
			return;
		}
		MusicManager.Get().StartPlaylist(MusicPlaylistType.UI_Credits);
		this.START_CREDITS_COROUTINE = base.StartCoroutine(this.StartCredits());
	}

	// Token: 0x06001515 RID: 5397 RVA: 0x00078A11 File Offset: 0x00076C11
	private IEnumerator StartCredits()
	{
		this.m_creditsText2.Text = this.GetNextCreditsChunk();
		this.m_currentText = this.m_creditsText2;
		this.FlopCredits();
		this.started = true;
		this.m_creditsRoot.SetActive(true);
		yield return new WaitForSeconds(4f);
		this.SHOW_NEW_CARD_COROUTINE = base.StartCoroutine(this.ShowNewCard());
		yield break;
	}

	// Token: 0x06001516 RID: 5398 RVA: 0x00078A20 File Offset: 0x00076C20
	private IEnumerator ShowNewCard()
	{
		if (this.m_creditsDefs == null || this.m_creditsDefs.Count == 0)
		{
			yield break;
		}
		float time = 1f;
		int num = 0;
		if (this.m_lastCard == 0)
		{
			num = 1;
		}
		this.m_lastCard = num;
		this.m_shownCreditsCard = this.m_fakeCards[num];
		int index = this.m_sortedCards ? 0 : UnityEngine.Random.Range(0, this.m_creditsDefs.Count);
		this.m_shownCreditsCard.SetCardDef(this.m_creditsDefs[index].DisposableCardDef);
		EntityDef entityDef = this.m_creditsDefs[index].EntityDef;
		bool flag = entityDef.GetCardId() == "CRED_10";
		if (flag)
		{
			entityDef.SetTag<TAG_RACE>(GAME_TAG.CARDRACE, TAG_RACE.PIRATE);
		}
		this.m_shownCreditsCard.SetEntityDef(entityDef);
		this.m_creditsDefs.DisposeAndRemoveAt(index);
		this.m_shownCreditsCard.UpdateAllComponents();
		this.m_shownCreditsCard.Show();
		if (flag)
		{
			this.m_shownCreditsCard.GetRaceText().Text = GameStrings.Get("GLUE_NINJA");
		}
		this.m_shownCreditsCard.transform.position = this.m_offscreenCardBone.position;
		this.m_shownCreditsCard.transform.localScale = this.m_offscreenCardBone.localScale;
		this.m_shownCreditsCard.transform.localEulerAngles = this.m_offscreenCardBone.localEulerAngles;
		SoundManager.Get().LoadAndPlay(this.s_credits_card_enter[UnityEngine.Random.Range(0, 2)]);
		iTween.MoveTo(this.m_shownCreditsCard.gameObject, this.m_cardBone.position, time);
		iTween.RotateTo(this.m_shownCreditsCard.gameObject, this.m_cardBone.localEulerAngles, time);
		Actor oldActor = this.m_shownCreditsCard;
		yield return new WaitForSeconds(0.5f);
		SoundManager.Get().LoadAndPlay(this.s_tavern_crowd_play_reaction_positive[UnityEngine.Random.Range(0, 4)]);
		yield return new WaitForSeconds(7.5f);
		this.m_shownCreditsCard.ActivateSpellBirthState(SpellType.BURN);
		SoundManager.Get().LoadAndPlay(this.s_credits_card_embers[UnityEngine.Random.Range(0, 2)]);
		if (this.m_shownCreditsCard == oldActor)
		{
			this.m_shownCreditsCard = null;
		}
		yield return new WaitForSeconds(11f);
		this.SHOW_NEW_CARD_COROUTINE = base.StartCoroutine(this.ShowNewCard());
		yield break;
	}

	// Token: 0x06001517 RID: 5399 RVA: 0x00078A30 File Offset: 0x00076C30
	private void Update()
	{
		Network.Get().ProcessNetwork();
		if (!this.started)
		{
			return;
		}
		this.m_creditsRoot.transform.localPosition += new Vector3(0f, 0f, this.m_creditsScrollSpeed * Time.deltaTime);
		if (this.m_creditsDone)
		{
			return;
		}
		if (this.m_currentText == null)
		{
			return;
		}
		if (this.GetTopOfCurrentCredits() > this.m_flopPoint.position.z)
		{
			this.FlopCredits();
		}
		this.ReadKeyboardInput();
	}

	// Token: 0x06001518 RID: 5400 RVA: 0x00078AC4 File Offset: 0x00076CC4
	private float GetTopOfCurrentCredits()
	{
		Bounds textWorldSpaceBounds = this.m_currentText.GetTextWorldSpaceBounds();
		return textWorldSpaceBounds.center.z + textWorldSpaceBounds.extents.z;
	}

	// Token: 0x06001519 RID: 5401 RVA: 0x00078AF6 File Offset: 0x00076CF6
	private void OnFullDefLoaded(string cardID, DefLoader.DisposableFullDef def, object userData)
	{
		this.m_creditsDefs.Add(def);
	}

	// Token: 0x0600151A RID: 5402 RVA: 0x00078B04 File Offset: 0x00076D04
	private void OnDonePressed(UIEvent e)
	{
		if (UniversalInputManager.UsePhoneUI)
		{
			Box.Get().m_letterboxingContainer.SetActive(true);
		}
		Navigation.GoBack();
	}

	// Token: 0x0600151B RID: 5403 RVA: 0x00078B28 File Offset: 0x00076D28
	private void OnYearPressed1(UIEvent e)
	{
		if (this.m_creditsYearIndex <= 0)
		{
			this.m_creditsYearIndex++;
		}
		else if (this.m_creditsYearIndex >= this.m_creditsYearsAvailable.Length - 1)
		{
			this.m_creditsYearIndex = this.m_creditsYearsAvailable.Length - 3;
		}
		else
		{
			this.m_creditsYearIndex--;
		}
		if (this.m_creditsYearIndex < 0 || this.m_creditsYearIndex >= this.m_creditsYearsAvailable.Length)
		{
			this.m_creditsYearIndex = -1;
		}
		this.OnYearPressed();
	}

	// Token: 0x0600151C RID: 5404 RVA: 0x00078BA8 File Offset: 0x00076DA8
	private void OnYearPressed2(UIEvent e)
	{
		if (this.m_creditsYearIndex <= 0)
		{
			this.m_creditsYearIndex += 2;
		}
		else if (this.m_creditsYearIndex >= this.m_creditsYearsAvailable.Length - 1)
		{
			this.m_creditsYearIndex = this.m_creditsYearsAvailable.Length - 2;
		}
		else
		{
			this.m_creditsYearIndex++;
		}
		if (this.m_creditsYearIndex < 0 || this.m_creditsYearIndex >= this.m_creditsYearsAvailable.Length)
		{
			this.m_creditsYearIndex = -1;
		}
		this.OnYearPressed();
	}

	// Token: 0x0600151D RID: 5405 RVA: 0x00078C28 File Offset: 0x00076E28
	private void OnYearPressed()
	{
		this.StopAndClearCoroutine(ref this.START_CREDITS_COROUTINE);
		this.StopAndClearCoroutine(ref this.SHOW_NEW_CARD_COROUTINE);
		if (this.m_shownCreditsCard != null)
		{
			this.m_shownCreditsCard.ActivateSpellBirthState(SpellType.BURN);
			SoundManager.Get().LoadAndPlay(this.s_credits_card_enter[UnityEngine.Random.Range(0, 2)]);
			this.m_shownCreditsCard = null;
		}
		base.StartCoroutine(this.ResetCredits());
	}

	// Token: 0x0600151E RID: 5406 RVA: 0x00078C98 File Offset: 0x00076E98
	private void OnFasterButtonPressed(UIEvent e)
	{
		this.m_creditsScrollSpeedCurrentStep++;
		if (this.m_creditsScrollSpeedCurrentStep == 3)
		{
			this.m_fasterButton.gameObject.SetActive(false);
		}
		this.m_slowerButton.gameObject.SetActive(true);
		this.m_creditsScrollSpeed = 3.5f + (float)this.m_creditsScrollSpeedCurrentStep * 0.75f;
	}

	// Token: 0x0600151F RID: 5407 RVA: 0x00078CF8 File Offset: 0x00076EF8
	private void OnSlowerButtonPressed(UIEvent e)
	{
		this.m_creditsScrollSpeedCurrentStep--;
		if (this.m_creditsScrollSpeedCurrentStep == -3)
		{
			this.m_slowerButton.gameObject.SetActive(false);
		}
		this.m_fasterButton.gameObject.SetActive(true);
		this.m_creditsScrollSpeed = 3.5f + (float)this.m_creditsScrollSpeedCurrentStep * 0.75f;
	}

	// Token: 0x06001520 RID: 5408 RVA: 0x00078D58 File Offset: 0x00076F58
	private void UpdateYearButtons()
	{
		int num;
		int num2;
		if (this.m_creditsYearIndex <= 0)
		{
			num = 1;
			num2 = 2;
		}
		else if (this.m_creditsYearIndex >= this.m_creditsYearsAvailable.Length - 1)
		{
			num = this.m_creditsYearsAvailable.Length - 3;
			num2 = this.m_creditsYearsAvailable.Length - 2;
		}
		else
		{
			num = this.m_creditsYearIndex - 1;
			num2 = this.m_creditsYearIndex + 1;
		}
		if (num >= 0 && num < this.m_creditsYearsAvailable.Length)
		{
			this.m_yearButton1.gameObject.SetActive(true);
			string text = this.m_creditsYearsAvailable[num].ButtonLabel;
			if (string.IsNullOrEmpty(text))
			{
				text = this.m_creditsYearsAvailable[num].ID.ToString();
			}
			this.m_yearButton1.SetText(text);
		}
		else
		{
			this.m_yearButton1.gameObject.SetActive(false);
		}
		if (num2 >= 0 && num2 < this.m_creditsYearsAvailable.Length)
		{
			this.m_yearButton2.gameObject.SetActive(true);
			string text2 = this.m_creditsYearsAvailable[num2].ButtonLabel;
			if (string.IsNullOrEmpty(text2))
			{
				text2 = this.m_creditsYearsAvailable[num2].ID.ToString();
			}
			this.m_yearButton2.SetText(text2);
			return;
		}
		this.m_yearButton2.gameObject.SetActive(false);
	}

	// Token: 0x06001521 RID: 5409 RVA: 0x00078E97 File Offset: 0x00077097
	private IEnumerator ResetCredits()
	{
		this.m_currentText = null;
		this.m_creditsText1.Text = "";
		this.m_creditsText2.Text = "";
		this.started = false;
		this.m_creditsTextLoaded = false;
		this.m_creditsTextLoadSucceeded = false;
		this.m_creditsDone = false;
		this.m_currentLine = 0;
		this.m_creditLines = null;
		this.UpdateYearButtons();
		this.m_creditsText1.transform.localPosition = this.creditsText1StartLocalPosition;
		this.m_creditsText2.transform.localPosition = this.creditsText2StartLocalPosition;
		this.m_creditsRoot.transform.localPosition = this.creditsRootStartLocalPosition;
		this.m_lastCard = 1;
		this.ReleaseAllCreditsCards();
		this.LoadCreditsText();
		while (!this.m_creditsTextLoaded)
		{
			yield return null;
		}
		this.StopAndClearCoroutine(ref this.END_CREDITS_COROUTINE);
		this.StopAndClearCoroutine(ref this.START_CREDITS_COROUTINE);
		this.START_CREDITS_COROUTINE = base.StartCoroutine(this.StartCredits());
		yield break;
	}

	// Token: 0x06001522 RID: 5410 RVA: 0x00078EA8 File Offset: 0x000770A8
	private bool EndCredits()
	{
		iTween.FadeTo(this.m_creditsText1.gameObject, 0f, 0.1f);
		iTween.FadeTo(this.m_creditsText2.gameObject, 0f, 0.1f);
		SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
		return true;
	}

	// Token: 0x06001523 RID: 5411 RVA: 0x00078EF7 File Offset: 0x000770F7
	private void StartEndCreditsTimer()
	{
		this.END_CREDITS_COROUTINE = base.StartCoroutine(this.EndCreditsTimer());
	}

	// Token: 0x06001524 RID: 5412 RVA: 0x00078F0B File Offset: 0x0007710B
	private IEnumerator EndCreditsTimer()
	{
		yield return new WaitForSeconds(300f);
		if (SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB) || SceneMgr.Get().GetMode() != SceneMgr.Mode.CREDITS)
		{
			yield break;
		}
		Navigation.GoBack();
		yield break;
	}

	// Token: 0x06001525 RID: 5413 RVA: 0x00078F14 File Offset: 0x00077114
	private void ReadKeyboardInput()
	{
		if (HearthstoneApplication.IsPublic())
		{
			return;
		}
		int num = Input.touches.Count((Touch t) => t.phase == TouchPhase.Began);
		if (InputCollection.GetKeyDown(KeyCode.Plus) || InputCollection.GetKeyDown(KeyCode.KeypadPlus))
		{
			this.OnFasterButtonPressed(null);
			this.m_slowerButton.gameObject.SetActive(false);
			this.m_fasterButton.gameObject.SetActive(false);
			return;
		}
		if (InputCollection.GetKeyDown(KeyCode.Minus) || InputCollection.GetKeyDown(KeyCode.KeypadMinus))
		{
			this.OnSlowerButtonPressed(null);
			this.m_slowerButton.gameObject.SetActive(false);
			this.m_fasterButton.gameObject.SetActive(false);
			return;
		}
		if (InputCollection.GetKeyDown(KeyCode.Space))
		{
			this.LoadAllCreditsCards();
			UIStatus.Get().AddInfo(string.Format("Reset cards list: {0} to display", this.m_creditsDefs.Count));
			return;
		}
		if (!InputCollection.GetKeyDown(KeyCode.N) && num != 2)
		{
			if (InputCollection.GetKeyDown(KeyCode.S) || num == 5)
			{
				if (this.m_creditsDefs != null)
				{
					this.m_sortedCards = !this.m_sortedCards;
					if (this.m_sortedCards)
					{
						this.m_creditsDefs.Sort((DefLoader.DisposableFullDef a, DefLoader.DisposableFullDef b) => this.m_cardsToLoad.IndexOf(a.EntityDef.GetCardId()).CompareTo(this.m_cardsToLoad.IndexOf(b.EntityDef.GetCardId())));
					}
					UIStatus.Get().AddInfo(string.Format("{0} remaining cards to display {1}.", this.m_creditsDefs.Count, this.m_sortedCards ? "sorted" : "randomized"));
					return;
				}
			}
			else if (InputCollection.GetKeyDown(KeyCode.D))
			{
				string[] array = (from n in (from d in this.m_creditsDefs
				let e = d.EntityDef
				select new
				{
					<>h__TransparentIdentifier0 = <>h__TransparentIdentifier0,
					c = ((e == null) ? null : GameDbf.GetIndex().GetCardRecord(e.GetCardId()))
				}).Select(delegate(<>h__TransparentIdentifier1)
				{
					if (<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.e != null)
					{
						return string.Format("{0}-{1} {2}{3}", new object[]
						{
							GameUtils.TranslateCardIdToDbId(<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.e.GetCardId(), false),
							<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.e.GetCardId(),
							<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.e.GetName(),
							(<>h__TransparentIdentifier1.c == null || string.IsNullOrEmpty(<>h__TransparentIdentifier1.c.CreditsCardName)) ? string.Empty : string.Format(" ({0})", <>h__TransparentIdentifier1.c.CreditsCardName)
						});
					}
					return null;
				})
				where !string.IsNullOrEmpty(n)
				select n).ToArray<string>();
				Log.All.Print("Credits Cards to show:\n{0}", new object[]
				{
					string.Join("\n", array)
				});
				UIStatus.Get().AddInfo(string.Format("Dumped to log: {0} remaining cards to display.", array.Length));
			}
			return;
		}
		if (this.m_creditsDefs == null || this.m_creditsDefs.Count == 0)
		{
			this.LoadAllCreditsCards();
			UIStatus.Get().AddInfo(string.Format("Reset cards list: {0} to display", this.m_creditsDefs.Count));
			return;
		}
		this.StopAndClearCoroutine(ref this.SHOW_NEW_CARD_COROUTINE);
		if (this.m_shownCreditsCard != null)
		{
			this.m_shownCreditsCard.ActivateSpellBirthState(SpellType.BURN);
			this.m_shownCreditsCard = null;
		}
		this.SHOW_NEW_CARD_COROUTINE = base.StartCoroutine(this.ShowNewCard());
	}

	// Token: 0x04000DFE RID: 3582
	public GameObject m_creditsRoot;

	// Token: 0x04000DFF RID: 3583
	public UberText m_creditsText1;

	// Token: 0x04000E00 RID: 3584
	public UberText m_creditsText2;

	// Token: 0x04000E01 RID: 3585
	private UberText m_currentText;

	// Token: 0x04000E02 RID: 3586
	public Transform m_offscreenCardBone;

	// Token: 0x04000E03 RID: 3587
	public Transform m_cardBone;

	// Token: 0x04000E04 RID: 3588
	public UIBButton m_doneButton;

	// Token: 0x04000E05 RID: 3589
	public UIBButton m_yearButton1;

	// Token: 0x04000E06 RID: 3590
	public UIBButton m_yearButton2;

	// Token: 0x04000E07 RID: 3591
	public UIBButton m_fasterButton;

	// Token: 0x04000E08 RID: 3592
	public UIBButton m_slowerButton;

	// Token: 0x04000E09 RID: 3593
	public Transform m_flopPoint;

	// Token: 0x04000E0A RID: 3594
	public GameObject m_doneArrowInButton;

	// Token: 0x04000E0B RID: 3595
	private const string CREDITS_TEXT_CARD_CUTOFF_LINE = "<!-- no more cards displayed after this line -->";

	// Token: 0x04000E0C RID: 3596
	private float m_creditsScrollSpeed = 3.5f;

	// Token: 0x04000E0D RID: 3597
	private const float CREDITS_SCROLL_SPEED_DEFAULT = 3.5f;

	// Token: 0x04000E0E RID: 3598
	private const float CREDITS_SCROLL_SPEED_STEP = 0.75f;

	// Token: 0x04000E0F RID: 3599
	private const int CREDITS_SCROLL_STEP_LIMIT = 3;

	// Token: 0x04000E10 RID: 3600
	private int m_creditsScrollSpeedCurrentStep;

	// Token: 0x04000E11 RID: 3601
	private const int MAX_LINES_PER_CHUNK = 70;

	// Token: 0x04000E12 RID: 3602
	private static CreditsDisplay s_instance;

	// Token: 0x04000E13 RID: 3603
	private string[] m_creditLines;

	// Token: 0x04000E14 RID: 3604
	private int m_currentLine;

	// Token: 0x04000E15 RID: 3605
	private List<Actor> m_fakeCards;

	// Token: 0x04000E16 RID: 3606
	private List<DefLoader.DisposableFullDef> m_creditsDefs = new List<DefLoader.DisposableFullDef>();

	// Token: 0x04000E17 RID: 3607
	private bool started;

	// Token: 0x04000E18 RID: 3608
	private bool m_creditsTextLoaded;

	// Token: 0x04000E19 RID: 3609
	private bool m_creditsTextLoadSucceeded;

	// Token: 0x04000E1A RID: 3610
	private bool m_creditsDone;

	// Token: 0x04000E1B RID: 3611
	private Actor m_shownCreditsCard;

	// Token: 0x04000E1C RID: 3612
	private Vector3 creditsRootStartLocalPosition;

	// Token: 0x04000E1D RID: 3613
	private Vector3 creditsText1StartLocalPosition;

	// Token: 0x04000E1E RID: 3614
	private Vector3 creditsText2StartLocalPosition;

	// Token: 0x04000E1F RID: 3615
	private int m_lastCard = 1;

	// Token: 0x04000E20 RID: 3616
	private List<string> m_cardsToLoad = new List<string>();

	// Token: 0x04000E21 RID: 3617
	private bool m_sortedCards;

	// Token: 0x04000E22 RID: 3618
	private Map<string, string> m_creditsCardsByName;

	// Token: 0x04000E23 RID: 3619
	private Coroutine END_CREDITS_COROUTINE;

	// Token: 0x04000E24 RID: 3620
	private Coroutine START_CREDITS_COROUTINE;

	// Token: 0x04000E25 RID: 3621
	private Coroutine SHOW_NEW_CARD_COROUTINE;

	// Token: 0x04000E26 RID: 3622
	private AssetReference[] s_credits_card_embers;

	// Token: 0x04000E27 RID: 3623
	private AssetReference[] s_credits_card_enter;

	// Token: 0x04000E28 RID: 3624
	private AssetReference[] s_tavern_crowd_play_reaction_positive;

	// Token: 0x04000E29 RID: 3625
	private int m_creditsYearIndex = -1;

	// Token: 0x04000E2A RID: 3626
	private CreditsYearDbfRecord[] m_creditsYearsAvailable = new CreditsYearDbfRecord[0];
}
