using System;
using System.Collections;
using System.Collections.Generic;
using PegasusShared;
using UnityEngine;

// Token: 0x0200063C RID: 1596
public class RAFFrame : UIBPopup
{
	// Token: 0x060059E2 RID: 23010 RVA: 0x001D5790 File Offset: 0x001D3990
	protected override void Awake()
	{
		base.Awake();
		RAFFrame.s_Instance = this;
		this.m_recruitFriendsButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnRecruitFriendsButtonReleased));
		this.m_infoButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnInfoButtonReleased));
		this.m_heroDisplay.m_previewToggle.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnHeroPreviewToggle));
	}

	// Token: 0x060059E3 RID: 23011 RVA: 0x001D57FC File Offset: 0x001D39FC
	protected override void Start()
	{
		base.Start();
		this.m_heroChest = this.m_chestList[0];
		this.m_heroChest.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.ShowHeroRewardTooltip));
		this.m_heroChest.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.HideHeroRewardTooltip));
		for (int i = 1; i < this.m_chestList.Count; i++)
		{
			this.m_chestList[i].AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.ShowPackRewardTooltip));
			this.m_chestList[i].AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.HidePackRewardTooltip));
		}
		this.m_packReward.AddBooster();
		if (this.m_shownDisplay == RAFFrame.Display.NONE)
		{
			this.ShowHeroFrame();
		}
		this.UpdateRecruitFriendsButtonGlow();
		BnetBar bnetBar = BnetBar.Get();
		if (bnetBar != null)
		{
			bnetBar.OnMenuOpened += this.OnMenuOpened;
		}
	}

	// Token: 0x060059E4 RID: 23012 RVA: 0x001D58E8 File Offset: 0x001D3AE8
	private void OnDestroy()
	{
		this.Hide(true);
		BnetBar bnetBar = BnetBar.Get();
		if (bnetBar != null)
		{
			bnetBar.OnMenuOpened -= this.OnMenuOpened;
		}
		RAFFrame.s_Instance = null;
	}

	// Token: 0x060059E5 RID: 23013 RVA: 0x001D5923 File Offset: 0x001D3B23
	public static RAFFrame Get()
	{
		return RAFFrame.s_Instance;
	}

	// Token: 0x060059E6 RID: 23014 RVA: 0x001D592C File Offset: 0x001D3B2C
	public override void Show()
	{
		if (base.IsShown())
		{
			return;
		}
		Navigation.Push(new Navigation.NavigateBackHandler(this.OnNavigateBack));
		FullScreenFXMgr.Get().StartStandardBlurVignette(0.1f);
		if (UniversalInputManager.UsePhoneUI)
		{
			BnetBar.Get().HideCurrencyTemporarily();
		}
		base.transform.parent = BaseUI.Get().transform;
		base.Show(false);
		if (this.m_inputBlockerPegUIElement != null)
		{
			UnityEngine.Object.Destroy(this.m_inputBlockerPegUIElement.gameObject);
			this.m_inputBlockerPegUIElement = null;
		}
		GameObject gameObject = CameraUtils.CreateInputBlocker(CameraUtils.FindFirstByLayer(base.gameObject.layer), "RAFInputBlocker");
		SceneUtils.SetLayer(gameObject, base.gameObject.layer, null);
		this.m_inputBlockerPegUIElement = gameObject.AddComponent<PegUIElement>();
		this.m_inputBlockerPegUIElement.transform.parent = base.transform;
		this.m_inputBlockerPegUIElement.transform.localPosition = new Vector3(0f, -5f, 0f);
		this.m_inputBlockerPegUIElement.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnInputBlockerRelease));
		Options.Get().SetBool(Option.HAS_SEEN_RAF, true);
		FriendListFrame friendListFrame = ChatMgr.Get().FriendListFrame;
		if (friendListFrame != null)
		{
			friendListFrame.UpdateRAFButtonGlow();
		}
	}

	// Token: 0x060059E7 RID: 23015 RVA: 0x001D5A78 File Offset: 0x001D3C78
	protected override void Hide(bool animate)
	{
		if (!base.IsShown())
		{
			return;
		}
		Navigation.RemoveHandler(new Navigation.NavigateBackHandler(this.OnNavigateBack));
		if (this.m_linkFrame != null)
		{
			this.m_linkFrame.Hide();
		}
		if (this.m_inputBlockerPegUIElement != null)
		{
			UnityEngine.Object.Destroy(this.m_inputBlockerPegUIElement.gameObject);
			this.m_inputBlockerPegUIElement = null;
		}
		FullScreenFXMgr.Get().EndStandardBlurVignette(0.1f, null);
		this.m_heroDisplay.ResetPreview();
		this.m_isHeroKeyArtShowing = true;
		this.StopHeroMusic();
		if (UniversalInputManager.UsePhoneUI)
		{
			BnetBar.Get().RefreshCurrency();
		}
		base.Hide(animate);
	}

	// Token: 0x060059E8 RID: 23016 RVA: 0x001D5B24 File Offset: 0x001D3D24
	public void ShowProgressFrame()
	{
		this.m_heroFrame.SetActive(false);
		this.m_progressFrame.SetActive(true);
		if (this.m_heroActor == null)
		{
			AssetLoader.Get().InstantiatePrefab("Card_Play_Hero.prefab:42cbbd2c4969afb46b3887bb628de19d", new PrefabCallback<GameObject>(this.OnHeroActorLoaded), null, AssetLoadingOptions.IgnorePrefabPosition);
		}
		if (this.m_heroPowerActor == null)
		{
			AssetLoader.Get().InstantiatePrefab("Card_Play_HeroPower.prefab:a3794839abb947146903a26be13e09af", new PrefabCallback<GameObject>(this.OnHeroPowerActorLoaded), null, AssetLoadingOptions.IgnorePrefabPosition);
		}
		this.m_shownDisplay = RAFFrame.Display.PROGRESS;
	}

	// Token: 0x060059E9 RID: 23017 RVA: 0x001D5BB4 File Offset: 0x001D3DB4
	public void ShowHeroFrame()
	{
		this.m_heroFrame.SetActive(true);
		if (!this.m_isHeroDisplaySetup)
		{
			this.m_heroDisplay.SetKeyArtRenderer(this.m_heroDisplay.m_parentLite.m_renderQuad);
			this.m_heroDisplay.m_parentLite.m_renderToTexture.GetComponent<RenderToTexture>().m_RenderToObject = this.m_heroDisplay.m_renderArtQuad;
			CardHeroDbfRecord record = GameDbf.CardHero.GetRecord(17);
			using (DefLoader.DisposableCardDef cardDef = DefLoader.Get().GetCardDef(record.CardId))
			{
				this.m_collectionHeroDef = GameUtils.LoadGameObjectWithComponent<CollectionHeroDef>(cardDef.CardDef.m_CollectionHeroDefPath);
			}
			this.m_heroDisplay.UpdateFrame(record, 0, this.m_collectionHeroDef);
			this.m_isHeroDisplaySetup = true;
		}
		else
		{
			this.m_heroDisplay.ResetPreview();
		}
		this.m_progressFrame.SetActive(false);
		this.m_shownDisplay = RAFFrame.Display.HERO;
	}

	// Token: 0x060059EA RID: 23018 RVA: 0x001D5CA4 File Offset: 0x001D3EA4
	public void ResetProgressFrame()
	{
		foreach (RAFRecruitBar rafrecruitBar in this.m_recruitContainerList)
		{
			rafrecruitBar.SetLocked(true);
		}
		this.m_recruitCount.SetActive(false);
		this.m_totalResultLabel.SetActive(false);
		this.m_totalResult.SetActive(false);
		foreach (RAFChest rafchest in this.m_chestList)
		{
			rafchest.SetOpen(false);
		}
	}

	// Token: 0x060059EB RID: 23019 RVA: 0x001D5D5C File Offset: 0x001D3F5C
	public void UpdateRecruitFriendsButtonGlow()
	{
		bool @bool = Options.Get().GetBool(Option.HAS_SEEN_RAF_RECRUIT_URL);
		this.m_recruitFriendsButtonGlow.ChangeState(@bool ? ActorStateType.NONE : ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
	}

	// Token: 0x060059EC RID: 23020 RVA: 0x001D5D90 File Offset: 0x001D3F90
	public void SetProgress(int numRecruits)
	{
		this.ResetProgressFrame();
		int num = 0;
		while (num < numRecruits && num < 5)
		{
			RAFRecruitBar rafrecruitBar = this.m_recruitContainerList[num];
			rafrecruitBar.SetLocked(false);
			rafrecruitBar.SetBattleTag("GoodKnight#1234");
			rafrecruitBar.SetLevel(20);
			this.m_chestList[num].SetOpen(true);
			num++;
		}
		if (numRecruits > 5)
		{
			this.m_recruitCount.gameObject.SetActive(true);
			int num2 = numRecruits - 5;
			this.m_recruitCountText.Text = GameStrings.Format("GLUE_RAF_PROGRESS_FRAME_RECRUIT_COUNT", new object[]
			{
				num2,
				num2
			});
		}
	}

	// Token: 0x060059ED RID: 23021 RVA: 0x001D5E30 File Offset: 0x001D4030
	public void SetProgressData(uint totalRecruitCount, List<RAFManager.RecruitData> topRecruits)
	{
		this.ResetProgressFrame();
		if (totalRecruitCount == 0U)
		{
			Log.RAF.PrintError("SetProgressData() - totalRecruitCount is 0!", Array.Empty<object>());
			this.ShowHeroFrame();
			return;
		}
		if (topRecruits == null)
		{
			Log.RAF.PrintError("SetProgressData() - topRecruits is NULL!", Array.Empty<object>());
			this.ShowHeroFrame();
			return;
		}
		for (int i = 0; i < topRecruits.Count; i++)
		{
			RAFRecruitBar rafrecruitBar = this.m_recruitContainerList[i];
			rafrecruitBar.SetLocked(false);
			RAFManager.RecruitData recruitData = topRecruits[i];
			string battleTag = (recruitData.m_recruitBattleTag == null) ? GameStrings.Get("GAMEPLAY_UNKNOWN_OPPONENT_NAME") : recruitData.m_recruitBattleTag;
			int progress = (int)recruitData.m_recruit.Progress;
			rafrecruitBar.SetGameAccountId(recruitData.m_recruit.GameAccountId);
			rafrecruitBar.SetBattleTag(battleTag);
			rafrecruitBar.SetLevel(progress);
			if (progress >= 20)
			{
				this.m_chestList[i].SetOpen(true);
			}
		}
		if (totalRecruitCount > 5U)
		{
			this.m_recruitCount.gameObject.SetActive(true);
			int num = (int)(totalRecruitCount - 5U);
			this.m_recruitCountText.Text = GameStrings.Format("GLUE_RAF_PROGRESS_FRAME_RECRUIT_COUNT", new object[]
			{
				num,
				num
			});
		}
	}

	// Token: 0x060059EE RID: 23022 RVA: 0x001D5F54 File Offset: 0x001D4154
	public void UpdateBattleTag(BnetId gameAccountId, string battleTag)
	{
		foreach (RAFRecruitBar rafrecruitBar in this.m_recruitContainerList)
		{
			if (rafrecruitBar.GetGameAccountId() == gameAccountId)
			{
				rafrecruitBar.SetBattleTag(battleTag);
				break;
			}
		}
	}

	// Token: 0x060059EF RID: 23023 RVA: 0x001D5FB4 File Offset: 0x001D41B4
	public void ShowLinkFrame(string displayURL, string fullURL)
	{
		Options.Get().SetBool(Option.HAS_SEEN_RAF_RECRUIT_URL, true);
		this.UpdateRecruitFriendsButtonGlow();
		this.m_linkFrame.SetURL(displayURL, fullURL);
		this.m_linkFrame.Show();
	}

	// Token: 0x060059F0 RID: 23024 RVA: 0x001D5FE4 File Offset: 0x001D41E4
	public void DarkenInputBlocker(GameObject inputBlockerObject, float alpha)
	{
		inputBlockerObject.AddComponent<MeshRenderer>().SetMaterial(this.m_inputBlockerRenderer.GetComponent<MeshRenderer>().GetMaterial());
		inputBlockerObject.AddComponent<MeshFilter>().SetMesh(this.m_inputBlockerRenderer.GetComponent<MeshFilter>().GetMesh());
		BoxCollider component = inputBlockerObject.GetComponent<BoxCollider>();
		TransformUtil.SetLocalScaleXY(inputBlockerObject, component.size.x, component.size.y);
		component.size = new Vector3(1f, 1f, 0f);
		TransformUtil.SetLocalEulerAngleX(inputBlockerObject, 90f);
		RenderUtils.SetAlpha(inputBlockerObject, alpha);
	}

	// Token: 0x060059F1 RID: 23025 RVA: 0x001D6076 File Offset: 0x001D4276
	private bool OnNavigateBack()
	{
		this.Hide(true);
		return true;
	}

	// Token: 0x060059F2 RID: 23026 RVA: 0x001D6080 File Offset: 0x001D4280
	private void OnInputBlockerRelease(UIEvent e)
	{
		this.Hide(true);
	}

	// Token: 0x060059F3 RID: 23027 RVA: 0x001D608C File Offset: 0x001D428C
	private void OnRecruitFriendsButtonReleased(UIEvent e)
	{
		string recruitDisplayURL = RAFManager.Get().GetRecruitDisplayURL();
		if (recruitDisplayURL != null)
		{
			string recruitFullURL = RAFManager.Get().GetRecruitFullURL();
			this.ShowLinkFrame(recruitDisplayURL, recruitFullURL);
		}
	}

	// Token: 0x060059F4 RID: 23028 RVA: 0x001D60BA File Offset: 0x001D42BA
	private void OnInfoButtonReleased(UIEvent e)
	{
		this.m_infoFrame.Show();
	}

	// Token: 0x060059F5 RID: 23029 RVA: 0x001D60C7 File Offset: 0x001D42C7
	private void OnHeroPreviewToggle(UIEvent e)
	{
		this.m_isHeroKeyArtShowing = !this.m_isHeroKeyArtShowing;
		if (this.m_isHeroKeyArtShowing)
		{
			this.StopHeroMusic();
			return;
		}
		this.PlayHeroMusic();
	}

	// Token: 0x060059F6 RID: 23030 RVA: 0x001D60F0 File Offset: 0x001D42F0
	private void PlayHeroMusic()
	{
		if (this.m_collectionHeroDef == null)
		{
			Log.RAF.PrintWarning("RAFFrame.PlayHeroMusic - m_collectionHeroDef is NULL!", Array.Empty<object>());
			return;
		}
		MusicPlaylistType heroPlaylist = this.m_collectionHeroDef.m_heroPlaylist;
		if (heroPlaylist != MusicPlaylistType.Invalid)
		{
			this.m_prevMusicPlaylist = MusicManager.Get().GetCurrentPlaylist();
			MusicManager.Get().StartPlaylist(heroPlaylist);
		}
	}

	// Token: 0x060059F7 RID: 23031 RVA: 0x001D614B File Offset: 0x001D434B
	private void StopHeroMusic()
	{
		if (this.m_prevMusicPlaylist != MusicPlaylistType.Invalid)
		{
			MusicManager.Get().StartPlaylist(this.m_prevMusicPlaylist);
			this.m_prevMusicPlaylist = MusicPlaylistType.Invalid;
		}
	}

	// Token: 0x060059F8 RID: 23032 RVA: 0x001D616D File Offset: 0x001D436D
	private void ShowHeroRewardTooltip(UIEvent e)
	{
		this.m_showHeroRewardChestTooltip = true;
		this.m_heroRewardChestTooltipText.Text = GameStrings.Get(this.m_heroChest.IsOpen() ? "GLUE_RAF_HERO_TOOLTIP_REDEEMED_TITLE" : "GLUE_RAF_HERO_TOOLTIP_TITLE");
		base.StartCoroutine(this.ShowHeroRewardTooltipWhenReady());
	}

	// Token: 0x060059F9 RID: 23033 RVA: 0x001D61AC File Offset: 0x001D43AC
	private IEnumerator ShowHeroRewardTooltipWhenReady()
	{
		while (this.m_heroActor == null)
		{
			yield return null;
		}
		if (this.m_showHeroRewardChestTooltip)
		{
			this.m_heroRewardChestTooltip.SetActive(true);
		}
		yield break;
	}

	// Token: 0x060059FA RID: 23034 RVA: 0x001D61BB File Offset: 0x001D43BB
	private void HideHeroRewardTooltip(UIEvent e)
	{
		this.m_showHeroRewardChestTooltip = false;
		this.m_heroRewardChestTooltip.SetActive(false);
	}

	// Token: 0x060059FB RID: 23035 RVA: 0x001D61D0 File Offset: 0x001D43D0
	private void ShowPackRewardTooltip(UIEvent e)
	{
		RAFChest rafchest = e.GetElement() as RAFChest;
		this.m_packRewardChestTooltipText.Text = GameStrings.Get(rafchest.IsOpen() ? "GLUE_RAF_PACK_TOOLTIP_REDEEMED_TITLE" : "GLUE_RAF_PACK_TOOLTIP_TITLE");
		this.m_packRewardChestTooltip.transform.position = rafchest.m_tooltipBone.transform.position;
		this.m_packRewardChestTooltip.SetActive(true);
	}

	// Token: 0x060059FC RID: 23036 RVA: 0x001D6239 File Offset: 0x001D4439
	private void HidePackRewardTooltip(UIEvent e)
	{
		this.m_packRewardChestTooltip.SetActive(false);
	}

	// Token: 0x060059FD RID: 23037 RVA: 0x001D6248 File Offset: 0x001D4448
	private void OnHeroActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Log.RAF.PrintWarning(string.Format("RAFFrame.OnHeroActorLoaded() - FAILED to load actor \"{0}\"", assetRef), Array.Empty<object>());
			return;
		}
		this.m_heroActor = go.GetComponent<Actor>();
		if (this.m_heroActor == null)
		{
			Log.RAF.PrintWarning(string.Format("RAFFrame.OnHeroActorLoaded() - ERROR actor \"{0}\" has no Actor component", assetRef), Array.Empty<object>());
			return;
		}
		go.transform.parent = this.m_heroRewardChestTooltip.transform;
		go.transform.localScale = this.m_heroRewardChestTooltipHeroBone.localScale;
		go.transform.localPosition = this.m_heroRewardChestTooltipHeroBone.localPosition;
		this.m_heroActor.SetUnlit();
		SceneUtils.SetLayer(this.m_heroActor.gameObject, base.gameObject.layer, null);
		UnityEngine.Object.Destroy(this.m_heroActor.m_healthObject);
		UnityEngine.Object.Destroy(this.m_heroActor.m_attackObject);
		this.m_heroActor.Hide();
		string cardIdFromHeroDbId = GameUtils.GetCardIdFromHeroDbId(17);
		DefLoader.Get().LoadFullDef(cardIdFromHeroDbId, new DefLoader.LoadDefCallback<DefLoader.DisposableFullDef>(this.OnHeroFullDefLoaded), null, null);
	}

	// Token: 0x060059FE RID: 23038 RVA: 0x001D636C File Offset: 0x001D456C
	private void OnHeroPowerActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogWarning(string.Format("RAFFrame.OnHeroActorLoaded() - FAILED to load actor \"{0}\"", assetRef));
			return;
		}
		this.m_heroPowerActor = go.GetComponent<Actor>();
		if (this.m_heroPowerActor == null)
		{
			Debug.LogWarning(string.Format("RAFFrame.OnHeroActorLoaded() - ERROR actor \"{0}\" has no Actor component", assetRef));
			return;
		}
		go.transform.parent = this.m_heroRewardChestTooltip.transform;
		go.transform.localScale = this.m_heroRewardChestTooltipHeroPowerBone.localScale;
		go.transform.localPosition = this.m_heroRewardChestTooltipHeroPowerBone.localPosition;
		this.m_heroPowerActor.SetUnlit();
		SceneUtils.SetLayer(this.m_heroPowerActor.gameObject, base.gameObject.layer, null);
		this.m_heroPowerActor.Hide();
		string heroPowerCardIdFromHero = GameUtils.GetHeroPowerCardIdFromHero(GameDbf.CardHero.GetRecord(17).CardId);
		DefLoader.Get().LoadFullDef(heroPowerCardIdFromHero, new DefLoader.LoadDefCallback<DefLoader.DisposableFullDef>(this.OnHeroPowerFullDefLoaded), null, null);
	}

	// Token: 0x060059FF RID: 23039 RVA: 0x001D646C File Offset: 0x001D466C
	private void OnHeroFullDefLoaded(string cardId, DefLoader.DisposableFullDef fullDef, object userData)
	{
		try
		{
			this.m_heroActor.SetPremium(TAG_PREMIUM.GOLDEN);
			this.m_heroActor.SetEntityDef(fullDef.EntityDef);
			this.m_heroActor.SetCardDef(fullDef.DisposableCardDef);
			this.m_heroActor.UpdateAllComponents();
			this.m_heroActor.SetUnlit();
			this.m_heroActor.Show();
		}
		finally
		{
			if (fullDef != null)
			{
				((IDisposable)fullDef).Dispose();
			}
		}
	}

	// Token: 0x06005A00 RID: 23040 RVA: 0x001D64E8 File Offset: 0x001D46E8
	private void OnHeroPowerFullDefLoaded(string cardId, DefLoader.DisposableFullDef def, object userData)
	{
		try
		{
			this.m_heroPowerActor.SetCardDef(def.DisposableCardDef);
			this.m_heroPowerActor.SetEntityDef(def.EntityDef);
			this.m_heroPowerActor.UpdateAllComponents();
			this.m_heroPowerActor.SetUnlit();
			def.CardDef.m_AlwaysRenderPremiumPortrait = false;
			this.m_heroPowerActor.UpdateMaterials();
			this.m_heroPowerActor.Show();
			base.StartCoroutine(this.UpdateHeroSkinHeroPower());
		}
		finally
		{
			if (def != null)
			{
				((IDisposable)def).Dispose();
			}
		}
	}

	// Token: 0x06005A01 RID: 23041 RVA: 0x001D657C File Offset: 0x001D477C
	private IEnumerator UpdateHeroSkinHeroPower()
	{
		while (this.m_heroActor == null)
		{
			yield return null;
		}
		while (!this.m_heroActor.HasCardDef)
		{
			yield return null;
		}
		HeroSkinHeroPower componentInChildren = this.m_heroPowerActor.gameObject.GetComponentInChildren<HeroSkinHeroPower>();
		if (componentInChildren == null)
		{
			yield break;
		}
		if (this.m_heroActor.GetEntityDef().GetCardSet() == TAG_CARD_SET.HERO_SKINS)
		{
			componentInChildren.m_Actor.AlwaysRenderPremiumPortrait = true;
		}
		else
		{
			componentInChildren.m_Actor.AlwaysRenderPremiumPortrait = false;
		}
		componentInChildren.m_Actor.UpdateMaterials();
		yield break;
	}

	// Token: 0x06005A02 RID: 23042 RVA: 0x001D2E09 File Offset: 0x001D1009
	private void OnMenuOpened()
	{
		if (this.m_shown)
		{
			this.Hide(false);
		}
	}

	// Token: 0x04004CEE RID: 19694
	private static RAFFrame s_Instance;

	// Token: 0x04004CEF RID: 19695
	public GeneralStoreHeroesContentDisplay m_heroDisplay;

	// Token: 0x04004CF0 RID: 19696
	public UIBButton m_recruitFriendsButton;

	// Token: 0x04004CF1 RID: 19697
	public HighlightState m_recruitFriendsButtonGlow;

	// Token: 0x04004CF2 RID: 19698
	public UIBButton m_infoButton;

	// Token: 0x04004CF3 RID: 19699
	public GameObject m_frame;

	// Token: 0x04004CF4 RID: 19700
	public GameObject m_heroFrame;

	// Token: 0x04004CF5 RID: 19701
	public GameObject m_progressFrame;

	// Token: 0x04004CF6 RID: 19702
	public RAFLinkFrame m_linkFrame;

	// Token: 0x04004CF7 RID: 19703
	public RAFInfo m_infoFrame;

	// Token: 0x04004CF8 RID: 19704
	public List<RAFRecruitBar> m_recruitContainerList;

	// Token: 0x04004CF9 RID: 19705
	public GameObject m_recruitCount;

	// Token: 0x04004CFA RID: 19706
	public UberText m_recruitCountText;

	// Token: 0x04004CFB RID: 19707
	public List<RAFChest> m_chestList;

	// Token: 0x04004CFC RID: 19708
	public GameObject m_heroRewardChestTooltip;

	// Token: 0x04004CFD RID: 19709
	public UberText m_heroRewardChestTooltipText;

	// Token: 0x04004CFE RID: 19710
	public Transform m_heroRewardChestTooltipHeroBone;

	// Token: 0x04004CFF RID: 19711
	public Transform m_heroRewardChestTooltipHeroPowerBone;

	// Token: 0x04004D00 RID: 19712
	public GameObject m_packRewardChestTooltip;

	// Token: 0x04004D01 RID: 19713
	public UberText m_packRewardChestTooltipText;

	// Token: 0x04004D02 RID: 19714
	public GameObject m_packRewardContainer;

	// Token: 0x04004D03 RID: 19715
	public UnopenedPack m_packReward;

	// Token: 0x04004D04 RID: 19716
	public GameObject m_totalResultLabel;

	// Token: 0x04004D05 RID: 19717
	public GameObject m_totalResult;

	// Token: 0x04004D06 RID: 19718
	public GameObject m_inputBlockerRenderer;

	// Token: 0x04004D07 RID: 19719
	private RAFChest m_heroChest;

	// Token: 0x04004D08 RID: 19720
	private Actor m_heroActor;

	// Token: 0x04004D09 RID: 19721
	private Actor m_heroPowerActor;

	// Token: 0x04004D0A RID: 19722
	private bool m_showHeroRewardChestTooltip;

	// Token: 0x04004D0B RID: 19723
	private PegUIElement m_inputBlockerPegUIElement;

	// Token: 0x04004D0C RID: 19724
	private bool m_isHeroDisplaySetup;

	// Token: 0x04004D0D RID: 19725
	private bool m_isHeroKeyArtShowing = true;

	// Token: 0x04004D0E RID: 19726
	private CollectionHeroDef m_collectionHeroDef;

	// Token: 0x04004D0F RID: 19727
	private MusicPlaylistType m_prevMusicPlaylist;

	// Token: 0x04004D10 RID: 19728
	private RAFFrame.Display m_shownDisplay;

	// Token: 0x02002143 RID: 8515
	private enum Display
	{
		// Token: 0x0400DFBB RID: 57275
		NONE,
		// Token: 0x0400DFBC RID: 57276
		HERO,
		// Token: 0x0400DFBD RID: 57277
		PROGRESS
	}
}
