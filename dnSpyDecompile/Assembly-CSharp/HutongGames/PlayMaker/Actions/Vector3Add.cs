using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EB4 RID: 3764
	[ActionCategory(ActionCategory.Vector3)]
	[Tooltip("Adds a value to Vector3 Variable.")]
	public class Vector3Add : FsmStateAction
	{
		// Token: 0x0600AA17 RID: 43543 RVA: 0x003544FC File Offset: 0x003526FC
		public override void Reset()
		{
			this.vector3Variable = null;
			this.addVector = new FsmVector3
			{
				UseVariable = true
			};
			this.everyFrame = false;
			this.perSecond = false;
		}

		// Token: 0x0600AA18 RID: 43544 RVA: 0x00354525 File Offset: 0x00352725
		public override void OnEnter()
		{
			this.DoVector3Add();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AA19 RID: 43545 RVA: 0x0035453B File Offset: 0x0035273B
		public override void OnUpdate()
		{
			this.DoVector3Add();
		}

		// Token: 0x0600AA1A RID: 43546 RVA: 0x00354544 File Offset: 0x00352744
		private void DoVector3Add()
		{
			if (this.perSecond)
			{
				this.vector3Variable.Value = this.vector3Variable.Value + this.addVector.Value * Time.deltaTime;
				return;
			}
			this.vector3Variable.Value = this.vector3Variable.Value + this.addVector.Value;
		}

		// Token: 0x040090B4 RID: 37044
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3 vector3Variable;

		// Token: 0x040090B5 RID: 37045
		[RequiredField]
		public FsmVector3 addVector;

		// Token: 0x040090B6 RID: 37046
		public bool everyFrame;

		// Token: 0x040090B7 RID: 37047
		public bool perSecond;
	}
}
