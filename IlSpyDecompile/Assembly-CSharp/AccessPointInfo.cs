using System;

public class AccessPointInfo : IComparable
{
	public string ssid;

	public string bssid;

	public float signalStrength;

	public override string ToString()
	{
		return $"ssid={ssid} bssid={bssid} signalStrength={signalStrength}";
	}

	public int CompareTo(object obj)
	{
		AccessPointInfo accessPointInfo = obj as AccessPointInfo;
		if (accessPointInfo == null)
		{
			return -1;
		}
		return -signalStrength.CompareTo(accessPointInfo.signalStrength);
	}
}
