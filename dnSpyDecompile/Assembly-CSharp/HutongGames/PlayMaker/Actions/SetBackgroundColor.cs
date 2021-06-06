using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DAD RID: 3501
	[ActionCategory(ActionCategory.Camera)]
	[Tooltip("Sets the Background Color used by the Camera.")]
	public class SetBackgroundColor : ComponentAction<Camera>
	{
		// Token: 0x0600A558 RID: 42328 RVA: 0x00346727 File Offset: 0x00344927
		public override void Reset()
		{
			this.gameObject = null;
			this.backgroundColor = Color.black;
			this.everyFrame = false;
		}

		// Token: 0x0600A559 RID: 42329 RVA: 0x00346747 File Offset: 0x00344947
		public override void OnEnter()
		{
			this.DoSetBackgroundColor();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A55A RID: 42330 RVA: 0x0034675D File Offset: 0x0034495D
		public override void OnUpdate()
		{
			this.DoSetBackgroundColor();
		}

		// Token: 0x0600A55B RID: 42331 RVA: 0x00346768 File Offset: 0x00344968
		private void DoSetBackgroundColor()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				base.camera.backgroundColor = this.backgroundColor.Value;
			}
		}

		// Token: 0x04008BE6 RID: 35814
		[RequiredField]
		[CheckForComponent(typeof(Camera))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008BE7 RID: 35815
		[RequiredField]
		public FsmColor backgroundColor;

		// Token: 0x04008BE8 RID: 35816
		public bool everyFrame;
	}
}
