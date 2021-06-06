using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x02000092 RID: 146
	public class ArenaSeasonInfo : IProtoBuf
	{
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x000243BD File Offset: 0x000225BD
		// (set) Token: 0x060009D1 RID: 2513 RVA: 0x000243C5 File Offset: 0x000225C5
		public ArenaSeasonSpec Season
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

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x000243D8 File Offset: 0x000225D8
		// (set) Token: 0x060009D3 RID: 2515 RVA: 0x000243E0 File Offset: 0x000225E0
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

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x000243F0 File Offset: 0x000225F0
		// (set) Token: 0x060009D5 RID: 2517 RVA: 0x000243F8 File Offset: 0x000225F8
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

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x0002440B File Offset: 0x0002260B
		// (set) Token: 0x060009D7 RID: 2519 RVA: 0x00024413 File Offset: 0x00022613
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

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x00024426 File Offset: 0x00022626
		// (set) Token: 0x060009D9 RID: 2521 RVA: 0x0002442E File Offset: 0x0002262E
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

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x0002443E File Offset: 0x0002263E
		// (set) Token: 0x060009DB RID: 2523 RVA: 0x00024446 File Offset: 0x00022646
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

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x00024456 File Offset: 0x00022656
		// (set) Token: 0x060009DD RID: 2525 RVA: 0x0002445E File Offset: 0x0002265E
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

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x0002446E File Offset: 0x0002266E
		// (set) Token: 0x060009DF RID: 2527 RVA: 0x00024476 File Offset: 0x00022676
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

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x00024489 File Offset: 0x00022689
		// (set) Token: 0x060009E1 RID: 2529 RVA: 0x00024491 File Offset: 0x00022691
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

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x000244A4 File Offset: 0x000226A4
		// (set) Token: 0x060009E3 RID: 2531 RVA: 0x000244AC File Offset: 0x000226AC
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

		// Token: 0x060009E4 RID: 2532 RVA: 0x000244B8 File Offset: 0x000226B8
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

		// Token: 0x060009E5 RID: 2533 RVA: 0x000245F0 File Offset: 0x000227F0
		public override bool Equals(object obj)
		{
			ArenaSeasonInfo arenaSeasonInfo = obj as ArenaSeasonInfo;
			if (arenaSeasonInfo == null)
			{
				return false;
			}
			if (this.HasSeason != arenaSeasonInfo.HasSeason || (this.HasSeason && !this.Season.Equals(arenaSeasonInfo.Season)))
			{
				return false;
			}
			if (this.HasSeasonEndingSoonDays != arenaSeasonInfo.HasSeasonEndingSoonDays || (this.HasSeasonEndingSoonDays && !this.SeasonEndingSoonDays.Equals(arenaSeasonInfo.SeasonEndingSoonDays)))
			{
				return false;
			}
			if (this.HasSeasonEndingSoonPrefab != arenaSeasonInfo.HasSeasonEndingSoonPrefab || (this.HasSeasonEndingSoonPrefab && !this.SeasonEndingSoonPrefab.Equals(arenaSeasonInfo.SeasonEndingSoonPrefab)))
			{
				return false;
			}
			if (this.HasSeasonEndingSoonPrefabExtra != arenaSeasonInfo.HasSeasonEndingSoonPrefabExtra || (this.HasSeasonEndingSoonPrefabExtra && !this.SeasonEndingSoonPrefabExtra.Equals(arenaSeasonInfo.SeasonEndingSoonPrefabExtra)))
			{
				return false;
			}
			if (this.HasNextStartSecondsFromNow != arenaSeasonInfo.HasNextStartSecondsFromNow || (this.HasNextStartSecondsFromNow && !this.NextStartSecondsFromNow.Equals(arenaSeasonInfo.NextStartSecondsFromNow)))
			{
				return false;
			}
			if (this.HasNextSeasonId != arenaSeasonInfo.HasNextSeasonId || (this.HasNextSeasonId && !this.NextSeasonId.Equals(arenaSeasonInfo.NextSeasonId)))
			{
				return false;
			}
			if (this.HasNextSeasonComingSoonDays != arenaSeasonInfo.HasNextSeasonComingSoonDays || (this.HasNextSeasonComingSoonDays && !this.NextSeasonComingSoonDays.Equals(arenaSeasonInfo.NextSeasonComingSoonDays)))
			{
				return false;
			}
			if (this.HasNextSeasonComingSoonPrefab != arenaSeasonInfo.HasNextSeasonComingSoonPrefab || (this.HasNextSeasonComingSoonPrefab && !this.NextSeasonComingSoonPrefab.Equals(arenaSeasonInfo.NextSeasonComingSoonPrefab)))
			{
				return false;
			}
			if (this.HasNextSeasonComingSoonPrefabExtra != arenaSeasonInfo.HasNextSeasonComingSoonPrefabExtra || (this.HasNextSeasonComingSoonPrefabExtra && !this.NextSeasonComingSoonPrefabExtra.Equals(arenaSeasonInfo.NextSeasonComingSoonPrefabExtra)))
			{
				return false;
			}
			if (this.NextSeasonStrings.Count != arenaSeasonInfo.NextSeasonStrings.Count)
			{
				return false;
			}
			for (int i = 0; i < this.NextSeasonStrings.Count; i++)
			{
				if (!this.NextSeasonStrings[i].Equals(arenaSeasonInfo.NextSeasonStrings[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x000247EA File Offset: 0x000229EA
		public void Deserialize(Stream stream)
		{
			ArenaSeasonInfo.Deserialize(stream, this);
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x000247F4 File Offset: 0x000229F4
		public static ArenaSeasonInfo Deserialize(Stream stream, ArenaSeasonInfo instance)
		{
			return ArenaSeasonInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x00024800 File Offset: 0x00022A00
		public static ArenaSeasonInfo DeserializeLengthDelimited(Stream stream)
		{
			ArenaSeasonInfo arenaSeasonInfo = new ArenaSeasonInfo();
			ArenaSeasonInfo.DeserializeLengthDelimited(stream, arenaSeasonInfo);
			return arenaSeasonInfo;
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0002481C File Offset: 0x00022A1C
		public static ArenaSeasonInfo DeserializeLengthDelimited(Stream stream, ArenaSeasonInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ArenaSeasonInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x00024844 File Offset: 0x00022A44
		public static ArenaSeasonInfo Deserialize(Stream stream, ArenaSeasonInfo instance, long limit)
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
									instance.Season = ArenaSeasonSpec.DeserializeLengthDelimited(stream);
									continue;
								}
								ArenaSeasonSpec.DeserializeLengthDelimited(stream, instance.Season);
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

		// Token: 0x060009EB RID: 2539 RVA: 0x00024A00 File Offset: 0x00022C00
		public void Serialize(Stream stream)
		{
			ArenaSeasonInfo.Serialize(stream, this);
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x00024A0C File Offset: 0x00022C0C
		public static void Serialize(Stream stream, ArenaSeasonInfo instance)
		{
			if (instance.HasSeason)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Season.GetSerializedSize());
				ArenaSeasonSpec.Serialize(stream, instance.Season);
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

		// Token: 0x060009ED RID: 2541 RVA: 0x00024BBC File Offset: 0x00022DBC
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

		// Token: 0x04000366 RID: 870
		public bool HasSeason;

		// Token: 0x04000367 RID: 871
		private ArenaSeasonSpec _Season;

		// Token: 0x04000368 RID: 872
		public bool HasSeasonEndingSoonDays;

		// Token: 0x04000369 RID: 873
		private int _SeasonEndingSoonDays;

		// Token: 0x0400036A RID: 874
		public bool HasSeasonEndingSoonPrefab;

		// Token: 0x0400036B RID: 875
		private string _SeasonEndingSoonPrefab;

		// Token: 0x0400036C RID: 876
		public bool HasSeasonEndingSoonPrefabExtra;

		// Token: 0x0400036D RID: 877
		private string _SeasonEndingSoonPrefabExtra;

		// Token: 0x0400036E RID: 878
		public bool HasNextStartSecondsFromNow;

		// Token: 0x0400036F RID: 879
		private ulong _NextStartSecondsFromNow;

		// Token: 0x04000370 RID: 880
		public bool HasNextSeasonId;

		// Token: 0x04000371 RID: 881
		private int _NextSeasonId;

		// Token: 0x04000372 RID: 882
		public bool HasNextSeasonComingSoonDays;

		// Token: 0x04000373 RID: 883
		private int _NextSeasonComingSoonDays;

		// Token: 0x04000374 RID: 884
		public bool HasNextSeasonComingSoonPrefab;

		// Token: 0x04000375 RID: 885
		private string _NextSeasonComingSoonPrefab;

		// Token: 0x04000376 RID: 886
		public bool HasNextSeasonComingSoonPrefabExtra;

		// Token: 0x04000377 RID: 887
		private string _NextSeasonComingSoonPrefabExtra;

		// Token: 0x04000378 RID: 888
		private List<LocalizedString> _NextSeasonStrings = new List<LocalizedString>();
	}
}
