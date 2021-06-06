using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DD5 RID: 3541
	[ActionCategory(ActionCategory.GUIElement)]
	[Tooltip("Sets the Alpha of the GUITexture attached to a Game Object. Useful for fading GUI elements in/out.")]
	[Obsolete("GUITexture is part of the legacy UI system and will be removed in a future release")]
	public class SetGUITextureAlpha : ComponentAction<GUITexture>
	{
		// Token: 0x0600A60B RID: 42507 RVA: 0x003485F0 File Offset: 0x003467F0
		public override void Reset()
		{
			this.gameObject = null;
			this.alpha = 1f;
			this.everyFrame = false;
		}

		// Token: 0x0600A60C RID: 42508 RVA: 0x00348610 File Offset: 0x00346810
		public override void OnEnter()
		{
			this.DoGUITextureAlpha();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A60D RID: 42509 RVA: 0x00348626 File Offset: 0x00346826
		public override void OnUpdate()
		{
			this.DoGUITextureAlpha();
		}

		// Token: 0x0600A60E RID: 42510 RVA: 0x00348630 File Offset: 0x00346830
		private void DoGUITextureAlpha()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				Color color = base.guiTexture.color;
				base.guiTexture.color = new Color(color.r, color.g, color.b, this.alpha.Value);
			}
		}

		// Token: 0x04008CAD RID: 36013
		[RequiredField]
		[CheckForComponent(typeof(GUITexture))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008CAE RID: 36014
		[RequiredField]
		public FsmFloat alpha;

		// Token: 0x04008CAF RID: 36015
		public bool everyFrame;
	}
}
