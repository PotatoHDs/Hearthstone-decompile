using System;

// Token: 0x020006AC RID: 1708
public class ProductAvailabilityRange
{
	// Token: 0x06005F19 RID: 24345 RVA: 0x000052CE File Offset: 0x000034CE
	public ProductAvailabilityRange()
	{
	}

	// Token: 0x06005F1A RID: 24346 RVA: 0x001EEBE0 File Offset: 0x001ECDE0
	public ProductAvailabilityRange(string eventTimingName, DateTime? startUtc, DateTime? endUtc)
	{
		ProductAvailabilityRange.Moment moment = new ProductAvailabilityRange.Moment
		{
			DateTimeUtc = startUtc,
			SourceEventTimingName = eventTimingName
		};
		this.Start = moment;
		moment = new ProductAvailabilityRange.Moment
		{
			DateTimeUtc = endUtc,
			SourceEventTimingName = eventTimingName
		};
		this.End = moment;
		moment = new ProductAvailabilityRange.Moment
		{
			DateTimeUtc = endUtc - TimeSpan.FromMinutes(10.0),
			SourceEventTimingName = eventTimingName
		};
		this.SoftEnd = moment;
	}

	// Token: 0x06005F1B RID: 24347 RVA: 0x001EEC88 File Offset: 0x001ECE88
	public ProductAvailabilityRange(Network.ShopSale shopSale)
	{
		DateTime? startUtc = shopSale.StartUtc;
		DateTime? hardEndUtc = shopSale.HardEndUtc;
		DateTime? softEndUtc = shopSale.SoftEndUtc;
		if (startUtc != null)
		{
			startUtc = new DateTime?(startUtc.Value.AddSeconds((double)(SpecialEventManager.Get().DevTimeOffsetSeconds * -1L)));
		}
		if (hardEndUtc != null)
		{
			hardEndUtc = new DateTime?(hardEndUtc.Value.AddSeconds((double)(SpecialEventManager.Get().DevTimeOffsetSeconds * -1L)));
		}
		if (softEndUtc != null)
		{
			softEndUtc = new DateTime?(softEndUtc.Value.AddSeconds((double)(SpecialEventManager.Get().DevTimeOffsetSeconds * -1L)));
		}
		this.Start = new ProductAvailabilityRange.Moment
		{
			DateTimeUtc = startUtc,
			SourceSaleId = (long)shopSale.SaleId
		};
		this.End = new ProductAvailabilityRange.Moment
		{
			DateTimeUtc = hardEndUtc,
			SourceSaleId = (long)shopSale.SaleId
		};
		this.SoftEnd = new ProductAvailabilityRange.Moment
		{
			DateTimeUtc = softEndUtc,
			SourceSaleId = (long)shopSale.SaleId
		};
	}

	// Token: 0x170005A3 RID: 1443
	// (get) Token: 0x06005F1C RID: 24348 RVA: 0x001EEDA9 File Offset: 0x001ECFA9
	// (set) Token: 0x06005F1D RID: 24349 RVA: 0x001EEDB1 File Offset: 0x001ECFB1
	public ProductAvailabilityRange.Moment Start { get; set; }

	// Token: 0x170005A4 RID: 1444
	// (get) Token: 0x06005F1E RID: 24350 RVA: 0x001EEDBA File Offset: 0x001ECFBA
	// (set) Token: 0x06005F1F RID: 24351 RVA: 0x001EEDC2 File Offset: 0x001ECFC2
	public ProductAvailabilityRange.Moment End { get; set; }

	// Token: 0x170005A5 RID: 1445
	// (get) Token: 0x06005F20 RID: 24352 RVA: 0x001EEDCB File Offset: 0x001ECFCB
	// (set) Token: 0x06005F21 RID: 24353 RVA: 0x001EEDD3 File Offset: 0x001ECFD3
	public ProductAvailabilityRange.Moment SoftEnd { get; set; }

	// Token: 0x170005A6 RID: 1446
	// (get) Token: 0x06005F22 RID: 24354 RVA: 0x001EEDDC File Offset: 0x001ECFDC
	public DateTime? StartDateTime
	{
		get
		{
			return this.Start.DateTimeUtc;
		}
	}

	// Token: 0x170005A7 RID: 1447
	// (get) Token: 0x06005F23 RID: 24355 RVA: 0x001EEDF8 File Offset: 0x001ECFF8
	public DateTime? EndDateTime
	{
		get
		{
			return this.End.DateTimeUtc;
		}
	}

