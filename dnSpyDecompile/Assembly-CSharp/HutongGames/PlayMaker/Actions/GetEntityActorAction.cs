using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F3B RID: 3899
	[ActionCategory("Pegasus")]
	[Tooltip("Get the Entity's game object from an Entity ID")]
	public class GetEntityActorAction : FsmStateAction
	{
		// Token: 0x0600AC76 RID: 44150 RVA: 0x0035D028 File Offset: 0x0035B228
		public override void Reset()
		{
			this.m_EntityActor = null;
		}

		// Token: 0x0600AC77 RID: 44151 RVA: 0x0035D034 File Offset: 0x0035B234
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.m_EntityId.IsNone)
			{
				global::Log.Spells.PrintError("{0}.OnEnter() - No entity ID was passed", new object[]
				{
					this
				});
				base.Finish();
				return;
			}
			if (this.m_EntityActor.IsNone)
			{
				global::Log.Spells.PrintError("{0}.OnEnter() - No variable hooked up to store Entity's actor!", new object[]
				{
					this
				});
				base.Finish();
				return;
			}
			Entity entity = GameState.Get().GetEntity(this.m_EntityId.Value);
			if (entity == null)
			{
				global::Log.Spells.PrintError("{0}.OnEnter() - FAILED to find entity with id {1}", new object[]
				{
					this,
					this.m_EntityId.Value
				});
				base.Finish();
				return;
			}
			Card card = entity.GetCard();
			if (card == null)
			{
				global::Log.Spells.PrintError("{0}.OnEnter() - FAILED to find card with entity id {1}", new object[]
				{
					this,
					this.m_EntityId.Value
				});
				base.Finish();
				return;
			}
			Actor actor = card.GetActor();
			if (actor == null)
			{
				global::Log.Spells.PrintError("{0}.OnEnter() - FAILED to find actor with entity id {1}", new object[]
				{
					this,
					this.m_EntityId.Value
				});
				base.Finish();
				return;
			}
			this.m_EntityActor.Value = actor.gameObject;
			base.Finish();
		}

		// Token: 0x0400934C RID: 37708
		public FsmInt m_EntityId;

		// Token: 0x0400934D RID: 37709
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Stores the Entity's actor in this output variable.")]
		public FsmGameObject m_EntityActor;
	}
}
