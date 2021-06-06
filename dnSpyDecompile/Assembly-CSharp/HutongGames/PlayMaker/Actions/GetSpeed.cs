using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C87 RID: 3207
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Gets the Speed of a Game Object and stores it in a Float Variable. NOTE: The Game Object must have a rigid body.")]
	public class GetSpeed : ComponentAction<Rigidbody>
	{
		// Token: 0x06009FDF RID: 40927 RVA: 0x0032F749 File Offset: 0x0032D949
		public override void Reset()
		{
			this.gameObject = null;
			this.storeResult = null;
			this.everyFrame = false;
		}

		// Token: 0x06009FE0 RID: 40928 RVA: 0x0032F760 File Offset: 0x0032D960
		public override void OnEnter()
		{
			this.DoGetSpeed();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009FE1 RID: 40929 RVA: 0x0032F776 File Offset: 0x0032D976
		public override void OnUpdate()
		{
			this.DoGetSpeed();
		}

		// Token: 0x06009FE2 RID: 40930 RVA: 0x0032F780 File Offset: 0x0032D980
		private void DoGetSpeed()
		{
			if (this.storeResult == null)
			{
				return;
			}
			GameObject go = (this.gameObject.OwnerOption == OwnerDefaultOption.UseOwner) ? base.Owner : this.gameObject.GameObject.Value;
			if (base.UpdateCache(go))
			{
				Vector3 velocity = base.rigidbody.velocity;
				this.storeResult.Value = velocity.magnitude;
			}
		}

		// Token: 0x04008568 RID: 34152
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody))]
		[Tooltip("The GameObject with a Rigidbody.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008569 RID: 34153
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the speed in a float variable.")]
		public FsmFloat storeResult;

		// Token: 0x0400856A RID: 34154
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
