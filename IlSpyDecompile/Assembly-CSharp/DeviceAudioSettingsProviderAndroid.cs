using System;
using UnityEngine;

public class DeviceAudioSettingsProviderAndroid : IDeviceAudioSettingsProvider
{
	private const string AUDIO_SERVICE = "audio";

	private const int STREAM_MUSIC = 3;

	public float Volume
	{
		get
		{
			try
			{
				using AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
				using AndroidJavaObject androidJavaObject = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
				using AndroidJavaObject androidJavaObject2 = androidJavaObject.Call<AndroidJavaObject>("getSystemService", new object[1] { "audio" });
				int num = androidJavaObject2.Call<int>("getStreamMaxVolume", new object[1] { 3 });
				return (float)androidJavaObject2.Call<int>("getStreamVolume", new object[1] { 3 }) / (float)num;
			}
			catch (Exception ex)
			{
				Log.Sound.PrintWarning("Exception {0} caught when retrieving device volume: {1}", ex.Message, ex.StackTrace);
				return 1f;
			}
		}
	}

	public bool IsMuted => Mathf.Abs(Volume) <= 0.001f;
}
