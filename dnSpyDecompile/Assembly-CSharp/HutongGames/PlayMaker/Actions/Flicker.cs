using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C28 RID: 3112
	[ActionCategory(ActionCategory.Effects)]
	[Tooltip("Randomly flickers a Game Object on/off.")]
	public class Flicker : ComponentAction<Renderer>
	{
		// Token: 0x06009E36 RID: 40502 RVA: 0x0032AF9F File Offset: 0x0032919F
		public override void Reset()
		{
			this.gameObject = null;
			this.frequency = 0.1f;
			this.amountOn = 0.5f;
			this.rendererOnly = true;
			this.realTime = false;
		}

		// Token: 0x06009E37 RID: 40503 RVA: 0x0032AFD6 File Offset: 0x003291D6
		public override void OnEnter()
		{
			this.startTime = FsmTime.RealtimeSinceStartup;
			this.timer = 0f;
		}

		// Token: 0x06009E38 RID: 40504 RVA: 0x0032AFF0 File Offset: 0x003291F0
		public override void OnUpdate()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			if (this.realTime)
			{
				this.timer = FsmTime.RealtimeSinceStartup - this.startTime;
			}
			else
			{
				this.timer += Time.deltaTime;
			}
			if (this.timer > this.frequency.Value)
			{
				bool flag = UnityEngine.Random.Range(0f, 1f) < this.amountOn.Value;
				if (this.rendererOnly)
				{
					if (base.UpdateCache(ownerDefaultTarget))
					{
						base.renderer.enabled = flag;
					}
				}
				else
				{
					ownerDefaultTarget.SetActive(flag);
				}
				this.startTime = this.timer;
				this.timer = 0f;
			}
		}

		// Token: 0x0400838C RID: 33676
		[RequiredField]
		[Tooltip("The GameObject to flicker.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400838D RID: 33677
		[HasFloatSlider(0f, 1f)]
		[Tooltip("The frequency of the flicker in seconds.")]
		public FsmFloat frequency;

		// Token: 0x0400838E RID: 33678
		[HasFloatSlider(0f, 1f)]
		[Tooltip("Amount of time flicker is On (0-1). E.g. Use 0.95 for an occasional flicker.")]
		public FsmFloat amountOn;

		// Token: 0x0400838F RID: 33679
		[Tooltip("Only effect the renderer, leaving other components active.")]
		public bool rendererOnly;

		// Token: 0x04008390 RID: 33680
		[Tooltip("Ignore time scale. Useful if flickering UI when the game is paused.")]
		public bool realTime;

		// Token: 0x04008391 RID: 33681
		private float startTime;

		// Token: 0x04008392 RID: 33682
		private float timer;
	}
}
