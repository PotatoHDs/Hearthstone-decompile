using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D4A RID: 3402
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Sets a Bool Variable to True or False randomly.")]
	public class RandomBool : FsmStateAction
	{
		// Token: 0x0600A37E RID: 41854 RVA: 0x0033F3B1 File Offset: 0x0033D5B1
		public override void Reset()
		{
			this.storeResult = null;
		}

		// Token: 0x0600A37F RID: 41855 RVA: 0x0033F3BA File Offset: 0x0033D5BA
		public override void OnEnter()
		{
			this.storeResult.Value = (UnityEngine.Random.Range(0, 100) < 50);
			base.Finish();
		}

		// Token: 0x040089AC RID: 35244
		[UIHint(UIHint.Variable)]
		public FsmBool storeResult;
	}
}
