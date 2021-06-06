using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C1B RID: 3099
	[ActionCategory(ActionCategory.GUI)]
	[Tooltip("Draws a GUI Texture. NOTE: Uses OnGUI so you need a PlayMakerGUI component in the scene.")]
	public class DrawTexture : FsmStateAction
	{
		// Token: 0x06009DFB RID: 40443 RVA: 0x0032A370 File Offset: 0x00328570
		public override void Reset()
		{
			this.texture = null;
			this.screenRect = null;
			this.left = 0f;
			this.top = 0f;
			this.width = 1f;
			this.height = 1f;
			this.scaleMode = ScaleMode.StretchToFill;
			this.alphaBlend = true;
			this.imageAspect = 0f;
			this.normalized = true;
		}

		// Token: 0x06009DFC RID: 40444 RVA: 0x0032A3FC File Offset: 0x003285FC
		public override void OnGUI()
		{
			if (this.texture.Value == null)
			{
				return;
			}
			this.rect = ((!this.screenRect.IsNone) ? this.screenRect.Value : default(Rect));
			if (!this.left.IsNone)
			{
				this.rect.x = this.left.Value;
			}
			if (!this.top.IsNone)
			{
				this.rect.y = this.top.Value;
			}
			if (!this.width.IsNone)
			{
				this.rect.width = this.width.Value;
			}
			if (!this.height.IsNone)
			{
				this.rect.height = this.height.Value;
			}
			if (this.normalized.Value)
			{
				this.rect.x = this.rect.x * (float)Screen.width;
				this.rect.width = this.rect.width * (float)Screen.width;
				this.rect.y = this.rect.y * (float)Screen.height;
				this.rect.height = this.rect.height * (float)Screen.height;
			}
			GUI.DrawTexture(this.rect, this.texture.Value, this.scaleMode, this.alphaBlend.Value, this.imageAspect.Value);
		}

		// Token: 0x0400834E RID: 33614
		[RequiredField]
		[Tooltip("Texture to draw.")]
		public FsmTexture texture;

		// Token: 0x0400834F RID: 33615
		[UIHint(UIHint.Variable)]
		[Tooltip("Rectangle on the screen to draw the texture within. Alternatively, set or override individual properties below.")]
		[Title("Position")]
		public FsmRect screenRect;

		// Token: 0x04008350 RID: 33616
		[Tooltip("Left screen coordinate.")]
		public FsmFloat left;

		// Token: 0x04008351 RID: 33617
		[Tooltip("Top screen coordinate.")]
		public FsmFloat top;

		// Token: 0x04008352 RID: 33618
		[Tooltip("Width of texture on screen.")]
		public FsmFloat width;

		// Token: 0x04008353 RID: 33619
		[Tooltip("Height of texture on screen.")]
		public FsmFloat height;

		// Token: 0x04008354 RID: 33620
		[Tooltip("How to scale the image when the aspect ratio of it doesn't fit the aspect ratio to be drawn within.")]
		public ScaleMode scaleMode;

		// Token: 0x04008355 RID: 33621
		[Tooltip("Whether to alpha blend the image on to the display (the default). If false, the picture is drawn on to the display.")]
		public FsmBool alphaBlend;

		// Token: 0x04008356 RID: 33622
		[Tooltip("Aspect ratio to use for the source image. If 0 (the default), the aspect ratio from the image is used. Pass in w/h for the desired aspect ratio. This allows the aspect ratio of the source image to be adjusted without changing the pixel width and height.")]
		public FsmFloat imageAspect;

		// Token: 0x04008357 RID: 33623
		[Tooltip("Use normalized screen coordinates (0-1)")]
		public FsmBool normalized;

		// Token: 0x04008358 RID: 33624
		private Rect rect;
	}
}
