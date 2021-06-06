using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E10 RID: 3600
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Keep transform of a Game Object unchangable.")]
	public class SteadyTransform : FsmStateAction
	{
		// Token: 0x0600A70E RID: 42766 RVA: 0x0034B502 File Offset: 0x00349702
		public override void Reset()
		{
			this.rootFSMObject = null;
			this.FSMObject = null;
			this.positionVector = null;
			this.steadyPosition = false;
			this.steadyRotation = false;
			this.steadyScale = false;
		}

		// Token: 0x0600A70F RID: 42767 RVA: 0x0034B52E File Offset: 0x0034972E
		public override void OnEnter()
		{
			this.DoTransform();
		}

		// Token: 0x0600A710 RID: 42768 RVA: 0x0034B52E File Offset: 0x0034972E
		public override void OnLateUpdate()
		{
			this.DoTransform();
		}

		// Token: 0x0600A711 RID: 42769 RVA: 0x0034B538 File Offset: 0x00349738
		private void DoTransform()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.rootFSMObject);
			GameObject ownerDefaultTarget2 = base.Fsm.GetOwnerDefaultTarget(this.FSMObject);
			if (ownerDefaultTarget == null || ownerDefaultTarget2 == null)
			{
				return;
			}
			if (!this.isInitialPositionHasGot)
			{
				this.initialPosition = ownerDefaultTarget.transform.position;
				this.isInitialPositionHasGot = true;
			}
			this.initialPosition = (this.positionVector.IsNone ? this.initialPosition : this.positionVector.Value);
			if (this.steadyPosition)
			{
				ownerDefaultTarget2.transform.position = this.initialPosition;
			}
			if (this.steadyRotation)
			{
				ownerDefaultTarget2.transform.rotation = Quaternion.identity;
			}
			Vector3 lossyScale = ownerDefaultTarget.transform.lossyScale;
			ownerDefaultTarget2.transform.localScale = new Vector3(1f / lossyScale.x, 1f / lossyScale.y, 1f / lossyScale.z);
		}

		// Token: 0x04008D8E RID: 36238
		[RequiredField]
		[Tooltip("The GameObject to take a global scale from.")]
		public FsmOwnerDefault rootFSMObject;

		// Token: 0x04008D8F RID: 36239
		[RequiredField]
		[Tooltip("The GameObject to set transform.")]
		public FsmOwnerDefault FSMObject;

		// Token: 0x04008D90 RID: 36240
		[UIHint(UIHint.Variable)]
		[Tooltip("Use stored position Vector3 or an initial position of rootFSMObject will be used instead.")]
		public FsmVector3 positionVector;

		// Token: 0x04008D91 RID: 36241
		[Tooltip("Steady position.")]
		public bool steadyPosition;

		// Token: 0x04008D92 RID: 36242
		[Tooltip("Steady rotation.")]
		public bool steadyRotation;

		// Token: 0x04008D93 RID: 36243
		[Tooltip("Steady scale.")]
		public bool steadyScale;

		// Token: 0x04008D94 RID: 36244
		private Vector3 initialPosition;

		// Token: 0x04008D95 RID: 36245
		private bool isInitialPositionHasGot;
	}
}
