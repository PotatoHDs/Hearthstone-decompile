using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C12 RID: 3090
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Unparents all children from the Game Object.")]
	public class DetachChildren : FsmStateAction
	{
		// Token: 0x06009DDD RID: 40413 RVA: 0x0032A077 File Offset: 0x00328277
		public override void Reset()
		{
			this.gameObject = null;
		}

		// Token: 0x06009DDE RID: 40414 RVA: 0x0032A080 File Offset: 0x00328280
		public override void OnEnter()
		{
			DetachChildren.DoDetachChildren(base.Fsm.GetOwnerDefaultTarget(this.gameObject));
			base.Finish();
		}

		// Token: 0x06009DDF RID: 40415 RVA: 0x0032A09E File Offset: 0x0032829E
		private static void DoDetachChildren(GameObject go)
		{
			if (go != null)
			{
				go.transform.DetachChildren();
			}
		}

		// Token: 0x0400833C RID: 33596
		[RequiredField]
		[Tooltip("GameObject to unparent children from.")]
		public FsmOwnerDefault gameObject;
	}
}
