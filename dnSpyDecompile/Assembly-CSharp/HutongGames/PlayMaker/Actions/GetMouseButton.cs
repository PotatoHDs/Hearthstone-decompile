using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C72 RID: 3186
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Gets the pressed state of the specified Mouse Button and stores it in a Bool Variable. See Unity Input Manager doc.")]
	public class GetMouseButton : FsmStateAction
	{
		// Token: 0x06009F85 RID: 40837 RVA: 0x0032EC13 File Offset: 0x0032CE13
		public override void Reset()
		{
			this.button = MouseButton.Left;
			this.storeResult = null;
		}

		// Token: 0x06009F86 RID: 40838 RVA: 0x0032EC23 File Offset: 0x0032CE23
		public override void OnEnter()
		{
			this.storeResult.Value = Input.GetMouseButton((int)this.button);
		}

		// Token: 0x06009F87 RID: 40839 RVA: 0x0032EC23 File Offset: 0x0032CE23
		public override void OnUpdate()
		{
			this.storeResult.Value = Input.GetMouseButton((int)this.button);
		}

		// Token: 0x04008520 RID: 34080
		[RequiredField]
		[Tooltip("The mouse button to test.")]
		public MouseButton button;

		// Token: 0x04008521 RID: 34081
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the pressed state in a Bool Variable.")]
		public FsmBool storeResult;
	}
}
