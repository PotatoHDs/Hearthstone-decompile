using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CB3 RID: 3251
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("GUILayout Label for a Float Variable.")]
	public class GUILayoutFloatLabel : GUILayoutAction
	{
		// Token: 0x0600A082 RID: 41090 RVA: 0x003314F1 File Offset: 0x0032F6F1
		public override void Reset()
		{
			base.Reset();
			this.prefix = "";
			this.style = "";
			this.floatVariable = null;
		}

		// Token: 0x0600A083 RID: 41091 RVA: 0x00331520 File Offset: 0x0032F720
		public override void OnGUI()
		{
			if (string.IsNullOrEmpty(this.style.Value))
			{
				GUILayout.Label(new GUIContent(this.prefix.Value + this.floatVariable.Value), base.LayoutOptions);
				return;
			}
			GUILayout.Label(new GUIContent(this.prefix.Value + this.floatVariable.Value), this.style.Value, base.LayoutOptions);
		}

		// Token: 0x04008607 RID: 34311
		[Tooltip("Text to put before the float variable.")]
		public FsmString prefix;

		// Token: 0x04008608 RID: 34312
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Float variable to display.")]
		public FsmFloat floatVariable;

		// Token: 0x04008609 RID: 34313
		[Tooltip("Optional GUIStyle in the active GUISKin.")]
		public FsmString style;
	}
}
