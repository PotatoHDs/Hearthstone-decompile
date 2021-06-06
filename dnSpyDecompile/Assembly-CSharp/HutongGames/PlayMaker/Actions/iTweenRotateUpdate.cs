using System;
using System.Collections;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CE0 RID: 3296
	[ActionCategory("iTween")]
	[Tooltip("Similar to RotateTo but incredibly less expensive for usage inside the Update function or similar looping situations involving a 'live' set of changing values. Does not utilize an EaseType.")]
	public class iTweenRotateUpdate : FsmStateAction
	{
		// Token: 0x0600A14A RID: 41290 RVA: 0x003363F0 File Offset: 0x003345F0
		public override void Reset()
		{
			this.transformRotation = new FsmGameObject
			{
				UseVariable = true
			};
			this.vectorRotation = new FsmVector3
			{
				UseVariable = true
			};
			this.time = 1f;
			this.space = Space.World;
		}

		// Token: 0x0600A14B RID: 41291 RVA: 0x00336430 File Offset: 0x00334630
		public override void OnEnter()
		{
			this.hash = new Hashtable();
			this.go = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (this.go == null)
			{
				base.Finish();
				return;
			}
			if (this.transformRotation.IsNone)
			{
				this.hash.Add("rotation", this.vectorRotation.IsNone ? Vector3.zero : this.vectorRotation.Value);
			}
			else if (this.vectorRotation.IsNone)
			{
				this.hash.Add("rotation", this.transformRotation.Value.transform);
			}
			else if (this.space == Space.World)
			{
				this.hash.Add("rotation", this.transformRotation.Value.transform.eulerAngles + this.vectorRotation.Value);
			}
			else
			{
				this.hash.Add("rotation", this.transformRotation.Value.transform.localEulerAngles + this.vectorRotation.Value);
			}
			this.hash.Add("time", this.time.IsNone ? 1f : this.time.Value);
			this.hash.Add("islocal", this.space == Space.Self);
			this.DoiTween();
		}

		// Token: 0x0600A14C RID: 41292 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void OnExit()
		{
		}

		// Token: 0x0600A14D RID: 41293 RVA: 0x003365C0 File Offset: 0x003347C0
		public override void OnUpdate()
		{
			this.hash.Remove("rotation");
			if (this.transformRotation.IsNone)
			{
				this.hash.Add("rotation", this.vectorRotation.IsNone ? Vector3.zero : this.vectorRotation.Value);
			}
			else if (this.vectorRotation.IsNone)
			{
				this.hash.Add("rotation", this.transformRotation.Value.transform);
			}
			else if (this.space == Space.World)
			{
				this.hash.Add("rotation", this.transformRotation.Value.transform.eulerAngles + this.vectorRotation.Value);
			}
			else
			{
				this.hash.Add("rotation", this.transformRotation.Value.transform.localEulerAngles + this.vectorRotation.Value);
			}
			this.DoiTween();
		}

		// Token: 0x0600A14E RID: 41294 RVA: 0x003366D6 File Offset: 0x003348D6
		private void DoiTween()
		{
			iTween.RotateUpdate(this.go, this.hash);
		}

		// Token: 0x04008731 RID: 34609
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008732 RID: 34610
		[Tooltip("Rotate to a transform rotation.")]
		public FsmGameObject transformRotation;

		// Token: 0x04008733 RID: 34611
		[Tooltip("A rotation the GameObject will animate from.")]
		public FsmVector3 vectorRotation;

		// Token: 0x04008734 RID: 34612
		[Tooltip("The time in seconds the animation will take to complete. If transformRotation is set, this is used as an offset.")]
		public FsmFloat time;

		// Token: 0x04008735 RID: 34613
		[Tooltip("Whether to animate in local or world space.")]
		public Space space;

		// Token: 0x04008736 RID: 34614
		private Hashtable hash;

		// Token: 0x04008737 RID: 34615
		private GameObject go;
	}
}
