using System.Collections;
using PegasusGame;
using UnityEngine;

public class CastSpellCardFromHandSepll : Spell
{
	[SerializeField]
	private float m_BigCardDisplayTime = 1f;

	public override bool AddPowerTargets()
	{
		foreach (PowerTask task in m_taskList.GetTaskList())
		{
			Network.PowerHistory power = task.GetPower();
			if (power.Type != Network.PowerType.META_DATA)
			{
				continue;
			}
			Network.HistMetaData histMetaData = (Network.HistMetaData)power;
			if (histMetaData.MetaType == HistoryMeta.Type.TARGET && histMetaData.Info.Count != 0)
			{
				int id = histMetaData.Info[0];
				Entity entity = GameState.Get().GetEntity(id);
				if (entity != null && entity.GetZone() == TAG_ZONE.HAND)
				{
					Card card = entity.GetCard();
					AddTarget(card.gameObject);
					return true;
				}
			}
		}
		return false;
	}

	protected override void OnAction(SpellStateType prevStateType)
	{
		StartCoroutine(DoEffectWithTiming());
		base.OnAction(prevStateType);
	}

	private IEnumerator DoEffectWithTiming()
	{
		Card card = GetTargetCard();
		Player controller = card.GetController();
		card.SetDoNotSort(on: true);
		bool complete;
		if (controller.IsFriendlySide())
		{
			yield return StartCoroutine(MoveCardToBigCardSpot());
			yield return StartCoroutine(PlayPowerUpSpell());
			complete = true;
		}
		else
		{
			yield return StartCoroutine(ShowBigCard());
			complete = true;
		}
		while (!complete)
		{
			yield return null;
		}
		card.SetDoNotSort(on: false);
		OnSpellFinished();
		OnStateFinished();
	}

	private IEnumerator ShowBigCard()
	{
		Card targetCard = GetTargetCard();
		targetCard.HideCard();
		Entity entity = targetCard.GetEntity();
		UpdateTags(entity);
		HistoryManager.Get().CreatePlayedBigCard(entity, delegate
		{
		}, delegate
		{
		}, fromMetaData: true, countered: false, (int)(m_BigCardDisplayTime * 1000f));
		yield return new WaitForSeconds(m_BigCardDisplayTime);
	}

	private void UpdateTags(Entity entity)
	{
		foreach (PowerTask task in m_taskList.GetTaskList())
		{
			Network.PowerHistory power = task.GetPower();
			if (power.Type != Network.PowerType.SHOW_ENTITY)
			{
				continue;
			}
			Network.Entity entity2 = (power as Network.HistShowEntity).Entity;
			if (entity2.ID != entity.GetEntityId())
			{
				continue;
			}
			entity.LoadCard(entity2.CardID);
			foreach (Network.Entity.Tag tag in entity2.Tags)
			{
				entity.SetTag(tag.Name, tag.Value);
			}
			return;
		}
		foreach (PowerTask task2 in m_taskList.GetTaskList())
		{
			Network.PowerHistory power2 = task2.GetPower();
			if (power2.Type == Network.PowerType.TAG_CHANGE)
			{
				Network.HistTagChange histTagChange = (Network.HistTagChange)power2;
				if (histTagChange.Entity == entity.GetEntityId() && (histTagChange.Tag == 219 || histTagChange.Tag == 199 || histTagChange.Tag == 48))
				{
					entity.SetTag(histTagChange.Tag, histTagChange.Value);
				}
			}
		}
	}

	private IEnumerator MoveCardToBigCardSpot()
	{
		while (HistoryManager.Get().IsShowingBigCard())
		{
			yield return null;
		}
		Card targetCard = GetTargetCard();
		string bigCardBoneName = HistoryManager.Get().GetBigCardBoneName();
		Transform transform = Board.Get().FindBone(bigCardBoneName);
		iTween.MoveTo(targetCard.gameObject, transform.position, m_BigCardDisplayTime);
		iTween.RotateTo(targetCard.gameObject, transform.rotation.eulerAngles, m_BigCardDisplayTime);
		iTween.ScaleTo(targetCard.gameObject, new Vector3(1f, 1f, 1f), m_BigCardDisplayTime);
		SoundManager.Get().LoadAndPlay("play_card_from_hand_1.prefab:ac4be75e319a97947a68308a08e54e88");
		yield return new WaitForSeconds(m_BigCardDisplayTime);
	}

	private IEnumerator PlayPowerUpSpell()
	{
		Card targetCard = GetTargetCard();
		Spell powerUpSpell = targetCard.GetActor().GetSpell(SpellType.POWER_UP);
		if (powerUpSpell == null)
		{
			yield break;
		}
		bool complete = false;
		powerUpSpell.AddStateFinishedCallback(delegate(Spell spell, SpellStateType prevStateType, object userData)
		{
			if (prevStateType == SpellStateType.BIRTH)
			{
				complete = true;
			}
		});
		powerUpSpell.ActivateState(SpellStateType.BIRTH);
		while (!complete)
		{
			yield return null;
		}
		powerUpSpell.Deactivate();
	}
}
