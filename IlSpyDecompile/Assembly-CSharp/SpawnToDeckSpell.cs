using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnToDeckSpell : SuperSpell
{
	public enum HandActorSource
	{
		CHOSEN_TARGET,
		OVERRIDE,
		SPELL_TARGET
	}

	public enum SpreadType
	{
		STACK,
		SEQUENCE,
		CUSTOM_SPELL
	}

	[Serializable]
	public class StackData
	{
		public float m_RevealTime = 1f;

		public float m_StaggerTime = 1.2f;
	}

	[Serializable]
	public class SequenceData
	{
		public float m_Spacing = 2.1f;

		public float m_RevealTime = 0.6f;

		public float m_NextCardRevealTimeMin = 0.1f;

		public float m_NextCardRevealTimeMax = 0.2f;

		public float m_HoldTime = 0.3f;

		public float m_NextCardHoldTime = 0.4f;
	}

	private struct RevealSpellFinishedCallbackData
	{
		public Actor actor;

		public Card card;

		public int id;
	}

	private const float PHONE_HAND_OFFSET = 1.5f;

	private const int SEQUENCE_BATCH_SIZE = 5;

	private const float SEQUENCE_BATCH_REVEAL_TIME = 0.3f;

	private const float SEQUENCE_BATCH_HOLD_TIME = 0f;

	private const float SEQUENCE_BATCH_NEXT_CARD_HOLD_TIME = 0.2f;

	public HandActorSource m_HandActorSource;

	public string m_OverrideCardId;

	public List<string> m_OverrideCardIds = new List<string>();

	public float m_CardDelay;

	public float m_CardAnimatePlayToDeckTimeScale = 1f;

	public float m_RevealStartScale = 0.1f;

	public float m_RevealYOffsetMin = 5f;

	public float m_RevealYOffsetMax = 5f;

	public float m_RevealFriendlySideZOffset;

	public float m_RevealOpponentSideZOffset;

	public Vector3 m_RevealBaseOffset = Vector3.zero;

	public SpreadType m_SpreadType;

	public StackData m_StackData = new StackData();

	public SequenceData m_SequenceData = new SequenceData();

	public Spell m_customRevealSpell;

	private List<DefLoader.DisposableCardDef> m_overrideCardDefs = new List<DefLoader.DisposableCardDef>();

	protected override void OnDestroy()
	{
		m_overrideCardDefs.DisposeValuesAndClear();
		base.OnDestroy();
	}

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
			if (!(targetCardFromPowerTask == null) && IsValidSpellTarget(targetCardFromPowerTask.GetEntity()))
			{
				AddTarget(targetCardFromPowerTask.gameObject);
			}
		}
		if (m_targets.Count > 0)
		{
			return true;
		}
		return false;
	}

	protected override Card GetTargetCardFromPowerTask(int index, PowerTask task)
	{
		Network.PowerHistory power = task.GetPower();
		if (power.Type != Network.PowerType.FULL_ENTITY)
		{
			return null;
		}
		Network.Entity entity = (power as Network.HistFullEntity).Entity;
		Entity entity2 = GameState.Get().GetEntity(entity.ID);
		if (entity2 == null)
		{
			Debug.LogWarning($"{this}.GetTargetCardFromPowerTask() - WARNING trying to target entity with id {entity.ID} but there is no entity with that id");
			return null;
		}
		if (entity2.GetZone() != TAG_ZONE.DECK)
		{
			return null;
		}
		return entity2.GetCard();
	}

	protected override void OnAction(SpellStateType prevStateType)
	{
		m_effectsPendingFinish++;
		base.OnAction(prevStateType);
		StartCoroutine(DoActionWithTiming());
	}

	private IEnumerator ProcessShowEntityForTargets()
	{
		PowerTaskList powerTaskList = GetPowerTaskList();
		foreach (PowerTask task in powerTaskList.GetTaskList())
		{
			Network.HistShowEntity histShowEntity = task.GetPower() as Network.HistShowEntity;
			if (histShowEntity == null)
			{
				continue;
			}
			Network.Entity entity = histShowEntity.Entity;
			Entity target = FindTargetEntity(entity.ID);
			if (target == null)
			{
				continue;
			}
			foreach (Network.Entity.Tag tag in entity.Tags)
			{
				target.SetTag(tag.Name, tag.Value);
			}
			target.LoadCard(entity.CardID);
			while (target.IsLoadingAssets())
			{
				yield return null;
			}
		}
	}

	private Entity FindTargetEntity(int entityID)
	{
		foreach (GameObject target in m_targets)
		{
			Card component = target.GetComponent<Card>();
			if (!(component == null))
			{
				Entity entity = component.GetEntity();
				if (entity != null && entity.GetEntityId() == entityID)
				{
					return entity;
				}
			}
		}
		Entity powerTarget = GetPowerTarget();
		if (powerTarget != null && powerTarget.GetEntityId() == entityID)
		{
			return powerTarget;
		}
		return null;
	}

	private IEnumerator DoActionWithTiming()
	{
		List<Actor> actors = new List<Actor>(m_targets.Count);
		yield return StartCoroutine(ProcessShowEntityForTargets());
		yield return StartCoroutine(LoadAssets(actors));
		yield return new WaitForSeconds(m_CardDelay);
		yield return StartCoroutine(DoEffects(actors));
	}

	private IEnumerator LoadAssets(List<Actor> actors)
	{
		bool loadingOverrideCardDef = false;
		if (m_OverrideCardIds.Count == 0)
		{
			m_OverrideCardIds.Add(m_OverrideCardId);
		}
		int i = 0;
		while (i < m_OverrideCardIds.Count)
		{
			if (!string.IsNullOrEmpty(m_OverrideCardIds[i]))
			{
				loadingOverrideCardDef = true;
				DefLoader.LoadDefCallback<DefLoader.DisposableCardDef> callback = delegate(string cardId, DefLoader.DisposableCardDef def, object userData)
				{
					loadingOverrideCardDef = false;
					if (def == null)
					{
						Error.AddDevFatal("SpawnToDeckSpell.LoadAssets() - FAILED to load CardDef for {0}", cardId);
					}
					else
					{
						m_overrideCardDefs.Add(def);
					}
				};
				DefLoader.Get().LoadCardDef(m_OverrideCardIds[i], callback);
				while (loadingOverrideCardDef)
				{
					yield return null;
				}
			}
			int assetsLoading = 1;
			if (i == m_OverrideCardIds.Count - 1 && m_targets.Count > m_OverrideCardIds.Count)
			{
				assetsLoading = m_targets.Count - m_OverrideCardIds.Count + 1;
			}
			PrefabCallback<GameObject> callback2 = delegate(AssetReference assetRef, GameObject go, object callbackData)
			{
				assetsLoading--;
				int num3 = (int)callbackData;
				if (num3 <= m_targets.Count - 1)
				{
					if (go == null)
					{
						Error.AddDevFatal("SpawnToDeckSpell.LoadAssets() - FAILED to load actor {0} (targetIndex {1})", base.name, num3);
					}
					else
					{
						Actor component = go.GetComponent<Actor>();
						Card component2 = m_targets[num3].GetComponent<Card>();
						Entity entity2 = component2.GetEntity();
						if (entity2.GetLoadState() == Entity.LoadState.DONE)
						{
							component.SetEntity(entity2);
						}
						else
						{
							component.SetPremium(GetPremium(entity2));
							if (m_HandActorSource == HandActorSource.CHOSEN_TARGET)
							{
								Entity powerTarget = GetPowerTarget();
								if (powerTarget != null)
								{
									string cardTextInHand = powerTarget.GetCardTextInHand();
									component.SetCardDefPowerTextOverride(cardTextInHand);
								}
							}
						}
						component.SetEntityDef(GetEntityDef(entity2, num3));
						using (DefLoader.DisposableCardDef cardDef = ShareDisposableCardDef(component2, num3))
						{
							component.SetCardDef(cardDef);
						}
						component.SetCardBackSideOverride(entity2.GetControllerSide());
						component.UpdateAllComponents();
						component.Hide();
						actors[num3] = component;
						OnActorLoaded(component);
					}
				}
			};
			int num = assetsLoading;
			for (int j = 0; j < num; j++)
			{
				Entity entity = m_targets[Math.Min(i + j, m_targets.Count - 1)].GetComponent<Card>().GetEntity();
				EntityDef entityDef = GetEntityDef(entity, i + j);
				TAG_PREMIUM premium = GetPremium(entity);
				if (actors.Count < m_targets.Count)
				{
					actors.Add(null);
				}
				AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(entityDef, premium), callback2, i + j, AssetLoadingOptions.IgnorePrefabPosition);
			}
			while (assetsLoading > 0)
			{
				yield return null;
			}
			int num2 = i + 1;
			i = num2;
		}
	}

	protected virtual void OnActorLoaded(Actor actor)
	{
	}

	private IEnumerator DoEffects(List<Actor> actors)
	{
		StartCoroutine(AnimateSpread(actors));
		Actor livingActor;
		do
		{
			livingActor = actors.Find((Actor currActor) => currActor);
			if ((bool)livingActor)
			{
				yield return null;
			}
		}
		while ((bool)livingActor);
		m_effectsPendingFinish--;
		FinishIfPossible();
	}

	private float GetRevealSec(int iterationCount)
	{
		if (iterationCount > 0)
		{
			return 0.3f;
		}
		return m_SequenceData.m_RevealTime;
	}

	private float GetHoldSec(int iterationCount)
	{
		if (iterationCount > 0)
		{
			return 0f;
		}
		return m_SequenceData.m_HoldTime;
	}

	private float GetNextCardHoldSec(int iterationCount)
	{
		if (iterationCount > 0)
		{
			return 0.2f;
		}
		return m_SequenceData.m_NextCardHoldTime;
	}

	private IEnumerator WaitForBatchToAnimate(int batchSize, int iterationCount)
	{
		float seconds = (m_SequenceData.m_NextCardRevealTimeMax + GetNextCardHoldSec(iterationCount)) * (float)(batchSize - 1) + GetRevealSec(iterationCount) + GetHoldSec(iterationCount);
		yield return new WaitForSeconds(seconds);
	}

	private void AnimateSequence(List<Actor> actors, int iterationCount)
	{
		List<Vector3> list = new List<Vector3>();
		float num = -0.5f * (float)(actors.Count - 1) * m_SequenceData.m_Spacing;
		for (int i = 0; i < actors.Count; i++)
		{
			float num2 = (float)i * m_SequenceData.m_Spacing;
			Vector3 offset = new Vector3(num + num2, 0f, 0f);
			Vector3 item = ComputeRevealPosition(offset);
			list.Add(item);
		}
		BoundRevealPositions(actors, list);
		PreventHandOverlapPhone(actors, list);
		float revealSec = GetRevealSec(iterationCount);
		float holdSec = GetHoldSec(iterationCount);
		float nextCardHoldSec = GetNextCardHoldSec(iterationCount);
		List<float> list2 = RandomizeRevealTimes(actors.Count, revealSec, m_SequenceData.m_NextCardRevealTimeMin, m_SequenceData.m_NextCardRevealTimeMax);
		float num3 = Mathf.Max(list2.ToArray());
		for (int j = 0; j < actors.Count; j++)
		{
			Vector3 revealPos = list[j];
			float revealSec2 = list2[j];
			float num4 = (float)(actors.Count - 1 - j) * nextCardHoldSec;
			float num5 = holdSec + num4;
			float waitSec = num3 + num5;
			StartCoroutine(AnimateActor(actors, j, revealSec2, revealPos, waitSec));
		}
	}

	private IEnumerator AnimateSpread(List<Actor> actors)
	{
		if (m_SpreadType == SpreadType.SEQUENCE)
		{
			List<Actor> batchedActors = new List<Actor>();
			int iterationCount2 = 0;
			foreach (Actor actor in actors)
			{
				if (batchedActors.Count == 5)
				{
					AnimateSequence(batchedActors, iterationCount2);
					yield return WaitForBatchToAnimate(batchedActors.Count, iterationCount2);
					iterationCount2++;
					batchedActors.Clear();
				}
				batchedActors.Add(actor);
			}
			if (batchedActors.Count > 0)
			{
				AnimateSequence(batchedActors, iterationCount2);
				yield return WaitForBatchToAnimate(batchedActors.Count, iterationCount2);
			}
		}
		else if (m_SpreadType == SpreadType.STACK)
		{
			int iterationCount2 = 0;
			while (iterationCount2 < actors.Count)
			{
				Vector3 revealPos = ComputeRevealPosition(Vector3.zero);
				StartCoroutine(AnimateActor(actors, iterationCount2, m_StackData.m_RevealTime, revealPos, m_StackData.m_RevealTime));
				if (iterationCount2 < actors.Count - 1)
				{
					yield return new WaitForSeconds(m_StackData.m_StaggerTime);
				}
				int num = iterationCount2 + 1;
				iterationCount2 = num;
			}
		}
		else if (m_SpreadType == SpreadType.CUSTOM_SPELL)
		{
			for (int i = 0; i < actors.Count; i++)
			{
				AnimateActorUsingSpell(actors, i);
			}
		}
	}

	private void AnimateActorUsingSpell(List<Actor> actors, int index)
	{
		Actor actor = actors[index];
		GameObject gameObject = m_targets[index];
		Card component = gameObject.GetComponent<Card>();
		actor.transform.localScale = new Vector3(m_RevealStartScale, m_RevealStartScale, m_RevealStartScale);
		actor.transform.rotation = base.transform.rotation;
		actor.transform.position = base.transform.position;
		actor.Show();
		RevealSpellFinishedCallbackData revealSpellFinishedCallbackData = default(RevealSpellFinishedCallbackData);
		revealSpellFinishedCallbackData.actor = actor;
		revealSpellFinishedCallbackData.card = component;
		revealSpellFinishedCallbackData.id = index;
		if (m_customRevealSpell == null)
		{
			Log.Spells.PrintError("SpawnToDeckSpell.AnimateSpread(): m_SpreadType is set to CUSTOM_SPELL, but m_customRevealSpell is null!");
			OnRevealSpellFinished(null, revealSpellFinishedCallbackData);
			return;
		}
		Spell spell = UnityEngine.Object.Instantiate(m_customRevealSpell);
		SpellUtils.SetCustomSpellParent(spell, actor);
		spell.AddFinishedCallback(OnRevealSpellFinished, revealSpellFinishedCallbackData);
		spell.SetSource(GetSource());
		spell.AddTarget(gameObject);
		spell.Activate();
	}

	public void OnRevealSpellFinished(Spell spell, object userData)
	{
		RevealSpellFinishedCallbackData revealSpellFinishedCallbackData = (RevealSpellFinishedCallbackData)userData;
		Actor actor = revealSpellFinishedCallbackData.actor;
		Card card = revealSpellFinishedCallbackData.card;
		Entity entity = card.GetEntity();
		ZoneDeck deckZone = entity.GetController().GetDeckZone();
		bool hideBackSide = GetEntityDef(entity, revealSpellFinishedCallbackData.id).GetCardType() == TAG_CARDTYPE.INVALID;
		StartCoroutine(AnimateRevealedCardToDeck(actor, card, deckZone, hideBackSide));
	}

	public IEnumerator AnimateRevealedCardToDeck(Actor actor, Card card, ZoneDeck deck, bool hideBackSide)
	{
		yield return StartCoroutine(card.AnimatePlayToDeck(actor.gameObject, deck, hideBackSide, m_CardAnimatePlayToDeckTimeScale));
		actor.Destroy();
	}

	private Vector3 ComputeRevealPosition(Vector3 offset)
	{
		Vector3 position = base.transform.position;
		float num = UnityEngine.Random.Range(m_RevealYOffsetMin, m_RevealYOffsetMax);
		position.y += num;
		switch (GetSourceCard().GetControllerSide())
		{
		case Player.Side.FRIENDLY:
			position.z += m_RevealFriendlySideZOffset;
			break;
		case Player.Side.OPPOSING:
			position.z += m_RevealOpponentSideZOffset;
			break;
		}
		position += m_RevealBaseOffset;
		position += offset;
		return position;
	}

	private void PreventHandOverlapPhone(List<Actor> actors, List<Vector3> revealPositions)
	{
		if (!UniversalInputManager.UsePhoneUI)
		{
			return;
		}
		Entity powerTarget = GetPowerTarget();
		if (powerTarget != null)
		{
			if (powerTarget.GetControllerSide() == Player.Side.OPPOSING)
			{
				return;
			}
		}
		else
		{
			Card sourceCard = GetSourceCard();
			if (sourceCard != null && sourceCard.GetControllerSide() == Player.Side.OPPOSING)
			{
				return;
			}
		}
		for (int i = 0; i < revealPositions.Count; i++)
		{
			Vector3 vector = revealPositions[i];
			vector = (revealPositions[i] = new Vector3(vector.x, vector.y, vector.z + 1.5f));
		}
	}

	private void BoundRevealPositions(List<Actor> actors, List<Vector3> revealPositions)
	{
		float num = float.MinValue;
		float num2 = float.MaxValue;
		for (int i = 0; i < revealPositions.Count; i++)
		{
			ZoneDeck deckZone = m_targets[i].GetComponent<Card>().GetEntity().GetController()
				.GetDeckZone();
			float num3 = 0f;
			Actor activeThickness = deckZone.GetActiveThickness();
			if (activeThickness != null)
			{
				num3 = activeThickness.GetMeshRenderer().bounds.extents.x;
			}
			Vector3 position = deckZone.transform.position;
			position.x -= num3;
			Vector3 position2 = revealPositions[i];
			position2.x += actors[i].GetMeshRenderer().bounds.extents.x;
			Vector3 position3 = revealPositions[i];
			position3.x -= actors[i].GetMeshRenderer().bounds.extents.x;
			Vector3 vector = Camera.main.WorldToScreenPoint(position);
			Vector3 vector2 = Camera.main.WorldToScreenPoint(position2);
			Vector3 vector3 = Camera.main.WorldToScreenPoint(position3);
			float num4 = vector2.x - vector.x;
			if (num4 > num)
			{
				num = num4;
			}
			float x = vector3.x;
			if (x < num2)
			{
				num2 = x;
			}
		}
		if (!(num2 >= 0f) || !(num <= 0f))
		{
			float num5 = 0f;
			float num6 = CameraUtils.ScreenToWorldDist(screenDist: (!(num > 0f)) ? Math.Max(num2, num) : num, camera: Camera.main, worldPoint: revealPositions[0]);
			for (int j = 0; j < revealPositions.Count; j++)
			{
				Vector3 value = revealPositions[j];
				value.x -= num6;
				revealPositions[j] = value;
			}
		}
	}

	private List<float> RandomizeRevealTimes(int count, float revealSec, float nextRevealSecMin, float nextRevealSecMax)
	{
		List<float> list = new List<float>(count);
		List<int> list2 = new List<int>(count);
		for (int i = 0; i < count; i++)
		{
			list.Add(0f);
			list2.Add(i);
		}
		float num = revealSec;
		for (int j = 0; j < count; j++)
		{
			int index = UnityEngine.Random.Range(0, list2.Count);
			int index2 = list2[index];
			list2.RemoveAt(index);
			list[index2] = num;
			float num2 = UnityEngine.Random.Range(nextRevealSecMin, nextRevealSecMax);
			num += num2;
		}
		return list;
	}

	private IEnumerator AnimateActor(List<Actor> actors, int index, float revealSec, Vector3 revealPos, float waitSec)
	{
		Actor actor = actors[index];
		GameObject gameObject = m_targets[index];
		Card card = gameObject.GetComponent<Card>();
		Entity entity = card.GetEntity();
		Player controller = entity.GetController();
		ZonePlay battlefieldZone = controller.GetBattlefieldZone();
		ZoneDeck deck = controller.GetDeckZone();
		actor.transform.localScale = new Vector3(m_RevealStartScale, m_RevealStartScale, m_RevealStartScale);
		actor.transform.rotation = base.transform.rotation;
		actor.transform.position = base.transform.position;
		actor.Show();
		Vector3 one = Vector3.one;
		Vector3 eulerAngles = battlefieldZone.transform.rotation.eulerAngles;
		iTween.MoveTo(actor.gameObject, iTween.Hash("position", revealPos, "time", revealSec, "easetype", iTween.EaseType.easeOutExpo));
		iTween.RotateTo(actor.gameObject, iTween.Hash("rotation", eulerAngles, "time", revealSec, "easetype", iTween.EaseType.easeOutExpo));
		iTween.ScaleTo(actor.gameObject, iTween.Hash("scale", one, "time", revealSec, "easetype", iTween.EaseType.easeOutExpo));
		if (waitSec > 0f)
		{
			yield return new WaitForSeconds(waitSec);
		}
		bool hideBackSide = GetEntityDef(entity, index).GetCardType() == TAG_CARDTYPE.INVALID;
		yield return StartCoroutine(card.AnimatePlayToDeck(actor.gameObject, deck, hideBackSide, m_CardAnimatePlayToDeckTimeScale));
		actor.Destroy();
	}

	private TAG_PREMIUM GetPremium(Entity entity)
	{
		TAG_PREMIUM premiumType = GetSourceCard().GetEntity().GetPremiumType();
		switch (m_HandActorSource)
		{
		case HandActorSource.CHOSEN_TARGET:
		{
			TAG_PREMIUM premiumType2 = GetPowerTarget().GetPremiumType();
			if (premiumType <= premiumType2)
			{
				return premiumType2;
			}
			return premiumType;
		}
		case HandActorSource.OVERRIDE:
			return premiumType;
		default:
			return entity.GetPremiumType();
		}
	}

	private EntityDef GetEntityDef(Entity entity, int index = 0)
	{
		return m_HandActorSource switch
		{
			HandActorSource.CHOSEN_TARGET => GetPowerTarget().GetEntityDef(), 
			HandActorSource.OVERRIDE => DefLoader.Get().GetEntityDef(m_OverrideCardIds[Math.Min(index, m_OverrideCardIds.Count - 1)]), 
			_ => entity.GetEntityDef(), 
		};
	}

	private DefLoader.DisposableCardDef ShareDisposableCardDef(Card card, int index = 0)
	{
		return m_HandActorSource switch
		{
			HandActorSource.CHOSEN_TARGET => GetPowerTargetCard().ShareDisposableCardDef(), 
			HandActorSource.OVERRIDE => m_overrideCardDefs[Math.Min(index, m_overrideCardDefs.Count - 1)]?.Share(), 
			_ => card.ShareDisposableCardDef(), 
		};
	}
}
