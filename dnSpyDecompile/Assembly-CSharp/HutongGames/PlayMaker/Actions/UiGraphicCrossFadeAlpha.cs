using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E61 RID: 3681
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Tweens the alpha of the CanvasRenderer color associated with this Graphic.")]
	public class UiGraphicCrossFadeAlpha : ComponentAction<Graphic>
	{
		// Token: 0x0600A886 RID: 43142 RVA: 0x003504AB File Offset: 0x0034E6AB
		public override void Reset()
		{
			this.gameObject = null;
			this.alpha = null;
			this.duration = 1f;
			this.ignoreTimeScale = null;
		}

		// Token: 0x0600A887 RID: 43143 RVA: 0x003504D4 File Offset: 0x0034E6D4
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.uiComponent = this.cachedComponent;
			}
			this.uiComponent.CrossFadeAlpha(this.alpha.Value, this.duration.Value, this.ignoreTimeScale.Value);
			base.Finish();
		}

		// Token: 0x04008F22 RID: 36642
		[RequiredField]
		[CheckForComponent(typeof(Graphic))]
		[Tooltip("The GameObject with an Unity UI component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008F23 RID: 36643
		[Tooltip("The alpha target")]
		public FsmFloat alpha;

		// Token: 0x04008F24 RID: 36644
		[Tooltip("The duration of the tween")]
		public FsmFloat duration;

		// Token: 0x04008F25 RID: 36645
		[Tooltip("Should ignore Time.scale?")]
		public FsmBool ignoreTimeScale;

		// Token: 0x04008F26 RID: 36646
		private Graphic uiComponent;
	}
}
