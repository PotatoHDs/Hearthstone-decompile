using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using PegasusUtil;
using UnityEngine;

// Token: 0x02000053 RID: 83
[CustomEditClass]
public class AdventureWing : MonoBehaviour
{
	// Token: 0x17000049 RID: 73
	// (get) Token: 0x060004C3 RID: 1219 RVA: 0x0001C41C File Offset: 0x0001A61C
	// (set) Token: 0x060004C4 RID: 1220 RVA: 0x0001C424 File Offset: 0x0001A624
	public float CoinSpacing
	{
		get
		{
			return this.m_CoinSpacing;
		}
		set
		{
			this.m_CoinSpacing = value;
			this.UpdateCoinPositions();
		}
	}

	// Token: 0x1700004A RID: 74
	// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0001C433 File Offset: 0x0001A633
	// (set) Token: 0x060004C6 RID: 1222 RVA: 0x0001C43B File Offset: 0x0001A63B
	public Vector3 CoinsOffset
	{
		get
		{
			return this.m_CoinsOffset;
		}
		set
		{
			this.m_CoinsOffset = value;
			this.UpdateCoinPositions();
		}
	}

	// Token: 0x1700004B RID: 75
	// (get) Token: 0x060004C7 RID: 1223 RVA: 0x0001C44A File Offset: 0x0001A64A
	// (set) Token: 0x060004C8 RID: 1224 RVA: 0x0001C452 File Offset: 0x0001A652
	public Vector3 CoinsChestOffset
	{
		get
		{
			return this.m_CoinsChestOffset;
		}
		set
		{
			this.m_CoinsChestOffset = value;
			this.UpdateCoinPositions();
		}
	}

	// Token: 0x1700004C RID: 76
	// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0001C461 File Offset: 0x0001A661
	// (set) Token: 0x060004CA RID: 1226 RVA: 0x0001C469 File Offset: 0x0001A669
	public bool IsDevMode { get; set; }

	// Token: 0x060004CB RID: 1227 RVA: 0x0001C472 File Offset: 0x0001A672
	protected virtual void Awake()
	{
		this.IsDevMode = (AdventureScene.Get() == null || AdventureScene.Get().IsDevMode);
	}

