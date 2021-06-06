using System;
using UnityEngine;

// Token: 0x02000942 RID: 2370
public class DeviceAudioSettingsProviderAndroid : IDeviceAudioSettingsProvider
{
	// Token: 0x17000774 RID: 1908
	// (get) Token: 0x06008301 RID: 33537 RVA: 0x002A7E50 File Offset: 0x002A6050
	public float Volume
	{
		get
		{
			float result;
			try
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
				{
					using (AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity"))
					{
						using (AndroidJavaObject androidJavaObject = @static.Call<AndroidJavaObject>("getSystemService", new object[]
						{
							"audio"
						}))
						{
							int num = androidJavaObject.Call<int>("getStreamMaxVolume", new object[]
							{
								3
							});
							result = (float)androidJavaObject.Call<int>("getStreamVolume", new object[]
							{
								3
							}) / (float)num;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Sound.PrintWarning("Exception {0} caught when retrieving device volume: {1}", new object[]
				{
					ex.Message,
					ex.StackTrace
				});
				result = 1f;
			}
			return result;
		}
	}

	// Token: 0x17000775 RID: 1909
	// (get) Token: 0x06008302 RID: 33538 RVA: 0x002A7F58 File Offset: 0x002A6158
	public bool IsMuted
	{
		get
		{
			return Mathf.Abs(this.Volume) <= 0.001f;
		}
	}

	// Token: 0x04006DA7 RID: 28071
	private const string AUDIO_SERVICE = "audio";

	// Token: 0x04006DA8 RID: 28072
	private const int STREAM_MUSIC = 3;
}
