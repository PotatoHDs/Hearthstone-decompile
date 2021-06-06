using System;
using UnityEngine;

public class GpsCoordinate
{
	public double Longitude;

	public double Latitude;

	public double Accuracy = double.MaxValue;

	public double Timestamp;

	public static implicit operator GpsCoordinate(LocationInfo locationInfo)
	{
		return new GpsCoordinate
		{
			Latitude = locationInfo.latitude,
			Longitude = locationInfo.longitude,
			Accuracy = ((locationInfo.horizontalAccuracy > 0f) ? ((double)locationInfo.horizontalAccuracy) : double.MaxValue),
			Timestamp = locationInfo.timestamp
		};
	}

	public GpsCoordinate()
	{
	}

	public GpsCoordinate(double latitude, double longitude, double accuracy, double timestamp)
	{
		Latitude = latitude;
		Longitude = longitude;
		Accuracy = accuracy;
		Timestamp = timestamp;
	}

	public override string ToString()
	{
		return $"[{Latitude}, {Longitude}] +/-{Accuracy}m, {(int)Age()}s ago";
	}

	public float Age()
	{
		return (float)(TimeUtils.GetElapsedTimeSinceEpoch().TotalSeconds - Timestamp);
	}

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

	public static double DistancePaddedWithAccuracy(GpsCoordinate p0, GpsCoordinate p1)
	{
		if (p0 == null || p1 == null)
		{
			return double.MaxValue;
		}
		return HaversineDistance(p0, p1) + p0.Accuracy + p1.Accuracy;
	}

	public bool Equals(GpsCoordinate other)
	{
		if (other == null)
		{
			return false;
		}
		if (Longitude == other.Longitude && Latitude == other.Latitude)
		{
			return Accuracy == other.Accuracy;
		}
		return false;
	}
}
