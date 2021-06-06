using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000ED6 RID: 3798
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Check the current State name on a specified layer, this is more than the layer name, it holds the current state as well.")]
	public class GetAnimatorCurrentStateInfoIsName : FsmStateActionAnimatorBase
	{
		// Token: 0x0600AAA6 RID: 43686 RVA: 0x0035652E File Offset: 0x0035472E
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.layerIndex = null;
			this.name = null;
			this.nameMatchEvent = null;
			this.nameDoNotMatchEvent = null;
			this.everyFrame = false;
		}

		// Token: 0x0600AAA7 RID: 43687 RVA: 0x00356560 File Offset: 0x00354760
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
			this.IsName();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AAA8 RID: 43688 RVA: 0x003565C4 File Offset: 0x003547C4
		public override void OnActionUpdate()
		{
			this.IsName();
		}

		// Token: 0x0600AAA9 RID: 43689 RVA: 0x003565CC File Offset: 0x003547CC
		private void IsName()
		{
			if (this._animator != null)
			{
				AnimatorStateInfo currentAnimatorStateInfo = this._animator.GetCurrentAnimatorStateInfo(this.layerIndex.Value);
				if (!this.isMatching.IsNone)
				{
					this.isMatching.Value = currentAnimatorStateInfo.IsName(this.name.Value);
				}
				if (currentAnimatorStateInfo.IsName(this.name.Value))
				{
					base.Fsm.Event(this.nameMatchEvent);
					return;
				}
				base.Fsm.Event(this.nameDoNotMatchEvent);
			}
		}

		// Token: 0x04009159 RID: 37209
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component and a PlayMakerAnimatorProxy component are required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400915A RID: 37210
		[RequiredField]
		[Tooltip("The layer's index")]
		public FsmInt layerIndex;

		// Token: 0x0400915B RID: 37211
		[Tooltip("The name to check the layer against.")]
		public FsmString name;

		// Token: 0x0400915C RID: 37212
		[ActionSection("Results")]
		[UIHint(UIHint.Variable)]
		[Tooltip("True if name matches")]
		public FsmBool isMatching;

		// Token: 0x0400915D RID: 37213
		[Tooltip("Event send if name matches")]
		public FsmEvent nameMatchEvent;

		// Token: 0x0400915E RID: 37214
		[Tooltip("Event send if name doesn't match")]
		public FsmEvent nameDoNotMatchEvent;

		// Token: 0x0400915F RID: 37215
		private Animator _animator;
	}
}
