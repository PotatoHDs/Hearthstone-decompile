using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000823 RID: 2083
public class SpawnToDeckSpell : SuperSpell
{
	// Token: 0x06006FF5 RID: 28661 RVA: 0x00241C6E File Offset: 0x0023FE6E
	protected override void OnDestroy()
	{
		this.m_overrideCardDefs.DisposeValuesAndClear<DefLoader.DisposableCardDef>();
		base.OnDestroy();
	}

	// Token: 0x06006FF6 RID: 28662 RVA: 0x00241C84 File Offset: 0x0023FE84
	public override bool AddPowerTargets()
	{
		this.m_visualToTargetIndexMap.Clear();
		this.m_targetToMetaDataMap.Clear();
		this.m_targets.Clear();
		List<PowerTask> taskList = this.m_taskList.GetTaskList();
		for (int i = 0; i < taskList.Count; i++)
		{
			PowerTask task = taskList[i];
			Card targetCardFromPowerTask = this.GetTargetCardFromPowerTask(i, task);
			if (!(targetCardFromPowerTask == null) && base.IsValidSpellTarget(targetCardFromPowerTask.GetEntity()))
			{
				this.AddTarget(targetCardFromPowerTask.gameObject);
			}
		}
		return this.m_targets.Count > 0;
	}

