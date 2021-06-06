using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D1D RID: 3357
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Controls whether 2D physics affects the Game Object.")]
	public class SetIsKinematic2d : ComponentAction<Rigidbody2D>
	{
		// Token: 0x0600A28C RID: 41612 RVA: 0x0033CA04 File Offset: 0x0033AC04
		public override void Reset()
		{
			this.gameObject = null;
			this.isKinematic = false;
		}

		// Token: 0x0600A28D RID: 41613 RVA: 0x0033CA19 File Offset: 0x0033AC19
		public override void OnEnter()
		{
			this.DoSetIsKinematic();
			base.Finish();
		}

		// Token: 0x0600A28E RID: 41614 RVA: 0x0033CA28 File Offset: 0x0033AC28
		private void DoSetIsKinematic()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (!base.UpdateCache(ownerDefaultTarget))
			{
				return;
			}
			base.rigidbody2d.isKinematic = this.isKinematic.Value;
		}

		// Token: 0x040088F6 RID: 35062
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		[Tooltip("The GameObject with the Rigidbody2D attached")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040088F7 RID: 35063
		[RequiredField]
		[Tooltip("The isKinematic value")]
		public FsmBool isKinematic;
	}
}
