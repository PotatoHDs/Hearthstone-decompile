using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200004B RID: 75
[CustomEditClass]
public class AdventureRewardsPreview : MonoBehaviour
{
	// Token: 0x17000042 RID: 66
	// (get) Token: 0x06000424 RID: 1060 RVA: 0x000190FC File Offset: 0x000172FC
	// (set) Token: 0x06000425 RID: 1061 RVA: 0x00019104 File Offset: 0x00017304
	[CustomEditField(Sections = "Cards Preview")]
	public float CardWidth
	{
		get
		{
			return this.m_CardWidth;
		}
		set
		{
			this.m_CardWidth = value;
			this.UpdateRewardPositions();
		}
	}

	// Token: 0x17000043 RID: 67
	// (get) Token: 0x06000426 RID: 1062 RVA: 0x00019113 File Offset: 0x00017313
	// (set) Token: 0x06000427 RID: 1063 RVA: 0x0001911B File Offset: 0x0001731B
	[CustomEditField(Sections = "Cards Preview")]
	public float CardSpacing
	{
		get
		{
			return this.m_CardSpacing;
		}
		set
		{
			this.m_CardSpacing = value;
			this.UpdateRewardPositions();
		}
	}

	// Token: 0x17000044 RID: 68
	// (get) Token: 0x06000428 RID: 1064 RVA: 0x0001912A File Offset: 0x0001732A
	// (set) Token: 0x06000429 RID: 1065 RVA: 0x00019132 File Offset: 0x00017332
	[CustomEditField(Sections = "Cards Preview")]
	public float CardClumpAngleIncrement
	{
		get
		{
			return this.m_CardClumpAngleIncrement;
		}
		set
		{
			this.m_CardClumpAngleIncrement = value;
			this.UpdateRewardPositions();
		}
	}

	// Token: 0x17000045 RID: 69
	// (get) Token: 0x0600042A RID: 1066 RVA: 0x00019141 File Offset: 0x00017341
	// (set) Token: 0x0600042B RID: 1067 RVA: 0x00019149 File Offset: 0x00017349
	[CustomEditField(Sections = "Cards Preview")]
	public Vector3 CardClumpSpacing
	{
		get
		{
			return this.m_CardClumpSpacing;
		}
		set
		{
			this.m_CardClumpSpacing = value;
			this.UpdateRewardPositions();
		}
	}

	// Token: 0x0600042C RID: 1068 RVA: 0x00019158 File Offset: 0x00017358
	private void Awake()
	{
		if (this.m_BackButton != null)
		{
			this.m_BackButton.AddEventListener(UIEventType.PRESS, delegate(UIEvent e)
			{
				Navigation.GoBack();
			});
		}
	}

	// Token: 0x0600042D RID: 1069 RVA: 0x00019194 File Offset: 0x00017394
	public void AddHideListener(AdventureRewardsPreview.OnHide dlg)
	{
		this.m_OnHideListeners.Add(dlg);
	}

	// Token: 0x0600042E RID: 1070 RVA: 0x000191A2 File Offset: 0x000173A2
	public void RemoveHideListener(AdventureRewardsPreview.OnHide dlg)
	{
		this.m_OnHideListeners.Remove(dlg);
	}

	// Token: 0x0600042F RID: 1071 RVA: 0x000191B1 File Offset: 0x000173B1
	private bool OnNavigateBack()
	{
		this.Show(false);
		return true;
	}

	// Token: 0x06000430 RID: 1072 RVA: 0x000191BB File Offset: 0x000173BB
	public void SetHeaderText(string text)
	{
		this.m_HeaderTextObject.Text = GameStrings.Format("GLUE_ADVENTURE_REWARDS_PREVIEW_HEADER", new object[]
		{
			text
		});
	}

	// Token: 0x06000431 RID: 1073 RVA: 0x000191DC File Offset: 0x000173DC
	public void AddSpecificCards(List<string> cardIds)
	{
		foreach (string item in cardIds)
		{
			this.AddCardBatch(new List<string>
			{
				item
			});
		}
	}

	// Token: 0x06000432 RID: 1074 RVA: 0x00019238 File Offset: 0x00017438
	public void AddSpecificCardBacks(List<int> cardBackIds)
	{
		foreach (int item in cardBackIds)
		{
			this.AddCardBackBatch(new List<int>
			{
				item
			});
		}
	}

	// Token: 0x06000433 RID: 1075 RVA: 0x00019294 File Offset: 0x00017494
	public void AddSpecificBoosters(List<BoosterDbId> boosterIds)
	{
		foreach (BoosterDbId item in boosterIds)
		{
			this.AddBoosterBatch(new List<BoosterDbId>
			{
				item
			});
		}
	}

