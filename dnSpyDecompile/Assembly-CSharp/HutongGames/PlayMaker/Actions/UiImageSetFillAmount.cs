using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E67 RID: 3687
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Set The Fill Amount on a UI Image")]
	public class UiImageSetFillAmount : ComponentAction<Image>
	{
		// Token: 0x0600A8A0 RID: 43168 RVA: 0x00350A36 File Offset: 0x0034EC36
		public override void Reset()
		{
			this.gameObject = null;
			this.ImageFillAmount = 1f;
			this.everyFrame = false;
		}

		// Token: 0x0600A8A1 RID: 43169 RVA: 0x00350A58 File Offset: 0x0034EC58
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.image = this.cachedComponent;
			}
			this.DoSetFillAmount();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A8A2 RID: 43170 RVA: 0x00350AA0 File Offset: 0x0034ECA0
		public override void OnUpdate()
		{
			this.DoSetFillAmount();
		}

		// Token: 0x0600A8A3 RID: 43171 RVA: 0x00350AA8 File Offset: 0x0034ECA8
		private void DoSetFillAmount()
		{
			if (this.image != null)
			{
				this.image.fillAmount = this.ImageFillAmount.Value;
			}
		}

		// Token: 0x04008F46 RID: 36678
		[RequiredField]
		[CheckForComponent(typeof(Image))]
		[Tooltip("The GameObject with the UI Image component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008F47 RID: 36679
		[RequiredField]
		[HasFloatSlider(0f, 1f)]
		[Tooltip("The fill amount.")]
		public FsmFloat ImageFillAmount;

		// Token: 0x04008F48 RID: 36680
		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		// Token: 0x04008F49 RID: 36681
		private Image image;
	}
}
