using System;
using UnityEngine;

// Token: 0x020007AC RID: 1964
public class GpsCoordinate
{
	// Token: 0x06006CF3 RID: 27891 RVA: 0x00232FBC File Offset: 0x002311BC
	public static implicit operator GpsCoordinate(LocationInfo locationInfo)
	{
		return new GpsCoordinate
		{
			Latitude = (double)locationInfo.latitude,
			Longitude = (double)locationInfo.longitude,
			Accuracy = ((locationInfo.horizontalAccuracy > 0f) ? ((double)locationInfo.horizontalAccuracy) : double.MaxValue),
			Timestamp = locationInfo.timestamp
		};
	}

	// Token: 0x06006CF4 RID: 27892 RVA: 0x0023301E File Offset: 0x0023121E
	public GpsCoordinate()
	{
	}

	// Token: 0x06006CF5 RID: 27893 RVA: 0x00233035 File Offset: 0x00231235
	public GpsCoordinate(double latitude, double longitude, double accuracy, double timestamp)
	{
		this.Latitude = latitude;
		this.Longitude = longitude;
		this.Accuracy = accuracy;
		this.Timestamp = timestamp;
	}

	// Token: 0x06006CF6 RID: 27894 RVA: 0x0023306C File Offset: 0x0023126C
	public override string ToString()
	{
		return string.Format("[{0}, {1}] +/-{2}m, {3}s ago", new object[]
		{
			this.Latitude,
			this.Longitude,
			this.Accuracy,
			(int)this.Age()
		});
	}

	// Token: 0x06006CF7 RID: 27895 RVA: 0x002330C4 File Offset: 0x002312C4
	public float Age()
	{
		return (float)(TimeUtils.GetElapsedTimeSinceEpoch(null).TotalSeconds - this.Timestamp);
	}

	// Token: 0x06006CF8 RID: 27896 RVA: 0x002330F0 File Offset: 0x002312F0
	public static double HaversineDistance(GpsCoordinate p0, GpsCoordinate p1)
	{
		if (p0 == null || p1 == null)
		{
			return double.MaxValue;
		}
		double num = 0.01745329238474369 * (p1.Latitude - p0.Latitude);
		double num2 = 0.01745329238474369 * (p1.Longitude - p0.Longitude);
		double d = Math.Sin(num / 2.0) * Math.Sin(num / 2.0) + Math.Cos(0.01745329238474369 * p0.Latitude) * Math.Cos(0.01745329238474369 * p1.Latitude) * Math.Sin(num2 / 2.0) * Math.Sin(num2 / 2.0);
		double num3 = 2.0 * Math.Asin(Math.Min(1.0, Math.Sqrt(d)));
		return 6371000.0 * num3;
	}

	// Token: 0x06006CF9 RID: 27897 RVA: 0x002331DC File Offset: 0x002313DC
	public static double DistancePaddedWithAccuracy(GpsCoordinate p0, GpsCoordinate p1)
	{
		if (p0 == null || p1 == null)
		{
			return double.MaxValue;
		}
		return GpsCoordinate.HaversineDistance(p0, p1) + p0.Accuracy + p1.Accuracy;
	}

	// Token: 0x06006CFA RID: 27898 RVA: 0x00233203 File Offset: 0x00231403
	public bool Equals(GpsCoordinate other)
	{
		return other != null && (this.Longitude == other.Longitude && this.Latitude == other.Latitude) && this.Accuracy == other.Accuracy;
	}

	// Token: 0x040057CF RID: 22479
	public double Longitude;

	// Token: 0x040057D0 RID: 22480
	public double Latitude;

	// Token: 0x040057D1 RID: 22481
	public double Accuracy = double.MaxValue;

	// Token: 0x040057D2 RID: 22482
	public double Timestamp;
}
