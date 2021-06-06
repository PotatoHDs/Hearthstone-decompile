using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DD6 RID: 3542
	[ActionCategory(ActionCategory.GUIElement)]
	[Tooltip("Sets the Color of the GUITexture attached to a Game Object.")]
	[Obsolete("GUITexture is part of the legacy UI system and will be removed in a future release")]
	public class SetGUITextureColor : ComponentAction<GUITexture>
	{
		// Token: 0x0600A610 RID: 42512 RVA: 0x00348691 File Offset: 0x00346891
		public override void Reset()
		{
			this.gameObject = null;
			this.color = Color.white;
			this.everyFrame = false;
		}

		// Token: 0x0600A611 RID: 42513 RVA: 0x003486B1 File Offset: 0x003468B1
		public override void OnEnter()
		{
			this.DoSetGUITextureColor();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A612 RID: 42514 RVA: 0x003486C7 File Offset: 0x003468C7
		public override void OnUpdate()
		{
			this.DoSetGUITextureColor();
		}

		// Token: 0x0600A613 RID: 42515 RVA: 0x003486D0 File Offset: 0x003468D0
		private void DoSetGUITextureColor()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				base.guiTexture.color = this.color.Value;
			}
		}

		// Token: 0x04008CB0 RID: 36016
		[RequiredField]
		[CheckForComponent(typeof(GUITexture))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008CB1 RID: 36017
		[RequiredField]
		public FsmColor color;

		// Token: 0x04008CB2 RID: 36018
		public bool everyFrame;
	}
}
