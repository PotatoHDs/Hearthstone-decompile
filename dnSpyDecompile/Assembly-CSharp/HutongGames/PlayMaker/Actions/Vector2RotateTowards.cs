using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EB2 RID: 3762
	[ActionCategory(ActionCategory.Vector2)]
	[Tooltip("Rotates a Vector2 direction from Current towards Target.")]
	public class Vector2RotateTowards : FsmStateAction
	{
		// Token: 0x0600AA0F RID: 43535 RVA: 0x00354338 File Offset: 0x00352538
		public override void Reset()
		{
			this.currentDirection = new FsmVector2
			{
				UseVariable = true
			};
			this.targetDirection = new FsmVector2
			{
				UseVariable = true
			};
			this.rotateSpeed = 360f;
		}

		// Token: 0x0600AA10 RID: 43536 RVA: 0x00354370 File Offset: 0x00352570
		public override void OnEnter()
		{
			this.current = new Vector3(this.currentDirection.Value.x, this.currentDirection.Value.y, 0f);
			this.target = new Vector3(this.targetDirection.Value.x, this.targetDirection.Value.y, 0f);
		}

		// Token: 0x0600AA11 RID: 43537 RVA: 0x003543E0 File Offset: 0x003525E0
		public override void OnUpdate()
		{
			this.current.x = this.currentDirection.Value.x;
			this.current.y = this.currentDirection.Value.y;
			this.current = Vector3.RotateTowards(this.current, this.target, this.rotateSpeed.Value * 0.017453292f * Time.deltaTime, 1000f);
			this.currentDirection.Value = new Vector2(this.current.x, this.current.y);
		}

		// Token: 0x040090AC RID: 37036
		[RequiredField]
		[Tooltip("The current direction. This will be the result of the rotation as well.")]
		public FsmVector2 currentDirection;

		// Token: 0x040090AD RID: 37037
		[RequiredField]
		[Tooltip("The direction to reach")]
		public FsmVector2 targetDirection;

		// Token: 0x040090AE RID: 37038
		[RequiredField]
		[Tooltip("Rotation speed in degrees per second")]
		public FsmFloat rotateSpeed;

		// Token: 0x040090AF RID: 37039
		private Vector3 current;

		// Token: 0x040090B0 RID: 37040
		private Vector3 target;
	}
}
