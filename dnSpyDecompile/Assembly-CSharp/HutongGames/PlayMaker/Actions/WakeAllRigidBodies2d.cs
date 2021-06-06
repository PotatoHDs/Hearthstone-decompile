using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D25 RID: 3365
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Rigid bodies 2D start sleeping when they come to rest. This action wakes up all rigid bodies 2D in the scene. E.g., if you Set Gravity 2D and want objects at rest to respond.")]
	public class WakeAllRigidBodies2d : FsmStateAction
	{
		// Token: 0x0600A2BD RID: 41661 RVA: 0x0033D6E8 File Offset: 0x0033B8E8
		public override void Reset()
		{
			this.everyFrame = false;
		}

		// Token: 0x0600A2BE RID: 41662 RVA: 0x0033D6F1 File Offset: 0x0033B8F1
		public override void OnEnter()
		{
			this.DoWakeAll();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A2BF RID: 41663 RVA: 0x0033D707 File Offset: 0x0033B907
		public override void OnUpdate()
		{
			this.DoWakeAll();
		}

		// Token: 0x0600A2C0 RID: 41664 RVA: 0x0033D710 File Offset: 0x0033B910
		private void DoWakeAll()
		{
			Rigidbody2D[] array = UnityEngine.Object.FindObjectsOfType(typeof(Rigidbody2D)) as Rigidbody2D[];
			if (array != null)
			{
				Rigidbody2D[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i].WakeUp();
				}
			}
		}

		// Token: 0x04008926 RID: 35110
		[Tooltip("Repeat every frame. Note: This would be very expensive!")]
		public bool everyFrame;
	}
}
