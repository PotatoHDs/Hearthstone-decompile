using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E5F RID: 3679
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Get the selected value (zero based index), sprite and text from a UI Dropdown Component")]
	public class UiDropDownGetSelectedData : ComponentAction<Dropdown>
	{
		// Token: 0x0600A87C RID: 43132 RVA: 0x003502DF File Offset: 0x0034E4DF
		public override void Reset()
		{
			this.gameObject = null;
			this.index = null;
			this.getText = null;
			this.getImage = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A87D RID: 43133 RVA: 0x00350304 File Offset: 0x0034E504
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.dropDown = this.cachedComponent;
			}
			this.GetValue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A87E RID: 43134 RVA: 0x0035034C File Offset: 0x0034E54C
		public override void OnUpdate()
		{
			this.GetValue();
		}

		// Token: 0x0600A87F RID: 43135 RVA: 0x00350354 File Offset: 0x0034E554
		private void GetValue()
		{
			if (this.dropDown == null)
			{
				return;
			}
			if (!this.index.IsNone)
			{
				this.index.Value = this.dropDown.value;
			}
			if (!this.getText.IsNone)
			{
				this.getText.Value = this.dropDown.options[this.dropDown.value].text;
			}
			if (!this.getImage.IsNone)
			{
				this.getImage.Value = this.dropDown.options[this.dropDown.value].image;
			}
		}

		// Token: 0x04008F18 RID: 36632
		[RequiredField]
		[CheckForComponent(typeof(Dropdown))]
		[Tooltip("The GameObject with the UI DropDown component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008F19 RID: 36633
		[Tooltip("The selected index of the dropdown (zero based index).")]
		[UIHint(UIHint.Variable)]
		public FsmInt index;

		// Token: 0x04008F1A RID: 36634
		[Tooltip("The selected text.")]
		[UIHint(UIHint.Variable)]
		public FsmString getText;

		// Token: 0x04008F1B RID: 36635
		[ObjectType(typeof(Sprite))]
		[Tooltip("The selected text.")]
		[UIHint(UIHint.Variable)]
		public FsmObject getImage;

		// Token: 0x04008F1C RID: 36636
		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		// Token: 0x04008F1D RID: 36637
		private Dropdown dropDown;
	}
}
