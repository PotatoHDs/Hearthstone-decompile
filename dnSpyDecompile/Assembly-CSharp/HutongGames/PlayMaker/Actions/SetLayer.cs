using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DDC RID: 3548
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Sets a Game Object's Layer.")]
	public class SetLayer : FsmStateAction
	{
		// Token: 0x0600A629 RID: 42537 RVA: 0x003488E2 File Offset: 0x00346AE2
		public override void Reset()
		{
			this.gameObject = null;
			this.layer = 0;
		}

		// Token: 0x0600A62A RID: 42538 RVA: 0x003488F2 File Offset: 0x00346AF2
		public override void OnEnter()
		{
			this.DoSetLayer();
			base.Finish();
		}

		// Token: 0x0600A62B RID: 42539 RVA: 0x00348900 File Offset: 0x00346B00
		private void DoSetLayer()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			ownerDefaultTarget.layer = this.layer;
		}

		// Token: 0x04008CBF RID: 36031
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008CC0 RID: 36032
		[UIHint(UIHint.Layer)]
		public int layer;
	}
}
