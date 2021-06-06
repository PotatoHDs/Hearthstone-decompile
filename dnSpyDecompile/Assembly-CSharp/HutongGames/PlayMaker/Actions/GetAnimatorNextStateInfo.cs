using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EEB RID: 3819
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Gets the next State information on a specified layer")]
	public class GetAnimatorNextStateInfo : FsmStateActionAnimatorBase
	{
		// Token: 0x0600AB09 RID: 43785 RVA: 0x00357870 File Offset: 0x00355A70
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.layerIndex = null;
			this.name = null;
			this.nameHash = null;
			this.fullPathHash = null;
			this.shortPathHash = null;
			this.tagHash = null;
			this.length = null;
			this.normalizedTime = null;
			this.isStateLooping = null;
			this.loopCount = null;
			this.currentLoopProgress = null;
		}

		// Token: 0x0600AB0A RID: 43786 RVA: 0x003578D8 File Offset: 0x00355AD8
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
			this.GetLayerInfo();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AB0B RID: 43787 RVA: 0x0035793C File Offset: 0x00355B3C
		public override void OnActionUpdate()
		{
			this.GetLayerInfo();
		}

		// Token: 0x0600AB0C RID: 43788 RVA: 0x00357944 File Offset: 0x00355B44
		private void GetLayerInfo()
		{
			if (this._animator != null)
			{
				AnimatorStateInfo nextAnimatorStateInfo = this._animator.GetNextAnimatorStateInfo(this.layerIndex.Value);
				if (!this.fullPathHash.IsNone)
				{
					this.fullPathHash.Value = nextAnimatorStateInfo.fullPathHash;
				}
				if (!this.shortPathHash.IsNone)
				{
					this.shortPathHash.Value = nextAnimatorStateInfo.shortNameHash;
				}
				if (!this.nameHash.IsNone)
				{
					this.nameHash.Value = nextAnimatorStateInfo.shortNameHash;
				}
				if (!this.name.IsNone)
				{
					this.name.Value = this._animator.GetLayerName(this.layerIndex.Value);
				}
				if (!this.tagHash.IsNone)
				{
					this.tagHash.Value = nextAnimatorStateInfo.tagHash;
				}
				if (!this.length.IsNone)
				{
					this.length.Value = nextAnimatorStateInfo.length;
				}
				if (!this.isStateLooping.IsNone)
				{
					this.isStateLooping.Value = nextAnimatorStateInfo.loop;
				}
				if (!this.normalizedTime.IsNone)
				{
					this.normalizedTime.Value = nextAnimatorStateInfo.normalizedTime;
				}
				if (!this.loopCount.IsNone || !this.currentLoopProgress.IsNone)
				{
					this.loopCount.Value = (int)Math.Truncate((double)nextAnimatorStateInfo.normalizedTime);
					this.currentLoopProgress.Value = nextAnimatorStateInfo.normalizedTime - (float)this.loopCount.Value;
				}
			}
		}

		// Token: 0x040091C7 RID: 37319
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040091C8 RID: 37320
		[RequiredField]
		[Tooltip("The layer's index")]
		public FsmInt layerIndex;

		// Token: 0x040091C9 RID: 37321
		[ActionSection("Results")]
		[UIHint(UIHint.Variable)]
		[Tooltip("The layer's name.")]
		public FsmString name;

		// Token: 0x040091CA RID: 37322
		[UIHint(UIHint.Variable)]
		[Tooltip("The layer's name Hash. Obsolete in Unity 5, use fullPathHash or shortPathHash instead, nameHash will be the same as shortNameHash for legacy")]
		public FsmInt nameHash;

		// Token: 0x040091CB RID: 37323
		[UIHint(UIHint.Variable)]
		[Tooltip("The full path hash for this state.")]
		public FsmInt fullPathHash;

		// Token: 0x040091CC RID: 37324
		[UIHint(UIHint.Variable)]
		[Tooltip("The name Hash. Doest not include the parent layer's name")]
		public FsmInt shortPathHash;

		// Token: 0x040091CD RID: 37325
		[UIHint(UIHint.Variable)]
		[Tooltip("The layer's tag hash")]
		public FsmInt tagHash;

		// Token: 0x040091CE RID: 37326
		[UIHint(UIHint.Variable)]
		[Tooltip("Is the state looping. All animations in the state must be looping")]
		public FsmBool isStateLooping;

		// Token: 0x040091CF RID: 37327
		[UIHint(UIHint.Variable)]
		[Tooltip("The Current duration of the state. In seconds, can vary when the State contains a Blend Tree ")]
		public FsmFloat length;

		// Token: 0x040091D0 RID: 37328
		[UIHint(UIHint.Variable)]
		[Tooltip("The integer part is the number of time a state has been looped. The fractional part is the % (0-1) of progress in the current loop")]
		public FsmFloat normalizedTime;

		// Token: 0x040091D1 RID: 37329
		[UIHint(UIHint.Variable)]
		[Tooltip("The integer part is the number of time a state has been looped. This is extracted from the normalizedTime")]
		public FsmInt loopCount;

		// Token: 0x040091D2 RID: 37330
		[UIHint(UIHint.Variable)]
		[Tooltip("The progress in the current loop. This is extracted from the normalizedTime")]
		public FsmFloat currentLoopProgress;

		// Token: 0x040091D3 RID: 37331
		private Animator _animator;
	}
}
