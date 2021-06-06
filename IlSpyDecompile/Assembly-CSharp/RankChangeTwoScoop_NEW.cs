using System;
using System.Collections;
using Assets;
using Hearthstone.DataModels;
using Hearthstone.UI;
using HutongGames.PlayMaker;
using PegasusShared;
using UnityEngine;

[CustomEditClass]
public class RankChangeTwoScoop_NEW : MonoBehaviour
{
	[CustomEditField(Sections = "Playmaker Test Data")]
	public League.LeagueType m_testLeagueType = League.LeagueType.NORMAL;

	[CustomEditField(Sections = "Playmaker Test Data")]
	public int m_testStarLevel = 1;

	[CustomEditField(Sections = "Playmaker Test Data")]
	public int m_testStars;

	[CustomEditField(Sections = "Playmaker Test Data")]
	public int m_testStarMultiplier = 1;

	[CustomEditField(Sections = "Playmaker Test Data")]
	public bool m_testWinStreak;

	[CustomEditField(Sections = "Playmaker Test Data")]
	public bool m_testLegend;

	[CustomEditField(Sections = "Playmaker Test Data")]
	public bool m_testWild;

	[CustomEditField(Sections = "Playmaker Test Data")]
	public bool m_testCanLoseStars;

	[CustomEditField(Sections = "Playmaker Test Data")]
	public bool m_testCanLoseLevel;

	[CustomEditField(Sections = "Animate In")]
	public Vector3_MobileOverride m_startScale;

	[CustomEditField(Sections = "Animate In")]
	public Vector3_MobileOverride m_punchScale;

	[CustomEditField(Sections = "Animate In")]
	public Vector3_MobileOverride m_afterPunchScale;

	[CustomEditField(Sections = "Banner")]
	public Float_MobileOverride m_bannerTextWidthMult;

	[CustomEditField(Sections = "Click Blocker")]
	public float m_maxAnimTimeBeforeClickToContinue = 3f;

	public AsyncReference m_prevRankedMedalWidgetReference;

	public AsyncReference m_currRankedMedalWidgetReference;

	public AsyncReference m_currRankedMedalLegendTextReference;

	public RankedStarArray m_prevMedalStars;

	public RankedStarArray m_currMedalStars;

	public RankedStarArray m_newlyEarnedStars;

	public RankedStarArray m_newlyEarnedStarsForRankUpRow1;

	public RankedStarArray m_newlyEarnedStarsForRankUpRow2;

	public GameObject m_medalGodRays;

	public GameObject m_banner;

	public UberText m_prevBannerText;

	public UberText m_currBannerText;

	public PlayMakerFSM m_mainFSM;

	public PlayMakerFSM m_starLossFSM;

	public PlayMakerFSM m_starGainSingleFSM;

	public PlayMakerFSM m_starGainMultiFSM;

	public PlayMakerFSM m_rankUpFSM;

	public PlayMakerFSM m_rankDownFSM;

	public UberText m_winStreakText;

	public UberText m_starMultiplierText;

	public UberText m_cannotLoseStarText;

	public UberText m_cannotLoseLevelText;

	public PegUIElement m_debugClickCatcher;

	private MedalInfoTranslator m_medalInfoTranslator;

	private FormatType m_formatType = FormatType.FT_STANDARD;

	private TranslatedMedalInfo m_currMedalInfo;

	private TranslatedMedalInfo m_prevMedalInfo;

	private RankedPlayDataModel m_currMedalDataModel;

	private RankedPlayDataModel m_prevMedalDataModel;

	private RankedMedal m_currRankedMedal;

	private RankedMedal m_prevRankedMedal;

	private Widget m_currRankedMedalWidget;

	private Widget m_prevRankedMedalWidget;

	private UberText m_currRankedMedalLegendText;

	private bool m_isRankChanging;

	private bool m_isOnWinStreak;

	private Action m_closedCallback;

	private bool m_isRankChangeCheat;

	private Coroutine m_clickToContinueCoroutine;

	private bool m_isPlayingAnimWithCancelPoint;

