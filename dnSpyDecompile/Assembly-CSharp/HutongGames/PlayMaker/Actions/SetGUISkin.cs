using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DD2 RID: 3538
	[ActionCategory(ActionCategory.GUI)]
	[Tooltip("Sets the GUISkin used by GUI elements.")]
	public class SetGUISkin : FsmStateAction
	{
		// Token: 0x0600A600 RID: 42496 RVA: 0x003484C6 File Offset: 0x003466C6
		public override void Reset()
		{
			this.skin = null;
			this.applyGlobally = true;
		}

		// Token: 0x0600A601 RID: 42497 RVA: 0x003484DB File Offset: 0x003466DB
		public override void OnGUI()
		{
			if (this.skin != null)
			{
				GUI.skin = this.skin;
			}
			if (this.applyGlobally.Value)
			{
				PlayMakerGUI.GUISkin = this.skin;
				base.Finish();
			}
		}

		// Token: 0x04008CA6 RID: 36006
		[RequiredField]
		public GUISkin skin;

		// Token: 0x04008CA7 RID: 36007
		public FsmBool applyGlobally;
	}
}
