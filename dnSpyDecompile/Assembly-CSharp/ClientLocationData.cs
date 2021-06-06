using System;
using System.Collections.Generic;
using System.Text;

// Token: 0x020007AA RID: 1962
public class ClientLocationData
{
	// Token: 0x06006CD6 RID: 27862 RVA: 0x00232C58 File Offset: 0x00230E58
	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(string.Format("Best Location:\n{0}\n", this.location));
		stringBuilder.Append("Wifi Samples:\n");
		for (int i = 0; i < this.accessPointSamples.Count; i++)
		{
			stringBuilder.Append(this.accessPointSamples[i] + "\n");
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06006CD7 RID: 27863 RVA: 0x00232CC8 File Offset: 0x00230EC8
	public override bool Equals(object obj)
	{
		ClientLocationData clientLocationData = obj as ClientLocationData;
		return clientLocationData != null && this.complete == clientLocationData.complete && this.location.Equals(clientLocationData.location) && this.accessPointSamples.Equals(clientLocationData.accessPointSamples);
	}

	// Token: 0x06006CD8 RID: 27864 RVA: 0x00232D17 File Offset: 0x00230F17
	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	// Token: 0x040057C2 RID: 22466
	public GpsCoordinate location;

	// Token: 0x040057C3 RID: 22467
	public List<AccessPointInfo> accessPointSamples = new List<AccessPointInfo>();

	// Token: 0x040057C4 RID: 22468
	public bool complete;
}
