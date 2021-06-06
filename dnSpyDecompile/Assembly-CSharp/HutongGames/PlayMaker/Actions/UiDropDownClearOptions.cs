using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E5E RID: 3678
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Clear the list of options in a UI Dropdown Component")]
	public class UiDropDownClearOptions : ComponentAction<Dropdown>
	{
		// Token: 0x0600A879 RID: 43129 RVA: 0x00350280 File Offset: 0x0034E480
		public override void Reset()
		{
			this.gameObject = null;
		}

		// Token: 0x0600A87A RID: 43130 RVA: 0x0035028C File Offset: 0x0034E48C
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.dropDown = this.cachedComponent;
			}
			if (this.dropDown != null)
			{
				this.dropDown.ClearOptions();
			}
			base.Finish();
		}

		// Token: 0x04008F16 RID: 36630
		[RequiredField]
		[CheckForComponent(typeof(Dropdown))]
		[Tooltip("The GameObject with the UI DropDown component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008F17 RID: 36631
		private Dropdown dropDown;
	}
}
