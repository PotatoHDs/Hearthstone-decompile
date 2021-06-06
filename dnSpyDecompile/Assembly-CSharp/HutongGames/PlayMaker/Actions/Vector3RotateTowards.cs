using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EC0 RID: 3776
	[ActionCategory(ActionCategory.Vector3)]
	[Tooltip("Rotates a Vector3 direction from Current towards Target.")]
	public class Vector3RotateTowards : FsmStateAction
	{
		// Token: 0x0600AA4C RID: 43596 RVA: 0x00354FA4 File Offset: 0x003531A4
		public override void Reset()
		{
			this.currentDirection = new FsmVector3
			{
				UseVariable = true
			};
			this.targetDirection = new FsmVector3
			{
				UseVariable = true
			};
			this.rotateSpeed = 360f;
			this.maxMagnitude = 1f;
		}

		// Token: 0x0600AA4D RID: 43597 RVA: 0x00354FF8 File Offset: 0x003531F8
		public override void OnUpdate()
		{
			this.currentDirection.Value = Vector3.RotateTowards(this.currentDirection.Value, this.targetDirection.Value, this.rotateSpeed.Value * 0.017453292f * Time.deltaTime, this.maxMagnitude.Value);
		}

		// Token: 0x040090E4 RID: 37092
		[RequiredField]
		public FsmVector3 currentDirection;

		// Token: 0x040090E5 RID: 37093
		[RequiredField]
		public FsmVector3 targetDirection;

		// Token: 0x040090E6 RID: 37094
		[RequiredField]
		[Tooltip("Rotation speed in degrees per second")]
		public FsmFloat rotateSpeed;

		// Token: 0x040090E7 RID: 37095
		[RequiredField]
		[Tooltip("Max Magnitude per second")]
		public FsmFloat maxMagnitude;
	}
}
