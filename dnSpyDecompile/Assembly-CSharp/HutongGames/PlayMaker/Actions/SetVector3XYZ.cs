using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E00 RID: 3584
	[ActionCategory(ActionCategory.Vector3)]
	[Tooltip("Sets the XYZ channels of a Vector3 Variable. To leave any channel unchanged, set variable to 'None'.")]
	public class SetVector3XYZ : FsmStateAction
	{
		// Token: 0x0600A6CA RID: 42698 RVA: 0x0034A454 File Offset: 0x00348654
		public override void Reset()
		{
			this.vector3Variable = null;
			this.vector3Value = null;
			this.x = new FsmFloat
			{
				UseVariable = true
			};
			this.y = new FsmFloat
			{
				UseVariable = true
			};
			this.z = new FsmFloat
			{
				UseVariable = true
			};
			this.everyFrame = false;
		}

		// Token: 0x0600A6CB RID: 42699 RVA: 0x0034A4AC File Offset: 0x003486AC
		public override void OnEnter()
		{
			this.DoSetVector3XYZ();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A6CC RID: 42700 RVA: 0x0034A4C2 File Offset: 0x003486C2
		public override void OnUpdate()
		{
			this.DoSetVector3XYZ();
		}

		// Token: 0x0600A6CD RID: 42701 RVA: 0x0034A4CC File Offset: 0x003486CC
		private void DoSetVector3XYZ()
		{
			if (this.vector3Variable == null)
			{
				return;
			}
			Vector3 value = this.vector3Variable.Value;
			if (!this.vector3Value.IsNone)
			{
				value = this.vector3Value.Value;
			}
			if (!this.x.IsNone)
			{
				value.x = this.x.Value;
			}
			if (!this.y.IsNone)
			{
				value.y = this.y.Value;
			}
			if (!this.z.IsNone)
			{
				value.z = this.z.Value;
			}
			this.vector3Variable.Value = value;
		}

		// Token: 0x04008D46 RID: 36166
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3 vector3Variable;

		// Token: 0x04008D47 RID: 36167
		[UIHint(UIHint.Variable)]
		public FsmVector3 vector3Value;

		// Token: 0x04008D48 RID: 36168
		public FsmFloat x;

		// Token: 0x04008D49 RID: 36169
		public FsmFloat y;

		// Token: 0x04008D4A RID: 36170
		public FsmFloat z;

		// Token: 0x04008D4B RID: 36171
		public bool everyFrame;
	}
}
