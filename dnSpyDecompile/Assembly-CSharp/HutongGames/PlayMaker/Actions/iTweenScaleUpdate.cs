using System;
using System.Collections;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CE5 RID: 3301
	[ActionCategory("iTween")]
	[Tooltip("CSimilar to ScaleTo but incredibly less expensive for usage inside the Update function or similar looping situations involving a 'live' set of changing values. Does not utilize an EaseType.")]
	public class iTweenScaleUpdate : FsmStateAction
	{
		// Token: 0x0600A164 RID: 41316 RVA: 0x003371C4 File Offset: 0x003353C4
		public override void Reset()
		{
			this.transformScale = new FsmGameObject
			{
				UseVariable = true
			};
			this.vectorScale = new FsmVector3
			{
				UseVariable = true
			};
			this.time = 1f;
		}

		// Token: 0x0600A165 RID: 41317 RVA: 0x003371FC File Offset: 0x003353FC
		public override void OnEnter()
		{
			this.hash = new Hashtable();
			this.go = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (this.go == null)
			{
				base.Finish();
				return;
			}
			if (this.transformScale.IsNone)
			{
				this.hash.Add("scale", this.vectorScale.IsNone ? Vector3.zero : this.vectorScale.Value);
			}
			else if (this.vectorScale.IsNone)
			{
				this.hash.Add("scale", this.transformScale.Value.transform);
			}
			else
			{
				this.hash.Add("scale", this.transformScale.Value.transform.localScale + this.vectorScale.Value);
			}
			this.hash.Add("time", this.time.IsNone ? 1f : this.time.Value);
			this.DoiTween();
		}

		// Token: 0x0600A166 RID: 41318 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void OnExit()
		{
		}

		// Token: 0x0600A167 RID: 41319 RVA: 0x00337328 File Offset: 0x00335528
		public override void OnUpdate()
		{
			this.hash.Remove("scale");
			if (this.transformScale.IsNone)
			{
				this.hash.Add("scale", this.vectorScale.IsNone ? Vector3.zero : this.vectorScale.Value);
			}
			else if (this.vectorScale.IsNone)
			{
				this.hash.Add("scale", this.transformScale.Value.transform);
			}
			else
			{
				this.hash.Add("scale", this.transformScale.Value.transform.localScale + this.vectorScale.Value);
			}
			this.DoiTween();
		}

		// Token: 0x0600A168 RID: 41320 RVA: 0x003373F7 File Offset: 0x003355F7
		private void DoiTween()
		{
			iTween.ScaleUpdate(this.go, this.hash);
		}

		// Token: 0x0400875A RID: 34650
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400875B RID: 34651
		[Tooltip("Scale To a transform scale.")]
		public FsmGameObject transformScale;

		// Token: 0x0400875C RID: 34652
		[Tooltip("A scale vector the GameObject will animate To.")]
		public FsmVector3 vectorScale;

		// Token: 0x0400875D RID: 34653
		[Tooltip("The time in seconds the animation will take to complete. If transformScale is set, this is used as an offset.")]
		public FsmFloat time;

		// Token: 0x0400875E RID: 34654
		private Hashtable hash;

		// Token: 0x0400875F RID: 34655
		private GameObject go;
	}
}
