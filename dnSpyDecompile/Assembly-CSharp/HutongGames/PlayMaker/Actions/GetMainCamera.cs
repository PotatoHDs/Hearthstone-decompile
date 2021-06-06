using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C6E RID: 3182
	[ActionCategory(ActionCategory.Camera)]
	[ActionTarget(typeof(Camera), "storeGameObject", false)]
	[Tooltip("Gets the GameObject tagged MainCamera from the scene")]
	public class GetMainCamera : FsmStateAction
	{
		// Token: 0x06009F76 RID: 40822 RVA: 0x0032E8D9 File Offset: 0x0032CAD9
		public override void Reset()
		{
			this.storeGameObject = null;
		}

		// Token: 0x06009F77 RID: 40823 RVA: 0x0032E8E2 File Offset: 0x0032CAE2
		public override void OnEnter()
		{
			this.storeGameObject.Value = ((Camera.main != null) ? Camera.main.gameObject : null);
			base.Finish();
		}

		// Token: 0x04008514 RID: 34068
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmGameObject storeGameObject;
	}
}
