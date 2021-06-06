using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D7C RID: 3452
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Allow scenes to be activated. Use this after LoadSceneAsynch where you did not activated the scene upon loading")]
	public class AllowSceneActivation : FsmStateAction
	{
		// Token: 0x0600A47D RID: 42109 RVA: 0x00343189 File Offset: 0x00341389
		public override void Reset()
		{
			this.aSynchOperationHashCode = null;
			this.allowSceneActivation = true;
			this.progress = null;
			this.isDone = null;
			this.doneEvent = null;
			this.failureEvent = null;
		}

		// Token: 0x0600A47E RID: 42110 RVA: 0x003431BA File Offset: 0x003413BA
		public override void OnEnter()
		{
			this.DoAllowSceneActivation();
		}

		// Token: 0x0600A47F RID: 42111 RVA: 0x003431C4 File Offset: 0x003413C4
		public override void OnUpdate()
		{
			if (!this.progress.IsNone)
			{
				this.progress.Value = LoadSceneAsynch.aSyncOperationLUT[this.aSynchOperationHashCode.Value].progress;
			}
			if (!this.isDone.IsNone)
			{
				this.isDone.Value = LoadSceneAsynch.aSyncOperationLUT[this.aSynchOperationHashCode.Value].isDone;
			}
			if (LoadSceneAsynch.aSyncOperationLUT[this.aSynchOperationHashCode.Value].isDone)
			{
				LoadSceneAsynch.aSyncOperationLUT.Remove(this.aSynchOperationHashCode.Value);
				base.Fsm.Event(this.doneEvent);
				base.Finish();
				return;
			}
		}

		// Token: 0x0600A480 RID: 42112 RVA: 0x00343280 File Offset: 0x00341480
		private void DoAllowSceneActivation()
		{
			if (this.aSynchOperationHashCode.IsNone || this.allowSceneActivation.IsNone || LoadSceneAsynch.aSyncOperationLUT == null || !LoadSceneAsynch.aSyncOperationLUT.ContainsKey(this.aSynchOperationHashCode.Value))
			{
				base.Fsm.Event(this.failureEvent);
				base.Finish();
				return;
			}
			LoadSceneAsynch.aSyncOperationLUT[this.aSynchOperationHashCode.Value].allowSceneActivation = this.allowSceneActivation.Value;
		}

		// Token: 0x04008AD6 RID: 35542
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The name of the new scene. It cannot be empty or null, or same as the name of the existing scenes.")]
		public FsmInt aSynchOperationHashCode;

		// Token: 0x04008AD7 RID: 35543
		[Tooltip("Allow the scene to be activated")]
		public FsmBool allowSceneActivation;

		// Token: 0x04008AD8 RID: 35544
		[ActionSection("Result")]
		[Tooltip("The loading's progress.")]
		[UIHint(UIHint.Variable)]
		public FsmFloat progress;

		// Token: 0x04008AD9 RID: 35545
		[Tooltip("True when loading is done")]
		[UIHint(UIHint.Variable)]
		public FsmBool isDone;

		// Token: 0x04008ADA RID: 35546
		[Tooltip("Event sent when scene loading is done")]
		public FsmEvent doneEvent;

		// Token: 0x04008ADB RID: 35547
		[Tooltip("Event sent when action could not be performed. Check Log for more information")]
		public FsmEvent failureEvent;
	}
}
