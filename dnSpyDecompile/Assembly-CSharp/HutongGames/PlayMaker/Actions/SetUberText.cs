using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F79 RID: 3961
	[ActionCategory("Pegasus")]
	[Tooltip("Sets the text on an UberText object.")]
	public class SetUberText : FsmStateAction
	{
		// Token: 0x0600AD6E RID: 44398 RVA: 0x00360F22 File Offset: 0x0035F122
		public override void Reset()
		{
			this.uberTextObject = null;
			this.text = null;
			this.everyFrame = false;
		}

		// Token: 0x0600AD6F RID: 44399 RVA: 0x00360F39 File Offset: 0x0035F139
		public override void OnEnter()
		{
			this.UpdateText();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AD70 RID: 44400 RVA: 0x00360F4F File Offset: 0x0035F14F
		public override void OnUpdate()
		{
			this.UpdateText();
		}

		// Token: 0x0600AD71 RID: 44401 RVA: 0x00360F58 File Offset: 0x0035F158
		private void UpdateText()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.uberTextObject);
			if (ownerDefaultTarget != null)
			{
				UberText component = ownerDefaultTarget.GetComponent<UberText>();
				if (component != null)
				{
					component.Text = this.text.Value;
				}
			}
		}

		// Token: 0x0400944A RID: 37962
		[RequiredField]
		public FsmOwnerDefault uberTextObject;

		// Token: 0x0400944B RID: 37963
		public FsmString text;

		// Token: 0x0400944C RID: 37964
		[Tooltip("Set the UberText every frame. Useful if the text variable is expected to change/animate.")]
		public bool everyFrame;
	}
}
