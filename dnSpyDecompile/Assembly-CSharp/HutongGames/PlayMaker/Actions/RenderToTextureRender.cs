using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F69 RID: 3945
	[ActionCategory("Pegasus")]
	[Tooltip("Triggers a render to texture to render.")]
	public class RenderToTextureRender : FsmStateAction
	{
		// Token: 0x0600AD2B RID: 44331 RVA: 0x003602A6 File Offset: 0x0035E4A6
		public override void Reset()
		{
			this.r2tObject = null;
			this.now = false;
		}

		// Token: 0x0600AD2C RID: 44332 RVA: 0x003602B8 File Offset: 0x0035E4B8
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.r2tObject);
			if (ownerDefaultTarget != null)
			{
				RenderToTexture component = ownerDefaultTarget.GetComponent<RenderToTexture>();
				if (component != null)
				{
					if (this.now)
					{
						component.RenderNow();
					}
					else
					{
						component.Render();
					}
				}
			}
			base.Finish();
		}

		// Token: 0x04009412 RID: 37906
		[RequiredField]
		public FsmOwnerDefault r2tObject;

		// Token: 0x04009413 RID: 37907
		public bool now;
	}
}
