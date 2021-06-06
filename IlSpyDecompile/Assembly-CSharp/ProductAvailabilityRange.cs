using System;

public class ProductAvailabilityRange
{
	public struct Moment
	{
		public DateTime? DateTimeUtc { get; set; }

		public string SourceEventTimingName { get; set; }

		public long SourceSaleId { get; set; }

		public string GetDateTimeAsString()
		{
			if (!DateTimeUtc.HasValue)
			{
				return "none";
			}
			return DateTimeUtc.Value.ToLocalTime().ToString("g");
		}

		public string GetSourceAsString()
		{
			if (!string.IsNullOrEmpty(SourceEventTimingName))
			{
				return SourceEventTimingName;
			}
			if (SourceSaleId != 0L)
			{
				return $"Sale {SourceSaleId}";
			}
			return "";
		}
	}

	private bool m_isNever;

	public Moment Start { get; set; }

	public Moment End { get; set; }

	public Moment SoftEnd { get; set; }

	public DateTime? StartDateTime => Start.DateTimeUtc;

	public DateTime? EndDateTime => End.DateTimeUtc;

	public DateTime? SoftEndDateTime => SoftEnd.DateTimeUtc;

	public bool IsNever
	{
		get
		{
			return GetDuration().Ticks <= 0;
		}
		set
		{
			m_isNever = value;
		}
	}

	public bool IsAlways
	{
		get
		{
			if (!m_isNever && !StartDateTime.HasValue)
			{
				return !EndDateTime.HasValue;
			}
			return false;
		}
	}

	public ProductAvailabilityRange()
	{
	}

	public ProductAvailabilityRange(string eventTimingName, DateTime? startUtc, DateTime? endUtc)
	{
		Start = new Moment
		{
			DateTimeUtc = startUtc,
			SourceEventTimingName = eventTimingName
		};
		End = new Moment
		{
			DateTimeUtc = endUtc,
			SourceEventTimingName = eventTimingName
		};
		SoftEnd = new Moment
		{
			DateTimeUtc = endUtc - TimeSpan.FromMinutes(10.0),
			SourceEventTimingName = eventTimingName
		};
	}

	public ProductAvailabilityRange(Network.ShopSale shopSale)
	{
		DateTime? dateTimeUtc = shopSale.StartUtc;
		DateTime? dateTimeUtc2 = shopSale.HardEndUtc;
		DateTime? dateTimeUtc3 = shopSale.SoftEndUtc;
		if (dateTimeUtc.HasValue)
		{
			dateTimeUtc = dateTimeUtc.Value.AddSeconds(SpecialEventManager.Get().DevTimeOffsetSeconds * -1);
		}
		if (dateTimeUtc2.HasValue)
		{
			dateTimeUtc2 = dateTimeUtc2.Value.AddSeconds(SpecialEventManager.Get().DevTimeOffsetSeconds * -1);
		}
		if (dateTimeUtc3.HasValue)
		{
			dateTimeUtc3 = dateTimeUtc3.Value.AddSeconds(SpecialEventManager.Get().DevTimeOffsetSeconds * -1);
		}
		Start = new Moment
		{
			DateTimeUtc = dateTimeUtc,
			SourceSaleId = shopSale.SaleId
		};
		End = new Moment
		{
			DateTimeUtc = dateTimeUtc2,
			SourceSaleId = shopSale.SaleId
		};
		SoftEnd = new Moment
		{
			DateTimeUtc = dateTimeUtc3,
			SourceSaleId = shopSale.SaleId
		};
	}

	public bool IsBuyableAtTime(DateTime time)
	{
		if (TryGetTimeDisplacementRequiredToBeBuyable(time, out var displacement))
		{
			return displacement.Ticks == 0;
		}
		return false;
	}

	public bool IsVisibleAtTime(DateTime time)
	{
		if (TryGetTimeDisplacementRequiredToBeVisible(time, out var displacement))
		{
			return displacement.Ticks == 0;
		}
		return false;
	}

	public bool TryGetTimeDisplacementRequiredToBeBuyable(DateTime time, out TimeSpan displacement)
	{
		if (m_isNever)
		{
			displacement = new TimeSpan(0L);
			return false;
		}
		return TryGetDisplacementToRange(time, StartDateTime, EndDateTime, out displacement);
	}