	// Token: 0x170005A8 RID: 1448
	// (get) Token: 0x06005F24 RID: 24356 RVA: 0x001EEE14 File Offset: 0x001ED014
	public DateTime? SoftEndDateTime
	{
		get
		{
			return this.SoftEnd.DateTimeUtc;
		}
	}

	// Token: 0x170005A9 RID: 1449
	// (get) Token: 0x06005F25 RID: 24357 RVA: 0x001EEE30 File Offset: 0x001ED030
	// (set) Token: 0x06005F26 RID: 24358 RVA: 0x001EEE52 File Offset: 0x001ED052
	public bool IsNever
	{
		get
		{
			return this.GetDuration().Ticks <= 0L;
		}
		set
		{
			this.m_isNever = value;
		}
	}

	// Token: 0x170005AA RID: 1450
	// (get) Token: 0x06005F27 RID: 24359 RVA: 0x001EEE5C File Offset: 0x001ED05C
	public bool IsAlways
	{
		get
		{
			return !this.m_isNever && this.StartDateTime == null && this.EndDateTime == null;
		}
	}

	// Token: 0x06005F28 RID: 24360 RVA: 0x001EEE94 File Offset: 0x001ED094
	public bool IsBuyableAtTime(DateTime time)
	{
		TimeSpan timeSpan;
		return this.TryGetTimeDisplacementRequiredToBeBuyable(time, out timeSpan) && timeSpan.Ticks == 0L;
	}

	// Token: 0x06005F29 RID: 24361 RVA: 0x001EEEBC File Offset: 0x001ED0BC
	public bool IsVisibleAtTime(DateTime time)
	{
		TimeSpan timeSpan;
		return this.TryGetTimeDisplacementRequiredToBeVisible(time, out timeSpan) && timeSpan.Ticks == 0L;
	}

	// Token: 0x06005F2A RID: 24362 RVA: 0x001EEEE1 File Offset: 0x001ED0E1
	public bool TryGetTimeDisplacementRequiredToBeBuyable(DateTime time, out TimeSpan displacement)
	{
		if (this.m_isNever)
		{
			displacement = new TimeSpan(0L);
			return false;
		}
		return ProductAvailabilityRange.TryGetDisplacementToRange(time, this.StartDateTime, this.EndDateTime, out displacement);
	}

	// Token: 0x06005F2B RID: 24363 RVA: 0x001EEF0D File Offset: 0x001ED10D
	public bool TryGetTimeDisplacementRequiredToBeVisible(DateTime time, out TimeSpan displacement)
	{
		if (this.m_isNever)
		{
			displacement = new TimeSpan(0L);
			return false;
		}
		return ProductAvailabilityRange.TryGetDisplacementToRange(time, this.StartDateTime, this.SoftEndDateTime, out displacement);
	}

	// Token: 0x06005F2C RID: 24364 RVA: 0x001EEF3C File Offset: 0x001ED13C
	public static bool TryGetDisplacementToRange(DateTime time, DateTime? start, DateTime? end, out TimeSpan displacement)
	{
		if (start != null && end != null && start.Value >= end.Value)
		{
			displacement = new TimeSpan(0L);
			return false;
		}
		if (start != null && time <= start.Value)
		{
			displacement = start.Value - time;
		}
		else if (end != null && time >= end.Value)
		{
			displacement = end.Value - time;
		}
		else
		{
			displacement = new TimeSpan(0L);
		}
		return true;
	}

	// Token: 0x06005F2D RID: 24365 RVA: 0x001EEFE8 File Offset: 0x001ED1E8
	public TimeSpan GetDuration()
	{
		if (this.m_isNever)
		{
			return new TimeSpan(0L);
		}
		if (this.StartDateTime != null && this.EndDateTime != null)
		{
			return this.EndDateTime.Value - this.StartDateTime.Value;
		}
		return new TimeSpan(long.MaxValue);
	}

