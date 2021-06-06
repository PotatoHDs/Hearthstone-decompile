using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200074F RID: 1871
public class InstallReferrer
{
	// Token: 0x060069A8 RID: 27048 RVA: 0x00226E5C File Offset: 0x0022505C
	public bool RequestAsync(AndroidJavaObject context, InstallReferrer.ReferrerReceivedCallback callback)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			using (AndroidJavaObject androidJavaObject = new AndroidJavaObject("com.blizzard.telemetry.sdk.platform.InstallReferrer", Array.Empty<object>()))
			{
				androidJavaObject.Call("RequestReferrer", new object[]
				{
					context,
					new InstallReferrer.ReferrerCallback(callback)
				});
			}
			return true;
		}
		Debug.LogError("Install referrer url is only supported on Android devices.");
		return false;
	}

	// Token: 0x060069A9 RID: 27049 RVA: 0x00226ECC File Offset: 0x002250CC
	private void OnReferrerReceived(int responseCode, string referrerUrl)
	{
		object obj = this.thisLock_;
		lock (obj)
		{
			this.responseCode_ = responseCode;
			this.referrerUrl_ = referrerUrl;
			this.done_ = true;
		}
	}

	// Token: 0x060069AA RID: 27050 RVA: 0x00226F1C File Offset: 0x0022511C
	public static IEnumerator RequestCoroutine(InstallReferrer.ReferrerReceivedCallback callback)
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			Debug.LogError("Install referrer url is only supported on Android devices.");
			yield break;
		}
		using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		{
			using (AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
			{
				using (AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext", Array.Empty<object>()))
				{
					InstallReferrer referrer = new InstallReferrer();
					if (referrer.RequestAsync(context, new InstallReferrer.ReferrerReceivedCallback(referrer.OnReferrerReceived)))
					{
						object obj;
						for (;;)
						{
							obj = referrer.thisLock_;
							lock (obj)
							{
								if (referrer.done_)
								{
									break;
								}
							}
							yield return null;
						}
						obj = referrer.thisLock_;
						int responseCode;
						string referrerUrl;
						lock (obj)
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
					referrer = null;
				}
				AndroidJavaObject context = null;
			}
			AndroidJavaObject activity = null;
		}
		AndroidJavaClass unityPlayer = null;
		yield break;
		yield break;
	}

	// Token: 0x0400566F RID: 22127
	private readonly object thisLock_ = new object();

	// Token: 0x04005670 RID: 22128
	private bool done_;

	// Token: 0x04005671 RID: 22129
	private string referrerUrl_;

	// Token: 0x04005672 RID: 22130
	private int responseCode_ = -1;

	// Token: 0x0200232C RID: 9004
	// (Invoke) Token: 0x06012A1E RID: 76318
	public delegate void ReferrerReceivedCallback(int responseCode, string referrerUrl);

	// Token: 0x0200232D RID: 9005
	private class ReferrerCallback : AndroidJavaProxy
	{
		// Token: 0x06012A21 RID: 76321 RVA: 0x005117F1 File Offset: 0x0050F9F1
		public ReferrerCallback(InstallReferrer.ReferrerReceivedCallback callback) : base("com.blizzard.telemetry.sdk.platform.InstallReferrerCallback")
		{
			this.callback_ = callback;
		}

		// Token: 0x06012A22 RID: 76322 RVA: 0x00511805 File Offset: 0x0050FA05
		public void OnReceivedReferrer(int responseCode, string referrerUrl)
		{
			if (this.callback_ != null)
			{
				this.callback_(responseCode, referrerUrl);
			}
		}

		// Token: 0x0400E5EB RID: 58859
		private InstallReferrer.ReferrerReceivedCallback callback_;
	}
}
