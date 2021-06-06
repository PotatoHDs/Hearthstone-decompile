using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CDB RID: 3291
	[ActionCategory("iTween")]
	[Tooltip("Resume an iTween action.")]
	public class iTweenResume : FsmStateAction
	{
		// Token: 0x0600A132 RID: 41266 RVA: 0x0033579A File Offset: 0x0033399A
		public override void Reset()
		{
			this.iTweenType = iTweenFSMType.all;
			this.includeChildren = false;
			this.inScene = false;
		}

		// Token: 0x0600A133 RID: 41267 RVA: 0x003357B1 File Offset: 0x003339B1
		public override void OnEnter()
		{
			base.OnEnter();
			this.DoiTween();
			base.Finish();
		}

		// Token: 0x0600A134 RID: 41268 RVA: 0x003357C8 File Offset: 0x003339C8
		private void DoiTween()
		{
			if (this.iTweenType == iTweenFSMType.all)
			{
				iTween.Resume();
				return;
			}
			if (this.inScene)
			{
				iTween.Resume(Enum.GetName(typeof(iTweenFSMType), this.iTweenType));
				return;
			}
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			iTween.Resume(ownerDefaultTarget, Enum.GetName(typeof(iTweenFSMType), this.iTweenType), this.includeChildren);
		}

		// Token: 0x04008707 RID: 34567
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008708 RID: 34568
		public iTweenFSMType iTweenType;

		// Token: 0x04008709 RID: 34569
		public bool includeChildren;

		// Token: 0x0400870A RID: 34570
		public bool inScene;
	}
}