	private bool IsReady
	{
		get
		{
			if (m_medalInfoTranslator == null)
			{
				return false;
			}
			if (m_prevRankedMedal == null || m_currRankedMedal == null)
			{
				return false;
			}
			if (!m_currRankedMedal.IsReady)
			{
				return false;
			}
			if (m_isRankChanging && !m_prevRankedMedal.IsReady)
			{
				return false;
			}
			if (m_newlyEarnedStars.IsLoading() || m_newlyEarnedStarsForRankUpRow1.IsLoading() || m_newlyEarnedStarsForRankUpRow2.IsLoading())
			{
				return false;
			}
			return true;
		}
	}

	private void Awake()
	{
		m_prevBannerText.Width *= m_bannerTextWidthMult;
		m_currBannerText.Width *= m_bannerTextWidthMult;
		Reset();
	}

	private void Start()
	{
		m_prevRankedMedalWidgetReference.RegisterReadyListener<Widget>(OnPrevRankedMedalWidgetReady);
		m_currRankedMedalWidgetReference.RegisterReadyListener<Widget>(OnCurrRankedMedalWidgetReady);
		m_currRankedMedalLegendTextReference.RegisterReadyListener<UberText>(OnCurrRankedMedalLegendTextReady);
	}

	private void OnDestroy()
	{
		if (EndGameScreen.Get() != null)
		{
			EndGameScreen.Get().m_hitbox.RemoveEventListener(UIEventType.RELEASE, OnClick);
		}
	}

	public void Initialize(MedalInfoTranslator medalInfoTranslator, FormatType formatType, Action callback)
	{
		if (medalInfoTranslator != null)
		{
			m_closedCallback = callback;
			m_medalInfoTranslator = medalInfoTranslator;
			m_formatType = formatType;
			m_currMedalInfo = m_medalInfoTranslator.GetCurrentMedal(m_formatType);
			m_prevMedalInfo = m_medalInfoTranslator.GetPreviousMedal(m_formatType);
			RankChangeType changeType = m_medalInfoTranslator.GetChangeType(m_formatType);
			m_isRankChanging = changeType == RankChangeType.RANK_UP || changeType == RankChangeType.RANK_DOWN;
			m_isOnWinStreak = m_prevMedalInfo.RankConfig.WinStreakThreshold > 0 && m_currMedalInfo.winStreak >= m_prevMedalInfo.RankConfig.WinStreakThreshold;
		}
	}

	private void InitializeFromDataModels()
	{
		int maxStars = m_prevMedalDataModel.MaxStars;
		int starCountDarkened = maxStars - m_prevMedalDataModel.Stars;
		m_prevMedalStars.Init(maxStars, starCountDarkened);
		if (m_isRankChanging)
		{
			int maxStars2 = m_currMedalDataModel.MaxStars;
			int starCountDarkened2 = maxStars2 - m_currMedalDataModel.Stars;
			m_currMedalStars.Init(maxStars2, starCountDarkened2);
		}
		if (m_isRankChanging)
		{
			if (m_prevMedalDataModel.StarMultiplier == 1 && m_isOnWinStreak)
			{
				m_newlyEarnedStarsForRankUpRow1.Init(2, 0);
			}
			else
			{
				m_newlyEarnedStarsForRankUpRow1.Init(m_prevMedalDataModel.StarMultiplier, 0);
				if (m_isOnWinStreak)
				{
					m_newlyEarnedStarsForRankUpRow2.Init(m_prevMedalDataModel.StarMultiplier, 0);
				}
			}
		}
		else
		{
			int num = m_currMedalDataModel.Stars - m_prevMedalDataModel.Stars;
			if (num > 1)
			{
				m_newlyEarnedStars.Init(num, 0);
			}
		}
		if (m_prevMedalDataModel.StarMultiplier > 1)
		{
			m_starMultiplierText.Text = GameStrings.Format("GLOBAL_RANK_STAR_MULT", m_prevMedalDataModel.StarMultiplier);
		}
		m_prevBannerText.Text = m_prevMedalDataModel.RankName;
		m_currBannerText.Text = m_currMedalDataModel.RankName;
	}

