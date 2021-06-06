using System.Collections;
using System.Collections.Generic;
using Assets;
using bgs;
using Hearthstone.Progression;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

public class PackOpening : MonoBehaviour
{
	public PackOpeningBones m_Bones;

	public PackOpeningDirector m_DirectorPrefab;

	public PackOpeningSocket m_Socket;

	public PackOpeningSocket m_SocketAccent;

	public UberText m_HeaderText;

	public UIBButton m_BackButton;

	public GameObject m_DragPlane;

	public Vector3 m_DragTolerance;

	public GameObject m_InputBlocker;

	public UIBObjectSpacing m_UnopenedPackContainer;

	public UIBScrollable m_UnopenedPackScroller;

	public CameraMask m_PackTrayCameraMask;

	public float m_UnopenedPackPadding;

	public bool m_OnePackCentered = true;

	private const int MAX_OPENED_PACKS_BEFORE_CARD_CACHE_RESET = 10;

	private static PackOpening s_instance;

	private bool m_waitingForInitialNetData = true;

	private bool m_shown;

	private Map<int, UnopenedPack> m_unopenedPacks = new Map<int, UnopenedPack>();

	private Map<int, bool> m_unopenedPacksLoading = new Map<int, bool>();

	private PackOpeningDirector m_director;

	private UnopenedPack m_draggedPack;

	private Notification m_hintArrow;

	private GameObject m_PackOpeningCardFX;

	private bool m_autoOpenPending;

	private int m_lastOpenedBoosterId;

	private bool m_enableBackButton;

	private bool m_openBoxTransitionFinished;

	private static bool m_hasAcknowledgedKoreanWarning;

	private Coroutine m_autoOpenPackCoroutine;

