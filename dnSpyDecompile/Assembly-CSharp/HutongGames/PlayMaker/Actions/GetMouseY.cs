using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C76 RID: 3190
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Gets the Y Position of the mouse and stores it in a Float Variable.")]
	public class GetMouseY : FsmStateAction
	{
		// Token: 0x06009F98 RID: 40856 RVA: 0x0032ED75 File Offset: 0x0032CF75
		public override void Reset()
		{
			this.storeResult = null;
			this.normalize = true;
		}

		// Token: 0x06009F99 RID: 40857 RVA: 0x0032ED85 File Offset: 0x0032CF85
		public override void OnEnter()
		{
			this.DoGetMouseY();
		}

		// Token: 0x06009F9A RID: 40858 RVA: 0x0032ED85 File Offset: 0x0032CF85
		public override void OnUpdate()
		{
			this.DoGetMouseY();
		}

		// Token: 0x06009F9B RID: 40859 RVA: 0x0032ED90 File Offset: 0x0032CF90
		private void DoGetMouseY()
		{
			if (this.storeResult != null)
			{
				float num = Input.mousePosition.y;
				if (this.normalize)
				{
					num /= (float)Screen.height;
				}
				this.storeResult.Value = num;
			}
		}

		// Token: 0x0400852C RID: 34092
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat storeResult;

		// Token: 0x0400852D RID: 34093
		public bool normalize;
	}
}
