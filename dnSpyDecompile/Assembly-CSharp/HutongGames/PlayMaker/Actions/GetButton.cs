using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C47 RID: 3143
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Gets the pressed state of the specified Button and stores it in a Bool Variable. See Unity Input Manager docs.")]
	public class GetButton : FsmStateAction
	{
		// Token: 0x06009EC4 RID: 40644 RVA: 0x0032C662 File Offset: 0x0032A862
		public override void Reset()
		{
			this.buttonName = "Fire1";
			this.storeResult = null;
			this.everyFrame = true;
		}

		// Token: 0x06009EC5 RID: 40645 RVA: 0x0032C682 File Offset: 0x0032A882
		public override void OnEnter()
		{
			this.DoGetButton();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009EC6 RID: 40646 RVA: 0x0032C698 File Offset: 0x0032A898
		public override void OnUpdate()
		{
			this.DoGetButton();
		}

		// Token: 0x06009EC7 RID: 40647 RVA: 0x0032C6A0 File Offset: 0x0032A8A0
		private void DoGetButton()
		{
			this.storeResult.Value = Input.GetButton(this.buttonName.Value);
		}

		// Token: 0x04008424 RID: 33828
		[RequiredField]
		[Tooltip("The name of the button. Set in the Unity Input Manager.")]
		public FsmString buttonName;

		// Token: 0x04008425 RID: 33829
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a bool variable.")]
		public FsmBool storeResult;

		// Token: 0x04008426 RID: 33830
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
