using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000ED2 RID: 3794
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Gets the GameObject mapped to this human bone id")]
	public class GetAnimatorBoneGameObject : FsmStateAction
	{
		// Token: 0x0600AA94 RID: 43668 RVA: 0x00356070 File Offset: 0x00354270
		public override void Reset()
		{
			this.gameObject = null;
			this.bone = HumanBodyBones.Hips;
			this.boneGameObject = null;
		}

		// Token: 0x0600AA95 RID: 43669 RVA: 0x00356094 File Offset: 0x00354294
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
			this.GetBoneTransform();
			base.Finish();
		}

		// Token: 0x0600AA96 RID: 43670 RVA: 0x003560F0 File Offset: 0x003542F0
		private void GetBoneTransform()
		{
			this.boneGameObject.Value = this._animator.GetBoneTransform((HumanBodyBones)this.bone.Value).gameObject;
		}

		// Token: 0x0400913E RID: 37182
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400913F RID: 37183
		[Tooltip("The bone reference")]
		[ObjectType(typeof(HumanBodyBones))]
		public FsmEnum bone;

		// Token: 0x04009140 RID: 37184
		[ActionSection("Results")]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Bone's GameObject")]
		public FsmGameObject boneGameObject;

		// Token: 0x04009141 RID: 37185
		private Animator _animator;
	}
}
