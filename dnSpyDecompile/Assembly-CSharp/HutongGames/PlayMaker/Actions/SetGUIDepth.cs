using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DD1 RID: 3537
	[ActionCategory(ActionCategory.GUI)]
	[Tooltip("Sets the sorting depth of subsequent GUI elements.")]
	public class SetGUIDepth : FsmStateAction
	{
		// Token: 0x0600A5FC RID: 42492 RVA: 0x00348498 File Offset: 0x00346698
		public override void Reset()
		{
			this.depth = 0;
		}

		// Token: 0x0600A5FD RID: 42493 RVA: 0x003484A6 File Offset: 0x003466A6
		public override void OnPreprocess()
		{
			base.Fsm.HandleOnGUI = true;
		}

		// Token: 0x0600A5FE RID: 42494 RVA: 0x003484B4 File Offset: 0x003466B4
		public override void OnGUI()
		{
			GUI.depth = this.depth.Value;
		}

		// Token: 0x04008CA5 RID: 36005
		[RequiredField]
		public FsmInt depth;
	}
}
