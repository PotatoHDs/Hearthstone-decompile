using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F85 RID: 3973
	[ActionCategory("Pegasus")]
	[Tooltip("Initialize a spell state, setting a variable that references one of the Actor's game objects by name.")]
	public class SpellCustomActorVariable : FsmStateAction
	{
		// Token: 0x0600ADAB RID: 44459 RVA: 0x00361F3F File Offset: 0x0036013F
		public override void Reset()
		{
			this.m_objectName = "";
			this.m_actorObject = null;
		}

		// Token: 0x0600ADAC RID: 44460 RVA: 0x00361F58 File Offset: 0x00360158
		public override void OnEnter()
		{
			if (!this.m_actorObject.IsNone)
			{
				Actor actor = SceneUtils.FindComponentInThisOrParents<Actor>(base.Owner);
				if (actor != null)
				{
					GameObject gameObject = SceneUtils.FindChildBySubstring(actor.gameObject, this.m_objectName.Value);
					if (gameObject == null)
					{
						Debug.LogWarning("Could not find object of name " + this.m_objectName + " in actor");
					}
					else
					{
						this.m_actorObject.Value = gameObject;
					}
				}
			}
			base.Finish();
		}

		// Token: 0x0600ADAD RID: 44461 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void OnUpdate()
		{
		}

		// Token: 0x0400948D RID: 38029
		public FsmString m_objectName;

		// Token: 0x0400948E RID: 38030
		public FsmGameObject m_actorObject;
	}
}
