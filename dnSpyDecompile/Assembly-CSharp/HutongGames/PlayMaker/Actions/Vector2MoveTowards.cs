using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EAD RID: 3757
	[ActionCategory(ActionCategory.Vector2)]
	[Tooltip("Moves a Vector2 towards a Target. Optionally sends an event when successful.")]
	public class Vector2MoveTowards : FsmStateAction
	{
		// Token: 0x0600A9FA RID: 43514 RVA: 0x00353F65 File Offset: 0x00352165
		public override void Reset()
		{
			this.source = null;
			this.target = null;
			this.maxSpeed = 10f;
			this.finishDistance = 1f;
			this.finishEvent = null;
		}

		// Token: 0x0600A9FB RID: 43515 RVA: 0x00353F9C File Offset: 0x0035219C
		public override void OnUpdate()
		{
			this.DoMoveTowards();
		}

		// Token: 0x0600A9FC RID: 43516 RVA: 0x00353FA4 File Offset: 0x003521A4
		private void DoMoveTowards()
		{
			this.source.Value = Vector2.MoveTowards(this.source.Value, this.target.Value, this.maxSpeed.Value * Time.deltaTime);
			if ((this.source.Value - this.target.Value).magnitude < this.finishDistance.Value)
			{
				base.Fsm.Event(this.finishEvent);
				base.Finish();
			}
		}

		// Token: 0x0400909A RID: 37018
		[RequiredField]
		[Tooltip("The Vector2 to Move")]
		public FsmVector2 source;

		// Token: 0x0400909B RID: 37019
		[Tooltip("A target Vector2 to move towards.")]
		public FsmVector2 target;

		// Token: 0x0400909C RID: 37020
		[HasFloatSlider(0f, 20f)]
		[Tooltip("The maximum movement speed. HINT: You can make this a variable to change it over time.")]
		public FsmFloat maxSpeed;

		// Token: 0x0400909D RID: 37021
		[HasFloatSlider(0f, 5f)]
		[Tooltip("Distance at which the move is considered finished, and the Finish Event is sent.")]
		public FsmFloat finishDistance;

		// Token: 0x0400909E RID: 37022
		[Tooltip("Event to send when the Finish Distance is reached.")]
		public FsmEvent finishEvent;
	}
}
