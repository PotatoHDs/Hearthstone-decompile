using System;
using UnityEngine.Networking;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EC6 RID: 3782
	[ActionCategory("WWW")]
	[Tooltip("Gets data from a url and store it in variables. See Unity WWW docs for more details.")]
	public class WWWObject : FsmStateAction
	{
		// Token: 0x0600AA65 RID: 43621 RVA: 0x00355431 File Offset: 0x00353631
		public override void Reset()
		{
			this.url = null;
			this.storeText = null;
			this.storeTexture = null;
			this.errorString = null;
			this.progress = null;
			this.isDone = null;
		}

		// Token: 0x0600AA66 RID: 43622 RVA: 0x00355460 File Offset: 0x00353660
		public override void OnEnter()
		{
			if (string.IsNullOrEmpty(this.url.Value))
			{
				base.Finish();
				return;
			}
			if (!this.storeTexture.IsNone)
			{
				this.uwr = UnityWebRequestTexture.GetTexture(this.url.Value);
			}
			else
			{
				this.uwr = new UnityWebRequest(this.url.Value);
				this.d = new DownloadHandlerBuffer();
				this.uwr.downloadHandler = this.d;
			}
			this.uwr.SendWebRequest();
		}

		// Token: 0x0600AA67 RID: 43623 RVA: 0x003554EC File Offset: 0x003536EC
		public override void OnUpdate()
		{
			if (this.uwr == null)
			{
				this.errorString.Value = "Unity Web Request is Null!";
				base.Finish();
				return;
			}
			this.errorString.Value = this.uwr.error;
			if (!string.IsNullOrEmpty(this.uwr.error))
			{
				base.Finish();
				base.Fsm.Event(this.isError);
				return;
			}
			this.progress.Value = this.uwr.downloadProgress;
			if (this.progress.Value.Equals(1f))
			{
				if (!this.storeText.IsNone)
				{
					this.storeText.Value = this.uwr.downloadHandler.text;
				}
				if (!this.storeTexture.IsNone)
				{
					this.storeTexture.Value = ((DownloadHandlerTexture)this.uwr.downloadHandler).texture;
				}
				this.errorString.Value = this.uwr.error;
				base.Fsm.Event(string.IsNullOrEmpty(this.errorString.Value) ? this.isDone : this.isError);
				base.Finish();
			}
		}

		// Token: 0x040090FC RID: 37116
		[RequiredField]
		[Tooltip("Url to download data from.")]
		public FsmString url;

		// Token: 0x040090FD RID: 37117
		[ActionSection("Results")]
		[UIHint(UIHint.Variable)]
		[Tooltip("Gets text from the url.")]
		public FsmString storeText;

		// Token: 0x040090FE RID: 37118
		[UIHint(UIHint.Variable)]
		[Tooltip("Gets a Texture from the url.")]
		public FsmTexture storeTexture;

		// Token: 0x040090FF RID: 37119
		[UIHint(UIHint.Variable)]
		[Tooltip("Error message if there was an error during the download.")]
		public FsmString errorString;

		// Token: 0x04009100 RID: 37120
		[UIHint(UIHint.Variable)]
		[Tooltip("How far the download progressed (0-1).")]
		public FsmFloat progress;

		// Token: 0x04009101 RID: 37121
		[ActionSection("Events")]
		[Tooltip("Event to send when the data has finished loading (progress = 1).")]
		public FsmEvent isDone;

		// Token: 0x04009102 RID: 37122
		[Tooltip("Event to send if there was an error.")]
		public FsmEvent isError;

		// Token: 0x04009103 RID: 37123
		private UnityWebRequest uwr;

		// Token: 0x04009104 RID: 37124
		private DownloadHandlerBuffer d;
	}
}
