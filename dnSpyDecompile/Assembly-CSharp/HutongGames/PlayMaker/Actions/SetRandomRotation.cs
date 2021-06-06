using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DF3 RID: 3571
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Sets Random Rotation for a Game Object. Uncheck an axis to keep its current value.")]
	public class SetRandomRotation : FsmStateAction
	{
		// Token: 0x0600A68B RID: 42635 RVA: 0x00349893 File Offset: 0x00347A93
		public override void Reset()
		{
			this.gameObject = null;
			this.x = true;
			this.y = true;
			this.z = true;
		}

		// Token: 0x0600A68C RID: 42636 RVA: 0x003498C0 File Offset: 0x00347AC0
		public override void OnEnter()
		{
			this.DoRandomRotation();
			base.Finish();
		}

		// Token: 0x0600A68D RID: 42637 RVA: 0x003498D0 File Offset: 0x00347AD0
		private void DoRandomRotation()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			Vector3 localEulerAngles = ownerDefaultTarget.transform.localEulerAngles;
			float num = localEulerAngles.x;
			float num2 = localEulerAngles.y;
			float num3 = localEulerAngles.z;
			if (this.x.Value)
			{
				num = (float)UnityEngine.Random.Range(0, 360);
			}
			if (this.y.Value)
			{
				num2 = (float)UnityEngine.Random.Range(0, 360);
			}
			if (this.z.Value)
			{
				num3 = (float)UnityEngine.Random.Range(0, 360);
			}
			ownerDefaultTarget.transform.localEulerAngles = new Vector3(num, num2, num3);
		}

		// Token: 0x04008D0C RID: 36108
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008D0D RID: 36109
		[RequiredField]
		public FsmBool x;

		// Token: 0x04008D0E RID: 36110
		[RequiredField]
		public FsmBool y;

		// Token: 0x04008D0F RID: 36111
		[RequiredField]
		public FsmBool z;
	}
}
