using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D4C RID: 3404
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Sets a Float Variable to a random value between Min/Max.")]
	public class RandomFloat : FsmStateAction
	{
		// Token: 0x0600A386 RID: 41862 RVA: 0x0033F513 File Offset: 0x0033D713
		public override void Reset()
		{
			this.min = 0f;
			this.max = 1f;
			this.storeResult = null;
		}

		// Token: 0x0600A387 RID: 41863 RVA: 0x0033F53C File Offset: 0x0033D73C
		public override void OnEnter()
		{
			this.storeResult.Value = UnityEngine.Random.Range(this.min.Value, this.max.Value);
			base.Finish();
		}

		// Token: 0x040089B2 RID: 35250
		[RequiredField]
		public FsmFloat min;

		// Token: 0x040089B3 RID: 35251
		[RequiredField]
		public FsmFloat max;

		// Token: 0x040089B4 RID: 35252
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat storeResult;
	}
}
