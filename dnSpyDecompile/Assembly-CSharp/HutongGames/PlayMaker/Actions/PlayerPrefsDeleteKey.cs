using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D29 RID: 3369
	[ActionCategory("PlayerPrefs")]
	[Tooltip("Removes key and its corresponding value from the preferences.")]
	public class PlayerPrefsDeleteKey : FsmStateAction
	{
		// Token: 0x0600A2D0 RID: 41680 RVA: 0x0033D9E8 File Offset: 0x0033BBE8
		public override void Reset()
		{
			this.key = "";
		}

		// Token: 0x0600A2D1 RID: 41681 RVA: 0x0033D9FA File Offset: 0x0033BBFA
		public override void OnEnter()
		{
			if (!this.key.IsNone && !this.key.Value.Equals(""))
			{
				PlayerPrefs.DeleteKey(this.key.Value);
			}
			base.Finish();
		}

		// Token: 0x04008931 RID: 35121
		public FsmString key;
	}
}
