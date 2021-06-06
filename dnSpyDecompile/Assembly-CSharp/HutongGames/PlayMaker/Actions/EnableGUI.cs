using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C20 RID: 3104
	[ActionCategory(ActionCategory.GUI)]
	[Tooltip("Enables/Disables the PlayMakerGUI component in the scene. Note, you need a PlayMakerGUI component in the scene to see OnGUI actions. However, OnGUI can be very expensive on mobile devices. This action lets you turn OnGUI on/off (e.g., turn it on for a menu, and off during gameplay).")]
	public class EnableGUI : FsmStateAction
	{
		// Token: 0x06009E12 RID: 40466 RVA: 0x0032A976 File Offset: 0x00328B76
		public override void Reset()
		{
			this.enableGUI = true;
		}

		// Token: 0x06009E13 RID: 40467 RVA: 0x0032A984 File Offset: 0x00328B84
		public override void OnEnter()
		{
			PlayMakerGUI.Instance.enabled = this.enableGUI.Value;
			base.Finish();
		}

		// Token: 0x0400836B RID: 33643
		[Tooltip("Set to True to enable, False to disable.")]
		public FsmBool enableGUI;
	}
}