	private void Awake()
	{
		s_instance = this;
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			AssetLoader.Get().InstantiatePrefab("PackOpeningCardFX_Phone.prefab:0ef32a20a9e7843c3ba360e49527dbfa", OnPackOpeningFXLoaded);
		}
		else
		{
			AssetLoader.Get().InstantiatePrefab("PackOpeningCardFX.prefab:b32177fb14f134edfb891dc93501b1ce", OnPackOpeningFXLoaded);
		}
		InitializeNet();
		InitializeUI();
		Box.Get().AddTransitionFinishedListener(OnBoxTransitionFinished);
		TelemetryWatcher.WatchFor(TelemetryWatcherWatchType.StoreFromPackOpening);
		SceneMgr.Get().RegisterScenePreUnloadEvent(OnScenePreUnload);
		FiresideGatheringManager.Get().OnJoinFSG += OnFiresideGatheringCheckinStatusChanged;
		FiresideGatheringManager.Get().OnLeaveFSG += OnFiresideGatheringCheckinStatusChanged;
	}

	private void Start()
	{
		Navigation.Push(OnNavigateBack);
		FatalErrorMgr.Get().AddErrorListener(OnFatalError);
	}

	private void Update()
	{
		UpdateDraggedPack();
	}

	private void OnDestroy()
	{
		if (m_draggedPack != null && PegCursor.Get() != null)
		{
			PegCursor.Get().SetMode(PegCursor.Mode.STOPDRAG);
		}
		ShutdownNet();
		s_instance = null;
		if (SceneMgr.Get() != null)
		{
			SceneMgr.Get().UnregisterScenePreUnloadEvent(OnScenePreUnload);
		}
		if (FiresideGatheringManager.Get() != null)
		{
			FiresideGatheringManager.Get().OnJoinFSG -= OnFiresideGatheringCheckinStatusChanged;
			FiresideGatheringManager.Get().OnLeaveFSG -= OnFiresideGatheringCheckinStatusChanged;
		}
		FatalErrorMgr.Get().RemoveErrorListener(OnFatalError);
	}

	public static PackOpening Get()
	{
		return s_instance;
	}

	public GameObject GetPackOpeningCardEffects()
	{
		return m_PackOpeningCardFX;
	}

	public bool HandleKeyboardInput()
	{
		if (InputCollection.GetKeyUp(KeyCode.Space))
		{
			if (m_director == null)
			{
				return false;
			}
			if (CanOpenPackAutomatically())
			{
				m_autoOpenPending = true;
				m_director.FinishPackOpen();
				m_autoOpenPackCoroutine = StartCoroutine(OpenNextPackWhenReady());
			}
			else if (PackOpeningDirector.QuickPackOpeningAllowed)
			{
				m_director.ForceRevealRandomCard();
			}
			return true;
		}
		return false;
	}

	private void OnScenePreUnload(SceneMgr.Mode prevMode, PegasusScene prevScene, object userData)
	{
		FullScreenFXMgr.Get().StopAllEffects();
		if (m_director != null)
		{
			m_director.HideCardsAndDoneButton();
		}
		Hide();
	}

	private void NotifySceneLoadedWhenReady()
	{
		if (m_waitingForInitialNetData)
		{
			m_waitingForInitialNetData = false;
			SceneMgr.Get().NotifySceneLoaded();
		}
	}

	private void OnBoxTransitionFinished(object userData)
	{
		Box.Get().RemoveTransitionFinishedListener(OnBoxTransitionFinished);
		Show();
		m_openBoxTransitionFinished = true;
	}

	private void Show()
	{
		if (m_shown)
		{
			return;
		}
		m_shown = true;
		PresenceMgr.Get().SetStatus(Global.PresenceStatus.PACKOPENING);
		if (!Options.Get().GetBool(Option.HAS_SEEN_PACK_OPENING, defaultVal: false))
		{
			NetCache.NetCacheBoosters netObject = NetCache.Get().GetNetObject<NetCache.NetCacheBoosters>();
			if (netObject != null && netObject.GetTotalNumBoosters() > 0)
			{
				Options.Get().SetBool(Option.HAS_SEEN_PACK_OPENING, val: true);
			}
		}
		MusicManager.Get().StartPlaylist(MusicPlaylistType.UI_PackOpening);
		CreateDirector();
		BnetBar.Get().RefreshCurrency();
		if (NetCache.Get().GetNetObject<NetCache.NetCacheBoosters>().BoosterStacks.Count < 2)
		{
			ShowHintOnUnopenedPack();
		}
		UpdateUIEvents();
		DisablePackTrayMask();
	}

	private void Hide()
	{
		if (m_shown)
		{
			m_shown = false;
			DestroyHint();
			m_InputBlocker.SetActive(value: false);
			EnablePackTrayMask();
			UnregisterUIEvents();
			ShutdownNet();
		}
	}

	private bool OnNavigateBack()
	{
		if (!m_enableBackButton || m_InputBlocker.activeSelf)
		{
			return false;
		}
		Hide();
		SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
		return true;
	}

	private void InitializeNet()
	{
		m_waitingForInitialNetData = true;
		NetCache.Get().RegisterScreenPackOpening(OnNetDataReceived, NetCache.DefaultErrorHandler);
		Network.Get().RegisterNetHandler(BoosterContent.PacketID.ID, OnBoosterOpened);
		Network.Get().RegisterNetHandler(DBAction.PacketID.ID, OnDBAction);
		LoginManager.Get().OnAchievesLoaded += OnReloginComplete;
	}

	private void ShutdownNet()
	{
		if (HearthstoneServices.TryGet<NetCache>(out var service))
		{
			service.UnregisterNetCacheHandler(OnNetDataReceived);
		}
		if (HearthstoneServices.TryGet<Network>(out var service2))
		{
			service2.RemoveNetHandler(BoosterContent.PacketID.ID, OnBoosterOpened);
			service2.RemoveNetHandler(DBAction.PacketID.ID, OnDBAction);
		}
		if (HearthstoneServices.TryGet<LoginManager>(out var _))
		{
			LoginManager.Get().OnAchievesLoaded -= OnReloginComplete;
		}
	}

	private void OnNetDataReceived()
	{
		NotifySceneLoadedWhenReady();
		UpdatePacks();
		UpdateUIEvents();
	}

	private void OnReloginComplete()
	{
		UpdatePacks();
		UpdateUIEvents();
	}

	private void UpdatePacks()
	{
		NetCache.NetCacheBoosters netObject = NetCache.Get().GetNetObject<NetCache.NetCacheBoosters>();
		if (netObject == null)
		{
			Debug.LogError($"PackOpening.UpdatePacks() - boosters are null");
			return;
		}
		foreach (NetCache.BoosterStack boosterStack in netObject.BoosterStacks)
		{
			int id = boosterStack.Id;
			if (m_unopenedPacks.ContainsKey(id) && m_unopenedPacks[id] != null)
			{
				if (netObject.GetBoosterStack(id) == null)
				{
					Object.Destroy(m_unopenedPacks[id]);
					m_unopenedPacks[id] = null;
				}
				else
				{
					UpdatePack(m_unopenedPacks[id], netObject.GetBoosterStack(id));
				}
			}
			else if (netObject.GetBoosterStack(id) != null && netObject.GetBoosterStack(id).Count > 0 && (!m_unopenedPacksLoading.ContainsKey(id) || !m_unopenedPacksLoading[id]))
			{
				m_unopenedPacksLoading[id] = true;
				BoosterDbfRecord record = GameDbf.Booster.GetRecord(id);
				if (record == null)
				{
					Debug.LogErrorFormat("PackOpening.UpdatePacks() - No DBF record for booster {0}", id);
				}
				else if (string.IsNullOrEmpty(record.PackOpeningPrefab))
				{
					Debug.LogError($"PackOpening.UpdatePacks() - no prefab found for booster {id}!");
				}
				else
				{
					AssetLoader.Get().InstantiatePrefab(record.PackOpeningPrefab, OnUnopenedPackLoaded, boosterStack, AssetLoadingOptions.IgnorePrefabPosition);
				}
			}
		}
		if (m_director == null || !m_director.IsPlaying())
		{
			LayoutPacks();
		}
	}

	private void OnBoosterOpened()
	{
		m_director.Play(m_lastOpenedBoosterId);
		m_autoOpenPending = false;
		List<NetCache.BoosterCard> cards = Network.Get().OpenedBooster();
		m_director.OnBoosterOpened(cards);
	}

	private void OnDBAction()
	{
		Network.DBAction dbAction = Network.Get().GetDbAction();
		if (dbAction.Action == Network.DBAction.ActionType.OPEN_BOOSTER && dbAction.Result != Network.DBAction.ResultType.SUCCESS)
		{
			Debug.LogError($"PackOpening.OnDBAction - Error while opening packs: {dbAction}");
			NetCache.NetCacheBoosters netObject = NetCache.Get().GetNetObject<NetCache.NetCacheBoosters>();
			if (netObject != null)
			{
				netObject.GetBoosterStack(m_lastOpenedBoosterId).LocallyPreConsumedCount--;
			}
			m_UnopenedPackScroller.Pause(pause: false);
			m_InputBlocker.SetActive(value: false);
			m_autoOpenPending = false;
			m_unopenedPacks[m_lastOpenedBoosterId].AddBooster();
			m_unopenedPacksLoading[m_lastOpenedBoosterId] = false;
			BnetBar.Get().RefreshCurrency();
		}
	}

	private void OnFiresideGatheringCheckinStatusChanged(FSGConfig gathering)
	{
		foreach (KeyValuePair<int, UnopenedPack> unopenedPack in m_unopenedPacks)
		{
			if (!(unopenedPack.Value == null))
			{
				unopenedPack.Value.UpdateState();
			}
		}
	}

	private void CreateDirector()
	{
		GameObject gameObject = Object.Instantiate(m_DirectorPrefab.gameObject);
		m_director = gameObject.GetComponent<PackOpeningDirector>();
		gameObject.transform.parent = base.transform;
		TransformUtil.CopyWorld(m_director, m_Bones.m_Director);
	}

	private void PickUpBooster()
	{
		UnopenedPack creatorPack = m_draggedPack.GetCreatorPack();
		creatorPack.RemoveBooster();
		m_draggedPack.SetBoosterStack(new NetCache.BoosterStack
		{
			Id = creatorPack.GetBoosterStack().Id,
			Count = 1
		});
	}

	private void OpenBooster(UnopenedPack pack)
	{
		AchievementManager.Get().PauseToastNotifications();
		int num = 1;
		if (!GameUtils.IsFakePackOpeningEnabled())
		{
			num = pack.GetBoosterStack().Id;
			Network.Get().OpenBooster(num);
		}
		m_InputBlocker.SetActive(value: true);
		if (m_autoOpenPackCoroutine != null)
		{
			StopCoroutine(m_autoOpenPackCoroutine);
			m_autoOpenPackCoroutine = null;
		}
		m_director.AddFinishedListener(OnDirectorFinished);
		m_lastOpenedBoosterId = num;
		BnetBar.Get().HideCurrencyTemporarily();
		if (GameUtils.IsFakePackOpeningEnabled())
		{
			StartCoroutine(OnFakeBoosterOpened());
		}
		m_UnopenedPackScroller.Pause(pause: true);
	}

	private IEnumerator OnFakeBoosterOpened()
	{
		float seconds = Random.Range(0f, 1f);
		yield return new WaitForSeconds(seconds);
		List<NetCache.BoosterCard> list = new List<NetCache.BoosterCard>();
		NetCache.BoosterCard boosterCard = new NetCache.BoosterCard();
		boosterCard.Def.Name = "CS1_042";
		boosterCard.Def.Premium = TAG_PREMIUM.NORMAL;
		list.Add(boosterCard);
		boosterCard = new NetCache.BoosterCard();
		boosterCard.Def.Name = "CS1_129";
		boosterCard.Def.Premium = TAG_PREMIUM.NORMAL;
		list.Add(boosterCard);
		boosterCard = new NetCache.BoosterCard();
		boosterCard.Def.Name = "EX1_050";
		boosterCard.Def.Premium = TAG_PREMIUM.NORMAL;
		list.Add(boosterCard);
		boosterCard = new NetCache.BoosterCard();
		boosterCard.Def.Name = "EX1_105";
		boosterCard.Def.Premium = TAG_PREMIUM.NORMAL;
		list.Add(boosterCard);
		boosterCard = new NetCache.BoosterCard();
		boosterCard.Def.Name = "EX1_350";
		boosterCard.Def.Premium = TAG_PREMIUM.NORMAL;
		list.Add(boosterCard);
		m_director.OnBoosterOpened(list);
	}

	private void PutBackBooster()
	{
		UnopenedPack creatorPack = m_draggedPack.GetCreatorPack();
		m_draggedPack.RemoveBooster();
		creatorPack.AddBooster();
	}

	private void UpdatePack(UnopenedPack pack, NetCache.BoosterStack boosterStack)
	{
		pack.SetBoosterStack(boosterStack);
	}

	private void OnUnopenedPackLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		NetCache.BoosterStack boosterStack = (NetCache.BoosterStack)callbackData;
		int id = boosterStack.Id;
		m_unopenedPacksLoading[id] = false;
		if (go == null)
		{
			Debug.LogError($"PackOpening.OnUnopenedPackLoaded() - FAILED to load {assetRef}");
			return;
		}
		UnopenedPack component = go.GetComponent<UnopenedPack>();
		go.SetActive(value: false);
		if (component == null)
		{
			Debug.LogError($"PackOpening.OnUnopenedPackLoaded() - asset {base.name} did not have a {typeof(UnopenedPack)} script on it");
			return;
		}
		m_unopenedPacks.Add(id, component);
		component.gameObject.SetActive(value: true);
		GameUtils.SetParent(component, m_UnopenedPackContainer);
		component.transform.localScale = Vector3.one;
		component.SetDragTolerance(m_DragTolerance);
		component.AddEventListener(UIEventType.PRESS, OnUnopenedPackPress);
		component.AddEventListener(UIEventType.DRAG, OnUnopenedPackDrag);
		component.AddEventListener(UIEventType.ROLLOVER, OnUnopenedPackRollover);
		component.AddEventListener(UIEventType.ROLLOUT, OnUnopenedPackRollout);
		component.AddEventListener(UIEventType.RELEASEALL, OnUnopenedPackReleaseAll);
		UpdatePack(component, boosterStack);
		AchieveManager.Get().NotifyOfPacksReadyToOpen(component);
		if (NetCache.Get().GetNetObject<NetCache.NetCacheBoosters>().BoosterStacks.Count < 2)
		{
			LayoutPacks();
			ShowHintOnUnopenedPack();
		}
		else
		{
			StopHintOnUnopenedPack();
			LayoutPacks();
		}
		UpdateUIEvents();
	}

	private void LayoutPacks(bool animate = false)
	{
		IEnumerable<int> sortedPackIds = GameUtils.GetSortedPackIds(ascending: false);
		m_UnopenedPackContainer.ClearObjects();
		if (!m_openBoxTransitionFinished)
		{
			DisablePackTrayMask();
		}
		int num = 18;
		if (TemporaryAccountManager.IsTemporaryAccount())
		{
			m_unopenedPacks.TryGetValue(num, out var value);
			if (value != null && value.GetBoosterStack().Count > 0)
			{
				value.gameObject.SetActive(value: true);
				m_UnopenedPackContainer.AddObject(value);
			}
		}
		foreach (int item in sortedPackIds)
		{
			if (num != item)
			{
				m_unopenedPacks.TryGetValue(item, out var value2);
				if (!(value2 == null) && value2.GetBoosterStack().Count != 0)
				{
					value2.gameObject.SetActive(value: true);
					m_UnopenedPackContainer.AddObject(value2);
				}
			}
		}
		if (m_OnePackCentered && m_UnopenedPackContainer.m_Objects.Count == 1)
		{
			m_UnopenedPackContainer.AddSpace(0, new Vector3(0f, 0f, 0.5f));
		}
		else if (m_OnePackCentered && m_UnopenedPackContainer.m_Objects.Count < 1)
		{
			m_UnopenedPackContainer.AddSpace(0);
		}
		if (animate)
		{
			m_UnopenedPackContainer.AnimateUpdatePositions(0.25f);
		}
		else
		{
			m_UnopenedPackContainer.UpdatePositions();
		}
		if (!m_openBoxTransitionFinished)
		{
			EnablePackTrayMask();
		}
	}

	private void CreateDraggedPack(UnopenedPack creatorPack)
	{
		m_draggedPack = creatorPack.AcquireDraggedPack();
		Vector3 position = m_draggedPack.transform.position;
		if (UniversalInputManager.Get().GetInputHitInfo(GameLayer.DragPlane.LayerBit(), out var hitInfo))
		{
			position = hitInfo.point;
		}
		float num = Vector3.Dot(Camera.main.transform.forward, Vector3.up);
		float num2 = (0f - num) / Mathf.Abs(num);
		Bounds bounds = m_draggedPack.GetComponent<Collider>().bounds;
		position.y += num2 * bounds.extents.y * m_draggedPack.transform.lossyScale.y;
		m_draggedPack.transform.position = position;
	}

	private void DestroyDraggedPack()
	{
		m_UnopenedPackScroller.Pause(pause: false);
		m_draggedPack.GetCreatorPack().ReleaseDraggedPack();
		m_draggedPack = null;
	}

	private void UpdateDraggedPack()
	{
		if (!(m_draggedPack == null))
		{
			Vector3 position = m_draggedPack.transform.position;
			if (UniversalInputManager.Get().GetInputHitInfo(GameLayer.DragPlane.LayerBit(), out var hitInfo))
			{
				position.x = hitInfo.point.x;
				position.z = hitInfo.point.z;
				m_draggedPack.transform.position = position;
			}
			if (UniversalInputManager.Get().GetMouseButtonUp(0))
			{
				DropPack();
			}
		}
	}

	private IEnumerator HideAfterNoMorePacks()
	{
		while (!(m_director == null) && !(m_director.gameObject == null))
		{
			yield return new WaitForSeconds(0.2f);
		}
		Hide();
		SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
	}

	private void OnDirectorFinished(object userData)
	{
		m_UnopenedPackScroller.Pause(pause: false);
		int num = 0;
		foreach (KeyValuePair<int, UnopenedPack> unopenedPack in m_unopenedPacks)
		{
			if (!(unopenedPack.Value == null))
			{
				int count = unopenedPack.Value.GetBoosterStack().Count;
				num += count;
				unopenedPack.Value.gameObject.SetActive(count > 0);
			}
		}
		if (num == 0)
		{
			StartCoroutine(HideAfterNoMorePacks());
		}
		else
		{
			m_InputBlocker.SetActive(value: false);
			CreateDirector();
			LayoutPacks(animate: true);
		}
		BnetBar.Get().RefreshCurrency();
	}

	private void ShowHintOnUnopenedPack()
	{
		if (!m_shown)
		{
			return;
		}
		List<UnopenedPack> list = new List<UnopenedPack>();
		foreach (KeyValuePair<int, UnopenedPack> unopenedPack in m_unopenedPacks)
		{
			if (!(unopenedPack.Value == null) && unopenedPack.Value.CanOpenPack() && unopenedPack.Value.GetBoosterStack().Count > 0)
			{
				list.Add(unopenedPack.Value);
			}
		}
		if (list.Count < 1 || list[0] == null || list[0].GetBoosterStack().Id == 18)
		{
			return;
		}
		list[0].PlayAlert();
		if (!Options.Get().GetBool(Option.HAS_OPENED_BOOSTER, defaultVal: false) && UserAttentionManager.CanShowAttentionGrabber("PackOpening.ShowHintOnUnopenedPack") && m_hintArrow == null)
		{
			Bounds bounds = list[0].GetComponent<Collider>().bounds;
			Vector3 center = bounds.center;
			m_hintArrow = NotificationManager.Get().CreateBouncingArrow(UserAttentionBlocker.NONE, center, new Vector3(0f, 90f, 0f), addToList: false);
			if (m_hintArrow != null)
			{
				FixArrowScale(list[0].transform);
				Bounds bounds2 = m_hintArrow.bounceObject.GetComponent<Renderer>().bounds;
				center.x += bounds.extents.x + bounds2.extents.x;
				m_hintArrow.transform.position = center;
			}
		}
	}

	private void StopHintOnUnopenedPack()
	{
		foreach (KeyValuePair<int, UnopenedPack> unopenedPack in m_unopenedPacks)
		{
			if (!(unopenedPack.Value == null) && unopenedPack.Value.CanOpenPack() && unopenedPack.Value.GetBoosterStack().Count > 0)
			{
				unopenedPack.Value.StopAlert();
				break;
			}
		}
	}

	private void ShowHintOnSlot()
	{
		if (!Options.Get().GetBool(Option.HAS_OPENED_BOOSTER, defaultVal: false) && UserAttentionManager.CanShowAttentionGrabber("PackOpening.ShowHintOnSlot"))
		{
			if (m_hintArrow == null)
			{
				m_hintArrow = NotificationManager.Get().CreateBouncingArrow(UserAttentionBlocker.NONE, addToList: false);
			}
			if (m_hintArrow != null)
			{
				FixArrowScale(m_draggedPack.transform);
				Bounds bounds = m_hintArrow.bounceObject.GetComponent<Renderer>().bounds;
				Vector3 position = m_Bones.m_Hint.position;
				position.z += bounds.extents.z;
				m_hintArrow.transform.position = position;
			}
		}
	}

	private void FixArrowScale(Transform parent)
	{
		Transform parent2 = m_hintArrow.transform.parent;
		m_hintArrow.transform.parent = parent;
		m_hintArrow.transform.localScale = Vector3.one;
		m_hintArrow.transform.parent = parent2;
	}

	private void HideHint()
	{
		if (!(m_hintArrow == null))
		{
			Options.Get().SetBool(Option.HAS_OPENED_BOOSTER, val: true);
			Object.Destroy(m_hintArrow.gameObject);
			m_hintArrow = null;
		}
	}

	private void DestroyHint()
	{
		if (!(m_hintArrow == null))
		{
			Object.Destroy(m_hintArrow.gameObject);
			m_hintArrow = null;
		}
	}

	private void InitializeUI()
	{
		m_HeaderText.Text = GameStrings.Get("GLUE_PACK_OPENING_HEADER");
		m_BackButton.AddEventListener(UIEventType.RELEASE, OnBackButtonPressed);
		m_DragPlane.SetActive(value: false);
		m_InputBlocker.SetActive(value: false);
	}

	private void UpdateUIEvents()
	{
		if (!m_shown)
		{
			UnregisterUIEvents();
			return;
		}
		if (m_draggedPack != null)
		{
			UnregisterUIEvents();
			return;
		}
		m_enableBackButton = true;
		m_BackButton.SetEnabled(enabled: true);
		foreach (KeyValuePair<int, UnopenedPack> unopenedPack in m_unopenedPacks)
		{
			if (unopenedPack.Value != null)
			{
				unopenedPack.Value.SetEnabled(enabled: true);
			}
		}
	}

	private void UnregisterUIEvents()
	{
		m_enableBackButton = false;
		m_BackButton.SetEnabled(enabled: false);
		foreach (KeyValuePair<int, UnopenedPack> unopenedPack in m_unopenedPacks)
		{
			if (unopenedPack.Value != null)
			{
				unopenedPack.Value.SetEnabled(enabled: false);
			}
		}
	}

	private void OnBackButtonPressed(UIEvent e)
	{
		Navigation.GoBack();
	}

	private void HoldPack(UnopenedPack selectedPack)
	{
		bool flag = UniversalInputManager.Get().InputIsOver(selectedPack.gameObject);
		if (!selectedPack.CanOpenPack() || !flag)
		{
			return;
		}
		HideUnopenedPackTooltip();
		PegCursor.Get().SetMode(PegCursor.Mode.DRAG);
		m_DragPlane.SetActive(value: true);
		CreateDraggedPack(selectedPack);
		if (m_draggedPack != null)
		{
			TooltipPanel componentInChildren = m_draggedPack.GetComponentInChildren<TooltipPanel>();
			if (componentInChildren != null)
			{
				Object.Destroy(componentInChildren.gameObject);
			}
		}
		PickUpBooster();
		selectedPack.StopAlert();
		ShowHintOnSlot();
		m_Socket.OnPackHeld();
		m_SocketAccent.OnPackHeld();
		UpdateUIEvents();
		m_UnopenedPackScroller.Pause(pause: true);
	}

	private void DropPack()
	{
		PegCursor.Get().SetMode(PegCursor.Mode.STOPDRAG);
		m_Socket.OnPackReleased();
		m_SocketAccent.OnPackReleased();
		if (UniversalInputManager.Get().InputIsOver(m_Socket.gameObject))
		{
			if (BattleNet.GetAccountCountry() == "KOR")
			{
				m_hasAcknowledgedKoreanWarning = true;
			}
			OpenBooster(m_draggedPack);
			HideHint();
		}
		else
		{
			PutBackBooster();
			DestroyHint();
		}
		DestroyDraggedPack();
		UpdateUIEvents();
		m_DragPlane.SetActive(value: false);
	}

	private void AutomaticallyOpenPack()
	{
		HideUnopenedPackTooltip();
		UnopenedPack value = null;
		if (!m_unopenedPacks.TryGetValue(m_lastOpenedBoosterId, out value) || value.GetBoosterStack().Count == 0)
		{
			foreach (KeyValuePair<int, UnopenedPack> unopenedPack in m_unopenedPacks)
			{
				if (!(unopenedPack.Value == null) && unopenedPack.Value.GetBoosterStack().Count > 0)
				{
					value = unopenedPack.Value;
					break;
				}
			}
		}
		if (!(value == null) && value.CanOpenPack())
		{
			if (m_draggedPack != null || m_InputBlocker.activeSelf)
			{
				m_autoOpenPending = false;
				return;
			}
			m_draggedPack = value.AcquireDraggedPack();
			PickUpBooster();
			value.StopAlert();
			OpenBooster(m_draggedPack);
			DestroyDraggedPack();
			UpdateUIEvents();
			m_DragPlane.SetActive(value: false);
		}
	}

	private void OnUnopenedPackPress(UIEvent e)
	{
		if ((e.GetElement() as UnopenedPack).GetBoosterStack().Id == 18)
		{
			TemporaryAccountManager.Get().ShowHealUpDialog(GameStrings.Get("GLUE_TEMPORARY_ACCOUNT_DIALOG_HEADER_01"), GameStrings.Get("GLUE_TEMPORARY_ACCOUNT_DIALOG_BODY_03"), TemporaryAccountManager.HealUpReason.LOCKED_PACK, userTriggered: true, null);
		}
	}

	private void OnUnopenedPackDrag(UIEvent e)
	{
		HoldPack(e.GetElement() as UnopenedPack);
	}

	private void OnUnopenedPackRollover(UIEvent e)
	{
		if (!m_hasAcknowledgedKoreanWarning && !(BattleNet.GetAccountCountry() != "KOR"))
		{
			TooltipZone component = (e.GetElement() as UnopenedPack).GetComponent<TooltipZone>();
			if (!(component == null))
			{
				component.ShowTooltip(string.Empty, GameStrings.Get("GLUE_PACK_OPENING_TOOLTIP"), 5f);
			}
		}
	}

	private void OnUnopenedPackRollout(UIEvent e)
	{
		HideUnopenedPackTooltip();
	}

	private void OnUnopenedPackReleaseAll(UIEvent e)
	{
		if (m_draggedPack == null)
		{
			if (!UniversalInputManager.Get().IsTouchMode() && ((UIReleaseAllEvent)e).GetMouseIsOver())
			{
				if ((e.GetElement() as UnopenedPack).GetBoosterStack().Id == 18)
				{
					TemporaryAccountManager.Get().ShowHealUpDialog(GameStrings.Get("GLUE_TEMPORARY_ACCOUNT_DIALOG_HEADER_01"), GameStrings.Get("GLUE_TEMPORARY_ACCOUNT_DIALOG_BODY_03"), TemporaryAccountManager.HealUpReason.LOCKED_PACK, userTriggered: true, null);
				}
				else
				{
					HoldPack(e.GetElement() as UnopenedPack);
				}
			}
		}
		else
		{
			DropPack();
		}
	}

	private void HideUnopenedPackTooltip()
	{
		foreach (KeyValuePair<int, UnopenedPack> unopenedPack in m_unopenedPacks)
		{
			if (!(unopenedPack.Value == null))
			{
				unopenedPack.Value.GetComponent<TooltipZone>().HideTooltip();
			}
		}
	}

	private bool CanOpenPackAutomatically()
	{
		if (PopupDisplayManager.Get().IsShowing)
		{
			return false;
		}
		if (m_autoOpenPending)
		{
			return false;
		}
		if (!m_shown)
		{
			return false;
		}
		if (!GameUtils.HaveBoosters())
		{
			return false;
		}
		if (m_director.IsPlaying() && !m_director.IsDoneButtonShown())
		{
			return false;
		}
		if (m_DragPlane.activeSelf)
		{
			return false;
		}
		if (StoreManager.Get().IsShownOrWaitingToShow())
		{
			return false;
		}
		return true;
	}

	private IEnumerator OpenNextPackWhenReady()
	{
		float waitTime = 0f;
		if (m_director.IsPlaying())
		{
			waitTime = 1f;
		}
		while (m_director.IsPlaying())
		{
			yield return null;
		}
		yield return new WaitForSeconds(waitTime);
		AutomaticallyOpenPack();
	}

	private void OnPackOpeningFXLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		m_PackOpeningCardFX = go;
	}

	private void EnablePackTrayMask()
	{
		if (!(m_PackTrayCameraMask == null))
		{
			m_PackTrayCameraMask.enabled = true;
		}
	}

	private void DisablePackTrayMask()
	{
		if (m_PackTrayCameraMask == null)
		{
			return;
		}
		Transform[] componentsInChildren = m_PackTrayCameraMask.m_ClipObjects.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			GameObject gameObject = componentsInChildren[i].gameObject;
			if (!(gameObject == null))
			{
				SceneUtils.SetLayer(gameObject, GameLayer.Default);
			}
		}
		m_PackTrayCameraMask.enabled = false;
	}

	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		if (!m_director.IsPlaying())
		{
			NavigateToBoxAfterDisconnect();
		}
		else
		{
			m_director.OnDoneOpeningPack += OnDonePackOpening_FatalError;
		}
	}

	private void OnDonePackOpening_FatalError()
	{
		m_director.OnDoneOpeningPack -= OnDonePackOpening_FatalError;
		if (!Network.IsLoggedIn())
		{
			NavigateToBoxAfterDisconnect();
		}
	}

	private void NavigateToBoxAfterDisconnect()
	{
		SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
		DialogManager.Get().ShowReconnectHelperDialog();
		Navigation.Clear();
	}
}
