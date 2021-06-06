using System;
using System.IO;
using System.Text;

namespace PegasusShared
{
	// Token: 0x02000169 RID: 361
	public class LogRelayMessage : IProtoBuf
	{
		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060018D7 RID: 6359 RVA: 0x000576D8 File Offset: 0x000558D8
		// (set) Token: 0x060018D8 RID: 6360 RVA: 0x000576E0 File Offset: 0x000558E0
		public string Log
		{
			get
			{
				return this._Log;
			}
			set
			{
				this._Log = value;
				this.HasLog = (value != null);
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060018D9 RID: 6361 RVA: 0x000576F3 File Offset: 0x000558F3
		// (set) Token: 0x060018DA RID: 6362 RVA: 0x000576FB File Offset: 0x000558FB
		public string Event
		{
			get
			{
				return this._Event;
			}
			set
			{
				this._Event = value;
				this.HasEvent = (value != null);
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x060018DB RID: 6363 RVA: 0x0005770E File Offset: 0x0005590E
		// (set) Token: 0x060018DC RID: 6364 RVA: 0x00057716 File Offset: 0x00055916
		public string Message
		{
			get
			{
				return this._Message;
			}
			set
			{
				this._Message = value;
				this.HasMessage = (value != null);
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x060018DD RID: 6365 RVA: 0x00057729 File Offset: 0x00055929
		// (set) Token: 0x060018DE RID: 6366 RVA: 0x00057731 File Offset: 0x00055931
		public long Timestamp
		{
			get
			{
				return this._Timestamp;
			}
			set
			{
				this._Timestamp = value;
				this.HasTimestamp = true;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x060018DF RID: 6367 RVA: 0x00057741 File Offset: 0x00055941
		// (set) Token: 0x060018E0 RID: 6368 RVA: 0x00057749 File Offset: 0x00055949
		public int Severity
		{
			get
			{
				return this._Severity;
			}
			set
			{
				this._Severity = value;
				this.HasSeverity = true;
			}
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x0005775C File Offset: 0x0005595C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasLog)
			{
				num ^= this.Log.GetHashCode();
			}
			if (this.HasEvent)
			{
				num ^= this.Event.GetHashCode();
			}
			if (this.HasMessage)
			{
				num ^= this.Message.GetHashCode();
			}
			if (this.HasTimestamp)
			{
				num ^= this.Timestamp.GetHashCode();
			}
			if (this.HasSeverity)
			{
				num ^= this.Severity.GetHashCode();
			}
			return num;
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x000577EC File Offset: 0x000559EC
		public override bool Equals(object obj)
		{
			LogRelayMessage logRelayMessage = obj as LogRelayMessage;
			return logRelayMessage != null && this.HasLog == logRelayMessage.HasLog && (!this.HasLog || this.Log.Equals(logRelayMessage.Log)) && this.HasEvent == logRelayMessage.HasEvent && (!this.HasEvent || this.Event.Equals(logRelayMessage.Event)) && this.HasMessage == logRelayMessage.HasMessage && (!this.HasMessage || this.Message.Equals(logRelayMessage.Message)) && this.HasTimestamp == logRelayMessage.HasTimestamp && (!this.HasTimestamp || this.Timestamp.Equals(logRelayMessage.Timestamp)) && this.HasSeverity == logRelayMessage.HasSeverity && (!this.HasSeverity || this.Severity.Equals(logRelayMessage.Severity));
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x000578E3 File Offset: 0x00055AE3
		public void Deserialize(Stream stream)
		{
			LogRelayMessage.Deserialize(stream, this);
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x000578ED File Offset: 0x00055AED
		public static LogRelayMessage Deserialize(Stream stream, LogRelayMessage instance)
		{
			return LogRelayMessage.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x000578F8 File Offset: 0x00055AF8
		public static LogRelayMessage DeserializeLengthDelimited(Stream stream)
		{
			LogRelayMessage logRelayMessage = new LogRelayMessage();
			LogRelayMessage.DeserializeLengthDelimited(stream, logRelayMessage);
			return logRelayMessage;
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x00057914 File Offset: 0x00055B14
		public static LogRelayMessage DeserializeLengthDelimited(Stream stream, LogRelayMessage instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return LogRelayMessage.Deserialize(stream, instance, num);
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x0005793C File Offset: 0x00055B3C
		public static LogRelayMessage Deserialize(Stream stream, LogRelayMessage instance, long limit)
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
					if (num <= 18)
					{
						if (num == 10)
						{
							instance.Log = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 18)
						{
							instance.Event = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					else
					{
						if (num == 26)
						{
							instance.Message = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 32)
						{
							instance.Timestamp = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 40)
						{
							instance.Severity = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x060018E8 RID: 6376 RVA: 0x00057A24 File Offset: 0x00055C24
		public void Serialize(Stream stream)
		{
			LogRelayMessage.Serialize(stream, this);
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x00057A30 File Offset: 0x00055C30
		public static void Serialize(Stream stream, LogRelayMessage instance)
		{
			if (instance.HasLog)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Log));
			}
			if (instance.HasEvent)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Event));
			}
			if (instance.HasMessage)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Message));
			}
			if (instance.HasTimestamp)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Timestamp);
			}
			if (instance.HasSeverity)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Severity));
			}
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x00057AE8 File Offset: 0x00055CE8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasLog)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Log);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasEvent)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.Event);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasMessage)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.Message);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (this.HasTimestamp)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.Timestamp);
			}
			if (this.HasSeverity)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Severity));
			}
			return num;
		}

		// Token: 0x040007FE RID: 2046
		public bool HasLog;

		// Token: 0x040007FF RID: 2047
		private string _Log;

		// Token: 0x04000800 RID: 2048
		public bool HasEvent;

		// Token: 0x04000801 RID: 2049
		private string _Event;

		// Token: 0x04000802 RID: 2050
		public bool HasMessage;

		// Token: 0x04000803 RID: 2051
		private string _Message;

		// Token: 0x04000804 RID: 2052
		public bool HasTimestamp;

		// Token: 0x04000805 RID: 2053
		private long _Timestamp;

		// Token: 0x04000806 RID: 2054
		public bool HasSeverity;

		// Token: 0x04000807 RID: 2055
		private int _Severity;
	}
}
