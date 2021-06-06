using System;
using System.Collections;
using System.Collections.Generic;
using PegasusGame;
using UnityEngine;

// Token: 0x02000902 RID: 2306
public class QuestCompleteSpell : Spell
{
	// Token: 0x06008051 RID: 32849 RVA: 0x0029B524 File Offset: 0x00299724
	public override bool AddPowerTargets()
	{
		if (!base.CanAddPowerTargets())
		{
			return false;
		}
		if (this.m_taskList.GetBlockType() != HistoryBlock.Type.TRIGGER)
		{
			return false;
		}
		this.m_originalQuestEntity = this.m_taskList.GetSourceEntity(false);
		if (!this.m_originalQuestEntity.IsQuest())
		{
			Log.Spells.PrintError("QuestCompleteSpell.AddPowerTargets(): QuestCompleteSpell has been hooked up to a Card that is not a quest!", Array.Empty<object>());
			return false;
		}
		return this.FindQuestRewardFullEntityTask() && this.LoadFakeQuestActors();
	}

	// Token: 0x06008052 RID: 32850 RVA: 0x0029B598 File Offset: 0x00299798
	private bool FindQuestRewardFullEntityTask()
	{
		List<PowerTask> taskList = this.m_taskList.GetTaskList();
		for (int i = 0; i < taskList.Count; i++)
		{
			Network.HistFullEntity histFullEntity = taskList[i].GetPower() as Network.HistFullEntity;
			if (histFullEntity != null)
			{
				this.m_questRewardSpawnTaskIndex = i;
				this.m_questReward = GameState.Get().GetEntity(histFullEntity.Entity.ID);
				Log.Spells.PrintDebug("QuestCompleteSpell.FindQuestRewardFullEntityTask(): Found reward at task index:{0}, entityId:{1}", new object[]
				{
					this.m_questRewardSpawnTaskIndex,
					histFullEntity.Entity.ID
				});
				return true;
			}
		}
		return false;
	}

	// Token: 0x06008053 RID: 32851 RVA: 0x0029B632 File Offset: 0x00299832
	private bool LoadFakeQuestActors()
	{
		return this.LoadFakeQuestActor() && (!(this.m_CustomRewardSpellPrefab == null) || this.LoadFakeQuestRewardActor());
	}

