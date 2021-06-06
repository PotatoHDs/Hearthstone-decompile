using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D30 RID: 3376
	[ActionCategory("PlayerPrefs")]
	[Tooltip("Sets the value of the preference identified by key.")]
	public class PlayerPrefsSetString : FsmStateAction
	{
		// Token: 0x0600A2E5 RID: 41701 RVA: 0x0033DDFB File Offset: 0x0033BFFB
		public override void Reset()
		{
			this.keys = new FsmString[1];
			this.values = new FsmString[1];
		}

		// Token: 0x0600A2E6 RID: 41702 RVA: 0x0033DE18 File Offset: 0x0033C018
		public override void OnEnter()
		{
			for (int i = 0; i < this.keys.Length; i++)
			{
				if (!this.keys[i].IsNone || !this.keys[i].Value.Equals(""))
				{
					PlayerPrefs.SetString(this.keys[i].Value, this.values[i].IsNone ? "" : this.values[i].Value);
				}
			}
			base.Finish();
		}

		// Token: 0x04008940 RID: 35136
		[CompoundArray("Count", "Key", "Value")]
		[Tooltip("Case sensitive key.")]
		public FsmString[] keys;

		// Token: 0x04008941 RID: 35137
		public FsmString[] values;
	}
}
