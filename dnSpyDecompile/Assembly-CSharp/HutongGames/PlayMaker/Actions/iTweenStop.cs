using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CE9 RID: 3305
	[ActionCategory("iTween")]
	[Tooltip("Stop an iTween action.")]
	public class iTweenStop : FsmStateAction
	{
		// Token: 0x0600A179 RID: 41337 RVA: 0x00337B12 File Offset: 0x00335D12
		public override void Reset()
		{
			this.id = new FsmString
			{
				UseVariable = true
			};
			this.iTweenType = iTweenFSMType.all;
			this.includeChildren = false;
			this.inScene = false;
		}

		// Token: 0x0600A17A RID: 41338 RVA: 0x00337B3B File Offset: 0x00335D3B
		public override void OnEnter()
		{
			base.OnEnter();
			this.DoiTween();
			base.Finish();
		}

		// Token: 0x0600A17B RID: 41339 RVA: 0x00337B50 File Offset: 0x00335D50
		private void DoiTween()
		{
			if (!this.id.IsNone)
			{
				iTween.StopByName(this.id.Value);
				return;
			}
			if (this.iTweenType == iTweenFSMType.all)
			{
				iTween.Stop();
				return;
			}
			if (this.inScene)
			{
				iTween.Stop(Enum.GetName(typeof(iTweenFSMType), this.iTweenType));
				return;
			}
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			iTween.Stop(ownerDefaultTarget, Enum.GetName(typeof(iTweenFSMType), this.iTweenType), this.includeChildren);
		}

		// Token: 0x04008775 RID: 34677
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008776 RID: 34678
		public FsmString id;

		// Token: 0x04008777 RID: 34679
		public iTweenFSMType iTweenType;

		// Token: 0x04008778 RID: 34680
		public bool includeChildren;

		// Token: 0x04008779 RID: 34681
		public bool inScene;
	}
}
