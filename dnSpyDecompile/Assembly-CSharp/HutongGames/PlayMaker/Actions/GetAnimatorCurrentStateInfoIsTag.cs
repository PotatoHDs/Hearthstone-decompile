using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000ED7 RID: 3799
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Does tag match the tag of the active state in the statemachine")]
	public class GetAnimatorCurrentStateInfoIsTag : FsmStateActionAnimatorBase
	{
		// Token: 0x0600AAAB RID: 43691 RVA: 0x0035665F File Offset: 0x0035485F
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.layerIndex = null;
			this.tag = null;
			this.tagMatch = null;
			this.tagMatchEvent = null;
			this.tagDoNotMatchEvent = null;
			this.everyFrame = false;
		}

		// Token: 0x0600AAAC RID: 43692 RVA: 0x00356698 File Offset: 0x00354898
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
			this.IsTag();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AAAD RID: 43693 RVA: 0x003566FC File Offset: 0x003548FC
		public override void OnActionUpdate()
		{
			this.IsTag();
		}

		// Token: 0x0600AAAE RID: 43694 RVA: 0x00356704 File Offset: 0x00354904
		private void IsTag()
		{
			if (this._animator != null)
			{
				if (this._animator.GetCurrentAnimatorStateInfo(this.layerIndex.Value).IsTag(this.tag.Value))
				{
					this.tagMatch.Value = true;
					base.Fsm.Event(this.tagMatchEvent);
					return;
				}
				this.tagMatch.Value = false;
				base.Fsm.Event(this.tagDoNotMatchEvent);
			}
		}

		// Token: 0x04009160 RID: 37216
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009161 RID: 37217
		[RequiredField]
		[Tooltip("The layer's index")]
		public FsmInt layerIndex;

		// Token: 0x04009162 RID: 37218
		[Tooltip("The tag to check the layer against.")]
		public FsmString tag;

		// Token: 0x04009163 RID: 37219
		[ActionSection("Results")]
		[UIHint(UIHint.Variable)]
		[Tooltip("True if tag matches")]
		public FsmBool tagMatch;

		// Token: 0x04009164 RID: 37220
		[Tooltip("Event send if tag matches")]
		public FsmEvent tagMatchEvent;

		// Token: 0x04009165 RID: 37221
		[Tooltip("Event send if tag matches")]
		public FsmEvent tagDoNotMatchEvent;

		// Token: 0x04009166 RID: 37222
		private Animator _animator;
	}
}
