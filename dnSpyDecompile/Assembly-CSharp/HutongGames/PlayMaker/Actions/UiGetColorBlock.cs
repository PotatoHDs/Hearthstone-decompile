using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E52 RID: 3666
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets the Color Block of a UI Selectable component.")]
	public class UiGetColorBlock : ComponentAction<Selectable>
	{
		// Token: 0x0600A83E RID: 43070 RVA: 0x0034F2AC File Offset: 0x0034D4AC
		public override void Reset()
		{
			this.gameObject = null;
			this.fadeDuration = null;
			this.colorMultiplier = null;
			this.normalColor = null;
			this.highlightedColor = null;
			this.pressedColor = null;
			this.disabledColor = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A83F RID: 43071 RVA: 0x0034F2E8 File Offset: 0x0034D4E8
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.selectable = this.cachedComponent;
			}
			this.DoGetValue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A840 RID: 43072 RVA: 0x0034F330 File Offset: 0x0034D530
		public override void OnUpdate()
		{
			this.DoGetValue();
		}

		// Token: 0x0600A841 RID: 43073 RVA: 0x0034F338 File Offset: 0x0034D538
		private void DoGetValue()
		{
			if (this.selectable == null)
			{
				return;
			}
			if (!this.colorMultiplier.IsNone)
			{
				this.colorMultiplier.Value = this.selectable.colors.colorMultiplier;
			}
			if (!this.fadeDuration.IsNone)
			{
				this.fadeDuration.Value = this.selectable.colors.fadeDuration;
			}
			if (!this.normalColor.IsNone)
			{
				this.normalColor.Value = this.selectable.colors.normalColor;
			}
			if (!this.pressedColor.IsNone)
			{
				this.pressedColor.Value = this.selectable.colors.pressedColor;
			}
			if (!this.highlightedColor.IsNone)
			{
				this.highlightedColor.Value = this.selectable.colors.highlightedColor;
			}
			if (!this.disabledColor.IsNone)
			{
				this.disabledColor.Value = this.selectable.colors.disabledColor;
			}
		}

		// Token: 0x04008EC1 RID: 36545
		[RequiredField]
		[CheckForComponent(typeof(Selectable))]
		[Tooltip("The GameObject with the UI Selectable component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008EC2 RID: 36546
		[Tooltip("The fade duration value. Leave as None for no effect")]
		[UIHint(UIHint.Variable)]
		public FsmFloat fadeDuration;

		// Token: 0x04008EC3 RID: 36547
		[Tooltip("The color multiplier value. Leave as None for no effect")]
		[UIHint(UIHint.Variable)]
		public FsmFloat colorMultiplier;

		// Token: 0x04008EC4 RID: 36548
		[Tooltip("The normal color value. Leave as None for no effect")]
		[UIHint(UIHint.Variable)]
		public FsmColor normalColor;

		// Token: 0x04008EC5 RID: 36549
		[Tooltip("The pressed color value. Leave as None for no effect")]
		[UIHint(UIHint.Variable)]
		public FsmColor pressedColor;

		// Token: 0x04008EC6 RID: 36550
		[Tooltip("The highlighted color value. Leave as None for no effect")]
		[UIHint(UIHint.Variable)]
		public FsmColor highlightedColor;

		// Token: 0x04008EC7 RID: 36551
		[Tooltip("The disabled color value. Leave as None for no effect")]
		[UIHint(UIHint.Variable)]
		public FsmColor disabledColor;

		// Token: 0x04008EC8 RID: 36552
		[Tooltip("Repeats every frame, useful for animation")]
		public bool everyFrame;

		// Token: 0x04008EC9 RID: 36553
		private Selectable selectable;
	}
}
