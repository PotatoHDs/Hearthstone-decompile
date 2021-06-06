using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E81 RID: 3713
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the texture of a UI RawImage component.")]
	public class UiRawImageSetTexture : ComponentAction<RawImage>
	{
		// Token: 0x0600A921 RID: 43297 RVA: 0x00351DAA File Offset: 0x0034FFAA
		public override void Reset()
		{
			this.gameObject = null;
			this.texture = null;
			this.resetOnExit = null;
		}

		// Token: 0x0600A922 RID: 43298 RVA: 0x00351DC4 File Offset: 0x0034FFC4
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this._texture = this.cachedComponent;
			}
			this._originalTexture = this._texture.texture;
			this.DoSetValue();
			base.Finish();
		}

		// Token: 0x0600A923 RID: 43299 RVA: 0x00351E15 File Offset: 0x00350015
		private void DoSetValue()
		{
			if (this._texture != null)
			{
				this._texture.texture = this.texture.Value;
			}
		}

		// Token: 0x0600A924 RID: 43300 RVA: 0x00351E3B File Offset: 0x0035003B
		public override void OnExit()
		{
			if (this._texture == null)
			{
				return;
			}
			if (this.resetOnExit.Value)
			{
				this._texture.texture = this._originalTexture;
			}
		}

		// Token: 0x04008FCC RID: 36812
		[RequiredField]
		[CheckForComponent(typeof(RawImage))]
		[Tooltip("The GameObject with the UI RawImage component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008FCD RID: 36813
		[RequiredField]
		[Tooltip("The texture of the UI RawImage component.")]
		public FsmTexture texture;

		// Token: 0x04008FCE RID: 36814
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		// Token: 0x04008FCF RID: 36815
		private RawImage _texture;

		// Token: 0x04008FD0 RID: 36816
		private Texture _originalTexture;
	}
}
