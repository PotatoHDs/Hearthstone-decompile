using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E9E RID: 3742
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("Turn GUILayout on/off. If you don't use GUILayout actions you can get some performance back by turning GUILayout off. This can make a difference on iOS platforms.")]
	public class UseGUILayout : FsmStateAction
	{
		// Token: 0x0600A9B9 RID: 43449 RVA: 0x0035359A File Offset: 0x0035179A
		public override void Reset()
		{
			this.turnOffGUIlayout = true;
		}

		// Token: 0x0600A9BA RID: 43450 RVA: 0x003535A3 File Offset: 0x003517A3
		public override void OnEnter()
		{
			base.Fsm.Owner.useGUILayout = !this.turnOffGUIlayout;
			base.Finish();
		}

		// Token: 0x04009063 RID: 36963
		[RequiredField]
		public bool turnOffGUIlayout;
	}
}
