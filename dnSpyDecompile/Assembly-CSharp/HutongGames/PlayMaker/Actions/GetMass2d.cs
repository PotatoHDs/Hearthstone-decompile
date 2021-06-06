using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D04 RID: 3332
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Gets the Mass of a Game Object's Rigid Body 2D.")]
	public class GetMass2d : ComponentAction<Rigidbody2D>
	{
		// Token: 0x0600A212 RID: 41490 RVA: 0x0033A0E6 File Offset: 0x003382E6
		public override void Reset()
		{
			this.gameObject = null;
			this.storeResult = null;
		}

		// Token: 0x0600A213 RID: 41491 RVA: 0x0033A0F6 File Offset: 0x003382F6
		public override void OnEnter()
		{
			this.DoGetMass();
			base.Finish();
		}

		// Token: 0x0600A214 RID: 41492 RVA: 0x0033A104 File Offset: 0x00338304
		private void DoGetMass()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (!base.UpdateCache(ownerDefaultTarget))
			{
				return;
			}
			this.storeResult.Value = base.rigidbody2d.mass;
		}

		// Token: 0x04008818 RID: 34840
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		[Tooltip("The GameObject with a Rigidbody2D attached.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008819 RID: 34841
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the mass of gameObject.")]
		public FsmFloat storeResult;
	}
}
