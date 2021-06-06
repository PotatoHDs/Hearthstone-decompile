using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DA4 RID: 3492
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Set the value of a Game Object Variable in another All FSM. Accept null reference")]
	public class SetAllFsmGameObject : FsmStateAction
	{
		// Token: 0x0600A530 RID: 42288 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void Reset()
		{
		}

		// Token: 0x0600A531 RID: 42289 RVA: 0x003461A9 File Offset: 0x003443A9
		public override void OnEnter()
		{
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A532 RID: 42290 RVA: 0x00003BE8 File Offset: 0x00001DE8
		private void DoSetFsmGameObject()
		{
		}

		// Token: 0x04008BCB RID: 35787
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008BCC RID: 35788
		public bool everyFrame;
	}
}
