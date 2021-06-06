using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EC4 RID: 3780
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Forces a Game Object's Rigid Body to wake up.")]
	public class WakeUp : ComponentAction<Rigidbody>
	{
		// Token: 0x0600AA5C RID: 43612 RVA: 0x0035523C File Offset: 0x0035343C
		public override void Reset()
		{
			this.gameObject = null;
		}

		// Token: 0x0600AA5D RID: 43613 RVA: 0x00355245 File Offset: 0x00353445
		public override void OnEnter()
		{
			this.DoWakeUp();
			base.Finish();
		}

		// Token: 0x0600AA5E RID: 43614 RVA: 0x00355254 File Offset: 0x00353454
		private void DoWakeUp()
		{
			GameObject go = (this.gameObject.OwnerOption == OwnerDefaultOption.UseOwner) ? base.Owner : this.gameObject.GameObject.Value;
			if (base.UpdateCache(go))
			{
				base.rigidbody.WakeUp();
			}
		}

		// Token: 0x040090F2 RID: 37106
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody))]
		public FsmOwnerDefault gameObject;
	}
}
