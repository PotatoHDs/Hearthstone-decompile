using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DE5 RID: 3557
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Sets the Mass of a Game Object's Rigid Body.")]
	public class SetMass : ComponentAction<Rigidbody>
	{
		// Token: 0x0600A650 RID: 42576 RVA: 0x00348CB4 File Offset: 0x00346EB4
		public override void Reset()
		{
			this.gameObject = null;
			this.mass = 1f;
		}

		// Token: 0x0600A651 RID: 42577 RVA: 0x00348CCD File Offset: 0x00346ECD
		public override void OnEnter()
		{
			this.DoSetMass();
			base.Finish();
		}

		// Token: 0x0600A652 RID: 42578 RVA: 0x00348CDC File Offset: 0x00346EDC
		private void DoSetMass()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				base.rigidbody.mass = this.mass.Value;
			}
		}

		// Token: 0x04008CD4 RID: 36052
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008CD5 RID: 36053
		[RequiredField]
		[HasFloatSlider(0.1f, 10f)]
		public FsmFloat mass;
	}
}
