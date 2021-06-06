using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E66 RID: 3686
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets the source image sprite of a UI Image component.")]
	public class UiImageGetSprite : ComponentAction<Image>
	{
		// Token: 0x0600A89C RID: 43164 RVA: 0x003509BE File Offset: 0x0034EBBE
		public override void Reset()
		{
			this.gameObject = null;
			this.sprite = null;
		}

		// Token: 0x0600A89D RID: 43165 RVA: 0x003509D0 File Offset: 0x0034EBD0
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.image = this.cachedComponent;
			}
			this.DoSetImageSourceValue();
			base.Finish();
		}

		// Token: 0x0600A89E RID: 43166 RVA: 0x00350A10 File Offset: 0x0034EC10
		private void DoSetImageSourceValue()
		{
			if (this.image != null)
			{
				this.sprite.Value = this.image.sprite;
			}
		}

		// Token: 0x04008F43 RID: 36675
		[RequiredField]
		[CheckForComponent(typeof(Image))]
		[Tooltip("The GameObject with the UI Image component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008F44 RID: 36676
		[RequiredField]
		[Tooltip("The source sprite of the UI Image component.")]
		[UIHint(UIHint.Variable)]
		[ObjectType(typeof(Sprite))]
		public FsmObject sprite;

		// Token: 0x04008F45 RID: 36677
		private Image image;
	}
}
