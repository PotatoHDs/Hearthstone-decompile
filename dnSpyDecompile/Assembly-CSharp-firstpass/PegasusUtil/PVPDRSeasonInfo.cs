using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x02000110 RID: 272
	public class PVPDRSeasonInfo : IProtoBuf
	{
		// Token: 0x17000365 RID: 869
		// (get) Token: 0x060011FA RID: 4602 RVA: 0x0003F4B4 File Offset: 0x0003D6B4
		// (set) Token: 0x060011FB RID: 4603 RVA: 0x0003F4BC File Offset: 0x0003D6BC
		public PVPDRSeasonSpec Season
		{
			get
			{
				return this._Season;
			}
			set
			{
				this._Season = value;
				this.HasSeason = (value != null);
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x060011FC RID: 4604 RVA: 0x0003F4CF File Offset: 0x0003D6CF
		// (set) Token: 0x060011FD RID: 4605 RVA: 0x0003F4D7 File Offset: 0x0003D6D7
		public int SeasonEndingSoonDays
		{
			get
			{
				return this._SeasonEndingSoonDays;
			}
			set
			{
				this._SeasonEndingSoonDays = value;
				this.HasSeasonEndingSoonDays = true;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x060011FE RID: 4606 RVA: 0x0003F4E7 File Offset: 0x0003D6E7
		// (set) Token: 0x060011FF RID: 4607 RVA: 0x0003F4EF File Offset: 0x0003D6EF
		public string SeasonEndingSoonPrefab
		{
			get
			{
				return this._SeasonEndingSoonPrefab;
			}
			set
			{
				this._SeasonEndingSoonPrefab = value;
				this.HasSeasonEndingSoonPrefab = (value != null);
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06001200 RID: 4608 RVA: 0x0003F502 File Offset: 0x0003D702
		// (set) Token: 0x06001201 RID: 4609 RVA: 0x0003F50A File Offset: 0x0003D70A
		public string SeasonEndingSoonPrefabExtra
		{
			get
			{
				return this._SeasonEndingSoonPrefabExtra;
			}
			set
			{
				this._SeasonEndingSoonPrefabExtra = value;
				this.HasSeasonEndingSoonPrefabExtra = (value != null);
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06001202 RID: 4610 RVA: 0x0003F51D File Offset: 0x0003D71D
		// (set) Token: 0x06001203 RID: 4611 RVA: 0x0003F525 File Offset: 0x0003D725
		public ulong NextStartSecondsFromNow
		{
			get
			{
				return this._NextStartSecondsFromNow;
			}
			set
			{
				this._NextStartSecondsFromNow = value;
				this.HasNextStartSecondsFromNow = true;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06001204 RID: 4612 RVA: 0x0003F535 File Offset: 0x0003D735
		// (set) Token: 0x06001205 RID: 4613 RVA: 0x0003F53D File Offset: 0x0003D73D
		public int NextSeasonId
		{
			get
			{
				return this._NextSeasonId;
			}
			set
			{
				this._NextSeasonId = value;
				this.HasNextSeasonId = true;
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06001206 RID: 4614 RVA: 0x0003F54D File Offset: 0x0003D74D
		// (set) Token: 0x06001207 RID: 4615 RVA: 0x0003F555 File Offset: 0x0003D755
		public int NextSeasonComingSoonDays
		{
			get
			{
				return this._NextSeasonComingSoonDays;
			}
			set
			{
				this._NextSeasonComingSoonDays = value;
				this.HasNextSeasonComingSoonDays = true;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06001208 RID: 4616 RVA: 0x0003F565 File Offset: 0x0003D765
		// (set) Token: 0x06001209 RID: 4617 RVA: 0x0003F56D File Offset: 0x0003D76D
		public string NextSeasonComingSoonPrefab
		{
			get
			{
				return this._NextSeasonComingSoonPrefab;
			}
			set
			{
				this._NextSeasonComingSoonPrefab = value;
				this.HasNextSeasonComingSoonPrefab = (value != null);
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x0600120A RID: 4618 RVA: 0x0003F580 File Offset: 0x0003D780
		// (set) Token: 0x0600120B RID: 4619 RVA: 0x0003F588 File Offset: 0x0003D788
		public string NextSeasonComingSoonPrefabExtra
		{
			get
			{
				return this._NextSeasonComingSoonPrefabExtra;
			}
			set
			{
				this._NextSeasonComingSoonPrefabExtra = value;
				this.HasNextSeasonComingSoonPrefabExtra = (value != null);
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x0600120C RID: 4620 RVA: 0x0003F59B File Offset: 0x0003D79B
		// (set) Token: 0x0600120D RID: 4621 RVA: 0x0003F5A3 File Offset: 0x0003D7A3
		public List<LocalizedString> NextSeasonStrings
		{
			get
			{
				return this._NextSeasonStrings;
			}
			set
			{
				this._NextSeasonStrings = value;
			}
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x0003F5AC File Offset: 0x0003D7AC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasSeason)
			{
				num ^= this.Season.GetHashCode();
			}
			if (this.HasSeasonEndingSoonDays)
			{
				num ^= this.SeasonEndingSoonDays.GetHashCode();
			}
			if (this.HasSeasonEndingSoonPrefab)
			{
				num ^= this.SeasonEndingSoonPrefab.GetHashCode();
			}
			if (this.HasSeasonEndingSoonPrefabExtra)
			{
				num ^= this.SeasonEndingSoonPrefabExtra.GetHashCode();
			}
			if (this.HasNextStartSecondsFromNow)
			{
				num ^= this.NextStartSecondsFromNow.GetHashCode();
			}
			if (this.HasNextSeasonId)
			{
				num ^= this.NextSeasonId.GetHashCode();
			}
			if (this.HasNextSeasonComingSoonDays)
			{
				num ^= this.NextSeasonComingSoonDays.GetHashCode();
			}
			if (this.HasNextSeasonComingSoonPrefab)
			{
				num ^= this.NextSeasonComingSoonPrefab.GetHashCode();
			}
			if (this.HasNextSeasonComingSoonPrefabExtra)
			{
				num ^= this.NextSeasonComingSoonPrefabExtra.GetHashCode();
			}
			foreach (LocalizedString localizedString in this.NextSeasonStrings)
			{
				num ^= localizedString.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x0003F6E4 File Offset: 0x0003D8E4
		public override bool Equals(object obj)
		{
			PVPDRSeasonInfo pvpdrseasonInfo = obj as PVPDRSeasonInfo;
			if (pvpdrseasonInfo == null)
			{
				return false;
			}
			if (this.HasSeason != pvpdrseasonInfo.HasSeason || (this.HasSeason && !this.Season.Equals(pvpdrseasonInfo.Season)))
			{
				return false;
			}
			if (this.HasSeasonEndingSoonDays != pvpdrseasonInfo.HasSeasonEndingSoonDays || (this.HasSeasonEndingSoonDays && !this.SeasonEndingSoonDays.Equals(pvpdrseasonInfo.SeasonEndingSoonDays)))
			{
				return false;
			}
			if (this.HasSeasonEndingSoonPrefab != pvpdrseasonInfo.HasSeasonEndingSoonPrefab || (this.HasSeasonEndingSoonPrefab && !this.SeasonEndingSoonPrefab.Equals(pvpdrseasonInfo.SeasonEndingSoonPrefab)))
			{
				return false;
			}
			if (this.HasSeasonEndingSoonPrefabExtra != pvpdrseasonInfo.HasSeasonEndingSoonPrefabExtra || (this.HasSeasonEndingSoonPrefabExtra && !this.SeasonEndingSoonPrefabExtra.Equals(pvpdrseasonInfo.SeasonEndingSoonPrefabExtra)))
			{
				return false;
			}
			if (this.HasNextStartSecondsFromNow != pvpdrseasonInfo.HasNextStartSecondsFromNow || (this.HasNextStartSecondsFromNow && !this.NextStartSecondsFromNow.Equals(pvpdrseasonInfo.NextStartSecondsFromNow)))
			{
				return false;
			}
			if (this.HasNextSeasonId != pvpdrseasonInfo.HasNextSeasonId || (this.HasNextSeasonId && !this.NextSeasonId.Equals(pvpdrseasonInfo.NextSeasonId)))
			{
				return false;
			}
			if (this.HasNextSeasonComingSoonDays != pvpdrseasonInfo.HasNextSeasonComingSoonDays || (this.HasNextSeasonComingSoonDays && !this.NextSeasonComingSoonDays.Equals(pvpdrseasonInfo.NextSeasonComingSoonDays)))
			{
				return false;
			}
			if (this.HasNextSeasonComingSoonPrefab != pvpdrseasonInfo.HasNextSeasonComingSoonPrefab || (this.HasNextSeasonComingSoonPrefab && !this.NextSeasonComingSoonPrefab.Equals(pvpdrseasonInfo.NextSeasonComingSoonPrefab)))
			{
				return false;
			}
			if (this.HasNextSeasonComingSoonPrefabExtra != pvpdrseasonInfo.HasNextSeasonComingSoonPrefabExtra || (this.HasNextSeasonComingSoonPrefabExtra && !this.NextSeasonComingSoonPrefabExtra.Equals(pvpdrseasonInfo.NextSeasonComingSoonPrefabExtra)))
			{
				return false;
			}
			if (this.NextSeasonStrings.Count != pvpdrseasonInfo.NextSeasonStrings.Count)
			{
				return false;
			}
			for (int i = 0; i < this.NextSeasonStrings.Count; i++)
			{
				if (!this.NextSeasonStrings[i].Equals(pvpdrseasonInfo.NextSeasonStrings[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x0003F8DE File Offset: 0x0003DADE
		public void Deserialize(Stream stream)
		{
			PVPDRSeasonInfo.Deserialize(stream, this);
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x0003F8E8 File Offset: 0x0003DAE8
		public static PVPDRSeasonInfo Deserialize(Stream stream, PVPDRSeasonInfo instance)
		{
			return PVPDRSeasonInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x0003F8F4 File Offset: 0x0003DAF4
		public static PVPDRSeasonInfo DeserializeLengthDelimited(Stream stream)
		{
			PVPDRSeasonInfo pvpdrseasonInfo = new PVPDRSeasonInfo();
			PVPDRSeasonInfo.DeserializeLengthDelimited(stream, pvpdrseasonInfo);
			return pvpdrseasonInfo;
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x0003F910 File Offset: 0x0003DB10
		public static PVPDRSeasonInfo DeserializeLengthDelimited(Stream stream, PVPDRSeasonInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PVPDRSeasonInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x0003F938 File Offset: 0x0003DB38
		public static PVPDRSeasonInfo Deserialize(Stream stream, PVPDRSeasonInfo instance, long limit)
		{
			if (instance.NextSeasonStrings == null)
			{
				instance.NextSeasonStrings = new List<LocalizedString>();
			}
			while (limit < 0L || stream.Position < limit)
			{
				int num = stream.ReadByte();
				if (num == -1)
				{
					if (limit >= 0L)
					{
						throw new EndOfStreamException();
					}
					return instance;
				}
				else
				{
					if (num <= 40)
					{
						if (num <= 16)
						{
							if (num != 10)
							{
								if (num == 16)
								{
									instance.NextStartSecondsFromNow = ProtocolParser.ReadUInt64(stream);
									continue;
								}
							}
							else
							{
								if (instance.Season == null)
								{
									instance.Season = PVPDRSeasonSpec.DeserializeLengthDelimited(stream);
									continue;
								}
								PVPDRSeasonSpec.DeserializeLengthDelimited(stream, instance.Season);
								continue;
							}
						}
						else
						{
							if (num == 24)
							{
								instance.SeasonEndingSoonDays = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 34)
							{
								instance.SeasonEndingSoonPrefab = ProtocolParser.ReadString(stream);
								continue;
							}
							if (num == 40)
							{
								instance.NextSeasonId = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
					}
					else if (num <= 58)
					{
						if (num == 48)
						{
							instance.NextSeasonComingSoonDays = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 58)
						{
							instance.NextSeasonComingSoonPrefab = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					else
					{
						if (num == 66)
						{
							instance.NextSeasonStrings.Add(LocalizedString.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 74)
						{
							instance.SeasonEndingSoonPrefabExtra = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 82)
						{
							instance.NextSeasonComingSoonPrefabExtra = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x0003FAF4 File Offset: 0x0003DCF4
		public void Serialize(Stream stream)
		{
			PVPDRSeasonInfo.Serialize(stream, this);
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x0003FB00 File Offset: 0x0003DD00
		public static void Serialize(Stream stream, PVPDRSeasonInfo instance)
		{
			if (instance.HasSeason)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Season.GetSerializedSize());
				PVPDRSeasonSpec.Serialize(stream, instance.Season);
			}
			if (instance.HasSeasonEndingSoonDays)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SeasonEndingSoonDays));
			}
			if (instance.HasSeasonEndingSoonPrefab)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SeasonEndingSoonPrefab));
			}
			if (instance.HasSeasonEndingSoonPrefabExtra)
			{
				stream.WriteByte(74);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SeasonEndingSoonPrefabExtra));
			}
			if (instance.HasNextStartSecondsFromNow)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.NextStartSecondsFromNow);
			}
			if (instance.HasNextSeasonId)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.NextSeasonId));
			}
			if (instance.HasNextSeasonComingSoonDays)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.NextSeasonComingSoonDays));
			}
			if (instance.HasNextSeasonComingSoonPrefab)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.NextSeasonComingSoonPrefab));
			}
			if (instance.HasNextSeasonComingSoonPrefabExtra)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.NextSeasonComingSoonPrefabExtra));
			}
			if (instance.NextSeasonStrings.Count > 0)
			{
				foreach (LocalizedString localizedString in instance.NextSeasonStrings)
				{
					stream.WriteByte(66);
					ProtocolParser.WriteUInt32(stream, localizedString.GetSerializedSize());
					LocalizedString.Serialize(stream, localizedString);
				}
			}
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x0003FCB0 File Offset: 0x0003DEB0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasSeason)
			{
				num += 1U;
				uint serializedSize = this.Season.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasSeasonEndingSoonDays)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.SeasonEndingSoonDays));
			}
			if (this.HasSeasonEndingSoonPrefab)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.SeasonEndingSoonPrefab);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasSeasonEndingSoonPrefabExtra)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.SeasonEndingSoonPrefabExtra);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasNextStartSecondsFromNow)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.NextStartSecondsFromNow);
			}
			if (this.HasNextSeasonId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.NextSeasonId));
			}
			if (this.HasNextSeasonComingSoonDays)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.NextSeasonComingSoonDays));
			}
			if (this.HasNextSeasonComingSoonPrefab)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.NextSeasonComingSoonPrefab);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (this.HasNextSeasonComingSoonPrefabExtra)
			{
				num += 1U;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(this.NextSeasonComingSoonPrefabExtra);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (this.NextSeasonStrings.Count > 0)
			{
				foreach (LocalizedString localizedString in this.NextSeasonStrings)
				{
					num += 1U;
					uint serializedSize2 = localizedString.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			return num;
		}

		// Token: 0x04000589 RID: 1417
		public bool HasSeason;

		// Token: 0x0400058A RID: 1418
		private PVPDRSeasonSpec _Season;

		// Token: 0x0400058B RID: 1419
		public bool HasSeasonEndingSoonDays;

		// Token: 0x0400058C RID: 1420
		private int _SeasonEndingSoonDays;

		// Token: 0x0400058D RID: 1421
		public bool HasSeasonEndingSoonPrefab;

		// Token: 0x0400058E RID: 1422
		private string _SeasonEndingSoonPrefab;

		// Token: 0x0400058F RID: 1423
		public bool HasSeasonEndingSoonPrefabExtra;

		// Token: 0x04000590 RID: 1424
		private string _SeasonEndingSoonPrefabExtra;

		// Token: 0x04000591 RID: 1425
		public bool HasNextStartSecondsFromNow;

		// Token: 0x04000592 RID: 1426
		private ulong _NextStartSecondsFromNow;

		// Token: 0x04000593 RID: 1427
		public bool HasNextSeasonId;

		// Token: 0x04000594 RID: 1428
		private int _NextSeasonId;

		// Token: 0x04000595 RID: 1429
		public bool HasNextSeasonComingSoonDays;

		// Token: 0x04000596 RID: 1430
		private int _NextSeasonComingSoonDays;

		// Token: 0x04000597 RID: 1431
		public bool HasNextSeasonComingSoonPrefab;

		// Token: 0x04000598 RID: 1432
		private string _NextSeasonComingSoonPrefab;

		// Token: 0x04000599 RID: 1433
		public bool HasNextSeasonComingSoonPrefabExtra;

		// Token: 0x0400059A RID: 1434
		private string _NextSeasonComingSoonPrefabExtra;

		// Token: 0x0400059B RID: 1435
		private List<LocalizedString> _NextSeasonStrings = new List<LocalizedString>();
	}
}
