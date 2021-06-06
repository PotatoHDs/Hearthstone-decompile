using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E68 RID: 3688
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the source image sprite of a UI Image component.")]
	public class UiImageSetSprite : ComponentAction<Image>
	{
		// Token: 0x0600A8A5 RID: 43173 RVA: 0x00350ACE File Offset: 0x0034ECCE
		public override void Reset()
		{
			this.gameObject = null;
			this.resetOnExit = false;
		}

		// Token: 0x0600A8A6 RID: 43174 RVA: 0x00350AE4 File Offset: 0x0034ECE4
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.image = this.cachedComponent;
			}
			this.originalSprite = this.image.sprite;
			this.DoSetImageSourceValue();
			base.Finish();
		}

		// Token: 0x0600A8A7 RID: 43175 RVA: 0x00350B35 File Offset: 0x0034ED35
		private void DoSetImageSourceValue()
		{
			if (this.image == null)
			{
				return;
			}
			this.image.sprite = (this.sprite.Value as Sprite);
		}

		// Token: 0x0600A8A8 RID: 43176 RVA: 0x00350B61 File Offset: 0x0034ED61
		public override void OnExit()
		{
			if (this.image == null)
			{
				return;
			}
			if (this.resetOnExit.Value)
			{
				this.image.sprite = this.originalSprite;
			}
		}

		// Token: 0x04008F4A RID: 36682
		[RequiredField]
		[CheckForComponent(typeof(Image))]
		[Tooltip("The GameObject with the Image UI component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008F4B RID: 36683
		[RequiredField]
		[Tooltip("The source sprite of the UI Image component.")]
		[ObjectType(typeof(Sprite))]
		public FsmObject sprite;

		// Token: 0x04008F4C RID: 36684
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		// Token: 0x04008F4D RID: 36685
		private Image image;

		// Token: 0x04008F4E RID: 36686
		private Sprite originalSprite;
	}
}
