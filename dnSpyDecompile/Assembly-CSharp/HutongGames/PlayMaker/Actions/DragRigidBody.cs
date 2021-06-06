using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F30 RID: 3888
	[ActionCategory("Pegasus")]
	[Tooltip("Drag a Rigid body with the mouse. If draggingPlaneTransform is defined, it will use the UP axis of this gameObject as the dragging plane normal \nThat is select the ground Plane, if you want to drag object on the ground instead of from the camera point of view.")]
	public class DragRigidBody : FsmStateAction
	{
		// Token: 0x0600AC4A RID: 44106 RVA: 0x0035C344 File Offset: 0x0035A544
		public override void Reset()
		{
			this.spring = 50f;
			this.damper = 5f;
			this.drag = 10f;
			this.angularDrag = 5f;
			this.distance = 0.2f;
			this.attachToCenterOfMass = false;
			this.draggingPlaneTransform = null;
			this.moveUp = true;
		}

		// Token: 0x0600AC4B RID: 44107 RVA: 0x0035C3C0 File Offset: 0x0035A5C0
		public override void OnEnter()
		{
			this._cam = Camera.main;
			this._goPlane = base.Fsm.GetOwnerDefaultTarget(this.draggingPlaneTransform);
		}

		// Token: 0x0600AC4C RID: 44108 RVA: 0x0035C3E4 File Offset: 0x0035A5E4
		public override void OnUpdate()
		{
			if (!this.isDragging && UniversalInputManager.Get().GetMouseButtonDown(0))
			{
				RaycastHit hit;
				if (!Physics.Raycast(this._cam.ScreenPointToRay(UniversalInputManager.Get().GetMousePosition()), out hit, 100f))
				{
					return;
				}
				if (!hit.rigidbody || hit.rigidbody.isKinematic)
				{
					return;
				}
				this.StartDragging(hit);
			}
			if (this.isDragging)
			{
				this.Drag();
			}
		}

		// Token: 0x0600AC4D RID: 44109 RVA: 0x0035C45C File Offset: 0x0035A65C
		private void StartDragging(RaycastHit hit)
		{
			this.isDragging = true;
			if (!this.springJoint)
			{
				GameObject gameObject = new GameObject("__Rigidbody dragger__");
				Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
				this.springJoint = gameObject.AddComponent<SpringJoint>();
				rigidbody.isKinematic = true;
			}
			this.springJoint.transform.position = hit.point;
			if (this.attachToCenterOfMass.Value)
			{
				Vector3 vector = this._cam.transform.TransformDirection(hit.rigidbody.centerOfMass) + hit.rigidbody.transform.position;
				vector = this.springJoint.transform.InverseTransformPoint(vector);
				this.springJoint.anchor = vector;
			}
			else
			{
				this.springJoint.anchor = Vector3.zero;
			}
			this._dragStartPos = hit.point;
			this.springJoint.spring = this.spring.Value;
			this.springJoint.damper = this.damper.Value;
			this.springJoint.maxDistance = this.distance.Value;
			this.springJoint.connectedBody = hit.rigidbody;
			this.oldDrag = this.springJoint.connectedBody.drag;
			this.oldAngularDrag = this.springJoint.connectedBody.angularDrag;
			this.springJoint.connectedBody.drag = this.drag.Value;
			this.springJoint.connectedBody.angularDrag = this.angularDrag.Value;
			this.dragDistance = hit.distance;
		}

		// Token: 0x0600AC4E RID: 44110 RVA: 0x0035C5F8 File Offset: 0x0035A7F8
		private void Drag()
		{
			if (!UniversalInputManager.Get().GetMouseButton(0))
			{
				this.StopDragging();
				return;
			}
			Ray ray = this._cam.ScreenPointToRay(UniversalInputManager.Get().GetMousePosition());
			if (this._goPlane != null)
			{
				Plane plane = default(Plane);
				if (this.moveUp.Value)
				{
					plane = new Plane(this._goPlane.transform.forward, this._dragStartPos);
				}
				else
				{
					plane = new Plane(this._goPlane.transform.up, this._dragStartPos);
				}
				float num;
				if (plane.Raycast(ray, out num))
				{
					this.springJoint.transform.position = ray.GetPoint(num);
					return;
				}
			}
			else
			{
				this.springJoint.transform.position = ray.GetPoint(this.dragDistance);
			}
		}

		// Token: 0x0600AC4F RID: 44111 RVA: 0x0035C6D0 File Offset: 0x0035A8D0
		private void StopDragging()
		{
			this.isDragging = false;
			if (this.springJoint == null)
			{
				return;
			}
			if (this.springJoint.connectedBody)
			{
				this.springJoint.connectedBody.drag = this.oldDrag;
				this.springJoint.connectedBody.angularDrag = this.oldAngularDrag;
				this.springJoint.connectedBody = null;
			}
		}

		// Token: 0x0600AC50 RID: 44112 RVA: 0x0035C73D File Offset: 0x0035A93D
		public override void OnExit()
		{
			this.StopDragging();
		}

		// Token: 0x0400931D RID: 37661
		[Tooltip("the springness of the drag")]
		public FsmFloat spring;

		// Token: 0x0400931E RID: 37662
		[Tooltip("the damping of the drag")]
		public FsmFloat damper;

		// Token: 0x0400931F RID: 37663
		[Tooltip("the drag during dragging")]
		public FsmFloat drag;

		// Token: 0x04009320 RID: 37664
		[Tooltip("the angular drag during dragging")]
		public FsmFloat angularDrag;

		// Token: 0x04009321 RID: 37665
		[Tooltip("The Max Distance between the dragging target and the RigidBody being dragged")]
		public FsmFloat distance;

		// Token: 0x04009322 RID: 37666
		[Tooltip("If TRUE, dragging will have close to no effect on the Rigidbody rotation ( except if it hits other bodies as you drag it)")]
		public FsmBool attachToCenterOfMass;

		// Token: 0x04009323 RID: 37667
		[Tooltip("Move th object forward and back or up and down")]
		public FsmBool moveUp;

		// Token: 0x04009324 RID: 37668
		[Tooltip("If Defined. Use this transform Up axis as the dragging plane normal. Typically, set it to the ground plane if you want to drag objects around on the floor..")]
		public FsmOwnerDefault draggingPlaneTransform;

		// Token: 0x04009325 RID: 37669
		private SpringJoint springJoint;

		// Token: 0x04009326 RID: 37670
		private bool isDragging;

		// Token: 0x04009327 RID: 37671
		private float oldDrag;

		// Token: 0x04009328 RID: 37672
		private float oldAngularDrag;

		// Token: 0x04009329 RID: 37673
		private Camera _cam;

		// Token: 0x0400932A RID: 37674
		private GameObject _goPlane;

		// Token: 0x0400932B RID: 37675
		private Vector3 _dragStartPos;

		// Token: 0x0400932C RID: 37676
		private float dragDistance;
	}
}
