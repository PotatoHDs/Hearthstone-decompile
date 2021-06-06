using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D90 RID: 3472
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Move a GameObject from its current scene to a new scene. It is required that the GameObject is at the root of its current scene.")]
	public class MoveGameObjectToScene : GetSceneActionBase
	{
		// Token: 0x0600A4D8 RID: 42200 RVA: 0x00344849 File Offset: 0x00342A49
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.findRootIfNecessary = null;
			this.success = null;
			this.successEvent = null;
			this.failureEvent = null;
		}

		// Token: 0x0600A4D9 RID: 42201 RVA: 0x00344874 File Offset: 0x00342A74
		public override void OnEnter()
		{
			base.OnEnter();
			if (this._sceneFound)
			{
				this._go = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
				if (this.findRootIfNecessary.Value)
				{
					this._go = this._go.transform.root.gameObject;
				}
				if (this._go.transform.parent == null)
				{
					SceneManager.MoveGameObjectToScene(this._go, this._scene);
					this.success.Value = true;
					base.Fsm.Event(this.successEvent);
				}
				else
				{
					base.LogError("GameObject must be a root ");
					this.success.Value = false;
					base.Fsm.Event(this.failureEvent);
				}
				base.Fsm.Event(this.sceneFoundEvent);
				this._go = null;
			}
			base.Finish();
		}

		// Token: 0x04008B54 RID: 35668
		[RequiredField]
		[Tooltip("The Root GameObject to move to the referenced scene")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008B55 RID: 35669
		[RequiredField]
		[Tooltip("Only root GameObject can be moved, set to true to get the root of the gameobject if necessary, else watch for failure events.")]
		public FsmBool findRootIfNecessary;

		// Token: 0x04008B56 RID: 35670
		[ActionSection("Result")]
		[Tooltip("True if the merge succeeded")]
		[UIHint(UIHint.Variable)]
		public FsmBool success;

		// Token: 0x04008B57 RID: 35671
		[Tooltip("Event sent if merge succeeded")]
		public FsmEvent successEvent;

		// Token: 0x04008B58 RID: 35672
		[Tooltip("Event sent if merge failed. Check log for information")]
		public FsmEvent failureEvent;

		// Token: 0x04008B59 RID: 35673
		private GameObject _go;
	}
}
