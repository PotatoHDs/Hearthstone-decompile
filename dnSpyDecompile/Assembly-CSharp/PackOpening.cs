using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using bgs;
using Hearthstone.Progression;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

// Token: 0x02000613 RID: 1555
public class PackOpening : MonoBehaviour
{
	// Token: 0x060056F3 RID: 22259 RVA: 0x001C77EC File Offset: 0x001C59EC
	private void Awake()
	{
		PackOpening.s_instance = this;
		if (UniversalInputManager.UsePhoneUI)
		{
			AssetLoader.Get().InstantiatePrefab("PackOpeningCardFX_Phone.prefab:0ef32a20a9e7843c3ba360e49527dbfa", new PrefabCallback<GameObject>(this.OnPackOpeningFXLoaded), null, AssetLoadingOptions.None);
		}
		else
		{
			AssetLoader.Get().InstantiatePrefab("PackOpeningCardFX.prefab:b32177fb14f134edfb891dc93501b1ce", new PrefabCallback<GameObject>(this.OnPackOpeningFXLoaded), null, AssetLoadingOptions.None);
		}
		this.InitializeNet();
		this.InitializeUI();
		Box.Get().AddTransitionFinishedListener(new Box.TransitionFinishedCallback(this.OnBoxTransitionFinished));
		TelemetryWatcher.WatchFor(TelemetryWatcherWatchType.StoreFromPackOpening);
		SceneMgr.Get().RegisterScenePreUnloadEvent(new SceneMgr.ScenePreUnloadCallback(this.OnScenePreUnload));
		FiresideGatheringManager.Get().OnJoinFSG += this.OnFiresideGatheringCheckinStatusChanged;
		FiresideGatheringManager.Get().OnLeaveFSG += this.OnFiresideGatheringCheckinStatusChanged;
	}

	// Token: 0x060056F4 RID: 22260 RVA: 0x001C78BD File Offset: 0x001C5ABD
	private void Start()
	{
		Navigation.Push(new Navigation.NavigateBackHandler(this.OnNavigateBack));
		FatalErrorMgr.Get().AddErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
	}

	// Token: 0x060056F5 RID: 22261 RVA: 0x001C78E7 File Offset: 0x001C5AE7
	private void Update()
	{
		this.UpdateDraggedPack();
	}

	// Token: 0x060056F6 RID: 22262 RVA: 0x001C78F0 File Offset: 0x001C5AF0
	private void OnDestroy()
	{
		if (this.m_draggedPack != null && PegCursor.Get() != null)
		{
			PegCursor.Get().SetMode(PegCursor.Mode.STOPDRAG);
		}
		this.ShutdownNet();
		PackOpening.s_instance = null;
		if (SceneMgr.Get() != null)
		{
			SceneMgr.Get().UnregisterScenePreUnloadEvent(new SceneMgr.ScenePreUnloadCallback(this.OnScenePreUnload));
		}
		if (FiresideGatheringManager.Get() != null)
		{
			FiresideGatheringManager.Get().OnJoinFSG -= this.OnFiresideGatheringCheckinStatusChanged;
			FiresideGatheringManager.Get().OnLeaveFSG -= this.OnFiresideGatheringCheckinStatusChanged;
		}
		FatalErrorMgr.Get().RemoveErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
	}

	// Token: 0x060056F7 RID: 22263 RVA: 0x001C7997 File Offset: 0x001C5B97
	public static PackOpening Get()
	{
		return PackOpening.s_instance;
	}

	// Token: 0x060056F8 RID: 22264 RVA: 0x001C799E File Offset: 0x001C5B9E
	public GameObject GetPackOpeningCardEffects()
	{
		return this.m_PackOpeningCardFX;
	}

	// Token: 0x060056F9 RID: 22265 RVA: 0x001C79A8 File Offset: 0x001C5BA8
	public bool HandleKeyboardInput()
	{
		if (!InputCollection.GetKeyUp(KeyCode.Space))
		{
			return false;
		}
		if (this.m_director == null)
		{
			return false;
		}
		if (this.CanOpenPackAutomatically())
		{
			this.m_autoOpenPending = true;
			this.m_director.FinishPackOpen();
			this.m_autoOpenPackCoroutine = base.StartCoroutine(this.OpenNextPackWhenReady());
		}
		else if (PackOpeningDirector.QuickPackOpeningAllowed)
		{
			this.m_director.ForceRevealRandomCard();
		}
		return true;
	}

	// Token: 0x060056FA RID: 22266 RVA: 0x001C7A12 File Offset: 0x001C5C12
	private void OnScenePreUnload(SceneMgr.Mode prevMode, PegasusScene prevScene, object userData)
	{
		FullScreenFXMgr.Get().StopAllEffects(0f);
		if (this.m_director != null)
		{
			this.m_director.HideCardsAndDoneButton();
		}
		this.Hide();
	}

	// Token: 0x060056FB RID: 22267 RVA: 0x001C7A42 File Offset: 0x001C5C42
	private void NotifySceneLoadedWhenReady()
	{
		if (!this.m_waitingForInitialNetData)
		{
			return;
		}
		this.m_waitingForInitialNetData = false;
		SceneMgr.Get().NotifySceneLoaded();
	}

	// Token: 0x060056FC RID: 22268 RVA: 0x001C7A5E File Offset: 0x001C5C5E
	private void OnBoxTransitionFinished(object userData)
	{
		Box.Get().RemoveTransitionFinishedListener(new Box.TransitionFinishedCallback(this.OnBoxTransitionFinished));
		this.Show();
		this.m_openBoxTransitionFinished = true;
	}

