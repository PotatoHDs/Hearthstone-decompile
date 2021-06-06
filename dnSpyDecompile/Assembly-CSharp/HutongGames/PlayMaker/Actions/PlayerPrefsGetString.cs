using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D2C RID: 3372
	[ActionCategory("PlayerPrefs")]
	[Tooltip("Returns the value corresponding to key in the preference file if it exists.")]
	public class PlayerPrefsGetString : FsmStateAction
	{
		// Token: 0x0600A2D9 RID: 41689 RVA: 0x0033DB88 File Offset: 0x0033BD88
		public override void Reset()
		{
			this.keys = new FsmString[1];
			this.variables = new FsmString[1];
		}

		// Token: 0x0600A2DA RID: 41690 RVA: 0x0033DBA4 File Offset: 0x0033BDA4
		public override void OnEnter()
		{
			for (int i = 0; i < this.keys.Length; i++)
			{
				if (!this.keys[i].IsNone || !this.keys[i].Value.Equals(""))
				{
					this.variables[i].Value = PlayerPrefs.GetString(this.keys[i].Value, this.variables[i].IsNone ? "" : this.variables[i].Value);
				}
			}
			base.Finish();
		}

		// Token: 0x04008936 RID: 35126
		[CompoundArray("Count", "Key", "Variable")]
		[Tooltip("Case sensitive key.")]
		public FsmString[] keys;

		// Token: 0x04008937 RID: 35127
		[UIHint(UIHint.Variable)]
		public FsmString[] variables;
	}
}
