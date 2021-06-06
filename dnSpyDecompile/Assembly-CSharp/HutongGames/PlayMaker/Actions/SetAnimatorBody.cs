using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EF6 RID: 3830
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Sets the position and rotation of the body. A GameObject can be set to control the position and rotation, or it can be manually expressed.")]
	public class SetAnimatorBody : FsmStateAction
	{
		// Token: 0x0600AB3F RID: 43839 RVA: 0x003582CF File Offset: 0x003564CF
		public override void Reset()
		{
			this.gameObject = null;
			this.target = null;
			this.position = new FsmVector3
			{
				UseVariable = true
			};
			this.rotation = new FsmQuaternion
			{
				UseVariable = true
			};
			this.everyFrame = false;
		}

		// Token: 0x0600AB40 RID: 43840 RVA: 0x0035830A File Offset: 0x0035650A
		public override void OnPreprocess()
		{
			base.Fsm.HandleAnimatorIK = true;
		}

		// Token: 0x0600AB41 RID: 43841 RVA: 0x00358318 File Offset: 0x00356518
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
			GameObject value = this.target.Value;
			if (value != null)
			{
				this._transform = value.transform;
			}
		}

		// Token: 0x0600AB42 RID: 43842 RVA: 0x00358389 File Offset: 0x00356589
		public override void DoAnimatorIK(int layerIndex)
		{
			this.DoSetBody();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AB43 RID: 43843 RVA: 0x003583A0 File Offset: 0x003565A0
		private void DoSetBody()
		{
			if (this._animator == null)
			{
				return;
			}
			if (!(this._transform != null))
			{
				if (!this.position.IsNone)
				{
					this._animator.bodyPosition = this.position.Value;
				}
				if (!this.rotation.IsNone)
				{
					this._animator.bodyRotation = this.rotation.Value;
				}
				return;
			}
			if (this.position.IsNone)
			{
				this._animator.bodyPosition = this._transform.position;
			}
			else
			{
				this._animator.bodyPosition = this._transform.position + this.position.Value;
			}
			if (this.rotation.IsNone)
			{
				this._animator.bodyRotation = this._transform.rotation;
				return;
			}
			this._animator.bodyRotation = this._transform.rotation * this.rotation.Value;
		}

		// Token: 0x040091FD RID: 37373
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040091FE RID: 37374
		[Tooltip("The gameObject target of the ik goal")]
		public FsmGameObject target;

		// Token: 0x040091FF RID: 37375
		[Tooltip("The position of the ik goal. If Goal GameObject set, position is used as an offset from Goal")]
		public FsmVector3 position;

		// Token: 0x04009200 RID: 37376
		[Tooltip("The rotation of the ik goal.If Goal GameObject set, rotation is used as an offset from Goal")]
		public FsmQuaternion rotation;

		// Token: 0x04009201 RID: 37377
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		// Token: 0x04009202 RID: 37378
		private Animator _animator;

		// Token: 0x04009203 RID: 37379
		private Transform _transform;
	}
}
