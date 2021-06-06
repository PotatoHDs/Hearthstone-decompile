using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D2E RID: 3374
	[ActionCategory("PlayerPrefs")]
	[Tooltip("Sets the value of the preference identified by key.")]
	public class PlayerPrefsSetFloat : FsmStateAction
	{
		// Token: 0x0600A2DF RID: 41695 RVA: 0x0033DCC0 File Offset: 0x0033BEC0
		public override void Reset()
		{
			this.keys = new FsmString[1];
			this.values = new FsmFloat[1];
		}

		// Token: 0x0600A2E0 RID: 41696 RVA: 0x0033DCDC File Offset: 0x0033BEDC
		public override void OnEnter()
		{
			for (int i = 0; i < this.keys.Length; i++)
			{
				if (!this.keys[i].IsNone || !this.keys[i].Value.Equals(""))
				{
					PlayerPrefs.SetFloat(this.keys[i].Value, this.values[i].IsNone ? 0f : this.values[i].Value);
				}
			}
			base.Finish();
		}

		// Token: 0x0400893C RID: 35132
		[CompoundArray("Count", "Key", "Value")]
		[Tooltip("Case sensitive key.")]
		public FsmString[] keys;

		// Token: 0x0400893D RID: 35133
		public FsmFloat[] values;
	}
}
