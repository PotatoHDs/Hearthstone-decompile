using System.Collections.Generic;
using System.Text;

public class ClientLocationData
{
	public GpsCoordinate location;

	public List<AccessPointInfo> accessPointSamples = new List<AccessPointInfo>();

	public bool complete;

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append($"Best Location:\n{location}\n");
		stringBuilder.Append("Wifi Samples:\n");
		for (int i = 0; i < accessPointSamples.Count; i++)
		{
			stringBuilder.Append(string.Concat(accessPointSamples[i], "\n"));
		}
		return stringBuilder.ToString();
	}

	public override bool Equals(object obj)
	{
		ClientLocationData clientLocationData = obj as ClientLocationData;
		if (clientLocationData == null)
		{
			return false;
		}
		if (complete != clientLocationData.complete)
		{
			return false;
		}
		if (!location.Equals(clientLocationData.location))
		{
			return false;
		}
		return accessPointSamples.Equals(clientLocationData.accessPointSamples);
	}

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}
}
