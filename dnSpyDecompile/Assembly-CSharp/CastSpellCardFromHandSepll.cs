using System;
using System.Collections;
using System.Collections.Generic;
using PegasusGame;
using UnityEngine;

// Token: 0x020007D7 RID: 2007
public class CastSpellCardFromHandSepll : Spell
{
	// Token: 0x06006E27 RID: 28199 RVA: 0x00238204 File Offset: 0x00236404
	public override bool AddPowerTargets()
	{
		foreach (PowerTask powerTask in this.m_taskList.GetTaskList())
		{
			Network.PowerHistory power = powerTask.GetPower();
			if (power.Type == Network.PowerType.META_DATA)
			{
				Network.HistMetaData histMetaData = (Network.HistMetaData)power;
				if (histMetaData.MetaType == HistoryMeta.Type.TARGET && histMetaData.Info.Count != 0)
				{
					int id = histMetaData.Info[0];
					global::Entity entity = GameState.Get().GetEntity(id);
					if (entity != null && entity.GetZone() == TAG_ZONE.HAND)
					{
						Card card = entity.GetCard();
						this.AddTarget(card.gameObject);
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06006E28 RID: 28200 RVA: 0x002382C8 File Offset: 0x002364C8
	protected override void OnAction(SpellStateType prevStateType)
	{
		base.StartCoroutine(this.DoEffectWithTiming());
		base.OnAction(prevStateType);
	}

	// Token: 0x06006E29 RID: 28201 RVA: 0x002382DE File Offset: 0x002364DE
	private IEnumerator DoEffectWithTiming()
	{
		Card card = base.GetTargetCard();
		global::Player controller = card.GetController();
		card.SetDoNotSort(true);
		bool complete = false;
		if (controller.IsFriendlySide())
		{
			yield return base.StartCoroutine(this.MoveCardToBigCardSpot());
			yield return base.StartCoroutine(this.PlayPowerUpSpell());
			complete = true;
		}
		else
		{
			yield return base.StartCoroutine(this.ShowBigCard());
			complete = true;
		}
		while (!complete)
		{
			yield return null;
		}
		card.SetDoNotSort(false);
		this.OnSpellFinished();
		this.OnStateFinished();
		yield break;
	}

	// Token: 0x06006E2A RID: 28202 RVA: 0x002382ED File Offset: 0x002364ED
	private IEnumerator ShowBigCard()
	{
		Card targetCard = base.GetTargetCard();
		targetCard.HideCard();
		global::Entity entity = targetCard.GetEntity();
		this.UpdateTags(entity);
		HistoryManager.Get().CreatePlayedBigCard(entity, delegate
		{
		}, delegate
		{
		}, true, false, (int)(this.m_BigCardDisplayTime * 1000f));
		yield return new WaitForSeconds(this.m_BigCardDisplayTime);
		yield break;
	}

	// Token: 0x06006E2B RID: 28203 RVA: 0x002382FC File Offset: 0x002364FC
	private void UpdateTags(global::Entity entity)
	{
		foreach (PowerTask powerTask in this.m_taskList.GetTaskList())
		{
			Network.PowerHistory power = powerTask.GetPower();
			if (power.Type == Network.PowerType.SHOW_ENTITY)
			{
				Network.Entity entity2 = (power as Network.HistShowEntity).Entity;
				if (entity2.ID == entity.GetEntityId())
				{
					entity.LoadCard(entity2.CardID, null);
					using (List<Network.Entity.Tag>.Enumerator enumerator2 = entity2.Tags.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							Network.Entity.Tag tag = enumerator2.Current;
							entity.SetTag(tag.Name, tag.Value);
						}
						return;
					}
				}
			}
		}
		foreach (PowerTask powerTask2 in this.m_taskList.GetTaskList())
		{
			Network.PowerHistory power2 = powerTask2.GetPower();
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

	// Token: 0x06006E2C RID: 28204 RVA: 0x00238480 File Offset: 0x00236680
	private IEnumerator MoveCardToBigCardSpot()
	{
		while (HistoryManager.Get().IsShowingBigCard())
		{
			yield return null;
		}
		Card targetCard = base.GetTargetCard();
		string bigCardBoneName = HistoryManager.Get().GetBigCardBoneName();
		Transform transform = Board.Get().FindBone(bigCardBoneName);
		iTween.MoveTo(targetCard.gameObject, transform.position, this.m_BigCardDisplayTime);
		iTween.RotateTo(targetCard.gameObject, transform.rotation.eulerAngles, this.m_BigCardDisplayTime);
		iTween.ScaleTo(targetCard.gameObject, new Vector3(1f, 1f, 1f), this.m_BigCardDisplayTime);
		SoundManager.Get().LoadAndPlay("play_card_from_hand_1.prefab:ac4be75e319a97947a68308a08e54e88");
		yield return new WaitForSeconds(this.m_BigCardDisplayTime);
		yield break;
	}

	// Token: 0x06006E2D RID: 28205 RVA: 0x0023848F File Offset: 0x0023668F
	private IEnumerator PlayPowerUpSpell()
	{
		Card targetCard = base.GetTargetCard();
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
		yield break;
	}

	// Token: 0x0400585D RID: 22621
	[SerializeField]
	private float m_BigCardDisplayTime = 1f;
}
