using System;
using System.IO;
using System.Text;

namespace PegasusUtil
{
	// Token: 0x02000036 RID: 54
	public class SpecialEventTiming : IProtoBuf
	{
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000BB90 File Offset: 0x00009D90
		// (set) Token: 0x060002CD RID: 717 RVA: 0x0000BB98 File Offset: 0x00009D98
		public string EventName { get; set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000BBA1 File Offset: 0x00009DA1
		// (set) Token: 0x060002CF RID: 719 RVA: 0x0000BBA9 File Offset: 0x00009DA9
		public ulong DeprecatedSecondsTilStart
		{
			get
			{
				return this._DeprecatedSecondsTilStart;
			}
			set
			{
				this._DeprecatedSecondsTilStart = value;
				this.HasDeprecatedSecondsTilStart = true;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000BBB9 File Offset: 0x00009DB9
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x0000BBC1 File Offset: 0x00009DC1
		public ulong DeprecatedSecondsTilEnd
		{
			get
			{
				return this._DeprecatedSecondsTilEnd;
			}
			set
			{
				this._DeprecatedSecondsTilEnd = value;
				this.HasDeprecatedSecondsTilEnd = true;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x0000BBD1 File Offset: 0x00009DD1
		// (set) Token: 0x060002D3 RID: 723 RVA: 0x0000BBD9 File Offset: 0x00009DD9
		public long SecondsToStart
		{
			get
			{
				return this._SecondsToStart;
			}
			set
			{
				this._SecondsToStart = value;
				this.HasSecondsToStart = true;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000BBE9 File Offset: 0x00009DE9
		// (set) Token: 0x060002D5 RID: 725 RVA: 0x0000BBF1 File Offset: 0x00009DF1
		public long SecondsToEnd
		{
			get
			{
				return this._SecondsToEnd;
			}
			set
			{
				this._SecondsToEnd = value;
				this.HasSecondsToEnd = true;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000BC01 File Offset: 0x00009E01
		// (set) Token: 0x060002D7 RID: 727 RVA: 0x0000BC09 File Offset: 0x00009E09
		public long EventId
		{
			get
			{
				return this._EventId;
			}
			set
			{
				this._EventId = value;
				this.HasEventId = true;
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000BC1C File Offset: 0x00009E1C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.EventName.GetHashCode();
			if (this.HasDeprecatedSecondsTilStart)
			{
				num ^= this.DeprecatedSecondsTilStart.GetHashCode();
			}
			if (this.HasDeprecatedSecondsTilEnd)
			{
				num ^= this.DeprecatedSecondsTilEnd.GetHashCode();
			}
			if (this.HasSecondsToStart)
			{
				num ^= this.SecondsToStart.GetHashCode();
			}
			if (this.HasSecondsToEnd)
			{
				num ^= this.SecondsToEnd.GetHashCode();
			}
			if (this.HasEventId)
			{
				num ^= this.EventId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000BCC4 File Offset: 0x00009EC4
		public override bool Equals(object obj)
		{
			SpecialEventTiming specialEventTiming = obj as SpecialEventTiming;
			return specialEventTiming != null && this.EventName.Equals(specialEventTiming.EventName) && this.HasDeprecatedSecondsTilStart == specialEventTiming.HasDeprecatedSecondsTilStart && (!this.HasDeprecatedSecondsTilStart || this.DeprecatedSecondsTilStart.Equals(specialEventTiming.DeprecatedSecondsTilStart)) && this.HasDeprecatedSecondsTilEnd == specialEventTiming.HasDeprecatedSecondsTilEnd && (!this.HasDeprecatedSecondsTilEnd || this.DeprecatedSecondsTilEnd.Equals(specialEventTiming.DeprecatedSecondsTilEnd)) && this.HasSecondsToStart == specialEventTiming.HasSecondsToStart && (!this.HasSecondsToStart || this.SecondsToStart.Equals(specialEventTiming.SecondsToStart)) && this.HasSecondsToEnd == specialEventTiming.HasSecondsToEnd && (!this.HasSecondsToEnd || this.SecondsToEnd.Equals(specialEventTiming.SecondsToEnd)) && this.HasEventId == specialEventTiming.HasEventId && (!this.HasEventId || this.EventId.Equals(specialEventTiming.EventId));
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000BDD9 File Offset: 0x00009FD9
		public void Deserialize(Stream stream)
		{
			SpecialEventTiming.Deserialize(stream, this);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000BDE3 File Offset: 0x00009FE3
		public static SpecialEventTiming Deserialize(Stream stream, SpecialEventTiming instance)
		{
			return SpecialEventTiming.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000BDF0 File Offset: 0x00009FF0
		public static SpecialEventTiming DeserializeLengthDelimited(Stream stream)
		{
			SpecialEventTiming specialEventTiming = new SpecialEventTiming();
			SpecialEventTiming.DeserializeLengthDelimited(stream, specialEventTiming);
			return specialEventTiming;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000BE0C File Offset: 0x0000A00C
		public static SpecialEventTiming DeserializeLengthDelimited(Stream stream, SpecialEventTiming instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SpecialEventTiming.Deserialize(stream, instance, num);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000BE34 File Offset: 0x0000A034
		public static SpecialEventTiming Deserialize(Stream stream, SpecialEventTiming instance, long limit)
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
					if (num <= 24)
					{
						if (num == 10)
						{
							instance.EventName = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 16)
						{
							instance.DeprecatedSecondsTilStart = ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 24)
						{
							instance.DeprecatedSecondsTilEnd = ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 32)
						{
							instance.SecondsToStart = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 40)
						{
							instance.SecondsToEnd = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 48)
						{
							instance.EventId = (long)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x060002DF RID: 735 RVA: 0x0000BF34 File Offset: 0x0000A134
		public void Serialize(Stream stream)
		{
			SpecialEventTiming.Serialize(stream, this);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000BF40 File Offset: 0x0000A140
		public static void Serialize(Stream stream, SpecialEventTiming instance)
		{
			if (instance.EventName == null)
			{
				throw new ArgumentNullException("EventName", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.EventName));
			if (instance.HasDeprecatedSecondsTilStart)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.DeprecatedSecondsTilStart);
			}
			if (instance.HasDeprecatedSecondsTilEnd)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, instance.DeprecatedSecondsTilEnd);
			}
			if (instance.HasSecondsToStart)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.SecondsToStart);
			}
			if (instance.HasSecondsToEnd)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.SecondsToEnd);
			}
			if (instance.HasEventId)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.EventId);
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000C010 File Offset: 0x0000A210
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.EventName);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (this.HasDeprecatedSecondsTilStart)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.DeprecatedSecondsTilStart);
			}
			if (this.HasDeprecatedSecondsTilEnd)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.DeprecatedSecondsTilEnd);
			}
			if (this.HasSecondsToStart)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.SecondsToStart);
			}
			if (this.HasSecondsToEnd)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.SecondsToEnd);
			}
			if (this.HasEventId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.EventId);
			}
			return num + 1U;
		}

		// Token: 0x040000DF RID: 223
		public bool HasDeprecatedSecondsTilStart;

		// Token: 0x040000E0 RID: 224
		private ulong _DeprecatedSecondsTilStart;

		// Token: 0x040000E1 RID: 225
		public bool HasDeprecatedSecondsTilEnd;

		// Token: 0x040000E2 RID: 226
		private ulong _DeprecatedSecondsTilEnd;

		// Token: 0x040000E3 RID: 227
		public bool HasSecondsToStart;

		// Token: 0x040000E4 RID: 228
		private long _SecondsToStart;

		// Token: 0x040000E5 RID: 229
		public bool HasSecondsToEnd;

		// Token: 0x040000E6 RID: 230
		private long _SecondsToEnd;

		// Token: 0x040000E7 RID: 231
		public bool HasEventId;

		// Token: 0x040000E8 RID: 232
		private long _EventId;
	}
}