	public bool TryGetTimeDisplacementRequiredToBeVisible(DateTime time, out TimeSpan displacement)
	{
		if (m_isNever)
		{
			displacement = new TimeSpan(0L);
			return false;
		}
		return TryGetDisplacementToRange(time, StartDateTime, SoftEndDateTime, out displacement);
	}

	public static bool TryGetDisplacementToRange(DateTime time, DateTime? start, DateTime? end, out TimeSpan displacement)
	{
		if (start.HasValue && end.HasValue && start.Value >= end.Value)
		{
			displacement = new TimeSpan(0L);
			return false;
		}
		if (start.HasValue && time <= start.Value)
		{
			displacement = start.Value - time;
		}
		else if (end.HasValue && time >= end.Value)
		{
			displacement = end.Value - time;
		}
		else
		{
			displacement = new TimeSpan(0L);
		}
		return true;
	}

	public TimeSpan GetDuration()
	{
		if (m_isNever)
		{
			return new TimeSpan(0L);
		}
		if (StartDateTime.HasValue && EndDateTime.HasValue)
		{
			return EndDateTime.Value - StartDateTime.Value;
		}
		return new TimeSpan(long.MaxValue);
	}

	public static bool AreOverlapping(ProductAvailabilityRange a, ProductAvailabilityRange b)
	{
		if (a.IsNever || b.IsNever)
		{
			return false;
		}
		if (a.IsAlways || b.IsAlways)
		{
			return true;
		}
		DateTime? startDateTime = a.StartDateTime;
		DateTime? endDateTime = a.EndDateTime;
		DateTime? startDateTime2 = b.StartDateTime;
		DateTime? endDateTime2 = b.EndDateTime;
		if (startDateTime.HasValue && b.IsBuyableAtTime(startDateTime.Value))
		{
			return true;
		}
		if (endDateTime.HasValue && b.IsBuyableAtTime(endDateTime.Value))
		{
			return true;
		}
		if (startDateTime2.HasValue && a.IsBuyableAtTime(startDateTime2.Value))
		{
			return true;
		}
		if (endDateTime2.HasValue && a.IsBuyableAtTime(endDateTime2.Value))
		{
			return true;
		}
		return false;
	}

	public static int CompareNullableStartDateTimes(DateTime? a, DateTime? b)
	{
		if (!a.HasValue || !b.HasValue)
		{
			if (a.HasValue)
			{
				return 1;
			}
			if (b.HasValue)
			{
				return -1;
			}
			return 0;
		}
		if (a.Value < b.Value)
		{
			return -1;
		}
		if (a.Value > b.Value)
		{
			return 1;
		}
		return 0;
	}

	public static int CompareNullableEndDateTimes(DateTime? a, DateTime? b)
	{
		if (!a.HasValue || !b.HasValue)
		{
			if (a.HasValue)
			{
				return -1;
			}
			if (b.HasValue)
			{
				return 1;
			}
			return 0;
		}
		if (a.Value < b.Value)
		{
			return -1;
		}
		if (a.Value > b.Value)
		{
			return 1;
		}
		return 0;
	}

	public void UnionWith(ProductAvailabilityRange other)
	{
		if (CompareNullableStartDateTimes(other.StartDateTime, StartDateTime) <= 0)
		{
			Start = other.Start;
		}
		if (CompareNullableEndDateTimes(other.EndDateTime, EndDateTime) >= 0)
		{
			End = other.End;
			SoftEnd = other.SoftEnd;
		}
	}

	public void IntersectWith(ProductAvailabilityRange other)
	{
		if (CompareNullableStartDateTimes(other.StartDateTime, StartDateTime) >= 0)
		{
			Start = other.Start;
		}
		if (CompareNullableEndDateTimes(other.EndDateTime, EndDateTime) <= 0)
		{
			End = other.End;
			SoftEnd = other.SoftEnd;
		}
	}

	public override string ToString()
	{
		if (Start.SourceEventTimingName == End.SourceEventTimingName && Start.SourceSaleId == End.SourceSaleId)
		{
			if (IsAlways)
			{
				return $"always[{Start.GetSourceAsString()}]";
			}
			if (IsNever)
			{
				return $"never[{Start.GetSourceAsString()}]";
			}
			return $"({Start.GetDateTimeAsString()} - {End.GetDateTimeAsString()})[{Start.GetSourceAsString()}]";
		}
		return $"({Start.GetDateTimeAsString()} - {End.GetDateTimeAsString()})[{Start.GetSourceAsString()} - {End.GetSourceAsString()}])";
	}
}
