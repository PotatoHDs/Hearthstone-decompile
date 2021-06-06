using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E4E RID: 3662
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets various properties of a UI Layout Element component.")]
	public class UiLayoutElementGetValues : ComponentAction<LayoutElement>
	{
		// Token: 0x0600A82A RID: 43050 RVA: 0x0034E9CC File Offset: 0x0034CBCC
		public override void Reset()
		{
			this.gameObject = null;
			this.ignoreLayout = null;
			this.minWidthEnabled = null;
			this.minHeightEnabled = null;
			this.preferredWidthEnabled = null;
			this.preferredHeightEnabled = null;
			this.flexibleWidthEnabled = null;
			this.flexibleHeightEnabled = null;
			this.minWidth = null;
			this.minHeight = null;
			this.preferredWidth = null;
			this.preferredHeight = null;
			this.flexibleWidth = null;
			this.flexibleHeight = null;
		}

		// Token: 0x0600A82B RID: 43051 RVA: 0x0034EA3C File Offset: 0x0034CC3C
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.layoutElement = this.cachedComponent;
			}
			this.DoGetValues();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A82C RID: 43052 RVA: 0x0034EA84 File Offset: 0x0034CC84
		public override void OnUpdate()
		{
			this.DoGetValues();
		}

		// Token: 0x0600A82D RID: 43053 RVA: 0x0034EA8C File Offset: 0x0034CC8C
		private void DoGetValues()
		{
			if (this.layoutElement == null)
			{
				return;
			}
			if (!this.ignoreLayout.IsNone)
			{
				this.ignoreLayout.Value = this.layoutElement.ignoreLayout;
			}
			if (!this.minWidthEnabled.IsNone)
			{
				this.minWidthEnabled.Value = (this.layoutElement.minWidth != 0f);
			}
			if (!this.minWidth.IsNone)
			{
				this.minWidth.Value = this.layoutElement.minWidth;
			}
			if (!this.minHeightEnabled.IsNone)
			{
				this.minHeightEnabled.Value = (this.layoutElement.minHeight != 0f);
			}
			if (!this.minHeight.IsNone)
			{
				this.minHeight.Value = this.layoutElement.minHeight;
			}
			if (!this.preferredWidthEnabled.IsNone)
			{
				this.preferredWidthEnabled.Value = (this.layoutElement.preferredWidth != 0f);
			}
			if (!this.preferredWidth.IsNone)
			{
				this.preferredWidth.Value = this.layoutElement.preferredWidth;
			}
			if (!this.preferredHeightEnabled.IsNone)
			{
				this.preferredHeightEnabled.Value = (this.layoutElement.preferredHeight != 0f);
			}
			if (!this.preferredHeight.IsNone)
			{
				this.preferredHeight.Value = this.layoutElement.preferredHeight;
			}
			if (!this.flexibleWidthEnabled.IsNone)
			{
				this.flexibleWidthEnabled.Value = (this.layoutElement.flexibleWidth != 0f);
			}
			if (!this.flexibleWidth.IsNone)
			{
				this.flexibleWidth.Value = this.layoutElement.flexibleWidth;
			}
			if (!this.flexibleHeightEnabled.IsNone)
			{
				this.flexibleHeightEnabled.Value = (this.layoutElement.flexibleHeight != 0f);
			}
			if (!this.flexibleHeight.IsNone)
			{
				this.flexibleHeight.Value = this.layoutElement.flexibleHeight;
			}
		}

		// Token: 0x04008E99 RID: 36505
		[RequiredField]
		[CheckForComponent(typeof(LayoutElement))]
		[Tooltip("The GameObject with the UI LayoutElement component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008E9A RID: 36506
		[ActionSection("Values")]
		[Tooltip("Is this element use Layout constraints")]
		[UIHint(UIHint.Variable)]
		public FsmBool ignoreLayout;

		// Token: 0x04008E9B RID: 36507
		[Tooltip("The minimum width enabled state")]
		[UIHint(UIHint.Variable)]
		public FsmBool minWidthEnabled;

		// Token: 0x04008E9C RID: 36508
		[Tooltip("The minimum width this layout element should have.")]
		[UIHint(UIHint.Variable)]
		public FsmFloat minWidth;

		// Token: 0x04008E9D RID: 36509
		[Tooltip("The minimum height enabled state")]
		[UIHint(UIHint.Variable)]
		public FsmBool minHeightEnabled;

		// Token: 0x04008E9E RID: 36510
		[Tooltip("The minimum height this layout element should have.")]
		[UIHint(UIHint.Variable)]
		public FsmFloat minHeight;

		// Token: 0x04008E9F RID: 36511
		[Tooltip("The preferred width enabled state")]
		[UIHint(UIHint.Variable)]
		public FsmBool preferredWidthEnabled;

		// Token: 0x04008EA0 RID: 36512
		[Tooltip("The preferred width this layout element should have before additional available width is allocated.")]
		[UIHint(UIHint.Variable)]
		public FsmFloat preferredWidth;

		// Token: 0x04008EA1 RID: 36513
		[Tooltip("The preferred height enabled state")]
		[UIHint(UIHint.Variable)]
		public FsmBool preferredHeightEnabled;

		// Token: 0x04008EA2 RID: 36514
		[Tooltip("The preferred height this layout element should have before additional available height is allocated.")]
		[UIHint(UIHint.Variable)]
		public FsmFloat preferredHeight;

		// Token: 0x04008EA3 RID: 36515
		[Tooltip("The flexible width enabled state")]
		[UIHint(UIHint.Variable)]
		public FsmBool flexibleWidthEnabled;

		// Token: 0x04008EA4 RID: 36516
		[Tooltip("The relative amount of additional available width this layout element should fill out relative to its siblings.")]
		[UIHint(UIHint.Variable)]
		public FsmFloat flexibleWidth;

		// Token: 0x04008EA5 RID: 36517
		[Tooltip("The flexible height enabled state")]
		[UIHint(UIHint.Variable)]
		public FsmBool flexibleHeightEnabled;

		// Token: 0x04008EA6 RID: 36518
		[Tooltip("The relative amount of additional available height this layout element should fill out relative to its siblings.")]
		[UIHint(UIHint.Variable)]
		public FsmFloat flexibleHeight;

		// Token: 0x04008EA7 RID: 36519
		[ActionSection("Options")]
		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		// Token: 0x04008EA8 RID: 36520
		private LayoutElement layoutElement;
	}
}