	// Token: 0x06008054 RID: 32852 RVA: 0x0029B658 File Offset: 0x00299858
	private bool LoadFakeQuestActor()
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(this.m_originalQuestEntity), AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject == null)
		{
			Log.Spells.PrintError("QuestCompleteSpell.LoadFakeQuestActor(): Unable to load hand actor for entity {0}.", new object[]
			{
				this.m_originalQuestEntity
			});
			return false;
		}
		base.GetComponent<PlayMakerFSM>().FsmVariables.GetFsmGameObject("RewardCard").Value = gameObject;
		this.m_fakeQuestActor = gameObject.GetComponentInChildren<Actor>();
		this.m_fakeQuestActor.SetEntityDef(this.m_originalQuestEntity.GetEntityDef());
		this.m_fakeQuestActor.SetCardDefFromEntity(this.m_originalQuestEntity);
		this.m_fakeQuestActor.SetPremium(this.m_originalQuestEntity.GetPremiumType());
		this.m_fakeQuestActor.SetWatermarkCardSetOverride(this.m_originalQuestEntity.GetWatermarkCardSetOverride());
		this.m_fakeQuestActor.UpdateAllComponents();
		this.m_fakeQuestActor.Hide();
		return true;
	}

	// Token: 0x06008055 RID: 32853 RVA: 0x0029B73C File Offset: 0x0029993C
	private bool LoadFakeQuestRewardActor()
	{
		string rewardCardIDFromQuestCardID = QuestController.GetRewardCardIDFromQuestCardID(this.m_originalQuestEntity);
		if (string.IsNullOrEmpty(rewardCardIDFromQuestCardID))
		{
			Log.Spells.PrintError("QuestCompleteSpell.LoadFakeQuestRewardActor(): No reward card ID found for quest card ID {0}.", new object[]
			{
				this.m_originalQuestEntity.GetCardId()
			});
			return false;
		}
		bool result;
		using (DefLoader.DisposableFullDef fullDef = DefLoader.Get().GetFullDef(rewardCardIDFromQuestCardID, null))
		{
			if (((fullDef != null) ? fullDef.CardDef : null) == null || ((fullDef != null) ? fullDef.EntityDef : null) == null)
			{
				Log.Spells.PrintError("QuestCompleteSpell.LoadFakeQuestRewardActor(): Unable to load def for card ID {0}.", new object[]
				{
					rewardCardIDFromQuestCardID
				});
				result = false;
			}
			else
			{
				GameObject gameObject = AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(fullDef.EntityDef, this.m_originalQuestEntity.GetPremiumType()), AssetLoadingOptions.IgnorePrefabPosition);
				if (gameObject == null)
				{
					Log.Spells.PrintError("QuestCompleteSpell.LoadFakeQuestRewardActor(): Unable to load Hand Actor for entity def {0}.", new object[]
					{
						fullDef.EntityDef
					});
					result = false;
				}
				else
				{
					this.m_fakeQuestRewardActor = gameObject.GetComponentInChildren<Actor>();
					this.m_fakeQuestRewardActor.SetFullDef(fullDef);
					this.m_fakeQuestRewardActor.SetPremium(this.m_originalQuestEntity.GetPremiumType());
					this.m_fakeQuestRewardActor.SetCardBackSideOverride(new global::Player.Side?(this.m_originalQuestEntity.GetControllerSide()));
					this.m_fakeQuestRewardActor.SetWatermarkCardSetOverride(this.m_originalQuestEntity.GetWatermarkCardSetOverride());
					this.m_fakeQuestRewardActor.UpdateAllComponents();
					this.m_fakeQuestRewardActor.Hide();
					TransformUtil.CopyWorld(this.m_fakeQuestRewardActor, this.m_QuestRewardBone);
					result = true;
				}
			}
		}
		return result;
	}

	// Token: 0x06008056 RID: 32854 RVA: 0x0029B8D8 File Offset: 0x00299AD8
	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		this.m_originalQuestEntity.GetCard().HideCard();
		base.StartCoroutine(this.ScaleUpFakeQuestActor());
	}

	// Token: 0x06008057 RID: 32855 RVA: 0x0029B8FE File Offset: 0x00299AFE
	private IEnumerator ScaleUpFakeQuestActor()
	{
		this.m_fakeQuestActor.Show();
		Transform source = (this.m_originalQuestEntity.GetControllerSide() == global::Player.Side.FRIENDLY) ? this.m_QuestStartBone : this.m_OpponentQuestStartBone;
		TransformUtil.CopyWorld(this.m_fakeQuestActor, source);
		iTween.MoveTo(this.m_fakeQuestActor.gameObject, this.m_QuestEndBone.position, this.m_QuestCardScaleTime);
		iTween.ScaleTo(this.m_fakeQuestActor.gameObject, this.m_QuestEndBone.localScale, this.m_QuestCardScaleTime);
		yield return new WaitForSeconds(this.m_QuestCardScaleTime + this.m_QuestCardHoldTime);
		base.ActivateState(SpellStateType.DEATH);
		yield break;
	}

	// Token: 0x06008058 RID: 32856 RVA: 0x0029B910 File Offset: 0x00299B10
	protected override void OnDeath(SpellStateType prevStateType)
	{
		base.OnDeath(prevStateType);
		if (this.m_CustomRewardSpellPrefab != null)
		{
			Log.Spells.PrintDebug("QuestCompleteSpell.OnDeath(): Register custom reward spell", Array.Empty<object>());
			this.m_AnimationEventDispatcher.RegisterAnimationEventListener(new OnAnimationEvent(this.OnCustomAnimationEvent));
			return;
		}
		Log.Spells.PrintDebug("QuestCompleteSpell.OnDeath(): Register default reward spell", Array.Empty<object>());
		this.m_AnimationEventDispatcher.RegisterAnimationEventListener(new OnAnimationEvent(this.OnDefaultAnimationEvent));
	}

	// Token: 0x06008059 RID: 32857 RVA: 0x0029B989 File Offset: 0x00299B89
	private IEnumerator WaitForRewardActor()
	{
		Card questRewardCard = this.m_questReward.GetCard();
		questRewardCard.SetDoNotSort(true);
		questRewardCard.SetDoNotWarpToNewZone(true);
		questRewardCard.HideCard();
		Log.Spells.PrintDebug("QuestCompleteSpell.WaitForRewardActor(): Start processing tasks up to reward task", Array.Empty<object>());
		this.m_taskList.DoTasks(0, this.m_questRewardSpawnTaskIndex + 1);
		while (questRewardCard.GetActor() == null || questRewardCard.IsActorLoading())
		{
			bool flag = questRewardCard.GetActor() != null;
			Log.Spells.PrintDebug("QuestCompleteSpell.WaitForRewardActor(): hasActor: {0}, IsActorLoading:{1}", new object[]
			{
				flag,
				questRewardCard.IsActorLoading()
			});
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600805A RID: 32858 RVA: 0x0029B998 File Offset: 0x00299B98
	private void OnCustomAnimationEvent(UnityEngine.Object obj)
	{
		this.m_AnimationEventDispatcher.UnregisterAnimationEventListener(new OnAnimationEvent(this.OnCustomAnimationEvent));
		this.m_fakeQuestActor.Hide();
		base.StartCoroutine(this.RunCustomRewardAnimation());
	}

	// Token: 0x0600805B RID: 32859 RVA: 0x0029B9C9 File Offset: 0x00299BC9
	private IEnumerator RunCustomRewardAnimation()
	{
		yield return this.WaitForRewardActor();
		Log.Spells.PrintDebug("QuestCompleteSpell.RunCustomRewardAnimation(): Reward actor ready", Array.Empty<object>());
		Card card = this.m_questReward.GetCard();
		Transform zoneTransformForCard = card.GetZone().GetZoneTransformForCard(card);
		TransformUtil.CopyWorld(card, zoneTransformForCard);
		Spell spell = UnityEngine.Object.Instantiate<Spell>(this.m_CustomRewardSpellPrefab);
		SpellUtils.SetCustomSpellParent(spell, card.GetActor());
		spell.SetSource(card.gameObject);
		spell.AddFinishedCallback(new Spell.FinishedCallback(this.OnCustomRewardSpellFinished));
		spell.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnCustomRewardSpellStateFinished));
		spell.Activate();
		Log.Spells.PrintDebug("QuestCompleteSpell.RunCustomRewardAnimation(): Activated custom spell", Array.Empty<object>());
		yield break;
	}

	// Token: 0x0600805C RID: 32860 RVA: 0x0029B9D8 File Offset: 0x00299BD8
	private void OnCustomRewardSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() == SpellStateType.NONE)
		{
			Log.Spells.PrintDebug("QuestCompleteSpell.OnCustomRewardSpellStateFinished(): NONE state reached", Array.Empty<object>());
			UnityEngine.Object.Destroy(spell.gameObject);
			this.OnStateFinished();
		}
	}

	// Token: 0x0600805D RID: 32861 RVA: 0x0029BA08 File Offset: 0x00299C08
	private void OnCustomRewardSpellFinished(Spell spell, object userData)
	{
		Log.Spells.PrintDebug("QuestCompleteSpell.OnCustomRewardSpellFinished()", Array.Empty<object>());
		Card card = this.m_questReward.GetCard();
		card.SetDoNotSort(false);
		card.SetDoNotWarpToNewZone(false);
		card.GetZone().UpdateLayout();
		if (card.GetZone() is ZoneHeroPower)
		{
			card.DisableHeroPowerFlipSoundOnce();
			card.ActivateStateSpells(false);
		}
		this.OnSpellFinished();
	}

	// Token: 0x0600805E RID: 32862 RVA: 0x0029BA6E File Offset: 0x00299C6E
	private void OnDefaultAnimationEvent(UnityEngine.Object obj)
	{
		this.m_AnimationEventDispatcher.UnregisterAnimationEventListener(new OnAnimationEvent(this.OnDefaultAnimationEvent));
		this.m_fakeQuestRewardActor.Show();
		this.m_fakeQuestActor.Hide();
		base.StartCoroutine(this.MoveRewardToHand());
	}

	// Token: 0x0600805F RID: 32863 RVA: 0x0029BAAA File Offset: 0x00299CAA
	private IEnumerator MoveRewardToHand()
	{
		yield return new WaitForSeconds(this.m_QuestRewardHoldTime);
		yield return this.WaitForRewardActor();
		Card questRewardCard = this.m_questReward.GetCard();
		if (questRewardCard.GetEntity().IsHidden())
		{
			yield return base.StartCoroutine(SpellUtils.FlipActorAndReplaceWithCard(this.m_fakeQuestRewardActor, questRewardCard, 0.5f));
		}
		else
		{
			TransformUtil.CopyWorld(questRewardCard, this.m_fakeQuestRewardActor);
			this.m_fakeQuestRewardActor.Hide();
		}
		ZoneTransitionStyle transitionStyle = (questRewardCard.GetControllerSide() == global::Player.Side.FRIENDLY) ? ZoneTransitionStyle.SLOW : ZoneTransitionStyle.NORMAL;
		questRewardCard.SetTransitionStyle(transitionStyle);
		questRewardCard.SetDoNotSort(false);
		questRewardCard.SetDoNotWarpToNewZone(false);
		questRewardCard.GetZone().UpdateLayout();
		this.OnSpellFinished();
		this.OnStateFinished();
		yield break;
	}

	// Token: 0x04006944 RID: 26948
	[Header("Quest card animation settings")]
	public float m_QuestCardScaleTime = 1f;

	// Token: 0x04006945 RID: 26949
	public float m_QuestCardHoldTime = 1f;

	// Token: 0x04006946 RID: 26950
	public Transform m_QuestStartBone;

	// Token: 0x04006947 RID: 26951
	public Transform m_OpponentQuestStartBone;

	// Token: 0x04006948 RID: 26952
	public Transform m_QuestEndBone;

	// Token: 0x04006949 RID: 26953
	[Header("Quest reward - Default animation settings")]
	public float m_QuestRewardHoldTime = 1f;

	// Token: 0x0400694A RID: 26954
	public AnimationEventDispatcher m_AnimationEventDispatcher;

	// Token: 0x0400694B RID: 26955
	public Transform m_QuestRewardBone;

	// Token: 0x0400694C RID: 26956
	[Header("Quest reward - Custom animation settings")]
	public Spell m_CustomRewardSpellPrefab;

	// Token: 0x0400694D RID: 26957
	private global::Entity m_originalQuestEntity;

	// Token: 0x0400694E RID: 26958
	private Actor m_fakeQuestActor;

	// Token: 0x0400694F RID: 26959
	private Actor m_fakeQuestRewardActor;

	// Token: 0x04006950 RID: 26960
	private global::Entity m_questReward;

	// Token: 0x04006951 RID: 26961
	private int m_questRewardSpawnTaskIndex;
}
