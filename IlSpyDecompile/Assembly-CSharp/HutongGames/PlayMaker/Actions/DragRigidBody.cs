using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Drag a Rigid body with the mouse. If draggingPlaneTransform is defined, it will use the UP axis of this gameObject as the dragging plane normal \nThat is select the ground Plane, if you want to drag object on the ground instead of from the camera point of view.")]
	public class DragRigidBody : FsmStateAction
	{
		[Tooltip("the springness of the drag")]
		public FsmFloat spring;

		[Tooltip("the damping of the drag")]
		public FsmFloat damper;

		[Tooltip("the drag during dragging")]
		public FsmFloat drag;

		[Tooltip("the angular drag during dragging")]
		public FsmFloat angularDrag;

		[Tooltip("The Max Distance between the dragging target and the RigidBody being dragged")]
		public FsmFloat distance;

		[Tooltip("If TRUE, dragging will have close to no effect on the Rigidbody rotation ( except if it hits other bodies as you drag it)")]
		public FsmBool attachToCenterOfMass;

		[Tooltip("Move th object forward and back or up and down")]
		public FsmBool moveUp;

		[Tooltip("If Defined. Use this transform Up axis as the dragging plane normal. Typically, set it to the ground plane if you want to drag objects around on the floor..")]
		public FsmOwnerDefault draggingPlaneTransform;

		private SpringJoint springJoint;

		private bool isDragging;

		private float oldDrag;

		private float oldAngularDrag;

		private Camera _cam;

		private GameObject _goPlane;

		private Vector3 _dragStartPos;

		private float dragDistance;

		public override void Reset()
		{
			spring = 50f;
			damper = 5f;
			drag = 10f;
			angularDrag = 5f;
			distance = 0.2f;
			attachToCenterOfMass = false;
			draggingPlaneTransform = null;
			moveUp = true;
		}

		public override void OnEnter()
		{
			_cam = Camera.main;
			_goPlane = base.Fsm.GetOwnerDefaultTarget(draggingPlaneTransform);
		}

		public override void OnUpdate()
		{
			if (!isDragging && UniversalInputManager.Get().GetMouseButtonDown(0))
			{
				if (!Physics.Raycast(_cam.ScreenPointToRay(UniversalInputManager.Get().GetMousePosition()), out var hitInfo, 100f) || !hitInfo.rigidbody || hitInfo.rigidbody.isKinematic)
				{
					return;
				}
				StartDragging(hitInfo);
			}
			if (isDragging)
			{
				Drag();
			}
		}

		private void StartDragging(RaycastHit hit)
		{
			isDragging = true;
			if (!springJoint)
			{
				GameObject gameObject = new GameObject("__Rigidbody dragger__");
				Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
				springJoint = gameObject.AddComponent<SpringJoint>();
				rigidbody.isKinematic = true;
			}
			springJoint.transform.position = hit.point;
			if (attachToCenterOfMass.Value)
			{
				Vector3 position = _cam.transform.TransformDirection(hit.rigidbody.centerOfMass) + hit.rigidbody.transform.position;
				position = springJoint.transform.InverseTransformPoint(position);
				springJoint.anchor = position;
			}
			else
			{
				springJoint.anchor = Vector3.zero;
			}
			_dragStartPos = hit.point;
			springJoint.spring = spring.Value;
			springJoint.damper = damper.Value;
			springJoint.maxDistance = distance.Value;
			springJoint.connectedBody = hit.rigidbody;
			oldDrag = springJoint.connectedBody.drag;
			oldAngularDrag = springJoint.connectedBody.angularDrag;
			springJoint.connectedBody.drag = drag.Value;
			springJoint.connectedBody.angularDrag = angularDrag.Value;
			dragDistance = hit.distance;
		}

		private void Drag()
		{
			if (!UniversalInputManager.Get().GetMouseButton(0))
			{
				StopDragging();
				return;
			}
			Ray ray = _cam.ScreenPointToRay(UniversalInputManager.Get().GetMousePosition());
			if (_goPlane != null)
			{
				Plane plane = default(Plane);
				if (((!moveUp.Value) ? new Plane(_goPlane.transform.up, _dragStartPos) : new Plane(_goPlane.transform.forward, _dragStartPos)).Raycast(ray, out var enter))
				{
					springJoint.transform.position = ray.GetPoint(enter);
				}
			}
			else
			{
				springJoint.transform.position = ray.GetPoint(dragDistance);
			}
		}

		private void StopDragging()
		{
			isDragging = false;
			if (!(springJoint == null) && (bool)springJoint.connectedBody)
			{
				springJoint.connectedBody.drag = oldDrag;
				springJoint.connectedBody.angularDrag = oldAngularDrag;
				springJoint.connectedBody = null;
			}
		}

		public override void OnExit()
		{
			StopDragging();
		}
	}
}
