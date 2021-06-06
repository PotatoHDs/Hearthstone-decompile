using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadManCopyHandToDeckSpell : SuperSpell
{
	private struct ActorCallbackData
	{
		public int targetIndex;

		public int handIndex;

		public int handSize;
	}

	public float m_MoveUpTime;

	public float m_MoveUpOffsetZ;

	public float m_MoveUpScale;

	public float m_MoveToDeckInterval;

	public bool m_ShuffleRealHandToDeck;

	private int m_taskCountToRunFirst;

	private bool m_waitForTasksToComplete;

	private List<Entity> m_entitiesToDrawBeforeFX = new List<Entity>();

	private List<Actor> m_actors = new List<Actor>();

	private List<Actor> m_friendlyActors = new List<Actor>();

	private List<Actor> m_opposingActors = new List<Actor>();

	private int m_numActorsInLoading;

	public override bool AddPowerTargets()
	{
		m_visualToTargetIndexMap.Clear();
		m_targetToMetaDataMap.Clear();
		m_targets.Clear();
		List<PowerTask> taskList = m_taskList.GetTaskList();
		for (int i = 0; i < taskList.Count; i++)
		{
			PowerTask task = taskList[i];
			Card targetCardFromPowerTask = GetTargetCardFromPowerTask(i, task);
			if (!(targetCardFromPowerTask == null) && IsValidSpellTarget(targetCardFromPowerTask.GetEntity()) && !m_targets.Contains(targetCardFromPowerTask.gameObject))
			{
				AddTarget(targetCardFromPowerTask.gameObject);
				targetCardFromPowerTask.SuppressHandToDeckTransition();
				if (m_targets.Count == 1)
				{
					m_taskCountToRunFirst = i;
				}
			}
		}
		return m_targets.Count != 0;
	}

	protected override Card GetTargetCardFromPowerTask(int index, PowerTask task)
	{
		Network.PowerHistory power = task.GetPower();
		if (power.Type == Network.PowerType.TAG_CHANGE)
		{
			Network.HistTagChange histTagChange = (Network.HistTagChange)power;
			if (histTagChange.Tag != 49)
			{
				return null;
			}
			if (histTagChange.Value != 2)
			{
				return null;
			}
			Entity entity = GameState.Get().GetEntity(histTagChange.Entity);
			if (entity == null)
			{
				return null;
			}
			if (entity.GetZone() != TAG_ZONE.HAND && entity.GetZone() != TAG_ZONE.SETASIDE)
			{
				return null;
			}
			return entity.GetCard();
		}
		if (power.Type == Network.PowerType.HIDE_ENTITY)
		{
			Network.HistHideEntity histHideEntity = (Network.HistHideEntity)power;
			if (histHideEntity.Zone != 2)
			{
				return null;
			}
			Entity entity2 = GameState.Get().GetEntity(histHideEntity.Entity);
			if (entity2 == null)
			{
				return null;
			}
			if (entity2.GetZone() != TAG_ZONE.HAND)
			{
				return null;
			}
			return entity2.GetCard();
		}
		return null;
	}

	protected override void OnAction(SpellStateType prevStateType)
	{
		m_effectsPendingFinish++;
		base.OnAction(prevStateType);
		FindEntitiesToDrawBeforeFX();
		DoTasks();
		StartCoroutine(DoActionWithTiming());
	}

	protected override void DoActionNow()
	{
	}

	private IEnumerator DoActionWithTiming()
	{
		if (m_ShuffleRealHandToDeck)
		{
			yield return StartCoroutine(WaitForPendingCardDraw());
		}
		yield return StartCoroutine(WaitForTasksAndDrawing());
		yield return StartCoroutine(LoadAssets());
		yield return StartCoroutine(DoEffects());
	}

	private void FindEntitiesToDrawBeforeFX()
	{
		Card sourceCard = GetSourceCard();
		m_entitiesToDrawBeforeFX.Clear();
		List<PowerTask> taskList = m_taskList.GetTaskList();
		for (int i = 0; i < m_taskCountToRunFirst; i++)
		{
			PowerTask powerTask = taskList[i];
			if (sourceCard.GetControllerSide() == Player.Side.FRIENDLY)
			{
				FindRevealedEntitiesToDrawBeforeFX(powerTask.GetPower());
				continue;
			}
			FindRevealedEntitiesToDrawBeforeFX(powerTask.GetPower());
			FindHiddenEntitiesToDrawBeforeFX(powerTask.GetPower());
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
		if (histTagChange.Tag == 49 && (histTagChange.Value == 3 || histTagChange.Value == 4))
		{
			Entity entity = GameState.Get().GetEntity(histTagChange.Entity);
			if (entity == null)
			{
				Debug.LogWarningFormat("{0}.FindOpponentEntitiesToDrawBeforeFX() - WARNING trying to target entity with id {1} but there is no entity with that id", this, histTagChange.Entity);
			}
			else if (entity.GetZone() == TAG_ZONE.DECK)
			{
				m_entitiesToDrawBeforeFX.Add(entity);
			}
		}
	}

	private void DoTasks()
	{
		if (m_taskCountToRunFirst <= 0)
		{
			m_waitForTasksToComplete = false;
			return;
		}
		m_waitForTasksToComplete = true;
		m_taskList.DoTasks(0, m_taskCountToRunFirst, delegate
		{
			m_waitForTasksToComplete = false;
		});
	}

	private IEnumerator LoadAssets()
	{
		m_numActorsInLoading = m_targets.Count;
		m_actors.Clear();
		m_friendlyActors.Clear();
		m_opposingActors.Clear();
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < m_targets.Count; i++)
		{
			if (m_targets[i].GetComponent<Card>().GetEntity().IsControlledByFriendlySidePlayer())
			{
				num++;
			}
			else
			{
				num2++;
			}
		}
		int num3 = 0;
		int num4 = 0;
		for (int j = 0; j < m_targets.Count; j++)
		{
			Entity entity = m_targets[j].GetComponent<Card>().GetEntity();
			bool flag = entity.IsControlledByFriendlySidePlayer();
			m_actors.Add(null);
			if (flag)
			{
				m_friendlyActors.Add(null);
			}
			else
			{
				m_opposingActors.Add(null);
			}
			string zoneActor = ActorNames.GetZoneActor(entity, TAG_ZONE.HAND);
			ActorCallbackData actorCallbackData = default(ActorCallbackData);
			actorCallbackData.targetIndex = j;
			actorCallbackData.handIndex = (flag ? num3++ : num4++);
			actorCallbackData.handSize = (flag ? num : num2);
			ActorCallbackData actorCallbackData2 = actorCallbackData;
			AssetLoader.Get().InstantiatePrefab(zoneActor, OnActorLoaded, actorCallbackData2, AssetLoadingOptions.IgnorePrefabPosition);
		}
		while (m_numActorsInLoading > 0)
		{
			yield return null;
		}
	}

	private void OnActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		m_numActorsInLoading--;
		ActorCallbackData obj = (ActorCallbackData)callbackData;
		int targetIndex = obj.targetIndex;
		int handIndex = obj.handIndex;
		int handSize = obj.handSize;
		if (go == null)
		{
			Error.AddDevFatal("DeadManCopyHandToDeckSpell.OnActorLoaded() - FAILED to load actor {0} (targetIndex {1})", assetRef, targetIndex);
			return;
		}
		Actor component = go.GetComponent<Actor>();
		Card component2 = m_targets[targetIndex].GetComponent<Card>();
		Entity entity = component2.GetEntity();
		ZoneHand handZone = component2.GetController().GetHandZone();
		component2.SetDoNotSort(on: true);
		component.SetCard(component2);
		component.SetCardDefFromCard(component2);
		component.SetEntity(entity);
		component.SetEntityDef(entity.GetEntityDef());
		component.SetCardBackSideOverride(entity.GetControllerSide());
		component.UpdateAllComponents();
		component2.transform.position = handZone.GetCardPosition(handIndex, handSize);
		component2.transform.localEulerAngles = handZone.GetCardRotation(handIndex, handSize);
		component2.transform.localScale = handZone.GetCardScale();
		component.Hide();
		m_actors[targetIndex] = component;
		if (entity.IsControlledByFriendlySidePlayer())
		{
			m_friendlyActors[handIndex] = component;
		}
		else
		{
			m_opposingActors[handIndex] = component;
		}
	}

	private IEnumerator WaitForPendingCardDraw()
	{
		Card sourceCard = GetSourceCard();
		if (sourceCard == null)
		{
			yield break;
		}
		Entity entity = sourceCard.GetEntity();
		if (entity == null)
		{
			yield break;
		}
		if (entity.IsControlledByFriendlySidePlayer())
		{
			while ((bool)GameState.Get().GetFriendlyCardBeingDrawn())
			{
				yield return null;
			}
			while (GameState.Get().GetFriendlySidePlayer().GetHandZone()
				.IsUpdatingLayout())
			{
				yield return null;
			}
		}
		else
		{
			while ((bool)GameState.Get().GetOpponentCardBeingDrawn())
			{
				yield return null;
			}
			while (GameState.Get().GetOpposingSidePlayer().GetHandZone()
				.IsUpdatingLayout())
			{
				yield return null;
			}
		}
	}

	private bool IsDrawing()
	{
		foreach (Entity item in m_entitiesToDrawBeforeFX)
		{
			Card card = item.GetCard();
			switch (item.GetZone())
			{
			case TAG_ZONE.GRAVEYARD:
				if (!card.IsActorReady())
				{
					return true;
				}
				break;
			case TAG_ZONE.HAND:
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
				break;
			}
		}
		return false;
	}

	private IEnumerator WaitForTasksAndDrawing()
	{
		while (m_waitForTasksToComplete)
		{
			yield return null;
		}
		while (IsDrawing())
		{
			yield return null;
		}
	}

	private void CheckHideOriginalHandActors()
	{
		if (!m_ShuffleRealHandToDeck)
		{
			return;
		}
		if (m_opposingActors.Count > 0)
		{
			foreach (Card card in GameState.Get().GetOpposingSidePlayer().GetHandZone()
				.GetCards())
			{
				card.GetActor().Hide();
			}
		}
		if (m_friendlyActors.Count <= 0)
		{
			return;
		}
		foreach (Card card2 in GameState.Get().GetFriendlySidePlayer().GetHandZone()
			.GetCards())
		{
			card2.GetActor().Hide();
		}
	}

	private IEnumerator DoEffects()
	{
		base.DoActionNow();
		CheckHideOriginalHandActors();
		AnimateSpread();
		Actor livingActor;
		do
		{
			livingActor = m_actors.Find((Actor currActor) => currActor);
			if ((bool)livingActor)
			{
				yield return null;
			}
		}
		while ((bool)livingActor);
		m_effectsPendingFinish--;
		FinishIfPossible();
	}

	private void AnimateSpread()
	{
		for (int i = 0; i < m_opposingActors.Count || i < m_friendlyActors.Count; i++)
		{
			if (i < m_opposingActors.Count)
			{
				float waitSec = (float)(m_opposingActors.Count - i - 1) * m_MoveToDeckInterval;
				StartCoroutine(AnimateActor(m_opposingActors[i], waitSec));
			}
			if (i < m_friendlyActors.Count)
			{
				float waitSec2 = (float)(m_friendlyActors.Count - i - 1) * m_MoveToDeckInterval;
				StartCoroutine(AnimateActor(m_friendlyActors[i], waitSec2));
			}
		}
	}

	private IEnumerator AnimateActor(Actor actor, float waitSec)
	{
		Card card = actor.GetCard();
		Player controller = card.GetController();
		ZoneDeck deck = controller.GetDeckZone();
		actor.Show();
		float num = (controller.IsFriendlySide() ? m_MoveUpOffsetZ : (0f - m_MoveUpOffsetZ));
		iTween.MoveTo(position: new Vector3(card.transform.position.x, card.transform.position.y, card.transform.position.z + num), target: card.gameObject, time: m_MoveUpTime);
		iTween.ScaleTo(card.gameObject, card.transform.localScale * m_MoveUpScale, m_MoveUpTime);
		yield return new WaitForSeconds(m_MoveUpTime + waitSec);
		bool hideBackSide = actor.GetEntityDef().GetCardType() == TAG_CARDTYPE.INVALID;
		yield return StartCoroutine(actor.GetCard().AnimatePlayToDeck(actor.gameObject, deck, hideBackSide));
		actor.Destroy();
		card.SetDoNotSort(on: false);
	}
}
