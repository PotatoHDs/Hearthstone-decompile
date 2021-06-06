using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F0D RID: 3853
	[ActionCategory("Pegasus")]
	[Tooltip("Show or Hide an Actor without messing up the game.")]
	public class ActorSetVisibility : ActorAction
	{
		// Token: 0x0600ABAA RID: 43946 RVA: 0x003597A2 File Offset: 0x003579A2
		protected override GameObject GetActorOwner()
		{
			return this.m_ActorObject.Value;
		}

		// Token: 0x0600ABAB RID: 43947 RVA: 0x003597AF File Offset: 0x003579AF
		public override void Reset()
		{
			this.m_ActorObject = null;
			this.m_Visible = false;
			this.m_IgnoreSpells = false;
			this.m_ResetOnExit = false;
		}

		// Token: 0x0600ABAC RID: 43948 RVA: 0x003597D8 File Offset: 0x003579D8
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.m_actor == null)
			{
				base.Finish();
				return;
			}
			this.m_initialVisibility = this.m_actor.IsShown();
			if (this.m_Visible.Value)
			{
				this.ShowActor();
			}
			else
			{
				this.HideActor();
			}
			base.Finish();
		}

		// Token: 0x0600ABAD RID: 43949 RVA: 0x00359832 File Offset: 0x00357A32
		public override void OnExit()
		{
			if (!this.m_ResetOnExit)
			{
				return;
			}
			if (this.m_initialVisibility)
			{
				this.ShowActor();
				return;
			}
			this.HideActor();
		}

		// Token: 0x0600ABAE RID: 43950 RVA: 0x00359852 File Offset: 0x00357A52
		public void ShowActor()
		{
			this.m_actor.Show(this.m_IgnoreSpells.Value);
		}

		// Token: 0x0600ABAF RID: 43951 RVA: 0x0035986A File Offset: 0x00357A6A
		public void HideActor()
		{
			this.m_actor.Hide(this.m_IgnoreSpells.Value);
		}

		// Token: 0x04009263 RID: 37475
		public FsmGameObject m_ActorObject;

		// Token: 0x04009264 RID: 37476
		[Tooltip("Should the Actor be set to visible or invisible?")]
		public FsmBool m_Visible;

		// Token: 0x04009265 RID: 37477
		[Tooltip("Don't touch the Actor's SpellTable when setting visibility")]
		public FsmBool m_IgnoreSpells;

		// Token: 0x04009266 RID: 37478
		[Tooltip("Resets to the initial visibility once\nit leaves the state")]
		public bool m_ResetOnExit;

		// Token: 0x04009267 RID: 37479
		protected bool m_initialVisibility;
	}
}
