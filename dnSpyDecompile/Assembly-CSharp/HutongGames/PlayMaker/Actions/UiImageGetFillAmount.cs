using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E65 RID: 3685
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Set The Fill Amount on a UI Image")]
	public class UiImageGetFillAmount : ComponentAction<Image>
	{
		// Token: 0x0600A897 RID: 43159 RVA: 0x00350928 File Offset: 0x0034EB28
		public override void Reset()
		{
			this.gameObject = null;
			this.ImageFillAmount = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A898 RID: 43160 RVA: 0x00350940 File Offset: 0x0034EB40
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.image = this.cachedComponent;
			}
			this.DoGetFillAmount();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A899 RID: 43161 RVA: 0x00350988 File Offset: 0x0034EB88
		public override void OnUpdate()
		{
			this.DoGetFillAmount();
		}

		// Token: 0x0600A89A RID: 43162 RVA: 0x00350990 File Offset: 0x0034EB90
		private void DoGetFillAmount()
		{
			if (this.image != null)
			{
				this.ImageFillAmount.Value = this.image.fillAmount;
			}
		}

		// Token: 0x04008F3F RID: 36671
		[RequiredField]
		[CheckForComponent(typeof(Image))]
		[Tooltip("The GameObject with the UI Image component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008F40 RID: 36672
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The fill amount.")]
		public FsmFloat ImageFillAmount;

		// Token: 0x04008F41 RID: 36673
		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		// Token: 0x04008F42 RID: 36674
		private Image image;
	}
}
