using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E62 RID: 3682
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Tweens the color of the CanvasRenderer color associated with this Graphic.")]
	public class UiGraphicCrossFadeColor : ComponentAction<Graphic>
	{
		// Token: 0x0600A889 RID: 43145 RVA: 0x00350544 File Offset: 0x0034E744
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
			this.useAlpha = null;
			this.duration = 1f;
			this.ignoreTimeScale = null;
		}

		// Token: 0x0600A88A RID: 43146 RVA: 0x003505C8 File Offset: 0x0034E7C8
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.uiComponent = this.cachedComponent;
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
			this.uiComponent.CrossFadeColor(value, this.duration.Value, this.ignoreTimeScale.Value, this.useAlpha.Value);
			base.Finish();
		}

		// Token: 0x04008F27 RID: 36647
		[RequiredField]
		[CheckForComponent(typeof(Graphic))]
		[Tooltip("The GameObject with a UI component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008F28 RID: 36648
		[Tooltip("The Color target of the UI component. Leave to none and set the individual color values, for example to affect just the alpha channel")]
		public FsmColor color;

		// Token: 0x04008F29 RID: 36649
		[Tooltip("The red channel Color target of the UI component. Leave to none for no effect, else it overrides the color property")]
		public FsmFloat red;

		// Token: 0x04008F2A RID: 36650
		[Tooltip("The green channel Color target of the UI component. Leave to none for no effect, else it overrides the color property")]
		public FsmFloat green;

		// Token: 0x04008F2B RID: 36651
		[Tooltip("The blue channel Color target of the UI component. Leave to none for no effect, else it overrides the color property")]
		public FsmFloat blue;

		// Token: 0x04008F2C RID: 36652
		[Tooltip("The alpha channel Color target of the UI component. Leave to none for no effect, else it overrides the color property")]
		public FsmFloat alpha;

		// Token: 0x04008F2D RID: 36653
		[Tooltip("The duration of the tween")]
		public FsmFloat duration;

		// Token: 0x04008F2E RID: 36654
		[Tooltip("Should ignore Time.scale?")]
		public FsmBool ignoreTimeScale;

		// Token: 0x04008F2F RID: 36655
		[Tooltip("Should also Tween the alpha channel?")]
		public FsmBool useAlpha;

		// Token: 0x04008F30 RID: 36656
		private Graphic uiComponent;
	}
}
