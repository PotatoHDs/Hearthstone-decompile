using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E57 RID: 3671
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the Color Block of a UI Selectable component. Modifications will not be visible if transition is not ColorTint")]
	public class UiSetColorBlock : ComponentAction<Selectable>
	{
		// Token: 0x0600A855 RID: 43093 RVA: 0x0034F970 File Offset: 0x0034DB70
		public override void Reset()
		{
			this.gameObject = null;
			this.fadeDuration = new FsmFloat
			{
				UseVariable = true
			};
			this.colorMultiplier = new FsmFloat
			{
				UseVariable = true
			};
			this.normalColor = new FsmColor
			{
				UseVariable = true
			};
			this.highlightedColor = new FsmColor
			{
				UseVariable = true
			};
			this.pressedColor = new FsmColor
			{
				UseVariable = true
			};
			this.disabledColor = new FsmColor
			{
				UseVariable = true
			};
			this.resetOnExit = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A856 RID: 43094 RVA: 0x0034FA00 File Offset: 0x0034DC00
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.selectable = this.cachedComponent;
			}
			if (this.selectable != null && this.resetOnExit.Value)
			{
				this.originalColorBlock = this.selectable.colors;
			}
			this.DoSetValue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A857 RID: 43095 RVA: 0x0034FA74 File Offset: 0x0034DC74
		public override void OnUpdate()
		{
			this.DoSetValue();
		}

		// Token: 0x0600A858 RID: 43096 RVA: 0x0034FA7C File Offset: 0x0034DC7C
		private void DoSetValue()
		{
			if (this.selectable == null)
			{
				return;
			}
			this._colorBlock = this.selectable.colors;
			if (!this.colorMultiplier.IsNone)
			{
				this._colorBlock.colorMultiplier = this.colorMultiplier.Value;
			}
			if (!this.fadeDuration.IsNone)
			{
				this._colorBlock.fadeDuration = this.fadeDuration.Value;
			}
			if (!this.normalColor.IsNone)
			{
				this._colorBlock.normalColor = this.normalColor.Value;
			}
			if (!this.pressedColor.IsNone)
			{
				this._colorBlock.pressedColor = this.pressedColor.Value;
			}
			if (!this.highlightedColor.IsNone)
			{
				this._colorBlock.highlightedColor = this.highlightedColor.Value;
			}
			if (!this.disabledColor.IsNone)
			{
				this._colorBlock.disabledColor = this.disabledColor.Value;
			}
			this.selectable.colors = this._colorBlock;
		}

		// Token: 0x0600A859 RID: 43097 RVA: 0x0034FB8C File Offset: 0x0034DD8C
		public override void OnExit()
		{
			if (this.selectable == null)
			{
				return;
			}
			if (this.resetOnExit.Value)
			{
				this.selectable.colors = this.originalColorBlock;
			}
		}

		// Token: 0x04008EE8 RID: 36584
		[RequiredField]
		[CheckForComponent(typeof(Selectable))]
		[Tooltip("The GameObject with the UI Selectable component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008EE9 RID: 36585
		[Tooltip("The fade duration value. Leave as None for no effect")]
		public FsmFloat fadeDuration;

		// Token: 0x04008EEA RID: 36586
		[Tooltip("The color multiplier value. Leave as None for no effect")]
		public FsmFloat colorMultiplier;

		// Token: 0x04008EEB RID: 36587
		[Tooltip("The normal color value. Leave as None for no effect")]
		public FsmColor normalColor;

		// Token: 0x04008EEC RID: 36588
		[Tooltip("The pressed color value. Leave as None for no effect")]
		public FsmColor pressedColor;

		// Token: 0x04008EED RID: 36589
		[Tooltip("The highlighted color value. Leave as None for no effect")]
		public FsmColor highlightedColor;

		// Token: 0x04008EEE RID: 36590
		[Tooltip("The disabled color value. Leave as None for no effect")]
		public FsmColor disabledColor;

		// Token: 0x04008EEF RID: 36591
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		// Token: 0x04008EF0 RID: 36592
		[Tooltip("Repeats every frame, useful for animation")]
		public bool everyFrame;

		// Token: 0x04008EF1 RID: 36593
		private Selectable selectable;

		// Token: 0x04008EF2 RID: 36594
		private ColorBlock _colorBlock;

		// Token: 0x04008EF3 RID: 36595
		private ColorBlock originalColorBlock;
	}
}
