using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D78 RID: 3448
	[Tooltip("Base class for actions that want to run a sub FSM.")]
	public abstract class RunFSMAction : FsmStateAction
	{
		// Token: 0x0600A457 RID: 42071 RVA: 0x00342CA0 File Offset: 0x00340EA0
		public override void Reset()
		{
			this.runFsm = null;
		}

		// Token: 0x0600A458 RID: 42072 RVA: 0x00342CA9 File Offset: 0x00340EA9
		public override bool Event(FsmEvent fsmEvent)
		{
			if (this.runFsm != null && (fsmEvent.IsGlobal || fsmEvent.IsSystemEvent))
			{
				this.runFsm.Event(fsmEvent);
			}
			return false;
		}

		// Token: 0x0600A459 RID: 42073 RVA: 0x00342CD0 File Offset: 0x00340ED0
		public override void OnEnter()
		{
			if (this.runFsm == null)
			{
				base.Finish();
				return;
			}
			this.runFsm.OnEnable();
			if (!this.runFsm.Started)
			{
				this.runFsm.Start();
			}
			this.CheckIfFinished();
		}

		// Token: 0x0600A45A RID: 42074 RVA: 0x00342D0A File Offset: 0x00340F0A
		public override void OnUpdate()
		{
			if (this.runFsm != null)
			{
				this.runFsm.Update();
				this.CheckIfFinished();
				return;
			}
			base.Finish();
		}

		// Token: 0x0600A45B RID: 42075 RVA: 0x00342D2C File Offset: 0x00340F2C
		public override void OnFixedUpdate()
		{
			if (this.runFsm != null)
			{
				this.runFsm.FixedUpdate();
				this.CheckIfFinished();
				return;
			}
			base.Finish();
		}

		// Token: 0x0600A45C RID: 42076 RVA: 0x00342D4E File Offset: 0x00340F4E
		public override void OnLateUpdate()
		{
			if (this.runFsm != null)
			{
				this.runFsm.LateUpdate();
				this.CheckIfFinished();
				return;
			}
			base.Finish();
		}

		// Token: 0x0600A45D RID: 42077 RVA: 0x00342D70 File Offset: 0x00340F70
		public override void DoTriggerEnter(Collider other)
		{
			if (this.runFsm.HandleTriggerEnter)
			{
				this.runFsm.OnTriggerEnter(other);
			}
		}

		// Token: 0x0600A45E RID: 42078 RVA: 0x00342D8B File Offset: 0x00340F8B
		public override void DoTriggerStay(Collider other)
		{
			if (this.runFsm.HandleTriggerStay)
			{
				this.runFsm.OnTriggerStay(other);
			}
		}

		// Token: 0x0600A45F RID: 42079 RVA: 0x00342DA6 File Offset: 0x00340FA6
		public override void DoTriggerExit(Collider other)
		{
			if (this.runFsm.HandleTriggerExit)
			{
				this.runFsm.OnTriggerExit(other);
			}
		}

		// Token: 0x0600A460 RID: 42080 RVA: 0x00342DC1 File Offset: 0x00340FC1
		public override void DoCollisionEnter(Collision collisionInfo)
		{
			if (this.runFsm.HandleCollisionEnter)
			{
				this.runFsm.OnCollisionEnter(collisionInfo);
			}
		}

		// Token: 0x0600A461 RID: 42081 RVA: 0x00342DDC File Offset: 0x00340FDC
		public override void DoCollisionStay(Collision collisionInfo)
		{
			if (this.runFsm.HandleCollisionStay)
			{
				this.runFsm.OnCollisionStay(collisionInfo);
			}
		}

		// Token: 0x0600A462 RID: 42082 RVA: 0x00342DF7 File Offset: 0x00340FF7
		public override void DoCollisionExit(Collision collisionInfo)
		{
			if (this.runFsm.HandleCollisionExit)
			{
				this.runFsm.OnCollisionExit(collisionInfo);
			}
		}

		// Token: 0x0600A463 RID: 42083 RVA: 0x00342E12 File Offset: 0x00341012
		public override void DoParticleCollision(GameObject other)
		{
			if (this.runFsm.HandleParticleCollision)
			{
				this.runFsm.OnParticleCollision(other);
			}
		}

		// Token: 0x0600A464 RID: 42084 RVA: 0x00342E2D File Offset: 0x0034102D
		public override void DoControllerColliderHit(ControllerColliderHit collisionInfo)
		{
			if (this.runFsm.HandleControllerColliderHit)
			{
				this.runFsm.OnControllerColliderHit(collisionInfo);
			}
		}

		// Token: 0x0600A465 RID: 42085 RVA: 0x00342E48 File Offset: 0x00341048
		public override void DoTriggerEnter2D(Collider2D other)
		{
			if (this.runFsm.HandleTriggerEnter2D)
			{
				this.runFsm.OnTriggerEnter2D(other);
			}
		}

		// Token: 0x0600A466 RID: 42086 RVA: 0x00342E63 File Offset: 0x00341063
		public override void DoTriggerStay2D(Collider2D other)
		{
			if (this.runFsm.HandleTriggerStay2D)
			{
				this.runFsm.OnTriggerStay2D(other);
			}
		}

		// Token: 0x0600A467 RID: 42087 RVA: 0x00342E7E File Offset: 0x0034107E
		public override void DoTriggerExit2D(Collider2D other)
		{
			if (this.runFsm.HandleTriggerExit2D)
			{
				this.runFsm.OnTriggerExit2D(other);
			}
		}

		// Token: 0x0600A468 RID: 42088 RVA: 0x00342E99 File Offset: 0x00341099
		public override void DoCollisionEnter2D(Collision2D collisionInfo)
		{
			if (this.runFsm.HandleCollisionEnter2D)
			{
				this.runFsm.OnCollisionEnter2D(collisionInfo);
			}
		}

		// Token: 0x0600A469 RID: 42089 RVA: 0x00342EB4 File Offset: 0x003410B4
		public override void DoCollisionStay2D(Collision2D collisionInfo)
		{
			if (this.runFsm.HandleCollisionStay2D)
			{
				this.runFsm.OnCollisionStay2D(collisionInfo);
			}
		}

		// Token: 0x0600A46A RID: 42090 RVA: 0x00342ECF File Offset: 0x003410CF
		public override void DoCollisionExit2D(Collision2D collisionInfo)
		{
			if (this.runFsm.HandleCollisionExit2D)
			{
				this.runFsm.OnCollisionExit2D(collisionInfo);
			}
		}

		// Token: 0x0600A46B RID: 42091 RVA: 0x00342EEA File Offset: 0x003410EA
		public override void OnGUI()
		{
			if (this.runFsm != null && this.runFsm.HandleOnGUI)
			{
				this.runFsm.OnGUI();
			}
		}

		// Token: 0x0600A46C RID: 42092 RVA: 0x00342F0C File Offset: 0x0034110C
		public override void OnExit()
		{
			if (this.runFsm != null)
			{
				this.runFsm.Stop();
			}
		}

		// Token: 0x0600A46D RID: 42093 RVA: 0x00342F21 File Offset: 0x00341121
		protected virtual void CheckIfFinished()
		{
			if (this.runFsm == null || this.runFsm.Finished)
			{
				base.Finish();
			}
		}

		// Token: 0x04008AC7 RID: 35527
		protected Fsm runFsm;
	}
}
