using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CB6 RID: 3254
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("GUILayout Label for an Int Variable.")]
	public class GUILayoutIntLabel : GUILayoutAction
	{
		// Token: 0x0600A08B RID: 41099 RVA: 0x00331749 File Offset: 0x0032F949
		public override void Reset()
		{
			base.Reset();
			this.prefix = "";
			this.style = "";
			this.intVariable = null;
		}

		// Token: 0x0600A08C RID: 41100 RVA: 0x00331778 File Offset: 0x0032F978
		public override void OnGUI()
		{
			if (string.IsNullOrEmpty(this.style.Value))
			{
				GUILayout.Label(new GUIContent(this.prefix.Value + this.intVariable.Value), base.LayoutOptions);
				return;
			}
			GUILayout.Label(new GUIContent(this.prefix.Value + this.intVariable.Value), this.style.Value, base.LayoutOptions);
		}

		// Token: 0x04008611 RID: 34321
		[Tooltip("Text to put before the int variable.")]
		public FsmString prefix;

		// Token: 0x04008612 RID: 34322
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Int variable to display.")]
		public FsmInt intVariable;

		// Token: 0x04008613 RID: 34323
		[Tooltip("Optional GUIStyle in the active GUISKin.")]
		public FsmString style;
	}
}
