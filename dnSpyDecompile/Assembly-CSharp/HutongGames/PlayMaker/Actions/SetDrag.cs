using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DB3 RID: 3507
	[ActionCategory(ActionCategory.Physics)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=4734.0")]
	[Tooltip("Sets the Drag of a Game Object's Rigid Body.")]
	public class SetDrag : ComponentAction<Rigidbody>
	{
		// Token: 0x0600A575 RID: 42357 RVA: 0x00346A8F File Offset: 0x00344C8F
		public override void Reset()
		{
			this.gameObject = null;
			this.drag = 1f;
		}

		// Token: 0x0600A576 RID: 42358 RVA: 0x00346AA8 File Offset: 0x00344CA8
		public override void OnEnter()
		{
			this.DoSetDrag();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A577 RID: 42359 RVA: 0x00346ABE File Offset: 0x00344CBE
		public override void OnUpdate()
		{
			this.DoSetDrag();
		}

		// Token: 0x0600A578 RID: 42360 RVA: 0x00346AC8 File Offset: 0x00344CC8
		private void DoSetDrag()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				base.rigidbody.drag = this.drag.Value;
			}
		}

		// Token: 0x04008BFC RID: 35836
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008BFD RID: 35837
		[RequiredField]
		[HasFloatSlider(0f, 10f)]
		public FsmFloat drag;

		// Token: 0x04008BFE RID: 35838
		[Tooltip("Repeat every frame. Typically this would be set to True.")]
		public bool everyFrame;
	}
}
