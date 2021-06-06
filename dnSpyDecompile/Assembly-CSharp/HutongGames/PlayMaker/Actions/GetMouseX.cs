using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C75 RID: 3189
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Gets the X Position of the mouse and stores it in a Float Variable.")]
	public class GetMouseX : FsmStateAction
	{
		// Token: 0x06009F93 RID: 40851 RVA: 0x0032ED1D File Offset: 0x0032CF1D
		public override void Reset()
		{
			this.storeResult = null;
			this.normalize = true;
		}

		// Token: 0x06009F94 RID: 40852 RVA: 0x0032ED2D File Offset: 0x0032CF2D
		public override void OnEnter()
		{
			this.DoGetMouseX();
		}

		// Token: 0x06009F95 RID: 40853 RVA: 0x0032ED2D File Offset: 0x0032CF2D
		public override void OnUpdate()
		{
			this.DoGetMouseX();
		}

		// Token: 0x06009F96 RID: 40854 RVA: 0x0032ED38 File Offset: 0x0032CF38
		private void DoGetMouseX()
		{
			if (this.storeResult != null)
			{
				float num = Input.mousePosition.x;
				if (this.normalize)
				{
					num /= (float)Screen.width;
				}
				this.storeResult.Value = num;
			}
		}

		// Token: 0x0400852A RID: 34090
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat storeResult;

		// Token: 0x0400852B RID: 34091
		public bool normalize;
	}
}
