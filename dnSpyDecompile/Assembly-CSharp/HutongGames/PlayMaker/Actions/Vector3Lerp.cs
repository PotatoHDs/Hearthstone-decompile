using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EBA RID: 3770
	[ActionCategory(ActionCategory.Vector3)]
	[Tooltip("Linearly interpolates between 2 vectors.")]
	public class Vector3Lerp : FsmStateAction
	{
		// Token: 0x0600AA32 RID: 43570 RVA: 0x00354A45 File Offset: 0x00352C45
		public override void Reset()
		{
			this.fromVector = new FsmVector3
			{
				UseVariable = true
			};
			this.toVector = new FsmVector3
			{
				UseVariable = true
			};
			this.storeResult = null;
			this.everyFrame = true;
		}

		// Token: 0x0600AA33 RID: 43571 RVA: 0x00354A79 File Offset: 0x00352C79
		public override void OnEnter()
		{
			this.DoVector3Lerp();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AA34 RID: 43572 RVA: 0x00354A8F File Offset: 0x00352C8F
		public override void OnUpdate()
		{
			this.DoVector3Lerp();
		}

		// Token: 0x0600AA35 RID: 43573 RVA: 0x00354A97 File Offset: 0x00352C97
		private void DoVector3Lerp()
		{
			this.storeResult.Value = Vector3.Lerp(this.fromVector.Value, this.toVector.Value, this.amount.Value);
		}

		// Token: 0x040090CF RID: 37071
		[RequiredField]
		[Tooltip("First Vector.")]
		public FsmVector3 fromVector;

		// Token: 0x040090D0 RID: 37072
		[RequiredField]
		[Tooltip("Second Vector.")]
		public FsmVector3 toVector;

		// Token: 0x040090D1 RID: 37073
		[RequiredField]
		[Tooltip("Interpolate between From Vector and ToVector by this amount. Value is clamped to 0-1 range. 0 = From Vector; 1 = To Vector; 0.5 = half way between.")]
		public FsmFloat amount;

		// Token: 0x040090D2 RID: 37074
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in this vector variable.")]
		public FsmVector3 storeResult;

		// Token: 0x040090D3 RID: 37075
		[Tooltip("Repeat every frame. Useful if any of the values are changing.")]
		public bool everyFrame;
	}
}
