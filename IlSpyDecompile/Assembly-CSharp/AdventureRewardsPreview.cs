using System;
using System.Collections.Generic;
using UnityEngine;

[CustomEditClass]
public class AdventureRewardsPreview : MonoBehaviour
{
	public delegate void OnHide();

	[CustomEditField(Sections = "Cards Preview")]
	public GameObject m_CardsContainer;

	[SerializeField]
	private float m_CardWidth = 30f;

	[SerializeField]
	private float m_CardSpacing = 5f;

	[SerializeField]
	private float m_CardClumpAngleIncrement = 10f;

	[SerializeField]
	private Vector3 m_CardClumpSpacing = Vector3.zero;

	[CustomEditField(Sections = "Cards Preview")]
	public UberText m_HeaderTextObject;

	[CustomEditField(Sections = "Cards Preview")]
	public PegUIElement m_BackButton;

	[CustomEditField(Sections = "Cards Preview")]
	public GameObject m_ClickBlocker;

	[CustomEditField(Sections = "Cards Preview")]
	public UIBScrollable m_DisableScrollbar;

	[CustomEditField(Sections = "Cards Preview")]
	public float m_ShowHideAnimationTime = 0.15f;

	[CustomEditField(Sections = "Cards Preview")]
	public bool m_PreviewCardsExpandable;

	[CustomEditField(Sections = "Cards Preview/Hidden Cards")]
	public GameObject m_HiddenCardsLabelObject;

	[CustomEditField(Sections = "Cards Preview/Hidden Cards")]
	public UberText m_HiddenCardsLabel;

	[CustomEditField(Sections = "Cards Preview", Parent = "m_PreviewCardsExpandable")]
	public AdventureRewardsDisplayArea m_CardsPreviewDisplay;

	[CustomEditField(Sections = "Sounds", T = EditType.SOUND_PREFAB)]
	public string m_PreviewAppearSound;

	[CustomEditField(Sections = "Sounds", T = EditType.SOUND_PREFAB)]
	public string m_PreviewShrinkSound;

	private List<List<GameObject>> m_GameObjectBatches = new List<List<GameObject>>();

	private List<OnHide> m_OnHideListeners = new List<OnHide>();

	private int m_HiddenCardCount;

	[CustomEditField(Sections = "Cards Preview")]
	public float CardWidth
	{
		get
		{
			return m_CardWidth;
		}
		set
		{
			m_CardWidth = value;
			UpdateRewardPositions();
		}
	}

	[CustomEditField(Sections = "Cards Preview")]
	public float CardSpacing
	{
		get
		{
			return m_CardSpacing;
		}
		set
		{
			m_CardSpacing = value;
			UpdateRewardPositions();
		}
	}

	[CustomEditField(Sections = "Cards Preview")]
	public float CardClumpAngleIncrement
	{
		get
		{
			return m_CardClumpAngleIncrement;
		}
		set
		{
			m_CardClumpAngleIncrement = value;
			UpdateRewardPositions();
		}
	}

	[CustomEditField(Sections = "Cards Preview")]
	public Vector3 CardClumpSpacing
	{
		get
		{
			return m_CardClumpSpacing;
		}
		set
		{
			m_CardClumpSpacing = value;
			UpdateRewardPositions();
		}
	}

	private void Awake()
	{
		if (m_BackButton != null)
		{
			m_BackButton.AddEventListener(UIEventType.PRESS, delegate
			{
				Navigation.GoBack();
			});
		}
	}

	public void AddHideListener(OnHide dlg)
	{
		m_OnHideListeners.Add(dlg);
	}

	public void RemoveHideListener(OnHide dlg)
	{
		m_OnHideListeners.Remove(dlg);
	}

	private bool OnNavigateBack()
	{
		Show(show: false);
		return true;
	}

	public void SetHeaderText(string text)
	{
		m_HeaderTextObject.Text = GameStrings.Format("GLUE_ADVENTURE_REWARDS_PREVIEW_HEADER", text);
	}

	public void AddSpecificCards(List<string> cardIds)
	{
		foreach (string cardId in cardIds)
		{
			List<string> list = new List<string>();
			list.Add(cardId);
			AddCardBatch(list);
		}
	}

	public void AddSpecificCardBacks(List<int> cardBackIds)
	{
		foreach (int cardBackId in cardBackIds)
		{
			AddCardBackBatch(new List<int> { cardBackId });
		}
	}

	public void AddSpecificBoosters(List<BoosterDbId> boosterIds)
	{
		foreach (BoosterDbId boosterId in boosterIds)
		{
			AddBoosterBatch(new List<BoosterDbId> { boosterId });
		}
	}

