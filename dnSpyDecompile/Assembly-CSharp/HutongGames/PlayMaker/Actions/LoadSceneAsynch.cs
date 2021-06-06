using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D8E RID: 3470
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Loads the scene by its name or index in Build Settings.")]
	public class LoadSceneAsynch : FsmStateAction
	{
		// Token: 0x0600A4CB RID: 42187 RVA: 0x00344254 File Offset: 0x00342454
		public override void Reset()
		{
			this.sceneReference = GetSceneActionBase.SceneSimpleReferenceOptions.SceneAtIndex;
			this.sceneByName = null;
			this.sceneAtIndex = null;
			this.loadSceneMode = null;
			this.aSyncOperationHashCode = null;
			this.allowSceneActivation = null;
			this.operationPriority = new FsmInt
			{
				UseVariable = true
			};
			this.pendingActivation = null;
			this.pendingActivationEvent = null;
			this.isDone = null;
			this.progress = null;
			this.doneEvent = null;
			this.sceneNotFoundEvent = null;
		}

		// Token: 0x0600A4CC RID: 42188 RVA: 0x003442C8 File Offset: 0x003424C8
		public override void OnEnter()
		{
			this.pendingActivationCallBackDone = false;
			this.pendingActivation.Value = false;
			this.isDone.Value = false;
			this.progress.Value = 0f;
			if (!this.DoLoadAsynch())
			{
				base.Fsm.Event(this.sceneNotFoundEvent);
				base.Finish();
			}
		}

		// Token: 0x0600A4CD RID: 42189 RVA: 0x00344324 File Offset: 0x00342524
		private bool DoLoadAsynch()
		{
			if (this.sceneReference == GetSceneActionBase.SceneSimpleReferenceOptions.SceneAtIndex)
			{
				if (SceneManager.GetActiveScene().buildIndex == this.sceneAtIndex.Value)
				{
					return false;
				}
				this._asyncOperation = SceneManager.LoadSceneAsync(this.sceneAtIndex.Value, (LoadSceneMode)this.loadSceneMode.Value);
			}
			else
			{
				if (SceneManager.GetActiveScene().name == this.sceneByName.Value)
				{
					return false;
				}
				this._asyncOperation = SceneManager.LoadSceneAsync(this.sceneByName.Value, (LoadSceneMode)this.loadSceneMode.Value);
			}
			if (!this.operationPriority.IsNone)
			{
				this._asyncOperation.priority = this.operationPriority.Value;
			}
			this._asyncOperation.allowSceneActivation = this.allowSceneActivation.Value;
			if (!this.aSyncOperationHashCode.IsNone)
			{
				if (LoadSceneAsynch.aSyncOperationLUT == null)
				{
					LoadSceneAsynch.aSyncOperationLUT = new Dictionary<int, AsyncOperation>();
				}
				this._asynchOperationUid = ++LoadSceneAsynch.aSynchUidCounter;
				this.aSyncOperationHashCode.Value = this._asynchOperationUid;
				LoadSceneAsynch.aSyncOperationLUT.Add(this._asynchOperationUid, this._asyncOperation);
			}
			return true;
		}

		// Token: 0x0600A4CE RID: 42190 RVA: 0x00344458 File Offset: 0x00342658
		public override void OnUpdate()
		{
			if (this._asyncOperation == null)
			{
				return;
			}
			if (this._asyncOperation.isDone)
			{
				this.isDone.Value = true;
				this.progress.Value = this._asyncOperation.progress;
				if (LoadSceneAsynch.aSyncOperationLUT != null && this._asynchOperationUid != -1)
				{
					LoadSceneAsynch.aSyncOperationLUT.Remove(this._asynchOperationUid);
				}
				this._asyncOperation = null;
				base.Fsm.Event(this.doneEvent);
				base.Finish();
				return;
			}
			this.progress.Value = this._asyncOperation.progress;
			if (!this._asyncOperation.allowSceneActivation && this.allowSceneActivation.Value)
			{
				this._asyncOperation.allowSceneActivation = true;
			}
			if (this._asyncOperation.progress == 0.9f && !this._asyncOperation.allowSceneActivation && !this.pendingActivationCallBackDone)
			{
				this.pendingActivationCallBackDone = true;
				if (!this.pendingActivation.IsNone)
				{
					this.pendingActivation.Value = true;
				}
				base.Fsm.Event(this.pendingActivationEvent);
			}
		}

		// Token: 0x0600A4CF RID: 42191 RVA: 0x00344571 File Offset: 0x00342771
		public override void OnExit()
		{
			this._asyncOperation = null;
		}

		// Token: 0x04008B31 RID: 35633
		[Tooltip("The reference options of the Scene")]
		public GetSceneActionBase.SceneSimpleReferenceOptions sceneReference;

		// Token: 0x04008B32 RID: 35634
		[Tooltip("The name of the scene to load. The given sceneName can either be the last part of the path, without .unity extension or the full path still without the .unity extension")]
		public FsmString sceneByName;

		// Token: 0x04008B33 RID: 35635
		[Tooltip("The index of the scene to load.")]
		public FsmInt sceneAtIndex;

		// Token: 0x04008B34 RID: 35636
		[Tooltip("Allows you to specify whether or not to load the scene additively. See LoadSceneMode Unity doc for more information about the options.")]
		[ObjectType(typeof(LoadSceneMode))]
		public FsmEnum loadSceneMode;

		// Token: 0x04008B35 RID: 35637
		[Tooltip("Allow the scene to be activated as soon as it's ready")]
		public FsmBool allowSceneActivation;

		// Token: 0x04008B36 RID: 35638
		[Tooltip("lets you tweak in which order async operation calls will be performed. Leave to none for default")]
		public FsmInt operationPriority;

		// Token: 0x04008B37 RID: 35639
		[ActionSection("Result")]
		[Tooltip("Use this hash to activate the Scene if you have set 'AllowSceneActivation' to false, you'll need to use it in the action 'AllowSceneActivation' to effectively load the scene.")]
		[UIHint(UIHint.Variable)]
		public FsmInt aSyncOperationHashCode;

		// Token: 0x04008B38 RID: 35640
		[Tooltip("The loading's progress.")]
		[UIHint(UIHint.Variable)]
		public FsmFloat progress;

		// Token: 0x04008B39 RID: 35641
		[Tooltip("True when loading is done")]
		[UIHint(UIHint.Variable)]
		public FsmBool isDone;

		// Token: 0x04008B3A RID: 35642
		[Tooltip("True when loading is done but still waiting for scene activation")]
		[UIHint(UIHint.Variable)]
		public FsmBool pendingActivation;

		// Token: 0x04008B3B RID: 35643
		[Tooltip("Event sent when scene loading is done")]
		public FsmEvent doneEvent;

		// Token: 0x04008B3C RID: 35644
		[Tooltip("Event sent when scene loading is done but scene not yet activated. Use aSyncOperationHashCode value in 'AllowSceneActivation' to proceed")]
		public FsmEvent pendingActivationEvent;

		// Token: 0x04008B3D RID: 35645
		[Tooltip("Event sent if the scene to load was not found")]
		public FsmEvent sceneNotFoundEvent;

		// Token: 0x04008B3E RID: 35646
		private AsyncOperation _asyncOperation;

		// Token: 0x04008B3F RID: 35647
		private int _asynchOperationUid = -1;

		// Token: 0x04008B40 RID: 35648
		private bool pendingActivationCallBackDone;

		// Token: 0x04008B41 RID: 35649
		public static Dictionary<int, AsyncOperation> aSyncOperationLUT;

		// Token: 0x04008B42 RID: 35650
		private static int aSynchUidCounter;
	}
}
