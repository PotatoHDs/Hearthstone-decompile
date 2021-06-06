using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E64 RID: 3684
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Set Graphic Color. E.g. to set Sprite Color.")]
	public class UiGraphicSetColor : ComponentAction<Graphic>
	{
		// Token: 0x0600A891 RID: 43153 RVA: 0x00350758 File Offset: 0x0034E958
		public override void Reset()
		{
			this.gameObject = null;
			this.color = null;
			this.red = new FsmFloat
			{
				UseVariable = true
			};
			this.green = new FsmFloat
			{
				UseVariable = true
			};
			this.blue = new FsmFloat
			{
				UseVariable = true
			};
			this.alpha = new FsmFloat
			{
				UseVariable = true
			};
			this.resetOnExit = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A892 RID: 43154 RVA: 0x003507CC File Offset: 0x0034E9CC
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.uiComponent = this.cachedComponent;
			}
			this.originalColor = this.uiComponent.color;
			this.DoSetColorValue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A893 RID: 43155 RVA: 0x00350825 File Offset: 0x0034EA25
		public override void OnUpdate()
		{
			this.DoSetColorValue();
		}

		// Token: 0x0600A894 RID: 43156 RVA: 0x00350830 File Offset: 0x0034EA30
		private void DoSetColorValue()
		{
			if (this.uiComponent == null)
			{
				return;
			}
			Color value = this.uiComponent.color;
			if (!this.color.IsNone)
			{
				value = this.color.Value;
			}
			if (!this.red.IsNone)
			{
				value.r = this.red.Value;
			}
			if (!this.green.IsNone)
			{
				value.g = this.green.Value;
			}
			if (!this.blue.IsNone)
			{
				value.b = this.blue.Value;
			}
			if (!this.alpha.IsNone)
			{
				value.a = this.alpha.Value;
			}
			this.uiComponent.color = value;
		}

		// Token: 0x0600A895 RID: 43157 RVA: 0x003508F9 File Offset: 0x0034EAF9
		public override void OnExit()
		{
			if (this.uiComponent == null)
			{
				return;
			}
			if (this.resetOnExit.Value)
			{
				this.uiComponent.color = this.originalColor;
			}
		}

		// Token: 0x04008F35 RID: 36661
		[RequiredField]
		[CheckForComponent(typeof(Graphic))]
		[Tooltip("The GameObject with a UI component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008F36 RID: 36662
		[Tooltip("The Color of the UI component. Leave to none and set the individual color values, for example to affect just the alpha channel")]
		public FsmColor color;

		// Token: 0x04008F37 RID: 36663
		[Tooltip("The red channel Color of the UI component. Leave to none for no effect, else it overrides the color property")]
		public FsmFloat red;

		// Token: 0x04008F38 RID: 36664
		[Tooltip("The green channel Color of the UI component. Leave to none for no effect, else it overrides the color property")]
		public FsmFloat green;

		// Token: 0x04008F39 RID: 36665
		[Tooltip("The blue channel Color of the UI component. Leave to none for no effect, else it overrides the color property")]
		public FsmFloat blue;

		// Token: 0x04008F3A RID: 36666
		[Tooltip("The alpha channel Color of the UI component. Leave to none for no effect, else it overrides the color property")]
		public FsmFloat alpha;

		// Token: 0x04008F3B RID: 36667
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		// Token: 0x04008F3C RID: 36668
		[Tooltip("Repeats every frame, useful for animation")]
		public bool everyFrame;

		// Token: 0x04008F3D RID: 36669
		private Graphic uiComponent;

		// Token: 0x04008F3E RID: 36670
		private Color originalColor;
	}
}
