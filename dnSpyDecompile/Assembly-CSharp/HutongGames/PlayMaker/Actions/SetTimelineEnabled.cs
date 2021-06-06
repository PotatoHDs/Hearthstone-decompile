using System;
using UnityEngine;
using UnityEngine.Playables;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F78 RID: 3960
	[ActionCategory("Pegasus")]
	[Tooltip("Enables the 'Playable Director' component on the given game object.")]
	public class SetTimelineEnabled : FsmStateAction
	{
		// Token: 0x0600AD6B RID: 44395 RVA: 0x00360EC6 File Offset: 0x0035F0C6
		public override void Reset()
		{
			this.gameObject = null;
			this.enableTimeline = false;
		}

		// Token: 0x0600AD6C RID: 44396 RVA: 0x00360ED8 File Offset: 0x0035F0D8
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget != null)
			{
				PlayableDirector component = ownerDefaultTarget.GetComponent<PlayableDirector>();
				if (component != null)
				{
					component.enabled = this.enableTimeline;
				}
			}
			base.Finish();
		}

		// Token: 0x04009448 RID: 37960
		[RequiredField]
		[CheckForComponent(typeof(PlayableDirector))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009449 RID: 37961
		public bool enableTimeline;
	}
}
