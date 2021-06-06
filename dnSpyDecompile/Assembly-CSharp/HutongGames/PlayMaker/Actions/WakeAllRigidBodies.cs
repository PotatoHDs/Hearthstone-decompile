using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EC3 RID: 3779
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Rigid bodies start sleeping when they come to rest. This action wakes up all rigid bodies in the scene. E.g., if you Set Gravity and want objects at rest to respond.")]
	public class WakeAllRigidBodies : FsmStateAction
	{
		// Token: 0x0600AA57 RID: 43607 RVA: 0x003551AD File Offset: 0x003533AD
		public override void Reset()
		{
			this.everyFrame = false;
		}

		// Token: 0x0600AA58 RID: 43608 RVA: 0x003551B6 File Offset: 0x003533B6
		public override void OnEnter()
		{
			this.bodies = (UnityEngine.Object.FindObjectsOfType(typeof(Rigidbody)) as Rigidbody[]);
			this.DoWakeAll();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AA59 RID: 43609 RVA: 0x003551E6 File Offset: 0x003533E6
		public override void OnUpdate()
		{
			this.DoWakeAll();
		}

		// Token: 0x0600AA5A RID: 43610 RVA: 0x003551F0 File Offset: 0x003533F0
		private void DoWakeAll()
		{
			this.bodies = (UnityEngine.Object.FindObjectsOfType(typeof(Rigidbody)) as Rigidbody[]);
			if (this.bodies != null)
			{
				Rigidbody[] array = this.bodies;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].WakeUp();
				}
			}
		}

		// Token: 0x040090F0 RID: 37104
		public bool everyFrame;

		// Token: 0x040090F1 RID: 37105
		private Rigidbody[] bodies;
	}
}
