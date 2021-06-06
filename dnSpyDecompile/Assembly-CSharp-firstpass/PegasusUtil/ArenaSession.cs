using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000091 RID: 145
	public class ArenaSession : IProtoBuf
	{
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x00023F65 File Offset: 0x00022165
		// (set) Token: 0x060009BC RID: 2492 RVA: 0x00023F6D File Offset: 0x0002216D
		public int Wins { get; set; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060009BD RID: 2493 RVA: 0x00023F76 File Offset: 0x00022176
		// (set) Token: 0x060009BE RID: 2494 RVA: 0x00023F7E File Offset: 0x0002217E
		public int Losses { get; set; }

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x00023F87 File Offset: 0x00022187
		// (set) Token: 0x060009C0 RID: 2496 RVA: 0x00023F8F File Offset: 0x0002218F
		public bool IsActive
		{
			get
			{
				return this._IsActive;
			}
			set
			{
				this._IsActive = value;
				this.HasIsActive = true;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x00023F9F File Offset: 0x0002219F
		// (set) Token: 0x060009C2 RID: 2498 RVA: 0x00023FA7 File Offset: 0x000221A7
		public bool IsInRewards
		{
			get
			{
				return this._IsInRewards;
			}
			set
			{
				this._IsInRewards = value;
				this.HasIsInRewards = true;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060009C3 RID: 2499 RVA: 0x00023FB7 File Offset: 0x000221B7
		// (set) Token: 0x060009C4 RID: 2500 RVA: 0x00023FBF File Offset: 0x000221BF
		public ArenaSeasonInfo Info
		{
			get
			{
				return this._Info;
			}
			set
			{
				this._Info = value;
				this.HasInfo = (value != null);
			}
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x00023FD4 File Offset: 0x000221D4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Wins.GetHashCode();
			num ^= this.Losses.GetHashCode();
			if (this.HasIsActive)
			{
				num ^= this.IsActive.GetHashCode();
			}
			if (this.HasIsInRewards)
			{
				num ^= this.IsInRewards.GetHashCode();
			}
			if (this.HasInfo)
			{
				num ^= this.Info.GetHashCode();
			}
			return num;
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x00024058 File Offset: 0x00022258
		public override bool Equals(object obj)
		{
			ArenaSession arenaSession = obj as ArenaSession;
			return arenaSession != null && this.Wins.Equals(arenaSession.Wins) && this.Losses.Equals(arenaSession.Losses) && this.HasIsActive == arenaSession.HasIsActive && (!this.HasIsActive || this.IsActive.Equals(arenaSession.IsActive)) && this.HasIsInRewards == arenaSession.HasIsInRewards && (!this.HasIsInRewards || this.IsInRewards.Equals(arenaSession.IsInRewards)) && this.HasInfo == arenaSession.HasInfo && (!this.HasInfo || this.Info.Equals(arenaSession.Info));
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x00024129 File Offset: 0x00022329
		public void Deserialize(Stream stream)
		{
			ArenaSession.Deserialize(stream, this);
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x00024133 File Offset: 0x00022333
		public static ArenaSession Deserialize(Stream stream, ArenaSession instance)
		{
			return ArenaSession.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00024140 File Offset: 0x00022340
		public static ArenaSession DeserializeLengthDelimited(Stream stream)
		{
			ArenaSession arenaSession = new ArenaSession();
			ArenaSession.DeserializeLengthDelimited(stream, arenaSession);
			return arenaSession;
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0002415C File Offset: 0x0002235C
		public static ArenaSession DeserializeLengthDelimited(Stream stream, ArenaSession instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ArenaSession.Deserialize(stream, instance, num);
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x00024184 File Offset: 0x00022384
		public static ArenaSession Deserialize(Stream stream, ArenaSession instance, long limit)
		{
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
					if (num <= 16)
					{
						if (num == 8)
						{
							instance.Wins = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.Losses = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.IsActive = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 32)
						{
							instance.IsInRewards = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					uint field = key.Field;
					if (field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					if (field != 100U)
					{
						ProtocolParser.SkipKey(stream, key);
					}
					else if (key.WireType == Wire.LengthDelimited)
					{
						if (instance.Info == null)
						{
							instance.Info = ArenaSeasonInfo.DeserializeLengthDelimited(stream);
						}
						else
						{
							ArenaSeasonInfo.DeserializeLengthDelimited(stream, instance.Info);
						}
					}
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00024294 File Offset: 0x00022494
		public void Serialize(Stream stream)
		{
			ArenaSession.Serialize(stream, this);
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x000242A0 File Offset: 0x000224A0
		public static void Serialize(Stream stream, ArenaSession instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Wins));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Losses));
			if (instance.HasIsActive)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.IsActive);
			}
			if (instance.HasIsInRewards)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.IsInRewards);
			}
			if (instance.HasInfo)
			{
				stream.WriteByte(162);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt32(stream, instance.Info.GetSerializedSize());
				ArenaSeasonInfo.Serialize(stream, instance.Info);
			}
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x00024348 File Offset: 0x00022548
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Wins));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Losses));
			if (this.HasIsActive)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasIsInRewards)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasInfo)
			{
				num += 2U;
				uint serializedSize = this.Info.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num + 2U;
		}

		// Token: 0x04000360 RID: 864
		public bool HasIsActive;

		// Token: 0x04000361 RID: 865
		private bool _IsActive;

		// Token: 0x04000362 RID: 866
		public bool HasIsInRewards;

		// Token: 0x04000363 RID: 867
		private bool _IsInRewards;

		// Token: 0x04000364 RID: 868
		public bool HasInfo;

		// Token: 0x04000365 RID: 869
		private ArenaSeasonInfo _Info;
	}
}
