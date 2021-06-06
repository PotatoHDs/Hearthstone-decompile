using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CD7 RID: 3287
	[ActionCategory("iTween")]
	[Tooltip("Pause an iTween action.")]
	public class iTweenPause : FsmStateAction
	{
		// Token: 0x0600A11F RID: 41247 RVA: 0x00334FED File Offset: 0x003331ED
		public override void Reset()
		{
			this.iTweenType = iTweenFSMType.all;
			this.includeChildren = false;
			this.inScene = false;
		}

		// Token: 0x0600A120 RID: 41248 RVA: 0x00335004 File Offset: 0x00333204
		public override void OnEnter()
		{
			base.OnEnter();
			this.DoiTween();
			base.Finish();
		}

		// Token: 0x0600A121 RID: 41249 RVA: 0x00335018 File Offset: 0x00333218
		private void DoiTween()
		{
			if (this.iTweenType == iTweenFSMType.all)
			{
				iTween.Pause();
				return;
			}
			if (this.inScene)
			{
				iTween.Pause(Enum.GetName(typeof(iTweenFSMType), this.iTweenType));
				return;
			}
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			iTween.Pause(ownerDefaultTarget, Enum.GetName(typeof(iTweenFSMType), this.iTweenType), this.includeChildren);
		}

		// Token: 0x040086EE RID: 34542
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x040086EF RID: 34543
		public iTweenFSMType iTweenType;

		// Token: 0x040086F0 RID: 34544
		public bool includeChildren;

		// Token: 0x040086F1 RID: 34545
		public bool inScene;
	}
}
