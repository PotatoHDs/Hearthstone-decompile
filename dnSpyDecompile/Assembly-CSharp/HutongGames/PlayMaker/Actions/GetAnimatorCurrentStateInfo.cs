using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000ED5 RID: 3797
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Gets the current State information on a specified layer")]
	public class GetAnimatorCurrentStateInfo : FsmStateActionAnimatorBase
	{
		// Token: 0x0600AAA1 RID: 43681 RVA: 0x003562C4 File Offset: 0x003544C4
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
			this.everyFrame = false;
		}

		// Token: 0x0600AAA2 RID: 43682 RVA: 0x00356334 File Offset: 0x00354534
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

		// Token: 0x0600AAA3 RID: 43683 RVA: 0x00356398 File Offset: 0x00354598
		public override void OnActionUpdate()
		{
			this.GetLayerInfo();
		}

		// Token: 0x0600AAA4 RID: 43684 RVA: 0x003563A0 File Offset: 0x003545A0
		private void GetLayerInfo()
		{
			if (this._animator != null)
			{
				AnimatorStateInfo currentAnimatorStateInfo = this._animator.GetCurrentAnimatorStateInfo(this.layerIndex.Value);
				if (!this.fullPathHash.IsNone)
				{
					this.fullPathHash.Value = currentAnimatorStateInfo.fullPathHash;
				}
				if (!this.shortPathHash.IsNone)
				{
					this.shortPathHash.Value = currentAnimatorStateInfo.shortNameHash;
				}
				if (!this.nameHash.IsNone)
				{
					this.nameHash.Value = currentAnimatorStateInfo.shortNameHash;
				}
				if (!this.name.IsNone)
				{
					this.name.Value = this._animator.GetLayerName(this.layerIndex.Value);
				}
				if (!this.tagHash.IsNone)
				{
					this.tagHash.Value = currentAnimatorStateInfo.tagHash;
				}
				if (!this.length.IsNone)
				{
					this.length.Value = currentAnimatorStateInfo.length;
				}
				if (!this.isStateLooping.IsNone)
				{
					this.isStateLooping.Value = currentAnimatorStateInfo.loop;
				}
				if (!this.normalizedTime.IsNone)
				{
					this.normalizedTime.Value = currentAnimatorStateInfo.normalizedTime;
				}
				if (!this.loopCount.IsNone || !this.currentLoopProgress.IsNone)
				{
					this.loopCount.Value = (int)Math.Truncate((double)currentAnimatorStateInfo.normalizedTime);
					this.currentLoopProgress.Value = currentAnimatorStateInfo.normalizedTime - (float)this.loopCount.Value;
				}
			}
		}

		// Token: 0x0400914C RID: 37196
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400914D RID: 37197
		[RequiredField]
		[Tooltip("The layer's index")]
		public FsmInt layerIndex;

		// Token: 0x0400914E RID: 37198
		[ActionSection("Results")]
		[UIHint(UIHint.Variable)]
		[Tooltip("The layer's name.")]
		public FsmString name;

		// Token: 0x0400914F RID: 37199
		[UIHint(UIHint.Variable)]
		[Tooltip("The layer's name Hash. Obsolete in Unity 5, use fullPathHash or shortPathHash instead, nameHash will be the same as shortNameHash for legacy")]
		public FsmInt nameHash;

		// Token: 0x04009150 RID: 37200
		[UIHint(UIHint.Variable)]
		[Tooltip("The full path hash for this state.")]
		public FsmInt fullPathHash;

		// Token: 0x04009151 RID: 37201
		[UIHint(UIHint.Variable)]
		[Tooltip("The name Hash. Doest not include the parent layer's name")]
		public FsmInt shortPathHash;

		// Token: 0x04009152 RID: 37202
		[UIHint(UIHint.Variable)]
		[Tooltip("The layer's tag hash")]
		public FsmInt tagHash;

		// Token: 0x04009153 RID: 37203
		[UIHint(UIHint.Variable)]
		[Tooltip("Is the state looping. All animations in the state must be looping")]
		public FsmBool isStateLooping;

		// Token: 0x04009154 RID: 37204
		[UIHint(UIHint.Variable)]
		[Tooltip("The Current duration of the state. In seconds, can vary when the State contains a Blend Tree ")]
		public FsmFloat length;

		// Token: 0x04009155 RID: 37205
		[UIHint(UIHint.Variable)]
		[Tooltip("The integer part is the number of time a state has been looped. The fractional part is the % (0-1) of progress in the current loop")]
		public FsmFloat normalizedTime;

		// Token: 0x04009156 RID: 37206
		[UIHint(UIHint.Variable)]
		[Tooltip("The integer part is the number of time a state has been looped. This is extracted from the normalizedTime")]
		public FsmInt loopCount;

		// Token: 0x04009157 RID: 37207
		[UIHint(UIHint.Variable)]
		[Tooltip("The progress in the current loop. This is extracted from the normalizedTime")]
		public FsmFloat currentLoopProgress;

		// Token: 0x04009158 RID: 37208
		private Animator _animator;
	}
}
