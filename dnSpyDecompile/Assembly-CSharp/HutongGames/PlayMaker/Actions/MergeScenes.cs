using System;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D8F RID: 3471
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("This will merge the source scene into the destinationScene. This function merges the contents of the source scene into the destination scene, and deletes the source scene. All GameObjects at the root of the source scene are moved to the root of the destination scene. NOTE: This function is destructive: The source scene will be destroyed once the merge has been completed.")]
	public class MergeScenes : FsmStateAction
	{
		// Token: 0x0600A4D2 RID: 42194 RVA: 0x0034458C File Offset: 0x0034278C
		public override void Reset()
		{
			this.sourceReference = GetSceneActionBase.SceneAllReferenceOptions.SceneAtIndex;
			this.sourceByPath = null;
			this.sourceByName = null;
			this.sourceAtIndex = null;
			this.sourceByGameObject = null;
			this.destinationReference = GetSceneActionBase.SceneAllReferenceOptions.ActiveScene;
			this.destinationByPath = null;
			this.destinationByName = null;
			this.destinationAtIndex = null;
			this.destinationByGameObject = null;
			this.success = null;
			this.successEvent = null;
			this.failureEvent = null;
		}

		// Token: 0x0600A4D3 RID: 42195 RVA: 0x003445F4 File Offset: 0x003427F4
		public override void OnEnter()
		{
			this.GetSourceScene();
			this.GetDestinationScene();
			if (this._destinationFound && this._sourceFound)
			{
				if (this._destinationScene.Equals(this._sourceScene))
				{
					base.LogError("Source and Destination scenes can not be the same");
				}
				else
				{
					SceneManager.MergeScenes(this._sourceScene, this._destinationScene);
				}
				this.success.Value = true;
				base.Fsm.Event(this.successEvent);
			}
			else
			{
				this.success.Value = false;
				base.Fsm.Event(this.failureEvent);
			}
			base.Finish();
		}

		// Token: 0x0600A4D4 RID: 42196 RVA: 0x0034469C File Offset: 0x0034289C
		private void GetSourceScene()
		{
			try
			{
				switch (this.sourceReference)
				{
				case GetSceneActionBase.SceneAllReferenceOptions.ActiveScene:
					this._sourceScene = SceneManager.GetActiveScene();
					break;
				case GetSceneActionBase.SceneAllReferenceOptions.SceneAtIndex:
					this._sourceScene = SceneManager.GetSceneAt(this.sourceAtIndex.Value);
					break;
				case GetSceneActionBase.SceneAllReferenceOptions.SceneByName:
					this._sourceScene = SceneManager.GetSceneByName(this.sourceByName.Value);
					break;
				case GetSceneActionBase.SceneAllReferenceOptions.SceneByPath:
					this._sourceScene = SceneManager.GetSceneByPath(this.sourceByPath.Value);
					break;
				}
			}
			catch (Exception ex)
			{
				base.LogError(ex.Message);
			}
			if (this._sourceScene == default(Scene))
			{
				this._sourceFound = false;
				return;
			}
			this._sourceFound = true;
		}

		// Token: 0x0600A4D5 RID: 42197 RVA: 0x00344764 File Offset: 0x00342964
		private void GetDestinationScene()
		{
			try
			{
				switch (this.sourceReference)
				{
				case GetSceneActionBase.SceneAllReferenceOptions.ActiveScene:
					this._destinationScene = SceneManager.GetActiveScene();
					break;
				case GetSceneActionBase.SceneAllReferenceOptions.SceneAtIndex:
					this._destinationScene = SceneManager.GetSceneAt(this.destinationAtIndex.Value);
					break;
				case GetSceneActionBase.SceneAllReferenceOptions.SceneByName:
					this._destinationScene = SceneManager.GetSceneByName(this.destinationByName.Value);
					break;
				case GetSceneActionBase.SceneAllReferenceOptions.SceneByPath:
					this._destinationScene = SceneManager.GetSceneByPath(this.destinationByPath.Value);
					break;
				}
			}
			catch (Exception ex)
			{
				base.LogError(ex.Message);
			}
			if (this._destinationScene == default(Scene))
			{
				this._destinationFound = false;
				return;
			}
			this._destinationFound = true;
		}

		// Token: 0x0600A4D6 RID: 42198 RVA: 0x0034482C File Offset: 0x00342A2C
		public override string ErrorCheck()
		{
			if (this.sourceReference == GetSceneActionBase.SceneAllReferenceOptions.ActiveScene && this.destinationReference == GetSceneActionBase.SceneAllReferenceOptions.ActiveScene)
			{
				return "Source and Destination scenes can not be the same";
			}
			return string.Empty;
		}

		// Token: 0x04008B43 RID: 35651
		[ActionSection("Source")]
		[Tooltip("The reference options of the source Scene")]
		public GetSceneActionBase.SceneAllReferenceOptions sourceReference;

		// Token: 0x04008B44 RID: 35652
		[Tooltip("The source scene Index.")]
		public FsmInt sourceAtIndex;

		// Token: 0x04008B45 RID: 35653
		[Tooltip("The source scene Name.")]
		public FsmString sourceByName;

		// Token: 0x04008B46 RID: 35654
		[Tooltip("The source scene Path.")]
		public FsmString sourceByPath;

		// Token: 0x04008B47 RID: 35655
		[Tooltip("The source scene from GameObject")]
		public FsmOwnerDefault sourceByGameObject;

		// Token: 0x04008B48 RID: 35656
		[ActionSection("Destination")]
		[Tooltip("The reference options of the destination Scene")]
		public GetSceneActionBase.SceneAllReferenceOptions destinationReference;

		// Token: 0x04008B49 RID: 35657
		[Tooltip("The destination scene Index.")]
		public FsmInt destinationAtIndex;

		// Token: 0x04008B4A RID: 35658
		[Tooltip("The destination scene Name.")]
		public FsmString destinationByName;

		// Token: 0x04008B4B RID: 35659
		[Tooltip("The destination scene Path.")]
		public FsmString destinationByPath;

		// Token: 0x04008B4C RID: 35660
		[Tooltip("The destination scene from GameObject")]
		public FsmOwnerDefault destinationByGameObject;

		// Token: 0x04008B4D RID: 35661
		[ActionSection("Result")]
		[Tooltip("True if the merge succeeded")]
		[UIHint(UIHint.Variable)]
		public FsmBool success;

		// Token: 0x04008B4E RID: 35662
		[Tooltip("Event sent if merge succeeded")]
		public FsmEvent successEvent;

		// Token: 0x04008B4F RID: 35663
		[Tooltip("Event sent if merge failed")]
		public FsmEvent failureEvent;

		// Token: 0x04008B50 RID: 35664
		private Scene _sourceScene;

		// Token: 0x04008B51 RID: 35665
		private bool _sourceFound;

		// Token: 0x04008B52 RID: 35666
		private Scene _destinationScene;

		// Token: 0x04008B53 RID: 35667
		private bool _destinationFound;
	}
}
