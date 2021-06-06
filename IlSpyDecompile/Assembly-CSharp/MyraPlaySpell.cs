using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyraPlaySpell : Spell
{
	[SerializeField]
	private Spell m_Spell;

	[SerializeField]
	private float m_DrawSpeedScale = 1f;

	private Spell m_spell;

	private List<Entity> m_entitiesToDrawBeforeFX = new List<Entity>();

	protected override void OnAction(SpellStateType prevStateType)
	{
		SetSuppressDeckEmotes(suppress: true);
		StartCoroutine(DoEffectWithTiming());
		base.OnAction(prevStateType);
	}

	private void SetSuppressDeckEmotes(bool suppress)
	{
		ZoneDeck deckZone = GetSourceCard().GetController().GetDeckZone();
		deckZone.SetSuppressEmotes(suppress);
		deckZone.UpdateLayout();
	}

	private IEnumerator DoEffectWithTiming()
	{
		ActivateBirth();
		FindEntitiesToDrawBeforeFX();
		yield return StartCoroutine(CompleteTasks());
		yield return StartCoroutine(WaitForDrawing());
		yield return StartCoroutine(ActivateAction());
	}

	private void ActivateBirth()
	{
		if (m_spell == null)
		{
			m_spell = Object.Instantiate(m_Spell);
			m_spell.SetSource(GetSource());
		}
		SpellUtils.ActivateBirthIfNecessary(m_spell);
	}

	private void FindEntitiesToDrawBeforeFX()
	{
		Card sourceCard = GetSourceCard();
		foreach (PowerTask task in m_taskList.GetTaskList())
		{
			if (sourceCard.GetControllerSide() == Player.Side.FRIENDLY)
			{
				FindRevealedEntitiesToDrawBeforeFX(task.GetPower());
				continue;
			}
			FindRevealedEntitiesToDrawBeforeFX(task.GetPower());
			FindHiddenEntitiesToDrawBeforeFX(task.GetPower());
		}
	}

	private void FindRevealedEntitiesToDrawBeforeFX(Network.PowerHistory power)
	{
		if (power.Type != Network.PowerType.SHOW_ENTITY)
		{
			return;
		}
		Network.HistShowEntity histShowEntity = (Network.HistShowEntity)power;
		Entity entity = GameState.Get().GetEntity(histShowEntity.Entity.ID);
		if (entity != null && entity.GetZone() == TAG_ZONE.DECK)
		{
			if (histShowEntity.Entity.Tags.Exists((Network.Entity.Tag tag) => tag.Name == 49 && tag.Value == 3))
			{
				m_entitiesToDrawBeforeFX.Add(entity);
			}
			else if (histShowEntity.Entity.Tags.Exists((Network.Entity.Tag tag) => tag.Name == 49 && tag.Value == 4))
			{
				entity.GetCard().SetSkipMilling(skipMilling: true);
				m_entitiesToDrawBeforeFX.Add(entity);
			}
		}
	}

	private void FindHiddenEntitiesToDrawBeforeFX(Network.PowerHistory power)
	{
		if (power.Type != Network.PowerType.TAG_CHANGE)
		{
			return;
		}
		Network.HistTagChange histTagChange = (Network.HistTagChange)power;
		if (histTagChange.Tag != 49 || (histTagChange.Value != 3 && histTagChange.Value != 4))
		{
			return;
		}
		Entity entity = GameState.Get().GetEntity(histTagChange.Entity);
		if (entity == null)
		{
			Debug.LogWarningFormat("{0}.FindOpponentEntitiesToDrawBeforeFX() - WARNING trying to target entity with id {1} but there is no entity with that id", this, histTagChange.Entity);
		}
		else if (entity.GetZone() == TAG_ZONE.DECK)
		{
			if (histTagChange.Value == 4)
			{
				entity.GetCard().SetSkipMilling(skipMilling: true);
			}
			m_entitiesToDrawBeforeFX.Add(entity);
		}
	}

	private void SetDrawTimeScale(float scale)
	{
		foreach (Entity item in m_entitiesToDrawBeforeFX)
		{
			item.GetCard().SetDrawTimeScale(scale);
		}
	}

	private IEnumerator CompleteTasks()
	{
		SetDrawTimeScale(1f / m_DrawSpeedScale);
		bool complete = false;
		m_taskList.DoAllTasks(delegate
		{
			complete = true;
		});
		while (!complete)
		{
			yield return null;
		}
	}

	private IEnumerator WaitForDrawing()
	{
		if (m_taskList.GetBlockEnd() != null)
		{
			while (IsDrawing())
			{
				yield return null;
			}
			SetDrawTimeScale(1f);
		}
	}

	private bool IsDrawing()
	{
		foreach (Entity item in m_entitiesToDrawBeforeFX)
		{
			Card card = item.GetCard();
			if (item.GetZone() != TAG_ZONE.HAND || card.GetZone() is ZonePlay || card.GetZone() == null)
			{
				continue;
			}
			if (!(card.GetZone() is ZoneHand))
			{
				return true;
			}
			if (card.IsDoNotSort())
			{
				return true;
			}
			if (item.IsControlledByFriendlySidePlayer())
			{
				if (!card.CardStandInIsInteractive())
				{
					return true;
				}
			}
			else if (card.IsBeingDrawnByOpponent())
			{
				return true;
			}
		}
		return false;
	}

	private IEnumerator ActivateAction()
	{
		if (m_taskList.GetBlockEnd() == null)
		{
			OnSpellFinished();
			yield break;
		}
		m_spell.ActivateState(SpellStateType.ACTION);
		while (!m_spell.IsFinished())
		{
			yield return null;
		}
		OnSpellFinished();
		SetSuppressDeckEmotes(suppress: false);
		while (m_spell.GetActiveState() != 0)
		{
			yield return null;
		}
		Object.Destroy(m_spell);
		m_spell = null;
		Deactivate();
	}
}
