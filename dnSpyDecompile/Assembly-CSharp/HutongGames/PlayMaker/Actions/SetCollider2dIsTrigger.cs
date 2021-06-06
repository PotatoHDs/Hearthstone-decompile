using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D18 RID: 3352
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Set the isTrigger option of a Collider2D. Optionally set all collider2D found on the gameobject Target.")]
	public class SetCollider2dIsTrigger : FsmStateAction
	{
		// Token: 0x0600A275 RID: 41589 RVA: 0x0033C570 File Offset: 0x0033A770
		public override void Reset()
		{
			this.gameObject = null;
			this.isTrigger = false;
			this.setAllColliders = false;
		}

		// Token: 0x0600A276 RID: 41590 RVA: 0x0033C58C File Offset: 0x0033A78C
		public override void OnEnter()
		{
			this.DoSetIsTrigger();
			base.Finish();
		}

		// Token: 0x0600A277 RID: 41591 RVA: 0x0033C59C File Offset: 0x0033A79C
		private void DoSetIsTrigger()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			if (this.setAllColliders)
			{
				Collider2D[] components = ownerDefaultTarget.GetComponents<Collider2D>();
				for (int i = 0; i < components.Length; i++)
				{
					components[i].isTrigger = this.isTrigger.Value;
				}
				return;
			}
			if (ownerDefaultTarget.GetComponent<Collider2D>() != null)
			{
				ownerDefaultTarget.GetComponent<Collider2D>().isTrigger = this.isTrigger.Value;
			}
		}

		// Token: 0x040088DF RID: 35039
		[RequiredField]
		[CheckForComponent(typeof(Collider2D))]
		[Tooltip("The GameObject with the Collider2D attached")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040088E0 RID: 35040
		[RequiredField]
		[Tooltip("The flag value")]
		public FsmBool isTrigger;

		// Token: 0x040088E1 RID: 35041
		[Tooltip("Set all Colliders on the GameObject target")]
		public bool setAllColliders;
	}
}
