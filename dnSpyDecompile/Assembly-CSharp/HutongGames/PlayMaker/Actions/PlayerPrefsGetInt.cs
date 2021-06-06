using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D2B RID: 3371
	[ActionCategory("PlayerPrefs")]
	[Tooltip("Returns the value corresponding to key in the preference file if it exists.")]
	public class PlayerPrefsGetInt : FsmStateAction
	{
		// Token: 0x0600A2D6 RID: 41686 RVA: 0x0033DAE0 File Offset: 0x0033BCE0
		public override void Reset()
		{
			this.keys = new FsmString[1];
			this.variables = new FsmInt[1];
		}

		// Token: 0x0600A2D7 RID: 41687 RVA: 0x0033DAFC File Offset: 0x0033BCFC
		public override void OnEnter()
		{
			for (int i = 0; i < this.keys.Length; i++)
			{
				if (!this.keys[i].IsNone || !this.keys[i].Value.Equals(""))
				{
					this.variables[i].Value = PlayerPrefs.GetInt(this.keys[i].Value, this.variables[i].IsNone ? 0 : this.variables[i].Value);
				}
			}
			base.Finish();
		}

		// Token: 0x04008934 RID: 35124
		[CompoundArray("Count", "Key", "Variable")]
		[Tooltip("Case sensitive key.")]
		public FsmString[] keys;

		// Token: 0x04008935 RID: 35125
		[UIHint(UIHint.Variable)]
		public FsmInt[] variables;
	}
}
