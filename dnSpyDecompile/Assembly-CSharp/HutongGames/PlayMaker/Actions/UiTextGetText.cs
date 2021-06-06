using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E98 RID: 3736
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets the text value of a UI Text component.")]
	public class UiTextGetText : ComponentAction<Text>
	{
		// Token: 0x0600A99B RID: 43419 RVA: 0x00353122 File Offset: 0x00351322
		public override void Reset()
		{
			this.text = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A99C RID: 43420 RVA: 0x00353134 File Offset: 0x00351334
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.uiText = this.cachedComponent;
			}
			this.DoGetTextValue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A99D RID: 43421 RVA: 0x0035317C File Offset: 0x0035137C
		public override void OnUpdate()
		{
			this.DoGetTextValue();
		}

		// Token: 0x0600A99E RID: 43422 RVA: 0x00353184 File Offset: 0x00351384
		private void DoGetTextValue()
		{
			if (this.uiText != null)
			{
				this.text.Value = this.uiText.text;
			}
		}

		// Token: 0x04009047 RID: 36935
		[RequiredField]
		[CheckForComponent(typeof(Text))]
		[Tooltip("The GameObject with the UI Text component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009048 RID: 36936
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The text value of the UI Text component.")]
		public FsmString text;

		// Token: 0x04009049 RID: 36937
		[Tooltip("Runs every frame. Useful to animate values over time.")]
		public bool everyFrame;

		// Token: 0x0400904A RID: 36938
		private Text uiText;
	}
}
