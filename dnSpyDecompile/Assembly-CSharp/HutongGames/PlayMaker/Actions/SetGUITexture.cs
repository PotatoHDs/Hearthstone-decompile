using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DD4 RID: 3540
	[ActionCategory(ActionCategory.GUIElement)]
	[Tooltip("Sets the Texture used by the GUITexture attached to a Game Object.")]
	[Obsolete("GUITexture is part of the legacy UI system and will be removed in a future release")]
	public class SetGUITexture : ComponentAction<GUITexture>
	{
		// Token: 0x0600A608 RID: 42504 RVA: 0x00348592 File Offset: 0x00346792
		public override void Reset()
		{
			this.gameObject = null;
			this.texture = null;
		}

		// Token: 0x0600A609 RID: 42505 RVA: 0x003485A4 File Offset: 0x003467A4
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				base.guiTexture.texture = this.texture.Value;
			}
			base.Finish();
		}

		// Token: 0x04008CAB RID: 36011
		[RequiredField]
		[CheckForComponent(typeof(GUITexture))]
		[Tooltip("The GameObject that owns the GUITexture.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008CAC RID: 36012
		[Tooltip("Texture to apply.")]
		public FsmTexture texture;
	}
}
