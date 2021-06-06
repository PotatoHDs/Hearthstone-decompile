using System.Collections;
using Blizzard.T5.AssetManager;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusShared;
using UnityEngine;

public class NameBanner : MonoBehaviour
{
	private const float SKINNED_BANNER_MIN_SIZE = 12f;

	private const float SKINNED_MEDAL_BANNER_MIN_SIZE = 17f;

	private const float SKINNED_GAME_ICON_BANNER_MIN_SIZE = 17f;

	public GameObject m_alphaBannerSkinned;

	public GameObject m_alphaBannerBone;

	public GameObject m_medalBannerSkinned;

	public GameObject m_medalBannerBone;

	public GameObject m_modeIconBannerSkinned;

	public GameObject m_modeIconBannerBone;

	public GameObject m_alphaBanner;

	public GameObject m_alphaBannerLeft;

	public GameObject m_alphaBannerMiddle;

	public GameObject m_alphaBannerRight;

	public GameObject m_medalAlphaBanner;

	public GameObject m_medalAlphaBannerLeft;

	public GameObject m_medalAlphaBannerMiddle;

	public GameObject m_medalAlphaBannerRight;

	public bool m_canShowModeIcons;

	public bool m_isGameplayNameBanner;

	public GameModeIcon m_casualStandardGameModeIcon;

	public GameModeIcon m_casualWildGameModeIcon;

	public GameModeIcon m_arenaGameModeIcon;

	public GameModeIcon m_adventureGameModeIcon;

	public GameObject m_adventureIcon;

	public GameObject m_adventureShadow;

	public GameModeIcon m_friendlyGameModeIcon;

	public TavernBrawlGameModeIcon m_tavernBrawlGameModeIcon;

	public GameModeIcon m_heroicSessionBasedTavernBrawlIcon;

	public TavernBrawlGameModeIcon m_normalSessionBasedTavernBrawlIcon;

	public GameModeIcon m_pvpdrGameModeIcon;

	public GameObject m_nameText;

	public GameObject m_longNameText;

	public Transform m_nameBone;

	public Transform m_classBone;

	public Transform m_longNameBone;

	public Transform m_longClassBone;

	public Transform m_medalNameBone;

	public Transform m_medalClassBone;

	public Transform m_longMedalNameBone;

	public Transform m_longMedalClassBone;

	public AsyncReference m_rankedMedalWidgetReference;

	public UberText m_playerName;

	public UberText m_subtext;

	public UberText m_longPlayerName;

	public UberText m_longSubtext;

	public float FUDGE_FACTOR = 0.1915f;

	private const float MARGIN_FACTOR = 0.1562f;

	private int m_playerId;

	private Player.Side m_playerSide;

	private const float UNKNOWN_NAME_WAIT = 5f;

	private const float RANK_WAIT = 5f;

	private Transform m_nameBoneToUse;

	private Transform m_classBoneToUse;

	private UberText m_currentPlayerName;

	private UberText m_currentSubtext;

	private int m_missionId;

	private bool m_useLongName;

	private bool m_shouldCenterName = true;

	private FormatType m_formatType;

	private bool m_shouldShowRankedMedal;

	private bool m_initialized;

	private MedalInfoTranslator m_medalInfo;

	private RankedMedal m_rankedMedal;

	private RankedPlayDataModel m_rankedDataModel;

	private Widget m_medalWidget;

	private AssetHandle<Texture> m_gameModeIconTexture;

	public bool IsWaitingForMedal
	{
		get
		{
			if (m_shouldShowRankedMedal)
			{
				if (m_medalInfo != null && !(m_medalWidget == null))
				{
					return !m_rankedMedal.IsReady;
				}
				return true;
			}
			return false;
		}
	}

	private void Update()
	{
		UpdateAnchor();
	}

	private void OnDestroy()
	{
		AssetHandle.SafeDispose(ref m_gameModeIconTexture);
	}

	public void SetName(string name)
	{
		m_currentPlayerName.Text = name;
		if (m_alphaBannerSkinned != null)
		{
			AdjustSkinnedBanner();
		}
		else
		{
			AdjustBanner();
		}
	}

