using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000734 RID: 1844
public class HeroicBrawlRewardDisplay : MonoBehaviour
{
	// Token: 0x06006788 RID: 26504 RVA: 0x0021BCC1 File Offset: 0x00219EC1
	private void Awake()
	{
		HeroicBrawlRewardDisplay.s_instance = this;
		this.m_doneCallbacks = new List<Action>();
	}

	// Token: 0x06006789 RID: 26505 RVA: 0x0021BCD4 File Offset: 0x00219ED4
	private void Start()
	{
		this.Init();
	}

	// Token: 0x0600678A RID: 26506 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void OnDisable()
	{
	}

	// Token: 0x0600678B RID: 26507 RVA: 0x0021BCDC File Offset: 0x00219EDC
	private void OnDestroy()
	{
		HeroicBrawlRewardDisplay.s_instance = null;
	}

	// Token: 0x0600678C RID: 26508 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void OnEnable()
	{
	}

	// Token: 0x0600678D RID: 26509 RVA: 0x0021BCE4 File Offset: 0x00219EE4
	public static HeroicBrawlRewardDisplay Get()
	{
		return HeroicBrawlRewardDisplay.s_instance;
	}

	// Token: 0x0600678E RID: 26510 RVA: 0x0021BCEC File Offset: 0x00219EEC
	public void ShowRewards(int wins, List<RewardData> rewards, bool fromNotice = false, long noticeID = -1L)
	{
		if (this.m_FSM == null)
		{
			Debug.LogErrorFormat("FSM is null!", Array.Empty<object>());
			return;
		}
		if (rewards == null && rewards.Count < 1)
		{
			Debug.LogErrorFormat("rewards is null!", Array.Empty<object>());
			return;
		}
		this.m_Rewards = rewards;
		this.m_wins = wins;
		this.m_fromNotice = fromNotice;
		this.m_noticeID = noticeID;
		this.m_DescText.SetActive(fromNotice);
		this.ShowRewardChest();
	}

	// Token: 0x0600678F RID: 26511 RVA: 0x0021BD64 File Offset: 0x00219F64
	[ContextMenu("Debug Show Rewards")]
	public void DebugShowRewards()
	{
		List<RewardData> rewards = this.DebugRewards(this.m_DebugWins);
		this.ShowRewards(this.m_DebugWins, rewards, false, -1L);
	}

	// Token: 0x06006790 RID: 26512 RVA: 0x0021BD8E File Offset: 0x00219F8E
	public void RegisterDoneCallback(Action action)
	{
		this.m_doneCallbacks.Add(action);
	}

	// Token: 0x06006791 RID: 26513 RVA: 0x0021BD9C File Offset: 0x00219F9C
	private void Init()
	{
		for (int i = 0; i < this.m_RewardZones.Length; i++)
		{
			this.m_RewardZones[i].goldReward = this.m_RewardZones[i].GoldGameObject.GetComponentInChildren<GoldReward>();
			this.m_RewardZones[i].dustReward = this.m_RewardZones[i].DustGameObject.GetComponentInChildren<ArcaneDustReward>();
		}
		this.m_FinalRewardsRoot.SetActive(false);
		this.m_RewardFireworksRoot.SetActive(false);
		this.m_PackFireworkFSM.gameObject.SetActive(false);
		this.m_GoldFireworkFSM.gameObject.SetActive(false);
		this.m_DustFireworkFSM.gameObject.SetActive(false);
		this.m_CardFireworkFSM.gameObject.SetActive(false);
	}

