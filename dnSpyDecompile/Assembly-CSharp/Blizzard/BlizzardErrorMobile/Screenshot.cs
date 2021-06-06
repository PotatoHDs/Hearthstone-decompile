using System;
using System.IO;
using UnityEngine;

namespace Blizzard.BlizzardErrorMobile
{
	// Token: 0x02001219 RID: 4633
	internal static class Screenshot
	{
		// Token: 0x17001079 RID: 4217
		// (get) Token: 0x0600D02D RID: 53293 RVA: 0x003DEB68 File Offset: 0x003DCD68
		public static string ScreenshotPath
		{
			get
			{
				if (string.IsNullOrEmpty(Screenshot.m_screenshotPath))
				{
					Screenshot.m_screenshotPath = Path.Combine(Application.persistentDataPath, "Screenshot.png");
				}
				return Screenshot.m_screenshotPath;
			}
		}

		// Token: 0x0600D02E RID: 53294 RVA: 0x003DEB8F File Offset: 0x003DCD8F
		public static void RemoveScreenshot()
		{
			if (File.Exists(Screenshot.ScreenshotPath))
			{
				File.Delete(Screenshot.ScreenshotPath);
			}
		}

		// Token: 0x0600D02F RID: 53295 RVA: 0x003DEBA8 File Offset: 0x003DCDA8
		public static bool CaptureScreenshot(int maxWidth)
		{
			Screenshot.RemoveScreenshot();
			if (maxWidth < 0)
			{
				ExceptionLogger.LogDebug("Skip to generate Screenshot", Array.Empty<object>());
				return false;
			}
			ExceptionLogger.LogDebug("Making Screenshot", Array.Empty<object>());
			Texture2D texture2D = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true);
			texture2D.ReadPixels(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), 0, 0);
			texture2D.Apply();
			if (maxWidth > 0)
			{
				ExceptionLogger.LogDebug("Resizing Screenshot", Array.Empty<object>());
				int width = texture2D.width;
				int targetHeight = texture2D.height;
				float num = (float)maxWidth / (float)texture2D.width;
				targetHeight = Convert.ToInt32((float)texture2D.height * num);
				texture2D = Screenshot.ScaleTexture(texture2D, maxWidth, targetHeight);
			}
			byte[] bytes = texture2D.EncodeToPNG();
			File.WriteAllBytes(Screenshot.ScreenshotPath, bytes);
			ExceptionLogger.LogDebug("Captured {0}", new object[]
			{
				Screenshot.ScreenshotPath
			});
			return true;
		}

		// Token: 0x0600D030 RID: 53296 RVA: 0x003DEC8C File Offset: 0x003DCE8C
		private static Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
		{
			Texture2D texture2D = new Texture2D(targetWidth, targetHeight, source.format, true);
			Color[] pixels = texture2D.GetPixels(0);
			float num = 1f / (float)source.width * ((float)source.width / (float)targetWidth);
			float num2 = 1f / (float)source.height * ((float)source.height / (float)targetHeight);
			for (int i = 0; i < pixels.Length; i++)
			{
				pixels[i] = source.GetPixelBilinear(num * ((float)i % (float)targetWidth), num2 * Mathf.Floor((float)(i / targetWidth)));
			}
			texture2D.SetPixels(pixels, 0);
			texture2D.Apply();
			return texture2D;
		}

		// Token: 0x0400A263 RID: 41571
		private static string m_screenshotPath;
	}
}