	public void AddRewardBatch(int scenarioId)
	{
		List<RewardData> immediateRewardsForDefeatingScenario = AdventureProgressMgr.Get().GetImmediateRewardsForDefeatingScenario(scenarioId);
		AddRewardBatch(immediateRewardsForDefeatingScenario);
	}

	public void AddRewardBatch(List<RewardData> rewards)
	{
		List<string> list = new List<string>();
		List<int> list2 = new List<int>();
		List<BoosterDbId> list3 = new List<BoosterDbId>();
		foreach (RewardData reward in rewards)
		{
			switch (reward.RewardType)
			{
			case Reward.Type.CARD:
				list.Add(((CardRewardData)reward).CardID);
				break;
			case Reward.Type.BOOSTER_PACK:
				list3.Add((BoosterDbId)((BoosterPackRewardData)reward).Id);
				break;
			case Reward.Type.CARD_BACK:
				list2.Add(((CardBackRewardData)reward).CardBackID);
				break;
			case Reward.Type.RANDOM_CARD:
				Debug.LogWarning("Random Card Rewards are not currently handled by adventure batch rewards.");
				break;
			}
		}
		AddCardBatch(list);
		AddCardBackBatch(list2);
		AddBoosterBatch(list3);
	}

	public void AddCardBatch(List<string> cardIds)
	{
		if (cardIds != null && cardIds.Count != 0)
		{
			List<GameObject> list = new List<GameObject>();
			m_GameObjectBatches.Add(list);
			AddCardBatch(cardIds, list);
		}
	}

	public void AddCardBackBatch(List<int> cardBackIds)
	{
		if (cardBackIds != null && cardBackIds.Count != 0)
		{
			List<GameObject> list = new List<GameObject>();
			m_GameObjectBatches.Add(list);
			AddCardBackBatch(cardBackIds, list);
		}
	}

	public void AddBoosterBatch(List<BoosterDbId> boosterIds)
	{
		if (boosterIds != null && boosterIds.Count != 0)
		{
			List<GameObject> list = new List<GameObject>();
			m_GameObjectBatches.Add(list);
			AddBoosterBatch(boosterIds, list);
		}
	}

	public void SetHiddenCardCount(int hiddenCardCount)
	{
		m_HiddenCardCount = hiddenCardCount;
	}

	public void Reset()
	{
		foreach (List<GameObject> gameObjectBatch in m_GameObjectBatches)
		{
			foreach (GameObject item in gameObjectBatch)
			{
				if (item != null)
				{
					UnityEngine.Object.Destroy(item.gameObject);
				}
			}
		}
		m_HiddenCardCount = 0;
		m_GameObjectBatches.Clear();
	}

	public void Show(bool show)
	{
		if (m_ClickBlocker != null)
		{
			m_ClickBlocker.SetActive(show);
		}
		if (m_DisableScrollbar != null)
		{
			m_DisableScrollbar.Enable(!show);
		}
		if (show)
		{
			UpdateRewardPositions();
			FullScreenFXMgr.Get().StartStandardBlurVignette(m_ShowHideAnimationTime);
			base.gameObject.SetActive(value: true);
			iTween.ScaleFrom(base.gameObject, iTween.Hash("scale", Vector3.one * 0.05f, "time", m_ShowHideAnimationTime));
			if (!string.IsNullOrEmpty(m_PreviewAppearSound))
			{
				SoundManager.Get().LoadAndPlay(m_PreviewAppearSound);
			}
			Navigation.Push(OnNavigateBack);
			return;
		}
		Vector3 origScale = base.transform.localScale;
		iTween.ScaleTo(base.gameObject, iTween.Hash("scale", Vector3.one * 0.05f, "time", m_ShowHideAnimationTime, "oncomplete", (Action<object>)delegate
		{
			base.gameObject.SetActive(value: false);
			base.transform.localScale = origScale;
			FireHideEvent();
		}));
		if (!string.IsNullOrEmpty(m_PreviewShrinkSound))
		{
			SoundManager.Get().LoadAndPlay(m_PreviewShrinkSound);
		}
		FullScreenFXMgr.Get().EndStandardBlurVignette(m_ShowHideAnimationTime);
	}