	[ContextMenu("Reset")]
	private void Reset()
	{
		m_banner.SetActive(value: false);
		m_winStreakText.gameObject.SetActive(value: false);
		m_starMultiplierText.gameObject.SetActive(value: false);
		m_cannotLoseStarText.gameObject.SetActive(value: false);
		m_cannotLoseLevelText.gameObject.SetActive(value: false);
		m_medalGodRays.SetActive(value: false);
		m_debugClickCatcher.gameObject.SetActive(value: false);
		m_prevMedalStars.Hide();
		m_currMedalStars.Hide();
		m_newlyEarnedStars.Hide();
		m_newlyEarnedStarsForRankUpRow1.Hide();
		m_newlyEarnedStarsForRankUpRow2.Hide();
		if (m_currRankedMedalWidget != null)
		{
			m_currRankedMedalWidget.Hide();
		}
		if (m_prevRankedMedalWidget != null)
		{
			m_prevRankedMedalWidget.Hide();
		}
		if (m_currRankedMedalLegendText != null)
		{
			m_currRankedMedalLegendText.Hide();
		}
		m_mainFSM.SendEvent("Reset");
		m_starLossFSM.SendEvent("Reset");
		m_starGainSingleFSM.SendEvent("Reset");
		m_starGainMultiFSM.SendEvent("Reset");
		m_rankUpFSM.SendEvent("Reset");
		m_rankDownFSM.SendEvent("Reset");
		m_isPlayingAnimWithCancelPoint = false;
	}

	public void Show()
	{
		Action<object> showFunc = delegate
		{
			AnimationUtil.ShowWithPunch(base.gameObject, m_startScale, m_punchScale, m_afterPunchScale, "OnShown", noFade: true);
			m_mainFSM.SendEvent("Birth");
		};
		StartCoroutine(ShowWhenReady(showFunc));
	}

	private void OnPrevRankedMedalWidgetReady(Widget widget)
	{
		m_prevRankedMedal = widget.GetComponentInChildren<RankedMedal>();
		m_prevRankedMedalWidget = widget;
		m_prevRankedMedalWidget.Hide();
	}

	private void OnCurrRankedMedalWidgetReady(Widget widget)
	{
		m_currRankedMedal = widget.GetComponentInChildren<RankedMedal>();
		m_currRankedMedalWidget = widget;
		m_currRankedMedalWidget.Hide();
	}

	private void OnCurrRankedMedalLegendTextReady(UberText text)
	{
		m_currRankedMedalLegendText = text;
	}

	private IEnumerator ShowWhenReady(Action<object> showFunc)
	{
		while (m_prevRankedMedalWidget == null || m_currRankedMedalWidget == null)
		{
			yield return null;
		}
		m_prevMedalDataModel = m_prevMedalInfo.CreateDataModel(RankedMedal.DisplayMode.Default, isTooltipEnabled: false, hasEarnedCardBack: false, delegate(RankedPlayDataModel dm)
		{
			m_prevRankedMedalWidget.BindDataModel(dm);
		});
		m_currMedalDataModel = m_currMedalInfo.CreateDataModel(RankedMedal.DisplayMode.Default, isTooltipEnabled: false, hasEarnedCardBack: false, delegate(RankedPlayDataModel dm)
		{
			m_currRankedMedalWidget.BindDataModel(dm);
		});
		InitializeFromDataModels();
		while (!IsReady)
		{
			yield return null;
		}
		m_banner.SetActive(value: true);
		m_prevBannerText.gameObject.SetActive(value: true);
		m_currBannerText.gameObject.SetActive(value: false);
		m_medalGodRays.SetActive(value: true);
		m_prevMedalStars.Show();
		if (m_isRankChanging)
		{
			m_prevRankedMedalWidget.Show();
		}
		else
		{
			m_currRankedMedalWidget.Show();
			m_currRankedMedalLegendText.Hide();
		}
		showFunc(this);
	}

	private void OnShown()
	{
		m_clickToContinueCoroutine = StartCoroutine(EnableClickToContinueAfterDelay(m_maxAnimTimeBeforeClickToContinue));
		switch (m_medalInfoTranslator.GetChangeType(m_formatType))
		{
		case RankChangeType.RANK_UP:
			PlayRankUp();
			break;
		case RankChangeType.RANK_DOWN:
			PlayRankDown();
			break;
		case RankChangeType.RANK_SAME:
			PlayStarChange(m_prevMedalInfo.CanLoseStars(), m_prevMedalInfo.CanLoseLevel());
			break;
		case RankChangeType.NO_GAME_PLAYED:
			HandleMissingRankChange();
			break;
		default:
			EnableClickToContinue();
			break;
		}
	}

	private IEnumerator EnableClickToContinueAfterDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		EnableClickToContinue();
	}

	private void EnableClickToContinue()
	{
		if (EndGameScreen.Get() != null)
		{
			EndGameScreen.Get().m_hitbox.AddEventListener(UIEventType.RELEASE, OnClick);
		}
		if (m_isRankChangeCheat)
		{
			m_debugClickCatcher.AddEventListener(UIEventType.RELEASE, OnClick);
		}
	}

	private void OnPlayMakerCancelPointPassed()
	{
		m_isPlayingAnimWithCancelPoint = false;
	}

	private void OnPlayMakerFinished()
	{
		m_isPlayingAnimWithCancelPoint = false;
		EnableClickToContinue();
		if (m_medalInfoTranslator.GetChangeType(m_formatType) == RankChangeType.RANK_UP && Gameplay.Get() != null)
		{
			Gameplay.Get().UpdateFriendlySideMedalChange(m_medalInfoTranslator);
		}
	}

	private void OnClick(UIEvent e)
	{
		if (m_isPlayingAnimWithCancelPoint)
		{
			m_isPlayingAnimWithCancelPoint = false;
			switch (m_medalInfoTranslator.GetChangeType(m_formatType))
			{
			case RankChangeType.RANK_UP:
				m_rankUpFSM.SendEvent("Cancel");
				break;
			case RankChangeType.RANK_DOWN:
				m_rankDownFSM.SendEvent("Cancel");
				break;
			}
		}
		else
		{
			Hide();
		}
	}

	private void Hide()
	{
		m_mainFSM.SendEvent("Death");
		if (m_clickToContinueCoroutine != null)
		{
			StopCoroutine(m_clickToContinueCoroutine);
		}
		if (EndGameScreen.Get() != null)
		{
			EndGameScreen.Get().m_hitbox.RemoveEventListener(UIEventType.RELEASE, OnClick);
		}
		if (base.gameObject != null)
		{
			AnimationUtil.ScaleFade(base.gameObject, new Vector3(0.1f, 0.1f, 0.1f), "DestroyRankChange");
		}
		if (m_isRankChangeCheat)
		{
			FullScreenFXMgr.Get().StopAllEffects();
		}
	}

	private void DestroyRankChange()
	{
		if (m_closedCallback != null)
		{
			m_closedCallback();
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private void PlayRankUp()
	{
		PopuplateBasicFsmVars(m_rankUpFSM);
		m_currMedalStars.PopulateFsmArrayWithStars(m_rankUpFSM, "NewlyEarnedMedalStars", 0, m_currMedalDataModel.Stars);
		m_newlyEarnedStarsForRankUpRow1.PopulateFsmArrayWithStars(m_rankUpFSM, "NewlyEarnedStarsRow1");
		if (m_isOnWinStreak)
		{
			m_newlyEarnedStarsForRankUpRow2.PopulateFsmArrayWithStars(m_rankUpFSM, "NewlyEarnedStarsRow2");
		}
		FsmGameObject fsmGameObject = m_rankUpFSM.FsmVariables.GetFsmGameObject("LegendRankText");
		if (fsmGameObject != null)
		{
			fsmGameObject.Value = m_currRankedMedalLegendText.gameObject;
		}
		m_isPlayingAnimWithCancelPoint = true;
		m_rankUpFSM.SendEvent("StartAnim");
	}

	private void PlayRankDown()
	{
		m_isPlayingAnimWithCancelPoint = true;
		m_rankDownFSM.SendEvent("StartAnim");
	}

	private void PlayStarChange(bool canLoseStars, bool canLoseLevel)
	{
		int num = m_currMedalDataModel.Stars - m_prevMedalDataModel.Stars;
		if (num < 0)
		{
			int startIndex = m_prevMedalDataModel.Stars - 1;
			int count = Mathf.Abs(num);
			m_prevMedalStars.PopulateFsmArrayWithStars(m_starLossFSM, "LostMedalStars", startIndex, count);
			m_starLossFSM.SendEvent("StartAnim");
			return;
		}
		switch (num)
		{
		case 0:
			if (m_currMedalDataModel.IsLegend)
			{
				m_currRankedMedalLegendText.Show();
			}
			else if (!m_currMedalDataModel.IsNewPlayer)
			{
				if (!canLoseStars)
				{
					m_cannotLoseStarText.gameObject.SetActive(value: true);
				}
				else if (!canLoseLevel)
				{
					m_cannotLoseLevelText.gameObject.SetActive(value: true);
				}
			}
			EnableClickToContinue();
			break;
		case 1:
			if (m_prevMedalDataModel.Stars > 0)
			{
				m_prevMedalStars.PopulateFsmArrayWithStars(m_starGainSingleFSM, "AlreadyEarnedMedalStars", 0, m_prevMedalDataModel.Stars);
			}
			m_prevMedalStars.PopulateFsmArrayWithStars(m_starGainSingleFSM, "NewlyEarnedMedalStars", m_prevMedalDataModel.Stars, num);
			m_starGainSingleFSM.SendEvent("StartAnim");
			break;
		default:
			PopuplateBasicFsmVars(m_starGainMultiFSM);
			m_prevMedalStars.PopulateFsmArrayWithStars(m_starGainMultiFSM, "UnearnedMedalStars", m_prevMedalDataModel.Stars, num);
			m_newlyEarnedStars.PopulateFsmArrayWithStars(m_starGainMultiFSM, "NewlyEarnedStars");
			m_starGainMultiFSM.SendEvent("StartAnim");
			break;
		}
	}

	private void PopuplateBasicFsmVars(PlayMakerFSM fsm)
	{
		FsmBool fsmBool = fsm.FsmVariables.GetFsmBool("IsWinStreak");
		if (fsmBool != null)
		{
			fsmBool.Value = m_isOnWinStreak;
		}
		FsmInt fsmInt = fsm.FsmVariables.GetFsmInt("StarMultiplier");
		if (fsmInt != null)
		{
			fsmInt.Value = m_prevMedalDataModel.StarMultiplier;
		}
		FsmBool fsmBool2 = fsm.FsmVariables.GetFsmBool("IsLegend");
		if (fsmBool2 != null)
		{
			fsmBool2.Value = m_currMedalDataModel.IsLegend;
		}
	}

	private void HandleMissingRankChange()
	{
		EnableClickToContinue();
	}

	public static void DebugShowFake(int leagueId, int starLevel, int stars, int starsPerWin, FormatType formatType, bool isWinStreak, bool showWin)
	{
		DebugShowHelper(MedalInfoTranslator.DebugCreateMedalInfo(leagueId, starLevel, stars, starsPerWin, formatType, isWinStreak, showWin), formatType);
	}

	public static void DebugShowHelper(MedalInfoTranslator medalInfoTranslator, FormatType formatType)
	{
		PrefabCallback<GameObject> callback = delegate(AssetReference assetRef, GameObject go, object callbackData)
		{
			RankChangeTwoScoop_NEW component = go.GetComponent<RankChangeTwoScoop_NEW>();
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				component.transform.localPosition = new Vector3(0f, 156.5f, 1.34f);
			}
			else
			{
				component.transform.localPosition = new Vector3(0f, 292f, -9f);
			}
			component.ActivateDebugEquivalentsOfEndGameScreen();
			component.Initialize(medalInfoTranslator, formatType, null);
			component.Show();
		};
		AssetLoader.Get().InstantiatePrefab(RankMgr.RANK_CHANGE_TWO_SCOOP_PREFAB_NEW, callback);
	}

	private void ActivateDebugEquivalentsOfEndGameScreen()
	{
		m_isRankChangeCheat = true;
		FullScreenFXMgr.Get().SetBlurDesaturation(0.5f);
		FullScreenFXMgr.Get().Blur(1f, 0.5f, iTween.EaseType.easeInCirc);
		m_debugClickCatcher.gameObject.SetActive(value: true);
	}

	[ContextMenu("Test StarLoss")]
	private void TestStarLoss()
	{
		m_isRankChanging = false;
		m_prevMedalDataModel = PrepareFakeDataModel();
		m_currMedalDataModel = PrepareFakeDataModel();
		if (m_currMedalDataModel.Stars == 0)
		{
			m_prevMedalDataModel.Stars = 1;
		}
		else
		{
			m_currMedalDataModel.Stars--;
		}
		TestShow(delegate
		{
			PlayStarChange(m_testCanLoseStars, m_testCanLoseLevel);
		});
	}

	[ContextMenu("Test StarGainSingle")]
	private void TestStarGainSingle()
	{
		m_isRankChanging = false;
		m_isOnWinStreak = m_testWinStreak;
		m_prevMedalDataModel = PrepareFakeDataModel();
		m_currMedalDataModel = PrepareFakeDataModel();
		m_currMedalDataModel.Stars++;
		m_currMedalDataModel.Stars = Mathf.Max(3, m_currMedalDataModel.Stars);
		TestShow(delegate
		{
			PlayStarChange(m_testCanLoseStars, m_testCanLoseLevel);
		});
	}

	[ContextMenu("Test StarGainMulti")]
	private void TestStarGainMulti()
	{
		m_isRankChanging = false;
		m_isOnWinStreak = m_testWinStreak;
		m_prevMedalDataModel = PrepareFakeDataModel();
		m_currMedalDataModel = PrepareFakeDataModel();
		int num = m_testStarMultiplier;
		if (m_testWinStreak)
		{
			num *= 2;
		}
		m_currMedalDataModel.Stars += num;
		m_currMedalDataModel.Stars = Mathf.Max(3, m_currMedalDataModel.Stars);
		TestShow(delegate
		{
			PlayStarChange(m_testCanLoseStars, m_testCanLoseLevel);
		});
	}

	[ContextMenu("Test RankUp")]
	private void TestRankUp()
	{
		m_isRankChanging = true;
		m_isOnWinStreak = m_testWinStreak;
		m_prevMedalDataModel = PrepareFakeDataModel();
		m_currMedalDataModel = PrepareFakeDataModel();
		int num = m_testStarMultiplier;
		if (m_testWinStreak)
		{
			num *= 2;
		}
		num -= m_currMedalDataModel.MaxStars - m_currMedalDataModel.Stars;
		num = Mathf.Max(1, num);
		int num2 = num / 3;
		int stars = num % 3;
		m_currMedalDataModel.StarLevel += num2;
		m_currMedalDataModel.Stars = stars;
		TestShow(delegate
		{
			PlayRankUp();
		});
	}

	[ContextMenu("Test RankDown")]
	private void TestRankDown()
	{
		m_isRankChanging = true;
		m_prevMedalDataModel = PrepareFakeDataModel();
		m_currMedalDataModel = PrepareFakeDataModel();
		m_currMedalDataModel.StarLevel--;
		m_currMedalDataModel.StarLevel = Mathf.Max(1, m_currMedalDataModel.StarLevel);
		m_currMedalDataModel.Stars = 2;
		TestShow(delegate
		{
			PlayRankDown();
		});
	}

	private bool TestShow(Action<object> showFunc)
	{
		if (m_medalInfoTranslator == null)
		{
			m_medalInfoTranslator = new MedalInfoTranslator();
		}
		Reset();
		if (m_prevRankedMedalWidget != null)
		{
			m_prevRankedMedalWidget.BindDataModel(m_prevMedalDataModel);
		}
		if (m_isRankChanging && m_currRankedMedalWidget != null)
		{
			m_currRankedMedalWidget.BindDataModel(m_currMedalDataModel);
		}
		InitializeFromDataModels();
		StartCoroutine(ShowWhenReady(showFunc));
		return true;
	}

	private RankedPlayDataModel PrepareFakeDataModel()
	{
		RankedPlayDataModel dataModel = new RankedPlayDataModel();
		dataModel.Stars = m_testStars;
		dataModel.MaxStars = 3;
		dataModel.StarMultiplier = m_testStarMultiplier;
		dataModel.StarLevel = m_testStarLevel;
		dataModel.MedalText = m_testStarLevel.ToString();
		dataModel.RankName = m_testStarLevel.ToString();
		dataModel.IsNewPlayer = m_testLeagueType == League.LeagueType.NEW_PLAYER;
		dataModel.IsLegend = m_testLegend;
		dataModel.LegendRank = 1337;
		dataModel.FormatType = m_formatType;
		AssetLoader.ObjectCallback callback = delegate(AssetReference assetRef, UnityEngine.Object textureObj, object data)
		{
			dataModel.MedalTexture = textureObj as Texture;
		};
		AssetLoader.Get().LoadTexture("Medal_Key_6.tif:1a7672de24da5bc4caea4d847cd690d0", callback);
		return dataModel;
	}
}
