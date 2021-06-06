using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CF0 RID: 3312
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Rotates a GameObject based on mouse movement. Minimum and Maximum values can be used to constrain the rotation.")]
	public class MouseLook2 : ComponentAction<Rigidbody>
	{
		// Token: 0x0600A19D RID: 41373 RVA: 0x003384FC File Offset: 0x003366FC
		public override void Reset()
		{
			this.gameObject = null;
			this.axes = MouseLook2.RotationAxes.MouseXAndY;
			this.sensitivityX = 15f;
			this.sensitivityY = 15f;
			this.minimumX = -360f;
			this.maximumX = 360f;
			this.minimumY = -60f;
			this.maximumY = 60f;
			this.everyFrame = true;
		}

		// Token: 0x0600A19E RID: 41374 RVA: 0x00338580 File Offset: 0x00336780
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				base.Finish();
				return;
			}
			if (!base.UpdateCache(ownerDefaultTarget) && base.rigidbody)
			{
				base.rigidbody.freezeRotation = true;
			}
			this.DoMouseLook();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A19F RID: 41375 RVA: 0x003385E5 File Offset: 0x003367E5
		public override void OnUpdate()
		{
			this.DoMouseLook();
		}

		// Token: 0x0600A1A0 RID: 41376 RVA: 0x003385F0 File Offset: 0x003367F0
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
			case MouseLook2.RotationAxes.MouseXAndY:
				transform.localEulerAngles = new Vector3(this.GetYRotation(), this.GetXRotation(), 0f);
				return;
			case MouseLook2.RotationAxes.MouseX:
				transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, this.GetXRotation(), 0f);
				return;
			case MouseLook2.RotationAxes.MouseY:
				transform.localEulerAngles = new Vector3(-this.GetYRotation(), transform.localEulerAngles.y, 0f);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600A1A1 RID: 41377 RVA: 0x0033869C File Offset: 0x0033689C
		private float GetXRotation()
		{
			this.rotationX += Input.GetAxis("Mouse X") * this.sensitivityX.Value;
			this.rotationX = MouseLook2.ClampAngle(this.rotationX, this.minimumX, this.maximumX);
			return this.rotationX;
		}

		// Token: 0x0600A1A2 RID: 41378 RVA: 0x003386F0 File Offset: 0x003368F0
		private float GetYRotation()
		{
			this.rotationY += Input.GetAxis("Mouse Y") * this.sensitivityY.Value;
			this.rotationY = MouseLook2.ClampAngle(this.rotationY, this.minimumY, this.maximumY);
			return this.rotationY;
		}

		// Token: 0x0600A1A3 RID: 41379 RVA: 0x003384C7 File Offset: 0x003366C7
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

		// Token: 0x040087A3 RID: 34723
		[RequiredField]
		[Tooltip("The GameObject to rotate.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040087A4 RID: 34724
		[Tooltip("The axes to rotate around.")]
		public MouseLook2.RotationAxes axes;

		// Token: 0x040087A5 RID: 34725
		[RequiredField]
		public FsmFloat sensitivityX;

		// Token: 0x040087A6 RID: 34726
		[RequiredField]
		public FsmFloat sensitivityY;

		// Token: 0x040087A7 RID: 34727
		[RequiredField]
		[HasFloatSlider(-360f, 360f)]
		public FsmFloat minimumX;

		// Token: 0x040087A8 RID: 34728
		[RequiredField]
		[HasFloatSlider(-360f, 360f)]
		public FsmFloat maximumX;

		// Token: 0x040087A9 RID: 34729
		[RequiredField]
		[HasFloatSlider(-360f, 360f)]
		public FsmFloat minimumY;

		// Token: 0x040087AA RID: 34730
		[RequiredField]
		[HasFloatSlider(-360f, 360f)]
		public FsmFloat maximumY;

		// Token: 0x040087AB RID: 34731
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		// Token: 0x040087AC RID: 34732
		private float rotationX;

		// Token: 0x040087AD RID: 34733
		private float rotationY;

		// Token: 0x0200279E RID: 10142
		public enum RotationAxes
		{
			// Token: 0x0400F4E6 RID: 62694
			MouseXAndY,
			// Token: 0x0400F4E7 RID: 62695
			MouseX,
			// Token: 0x0400F4E8 RID: 62696
			MouseY
		}
	}
}
