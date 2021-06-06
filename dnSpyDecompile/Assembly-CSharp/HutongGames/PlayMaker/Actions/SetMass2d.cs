using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D1E RID: 3358
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Sets the Mass of a Game Object's Rigid Body 2D.")]
	public class SetMass2d : ComponentAction<Rigidbody2D>
	{
		// Token: 0x0600A290 RID: 41616 RVA: 0x0033CA67 File Offset: 0x0033AC67
		public override void Reset()
		{
			this.gameObject = null;
			this.mass = 1f;
		}

		// Token: 0x0600A291 RID: 41617 RVA: 0x0033CA80 File Offset: 0x0033AC80
		public override void OnEnter()
		{
			this.DoSetMass();
			base.Finish();
		}

		// Token: 0x0600A292 RID: 41618 RVA: 0x0033CA90 File Offset: 0x0033AC90
		private void DoSetMass()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (!base.UpdateCache(ownerDefaultTarget))
			{
				return;
			}
			base.rigidbody2d.mass = this.mass.Value;
		}

		// Token: 0x040088F8 RID: 35064
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		[Tooltip("The GameObject with the Rigidbody2D attached")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040088F9 RID: 35065
		[RequiredField]
		[HasFloatSlider(0.1f, 10f)]
		[Tooltip("The Mass")]
		public FsmFloat mass;
	}
}