	// Token: 0x06006792 RID: 26514 RVA: 0x0021BE66 File Offset: 0x0021A066
	private void ShowRewardChest()
	{
		if (FullScreenFXMgr.Get() != null)
		{
			FullScreenFXMgr.Get().StartStandardBlurVignette(0.5f);
		}
		this.LoadFinalRewards();
		this.m_HeroicRewardChest.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.ShowRewardsCeremony));
	}

	// Token: 0x06006793 RID: 26515 RVA: 0x0021BE9D File Offset: 0x0021A09D
	private void ShowRewardsCeremony(UIEvent e)
	{
		base.StartCoroutine(this.AnimateRewardsCeremony());
	}

	// Token: 0x06006794 RID: 26516 RVA: 0x0021BEAC File Offset: 0x0021A0AC
	private IEnumerator AnimateRewardsCeremony()
	{
		this.m_HeroicRewardChest.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.ShowRewardsCeremony));
		HeroicBrawlRewardDisplay.RewardVisuals visuals = this.m_RewardVisuals[this.m_wins];
		this.m_DescText.SetActive(false);
		if (visuals.DropBox)
		{
			this.m_FSM.FsmVariables.GetFsmBool("ShatterDialog").Value = visuals.ShatterDialog;
			this.m_FSM.SendEvent("DropBox");
		}
		else
		{
			this.m_FSM.SendEvent("OpenBoxOnly");
		}
		while (!this.m_FSM.FsmVariables.GetFsmBool("isChestAnimationDone").Value)
		{
			yield return null;
		}
		if (visuals.DropBox)
		{
			base.StartCoroutine(this.ShowRewardsFireworks(this.m_wins));
		}
		else
		{
			base.StartCoroutine(this.ShowRewardsSimple(this.m_wins));
		}
		yield break;
	}

	// Token: 0x06006795 RID: 26517 RVA: 0x0021BEBB File Offset: 0x0021A0BB
	private IEnumerator ShowRewardsSimple(int wins)
	{
		yield return null;
		base.StartCoroutine(this.ShowFinalRewards(wins, true));
		yield break;
	}

	// Token: 0x06006796 RID: 26518 RVA: 0x0021BED1 File Offset: 0x0021A0D1
	private IEnumerator ShowRewardsFireworks(int wins)
	{
		HeroicBrawlRewardDisplay.RewardVisuals rewardvisuals = this.m_RewardVisuals[wins];
		this.m_RewardFireworksRoot.SetActive(true);
		this.LoadBoosterReward();
		this.InitRewardsReceived();
		int remainingPacks = this.m_RewardsReceived.PackCount;
		int remainingGold = this.m_RewardsReceived.GoldCount;
		int remainingDust = this.m_RewardsReceived.DustCount;
		int lastType = 0;
		while (remainingPacks > 0 || remainingGold > 0 || remainingDust > 0)
		{
			int zone = this.NextRewardZone();
			int num = UnityEngine.Random.Range(0, 4);
			if (num == lastType)
			{
				if (lastType == 0 || lastType == 1)
				{
					num = UnityEngine.Random.Range(2, 4);
				}
				else if (lastType == 2)
				{
					num = UnityEngine.Random.Range(0, 3);
					if (num == 2)
					{
						num = 3;
					}
				}
				else
				{
					num = UnityEngine.Random.Range(0, 3);
				}
			}
			if ((num == 0 || num == 1) && remainingPacks <= 0)
			{
				num = 2;
			}
			if (num == 2 && remainingGold <= 0)
			{
				num = 3;
			}
			if (num == 3 && remainingDust <= 0)
			{
				num = 0;
			}
			if ((num == 0 || num == 1) && remainingPacks <= 0)
			{
				num = 2;
			}
			if (num == 2 && remainingGold <= 0)
			{
				num = 3;
			}
			Vector3 localPosition = this.ZoneRandomLocalPosition(zone);
			if ((num == 0 || num == 1) && remainingPacks > 0)
			{
				lastType = num;
				remainingPacks--;
				base.StartCoroutine(this.DisplayFireworkPack(zone, localPosition));
				yield return new WaitForSeconds(this.GetFireworkRewardDelay());
			}
			else if (num == 2 && remainingGold > 0)
			{
				lastType = num;
				int num2 = UnityEngine.Random.Range(rewardvisuals.GoldPerBagMin, rewardvisuals.GoldPerBagMax);
				int num3 = this.m_RewardsReceived.GoldCount - num2;
				if (num3 > num2)
				{
					num3 = num2;
				}
				remainingGold -= num3;
				base.StartCoroutine(this.DisplayFireworkGold(zone, localPosition, num3));
				yield return new WaitForSeconds(this.GetFireworkRewardDelay());
			}
			else
			{
				if (num != 3 || remainingDust <= 0)
				{
					Debug.LogWarningFormat("No reward found: Packs: {0}, Gold: {1}, Dust: {2}", new object[]
					{
						remainingPacks,
						remainingGold,
						remainingDust
					});
					break;
				}
				lastType = num;
				int num4 = UnityEngine.Random.Range(rewardvisuals.DustPerBottleMin, rewardvisuals.DustPerBottleMax);
				int num5 = this.m_RewardsReceived.DustCount - num4;
				if (num5 > num4)
				{
					num5 = num4;
				}
				remainingDust -= num5;
				base.StartCoroutine(this.DisplayFireworkDust(zone, localPosition, num5));
				yield return new WaitForSeconds(this.GetFireworkRewardDelay());
			}
		}
		yield return new WaitForSeconds(1f);
		if (this.m_RewardsReceived.CardsCount > 0)
		{
			base.StartCoroutine(this.ShowCards(wins));
			yield return new WaitForSeconds((this.m_CardRewardDelay + this.m_CardRewardBurstDelay) * (float)this.m_RewardsReceived.CardsCount);
		}
		else
		{
			base.StartCoroutine(this.ShowFinalRewards(wins, false));
		}
		yield break;
	}

	// Token: 0x06006797 RID: 26519 RVA: 0x0021BEE7 File Offset: 0x0021A0E7
	private float GetFireworkRewardDelay()
	{
		return UnityEngine.Random.Range(this.m_FirewarksRewardDelayMin, this.m_FirewarksRewardDelayMax);
	}

	// Token: 0x06006798 RID: 26520 RVA: 0x0021BEFC File Offset: 0x0021A0FC
	private float DisplayFirework(PlayMakerFSM fsm, Vector3 targetPosition)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(fsm.gameObject);
		gameObject.transform.parent = base.transform;
		gameObject.transform.position = fsm.transform.position;
		gameObject.SetActive(true);
		gameObject.gameObject.layer = fsm.gameObject.layer;
		PlayMakerFSM component = gameObject.GetComponent<PlayMakerFSM>();
		component.FsmVariables.FindFsmVector3("TargetPosition").Value = targetPosition;
		component.SendEvent("Firework");
		this.m_FSM.SendEvent("BounceBox");
		return component.FsmVariables.GetFsmFloat("FireworkTime").Value;
	}

	// Token: 0x06006799 RID: 26521 RVA: 0x0021BFA2 File Offset: 0x0021A1A2
	private IEnumerator DisplayFireworkPack(int zone, Vector3 localPosition)
	{
		Vector3 targetPosition = this.m_RewardZones[zone].ZoneRoot.transform.TransformPoint(localPosition);
		float seconds = this.DisplayFirework(this.m_PackFireworkFSM, targetPosition);
		yield return new WaitForSeconds(seconds);
		GameObject packGO = UnityEngine.Object.Instantiate<GameObject>(this.m_RewardZones[zone].packReward.gameObject);
		packGO.layer = this.m_RewardZones[zone].PackGameObject.layer;
		packGO.transform.parent = this.m_RewardZones[zone].ZoneRoot.transform;
		packGO.transform.localPosition = localPosition;
		float y = UnityEngine.Random.Range(-this.m_FireworksRewardRanRot, this.m_FireworksRewardRanRot);
		packGO.transform.localEulerAngles = new Vector3(0f, y, 0f);
		packGO.transform.localScale = this.m_RewardZones[zone].PackGameObject.transform.localScale;
		BoosterPackReward packReward = packGO.GetComponent<BoosterPackReward>();
		packReward.SetData(new BoosterPackRewardData
		{
			Count = 1,
			Id = this.m_RewardsReceived.PackID
		}, true);
		packReward.m_RotateIn = false;
		packReward.m_showBanner = false;
		packReward.m_playSounds = false;
		yield return null;
		packReward.Show(true);
		yield return new WaitForSeconds(this.m_FirewarksRewardHold);
		packReward.HideWithFX();
		this.m_RewardZones[zone].packReward.Hide(true);
		yield return new WaitForSeconds(3f);
		if (packGO != null)
		{
			UnityEngine.Object.Destroy(packGO);
		}
		yield break;
	}

	// Token: 0x0600679A RID: 26522 RVA: 0x0021BFBF File Offset: 0x0021A1BF
	private IEnumerator DisplayFireworkGold(int zone, Vector3 localPosition, int amount)
	{
		Vector3 targetPosition = this.m_RewardZones[zone].ZoneRoot.transform.TransformPoint(localPosition);
		float seconds = this.DisplayFirework(this.m_GoldFireworkFSM, targetPosition);
		yield return new WaitForSeconds(seconds);
		GoldRewardData data = new GoldRewardData();
		data.Amount = (long)amount;
		GameObject goldGO = UnityEngine.Object.Instantiate<GameObject>(this.m_RewardZones[zone].goldReward.gameObject);
		goldGO.layer = this.m_RewardZones[zone].GoldGameObject.layer;
		goldGO.transform.parent = this.m_RewardZones[zone].ZoneRoot.transform;
		goldGO.transform.localPosition = localPosition;
		float y = UnityEngine.Random.Range(-this.m_FireworksRewardRanRot, this.m_FireworksRewardRanRot);
		goldGO.transform.localEulerAngles = new Vector3(0f, y, 0f);
		goldGO.transform.localScale = this.m_RewardZones[zone].GoldGameObject.transform.localScale;
		GoldReward goldReward = goldGO.GetComponent<GoldReward>();
		yield return null;
		goldReward.SetData(data, true);
		goldReward.m_RotateIn = false;
		goldReward.m_showBanner = false;
		goldReward.m_playSounds = false;
		goldReward.Show(true);
		yield return new WaitForSeconds(this.m_FirewarksRewardHold);
		goldReward.HideWithFX();
		yield return new WaitForSeconds(3f);
		if (goldGO != null)
		{
			UnityEngine.Object.Destroy(goldGO);
		}
		yield break;
	}

	// Token: 0x0600679B RID: 26523 RVA: 0x0021BFE3 File Offset: 0x0021A1E3
	private IEnumerator DisplayFireworkDust(int zone, Vector3 localPosition, int amount)
	{
		Vector3 targetPosition = this.m_RewardZones[zone].ZoneRoot.transform.TransformPoint(localPosition);
		float seconds = this.DisplayFirework(this.m_DustFireworkFSM, targetPosition);
		yield return new WaitForSeconds(seconds);
		ArcaneDustRewardData data = new ArcaneDustRewardData();
		data.Amount = amount;
		data.MarkAsDummyReward();
		GameObject dustGO = UnityEngine.Object.Instantiate<GameObject>(this.m_RewardZones[zone].dustReward.gameObject);
		dustGO.layer = this.m_RewardZones[zone].DustGameObject.layer;
		dustGO.transform.parent = this.m_RewardZones[zone].ZoneRoot.transform;
		dustGO.transform.localPosition = localPosition;
		float y = UnityEngine.Random.Range(-this.m_FireworksRewardRanRot, this.m_FireworksRewardRanRot);
		dustGO.transform.localEulerAngles = new Vector3(0f, y, 0f);
		dustGO.transform.localScale = this.m_RewardZones[zone].DustGameObject.transform.localScale;
		ArcaneDustReward dustReward = dustGO.GetComponent<ArcaneDustReward>();
		yield return null;
		dustReward.SetData(data, true);
		dustReward.m_showBanner = false;
		dustReward.m_playSounds = false;
		dustReward.Show(true);
		yield return new WaitForSeconds(this.m_FirewarksRewardHold);
		dustReward.HideWithFX();
		yield return new WaitForSeconds(3f);
		if (dustGO != null)
		{
			UnityEngine.Object.Destroy(dustGO);
		}
		yield break;
	}

	// Token: 0x0600679C RID: 26524 RVA: 0x0021C008 File Offset: 0x0021A208
	private Vector3 ZoneRandomLocalPosition(int zone)
	{
		Bounds bounds = this.m_RewardZones[zone].Collider.bounds;
		float x = UnityEngine.Random.Range(-bounds.extents.x, bounds.extents.x);
		float z = UnityEngine.Random.Range(-bounds.extents.z, bounds.extents.z);
		return new Vector3(x, 0f, z);
	}

	// Token: 0x0600679D RID: 26525 RVA: 0x0021C076 File Offset: 0x0021A276
	private IEnumerator ShowCards(int wins)
	{
		if (this.m_RewardsReceived.CardsCount == 0)
		{
			base.StartCoroutine(this.ShowFinalRewards(wins, false));
			yield break;
		}
		int cardNum = 0;
		int num;
		for (int i = 0; i < this.m_Rewards.Count; i = num + 1)
		{
			if (this.m_Rewards[i].RewardType == Reward.Type.CARD)
			{
				GameObject cardRoot = this.m_CardVisuals[this.m_RewardsReceived.CardsCount - 1].m_Cards[cardNum];
				if (cardRoot == null)
				{
					Debug.LogWarningFormat("ShowCards() m_CardVisuals[{0}].m_Cards[{1}] is null!", new object[]
					{
						this.m_RewardsReceived.CardsCount,
						cardNum
					});
				}
				else
				{
					Vector3 position = cardRoot.transform.position;
					float seconds = this.DisplayFirework(this.m_CardFireworkFSM, position);
					yield return new WaitForSeconds(seconds);
					cardRoot.SetActive(true);
					yield return null;
					cardRoot.GetComponentInChildren<CardBurstLegendary>().Activate();
					yield return new WaitForSeconds(this.m_CardRewardBurstDelay);
					CardReward componentInChildren = cardRoot.GetComponentInChildren<CardReward>();
					componentInChildren.SetData(this.m_Rewards[i], true);
					componentInChildren.m_showBanner = false;
					componentInChildren.m_showCardCount = false;
					componentInChildren.m_RotateIn = false;
					componentInChildren.Show(false);
					yield return new WaitForSeconds(this.m_CardRewardDelay);
					num = cardNum;
					cardNum = num + 1;
					cardRoot = null;
				}
			}
			num = i;
		}
		if (this.m_RewardsReceived.CardsCount >= 3)
		{
			for (int j = 0; j < this.m_CardVisuals[this.m_RewardsReceived.CardsCount - 1].m_Cards.Length; j++)
			{
				UberFloaty componentInChildren2 = this.m_CardVisuals[this.m_RewardsReceived.CardsCount - 1].m_Cards[j].GetComponentInChildren<UberFloaty>();
				if (componentInChildren2 != null)
				{
					componentInChildren2.enabled = false;
				}
				Hashtable args = iTween.Hash(new object[]
				{
					"position",
					this.m_CardVisuals[this.m_RewardsReceived.CardsCount - 1].m_CardTargets[j].transform.position,
					"time",
					this.m_CardAnimationTime,
					"easetype",
					iTween.EaseType.easeInOutCubic,
					"islocal",
					false
				});
				iTween.MoveTo(this.m_CardVisuals[this.m_RewardsReceived.CardsCount - 1].m_Cards[j], args);
				Hashtable args2 = iTween.Hash(new object[]
				{
					"rotation",
					this.m_CardVisuals[this.m_RewardsReceived.CardsCount - 1].m_CardTargets[j].transform.localEulerAngles,
					"time",
					this.m_CardAnimationTime,
					"easetype",
					iTween.EaseType.easeInOutCubic,
					"islocal",
					true
				});
				iTween.RotateTo(this.m_CardVisuals[this.m_RewardsReceived.CardsCount - 1].m_Cards[j], args2);
			}
		}
		yield return new WaitForSeconds(this.m_CardAnimationTime * 0.5f);
		base.StartCoroutine(this.ShowFinalRewards(wins, false));
		if (this.m_RewardsReceived.CardsCount >= 3)
		{
			yield return new WaitForSeconds(this.m_CardAnimationTime * 0.5f);
			for (int k = 0; k < this.m_CardVisuals[this.m_RewardsReceived.CardsCount - 1].m_Cards.Length; k++)
			{
				UberFloaty componentInChildren3 = this.m_CardVisuals[this.m_RewardsReceived.CardsCount - 1].m_Cards[k].GetComponentInChildren<UberFloaty>();
				if (componentInChildren3 != null)
				{
					componentInChildren3.enabled = true;
				}
			}
		}
		PlayMakerFSM component = this.m_CardsRoot.GetComponent<PlayMakerFSM>();
		if (component != null)
		{
			component.SendEvent("Birth");
		}
		yield break;
	}

	// Token: 0x0600679E RID: 26526 RVA: 0x0021C08C File Offset: 0x0021A28C
	private void InitRewardsReceived()
	{
		this.m_RewardsReceived.PackID = 1;
		this.m_RewardsReceived.PackCount = 0;
		this.m_RewardsReceived.DustCount = 0;
		this.m_RewardsReceived.GoldCount = 0;
		this.m_RewardsReceived.CardsCount = 0;
		this.m_RewardsReceived.Cards = new List<HeroicBrawlRewardDisplay.RewardCardReceived>();
		for (int i = 0; i < this.m_finalRewards.Count; i++)
		{
			Reward reward = this.m_finalRewards[i];
			switch (reward.RewardType)
			{
			case Reward.Type.ARCANE_DUST:
			{
				ArcaneDustRewardData arcaneDustRewardData = (ArcaneDustRewardData)reward.Data;
				this.m_RewardsReceived.DustCount = arcaneDustRewardData.Amount;
				break;
			}
			case Reward.Type.BOOSTER_PACK:
			{
				BoosterPackRewardData boosterPackRewardData = (BoosterPackRewardData)reward.Data;
				this.m_RewardsReceived.PackID = boosterPackRewardData.Id;
				this.m_RewardsReceived.PackCount = boosterPackRewardData.Count;
				break;
			}
			case Reward.Type.CARD:
			{
				CardRewardData cardRewardData = (CardRewardData)reward.Data;
				HeroicBrawlRewardDisplay.RewardCardReceived rewardCardReceived = default(HeroicBrawlRewardDisplay.RewardCardReceived);
				rewardCardReceived.CardID = cardRewardData.CardID;
				rewardCardReceived.Premium = cardRewardData.Premium;
				EntityDef entityDef = DefLoader.Get().GetEntityDef(rewardCardReceived.CardID);
				if (entityDef == null)
				{
					Debug.LogWarningFormat("InitRewardsReceived() - entityDef for Card ID {0} is null", new object[]
					{
						rewardCardReceived.CardID
					});
					return;
				}
				rewardCardReceived.CardEntityDef = entityDef;
				this.m_RewardsReceived.Cards.Add(rewardCardReceived);
				this.m_RewardsReceived.CardsCount = this.m_RewardsReceived.CardsCount + 1;
				break;
			}
			case Reward.Type.GOLD:
			{
				GoldRewardData goldRewardData = (GoldRewardData)reward.Data;
				this.m_RewardsReceived.GoldCount = (int)goldRewardData.Amount;
				break;
			}
			}
		}
	}

	// Token: 0x0600679F RID: 26527 RVA: 0x0021C248 File Offset: 0x0021A448
	private int NextRewardZone()
	{
		int num = UnityEngine.Random.Range(0, this.m_RewardZones.Length);
		if (num == this.m_lastZone)
		{
			num = UnityEngine.Random.Range(0, this.m_RewardZones.Length);
			if (num == this.m_lastZone)
			{
				num++;
				if (num >= this.m_RewardZones.Length)
				{
					num = 0;
				}
			}
		}
		this.m_lastZone = num;
		return num;
	}

	// Token: 0x060067A0 RID: 26528 RVA: 0x0021C29E File Offset: 0x0021A49E
	private IEnumerator ShowFinalRewards(int wins, bool simpleRewards = false)
	{
		while (!this.IsFinalRewardsLoaded())
		{
			yield return null;
		}
		this.m_FSM.SendEvent("HideBox");
		this.m_FinalRewardsRoot.SetActive(true);
		string text;
		if (wins == 0)
		{
			text = GameStrings.Get("GLUE_HEROIC_BRAWL_NO_WINS_REWARD_PACK_TEXT");
		}
		else
		{
			text = GameStrings.Format("GLUE_HEROIC_BRAWL_REWARDS_WIN_BANNER_TEXT", new object[]
			{
				wins,
				wins
			});
		}
		this.m_BannerUberText.Text = text;
		HeroicBrawlRewardDisplay.RewardVisuals rewardVisuals = this.m_RewardVisuals[wins];
		for (int i = 0; i < this.m_finalRewards.Count; i++)
		{
			Reward reward = this.m_finalRewards[i];
			reward.m_playSounds = false;
			reward.m_showBanner = false;
			Transform transform = null;
			Reward.Type rewardType = reward.RewardType;
			if (rewardType != Reward.Type.ARCANE_DUST)
			{
				if (rewardType != Reward.Type.BOOSTER_PACK)
				{
					if (rewardType == Reward.Type.GOLD)
					{
						reward.transform.parent = rewardVisuals.m_FinalGoldBone;
						reward.Show(false);
						transform = rewardVisuals.m_FinalGoldBone;
					}
				}
				else
				{
					reward.transform.parent = rewardVisuals.m_FinalPacksBone;
					reward.Show(false);
					transform = rewardVisuals.m_FinalPacksBone;
				}
			}
			else
			{
				reward.transform.parent = rewardVisuals.m_FinalDustBone;
				reward.Show(false);
				transform = rewardVisuals.m_FinalDustBone;
			}
			if (simpleRewards && transform != null)
			{
				PlayMakerFSM component = transform.GetComponent<PlayMakerFSM>();
				if (component != null)
				{
					component.SendEvent("Birth");
				}
			}
			if (!simpleRewards)
			{
				PlayMakerFSM component2 = this.m_FinalRewardsRoot.GetComponent<PlayMakerFSM>();
				if (component2 != null)
				{
					component2.SendEvent("Birth");
				}
			}
			reward.transform.localPosition = Vector3.zero;
			reward.transform.localRotation = Quaternion.identity;
			reward.transform.localScale = Vector3.one;
		}
		this.AllDone();
		yield break;
	}

	// Token: 0x060067A1 RID: 26529 RVA: 0x0021C2BC File Offset: 0x0021A4BC
	private void LoadFinalRewards()
	{
		this.m_finalRewardsLoadedCount = 0;
		for (int i = 0; i < this.m_Rewards.Count; i++)
		{
			this.m_Rewards[i].LoadRewardObject(new Reward.DelOnRewardLoaded(this.FinalRewardLoaded));
		}
	}

	// Token: 0x060067A2 RID: 26530 RVA: 0x0021C303 File Offset: 0x0021A503
	private bool IsFinalRewardsLoaded()
	{
		return this.m_finalRewardsLoadedCount >= this.m_Rewards.Count;
	}

	// Token: 0x060067A3 RID: 26531 RVA: 0x0021C31C File Offset: 0x0021A51C
	private void FinalRewardLoaded(Reward reward, object callbackData)
	{
		this.m_finalRewardsLoadedCount++;
		if (reward == null)
		{
			Debug.LogWarningFormat("HeroicBrawlRewardDisplay.FinalRewardLoaded() - FAILED to load reward", Array.Empty<object>());
			return;
		}
		if (reward.gameObject == null)
		{
			Debug.LogWarningFormat("HeroicBrawlRewardDisplay.FinalRewardLoaded() - reward GameObject is null", Array.Empty<object>());
			return;
		}
		reward.gameObject.layer = base.gameObject.layer;
		this.m_finalRewards.Add(reward);
	}

	// Token: 0x060067A4 RID: 26532 RVA: 0x0021C390 File Offset: 0x0021A590
	private void AllDone()
	{
		this.m_DoneButton.gameObject.SetActive(true);
		Spell component = this.m_DoneButton.m_button.GetComponent<Spell>();
		component.AddFinishedCallback(new Spell.FinishedCallback(this.OnDoneButtonShown));
		component.ActivateState(SpellStateType.BIRTH);
	}

	// Token: 0x060067A5 RID: 26533 RVA: 0x0021C3CB File Offset: 0x0021A5CB
	private void OnDoneButtonShown(Spell spell, object userData)
	{
		this.m_DoneButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnDoneButtonPressed));
	}

	// Token: 0x060067A6 RID: 26534 RVA: 0x0021C3E8 File Offset: 0x0021A5E8
	private void OnDoneButtonPressed(UIEvent e)
	{
		this.m_DoneButton.m_button.GetComponent<Spell>().ActivateState(SpellStateType.DEATH);
		iTween.ScaleTo(this.m_Root, Vector3.zero, this.m_EndScaleAwayTime);
		FullScreenFXMgr.Get().DisableBlur();
		if (this.m_fromNotice)
		{
			Network.Get().AckNotice(this.m_noticeID);
		}
		foreach (Action action in this.m_doneCallbacks)
		{
			if (action != null)
			{
				action();
			}
		}
		base.StartCoroutine(this.OnDone());
	}

	// Token: 0x060067A7 RID: 26535 RVA: 0x0021C498 File Offset: 0x0021A698
	private IEnumerator OnDone()
	{
		yield return new WaitForSeconds(this.m_EndScaleAwayTime);
		UnityEngine.Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x060067A8 RID: 26536 RVA: 0x0021C4A8 File Offset: 0x0021A6A8
	private void LoadRewardCards()
	{
		for (int i = 0; i < this.m_RewardsReceived.Cards.Count; i++)
		{
			HeroicBrawlRewardDisplay.RewardCardReceived rewardCardReceived = this.m_RewardsReceived.Cards[i];
			string handActor = ActorNames.GetHandActor(rewardCardReceived.CardEntityDef);
			GameObject cardGameObject = AssetLoader.Get().InstantiatePrefab(handActor, AssetLoadingOptions.IgnorePrefabPosition);
			HeroicBrawlRewardDisplay.RewardCardReceived value = default(HeroicBrawlRewardDisplay.RewardCardReceived);
			value.CardGameObject = cardGameObject;
			value.CardID = rewardCardReceived.CardID;
			value.CardEntityDef = rewardCardReceived.CardEntityDef;
			value.Premium = rewardCardReceived.Premium;
			this.m_RewardsReceived.Cards[i] = value;
		}
	}

	// Token: 0x060067A9 RID: 26537 RVA: 0x0021C550 File Offset: 0x0021A750
	private void LoadBoosterReward()
	{
		string input = "BoosterPackReward.prefab:b3f2b69bf55efe2419ca6d55c46f7fa7";
		for (int i = 0; i < this.m_RewardZones.Length; i++)
		{
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab(input, AssetLoadingOptions.IgnorePrefabPosition);
			gameObject.transform.parent = this.m_RewardZones[i].PackGameObject.transform;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.transform.localScale = Vector3.one;
			this.m_RewardZones[i].packReward = this.m_RewardZones[i].PackGameObject.GetComponentInChildren<BoosterPackReward>();
		}
	}

	// Token: 0x060067AA RID: 26538 RVA: 0x0021C604 File Offset: 0x0021A804
	private List<RewardData> DebugRewards(int wins)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		switch (wins)
		{
		case 0:
			num = 1;
			break;
		case 1:
			num = 2;
			break;
		case 2:
			num = 3;
			break;
		case 3:
			num = 4;
			num2 = 200;
			num3 = 200;
			break;
		case 4:
			num = 5;
			num2 = 350;
			num3 = 350;
			break;
		case 5:
			num = 6;
			num2 = 400;
			num3 = 400;
			break;
		case 6:
			num = 7;
			num2 = 450;
			num3 = 450;
			break;
		case 7:
			num = 8;
			num2 = 500;
			num3 = 500;
			break;
		case 8:
			num = 9;
			num2 = 550;
			num3 = 550;
			break;
		case 9:
			num = 14;
			num2 = 800;
			num3 = 800;
			break;
		case 10:
			num = 15;
			num2 = 950;
			num3 = 950;
			num4 = 1;
			break;
		case 11:
			num = 20;
			num2 = 1000;
			num3 = 1000;
			num4 = 2;
			break;
		case 12:
			num = 50;
			num2 = 2300;
			num3 = 2300;
			num4 = 3;
			break;
		}
		List<RewardData> list = new List<RewardData>();
		if (num > 0)
		{
			BoosterPackRewardData boosterPackRewardData = new BoosterPackRewardData();
			boosterPackRewardData.Count = num;
			boosterPackRewardData.Id = 11;
			boosterPackRewardData.MarkAsDummyReward();
			list.Add(boosterPackRewardData);
		}
		if (num3 > 0)
		{
			GoldRewardData goldRewardData = new GoldRewardData();
			goldRewardData.Amount = (long)num3;
			goldRewardData.MarkAsDummyReward();
			list.Add(goldRewardData);
		}
		if (num2 > 0)
		{
			ArcaneDustRewardData arcaneDustRewardData = new ArcaneDustRewardData();
			arcaneDustRewardData.Amount = num2;
			arcaneDustRewardData.MarkAsDummyReward();
			list.Add(arcaneDustRewardData);
		}
		if (num4 > 0)
		{
			string[] array = new string[]
			{
				"NEW1_030",
				"NEW1_030",
				"NEW1_030"
			};
			for (int i = 0; i < num4; i++)
			{
				CardRewardData cardRewardData = new CardRewardData();
				cardRewardData.CardID = array[i];
				cardRewardData.Premium = TAG_PREMIUM.GOLDEN;
				cardRewardData.MarkAsDummyReward();
				list.Add(cardRewardData);
			}
		}
		return list;
	}

	// Token: 0x04005541 RID: 21825
	public GameObject m_Root;

	// Token: 0x04005542 RID: 21826
	public float m_FirewarksRewardDelayMin = 0.45f;

	// Token: 0x04005543 RID: 21827
	public float m_FirewarksRewardDelayMax = 0.75f;

	// Token: 0x04005544 RID: 21828
	public float m_FirewarksRewardHold = 0.7f;

	// Token: 0x04005545 RID: 21829
	public float m_FireworksRewardRanRot = 30f;

	// Token: 0x04005546 RID: 21830
	public float m_CardRewardDelay = 0.5f;

	// Token: 0x04005547 RID: 21831
	public float m_CardRewardBurstDelay = 0.2f;

	// Token: 0x04005548 RID: 21832
	public float m_EndScaleAwayTime = 0.3f;

	// Token: 0x04005549 RID: 21833
	public float m_CardAnimationTime = 0.5f;

	// Token: 0x0400554A RID: 21834
	public PlayMakerFSM m_PackFireworkFSM;

	// Token: 0x0400554B RID: 21835
	public PlayMakerFSM m_GoldFireworkFSM;

	// Token: 0x0400554C RID: 21836
	public PlayMakerFSM m_DustFireworkFSM;

	// Token: 0x0400554D RID: 21837
	public PlayMakerFSM m_CardFireworkFSM;

	// Token: 0x0400554E RID: 21838
	public NormalButton m_DoneButton;

	// Token: 0x0400554F RID: 21839
	public PlayMakerFSM m_FSM;

	// Token: 0x04005550 RID: 21840
	public GameObject m_RewardFireworksRoot;

	// Token: 0x04005551 RID: 21841
	public HeroicBrawlRewardDisplay.FireworkRewardZone[] m_RewardZones;

	// Token: 0x04005552 RID: 21842
	public GameObject m_FinalRewardsRoot;

	// Token: 0x04005553 RID: 21843
	public UberText m_BannerUberText;

	// Token: 0x04005554 RID: 21844
	public HeroicBrawlRewardDisplay.RewardVisuals[] m_RewardVisuals = new HeroicBrawlRewardDisplay.RewardVisuals[12];

	// Token: 0x04005555 RID: 21845
	public GameObject m_CardsRoot;

	// Token: 0x04005556 RID: 21846
	public HeroicBrawlRewardDisplay.CardVisuals[] m_CardVisuals = new HeroicBrawlRewardDisplay.CardVisuals[3];

	// Token: 0x04005557 RID: 21847
	public int m_DebugWins;

	// Token: 0x04005558 RID: 21848
	public const string DEFAULT_PREFAB = "HeroicBrawlReward.prefab:8f49f1fcb5ca4485d9b6b22993e1b1ab";

	// Token: 0x04005559 RID: 21849
	public PegUIElement m_HeroicRewardChest;

	// Token: 0x0400555A RID: 21850
	public GameObject m_DescText;

	// Token: 0x0400555B RID: 21851
	private List<RewardData> m_Rewards = new List<RewardData>();

	// Token: 0x0400555C RID: 21852
	private HeroicBrawlRewardDisplay.RewardsReceivedData m_RewardsReceived;

	// Token: 0x0400555D RID: 21853
	private List<Reward> m_finalRewards = new List<Reward>();

	// Token: 0x0400555E RID: 21854
	private int m_finalRewardsLoadedCount;

	// Token: 0x0400555F RID: 21855
	private int m_lastZone = 1;

	// Token: 0x04005560 RID: 21856
	private List<Action> m_doneCallbacks;

	// Token: 0x04005561 RID: 21857
	private int m_wins;

	// Token: 0x04005562 RID: 21858
	private bool m_fromNotice;

	// Token: 0x04005563 RID: 21859
	private long m_noticeID = -1L;

	// Token: 0x04005564 RID: 21860
	private static HeroicBrawlRewardDisplay s_instance;

	// Token: 0x020022E9 RID: 8937
	[Serializable]
	public class RewardVisuals
	{
		// Token: 0x0400E516 RID: 58646
		public bool DropBox;

		// Token: 0x0400E517 RID: 58647
		public bool ShatterDialog;

		// Token: 0x0400E518 RID: 58648
		public int DustPerBottleMin = 50;

		// Token: 0x0400E519 RID: 58649
		public int DustPerBottleMax = 100;

		// Token: 0x0400E51A RID: 58650
		public int GoldPerBagMin = 50;

		// Token: 0x0400E51B RID: 58651
		public int GoldPerBagMax = 100;

		// Token: 0x0400E51C RID: 58652
		public Transform m_FinalPacksBone;

		// Token: 0x0400E51D RID: 58653
		public Transform m_FinalGoldBone;

		// Token: 0x0400E51E RID: 58654
		public Transform m_FinalDustBone;
	}

	// Token: 0x020022EA RID: 8938
	[Serializable]
	public struct FireworkRewardZone
	{
		// Token: 0x0400E51F RID: 58655
		public GameObject ZoneRoot;

		// Token: 0x0400E520 RID: 58656
		public BoxCollider Collider;

		// Token: 0x0400E521 RID: 58657
		public GameObject PackGameObject;

		// Token: 0x0400E522 RID: 58658
		public GameObject GoldGameObject;

		// Token: 0x0400E523 RID: 58659
		public GameObject DustGameObject;

		// Token: 0x0400E524 RID: 58660
		public BoosterPackReward packReward;

		// Token: 0x0400E525 RID: 58661
		public ArcaneDustReward dustReward;

		// Token: 0x0400E526 RID: 58662
		public GoldReward goldReward;
	}

	// Token: 0x020022EB RID: 8939
	[Serializable]
	public struct CardVisuals
	{
		// Token: 0x0400E527 RID: 58663
		public GameObject[] m_Cards;

		// Token: 0x0400E528 RID: 58664
		public GameObject[] m_CardTargets;
	}

	// Token: 0x020022EC RID: 8940
	public struct RewardCardReceived
	{
		// Token: 0x0400E529 RID: 58665
		public string CardID;

		// Token: 0x0400E52A RID: 58666
		public TAG_PREMIUM Premium;

		// Token: 0x0400E52B RID: 58667
		public EntityDef CardEntityDef;

		// Token: 0x0400E52C RID: 58668
		public GameObject CardGameObject;
	}

	// Token: 0x020022ED RID: 8941
	public struct RewardsReceivedData
	{
		// Token: 0x0400E52D RID: 58669
		public int PackID;

		// Token: 0x0400E52E RID: 58670
		public int PackCount;

		// Token: 0x0400E52F RID: 58671
		public int GoldCount;

		// Token: 0x0400E530 RID: 58672
		public int DustCount;

		// Token: 0x0400E531 RID: 58673
		public int CardsCount;

		// Token: 0x0400E532 RID: 58674
		public List<HeroicBrawlRewardDisplay.RewardCardReceived> Cards;
	}
}
