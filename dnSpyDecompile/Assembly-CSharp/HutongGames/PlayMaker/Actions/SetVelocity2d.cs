using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D1F RID: 3359
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Sets the 2d Velocity of a Game Object. To leave any axis unchanged, set variable to 'None'. NOTE: Game object must have a rigidbody 2D.")]
	public class SetVelocity2d : ComponentAction<Rigidbody2D>
	{
		// Token: 0x0600A294 RID: 41620 RVA: 0x0033CACF File Offset: 0x0033ACCF
		public override void Reset()
		{
			this.gameObject = null;
			this.vector = null;
			this.x = new FsmFloat
			{
				UseVariable = true
			};
			this.y = new FsmFloat
			{
				UseVariable = true
			};
			this.everyFrame = false;
		}

		// Token: 0x0600A295 RID: 41621 RVA: 0x003201AC File Offset: 0x0031E3AC
		public override void Awake()
		{
			base.Fsm.HandleFixedUpdate = true;
		}

		// Token: 0x0600A296 RID: 41622 RVA: 0x0033CB0A File Offset: 0x0033AD0A
		public override void OnEnter()
		{
			this.DoSetVelocity();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A297 RID: 41623 RVA: 0x0033CB0A File Offset: 0x0033AD0A
		public override void OnFixedUpdate()
		{
			this.DoSetVelocity();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A298 RID: 41624 RVA: 0x0033CB20 File Offset: 0x0033AD20
		private void DoSetVelocity()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (!base.UpdateCache(ownerDefaultTarget))
			{
				return;
			}
			Vector2 velocity;
			if (this.vector.IsNone)
			{
				velocity = base.rigidbody2d.velocity;
			}
			else
			{
				velocity = this.vector.Value;
			}
			if (!this.x.IsNone)
			{
				velocity.x = this.x.Value;
			}
			if (!this.y.IsNone)
			{
				velocity.y = this.y.Value;
			}
			base.rigidbody2d.velocity = velocity;
		}

		// Token: 0x040088FA RID: 35066
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		[Tooltip("The GameObject with the Rigidbody2D attached")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040088FB RID: 35067
		[Tooltip("A Vector2 value for the velocity")]
		public FsmVector2 vector;

		// Token: 0x040088FC RID: 35068
		[Tooltip("The y value of the velocity. Overrides 'Vector' x value if set")]
		public FsmFloat x;

		// Token: 0x040088FD RID: 35069
		[Tooltip("The y value of the velocity. Overrides 'Vector' y value if set")]
		public FsmFloat y;

		// Token: 0x040088FE RID: 35070
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