	// Token: 0x06000434 RID: 1076 RVA: 0x000192F0 File Offset: 0x000174F0
	public void AddRewardBatch(int scenarioId)
	{
		List<RewardData> immediateRewardsForDefeatingScenario = AdventureProgressMgr.Get().GetImmediateRewardsForDefeatingScenario(scenarioId);
		this.AddRewardBatch(immediateRewardsForDefeatingScenario);
	}

	// Token: 0x06000435 RID: 1077 RVA: 0x00019310 File Offset: 0x00017510
	public void AddRewardBatch(List<RewardData> rewards)
	{
		List<string> list = new List<string>();
		List<int> list2 = new List<int>();
		List<BoosterDbId> list3 = new List<BoosterDbId>();
		foreach (RewardData rewardData in rewards)
		{
			Reward.Type rewardType = rewardData.RewardType;
			switch (rewardType)
			{
			case Reward.Type.BOOSTER_PACK:
				list3.Add((BoosterDbId)((BoosterPackRewardData)rewardData).Id);
				break;
			case Reward.Type.CARD:
				list.Add(((CardRewardData)rewardData).CardID);
				break;
			case Reward.Type.CARD_BACK:
				list2.Add(((CardBackRewardData)rewardData).CardBackID);
				break;
			default:
				if (rewardType == Reward.Type.RANDOM_CARD)
				{
					Debug.LogWarning("Random Card Rewards are not currently handled by adventure batch rewards.");
				}
				break;
			}
		}
		this.AddCardBatch(list);
		this.AddCardBackBatch(list2);
		this.AddBoosterBatch(list3);
	}

	// Token: 0x06000436 RID: 1078 RVA: 0x000193EC File Offset: 0x000175EC
	public void AddCardBatch(List<string> cardIds)
	{
		if (cardIds == null || cardIds.Count == 0)
		{
			return;
		}
		List<GameObject> list = new List<GameObject>();
		this.m_GameObjectBatches.Add(list);
		this.AddCardBatch(cardIds, list);
	}

	// Token: 0x06000437 RID: 1079 RVA: 0x00019420 File Offset: 0x00017620
	public void AddCardBackBatch(List<int> cardBackIds)
	{
		if (cardBackIds == null || cardBackIds.Count == 0)
		{
			return;
		}
		List<GameObject> list = new List<GameObject>();
		this.m_GameObjectBatches.Add(list);
		this.AddCardBackBatch(cardBackIds, list);
	}

	// Token: 0x06000438 RID: 1080 RVA: 0x00019454 File Offset: 0x00017654
	public void AddBoosterBatch(List<BoosterDbId> boosterIds)
	{
		if (boosterIds == null || boosterIds.Count == 0)
		{
			return;
		}
		List<GameObject> list = new List<GameObject>();
		this.m_GameObjectBatches.Add(list);
		this.AddBoosterBatch(boosterIds, list);
	}

	// Token: 0x06000439 RID: 1081 RVA: 0x00019487 File Offset: 0x00017687
	public void SetHiddenCardCount(int hiddenCardCount)
	{
		this.m_HiddenCardCount = hiddenCardCount;
	}

	// Token: 0x0600043A RID: 1082 RVA: 0x00019490 File Offset: 0x00017690
	public void Reset()
	{
		foreach (List<GameObject> list in this.m_GameObjectBatches)
		{
			foreach (GameObject gameObject in list)
			{
				if (gameObject != null)
				{
					UnityEngine.Object.Destroy(gameObject.gameObject);
				}
			}
		}
		this.m_HiddenCardCount = 0;
		this.m_GameObjectBatches.Clear();
	}

	// Token: 0x0600043B RID: 1083 RVA: 0x00019538 File Offset: 0x00017738
	public void Show(bool show)
	{
		if (this.m_ClickBlocker != null)
		{
			this.m_ClickBlocker.SetActive(show);
		}
		if (this.m_DisableScrollbar != null)
		{
			this.m_DisableScrollbar.Enable(!show);
		}
		if (show)
		{
			this.UpdateRewardPositions();
			FullScreenFXMgr.Get().StartStandardBlurVignette(this.m_ShowHideAnimationTime);
			base.gameObject.SetActive(true);
			iTween.ScaleFrom(base.gameObject, iTween.Hash(new object[]
			{
				"scale",
				Vector3.one * 0.05f,
				"time",
				this.m_ShowHideAnimationTime
			}));
			if (!string.IsNullOrEmpty(this.m_PreviewAppearSound))
			{
				SoundManager.Get().LoadAndPlay(this.m_PreviewAppearSound);
			}
			Navigation.Push(new Navigation.NavigateBackHandler(this.OnNavigateBack));
			return;
		}
		Vector3 origScale = base.transform.localScale;
		iTween.ScaleTo(base.gameObject, iTween.Hash(new object[]
		{
			"scale",
			Vector3.one * 0.05f,
			"time",
			this.m_ShowHideAnimationTime,
			"oncomplete",
			new Action<object>(delegate(object o)
			{
				this.gameObject.SetActive(false);
				this.transform.localScale = origScale;
				this.FireHideEvent();
			})
		}));
		if (!string.IsNullOrEmpty(this.m_PreviewShrinkSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_PreviewShrinkSound);
		}
		FullScreenFXMgr.Get().EndStandardBlurVignette(this.m_ShowHideAnimationTime, null);
	}

