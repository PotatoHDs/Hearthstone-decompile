using System;
using System.Collections;
using Blizzard.T5.AssetManager;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusShared;
using UnityEngine;

// Token: 0x02000330 RID: 816
public class NameBanner : MonoBehaviour
{
	// Token: 0x06002E56 RID: 11862 RVA: 0x000EC407 File Offset: 0x000EA607
	private void Update()
	{
		this.UpdateAnchor();
	}

	// Token: 0x06002E57 RID: 11863 RVA: 0x000EC40F File Offset: 0x000EA60F
	private void OnDestroy()
	{
		AssetHandle.SafeDispose<Texture>(ref this.m_gameModeIconTexture);
	}

	// Token: 0x17000508 RID: 1288
	// (get) Token: 0x06002E58 RID: 11864 RVA: 0x000EC41C File Offset: 0x000EA61C
	public bool IsWaitingForMedal
	{
		get
		{
			return this.m_shouldShowRankedMedal && (this.m_medalInfo == null || this.m_medalWidget == null || !this.m_rankedMedal.IsReady);
		}
	}

	// Token: 0x06002E59 RID: 11865 RVA: 0x000EC44E File Offset: 0x000EA64E
	public void SetName(string name)
	{
		this.m_currentPlayerName.Text = name;
		if (this.m_alphaBannerSkinned != null)
		{
			this.AdjustSkinnedBanner();
			return;
		}
		this.AdjustBanner();
	}

	// Token: 0x06002E5A RID: 11866 RVA: 0x000EC478 File Offset: 0x000EA678
	private void SetMobilePositionOffset()
	{
		if (!UniversalInputManager.UsePhoneUI)
		{
			return;
		}
		float num = BaseUI.Get().m_BnetBar.HorizontalMargin * 0.1562f;
		TransformUtil.SetLocalPosX(base.gameObject, base.gameObject.transform.localPosition.x + num);
	}

	// Token: 0x06002E5B RID: 11867 RVA: 0x000EC4CC File Offset: 0x000EA6CC
	private void AdjustBanner()
	{
		Vector3 vector = TransformUtil.ComputeWorldScale(this.m_currentPlayerName.gameObject);
		float num = this.FUDGE_FACTOR * vector.x * this.m_currentPlayerName.GetTextWorldSpaceBounds().size.x;
		float num2 = this.m_currentPlayerName.GetTextWorldSpaceBounds().size.x * vector.x + num;
		float x = this.m_alphaBannerMiddle.GetComponent<Renderer>().bounds.size.x;
		float x2 = this.m_currentPlayerName.GetTextBounds().size.x;
		if (this.m_medalAlphaBannerMiddle != null)
		{
			MeshRenderer meshRenderer = this.m_medalAlphaBannerMiddle.GetComponentsInChildren<MeshRenderer>(true)[0];
			float x3 = meshRenderer.bounds.size.x;
			if (num2 > x)
			{
				if (this.m_shouldShowRankedMedal)
				{
					TransformUtil.SetLocalScaleX(this.m_medalAlphaBannerMiddle, x2 / x3);
					TransformUtil.SetPoint(this.m_medalAlphaBannerRight, Anchor.LEFT, meshRenderer.gameObject, Anchor.RIGHT, new Vector3(0f, 0f, 0f));
					return;
				}
				TransformUtil.SetLocalScaleX(this.m_alphaBannerMiddle, num2 / x);
				TransformUtil.SetPoint(this.m_alphaBannerRight, Anchor.LEFT, this.m_alphaBannerMiddle, Anchor.RIGHT, new Vector3(-num, 0f, 0f));
				return;
			}
		}
		else if (num2 > x)
		{
			TransformUtil.SetLocalScaleX(this.m_alphaBanner, num2 / x);
		}
	}

