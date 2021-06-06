using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E99 RID: 3737
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the text value of a UI Text component.")]
	public class UiTextSetText : ComponentAction<Text>
	{
		// Token: 0x0600A9A0 RID: 43424 RVA: 0x003531B2 File Offset: 0x003513B2
		public override void Reset()
		{
			this.gameObject = null;
			this.text = null;
			this.resetOnExit = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A9A1 RID: 43425 RVA: 0x003531D0 File Offset: 0x003513D0
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.uiText = this.cachedComponent;
			}
			this.originalString = this.uiText.text;
			this.DoSetTextValue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A9A2 RID: 43426 RVA: 0x00353229 File Offset: 0x00351429
		public override void OnUpdate()
		{
			this.DoSetTextValue();
		}

		// Token: 0x0600A9A3 RID: 43427 RVA: 0x00353231 File Offset: 0x00351431
		private void DoSetTextValue()
		{
			if (this.uiText == null)
			{
				return;
			}
			this.uiText.text = this.text.Value;
		}

		// Token: 0x0600A9A4 RID: 43428 RVA: 0x00353258 File Offset: 0x00351458
		public override void OnExit()
		{
			if (this.uiText == null)
			{
				return;
			}
			if (this.resetOnExit.Value)
			{
				this.uiText.text = this.originalString;
			}
		}

		// Token: 0x0400904B RID: 36939
		[RequiredField]
		[CheckForComponent(typeof(Text))]
		[Tooltip("The GameObject with the UI Text component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400904C RID: 36940
		[UIHint(UIHint.TextArea)]
		[Tooltip("The text of the UI Text component.")]
		public FsmString text;

		// Token: 0x0400904D RID: 36941
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		// Token: 0x0400904E RID: 36942
		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		// Token: 0x0400904F RID: 36943
		private Text uiText;

		// Token: 0x04009050 RID: 36944
		private string originalString;
	}
}
