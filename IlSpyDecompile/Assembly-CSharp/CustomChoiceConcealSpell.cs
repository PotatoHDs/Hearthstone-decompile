using System.Collections;
using System.Collections.Generic;
using PegasusGame;
using UnityEngine;

public class CustomChoiceConcealSpell : CustomChoiceSpell
{
	public Spell m_WrongChoiceSpell;

	public Spell m_CorrectChoiceSpell;

	public Spell m_SuperCorrectChoiceSpell;

	public Spell m_HiddenWrongChoiceSpell;

	public Spell m_HiddenCorrectChoiceSpell;

	public Spell m_HiddenSuperCorrectChoiceSpell;

	public Spell m_CorrectChoiceFadeAwaySpell;

	public float m_SendCardBackToOpponentsDeckDelay = 0.25f;

	private bool m_choseCorrectly;

	private Card m_correctCard;

	private Actor m_fakeCorrectCardActor;

	private Actor m_fakeCorrectCardBackActor;

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (m_fakeCorrectCardActor != null)
		{
			m_fakeCorrectCardActor.Destroy();
		}
		if (m_fakeCorrectCardBackActor != null)
		{
			m_fakeCorrectCardBackActor.Destroy();
		}
	}

	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		StartCoroutine(DoEffects());
	}

	private IEnumerator DoEffects()
	{
		while (!FindResultOfChoice())
		{
			yield return null;
		}
		if (!m_choseCorrectly && !LoadFakeActors())
		{
			OnSpellFinished();
			OnStateFinished();
			yield break;
		}
		yield return StartCoroutine(PlayChoiceEffects());
		ResetCorrectCardCardback();
		foreach (Card card in m_choiceState.m_cards)
		{
			if (!(card == m_correctCard) || !m_choseCorrectly)
			{
				card.HideCard();
			}
		}
		if (!m_choseCorrectly)
		{
			m_fakeCorrectCardActor.Show();
			yield return new WaitForSeconds(m_SendCardBackToOpponentsDeckDelay);
			if (!m_fakeCorrectCardActor.GetEntity().IsHidden())
			{
				yield return StartCoroutine(SpellUtils.FlipActorAndReplaceWithOtherActor(m_fakeCorrectCardActor, m_fakeCorrectCardBackActor, 0.5f));
			}
			else
			{
				m_fakeCorrectCardBackActor.Show();
				m_fakeCorrectCardActor.Hide();
			}
			PlayFadeAwaySpellThenFinish();
		}
		else
		{
			FinishSpell();
		}
	}

	private void FinishSpell()
	{
		OnSpellFinished();
		OnStateFinished();
	}

	private bool LoadFakeActors()
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(m_correctCard.GetActorAssetPath(), AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject == null)
		{
			Log.Spells.PrintError("CustomChoiceConcealSpell.LoadFakeActors(): Failed to load fake actor for card " + m_correctCard);
			return false;
		}
		Player.Side oppositePlayerSide = Player.GetOppositePlayerSide(m_correctCard.GetControllerSide());
		m_fakeCorrectCardActor = gameObject.GetComponent<Actor>();
		m_fakeCorrectCardActor.SetCardDefFromCard(m_correctCard);
		m_fakeCorrectCardActor.SetEntity(m_correctCard.GetEntity());
		m_fakeCorrectCardActor.SetEntityDef(m_correctCard.GetEntity().GetEntityDef());
		m_fakeCorrectCardActor.SetCardBackSideOverride(oppositePlayerSide);
		m_fakeCorrectCardActor.UpdateAllComponents();
		TransformUtil.CopyWorld(m_fakeCorrectCardActor, m_correctCard.GetActor());
		m_fakeCorrectCardActor.Hide();
		GameObject gameObject2 = AssetLoader.Get().InstantiatePrefab("Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9", AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject2 == null)
		{
			Log.Spells.PrintError("CustomChoiceConcealSpell.LoadFakeActors(): Failed to load fake card back actor.");
			return false;
		}
		m_fakeCorrectCardBackActor = gameObject2.GetComponent<Actor>();
		m_fakeCorrectCardBackActor.SetCardBackSideOverride(oppositePlayerSide);
		m_fakeCorrectCardBackActor.UpdateAllComponents();
		TransformUtil.CopyWorld(m_fakeCorrectCardBackActor, m_correctCard.GetActor());
		m_fakeCorrectCardBackActor.Hide();
		return true;
	}

	private IEnumerator DestroyChoiceEffect(Spell spell)
	{
		yield return new WaitForSeconds(2f);
		Object.Destroy(spell);
	}

	private void OnEffectStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() == SpellStateType.NONE)
		{
			StartCoroutine(DestroyChoiceEffect(spell));
		}
	}

	private void OnChoiceEffectSpellEvent(string eventName, object eventData, object userData)
	{
		if (eventName == "ResetCorrectCardCardBack")
		{
			ResetCorrectCardCardback();
		}
	}

	private IEnumerator PlayChoiceEffects()
	{
		int effectsToWaitFor = 0;
		FinishedCallback callback = delegate
		{
			effectsToWaitFor--;
		};
		foreach (Card card in m_choiceState.m_cards)
		{
			bool num = card.GetEntity().IsHidden();
			Spell original = (num ? m_HiddenCorrectChoiceSpell : m_CorrectChoiceSpell);
			Spell original2 = (num ? m_HiddenSuperCorrectChoiceSpell : m_SuperCorrectChoiceSpell);
			Spell original3 = (num ? m_HiddenWrongChoiceSpell : m_WrongChoiceSpell);
			Actor actor = card.GetActor();
			if (card == m_correctCard)
			{
				effectsToWaitFor++;
				Spell obj = (m_choseCorrectly ? Object.Instantiate(original2) : Object.Instantiate(original));
				obj.transform.parent = actor.transform;
				TransformUtil.Identity(obj);
				obj.AddFinishedCallback(callback);
				obj.AddStateFinishedCallback(OnEffectStateFinished);
				obj.AddSpellEventCallback(OnChoiceEffectSpellEvent);
				obj.Activate();
			}
			else
			{
				effectsToWaitFor++;
				Spell spell2 = Object.Instantiate(original3);
				spell2.transform.parent = actor.transform;
				TransformUtil.Identity(spell2);
				spell2.AddFinishedCallback(callback);
				spell2.AddStateFinishedCallback(OnEffectStateFinished);
				spell2.Activate();
			}
		}
		while (effectsToWaitFor > 0)
		{
			yield return null;
		}
	}

	private void PlayFadeAwaySpellThenFinish()
	{
		FinishedCallback callback = delegate
		{
			m_fakeCorrectCardBackActor.Hide();
			FinishSpell();
		};
		Spell spell2 = Object.Instantiate(m_CorrectChoiceFadeAwaySpell);
		spell2.transform.parent = m_correctCard.GetActor().transform;
		TransformUtil.Identity(spell2);
		spell2.AddFinishedCallback(callback);
		spell2.AddStateFinishedCallback(OnEffectStateFinished);
		spell2.Activate();
	}

	private void ResetCorrectCardCardback()
	{
		m_correctCard.GetActor().SetCardBackSideOverride(null);
		m_correctCard.GetActor().UpdateCardBack();
		if (m_correctCard.GetControllerSide() == Player.Side.FRIENDLY)
		{
			m_correctCard.SetTransitionStyle(ZoneTransitionStyle.SLOW);
		}
	}

	private bool FindResultOfChoice()
	{
		List<PowerTaskList> list = new List<PowerTaskList>();
		list.Add(GameState.Get().GetPowerProcessor().GetCurrentTaskList());
		foreach (PowerTaskList item in GameState.Get().GetPowerProcessor().GetPowerQueue()
			.GetList())
		{
			list.Add(item);
		}
		foreach (PowerTaskList item2 in list)
		{
			if (item2 == null || item2.GetBlockType() != HistoryBlock.Type.POWER || item2.GetSourceEntity() != GameState.Get().GetEntity(m_choiceState.m_sourceEntityId))
			{
				continue;
			}
			if (item2.IsBlockUnended())
			{
				return false;
			}
			foreach (PowerTask task in item2.GetTaskList())
			{
				Network.HistMetaData histMetaData = task.GetPower() as Network.HistMetaData;
				if (histMetaData == null || histMetaData.MetaType != 0)
				{
					continue;
				}
				Entity entity = GameState.Get().GetEntity(histMetaData.Info[0]);
				if (entity == null)
				{
					continue;
				}
				foreach (Card card in m_choiceState.m_cards)
				{
					if (card.GetEntity() == entity)
					{
						m_correctCard = card;
						m_choseCorrectly = m_choiceState.m_chosenEntities.Contains(card.GetEntity());
						return true;
					}
				}
			}
		}
		return false;
	}
}
