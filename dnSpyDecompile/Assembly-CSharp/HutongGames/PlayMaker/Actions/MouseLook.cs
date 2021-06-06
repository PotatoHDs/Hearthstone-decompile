using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CEF RID: 3311
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Rotates a GameObject based on mouse movement. Minimum and Maximum values can be used to constrain the rotation.")]
	public class MouseLook : FsmStateAction
	{
		// Token: 0x0600A195 RID: 41365 RVA: 0x0033824C File Offset: 0x0033644C
		public override void Reset()
		{
			this.gameObject = null;
			this.axes = MouseLook.RotationAxes.MouseXAndY;
			this.sensitivityX = 15f;
			this.sensitivityY = 15f;
			this.minimumX = new FsmFloat
			{
				UseVariable = true
			};
			this.maximumX = new FsmFloat
			{
				UseVariable = true
			};
			this.minimumY = -60f;
			this.maximumY = 60f;
			this.everyFrame = true;
		}

		// Token: 0x0600A196 RID: 41366 RVA: 0x003382D4 File Offset: 0x003364D4
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				base.Finish();
				return;
			}
			Rigidbody component = ownerDefaultTarget.GetComponent<Rigidbody>();
			if (component != null)
			{
				component.freezeRotation = true;
			}
			this.rotationX = ownerDefaultTarget.transform.localRotation.eulerAngles.y;
			this.rotationY = ownerDefaultTarget.transform.localRotation.eulerAngles.x;
			this.DoMouseLook();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A197 RID: 41367 RVA: 0x0033836A File Offset: 0x0033656A
		public override void OnUpdate()
		{
			this.DoMouseLook();
		}

		// Token: 0x0600A198 RID: 41368 RVA: 0x00338374 File Offset: 0x00336574
		private void DoMouseLook()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			Transform transform = ownerDefaultTarget.transform;
			switch (this.axes)
			{
			case MouseLook.RotationAxes.MouseXAndY:
				transform.localEulerAngles = new Vector3(this.GetYRotation(), this.GetXRotation(), 0f);
				return;
			case MouseLook.RotationAxes.MouseX:
				transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, this.GetXRotation(), 0f);
				return;
			case MouseLook.RotationAxes.MouseY:
				transform.localEulerAngles = new Vector3(-this.GetYRotation(), transform.localEulerAngles.y, 0f);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600A199 RID: 41369 RVA: 0x00338420 File Offset: 0x00336620
		private float GetXRotation()
		{
			this.rotationX += Input.GetAxis("Mouse X") * this.sensitivityX.Value;
			this.rotationX = MouseLook.ClampAngle(this.rotationX, this.minimumX, this.maximumX);
			return this.rotationX;
		}

		// Token: 0x0600A19A RID: 41370 RVA: 0x00338474 File Offset: 0x00336674
		private float GetYRotation()
		{
			this.rotationY += Input.GetAxis("Mouse Y") * this.sensitivityY.Value;
			this.rotationY = MouseLook.ClampAngle(this.rotationY, this.minimumY, this.maximumY);
			return this.rotationY;
		}

		// Token: 0x0600A19B RID: 41371 RVA: 0x003384C7 File Offset: 0x003366C7
		private static float ClampAngle(float angle, FsmFloat min, FsmFloat max)
		{
			if (!min.IsNone && angle < min.Value)
			{
				angle = min.Value;
			}
			if (!max.IsNone && angle > max.Value)
			{
				angle = max.Value;
			}
			return angle;
		}

		// Token: 0x04008798 RID: 34712
		[RequiredField]
		[Tooltip("The GameObject to rotate.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008799 RID: 34713
		[Tooltip("The axes to rotate around.")]
		public MouseLook.RotationAxes axes;

		// Token: 0x0400879A RID: 34714
		[RequiredField]
		[Tooltip("Sensitivity of movement in X direction.")]
		public FsmFloat sensitivityX;

		// Token: 0x0400879B RID: 34715
		[RequiredField]
		[Tooltip("Sensitivity of movement in Y direction.")]
		public FsmFloat sensitivityY;

		// Token: 0x0400879C RID: 34716
		[HasFloatSlider(-360f, 360f)]
		[Tooltip("Clamp rotation around X axis. Set to None for no clamping.")]
		public FsmFloat minimumX;

		// Token: 0x0400879D RID: 34717
		[HasFloatSlider(-360f, 360f)]
		[Tooltip("Clamp rotation around X axis. Set to None for no clamping.")]
		public FsmFloat maximumX;

		// Token: 0x0400879E RID: 34718
		[HasFloatSlider(-360f, 360f)]
		[Tooltip("Clamp rotation around Y axis. Set to None for no clamping.")]
		public FsmFloat minimumY;

		// Token: 0x0400879F RID: 34719
		[HasFloatSlider(-360f, 360f)]
		[Tooltip("Clamp rotation around Y axis. Set to None for no clamping.")]
		public FsmFloat maximumY;

		// Token: 0x040087A0 RID: 34720
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		// Token: 0x040087A1 RID: 34721
		private float rotationX;

		// Token: 0x040087A2 RID: 34722
		private float rotationY;

		// Token: 0x0200279D RID: 10141
		public enum RotationAxes
		{
			// Token: 0x0400F4E2 RID: 62690
			MouseXAndY,
			// Token: 0x0400F4E3 RID: 62691
			MouseX,
			// Token: 0x0400F4E4 RID: 62692
			MouseY
		}
	}
}
