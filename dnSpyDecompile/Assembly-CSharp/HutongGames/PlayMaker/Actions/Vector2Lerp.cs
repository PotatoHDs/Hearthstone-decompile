using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EAB RID: 3755
	[ActionCategory(ActionCategory.Vector2)]
	[Tooltip("Linearly interpolates between 2 vectors.")]
	public class Vector2Lerp : FsmStateAction
	{
		// Token: 0x0600A9F1 RID: 43505 RVA: 0x00353DDD File Offset: 0x00351FDD
		public override void Reset()
		{
			this.fromVector = new FsmVector2
			{
				UseVariable = true
			};
			this.toVector = new FsmVector2
			{
				UseVariable = true
			};
			this.storeResult = null;
			this.everyFrame = true;
		}

		// Token: 0x0600A9F2 RID: 43506 RVA: 0x00353E11 File Offset: 0x00352011
		public override void OnEnter()
		{
			this.DoVector2Lerp();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A9F3 RID: 43507 RVA: 0x00353E27 File Offset: 0x00352027
		public override void OnUpdate()
		{
			this.DoVector2Lerp();
		}

		// Token: 0x0600A9F4 RID: 43508 RVA: 0x00353E2F File Offset: 0x0035202F
		private void DoVector2Lerp()
		{
			this.storeResult.Value = Vector2.Lerp(this.fromVector.Value, this.toVector.Value, this.amount.Value);
		}

		// Token: 0x04009092 RID: 37010
		[RequiredField]
		[Tooltip("First Vector.")]
		public FsmVector2 fromVector;

		// Token: 0x04009093 RID: 37011
		[RequiredField]
		[Tooltip("Second Vector.")]
		public FsmVector2 toVector;

		// Token: 0x04009094 RID: 37012
		[RequiredField]
		[Tooltip("Interpolate between From Vector and ToVector by this amount. Value is clamped to 0-1 range. 0 = From Vector; 1 = To Vector; 0.5 = half way between.")]
		public FsmFloat amount;

		// Token: 0x04009095 RID: 37013
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in this vector variable.")]
		public FsmVector2 storeResult;

		// Token: 0x04009096 RID: 37014
		[Tooltip("Repeat every frame. Useful if any of the values are changing.")]
		public bool everyFrame;
	}
}
