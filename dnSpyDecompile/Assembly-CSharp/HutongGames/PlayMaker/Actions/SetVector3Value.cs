using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DFF RID: 3583
	[ActionCategory(ActionCategory.Vector3)]
	[Tooltip("Sets the value of a Vector3 Variable.")]
	public class SetVector3Value : FsmStateAction
	{
		// Token: 0x0600A6C6 RID: 42694 RVA: 0x0034A3FE File Offset: 0x003485FE
		public override void Reset()
		{
			this.vector3Variable = null;
			this.vector3Value = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A6C7 RID: 42695 RVA: 0x0034A415 File Offset: 0x00348615
		public override void OnEnter()
		{
			this.vector3Variable.Value = this.vector3Value.Value;
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A6C8 RID: 42696 RVA: 0x0034A43B File Offset: 0x0034863B
		public override void OnUpdate()
		{
			this.vector3Variable.Value = this.vector3Value.Value;
		}

		// Token: 0x04008D43 RID: 36163
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3 vector3Variable;

		// Token: 0x04008D44 RID: 36164
		[RequiredField]
		public FsmVector3 vector3Value;

		// Token: 0x04008D45 RID: 36165
		public bool everyFrame;
	}
}