	// Token: 0x06005F2E RID: 24366 RVA: 0x001EF058 File Offset: 0x001ED258
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
		return (startDateTime != null && b.IsBuyableAtTime(startDateTime.Value)) || (endDateTime != null && b.IsBuyableAtTime(endDateTime.Value)) || (startDateTime2 != null && a.IsBuyableAtTime(startDateTime2.Value)) || (endDateTime2 != null && a.IsBuyableAtTime(endDateTime2.Value));
	}

	// Token: 0x06005F2F RID: 24367 RVA: 0x001EF110 File Offset: 0x001ED310
	public static int CompareNullableStartDateTimes(DateTime? a, DateTime? b)
	{
		if (a == null || b == null)
		{
			if (a != null)
			{
				return 1;
			}
			if (b != null)
			{
				return -1;
			}
			return 0;
		}
		else
		{
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
	}

	// Token: 0x06005F30 RID: 24368 RVA: 0x001EF178 File Offset: 0x001ED378
	public static int CompareNullableEndDateTimes(DateTime? a, DateTime? b)
	{
		if (a == null || b == null)
		{
			if (a != null)
			{
				return -1;
			}
			if (b != null)
			{
				return 1;
			}
			return 0;
		}
		else
		{
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
	}

	// Token: 0x06005F31 RID: 24369 RVA: 0x001EF1E0 File Offset: 0x001ED3E0
	public void UnionWith(ProductAvailabilityRange other)
	{
		if (ProductAvailabilityRange.CompareNullableStartDateTimes(other.StartDateTime, this.StartDateTime) <= 0)
		{
			this.Start = other.Start;
		}
		if (ProductAvailabilityRange.CompareNullableEndDateTimes(other.EndDateTime, this.EndDateTime) >= 0)
		{
			this.End = other.End;
			this.SoftEnd = other.SoftEnd;
		}
	}

	// Token: 0x06005F32 RID: 24370 RVA: 0x001EF23C File Offset: 0x001ED43C
	public void IntersectWith(ProductAvailabilityRange other)
	{
		if (ProductAvailabilityRange.CompareNullableStartDateTimes(other.StartDateTime, this.StartDateTime) >= 0)
		{
			this.Start = other.Start;
		}
		if (ProductAvailabilityRange.CompareNullableEndDateTimes(other.EndDateTime, this.EndDateTime) <= 0)
		{
			this.End = other.End;
			this.SoftEnd = other.SoftEnd;
		}
	}

	// Token: 0x06005F33 RID: 24371 RVA: 0x001EF298 File Offset: 0x001ED498
	public override string ToString()
	{
		if (!(this.Start.SourceEventTimingName == this.End.SourceEventTimingName) || this.Start.SourceSaleId != this.End.SourceSaleId)
		{
			return string.Format("({0} - {1})[{2} - {3}])", new object[]
			{
				this.Start.GetDateTimeAsString(),
				this.End.GetDateTimeAsString(),
				this.Start.GetSourceAsString(),
				this.End.GetSourceAsString()
			});
		}
		if (this.IsAlways)
		{
			return string.Format("always[{0}]", this.Start.GetSourceAsString());
		}
		if (this.IsNever)
		{
			return string.Format("never[{0}]", this.Start.GetSourceAsString());
		}
		return string.Format("({0} - {1})[{2}]", this.Start.GetDateTimeAsString(), this.End.GetDateTimeAsString(), this.Start.GetSourceAsString());
	}

	// Token: 0x0400502B RID: 20523
	private bool m_isNever;

	// Token: 0x020021D6 RID: 8662
	public struct Moment
	{
		// Token: 0x170028ED RID: 10477
		// (get) Token: 0x060124F6 RID: 74998 RVA: 0x005046EE File Offset: 0x005028EE
		// (set) Token: 0x060124F7 RID: 74999 RVA: 0x005046F6 File Offset: 0x005028F6
		public DateTime? DateTimeUtc { get; set; }

		// Token: 0x170028EE RID: 10478
		// (get) Token: 0x060124F8 RID: 75000 RVA: 0x005046FF File Offset: 0x005028FF
		// (set) Token: 0x060124F9 RID: 75001 RVA: 0x00504707 File Offset: 0x00502907
		public string SourceEventTimingName { get; set; }

		// Token: 0x170028EF RID: 10479
		// (get) Token: 0x060124FA RID: 75002 RVA: 0x00504710 File Offset: 0x00502910
		// (set) Token: 0x060124FB RID: 75003 RVA: 0x00504718 File Offset: 0x00502918
		public long SourceSaleId { get; set; }

		// Token: 0x060124FC RID: 75004 RVA: 0x00504724 File Offset: 0x00502924
		public string GetDateTimeAsString()
		{
			if (this.DateTimeUtc == null)
			{
				return "none";
			}
			return this.DateTimeUtc.Value.ToLocalTime().ToString("g");
		}

		// Token: 0x060124FD RID: 75005 RVA: 0x0050476A File Offset: 0x0050296A
		public string GetSourceAsString()
		{
			if (!string.IsNullOrEmpty(this.SourceEventTimingName))
			{
				return this.SourceEventTimingName;
			}
			if (this.SourceSaleId != 0L)
			{
				return string.Format("Sale {0}", this.SourceSaleId);
			}
			return "";
		}
	}
}
