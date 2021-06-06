using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D2F RID: 3375
	[ActionCategory("PlayerPrefs")]
	[Tooltip("Sets the value of the preference identified by key.")]
	public class PlayerPrefsSetInt : FsmStateAction
	{
		// Token: 0x0600A2E2 RID: 41698 RVA: 0x0033DD5F File Offset: 0x0033BF5F
		public override void Reset()
		{
			this.keys = new FsmString[1];
			this.values = new FsmInt[1];
		}

		// Token: 0x0600A2E3 RID: 41699 RVA: 0x0033DD7C File Offset: 0x0033BF7C
		public override void OnEnter()
		{
			for (int i = 0; i < this.keys.Length; i++)
			{
				if (!this.keys[i].IsNone || !this.keys[i].Value.Equals(""))
				{
					PlayerPrefs.SetInt(this.keys[i].Value, this.values[i].IsNone ? 0 : this.values[i].Value);
				}
			}
			base.Finish();
		}

		// Token: 0x0400893E RID: 35134
		[CompoundArray("Count", "Key", "Value")]
		[Tooltip("Case sensitive key.")]
		public FsmString[] keys;

		// Token: 0x0400893F RID: 35135
		public FsmInt[] values;
	}
}
