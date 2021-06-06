using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D2A RID: 3370
	[ActionCategory("PlayerPrefs")]
	[Tooltip("Returns the value corresponding to key in the preference file if it exists.")]
	public class PlayerPrefsGetFloat : FsmStateAction
	{
		// Token: 0x0600A2D3 RID: 41683 RVA: 0x0033DA36 File Offset: 0x0033BC36
		public override void Reset()
		{
			this.keys = new FsmString[1];
			this.variables = new FsmFloat[1];
		}

		// Token: 0x0600A2D4 RID: 41684 RVA: 0x0033DA50 File Offset: 0x0033BC50
		public override void OnEnter()
		{
			for (int i = 0; i < this.keys.Length; i++)
			{
				if (!this.keys[i].IsNone || !this.keys[i].Value.Equals(""))
				{
					this.variables[i].Value = PlayerPrefs.GetFloat(this.keys[i].Value, this.variables[i].IsNone ? 0f : this.variables[i].Value);
				}
			}
			base.Finish();
		}

		// Token: 0x04008932 RID: 35122
		[CompoundArray("Count", "Key", "Variable")]
		[Tooltip("Case sensitive key.")]
		public FsmString[] keys;

		// Token: 0x04008933 RID: 35123
		[UIHint(UIHint.Variable)]
		public FsmFloat[] variables;
	}
}
