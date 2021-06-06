using System;
using System.IO;
using UnityEngine;

namespace Blizzard.BlizzardErrorMobile
{
	internal static class Screenshot
	{
		private static string m_screenshotPath;

		public static string ScreenshotPath
		{
			get
			{
				if (string.IsNullOrEmpty(m_screenshotPath))
				{
					m_screenshotPath = Path.Combine(Application.persistentDataPath, "Screenshot.png");
				}
				return m_screenshotPath;
			}
		}

		public static void RemoveScreenshot()
		{
			if (File.Exists(ScreenshotPath))
			{
				File.Delete(ScreenshotPath);
			}
		}

		public static bool CaptureScreenshot(int maxWidth)
		{
			RemoveScreenshot();
			if (maxWidth < 0)
			{
				ExceptionLogger.LogDebug("Skip to generate Screenshot");
				return false;
			}
			ExceptionLogger.LogDebug("Making Screenshot");
			Texture2D texture2D = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, mipChain: true);
			texture2D.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height), 0, 0);
			texture2D.Apply();
			if (maxWidth > 0)
			{
				ExceptionLogger.LogDebug("Resizing Screenshot");
				int width = texture2D.width;
				int height = texture2D.height;
				float num = (float)maxWidth / (float)texture2D.width;
				width = maxWidth;
				height = Convert.ToInt32((float)texture2D.height * num);
				texture2D = ScaleTexture(texture2D, width, height);
			}
			byte[] bytes = texture2D.EncodeToPNG();
			File.WriteAllBytes(ScreenshotPath, bytes);
			ExceptionLogger.LogDebug("Captured {0}", ScreenshotPath);
			return true;
		}

		private static Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
		{
			Texture2D texture2D = new Texture2D(targetWidth, targetHeight, source.format, mipChain: true);
			Color[] pixels = texture2D.GetPixels(0);
			float num = 1f / (float)source.width * ((float)source.width / (float)targetWidth);
			float num2 = 1f / (float)source.height * ((float)source.height / (float)targetHeight);
			for (int i = 0; i < pixels.Length; i++)
			{
				pixels[i] = source.GetPixelBilinear(num * ((float)i % (float)targetWidth), num2 * Mathf.Floor(i / targetWidth));
			}
			texture2D.SetPixels(pixels, 0);
			texture2D.Apply();
			return texture2D;
		}
	}
}
