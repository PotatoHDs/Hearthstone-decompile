using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Create a dummy hand actor from a spell target.")]
	public class CreateHandActorFromTargetAction : FsmStateAction
	{
		[RequiredField]
		[Tooltip("GameObject(Spell) to retrieve a target info.")]
		public FsmOwnerDefault m_OwnerObject;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Variable to store the newly created dummy actor.")]
		public FsmGameObject m_DummyActor;

		[Tooltip("Bone name for setting its initial transform. (Friendly side)")]
		public string m_FriendlyBoneName = "FriendlyJoust";

		[Tooltip("Bone name for setting its initial transform. (Opponent Side)")]
		public string m_OpponentBoneName = "OpponentJoust";

		[Tooltip("Whether it appears or not.")]
		public bool m_Show;

		[Tooltip("FSM event to fire after actor finishes loading")]
		public FsmEvent m_FinishEvent;

		public override void Reset()
		{
			m_OwnerObject = null;
			m_DummyActor = null;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(m_OwnerObject);
			Spell spell = SceneUtils.FindComponentInThisOrParents<Spell>(ownerDefaultTarget);
			if (spell == null)
			{
				Error.AddDevWarning("CreateHandActorFromTargetAction", "Invalid owner spell. owner:{0}", ownerDefaultTarget);
				FinishAction();
				return;
			}
			Card targetCard = spell.GetTargetCard();
			if (targetCard == null)
			{
				FinishAction();
				return;
			}
			Entity entity = targetCard.GetEntity();
			if (entity == null)
			{
				Error.AddDevWarning("CreateHandActorFromTargetAction", "Invalid target entity. card:{0}, spell:{1}", targetCard, spell);
				FinishAction();
			}
			else if (!entity.IsRevealed())
			{
				ParseShowEntityAndLoadDummyActor(spell, entity);
			}
			else
			{
				LoadDummyActor(entity);
			}
		}

		private void ParseShowEntityAndLoadDummyActor(Spell spell, Entity entity)
		{
			PowerTaskList powerTaskList = spell.GetPowerTaskList();
			if (powerTaskList == null)
			{
				Error.AddDevWarning("CreateHandActorFromTargetAction", "Invalid PowerTaskList. entity:{0}, spell:{1}", entity, spell);
				FinishAction();
				return;
			}
			Entity entity2 = new Entity();
			entity2.SetCardId(entity.GetCardId());
			foreach (KeyValuePair<int, int> item in entity.GetTags().GetMap())
			{
				entity2.SetTag(item.Key, item.Value);
			}
			foreach (PowerTask task in powerTaskList.GetTaskList())
			{
				Network.PowerHistory power = task.GetPower();
				if (power.Type != Network.PowerType.SHOW_ENTITY)
				{
					continue;
				}
				Network.Entity entity3 = (power as Network.HistShowEntity).Entity;
				if (entity3.ID != entity.GetEntityId())
				{
					continue;
				}
				entity2.LoadCard(entity3.CardID);
				foreach (Network.Entity.Tag tag in entity3.Tags)
				{
					entity2.SetTag(tag.Name, tag.Value);
				}
				break;
			}
			LoadDummyActor(entity2);
		}

		private void LoadDummyActor(Entity entity)
		{
			AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(entity.GetEntityDef(), entity.GetPremiumType()), OnActorLoaded, entity, AssetLoadingOptions.IgnorePrefabPosition);
		}

		private void OnActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
		{
			Actor component = go.GetComponent<Actor>();
			Entity entity = (Entity)callbackData;
			component.SetEntity(entity);
			component.SetCardDefFromEntity(entity);
			component.SetCardBackSideOverride(entity.GetControllerSide());
			component.UpdateAllComponents();
			Transform intialTransform = GetIntialTransform(entity.GetControllerSide());
			if (intialTransform != null)
			{
				TransformUtil.CopyWorld(component, intialTransform);
			}
			else
			{
				TransformUtil.Identity(component);
			}
			if (m_Show)
			{
				component.Show();
			}
			else
			{
				component.Hide();
			}
			m_DummyActor.Value = component.gameObject;
			FinishAction();
		}

		private Transform GetIntialTransform(Player.Side playerSide)
		{
			string text = ((playerSide == Player.Side.FRIENDLY) ? m_FriendlyBoneName : m_OpponentBoneName);
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				text += "_phone";
			}
			return Board.Get().FindBone(text);
		}

		private void FinishAction()
		{
			if (m_FinishEvent != null)
			{
				base.Fsm.Event(m_FinishEvent);
			}
			Finish();
		}
	}
}
