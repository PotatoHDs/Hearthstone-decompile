using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D00 RID: 3328
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Adds a 2d torque (rotational force) to a Game Object.")]
	public class AddTorque2d : ComponentAction<Rigidbody2D>
	{
		// Token: 0x0600A1F1 RID: 41457 RVA: 0x003201AC File Offset: 0x0031E3AC
		public override void OnPreprocess()
		{
			base.Fsm.HandleFixedUpdate = true;
		}

		// Token: 0x0600A1F2 RID: 41458 RVA: 0x0033997A File Offset: 0x00337B7A
		public override void Reset()
		{
			this.gameObject = null;
			this.torque = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A1F3 RID: 41459 RVA: 0x00339991 File Offset: 0x00337B91
		public override void OnEnter()
		{
			this.DoAddTorque();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A1F4 RID: 41460 RVA: 0x003399A7 File Offset: 0x00337BA7
		public override void OnFixedUpdate()
		{
			this.DoAddTorque();
		}

		// Token: 0x0600A1F5 RID: 41461 RVA: 0x003399B0 File Offset: 0x00337BB0
		private void DoAddTorque()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (!base.UpdateCache(ownerDefaultTarget))
			{
				return;
			}
			base.rigidbody2d.AddTorque(this.torque.Value, this.forceMode);
		}

		// Token: 0x04008802 RID: 34818
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		[Tooltip("The GameObject to add torque to.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008803 RID: 34819
		[Tooltip("Option for applying the force")]
		public ForceMode2D forceMode;

		// Token: 0x04008804 RID: 34820
		[Tooltip("Torque")]
		public FsmFloat torque;

		// Token: 0x04008805 RID: 34821
		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;
	}
}
