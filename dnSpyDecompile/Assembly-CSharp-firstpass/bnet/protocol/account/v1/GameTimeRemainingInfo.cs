using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x02000532 RID: 1330
	public class GameTimeRemainingInfo : IProtoBuf
	{
		// Token: 0x17001220 RID: 4640
		// (get) Token: 0x06005FB5 RID: 24501 RVA: 0x00122026 File Offset: 0x00120226
		// (set) Token: 0x06005FB6 RID: 24502 RVA: 0x0012202E File Offset: 0x0012022E
		public uint MinutesRemaining
		{
			get
			{
				return this._MinutesRemaining;
			}
			set
			{
				this._MinutesRemaining = value;
				this.HasMinutesRemaining = true;
			}
		}

		// Token: 0x06005FB7 RID: 24503 RVA: 0x0012203E File Offset: 0x0012023E
		public void SetMinutesRemaining(uint val)
		{
			this.MinutesRemaining = val;
		}

		// Token: 0x17001221 RID: 4641
		// (get) Token: 0x06005FB8 RID: 24504 RVA: 0x00122047 File Offset: 0x00120247
		// (set) Token: 0x06005FB9 RID: 24505 RVA: 0x0012204F File Offset: 0x0012024F
		public uint ParentalDailyMinutesRemaining
		{
			get
			{
				return this._ParentalDailyMinutesRemaining;
			}
			set
			{
				this._ParentalDailyMinutesRemaining = value;
				this.HasParentalDailyMinutesRemaining = true;
			}
		}

		// Token: 0x06005FBA RID: 24506 RVA: 0x0012205F File Offset: 0x0012025F
		public void SetParentalDailyMinutesRemaining(uint val)
		{
			this.ParentalDailyMinutesRemaining = val;
		}

		// Token: 0x17001222 RID: 4642
		// (get) Token: 0x06005FBB RID: 24507 RVA: 0x00122068 File Offset: 0x00120268
		// (set) Token: 0x06005FBC RID: 24508 RVA: 0x00122070 File Offset: 0x00120270
		public uint ParentalWeeklyMinutesRemaining
		{
			get
			{
				return this._ParentalWeeklyMinutesRemaining;
			}
			set
			{
				this._ParentalWeeklyMinutesRemaining = value;
				this.HasParentalWeeklyMinutesRemaining = true;
			}
		}

		// Token: 0x06005FBD RID: 24509 RVA: 0x00122080 File Offset: 0x00120280
		public void SetParentalWeeklyMinutesRemaining(uint val)
		{
			this.ParentalWeeklyMinutesRemaining = val;
		}

		// Token: 0x17001223 RID: 4643
		// (get) Token: 0x06005FBE RID: 24510 RVA: 0x00122089 File Offset: 0x00120289
		// (set) Token: 0x06005FBF RID: 24511 RVA: 0x00122091 File Offset: 0x00120291
		public uint SecondsRemainingUntilKick
		{
			get
			{
				return this._SecondsRemainingUntilKick;
			}
			set
			{
				this._SecondsRemainingUntilKick = value;
				this.HasSecondsRemainingUntilKick = true;
			}
		}

		// Token: 0x06005FC0 RID: 24512 RVA: 0x001220A1 File Offset: 0x001202A1
		public void SetSecondsRemainingUntilKick(uint val)
		{
			this.SecondsRemainingUntilKick = val;
		}

		// Token: 0x06005FC1 RID: 24513 RVA: 0x001220AC File Offset: 0x001202AC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasMinutesRemaining)
			{
				num ^= this.MinutesRemaining.GetHashCode();
			}
			if (this.HasParentalDailyMinutesRemaining)
			{
				num ^= this.ParentalDailyMinutesRemaining.GetHashCode();
			}
			if (this.HasParentalWeeklyMinutesRemaining)
			{
				num ^= this.ParentalWeeklyMinutesRemaining.GetHashCode();
			}
			if (this.HasSecondsRemainingUntilKick)
			{
				num ^= this.SecondsRemainingUntilKick.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005FC2 RID: 24514 RVA: 0x0012212C File Offset: 0x0012032C
		public override bool Equals(object obj)
		{
			GameTimeRemainingInfo gameTimeRemainingInfo = obj as GameTimeRemainingInfo;
			return gameTimeRemainingInfo != null && this.HasMinutesRemaining == gameTimeRemainingInfo.HasMinutesRemaining && (!this.HasMinutesRemaining || this.MinutesRemaining.Equals(gameTimeRemainingInfo.MinutesRemaining)) && this.HasParentalDailyMinutesRemaining == gameTimeRemainingInfo.HasParentalDailyMinutesRemaining && (!this.HasParentalDailyMinutesRemaining || this.ParentalDailyMinutesRemaining.Equals(gameTimeRemainingInfo.ParentalDailyMinutesRemaining)) && this.HasParentalWeeklyMinutesRemaining == gameTimeRemainingInfo.HasParentalWeeklyMinutesRemaining && (!this.HasParentalWeeklyMinutesRemaining || this.ParentalWeeklyMinutesRemaining.Equals(gameTimeRemainingInfo.ParentalWeeklyMinutesRemaining)) && this.HasSecondsRemainingUntilKick == gameTimeRemainingInfo.HasSecondsRemainingUntilKick && (!this.HasSecondsRemainingUntilKick || this.SecondsRemainingUntilKick.Equals(gameTimeRemainingInfo.SecondsRemainingUntilKick));
		}

		// Token: 0x17001224 RID: 4644
		// (get) Token: 0x06005FC3 RID: 24515 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005FC4 RID: 24516 RVA: 0x001221FE File Offset: 0x001203FE
		public static GameTimeRemainingInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameTimeRemainingInfo>(bs, 0, -1);
		}

		// Token: 0x06005FC5 RID: 24517 RVA: 0x00122208 File Offset: 0x00120408
		public void Deserialize(Stream stream)
		{
			GameTimeRemainingInfo.Deserialize(stream, this);
		}

		// Token: 0x06005FC6 RID: 24518 RVA: 0x00122212 File Offset: 0x00120412
		public static GameTimeRemainingInfo Deserialize(Stream stream, GameTimeRemainingInfo instance)
		{
			return GameTimeRemainingInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005FC7 RID: 24519 RVA: 0x00122220 File Offset: 0x00120420
		public static GameTimeRemainingInfo DeserializeLengthDelimited(Stream stream)
		{
			GameTimeRemainingInfo gameTimeRemainingInfo = new GameTimeRemainingInfo();
			GameTimeRemainingInfo.DeserializeLengthDelimited(stream, gameTimeRemainingInfo);
			return gameTimeRemainingInfo;
		}

		// Token: 0x06005FC8 RID: 24520 RVA: 0x0012223C File Offset: 0x0012043C
		public static GameTimeRemainingInfo DeserializeLengthDelimited(Stream stream, GameTimeRemainingInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameTimeRemainingInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x06005FC9 RID: 24521 RVA: 0x00122264 File Offset: 0x00120464
		public static GameTimeRemainingInfo Deserialize(Stream stream, GameTimeRemainingInfo instance, long limit)
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
							instance.MinutesRemaining = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num == 16)
						{
							instance.ParentalDailyMinutesRemaining = ProtocolParser.ReadUInt32(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.ParentalWeeklyMinutesRemaining = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num == 32)
						{
							instance.SecondsRemainingUntilKick = ProtocolParser.ReadUInt32(stream);
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

		// Token: 0x06005FCA RID: 24522 RVA: 0x00122334 File Offset: 0x00120534
		public void Serialize(Stream stream)
		{
			GameTimeRemainingInfo.Serialize(stream, this);
		}

		// Token: 0x06005FCB RID: 24523 RVA: 0x00122340 File Offset: 0x00120540
		public static void Serialize(Stream stream, GameTimeRemainingInfo instance)
		{
			if (instance.HasMinutesRemaining)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.MinutesRemaining);
			}
			if (instance.HasParentalDailyMinutesRemaining)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.ParentalDailyMinutesRemaining);
			}
			if (instance.HasParentalWeeklyMinutesRemaining)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.ParentalWeeklyMinutesRemaining);
			}
			if (instance.HasSecondsRemainingUntilKick)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt32(stream, instance.SecondsRemainingUntilKick);
			}
		}

		// Token: 0x06005FCC RID: 24524 RVA: 0x001223BC File Offset: 0x001205BC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasMinutesRemaining)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.MinutesRemaining);
			}
			if (this.HasParentalDailyMinutesRemaining)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.ParentalDailyMinutesRemaining);
			}
			if (this.HasParentalWeeklyMinutesRemaining)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.ParentalWeeklyMinutesRemaining);
			}
			if (this.HasSecondsRemainingUntilKick)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.SecondsRemainingUntilKick);
			}
			return num;
		}

		// Token: 0x04001D81 RID: 7553
		public bool HasMinutesRemaining;

		// Token: 0x04001D82 RID: 7554
		private uint _MinutesRemaining;

		// Token: 0x04001D83 RID: 7555
		public bool HasParentalDailyMinutesRemaining;

		// Token: 0x04001D84 RID: 7556
		private uint _ParentalDailyMinutesRemaining;

		// Token: 0x04001D85 RID: 7557
		public bool HasParentalWeeklyMinutesRemaining;

		// Token: 0x04001D86 RID: 7558
		private uint _ParentalWeeklyMinutesRemaining;

		// Token: 0x04001D87 RID: 7559
		public bool HasSecondsRemainingUntilKick;

		// Token: 0x04001D88 RID: 7560
		private uint _SecondsRemainingUntilKick;
	}
}
