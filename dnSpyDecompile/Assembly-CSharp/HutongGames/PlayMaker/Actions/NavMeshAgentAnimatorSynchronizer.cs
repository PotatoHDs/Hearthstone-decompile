using System;
using UnityEngine;
using UnityEngine.AI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EF3 RID: 3827
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Synchronize a NavMesh Agent velocity and rotation with the animator process.")]
	public class NavMeshAgentAnimatorSynchronizer : FsmStateAction
	{
		// Token: 0x0600AB32 RID: 43826 RVA: 0x003580EE File Offset: 0x003562EE
		public override void Reset()
		{
			this.gameObject = null;
		}

		// Token: 0x0600AB33 RID: 43827 RVA: 0x003580F7 File Offset: 0x003562F7
		public override void OnPreprocess()
		{
			base.Fsm.HandleAnimatorMove = true;
		}

		// Token: 0x0600AB34 RID: 43828 RVA: 0x00358108 File Offset: 0x00356308
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				base.Finish();
				return;
			}
			this._agent = ownerDefaultTarget.GetComponent<NavMeshAgent>();
			this._animator = ownerDefaultTarget.GetComponent<Animator>();
			if (this._animator == null)
			{
				base.Finish();
				return;
			}
			this._trans = ownerDefaultTarget.transform;
		}

		// Token: 0x0600AB35 RID: 43829 RVA: 0x00358170 File Offset: 0x00356370
		public override void DoAnimatorMove()
		{
			this._agent.velocity = this._animator.deltaPosition / Time.deltaTime;
			this._trans.rotation = this._animator.rootRotation;
		}

		// Token: 0x040091F3 RID: 37363
		[RequiredField]
		[CheckForComponent(typeof(NavMeshAgent))]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Agent target. An Animator component and a NavMeshAgent component are required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040091F4 RID: 37364
		private Animator _animator;

		// Token: 0x040091F5 RID: 37365
		private NavMeshAgent _agent;

		// Token: 0x040091F6 RID: 37366
		private Transform _trans;
	}
}
