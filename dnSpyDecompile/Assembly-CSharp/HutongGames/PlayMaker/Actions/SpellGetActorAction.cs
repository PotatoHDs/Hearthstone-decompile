using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F88 RID: 3976
	[ActionCategory("Pegasus")]
	[Tooltip("Put a Spell's Source or Target Actor into a GameObject variable.")]
	public class SpellGetActorAction : SpellAction
	{
		// Token: 0x0600ADB8 RID: 44472 RVA: 0x00362186 File Offset: 0x00360386
		protected override GameObject GetSpellOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(this.m_SpellObject);
		}

		// Token: 0x0600ADB9 RID: 44473 RVA: 0x00362199 File Offset: 0x00360399
		public override void Reset()
		{
			this.m_SpellObject = null;
			this.m_WhichActor = SpellAction.Which.SOURCE;
			this.m_GameObject = null;
		}

		// Token: 0x0600ADBA RID: 44474 RVA: 0x003621B0 File Offset: 0x003603B0
		public override void OnEnter()
		{
			base.OnEnter();
			Actor actor = base.GetActor(this.m_WhichActor);
			if (actor == null)
			{
				Error.AddDevFatal("SpellGetActorAction.OnEnter() - Actor not found!", Array.Empty<object>());
				base.Finish();
				return;
			}
			if (!this.m_GameObject.IsNone)
			{
				this.m_GameObject.Value = actor.gameObject;
			}
			base.Finish();
		}

		// Token: 0x04009495 RID: 38037
		public FsmOwnerDefault m_SpellObject;

		// Token: 0x04009496 RID: 38038
		public SpellAction.Which m_WhichActor;

		// Token: 0x04009497 RID: 38039
		public FsmGameObject m_GameObject;
	}
}