	private void AddCardBatch(List<string> cardIds, List<GameObject> cardBatch)
	{
		if (cardIds == null || cardIds.Count == 0)
		{
			return;
		}
		Actor actor;
		for (int i = 0; i < cardIds.Count; i++)
		{
			string cardId = cardIds[i];
			using DefLoader.DisposableFullDef disposableFullDef = DefLoader.Get().GetFullDef(cardId);
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(disposableFullDef.EntityDef, TAG_PREMIUM.NORMAL), AssetLoadingOptions.IgnorePrefabPosition);
			actor = gameObject.GetComponent<Actor>();
			actor.SetFullDef(disposableFullDef);
			actor.CreateBannedRibbon();
			GameUtils.SetParent(actor, m_CardsContainer);
			SceneUtils.SetLayer(actor, m_CardsContainer.gameObject.layer);
			cardBatch.Add(gameObject);
			if (!m_PreviewCardsExpandable || !(m_CardsPreviewDisplay != null))
			{
				continue;
			}
			PegUIElement pegUIElement = actor.m_cardMesh.gameObject.AddComponent<PegUIElement>();
			pegUIElement.GetComponent<Collider>().enabled = true;
			pegUIElement.AddEventListener(UIEventType.RELEASE, delegate
			{
				if (!m_CardsPreviewDisplay.IsShowing())
				{
					List<RewardData> rewards = new List<RewardData>
					{
						new CardRewardData(cardId, TAG_PREMIUM.NORMAL, 1)
					};
					m_CardsPreviewDisplay.ShowRewards(rewards, actor.transform.position, actor.transform.position);
				}
			});
		}
	}

	private void AddCardBackBatch(List<int> cardBackIds, List<GameObject> cardBackBatch)
	{
		if (cardBackIds == null || cardBackIds.Count == 0)
		{
			return;
		}
		foreach (int cardBackId in cardBackIds)
		{
			GameObject gameObject = CardBackManager.Get().LoadCardBackByIndex(cardBackId).m_GameObject;
			GameUtils.SetParent(gameObject, m_CardsContainer);
			SceneUtils.SetLayer(gameObject, m_CardsContainer.gameObject.layer);
			cardBackBatch.Add(gameObject);
		}
	}

	private void AddBoosterBatch(List<BoosterDbId> boosterIds, List<GameObject> boosterBatch)
	{
		if (boosterIds == null || boosterIds.Count == 0)
		{
			return;
		}
		foreach (BoosterDbId boosterId in boosterIds)
		{
			BoosterDbfRecord record = GameDbf.Booster.GetRecord((int)boosterId);
			if (record != null)
			{
				GameObject gameObject = AssetLoader.Get().InstantiatePrefab(record.PackOpeningPrefab, AssetLoadingOptions.IgnorePrefabPosition);
				gameObject.GetComponent<UnopenedPack>().m_SingleStack.m_RootObject.SetActive(value: true);
				GameUtils.SetParent(gameObject, m_CardsContainer);
				SceneUtils.SetLayer(gameObject, m_CardsContainer.gameObject.layer);
				boosterBatch.Add(gameObject);
			}
		}
	}

	private void UpdateRewardPositions()
	{
		int num = m_GameObjectBatches.Count;
		bool flag = m_HiddenCardCount > 0;
		bool flag2 = m_HiddenCardsLabelObject != null;
		if (flag && flag2)
		{
			num++;
		}
		float num2 = ((float)(num - 1) * m_CardSpacing + (float)num * m_CardWidth) * 0.5f - m_CardWidth * 0.5f;
		int num3 = 0;
		foreach (List<GameObject> gameObjectBatch in m_GameObjectBatches)
		{
			if (gameObjectBatch.Count == 0)
			{
				continue;
			}
			int num4 = 0;
			foreach (GameObject item in gameObjectBatch)
			{
				if (!(item == null))
				{
					Vector3 localPosition = m_CardClumpSpacing * num4;
					localPosition.x += (float)num3 * (m_CardSpacing + m_CardWidth) - num2;
					item.transform.localScale = Vector3.one * 5f;
					item.transform.localRotation = Quaternion.identity;
					item.transform.Rotate(new Vector3(0f, 1f, 0f), (float)num4 * m_CardClumpAngleIncrement);
					item.transform.localPosition = localPosition;
					Actor component = item.GetComponent<Actor>();
					if (component != null)
					{
						component.SetUnlit();
						component.ContactShadow(visible: true);
						component.UpdateAllComponents();
						component.Show();
					}
					num4++;
				}
			}
			num3++;
		}
		if (flag && flag2)
		{
			Vector3 zero = Vector3.zero;
			zero.x += (float)num3 * (m_CardSpacing + m_CardWidth) - num2;
			m_HiddenCardsLabelObject.transform.localPosition = zero;
			m_HiddenCardsLabel.Text = $"+{m_HiddenCardCount}";
		}
		if (flag2)
		{
			m_HiddenCardsLabelObject.SetActive(flag);
		}
	}

	private void FireHideEvent()
	{
		OnHide[] array = m_OnHideListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
	}
}
