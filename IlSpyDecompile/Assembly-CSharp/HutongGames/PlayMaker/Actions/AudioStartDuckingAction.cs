using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus Audio")]
	[Tooltip("Start ducking one or more category of sounds.")]
	public class AudioStartDuckingAction : FsmStateAction
	{
		[RequiredField]
		[Tooltip("Game Object to responsible for ducking.")]
		public FsmOwnerDefault m_GameObject;

		public SoundDuckedCategoryDef[] m_DuckedCategoryDefs;

		public override void Reset()
		{
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(m_GameObject);
			if (ownerDefaultTarget != null)
			{
				SoundDucker soundDucker = null;
				soundDucker = ownerDefaultTarget.GetComponent<SoundDucker>();
				if (soundDucker == null)
				{
					soundDucker = ownerDefaultTarget.AddComponent<SoundDucker>();
				}
				List<SoundDuckedCategoryDef> duckedCategoryDefs = new List<SoundDuckedCategoryDef>(m_DuckedCategoryDefs);
				soundDucker.SetDuckedCategoryDefs(duckedCategoryDefs);
				soundDucker.StartDucking();
			}
			Finish();
		}
	}
}
