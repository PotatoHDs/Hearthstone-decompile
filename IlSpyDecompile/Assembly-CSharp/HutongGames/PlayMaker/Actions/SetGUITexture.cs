using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GUIElement)]
	[Tooltip("Sets the Texture used by the GUITexture attached to a Game Object.")]
	[Obsolete("GUITexture is part of the legacy UI system and will be removed in a future release")]
	public class SetGUITexture : ComponentAction<GUITexture>
	{
		[RequiredField]
		[CheckForComponent(typeof(GUITexture))]
		[Tooltip("The GameObject that owns the GUITexture.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Texture to apply.")]
		public FsmTexture texture;

		public override void Reset()
		{
			gameObject = null;
			texture = null;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(ownerDefaultTarget))
			{
				base.guiTexture.texture = texture.Value;
			}
			Finish();
		}
	}
}
