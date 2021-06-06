using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DE4 RID: 3556
	[ActionCategory(ActionCategory.Camera)]
	[Tooltip("Sets the Main Camera.")]
	public class SetMainCamera : FsmStateAction
	{
		// Token: 0x0600A64D RID: 42573 RVA: 0x00348C4F File Offset: 0x00346E4F
		public override void Reset()
		{
			this.gameObject = null;
		}

		// Token: 0x0600A64E RID: 42574 RVA: 0x00348C58 File Offset: 0x00346E58
		public override void OnEnter()
		{
			if (this.gameObject.Value != null)
			{
				if (Camera.main != null)
				{
					Camera.main.gameObject.tag = "Untagged";
				}
				this.gameObject.Value.tag = "MainCamera";
			}
			base.Finish();
		}

		// Token: 0x04008CD3 RID: 36051
		[RequiredField]
		[CheckForComponent(typeof(Camera))]
		[Tooltip("The GameObject to set as the main camera (should have a Camera component).")]
		public FsmGameObject gameObject;
	}
}
