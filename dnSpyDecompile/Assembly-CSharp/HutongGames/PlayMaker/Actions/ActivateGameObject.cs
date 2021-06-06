using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000B9A RID: 2970
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Activates/deactivates a Game Object. Use this to hide/show areas, or enable/disable many Behaviours at once.")]
	public class ActivateGameObject : FsmStateAction
	{
		// Token: 0x06009B92 RID: 39826 RVA: 0x0031FD39 File Offset: 0x0031DF39
		public override void Reset()
		{
			this.gameObject = null;
			this.activate = true;
			this.recursive = true;
			this.resetOnExit = false;
			this.everyFrame = false;
		}

		// Token: 0x06009B93 RID: 39827 RVA: 0x0031FD68 File Offset: 0x0031DF68
		public override void OnEnter()
		{
			this.DoActivateGameObject();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009B94 RID: 39828 RVA: 0x0031FD7E File Offset: 0x0031DF7E
		public override void OnUpdate()
		{
			this.DoActivateGameObject();
		}

		// Token: 0x06009B95 RID: 39829 RVA: 0x0031FD88 File Offset: 0x0031DF88
		public override void OnExit()
		{
			if (this.activatedGameObject == null)
			{
				return;
			}
			if (this.resetOnExit)
			{
				if (this.recursive.Value)
				{
					this.SetActiveRecursively(this.activatedGameObject, !this.activate.Value);
					return;
				}
				this.activatedGameObject.SetActive(!this.activate.Value);
			}
		}

		// Token: 0x06009B96 RID: 39830 RVA: 0x0031FDF0 File Offset: 0x0031DFF0
		private void DoActivateGameObject()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			if (this.recursive.Value)
			{
				this.SetActiveRecursively(ownerDefaultTarget, this.activate.Value);
			}
			else
			{
				ownerDefaultTarget.SetActive(this.activate.Value);
			}
			this.activatedGameObject = ownerDefaultTarget;
		}

		// Token: 0x06009B97 RID: 39831 RVA: 0x0031FE54 File Offset: 0x0031E054
		public void SetActiveRecursively(GameObject go, bool state)
		{
			go.SetActive(state);
			foreach (object obj in go.transform)
			{
				Transform transform = (Transform)obj;
				this.SetActiveRecursively(transform.gameObject, state);
			}
		}

		// Token: 0x040080DD RID: 32989
		[RequiredField]
		[Tooltip("The GameObject to activate/deactivate.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040080DE RID: 32990
		[RequiredField]
		[Tooltip("Check to activate, uncheck to deactivate Game Object.")]
		public FsmBool activate;

		// Token: 0x040080DF RID: 32991
		[Tooltip("Recursively activate/deactivate all children.")]
		public FsmBool recursive;

		// Token: 0x040080E0 RID: 32992
		[Tooltip("Reset the game objects when exiting this state. Useful if you want an object to be active only while this state is active.\nNote: Only applies to the last Game Object activated/deactivated (won't work if Game Object changes).")]
		public bool resetOnExit;

		// Token: 0x040080E1 RID: 32993
		[Tooltip("Repeat this action every frame. Useful if Activate changes over time.")]
		public bool everyFrame;

		// Token: 0x040080E2 RID: 32994
		private GameObject activatedGameObject;
	}
}
