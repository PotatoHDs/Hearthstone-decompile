using System;

// Token: 0x020007AE RID: 1966
public class AccessPointInfo : IComparable
{
	// Token: 0x06006D14 RID: 27924 RVA: 0x0023343A File Offset: 0x0023163A
	public override string ToString()
	{
		return string.Format("ssid={0} bssid={1} signalStrength={2}", this.ssid, this.bssid, this.signalStrength);
	}

	// Token: 0x06006D15 RID: 27925 RVA: 0x00233460 File Offset: 0x00231660
	public int CompareTo(object obj)
	{
		AccessPointInfo accessPointInfo = obj as AccessPointInfo;
		if (accessPointInfo == null)
		{
			return -1;
		}
		return -this.signalStrength.CompareTo(accessPointInfo.signalStrength);
	}

	// Token: 0x040057D9 RID: 22489
	public string ssid;

	// Token: 0x040057DA RID: 22490
	public string bssid;

	// Token: 0x040057DB RID: 22491
	public float signalStrength;
}
