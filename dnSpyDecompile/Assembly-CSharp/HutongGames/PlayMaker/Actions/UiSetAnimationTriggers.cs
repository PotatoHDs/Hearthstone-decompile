using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E56 RID: 3670
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the Animation Triggers of a UI Selectable component. Modifications will not be visible if transition is not Animation")]
	public class UiSetAnimationTriggers : ComponentAction<Selectable>
	{
		// Token: 0x0600A850 RID: 43088 RVA: 0x0034F7A4 File Offset: 0x0034D9A4
		public override void Reset()
		{
			this.gameObject = null;
			this.normalTrigger = new FsmString
			{
				UseVariable = true
			};
			this.highlightedTrigger = new FsmString
			{
				UseVariable = true
			};
			this.pressedTrigger = new FsmString
			{
				UseVariable = true
			};
			this.disabledTrigger = new FsmString
			{
				UseVariable = true
			};
			this.resetOnExit = null;
		}

		// Token: 0x0600A851 RID: 43089 RVA: 0x0034F808 File Offset: 0x0034DA08
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.selectable = this.cachedComponent;
			}
			if (this.selectable != null && this.resetOnExit.Value)
			{
				this.originalAnimationTriggers = this.selectable.animationTriggers;
			}
			this.DoSetValue();
			base.Finish();
		}

		// Token: 0x0600A852 RID: 43090 RVA: 0x0034F874 File Offset: 0x0034DA74
		private void DoSetValue()
		{
			if (this.selectable == null)
			{
				return;
			}
			this._animationTriggers = this.selectable.animationTriggers;
			if (!this.normalTrigger.IsNone)
			{
				this._animationTriggers.normalTrigger = this.normalTrigger.Value;
			}
			if (!this.highlightedTrigger.IsNone)
			{
				this._animationTriggers.highlightedTrigger = this.highlightedTrigger.Value;
			}
			if (!this.pressedTrigger.IsNone)
			{
				this._animationTriggers.pressedTrigger = this.pressedTrigger.Value;
			}
			if (!this.disabledTrigger.IsNone)
			{
				this._animationTriggers.disabledTrigger = this.disabledTrigger.Value;
			}
			this.selectable.animationTriggers = this._animationTriggers;
		}

		// Token: 0x0600A853 RID: 43091 RVA: 0x0034F93E File Offset: 0x0034DB3E
		public override void OnExit()
		{
			if (this.selectable == null)
			{
				return;
			}
			if (this.resetOnExit.Value)
			{
				this.selectable.animationTriggers = this.originalAnimationTriggers;
			}
		}

		// Token: 0x04008EDF RID: 36575
		[RequiredField]
		[CheckForComponent(typeof(Selectable))]
		[Tooltip("The GameObject with the UI Selectable component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008EE0 RID: 36576
		[Tooltip("The normal trigger value. Leave as None for no effect")]
		public FsmString normalTrigger;

		// Token: 0x04008EE1 RID: 36577
		[Tooltip("The highlighted trigger value. Leave as None for no effect")]
		public FsmString highlightedTrigger;

		// Token: 0x04008EE2 RID: 36578
		[Tooltip("The pressed trigger value. Leave as None for no effect")]
		public FsmString pressedTrigger;

		// Token: 0x04008EE3 RID: 36579
		[Tooltip("The disabled trigger value. Leave as None for no effect")]
		public FsmString disabledTrigger;

		// Token: 0x04008EE4 RID: 36580
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		// Token: 0x04008EE5 RID: 36581
		private Selectable selectable;

		// Token: 0x04008EE6 RID: 36582
		private AnimationTriggers _animationTriggers;

		// Token: 0x04008EE7 RID: 36583
		private AnimationTriggers originalAnimationTriggers;
	}
}
