using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F27 RID: 3879
	[ActionCategory("Pegasus Audio")]
	[Tooltip("Stops the ducking started by AudioStartDuckingAction on this object.")]
	public class AudioStopDuckingAction : FsmStateAction
	{
		// Token: 0x0600AC26 RID: 44070 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void Reset()
		{
		}

		// Token: 0x0600AC27 RID: 44071 RVA: 0x0035B5AC File Offset: 0x003597AC
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.m_GameObject);
			if (ownerDefaultTarget != null)
			{
				SoundDucker component = ownerDefaultTarget.GetComponent<SoundDucker>();
				if (component != null)
				{
					component.StopDucking();
					UnityEngine.Object.Destroy(component);
				}
			}
			base.Finish();
		}

		// Token: 0x040092EA RID: 37610
		[RequiredField]
		[Tooltip("Game Object whose ducking we want to stop.")]
		public FsmOwnerDefault m_GameObject;
	}
}
