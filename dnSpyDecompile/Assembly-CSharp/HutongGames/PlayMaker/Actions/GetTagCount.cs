using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C8E RID: 3214
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Gets the number of Game Objects in the scene with the specified Tag.")]
	public class GetTagCount : FsmStateAction
	{
		// Token: 0x0600A001 RID: 40961 RVA: 0x0032FB26 File Offset: 0x0032DD26
		public override void Reset()
		{
			this.tag = "Untagged";
			this.storeResult = null;
		}

		// Token: 0x0600A002 RID: 40962 RVA: 0x0032FB40 File Offset: 0x0032DD40
		public override void OnEnter()
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag(this.tag.Value);
			if (this.storeResult != null)
			{
				this.storeResult.Value = ((array != null) ? array.Length : 0);
			}
			base.Finish();
		}

		// Token: 0x04008581 RID: 34177
		[UIHint(UIHint.Tag)]
		public FsmString tag;

		// Token: 0x04008582 RID: 34178
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmInt storeResult;
	}
}