	// Token: 0x06002E5C RID: 11868 RVA: 0x000EC634 File Offset: 0x000EA834
	private void AdjustSkinnedBanner()
	{
		bool flag = false;
		if (!this.m_shouldShowRankedMedal && this.ShouldShowGameIconBanner())
		{
			flag = true;
		}
		float num;
		if (this.m_shouldShowRankedMedal)
		{
			num = -this.m_currentPlayerName.GetTextBounds().size.x - 10f;
			if (num > -17f)
			{
				num = -17f;
			}
			Vector3 localPosition = this.m_medalBannerBone.transform.localPosition;
			this.m_medalBannerBone.transform.localPosition = new Vector3(num, localPosition.y, localPosition.z);
			return;
		}
		if (flag)
		{
			num = -this.m_currentPlayerName.GetTextBounds().size.x - 10f;
			if (num > -17f)
			{
				num = -17f;
			}
			Vector3 localPosition2 = this.m_modeIconBannerBone.transform.localPosition;
			this.m_modeIconBannerBone.transform.localPosition = new Vector3(num, localPosition2.y, localPosition2.z);
			return;
		}
		num = -this.m_currentPlayerName.GetTextBounds().size.x - 1f;
		if (num > -12f)
		{
			num = -12f;
		}
		Vector3 localPosition3 = this.m_alphaBannerBone.transform.localPosition;
		this.m_alphaBannerBone.transform.localPosition = new Vector3(num, localPosition3.y, localPosition3.z);
	}

	// Token: 0x06002E5D RID: 11869 RVA: 0x000EC78C File Offset: 0x000EA98C
	public void SetSubtext(string subtext)
	{
		if (this.m_currentSubtext != null)
		{
			this.m_currentSubtext.gameObject.SetActive(true);
			this.m_currentSubtext.Text = subtext;
		}
		if (this.m_currentPlayerName != null)
		{
			Vector3 localPosition = (this.m_classBoneToUse == null) ? this.m_nameBoneToUse.localPosition : this.m_classBoneToUse.localPosition;
			this.m_currentPlayerName.transform.localPosition = localPosition;
		}
	}

