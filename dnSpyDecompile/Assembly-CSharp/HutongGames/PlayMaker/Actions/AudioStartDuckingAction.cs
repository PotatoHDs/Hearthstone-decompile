using System;
using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F24 RID: 3876
	[ActionCategory("Pegasus Audio")]
	[Tooltip("Start ducking one or more category of sounds.")]
	public class AudioStartDuckingAction : FsmStateAction
	{
		// Token: 0x0600AC1D RID: 44061 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void Reset()
		{
		}

		// Token: 0x0600AC1E RID: 44062 RVA: 0x0035B4DC File Offset: 0x003596DC
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.m_GameObject);
			if (ownerDefaultTarget != null)
			{
				SoundDucker soundDucker = ownerDefaultTarget.GetComponent<SoundDucker>();
				if (soundDucker == null)
				{
					soundDucker = ownerDefaultTarget.AddComponent<SoundDucker>();
				}
				List<SoundDuckedCategoryDef> duckedCategoryDefs = new List<SoundDuckedCategoryDef>(this.m_DuckedCategoryDefs);
				soundDucker.SetDuckedCategoryDefs(duckedCategoryDefs);
				soundDucker.StartDucking();
			}
			base.Finish();
		}

		// Token: 0x040092E6 RID: 37606
		[RequiredField]
		[Tooltip("Game Object to responsible for ducking.")]
		public FsmOwnerDefault m_GameObject;

		// Token: 0x040092E7 RID: 37607
		public SoundDuckedCategoryDef[] m_DuckedCategoryDefs;
	}
}
