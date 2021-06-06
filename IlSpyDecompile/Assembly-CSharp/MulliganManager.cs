using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class MulliganManager : MonoBehaviour
{
	public AnimationClip cardAnimatesFromBoardToDeck;

	public AnimationClip cardAnimatesFromBoardToDeck_iPhone;

	public AnimationClip cardAnimatesFromTableToSky;

	public AnimationClip cardAnimatesFromDeckToBoard;

	public AnimationClip shuffleDeck;

	public AnimationClip myheroAnimatesToPosition;

	public AnimationClip hisheroAnimatesToPosition;

	public AnimationClip myheroAnimatesToPosition_iPhone;

	public AnimationClip hisheroAnimatesToPosition_iPhone;

	public GameObject coinPrefab;

	public GameObject weldPrefab;

	public GameObject mulliganChooseBannerPrefab;

	public GameObject mulliganDetailLabelPrefab;

	public GameObject mulliganKeepLabelPrefab;

	public MulliganReplaceLabel mulliganReplaceLabelPrefab;

	public GameObject mulliganXlabelPrefab;

	public GameObject mulliganTimerPrefab;

	public GameObject heroLabelPrefab;

	public MulliganButton mulliganButtonWidget;

	public UberText conditionalHelperTextLabel;

	private const float PHONE_HEIGHT_OFFSET = 7f;

	private const float PHONE_CARD_Z_OFFSET = 0.2f;

	private const float PHONE_CARD_SCALE = 0.9f;

	private const float PHONE_ZONE_SIZE_ADJUST = 0.55f;

	public const float BATTLEGROUNDS_HERO_ENDING_POSITION_X = -7.7726f;

	public const float BATTLEGROUNDS_HERO_ENDING_POSITION_Y = 0.0055918f;

	public const float BATTLEGROUNDS_HERO_ENDING_POSITION_Z = -8.054f;

	public const float BATTLEGROUNDS_HERO_ENDING_SCALE = 1.134f;

	public static readonly PlatformDependentValue<Vector3> FRIENDLY_PLAYER_CARD_SCALE = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(1.1f, 0.28f, 1.1f),
		Phone = new Vector3(0.9f, 0.28f, 0.9f)
	};

	private static MulliganManager s_instance;

	private bool mulliganActive;

	private MulliganTimer m_mulliganTimer;

	private NormalButton mulliganButton;

	private GameObject myWeldEffect;

	private GameObject hisWeldEffect;

	private GameObject coinObject;

	private GameObject startingHandZone;

	private GameObject coinTossText;

	private ZoneHand friendlySideHandZone;

	private ZoneHand opposingSideHandZone;

	private ZoneDeck friendlySideDeck;

	private ZoneDeck opposingSideDeck;

	private Actor myHeroCardActor;

	private Actor hisHeroCardActor;

	private Actor myHeroPowerCardActor;

	private Actor hisHeroPowerCardActor;

	private Map<Card, Actor> opponentHeroActors = new Map<Card, Actor>();

	private Map<Card, Actor> choiceHeroActors = new Map<Card, Actor>();

	private List<Actor> fakeCardsOnLeft = new List<Actor>();

	private List<Actor> fakeCardsOnRight = new List<Actor>();

	private bool waitingForVersusText;

	private GameStartVsLetters versusText;

	private bool waitingForVersusVo;

	private AudioSource versusVo;

	private bool introComplete;

	private bool skipCardChoosing;

	private List<Card> m_startingCards;

	private List<Card> m_startingOppCards;

	private int m_coinCardIndex = -1;

	private int m_bonusCardIndex = -1;

	private GameObject mulliganChooseBanner;

	private GameObject mulliganDetailLabel;

	private List<MulliganReplaceLabel> m_replaceLabels;

	private GameObject[] m_xLabels;

	private bool[] m_handCardsMarkedForReplace = new bool[4];

	private Vector3 coinLocation;

	private bool friendlyPlayerGoesFirst;

	private HeroLabel myheroLabel;

	private HeroLabel hisheroLabel;

	private Spell m_MyCustomSocketInSpell;

	private Spell m_HisCustomSocketInSpell;

	private bool m_isLoadingMyCustomSocketIn;

	private bool m_isLoadingHisCustomSocketIn;

	private int pendingHeroCount;

	private int pendingFakeHeroCount;

	public static readonly float ANIMATION_TIME_DEAL_CARD = 1.5f;

	public static readonly float DEFAULT_STARTING_TAUNT_DURATION = 2.5f;

	private bool friendlyPlayerHasReplacementCards;

	private bool opponentPlayerHasReplacementCards;

	private bool m_waitingForUserInput;

	private Notification innkeeperMulliganDialog;

	private bool m_resuming;

	private Coroutine m_customIntroCoroutine;

	private IEnumerator m_DimLightsOnceBoardLoads;

	private IEnumerator m_WaitForBoardThenLoadButton;

	private IEnumerator m_WaitForHeroesAndStartAnimations;

	private IEnumerator m_ResumeMulligan;

	private IEnumerator m_DealStartingCards;

	private IEnumerator m_ShowMultiplayerWaitingArea;

	private IEnumerator m_RemoveOldCardsAnimation;

	private IEnumerator m_PlayStartingTaunts;

	private IEnumerator m_Spectator_WaitForFriendlyPlayerThenProcessEntitiesChosen;

	private IEnumerator m_ContinueMulliganWhenBoardLoads;

	private IEnumerator m_WaitAFrameBeforeSendingEventToMulliganButton;

	private IEnumerator m_ShrinkStartingHandBanner;

	private IEnumerator m_AnimateCoinTossText;

	private IEnumerator m_UpdateChooseBanner;

	private IEnumerator m_RemoveUIButtons;

	private IEnumerator m_WaitForOpponentToFinishMulligan;

	private IEnumerator m_EndMulliganWithTiming;

	private IEnumerator m_HandleCoinCard;

	private IEnumerator m_EnableHandCollidersAfterCardsAreDealt;

	private IEnumerator m_SkipMulliganForResume;

	private IEnumerator m_SkipMulliganWhenIntroComplete;

	private IEnumerator m_WaitForBoardAnimToCompleteThenStartTurn;

	private void Awake()
	{
		s_instance = this;
	}

	private void OnDestroy()
	{
		if (GameState.Get() != null)
		{
			GameState.Get().UnregisterCreateGameListener(OnCreateGame);
			GameState.Get().UnregisterMulliganTimerUpdateListener(OnMulliganTimerUpdate);
			GameState.Get().UnregisterEntitiesChosenReceivedListener(OnEntitiesChosenReceived);
			GameState.Get().UnregisterGameOverListener(OnGameOver);
		}
		s_instance = null;
	}

	private void Start()
	{
		if (GameState.Get() == null)
		{
			Debug.LogError($"MulliganManager.Start() - GameState already Shutdown before MulliganManager was loaded.");
			return;
		}
		if (GameState.Get().IsGameCreatedOrCreating())
		{
			HandleGameStart();
		}
		else
		{
			GameState.Get().RegisterCreateGameListener(OnCreateGame);
		}
		GameState.Get().RegisterMulliganTimerUpdateListener(OnMulliganTimerUpdate);
		GameState.Get().RegisterEntitiesChosenReceivedListener(OnEntitiesChosenReceived);
		GameState.Get().RegisterGameOverListener(OnGameOver);
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			myheroAnimatesToPosition = myheroAnimatesToPosition_iPhone;
			hisheroAnimatesToPosition = hisheroAnimatesToPosition_iPhone;
			cardAnimatesFromBoardToDeck = cardAnimatesFromBoardToDeck_iPhone;
		}
	}

	public static MulliganManager Get()
	{
		return s_instance;
	}

	public bool IsCustomIntroActive()
	{
		return m_customIntroCoroutine != null;
	}

	public bool IsMulliganActive()
	{
		return mulliganActive;
	}

	public bool IsMulliganIntroActive()
	{
		return !introComplete;
	}

	public void ForceMulliganActive(bool active)
	{
		mulliganActive = active;
		if (mulliganActive)
		{
			GameState.Get().HideZzzEffects();
		}
		else
		{
			GameState.Get().UnhideZzzEffects();
		}
	}

	public void LoadMulliganButton()
	{
		if (m_WaitForBoardThenLoadButton != null)
		{
			StopCoroutine(m_WaitForBoardThenLoadButton);
		}
		m_WaitForBoardThenLoadButton = WaitForBoardThenLoadButton();
		StartCoroutine(m_WaitForBoardThenLoadButton);
	}

	private IEnumerator WaitForBoardThenLoadButton()
	{
		while (Gameplay.Get().GetBoardLayout() == null)
		{
			yield return null;
		}
		if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_REQUIRES_CONFIRMATION))
		{
			AssetLoader.Get().InstantiatePrefab("MulliganButton.prefab:f58c065fc711b604c891cefd1faf722a", OnMulliganButtonLoaded, null, AssetLoadingOptions.IgnorePrefabPosition);
		}
		if (conditionalHelperTextLabel != null)
		{
			conditionalHelperTextLabel.transform.position = Board.Get().FindBone("MulliganHelperTextPosition").position;
		}
	}

	private void OnMulliganButtonLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError($"MulliganManager.OnMulliganButtonLoaded() - FAILED to load \"{assetRef}\"");
			return;
		}
		mulliganButton = go.GetComponent<NormalButton>();
		if (mulliganButton == null)
		{
			Debug.LogError($"MulliganManager.OnMulliganButtonLoaded() - ERROR \"{assetRef}\" has no {typeof(NormalButton)} component");
			return;
		}
		mulliganButton.SetText(GameStrings.Get("GLOBAL_CONFIRM"));
		mulliganButtonWidget.SetText(GameStrings.Get("GLOBAL_CONFIRM"));
		if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_IS_CHOOSE_ONE))
		{
			mulliganButton.SetEnabled(enabled: false);
			mulliganButton.gameObject.SetActive(value: false);
			mulliganButtonWidget.SetEnabled(active: false);
		}
		else
		{
			mulliganButtonWidget.gameObject.SetActive(value: false);
		}
	}

	private void OnVersusVoLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		waitingForVersusVo = false;
		if (go == null)
		{
			Debug.LogError($"MulliganManager.OnVersusVoLoaded() - FAILED to load \"{assetRef}\"");
			return;
		}
		versusVo = go.GetComponent<AudioSource>();
		if (versusVo == null)
		{
			Debug.LogError($"MulliganManager.OnVersusVoLoaded() - ERROR \"{assetRef}\" has no {typeof(AudioSource)} component");
		}
	}

	private void OnVersusTextLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		waitingForVersusText = false;
		if (go == null)
		{
			Debug.LogError($"MulliganManager.OnVersusTextLoaded() - FAILED to load \"{assetRef}\"");
			return;
		}
		versusText = go.GetComponent<GameStartVsLetters>();
		if (versusText == null)
		{
			Log.All.PrintError("MulliganManager.OnVersusTextLoaded() object loaded does not have a GameStartVsLetters component");
		}
	}

	private IEnumerator WaitForHeroesAndStartAnimations()
	{
		Log.LoadingScreen.Print("MulliganManager.WaitForHeroesAndStartAnimations()");
		GameEntity gameEntity = GameState.Get().GetGameEntity();
		Player friendlyPlayer;
		for (friendlyPlayer = GameState.Get().GetFriendlySidePlayer(); friendlyPlayer == null; friendlyPlayer = GameState.Get().GetFriendlySidePlayer())
		{
			yield return null;
		}
		Player opposingPlayer;
		for (opposingPlayer = GameState.Get().GetOpposingSidePlayer(); opposingPlayer == null; opposingPlayer = GameState.Get().GetOpposingSidePlayer())
		{
			yield return null;
		}
		Card myHeroCard = null;
		while (myHeroCardActor == null)
		{
			myHeroCard = friendlyPlayer.GetHeroCard();
			if (myHeroCard != null)
			{
				myHeroCardActor = myHeroCard.GetActor();
			}
			yield return null;
		}
		Card hisHeroCard = null;
		while (hisHeroCardActor == null)
		{
			hisHeroCard = opposingPlayer.GetHeroCard();
			if (hisHeroCard != null)
			{
				hisHeroCardActor = hisHeroCard.GetActor();
			}
			yield return null;
		}
		while (friendlyPlayer.GetHeroPower() != null && myHeroPowerCardActor == null)
		{
			Card heroPowerCard = friendlyPlayer.GetHeroPowerCard();
			if (heroPowerCard != null)
			{
				myHeroPowerCardActor = heroPowerCard.GetActor();
				if (myHeroPowerCardActor != null)
				{
					myHeroPowerCardActor.TurnOffCollider();
				}
			}
			yield return null;
		}
		while (opposingPlayer.GetHeroPower() != null && hisHeroPowerCardActor == null)
		{
			Card heroPowerCard2 = opposingPlayer.GetHeroPowerCard();
			if (heroPowerCard2 != null)
			{
				hisHeroPowerCardActor = heroPowerCard2.GetActor();
				if (hisHeroPowerCardActor != null)
				{
					hisHeroPowerCardActor.TurnOffCollider();
				}
			}
			yield return null;
		}
		while (GameState.Get() == null || GameState.Get().GetGameEntity().IsPreloadingAssets())
		{
			yield return null;
		}
		while (!myHeroCardActor.HasCardDef)
		{
			yield return null;
		}
		while (!hisHeroCardActor.HasCardDef)
		{
			yield return null;
		}
		LoadMyHeroSkinSocketInEffect(myHeroCardActor);
		LoadHisHeroSkinSocketInEffect(hisHeroCardActor);
		while (m_isLoadingMyCustomSocketIn || m_isLoadingHisCustomSocketIn)
		{
			yield return null;
		}
		List<Material> materials = myHeroCardActor.m_portraitMesh.GetComponent<Renderer>().GetMaterials();
		Material myHeroMat = materials[myHeroCardActor.m_portraitMatIdx];
		Material myHeroFrameMat = materials[myHeroCardActor.m_portraitFrameMatIdx];
		if (myHeroMat != null && myHeroMat.HasProperty("_LightingBlend"))
		{
			myHeroMat.SetFloat("_LightingBlend", 0f);
		}
		if (myHeroFrameMat != null && myHeroFrameMat.HasProperty("_LightingBlend"))
		{
			myHeroFrameMat.SetFloat("_LightingBlend", 0f);
		}
		float value = (GameState.Get().GetBooleanGameOption(GameEntityOption.DIM_OPPOSING_HERO_DURING_MULLIGAN) ? 1f : 0f);
		List<Material> materials2 = hisHeroCardActor.m_portraitMesh.GetComponent<Renderer>().GetMaterials();
		Material hisHeroMat = materials2[hisHeroCardActor.m_portraitMatIdx];
		Material hisHeroFrameMat = materials2[hisHeroCardActor.m_portraitFrameMatIdx];
		if (hisHeroMat != null && hisHeroMat.HasProperty("_LightingBlend"))
		{
			hisHeroMat.SetFloat("_LightingBlend", value);
		}
		if (hisHeroFrameMat != null && hisHeroFrameMat.HasProperty("_LightingBlend"))
		{
			hisHeroFrameMat.SetFloat("_LightingBlend", value);
		}
		if (myHeroPowerCardActor != null && myHeroPowerCardActor.m_portraitMesh != null)
		{
			List<Material> materials3 = myHeroPowerCardActor.m_portraitMesh.GetComponent<Renderer>().GetMaterials();
			Material material = materials3[myHeroPowerCardActor.m_portraitMatIdx];
			if (material != null && material.HasProperty("_LightingBlend"))
			{
				material.SetFloat("_LightingBlend", 1f);
			}
			Material material2 = materials3[myHeroPowerCardActor.m_portraitFrameMatIdx];
			if (material2 != null && material2.HasProperty("_LightingBlend"))
			{
				material2.SetFloat("_LightingBlend", 1f);
			}
		}
		if (hisHeroPowerCardActor != null && hisHeroPowerCardActor.m_portraitMesh != null)
		{
			List<Material> materials4 = hisHeroPowerCardActor.m_portraitMesh.GetComponent<Renderer>().GetMaterials();
			Material material3 = materials4[hisHeroPowerCardActor.m_portraitMatIdx];
			if (material3 != null && material3.HasProperty("_LightingBlend"))
			{
				material3.SetFloat("_LightingBlend", 1f);
			}
			Material material4 = materials4[hisHeroPowerCardActor.m_portraitFrameMatIdx];
			if (material4 != null && material4.HasProperty("_LightingBlend"))
			{
				material4.SetFloat("_LightingBlend", 1f);
			}
		}
		myHeroCardActor.TurnOffCollider();
		hisHeroCardActor.TurnOffCollider();
		gameEntity.NotifyOfMulliganInitialized();
		if (GameState.Get().GetGameEntity().DoAlternateMulliganIntro())
		{
			if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_HAS_HERO_LOBBY))
			{
				myHeroCardActor.Hide();
			}
			introComplete = true;
			yield break;
		}
		while (waitingForVersusText || waitingForVersusVo)
		{
			yield return null;
		}
		Log.LoadingScreen.Print("MulliganManager.WaitForHeroesAndStartAnimations() - NotifySceneLoaded()");
		SceneMgr.Get().NotifySceneLoaded();
		while (LoadingScreen.Get().IsPreviousSceneActive() || LoadingScreen.Get().IsFadingOut())
		{
			yield return null;
		}
		GameMgr.Get().UpdatePresence();
		GameObject myHero = myHeroCardActor.gameObject;
		GameObject hisHero = hisHeroCardActor.gameObject;
		myHeroCardActor.GetHealthObject().Hide();
		hisHeroCardActor.GetHealthObject().Hide();
		if (myHeroCardActor.GetAttackObject() != null)
		{
			myHeroCardActor.GetAttackObject().Hide();
		}
		if (hisHeroCardActor.GetAttackObject() != null)
		{
			hisHeroCardActor.GetAttackObject().Hide();
		}
		if ((bool)versusText)
		{
			versusText.transform.position = Board.Get().FindBone("VS_Position").position;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate(heroLabelPrefab);
		myheroLabel = gameObject.GetComponent<HeroLabel>();
		myheroLabel.transform.parent = myHeroCardActor.GetMeshRenderer().transform;
		myheroLabel.transform.localPosition = new Vector3(0f, 0f, 0f);
		TAG_CLASS @class = myHeroCardActor.GetEntity().GetClass();
		string classText = "";
		if (@class != TAG_CLASS.NEUTRAL && gameEntity.ShouldShowHeroClassDuringMulligan(Player.Side.FRIENDLY))
		{
			classText = GameStrings.GetClassName(@class).ToUpper();
		}
		myheroLabel.UpdateText(myHeroCardActor.GetEntity().GetName(), classText);
		gameObject = UnityEngine.Object.Instantiate(heroLabelPrefab);
		hisheroLabel = gameObject.GetComponent<HeroLabel>();
		hisheroLabel.transform.parent = hisHeroCardActor.GetMeshRenderer().transform;
		hisheroLabel.transform.localPosition = new Vector3(0f, 0f, 0f);
		@class = hisHeroCardActor.GetEntity().GetClass();
		classText = "";
		if (@class != TAG_CLASS.NEUTRAL && gameEntity.ShouldShowHeroClassDuringMulligan(Player.Side.OPPOSING))
		{
			classText = GameStrings.GetClassName(@class).ToUpper();
		}
		hisheroLabel.UpdateText(hisHeroCardActor.GetEntity().GetName(), classText);
		if (GameState.Get().WasConcedeRequested())
		{
			yield break;
		}
		gameEntity.StartMulliganSoundtracks(soft: false);
		Animation cardAnim = myHero.GetComponent<Animation>();
		if (cardAnim == null)
		{
			cardAnim = myHero.AddComponent<Animation>();
		}
		cardAnim.AddClip(hisheroAnimatesToPosition, "hisHeroAnimateToPosition");
		StartCoroutine(SampleAnimFrame(cardAnim, "hisHeroAnimateToPosition", 0f));
		Animation oppCardAnim = hisHero.GetComponent<Animation>();
		if (oppCardAnim == null)
		{
			oppCardAnim = hisHero.AddComponent<Animation>();
		}
		oppCardAnim.AddClip(myheroAnimatesToPosition, "myHeroAnimateToPosition");
		StartCoroutine(SampleAnimFrame(oppCardAnim, "myHeroAnimateToPosition", 0f));
		m_customIntroCoroutine = StartCoroutine(GameState.Get().GetGameEntity().DoCustomIntro(myHeroCard, hisHeroCard, myheroLabel, hisheroLabel, versusText));
		yield return m_customIntroCoroutine;
		m_customIntroCoroutine = null;
		while (LoadingScreen.Get().IsTransitioning())
		{
			yield return null;
		}
		AudioSource myHeroLine = gameEntity.GetAnnouncerLine(myHeroCard, Card.AnnouncerLineType.BEFORE_VERSUS);
		AudioSource hisHeroLine = gameEntity.GetAnnouncerLine(hisHeroCard, Card.AnnouncerLineType.AFTER_VERSUS);
		if ((bool)versusVo && (bool)myHeroLine && (bool)hisHeroLine)
		{
			SoundManager.Get().Play(myHeroLine);
			while (SoundManager.Get().IsActive(myHeroLine) && !SoundManager.Get().IsPlaybackFinished(myHeroLine))
			{
				yield return null;
			}
			yield return new WaitForSeconds(0.05f);
			SoundManager.Get().PlayPreloaded(versusVo);
			while (SoundManager.Get().IsActive(versusVo) && !SoundManager.Get().IsPlaybackFinished(versusVo))
			{
				yield return null;
			}
			yield return new WaitForSeconds(0.05f);
			if (hisHeroLine != null && hisHeroLine.clip != null)
			{
				SoundManager.Get().Play(hisHeroLine);
				while (SoundManager.Get().IsActive(hisHeroLine) && !SoundManager.Get().IsPlaybackFinished(hisHeroLine))
				{
					yield return null;
				}
			}
		}
		else
		{
			yield return new WaitForSeconds(0.6f);
		}
		yield return StartCoroutine(GameState.Get().GetGameEntity().PlayMissionIntroLineAndWait());
		myheroLabel.transform.parent = null;
		hisheroLabel.transform.parent = null;
		myheroLabel.FadeOut();
		hisheroLabel.FadeOut();
		yield return new WaitForSeconds(0.5f);
		if (m_MyCustomSocketInSpell != null)
		{
			m_MyCustomSocketInSpell.m_Location = SpellLocation.NONE;
			m_MyCustomSocketInSpell.gameObject.SetActive(value: true);
			if (myHeroCardActor.SocketInParentEffectToHero)
			{
				Vector3 localScale = myHeroCardActor.transform.localScale;
				myHeroCardActor.transform.localScale = Vector3.one;
				m_MyCustomSocketInSpell.transform.parent = myHeroCardActor.transform;
				m_MyCustomSocketInSpell.transform.localPosition = Vector3.zero;
				myHeroCardActor.transform.localScale = localScale;
			}
			m_MyCustomSocketInSpell.SetSource(myHeroCardActor.GetCard().gameObject);
			m_MyCustomSocketInSpell.RemoveAllTargets();
			GameObject myHeroSocketBone = ZoneMgr.Get().FindZoneOfType<ZoneHero>(Player.Side.FRIENDLY).gameObject;
			m_MyCustomSocketInSpell.AddTarget(myHeroSocketBone);
			m_MyCustomSocketInSpell.ActivateState(SpellStateType.BIRTH);
			m_MyCustomSocketInSpell.AddStateFinishedCallback(delegate
			{
				myHeroCardActor.transform.position = myHeroSocketBone.transform.position;
				myHeroCardActor.transform.localScale = Vector3.one;
			});
			if (!myHeroCardActor.SocketInOverrideHeroAnimation)
			{
				cardAnim.Play("hisHeroAnimateToPosition");
			}
		}
		else
		{
			cardAnim.Play("hisHeroAnimateToPosition");
		}
		if (m_HisCustomSocketInSpell != null)
		{
			if ((bool)m_MyCustomSocketInSpell)
			{
				SoundUtils.SetSourceVolumes(m_HisCustomSocketInSpell, 0f);
			}
			m_HisCustomSocketInSpell.m_Location = SpellLocation.NONE;
			if (hisHeroCardActor.SocketInOverrideHeroAnimation)
			{
				yield return new WaitForSeconds(0.25f);
			}
			m_HisCustomSocketInSpell.gameObject.SetActive(value: true);
			if (hisHeroCardActor.SocketInParentEffectToHero)
			{
				Vector3 localScale2 = hisHeroCardActor.transform.localScale;
				hisHeroCardActor.transform.localScale = Vector3.one;
				m_HisCustomSocketInSpell.transform.parent = hisHeroCardActor.transform;
				m_HisCustomSocketInSpell.transform.localPosition = Vector3.zero;
				hisHeroCardActor.transform.localScale = localScale2;
			}
			m_HisCustomSocketInSpell.SetSource(hisHeroCardActor.GetCard().gameObject);
			m_HisCustomSocketInSpell.RemoveAllTargets();
			GameObject hisHeroSocketBone = ZoneMgr.Get().FindZoneOfType<ZoneHero>(Player.Side.OPPOSING).gameObject;
			m_HisCustomSocketInSpell.AddTarget(hisHeroSocketBone);
			m_HisCustomSocketInSpell.ActivateState(SpellStateType.BIRTH);
			m_HisCustomSocketInSpell.AddStateFinishedCallback(delegate
			{
				hisHeroCardActor.transform.position = hisHeroSocketBone.transform.position;
				hisHeroCardActor.transform.localScale = Vector3.one;
			});
			if (!hisHeroCardActor.SocketInOverrideHeroAnimation)
			{
				oppCardAnim.Play("myHeroAnimateToPosition");
			}
		}
		else
		{
			oppCardAnim.Play("myHeroAnimateToPosition");
		}
		SoundManager.Get().LoadAndPlay("FX_MulliganCoin01_HeroCoinDrop.prefab:c46488739eda9f94eb0160290e35f321", hisHeroCardActor.GetCard().gameObject);
		if ((bool)versusText)
		{
			yield return new WaitForSeconds(0.1f);
			versusText.FadeOut();
			yield return new WaitForSeconds(0.32f);
		}
		if (m_MyCustomSocketInSpell == null)
		{
			myWeldEffect = UnityEngine.Object.Instantiate(weldPrefab);
			myWeldEffect.transform.position = myHero.transform.position;
			if ((bool)m_HisCustomSocketInSpell)
			{
				SoundUtils.SetSourceVolumes(myWeldEffect, 0f);
			}
			myWeldEffect.GetComponent<HeroWeld>().DoAnim();
		}
		if (m_HisCustomSocketInSpell == null)
		{
			hisWeldEffect = UnityEngine.Object.Instantiate(weldPrefab);
			hisWeldEffect.transform.position = hisHero.transform.position;
			if ((bool)m_MyCustomSocketInSpell)
			{
				SoundUtils.SetSourceVolumes(hisWeldEffect, 0f);
			}
			hisWeldEffect.GetComponent<HeroWeld>().DoAnim();
		}
		yield return new WaitForSeconds(0.05f);
		iTween.ShakePosition(Camera.main.gameObject, iTween.Hash("time", 0.6f, "amount", new Vector3(0.03f, 0.01f, 0.03f)));
		Action<object> action = delegate(object amount)
		{
			if (myHeroMat != null)
			{
				myHeroMat.SetFloat("_LightingBlend", (float)amount);
			}
			if (myHeroFrameMat != null)
			{
				myHeroFrameMat.SetFloat("_LightingBlend", (float)amount);
			}
		};
		action(0f);
		Hashtable args = iTween.Hash("time", 1f, "from", 0f, "to", 1f, "delay", 2f, "onupdate", action, "onupdatetarget", base.gameObject, "name", "MyHeroLightBlend");
		iTween.ValueTo(base.gameObject, args);
		Action<object> action2 = delegate(object amount)
		{
			if (hisHeroMat != null)
			{
				hisHeroMat.SetFloat("_LightingBlend", (float)amount);
			}
			if (hisHeroFrameMat != null)
			{
				hisHeroFrameMat.SetFloat("_LightingBlend", (float)amount);
			}
		};
		action2(0f);
		Hashtable args2 = iTween.Hash("time", 1f, "from", 0f, "to", 1f, "delay", 2f, "onupdate", action2, "onupdatetarget", base.gameObject, "name", "HisHeroLightBlend");
		iTween.ValueTo(base.gameObject, args2);
		yield return GameState.Get().GetGameEntity().DoGameSpecificPostIntroActions();
		introComplete = true;
		GameState.Get().GetGameEntity().NotifyOfHeroesFinishedAnimatingInMulligan();
		ScreenEffectsMgr.Get().SetActive(enabled: true);
	}

	public void BeginMulligan()
	{
		bool flag = mulliganActive;
		ForceMulliganActive(active: true);
		if (GameState.Get().WasConcedeRequested())
		{
			HandleGameOverDuringMulligan();
		}
		else if (!flag || !SpectatorManager.Get().IsSpectatingOpposingSide())
		{
			m_ContinueMulliganWhenBoardLoads = ContinueMulliganWhenBoardLoads();
			StartCoroutine(m_ContinueMulliganWhenBoardLoads);
		}
	}

	private void OnCreateGame(GameState.CreateGamePhase phase, object userData)
	{
		GameState.Get().UnregisterCreateGameListener(OnCreateGame);
		HandleGameStart();
	}

	private void HandleGameStart()
	{
		Log.LoadingScreen.Print("MulliganManager.HandleGameStart() - IsPastBeginPhase()={0}", GameState.Get().IsPastBeginPhase());
		bool flag = GameMgr.Get().IsSpectator() && GameState.Get().GetGameEntity().HasTag(GAME_TAG.PUZZLE_MODE);
		if (GameState.Get().IsPastBeginPhase() || flag)
		{
			m_SkipMulliganForResume = SkipMulliganForResume();
			StartCoroutine(m_SkipMulliganForResume);
			return;
		}
		InitZones();
		m_DimLightsOnceBoardLoads = DimLightsOnceBoardLoads();
		StartCoroutine(m_DimLightsOnceBoardLoads);
		if (!GameState.Get().GetGameEntity().ShouldDoAlternateMulliganIntro())
		{
			m_xLabels = new GameObject[4];
			coinObject = UnityEngine.Object.Instantiate(coinPrefab);
			coinObject.SetActive(value: false);
			if (!Cheats.Get().ShouldSkipMulligan())
			{
				if (Cheats.Get().IsLaunchingQuickGame())
				{
					TimeScaleMgr.Get().SetTimeScaleMultiplier(SceneDebugger.GetDevTimescaleMultiplier());
				}
				waitingForVersusVo = true;
				SoundLoader.LoadSound("VO_ANNOUNCER_VERSUS_21.prefab:acc34acb15f07ff4ba08025a57a9a458", OnVersusVoLoaded);
			}
			waitingForVersusText = true;
			AssetLoader.Get().InstantiatePrefab("GameStart_VS_Letters.prefab:3cb2cbed6d44a694eb23fb8791684003", OnVersusTextLoaded);
			if (m_WaitForBoardThenLoadButton != null)
			{
				StopCoroutine(m_WaitForBoardThenLoadButton);
			}
			m_WaitForBoardThenLoadButton = WaitForBoardThenLoadButton();
			StartCoroutine(m_WaitForBoardThenLoadButton);
		}
		else
		{
			waitingForVersusVo = true;
			SoundLoader.LoadSound("VO_ANNOUNCER_VERSUS_21.prefab:acc34acb15f07ff4ba08025a57a9a458", OnVersusVoLoaded);
			waitingForVersusText = true;
			AssetLoader.Get().InstantiatePrefab("GameStart_VS_Letters.prefab:3cb2cbed6d44a694eb23fb8791684003", OnVersusTextLoaded);
		}
		m_WaitForHeroesAndStartAnimations = WaitForHeroesAndStartAnimations();
		StartCoroutine(m_WaitForHeroesAndStartAnimations);
		Log.LoadingScreen.Print("MulliganManager.HandleGameStart() - IsMulliganPhase()={0}", GameState.Get().IsMulliganPhase());
		if (GameState.Get().IsMulliganPhase())
		{
			m_ResumeMulligan = ResumeMulligan();
			StartCoroutine(m_ResumeMulligan);
		}
	}

	private IEnumerator DimLightsOnceBoardLoads()
	{
		while (Board.Get() == null)
		{
			yield return null;
		}
		Board.Get().SetMulliganLighting();
	}

	private IEnumerator ResumeMulligan()
	{
		m_resuming = true;
		foreach (Player value in GameState.Get().GetPlayerMap().Values)
		{
			if (value.GetTag<TAG_MULLIGAN>(GAME_TAG.MULLIGAN_STATE) == TAG_MULLIGAN.DONE)
			{
				if (value.IsFriendlySide())
				{
					friendlyPlayerHasReplacementCards = true;
				}
				else
				{
					opponentPlayerHasReplacementCards = true;
				}
			}
		}
		if (friendlyPlayerHasReplacementCards)
		{
			SkipCardChoosing();
		}
		else
		{
			while (GameState.Get().GetResponseMode() != GameState.ResponseMode.CHOICE)
			{
				yield return null;
			}
		}
		BeginMulligan();
	}

	private void OnMulliganTimerUpdate(TurnTimerUpdate update, object userData)
	{
		if (update.GetSecondsRemaining() > Mathf.Epsilon)
		{
			if (update.ShouldShow())
			{
				BeginMulliganCountdown(update.GetEndTimestamp());
			}
			else
			{
				StopMulliganCountdown();
			}
		}
		else
		{
			GameState.Get().UnregisterMulliganTimerUpdateListener(OnMulliganTimerUpdate);
			AutomaticContinueMulligan();
		}
	}

	private bool OnEntitiesChosenReceived(Network.EntitiesChosen chosen, object userData)
	{
		if (!GameMgr.Get().IsSpectator())
		{
			return false;
		}
		int playerId = chosen.PlayerId;
		int friendlyPlayerId = GameState.Get().GetFriendlyPlayerId();
		if (playerId == friendlyPlayerId)
		{
			m_Spectator_WaitForFriendlyPlayerThenProcessEntitiesChosen = Spectator_WaitForFriendlyPlayerThenProcessEntitiesChosen(chosen);
			StartCoroutine(m_Spectator_WaitForFriendlyPlayerThenProcessEntitiesChosen);
			return true;
		}
		return false;
	}

	private void OnGameOver(TAG_PLAYSTATE playState, object userData)
	{
		HandleGameOverDuringMulligan();
	}

	private IEnumerator Spectator_WaitForFriendlyPlayerThenProcessEntitiesChosen(Network.EntitiesChosen chosen)
	{
		while (!m_waitingForUserInput)
		{
			if (GameState.Get().IsGameOver() || skipCardChoosing)
			{
				yield break;
			}
			yield return null;
		}
		for (int i = 0; i < m_startingCards.Count; i++)
		{
			int entityId = m_startingCards[i].GetEntity().GetEntityId();
			bool flag = !chosen.Entities.Contains(entityId);
			if (m_handCardsMarkedForReplace[i] != flag)
			{
				ToggleHoldState(i);
			}
		}
		GameState.Get().OnEntitiesChosenProcessed(chosen);
		BeginDealNewCards();
	}

	private IEnumerator ContinueMulliganWhenBoardLoads()
	{
		while (ZoneMgr.Get() == null)
		{
			yield return null;
		}
		Board board = Board.Get();
		startingHandZone = board.FindBone("StartingHandZone").gameObject;
		InitZones();
		if (m_resuming)
		{
			while (ShouldWaitForMulliganCardsToBeProcessed())
			{
				yield return null;
			}
		}
		SortHand(friendlySideHandZone);
		SortHand(opposingSideHandZone);
		board.CombinedSurface();
		board.FindCollider("DragPlane").enabled = false;
		if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_HAS_HERO_LOBBY))
		{
			m_ShowMultiplayerWaitingArea = ShowMultiplayerWaitingArea();
			StartCoroutine(m_ShowMultiplayerWaitingArea);
		}
		else
		{
			m_DealStartingCards = DealStartingCards();
			StartCoroutine(m_DealStartingCards);
		}
	}

	private void InitZones()
	{
		foreach (Zone zone in ZoneMgr.Get().GetZones())
		{
			if (zone is ZoneHand)
			{
				if (zone.m_Side == Player.Side.FRIENDLY)
				{
					friendlySideHandZone = (ZoneHand)zone;
				}
				else
				{
					opposingSideHandZone = (ZoneHand)zone;
				}
			}
			if (zone is ZoneDeck)
			{
				if (zone.m_Side == Player.Side.FRIENDLY)
				{
					friendlySideDeck = (ZoneDeck)zone;
					friendlySideDeck.SetSuppressEmotes(suppress: true);
					friendlySideDeck.UpdateLayout();
				}
				else
				{
					opposingSideDeck = (ZoneDeck)zone;
					opposingSideDeck.SetSuppressEmotes(suppress: true);
					opposingSideDeck.UpdateLayout();
				}
			}
		}
	}

	private bool ShouldWaitForMulliganCardsToBeProcessed()
	{
		PowerProcessor powerProcessor = GameState.Get().GetPowerProcessor();
		bool receivedEndOfMulligan = false;
		powerProcessor.ForEachTaskList(delegate(int index, PowerTaskList taskList)
		{
			if (IsTaskListPuttingUsPastMulligan(taskList))
			{
				receivedEndOfMulligan = true;
			}
		});
		if (receivedEndOfMulligan)
		{
			return false;
		}
		return powerProcessor.HasTaskLists();
	}

	private bool IsTaskListPuttingUsPastMulligan(PowerTaskList taskList)
	{
		foreach (PowerTask task in taskList.GetTaskList())
		{
			Network.PowerHistory power = task.GetPower();
			if (power.Type == Network.PowerType.TAG_CHANGE)
			{
				Network.HistTagChange histTagChange = power as Network.HistTagChange;
				if (histTagChange.Tag == 198 && GameUtils.IsPastBeginPhase((TAG_STEP)histTagChange.Value))
				{
					return true;
				}
			}
		}
		return false;
	}

	private void GetStartingLists()
	{
		List<Card> cards = friendlySideHandZone.GetCards();
		List<Card> cards2 = opposingSideHandZone.GetCards();
		int num;
		if (ShouldHandleCoinCard())
		{
			if (friendlyPlayerGoesFirst)
			{
				num = cards.Count;
				m_bonusCardIndex = cards2.Count - 2;
				m_coinCardIndex = cards2.Count - 1;
			}
			else
			{
				num = cards.Count - 1;
				m_bonusCardIndex = cards.Count - 2;
			}
		}
		else
		{
			num = cards.Count;
			if (friendlyPlayerGoesFirst)
			{
				m_bonusCardIndex = cards2.Count - 1;
			}
			else
			{
				m_bonusCardIndex = cards.Count - 1;
			}
		}
		m_startingCards = new List<Card>();
		for (int i = 0; i < num; i++)
		{
			m_startingCards.Add(cards[i]);
		}
		m_startingOppCards = new List<Card>();
		for (int j = 0; j < cards2.Count; j++)
		{
			m_startingOppCards.Add(cards2[j]);
		}
	}

	private IEnumerator PlayStartingTaunts()
	{
		Card heroCard = GameState.Get().GetOpposingSidePlayer().GetHeroCard();
		Card heroPowerCard = GameState.Get().GetOpposingSidePlayer().GetHeroPowerCard();
		iTween.StopByName(base.gameObject, "HisHeroLightBlend");
		if (heroPowerCard != null)
		{
			while (!heroPowerCard.GetActor().IsShown())
			{
				yield return null;
			}
			GameState.Get().GetGameEntity().FadeInActor(heroPowerCard.GetActor(), 0.4f);
		}
		while (!heroCard.GetActor().IsShown())
		{
			yield return null;
		}
		GameState.Get().GetGameEntity().FadeInHeroActor(heroCard.GetActor());
		EmoteEntry emoteEntry = heroCard.GetEmoteEntry(EmoteType.START);
		bool flag = true;
		if (emoteEntry != null)
		{
			CardSoundSpell soundSpell = emoteEntry.GetSoundSpell();
			if (soundSpell != null && soundSpell.DetermineBestAudioSource() == null)
			{
				flag = false;
			}
		}
		CardSoundSpell emoteSpell2 = null;
		if (flag)
		{
			emoteSpell2 = heroCard.PlayEmote(EmoteType.START);
		}
		if (emoteSpell2 != null)
		{
			while (emoteSpell2.GetActiveState() != 0)
			{
				yield return null;
			}
		}
		else
		{
			yield return new WaitForSeconds(DEFAULT_STARTING_TAUNT_DURATION);
		}
		GameState.Get().GetGameEntity().FadeOutHeroActor(heroCard.GetActor());
		if (heroPowerCard != null)
		{
			GameState.Get().GetGameEntity().FadeOutActor(heroPowerCard.GetActor());
		}
		Card myHeroCard = GameState.Get().GetFriendlySidePlayer().GetHeroCard();
		Card myHeroPowerCard = GameState.Get().GetFriendlySidePlayer().GetHeroPowerCard();
		if (Get() == null)
		{
			yield break;
		}
		iTween.StopByName(base.gameObject, "MyHeroLightBlend");
		if (myHeroPowerCard != null)
		{
			GameState.Get().GetGameEntity().FadeInActor(myHeroPowerCard.GetActor(), 0.4f);
		}
		EmoteType emoteToPlay = EmoteType.START;
		EmoteEntry emoteEntry2 = myHeroCard.GetEmoteEntry(EmoteType.START);
		if (emoteEntry2 != null && !string.IsNullOrEmpty(emoteEntry2.GetGameStringKey()))
		{
			EmoteEntry emoteEntry3 = heroCard.GetEmoteEntry(EmoteType.START);
			if (emoteEntry3 != null && emoteEntry2.GetGameStringKey() == emoteEntry3.GetGameStringKey())
			{
				emoteToPlay = EmoteType.MIRROR_START;
			}
		}
		while (!myHeroCard.GetActor().IsShown())
		{
			yield return null;
		}
		GameState.Get().GetGameEntity().FadeInHeroActor(myHeroCard.GetActor());
		emoteSpell2 = myHeroCard.PlayEmote(emoteToPlay, Notification.SpeechBubbleDirection.BottomRight);
		if (emoteSpell2 != null)
		{
			while (emoteSpell2.GetActiveState() != 0)
			{
				yield return null;
			}
		}
		else
		{
			yield return new WaitForSeconds(DEFAULT_STARTING_TAUNT_DURATION);
		}
		GameState.Get().GetGameEntity().FadeOutHeroActor(myHeroCard.GetActor());
		if (myHeroPowerCard != null)
		{
			GameState.Get().GetGameEntity().FadeOutActor(myHeroPowerCard.GetActor());
		}
	}

	private IEnumerator ShowMultiplayerWaitingArea()
	{
		yield return new WaitForSeconds(1f);
		while (!introComplete)
		{
			yield return null;
		}
		yield return StartCoroutine(GameState.Get().GetGameEntity().DoActionsAfterIntroBeforeMulligan());
		if (GameState.Get().GetBooleanGameOption(GameEntityOption.DO_OPENING_TAUNTS) && !Cheats.Get().ShouldSkipMulligan())
		{
			m_PlayStartingTaunts = PlayStartingTaunts();
			StartCoroutine(m_PlayStartingTaunts);
		}
		Player friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		friendlyPlayerGoesFirst = friendlySidePlayer.HasTag(GAME_TAG.FIRST_PLAYER);
		GetStartingLists();
		bool isMulliganOver = false;
		bool shouldSendTelemetry = true;
		if (m_startingCards.Count == 0)
		{
			while (GameState.Get().GetFriendlySidePlayer().GetHeroCard() == null)
			{
				if (shouldSendTelemetry)
				{
					TelemetryManager.Client().SendLiveIssue("Gameplay_MulliganManager", "No hero card set for friendly side player");
					shouldSendTelemetry = false;
				}
				yield return null;
			}
			m_startingCards.Add(GameState.Get().GetFriendlySidePlayer().GetHeroCard());
			isMulliganOver = true;
		}
		shouldSendTelemetry = false;
		foreach (Card startingCard in m_startingCards)
		{
			if (startingCard != null && startingCard.GetActor() != null)
			{
				startingCard.GetActor().SetActorState(ActorStateType.CARD_IDLE);
				startingCard.GetActor().TurnOffCollider();
				startingCard.GetActor().GetMeshRenderer().gameObject.layer = 8;
				if (startingCard.GetActor().m_nameTextMesh != null)
				{
					startingCard.GetActor().m_nameTextMesh.UpdateNow();
				}
			}
			else if (startingCard == null)
			{
				shouldSendTelemetry = true;
			}
			if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS))
			{
				pendingHeroCount++;
				if (startingCard != null && startingCard.GetActor() != null)
				{
					startingCard.GetActor().gameObject.SetActive(value: false);
					AssetLoader.Get().InstantiatePrefab(GameState.Get().GetStringGameOption(GameEntityOption.ALTERNATE_MULLIGAN_ACTOR_NAME), OnHeroActorLoaded, startingCard, AssetLoadingOptions.IgnorePrefabPosition);
				}
			}
		}
		if (shouldSendTelemetry)
		{
			string text = "ShowMultiplayerWaitingArea - Found a null card within starting hero cards during initialization. Starting Cards: ";
			for (int j = 0; j < m_startingCards.Count; j++)
			{
				text += ((m_startingCards[j] == null) ? "NULL" : m_startingCards[j].GetEntity().GetName());
				text += ((j == m_startingCards.Count - 1) ? "." : ", ");
			}
			TelemetryManager.Client().SendLiveIssue("Gameplay_MulliganManager", text);
			Log.MulliganManager.PrintWarning(text);
			shouldSendTelemetry = false;
		}
		while (pendingHeroCount > 0)
		{
			yield return null;
		}
		float zoneWidth = startingHandZone.GetComponent<Collider>().bounds.size.x;
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			zoneWidth *= 0.55f;
		}
		int numFakeCardsOnLeft = GameState.Get().GetGameEntity().GetNumberOfFakeMulliganCardsToShowOnLeft(m_startingCards.Count);
		int numFakeCardsOnRight = GameState.Get().GetGameEntity().GetNumberOfFakeMulliganCardsToShowOnRight(m_startingCards.Count);
		if (!isMulliganOver)
		{
			if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS))
			{
				pendingFakeHeroCount = numFakeCardsOnLeft + numFakeCardsOnRight;
				for (int k = 0; k < numFakeCardsOnLeft; k++)
				{
					AssetLoader.Get().InstantiatePrefab(GameState.Get().GetStringGameOption(GameEntityOption.ALTERNATE_MULLIGAN_ACTOR_NAME), OnFakeHeroActorLoaded, fakeCardsOnLeft, AssetLoadingOptions.IgnorePrefabPosition);
				}
				for (int l = 0; l < numFakeCardsOnRight; l++)
				{
					AssetLoader.Get().InstantiatePrefab(GameState.Get().GetStringGameOption(GameEntityOption.ALTERNATE_MULLIGAN_ACTOR_NAME), OnFakeHeroActorLoaded, fakeCardsOnRight, AssetLoadingOptions.IgnorePrefabPosition);
				}
			}
			while (pendingFakeHeroCount > 0)
			{
				yield return null;
			}
		}
		else
		{
			numFakeCardsOnLeft = 0;
			numFakeCardsOnRight = 0;
		}
		float spaceForEachCard = zoneWidth / (float)Mathf.Max(m_startingCards.Count + numFakeCardsOnLeft + numFakeCardsOnRight, 1);
		float spacingToUse = spaceForEachCard;
		float leftSideOfZone = startingHandZone.transform.position.x - zoneWidth / 2f;
		float rightSideOfZone = startingHandZone.transform.position.x + zoneWidth / 2f;
		float timingBonus = 0.1f;
		int numCardsToDealExcludingBonusCard = m_startingCards.Count;
		opposingSideHandZone.SetDoNotUpdateLayout(enable: false);
		opposingSideHandZone.UpdateLayout(null, forced: true, 3);
		float cardHeightOffset = 0f;
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			cardHeightOffset = 7f;
		}
		float cardZpos = startingHandZone.transform.position.z - 0.3f;
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			cardZpos = startingHandZone.transform.position.z - 0.2f;
		}
		float xOffset2 = spacingToUse / 2f;
		foreach (Actor item in fakeCardsOnLeft)
		{
			if (item != null)
			{
				GameObject card3 = item.gameObject;
				iTween.Stop(card3);
				Vector3[] array = new Vector3[3]
				{
					card3.transform.position,
					new Vector3(card3.transform.position.x, card3.transform.position.y + 3.6f, card3.transform.position.z),
					new Vector3(leftSideOfZone + xOffset2, friendlySideHandZone.transform.position.y + cardHeightOffset, cardZpos)
				};
				iTween.MoveTo(card3, iTween.Hash("path", array, "time", ANIMATION_TIME_DEAL_CARD, "easetype", iTween.EaseType.easeInSineOutExpo));
				if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS))
				{
					iTween.ScaleTo(card3, GameState.Get().GetGameEntity().GetAlternateMulliganActorScale(), ANIMATION_TIME_DEAL_CARD);
				}
				else
				{
					iTween.ScaleTo(card3, FRIENDLY_PLAYER_CARD_SCALE, ANIMATION_TIME_DEAL_CARD);
				}
				iTween.RotateTo(card3, iTween.Hash("rotation", new Vector3(0f, 0f, 0f), "time", ANIMATION_TIME_DEAL_CARD, "delay", ANIMATION_TIME_DEAL_CARD / 16f));
				yield return new WaitForSeconds(0.04f);
				SoundManager.Get().LoadAndPlay("FX_GameStart09_CardsOntoTable.prefab:da502e035813b5742a04d2ef4f588255", card3);
				xOffset2 += spacingToUse;
				yield return new WaitForSeconds(0.05f + timingBonus);
				timingBonus = 0f;
			}
		}
		for (int i = 0; i < numCardsToDealExcludingBonusCard; i++)
		{
			if (!(m_startingCards[i] == null))
			{
				GameObject card3 = m_startingCards[i].gameObject;
				if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS) && choiceHeroActors.ContainsKey(m_startingCards[i]))
				{
					card3 = choiceHeroActors[m_startingCards[i]].transform.parent.gameObject;
				}
				iTween.Stop(card3);
				Vector3[] array2 = new Vector3[3]
				{
					card3.transform.position,
					new Vector3(card3.transform.position.x, card3.transform.position.y + 3.6f, card3.transform.position.z),
					new Vector3(leftSideOfZone + xOffset2, friendlySideHandZone.transform.position.y + cardHeightOffset, cardZpos)
				};
				iTween.MoveTo(card3, iTween.Hash("path", array2, "time", ANIMATION_TIME_DEAL_CARD, "easetype", iTween.EaseType.easeInSineOutExpo));
				if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS))
				{
					iTween.ScaleTo(card3, GameState.Get().GetGameEntity().GetAlternateMulliganActorScale(), ANIMATION_TIME_DEAL_CARD);
				}
				else
				{
					iTween.ScaleTo(card3, FRIENDLY_PLAYER_CARD_SCALE, ANIMATION_TIME_DEAL_CARD);
				}
				iTween.RotateTo(card3, iTween.Hash("rotation", new Vector3(0f, 0f, 0f), "time", ANIMATION_TIME_DEAL_CARD, "delay", ANIMATION_TIME_DEAL_CARD / 16f));
				yield return new WaitForSeconds(0.04f);
				SoundManager.Get().LoadAndPlay("FX_GameStart09_CardsOntoTable.prefab:da502e035813b5742a04d2ef4f588255", card3);
				xOffset2 += spacingToUse;
				yield return new WaitForSeconds(0.05f + timingBonus);
				timingBonus = 0f;
			}
		}
		foreach (Actor item2 in fakeCardsOnRight)
		{
			if (item2 != null)
			{
				GameObject card3 = item2.gameObject;
				iTween.Stop(card3);
				Vector3[] array3 = new Vector3[3]
				{
					card3.transform.position,
					new Vector3(card3.transform.position.x, card3.transform.position.y + 3.6f, card3.transform.position.z),
					new Vector3(leftSideOfZone + xOffset2, friendlySideHandZone.transform.position.y + cardHeightOffset, cardZpos)
				};
				iTween.MoveTo(card3, iTween.Hash("path", array3, "time", ANIMATION_TIME_DEAL_CARD, "easetype", iTween.EaseType.easeInSineOutExpo));
				if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS))
				{
					iTween.ScaleTo(card3, GameState.Get().GetGameEntity().GetAlternateMulliganActorScale(), ANIMATION_TIME_DEAL_CARD);
				}
				else
				{
					iTween.ScaleTo(card3, FRIENDLY_PLAYER_CARD_SCALE, ANIMATION_TIME_DEAL_CARD);
				}
				iTween.RotateTo(card3, iTween.Hash("rotation", new Vector3(0f, 0f, 0f), "time", ANIMATION_TIME_DEAL_CARD, "delay", ANIMATION_TIME_DEAL_CARD / 16f));
				yield return new WaitForSeconds(0.04f);
				SoundManager.Get().LoadAndPlay("FX_GameStart09_CardsOntoTable.prefab:da502e035813b5742a04d2ef4f588255", card3);
				xOffset2 += spacingToUse;
				yield return new WaitForSeconds(0.05f + timingBonus);
				timingBonus = 0f;
			}
		}
		if (skipCardChoosing)
		{
			mulliganChooseBanner = UnityEngine.Object.Instantiate(mulliganChooseBannerPrefab);
			SetMulliganBannerText(GameStrings.Get("GAMEPLAY_MULLIGAN_STARTING_HAND"));
			Vector3 position = Board.Get().FindBone("ChoiceBanner").position;
			mulliganChooseBanner.transform.position = position;
			Vector3 localScale = mulliganChooseBanner.transform.localScale;
			mulliganChooseBanner.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
			iTween.ScaleTo(mulliganChooseBanner, localScale, 0.5f);
			m_ShrinkStartingHandBanner = ShrinkStartingHandBanner(mulliganChooseBanner);
			StartCoroutine(m_ShrinkStartingHandBanner);
			ShowMulliganDetail();
		}
		yield return new WaitForSeconds(1.1f);
		while (GameState.Get().IsBusy())
		{
			yield return null;
		}
		if (friendlyPlayerGoesFirst)
		{
			xOffset2 = 0f;
			for (int num = m_startingCards.Count - 1; num >= 0; num--)
			{
				if (m_startingCards[num] != null)
				{
					GameObject target = m_startingCards[num].gameObject;
					if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS) && choiceHeroActors.ContainsKey(m_startingCards[num]))
					{
						target = choiceHeroActors[m_startingCards[num]].gameObject;
					}
					iTween.Stop(target);
					iTween.MoveTo(target, iTween.Hash("position", new Vector3(rightSideOfZone - spaceForEachCard - xOffset2 + spaceForEachCard / 2f, friendlySideHandZone.transform.position.y + cardHeightOffset, cardZpos), "time", 14f / 15f, "easetype", iTween.EaseType.easeInOutCubic));
					xOffset2 += spaceForEachCard;
				}
			}
		}
		GameState.Get().GetGameEntity().OnMulliganCardsDealt(m_startingCards);
		yield return new WaitForSeconds(0.6f);
		if (skipCardChoosing)
		{
			if (GameState.Get().IsMulliganPhase() || GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_HAS_HERO_LOBBY))
			{
				if (GameState.Get().IsFriendlySidePlayerTurn())
				{
					TurnStartManager.Get().BeginListeningForTurnEvents();
				}
				m_WaitForOpponentToFinishMulligan = WaitForOpponentToFinishMulligan();
				StartCoroutine(m_WaitForOpponentToFinishMulligan);
			}
			else
			{
				yield return new WaitForSeconds(2f);
				EndMulligan();
			}
			yield break;
		}
		foreach (Card startingCard2 in m_startingCards)
		{
			if (startingCard2 != null)
			{
				if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS) && choiceHeroActors.ContainsKey(startingCard2))
				{
					choiceHeroActors[startingCard2].TurnOnCollider();
				}
				else
				{
					startingCard2.GetActor().TurnOnCollider();
				}
			}
			else
			{
				shouldSendTelemetry = true;
			}
		}
		if (shouldSendTelemetry)
		{
			string text2 = "ShowMultiplayerWaitingArea - Found a null card in starting cards while enabling colliders. Starting cards: ";
			for (int m = 0; m < m_startingCards.Count; m++)
			{
				text2 += ((m_startingCards[m] == null) ? "NULL" : m_startingCards[m].GetEntity().GetName());
				text2 += ((m == m_startingCards.Count - 1) ? "." : ", ");
			}
			TelemetryManager.Client().SendLiveIssue("Gameplay_MulliganManager", text2);
			Log.MulliganManager.PrintWarning(text2);
		}
		string mulliganBannerText = GameState.Get().GetGameEntity().GetMulliganBannerText();
		string mulliganBannerSubtitleText = GameState.Get().GetGameEntity().GetMulliganBannerSubtitleText();
		mulliganChooseBanner = UnityEngine.Object.Instantiate(mulliganChooseBannerPrefab, Board.Get().FindBone("ChoiceBanner").position, new Quaternion(0f, 0f, 0f, 0f));
		SetMulliganBannerText(mulliganBannerText, mulliganBannerSubtitleText);
		ShowMulliganDetail();
		if (GameState.Get().IsInChoiceMode() && GameMgr.Get().IsSpectator())
		{
			m_replaceLabels = new List<MulliganReplaceLabel>();
			for (int n = 0; n < m_startingCards.Count; n++)
			{
				if (m_startingCards[n] != null)
				{
					InputManager.Get().DoNetworkResponse(m_startingCards[n].GetEntity());
				}
				m_replaceLabels.Add(null);
			}
		}
		while (mulliganButton == null && GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_REQUIRES_CONFIRMATION))
		{
			yield return null;
		}
		if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_REQUIRES_CONFIRMATION))
		{
			mulliganButton.transform.position = new Vector3(startingHandZone.transform.position.x, friendlySideHandZone.transform.position.y, myHeroCardActor.transform.position.z);
			mulliganButton.transform.localEulerAngles = new Vector3(90f, 90f, 90f);
			mulliganButton.AddEventListener(UIEventType.RELEASE, OnMulliganButtonReleased);
			mulliganButtonWidget.transform.position = new Vector3(startingHandZone.transform.position.x, friendlySideHandZone.transform.position.y, myHeroCardActor.transform.position.z);
			mulliganButtonWidget.AddEventListener(UIEventType.RELEASE, OnMulliganButtonReleased);
			m_WaitAFrameBeforeSendingEventToMulliganButton = WaitAFrameBeforeSendingEventToMulliganButton();
			StartCoroutine(m_WaitAFrameBeforeSendingEventToMulliganButton);
			if (!GameMgr.Get().IsSpectator() && !Options.Get().GetBool(Option.HAS_SEEN_MULLIGAN, defaultVal: false) && !GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_IS_CHOOSE_ONE) && UserAttentionManager.CanShowAttentionGrabber("MulliganManager.DealStartingCards:" + Option.HAS_SEEN_MULLIGAN))
			{
				innkeeperMulliganDialog = NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, new Vector3(155.3f, NotificationManager.DEPTH, 34.5f), GameStrings.Get("VO_INNKEEPER_MULLIGAN_13"), "VO_INNKEEPER_MULLIGAN_13.prefab:3ec6b2e741ac16d4ca519bdfd26d10e3");
				Options.Get().SetBool(Option.HAS_SEEN_MULLIGAN, val: true);
				mulliganButton.GetComponent<Collider>().enabled = false;
			}
		}
		GameState.Get().GetGameEntity().StartMulliganSoundtracks(soft: true);
		m_waitingForUserInput = true;
		while (innkeeperMulliganDialog != null)
		{
			yield return null;
		}
		if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_REQUIRES_CONFIRMATION))
		{
			mulliganButton.GetComponent<Collider>().enabled = true;
		}
		if (skipCardChoosing || Cheats.Get().ShouldSkipMulligan())
		{
			BeginDealNewCards();
		}
	}

	private IEnumerator DealStartingCards()
	{
		yield return new WaitForSeconds(1f);
		while (!introComplete)
		{
			yield return null;
		}
		yield return StartCoroutine(GameState.Get().GetGameEntity().DoActionsAfterIntroBeforeMulligan());
		if (GameState.Get().GetBooleanGameOption(GameEntityOption.DO_OPENING_TAUNTS) && !Cheats.Get().ShouldSkipMulligan())
		{
			m_PlayStartingTaunts = PlayStartingTaunts();
			StartCoroutine(m_PlayStartingTaunts);
		}
		Player friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		friendlyPlayerGoesFirst = friendlySidePlayer.HasTag(GAME_TAG.FIRST_PLAYER);
		GetStartingLists();
		if (m_startingCards.Count == 0)
		{
			SkipCardChoosing();
		}
		foreach (Card startingCard in m_startingCards)
		{
			startingCard.GetActor().SetActorState(ActorStateType.CARD_IDLE);
			startingCard.GetActor().TurnOffCollider();
			startingCard.GetActor().GetMeshRenderer().gameObject.layer = 8;
			startingCard.GetActor().m_nameTextMesh.UpdateNow();
		}
		float num = startingHandZone.GetComponent<Collider>().bounds.size.x;
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			num *= 0.55f;
		}
		float spaceForEachCard = num / (float)m_startingCards.Count;
		float num2 = num / (float)(m_startingCards.Count + 1);
		float spacingToUse = num2;
		float leftSideOfZone = startingHandZone.transform.position.x - num / 2f;
		float rightSideOfZone = startingHandZone.transform.position.x + num / 2f;
		float timingBonus = 0.1f;
		int numCardsToDealExcludingBonusCard = m_startingCards.Count;
		if (!friendlyPlayerGoesFirst)
		{
			numCardsToDealExcludingBonusCard = m_bonusCardIndex;
			spacingToUse = spaceForEachCard;
		}
		else if (m_startingOppCards.Count > 0)
		{
			m_startingOppCards[m_bonusCardIndex].SetDoNotSort(on: true);
			if (m_coinCardIndex >= 0)
			{
				m_startingOppCards[m_coinCardIndex].SetDoNotSort(on: true);
			}
		}
		opposingSideHandZone.SetDoNotUpdateLayout(enable: false);
		opposingSideHandZone.UpdateLayout(null, forced: true, 3);
		float cardHeightOffset = 0f;
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			cardHeightOffset = 7f;
		}
		float cardZpos = startingHandZone.transform.position.z - 0.3f;
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			cardZpos = startingHandZone.transform.position.z - 0.2f;
		}
		yield return StartCoroutine(GameState.Get().GetGameEntity().DoActionsBeforeDealingBaseMulliganCards());
		float xOffset2 = spacingToUse / 2f;
		for (int i = 0; i < numCardsToDealExcludingBonusCard; i++)
		{
			GameObject topCard = m_startingCards[i].gameObject;
			iTween.Stop(topCard);
			Vector3[] array = new Vector3[3]
			{
				topCard.transform.position,
				new Vector3(topCard.transform.position.x, topCard.transform.position.y + 3.6f, topCard.transform.position.z),
				new Vector3(leftSideOfZone + xOffset2, friendlySideHandZone.transform.position.y + cardHeightOffset, cardZpos)
			};
			iTween.MoveTo(topCard, iTween.Hash("path", array, "time", ANIMATION_TIME_DEAL_CARD, "easetype", iTween.EaseType.easeInSineOutExpo));
			iTween.ScaleTo(topCard, FRIENDLY_PLAYER_CARD_SCALE, ANIMATION_TIME_DEAL_CARD);
			iTween.RotateTo(topCard, iTween.Hash("rotation", new Vector3(0f, 0f, 0f), "time", ANIMATION_TIME_DEAL_CARD, "delay", ANIMATION_TIME_DEAL_CARD / 16f));
			yield return new WaitForSeconds(0.04f);
			SoundManager.Get().LoadAndPlay("FX_GameStart09_CardsOntoTable.prefab:da502e035813b5742a04d2ef4f588255", topCard);
			xOffset2 += spacingToUse;
			yield return new WaitForSeconds(0.05f + timingBonus);
			timingBonus = 0f;
		}
		if (skipCardChoosing)
		{
			mulliganChooseBanner = UnityEngine.Object.Instantiate(mulliganChooseBannerPrefab);
			SetMulliganBannerText(GameStrings.Get("GAMEPLAY_MULLIGAN_STARTING_HAND"));
			Vector3 position = Board.Get().FindBone("ChoiceBanner").position;
			mulliganChooseBanner.transform.position = position;
			Vector3 localScale = mulliganChooseBanner.transform.localScale;
			mulliganChooseBanner.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
			iTween.ScaleTo(mulliganChooseBanner, localScale, 0.5f);
			m_ShrinkStartingHandBanner = ShrinkStartingHandBanner(mulliganChooseBanner);
			StartCoroutine(m_ShrinkStartingHandBanner);
		}
		yield return new WaitForSeconds(1.1f);
		yield return StartCoroutine(GameState.Get().GetGameEntity().DoActionsAfterDealingBaseMulliganCards());
		yield return StartCoroutine(GameState.Get().GetGameEntity().DoActionsBeforeCoinFlip());
		if (coinObject != null)
		{
			Transform transform = Board.Get().FindBone("MulliganCoinPosition");
			coinObject.transform.position = transform.position;
			coinObject.transform.localEulerAngles = transform.localEulerAngles;
			coinObject.SetActive(value: true);
			coinObject.GetComponent<CoinEffect>().DoAnim(friendlyPlayerGoesFirst);
			SoundManager.Get().LoadAndPlay("FX_MulliganCoin03_CoinFlip.prefab:07015cb3f02713a45aa03fc3aa798778", coinObject);
			coinLocation = transform.position;
			AssetLoader.Get().InstantiatePrefab("MulliganResultText.prefab:0369b435afd2e344db21e58648f8636c", CoinTossTextCallback, null, AssetLoadingOptions.IgnorePrefabPosition);
			yield return new WaitForSeconds(2f);
		}
		yield return StartCoroutine(GameState.Get().GetGameEntity().DoActionsAfterCoinFlip());
		if (!friendlyPlayerGoesFirst)
		{
			GameObject topCard = m_startingCards[m_bonusCardIndex].gameObject;
			Vector3[] array2 = new Vector3[3]
			{
				topCard.transform.position,
				new Vector3(topCard.transform.position.x, topCard.transform.position.y + 3.6f, topCard.transform.position.z),
				new Vector3(leftSideOfZone + xOffset2, friendlySideHandZone.transform.position.y + cardHeightOffset, cardZpos)
			};
			iTween.MoveTo(topCard, iTween.Hash("path", array2, "time", ANIMATION_TIME_DEAL_CARD, "easetype", iTween.EaseType.easeInSineOutExpo));
			iTween.ScaleTo(topCard, FRIENDLY_PLAYER_CARD_SCALE, ANIMATION_TIME_DEAL_CARD);
			iTween.RotateTo(topCard, iTween.Hash("rotation", new Vector3(0f, 0f, 0f), "time", ANIMATION_TIME_DEAL_CARD, "delay", ANIMATION_TIME_DEAL_CARD / 8f));
			yield return new WaitForSeconds(0.04f);
			SoundManager.Get().LoadAndPlay("FX_GameStart20_CardDealSingle.prefab:0da693603ca05d846b9cfe26e9f0e3c7", topCard);
		}
		else if (m_startingOppCards.Count > 0)
		{
			m_startingOppCards[m_bonusCardIndex].SetDoNotSort(on: false);
			opposingSideHandZone.UpdateLayout(null, forced: true, 4);
		}
		yield return StartCoroutine(GameState.Get().GetGameEntity().DoActionsAfterDealingBonusCard());
		yield return new WaitForSeconds(1.75f);
		while (GameState.Get().IsBusy())
		{
			yield return null;
		}
		yield return StartCoroutine(GameState.Get().GetGameEntity().DoActionsBeforeSpreadingMulliganCards());
		if (friendlyPlayerGoesFirst)
		{
			xOffset2 = 0f;
			for (int num3 = m_startingCards.Count - 1; num3 >= 0; num3--)
			{
				GameObject target = m_startingCards[num3].gameObject;
				iTween.Stop(target);
				iTween.MoveTo(target, iTween.Hash("position", new Vector3(rightSideOfZone - spaceForEachCard - xOffset2 + spaceForEachCard / 2f, friendlySideHandZone.transform.position.y + cardHeightOffset, cardZpos), "time", 14f / 15f, "easetype", iTween.EaseType.easeInOutCubic));
				xOffset2 += spaceForEachCard;
			}
		}
		GameState.Get().GetGameEntity().OnMulliganCardsDealt(m_startingCards);
		yield return new WaitForSeconds(0.6f);
		yield return StartCoroutine(GameState.Get().GetGameEntity().DoActionsAfterSpreadingMulliganCards());
		if (skipCardChoosing)
		{
			if (GameState.Get().IsMulliganPhase())
			{
				if (GameState.Get().IsFriendlySidePlayerTurn())
				{
					TurnStartManager.Get().BeginListeningForTurnEvents();
				}
				m_WaitForOpponentToFinishMulligan = WaitForOpponentToFinishMulligan();
				StartCoroutine(m_WaitForOpponentToFinishMulligan);
			}
			else
			{
				yield return new WaitForSeconds(2f);
				EndMulligan();
			}
			yield break;
		}
		foreach (Card startingCard2 in m_startingCards)
		{
			startingCard2.GetActor().TurnOnCollider();
		}
		mulliganChooseBanner = UnityEngine.Object.Instantiate(mulliganChooseBannerPrefab, Board.Get().FindBone("ChoiceBanner").position, new Quaternion(0f, 0f, 0f, 0f));
		SetMulliganBannerText(GameStrings.Get("GAMEPLAY_MULLIGAN_STARTING_HAND"), GameStrings.Get("GAMEPLAY_MULLIGAN_SUBTITLE"));
		if (GameState.Get().IsInChoiceMode())
		{
			m_replaceLabels = new List<MulliganReplaceLabel>();
			for (int j = 0; j < m_startingCards.Count; j++)
			{
				InputManager.Get().DoNetworkResponse(m_startingCards[j].GetEntity());
				m_replaceLabels.Add(null);
			}
		}
		while (mulliganButton == null && !GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_REQUIRES_CONFIRMATION))
		{
			yield return null;
		}
		if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_REQUIRES_CONFIRMATION))
		{
			mulliganButton.transform.position = new Vector3(startingHandZone.transform.position.x, friendlySideHandZone.transform.position.y, myHeroCardActor.transform.position.z);
			mulliganButton.transform.localEulerAngles = new Vector3(90f, 90f, 90f);
			mulliganButton.AddEventListener(UIEventType.RELEASE, OnMulliganButtonReleased);
			mulliganButtonWidget.transform.position = new Vector3(startingHandZone.transform.position.x, friendlySideHandZone.transform.position.y, myHeroCardActor.transform.position.z);
			mulliganButtonWidget.AddEventListener(UIEventType.RELEASE, OnMulliganButtonReleased);
			m_WaitAFrameBeforeSendingEventToMulliganButton = WaitAFrameBeforeSendingEventToMulliganButton();
			StartCoroutine(m_WaitAFrameBeforeSendingEventToMulliganButton);
			if (!GameMgr.Get().IsSpectator() && !Options.Get().GetBool(Option.HAS_SEEN_MULLIGAN, defaultVal: false) && !GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_IS_CHOOSE_ONE) && UserAttentionManager.CanShowAttentionGrabber("MulliganManager.DealStartingCards:" + Option.HAS_SEEN_MULLIGAN))
			{
				innkeeperMulliganDialog = NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, new Vector3(155.3f, NotificationManager.DEPTH, 34.5f), GameStrings.Get("VO_INNKEEPER_MULLIGAN_13"), "VO_INNKEEPER_MULLIGAN_13.prefab:3ec6b2e741ac16d4ca519bdfd26d10e3");
				Options.Get().SetBool(Option.HAS_SEEN_MULLIGAN, val: true);
				mulliganButton.GetComponent<Collider>().enabled = false;
			}
		}
		GameState.Get().GetGameEntity().StartMulliganSoundtracks(soft: true);
		m_waitingForUserInput = true;
		while (innkeeperMulliganDialog != null)
		{
			yield return null;
		}
		if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_REQUIRES_CONFIRMATION))
		{
			mulliganButton.GetComponent<Collider>().enabled = true;
		}
		if (skipCardChoosing || Cheats.Get().ShouldSkipMulligan())
		{
			BeginDealNewCards();
		}
	}

	private IEnumerator WaitAFrameBeforeSendingEventToMulliganButton()
	{
		yield return null;
		mulliganButton.m_button.GetComponent<PlayMakerFSM>().SendEvent("Birth");
	}

	public bool IsMulliganTimerActive()
	{
		return m_mulliganTimer != null;
	}

	private void BeginMulliganCountdown(float endTimeStamp)
	{
		if (!m_waitingForUserInput && !GameState.Get().GetBooleanGameOption(GameEntityOption.ALWAYS_SHOW_MULLIGAN_TIMER))
		{
			return;
		}
		if (m_mulliganTimer == null)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(mulliganTimerPrefab);
			m_mulliganTimer = gameObject.GetComponent<MulliganTimer>();
			if (m_mulliganTimer == null)
			{
				UnityEngine.Object.Destroy(gameObject);
				return;
			}
		}
		m_mulliganTimer.SetEndTime(endTimeStamp);
	}

	private void StopMulliganCountdown()
	{
		DestroyMulliganTimer();
	}

	public GameObject GetMulliganBanner()
	{
		return mulliganChooseBanner;
	}

	public GameObject GetMulliganButton()
	{
		if (mulliganButton != null)
		{
			return mulliganButton.gameObject;
		}
		return null;
	}

	public Vector3 GetMulliganTimerPosition()
	{
		if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_TIMER_HAS_ALTERNATE_POSITION))
		{
			return GameState.Get().GetGameEntity().GetMulliganTimerAlternatePosition();
		}
		return mulliganButton.transform.position;
	}

	private void CoinTossTextCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		coinTossText = go;
		RenderUtils.SetAlpha(go, 1f);
		go.transform.position = coinLocation + new Vector3(0f, 0f, -1f);
		go.transform.eulerAngles = new Vector3(90f, 0f, 0f);
		string text2 = (go.transform.GetComponentInChildren<UberText>().Text = ((!friendlyPlayerGoesFirst) ? GameStrings.Get("GAMEPLAY_COIN_TOSS_LOST") : GameStrings.Get("GAMEPLAY_COIN_TOSS_WON")));
		GameState.Get().GetGameEntity().NotifyOfCoinFlipResult();
		m_AnimateCoinTossText = AnimateCoinTossText();
		StartCoroutine(m_AnimateCoinTossText);
	}

	private IEnumerator AnimateCoinTossText()
	{
		yield return new WaitForSeconds(1.8f);
		if (!(coinTossText == null))
		{
			iTween.FadeTo(coinTossText, 1f, 0.25f);
			iTween.MoveTo(coinTossText, coinTossText.transform.position + new Vector3(0f, 0.5f, 0f), 2f);
			yield return new WaitForSeconds(1.9f);
			while (GameState.Get().IsBusy())
			{
				yield return null;
			}
			if (!(coinTossText == null))
			{
				iTween.FadeTo(coinTossText, 0f, 1f);
				yield return new WaitForSeconds(0.1f);
				UnityEngine.Object.Destroy(coinTossText);
			}
		}
	}

	private MulliganReplaceLabel CreateNewUILabelAtCardPosition(MulliganReplaceLabel prefab, int cardPosition)
	{
		MulliganReplaceLabel mulliganReplaceLabel = UnityEngine.Object.Instantiate(prefab);
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			mulliganReplaceLabel.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
			mulliganReplaceLabel.transform.position = new Vector3(m_startingCards[cardPosition].transform.position.x, m_startingCards[cardPosition].transform.position.y + 0.3f, m_startingCards[cardPosition].transform.position.z - 1.1f);
		}
		else
		{
			mulliganReplaceLabel.transform.position = new Vector3(m_startingCards[cardPosition].transform.position.x, m_startingCards[cardPosition].transform.position.y + 0.3f, m_startingCards[cardPosition].transform.position.z - startingHandZone.GetComponent<Collider>().bounds.size.z / 2.6f);
		}
		return mulliganReplaceLabel;
	}

	public void SetAllMulliganCardsToHold()
	{
		foreach (Card card in friendlySideHandZone.GetCards())
		{
			InputManager.Get().DoNetworkResponse(card.GetEntity());
		}
	}

	private void ToggleHoldState(int startingCardsIndex, bool forceDisable = false)
	{
		if (!GameState.Get().IsInChoiceMode() || startingCardsIndex >= m_startingCards.Count || ((!forceDisable || (forceDisable && m_handCardsMarkedForReplace[startingCardsIndex])) && !InputManager.Get().DoNetworkResponse(m_startingCards[startingCardsIndex].GetEntity())))
		{
			return;
		}
		if (!GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_IS_CHOOSE_ONE))
		{
			if (forceDisable)
			{
				m_handCardsMarkedForReplace[startingCardsIndex] = false;
			}
			else
			{
				m_handCardsMarkedForReplace[startingCardsIndex] = !m_handCardsMarkedForReplace[startingCardsIndex];
			}
			if (!m_handCardsMarkedForReplace[startingCardsIndex])
			{
				SoundManager.Get().LoadAndPlay("GM_ChatWarning.prefab:41baa28576a71664eabd8712a198b67f");
				if (m_xLabels != null && m_xLabels[startingCardsIndex] != null)
				{
					UnityEngine.Object.Destroy(m_xLabels[startingCardsIndex]);
				}
				UnityEngine.Object.Destroy(m_replaceLabels[startingCardsIndex].gameObject);
			}
			else
			{
				SoundManager.Get().LoadAndPlay("HeroDropItem1.prefab:587232e6704b20942af1205d00cfc0f9");
				if (m_xLabels != null && m_xLabels[startingCardsIndex] != null)
				{
					UnityEngine.Object.Destroy(m_xLabels[startingCardsIndex]);
				}
				GameObject gameObject = UnityEngine.Object.Instantiate(mulliganXlabelPrefab);
				gameObject.transform.position = m_startingCards[startingCardsIndex].transform.position;
				gameObject.transform.rotation = m_startingCards[startingCardsIndex].transform.rotation;
				if (m_xLabels != null)
				{
					m_xLabels[startingCardsIndex] = gameObject;
				}
				if (m_replaceLabels != null)
				{
					m_replaceLabels[startingCardsIndex] = CreateNewUILabelAtCardPosition(mulliganReplaceLabelPrefab, startingCardsIndex);
				}
			}
		}
		else
		{
			if (forceDisable)
			{
				m_handCardsMarkedForReplace[startingCardsIndex] = false;
			}
			else
			{
				m_handCardsMarkedForReplace[startingCardsIndex] = !m_handCardsMarkedForReplace[startingCardsIndex];
			}
			if (!m_handCardsMarkedForReplace[startingCardsIndex])
			{
				SoundManager.Get().LoadAndPlay("GM_ChatWarning.prefab:41baa28576a71664eabd8712a198b67f");
			}
			else
			{
				SoundManager.Get().LoadAndPlay("HeroDropItem1.prefab:587232e6704b20942af1205d00cfc0f9");
			}
			if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS))
			{
				GameState.Get().GetGameEntity().ToggleAlternateMulliganActorHighlight(m_startingCards[startingCardsIndex], m_handCardsMarkedForReplace[startingCardsIndex]);
			}
		}
		if (!GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_REQUIRES_CONFIRMATION))
		{
			BeginDealNewCards();
		}
	}

	private void DestroyXobjects()
	{
		if (m_xLabels != null)
		{
			for (int i = 0; i < m_xLabels.Length; i++)
			{
				UnityEngine.Object.Destroy(m_xLabels[i]);
			}
			m_xLabels = null;
		}
	}

	private void DestroyChooseBanner()
	{
		if (!(mulliganChooseBanner == null))
		{
			UnityEngine.Object.Destroy(mulliganChooseBanner);
		}
	}

	private void DestroyDetailLabel()
	{
		if (mulliganDetailLabel != null)
		{
			UnityEngine.Object.Destroy(mulliganDetailLabel);
			mulliganDetailLabel = null;
		}
	}

	private void DestroyMulliganTimer()
	{
		if (!(m_mulliganTimer == null))
		{
			m_mulliganTimer.SelfDestruct();
			m_mulliganTimer = null;
		}
	}

	public void ToggleHoldState(Actor toggleActor)
	{
		bool flag = false;
		GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_IS_CHOOSE_ONE);
		List<Actor> list = new List<Actor>(fakeCardsOnLeft.Count + fakeCardsOnRight.Count);
		list.AddRange(fakeCardsOnLeft);
		list.AddRange(fakeCardsOnRight);
		if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS))
		{
			foreach (Actor item in list)
			{
				if (toggleActor == item)
				{
					flag = GameState.Get().GetGameEntity().ToggleAlternateMulliganActorHighlight(item);
				}
				else
				{
					GameState.Get().GetGameEntity().ToggleAlternateMulliganActorHighlight(item, false);
				}
			}
		}
		if (flag)
		{
			for (int i = 0; i < m_startingCards.Count; i++)
			{
				ToggleHoldState(i, forceDisable: true);
			}
			if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_IS_CHOOSE_ONE))
			{
				if (mulliganButtonWidget != null)
				{
					mulliganButtonWidget.SetEnabled(active: false);
					mulliganButtonWidget.gameObject.SetActive(value: false);
				}
			}
			else if (mulliganButton != null)
			{
				mulliganButton.SetEnabled(enabled: false);
				mulliganButton.gameObject.SetActive(value: false);
			}
			if (conditionalHelperTextLabel != null)
			{
				conditionalHelperTextLabel.gameObject.SetActive(value: true);
			}
			return;
		}
		if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_IS_CHOOSE_ONE))
		{
			if (mulliganButtonWidget != null)
			{
				mulliganButtonWidget.gameObject.SetActive(value: true);
			}
		}
		else if (mulliganButton != null)
		{
			mulliganButton.gameObject.SetActive(value: true);
		}
		if (conditionalHelperTextLabel != null)
		{
			conditionalHelperTextLabel.gameObject.SetActive(value: false);
		}
	}

	public void ToggleHoldState(Card toggleCard)
	{
		bool flag = false;
		bool booleanGameOption = GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_IS_CHOOSE_ONE);
		for (int i = 0; i < m_startingCards.Count; i++)
		{
			if (m_startingCards[i] == toggleCard)
			{
				ToggleHoldState(i);
			}
			else if (booleanGameOption)
			{
				ToggleHoldState(i, forceDisable: true);
			}
			flag |= m_handCardsMarkedForReplace[i];
		}
		List<Actor> list = new List<Actor>(fakeCardsOnLeft.Count + fakeCardsOnRight.Count);
		list.AddRange(fakeCardsOnLeft);
		list.AddRange(fakeCardsOnRight);
		if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_IS_CHOOSE_ONE))
		{
			if (mulliganButtonWidget != null)
			{
				mulliganButtonWidget.gameObject.SetActive(value: true);
			}
		}
		else if (mulliganButton != null)
		{
			mulliganButton.gameObject.SetActive(value: true);
		}
		if (conditionalHelperTextLabel != null)
		{
			conditionalHelperTextLabel.gameObject.SetActive(value: false);
		}
		if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS))
		{
			foreach (Actor item in list)
			{
				GameState.Get().GetGameEntity().ToggleAlternateMulliganActorHighlight(item, false);
			}
		}
		if (booleanGameOption && mulliganButton != null)
		{
			if (!flag)
			{
				mulliganButton.SetEnabled(enabled: false);
				mulliganButtonWidget.SetEnabled(active: false);
			}
			else
			{
				mulliganButton.SetEnabled(enabled: true);
				mulliganButtonWidget.SetEnabled(active: true);
			}
		}
	}

	public void ServerHasDealtReplacementCards(bool isFriendlySide)
	{
		if (isFriendlySide)
		{
			friendlyPlayerHasReplacementCards = true;
			if (GameState.Get().IsFriendlySidePlayerTurn())
			{
				TurnStartManager.Get().BeginListeningForTurnEvents();
			}
		}
		else
		{
			opponentPlayerHasReplacementCards = true;
		}
	}

	public void AutomaticContinueMulligan()
	{
		if (m_waitingForUserInput)
		{
			if (mulliganButton != null)
			{
				mulliganButton.SetEnabled(enabled: false);
			}
			if (mulliganButtonWidget != null)
			{
				mulliganButtonWidget.SetEnabled(active: false);
			}
			DestroyMulliganTimer();
			BeginDealNewCards();
		}
		else
		{
			SkipCardChoosing();
		}
	}

	private void OnMulliganButtonReleased(UIEvent e)
	{
		if (InputManager.Get().PermitDecisionMakingInput())
		{
			if (mulliganButton != null)
			{
				mulliganButton.SetEnabled(enabled: false);
			}
			if (mulliganButtonWidget != null)
			{
				mulliganButtonWidget.SetEnabled(active: false);
			}
			BeginDealNewCards();
		}
	}

	private void BeginDealNewCards()
	{
		GameState.Get().GetGameEntity().OnMulliganBeginDealNewCards();
		if (m_waitingForUserInput)
		{
			m_waitingForUserInput = false;
			m_RemoveOldCardsAnimation = RemoveOldCardsAnimation();
			StartCoroutine(m_RemoveOldCardsAnimation);
		}
	}

	private IEnumerator RemoveOldCardsAnimation()
	{
		m_waitingForUserInput = false;
		if (!GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_HAS_HERO_LOBBY))
		{
			DestroyMulliganTimer();
		}
		if (!GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_IS_CHOOSE_ONE))
		{
			SoundManager.Get().LoadAndPlay("FX_GameStart28_CardDismissWoosh2_v2.prefab:6eb21cb332351ea419772cb5ae32772a");
			DestroyXobjects();
		}
		else
		{
			SoundManager.Get().LoadAndPlay("BG_SelectHero.prefab:40cb8c418fca5f44391df4df2e9660cd");
		}
		Vector3 mulliganedCardsPosition = Board.Get().FindBone("MulliganedCardsPosition").position;
		if (!GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_HAS_HERO_LOBBY))
		{
			DestroyChooseBanner();
			DestroyDetailLabel();
		}
		else
		{
			m_UpdateChooseBanner = UpdateChooseBanner();
			StartCoroutine(m_UpdateChooseBanner);
		}
		if (!UniversalInputManager.UsePhoneUI || GameState.Get().GetBooleanGameOption(GameEntityOption.SUPPRESS_CLASS_NAMES))
		{
			Gameplay.Get().RemoveClassNames();
		}
		foreach (Card startingCard in m_startingCards)
		{
			startingCard.GetActor().SetActorState(ActorStateType.CARD_IDLE);
			startingCard.GetActor().ToggleForceIdle(bOn: true);
			startingCard.GetActor().TurnOffCollider();
		}
		hisHeroCardActor.SetActorState(ActorStateType.CARD_IDLE);
		hisHeroCardActor.ToggleForceIdle(bOn: true);
		Card heroPowerCard = GameState.Get().GetFriendlySidePlayer().GetHeroPowerCard();
		if (heroPowerCard != null && heroPowerCard.GetActor() != null)
		{
			heroPowerCard.GetActor().SetActorState(ActorStateType.CARD_IDLE);
			heroPowerCard.GetActor().ToggleForceIdle(bOn: true);
		}
		if (m_RemoveUIButtons != null)
		{
			StopCoroutine(m_RemoveUIButtons);
		}
		m_RemoveUIButtons = RemoveUIButtons();
		StartCoroutine(m_RemoveUIButtons);
		float TO_DECK_ANIMATION_TIME = 1.5f;
		if (!GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_IS_CHOOSE_ONE))
		{
			for (int j = 0; j < m_startingCards.Count; j++)
			{
				if (m_handCardsMarkedForReplace[j])
				{
					GameObject gameObject = m_startingCards[j].gameObject;
					Vector3[] array = new Vector3[4]
					{
						gameObject.transform.position,
						new Vector3(gameObject.transform.position.x + 2f, gameObject.transform.position.y - 1.7f, gameObject.transform.position.z),
						new Vector3(mulliganedCardsPosition.x, mulliganedCardsPosition.y, mulliganedCardsPosition.z),
						friendlySideDeck.transform.position
					};
					iTween.MoveTo(gameObject, iTween.Hash("path", array, "time", TO_DECK_ANIMATION_TIME, "easetype", iTween.EaseType.easeOutCubic));
					Animation animation = gameObject.GetComponent<Animation>();
					if (animation == null)
					{
						animation = gameObject.AddComponent<Animation>();
					}
					animation.AddClip(cardAnimatesFromBoardToDeck, "putCardBack");
					animation.Play("putCardBack");
					yield return new WaitForSeconds(0.5f);
				}
			}
		}
		if (!EndTurnButton.Get().IsDisabled)
		{
			InputManager.Get().DoEndTurnButton();
		}
		else
		{
			GameState.Get().SendChoices();
		}
		if (!GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_IS_CHOOSE_ONE))
		{
			friendlySideHandZone.AddInputBlocker();
			while (!friendlyPlayerHasReplacementCards)
			{
				yield return null;
			}
			friendlySideHandZone.RemoveInputBlocker();
			SortHand(friendlySideHandZone);
			List<Card> handZoneCards = friendlySideHandZone.GetCards();
			foreach (Card item in handZoneCards)
			{
				if (!IsCoinCard(item))
				{
					item.GetActor().SetActorState(ActorStateType.CARD_IDLE);
					item.GetActor().ToggleForceIdle(bOn: true);
					item.GetActor().TurnOffCollider();
				}
			}
			float num = startingHandZone.GetComponent<Collider>().bounds.size.x;
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				num *= 0.55f;
			}
			float spaceForEachCard = num / (float)m_startingCards.Count;
			float leftSideOfZone = startingHandZone.transform.position.x - num / 2f;
			float xOffset = 0f;
			float cardHeightOffset = 0f;
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				cardHeightOffset = 7f;
			}
			float cardZpos = startingHandZone.transform.position.z - 0.3f;
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				cardZpos = startingHandZone.transform.position.z - 0.2f;
			}
			for (int j = 0; j < m_startingCards.Count; j++)
			{
				if (m_handCardsMarkedForReplace[j])
				{
					GameObject topCard = handZoneCards[j].gameObject;
					iTween.Stop(topCard);
					iTween.MoveTo(topCard, iTween.Hash("position", new Vector3(leftSideOfZone + spaceForEachCard + xOffset - spaceForEachCard / 2f, friendlySideHandZone.GetComponent<Collider>().bounds.center.y, startingHandZone.transform.position.z), "time", 3f));
					Vector3[] array2 = new Vector3[4];
					array2[0] = topCard.transform.position;
					array2[1] = new Vector3(mulliganedCardsPosition.x, mulliganedCardsPosition.y, mulliganedCardsPosition.z);
					array2[3] = new Vector3(leftSideOfZone + spaceForEachCard + xOffset - spaceForEachCard / 2f, friendlySideHandZone.GetComponent<Collider>().bounds.center.y + cardHeightOffset, cardZpos);
					array2[2] = new Vector3(array2[3].x + 2f, array2[3].y - 1.7f, array2[3].z);
					iTween.MoveTo(topCard, iTween.Hash("path", array2, "time", TO_DECK_ANIMATION_TIME, "easetype", iTween.EaseType.easeInCubic));
					iTween.ScaleTo(topCard, FRIENDLY_PLAYER_CARD_SCALE, ANIMATION_TIME_DEAL_CARD);
					Animation animation2 = topCard.GetComponent<Animation>();
					if (animation2 == null)
					{
						animation2 = topCard.AddComponent<Animation>();
					}
					string text = "putCardBack";
					animation2.AddClip(cardAnimatesFromBoardToDeck, text);
					animation2[text].normalizedTime = 1f;
					animation2[text].speed = -1f;
					animation2.Play(text);
					yield return new WaitForSeconds(0.5f);
					if (topCard.GetComponent<AudioSource>() == null)
					{
						topCard.AddComponent<AudioSource>();
					}
					SoundManager.Get().LoadAndPlay("FX_GameStart30_CardReplaceSingle.prefab:aa2b215965bf6484da413a795c17e995", topCard);
				}
				xOffset += spaceForEachCard;
			}
			yield return new WaitForSeconds(1f);
			ShuffleDeck();
			yield return new WaitForSeconds(1.5f);
		}
		if (opponentPlayerHasReplacementCards && !GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_HAS_HERO_LOBBY))
		{
			EndMulligan();
			yield break;
		}
		m_WaitForOpponentToFinishMulligan = WaitForOpponentToFinishMulligan();
		StartCoroutine(WaitForOpponentToFinishMulligan());
	}

	private IEnumerator UpdateChooseBanner()
	{
		yield break;
	}

	private IEnumerator WaitForOpponentToFinishMulligan()
	{
		DestroyChooseBanner();
		DestroyDetailLabel();
		Vector3 position = Board.Get().FindBone("ChoiceBanner").position;
		Vector3 position2;
		Vector3 scale;
		if (!GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_HAS_HERO_LOBBY))
		{
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				position2 = new Vector3(position.x, friendlySideHandZone.transform.position.y + 1f, myHeroCardActor.transform.position.z + 6.8f);
				scale = new Vector3(2.5f, 2.5f, 2.5f);
			}
			else
			{
				position2 = new Vector3(position.x, friendlySideHandZone.transform.position.y, myHeroCardActor.transform.position.z + 0.4f);
				scale = new Vector3(1.4f, 1.4f, 1.4f);
			}
		}
		else
		{
			position2 = position;
			scale = new Vector3(1.4f, 1.4f, 1.4f);
		}
		mulliganChooseBanner = UnityEngine.Object.Instantiate(mulliganChooseBannerPrefab, position2, new Quaternion(0f, 0f, 0f, 0f));
		mulliganChooseBanner.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		iTween.ScaleTo(mulliganChooseBanner, scale, 0.4f);
		Actor yourHeroActor = null;
		if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_HAS_HERO_LOBBY))
		{
			GameState.Get().GetGameEntity().GetMulliganWaitingText();
			GameState.Get().GetGameEntity().GetMulliganWaitingSubtitleText();
			while (GameState.Get().GetPlayerInfoMap()[GameState.Get().GetFriendlyPlayerId()].GetPlayerHero() == null)
			{
				string mulliganWaitingText = GameState.Get().GetGameEntity().GetMulliganWaitingText();
				string mulliganWaitingSubtitleText = GameState.Get().GetGameEntity().GetMulliganWaitingSubtitleText();
				SetMulliganBannerText(mulliganWaitingText, mulliganWaitingSubtitleText);
				yield return new WaitForSeconds(0.5f);
			}
			if (m_startingCards.Count == 0)
			{
				m_startingCards.Add(GameState.Get().GetFriendlySidePlayer().GetHeroCard());
				foreach (Card startingCard in m_startingCards)
				{
					startingCard.GetActor().SetActorState(ActorStateType.CARD_IDLE);
					startingCard.GetActor().TurnOffCollider();
					startingCard.GetActor().GetMeshRenderer().gameObject.layer = 8;
					if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS))
					{
						pendingHeroCount++;
						startingCard.GetActor().gameObject.SetActive(value: false);
						AssetLoader.Get().InstantiatePrefab(GameState.Get().GetStringGameOption(GameEntityOption.ALTERNATE_MULLIGAN_ACTOR_NAME), OnHeroActorLoaded, startingCard, AssetLoadingOptions.IgnorePrefabPosition);
					}
				}
				while (pendingHeroCount > 0)
				{
					yield return null;
				}
			}
			foreach (Card startingCard2 in m_startingCards)
			{
				if (startingCard2.GetEntity().GetCardId() == GameState.Get().GetPlayerInfoMap()[GameState.Get().GetFriendlyPlayerId()].GetPlayerHero().GetCardId())
				{
					float num = startingHandZone.GetComponent<Collider>().bounds.size.x;
					if ((bool)UniversalInputManager.UsePhoneUI)
					{
						num *= 0.55f;
					}
					float num2 = num;
					float num3 = startingHandZone.transform.position.x - num / 2f;
					float num4 = 0f;
					if ((bool)UniversalInputManager.UsePhoneUI)
					{
						num4 = 7f;
					}
					float z = startingHandZone.transform.position.z - 0.3f;
					if ((bool)UniversalInputManager.UsePhoneUI)
					{
						z = startingHandZone.transform.position.z - 0.2f;
					}
					float num5 = num2 / 2f;
					GameObject gameObject = startingCard2.gameObject;
					if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS))
					{
						gameObject = choiceHeroActors[startingCard2].gameObject.transform.parent.gameObject;
						yourHeroActor = choiceHeroActors[startingCard2];
						yourHeroActor.GetCard().SetActor(yourHeroActor);
						yourHeroActor.GetCard().GetActor().Show();
						GameState.Get().GetGameEntity().ApplyMulliganActorLobbyStateChanges(yourHeroActor);
						((PlayerLeaderboardMainCardActor)yourHeroActor).UpdatePlayerNameText(GameState.Get().GetGameEntity().GetBestNameForPlayer(GameState.Get().GetFriendlySidePlayer().GetPlayerId()));
						myHeroCardActor = yourHeroActor;
					}
					iTween.Stop(gameObject);
					Vector3[] array = new Vector3[3]
					{
						gameObject.transform.position,
						new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 3.6f, gameObject.transform.position.z),
						new Vector3(num3 + num5, friendlySideHandZone.transform.position.y + num4, z)
					};
					iTween.MoveTo(gameObject, iTween.Hash("path", array, "time", ANIMATION_TIME_DEAL_CARD, "easetype", iTween.EaseType.easeInSineOutExpo));
					if ((bool)UniversalInputManager.UsePhoneUI)
					{
						iTween.ScaleTo(gameObject, new Vector3(0.9f, 1.1f, 0.9f), ANIMATION_TIME_DEAL_CARD);
					}
					else
					{
						iTween.ScaleTo(gameObject, new Vector3(1.2f, 1.1f, 1.2f), ANIMATION_TIME_DEAL_CARD);
					}
					iTween.RotateTo(gameObject, iTween.Hash("rotation", new Vector3(0f, 0f, 0f), "time", ANIMATION_TIME_DEAL_CARD, "delay", ANIMATION_TIME_DEAL_CARD / 16f));
				}
				else if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS))
				{
					choiceHeroActors[startingCard2].ActivateSpellBirthState(SpellType.DEATH);
					((PlayerLeaderboardMainCardActor)choiceHeroActors[startingCard2]).m_fullSelectionHighlight.SetActive(value: false);
				}
				else
				{
					startingCard2.FakeDeath();
				}
			}
			CleanupFakeCards();
			bool heroPowerCreated = false;
			do
			{
				if (!heroPowerCreated)
				{
					Card heroPowerCard = GameState.Get().GetFriendlySidePlayer().GetHeroPowerCard();
					if (heroPowerCard != null && heroPowerCard.GetActor() != null)
					{
						heroPowerCard.GetActor().SetActorState(ActorStateType.CARD_IDLE);
						heroPowerCard.GetActor().ToggleForceIdle(bOn: true);
						heroPowerCard.GetActor().TurnOffCollider();
						heroPowerCreated = true;
					}
				}
				string mulliganWaitingText = GameState.Get().GetGameEntity().GetMulliganWaitingText();
				string mulliganWaitingSubtitleText = GameState.Get().GetGameEntity().GetMulliganWaitingSubtitleText();
				SetMulliganBannerText(mulliganWaitingText, mulliganWaitingSubtitleText);
				yield return null;
			}
			while (!GameState.Get().GetGameEntity().IsHeroMulliganLobbyFinished());
			foreach (SharedPlayerInfo sph in GameState.Get().GetPlayerInfoMap().Values)
			{
				if (sph.GetPlayerId() != GameState.Get().GetFriendlyPlayerId())
				{
					pendingHeroCount++;
					while (sph.GetPlayerHero() == null)
					{
						yield return null;
					}
					AssetLoader.Get().InstantiatePrefab(GameState.Get().GetStringGameOption(GameEntityOption.ALTERNATE_MULLIGAN_LOBBY_ACTOR_NAME), OnOpponentHeroActorLoaded, sph.GetPlayerHero().GetCard(), AssetLoadingOptions.IgnorePrefabPosition);
				}
			}
			while (pendingHeroCount > 0)
			{
				yield return null;
			}
			yield return new WaitForSeconds(0.5f);
			DestroyMulliganTimer();
			DestroyChooseBanner();
			DestroyDetailLabel();
			Transform rootTransform = yourHeroActor.gameObject.transform.parent.parent;
			Transform yourHeroRoot = yourHeroActor.gameObject.transform.parent;
			Vector3 vsPosition = Board.Get().FindBone("VS_Position").position;
			yield return new WaitForSeconds(1f);
			iTween.Stop(yourHeroRoot.gameObject);
			int num6 = 1;
			foreach (Actor value in opponentHeroActors.Values)
			{
				value.gameObject.transform.parent = rootTransform;
				value.gameObject.transform.localScale = new Vector3(1.0506f, 1.0506f, 1.0506f);
				value.gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
				Vector3 position3 = Board.Get().FindBone("HeroSpawnLineUp0" + num6++).position;
				value.gameObject.transform.position = position3;
				((PlayerLeaderboardMainCardActor)value).SetAlternateNameTextActive(active: false);
				SharedPlayerInfo playerForCard = GetPlayerForCard(value.GetCard());
				if (playerForCard != null)
				{
					((PlayerLeaderboardMainCardActor)value).UpdatePlayerNameText(GameState.Get().GetGameEntity().GetBestNameForPlayer(playerForCard.GetPlayerId()));
				}
			}
			yourHeroActor.transform.parent = null;
			yourHeroRoot.position = new Vector3(-7.7726f, 0.0055918f, -8.054f);
			yourHeroRoot.localScale = new Vector3(1.134f, 1.134f, 1.134f);
			yourHeroActor.transform.parent = yourHeroRoot;
			yourHeroActor.GetComponent<PlayMakerFSM>().SendEvent(UniversalInputManager.UsePhoneUI ? "SlotInHeroAfterFlyIn_Phone" : "SlotInHeroAfterFlyIn");
			yield return new WaitForSeconds(1f);
			if ((bool)versusText)
			{
				versusText.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
				versusText.transform.position = vsPosition;
			}
			yield return new WaitForSeconds(1.5f);
			int num7 = 1;
			foreach (Actor value2 in opponentHeroActors.Values)
			{
				PlayMakerFSM component = value2.GetComponent<PlayMakerFSM>();
				component.FsmVariables.GetFsmInt("Player").Value = num7++;
				component.SendEvent(UniversalInputManager.UsePhoneUI ? "Spawn_Phone" : "Spawn");
			}
			yield return new WaitForSeconds(1.5f);
			if ((bool)versusText)
			{
				yield return new WaitForSeconds(0.1f);
				versusText.FadeOut();
				yield return new WaitForSeconds(0.32f);
			}
			foreach (Actor value3 in opponentHeroActors.Values)
			{
				value3.GetComponent<PlayMakerFSM>().SendEvent(UniversalInputManager.UsePhoneUI ? "FlyIn_Phone" : "FlyIn");
			}
			PlayerLeaderboardManager.Get().UpdateLayout(animate: false);
			yield return new WaitForSeconds(1.5f);
			if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS))
			{
				GameState.Get().GetGameEntity().ClearMulliganActorStateChanges(yourHeroActor);
			}
			foreach (Actor value4 in opponentHeroActors.Values)
			{
				value4.gameObject.SetActive(value: false);
			}
		}
		else
		{
			SetMulliganBannerText(GameStrings.Get("GAMEPLAY_MULLIGAN_WAITING"));
			mulliganChooseBanner.GetComponent<Banner>().MoveGlowForBottomPlacement();
			while (!opponentPlayerHasReplacementCards && !GameState.Get().IsGameOver())
			{
				yield return null;
			}
		}
		EndMulligan();
	}

	private SharedPlayerInfo GetPlayerForCard(Card card)
	{
		foreach (SharedPlayerInfo value in GameState.Get().GetPlayerInfoMap().Values)
		{
			if (card.GetEntity().GetCardId() == value.GetPlayerHero().GetCardId())
			{
				return value;
			}
		}
		return null;
	}

	private void SetMulliganBannerText(string title)
	{
		SetMulliganBannerText(title, null);
	}

	private void SetMulliganBannerText(string title, string subtitle)
	{
		if (!(mulliganChooseBanner == null))
		{
			if (subtitle != null)
			{
				mulliganChooseBanner.GetComponent<Banner>().SetText(title, subtitle);
			}
			else
			{
				mulliganChooseBanner.GetComponent<Banner>().SetText(title);
			}
		}
	}

	private void SetMulliganDetailLabelText(string title)
	{
		if (!(mulliganDetailLabel == null))
		{
			mulliganDetailLabel.GetComponent<UberText>().Text = title;
		}
	}

	private void ShowMulliganDetail()
	{
		if (GameState.Get().GetBooleanGameOption(GameEntityOption.DISPLAY_MULLIGAN_DETAIL_LABEL))
		{
			string mulliganDetailText = GameState.Get().GetGameEntity().GetMulliganDetailText();
			if (mulliganDetailText != null)
			{
				mulliganDetailLabel = UnityEngine.Object.Instantiate(mulliganDetailLabelPrefab);
				mulliganDetailLabel.transform.position = Board.Get().FindBone("MulliganDetail").position;
				SetMulliganDetailLabelText(mulliganDetailText);
			}
		}
	}

	private IEnumerator RemoveUIButtons()
	{
		if (mulliganButton != null)
		{
			mulliganButton.m_button.GetComponent<PlayMakerFSM>().SendEvent("Death");
		}
		if (mulliganButtonWidget != null)
		{
			mulliganButtonWidget.gameObject.SetActive(value: false);
		}
		if (m_replaceLabels != null)
		{
			for (int i = 0; i < m_replaceLabels.Count; i++)
			{
				if (m_replaceLabels[i] != null)
				{
					iTween.RotateTo(m_replaceLabels[i].gameObject, iTween.Hash("rotation", new Vector3(0f, 0f, 0f), "time", 0.5f, "easetype", iTween.EaseType.easeInExpo));
					iTween.ScaleTo(m_replaceLabels[i].gameObject, iTween.Hash("scale", new Vector3(0.001f, 0.001f, 0.001f), "time", 0.5f, "easetype", iTween.EaseType.easeInExpo, "oncomplete", "DestroyButton", "oncompletetarget", base.gameObject, "oncompleteparams", m_replaceLabels[i]));
					yield return new WaitForSeconds(0.05f);
				}
			}
		}
		yield return new WaitForSeconds(3.5f);
		if (mulliganButton != null)
		{
			UnityEngine.Object.Destroy(mulliganButton.gameObject);
		}
		if (mulliganButtonWidget != null)
		{
			UnityEngine.Object.Destroy(mulliganButtonWidget.gameObject);
		}
	}

	private void DestroyButton(UnityEngine.Object buttonToDestroy)
	{
		UnityEngine.Object.Destroy(buttonToDestroy);
	}

	private void HandleGameOverDuringMulligan()
	{
		if (m_WaitForBoardThenLoadButton != null)
		{
			StopCoroutine(m_WaitForBoardThenLoadButton);
		}
		m_WaitForBoardThenLoadButton = null;
		if (m_WaitForHeroesAndStartAnimations != null)
		{
			StopCoroutine(m_WaitForHeroesAndStartAnimations);
		}
		m_WaitForHeroesAndStartAnimations = null;
		if (m_ResumeMulligan != null)
		{
			StopCoroutine(m_ResumeMulligan);
		}
		m_ResumeMulligan = null;
		if (m_DealStartingCards != null)
		{
			StopCoroutine(m_DealStartingCards);
		}
		m_DealStartingCards = null;
		if (m_ShowMultiplayerWaitingArea != null)
		{
			StopCoroutine(m_ShowMultiplayerWaitingArea);
		}
		m_ShowMultiplayerWaitingArea = null;
		if (m_RemoveOldCardsAnimation != null)
		{
			StopCoroutine(m_RemoveOldCardsAnimation);
		}
		m_RemoveOldCardsAnimation = null;
		if (m_PlayStartingTaunts != null)
		{
			StopCoroutine(m_PlayStartingTaunts);
		}
		m_PlayStartingTaunts = null;
		if (m_Spectator_WaitForFriendlyPlayerThenProcessEntitiesChosen != null)
		{
			StopCoroutine(m_Spectator_WaitForFriendlyPlayerThenProcessEntitiesChosen);
		}
		m_Spectator_WaitForFriendlyPlayerThenProcessEntitiesChosen = null;
		if (m_ContinueMulliganWhenBoardLoads != null)
		{
			StopCoroutine(m_ContinueMulliganWhenBoardLoads);
		}
		m_ContinueMulliganWhenBoardLoads = null;
		if (m_WaitAFrameBeforeSendingEventToMulliganButton != null)
		{
			StopCoroutine(m_WaitAFrameBeforeSendingEventToMulliganButton);
		}
		m_WaitAFrameBeforeSendingEventToMulliganButton = null;
		if (m_ShrinkStartingHandBanner != null)
		{
			StopCoroutine(m_ShrinkStartingHandBanner);
		}
		m_ShrinkStartingHandBanner = null;
		if (m_AnimateCoinTossText != null)
		{
			StopCoroutine(m_AnimateCoinTossText);
		}
		m_AnimateCoinTossText = null;
		if (m_WaitForOpponentToFinishMulligan != null)
		{
			StopCoroutine(m_WaitForOpponentToFinishMulligan);
		}
		m_WaitForOpponentToFinishMulligan = null;
		if (m_EndMulliganWithTiming != null)
		{
			StopCoroutine(m_EndMulliganWithTiming);
		}
		m_EndMulliganWithTiming = null;
		if (m_HandleCoinCard != null)
		{
			StopCoroutine(m_HandleCoinCard);
		}
		m_HandleCoinCard = null;
		if (m_EnableHandCollidersAfterCardsAreDealt != null)
		{
			StopCoroutine(m_EnableHandCollidersAfterCardsAreDealt);
		}
		m_EnableHandCollidersAfterCardsAreDealt = null;
		if (m_SkipMulliganForResume != null)
		{
			StopCoroutine(m_SkipMulliganForResume);
		}
		m_SkipMulliganForResume = null;
		if (m_SkipMulliganWhenIntroComplete != null)
		{
			StopCoroutine(m_SkipMulliganWhenIntroComplete);
		}
		m_SkipMulliganWhenIntroComplete = null;
		if (m_WaitForBoardAnimToCompleteThenStartTurn != null)
		{
			StopCoroutine(m_WaitForBoardAnimToCompleteThenStartTurn);
		}
		m_WaitForBoardAnimToCompleteThenStartTurn = null;
		if (m_customIntroCoroutine != null)
		{
			StopCoroutine(m_customIntroCoroutine);
			GameState.Get().GetGameEntity().OnCustomIntroCancelled(myHeroCardActor.GetCard(), hisHeroCardActor.GetCard(), myheroLabel, hisheroLabel, versusText);
			m_customIntroCoroutine = null;
		}
		m_waitingForUserInput = false;
		DestroyXobjects();
		DestroyChooseBanner();
		DestroyDetailLabel();
		DestroyMulliganTimer();
		if (coinObject != null)
		{
			UnityEngine.Object.Destroy(coinObject);
		}
		if (versusText != null)
		{
			UnityEngine.Object.Destroy(versusText.gameObject);
		}
		if (versusVo != null)
		{
			SoundManager.Get().Destroy(versusVo);
		}
		if (coinTossText != null)
		{
			UnityEngine.Object.Destroy(coinTossText);
		}
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			Gameplay.Get().RemoveNameBanners();
		}
		else
		{
			Gameplay.Get().RemoveClassNames();
		}
		if (m_RemoveUIButtons != null)
		{
			StopCoroutine(m_RemoveUIButtons);
		}
		m_RemoveUIButtons = RemoveUIButtons();
		StartCoroutine(m_RemoveUIButtons);
		if (mulliganButton != null)
		{
			mulliganButton.SetEnabled(enabled: false);
		}
		if (mulliganButtonWidget != null)
		{
			mulliganButtonWidget.SetEnabled(active: false);
		}
		DestoryHeroSkinSocketInEffects();
		if (myheroLabel != null && myheroLabel.isActiveAndEnabled)
		{
			myheroLabel.FadeOut();
		}
		if (hisheroLabel != null && hisheroLabel.isActiveAndEnabled)
		{
			hisheroLabel.FadeOut();
		}
		if (friendlySideHandZone != null)
		{
			foreach (Card card in friendlySideHandZone.GetCards())
			{
				Actor actor = card.GetActor();
				actor.SetActorState(ActorStateType.CARD_IDLE);
				actor.ToggleForceIdle(bOn: true);
				if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS) && GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_HAS_HERO_LOBBY))
				{
					actor.ActivateSpellBirthState(SpellType.DEATH);
					((PlayerLeaderboardMainCardActor)actor).m_fullSelectionHighlight.SetActive(value: false);
				}
			}
			if (hisHeroCardActor != null)
			{
				hisHeroCardActor.SetActorState(ActorStateType.CARD_IDLE);
				hisHeroCardActor.ToggleForceIdle(bOn: true);
			}
			Card heroPowerCard = GameState.Get().GetFriendlySidePlayer().GetHeroPowerCard();
			if (heroPowerCard != null && heroPowerCard.GetActor() != null)
			{
				heroPowerCard.GetActor().SetActorState(ActorStateType.CARD_IDLE);
				heroPowerCard.GetActor().ToggleForceIdle(bOn: true);
			}
			if (!friendlyPlayerGoesFirst && ShouldHandleCoinCard())
			{
				Card coinCardFromFriendlyHand = GetCoinCardFromFriendlyHand();
				coinCardFromFriendlyHand.SetDoNotSort(on: false);
				coinCardFromFriendlyHand.SetTransitionStyle(ZoneTransitionStyle.NORMAL);
				PutCoinCardInSpawnPosition(coinCardFromFriendlyHand);
				coinCardFromFriendlyHand.GetActor().Show();
			}
			friendlySideHandZone.ForceStandInUpdate();
			friendlySideHandZone.SetDoNotUpdateLayout(enable: false);
			friendlySideHandZone.UpdateLayout();
		}
		CleanupFakeCards();
		Board board = Board.Get();
		if (board != null)
		{
			board.RaiseTheLightsQuickly();
		}
		if (myHeroCardActor != null)
		{
			Animation component = myHeroCardActor.gameObject.GetComponent<Animation>();
			if (component != null)
			{
				component.Stop();
			}
			myHeroCardActor.transform.localScale = Vector3.one;
			myHeroCardActor.transform.rotation = Quaternion.identity;
			myHeroCardActor.transform.position = ZoneMgr.Get().FindZoneOfType<ZoneHero>(Player.Side.FRIENDLY).transform.position;
		}
		if (hisHeroCardActor != null)
		{
			Animation component2 = hisHeroCardActor.gameObject.GetComponent<Animation>();
			if (component2 != null)
			{
				component2.Stop();
			}
			hisHeroCardActor.transform.localScale = Vector3.one;
			hisHeroCardActor.transform.rotation = Quaternion.identity;
			hisHeroCardActor.transform.position = ZoneMgr.Get().FindZoneOfType<ZoneHero>(Player.Side.OPPOSING).transform.position;
		}
	}

	private void CleanupFakeCards()
	{
		List<Actor> list = new List<Actor>(fakeCardsOnLeft.Count + fakeCardsOnRight.Count);
		list.AddRange(fakeCardsOnLeft);
		list.AddRange(fakeCardsOnRight);
		foreach (Actor item in list)
		{
			item.ActivateSpellBirthState(SpellType.DEATH);
			if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS))
			{
				GameState.Get().GetGameEntity().ConfigureFakeMulliganCardActor(item, shown: false);
			}
		}
		if (conditionalHelperTextLabel != null)
		{
			conditionalHelperTextLabel.gameObject.SetActive(value: false);
		}
	}

	public void EndMulligan()
	{
		m_waitingForUserInput = false;
		if (m_replaceLabels != null)
		{
			for (int i = 0; i < m_replaceLabels.Count; i++)
			{
				UnityEngine.Object.Destroy(m_replaceLabels[i]);
			}
		}
		if (mulliganButton != null)
		{
			UnityEngine.Object.Destroy(mulliganButton.gameObject);
		}
		if (mulliganButtonWidget != null)
		{
			UnityEngine.Object.Destroy(mulliganButtonWidget.gameObject);
		}
		DestroyXobjects();
		DestroyChooseBanner();
		DestroyDetailLabel();
		if (versusText != null)
		{
			UnityEngine.Object.Destroy(versusText.gameObject);
		}
		if (versusVo != null)
		{
			SoundManager.Get().Destroy(versusVo);
		}
		if (coinTossText != null)
		{
			UnityEngine.Object.Destroy(coinTossText);
		}
		if (hisheroLabel != null)
		{
			hisheroLabel.FadeOut();
		}
		if (myheroLabel != null)
		{
			myheroLabel.FadeOut();
		}
		DestoryHeroSkinSocketInEffects();
		myHeroCardActor.transform.localPosition = new Vector3(0f, 0f, 0f);
		hisHeroCardActor.transform.localPosition = new Vector3(0f, 0f, 0f);
		myHeroCardActor.Show();
		if (!GameState.Get().IsGameOver())
		{
			myHeroCardActor.GetHealthObject().Show();
			hisHeroCardActor.GetHealthObject().Show();
			if (myHeroCardActor.GetAttackObject() != null)
			{
				myHeroCardActor.GetAttackObject().Show();
			}
			if (hisHeroCardActor.GetAttackObject() != null)
			{
				hisHeroCardActor.GetAttackObject().Show();
			}
			friendlySideHandZone.ForceStandInUpdate();
			friendlySideHandZone.SetDoNotUpdateLayout(enable: false);
			friendlySideHandZone.UpdateLayout();
			if (m_startingOppCards != null && m_startingOppCards.Count > 0)
			{
				m_startingOppCards[m_startingOppCards.Count - 1].SetDoNotSort(on: false);
			}
			opposingSideHandZone.SetDoNotUpdateLayout(enable: false);
			opposingSideHandZone.UpdateLayout();
			friendlySideDeck.SetSuppressEmotes(suppress: false);
			opposingSideDeck.SetSuppressEmotes(suppress: false);
			Board.Get().SplitSurface();
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				Gameplay.Get().RemoveNameBanners();
				Gameplay.Get().AddGamePlayNameBannerPhone();
			}
			if (m_MyCustomSocketInSpell != null)
			{
				UnityEngine.Object.Destroy(m_MyCustomSocketInSpell);
			}
			if (m_HisCustomSocketInSpell != null)
			{
				UnityEngine.Object.Destroy(m_HisCustomSocketInSpell);
			}
			m_EndMulliganWithTiming = EndMulliganWithTiming();
			StartCoroutine(m_EndMulliganWithTiming);
		}
	}

	private IEnumerator EndMulliganWithTiming()
	{
		if (ShouldHandleCoinCard())
		{
			m_HandleCoinCard = HandleCoinCard();
			yield return StartCoroutine(m_HandleCoinCard);
		}
		else
		{
			UnityEngine.Object.Destroy(coinObject);
		}
		myHeroCardActor.TurnOnCollider();
		hisHeroCardActor.TurnOnCollider();
		FadeOutMulliganMusicAndStartGameplayMusic();
		foreach (Card card in friendlySideHandZone.GetCards())
		{
			card.GetActor().TurnOnCollider();
			card.GetActor().ToggleForceIdle(bOn: false);
		}
		myHeroCardActor.ToggleForceIdle(bOn: false);
		hisHeroCardActor.ToggleForceIdle(bOn: false);
		Card heroPowerCard = GameState.Get().GetFriendlySidePlayer().GetHeroPowerCard();
		if (heroPowerCard != null && heroPowerCard.GetActor() != null)
		{
			heroPowerCard.GetActor().ToggleForceIdle(bOn: false);
		}
		if (!friendlyPlayerHasReplacementCards)
		{
			m_EnableHandCollidersAfterCardsAreDealt = EnableHandCollidersAfterCardsAreDealt();
			StartCoroutine(m_EnableHandCollidersAfterCardsAreDealt);
		}
		Board.Get().FindCollider("DragPlane").enabled = true;
		ForceMulliganActive(active: false);
		Board.Get().RaiseTheLights();
		FadeHeroPowerIn(GameState.Get().GetFriendlySidePlayer().GetHeroPowerCard());
		FadeHeroPowerIn(GameState.Get().GetOpposingSidePlayer().GetHeroPowerCard());
		InputManager.Get().OnMulliganEnded();
		EndTurnButton.Get().OnMulliganEnded();
		GameState.Get().GetGameEntity().NotifyOfMulliganEnded();
		m_WaitForBoardAnimToCompleteThenStartTurn = WaitForBoardAnimToCompleteThenStartTurn();
		StartCoroutine(m_WaitForBoardAnimToCompleteThenStartTurn);
	}

	private IEnumerator HandleCoinCard()
	{
		if (!friendlyPlayerGoesFirst)
		{
			if (coinObject != null && coinObject.activeSelf)
			{
				yield return new WaitForSeconds(0.5f);
				coinObject.GetComponentInChildren<PlayMakerFSM>().SendEvent("Birth");
				yield return new WaitForSeconds(0.1f);
			}
			if (!GameMgr.Get().IsSpectator() && !Options.Get().GetBool(Option.HAS_SEEN_THE_COIN, defaultVal: false) && UserAttentionManager.CanShowAttentionGrabber("MulliganManager.HandleCoinCard:" + Option.HAS_SEEN_THE_COIN))
			{
				NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, new Vector3(155.3f, NotificationManager.DEPTH, 34.5f), GameStrings.Get("VO_INNKEEPER_COIN_INTRO"), "VO_INNKEEPER_COIN_INTRO.prefab:6fb1b3b124d474c4c84e392646caada4");
				Options.Get().SetBool(Option.HAS_SEEN_THE_COIN, val: true);
			}
			Card coinCardFromFriendlyHand = GetCoinCardFromFriendlyHand();
			PutCoinCardInSpawnPosition(coinCardFromFriendlyHand);
			coinCardFromFriendlyHand.ActivateActorSpell(SpellType.SUMMON_IN, CoinCardSummonFinishedCallback);
			yield return new WaitForSeconds(1f);
		}
		else
		{
			UnityEngine.Object.Destroy(coinObject);
			if (m_coinCardIndex >= 0)
			{
				m_startingOppCards[m_coinCardIndex].SetDoNotSort(on: false);
			}
			opposingSideHandZone.UpdateLayout();
		}
	}

	private bool IsCoinCard(Card card)
	{
		return card.GetEntity().GetCardId() == CoinManager.Get().GetFavoriteCoinCardId();
	}

	private Card GetCoinCardFromFriendlyHand()
	{
		List<Card> cards = friendlySideHandZone.GetCards();
		if (cards.Count > 0)
		{
			return cards[cards.Count - 1];
		}
		Debug.LogError("GetCoinCardFromFriendlyHand() failed. friendlySideHandZone is empty.");
		return null;
	}

	private void PutCoinCardInSpawnPosition(Card coinCard)
	{
		coinCard.transform.position = Board.Get().FindBone("MulliganCoinCardSpawnPosition").position;
		coinCard.transform.localScale = Board.Get().FindBone("MulliganCoinCardSpawnPosition").localScale;
	}

	private bool ShouldHandleCoinCard()
	{
		if (!GameState.Get().IsMulliganPhase())
		{
			return false;
		}
		if (!GameState.Get().GetBooleanGameOption(GameEntityOption.HANDLE_COIN))
		{
			return false;
		}
		return true;
	}

	private void CoinCardSummonFinishedCallback(Spell spell, object userData)
	{
		Card card = SceneUtils.FindComponentInParents<Card>(spell);
		card.RefreshActor();
		card.UpdateActorComponents();
		card.SetDoNotSort(on: false);
		UnityEngine.Object.Destroy(coinObject);
		card.SetTransitionStyle(ZoneTransitionStyle.VERY_SLOW);
		friendlySideHandZone.UpdateLayout(null, forced: true);
	}

	private IEnumerator EnableHandCollidersAfterCardsAreDealt()
	{
		while (!friendlyPlayerHasReplacementCards)
		{
			yield return null;
		}
		foreach (Card card in friendlySideHandZone.GetCards())
		{
			card.GetActor().TurnOnCollider();
		}
	}

	public void SkipCardChoosing()
	{
		skipCardChoosing = true;
	}

	public void SkipMulliganForDev()
	{
		if (m_WaitForBoardThenLoadButton != null)
		{
			StopCoroutine(m_WaitForBoardThenLoadButton);
		}
		m_WaitForBoardThenLoadButton = null;
		if (m_WaitForHeroesAndStartAnimations != null)
		{
			StopCoroutine(m_WaitForHeroesAndStartAnimations);
		}
		m_WaitForHeroesAndStartAnimations = null;
		if (m_DealStartingCards != null)
		{
			StopCoroutine(m_DealStartingCards);
		}
		m_DealStartingCards = null;
		if (m_ShowMultiplayerWaitingArea != null)
		{
			StopCoroutine(m_ShowMultiplayerWaitingArea);
		}
		m_ShowMultiplayerWaitingArea = null;
		EndMulligan();
	}

	private IEnumerator SkipMulliganForResume()
	{
		introComplete = true;
		ForceMulliganActive(active: false);
		SoundDucker ducker = null;
		if (!GameMgr.Get().IsSpectator())
		{
			ducker = base.gameObject.AddComponent<SoundDucker>();
			ducker.m_DuckedCategoryDefs = new List<SoundDuckedCategoryDef>();
			foreach (Global.SoundCategory value in Enum.GetValues(typeof(Global.SoundCategory)))
			{
				if (value != Global.SoundCategory.AMBIENCE && value != Global.SoundCategory.MUSIC)
				{
					SoundDuckedCategoryDef soundDuckedCategoryDef = new SoundDuckedCategoryDef();
					soundDuckedCategoryDef.m_Category = value;
					soundDuckedCategoryDef.m_Volume = 0f;
					soundDuckedCategoryDef.m_RestoreSec = 5f;
					soundDuckedCategoryDef.m_BeginSec = 0f;
					ducker.m_DuckedCategoryDefs.Add(soundDuckedCategoryDef);
				}
			}
			ducker.StartDucking();
		}
		while (Board.Get() == null)
		{
			yield return null;
		}
		Board.Get().RaiseTheLightsQuickly();
		while (ZoneMgr.Get() == null)
		{
			yield return null;
		}
		InitZones();
		Collider dragPlane = Board.Get().FindCollider("DragPlane");
		friendlySideHandZone.SetDoNotUpdateLayout(enable: false);
		opposingSideHandZone.SetDoNotUpdateLayout(enable: false);
		dragPlane.enabled = false;
		friendlySideHandZone.AddInputBlocker();
		opposingSideHandZone.AddInputBlocker();
		while (!GameState.Get().IsGameCreated())
		{
			yield return null;
		}
		while (ZoneMgr.Get().HasActiveServerChange())
		{
			yield return null;
		}
		GameState.Get().GetGameEntity().NotifyOfMulliganInitialized();
		SceneMgr.Get().NotifySceneLoaded();
		while (LoadingScreen.Get().IsPreviousSceneActive() || LoadingScreen.Get().IsFadingOut())
		{
			yield return null;
		}
		if (ducker != null)
		{
			ducker.StopDucking();
			UnityEngine.Object.Destroy(ducker);
		}
		if (SceneMgr.Get().GetPrevMode() != SceneMgr.Mode.GAMEPLAY)
		{
			FadeOutMulliganMusicAndStartGameplayMusic();
		}
		dragPlane.enabled = true;
		friendlySideHandZone.RemoveInputBlocker();
		opposingSideHandZone.RemoveInputBlocker();
		friendlySideDeck.SetSuppressEmotes(suppress: false);
		opposingSideDeck.SetSuppressEmotes(suppress: false);
		if (GameState.Get().GetResponseMode() == GameState.ResponseMode.CHOICE)
		{
			GameState.Get().UpdateChoiceHighlights();
		}
		else if (GameState.Get().GetResponseMode() == GameState.ResponseMode.OPTION)
		{
			GameState.Get().UpdateOptionHighlights();
		}
		GameMgr.Get().UpdatePresence();
		InputManager.Get().OnMulliganEnded();
		EndTurnButton.Get().OnMulliganEnded();
		GameState.Get().GetGameEntity().NotifyOfMulliganEnded();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public void SkipMulligan()
	{
		Gameplay.Get().RemoveClassNames();
		m_SkipMulliganWhenIntroComplete = SkipMulliganWhenIntroComplete();
		StartCoroutine(m_SkipMulliganWhenIntroComplete);
	}

	private IEnumerator SkipMulliganWhenIntroComplete()
	{
		m_waitingForUserInput = false;
		while (!introComplete)
		{
			yield return null;
		}
		myHeroCardActor.TurnOnCollider();
		hisHeroCardActor.TurnOnCollider();
		FadeOutMulliganMusicAndStartGameplayMusic();
		myHeroCardActor.GetHealthObject().Show();
		hisHeroCardActor.GetHealthObject().Show();
		Board.Get().FindCollider("DragPlane").enabled = true;
		Board.Get().SplitSurface();
		Board.Get().RaiseTheLights();
		FadeHeroPowerIn(GameState.Get().GetFriendlySidePlayer().GetHeroPowerCard());
		FadeHeroPowerIn(GameState.Get().GetOpposingSidePlayer().GetHeroPowerCard());
		ForceMulliganActive(active: false);
		InitZones();
		friendlySideHandZone.SetDoNotUpdateLayout(enable: false);
		friendlySideHandZone.UpdateLayout();
		opposingSideHandZone.SetDoNotUpdateLayout(enable: false);
		opposingSideHandZone.UpdateLayout();
		friendlySideDeck.SetSuppressEmotes(suppress: false);
		opposingSideDeck.SetSuppressEmotes(suppress: false);
		InputManager.Get().OnMulliganEnded();
		EndTurnButton.Get().OnMulliganEnded();
		GameState.Get().GetGameEntity().NotifyOfMulliganEnded();
		m_WaitForBoardAnimToCompleteThenStartTurn = WaitForBoardAnimToCompleteThenStartTurn();
		StartCoroutine(m_WaitForBoardAnimToCompleteThenStartTurn);
	}

	private void FadeOutMulliganMusicAndStartGameplayMusic()
	{
		GameState.Get().GetGameEntity().StartGameplaySoundtracks();
	}

	private IEnumerator WaitForBoardAnimToCompleteThenStartTurn()
	{
		yield return new WaitForSeconds(1.5f);
		GameState.Get().SetMulliganBusy(busy: false);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private void ShuffleDeck()
	{
		SoundManager.Get().LoadAndPlay("FX_MulliganCoin09_DeckShuffle.prefab:e80f93eec961ec24485521285a8addf7", friendlySideDeck.gameObject);
		Animation animation = friendlySideDeck.gameObject.GetComponent<Animation>();
		if (animation == null)
		{
			animation = friendlySideDeck.gameObject.AddComponent<Animation>();
		}
		animation.AddClip(shuffleDeck, "shuffleDeckAnim");
		animation.Play("shuffleDeckAnim");
		animation = opposingSideDeck.gameObject.GetComponent<Animation>();
		if (animation == null)
		{
			animation = opposingSideDeck.gameObject.AddComponent<Animation>();
		}
		animation.AddClip(shuffleDeck, "shuffleDeckAnim");
		animation.Play("shuffleDeckAnim");
	}

	private void SlideCard(GameObject topCard)
	{
		iTween.MoveTo(topCard, iTween.Hash("position", new Vector3(topCard.transform.position.x - 0.5f, topCard.transform.position.y, topCard.transform.position.z), "time", 0.5f, "easetype", iTween.EaseType.linear));
	}

	private IEnumerator SampleAnimFrame(Animation animToUse, string animName, float startSec)
	{
		AnimationState state = animToUse[animName];
		state.enabled = true;
		state.time = startSec;
		animToUse.Play(animName);
		yield return null;
		state.enabled = false;
	}

	private void SortHand(Zone zone)
	{
		zone.GetCards().Sort(Zone.CardSortComparison);
	}

	private IEnumerator ShrinkStartingHandBanner(GameObject banner)
	{
		yield return new WaitForSeconds(4f);
		if (!(banner == null))
		{
			iTween.ScaleTo(banner, new Vector3(0f, 0f, 0f), 0.5f);
			yield return new WaitForSeconds(0.5f);
			UnityEngine.Object.Destroy(banner);
		}
	}

	private void FadeHeroPowerIn(Card heroPowerCard)
	{
		if (!(heroPowerCard == null))
		{
			Actor actor = heroPowerCard.GetActor();
			if (!(actor == null))
			{
				actor.TurnOnCollider();
			}
		}
	}

	private void LoadMyHeroSkinSocketInEffect(Actor myHero)
	{
		if ((!string.IsNullOrEmpty(myHero.SocketInEffectFriendly) || (bool)UniversalInputManager.UsePhoneUI) && (!string.IsNullOrEmpty(myHero.SocketInEffectFriendlyPhone) || !UniversalInputManager.UsePhoneUI))
		{
			m_isLoadingMyCustomSocketIn = true;
			string text = myHero.SocketInEffectFriendly;
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				text = myHero.SocketInEffectFriendlyPhone;
			}
			AssetLoader.Get().InstantiatePrefab(text, OnMyHeroSkinSocketInEffectLoaded);
		}
	}

	private void OnMyHeroSkinSocketInEffectLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError("Failed to load My custom hero socket in effect!");
			m_isLoadingMyCustomSocketIn = false;
			return;
		}
		go.transform.position = Board.Get().FindBone("CustomSocketIn_Friendly").position;
		Spell component = go.GetComponent<Spell>();
		if (component == null)
		{
			Debug.LogError("Faild to locate Spell on custom socket in effect!");
			m_isLoadingMyCustomSocketIn = false;
			return;
		}
		m_MyCustomSocketInSpell = component;
		if (m_MyCustomSocketInSpell.HasUsableState(SpellStateType.IDLE))
		{
			m_MyCustomSocketInSpell.ActivateState(SpellStateType.IDLE);
		}
		else
		{
			m_MyCustomSocketInSpell.gameObject.SetActive(value: false);
		}
		m_isLoadingMyCustomSocketIn = false;
	}

	private void LoadHisHeroSkinSocketInEffect(Actor hisHero)
	{
		if ((!string.IsNullOrEmpty(hisHero.SocketInEffectOpponent) || (bool)UniversalInputManager.UsePhoneUI) && (!string.IsNullOrEmpty(hisHero.SocketInEffectOpponentPhone) || !UniversalInputManager.UsePhoneUI))
		{
			m_isLoadingHisCustomSocketIn = true;
			string text = hisHero.SocketInEffectOpponent;
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				text = hisHero.SocketInEffectOpponentPhone;
			}
			AssetLoader.Get().InstantiatePrefab(text, OnHisHeroSkinSocketInEffectLoaded);
		}
	}

	private void OnHisHeroSkinSocketInEffectLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError("Failed to load His custom hero socket in effect!");
			m_isLoadingHisCustomSocketIn = false;
			return;
		}
		go.transform.position = Board.Get().FindBone("CustomSocketIn_Opposing").position;
		Spell component = go.GetComponent<Spell>();
		if (component == null)
		{
			Debug.LogError("Faild to locate Spell on custom socket in effect!");
			m_isLoadingHisCustomSocketIn = false;
			return;
		}
		m_HisCustomSocketInSpell = component;
		if (m_HisCustomSocketInSpell.HasUsableState(SpellStateType.IDLE))
		{
			m_HisCustomSocketInSpell.ActivateState(SpellStateType.IDLE);
		}
		else
		{
			m_HisCustomSocketInSpell.gameObject.SetActive(value: false);
		}
		m_isLoadingHisCustomSocketIn = false;
	}

	private void DestoryHeroSkinSocketInEffects()
	{
		if (m_MyCustomSocketInSpell != null)
		{
			UnityEngine.Object.Destroy(m_MyCustomSocketInSpell.gameObject);
		}
		if (m_HisCustomSocketInSpell != null)
		{
			UnityEngine.Object.Destroy(m_HisCustomSocketInSpell.gameObject);
		}
	}

	private void OnFakeHeroActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Log.MulliganManager.PrintWarning($"MulliganManager.OnFakeHeroActorLoaded() - FAILED to load actor \"{assetRef}\"");
			pendingFakeHeroCount--;
			return;
		}
		Actor component = go.GetComponent<Actor>();
		if (component == null)
		{
			Log.MulliganManager.PrintWarning($"MulliganManager.OnFakeHeroActorLoaded() - ERROR actor \"{assetRef}\" has no Actor component");
			pendingFakeHeroCount--;
			return;
		}
		((List<Actor>)callbackData).Add(component);
		component.SetUnlit();
		SceneUtils.SetLayer(component.gameObject, base.gameObject.layer);
		component.GetMeshRenderer().gameObject.layer = 8;
		GameState.Get().GetGameEntity().ConfigureFakeMulliganCardActor(component, shown: true);
		if (m_startingCards.Count > 0)
		{
			component.gameObject.transform.position = new Vector3(m_startingCards[0].transform.position.x, m_startingCards[0].transform.position.y, m_startingCards[0].transform.position.z);
		}
		pendingFakeHeroCount--;
	}

	private void OnHeroActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Log.MulliganManager.PrintWarning($"MulliganManager.OnHeroActorLoaded() - FAILED to load actor \"{assetRef}\"");
			pendingHeroCount--;
			return;
		}
		Actor component = go.GetComponent<Actor>();
		if (component == null)
		{
			Log.MulliganManager.PrintWarning($"MulliganManager.OnHeroActorLoaded() - ERROR actor \"{assetRef}\" has no Actor component");
			pendingHeroCount--;
			return;
		}
		Card card = (Card)callbackData;
		component.SetCard(card);
		component.SetCardDefFromCard(card);
		component.SetPremium(card.GetPremium());
		component.UpdateAllComponents();
		if (card.GetActor() != null)
		{
			card.GetActor().Destroy();
		}
		card.SetActor(component);
		component.SetEntity(card.GetEntity());
		component.UpdateAllComponents();
		component.SetUnlit();
		SceneUtils.SetLayer(component.gameObject, base.gameObject.layer);
		component.GetMeshRenderer().gameObject.layer = 8;
		component.GetHealthObject().Hide();
		GameState.Get().GetGameEntity().ApplyMulliganActorStateChanges(component);
		choiceHeroActors.Add(card, component);
		pendingHeroCount--;
	}

	private void OnOpponentHeroActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Log.MulliganManager.PrintWarning($"MulliganManager.OnOpponentHeroActorLoaded() - FAILED to load actor \"{assetRef}\"");
			pendingHeroCount--;
			return;
		}
		Actor component = go.GetComponent<Actor>();
		if (component == null)
		{
			Log.MulliganManager.PrintWarning($"MulliganManager.OnOpponentHeroActorLoaded() - ERROR actor \"{assetRef}\" has no Actor component");
			pendingHeroCount--;
			return;
		}
		Card card = (Card)callbackData;
		component.SetCard(card);
		component.SetCardDefFromCard(card);
		component.SetPremium(card.GetPremium());
		component.UpdateAllComponents();
		if (card.GetActor() != null)
		{
			card.GetActor().Destroy();
		}
		card.SetActor(component);
		component.SetEntity(card.GetEntity());
		component.UpdateAllComponents();
		component.SetUnlit();
		component.transform.localPosition = new Vector3(component.transform.localPosition.x + 1000f, component.transform.localPosition.y, component.transform.localPosition.z);
		SceneUtils.SetLayer(component.gameObject, base.gameObject.layer);
		UnityEngine.Object.Destroy(component.m_healthObject);
		UnityEngine.Object.Destroy(component.m_attackObject);
		GameState.Get().GetGameEntity().ApplyMulliganActorLobbyStateChanges(component);
		opponentHeroActors.Add(card, component);
		pendingHeroCount--;
	}
}
