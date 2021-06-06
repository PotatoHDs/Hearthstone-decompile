using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EE6 RID: 3814
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Returns the Animator controller layer count")]
	public class GetAnimatorLayerCount : FsmStateAction
	{
		// Token: 0x0600AAF2 RID: 43762 RVA: 0x003574F9 File Offset: 0x003556F9
		public override void Reset()
		{
			this.gameObject = null;
			this.layerCount = null;
		}

		// Token: 0x0600AAF3 RID: 43763 RVA: 0x0035750C File Offset: 0x0035570C
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				base.Finish();
				return;
			}
			this._animator = ownerDefaultTarget.GetComponent<Animator>();
			if (this._animator == null)
			{
				base.Finish();
				return;
			}
			this.DoGetLayerCount();
			base.Finish();
		}

		// Token: 0x0600AAF4 RID: 43764 RVA: 0x00357568 File Offset: 0x00355768
		private void DoGetLayerCount()
		{
			if (this._animator == null)
			{
				return;
			}
			this.layerCount.Value = this._animator.layerCount;
		}

		// Token: 0x040091B3 RID: 37299
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040091B4 RID: 37300
		[ActionSection("Results")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Animator controller layer count")]
		public FsmInt layerCount;

		// Token: 0x040091B5 RID: 37301
		private Animator _animator;
	}
}
