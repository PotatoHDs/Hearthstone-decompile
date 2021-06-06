using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;

// Token: 0x020007AF RID: 1967
public class WifiInfo : IService
{
	// Token: 0x17000673 RID: 1651
	// (get) Token: 0x06006D17 RID: 27927 RVA: 0x0023348B File Offset: 0x0023168B
	// (set) Token: 0x06006D18 RID: 27928 RVA: 0x00233493 File Offset: 0x00231693
	public bool IsAvailable { get; private set; }

	// Token: 0x06006D19 RID: 27929 RVA: 0x0023349C File Offset: 0x0023169C
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		this.IsAvailable = false;
		yield return new ServiceSoftDependency(typeof(LoginManager), serviceLocator);
		this.IsAvailable = this.DoWifiScan();
		yield break;
	}

	// Token: 0x06006D1A RID: 27930 RVA: 0x002334B2 File Offset: 0x002316B2
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(MobileCallbackManager)
		};
	}

	// Token: 0x06006D1B RID: 27931 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Shutdown()
	{
	}

	// Token: 0x06006D1C RID: 27932 RVA: 0x002334C8 File Offset: 0x002316C8
	public bool DoWifiScan()
	{
		Process process = new Process
		{
			StartInfo = 
			{
				FileName = "netsh.exe",
				Arguments = "wlan show networks mode=bssid",
				UseShellExecute = false,
				RedirectStandardOutput = true,
				CreateNoWindow = true
			}
		};
		try
		{
			process.Start();
		}
		catch (Exception ex)
		{
			Log.FiresideGatherings.Print("Failed to execute netsh: " + ex.Message, Array.Empty<object>());
			return false;
		}
		string[] array = process.StandardOutput.ReadToEnd().Split(new string[]
		{
			Environment.NewLine
		}, StringSplitOptions.RemoveEmptyEntries);
		if (array == null || array.Length < 2)
		{
			return false;
		}
		List<AccessPointInfo> list = new List<AccessPointInfo>();
		string text = null;
		AccessPointInfo accessPointInfo = null;
		foreach (string text2 in array)
		{
			if (text2.TrimStart(Array.Empty<char>()).StartsWith("SSID"))
			{
				text = text2.Substring(text2.IndexOf(':', 0) + 1).Trim();
			}
			else if (text2.TrimStart(Array.Empty<char>()).StartsWith("BSSID"))
			{
				if (text == null)
				{
					Log.FiresideGatherings.Print("Warning currentSSID is Null", Array.Empty<object>());
				}
				if (accessPointInfo != null)
				{
					Log.FiresideGatherings.Print("Failed to find BSSID", Array.Empty<object>());
					return true;
				}
				accessPointInfo = new AccessPointInfo();
				accessPointInfo.ssid = text;
				accessPointInfo.bssid = text2.Substring(text2.IndexOf(':', 0) + 1).Trim();
			}
			else if (text2.TrimStart(Array.Empty<char>()).StartsWith("Signal"))
			{
				if (accessPointInfo == null)
				{
					Log.FiresideGatherings.Print("Failed to find Signal", Array.Empty<object>());
					return true;
				}
				string text3 = text2.Substring(text2.IndexOf(':', 0) + 1).Trim();
				accessPointInfo.signalStrength = (float)Convert.ToInt32(text3.Substring(0, text3.Length - 1));
				list.Add(accessPointInfo);
				accessPointInfo = null;
			}
		}
		this.ReceiveVisibleAccessPointList(list);
		Process process2 = new Process();
		process2.StartInfo.FileName = "netsh.exe";
		process2.StartInfo.Arguments = "wlan show interfaces";
		process2.StartInfo.UseShellExecute = false;
		process2.StartInfo.RedirectStandardOutput = true;
		process2.StartInfo.CreateNoWindow = true;
		process2.Start();
		array = process2.StandardOutput.ReadToEnd().Split(new string[]
		{
			Environment.NewLine
		}, StringSplitOptions.RemoveEmptyEntries);
		if (array == null)
		{
			return true;
		}
		this.m_connectedSSID = null;
		foreach (string text4 in array)
		{
			if (text4.TrimStart(Array.Empty<char>()).StartsWith("State"))
			{
				if (!text4.Substring(text4.IndexOf(':', 0) + 1).Trim().Equals("connected"))
				{
					break;
				}
			}
			else if (text4.TrimStart(Array.Empty<char>()).StartsWith("SSID"))
			{
				this.m_connectedSSID = text4.Substring(text4.IndexOf(':', 0) + 1).Trim();
				break;
			}
		}
		return true;
	}

	// Token: 0x06006D1D RID: 27933 RVA: 0x00233800 File Offset: 0x00231A00
	public bool IsScanningWifi()
	{
		return this.m_waitingForResponse;
	}

	// Token: 0x06006D1E RID: 27934 RVA: 0x00233808 File Offset: 0x00231A08
	public string GetConnectedSSIDString()
	{
		this.IsAvailable = this.DoWifiScan();
		if (this.IsAvailable)
		{
			return this.m_connectedSSID;
		}
		return null;
	}

	// Token: 0x06006D1F RID: 27935 RVA: 0x00233826 File Offset: 0x00231A26
	public IEnumerator RequestVisibleAccessPoints()
	{
		if (this.m_waitingForResponse)
		{
			yield break;
		}
		this.m_waitingForResponse = false;
		this.IsAvailable = this.DoWifiScan();
		while (this.m_waitingForResponse)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x06006D20 RID: 27936 RVA: 0x00233838 File Offset: 0x00231A38
	public void ReceiveVisibleAccessPoints(string accessPointData)
	{
		List<AccessPointInfo> list = new List<AccessPointInfo>();
		if (string.IsNullOrEmpty(accessPointData))
		{
			this.ReceiveVisibleAccessPointList(list);
			return;
		}
		char[] separator = new char[]
		{
			'\n'
		};
		string[] array = accessPointData.Split(separator);
		for (int i = 0; i < array.Length; i++)
		{
			char[] separator2 = new char[]
			{
				'\t'
			};
			string[] array2 = array[i].Split(separator2);
			if (!string.IsNullOrEmpty(array[i].Trim()))
			{
				if (array2.Length != 3)
				{
					Log.FiresideGatherings.PrintWarning("Invalid access point data string: \"{0}\"", new object[]
					{
						array[i]
					});
				}
				else
				{
					string[] array3 = array2[1].Split(new char[]
					{
						':'
					});
					for (int j = 0; j < array3.Length; j++)
					{
						if (array3[j].Length < 2)
						{
							array3[j] = "0" + array3[j];
						}
					}
					string bssid = string.Join(":", array3);
					float signalStrength;
					float.TryParse(array2[2], out signalStrength);
					AccessPointInfo item = new AccessPointInfo
					{
						ssid = array2[0],
						bssid = bssid,
						signalStrength = signalStrength
					};
					list.Add(item);
				}
			}
		}
		this.ReceiveVisibleAccessPointList(list);
	}

	// Token: 0x06006D21 RID: 27937 RVA: 0x00233969 File Offset: 0x00231B69
	public void ReceiveVisibleAccessPointList(List<AccessPointInfo> points)
	{
		this.m_waitingForResponse = false;
		this.m_lastKnownAccessPoints = points;
		if (points == null || points.Count < 1)
		{
			return;
		}
		points.Sort();
	}

	// Token: 0x06006D22 RID: 27938 RVA: 0x0023398C File Offset: 0x00231B8C
	public List<AccessPointInfo> GetLastKnownAccessPoints()
	{
		return this.m_lastKnownAccessPoints;
	}

	// Token: 0x040057DC RID: 22492
	private List<AccessPointInfo> m_lastKnownAccessPoints = new List<AccessPointInfo>();

	// Token: 0x040057DD RID: 22493
	private bool m_waitingForResponse;

	// Token: 0x040057DE RID: 22494
	private const int m_accessPointScanAttempts = 3;

	// Token: 0x040057E0 RID: 22496
	private string m_connectedSSID;
}
