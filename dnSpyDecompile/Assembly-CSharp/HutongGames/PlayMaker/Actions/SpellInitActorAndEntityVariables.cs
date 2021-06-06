using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F8C RID: 3980
	[ActionCategory("Pegasus")]
	[Tooltip("Initialize a spell state, setting variables that reference the parent actor and its contents.")]
	public class SpellInitActorAndEntityVariables : SpellInitActorVariables
	{
		// Token: 0x0600ADC7 RID: 44487 RVA: 0x00362484 File Offset: 0x00360684
		public override void Reset()
		{
			base.Reset();
			if (this.m_entityID != null)
			{
				this.m_entityID.UseVariable = true;
			}
		}

		// Token: 0x0600ADC8 RID: 44488 RVA: 0x003624A0 File Offset: 0x003606A0
		public override void OnEnter()
		{
			Actor actor = base.GetActor();
			if (actor == null)
			{
				base.Finish();
				return;
			}
			GameObject gameObject = actor.gameObject;
			if (!this.m_actorObject.IsNone)
			{
				this.m_actorObject.Value = gameObject;
			}
			if (!this.m_rootObject.IsNone)
			{
				this.m_rootObject.Value = actor.GetRootObject();
			}
			if (!this.m_rootObjectMesh.IsNone)
			{
				this.m_rootObjectMesh.Value = actor.GetMeshRenderer(false).gameObject;
			}
			if (!this.m_entityID.IsNone)
			{
				if (actor.GetEntity() == null)
				{
					global::Log.Spells.PrintError("{0}.OnEnter(): An Entity ID FSM Variable was hooked up but actor {1} doesn't have an entity!", new object[]
					{
						this,
						actor
					});
				}
				else
				{
					this.m_entityID.Value = actor.GetEntity().GetEntityId();
				}
			}
			base.Finish();
		}

		// Token: 0x040094A1 RID: 38049
		[UIHint(UIHint.Variable)]
		public FsmInt m_entityID;
	}
}
