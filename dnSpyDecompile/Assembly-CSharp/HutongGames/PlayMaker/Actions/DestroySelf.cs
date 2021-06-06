using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C11 RID: 3089
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Destroys the Owner of the Fsm! Useful for spawned Prefabs that need to kill themselves, e.g., a projectile that explodes on impact.")]
	public class DestroySelf : FsmStateAction
	{
		// Token: 0x06009DDA RID: 40410 RVA: 0x0032A02B File Offset: 0x0032822B
		public override void Reset()
		{
			this.detachChildren = false;
		}

		// Token: 0x06009DDB RID: 40411 RVA: 0x0032A039 File Offset: 0x00328239
		public override void OnEnter()
		{
			if (base.Owner != null)
			{
				if (this.detachChildren.Value)
				{
					base.Owner.transform.DetachChildren();
				}
				UnityEngine.Object.Destroy(base.Owner);
			}
			base.Finish();
		}

		// Token: 0x0400833B RID: 33595
		[Tooltip("Detach children before destroying the Owner.")]
		public FsmBool detachChildren;
	}
}