	// Token: 0x060056FD RID: 22269 RVA: 0x001C7A84 File Offset: 0x001C5C84
	private void Show()
	{
		if (this.m_shown)
		{
			return;
		}
		this.m_shown = true;
		PresenceMgr.Get().SetStatus(new Enum[]
		{
			Global.PresenceStatus.PACKOPENING
		});
		if (!Options.Get().GetBool(Option.HAS_SEEN_PACK_OPENING, false))
		{
			NetCache.NetCacheBoosters netObject = NetCache.Get().GetNetObject<NetCache.NetCacheBoosters>();
			if (netObject != null && netObject.GetTotalNumBoosters() > 0)
			{
				Options.Get().SetBool(Option.HAS_SEEN_PACK_OPENING, true);
			}
		}
		MusicManager.Get().StartPlaylist(MusicPlaylistType.UI_PackOpening);
		this.CreateDirector();
		BnetBar.Get().RefreshCurrency();
		if (NetCache.Get().GetNetObject<NetCache.NetCacheBoosters>().BoosterStacks.Count < 2)
		{
			this.ShowHintOnUnopenedPack();
		}
		this.UpdateUIEvents();
		this.DisablePackTrayMask();
	}

	// Token: 0x060056FE RID: 22270 RVA: 0x001C7B3F File Offset: 0x001C5D3F
	private void Hide()
	{
		if (!this.m_shown)
		{
			return;
		}
		this.m_shown = false;
		this.DestroyHint();
		this.m_InputBlocker.SetActive(false);
		this.EnablePackTrayMask();
		this.UnregisterUIEvents();
		this.ShutdownNet();
	}

	// Token: 0x060056FF RID: 22271 RVA: 0x001C7B75 File Offset: 0x001C5D75
	private bool OnNavigateBack()
	{
		if (!this.m_enableBackButton || this.m_InputBlocker.activeSelf)
		{
			return false;
		}
		this.Hide();
		SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
		return true;
	}

	// Token: 0x06005700 RID: 22272 RVA: 0x001C7BA4 File Offset: 0x001C5DA4
	private void InitializeNet()
	{
		this.m_waitingForInitialNetData = true;
		NetCache.Get().RegisterScreenPackOpening(new NetCache.NetCacheCallback(this.OnNetDataReceived), new NetCache.ErrorCallback(NetCache.DefaultErrorHandler));
		Network.Get().RegisterNetHandler(BoosterContent.PacketID.ID, new Network.NetHandler(this.OnBoosterOpened), null);
		Network.Get().RegisterNetHandler(DBAction.PacketID.ID, new Network.NetHandler(this.OnDBAction), null);
		LoginManager.Get().OnAchievesLoaded += this.OnReloginComplete;
	}

	// Token: 0x06005701 RID: 22273 RVA: 0x001C7C34 File Offset: 0x001C5E34
	private void ShutdownNet()
	{
		NetCache netCache;
		if (HearthstoneServices.TryGet<NetCache>(out netCache))
		{
			netCache.UnregisterNetCacheHandler(new NetCache.NetCacheCallback(this.OnNetDataReceived));
		}
		Network network;
		if (HearthstoneServices.TryGet<Network>(out network))
		{
			network.RemoveNetHandler(BoosterContent.PacketID.ID, new Network.NetHandler(this.OnBoosterOpened));
			network.RemoveNetHandler(DBAction.PacketID.ID, new Network.NetHandler(this.OnDBAction));
		}
		LoginManager loginManager;
		if (HearthstoneServices.TryGet<LoginManager>(out loginManager))
		{
			LoginManager.Get().OnAchievesLoaded -= this.OnReloginComplete;
		}
	}

	// Token: 0x06005702 RID: 22274 RVA: 0x001C7CBE File Offset: 0x001C5EBE
	private void OnNetDataReceived()
	{
		this.NotifySceneLoadedWhenReady();
		this.UpdatePacks();
		this.UpdateUIEvents();
	}

	// Token: 0x06005703 RID: 22275 RVA: 0x001C7CD2 File Offset: 0x001C5ED2
	private void OnReloginComplete()
	{
		this.UpdatePacks();
		this.UpdateUIEvents();
	}

	// Token: 0x06005704 RID: 22276 RVA: 0x001C7CE0 File Offset: 0x001C5EE0
	private void UpdatePacks()
	{
		NetCache.NetCacheBoosters netObject = NetCache.Get().GetNetObject<NetCache.NetCacheBoosters>();
		if (netObject == null)
		{
			Debug.LogError(string.Format("PackOpening.UpdatePacks() - boosters are null", Array.Empty<object>()));
			return;
		}
		foreach (NetCache.BoosterStack boosterStack in netObject.BoosterStacks)
		{
			int id = boosterStack.Id;
			if (this.m_unopenedPacks.ContainsKey(id) && this.m_unopenedPacks[id] != null)
			{
				if (netObject.GetBoosterStack(id) == null)
				{
					UnityEngine.Object.Destroy(this.m_unopenedPacks[id]);
					this.m_unopenedPacks[id] = null;
				}
				else
				{
					this.UpdatePack(this.m_unopenedPacks[id], netObject.GetBoosterStack(id));
				}
			}
			else if (netObject.GetBoosterStack(id) != null && netObject.GetBoosterStack(id).Count > 0 && (!this.m_unopenedPacksLoading.ContainsKey(id) || !this.m_unopenedPacksLoading[id]))
			{
				this.m_unopenedPacksLoading[id] = true;
				BoosterDbfRecord record = GameDbf.Booster.GetRecord(id);
				if (record == null)
				{
					Debug.LogErrorFormat("PackOpening.UpdatePacks() - No DBF record for booster {0}", new object[]
					{
						id
					});
				}
				else if (string.IsNullOrEmpty(record.PackOpeningPrefab))
				{
					Debug.LogError(string.Format("PackOpening.UpdatePacks() - no prefab found for booster {0}!", id));
				}
				else
				{
					AssetLoader.Get().InstantiatePrefab(record.PackOpeningPrefab, new PrefabCallback<GameObject>(this.OnUnopenedPackLoaded), boosterStack, AssetLoadingOptions.IgnorePrefabPosition);
				}
			}
		}
		if (this.m_director == null || !this.m_director.IsPlaying())
		{
			this.LayoutPacks(false);
		}
	}

