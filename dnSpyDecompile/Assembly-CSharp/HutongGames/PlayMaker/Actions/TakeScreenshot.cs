using System;
using System.IO;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E1D RID: 3613
	[ActionCategory(ActionCategory.Application)]
	[Tooltip("Saves a Screenshot. NOTE: Does nothing in Web Player. On Android, the resulting screenshot is available some time later.")]
	public class TakeScreenshot : FsmStateAction
	{
		// Token: 0x0600A742 RID: 42818 RVA: 0x0034BDDA File Offset: 0x00349FDA
		public override void Reset()
		{
			this.destination = TakeScreenshot.Destination.MyPictures;
			this.filename = "";
			this.autoNumber = null;
			this.superSize = null;
			this.debugLog = null;
		}

		// Token: 0x0600A743 RID: 42819 RVA: 0x0034BE08 File Offset: 0x0034A008
		public override void OnEnter()
		{
			if (string.IsNullOrEmpty(this.filename.Value))
			{
				return;
			}
			string text;
			switch (this.destination)
			{
			case TakeScreenshot.Destination.MyPictures:
				text = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
				break;
			case TakeScreenshot.Destination.PersistentDataPath:
				text = Application.persistentDataPath;
				break;
			case TakeScreenshot.Destination.CustomPath:
				text = this.customPath.Value;
				break;
			default:
				text = "";
				break;
			}
			text = text.Replace("\\", "/") + "/";
			string text2 = text + this.filename.Value + ".png";
			if (this.autoNumber.Value)
			{
				while (File.Exists(text2))
				{
					this.screenshotCount++;
					text2 = string.Concat(new object[]
					{
						text,
						this.filename.Value,
						this.screenshotCount,
						".png"
					});
				}
			}
			if (this.debugLog.Value)
			{
				Debug.Log("TakeScreenshot: " + text2);
			}
			ScreenCapture.CaptureScreenshot(text2, this.superSize.Value);
			base.Finish();
		}

		// Token: 0x04008DC5 RID: 36293
		[Tooltip("Where to save the screenshot.")]
		public TakeScreenshot.Destination destination;

		// Token: 0x04008DC6 RID: 36294
		[Tooltip("Path used with Custom Path Destination option.")]
		public FsmString customPath;

		// Token: 0x04008DC7 RID: 36295
		[RequiredField]
		public FsmString filename;

		// Token: 0x04008DC8 RID: 36296
		[Tooltip("Add an auto-incremented number to the filename.")]
		public FsmBool autoNumber;

		// Token: 0x04008DC9 RID: 36297
		[Tooltip("Factor by which to increase resolution.")]
		public FsmInt superSize;

		// Token: 0x04008DCA RID: 36298
		[Tooltip("Log saved file info in Unity console.")]
		public FsmBool debugLog;

		// Token: 0x04008DCB RID: 36299
		private int screenshotCount;

		// Token: 0x020027AC RID: 10156
		public enum Destination
		{
			// Token: 0x0400F52A RID: 62762
			MyPictures,
			// Token: 0x0400F52B RID: 62763
			PersistentDataPath,
			// Token: 0x0400F52C RID: 62764
			CustomPath
		}
	}
}
