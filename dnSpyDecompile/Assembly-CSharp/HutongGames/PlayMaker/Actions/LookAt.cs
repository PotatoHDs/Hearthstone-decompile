using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CEE RID: 3310
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Rotates a Game Object so its forward vector points at a Target. The Target can be specified as a GameObject or a world Position. If you specify both, then Position specifies a local offset from the target object's Position.")]
	public class LookAt : FsmStateAction
	{
		// Token: 0x0600A18C RID: 41356 RVA: 0x00338018 File Offset: 0x00336218
		public override void Reset()
		{
			this.gameObject = null;
			this.targetObject = null;
			this.targetPosition = new FsmVector3
			{
				UseVariable = true
			};
			this.upVector = new FsmVector3
			{
				UseVariable = true
			};
			this.keepVertical = true;
			this.debug = false;
			this.debugLineColor = Color.yellow;
			this.everyFrame = true;
		}

		// Token: 0x0600A18D RID: 41357 RVA: 0x0032C298 File Offset: 0x0032A498
		public override void OnPreprocess()
		{
			base.Fsm.HandleLateUpdate = true;
		}

		// Token: 0x0600A18E RID: 41358 RVA: 0x00338086 File Offset: 0x00336286
		public override void OnEnter()
		{
			this.DoLookAt();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A18F RID: 41359 RVA: 0x0033809C File Offset: 0x0033629C
		public override void OnLateUpdate()
		{
			this.DoLookAt();
		}

		// Token: 0x0600A190 RID: 41360 RVA: 0x003380A4 File Offset: 0x003362A4
		private void DoLookAt()
		{
			if (!this.UpdateLookAtPosition())
			{
				return;
			}
			this.go.transform.LookAt(this.lookAtPos, this.upVector.IsNone ? Vector3.up : this.upVector.Value);
			if (this.debug.Value)
			{
				Debug.DrawLine(this.go.transform.position, this.lookAtPos, this.debugLineColor.Value);
			}
		}

		// Token: 0x0600A191 RID: 41361 RVA: 0x00338124 File Offset: 0x00336324
		public bool UpdateLookAtPosition()
		{
			if (base.Fsm == null)
			{
				return false;
			}
			this.go = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (this.go == null)
			{
				return false;
			}
			this.goTarget = this.targetObject.Value;
			if (this.goTarget == null && this.targetPosition.IsNone)
			{
				return false;
			}
			if (this.goTarget != null)
			{
				this.lookAtPos = ((!this.targetPosition.IsNone) ? this.goTarget.transform.TransformPoint(this.targetPosition.Value) : this.goTarget.transform.position);
			}
			else
			{
				this.lookAtPos = this.targetPosition.Value;
			}
			this.lookAtPosWithVertical = this.lookAtPos;
			if (this.keepVertical.Value)
			{
				this.lookAtPos.y = this.go.transform.position.y;
			}
			return true;
		}

		// Token: 0x0600A192 RID: 41362 RVA: 0x0033822B File Offset: 0x0033642B
		public Vector3 GetLookAtPosition()
		{
			return this.lookAtPos;
		}

		// Token: 0x0600A193 RID: 41363 RVA: 0x00338233 File Offset: 0x00336433
		public Vector3 GetLookAtPositionWithVertical()
		{
			return this.lookAtPosWithVertical;
		}

		// Token: 0x0400878C RID: 34700
		[RequiredField]
		[Tooltip("The GameObject to rotate.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400878D RID: 34701
		[Tooltip("The GameObject to Look At.")]
		public FsmGameObject targetObject;

		// Token: 0x0400878E RID: 34702
		[Tooltip("World position to look at, or local offset from Target Object if specified.")]
		public FsmVector3 targetPosition;

		// Token: 0x0400878F RID: 34703
		[Tooltip("Rotate the GameObject to point its up direction vector in the direction hinted at by the Up Vector. See Unity Look At docs for more details.")]
		public FsmVector3 upVector;

		// Token: 0x04008790 RID: 34704
		[Tooltip("Don't rotate vertically.")]
		public FsmBool keepVertical;

		// Token: 0x04008791 RID: 34705
		[Title("Draw Debug Line")]
		[Tooltip("Draw a debug line from the GameObject to the Target.")]
		public FsmBool debug;

		// Token: 0x04008792 RID: 34706
		[Tooltip("Color to use for the debug line.")]
		public FsmColor debugLineColor;

		// Token: 0x04008793 RID: 34707
		[Tooltip("Repeat every frame.")]
		public bool everyFrame = true;

		// Token: 0x04008794 RID: 34708
		private GameObject go;

		// Token: 0x04008795 RID: 34709
		private GameObject goTarget;

		// Token: 0x04008796 RID: 34710
		private Vector3 lookAtPos;

		// Token: 0x04008797 RID: 34711
		private Vector3 lookAtPosWithVertical;
	}
}