	// Token: 0x06005705 RID: 22277 RVA: 0x001C7EB8 File Offset: 0x001C60B8
	private void OnBoosterOpened()
	{
		this.m_director.Play(this.m_lastOpenedBoosterId);
		this.m_autoOpenPending = false;
		List<NetCache.BoosterCard> cards = Network.Get().OpenedBooster();
		this.m_director.OnBoosterOpened(cards);
	}

	// Token: 0x06005706 RID: 22278 RVA: 0x001C7EF4 File Offset: 0x001C60F4
	private void OnDBAction()
	{
		Network.DBAction dbAction = Network.Get().GetDbAction();
		if (dbAction.Action != Network.DBAction.ActionType.OPEN_BOOSTER || dbAction.Result == Network.DBAction.ResultType.SUCCESS)
		{
			return;
		}
		Debug.LogError(string.Format("PackOpening.OnDBAction - Error while opening packs: {0}", dbAction));
		NetCache.NetCacheBoosters netObject = NetCache.Get().GetNetObject<NetCache.NetCacheBoosters>();
		if (netObject != null)
		{
			NetCache.BoosterStack boosterStack = netObject.GetBoosterStack(this.m_lastOpenedBoosterId);
			int locallyPreConsumedCount = boosterStack.LocallyPreConsumedCount - 1;
			boosterStack.LocallyPreConsumedCount = locallyPreConsumedCount;
		}
		this.m_UnopenedPackScroller.Pause(false);
		this.m_InputBlocker.SetActive(false);
		this.m_autoOpenPending = false;
		this.m_unopenedPacks[this.m_lastOpenedBoosterId].AddBooster();
		this.m_unopenedPacksLoading[this.m_lastOpenedBoosterId] = false;
		BnetBar.Get().RefreshCurrency();
	}

	// Token: 0x06005707 RID: 22279 RVA: 0x001C7FAC File Offset: 0x001C61AC
	private void OnFiresideGatheringCheckinStatusChanged(FSGConfig gathering)
	{
		foreach (KeyValuePair<int, UnopenedPack> keyValuePair in this.m_unopenedPacks)
		{
			if (!(keyValuePair.Value == null))
			{
				keyValuePair.Value.UpdateState();
			}
		}
	}