	// Token: 0x06006FF7 RID: 28663 RVA: 0x00241D14 File Offset: 0x0023FF14
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
			Debug.LogWarning(string.Format("{0}.GetTargetCardFromPowerTask() - WARNING trying to target entity with id {1} but there is no entity with that id", this, entity.ID));
			return null;
		}
		if (entity2.GetZone() != TAG_ZONE.DECK)
		{
			return null;
		}
		return entity2.GetCard();
	}

	// Token: 0x06006FF8 RID: 28664 RVA: 0x00241D81 File Offset: 0x0023FF81
	protected override void OnAction(SpellStateType prevStateType)
	{
		this.m_effectsPendingFinish++;
		base.OnAction(prevStateType);
		base.StartCoroutine(this.DoActionWithTiming());
	}

	// Token: 0x06006FF9 RID: 28665 RVA: 0x00241DA5 File Offset: 0x0023FFA5
	private IEnumerator ProcessShowEntityForTargets()
	{
		PowerTaskList powerTaskList = base.GetPowerTaskList();
		foreach (PowerTask powerTask in powerTaskList.GetTaskList())
		{
			Network.HistShowEntity histShowEntity = powerTask.GetPower() as Network.HistShowEntity;
			if (histShowEntity != null)
			{
				Network.Entity entity = histShowEntity.Entity;
				Entity target = this.FindTargetEntity(entity.ID);
				if (target != null)
				{
					foreach (Network.Entity.Tag tag in entity.Tags)
					{
						target.SetTag(tag.Name, tag.Value);
					}
					target.LoadCard(entity.CardID, null);
					while (target.IsLoadingAssets())
					{
						yield return null;
					}
					target = null;
				}
			}
		}
		List<PowerTask>.Enumerator enumerator = default(List<PowerTask>.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x06006FFA RID: 28666 RVA: 0x00241DB4 File Offset: 0x0023FFB4
	private Entity FindTargetEntity(int entityID)
	{
		foreach (GameObject gameObject in this.m_targets)
		{
			Card component = gameObject.GetComponent<Card>();
			if (!(component == null))
			{
				Entity entity = component.GetEntity();
				if (entity != null && entity.GetEntityId() == entityID)
				{
					return entity;
				}
			}
		}
		Entity powerTarget = base.GetPowerTarget();
		if (powerTarget != null && powerTarget.GetEntityId() == entityID)
		{
			return powerTarget;
		}
		return null;
	}

	// Token: 0x06006FFB RID: 28667 RVA: 0x00241E40 File Offset: 0x00240040
	private IEnumerator DoActionWithTiming()
	{
		List<Actor> actors = new List<Actor>(this.m_targets.Count);
		yield return base.StartCoroutine(this.ProcessShowEntityForTargets());
		yield return base.StartCoroutine(this.LoadAssets(actors));
		yield return new WaitForSeconds(this.m_CardDelay);
		yield return base.StartCoroutine(this.DoEffects(actors));
		yield break;
	}

	// Token: 0x06006FFC RID: 28668 RVA: 0x00241E4F File Offset: 0x0024004F
	private IEnumerator LoadAssets(List<Actor> actors)
	{
		SpawnToDeckSpell.<>c__DisplayClass33_0 CS$<>8__locals1 = new SpawnToDeckSpell.<>c__DisplayClass33_0();
		CS$<>8__locals1.<>4__this = this;
		CS$<>8__locals1.actors = actors;
		CS$<>8__locals1.loadingOverrideCardDef = false;
		if (this.m_OverrideCardIds.Count == 0)
		{
			this.m_OverrideCardIds.Add(this.m_OverrideCardId);
		}
		int num;
		for (int i = 0; i < this.m_OverrideCardIds.Count; i = num)
		{
			SpawnToDeckSpell.<>c__DisplayClass33_1 CS$<>8__locals2 = new SpawnToDeckSpell.<>c__DisplayClass33_1();
			CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
			if (!string.IsNullOrEmpty(this.m_OverrideCardIds[i]))
			{
				CS$<>8__locals2.CS$<>8__locals1.loadingOverrideCardDef = true;
				DefLoader.LoadDefCallback<DefLoader.DisposableCardDef> loadDefCallback;
				if ((loadDefCallback = CS$<>8__locals2.CS$<>8__locals1.<>9__1) == null)
				{
					loadDefCallback = (CS$<>8__locals2.CS$<>8__locals1.<>9__1 = delegate(string cardId, DefLoader.DisposableCardDef def, object userData)
					{
						CS$<>8__locals2.CS$<>8__locals1.loadingOverrideCardDef = false;
						if (def == null)
						{
							Error.AddDevFatal("SpawnToDeckSpell.LoadAssets() - FAILED to load CardDef for {0}", new object[]
							{
								cardId
							});
							return;
						}
						CS$<>8__locals2.CS$<>8__locals1.<>4__this.m_overrideCardDefs.Add(def);
					});
				}
				DefLoader.LoadDefCallback<DefLoader.DisposableCardDef> callback = loadDefCallback;
				DefLoader.Get().LoadCardDef(this.m_OverrideCardIds[i], callback, null, null);
				while (CS$<>8__locals2.CS$<>8__locals1.loadingOverrideCardDef)
				{
					yield return null;
				}
			}
			CS$<>8__locals2.assetsLoading = 1;
			if (i == this.m_OverrideCardIds.Count - 1 && this.m_targets.Count > this.m_OverrideCardIds.Count)
			{
				CS$<>8__locals2.assetsLoading = this.m_targets.Count - this.m_OverrideCardIds.Count + 1;
			}
			PrefabCallback<GameObject> callback2 = delegate(AssetReference assetRef, GameObject go, object callbackData)
			{
				int assetsLoading2 = CS$<>8__locals2.assetsLoading - 1;
				CS$<>8__locals2.assetsLoading = assetsLoading2;
				int num2 = (int)callbackData;
				if (num2 > CS$<>8__locals2.CS$<>8__locals1.<>4__this.m_targets.Count - 1)
				{
					return;
				}
				if (go == null)
				{
					Error.AddDevFatal("SpawnToDeckSpell.LoadAssets() - FAILED to load actor {0} (targetIndex {1})", new object[]
					{
						CS$<>8__locals2.CS$<>8__locals1.<>4__this.name,
						num2
					});
					return;
				}
				Actor component = go.GetComponent<Actor>();
				Card component2 = CS$<>8__locals2.CS$<>8__locals1.<>4__this.m_targets[num2].GetComponent<Card>();
				Entity entity2 = component2.GetEntity();
				if (entity2.GetLoadState() == Entity.LoadState.DONE)
				{
					component.SetEntity(entity2);
				}
				else
				{
					component.SetPremium(CS$<>8__locals2.CS$<>8__locals1.<>4__this.GetPremium(entity2));
					if (CS$<>8__locals2.CS$<>8__locals1.<>4__this.m_HandActorSource == SpawnToDeckSpell.HandActorSource.CHOSEN_TARGET)
					{
						Entity powerTarget = CS$<>8__locals2.CS$<>8__locals1.<>4__this.GetPowerTarget();
						if (powerTarget != null)
						{
							string cardTextInHand = powerTarget.GetCardTextInHand();
							component.SetCardDefPowerTextOverride(cardTextInHand);
						}
					}
				}
				component.SetEntityDef(CS$<>8__locals2.CS$<>8__locals1.<>4__this.GetEntityDef(entity2, num2));
				using (DefLoader.DisposableCardDef disposableCardDef = CS$<>8__locals2.CS$<>8__locals1.<>4__this.ShareDisposableCardDef(component2, num2))
				{
					component.SetCardDef(disposableCardDef);
				}
				component.SetCardBackSideOverride(new Player.Side?(entity2.GetControllerSide()));
				component.UpdateAllComponents();
				component.Hide();
				CS$<>8__locals2.CS$<>8__locals1.actors[num2] = component;
				CS$<>8__locals2.CS$<>8__locals1.<>4__this.OnActorLoaded(component);
			};
			int assetsLoading = CS$<>8__locals2.assetsLoading;
			for (int j = 0; j < assetsLoading; j++)
			{
				Entity entity = this.m_targets[Math.Min(i + j, this.m_targets.Count - 1)].GetComponent<Card>().GetEntity();
				EntityDef entityDef = this.GetEntityDef(entity, i + j);
				TAG_PREMIUM premium = this.GetPremium(entity);
				if (CS$<>8__locals2.CS$<>8__locals1.actors.Count < this.m_targets.Count)
				{
					CS$<>8__locals2.CS$<>8__locals1.actors.Add(null);
				}
				AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(entityDef, premium), callback2, i + j, AssetLoadingOptions.IgnorePrefabPosition);
			}
			while (CS$<>8__locals2.assetsLoading > 0)
			{
				yield return null;
			}
			CS$<>8__locals2 = null;
			num = i + 1;
		}
		yield break;
	}

	// Token: 0x06006FFD RID: 28669 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void OnActorLoaded(Actor actor)
	{
	}

	// Token: 0x06006FFE RID: 28670 RVA: 0x00241E65 File Offset: 0x00240065
	private IEnumerator DoEffects(List<Actor> actors)
	{
		base.StartCoroutine(this.AnimateSpread(actors));
		Actor livingActor = null;
		do
		{
			livingActor = actors.Find((Actor currActor) => currActor);
			if (livingActor)
			{
				yield return null;
			}
		}
		while (livingActor);
		this.m_effectsPendingFinish--;
		base.FinishIfPossible();
		yield break;
	}

	// Token: 0x06006FFF RID: 28671 RVA: 0x00241E7B File Offset: 0x0024007B
	private float GetRevealSec(int iterationCount)
	{
		if (iterationCount > 0)
		{
			return 0.3f;
		}
		return this.m_SequenceData.m_RevealTime;
	}

	// Token: 0x06007000 RID: 28672 RVA: 0x00241E92 File Offset: 0x00240092
	private float GetHoldSec(int iterationCount)
	{
		if (iterationCount > 0)
		{
			return 0f;
		}
		return this.m_SequenceData.m_HoldTime;
	}

	// Token: 0x06007001 RID: 28673 RVA: 0x00241EA9 File Offset: 0x002400A9
	private float GetNextCardHoldSec(int iterationCount)
	{
		if (iterationCount > 0)
		{
			return 0.2f;
		}
		return this.m_SequenceData.m_NextCardHoldTime;
	}

	// Token: 0x06007002 RID: 28674 RVA: 0x00241EC0 File Offset: 0x002400C0
	private IEnumerator WaitForBatchToAnimate(int batchSize, int iterationCount)
	{
		float seconds = (this.m_SequenceData.m_NextCardRevealTimeMax + this.GetNextCardHoldSec(iterationCount)) * (float)(batchSize - 1) + this.GetRevealSec(iterationCount) + this.GetHoldSec(iterationCount);
		yield return new WaitForSeconds(seconds);
		yield break;
	}

	// Token: 0x06007003 RID: 28675 RVA: 0x00241EE0 File Offset: 0x002400E0
	private void AnimateSequence(List<Actor> actors, int iterationCount)
	{
		List<Vector3> list = new List<Vector3>();
		float num = -0.5f * (float)(actors.Count - 1) * this.m_SequenceData.m_Spacing;
		for (int i = 0; i < actors.Count; i++)
		{
			float num2 = (float)i * this.m_SequenceData.m_Spacing;
			Vector3 offset = new Vector3(num + num2, 0f, 0f);
			Vector3 item = this.ComputeRevealPosition(offset);
			list.Add(item);
		}
		this.BoundRevealPositions(actors, list);
		this.PreventHandOverlapPhone(actors, list);
		float revealSec = this.GetRevealSec(iterationCount);
		float holdSec = this.GetHoldSec(iterationCount);
		float nextCardHoldSec = this.GetNextCardHoldSec(iterationCount);
		List<float> list2 = this.RandomizeRevealTimes(actors.Count, revealSec, this.m_SequenceData.m_NextCardRevealTimeMin, this.m_SequenceData.m_NextCardRevealTimeMax);
		float num3 = Mathf.Max(list2.ToArray());
		for (int j = 0; j < actors.Count; j++)
		{
			Vector3 revealPos = list[j];
			float revealSec2 = list2[j];
			float num4 = (float)(actors.Count - 1 - j) * nextCardHoldSec;
			float num5 = holdSec + num4;
			float waitSec = num3 + num5;
			base.StartCoroutine(this.AnimateActor(actors, j, revealSec2, revealPos, waitSec));
		}
	}

	// Token: 0x06007004 RID: 28676 RVA: 0x00242016 File Offset: 0x00240216
	private IEnumerator AnimateSpread(List<Actor> actors)
	{
		if (this.m_SpreadType == SpawnToDeckSpell.SpreadType.SEQUENCE)
		{
			List<Actor> batchedActors = new List<Actor>();
			int iterationCount = 0;
			foreach (Actor actor in actors)
			{
				if (batchedActors.Count == 5)
				{
					this.AnimateSequence(batchedActors, iterationCount);
					yield return this.WaitForBatchToAnimate(batchedActors.Count, iterationCount);
					int num = iterationCount;
					iterationCount = num + 1;
					batchedActors.Clear();
				}
				batchedActors.Add(actor);
				actor = null;
			}
			List<Actor>.Enumerator enumerator = default(List<Actor>.Enumerator);
			if (batchedActors.Count > 0)
			{
				this.AnimateSequence(batchedActors, iterationCount);
				yield return this.WaitForBatchToAnimate(batchedActors.Count, iterationCount);
			}
			batchedActors = null;
		}
		else if (this.m_SpreadType == SpawnToDeckSpell.SpreadType.STACK)
		{
			int num;
			for (int iterationCount = 0; iterationCount < actors.Count; iterationCount = num)
			{
				Vector3 revealPos = this.ComputeRevealPosition(Vector3.zero);
				base.StartCoroutine(this.AnimateActor(actors, iterationCount, this.m_StackData.m_RevealTime, revealPos, this.m_StackData.m_RevealTime));
				if (iterationCount < actors.Count - 1)
				{
					yield return new WaitForSeconds(this.m_StackData.m_StaggerTime);
				}
				num = iterationCount + 1;
			}
		}
		else if (this.m_SpreadType == SpawnToDeckSpell.SpreadType.CUSTOM_SPELL)
		{
			for (int i = 0; i < actors.Count; i++)
			{
				this.AnimateActorUsingSpell(actors, i);
			}
		}
		yield break;
		yield break;
	}

	// Token: 0x06007005 RID: 28677 RVA: 0x0024202C File Offset: 0x0024022C
	private void AnimateActorUsingSpell(List<Actor> actors, int index)
	{
		Actor actor = actors[index];
		GameObject gameObject = this.m_targets[index];
		Card component = gameObject.GetComponent<Card>();
		actor.transform.localScale = new Vector3(this.m_RevealStartScale, this.m_RevealStartScale, this.m_RevealStartScale);
		actor.transform.rotation = base.transform.rotation;
		actor.transform.position = base.transform.position;
		actor.Show();
		SpawnToDeckSpell.RevealSpellFinishedCallbackData revealSpellFinishedCallbackData = default(SpawnToDeckSpell.RevealSpellFinishedCallbackData);
		revealSpellFinishedCallbackData.actor = actor;
		revealSpellFinishedCallbackData.card = component;
		revealSpellFinishedCallbackData.id = index;
		if (this.m_customRevealSpell == null)
		{
			Log.Spells.PrintError("SpawnToDeckSpell.AnimateSpread(): m_SpreadType is set to CUSTOM_SPELL, but m_customRevealSpell is null!", Array.Empty<object>());
			this.OnRevealSpellFinished(null, revealSpellFinishedCallbackData);
			return;
		}
		Spell spell = UnityEngine.Object.Instantiate<Spell>(this.m_customRevealSpell);
		SpellUtils.SetCustomSpellParent(spell, actor);
		spell.AddFinishedCallback(new Spell.FinishedCallback(this.OnRevealSpellFinished), revealSpellFinishedCallbackData);
		spell.SetSource(base.GetSource());
		spell.AddTarget(gameObject);
		spell.Activate();
	}

	// Token: 0x06007006 RID: 28678 RVA: 0x0024213C File Offset: 0x0024033C
	public void OnRevealSpellFinished(Spell spell, object userData)
	{
		SpawnToDeckSpell.RevealSpellFinishedCallbackData revealSpellFinishedCallbackData = (SpawnToDeckSpell.RevealSpellFinishedCallbackData)userData;
		Actor actor = revealSpellFinishedCallbackData.actor;
		Card card = revealSpellFinishedCallbackData.card;
		Entity entity = card.GetEntity();
		ZoneDeck deckZone = entity.GetController().GetDeckZone();
		bool hideBackSide = this.GetEntityDef(entity, revealSpellFinishedCallbackData.id).GetCardType() == TAG_CARDTYPE.INVALID;
		base.StartCoroutine(this.AnimateRevealedCardToDeck(actor, card, deckZone, hideBackSide));
	}

	// Token: 0x06007007 RID: 28679 RVA: 0x0024219C File Offset: 0x0024039C
	public IEnumerator AnimateRevealedCardToDeck(Actor actor, Card card, ZoneDeck deck, bool hideBackSide)
	{
		yield return base.StartCoroutine(card.AnimatePlayToDeck(actor.gameObject, deck, hideBackSide, this.m_CardAnimatePlayToDeckTimeScale));
		actor.Destroy();
		yield break;
	}

	// Token: 0x06007008 RID: 28680 RVA: 0x002421C8 File Offset: 0x002403C8
	private Vector3 ComputeRevealPosition(Vector3 offset)
	{
		Vector3 vector = base.transform.position;
		float num = UnityEngine.Random.Range(this.m_RevealYOffsetMin, this.m_RevealYOffsetMax);
		vector.y += num;
		Player.Side controllerSide = base.GetSourceCard().GetControllerSide();
		if (controllerSide != Player.Side.FRIENDLY)
		{
			if (controllerSide == Player.Side.OPPOSING)
			{
				vector.z += this.m_RevealOpponentSideZOffset;
			}
		}
		else
		{
			vector.z += this.m_RevealFriendlySideZOffset;
		}
		vector += this.m_RevealBaseOffset;
		vector += offset;
		return vector;
	}

	// Token: 0x06007009 RID: 28681 RVA: 0x00242250 File Offset: 0x00240450
	private void PreventHandOverlapPhone(List<Actor> actors, List<Vector3> revealPositions)
	{
		if (!UniversalInputManager.UsePhoneUI)
		{
			return;
		}
		Entity powerTarget = base.GetPowerTarget();
		if (powerTarget != null)
		{
			if (powerTarget.GetControllerSide() == Player.Side.OPPOSING)
			{
				return;
			}
		}
		else
		{
			Card sourceCard = base.GetSourceCard();
			if (sourceCard != null && sourceCard.GetControllerSide() == Player.Side.OPPOSING)
			{
				return;
			}
		}
		for (int i = 0; i < revealPositions.Count; i++)
		{
			Vector3 vector = revealPositions[i];
			vector = new Vector3(vector.x, vector.y, vector.z + 1.5f);
			revealPositions[i] = vector;
		}
	}

	// Token: 0x0600700A RID: 28682 RVA: 0x002422D8 File Offset: 0x002404D8
	private void BoundRevealPositions(List<Actor> actors, List<Vector3> revealPositions)
	{
		float num = float.MinValue;
		float num2 = float.MaxValue;
		for (int i = 0; i < revealPositions.Count; i++)
		{
			ZoneDeck deckZone = this.m_targets[i].GetComponent<Card>().GetEntity().GetController().GetDeckZone();
			float num3 = 0f;
			Actor activeThickness = deckZone.GetActiveThickness();
			if (activeThickness != null)
			{
				num3 = activeThickness.GetMeshRenderer(false).bounds.extents.x;
			}
			Vector3 position = deckZone.transform.position;
			position.x -= num3;
			Vector3 position2 = revealPositions[i];
			position2.x += actors[i].GetMeshRenderer(false).bounds.extents.x;
			Vector3 position3 = revealPositions[i];
			position3.x -= actors[i].GetMeshRenderer(false).bounds.extents.x;
			Vector3 vector = Camera.main.WorldToScreenPoint(position);
			ref Vector3 ptr = Camera.main.WorldToScreenPoint(position2);
			Vector3 vector2 = Camera.main.WorldToScreenPoint(position3);
			float num4 = ptr.x - vector.x;
			if (num4 > num)
			{
				num = num4;
			}
			float x = vector2.x;
			if (x < num2)
			{
				num2 = x;
			}
		}
		if (num2 >= 0f && num <= 0f)
		{
			return;
		}
		float screenDist;
		if (num > 0f)
		{
			screenDist = num;
		}
		else
		{
			screenDist = Math.Max(num2, num);
		}
		float num5 = CameraUtils.ScreenToWorldDist(Camera.main, screenDist, revealPositions[0]);
		for (int j = 0; j < revealPositions.Count; j++)
		{
			Vector3 value = revealPositions[j];
			value.x -= num5;
			revealPositions[j] = value;
		}
	}

	// Token: 0x0600700B RID: 28683 RVA: 0x002424AC File Offset: 0x002406AC
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

	// Token: 0x0600700C RID: 28684 RVA: 0x00242530 File Offset: 0x00240730
	private IEnumerator AnimateActor(List<Actor> actors, int index, float revealSec, Vector3 revealPos, float waitSec)
	{
		Actor actor = actors[index];
		GameObject gameObject = this.m_targets[index];
		Card card = gameObject.GetComponent<Card>();
		Entity entity = card.GetEntity();
		Player controller = entity.GetController();
		Component battlefieldZone = controller.GetBattlefieldZone();
		ZoneDeck deck = controller.GetDeckZone();
		actor.transform.localScale = new Vector3(this.m_RevealStartScale, this.m_RevealStartScale, this.m_RevealStartScale);
		actor.transform.rotation = base.transform.rotation;
		actor.transform.position = base.transform.position;
		actor.Show();
		Vector3 one = Vector3.one;
		Vector3 eulerAngles = battlefieldZone.transform.rotation.eulerAngles;
		iTween.MoveTo(actor.gameObject, iTween.Hash(new object[]
		{
			"position",
			revealPos,
			"time",
			revealSec,
			"easetype",
			iTween.EaseType.easeOutExpo
		}));
		iTween.RotateTo(actor.gameObject, iTween.Hash(new object[]
		{
			"rotation",
			eulerAngles,
			"time",
			revealSec,
			"easetype",
			iTween.EaseType.easeOutExpo
		}));
		iTween.ScaleTo(actor.gameObject, iTween.Hash(new object[]
		{
			"scale",
			one,
			"time",
			revealSec,
			"easetype",
			iTween.EaseType.easeOutExpo
		}));
		if (waitSec > 0f)
		{
			yield return new WaitForSeconds(waitSec);
		}
		bool hideBackSide = this.GetEntityDef(entity, index).GetCardType() == TAG_CARDTYPE.INVALID;
		yield return base.StartCoroutine(card.AnimatePlayToDeck(actor.gameObject, deck, hideBackSide, this.m_CardAnimatePlayToDeckTimeScale));
		actor.Destroy();
		yield break;
	}

	// Token: 0x0600700D RID: 28685 RVA: 0x00242564 File Offset: 0x00240764
	private TAG_PREMIUM GetPremium(Entity entity)
	{
		TAG_PREMIUM premiumType = base.GetSourceCard().GetEntity().GetPremiumType();
		SpawnToDeckSpell.HandActorSource handActorSource = this.m_HandActorSource;
		if (handActorSource != SpawnToDeckSpell.HandActorSource.CHOSEN_TARGET)
		{
			if (handActorSource != SpawnToDeckSpell.HandActorSource.OVERRIDE)
			{
				return entity.GetPremiumType();
			}
			return premiumType;
		}
		else
		{
			TAG_PREMIUM premiumType2 = base.GetPowerTarget().GetPremiumType();
			if (premiumType <= premiumType2)
			{
				return premiumType2;
			}
			return premiumType;
		}
	}

	// Token: 0x0600700E RID: 28686 RVA: 0x002425B0 File Offset: 0x002407B0
	private EntityDef GetEntityDef(Entity entity, int index = 0)
	{
		SpawnToDeckSpell.HandActorSource handActorSource = this.m_HandActorSource;
		if (handActorSource == SpawnToDeckSpell.HandActorSource.CHOSEN_TARGET)
		{
			return base.GetPowerTarget().GetEntityDef();
		}
		if (handActorSource != SpawnToDeckSpell.HandActorSource.OVERRIDE)
		{
			return entity.GetEntityDef();
		}
		return DefLoader.Get().GetEntityDef(this.m_OverrideCardIds[Math.Min(index, this.m_OverrideCardIds.Count - 1)]);
	}

	// Token: 0x0600700F RID: 28687 RVA: 0x00242608 File Offset: 0x00240808
	private DefLoader.DisposableCardDef ShareDisposableCardDef(Card card, int index = 0)
	{
		SpawnToDeckSpell.HandActorSource handActorSource = this.m_HandActorSource;
		if (handActorSource == SpawnToDeckSpell.HandActorSource.CHOSEN_TARGET)
		{
			return base.GetPowerTargetCard().ShareDisposableCardDef();
		}
		if (handActorSource != SpawnToDeckSpell.HandActorSource.OVERRIDE)
		{
			return card.ShareDisposableCardDef();
		}
		DefLoader.DisposableCardDef disposableCardDef = this.m_overrideCardDefs[Math.Min(index, this.m_overrideCardDefs.Count - 1)];
		if (disposableCardDef == null)
		{
			return null;
		}
		return disposableCardDef.Share();
	}

	// Token: 0x040059C7 RID: 22983
	private const float PHONE_HAND_OFFSET = 1.5f;

	// Token: 0x040059C8 RID: 22984
	private const int SEQUENCE_BATCH_SIZE = 5;

	// Token: 0x040059C9 RID: 22985
	private const float SEQUENCE_BATCH_REVEAL_TIME = 0.3f;

	// Token: 0x040059CA RID: 22986
	private const float SEQUENCE_BATCH_HOLD_TIME = 0f;

	// Token: 0x040059CB RID: 22987
	private const float SEQUENCE_BATCH_NEXT_CARD_HOLD_TIME = 0.2f;

	// Token: 0x040059CC RID: 22988
	public SpawnToDeckSpell.HandActorSource m_HandActorSource;

	// Token: 0x040059CD RID: 22989
	public string m_OverrideCardId;

	// Token: 0x040059CE RID: 22990
	public List<string> m_OverrideCardIds = new List<string>();

	// Token: 0x040059CF RID: 22991
	public float m_CardDelay;

	// Token: 0x040059D0 RID: 22992
	public float m_CardAnimatePlayToDeckTimeScale = 1f;

	// Token: 0x040059D1 RID: 22993
	public float m_RevealStartScale = 0.1f;

	// Token: 0x040059D2 RID: 22994
	public float m_RevealYOffsetMin = 5f;

	// Token: 0x040059D3 RID: 22995
	public float m_RevealYOffsetMax = 5f;

	// Token: 0x040059D4 RID: 22996
	public float m_RevealFriendlySideZOffset;

	// Token: 0x040059D5 RID: 22997
	public float m_RevealOpponentSideZOffset;

	// Token: 0x040059D6 RID: 22998
	public Vector3 m_RevealBaseOffset = Vector3.zero;

	// Token: 0x040059D7 RID: 22999
	public SpawnToDeckSpell.SpreadType m_SpreadType;

	// Token: 0x040059D8 RID: 23000
	public SpawnToDeckSpell.StackData m_StackData = new SpawnToDeckSpell.StackData();

	// Token: 0x040059D9 RID: 23001
	public SpawnToDeckSpell.SequenceData m_SequenceData = new SpawnToDeckSpell.SequenceData();

	// Token: 0x040059DA RID: 23002
	public Spell m_customRevealSpell;

	// Token: 0x040059DB RID: 23003
	private List<DefLoader.DisposableCardDef> m_overrideCardDefs = new List<DefLoader.DisposableCardDef>();

	// Token: 0x020023E7 RID: 9191
	public enum HandActorSource
	{
		// Token: 0x0400E875 RID: 59509
		CHOSEN_TARGET,
		// Token: 0x0400E876 RID: 59510
		OVERRIDE,
		// Token: 0x0400E877 RID: 59511
		SPELL_TARGET
	}

	// Token: 0x020023E8 RID: 9192
	public enum SpreadType
	{
		// Token: 0x0400E879 RID: 59513
		STACK,
		// Token: 0x0400E87A RID: 59514
		SEQUENCE,
		// Token: 0x0400E87B RID: 59515
		CUSTOM_SPELL
	}

	// Token: 0x020023E9 RID: 9193
	[Serializable]
	public class StackData
	{
		// Token: 0x0400E87C RID: 59516
		public float m_RevealTime = 1f;

		// Token: 0x0400E87D RID: 59517
		public float m_StaggerTime = 1.2f;
	}

	// Token: 0x020023EA RID: 9194
	[Serializable]
	public class SequenceData
	{
		// Token: 0x0400E87E RID: 59518
		public float m_Spacing = 2.1f;

		// Token: 0x0400E87F RID: 59519
		public float m_RevealTime = 0.6f;

		// Token: 0x0400E880 RID: 59520
		public float m_NextCardRevealTimeMin = 0.1f;

		// Token: 0x0400E881 RID: 59521
		public float m_NextCardRevealTimeMax = 0.2f;

		// Token: 0x0400E882 RID: 59522
		public float m_HoldTime = 0.3f;

		// Token: 0x0400E883 RID: 59523
		public float m_NextCardHoldTime = 0.4f;
	}

	// Token: 0x020023EB RID: 9195
	private struct RevealSpellFinishedCallbackData
	{
		// Token: 0x0400E884 RID: 59524
		public Actor actor;

		// Token: 0x0400E885 RID: 59525
		public Card card;

		// Token: 0x0400E886 RID: 59526
		public int id;
	}
}
