using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E60 RID: 3680
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Set the selected value (zero based index) of the UI Dropdown Component")]
	public class UiDropDownSetValue : ComponentAction<Dropdown>
	{
		// Token: 0x0600A881 RID: 43137 RVA: 0x00350403 File Offset: 0x0034E603
		public override void Reset()
		{
			this.gameObject = null;
			this.value = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A882 RID: 43138 RVA: 0x0035041C File Offset: 0x0034E61C
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.dropDown = this.cachedComponent;
			}
			this.SetValue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A883 RID: 43139 RVA: 0x00350464 File Offset: 0x0034E664
		public override void OnUpdate()
		{
			this.SetValue();
		}

		// Token: 0x0600A884 RID: 43140 RVA: 0x0035046C File Offset: 0x0034E66C
		private void SetValue()
		{
			if (this.dropDown == null)
			{
				return;
			}
			if (this.dropDown.value != this.value.Value)
			{
				this.dropDown.value = this.value.Value;
			}
		}

		// Token: 0x04008F1E RID: 36638
		[RequiredField]
		[CheckForComponent(typeof(Dropdown))]
		[Tooltip("The GameObject with the UI DropDown component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008F1F RID: 36639
		[RequiredField]
		[Tooltip("The selected index of the dropdown (zero based index).")]
		public FsmInt value;

		// Token: 0x04008F20 RID: 36640
		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		// Token: 0x04008F21 RID: 36641
		private Dropdown dropDown;
	}
}
