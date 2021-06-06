using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F6F RID: 3951
	[ActionCategory("Pegasus")]
	[Tooltip("Show or Hide an Actor without messing up the game.")]
	public class SetActorLightBlend : FsmStateAction
	{
		// Token: 0x0600AD3F RID: 44351 RVA: 0x003605FF File Offset: 0x0035E7FF
		public override void Reset()
		{
			this.m_ActorObject = null;
			this.m_BlendValue = 1f;
			this.m_EveryFrame = false;
			this.m_actor = null;
		}

		// Token: 0x0600AD40 RID: 44352 RVA: 0x00360628 File Offset: 0x0035E828
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.m_actor == null && this.FindActor())
			{
				this.m_actor.SetLightBlend(this.m_BlendValue.Value, false);
			}
			if (!this.m_EveryFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AD41 RID: 44353 RVA: 0x00360676 File Offset: 0x0035E876
		public override void OnUpdate()
		{
			if (this.m_actor == null)
			{
				this.FindActor();
			}
			if (this.m_actor != null)
			{
				this.m_actor.SetLightBlend(this.m_BlendValue.Value, false);
			}
		}

		// Token: 0x0600AD42 RID: 44354 RVA: 0x003606B4 File Offset: 0x0035E8B4
		private bool FindActor()
		{
			if (this.m_ActorObject.IsNone || this.m_ActorObject.Value == null)
			{
				return false;
			}
			GameObject value = this.m_ActorObject.Value;
			if (value != null)
			{
				this.m_actor = value.GetComponentInChildren<Actor>();
				if (this.m_actor == null)
				{
					this.m_actor = SceneUtils.FindComponentInThisOrParents<Actor>(value);
					if (this.m_actor == null)
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x04009420 RID: 37920
		public FsmGameObject m_ActorObject;

		// Token: 0x04009421 RID: 37921
		[Tooltip("Light Blend Value")]
		public FsmFloat m_BlendValue;

		// Token: 0x04009422 RID: 37922
		[Tooltip("Update Every Frame")]
		public bool m_EveryFrame;

		// Token: 0x04009423 RID: 37923
		protected float m_initialLightBlendValue;

		// Token: 0x04009424 RID: 37924
		protected Actor m_actor;
	}
}
