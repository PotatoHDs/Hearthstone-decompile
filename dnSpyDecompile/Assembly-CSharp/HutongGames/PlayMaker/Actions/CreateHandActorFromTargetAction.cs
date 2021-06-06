using System;
using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F2E RID: 3886
	[ActionCategory("Pegasus")]
	[Tooltip("Create a dummy hand actor from a spell target.")]
	public class CreateHandActorFromTargetAction : FsmStateAction
	{
		// Token: 0x0600AC3E RID: 44094 RVA: 0x0035BE32 File Offset: 0x0035A032
		public override void Reset()
		{
			this.m_OwnerObject = null;
			this.m_DummyActor = null;
		}

		// Token: 0x0600AC3F RID: 44095 RVA: 0x0035BE44 File Offset: 0x0035A044
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.m_OwnerObject);
			Spell spell = SceneUtils.FindComponentInThisOrParents<Spell>(ownerDefaultTarget);
			if (spell == null)
			{
				Error.AddDevWarning("CreateHandActorFromTargetAction", "Invalid owner spell. owner:{0}", new object[]
				{
					ownerDefaultTarget
				});
				this.FinishAction();
				return;
			}
			Card targetCard = spell.GetTargetCard();
			if (targetCard == null)
			{
				this.FinishAction();
				return;
			}
			Entity entity = targetCard.GetEntity();
			if (entity == null)
			{
				Error.AddDevWarning("CreateHandActorFromTargetAction", "Invalid target entity. card:{0}, spell:{1}", new object[]
				{
					targetCard,
					spell
				});
				this.FinishAction();
				return;
			}
			if (!entity.IsRevealed())
			{
				this.ParseShowEntityAndLoadDummyActor(spell, entity);
				return;
			}
			this.LoadDummyActor(entity);
		}

		// Token: 0x0600AC40 RID: 44096 RVA: 0x0035BEF0 File Offset: 0x0035A0F0
		private void ParseShowEntityAndLoadDummyActor(Spell spell, Entity entity)
		{
			PowerTaskList powerTaskList = spell.GetPowerTaskList();
			if (powerTaskList == null)
			{
				Error.AddDevWarning("CreateHandActorFromTargetAction", "Invalid PowerTaskList. entity:{0}, spell:{1}", new object[]
				{
					entity,
					spell
				});
				this.FinishAction();
				return;
			}
			Entity entity2 = new Entity();
			entity2.SetCardId(entity.GetCardId());
			foreach (KeyValuePair<int, int> keyValuePair in entity.GetTags().GetMap())
			{
				entity2.SetTag(keyValuePair.Key, keyValuePair.Value);
			}
			foreach (PowerTask powerTask in powerTaskList.GetTaskList())
			{
				Network.PowerHistory power = powerTask.GetPower();
				if (power.Type == Network.PowerType.SHOW_ENTITY)
				{
					Network.Entity entity3 = (power as Network.HistShowEntity).Entity;
					if (entity3.ID == entity.GetEntityId())
					{
						entity2.LoadCard(entity3.CardID, null);
						using (List<Network.Entity.Tag>.Enumerator enumerator3 = entity3.Tags.GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								Network.Entity.Tag tag = enumerator3.Current;
								entity2.SetTag(tag.Name, tag.Value);
							}
							break;
						}
					}
				}
			}
			this.LoadDummyActor(entity2);
		}

		// Token: 0x0600AC41 RID: 44097 RVA: 0x0035C06C File Offset: 0x0035A26C
		private void LoadDummyActor(Entity entity)
		{
			AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(entity.GetEntityDef(), entity.GetPremiumType()), new PrefabCallback<GameObject>(this.OnActorLoaded), entity, AssetLoadingOptions.IgnorePrefabPosition);
		}

		// Token: 0x0600AC42 RID: 44098 RVA: 0x0035C0A0 File Offset: 0x0035A2A0
		private void OnActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
		{
			Actor component = go.GetComponent<Actor>();
			Entity entity = (Entity)callbackData;
			component.SetEntity(entity);
			component.SetCardDefFromEntity(entity);
			component.SetCardBackSideOverride(new Player.Side?(entity.GetControllerSide()));
			component.UpdateAllComponents();
			Transform intialTransform = this.GetIntialTransform(entity.GetControllerSide());
			if (intialTransform != null)
			{
				TransformUtil.CopyWorld(component, intialTransform);
			}
			else
			{
				TransformUtil.Identity(component);
			}
			if (this.m_Show)
			{
				component.Show();
			}
			else
			{
				component.Hide();
			}
			this.m_DummyActor.Value = component.gameObject;
			this.FinishAction();
		}

		// Token: 0x0600AC43 RID: 44099 RVA: 0x0035C134 File Offset: 0x0035A334
		private Transform GetIntialTransform(Player.Side playerSide)
		{
			string text = (playerSide == Player.Side.FRIENDLY) ? this.m_FriendlyBoneName : this.m_OpponentBoneName;
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			if (UniversalInputManager.UsePhoneUI)
			{
				text += "_phone";
			}
			return Board.Get().FindBone(text);
		}

		// Token: 0x0600AC44 RID: 44100 RVA: 0x0035C181 File Offset: 0x0035A381
		private void FinishAction()
		{
			if (this.m_FinishEvent != null)
			{
				base.Fsm.Event(this.m_FinishEvent);
			}
			base.Finish();
		}

		// Token: 0x04009312 RID: 37650
		[RequiredField]
		[Tooltip("GameObject(Spell) to retrieve a target info.")]
		public FsmOwnerDefault m_OwnerObject;

		// Token: 0x04009313 RID: 37651
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Variable to store the newly created dummy actor.")]
		public FsmGameObject m_DummyActor;

		// Token: 0x04009314 RID: 37652
		[Tooltip("Bone name for setting its initial transform. (Friendly side)")]
		public string m_FriendlyBoneName = "FriendlyJoust";

		// Token: 0x04009315 RID: 37653
		[Tooltip("Bone name for setting its initial transform. (Opponent Side)")]
		public string m_OpponentBoneName = "OpponentJoust";

		// Token: 0x04009316 RID: 37654
		[Tooltip("Whether it appears or not.")]
		public bool m_Show;

		// Token: 0x04009317 RID: 37655
		[Tooltip("FSM event to fire after actor finishes loading")]
		public FsmEvent m_FinishEvent;
	}
}