	// Token: 0x0600043C RID: 1084 RVA: 0x000196D8 File Offset: 0x000178D8
	private void AddCardBatch(List<string> cardIds, List<GameObject> cardBatch)
	{
		if (cardIds == null || cardIds.Count == 0)
		{
			return;
		}
		for (int i = 0; i < cardIds.Count; i++)
		{
			string cardId = cardIds[i];
			using (DefLoader.DisposableFullDef fullDef = DefLoader.Get().GetFullDef(cardId, null))
			{
				GameObject gameObject = AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(fullDef.EntityDef, TAG_PREMIUM.NORMAL), AssetLoadingOptions.IgnorePrefabPosition);
				Actor actor = gameObject.GetComponent<Actor>();
				actor.SetFullDef(fullDef);
				actor.CreateBannedRibbon();
				GameUtils.SetParent(actor, this.m_CardsContainer, false);
				SceneUtils.SetLayer(actor, this.m_CardsContainer.gameObject.layer);
				cardBatch.Add(gameObject);
				if (this.m_PreviewCardsExpandable && this.m_CardsPreviewDisplay != null)
				{
					PegUIElement pegUIElement = actor.m_cardMesh.gameObject.AddComponent<PegUIElement>();
					pegUIElement.GetComponent<Collider>().enabled = true;
					pegUIElement.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
					{
						if (this.m_CardsPreviewDisplay.IsShowing())
						{
							return;
						}
						List<RewardData> list = new List<RewardData>();
						list.Add(new CardRewardData(cardId, TAG_PREMIUM.NORMAL, 1));
						this.m_CardsPreviewDisplay.ShowRewards(list, actor.transform.position, new Vector3?(actor.transform.position));
					});
				}
			}
		}
	}

	// Token: 0x0600043D RID: 1085 RVA: 0x00019820 File Offset: 0x00017A20
	private void AddCardBackBatch(List<int> cardBackIds, List<GameObject> cardBackBatch)
	{
		if (cardBackIds == null || cardBackIds.Count == 0)
		{
			return;
		}
		foreach (int cardBackIdx in cardBackIds)
		{
			GameObject gameObject = CardBackManager.Get().LoadCardBackByIndex(cardBackIdx, false, "Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9", false).m_GameObject;
			GameUtils.SetParent(gameObject, this.m_CardsContainer, false);
			SceneUtils.SetLayer(gameObject, this.m_CardsContainer.gameObject.layer, null);
			cardBackBatch.Add(gameObject);
		}
	}

	// Token: 0x0600043E RID: 1086 RVA: 0x000198C0 File Offset: 0x00017AC0
	private void AddBoosterBatch(List<BoosterDbId> boosterIds, List<GameObject> boosterBatch)
	{
		if (boosterIds == null || boosterIds.Count == 0)
		{
			return;
		}
		foreach (BoosterDbId id in boosterIds)
		{
			BoosterDbfRecord record = GameDbf.Booster.GetRecord((int)id);
			if (record != null)
			{
				GameObject gameObject = AssetLoader.Get().InstantiatePrefab(record.PackOpeningPrefab, AssetLoadingOptions.IgnorePrefabPosition);
				gameObject.GetComponent<UnopenedPack>().m_SingleStack.m_RootObject.SetActive(true);
				GameUtils.SetParent(gameObject, this.m_CardsContainer, false);
				SceneUtils.SetLayer(gameObject, this.m_CardsContainer.gameObject.layer, null);
				boosterBatch.Add(gameObject);
			}
		}
	}

	// Token: 0x0600043F RID: 1087 RVA: 0x00019988 File Offset: 0x00017B88
	private void UpdateRewardPositions()
	{
		int num = this.m_GameObjectBatches.Count;
		bool flag = this.m_HiddenCardCount > 0;
		bool flag2 = this.m_HiddenCardsLabelObject != null;
		if (flag && flag2)
		{
			num++;
		}
		float num2 = ((float)(num - 1) * this.m_CardSpacing + (float)num * this.m_CardWidth) * 0.5f - this.m_CardWidth * 0.5f;
		int num3 = 0;
		foreach (List<GameObject> list in this.m_GameObjectBatches)
		{
			if (list.Count != 0)
			{
				int num4 = 0;
				foreach (GameObject gameObject in list)
				{
					if (!(gameObject == null))
					{
						Vector3 localPosition = this.m_CardClumpSpacing * (float)num4;
						localPosition.x += (float)num3 * (this.m_CardSpacing + this.m_CardWidth) - num2;
						gameObject.transform.localScale = Vector3.one * 5f;
						gameObject.transform.localRotation = Quaternion.identity;
						gameObject.transform.Rotate(new Vector3(0f, 1f, 0f), (float)num4 * this.m_CardClumpAngleIncrement);
						gameObject.transform.localPosition = localPosition;
						Actor component = gameObject.GetComponent<Actor>();
						if (component != null)
						{
							component.SetUnlit();
							component.ContactShadow(true);
							component.UpdateAllComponents();
							component.Show();
						}
						num4++;
					}
				}
				num3++;
			}
		}
		if (flag && flag2)
		{
			Vector3 zero = Vector3.zero;
			zero.x += (float)num3 * (this.m_CardSpacing + this.m_CardWidth) - num2;
			this.m_HiddenCardsLabelObject.transform.localPosition = zero;
			this.m_HiddenCardsLabel.Text = string.Format("+{0}", this.m_HiddenCardCount);
		}
		if (flag2)
		{
			this.m_HiddenCardsLabelObject.SetActive(flag);
		}
	}

	// Token: 0x06000440 RID: 1088 RVA: 0x00019BE0 File Offset: 0x00017DE0
	private void FireHideEvent()
	{
		AdventureRewardsPreview.OnHide[] array = this.m_OnHideListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
	}

	// Token: 0x040002E3 RID: 739
	[CustomEditField(Sections = "Cards Preview")]
	public GameObject m_CardsContainer;

	// Token: 0x040002E4 RID: 740
	[SerializeField]
	private float m_CardWidth = 30f;

	// Token: 0x040002E5 RID: 741
	[SerializeField]
	private float m_CardSpacing = 5f;

	// Token: 0x040002E6 RID: 742
	[SerializeField]
	private float m_CardClumpAngleIncrement = 10f;

	// Token: 0x040002E7 RID: 743
	[SerializeField]
	private Vector3 m_CardClumpSpacing = Vector3.zero;

	// Token: 0x040002E8 RID: 744
	[CustomEditField(Sections = "Cards Preview")]
	public UberText m_HeaderTextObject;

	// Token: 0x040002E9 RID: 745
	[CustomEditField(Sections = "Cards Preview")]
	public PegUIElement m_BackButton;

	// Token: 0x040002EA RID: 746
	[CustomEditField(Sections = "Cards Preview")]
	public GameObject m_ClickBlocker;

	// Token: 0x040002EB RID: 747
	[CustomEditField(Sections = "Cards Preview")]
	public UIBScrollable m_DisableScrollbar;

	// Token: 0x040002EC RID: 748
	[CustomEditField(Sections = "Cards Preview")]
	public float m_ShowHideAnimationTime = 0.15f;

	// Token: 0x040002ED RID: 749
	[CustomEditField(Sections = "Cards Preview")]
	public bool m_PreviewCardsExpandable;

	// Token: 0x040002EE RID: 750
	[CustomEditField(Sections = "Cards Preview/Hidden Cards")]
	public GameObject m_HiddenCardsLabelObject;

	// Token: 0x040002EF RID: 751
	[CustomEditField(Sections = "Cards Preview/Hidden Cards")]
	public UberText m_HiddenCardsLabel;

	// Token: 0x040002F0 RID: 752
	[CustomEditField(Sections = "Cards Preview", Parent = "m_PreviewCardsExpandable")]
	public AdventureRewardsDisplayArea m_CardsPreviewDisplay;

	// Token: 0x040002F1 RID: 753
	[CustomEditField(Sections = "Sounds", T = EditType.SOUND_PREFAB)]
	public string m_PreviewAppearSound;

	// Token: 0x040002F2 RID: 754
	[CustomEditField(Sections = "Sounds", T = EditType.SOUND_PREFAB)]
	public string m_PreviewShrinkSound;

	// Token: 0x040002F3 RID: 755
	private List<List<GameObject>> m_GameObjectBatches = new List<List<GameObject>>();

	// Token: 0x040002F4 RID: 756
	private List<AdventureRewardsPreview.OnHide> m_OnHideListeners = new List<AdventureRewardsPreview.OnHide>();

	// Token: 0x040002F5 RID: 757
	private int m_HiddenCardCount;

	// Token: 0x02001314 RID: 4884
	// (Invoke) Token: 0x0600D671 RID: 54897
	public delegate void OnHide();
}
