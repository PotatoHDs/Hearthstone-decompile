using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BDC RID: 3036
	[ActionCategory(ActionCategory.Effects)]
	[Tooltip("Turns a Game Object on/off in a regular repeating pattern.")]
	public class Blink : ComponentAction<Renderer>
	{
		// Token: 0x06009CD6 RID: 40150 RVA: 0x00326B08 File Offset: 0x00324D08
		public override void Reset()
		{
			this.gameObject = null;
			this.timeOff = 0.5f;
			this.timeOn = 0.5f;
			this.rendererOnly = true;
			this.startOn = false;
			this.realTime = false;
		}

		// Token: 0x06009CD7 RID: 40151 RVA: 0x00326B56 File Offset: 0x00324D56
		public override void OnEnter()
		{
			this.startTime = FsmTime.RealtimeSinceStartup;
			this.timer = 0f;
			this.UpdateBlinkState(this.startOn.Value);
		}

		// Token: 0x06009CD8 RID: 40152 RVA: 0x00326B80 File Offset: 0x00324D80
		public override void OnUpdate()
		{
			if (this.realTime)
			{
				this.timer = FsmTime.RealtimeSinceStartup - this.startTime;
			}
			else
			{
				this.timer += Time.deltaTime;
			}
			if (this.blinkOn && this.timer > this.timeOn.Value)
			{
				this.UpdateBlinkState(false);
			}
			if (!this.blinkOn && this.timer > this.timeOff.Value)
			{
				this.UpdateBlinkState(true);
			}
		}

		// Token: 0x06009CD9 RID: 40153 RVA: 0x00326C00 File Offset: 0x00324E00
		private void UpdateBlinkState(bool state)
		{
			GameObject gameObject = (this.gameObject.OwnerOption == OwnerDefaultOption.UseOwner) ? base.Owner : this.gameObject.GameObject.Value;
			if (gameObject == null)
			{
				return;
			}
			if (this.rendererOnly)
			{
				if (base.UpdateCache(gameObject))
				{
					base.renderer.enabled = state;
				}
			}
			else
			{
				gameObject.SetActive(state);
			}
			this.blinkOn = state;
			this.startTime = FsmTime.RealtimeSinceStartup;
			this.timer = 0f;
		}

		// Token: 0x0400824C RID: 33356
		[RequiredField]
		[Tooltip("The GameObject to blink on/off.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400824D RID: 33357
		[HasFloatSlider(0f, 5f)]
		[Tooltip("Time to stay off in seconds.")]
		public FsmFloat timeOff;

		// Token: 0x0400824E RID: 33358
		[HasFloatSlider(0f, 5f)]
		[Tooltip("Time to stay on in seconds.")]
		public FsmFloat timeOn;

		// Token: 0x0400824F RID: 33359
		[Tooltip("Should the object start in the active/visible state?")]
		public FsmBool startOn;

		// Token: 0x04008250 RID: 33360
		[Tooltip("Only effect the renderer, keeping other components active.")]
		public bool rendererOnly;

		// Token: 0x04008251 RID: 33361
		[Tooltip("Ignore TimeScale. Useful if the game is paused.")]
		public bool realTime;

		// Token: 0x04008252 RID: 33362
		private float startTime;

		// Token: 0x04008253 RID: 33363
		private float timer;

		// Token: 0x04008254 RID: 33364
		private bool blinkOn;
	}
}
