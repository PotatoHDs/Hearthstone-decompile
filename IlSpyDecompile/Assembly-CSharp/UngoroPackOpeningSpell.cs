using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UngoroPackOpeningSpell : SuperSpell
{
	private List<Entity> m_newCards;

	private List<int> m_fullEntityTaskIndices;

	private List<Transform> cardDestinations = new List<Transform>();

	private Transform cardSpawningPosition;

	public float m_CardFlyOutTime = 2f;

	public float m_CardHangTime = 3f;

	private int previousLayer;

	public override bool AddPowerTargets()
	{
		if (!CanAddPowerTargets())
		{
			return false;
		}
		m_newCards = new List<Entity>();
		m_fullEntityTaskIndices = new List<int>();
		FindNewCardsFullEntityTask();
		return true;
	}

	private void FindNewCardsFullEntityTask()
	{
		List<PowerTask> taskList = m_taskList.GetTaskList();
		for (int i = 0; i < taskList.Count; i++)
		{
			Network.HistFullEntity histFullEntity = taskList[i].GetPower() as Network.HistFullEntity;
			if (histFullEntity == null)
			{
				continue;
			}
			foreach (Network.Entity.Tag tag in histFullEntity.Entity.Tags)
			{
				if (tag.Name == 49 && tag.Value == 3)
				{
					m_fullEntityTaskIndices.Add(i);
					m_newCards.Add(GameState.Get().GetEntity(histFullEntity.Entity.ID));
					break;
				}
			}
		}
	}

	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		UngoroPackOpeningPositioner component = m_activeAreaEffectSpell.GetComponent<UngoroPackOpeningPositioner>();
		if (component == null)
		{
			Log.Spells.PrintError("UngoroPackOpeningSpell.OnAction(): No UngoroPackOpeningPositioner found on spell {0}.", m_activeAreaEffectSpell.gameObject.name);
			OnSpellFinished();
			OnStateFinished();
		}
		else if (m_newCards.Count <= 0)
		{
			OnSpellFinished();
			OnStateFinished();
		}
		else
		{
			m_effectsPendingFinish++;
			m_activeAreaEffectSpell.AddSpellEventCallback(OnSpellEvent);
			cardDestinations = component.GetPositioningBonesForCardCount(m_newCards.Count);
			cardSpawningPosition = component.m_PackSpawningBone;
			StartCoroutine(SpawnAndHideReceivedCards());
		}
	}

	private IEnumerator SpawnAndHideReceivedCards()
	{
		int num = 0;
		Player.Side controllerSide = m_newCards[0].GetControllerSide();
		ZoneMgr.Get().FindZoneOfType<ZoneHand>(controllerSide).AddLayoutBlocker();
		for (int i = 0; i < m_newCards.Count; i++)
		{
			bool complete = false;
			PowerTaskList.CompleteCallback callback = delegate
			{
				complete = true;
			};
			int count2 = 1 + (m_fullEntityTaskIndices[i] - num);
			Card newCard = m_newCards[i].GetCard();
			newCard.SetDoNotSort(on: true);
			newCard.SetDoNotWarpToNewZone(on: true);
			newCard.SetInputEnabled(enabled: false);
			m_taskList.DoTasks(num, count2, callback);
			while (newCard.GetActor() == null || newCard.IsActorLoading())
			{
				yield return null;
			}
			newCard.HideCard();
			while (!complete)
			{
				yield return null;
			}
			num = m_fullEntityTaskIndices[i] + 1;
		}
		if (m_newCards.Count > 0)
		{
			previousLayer = m_newCards[0].GetCard().gameObject.layer;
		}
	}

	public void OnSpellEvent(string eventName, object eventData, object userData)
	{
		PlayInnkeeperVO();
		StartCoroutine(SplayOutReceivedCards());
	}

	private void PlayInnkeeperVO()
	{
		TAG_RARITY tAG_RARITY = TAG_RARITY.INVALID;
		TAG_PREMIUM tAG_PREMIUM = TAG_PREMIUM.NORMAL;
		foreach (Entity newCard in m_newCards)
		{
			if (!newCard.IsHidden())
			{
				TAG_RARITY rarity = newCard.GetRarity();
				TAG_PREMIUM premiumType = newCard.GetPremiumType();
				if (rarity > tAG_RARITY)
				{
					tAG_RARITY = rarity;
					tAG_PREMIUM = premiumType;
				}
				else if (rarity == tAG_RARITY && premiumType == TAG_PREMIUM.GOLDEN)
				{
					tAG_PREMIUM = premiumType;
				}
			}
		}
		switch (tAG_RARITY)
		{
		case TAG_RARITY.COMMON:
			if (tAG_PREMIUM == TAG_PREMIUM.GOLDEN)
			{
				SoundManager.Get().LoadAndPlay("VO_ANNOUNCER_FOIL_C_29.prefab:69820e4999e4afa439761151e057a526");
			}
			break;
		case TAG_RARITY.RARE:
			if (tAG_PREMIUM == TAG_PREMIUM.GOLDEN)
			{
				SoundManager.Get().LoadAndPlay("VO_ANNOUNCER_FOIL_R_30.prefab:f5bf5bfd8e5f4d247aa8a6da966969cf");
			}
			else
			{
				SoundManager.Get().LoadAndPlay("VO_ANNOUNCER_RARE_27.prefab:8ff0de7a4fd144b4b983caea4c54da4d");
			}
			break;
		case TAG_RARITY.EPIC:
			if (tAG_PREMIUM == TAG_PREMIUM.GOLDEN)
			{
				SoundManager.Get().LoadAndPlay("VO_ANNOUNCER_FOIL_E_31.prefab:d419d6eca0e2a72469544bae5f11542f");
			}
			else
			{
				SoundManager.Get().LoadAndPlay("VO_ANNOUNCER_EPIC_26.prefab:e76d67f55b976104794c3cf73382e82a");
			}
			break;
		case TAG_RARITY.LEGENDARY:
			if (tAG_PREMIUM == TAG_PREMIUM.GOLDEN)
			{
				SoundManager.Get().LoadAndPlay("VO_ANNOUNCER_FOIL_L_32.prefab:caefd66acfc4e2b4f858035c274b257e");
			}
			else
			{
				SoundManager.Get().LoadAndPlay("VO_ANNOUNCER_LEGENDARY_25.prefab:e015c982aec12bc4893f36396d426750");
			}
			break;
		case TAG_RARITY.FREE:
			break;
		}
	}

	private IEnumerator SplayOutReceivedCards()
	{
		for (int i = 0; i < m_newCards.Count; i++)
		{
			Card newCard = m_newCards[i].GetCard();
			while (newCard.GetActor() == null || newCard.IsActorLoading())
			{
				yield return null;
			}
		}
		for (int j = 0; j < m_newCards.Count; j++)
		{
			Card card = m_newCards[j].GetCard();
			TransformUtil.CopyWorld(card, cardSpawningPosition);
			card.ShowCard();
			SceneUtils.SetLayer(card.gameObject, GameLayer.Tooltip);
			Transform transform = cardDestinations[j];
			card.transform.localScale = new Vector3(card.transform.localScale.x * transform.localScale.x, card.transform.localScale.y * transform.localScale.y, card.transform.localScale.z * transform.localScale.z);
			Vector3 position = cardDestinations[j].position;
			iTween.MoveTo(card.gameObject, position, m_CardFlyOutTime);
		}
		yield return new WaitForSeconds(m_CardHangTime);
		for (int k = 0; k < m_newCards.Count; k++)
		{
			Card card2 = m_newCards[k].GetCard();
			ZoneTransitionStyle transitionStyle = ZoneTransitionStyle.VERY_SLOW;
			card2.SetTransitionStyle(transitionStyle);
			card2.SetDoNotSort(on: false);
			card2.SetDoNotWarpToNewZone(on: false);
			card2.SetInputEnabled(enabled: true);
			SceneUtils.SetLayer(card2.gameObject, previousLayer);
		}
		Zone zone = m_newCards[0].GetCard().GetZone();
		zone.RemoveLayoutBlocker();
		zone.UpdateLayout();
		m_effectsPendingFinish--;
		OnSpellFinished();
		OnStateFinished();
	}
}