	private void SetMobilePositionOffset()
	{
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			float num = BaseUI.Get().m_BnetBar.HorizontalMargin * 0.1562f;
			TransformUtil.SetLocalPosX(base.gameObject, base.gameObject.transform.localPosition.x + num);
		}
	}

	private void AdjustBanner()
	{
		Vector3 vector = TransformUtil.ComputeWorldScale(m_currentPlayerName.gameObject);
		float num = FUDGE_FACTOR * vector.x * m_currentPlayerName.GetTextWorldSpaceBounds().size.x;
		float num2 = m_currentPlayerName.GetTextWorldSpaceBounds().size.x * vector.x + num;
		float x = m_alphaBannerMiddle.GetComponent<Renderer>().bounds.size.x;
		float x2 = m_currentPlayerName.GetTextBounds().size.x;
		if (m_medalAlphaBannerMiddle != null)
		{
			MeshRenderer meshRenderer = m_medalAlphaBannerMiddle.GetComponentsInChildren<MeshRenderer>(includeInactive: true)[0];
			float x3 = meshRenderer.bounds.size.x;
			if (num2 > x)
			{
				if (m_shouldShowRankedMedal)
				{
					TransformUtil.SetLocalScaleX(m_medalAlphaBannerMiddle, x2 / x3);
					TransformUtil.SetPoint(m_medalAlphaBannerRight, Anchor.LEFT, meshRenderer.gameObject, Anchor.RIGHT, new Vector3(0f, 0f, 0f));
				}
				else
				{
					TransformUtil.SetLocalScaleX(m_alphaBannerMiddle, num2 / x);
					TransformUtil.SetPoint(m_alphaBannerRight, Anchor.LEFT, m_alphaBannerMiddle, Anchor.RIGHT, new Vector3(0f - num, 0f, 0f));
				}
			}
		}
		else if (num2 > x)
		{
			TransformUtil.SetLocalScaleX(m_alphaBanner, num2 / x);
		}
	}

	private void AdjustSkinnedBanner()
	{
		bool flag = false;
		if (!m_shouldShowRankedMedal && ShouldShowGameIconBanner())
		{
			flag = true;
		}
		if (m_shouldShowRankedMedal)
		{
			float num = 0f - m_currentPlayerName.GetTextBounds().size.x - 10f;
			if (num > -17f)
			{
				num = -17f;
			}
			Vector3 localPosition = m_medalBannerBone.transform.localPosition;
			m_medalBannerBone.transform.localPosition = new Vector3(num, localPosition.y, localPosition.z);
		}
		else if (flag)
		{
			float num = 0f - m_currentPlayerName.GetTextBounds().size.x - 10f;
			if (num > -17f)
			{
				num = -17f;
			}
			Vector3 localPosition2 = m_modeIconBannerBone.transform.localPosition;
			m_modeIconBannerBone.transform.localPosition = new Vector3(num, localPosition2.y, localPosition2.z);
		}
		else
		{
			float num = 0f - m_currentPlayerName.GetTextBounds().size.x - 1f;
			if (num > -12f)
			{
				num = -12f;
			}
			Vector3 localPosition3 = m_alphaBannerBone.transform.localPosition;
			m_alphaBannerBone.transform.localPosition = new Vector3(num, localPosition3.y, localPosition3.z);
		}
	}

	public void SetSubtext(string subtext)
	{
		if (m_currentSubtext != null)
		{
			m_currentSubtext.gameObject.SetActive(value: true);
			m_currentSubtext.Text = subtext;
		}
		if (m_currentPlayerName != null)
		{
			Vector3 localPosition = ((m_classBoneToUse == null) ? m_nameBoneToUse.localPosition : m_classBoneToUse.localPosition);
			m_currentPlayerName.transform.localPosition = localPosition;
		}
	}

	public void PositionNameText(bool shouldTween)
	{
		if (m_shouldCenterName && !(m_currentPlayerName == null))
		{
			if ((bool)UniversalInputManager.UsePhoneUI || !shouldTween)
			{
				m_currentPlayerName.transform.position = m_nameBoneToUse.position;
				return;
			}
			Hashtable args = iTween.Hash("position", m_nameBoneToUse.localPosition, "isLocal", true, "time", 1f);
			iTween.MoveTo(m_currentPlayerName.gameObject, args);
		}
	}

	public void PositionNameText_Reconnect()
	{
		if (!(m_currentPlayerName == null))
		{
			m_currentPlayerName.transform.position = m_nameBoneToUse.position;
			OnSubtextFadeComplete();
		}
	}

	public void FadeOutSubtext()
	{
		if (m_currentSubtext == null)
		{
			return;
		}
		bool flag = GameUtils.IsExpansionAdventure(GameUtils.GetAdventureId(m_missionId)) && !GameUtils.IsClassChallengeMission(m_missionId);
		if (m_playerSide == Player.Side.OPPOSING && flag)
		{
			m_shouldCenterName = false;
			return;
		}
		if (m_playerSide == Player.Side.FRIENDLY && !string.IsNullOrEmpty(GameState.Get().GetGameEntity().GetAlternatePlayerName()))
		{
			if (m_adventureGameModeIcon != null)
			{
				m_adventureGameModeIcon.Show(show: false);
			}
			iTween.FadeTo(base.gameObject, 0f, 1f);
			return;
		}
		Hashtable args = iTween.Hash("alpha", 0f, "time", 1f, "oncomplete", "OnSubtextFadeComplete", "oncompletetarget", base.gameObject);
		iTween.FadeTo(m_currentSubtext.gameObject, args);
	}

	public void OnSubtextFadeComplete()
	{
		m_currentSubtext.gameObject.SetActive(value: false);
	}

	public void FadeIn()
	{
		if (m_alphaBannerSkinned != null)
		{
			iTween.FadeTo(m_alphaBannerSkinned.gameObject, 1f, 1f);
		}
		else
		{
			iTween.FadeTo(m_alphaBanner.gameObject, 1f, 1f);
		}
		iTween.FadeTo(m_currentPlayerName.gameObject, 1f, 1f);
	}

	public void Initialize(Player.Side side)
	{
		m_playerSide = side;
		m_currentPlayerName = m_playerName;
		m_currentSubtext = m_subtext;
		m_nameText.SetActive(value: true);
		m_useLongName = false;
		m_playerName.Text = string.Empty;
		m_nameBoneToUse = m_nameBone;
		if ((bool)m_longNameText)
		{
			m_longNameText.SetActive(value: false);
		}
		m_missionId = GameMgr.Get().GetMissionId();
		m_formatType = GameMgr.Get().GetFormatType();
		m_shouldShowRankedMedal = GameUtils.IsGameTypeRanked();
		m_initialized = true;
		UpdateAnchor();
		if (!GameState.Get().GetBooleanGameOption(GameEntityOption.ALLOW_NAME_BANNER_MODE_ICONS))
		{
			m_canShowModeIcons = false;
		}
		if (!m_canShowModeIcons)
		{
			m_shouldShowRankedMedal = false;
		}
		if (m_shouldShowRankedMedal)
		{
			StartCoroutine(UpdateMedalWhenReady());
			m_rankedMedalWidgetReference.RegisterReadyListener<Widget>(OnRankedMedalWidgetReady);
		}
	}

	public void Show()
	{
		StartCoroutine(UpdateName());
	}

	public Player.Side GetPlayerSide()
	{
		return m_playerSide;
	}

	public void Unload()
	{
		Object.DestroyImmediate(base.gameObject);
	}

	public void UseLongName()
	{
		m_currentPlayerName = m_longPlayerName;
		m_currentSubtext = m_longSubtext;
		m_longNameText.SetActive(value: true);
		m_nameText.SetActive(value: false);
		m_useLongName = true;
	}

	public void UpdateHeroNameBanner()
	{
		Player player = GameState.Get().GetPlayer(m_playerId);
		SetName(player.GetHero().GetName());
	}

	public void UpdatePlayerNameBanner()
	{
		Player player = GameState.Get().GetPlayer(m_playerId);
		SetName(player.GetName());
	}

	public void UpdateSubtext()
	{
		StartCoroutine(UpdateSubtextImpl());
	}

	private void UpdateAnchor()
	{
		if (!m_initialized)
		{
			return;
		}
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			if (m_playerSide == Player.Side.FRIENDLY)
			{
				OverlayUI.Get().AddGameObject(base.gameObject, CanvasAnchor.BOTTOM_RIGHT);
			}
			else
			{
				OverlayUI.Get().AddGameObject(base.gameObject, CanvasAnchor.BOTTOM_LEFT);
			}
			return;
		}
		if (m_playerSide == Player.Side.FRIENDLY)
		{
			OverlayUI.Get().AddGameObject(base.gameObject, CanvasAnchor.BOTTOM_LEFT);
		}
		else
		{
			OverlayUI.Get().AddGameObject(base.gameObject, CanvasAnchor.TOP_LEFT);
		}
		base.transform.localPosition = GameState.Get().GetGameEntity().NameBannerPosition(m_playerSide);
	}

	private Player GetPlayerForSide(Player.Side side)
	{
		foreach (Player value in GameState.Get().GetPlayerMap().Values)
		{
			if (value.GetSide() == side)
			{
				return value;
			}
		}
		return null;
	}

	private IEnumerator UpdateName()
	{
		while (GameState.Get().GetPlayerMap().Count == 0)
		{
			yield return null;
		}
		Player p = null;
		while (p == null)
		{
			p = GetPlayerForSide(m_playerSide);
			yield return null;
		}
		m_playerId = p.GetPlayerId();
		string value = p.GetName();
		if (p.IsHuman() && Options.Get().GetBool(Option.STREAMER_MODE) && !SpectatorManager.Get().IsInSpectatorMode())
		{
			value = ((!p.IsLocalUser()) ? GameStrings.Get("GAMEPLAY_MISSING_OPPONENT_NAME") : GameStrings.Get("GAMEPLAY_HIDDEN_PLAYER_NAME"));
		}
		if (p.IsLocalUser())
		{
			string alternatePlayerName = GameState.Get().GetGameEntity().GetAlternatePlayerName();
			if (!string.IsNullOrEmpty(alternatePlayerName))
			{
				value = alternatePlayerName;
			}
		}
		string nameBannerOverride = GameState.Get().GetGameEntity().GetNameBannerOverride(m_playerSide);
		if (!string.IsNullOrEmpty(nameBannerOverride))
		{
			value = nameBannerOverride;
		}
		float timeStart2 = Time.time;
		while (string.IsNullOrEmpty(value))
		{
			if (Time.time - timeStart2 >= 5f)
			{
				if (GameMgr.Get().GetReconnectType() == ReconnectType.GAMEPLAY)
				{
					string lastDisplayedPlayerName = GameMgr.Get().GetLastDisplayedPlayerName(m_playerId);
					if (!string.IsNullOrEmpty(lastDisplayedPlayerName))
					{
						value = lastDisplayedPlayerName;
					}
				}
				if (string.IsNullOrEmpty(value))
				{
					value = ((!p.IsLocalUser()) ? GameStrings.Get("GAMEPLAY_MISSING_OPPONENT_NAME") : GameStrings.Get("GAMEPLAY_HIDDEN_PLAYER_NAME"));
				}
				break;
			}
			yield return null;
			value = p.GetName();
		}
		bool flag = false;
		if (ShouldShowGameIconBanner())
		{
			flag = true;
		}
		if (!m_canShowModeIcons)
		{
			flag = false;
		}
		if (m_shouldShowRankedMedal)
		{
			m_nameBoneToUse = (m_useLongName ? m_longMedalNameBone : m_medalNameBone);
			m_classBoneToUse = (m_useLongName ? m_longMedalClassBone : m_medalClassBone);
			if (m_medalBannerSkinned == null)
			{
				if (m_medalAlphaBanner != null)
				{
					m_medalAlphaBanner.SetActive(value: true);
				}
			}
			else
			{
				m_medalBannerSkinned.SetActive(value: true);
				m_alphaBannerSkinned.SetActive(value: false);
				if (m_medalAlphaBanner != null)
				{
					m_medalAlphaBanner.SetActive(value: false);
				}
			}
			if (m_alphaBanner != null)
			{
				m_alphaBanner.SetActive(value: false);
			}
		}
		else if (flag)
		{
			m_nameBoneToUse = (m_useLongName ? m_longMedalNameBone : m_medalNameBone);
			m_classBoneToUse = (m_useLongName ? m_longMedalClassBone : m_medalClassBone);
			if (m_modeIconBannerSkinned == null)
			{
				if (m_medalAlphaBanner != null)
				{
					m_medalAlphaBanner.SetActive(value: true);
				}
			}
			else
			{
				m_modeIconBannerSkinned.SetActive(value: true);
				m_alphaBannerSkinned.SetActive(value: false);
				if (m_medalAlphaBanner != null)
				{
					m_medalAlphaBanner.SetActive(value: false);
				}
			}
			if (m_alphaBanner != null)
			{
				m_alphaBanner.SetActive(value: false);
			}
		}
		else
		{
			m_nameBoneToUse = (m_useLongName ? m_longNameBone : m_nameBone);
			m_classBoneToUse = (m_useLongName ? m_longClassBone : m_classBone);
			if (m_alphaBannerSkinned == null)
			{
				if (m_alphaBanner != null)
				{
					m_alphaBanner.SetActive(value: true);
				}
			}
			else
			{
				m_alphaBannerSkinned.SetActive(value: true);
				if (m_alphaBanner != null)
				{
					m_alphaBanner.SetActive(value: false);
				}
				m_medalBannerSkinned.SetActive(value: false);
			}
			if (m_medalAlphaBanner != null)
			{
				m_medalAlphaBanner.SetActive(value: false);
			}
			if (m_medalBannerSkinned != null)
			{
				m_medalBannerSkinned.SetActive(value: false);
			}
		}
		SetName(value);
		if (GameMgr.Get().IsTutorial() || m_isGameplayNameBanner)
		{
			SetMobilePositionOffset();
			m_shouldCenterName = true;
			PositionNameText(shouldTween: false);
			yield break;
		}
		AdventureDbId adventureId = GameUtils.GetAdventureId(m_missionId);
		if (m_shouldShowRankedMedal)
		{
			if (m_medalWidget != null)
			{
				m_medalWidget.Show();
			}
		}
		else if (m_playerSide == Player.Side.FRIENDLY && !UniversalInputManager.UsePhoneUI)
		{
			if (GameUtils.ShouldShowAdventureModeIcon())
			{
				AdventureDbfRecord record = GameDbf.Adventure.GetRecord((int)adventureId);
				if (m_adventureGameModeIcon != null)
				{
					AssetLoader.Get().LoadAsset(ref m_gameModeIconTexture, record.GameModeIcon);
					m_adventureIcon.GetComponent<MeshRenderer>().GetMaterial().SetTexture("_MainTex", m_gameModeIconTexture);
					m_adventureGameModeIcon.Show(show: true);
				}
			}
			else if (GameUtils.ShouldShowCasualModeIcon())
			{
				if (m_formatType == FormatType.FT_STANDARD)
				{
					m_casualStandardGameModeIcon.Show(show: true);
				}
				else
				{
					m_casualWildGameModeIcon.Show(show: true);
				}
			}
			else if (GameUtils.ShouldShowArenaModeIcon())
			{
				m_arenaGameModeIcon.Show(show: true);
				uint num = p.GetArenaWins();
				uint numberOfMarks = p.GetArenaLosses();
				if (p.GetGameAccountId() == BnetPresenceMgr.Get().GetMyGameAccountId())
				{
					timeStart2 = Time.time;
					while (!DraftManager.Get().CanShowWinsLosses)
					{
						yield return null;
						if (Time.time - timeStart2 >= 5f)
						{
							break;
						}
					}
					num = (uint)DraftManager.Get().GetWins();
					numberOfMarks = (uint)DraftManager.Get().GetLosses();
				}
				m_arenaGameModeIcon.SetText(num.ToString());
				m_arenaGameModeIcon.ShowXMarks(numberOfMarks);
			}
			else if (GameUtils.ShouldShowFriendlyChallengeIcon())
			{
				TavernBrawlMission tavernBrawlMission = TavernBrawlManager.Get().CurrentMission();
				if (tavernBrawlMission != null && tavernBrawlMission.missionId == GameMgr.Get().GetMissionId())
				{
					m_tavernBrawlGameModeIcon.Show(show: true);
					m_tavernBrawlGameModeIcon.ShowFriendlyChallengeBanner(showBanner: true);
				}
				else
				{
					m_friendlyGameModeIcon.Show(show: true);
					m_friendlyGameModeIcon.ShowWildVines(m_formatType == FormatType.FT_WILD);
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
					GameModeIcon obj = ((TavernBrawlManager.Get().CurrentSeasonBrawlMode == TavernBrawlMode.TB_MODE_HEROIC) ? m_heroicSessionBasedTavernBrawlIcon : m_normalSessionBasedTavernBrawlIcon);
					obj.Show(show: true);
					obj.SetText(num2.ToString());
					obj.ShowXMarks(numberOfMarks2);
				}
				else
				{
					m_tavernBrawlGameModeIcon.Show(show: true);
					m_tavernBrawlGameModeIcon.ShowFriendlyChallengeBanner(showBanner: false);
				}
			}
			else if (GameUtils.ShouldShowPvpDrModeIcon())
			{
				m_pvpdrGameModeIcon.Show(show: true);
				uint duelsWins = p.GetDuelsWins();
				uint duelsLosses = p.GetDuelsLosses();
				m_pvpdrGameModeIcon.SetText(duelsWins.ToString());
				m_pvpdrGameModeIcon.ShowXMarks(duelsLosses);
			}
		}
		yield return UpdateSubtextImpl();
		if (GameState.Get().GetGameEntity().ShouldDoAlternateMulliganIntro())
		{
			FadeOutSubtext();
			PositionNameText(shouldTween: false);
		}
	}

	private IEnumerator UpdateSubtextImpl()
	{
		AdventureModeDbId adventureModeId = GameUtils.GetAdventureModeId(m_missionId);
		AdventureDbId adventureId = GameUtils.GetAdventureId(m_missionId);
		bool flag = GameUtils.IsExpansionAdventure(adventureId) && adventureModeId != AdventureModeDbId.CLASS_CHALLENGE;
		GameEntity gameEntity = GameState.Get().GetGameEntity();
		Player player = GameState.Get().GetPlayer(m_playerId);
		if (gameEntity != null && gameEntity.GetNameBannerSubtextOverride(m_playerSide) != null)
		{
			SetSubtext(gameEntity.GetNameBannerSubtextOverride(m_playerSide));
		}
		else if (m_playerSide == Player.Side.OPPOSING && flag)
		{
			AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)adventureId, (int)adventureModeId);
			if (adventureDataRecord != null)
			{
				SetSubtext(((string)adventureDataRecord.ShortName).ToUpper());
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
				SetSubtext(GameStrings.GetClassName(player.GetHero().GetClass()).ToUpper());
			}
			else
			{
				m_currentPlayerName.transform.position = m_nameBoneToUse.position;
			}
		}
		if (GameState.Get().GetGameEntity().ShouldDoAlternateMulliganIntro())
		{
			FadeOutSubtext();
			PositionNameText(shouldTween: false);
		}
		if (GameMgr.Get().IsReconnect() && GameState.Get().IsMainPhase())
		{
			PositionNameText_Reconnect();
		}
	}

	public void UpdateMedalChange(MedalInfoTranslator medalInfo)
	{
		medalInfo.CreateOrUpdateDataModel(m_formatType, ref m_rankedDataModel, RankedMedal.DisplayMode.Default);
		if (!(m_rankedMedal == null))
		{
			if (!m_shouldShowRankedMedal || medalInfo == null || !medalInfo.IsDisplayable())
			{
				m_rankedMedal.gameObject.SetActive(value: false);
				return;
			}
			m_rankedMedal.gameObject.SetActive(value: true);
			m_rankedMedal.BindRankedPlayDataModel(m_rankedDataModel);
			m_medalWidget.Show();
		}
	}

	public void UpdatePvpDRInfo(PVPDRLobbyDataModel dataModel)
	{
		m_pvpdrGameModeIcon.SetText(dataModel.Wins.ToString());
		m_pvpdrGameModeIcon.ShowXMarks((uint)dataModel.Losses);
	}

	private bool ShouldShowGameIconBanner()
	{
		if (m_playerSide == Player.Side.FRIENDLY && !UniversalInputManager.UsePhoneUI && !GameUtils.IsPracticeMission(m_missionId))
		{
			return !GameUtils.IsTutorialMission(m_missionId);
		}
		return false;
	}

	private void OnRankedMedalWidgetReady(Widget widget)
	{
		if (!(widget == null))
		{
			widget.Hide();
			m_medalWidget = widget;
			m_rankedMedal = widget.GetComponentInChildren<RankedMedal>();
		}
	}

	private IEnumerator UpdateMedalWhenReady()
	{
		if (m_medalInfo != null)
		{
			yield break;
		}
		Player player = GetPlayerForSide(m_playerSide);
		float timeStart = Time.time;
		while (player.GetRank() == null || m_rankedMedal == null)
		{
			yield return null;
			if (Time.time - timeStart >= 5f)
			{
				break;
			}
		}
		m_medalInfo = player.GetRank();
		if (m_medalInfo == null || !m_medalInfo.IsDisplayable())
		{
			m_shouldShowRankedMedal = false;
		}
		if (m_shouldShowRankedMedal && m_playerSide == Player.Side.OPPOSING)
		{
			Player playerForSide = GetPlayerForSide(Player.Side.FRIENDLY);
			MedalInfoTranslator medalInfoTranslator = null;
			if (playerForSide != null)
			{
				medalInfoTranslator = playerForSide.GetRank();
			}
			if (playerForSide == null || medalInfoTranslator == null || !medalInfoTranslator.GetCurrentMedal(m_formatType).RankConfig.ShowOpponentRankInGame)
			{
				m_shouldShowRankedMedal = false;
			}
		}
		if (m_shouldShowRankedMedal)
		{
			UpdateMedalChange(m_medalInfo);
		}
	}
}
