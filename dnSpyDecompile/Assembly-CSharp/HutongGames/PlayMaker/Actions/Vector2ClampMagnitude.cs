using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EA7 RID: 3751
	[ActionCategory(ActionCategory.Vector2)]
	[Tooltip("Clamps the Magnitude of Vector2 Variable.")]
	public class Vector2ClampMagnitude : FsmStateAction
	{
		// Token: 0x0600A9E0 RID: 43488 RVA: 0x00353A9F File Offset: 0x00351C9F
		public override void Reset()
		{
			this.vector2Variable = null;
			this.maxLength = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A9E1 RID: 43489 RVA: 0x00353AB6 File Offset: 0x00351CB6
		public override void OnEnter()
		{
			this.DoVector2ClampMagnitude();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A9E2 RID: 43490 RVA: 0x00353ACC File Offset: 0x00351CCC
		public override void OnUpdate()
		{
			this.DoVector2ClampMagnitude();
		}

		// Token: 0x0600A9E3 RID: 43491 RVA: 0x00353AD4 File Offset: 0x00351CD4
		private void DoVector2ClampMagnitude()
		{
			this.vector2Variable.Value = Vector2.ClampMagnitude(this.vector2Variable.Value, this.maxLength.Value);
		}

		// Token: 0x04009081 RID: 36993
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Vector2")]
		public FsmVector2 vector2Variable;

		// Token: 0x04009082 RID: 36994
		[RequiredField]
		[Tooltip("The maximum Magnitude")]
		public FsmFloat maxLength;

		// Token: 0x04009083 RID: 36995
		[Tooltip("Repeat every frame")]
		public bool everyFrame;
	}
}
