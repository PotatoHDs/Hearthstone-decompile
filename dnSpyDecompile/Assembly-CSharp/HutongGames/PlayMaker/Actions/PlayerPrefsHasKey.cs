using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D2D RID: 3373
	[ActionCategory("PlayerPrefs")]
	[Tooltip("Returns true if key exists in the preferences.")]
	public class PlayerPrefsHasKey : FsmStateAction
	{
		// Token: 0x0600A2DC RID: 41692 RVA: 0x0033DC34 File Offset: 0x0033BE34
		public override void Reset()
		{
			this.key = "";
		}

		// Token: 0x0600A2DD RID: 41693 RVA: 0x0033DC48 File Offset: 0x0033BE48
		public override void OnEnter()
		{
			base.Finish();
			if (!this.key.IsNone && !this.key.Value.Equals(""))
			{
				this.variable.Value = PlayerPrefs.HasKey(this.key.Value);
			}
			base.Fsm.Event(this.variable.Value ? this.trueEvent : this.falseEvent);
		}

		// Token: 0x04008938 RID: 35128
		[RequiredField]
		public FsmString key;

		// Token: 0x04008939 RID: 35129
		[UIHint(UIHint.Variable)]
		[Title("Store Result")]
		public FsmBool variable;

		// Token: 0x0400893A RID: 35130
		[Tooltip("Event to send if key exists.")]
		public FsmEvent trueEvent;

		// Token: 0x0400893B RID: 35131
		[Tooltip("Event to send if key does not exist.")]
		public FsmEvent falseEvent;
	}
}