	// Token: 0x06005708 RID: 22280 RVA: 0x001C8014 File Offset: 0x001C6214
	private void CreateDirector()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_DirectorPrefab.gameObject);
		this.m_director = gameObject.GetComponent<PackOpeningDirector>();
		gameObject.transform.parent = base.transform;
		TransformUtil.CopyWorld(this.m_director, this.m_Bones.m_Director);
	}

	// Token: 0x06005709 RID: 22281 RVA: 0x001C8068 File Offset: 0x001C6268
	private void PickUpBooster()
	{
		UnopenedPack creatorPack = this.m_draggedPack.GetCreatorPack();
		creatorPack.RemoveBooster();
		this.m_draggedPack.SetBoosterStack(new NetCache.BoosterStack
		{
			Id = creatorPack.GetBoosterStack().Id,
			Count = 1
		});
	}

	// Token: 0x0600570A RID: 22282 RVA: 0x001C80B0 File Offset: 0x001C62B0
	private void OpenBooster(UnopenedPack pack)
	{
		AchievementManager.Get().PauseToastNotifications();
		int num = 1;
		if (!GameUtils.IsFakePackOpeningEnabled())
		{
			num = pack.GetBoosterStack().Id;
			Network.Get().OpenBooster(num);
		}
		this.m_InputBlocker.SetActive(true);
		if (this.m_autoOpenPackCoroutine != null)
		{
			base.StopCoroutine(this.m_autoOpenPackCoroutine);
			this.m_autoOpenPackCoroutine = null;
		}
		this.m_director.AddFinishedListener(new PackOpeningDirector.FinishedCallback(this.OnDirectorFinished));
		this.m_lastOpenedBoosterId = num;
		BnetBar.Get().HideCurrencyTemporarily();
		if (GameUtils.IsFakePackOpeningEnabled())
		{
			base.StartCoroutine(this.OnFakeBoosterOpened());
		}
		this.m_UnopenedPackScroller.Pause(true);
	}

	// Token: 0x0600570B RID: 22283 RVA: 0x001C8156 File Offset: 0x001C6356
	private IEnumerator OnFakeBoosterOpened()
	{
		float seconds = UnityEngine.Random.Range(0f, 1f);
		yield return new WaitForSeconds(seconds);
		List<NetCache.BoosterCard> list = new List<NetCache.BoosterCard>();
		list.Add(new NetCache.BoosterCard
		{
			Def = 
			{
				Name = "CS1_042",
				Premium = TAG_PREMIUM.NORMAL
			}
		});
		list.Add(new NetCache.BoosterCard
		{
			Def = 
			{
				Name = "CS1_129",
				Premium = TAG_PREMIUM.NORMAL
			}
		});
		list.Add(new NetCache.BoosterCard
		{
			Def = 
			{
				Name = "EX1_050",
				Premium = TAG_PREMIUM.NORMAL
			}
		});
		list.Add(new NetCache.BoosterCard
		{
			Def = 
			{
				Name = "EX1_105",
				Premium = TAG_PREMIUM.NORMAL
			}
		});
		list.Add(new NetCache.BoosterCard
		{
			Def = 
			{
				Name = "EX1_350",
				Premium = TAG_PREMIUM.NORMAL
			}
		});
		this.m_director.OnBoosterOpened(list);
		yield break;
	}

	// Token: 0x0600570C RID: 22284 RVA: 0x001C8165 File Offset: 0x001C6365
	private void PutBackBooster()
	{
		UnopenedPack creatorPack = this.m_draggedPack.GetCreatorPack();
		this.m_draggedPack.RemoveBooster();
		creatorPack.AddBooster();
	}

	// Token: 0x0600570D RID: 22285 RVA: 0x001C8182 File Offset: 0x001C6382
	private void UpdatePack(UnopenedPack pack, NetCache.BoosterStack boosterStack)
	{
		pack.SetBoosterStack(boosterStack);
	}

	// Token: 0x0600570E RID: 22286 RVA: 0x001C818C File Offset: 0x001C638C
	private void OnUnopenedPackLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		NetCache.BoosterStack boosterStack = (NetCache.BoosterStack)callbackData;
		int id = boosterStack.Id;
		this.m_unopenedPacksLoading[id] = false;
		if (go == null)
		{
			Debug.LogError(string.Format("PackOpening.OnUnopenedPackLoaded() - FAILED to load {0}", assetRef));
			return;
		}
		UnopenedPack component = go.GetComponent<UnopenedPack>();
		go.SetActive(false);
		if (component == null)
		{
			Debug.LogError(string.Format("PackOpening.OnUnopenedPackLoaded() - asset {0} did not have a {1} script on it", base.name, typeof(UnopenedPack)));
			return;
		}
		this.m_unopenedPacks.Add(id, component);
		component.gameObject.SetActive(true);
		GameUtils.SetParent(component, this.m_UnopenedPackContainer, false);
		component.transform.localScale = Vector3.one;
		component.SetDragTolerance(this.m_DragTolerance);
		component.AddEventListener(UIEventType.PRESS, new UIEvent.Handler(this.OnUnopenedPackPress));
		component.AddEventListener(UIEventType.DRAG, new UIEvent.Handler(this.OnUnopenedPackDrag));
		component.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnUnopenedPackRollover));
		component.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnUnopenedPackRollout));
		component.AddEventListener(UIEventType.RELEASEALL, new UIEvent.Handler(this.OnUnopenedPackReleaseAll));
		this.UpdatePack(component, boosterStack);
		AchieveManager.Get().NotifyOfPacksReadyToOpen(component);
		if (NetCache.Get().GetNetObject<NetCache.NetCacheBoosters>().BoosterStacks.Count < 2)
		{
			this.LayoutPacks(false);
			this.ShowHintOnUnopenedPack();
		}
		else
		{
			this.StopHintOnUnopenedPack();
			this.LayoutPacks(false);
		}
		this.UpdateUIEvents();
	}

	// Token: 0x0600570F RID: 22287 RVA: 0x001C82F8 File Offset: 0x001C64F8
	private void LayoutPacks(bool animate = false)
	{
		IEnumerable<int> sortedPackIds = GameUtils.GetSortedPackIds(false);
		this.m_UnopenedPackContainer.ClearObjects();
		if (!this.m_openBoxTransitionFinished)
		{
			this.DisablePackTrayMask();
		}
		int num = 18;
		if (TemporaryAccountManager.IsTemporaryAccount())
		{
			UnopenedPack unopenedPack;
			this.m_unopenedPacks.TryGetValue(num, out unopenedPack);
			if (unopenedPack != null && unopenedPack.GetBoosterStack().Count > 0)
			{
				unopenedPack.gameObject.SetActive(true);
				this.m_UnopenedPackContainer.AddObject(unopenedPack, true);
			}
		}
		foreach (int num2 in sortedPackIds)
		{
			if (num != num2)
			{
				UnopenedPack unopenedPack2;
				this.m_unopenedPacks.TryGetValue(num2, out unopenedPack2);
				if (!(unopenedPack2 == null) && unopenedPack2.GetBoosterStack().Count != 0)
				{
					unopenedPack2.gameObject.SetActive(true);
					this.m_UnopenedPackContainer.AddObject(unopenedPack2, true);
				}
			}
		}
		if (this.m_OnePackCentered && this.m_UnopenedPackContainer.m_Objects.Count == 1)
		{
			this.m_UnopenedPackContainer.AddSpace(0, new Vector3(0f, 0f, 0.5f));
		}
		else if (this.m_OnePackCentered && this.m_UnopenedPackContainer.m_Objects.Count < 1)
		{
			this.m_UnopenedPackContainer.AddSpace(0);
		}
		if (animate)
		{
			this.m_UnopenedPackContainer.AnimateUpdatePositions(0.25f, iTween.EaseType.easeInOutQuad);
		}
		else
		{
			this.m_UnopenedPackContainer.UpdatePositions();
		}
		if (!this.m_openBoxTransitionFinished)
		{
			this.EnablePackTrayMask();
		}
	}

	// Token: 0x06005710 RID: 22288 RVA: 0x001C847C File Offset: 0x001C667C
	private void CreateDraggedPack(UnopenedPack creatorPack)
	{
		this.m_draggedPack = creatorPack.AcquireDraggedPack();
		Vector3 position = this.m_draggedPack.transform.position;
		RaycastHit raycastHit;
		if (UniversalInputManager.Get().GetInputHitInfo(GameLayer.DragPlane.LayerBit(), out raycastHit))
		{
			position = raycastHit.point;
		}
		float num = Vector3.Dot(Camera.main.transform.forward, Vector3.up);
		float num2 = -num / Mathf.Abs(num);
		Bounds bounds = this.m_draggedPack.GetComponent<Collider>().bounds;
		position.y += num2 * bounds.extents.y * this.m_draggedPack.transform.lossyScale.y;
		this.m_draggedPack.transform.position = position;
	}

	// Token: 0x06005711 RID: 22289 RVA: 0x001C853E File Offset: 0x001C673E
	private void DestroyDraggedPack()
	{
		this.m_UnopenedPackScroller.Pause(false);
		this.m_draggedPack.GetCreatorPack().ReleaseDraggedPack();
		this.m_draggedPack = null;
	}

	// Token: 0x06005712 RID: 22290 RVA: 0x001C8564 File Offset: 0x001C6764
	private void UpdateDraggedPack()
	{
		if (this.m_draggedPack == null)
		{
			return;
		}
		Vector3 position = this.m_draggedPack.transform.position;
		RaycastHit raycastHit;
		if (UniversalInputManager.Get().GetInputHitInfo(GameLayer.DragPlane.LayerBit(), out raycastHit))
		{
			position.x = raycastHit.point.x;
			position.z = raycastHit.point.z;
			this.m_draggedPack.transform.position = position;
		}
		if (UniversalInputManager.Get().GetMouseButtonUp(0))
		{
			this.DropPack();
		}
	}

	// Token: 0x06005713 RID: 22291 RVA: 0x001C85F5 File Offset: 0x001C67F5
	private IEnumerator HideAfterNoMorePacks()
	{
		while (!(this.m_director == null) && !(this.m_director.gameObject == null))
		{
			yield return new WaitForSeconds(0.2f);
		}
		this.Hide();
		SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
		yield break;
	}

	// Token: 0x06005714 RID: 22292 RVA: 0x001C8604 File Offset: 0x001C6804
	private void OnDirectorFinished(object userData)
	{
		this.m_UnopenedPackScroller.Pause(false);
		int num = 0;
		foreach (KeyValuePair<int, UnopenedPack> keyValuePair in this.m_unopenedPacks)
		{
			if (!(keyValuePair.Value == null))
			{
				int count = keyValuePair.Value.GetBoosterStack().Count;
				num += count;
				keyValuePair.Value.gameObject.SetActive(count > 0);
			}
		}
		if (num == 0)
		{
			base.StartCoroutine(this.HideAfterNoMorePacks());
		}
		else
		{
			this.m_InputBlocker.SetActive(false);
			this.CreateDirector();
			this.LayoutPacks(true);
		}
		BnetBar.Get().RefreshCurrency();
	}

	// Token: 0x06005715 RID: 22293 RVA: 0x001C86D0 File Offset: 0x001C68D0
	private void ShowHintOnUnopenedPack()
	{
		if (!this.m_shown)
		{
			return;
		}
		List<UnopenedPack> list = new List<UnopenedPack>();
		foreach (KeyValuePair<int, UnopenedPack> keyValuePair in this.m_unopenedPacks)
		{
			if (!(keyValuePair.Value == null) && keyValuePair.Value.CanOpenPack() && keyValuePair.Value.GetBoosterStack().Count > 0)
			{
				list.Add(keyValuePair.Value);
			}
		}
		if (list.Count < 1 || list[0] == null)
		{
			return;
		}
		if (list[0].GetBoosterStack().Id == 18)
		{
			return;
		}
		list[0].PlayAlert();
		if (Options.Get().GetBool(Option.HAS_OPENED_BOOSTER, false) || !UserAttentionManager.CanShowAttentionGrabber("PackOpening.ShowHintOnUnopenedPack"))
		{
			return;
		}
		if (this.m_hintArrow == null)
		{
			Bounds bounds = list[0].GetComponent<Collider>().bounds;
			Vector3 center = bounds.center;
			this.m_hintArrow = NotificationManager.Get().CreateBouncingArrow(UserAttentionBlocker.NONE, center, new Vector3(0f, 90f, 0f), false, 1f);
			if (this.m_hintArrow != null)
			{
				this.FixArrowScale(list[0].transform);
				Bounds bounds2 = this.m_hintArrow.bounceObject.GetComponent<Renderer>().bounds;
				center.x += bounds.extents.x + bounds2.extents.x;
				this.m_hintArrow.transform.position = center;
			}
		}
	}

	// Token: 0x06005716 RID: 22294 RVA: 0x001C888C File Offset: 0x001C6A8C
	private void StopHintOnUnopenedPack()
	{
		foreach (KeyValuePair<int, UnopenedPack> keyValuePair in this.m_unopenedPacks)
		{
			if (!(keyValuePair.Value == null) && keyValuePair.Value.CanOpenPack() && keyValuePair.Value.GetBoosterStack().Count > 0)
			{
				keyValuePair.Value.StopAlert();
				break;
			}
		}
	}

	// Token: 0x06005717 RID: 22295 RVA: 0x001C8918 File Offset: 0x001C6B18
	private void ShowHintOnSlot()
	{
		if (Options.Get().GetBool(Option.HAS_OPENED_BOOSTER, false) || !UserAttentionManager.CanShowAttentionGrabber("PackOpening.ShowHintOnSlot"))
		{
			return;
		}
		if (this.m_hintArrow == null)
		{
			this.m_hintArrow = NotificationManager.Get().CreateBouncingArrow(UserAttentionBlocker.NONE, false);
		}
		if (this.m_hintArrow != null)
		{
			this.FixArrowScale(this.m_draggedPack.transform);
			Bounds bounds = this.m_hintArrow.bounceObject.GetComponent<Renderer>().bounds;
			Vector3 position = this.m_Bones.m_Hint.position;
			position.z += bounds.extents.z;
			this.m_hintArrow.transform.position = position;
		}
	}

	// Token: 0x06005718 RID: 22296 RVA: 0x001C89D4 File Offset: 0x001C6BD4
	private void FixArrowScale(Transform parent)
	{
		Transform parent2 = this.m_hintArrow.transform.parent;
		this.m_hintArrow.transform.parent = parent;
		this.m_hintArrow.transform.localScale = Vector3.one;
		this.m_hintArrow.transform.parent = parent2;
	}

	// Token: 0x06005719 RID: 22297 RVA: 0x001C8A29 File Offset: 0x001C6C29
	private void HideHint()
	{
		if (this.m_hintArrow == null)
		{
			return;
		}
		Options.Get().SetBool(Option.HAS_OPENED_BOOSTER, true);
		UnityEngine.Object.Destroy(this.m_hintArrow.gameObject);
		this.m_hintArrow = null;
	}

	// Token: 0x0600571A RID: 22298 RVA: 0x001C8A61 File Offset: 0x001C6C61
	private void DestroyHint()
	{
		if (this.m_hintArrow == null)
		{
			return;
		}
		UnityEngine.Object.Destroy(this.m_hintArrow.gameObject);
		this.m_hintArrow = null;
	}

	// Token: 0x0600571B RID: 22299 RVA: 0x001C8A8C File Offset: 0x001C6C8C
	private void InitializeUI()
	{
		this.m_HeaderText.Text = GameStrings.Get("GLUE_PACK_OPENING_HEADER");
		this.m_BackButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnBackButtonPressed));
		this.m_DragPlane.SetActive(false);
		this.m_InputBlocker.SetActive(false);
	}

	// Token: 0x0600571C RID: 22300 RVA: 0x001C8AE0 File Offset: 0x001C6CE0
	private void UpdateUIEvents()
	{
		if (!this.m_shown)
		{
			this.UnregisterUIEvents();
			return;
		}
		if (this.m_draggedPack != null)
		{
			this.UnregisterUIEvents();
			return;
		}
		this.m_enableBackButton = true;
		this.m_BackButton.SetEnabled(true, false);
		foreach (KeyValuePair<int, UnopenedPack> keyValuePair in this.m_unopenedPacks)
		{
			if (keyValuePair.Value != null)
			{
				keyValuePair.Value.SetEnabled(true, false);
			}
		}
	}

	// Token: 0x0600571D RID: 22301 RVA: 0x001C8B84 File Offset: 0x001C6D84
	private void UnregisterUIEvents()
	{
		this.m_enableBackButton = false;
		this.m_BackButton.SetEnabled(false, false);
		foreach (KeyValuePair<int, UnopenedPack> keyValuePair in this.m_unopenedPacks)
		{
			if (keyValuePair.Value != null)
			{
				keyValuePair.Value.SetEnabled(false, false);
			}
		}
	}

	// Token: 0x0600571E RID: 22302 RVA: 0x00004EB5 File Offset: 0x000030B5
	private void OnBackButtonPressed(UIEvent e)
	{
		Navigation.GoBack();
	}

	// Token: 0x0600571F RID: 22303 RVA: 0x001C8C04 File Offset: 0x001C6E04
	private void HoldPack(UnopenedPack selectedPack)
	{
		bool flag = UniversalInputManager.Get().InputIsOver(selectedPack.gameObject);
		if (!selectedPack.CanOpenPack() || !flag)
		{
			return;
		}
		this.HideUnopenedPackTooltip();
		PegCursor.Get().SetMode(PegCursor.Mode.DRAG);
		this.m_DragPlane.SetActive(true);
		this.CreateDraggedPack(selectedPack);
		if (this.m_draggedPack != null)
		{
			TooltipPanel componentInChildren = this.m_draggedPack.GetComponentInChildren<TooltipPanel>();
			if (componentInChildren != null)
			{
				UnityEngine.Object.Destroy(componentInChildren.gameObject);
			}
		}
		this.PickUpBooster();
		selectedPack.StopAlert();
		this.ShowHintOnSlot();
		this.m_Socket.OnPackHeld();
		this.m_SocketAccent.OnPackHeld();
		this.UpdateUIEvents();
		this.m_UnopenedPackScroller.Pause(true);
	}

	// Token: 0x06005720 RID: 22304 RVA: 0x001C8CBC File Offset: 0x001C6EBC
	private void DropPack()
	{
		PegCursor.Get().SetMode(PegCursor.Mode.STOPDRAG);
		this.m_Socket.OnPackReleased();
		this.m_SocketAccent.OnPackReleased();
		if (UniversalInputManager.Get().InputIsOver(this.m_Socket.gameObject))
		{
			if (BattleNet.GetAccountCountry() == "KOR")
			{
				PackOpening.m_hasAcknowledgedKoreanWarning = true;
			}
			this.OpenBooster(this.m_draggedPack);
			this.HideHint();
		}
		else
		{
			this.PutBackBooster();
			this.DestroyHint();
		}
		this.DestroyDraggedPack();
		this.UpdateUIEvents();
		this.m_DragPlane.SetActive(false);
	}

	// Token: 0x06005721 RID: 22305 RVA: 0x001C8D50 File Offset: 0x001C6F50
	private void AutomaticallyOpenPack()
	{
		this.HideUnopenedPackTooltip();
		UnopenedPack unopenedPack = null;
		if (!this.m_unopenedPacks.TryGetValue(this.m_lastOpenedBoosterId, out unopenedPack) || unopenedPack.GetBoosterStack().Count == 0)
		{
			foreach (KeyValuePair<int, UnopenedPack> keyValuePair in this.m_unopenedPacks)
			{
				if (!(keyValuePair.Value == null) && keyValuePair.Value.GetBoosterStack().Count > 0)
				{
					unopenedPack = keyValuePair.Value;
					break;
				}
			}
		}
		if (unopenedPack == null)
		{
			return;
		}
		if (!unopenedPack.CanOpenPack())
		{
			return;
		}
		if (this.m_draggedPack != null || this.m_InputBlocker.activeSelf)
		{
			this.m_autoOpenPending = false;
			return;
		}
		this.m_draggedPack = unopenedPack.AcquireDraggedPack();
		this.PickUpBooster();
		unopenedPack.StopAlert();
		this.OpenBooster(this.m_draggedPack);
		this.DestroyDraggedPack();
		this.UpdateUIEvents();
		this.m_DragPlane.SetActive(false);
	}

	// Token: 0x06005722 RID: 22306 RVA: 0x001C8E68 File Offset: 0x001C7068
	private void OnUnopenedPackPress(UIEvent e)
	{
		if ((e.GetElement() as UnopenedPack).GetBoosterStack().Id == 18)
		{
			TemporaryAccountManager.Get().ShowHealUpDialog(GameStrings.Get("GLUE_TEMPORARY_ACCOUNT_DIALOG_HEADER_01"), GameStrings.Get("GLUE_TEMPORARY_ACCOUNT_DIALOG_BODY_03"), TemporaryAccountManager.HealUpReason.LOCKED_PACK, true, null);
			return;
		}
	}

	// Token: 0x06005723 RID: 22307 RVA: 0x001C8EA6 File Offset: 0x001C70A6
	private void OnUnopenedPackDrag(UIEvent e)
	{
		this.HoldPack(e.GetElement() as UnopenedPack);
	}

	// Token: 0x06005724 RID: 22308 RVA: 0x001C8EBC File Offset: 0x001C70BC
	private void OnUnopenedPackRollover(UIEvent e)
	{
		if (PackOpening.m_hasAcknowledgedKoreanWarning || BattleNet.GetAccountCountry() != "KOR")
		{
			return;
		}
		TooltipZone component = (e.GetElement() as UnopenedPack).GetComponent<TooltipZone>();
		if (component == null)
		{
			return;
		}
		component.ShowTooltip(string.Empty, GameStrings.Get("GLUE_PACK_OPENING_TOOLTIP"), 5f, 0);
	}

	// Token: 0x06005725 RID: 22309 RVA: 0x001C8F19 File Offset: 0x001C7119
	private void OnUnopenedPackRollout(UIEvent e)
	{
		this.HideUnopenedPackTooltip();
	}

	// Token: 0x06005726 RID: 22310 RVA: 0x001C8F24 File Offset: 0x001C7124
	private void OnUnopenedPackReleaseAll(UIEvent e)
	{
		if (this.m_draggedPack == null)
		{
			if (!UniversalInputManager.Get().IsTouchMode() && ((UIReleaseAllEvent)e).GetMouseIsOver())
			{
				if ((e.GetElement() as UnopenedPack).GetBoosterStack().Id == 18)
				{
					TemporaryAccountManager.Get().ShowHealUpDialog(GameStrings.Get("GLUE_TEMPORARY_ACCOUNT_DIALOG_HEADER_01"), GameStrings.Get("GLUE_TEMPORARY_ACCOUNT_DIALOG_BODY_03"), TemporaryAccountManager.HealUpReason.LOCKED_PACK, true, null);
					return;
				}
				this.HoldPack(e.GetElement() as UnopenedPack);
				return;
			}
		}
		else
		{
			this.DropPack();
		}
	}

	// Token: 0x06005727 RID: 22311 RVA: 0x001C8FAC File Offset: 0x001C71AC
	private void HideUnopenedPackTooltip()
	{
		foreach (KeyValuePair<int, UnopenedPack> keyValuePair in this.m_unopenedPacks)
		{
			if (!(keyValuePair.Value == null))
			{
				keyValuePair.Value.GetComponent<TooltipZone>().HideTooltip();
			}
		}
	}

	// Token: 0x06005728 RID: 22312 RVA: 0x001C9018 File Offset: 0x001C7218
	private bool CanOpenPackAutomatically()
	{
		return !PopupDisplayManager.Get().IsShowing && !this.m_autoOpenPending && this.m_shown && GameUtils.HaveBoosters() && (!this.m_director.IsPlaying() || this.m_director.IsDoneButtonShown()) && !this.m_DragPlane.activeSelf && !StoreManager.Get().IsShownOrWaitingToShow();
	}

	// Token: 0x06005729 RID: 22313 RVA: 0x001C908A File Offset: 0x001C728A
	private IEnumerator OpenNextPackWhenReady()
	{
		float waitTime = 0f;
		if (this.m_director.IsPlaying())
		{
			waitTime = 1f;
		}
		while (this.m_director.IsPlaying())
		{
			yield return null;
		}
		yield return new WaitForSeconds(waitTime);
		this.AutomaticallyOpenPack();
		yield break;
	}

	// Token: 0x0600572A RID: 22314 RVA: 0x001C9099 File Offset: 0x001C7299
	private void OnPackOpeningFXLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.m_PackOpeningCardFX = go;
	}

	// Token: 0x0600572B RID: 22315 RVA: 0x001C90A2 File Offset: 0x001C72A2
	private void EnablePackTrayMask()
	{
		if (this.m_PackTrayCameraMask == null)
		{
			return;
		}
		this.m_PackTrayCameraMask.enabled = true;
	}

	// Token: 0x0600572C RID: 22316 RVA: 0x001C90C0 File Offset: 0x001C72C0
	private void DisablePackTrayMask()
	{
		if (this.m_PackTrayCameraMask == null)
		{
			return;
		}
		Transform[] componentsInChildren = this.m_PackTrayCameraMask.m_ClipObjects.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			GameObject gameObject = componentsInChildren[i].gameObject;
			if (!(gameObject == null))
			{
				SceneUtils.SetLayer(gameObject, GameLayer.Default);
			}
		}
		this.m_PackTrayCameraMask.enabled = false;
	}

	// Token: 0x0600572D RID: 22317 RVA: 0x001C9120 File Offset: 0x001C7320
	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		if (!this.m_director.IsPlaying())
		{
			this.NavigateToBoxAfterDisconnect();
			return;
		}
		this.m_director.OnDoneOpeningPack += this.OnDonePackOpening_FatalError;
	}

	// Token: 0x0600572E RID: 22318 RVA: 0x001C914D File Offset: 0x001C734D
	private void OnDonePackOpening_FatalError()
	{
		this.m_director.OnDoneOpeningPack -= this.OnDonePackOpening_FatalError;
		if (!Network.IsLoggedIn())
		{
			this.NavigateToBoxAfterDisconnect();
		}
	}

	// Token: 0x0600572F RID: 22319 RVA: 0x001C9173 File Offset: 0x001C7373
	private void NavigateToBoxAfterDisconnect()
	{
		SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
		DialogManager.Get().ShowReconnectHelperDialog(null, null);
		Navigation.Clear();
	}

	// Token: 0x04004ADA RID: 19162
	public PackOpeningBones m_Bones;

	// Token: 0x04004ADB RID: 19163
	public PackOpeningDirector m_DirectorPrefab;

	// Token: 0x04004ADC RID: 19164
	public PackOpeningSocket m_Socket;

	// Token: 0x04004ADD RID: 19165
	public PackOpeningSocket m_SocketAccent;

	// Token: 0x04004ADE RID: 19166
	public UberText m_HeaderText;

	// Token: 0x04004ADF RID: 19167
	public UIBButton m_BackButton;

	// Token: 0x04004AE0 RID: 19168
	public GameObject m_DragPlane;

	// Token: 0x04004AE1 RID: 19169
	public Vector3 m_DragTolerance;

	// Token: 0x04004AE2 RID: 19170
	public GameObject m_InputBlocker;

	// Token: 0x04004AE3 RID: 19171
	public UIBObjectSpacing m_UnopenedPackContainer;

	// Token: 0x04004AE4 RID: 19172
	public UIBScrollable m_UnopenedPackScroller;

	// Token: 0x04004AE5 RID: 19173
	public CameraMask m_PackTrayCameraMask;

	// Token: 0x04004AE6 RID: 19174
	public float m_UnopenedPackPadding;

	// Token: 0x04004AE7 RID: 19175
	public bool m_OnePackCentered = true;

	// Token: 0x04004AE8 RID: 19176
	private const int MAX_OPENED_PACKS_BEFORE_CARD_CACHE_RESET = 10;

	// Token: 0x04004AE9 RID: 19177
	private static PackOpening s_instance;

	// Token: 0x04004AEA RID: 19178
	private bool m_waitingForInitialNetData = true;

	// Token: 0x04004AEB RID: 19179
	private bool m_shown;

	// Token: 0x04004AEC RID: 19180
	private global::Map<int, UnopenedPack> m_unopenedPacks = new global::Map<int, UnopenedPack>();

	// Token: 0x04004AED RID: 19181
	private global::Map<int, bool> m_unopenedPacksLoading = new global::Map<int, bool>();

	// Token: 0x04004AEE RID: 19182
	private PackOpeningDirector m_director;

	// Token: 0x04004AEF RID: 19183
	private UnopenedPack m_draggedPack;

	// Token: 0x04004AF0 RID: 19184
	private Notification m_hintArrow;

	// Token: 0x04004AF1 RID: 19185
	private GameObject m_PackOpeningCardFX;

	// Token: 0x04004AF2 RID: 19186
	private bool m_autoOpenPending;

	// Token: 0x04004AF3 RID: 19187
	private int m_lastOpenedBoosterId;

	// Token: 0x04004AF4 RID: 19188
	private bool m_enableBackButton;

	// Token: 0x04004AF5 RID: 19189
	private bool m_openBoxTransitionFinished;

	// Token: 0x04004AF6 RID: 19190
	private static bool m_hasAcknowledgedKoreanWarning;

	// Token: 0x04004AF7 RID: 19191
	private Coroutine m_autoOpenPackCoroutine;
}
