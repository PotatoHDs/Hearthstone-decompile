using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000ED9 RID: 3801
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Check the active Transition name on a specified layer. Format is 'CURRENT_STATE -> NEXT_STATE'.")]
	public class GetAnimatorCurrentTransitionInfoIsName : FsmStateActionAnimatorBase
	{
		// Token: 0x0600AAB5 RID: 43701 RVA: 0x003568EC File Offset: 0x00354AEC
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.layerIndex = null;
			this.name = null;
			this.nameMatch = null;
			this.nameMatchEvent = null;
			this.nameDoNotMatchEvent = null;
		}

		// Token: 0x0600AAB6 RID: 43702 RVA: 0x00356920 File Offset: 0x00354B20
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

		// Token: 0x0600AAB7 RID: 43703 RVA: 0x00356984 File Offset: 0x00354B84
		public override void OnActionUpdate()
		{
			this.IsName();
		}

		// Token: 0x0600AAB8 RID: 43704 RVA: 0x0035698C File Offset: 0x00354B8C
		private void IsName()
		{
			if (this._animator != null)
			{
				if (this._animator.GetAnimatorTransitionInfo(this.layerIndex.Value).IsName(this.name.Value))
				{
					this.nameMatch.Value = true;
					base.Fsm.Event(this.nameMatchEvent);
					return;
				}
				this.nameMatch.Value = false;
				base.Fsm.Event(this.nameDoNotMatchEvent);
			}
		}

		// Token: 0x0400916E RID: 37230
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400916F RID: 37231
		[RequiredField]
		[Tooltip("The layer's index")]
		public FsmInt layerIndex;

		// Token: 0x04009170 RID: 37232
		[Tooltip("The name to check the transition against.")]
		public FsmString name;

		// Token: 0x04009171 RID: 37233
		[ActionSection("Results")]
		[UIHint(UIHint.Variable)]
		[Tooltip("True if name matches")]
		public FsmBool nameMatch;

		// Token: 0x04009172 RID: 37234
		[Tooltip("Event send if name matches")]
		public FsmEvent nameMatchEvent;

		// Token: 0x04009173 RID: 37235
		[Tooltip("Event send if name doesn't match")]
		public FsmEvent nameDoNotMatchEvent;

		// Token: 0x04009174 RID: 37236
		private Animator _animator;
	}
}
