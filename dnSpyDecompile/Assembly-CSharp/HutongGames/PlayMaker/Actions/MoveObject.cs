using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CF3 RID: 3315
	[ActionCategory(ActionCategory.Transform)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=4758.0")]
	[Tooltip("Move a GameObject to another GameObject. Works like iTween Move To, but with better performance.")]
	public class MoveObject : EaseFsmAction
	{
		// Token: 0x0600A1B1 RID: 41393 RVA: 0x00338A7B File Offset: 0x00336C7B
		public override void Reset()
		{
			base.Reset();
			this.fromValue = null;
			this.toVector = null;
			this.finishInNextStep = false;
			this.fromVector = null;
		}

		// Token: 0x0600A1B2 RID: 41394 RVA: 0x00338AA0 File Offset: 0x00336CA0
		public override void OnEnter()
		{
			base.OnEnter();
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.objectToMove);
			this.fromVector = ownerDefaultTarget.transform.position;
			this.toVector = this.destination.Value.transform.position;
			this.fromFloats = new float[3];
			this.fromFloats[0] = this.fromVector.Value.x;
			this.fromFloats[1] = this.fromVector.Value.y;
			this.fromFloats[2] = this.fromVector.Value.z;
			this.toFloats = new float[3];
			this.toFloats[0] = this.toVector.Value.x;
			this.toFloats[1] = this.toVector.Value.y;
			this.toFloats[2] = this.toVector.Value.z;
			this.resultFloats = new float[3];
			this.resultFloats[0] = this.fromVector.Value.x;
			this.resultFloats[1] = this.fromVector.Value.y;
			this.resultFloats[2] = this.fromVector.Value.z;
			this.finishInNextStep = false;
		}

		// Token: 0x0600A1B3 RID: 41395 RVA: 0x00338C00 File Offset: 0x00336E00
		public override void OnUpdate()
		{
			base.OnUpdate();
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.objectToMove);
			ownerDefaultTarget.transform.position = new Vector3(this.resultFloats[0], this.resultFloats[1], this.resultFloats[2]);
			if (this.finishInNextStep)
			{
				base.Finish();
				if (this.finishEvent != null)
				{
					base.Fsm.Event(this.finishEvent);
				}
			}
			if (this.finishAction && !this.finishInNextStep)
			{
				ownerDefaultTarget.transform.position = new Vector3(this.reverse.IsNone ? this.toVector.Value.x : (this.reverse.Value ? this.fromValue.Value.x : this.toVector.Value.x), this.reverse.IsNone ? this.toVector.Value.y : (this.reverse.Value ? this.fromValue.Value.y : this.toVector.Value.y), this.reverse.IsNone ? this.toVector.Value.z : (this.reverse.Value ? this.fromValue.Value.z : this.toVector.Value.z));
				this.finishInNextStep = true;
			}
		}

		// Token: 0x040087C0 RID: 34752
		[RequiredField]
		public FsmOwnerDefault objectToMove;

		// Token: 0x040087C1 RID: 34753
		[RequiredField]
		public FsmGameObject destination;

		// Token: 0x040087C2 RID: 34754
		private FsmVector3 fromValue;

		// Token: 0x040087C3 RID: 34755
		private FsmVector3 toVector;

		// Token: 0x040087C4 RID: 34756
		private FsmVector3 fromVector;

		// Token: 0x040087C5 RID: 34757
		private bool finishInNextStep;
	}
}
