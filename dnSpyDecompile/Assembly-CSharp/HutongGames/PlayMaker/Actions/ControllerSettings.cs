using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BF2 RID: 3058
	[ActionCategory(ActionCategory.Character)]
	[Tooltip("Modify various character controller settings.\n'None' leaves the setting unchanged.")]
	public class ControllerSettings : FsmStateAction
	{
		// Token: 0x06009D5C RID: 40284 RVA: 0x00328C28 File Offset: 0x00326E28
		public override void Reset()
		{
			this.gameObject = null;
			this.height = new FsmFloat
			{
				UseVariable = true
			};
			this.radius = new FsmFloat
			{
				UseVariable = true
			};
			this.slopeLimit = new FsmFloat
			{
				UseVariable = true
			};
			this.stepOffset = new FsmFloat
			{
				UseVariable = true
			};
			this.center = new FsmVector3
			{
				UseVariable = true
			};
			this.detectCollisions = new FsmBool
			{
				UseVariable = true
			};
			this.everyFrame = false;
		}

		// Token: 0x06009D5D RID: 40285 RVA: 0x00328CAF File Offset: 0x00326EAF
		public override void OnEnter()
		{
			this.DoControllerSettings();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009D5E RID: 40286 RVA: 0x00328CC5 File Offset: 0x00326EC5
		public override void OnUpdate()
		{
			this.DoControllerSettings();
		}

		// Token: 0x06009D5F RID: 40287 RVA: 0x00328CD0 File Offset: 0x00326ED0
		private void DoControllerSettings()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			if (ownerDefaultTarget != this.previousGo)
			{
				this.controller = ownerDefaultTarget.GetComponent<CharacterController>();
				this.previousGo = ownerDefaultTarget;
			}
			if (this.controller != null)
			{
				if (!this.height.IsNone)
				{
					this.controller.height = this.height.Value;
				}
				if (!this.radius.IsNone)
				{
					this.controller.radius = this.radius.Value;
				}
				if (!this.slopeLimit.IsNone)
				{
					this.controller.slopeLimit = this.slopeLimit.Value;
				}
				if (!this.stepOffset.IsNone)
				{
					this.controller.stepOffset = this.stepOffset.Value;
				}
				if (!this.center.IsNone)
				{
					this.controller.center = this.center.Value;
				}
				if (!this.detectCollisions.IsNone)
				{
					this.controller.detectCollisions = this.detectCollisions.Value;
				}
			}
		}

		// Token: 0x040082C7 RID: 33479
		[RequiredField]
		[CheckForComponent(typeof(CharacterController))]
		[Tooltip("The GameObject that owns the CharacterController.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040082C8 RID: 33480
		[Tooltip("The height of the character's capsule.")]
		public FsmFloat height;

		// Token: 0x040082C9 RID: 33481
		[Tooltip("The radius of the character's capsule.")]
		public FsmFloat radius;

		// Token: 0x040082CA RID: 33482
		[Tooltip("The character controllers slope limit in degrees.")]
		public FsmFloat slopeLimit;

		// Token: 0x040082CB RID: 33483
		[Tooltip("The character controllers step offset in meters.")]
		public FsmFloat stepOffset;

		// Token: 0x040082CC RID: 33484
		[Tooltip("The center of the character's capsule relative to the transform's position")]
		public FsmVector3 center;

		// Token: 0x040082CD RID: 33485
		[Tooltip("Should other rigidbodies or character controllers collide with this character controller (By default always enabled).")]
		public FsmBool detectCollisions;

		// Token: 0x040082CE RID: 33486
		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		// Token: 0x040082CF RID: 33487
		private GameObject previousGo;

		// Token: 0x040082D0 RID: 33488
		private CharacterController controller;
	}
}