	// Token: 0x06002E5E RID: 11870 RVA: 0x000EC80C File Offset: 0x000EAA0C
	public void PositionNameText(bool shouldTween)
	{
		if (!this.m_shouldCenterName || this.m_currentPlayerName == null)
		{
			return;
		}
		if (UniversalInputManager.UsePhoneUI || !shouldTween)
		{
			this.m_currentPlayerName.transform.position = this.m_nameBoneToUse.position;
			return;
		}
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			this.m_nameBoneToUse.localPosition,
			"isLocal",
			true,
			"time",
			1f
		});
		iTween.MoveTo(this.m_currentPlayerName.gameObject, args);
	}

	// Token: 0x06002E5F RID: 11871 RVA: 0x000EC8B9 File Offset: 0x000EAAB9
	public void PositionNameText_Reconnect()
	{
		if (this.m_currentPlayerName == null)
		{
			return;
		}
		this.m_currentPlayerName.transform.position = this.m_nameBoneToUse.position;
		this.OnSubtextFadeComplete();
	}

	// Token: 0x06002E60 RID: 11872 RVA: 0x000EC8EC File Offset: 0x000EAAEC
	public void FadeOutSubtext()
	{
		if (this.m_currentSubtext == null)
		{
			return;
		}
		bool flag = GameUtils.IsExpansionAdventure(GameUtils.GetAdventureId(this.m_missionId)) && !GameUtils.IsClassChallengeMission(this.m_missionId);
		if (this.m_playerSide == Player.Side.OPPOSING && flag)
		{
			this.m_shouldCenterName = false;
			return;
		}
		if (this.m_playerSide == Player.Side.FRIENDLY && !string.IsNullOrEmpty(GameState.Get().GetGameEntity().GetAlternatePlayerName()))
		{
			if (this.m_adventureGameModeIcon != null)
			{
				this.m_adventureGameModeIcon.Show(false);
			}
			iTween.FadeTo(base.gameObject, 0f, 1f);
			return;
		}
		Hashtable args = iTween.Hash(new object[]
		{
			"alpha",
			0f,
			"time",
			1f,
			"oncomplete",
			"OnSubtextFadeComplete",
			"oncompletetarget",
			base.gameObject
		});
		iTween.FadeTo(this.m_currentSubtext.gameObject, args);
	}

	// Token: 0x06002E61 RID: 11873 RVA: 0x000EC9F8 File Offset: 0x000EABF8
	public void OnSubtextFadeComplete()
	{
		this.m_currentSubtext.gameObject.SetActive(false);
	}

	// Token: 0x06002E62 RID: 11874 RVA: 0x000ECA0C File Offset: 0x000EAC0C
	public void FadeIn()
	{
		if (this.m_alphaBannerSkinned != null)
		{
			iTween.FadeTo(this.m_alphaBannerSkinned.gameObject, 1f, 1f);
		}
		else
		{
			iTween.FadeTo(this.m_alphaBanner.gameObject, 1f, 1f);
		}
		iTween.FadeTo(this.m_currentPlayerName.gameObject, 1f, 1f);
	}

	// Token: 0x06002E63 RID: 11875 RVA: 0x000ECA78 File Offset: 0x000EAC78
	public void Initialize(Player.Side side)
	{
		this.m_playerSide = side;
		this.m_currentPlayerName = this.m_playerName;
		this.m_currentSubtext = this.m_subtext;
		this.m_nameText.SetActive(true);
		this.m_useLongName = false;
		this.m_playerName.Text = string.Empty;
		this.m_nameBoneToUse = this.m_nameBone;
		if (this.m_longNameText)
		{
			this.m_longNameText.SetActive(false);
		}
		this.m_missionId = GameMgr.Get().GetMissionId();
		this.m_formatType = GameMgr.Get().GetFormatType();
		this.m_shouldShowRankedMedal = GameUtils.IsGameTypeRanked();
		this.m_initialized = true;
		this.UpdateAnchor();
		if (!GameState.Get().GetBooleanGameOption(GameEntityOption.ALLOW_NAME_BANNER_MODE_ICONS))
		{
			this.m_canShowModeIcons = false;
		}
		if (!this.m_canShowModeIcons)
		{
			this.m_shouldShowRankedMedal = false;
		}
		if (this.m_shouldShowRankedMedal)
		{
			base.StartCoroutine(this.UpdateMedalWhenReady());
			this.m_rankedMedalWidgetReference.RegisterReadyListener<Widget>(new Action<Widget>(this.OnRankedMedalWidgetReady));
		}
	}

	// Token: 0x06002E64 RID: 11876 RVA: 0x000ECB74 File Offset: 0x000EAD74
	public void Show()
	{
		base.StartCoroutine(this.UpdateName());
	}

	// Token: 0x06002E65 RID: 11877 RVA: 0x000ECB83 File Offset: 0x000EAD83
	public Player.Side GetPlayerSide()
	{
		return this.m_playerSide;
	}

	// Token: 0x06002E66 RID: 11878 RVA: 0x000ECB8B File Offset: 0x000EAD8B
	public void Unload()
	{
		UnityEngine.Object.DestroyImmediate(base.gameObject);
	}

	// Token: 0x06002E67 RID: 11879 RVA: 0x000ECB98 File Offset: 0x000EAD98
	public void UseLongName()
	{
		this.m_currentPlayerName = this.m_longPlayerName;
		this.m_currentSubtext = this.m_longSubtext;
		this.m_longNameText.SetActive(true);
		this.m_nameText.SetActive(false);
		this.m_useLongName = true;
	}

	// Token: 0x06002E68 RID: 11880 RVA: 0x000ECBD4 File Offset: 0x000EADD4
	public void UpdateHeroNameBanner()
	{
		Player player = GameState.Get().GetPlayer(this.m_playerId);
		this.SetName(player.GetHero().GetName());
	}

	// Token: 0x06002E69 RID: 11881 RVA: 0x000ECC04 File Offset: 0x000EAE04
	public void UpdatePlayerNameBanner()
	{
		Player player = GameState.Get().GetPlayer(this.m_playerId);
		this.SetName(player.GetName());
	}

	// Token: 0x06002E6A RID: 11882 RVA: 0x000ECC2E File Offset: 0x000EAE2E
	public void UpdateSubtext()
	{
		base.StartCoroutine(this.UpdateSubtextImpl());
	}

	// Token: 0x06002E6B RID: 11883 RVA: 0x000ECC40 File Offset: 0x000EAE40
	private void UpdateAnchor()
	{
		if (!this.m_initialized)
		{
			return;
		}
		if (!UniversalInputManager.UsePhoneUI)
		{
			if (this.m_playerSide == Player.Side.FRIENDLY)
			{
				OverlayUI.Get().AddGameObject(base.gameObject, CanvasAnchor.BOTTOM_LEFT, false, CanvasScaleMode.HEIGHT);
			}
			else
			{
				OverlayUI.Get().AddGameObject(base.gameObject, CanvasAnchor.TOP_LEFT, false, CanvasScaleMode.HEIGHT);
			}
			base.transform.localPosition = GameState.Get().GetGameEntity().NameBannerPosition(this.m_playerSide);
			return;
		}
		if (this.m_playerSide == Player.Side.FRIENDLY)
		{
			OverlayUI.Get().AddGameObject(base.gameObject, CanvasAnchor.BOTTOM_RIGHT, false, CanvasScaleMode.HEIGHT);
			return;
		}
		OverlayUI.Get().AddGameObject(base.gameObject, CanvasAnchor.BOTTOM_LEFT, false, CanvasScaleMode.HEIGHT);
	}

	// Token: 0x06002E6C RID: 11884 RVA: 0x000ECCE4 File Offset: 0x000EAEE4
	private Player GetPlayerForSide(Player.Side side)
	{
		foreach (Player player in GameState.Get().GetPlayerMap().Values)
		{
			if (player.GetSide() == side)
			{
				return player;
			}
		}
		return null;
	}

	// Token: 0x06002E6D RID: 11885 RVA: 0x000ECD4C File Offset: 0x000EAF4C
	private IEnumerator UpdateName()
	{
		while (GameState.Get().GetPlayerMap().Count == 0)
		{
			yield return null;
		}
		Player p = null;
		while (p == null)
		{
			p = this.GetPlayerForSide(this.m_playerSide);
			yield return null;
		}
		this.m_playerId = p.GetPlayerId();
		string text = p.GetName();
		if (p.IsHuman() && Options.Get().GetBool(Option.STREAMER_MODE) && !SpectatorManager.Get().IsInSpectatorMode())
		{
			if (p.IsLocalUser())
			{
				text = GameStrings.Get("GAMEPLAY_HIDDEN_PLAYER_NAME");
			}
			else
			{
				text = GameStrings.Get("GAMEPLAY_MISSING_OPPONENT_NAME");
			}
		}
		if (p.IsLocalUser())
		{
			string alternatePlayerName = GameState.Get().GetGameEntity().GetAlternatePlayerName();
			if (!string.IsNullOrEmpty(alternatePlayerName))
			{
				text = alternatePlayerName;
			}
		}
		string nameBannerOverride = GameState.Get().GetGameEntity().GetNameBannerOverride(this.m_playerSide);
		if (!string.IsNullOrEmpty(nameBannerOverride))
		{
			text = nameBannerOverride;
		}
		float timeStart = Time.time;
		while (string.IsNullOrEmpty(text))
		{
			if (Time.time - timeStart >= 5f)
			{
				if (GameMgr.Get().GetReconnectType() == ReconnectType.GAMEPLAY)
				{
					string lastDisplayedPlayerName = GameMgr.Get().GetLastDisplayedPlayerName(this.m_playerId);
					if (!string.IsNullOrEmpty(lastDisplayedPlayerName))
					{
						text = lastDisplayedPlayerName;
					}
				}
				if (!string.IsNullOrEmpty(text))
				{
					break;
				}
				if (p.IsLocalUser())
				{
					text = GameStrings.Get("GAMEPLAY_HIDDEN_PLAYER_NAME");
					break;
				}
				text = GameStrings.Get("GAMEPLAY_MISSING_OPPONENT_NAME");
				break;
			}
			else
			{
				yield return null;
				text = p.GetName();
			}
		}
		bool flag = false;
		if (this.ShouldShowGameIconBanner())
		{
			flag = true;
		}
		if (!this.m_canShowModeIcons)
		{
			flag = false;
		}
		if (this.m_shouldShowRankedMedal)
		{
			this.m_nameBoneToUse = (this.m_useLongName ? this.m_longMedalNameBone : this.m_medalNameBone);
			this.m_classBoneToUse = (this.m_useLongName ? this.m_longMedalClassBone : this.m_medalClassBone);
			if (this.m_medalBannerSkinned == null)
			{
				if (this.m_medalAlphaBanner != null)
				{
					this.m_medalAlphaBanner.SetActive(true);
				}
			}
			else
			{
				this.m_medalBannerSkinned.SetActive(true);
				this.m_alphaBannerSkinned.SetActive(false);
				if (this.m_medalAlphaBanner != null)
				{
					this.m_medalAlphaBanner.SetActive(false);
				}
			}
			if (this.m_alphaBanner != null)
			{
				this.m_alphaBanner.SetActive(false);
			}
		}
		else if (flag)
		{
			this.m_nameBoneToUse = (this.m_useLongName ? this.m_longMedalNameBone : this.m_medalNameBone);
			this.m_classBoneToUse = (this.m_useLongName ? this.m_longMedalClassBone : this.m_medalClassBone);
			if (this.m_modeIconBannerSkinned == null)
			{
				if (this.m_medalAlphaBanner != null)
				{
					this.m_medalAlphaBanner.SetActive(true);
				}
			}
			else
			{
				this.m_modeIconBannerSkinned.SetActive(true);
				this.m_alphaBannerSkinned.SetActive(false);
				if (this.m_medalAlphaBanner != null)
				{
					this.m_medalAlphaBanner.SetActive(false);
				}
			}
			if (this.m_alphaBanner != null)
			{
				this.m_alphaBanner.SetActive(false);
			}
		}
		else
		{
			this.m_nameBoneToUse = (this.m_useLongName ? this.m_longNameBone : this.m_nameBone);
			this.m_classBoneToUse = (this.m_useLongName ? this.m_longClassBone : this.m_classBone);
			if (this.m_alphaBannerSkinned == null)
			{
				if (this.m_alphaBanner != null)
				{
					this.m_alphaBanner.SetActive(true);
				}
			}
			else
			{
				this.m_alphaBannerSkinned.SetActive(true);
				if (this.m_alphaBanner != null)
				{
					this.m_alphaBanner.SetActive(false);
				}
				this.m_medalBannerSkinned.SetActive(false);
			}
			if (this.m_medalAlphaBanner != null)
			{
				this.m_medalAlphaBanner.SetActive(false);
			}
			if (this.m_medalBannerSkinned != null)
			{
				this.m_medalBannerSkinned.SetActive(false);
			}
		}
		this.SetName(text);
		if (GameMgr.Get().IsTutorial() || this.m_isGameplayNameBanner)
		{
			this.SetMobilePositionOffset();
			this.m_shouldCenterName = true;
			this.PositionNameText(false);
			yield break;
		}
		AdventureDbId adventureId = GameUtils.GetAdventureId(this.m_missionId);
		if (this.m_shouldShowRankedMedal)
		{
			if (this.m_medalWidget != null)
			{
				this.m_medalWidget.Show();
			}
		}
		else if (this.m_playerSide == Player.Side.FRIENDLY && !UniversalInputManager.UsePhoneUI)
		{
			if (GameUtils.ShouldShowAdventureModeIcon())
			{
				AdventureDbfRecord record = GameDbf.Adventure.GetRecord((int)adventureId);
				if (this.m_adventureGameModeIcon != null)
				{
					AssetLoader.Get().LoadAsset<Texture>(ref this.m_gameModeIconTexture, record.GameModeIcon, AssetLoadingOptions.None);
					this.m_adventureIcon.GetComponent<MeshRenderer>().GetMaterial().SetTexture("_MainTex", this.m_gameModeIconTexture);
					this.m_adventureGameModeIcon.Show(true);
				}
			}
			else if (GameUtils.ShouldShowCasualModeIcon())
			{
				if (this.m_formatType == FormatType.FT_STANDARD)
				{
					this.m_casualStandardGameModeIcon.Show(true);
				}
				else
				{
					this.m_casualWildGameModeIcon.Show(true);
				}
			}
			else if (GameUtils.ShouldShowArenaModeIcon())
			{
				this.m_arenaGameModeIcon.Show(true);
				uint num = p.GetArenaWins();
				uint numberOfMarks = p.GetArenaLosses();
				if (p.GetGameAccountId() == BnetPresenceMgr.Get().GetMyGameAccountId())
				{
					timeStart = Time.time;
					while (!DraftManager.Get().CanShowWinsLosses)
					{
						yield return null;
						if (Time.time - timeStart >= 5f)
						{
							break;
						}
					}
					num = (uint)DraftManager.Get().GetWins();
					numberOfMarks = (uint)DraftManager.Get().GetLosses();
				}
				this.m_arenaGameModeIcon.SetText(num.ToString());
				this.m_arenaGameModeIcon.ShowXMarks(numberOfMarks);
			}
			else if (GameUtils.ShouldShowFriendlyChallengeIcon())
			{
				TavernBrawlMission tavernBrawlMission = TavernBrawlManager.Get().CurrentMission();
				if (tavernBrawlMission != null && tavernBrawlMission.missionId == GameMgr.Get().GetMissionId())
				{
					this.m_tavernBrawlGameModeIcon.Show(true);
					this.m_tavernBrawlGameModeIcon.ShowFriendlyChallengeBanner(true);
				}
				else
				{
					this.m_friendlyGameModeIcon.Show(true);
					this.m_friendlyGameModeIcon.ShowWildVines(this.m_formatType == FormatType.FT_WILD);
				}
			}
			else if (GameUtils.ShouldShowTavernBrawlModeIcon())
			{
				if (TavernBrawlManager.Get().IsCurrentSeasonSessionBased)
				{
					uint num2 = p.GetTavernBrawlWins();
					uint numberOfMarks2 = p.GetTavernBrawlLosses();
					if (p.GetGameAccountId() == BnetPresenceMgr.Get().GetMyGameAccountId())
					{
						num2 = (uint)TavernBrawlManager.Get().GamesWon;
						numberOfMarks2 = (uint)TavernBrawlManager.Get().GamesLost;
					}
					GameModeIcon gameModeIcon = (TavernBrawlManager.Get().CurrentSeasonBrawlMode == TavernBrawlMode.TB_MODE_HEROIC) ? this.m_heroicSessionBasedTavernBrawlIcon : this.m_normalSessionBasedTavernBrawlIcon;
					gameModeIcon.Show(true);
					gameModeIcon.SetText(num2.ToString());
					gameModeIcon.ShowXMarks(numberOfMarks2);
				}
				else
				{
					this.m_tavernBrawlGameModeIcon.Show(true);
					this.m_tavernBrawlGameModeIcon.ShowFriendlyChallengeBanner(false);
				}
			}
			else if (GameUtils.ShouldShowPvpDrModeIcon())
			{
				this.m_pvpdrGameModeIcon.Show(true);
				uint duelsWins = p.GetDuelsWins();
				uint duelsLosses = p.GetDuelsLosses();
				this.m_pvpdrGameModeIcon.SetText(duelsWins.ToString());
				this.m_pvpdrGameModeIcon.ShowXMarks(duelsLosses);
			}
		}
		yield return this.UpdateSubtextImpl();
		if (GameState.Get().GetGameEntity().ShouldDoAlternateMulliganIntro())
		{
			this.FadeOutSubtext();
			this.PositionNameText(false);
		}
		yield break;
	}

	// Token: 0x06002E6E RID: 11886 RVA: 0x000ECD5B File Offset: 0x000EAF5B
	private IEnumerator UpdateSubtextImpl()
	{
		AdventureModeDbId adventureModeId = GameUtils.GetAdventureModeId(this.m_missionId);
		AdventureDbId adventureId = GameUtils.GetAdventureId(this.m_missionId);
		bool flag = GameUtils.IsExpansionAdventure(adventureId) && adventureModeId != AdventureModeDbId.CLASS_CHALLENGE;
		GameEntity gameEntity = GameState.Get().GetGameEntity();
		Player player = GameState.Get().GetPlayer(this.m_playerId);
		if (gameEntity != null && gameEntity.GetNameBannerSubtextOverride(this.m_playerSide) != null)
		{
			this.SetSubtext(gameEntity.GetNameBannerSubtextOverride(this.m_playerSide));
		}
		else if (this.m_playerSide == Player.Side.OPPOSING && flag)
		{
			AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)adventureId, (int)adventureModeId);
			if (adventureDataRecord != null)
			{
				this.SetSubtext(adventureDataRecord.ShortName.ToUpper());
			}
		}
		else
		{
			while (player.GetHero().GetClass() == TAG_CLASS.INVALID)
			{
				yield return null;
			}
			if (player.GetHero().GetClass() != TAG_CLASS.NEUTRAL)
			{
				this.SetSubtext(GameStrings.GetClassName(player.GetHero().GetClass()).ToUpper());
			}
			else
			{
				this.m_currentPlayerName.transform.position = this.m_nameBoneToUse.position;
			}
		}
		if (GameState.Get().GetGameEntity().ShouldDoAlternateMulliganIntro())
		{
			this.FadeOutSubtext();
			this.PositionNameText(false);
		}
		if (GameMgr.Get().IsReconnect() && GameState.Get().IsMainPhase())
		{
			this.PositionNameText_Reconnect();
		}
		yield break;
	}

	// Token: 0x06002E6F RID: 11887 RVA: 0x000ECD6C File Offset: 0x000EAF6C
	public void UpdateMedalChange(MedalInfoTranslator medalInfo)
	{
		medalInfo.CreateOrUpdateDataModel(this.m_formatType, ref this.m_rankedDataModel, RankedMedal.DisplayMode.Default, false, false, null);
		if (this.m_rankedMedal == null)
		{
			return;
		}
		if (!this.m_shouldShowRankedMedal || medalInfo == null || !medalInfo.IsDisplayable())
		{
			this.m_rankedMedal.gameObject.SetActive(false);
			return;
		}
		this.m_rankedMedal.gameObject.SetActive(true);
		this.m_rankedMedal.BindRankedPlayDataModel(this.m_rankedDataModel);
		this.m_medalWidget.Show();
	}

	// Token: 0x06002E70 RID: 11888 RVA: 0x000ECDF0 File Offset: 0x000EAFF0
	public void UpdatePvpDRInfo(PVPDRLobbyDataModel dataModel)
	{
		this.m_pvpdrGameModeIcon.SetText(dataModel.Wins.ToString());
		this.m_pvpdrGameModeIcon.ShowXMarks((uint)dataModel.Losses);
	}

	// Token: 0x06002E71 RID: 11889 RVA: 0x000ECE27 File Offset: 0x000EB027
	private bool ShouldShowGameIconBanner()
	{
		return this.m_playerSide == Player.Side.FRIENDLY && !UniversalInputManager.UsePhoneUI && !GameUtils.IsPracticeMission(this.m_missionId) && !GameUtils.IsTutorialMission(this.m_missionId);
	}

	// Token: 0x06002E72 RID: 11890 RVA: 0x000ECE5B File Offset: 0x000EB05B
	private void OnRankedMedalWidgetReady(Widget widget)
	{
		if (widget == null)
		{
			return;
		}
		widget.Hide();
		this.m_medalWidget = widget;
		this.m_rankedMedal = widget.GetComponentInChildren<RankedMedal>();
	}

	// Token: 0x06002E73 RID: 11891 RVA: 0x000ECE80 File Offset: 0x000EB080
	private IEnumerator UpdateMedalWhenReady()
	{
		if (this.m_medalInfo != null)
		{
			yield break;
		}
		Player player = this.GetPlayerForSide(this.m_playerSide);
		float timeStart = Time.time;
		while (player.GetRank() == null || this.m_rankedMedal == null)
		{
			yield return null;
			if (Time.time - timeStart >= 5f)
			{
				break;
			}
		}
		this.m_medalInfo = player.GetRank();
		if (this.m_medalInfo == null || !this.m_medalInfo.IsDisplayable())
		{
			this.m_shouldShowRankedMedal = false;
		}
		if (this.m_shouldShowRankedMedal && this.m_playerSide == Player.Side.OPPOSING)
		{
			Player playerForSide = this.GetPlayerForSide(Player.Side.FRIENDLY);
			MedalInfoTranslator medalInfoTranslator = null;
			if (playerForSide != null)
			{
				medalInfoTranslator = playerForSide.GetRank();
			}
			if (playerForSide == null || medalInfoTranslator == null || !medalInfoTranslator.GetCurrentMedal(this.m_formatType).RankConfig.ShowOpponentRankInGame)
			{
				this.m_shouldShowRankedMedal = false;
			}
		}
		if (this.m_shouldShowRankedMedal)
		{
			this.UpdateMedalChange(this.m_medalInfo);
		}
		yield break;
	}

	// Token: 0x040019C1 RID: 6593
	private const float SKINNED_BANNER_MIN_SIZE = 12f;

	// Token: 0x040019C2 RID: 6594
	private const float SKINNED_MEDAL_BANNER_MIN_SIZE = 17f;

	// Token: 0x040019C3 RID: 6595
	private const float SKINNED_GAME_ICON_BANNER_MIN_SIZE = 17f;

	// Token: 0x040019C4 RID: 6596
	public GameObject m_alphaBannerSkinned;

	// Token: 0x040019C5 RID: 6597
	public GameObject m_alphaBannerBone;

	// Token: 0x040019C6 RID: 6598
	public GameObject m_medalBannerSkinned;

	// Token: 0x040019C7 RID: 6599
	public GameObject m_medalBannerBone;

	// Token: 0x040019C8 RID: 6600
	public GameObject m_modeIconBannerSkinned;

	// Token: 0x040019C9 RID: 6601
	public GameObject m_modeIconBannerBone;

	// Token: 0x040019CA RID: 6602
	public GameObject m_alphaBanner;

	// Token: 0x040019CB RID: 6603
	public GameObject m_alphaBannerLeft;

	// Token: 0x040019CC RID: 6604
	public GameObject m_alphaBannerMiddle;

	// Token: 0x040019CD RID: 6605
	public GameObject m_alphaBannerRight;

	// Token: 0x040019CE RID: 6606
	public GameObject m_medalAlphaBanner;

	// Token: 0x040019CF RID: 6607
	public GameObject m_medalAlphaBannerLeft;

	// Token: 0x040019D0 RID: 6608
	public GameObject m_medalAlphaBannerMiddle;

	// Token: 0x040019D1 RID: 6609
	public GameObject m_medalAlphaBannerRight;

	// Token: 0x040019D2 RID: 6610
	public bool m_canShowModeIcons;

	// Token: 0x040019D3 RID: 6611
	public bool m_isGameplayNameBanner;

	// Token: 0x040019D4 RID: 6612
	public GameModeIcon m_casualStandardGameModeIcon;

	// Token: 0x040019D5 RID: 6613
	public GameModeIcon m_casualWildGameModeIcon;

	// Token: 0x040019D6 RID: 6614
	public GameModeIcon m_arenaGameModeIcon;

	// Token: 0x040019D7 RID: 6615
	public GameModeIcon m_adventureGameModeIcon;

	// Token: 0x040019D8 RID: 6616
	public GameObject m_adventureIcon;

	// Token: 0x040019D9 RID: 6617
	public GameObject m_adventureShadow;

	// Token: 0x040019DA RID: 6618
	public GameModeIcon m_friendlyGameModeIcon;

	// Token: 0x040019DB RID: 6619
	public TavernBrawlGameModeIcon m_tavernBrawlGameModeIcon;

	// Token: 0x040019DC RID: 6620
	public GameModeIcon m_heroicSessionBasedTavernBrawlIcon;

	// Token: 0x040019DD RID: 6621
	public TavernBrawlGameModeIcon m_normalSessionBasedTavernBrawlIcon;

	// Token: 0x040019DE RID: 6622
	public GameModeIcon m_pvpdrGameModeIcon;

	// Token: 0x040019DF RID: 6623
	public GameObject m_nameText;

	// Token: 0x040019E0 RID: 6624
	public GameObject m_longNameText;

	// Token: 0x040019E1 RID: 6625
	public Transform m_nameBone;

	// Token: 0x040019E2 RID: 6626
	public Transform m_classBone;

	// Token: 0x040019E3 RID: 6627
	public Transform m_longNameBone;

	// Token: 0x040019E4 RID: 6628
	public Transform m_longClassBone;

	// Token: 0x040019E5 RID: 6629
	public Transform m_medalNameBone;

	// Token: 0x040019E6 RID: 6630
	public Transform m_medalClassBone;

	// Token: 0x040019E7 RID: 6631
	public Transform m_longMedalNameBone;

	// Token: 0x040019E8 RID: 6632
	public Transform m_longMedalClassBone;

	// Token: 0x040019E9 RID: 6633
	public AsyncReference m_rankedMedalWidgetReference;

	// Token: 0x040019EA RID: 6634
	public UberText m_playerName;

	// Token: 0x040019EB RID: 6635
	public UberText m_subtext;

	// Token: 0x040019EC RID: 6636
	public UberText m_longPlayerName;

	// Token: 0x040019ED RID: 6637
	public UberText m_longSubtext;

	// Token: 0x040019EE RID: 6638
	public float FUDGE_FACTOR = 0.1915f;

	// Token: 0x040019EF RID: 6639
	private const float MARGIN_FACTOR = 0.1562f;

	// Token: 0x040019F0 RID: 6640
	private int m_playerId;

	// Token: 0x040019F1 RID: 6641
	private Player.Side m_playerSide;

	// Token: 0x040019F2 RID: 6642
	private const float UNKNOWN_NAME_WAIT = 5f;

	// Token: 0x040019F3 RID: 6643
	private const float RANK_WAIT = 5f;

	// Token: 0x040019F4 RID: 6644
	private Transform m_nameBoneToUse;

	// Token: 0x040019F5 RID: 6645
	private Transform m_classBoneToUse;

	// Token: 0x040019F6 RID: 6646
	private UberText m_currentPlayerName;

	// Token: 0x040019F7 RID: 6647
	private UberText m_currentSubtext;

	// Token: 0x040019F8 RID: 6648
	private int m_missionId;

	// Token: 0x040019F9 RID: 6649
	private bool m_useLongName;

	// Token: 0x040019FA RID: 6650
	private bool m_shouldCenterName = true;

	// Token: 0x040019FB RID: 6651
	private FormatType m_formatType;

	// Token: 0x040019FC RID: 6652
	private bool m_shouldShowRankedMedal;

	// Token: 0x040019FD RID: 6653
	private bool m_initialized;

	// Token: 0x040019FE RID: 6654
	private MedalInfoTranslator m_medalInfo;

	// Token: 0x040019FF RID: 6655
	private RankedMedal m_rankedMedal;

	// Token: 0x04001A00 RID: 6656
	private RankedPlayDataModel m_rankedDataModel;

	// Token: 0x04001A01 RID: 6657
	private Widget m_medalWidget;

	// Token: 0x04001A02 RID: 6658
	private AssetHandle<Texture> m_gameModeIconTexture;
}
