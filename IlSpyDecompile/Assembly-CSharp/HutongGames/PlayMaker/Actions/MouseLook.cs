using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Rotates a GameObject based on mouse movement. Minimum and Maximum values can be used to constrain the rotation.")]
	public class MouseLook : FsmStateAction
	{
		public enum RotationAxes
		{
			MouseXAndY,
			MouseX,
			MouseY
		}

		[RequiredField]
		[Tooltip("The GameObject to rotate.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The axes to rotate around.")]
		public RotationAxes axes;

		[RequiredField]
		[Tooltip("Sensitivity of movement in X direction.")]
		public FsmFloat sensitivityX;

		[RequiredField]
		[Tooltip("Sensitivity of movement in Y direction.")]
		public FsmFloat sensitivityY;

		[HasFloatSlider(-360f, 360f)]
		[Tooltip("Clamp rotation around X axis. Set to None for no clamping.")]
		public FsmFloat minimumX;

		[HasFloatSlider(-360f, 360f)]
		[Tooltip("Clamp rotation around X axis. Set to None for no clamping.")]
		public FsmFloat maximumX;

		[HasFloatSlider(-360f, 360f)]
		[Tooltip("Clamp rotation around Y axis. Set to None for no clamping.")]
		public FsmFloat minimumY;

		[HasFloatSlider(-360f, 360f)]
		[Tooltip("Clamp rotation around Y axis. Set to None for no clamping.")]
		public FsmFloat maximumY;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		private float rotationX;

		private float rotationY;

		public override void Reset()
		{
			gameObject = null;
			axes = RotationAxes.MouseXAndY;
			sensitivityX = 15f;
			sensitivityY = 15f;
			minimumX = new FsmFloat
			{
				UseVariable = true
			};
			maximumX = new FsmFloat
			{
				UseVariable = true
			};
			minimumY = -60f;
			maximumY = 60f;
			everyFrame = true;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget == null)
			{
				Finish();
				return;
			}
			Rigidbody component = ownerDefaultTarget.GetComponent<Rigidbody>();
			if (component != null)
			{
				component.freezeRotation = true;
			}
			rotationX = ownerDefaultTarget.transform.localRotation.eulerAngles.y;
			rotationY = ownerDefaultTarget.transform.localRotation.eulerAngles.x;
			DoMouseLook();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoMouseLook();
		}

		private void DoMouseLook()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (!(ownerDefaultTarget == null))
			{
				Transform transform = ownerDefaultTarget.transform;
				switch (axes)
				{
				case RotationAxes.MouseXAndY:
					transform.localEulerAngles = new Vector3(GetYRotation(), GetXRotation(), 0f);
					break;
				case RotationAxes.MouseX:
					transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, GetXRotation(), 0f);
					break;
				case RotationAxes.MouseY:
					transform.localEulerAngles = new Vector3(0f - GetYRotation(), transform.localEulerAngles.y, 0f);
					break;
				}
			}
		}

		private float GetXRotation()
		{
			rotationX += Input.GetAxis("Mouse X") * sensitivityX.Value;
			rotationX = ClampAngle(rotationX, minimumX, maximumX);
			return rotationX;
		}

		private float GetYRotation()
		{
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY.Value;
			rotationY = ClampAngle(rotationY, minimumY, maximumY);
			return rotationY;
		}

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
	}
}
