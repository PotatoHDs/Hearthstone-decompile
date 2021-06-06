using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EB5 RID: 3765
	[ActionCategory(ActionCategory.Vector3)]
	[Tooltip("Adds a XYZ values to Vector3 Variable.")]
	public class Vector3AddXYZ : FsmStateAction
	{
		// Token: 0x0600AA1C RID: 43548 RVA: 0x003545B0 File Offset: 0x003527B0
		public override void Reset()
		{
			this.vector3Variable = null;
			this.addX = 0f;
			this.addY = 0f;
			this.addZ = 0f;
			this.everyFrame = false;
			this.perSecond = false;
		}

		// Token: 0x0600AA1D RID: 43549 RVA: 0x00354602 File Offset: 0x00352802
		public override void OnEnter()
		{
			this.DoVector3AddXYZ();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AA1E RID: 43550 RVA: 0x00354618 File Offset: 0x00352818
		public override void OnUpdate()
		{
			this.DoVector3AddXYZ();
		}

		// Token: 0x0600AA1F RID: 43551 RVA: 0x00354620 File Offset: 0x00352820
		private void DoVector3AddXYZ()
		{
			Vector3 vector = new Vector3(this.addX.Value, this.addY.Value, this.addZ.Value);
			if (this.perSecond)
			{
				this.vector3Variable.Value += vector * Time.deltaTime;
				return;
			}
			this.vector3Variable.Value += vector;
		}

		// Token: 0x040090B8 RID: 37048
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3 vector3Variable;

		// Token: 0x040090B9 RID: 37049
		public FsmFloat addX;

		// Token: 0x040090BA RID: 37050
		public FsmFloat addY;

		// Token: 0x040090BB RID: 37051
		public FsmFloat addZ;

		// Token: 0x040090BC RID: 37052
		public bool everyFrame;

		// Token: 0x040090BD RID: 37053
		public bool perSecond;
	}
}
