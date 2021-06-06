using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DDA RID: 3546
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Controls whether physics affects the Game Object.")]
	public class SetIsKinematic : ComponentAction<Rigidbody>
	{
		// Token: 0x0600A622 RID: 42530 RVA: 0x00348803 File Offset: 0x00346A03
		public override void Reset()
		{
			this.gameObject = null;
			this.isKinematic = false;
		}

		// Token: 0x0600A623 RID: 42531 RVA: 0x00348818 File Offset: 0x00346A18
		public override void OnEnter()
		{
			this.DoSetIsKinematic();
			base.Finish();
		}

		// Token: 0x0600A624 RID: 42532 RVA: 0x00348828 File Offset: 0x00346A28
		private void DoSetIsKinematic()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				base.rigidbody.isKinematic = this.isKinematic.Value;
			}
		}

		// Token: 0x04008CBB RID: 36027
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008CBC RID: 36028
		[RequiredField]
		public FsmBool isKinematic;
	}
}
