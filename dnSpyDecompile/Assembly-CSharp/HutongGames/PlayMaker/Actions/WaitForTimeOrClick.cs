using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F9A RID: 3994
	[ActionCategory(ActionCategory.Time)]
	[Tooltip("Delays a state from finishing by the specified time, unless a mouse press interrupts it. NOTE: Other actions continue, but FINISHED can't happen before Time.")]
	public class WaitForTimeOrClick : FsmStateAction
	{
		// Token: 0x0600ADFB RID: 44539 RVA: 0x00362D6A File Offset: 0x00360F6A
		public override void Reset()
		{
			this.time = 1f;
			this.button = MouseButton.Left;
		}

		// Token: 0x0600ADFC RID: 44540 RVA: 0x00362D84 File Offset: 0x00360F84
		public override void OnEnter()
		{
			if (this.time.Value <= 0f)
			{
				base.Fsm.Event(this.finishEvent);
				base.Finish();
				return;
			}
			this.startTime = FsmTime.RealtimeSinceStartup;
			this.timer = 0f;
		}

		// Token: 0x0600ADFD RID: 44541 RVA: 0x00362DD4 File Offset: 0x00360FD4
		public override void OnUpdate()
		{
			this.timer += Time.deltaTime;
			bool flag = this.timer >= this.time.Value;
			bool mouseButton = UniversalInputManager.Get().GetMouseButton((int)this.button);
			if (flag || mouseButton)
			{
				base.Finish();
				if (this.finishEvent != null)
				{
					base.Fsm.Event(this.finishEvent);
				}
			}
		}

		// Token: 0x040094CA RID: 38090
		[RequiredField]
		[Tooltip("The maximum time this will delay the FINISHED event, unless interrupted by a click.")]
		public FsmFloat time;

		// Token: 0x040094CB RID: 38091
		[RequiredField]
		[Tooltip("The mouse button to listen for.")]
		public MouseButton button;

		// Token: 0x040094CC RID: 38092
		public FsmEvent finishEvent;

		// Token: 0x040094CD RID: 38093
		private float startTime;

		// Token: 0x040094CE RID: 38094
		private float timer;
	}
}
