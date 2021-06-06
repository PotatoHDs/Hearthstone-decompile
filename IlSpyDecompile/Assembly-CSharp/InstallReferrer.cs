using System;
using System.Collections;
using UnityEngine;

public class InstallReferrer
{
	public delegate void ReferrerReceivedCallback(int responseCode, string referrerUrl);

	private class ReferrerCallback : AndroidJavaProxy
	{
		private ReferrerReceivedCallback callback_;

		public ReferrerCallback(ReferrerReceivedCallback callback)
			: base("com.blizzard.telemetry.sdk.platform.InstallReferrerCallback")
		{
			callback_ = callback;
		}

		public void OnReceivedReferrer(int responseCode, string referrerUrl)
		{
			if (callback_ != null)
			{
				callback_(responseCode, referrerUrl);
			}
		}
	}

	private readonly object thisLock_ = new object();

	private bool done_;

	private string referrerUrl_;

	private int responseCode_ = -1;

	public bool RequestAsync(AndroidJavaObject context, ReferrerReceivedCallback callback)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			using (AndroidJavaObject androidJavaObject = new AndroidJavaObject("com.blizzard.telemetry.sdk.platform.InstallReferrer"))
			{
				androidJavaObject.Call("RequestReferrer", context, new ReferrerCallback(callback));
			}
			return true;
		}
		Debug.LogError("Install referrer url is only supported on Android devices.");
		return false;
	}

	private void OnReferrerReceived(int responseCode, string referrerUrl)
	{
		lock (thisLock_)
		{
			responseCode_ = responseCode;
			referrerUrl_ = referrerUrl;
			done_ = true;
		}
	}

	public static IEnumerator RequestCoroutine(ReferrerReceivedCallback callback)
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			Debug.LogError("Install referrer url is only supported on Android devices.");
			yield break;
		}
		using AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		using AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
		using AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext", Array.Empty<object>());
		InstallReferrer referrer = new InstallReferrer();
		if (referrer.RequestAsync(context, referrer.OnReferrerReceived))
		{
			while (true)
			{
				lock (referrer.thisLock_)
				{
					if (referrer.done_)
					{
						break;
					}
				}
				yield return null;
			}
			int responseCode;
			string referrerUrl;
			lock (referrer.thisLock_)
			{
				responseCode = referrer.responseCode_;
				referrerUrl = string.Copy(referrer.referrerUrl_);
			}
			callback(responseCode, referrerUrl);
		}
		else
		{
			callback(-1, "");
		}
	}
}
