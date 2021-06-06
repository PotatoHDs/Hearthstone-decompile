using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;

public class WifiInfo : IService
{
	private List<AccessPointInfo> m_lastKnownAccessPoints = new List<AccessPointInfo>();

	private bool m_waitingForResponse;

	private const int m_accessPointScanAttempts = 3;

	private string m_connectedSSID;

	public bool IsAvailable { get; private set; }

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		IsAvailable = false;
		yield return new ServiceSoftDependency(typeof(LoginManager), serviceLocator);
		IsAvailable = DoWifiScan();
	}

	public Type[] GetDependencies()
	{
		return new Type[1] { typeof(MobileCallbackManager) };
	}

	public void Shutdown()
	{
	}

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
			Log.FiresideGatherings.Print("Failed to execute netsh: " + ex.Message);
			return false;
		}
		string[] array = process.StandardOutput.ReadToEnd().Split(new string[1] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
		if (array == null || array.Length < 2)
		{
			return false;
		}
		List<AccessPointInfo> list = new List<AccessPointInfo>();
		string text = null;
		AccessPointInfo accessPointInfo = null;
		string[] array2 = array;
		foreach (string text2 in array2)
		{
			if (text2.TrimStart().StartsWith("SSID"))
			{
				text = text2.Substring(text2.IndexOf(':', 0) + 1).Trim();
			}
			else if (text2.TrimStart().StartsWith("BSSID"))
			{
				if (text == null)
				{
					Log.FiresideGatherings.Print("Warning currentSSID is Null");
				}
				if (accessPointInfo != null)
				{
					Log.FiresideGatherings.Print("Failed to find BSSID");
					return true;
				}
				accessPointInfo = new AccessPointInfo();
				accessPointInfo.ssid = text;
				accessPointInfo.bssid = text2.Substring(text2.IndexOf(':', 0) + 1).Trim();
			}
			else if (text2.TrimStart().StartsWith("Signal"))
			{
				if (accessPointInfo == null)
				{
					Log.FiresideGatherings.Print("Failed to find Signal");
					return true;
				}
				string text3 = text2.Substring(text2.IndexOf(':', 0) + 1).Trim();
				accessPointInfo.signalStrength = Convert.ToInt32(text3.Substring(0, text3.Length - 1));
				list.Add(accessPointInfo);
				accessPointInfo = null;
			}
		}
		ReceiveVisibleAccessPointList(list);
		Process process2 = new Process();
		process2.StartInfo.FileName = "netsh.exe";
		process2.StartInfo.Arguments = "wlan show interfaces";
		process2.StartInfo.UseShellExecute = false;
		process2.StartInfo.RedirectStandardOutput = true;
		process2.StartInfo.CreateNoWindow = true;
		process2.Start();
		array = process2.StandardOutput.ReadToEnd().Split(new string[1] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
		if (array == null)
		{
			return true;
		}
		m_connectedSSID = null;
		array2 = array;
		foreach (string text4 in array2)
		{
			if (text4.TrimStart().StartsWith("State"))
			{
				if (!text4.Substring(text4.IndexOf(':', 0) + 1).Trim().Equals("connected"))
				{
					break;
				}
			}
			else if (text4.TrimStart().StartsWith("SSID"))
			{
				m_connectedSSID = text4.Substring(text4.IndexOf(':', 0) + 1).Trim();
				break;
			}
		}
		return true;
	}

	public bool IsScanningWifi()
	{
		return m_waitingForResponse;
	}

	public string GetConnectedSSIDString()
	{
		IsAvailable = DoWifiScan();
		if (IsAvailable)
		{
			return m_connectedSSID;
		}
		return null;
	}

	public IEnumerator RequestVisibleAccessPoints()
	{
		if (!m_waitingForResponse)
		{
			m_waitingForResponse = false;
			IsAvailable = DoWifiScan();
			while (m_waitingForResponse)
			{
				yield return null;
			}
		}
	}

	public void ReceiveVisibleAccessPoints(string accessPointData)
	{
		List<AccessPointInfo> list = new List<AccessPointInfo>();
		if (string.IsNullOrEmpty(accessPointData))
		{
			ReceiveVisibleAccessPointList(list);
			return;
		}
		char[] separator = new char[1] { '\n' };
		string[] array = accessPointData.Split(separator);
		for (int i = 0; i < array.Length; i++)
		{
			char[] separator2 = new char[1] { '\t' };
			string[] array2 = array[i].Split(separator2);
			if (string.IsNullOrEmpty(array[i].Trim()))
			{
				continue;
			}
			if (array2.Length != 3)
			{
				Log.FiresideGatherings.PrintWarning("Invalid access point data string: \"{0}\"", array[i]);
				continue;
			}
			string[] array3 = array2[1].Split(':');
			for (int j = 0; j < array3.Length; j++)
			{
				if (array3[j].Length < 2)
				{
					array3[j] = "0" + array3[j];
				}
			}
			string bssid = string.Join(":", array3);
			float.TryParse(array2[2], out var result);
			AccessPointInfo item = new AccessPointInfo
			{
				ssid = array2[0],
				bssid = bssid,
				signalStrength = result
			};
			list.Add(item);
		}
		ReceiveVisibleAccessPointList(list);
	}

	public void ReceiveVisibleAccessPointList(List<AccessPointInfo> points)
	{
		m_waitingForResponse = false;
		m_lastKnownAccessPoints = points;
		if (points != null && points.Count >= 1)
		{
			points.Sort();
		}
	}

	public List<AccessPointInfo> GetLastKnownAccessPoints()
	{
		return m_lastKnownAccessPoints;
	}
}
