using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F8D RID: 3981
	[ActionCategory("Pegasus")]
	[Tooltip("Initialize a spell state, setting variables that reference the parent actor and its contents.")]
	public class SpellInitActorVariables : FsmStateAction
	{
		// Token: 0x0600ADCA RID: 44490 RVA: 0x00362580 File Offset: 0x00360780
		protected Actor GetActor()
		{
			Actor actor = SceneUtils.FindComponentInThisOrParents<Actor>(base.Owner);
			if (actor == null)
			{
				Card card = SceneUtils.FindComponentInThisOrParents<Card>(base.Owner);
				if (card != null)
				{
					actor = card.GetActor();
				}
			}
			return actor;
		}

		// Token: 0x0600ADCB RID: 44491 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void Reset()
		{
		}

		// Token: 0x0600ADCC RID: 44492 RVA: 0x003625C0 File Offset: 0x003607C0
		public override void OnEnter()
		{
			Actor actor = this.GetActor();
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
			if (!this.m_rootObjectMesh.IsNone && actor.GetMeshRenderer(true) != null)
			{
				this.m_rootObjectMesh.Value = actor.GetMeshRenderer(true).gameObject;
			}
			base.Finish();
		}

		// Token: 0x040094A2 RID: 38050
		public FsmGameObject m_actorObject;

		// Token: 0x040094A3 RID: 38051
		public FsmGameObject m_rootObject;

		// Token: 0x040094A4 RID: 38052
		public FsmGameObject m_rootObjectMesh;
	}
}