	// Token: 0x060004CC RID: 1228 RVA: 0x0001C494 File Offset: 0x0001A694
	private void Start()
	{
		if (this.m_UnlockButton != null)
		{
			this.m_UnlockButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.UnlockButtonPressed));
			this.m_UnlockButton.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnUnlockButtonOut));
			this.m_UnlockButton.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnUnlockButtonOver));
		}
		if (this.m_BuyButton != null)
		{
			this.m_BuyButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
			{
				if (this.IsDevMode)
				{
					this.SetPlateKeyEvent(false);
					return;
				}
				this.FireTryPurchaseWingEvent();
			});
		}
		if (this.m_RewardsPreviewButton != null)
		{
			this.m_RewardsPreviewButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
			{
				this.FireShowRewardsPreviewEvent();
			});
		}
		if (this.m_BigChest != null)
		{
			if (UniversalInputManager.UsePhoneUI)
			{
				this.m_BigChest.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.ShowBigChestRewards));
				return;
			}
			this.m_BigChest.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.ShowBigChestRewards));
			this.m_BigChest.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.HideBigChestRewards));
		}
	}

	// Token: 0x060004CD RID: 1229 RVA: 0x0001C5AE File Offset: 0x0001A7AE
	private void OnDestroy()
	{
		if (StoreManager.Get() != null)
		{
			StoreManager.Get().RemoveStatusChangedListener(new Action<bool>(this.UpdateBuyButton));
		}
	}

	// Token: 0x060004CE RID: 1230 RVA: 0x0001C5D0 File Offset: 0x0001A7D0
	private void Update()
	{
		if (this.m_WingEventTable.IsPlateInOrGoingToAnActiveState() && !this.IsDevMode)
		{
			if (!this.m_EventStartDetected && AdventureProgressMgr.IsWingEventActive((int)this.m_WingDef.GetWingId()))
			{
				this.UpdatePlateState();
			}
			else if (!this.m_Owned && AdventureProgressMgr.Get().OwnsWing((int)this.m_WingDef.GetWingId()))
			{
				this.UpdatePlateState();
			}
		}
		if (!this.IsDevMode)
		{
			return;
		}
		if (InputCollection.GetKeyDown(KeyCode.Alpha1))
		{
			this.m_LockPlate.SetActive(true);
			this.SetPlateKeyEvent(true);
		}
		if (InputCollection.GetKeyDown(KeyCode.Alpha2))
		{
			this.m_LockPlate.SetActive(true);
			this.m_WingEventTable.DoStatePlateBuy(true);
		}
		if (InputCollection.GetKeyDown(KeyCode.Alpha3))
		{
			this.m_LockPlate.SetActive(true);
			this.m_WingEventTable.DoStatePlateInitialText();
		}
		if (InputCollection.GetKeyDown(KeyCode.Alpha4))
		{
			this.m_WingEventTable.DoStatePlateDeactivate();
		}
		if (InputCollection.GetKeyDown(KeyCode.Alpha5))
		{
			this.m_LockPlate.SetActive(true);
			this.UnlockPlate();
		}
		if (InputCollection.GetKeyDown(KeyCode.Alpha6))
		{
			this.m_WingEventTable.DoStatePlateDeactivate();
			foreach (AdventureWing.Boss boss in this.m_BossCoins)
			{
				boss.m_Chest.SlamInCheckmark();
			}
		}
		if (InputCollection.GetKeyDown(KeyCode.Alpha7))
		{
			this.m_WingEventTable.DoStatePlateDeactivate();
			this.OpenBigChest();
		}
		if (InputCollection.GetKeyDown(KeyCode.Alpha8))
		{
			this.m_WingEventTable.DoStatePlateDeactivate();
			base.StartCoroutine(this.AnimateCoinsAndChests(this.m_BossCoins, 0f, null));
		}
		if (InputCollection.GetKeyDown(KeyCode.Alpha9))
		{
			this.m_LockPlate.SetActive(true);
			this.m_WingEventTable.DoStatePlateReset();
		}
	}

	// Token: 0x060004CF RID: 1231 RVA: 0x0001C794 File Offset: 0x0001A994
	public void Initialize(AdventureWingDef wingDef)
	{
		this.m_WingDef = wingDef;
		base.gameObject.name = string.Format("{0}_{1}", base.gameObject.name, (int)wingDef.GetWingId());
		foreach (UberText uberText in this.m_WingTitles)
		{
			if (uberText != null)
			{
				uberText.Text = this.m_WingDef.GetWingName();
			}
		}
		if (!string.IsNullOrEmpty(wingDef.m_UnlockSpellPrefab) && this.m_LockPlateFXContainer != null)
		{
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab(wingDef.m_UnlockSpellPrefab, AssetLoadingOptions.None);
			this.m_UnlockSpell = gameObject.GetComponent<Spell>();
			GameUtils.SetParent(this.m_UnlockSpell, this.m_LockPlateFXContainer, false);
			this.m_UnlockSpell.gameObject.SetActive(false);
		}
		this.SetAccent(wingDef.m_AccentPrefab);
		this.m_Owned = AdventureProgressMgr.Get().OwnsWing((int)wingDef.GetWingId());
		this.m_EventStartDetected = AdventureProgressMgr.IsWingEventActive((int)wingDef.GetWingId());
		this.m_Playable = (this.m_Owned && this.m_EventStartDetected);
		this.m_Locked = AdventureProgressMgr.Get().IsWingLocked(wingDef);
		this.UpdatePurchasedBanner();
		bool flag = AdventureConfig.Get().GetSelectedMode() == AdventureModeDbId.LINEAR_HEROIC;
		bool flag2 = this.HasAckedAllPlateOpenEvents();
		AdventureWingKarazhanHelper component = base.GetComponent<AdventureWingKarazhanHelper>();
		if (component != null)
		{
			component.Initialize();
		}
		if (!this.IsDevMode)
		{
			if (this.m_Playable && (flag2 || flag))
			{
				this.m_WingEventTable.DoStatePlateDeactivate();
				return;
			}
			this.UpdateBuyButton(StoreManager.Get().IsOpen(true));
			StoreManager.Get().RegisterStatusChangedListener(new Action<bool>(this.UpdateBuyButton));
			this.m_WingEventTable.DoStatePlateActivate();
			if (this.m_Locked && this.m_EventStartDetected)
			{
				this.m_WingEventTable.DoStatePlateInitialText();
				if (this.m_ReleaseLabelText != null)
				{
					this.m_ReleaseLabelText.Text = (this.m_Owned ? GameStrings.Get(this.m_WingDef.m_LockedLocString) : GameStrings.Get(this.m_WingDef.m_LockedPurchaseLocString));
					return;
				}
			}
			else
			{
				bool flag3 = AdventureProgressMgr.Get().OwnershipPrereqWingIsOwned(this.m_WingDef);
				if (!this.m_EventStartDetected)
				{
					this.m_WingEventTable.DoStatePlateInitialText();
					if (this.m_ReleaseLabelText != null)
					{
						this.m_ReleaseLabelText.Text = this.m_WingDef.GetComingSoonLabel();
						return;
					}
				}
				else
				{
					if (!this.m_Owned && flag3)
					{
						if (this.m_prologueLoadingPlayMakerFSM_KARA != null)
						{
							this.m_prologueLoadingPlayMakerFSM_KARA.SendEvent("on");
						}
						this.m_WingEventTable.DoStatePlateBuy(true);
						return;
					}
					if (this.m_Owned && !flag2)
					{
						this.SetPlateKeyEvent(true);
						return;
					}
					this.m_WingEventTable.DoStatePlateInitialText();
					if (this.m_ReleaseLabelText != null)
					{
						this.m_ReleaseLabelText.Text = this.m_WingDef.GetRequiresLabel();
					}
				}
			}
		}
	}

	// Token: 0x060004D0 RID: 1232 RVA: 0x0001CAA0 File Offset: 0x0001ACA0
	public void InitializeDevMode()
	{
		if (!this.IsDevMode)
		{
			return;
		}
		int devModeSetting = AdventureScene.Get().DevModeSetting;
		this.m_WingEventTable.DoStatePlateActivate();
		if (devModeSetting == 1)
		{
			this.SetPlateKeyEvent(true);
		}
		else if (devModeSetting == 2)
		{
			this.m_WingEventTable.DoStatePlateInitialText();
		}
		if (this.m_ReleaseLabelText != null)
		{
			this.m_ReleaseLabelText.Text = this.m_WingDef.GetComingSoonLabel();
		}
	}

	// Token: 0x060004D1 RID: 1233 RVA: 0x0001CB0C File Offset: 0x0001AD0C
	public AdventureWingDef GetWingDef()
	{
		return this.m_WingDef;
	}

	// Token: 0x060004D2 RID: 1234 RVA: 0x0001CB14 File Offset: 0x0001AD14
	public WingDbId GetWingId()
	{
		return this.m_WingDef.GetWingId();
	}

	// Token: 0x060004D3 RID: 1235 RVA: 0x0001CB24 File Offset: 0x0001AD24
	public List<AdventureRewardsChest> GetChests()
	{
		List<AdventureRewardsChest> list = new List<AdventureRewardsChest>();
		foreach (AdventureWing.Boss boss in this.m_BossCoins)
		{
			list.Add(boss.m_Chest);
		}
		return list;
	}

	// Token: 0x060004D4 RID: 1236 RVA: 0x0001CB84 File Offset: 0x0001AD84
	public AdventureDbId GetAdventureId()
	{
		return this.m_WingDef.GetAdventureId();
	}

	// Token: 0x060004D5 RID: 1237 RVA: 0x0001CB91 File Offset: 0x0001AD91
	public ProductType GetProductType()
	{
		return StoreManager.GetAdventureProductType(this.GetAdventureId());
	}

	// Token: 0x060004D6 RID: 1238 RVA: 0x0001CB9E File Offset: 0x0001AD9E
	public int GetProductData()
	{
		return (int)this.GetWingId();
	}

	// Token: 0x060004D7 RID: 1239 RVA: 0x0001CBA6 File Offset: 0x0001ADA6
	public string GetWingName()
	{
		return this.m_WingDef.GetWingName();
	}

	// Token: 0x060004D8 RID: 1240 RVA: 0x0001CBB3 File Offset: 0x0001ADB3
	public void AddBossSelectedListener(AdventureWing.BossSelected dlg)
	{
		this.m_BossSelectedListeners.Add(dlg);
	}

	// Token: 0x060004D9 RID: 1241 RVA: 0x0001CBC1 File Offset: 0x0001ADC1
	public void AddOpenPlateStartListener(AdventureWing.OpenPlateStart dlg)
	{
		this.m_OpenPlateStartListeners.Add(dlg);
	}

	// Token: 0x060004DA RID: 1242 RVA: 0x0001CBCF File Offset: 0x0001ADCF
	public void AddOpenPlateEndListener(AdventureWing.OpenPlateEnd dlg)
	{
		this.m_OpenPlateEndListeners.Add(dlg);
	}

	// Token: 0x060004DB RID: 1243 RVA: 0x0001CBDD File Offset: 0x0001ADDD
	public void AddShowRewardsListener(AdventureWing.ShowRewards dlg)
	{
		this.m_ShowRewardsListeners.Add(dlg);
	}

	// Token: 0x060004DC RID: 1244 RVA: 0x0001CBEB File Offset: 0x0001ADEB
	public void AddHideRewardsListener(AdventureWing.HideRewards dlg)
	{
		this.m_HideRewardsListeners.Add(dlg);
	}

	// Token: 0x060004DD RID: 1245 RVA: 0x0001CBF9 File Offset: 0x0001ADF9
	public void AddShowRewardsPreviewListeners(AdventureWing.ShowRewardsPreview dlg)
	{
		this.m_ShowRewardsPreviewListeners.Add(dlg);
	}

	// Token: 0x060004DE RID: 1246 RVA: 0x0001CC07 File Offset: 0x0001AE07
	public void AddTryPurchaseWingListener(AdventureWing.TryPurchaseWing dlg)
	{
		this.m_TryPurchaseWingListeners.Add(dlg);
	}

	// Token: 0x060004DF RID: 1247 RVA: 0x0001CC18 File Offset: 0x0001AE18
	public bool ContainsBossCoin(AdventureBossCoin coin)
	{
		using (List<AdventureWing.Boss>.Enumerator enumerator = this.m_BossCoins.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.m_Coin == coin)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060004E0 RID: 1248 RVA: 0x0001CC78 File Offset: 0x0001AE78
	public AdventureBossCoin CreateBoss(string coinPrefab, string rewardsPrefab, ScenarioDbId mission, bool enabled)
	{
		AdventureBossCoin newcoin = GameUtils.LoadGameObjectWithComponent<AdventureBossCoin>(coinPrefab);
		AdventureRewardsChest newchest = GameUtils.LoadGameObjectWithComponent<AdventureRewardsChest>(rewardsPrefab);
		newcoin.gameObject.name = string.Format("{0}_{1}", newcoin.gameObject.name, mission);
		if (newchest != null)
		{
			newchest.gameObject.name = string.Format("{0}_{1}", newchest.gameObject.name, mission);
			this.UpdateBossChest(newchest, mission);
		}
		if (this.m_CoinContainer != null)
		{
			GameUtils.SetParent(newcoin, this.m_CoinContainer, false);
			if (newchest != null)
			{
				GameUtils.SetParent(newchest, this.m_CoinContainer, false);
				TransformUtil.SetLocalPosY(newchest.transform, 0.01f);
			}
		}
		newcoin.Enable(enabled, false);
		newcoin.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.FireBossSelectedEvent(newcoin, mission);
		});
		if (UniversalInputManager.UsePhoneUI)
		{
			newchest.Enable(false);
			if (newcoin.m_DisabledCollider != null)
			{
				newcoin.m_DisabledCollider.AddEventListener(UIEventType.PRESS, delegate(UIEvent e)
				{
					this.ShowBossRewards(mission, newcoin.transform.position);
				});
			}
		}
		else
		{
			newchest.AddChestEventListener(UIEventType.ROLLOVER, delegate(UIEvent e)
			{
				this.ShowBossRewards(mission, newchest.transform.position);
			});
			newchest.AddChestEventListener(UIEventType.ROLLOUT, delegate(UIEvent e)
			{
				this.HideBossRewards(mission);
			});
		}
		if (this.m_BossCoins.Count == 0)
		{
			newcoin.ShowConnector(false);
		}
		this.m_BossCoins.Add(new AdventureWing.Boss
		{
			m_MissionId = mission,
			m_Coin = newcoin,
			m_Chest = newchest
		});
		this.UpdateCoinPositions();
		return newcoin;
	}

	// Token: 0x060004E1 RID: 1249 RVA: 0x0001CE8C File Offset: 0x0001B08C
	public void UpdateAllBossCoinChests()
	{
		foreach (AdventureWing.Boss boss in this.m_BossCoins)
		{
			this.UpdateBossChest(boss.m_Chest, boss.m_MissionId);
		}
	}

	// Token: 0x060004E2 RID: 1250 RVA: 0x0001CEEC File Offset: 0x0001B0EC
	public void SetAccent(string accentPrefab)
	{
		if (this.m_WallAccentObject != null)
		{
			UnityEngine.Object.Destroy(this.m_WallAccentObject);
		}
		if (this.m_PlateAccentObject != null)
		{
			UnityEngine.Object.Destroy(this.m_PlateAccentObject);
		}
		if (!string.IsNullOrEmpty(accentPrefab))
		{
			if (this.m_WallAccentContainer != null)
			{
				this.m_WallAccentObject = AssetLoader.Get().InstantiatePrefab(accentPrefab, AssetLoadingOptions.None);
				GameUtils.SetParent(this.m_WallAccentObject, this.m_WallAccentContainer, false);
			}
			if (this.m_PlateAccentContainer != null)
			{
				this.m_PlateAccentObject = UnityEngine.Object.Instantiate<GameObject>(this.m_WallAccentObject);
				GameUtils.SetParent(this.m_PlateAccentObject, this.m_PlateAccentContainer, false);
			}
		}
	}

	// Token: 0x060004E3 RID: 1251 RVA: 0x0001CF9B File Offset: 0x0001B19B
	public void SetBringToFocusCallback(AdventureWing.BringToFocusCallback dlg)
	{
		this.m_BringToFocusCallback = dlg;
	}

	// Token: 0x060004E4 RID: 1252 RVA: 0x0001CFA4 File Offset: 0x0001B1A4
	public void OpenBigChest()
	{
		if (this.m_BigChest != null)
		{
			this.m_WingEventTable.DoStateBigChestOpen();
			this.BringToFocus();
			this.m_BigChest.RemoveEventListener(UIEventType.PRESS, new UIEvent.Handler(this.ShowBigChestRewards));
			this.m_BigChest.RemoveEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.ShowBigChestRewards));
			this.m_BigChest.RemoveEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.HideBigChestRewards));
		}
	}

	// Token: 0x060004E5 RID: 1253 RVA: 0x0001D01B File Offset: 0x0001B21B
	public void HideBigChest()
	{
		this.m_WingEventTable.DoStateBigChestCover();
	}

	// Token: 0x060004E6 RID: 1254 RVA: 0x0001D028 File Offset: 0x0001B228
	public void BigChestStayOpen()
	{
		this.m_WingEventTable.DoStateBigChestStayOpen();
	}

	// Token: 0x060004E7 RID: 1255 RVA: 0x0001D038 File Offset: 0x0001B238
	public void SetBigChestRewards(WingDbId wingId)
	{
		if (AdventureConfig.Get().GetSelectedMode() != AdventureModeDbId.LINEAR)
		{
			return;
		}
		HashSet<Assets.Achieve.RewardTiming> rewardTimings = new HashSet<Assets.Achieve.RewardTiming>
		{
			Assets.Achieve.RewardTiming.ADVENTURE_CHEST
		};
		List<RewardData> rewardsForWing = AdventureProgressMgr.GetRewardsForWing((int)wingId, rewardTimings);
		if (this.m_BigChest != null)
		{
			this.m_BigChest.SetData(rewardsForWing);
		}
		AdventureWingFrozenThroneHelper component = base.GetComponent<AdventureWingFrozenThroneHelper>();
		if (component != null)
		{
			component.SetBigChestRewards(wingId);
		}
	}

	// Token: 0x060004E8 RID: 1256 RVA: 0x0001D09A File Offset: 0x0001B29A
	public List<RewardData> GetBigChestRewards()
	{
		if (!(this.m_BigChest != null))
		{
			return null;
		}
		return (List<RewardData>)this.m_BigChest.GetData();
	}

	// Token: 0x060004E9 RID: 1257 RVA: 0x0001D0BC File Offset: 0x0001B2BC
	public bool HasBigChestRewards()
	{
		return this.m_BigChest != null && this.m_BigChest.GetData() != null;
	}

	// Token: 0x060004EA RID: 1258 RVA: 0x0001D0DC File Offset: 0x0001B2DC
	public bool UpdateAndAnimateCoinsAndChests(float startDelay, bool forceCoinAnimation, AdventureWing.DelOnCoinAnimateCallback dlg)
	{
		if (this.m_WingEventTable.IsPlateInOrGoingToAnActiveState())
		{
			return false;
		}
		List<AdventureWing.Boss> list = new List<AdventureWing.Boss>();
		List<KeyValuePair<int, int>> list2 = new List<KeyValuePair<int, int>>();
		foreach (AdventureWing.Boss boss in this.m_BossCoins)
		{
			int key = 0;
			int value = 0;
			bool flag = AdventureConfig.IsMissionNewlyAvailableAndGetReqs((int)boss.m_MissionId, ref key, ref value);
			if ((forceCoinAnimation || flag) && (!forceCoinAnimation || AdventureProgressMgr.Get().CanPlayScenario((int)boss.m_MissionId, true)) && !AdventureProgressMgr.Get().HasDefeatedScenario((int)boss.m_MissionId))
			{
				list2.Add(new KeyValuePair<int, int>(key, value));
				AdventureWing.Boss boss2 = new AdventureWing.Boss();
				boss2.m_MissionId = boss.m_MissionId;
				boss2.m_Coin = boss.m_Coin;
				if (AdventureProgressMgr.Get().ScenarioHasRewardData((int)boss.m_MissionId))
				{
					boss2.m_Chest = boss.m_Chest;
				}
				list.Add(boss2);
			}
		}
		foreach (KeyValuePair<int, int> keyValuePair in list2)
		{
			if (AdventureConfig.SetWingAckIfGreater(keyValuePair.Key, keyValuePair.Value))
			{
				AdventureMissionDisplay.Get().SetWingHasJustAckedProgress(keyValuePair.Key, true);
			}
		}
		if (list.Count > 0)
		{
			base.StartCoroutine(this.AnimateCoinsAndChests(list, startDelay, dlg));
			return true;
		}
		return false;
	}

	// Token: 0x060004EB RID: 1259 RVA: 0x0001D264 File Offset: 0x0001B464
	public void UpdatePlateState()
	{
		this.UpdatePurchasedBanner();
		bool flag = AdventureProgressMgr.Get().IsWingLocked(this.m_WingDef);
		bool flag2 = AdventureProgressMgr.Get().OwnsWing((int)this.m_WingDef.GetWingId());
		bool flag3 = AdventureProgressMgr.IsWingEventActive((int)this.m_WingDef.GetWingId());
		bool flag4 = flag2 && flag3;
		this.TryToUnlockAutomatically();
		if (flag4 && this.m_Playable && !flag && !this.m_Locked)
		{
			return;
		}
		if (flag4 && !flag && !this.m_WingEventTable.IsPlateKey())
		{
			if (this.m_BuyButtonOnOppositeSideOfKey && !this.m_WingEventTable.IsPlateBuy())
			{
				this.m_WingEventTable.DoStatePlateBuy(false);
			}
			if (this.m_prologueLoadingPlayMakerFSM_KARA != null)
			{
				this.m_prologueLoadingPlayMakerFSM_KARA.SendEvent("off");
			}
			this.SetPlateKeyEvent(false);
		}
		else if (!this.m_WingEventTable.IsPlateBuy())
		{
			if (flag && flag3)
			{
				this.m_WingEventTable.DoStatePlateInitialText();
				if (this.m_ReleaseLabelText != null)
				{
					this.m_ReleaseLabelText.Text = (flag2 ? GameStrings.Get(this.m_WingDef.m_LockedLocString) : GameStrings.Get(this.m_WingDef.m_LockedPurchaseLocString));
				}
			}
			else
			{
				bool flag5 = AdventureProgressMgr.Get().OwnershipPrereqWingIsOwned(this.m_WingDef);
				if (!flag3)
				{
					this.m_WingEventTable.DoStatePlateInitialText();
					if (this.m_ReleaseLabelText != null)
					{
						this.m_ReleaseLabelText.Text = this.m_WingDef.GetComingSoonLabel();
					}
				}
				else if (!flag2 && flag5)
				{
					this.m_WingEventTable.DoStatePlateBuy(false);
				}
				else
				{
					this.m_WingEventTable.DoStatePlateInitialText();
					if (this.m_ReleaseLabelText != null)
					{
						this.m_ReleaseLabelText.Text = this.m_WingDef.GetRequiresLabel();
					}
				}
			}
		}
		this.m_EventStartDetected = flag3;
		this.m_Playable = flag4;
		this.m_Owned = flag2;
		this.m_Locked = flag;
	}

	// Token: 0x060004EC RID: 1260 RVA: 0x0001D43D File Offset: 0x0001B63D
	public void UpdateRewardsPreviewCover()
	{
		if (!this.HasRewards())
		{
			this.m_WingEventTable.DoStatePlateCoverPreviewChest();
		}
	}

	// Token: 0x060004ED RID: 1261 RVA: 0x0001D454 File Offset: 0x0001B654
	public bool HasRewards()
	{
		List<RewardData> bigChestRewards = this.GetBigChestRewards();
		if (bigChestRewards != null && bigChestRewards.Count > 0)
		{
			return true;
		}
		foreach (AdventureWing.Boss boss in this.m_BossCoins)
		{
			if (AdventureProgressMgr.Get().ScenarioHasRewardData((int)boss.m_MissionId))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060004EE RID: 1262 RVA: 0x0001D4D0 File Offset: 0x0001B6D0
	public void RandomizeBackground()
	{
		if (this.m_BackgroundOffsets.Count == 0)
		{
			return;
		}
		int num;
		do
		{
			num = UnityEngine.Random.Range(0, this.m_BackgroundOffsets.Count);
		}
		while (AdventureWing.s_LastRandomNumbers.Contains(num));
		AdventureWing.s_LastRandomNumbers.Add(num);
		if (AdventureWing.s_LastRandomNumbers.Count >= this.m_BackgroundOffsets.Count)
		{
			AdventureWing.s_LastRandomNumbers.RemoveAt(0);
		}
		foreach (AdventureWing.BackgroundRandomization backgroundRandomization in this.m_BackgroundRenderers)
		{
			if (!(backgroundRandomization.m_backgroundRenderer == null) && !string.IsNullOrEmpty(backgroundRandomization.m_materialTextureName))
			{
				Material material = backgroundRandomization.m_backgroundRenderer.GetMaterial();
				Vector2 textureOffset = material.GetTextureOffset(backgroundRandomization.m_materialTextureName);
				textureOffset.y = this.m_BackgroundOffsets[num];
				material.SetTextureOffset(backgroundRandomization.m_materialTextureName, textureOffset);
			}
		}
	}

	// Token: 0x060004EF RID: 1263 RVA: 0x0001D5C8 File Offset: 0x0001B7C8
	public void BringToFocus()
	{
		if (this.m_BringToFocusCallback != null)
		{
			this.m_BringToFocusCallback(this);
		}
	}

	// Token: 0x060004F0 RID: 1264 RVA: 0x0001D5E0 File Offset: 0x0001B7E0
	public void HideBossChests()
	{
		foreach (AdventureWing.Boss boss in this.m_BossCoins)
		{
			if (boss.m_Chest != null)
			{
				boss.m_Chest.FadeOutChestImmediate();
			}
		}
	}

	// Token: 0x060004F1 RID: 1265 RVA: 0x0001D648 File Offset: 0x0001B848
	public void NavigateBackCleanup()
	{
		if (this.m_prologueLoadingPlayMakerFSM_KARA != null)
		{
			this.m_prologueLoadingPlayMakerFSM_KARA.SendEvent("cancel");
		}
	}

	// Token: 0x060004F2 RID: 1266 RVA: 0x0001D668 File Offset: 0x0001B868
	public void GetCompleteQuoteAssetsFromTargetWingEventTiming(int targetWingId, out string completeQuotePrefab, out string completeQuoteVOLine)
	{
		completeQuotePrefab = this.m_WingDef.m_CompleteQuotePrefab;
		completeQuoteVOLine = this.m_WingDef.m_CompleteQuoteVOLine;
		if (targetWingId != 0 && !AdventureProgressMgr.IsWingEventActive(targetWingId) && !string.IsNullOrEmpty(this.m_WingDef.m_CompleteQuoteNextWingLockedPrefab) && !string.IsNullOrEmpty(this.m_WingDef.m_CompleteQuoteNextWingLockedVOLine))
		{
			completeQuotePrefab = this.m_WingDef.m_CompleteQuoteNextWingLockedPrefab;
			completeQuoteVOLine = this.m_WingDef.m_CompleteQuoteNextWingLockedVOLine;
		}
	}

	// Token: 0x060004F3 RID: 1267 RVA: 0x0001D6D8 File Offset: 0x0001B8D8
	private IEnumerator AnimateCoinsAndChests(List<AdventureWing.Boss> thingsToFlip, float delaySeconds, AdventureWing.DelOnCoinAnimateCallback dlg)
	{
		if (delaySeconds > 0f)
		{
			yield return new WaitForSeconds(delaySeconds);
		}
		if (dlg != null)
		{
			dlg(thingsToFlip[0].m_Coin.transform.position);
		}
		int num;
		for (int i = 0; i < thingsToFlip.Count; i = num)
		{
			AdventureWing.Boss boss = thingsToFlip[i];
			base.StartCoroutine(this.AnimateOneCoinAndChest(boss));
			yield return new WaitForSeconds(0.2f);
			num = i + 1;
		}
		yield return new WaitForSeconds(0.5f);
		yield break;
	}

	// Token: 0x060004F4 RID: 1268 RVA: 0x0001D6FC File Offset: 0x0001B8FC
	private IEnumerator AnimateOneCoinAndChest(AdventureWing.Boss boss)
	{
		if (boss.m_Chest != null && !AdventureProgressMgr.Get().HasDefeatedScenario((int)boss.m_MissionId))
		{
			boss.m_Chest.BlinkChest();
		}
		yield return new WaitForSeconds(0.5f);
		boss.m_Coin.Enable(true, true);
		yield return new WaitForSeconds(1f);
		if (boss.m_Chest != null && boss.m_Chest.m_fadedOut)
		{
			boss.m_Chest.FadeInChest();
		}
		boss.m_Coin.ShowNewLookGlow();
		yield break;
	}

	// Token: 0x060004F5 RID: 1269 RVA: 0x0001D70C File Offset: 0x0001B90C
	private void UpdateCoinPositions()
	{
		int num = 0;
		foreach (AdventureWing.Boss boss in this.m_BossCoins)
		{
			boss.m_Coin.transform.localPosition = this.m_CoinsOffset;
			TransformUtil.SetLocalPosX(boss.m_Coin, this.m_CoinsOffset.x + (float)num * this.m_CoinSpacing);
			if (boss.m_Chest != null)
			{
				boss.m_Chest.transform.localPosition = this.m_CoinsOffset;
				TransformUtil.SetLocalPosX(boss.m_Chest, this.m_CoinsOffset.x + (float)num * this.m_CoinSpacing);
				boss.m_Chest.transform.localPosition += this.m_CoinsChestOffset;
			}
			num++;
		}
	}

	// Token: 0x060004F6 RID: 1270 RVA: 0x0001D800 File Offset: 0x0001BA00
	private void FireBossSelectedEvent(AdventureBossCoin coin, ScenarioDbId mission)
	{
		AdventureWing.BossSelected[] array = this.m_BossSelectedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](coin, mission);
		}
	}

	// Token: 0x060004F7 RID: 1271 RVA: 0x0001D834 File Offset: 0x0001BA34
	private void FireOpenPlateStartEvent()
	{
		AdventureWing.OpenPlateStart[] array = this.m_OpenPlateStartListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](this);
		}
	}

	// Token: 0x060004F8 RID: 1272 RVA: 0x0001D864 File Offset: 0x0001BA64
	protected void FireOpenPlateEndEvent(Spell s)
	{
		if (this.m_UnlockSpell != null)
		{
			this.m_UnlockSpell.gameObject.SetActive(false);
		}
		AdventureWing.OpenPlateEnd[] array = this.m_OpenPlateEndListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](this);
		}
	}

	// Token: 0x060004F9 RID: 1273 RVA: 0x0001D8B3 File Offset: 0x0001BAB3
	private void OnUnlockButtonOut(UIEvent e)
	{
		if (this.m_UnlockButtonHighlightMesh_LOE == null)
		{
			return;
		}
		this.m_UnlockButtonHighlightMesh_LOE.GetMaterial().SetFloat("_Intensity", this.m_UnlockButtonHighlightIntensityOut);
	}

	// Token: 0x060004FA RID: 1274 RVA: 0x0001D8DF File Offset: 0x0001BADF
	private void OnUnlockButtonOver(UIEvent e)
	{
		if (this.m_UnlockButtonHighlightMesh_LOE == null)
		{
			return;
		}
		this.m_UnlockButtonHighlightMesh_LOE.GetMaterial().SetFloat("_Intensity", this.m_UnlockButtonHighlightIntensityOver);
	}

	// Token: 0x060004FB RID: 1275 RVA: 0x0001D90C File Offset: 0x0001BB0C
	private void UnlockButtonPressed(UIEvent e)
	{
		if (this.m_WingDef != null && !string.IsNullOrEmpty(this.m_WingDef.GetOpeningNotRecommendedWarning()) && !this.IsWingRecommendedToOpen())
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_text = this.m_WingDef.GetOpeningNotRecommendedWarning();
			popupInfo.m_showAlertIcon = true;
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
			popupInfo.m_responseCallback = new AlertPopup.ResponseCallback(this.OnConfirmWingUnlockResponse);
			DialogManager.Get().ShowPopup(popupInfo);
			return;
		}
		this.UnlockPlate();
	}

	// Token: 0x060004FC RID: 1276 RVA: 0x0001D98A File Offset: 0x0001BB8A
	private void OnConfirmWingUnlockResponse(AlertPopup.Response response, object userData)
	{
		if (response == AlertPopup.Response.CONFIRM)
		{
			this.UnlockPlate();
		}
	}

	// Token: 0x060004FD RID: 1277 RVA: 0x0001D998 File Offset: 0x0001BB98
	public bool TryToUnlockAutomatically()
	{
		if (this.IsDevMode)
		{
			return false;
		}
		if (!this.m_WingDef.GetUnlocksAutomatically())
		{
			return false;
		}
		if (!this.m_WingEventTable.IsPlateKey() && (!this.m_WingEventTable.IsPlatePartiallyOpen() || !this.HasDependentWingJustAckedRequiredProgress()))
		{
			return false;
		}
		if (AdventureProgressMgr.Get().IsWingLocked(this.m_WingDef))
		{
			return false;
		}
		if (!AdventureProgressMgr.Get().OwnsWing((int)this.m_WingDef.GetWingId()) || !AdventureProgressMgr.IsWingEventActive((int)this.m_WingDef.GetWingId()))
		{
			return false;
		}
		if (!this.CanPlayAtLeastOneScenario())
		{
			return false;
		}
		this.UnlockPlate();
		return true;
	}

	// Token: 0x060004FE RID: 1278 RVA: 0x0001DA34 File Offset: 0x0001BC34
	public void UnlockPlate()
	{
		if (this.m_UnlockButton != null)
		{
			this.m_UnlockButton.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.UnlockButtonPressed));
		}
		float startDelay = 0f;
		if (this.m_BringToFocusCallback != null)
		{
			startDelay = 0.5f;
			this.m_BringToFocusCallback(this);
		}
		base.StartCoroutine(this.DoUnlockPlate(startDelay));
	}

	// Token: 0x060004FF RID: 1279 RVA: 0x0001DA96 File Offset: 0x0001BC96
	private IEnumerator DoUnlockPlate(float startDelay)
	{
		this.FireOpenPlateStartEvent();
		if (startDelay > 0f)
		{
			yield return new WaitForSeconds(startDelay);
		}
		this.m_WingEventTable.AddOpenPlateEndEventListener(new StateEventTable.StateEventTrigger(this.FireOpenPlateEndEvent), true);
		if (this.m_UnlockButton != null)
		{
			this.m_UnlockButton.GetComponent<Collider>().enabled = false;
		}
		float unlockDelay = 0f;
		if (this.m_UnlockSpell != null)
		{
			AdventureWingUnlockSpell component = this.m_UnlockSpell.GetComponent<AdventureWingUnlockSpell>();
			unlockDelay = ((component != null) ? component.m_UnlockDelay : 0f);
		}
		this.DoOpenPlate(unlockDelay);
		this.m_ContentsContainer.SetActive(true);
		if (this.m_UnlockSpell != null)
		{
			this.m_UnlockSpell.gameObject.SetActive(true);
			this.m_UnlockSpell.AddFinishedCallback(new Spell.FinishedCallback(this.OnUnlockSpellFinished));
			this.m_UnlockSpell.Activate();
		}
		else
		{
			this.OnUnlockSpellFinished(null, null);
		}
		if (this.m_UnlockButton != null)
		{
			this.m_UnlockButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.UnlockButtonPressed));
		}
		yield break;
	}

	// Token: 0x06000500 RID: 1280 RVA: 0x0001DAAC File Offset: 0x0001BCAC
	protected virtual void DoOpenPlate(float unlockDelay)
	{
		int progress = AdventureProgressMgr.Get().GetProgress((int)this.m_WingDef.GetWingId()).Progress;
		int plateOpenEventIndex = progress - 1;
		int num;
		AdventureProgressMgr.Get().GetWingAck((int)this.m_WingDef.GetWingId(), out num);
		if (num < progress || this.m_HasJustAckedProgress || this.IsDevMode)
		{
			if (this.m_WingEventTable.SupportsIncrementalOpening() || num == 0)
			{
				if (this.IsDevMode)
				{
					plateOpenEventIndex = 0;
				}
				this.m_WingEventTable.DoStatePlateOpen(plateOpenEventIndex, unlockDelay);
				return;
			}
		}
		else
		{
			this.FireOpenPlateEndEvent(null);
		}
	}

	// Token: 0x06000501 RID: 1281 RVA: 0x0001DB34 File Offset: 0x0001BD34
	protected bool HasDependentWingJustAckedRequiredProgress()
	{
		foreach (AdventureMissionDbfRecord adventureMissionDbfRecord in GameDbf.AdventureMission.GetRecords((AdventureMissionDbfRecord r) => r.GrantsWingId == (int)this.m_WingDef.GetWingId(), -1))
		{
			if (AdventureMissionDisplay.Get().HasWingJustAckedRequiredProgress(adventureMissionDbfRecord.ReqWingId, adventureMissionDbfRecord.ReqProgress))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000502 RID: 1282 RVA: 0x0001DBB0 File Offset: 0x0001BDB0
	protected bool HasDependentWingJustAckedRequiredProgress(AdventureMissionDbfRecord record)
	{
		return AdventureMissionDisplay.Get().HasWingJustAckedRequiredProgress(record.ReqWingId, record.ReqProgress);
	}

	// Token: 0x06000503 RID: 1283 RVA: 0x0001DBC8 File Offset: 0x0001BDC8
	public bool HasJustAckedRequiredProgress(int requiredProgress)
	{
		int progress = AdventureProgressMgr.Get().GetProgress((int)this.m_WingDef.GetWingId()).Progress;
		return this.m_HasJustAckedProgress && progress == requiredProgress;
	}

	// Token: 0x06000504 RID: 1284 RVA: 0x0001DBFE File Offset: 0x0001BDFE
	public void SetHasJustAckedProgress(bool hasJustAckedProgress)
	{
		this.m_HasJustAckedProgress = hasJustAckedProgress;
	}

	// Token: 0x06000505 RID: 1285 RVA: 0x0001DC08 File Offset: 0x0001BE08
	private bool CanPlayAtLeastOneScenario()
	{
		foreach (AdventureWing.Boss boss in this.m_BossCoins)
		{
			if (AdventureProgressMgr.Get().CanPlayScenario((int)boss.m_MissionId, true))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000506 RID: 1286 RVA: 0x0001DC70 File Offset: 0x0001BE70
	protected virtual bool InitializePlateOpenState()
	{
		int num;
		AdventureProgressMgr.Get().GetWingAck((int)this.m_WingDef.GetWingId(), out num);
		int plateAlreadyOpenEventIndex = num - 1;
		if (num >= 1)
		{
			this.m_WingEventTable.DoStatePlateAlreadyOpen(plateAlreadyOpenEventIndex);
			return true;
		}
		return false;
	}

	// Token: 0x06000507 RID: 1287 RVA: 0x0001DCAC File Offset: 0x0001BEAC
	private void OnUnlockSpellFinished(Spell spell, object userData)
	{
		if (AdventureUtils.CanPlayWingOpenQuote(this.m_WingDef))
		{
			base.StartCoroutine(this.PlayOpenQuoteAfterDelay());
		}
	}

	// Token: 0x06000508 RID: 1288 RVA: 0x0001DCC8 File Offset: 0x0001BEC8
	private IEnumerator PlayOpenQuoteAfterDelay()
	{
		if (this.m_WingDef == null)
		{
			yield break;
		}
		yield return new WaitForSeconds(this.m_WingDef.m_OpenQuoteDelay);
		string text = this.m_WingDef.m_OpenQuotePrefab;
		if (string.IsNullOrEmpty(text))
		{
			text = AdventureScene.Get().GetAdventureDef(this.GetAdventureId()).m_DefaultQuotePrefab;
		}
		Vector3 position = UniversalInputManager.UsePhoneUI ? NotificationManager.PHONE_CHARACTER_POS : NotificationManager.ALT_ADVENTURE_SCREEN_POS;
		string legacyAssetName = new AssetReference(this.m_WingDef.m_OpenQuoteVOLine).GetLegacyAssetName();
		NotificationManager.Get().CreateCharacterQuote(text, position, GameStrings.Get(legacyAssetName), this.m_WingDef.m_OpenQuoteVOLine, this.IsDevMode, 0f, null, CanvasAnchor.BOTTOM_LEFT, false);
		yield break;
	}

	// Token: 0x06000509 RID: 1289 RVA: 0x0001DCD8 File Offset: 0x0001BED8
	protected void ShowBigChestRewards(UIEvent e)
	{
		List<RewardData> bigChestRewards = this.GetBigChestRewards();
		if (bigChestRewards == null)
		{
			return;
		}
		this.FireShowRewardsEvent(bigChestRewards, this.m_BigChest.transform.position);
	}

	// Token: 0x0600050A RID: 1290 RVA: 0x0001DD08 File Offset: 0x0001BF08
	protected void HideBigChestRewards(UIEvent e)
	{
		List<RewardData> bigChestRewards = this.GetBigChestRewards();
		if (bigChestRewards == null)
		{
			return;
		}
		this.FireHideRewardsEvent(bigChestRewards);
	}

	// Token: 0x0600050B RID: 1291 RVA: 0x0001DD28 File Offset: 0x0001BF28
	private void ShowBossRewards(ScenarioDbId mission, Vector3 origin)
	{
		List<RewardData> immediateRewardsForDefeatingScenario = AdventureProgressMgr.Get().GetImmediateRewardsForDefeatingScenario((int)mission);
		this.FireShowRewardsEvent(immediateRewardsForDefeatingScenario, origin);
	}

	// Token: 0x0600050C RID: 1292 RVA: 0x0001DD4C File Offset: 0x0001BF4C
	private void HideBossRewards(ScenarioDbId mission)
	{
		List<RewardData> immediateRewardsForDefeatingScenario = AdventureProgressMgr.Get().GetImmediateRewardsForDefeatingScenario((int)mission);
		this.FireHideRewardsEvent(immediateRewardsForDefeatingScenario);
	}

	// Token: 0x0600050D RID: 1293 RVA: 0x0001DD6C File Offset: 0x0001BF6C
	public void FireShowRewardsEvent(List<RewardData> rewards, Vector3 origin)
	{
		AdventureWing.ShowRewards[] array = this.m_ShowRewardsListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](rewards, origin);
		}
	}

	// Token: 0x0600050E RID: 1294 RVA: 0x0001DDA0 File Offset: 0x0001BFA0
	public void FireHideRewardsEvent(List<RewardData> rewards)
	{
		AdventureWing.HideRewards[] array = this.m_HideRewardsListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](rewards);
		}
	}

	// Token: 0x0600050F RID: 1295 RVA: 0x0001DDD0 File Offset: 0x0001BFD0
	private void FireShowRewardsPreviewEvent()
	{
		AdventureWing.ShowRewardsPreview[] array = this.m_ShowRewardsPreviewListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
	}

	// Token: 0x06000510 RID: 1296 RVA: 0x0001DE00 File Offset: 0x0001C000
	private void FireTryPurchaseWingEvent()
	{
		AdventureWing.TryPurchaseWing[] array = this.m_TryPurchaseWingListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
	}

	// Token: 0x06000511 RID: 1297 RVA: 0x0001DE30 File Offset: 0x0001C030
	private void UpdateBossChest(AdventureRewardsChest chest, ScenarioDbId mission)
	{
		AdventureConfig adventureConfig = AdventureConfig.Get();
		if (!adventureConfig.IsScenarioDefeatedAndInitCache(mission))
		{
			if (AdventureProgressMgr.ScenarioUsesGameSaveDataProgress((int)mission) && AdventureProgressMgr.Get().CanPlayScenario((int)mission, true))
			{
				int progress;
				int maxProgress;
				if (AdventureProgressMgr.GetGameSaveDataProgressForScenario((int)mission, out progress, out maxProgress))
				{
					chest.ShowGameSaveDataProgress(progress, maxProgress);
					return;
				}
			}
			else if (!AdventureProgressMgr.Get().ScenarioHasRewardData((int)mission))
			{
				chest.HideAll();
			}
			return;
		}
		if (adventureConfig.IsScenarioJustDefeated(mission))
		{
			chest.SlamInCheckmark();
			return;
		}
		chest.ShowCheckmark();
	}

	// Token: 0x06000512 RID: 1298 RVA: 0x0001DEA4 File Offset: 0x0001C0A4
	private void UpdatePurchasedBanner()
	{
		if (this.m_PurchasedBanner != null)
		{
			bool flag = AdventureProgressMgr.Get().OwnsWing((int)this.m_WingDef.GetWingId());
			bool flag2 = AdventureProgressMgr.IsWingEventActive((int)this.m_WingDef.GetWingId());
			this.m_PurchasedBanner.SetActive(flag && !flag2);
		}
	}

	// Token: 0x06000513 RID: 1299 RVA: 0x0001DEFC File Offset: 0x0001C0FC
	private void UpdateBuyButton(bool isStoreOpen)
	{
		if (this.m_BuyButton == null)
		{
			return;
		}
		float value = 0f;
		bool enabled = true;
		string gameStringText = "GLUE_STORE_MONEY_BUTTON_TOOLTIP_HEADLINE";
		if (!isStoreOpen)
		{
			value = 1f;
			enabled = false;
			gameStringText = "GLUE_ADVENTURE_LABEL_SHOP_CLOSED";
		}
		this.m_BuyButtonMesh.GetMaterial().SetFloat("_Desaturate", value);
		this.m_BuyButton.GetComponent<Collider>().enabled = enabled;
		this.m_BuyButtonText.SetGameStringText(gameStringText);
	}

	// Token: 0x06000514 RID: 1300 RVA: 0x0001DF6C File Offset: 0x0001C16C
	private bool HasAckedAllPlateOpenEvents()
	{
		int num;
		AdventureProgressMgr.Get().GetWingAck((int)this.m_WingDef.GetWingId(), out num);
		if (!this.m_WingEventTable.SupportsIncrementalOpening())
		{
			return num >= 1;
		}
		return this.m_WingEventTable.m_PlateOpenEvents.Count == num;
	}

	// Token: 0x06000515 RID: 1301 RVA: 0x0001DFBC File Offset: 0x0001C1BC
	private bool IsWingRecommendedToOpen()
	{
		if (this.m_WingDef.GetOpenPrereqId() == WingDbId.INVALID)
		{
			return true;
		}
		AdventureWingDef wingDef = AdventureScene.Get().GetWingDef(this.m_WingDef.GetOpenPrereqId());
		return wingDef == null || AdventureProgressMgr.Get().IsWingComplete(wingDef.GetAdventureId(), AdventureConfig.Get().GetSelectedMode(), wingDef.GetWingId());
	}

	// Token: 0x06000516 RID: 1302 RVA: 0x0001E01C File Offset: 0x0001C21C
	private void SetPlateKeyEvent(bool initial)
	{
		bool flag = this.IsWingRecommendedToOpen();
		this.m_WingEventTable.DoStatePlateKey(flag, initial);
		if (this.m_ReleaseLabelText != null)
		{
			if (!flag && !string.IsNullOrEmpty(this.m_WingDef.GetOpeningNotRecommendedLabel()))
			{
				this.m_ReleaseLabelText.Text = this.m_WingDef.GetOpeningNotRecommendedLabel();
			}
			else if (this.m_WingDef.GetUnlocksAutomatically())
			{
				this.m_ReleaseLabelText.Text = this.m_WingDef.GetWingName();
			}
			else
			{
				this.m_ReleaseLabelText.Text = GameStrings.Get("GLUE_ADVENTURE_READY_TO_OPEN");
			}
		}
		this.InitializePlateOpenState();
		this.TryToUnlockAutomatically();
	}

	// Token: 0x0400033B RID: 827
	[CustomEditField(Sections = "Wing Event Table")]
	public AdventureWingEventTable m_WingEventTable;

	// Token: 0x0400033C RID: 828
	[CustomEditField(Sections = "Containers & Bones")]
	public GameObject m_ContentsContainer;

	// Token: 0x0400033D RID: 829
	[CustomEditField(Sections = "Containers & Bones")]
	public GameObject m_CoinContainer;

	// Token: 0x0400033E RID: 830
	[CustomEditField(Sections = "Containers & Bones")]
	public GameObject m_WallAccentContainer;

	// Token: 0x0400033F RID: 831
	[CustomEditField(Sections = "Containers & Bones")]
	public GameObject m_PlateAccentContainer;

	// Token: 0x04000340 RID: 832
	[CustomEditField(Sections = "Lock Plate")]
	public GameObject m_LockPlate;

	// Token: 0x04000341 RID: 833
	[CustomEditField(Sections = "Lock Plate")]
	public GameObject m_LockPlateFXContainer;

	// Token: 0x04000342 RID: 834
	[CustomEditField(Sections = "UI")]
	public List<UberText> m_WingTitles = new List<UberText>();

	// Token: 0x04000343 RID: 835
	[CustomEditField(Sections = "UI")]
	public PegUIElement m_UnlockButton;

	// Token: 0x04000344 RID: 836
	[CustomEditField(Sections = "UI")]
	public PegUIElement m_BuyButton;

	// Token: 0x04000345 RID: 837
	[CustomEditField(Sections = "UI")]
	public MeshRenderer m_BuyButtonMesh;

	// Token: 0x04000346 RID: 838
	[CustomEditField(Sections = "UI")]
	public UberText m_BuyButtonText;

	// Token: 0x04000347 RID: 839
	[CustomEditField(Sections = "UI")]
	public UberText m_ReleaseLabelText;

	// Token: 0x04000348 RID: 840
	[CustomEditField(Sections = "UI")]
	public PegUIElement m_RewardsPreviewButton;

	// Token: 0x04000349 RID: 841
	[CustomEditField(Sections = "UI")]
	public GameObject m_PurchasedBanner;

	// Token: 0x0400034A RID: 842
	[CustomEditField(Sections = "Wing Rewards")]
	public PegUIElement m_BigChest;

	// Token: 0x0400034B RID: 843
	[CustomEditField(Sections = "Random Background Properties", ListTable = true)]
	public List<AdventureWing.BackgroundRandomization> m_BackgroundRenderers = new List<AdventureWing.BackgroundRandomization>();

	// Token: 0x0400034C RID: 844
	[CustomEditField(Sections = "Random Background Properties")]
	public List<float> m_BackgroundOffsets = new List<float>();

	// Token: 0x0400034D RID: 845
	[CustomEditField(Sections = "Special UI")]
	public bool m_BuyButtonOnOppositeSideOfKey;

	// Token: 0x0400034E RID: 846
	[CustomEditField(Sections = "Special UI/LOE")]
	public MeshRenderer m_UnlockButtonHighlightMesh_LOE;

	// Token: 0x0400034F RID: 847
	[CustomEditField(Sections = "Special UI/LOE")]
	public float m_UnlockButtonHighlightIntensityOut = 1.52f;

	// Token: 0x04000350 RID: 848
	[CustomEditField(Sections = "Special UI/LOE")]
	public float m_UnlockButtonHighlightIntensityOver = 2f;

	// Token: 0x04000351 RID: 849
	[CustomEditField(Sections = "Special UI/KARA")]
	public PlayMakerFSM m_prologueLoadingPlayMakerFSM_KARA;

	// Token: 0x04000352 RID: 850
	[SerializeField]
	private float m_CoinSpacing = 25f;

	// Token: 0x04000353 RID: 851
	[SerializeField]
	private Vector3 m_CoinsOffset = Vector3.zero;

	// Token: 0x04000354 RID: 852
	[SerializeField]
	private Vector3 m_CoinsChestOffset = Vector3.zero;

	// Token: 0x04000356 RID: 854
	protected AdventureWingDef m_WingDef;

	// Token: 0x04000357 RID: 855
	private Spell m_UnlockSpell;

	// Token: 0x04000358 RID: 856
	private GameObject m_WallAccentObject;

	// Token: 0x04000359 RID: 857
	private GameObject m_PlateAccentObject;

	// Token: 0x0400035A RID: 858
	private List<AdventureWing.Boss> m_BossCoins = new List<AdventureWing.Boss>();

	// Token: 0x0400035B RID: 859
	private AdventureWing.BringToFocusCallback m_BringToFocusCallback;

	// Token: 0x0400035C RID: 860
	private bool m_Owned;

	// Token: 0x0400035D RID: 861
	private bool m_Playable;

	// Token: 0x0400035E RID: 862
	private bool m_Locked;

	// Token: 0x0400035F RID: 863
	private bool m_EventStartDetected;

	// Token: 0x04000360 RID: 864
	private bool m_HasJustAckedProgress;

	// Token: 0x04000361 RID: 865
	private List<AdventureWing.BossSelected> m_BossSelectedListeners = new List<AdventureWing.BossSelected>();

	// Token: 0x04000362 RID: 866
	private List<AdventureWing.OpenPlateStart> m_OpenPlateStartListeners = new List<AdventureWing.OpenPlateStart>();

	// Token: 0x04000363 RID: 867
	private List<AdventureWing.OpenPlateEnd> m_OpenPlateEndListeners = new List<AdventureWing.OpenPlateEnd>();

	// Token: 0x04000364 RID: 868
	private List<AdventureWing.ShowRewards> m_ShowRewardsListeners = new List<AdventureWing.ShowRewards>();

	// Token: 0x04000365 RID: 869
	private List<AdventureWing.HideRewards> m_HideRewardsListeners = new List<AdventureWing.HideRewards>();

	// Token: 0x04000366 RID: 870
	private List<AdventureWing.ShowRewardsPreview> m_ShowRewardsPreviewListeners = new List<AdventureWing.ShowRewardsPreview>();

	// Token: 0x04000367 RID: 871
	private List<AdventureWing.TryPurchaseWing> m_TryPurchaseWingListeners = new List<AdventureWing.TryPurchaseWing>();

	// Token: 0x04000368 RID: 872
	private static List<int> s_LastRandomNumbers = new List<int>();

	// Token: 0x0200133B RID: 4923
	[Serializable]
	public class BackgroundRandomization
	{
		// Token: 0x0400A5DF RID: 42463
		public MeshRenderer m_backgroundRenderer;

		// Token: 0x0400A5E0 RID: 42464
		public string m_materialTextureName = "_MainTex";
	}

	// Token: 0x0200133C RID: 4924
	// (Invoke) Token: 0x0600D6D2 RID: 54994
	public delegate void BossSelected(AdventureBossCoin coin, ScenarioDbId mission);

	// Token: 0x0200133D RID: 4925
	// (Invoke) Token: 0x0600D6D6 RID: 54998
	public delegate void OpenPlateStart(AdventureWing wing);

	// Token: 0x0200133E RID: 4926
	// (Invoke) Token: 0x0600D6DA RID: 55002
	public delegate void OpenPlateEnd(AdventureWing wing);

	// Token: 0x0200133F RID: 4927
	// (Invoke) Token: 0x0600D6DE RID: 55006
	public delegate void ShowRewards(List<RewardData> rewards, Vector3 origin);

	// Token: 0x02001340 RID: 4928
	// (Invoke) Token: 0x0600D6E2 RID: 55010
	public delegate void HideRewards(List<RewardData> rewards);

	// Token: 0x02001341 RID: 4929
	// (Invoke) Token: 0x0600D6E6 RID: 55014
	public delegate void ShowRewardsPreview();

	// Token: 0x02001342 RID: 4930
	// (Invoke) Token: 0x0600D6EA RID: 55018
	public delegate void TryPurchaseWing();

	// Token: 0x02001343 RID: 4931
	// (Invoke) Token: 0x0600D6EE RID: 55022
	public delegate void DelOnCoinAnimateCallback(Vector3 coinPosition);

	// Token: 0x02001344 RID: 4932
	// (Invoke) Token: 0x0600D6F2 RID: 55026
	public delegate void BringToFocusCallback(AdventureWing wing);

	// Token: 0x02001345 RID: 4933
	protected class Boss
	{
		// Token: 0x0400A5E1 RID: 42465
		public ScenarioDbId m_MissionId;

		// Token: 0x0400A5E2 RID: 42466
		public AdventureBossCoin m_Coin;

		// Token: 0x0400A5E3 RID: 42467
		public AdventureRewardsChest m_Chest;
	}
}
