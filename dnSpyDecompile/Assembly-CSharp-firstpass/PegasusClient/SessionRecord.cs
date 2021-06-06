using System;
using System.IO;

namespace PegasusClient
{
	// Token: 0x02000027 RID: 39
	public class SessionRecord : IProtoBuf
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00008931 File Offset: 0x00006B31
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x00008939 File Offset: 0x00006B39
		public uint Wins { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00008942 File Offset: 0x00006B42
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x0000894A File Offset: 0x00006B4A
		public uint Losses { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00008953 File Offset: 0x00006B53
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x0000895B File Offset: 0x00006B5B
		public bool RunFinished { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00008964 File Offset: 0x00006B64
		// (set) Token: 0x060001EB RID: 491 RVA: 0x0000896C File Offset: 0x00006B6C
		public SessionRecordType SessionRecordType { get; set; }

		// Token: 0x060001EC RID: 492 RVA: 0x00008978 File Offset: 0x00006B78
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Wins.GetHashCode() ^ this.Losses.GetHashCode() ^ this.RunFinished.GetHashCode() ^ this.SessionRecordType.GetHashCode();
		}

		// Token: 0x060001ED RID: 493 RVA: 0x000089D4 File Offset: 0x00006BD4
		public override bool Equals(object obj)
		{
			SessionRecord sessionRecord = obj as SessionRecord;
			return sessionRecord != null && this.Wins.Equals(sessionRecord.Wins) && this.Losses.Equals(sessionRecord.Losses) && this.RunFinished.Equals(sessionRecord.RunFinished) && this.SessionRecordType.Equals(sessionRecord.SessionRecordType);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00008A59 File Offset: 0x00006C59
		public void Deserialize(Stream stream)
		{
			SessionRecord.Deserialize(stream, this);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00008A63 File Offset: 0x00006C63
		public static SessionRecord Deserialize(Stream stream, SessionRecord instance)
		{
			return SessionRecord.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00008A70 File Offset: 0x00006C70
		public static SessionRecord DeserializeLengthDelimited(Stream stream)
		{
			SessionRecord sessionRecord = new SessionRecord();
			SessionRecord.DeserializeLengthDelimited(stream, sessionRecord);
			return sessionRecord;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00008A8C File Offset: 0x00006C8C
		public static SessionRecord DeserializeLengthDelimited(Stream stream, SessionRecord instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SessionRecord.Deserialize(stream, instance, num);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00008AB4 File Offset: 0x00006CB4
		public static SessionRecord Deserialize(Stream stream, SessionRecord instance, long limit)
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
							instance.Wins = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num == 16)
						{
							instance.Losses = ProtocolParser.ReadUInt32(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.RunFinished = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 32)
						{
							instance.SessionRecordType = (SessionRecordType)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x060001F3 RID: 499 RVA: 0x00008B85 File Offset: 0x00006D85
		public void Serialize(Stream stream)
		{
			SessionRecord.Serialize(stream, this);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00008B90 File Offset: 0x00006D90
		public static void Serialize(Stream stream, SessionRecord instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.Wins);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt32(stream, instance.Losses);
			stream.WriteByte(24);
			ProtocolParser.WriteBool(stream, instance.RunFinished);
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SessionRecordType));
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00008BED File Offset: 0x00006DED
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt32(this.Wins) + ProtocolParser.SizeOfUInt32(this.Losses) + 1U + ProtocolParser.SizeOfUInt64((ulong)((long)this.SessionRecordType)) + 4U;
		}
	}
}
