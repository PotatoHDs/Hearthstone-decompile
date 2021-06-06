using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E1E RID: 3614
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Sends events based on Touch Phases. Optionally filter by a fingerID.")]
	public class TouchEvent : FsmStateAction
	{
		// Token: 0x0600A745 RID: 42821 RVA: 0x0034BF28 File Offset: 0x0034A128
		public override void Reset()
		{
			this.fingerId = new FsmInt
			{
				UseVariable = true
			};
			this.storeFingerId = null;
		}

		// Token: 0x0600A746 RID: 42822 RVA: 0x0034BF44 File Offset: 0x0034A144
		public override void OnUpdate()
		{
			if (Input.touchCount > 0)
			{
				foreach (Touch touch in Input.touches)
				{
					if ((this.fingerId.IsNone || touch.fingerId == this.fingerId.Value) && touch.phase == this.touchPhase)
					{
						this.storeFingerId.Value = touch.fingerId;
						base.Fsm.Event(this.sendEvent);
					}
				}
			}
		}

		// Token: 0x04008DCC RID: 36300
		public FsmInt fingerId;

		// Token: 0x04008DCD RID: 36301
		public TouchPhase touchPhase;

		// Token: 0x04008DCE RID: 36302
		public FsmEvent sendEvent;

		// Token: 0x04008DCF RID: 36303
		[UIHint(UIHint.Variable)]
		public FsmInt storeFingerId;
	}
}
