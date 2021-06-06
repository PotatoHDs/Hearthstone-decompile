using System;
using System.IO;

namespace PegasusGame
{
	// Token: 0x020001B6 RID: 438
	public class RopeTimerDebugInformation : IProtoBuf
	{
		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06001BCB RID: 7115 RVA: 0x0006235F File Offset: 0x0006055F
		// (set) Token: 0x06001BCC RID: 7116 RVA: 0x00062367 File Offset: 0x00060567
		public int MicrosecondsRemainingInTurn { get; set; }

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06001BCD RID: 7117 RVA: 0x00062370 File Offset: 0x00060570
		// (set) Token: 0x06001BCE RID: 7118 RVA: 0x00062378 File Offset: 0x00060578
		public int BaseMicrosecondsInTurn { get; set; }

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06001BCF RID: 7119 RVA: 0x00062381 File Offset: 0x00060581
		// (set) Token: 0x06001BD0 RID: 7120 RVA: 0x00062389 File Offset: 0x00060589
		public int SlushTimeInMicroseconds { get; set; }

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001BD1 RID: 7121 RVA: 0x00062392 File Offset: 0x00060592
		// (set) Token: 0x06001BD2 RID: 7122 RVA: 0x0006239A File Offset: 0x0006059A
		public int TotalMicrosecondsInTurn { get; set; }

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001BD3 RID: 7123 RVA: 0x000623A3 File Offset: 0x000605A3
		// (set) Token: 0x06001BD4 RID: 7124 RVA: 0x000623AB File Offset: 0x000605AB
		public int OpponentSlushTimeInMicroseconds { get; set; }

		// Token: 0x06001BD5 RID: 7125 RVA: 0x000623B4 File Offset: 0x000605B4
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.MicrosecondsRemainingInTurn.GetHashCode() ^ this.BaseMicrosecondsInTurn.GetHashCode() ^ this.SlushTimeInMicroseconds.GetHashCode() ^ this.TotalMicrosecondsInTurn.GetHashCode() ^ this.OpponentSlushTimeInMicroseconds.GetHashCode();
		}

		// Token: 0x06001BD6 RID: 7126 RVA: 0x00062418 File Offset: 0x00060618
		public override bool Equals(object obj)
		{
			RopeTimerDebugInformation ropeTimerDebugInformation = obj as RopeTimerDebugInformation;
			return ropeTimerDebugInformation != null && this.MicrosecondsRemainingInTurn.Equals(ropeTimerDebugInformation.MicrosecondsRemainingInTurn) && this.BaseMicrosecondsInTurn.Equals(ropeTimerDebugInformation.BaseMicrosecondsInTurn) && this.SlushTimeInMicroseconds.Equals(ropeTimerDebugInformation.SlushTimeInMicroseconds) && this.TotalMicrosecondsInTurn.Equals(ropeTimerDebugInformation.TotalMicrosecondsInTurn) && this.OpponentSlushTimeInMicroseconds.Equals(ropeTimerDebugInformation.OpponentSlushTimeInMicroseconds);
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x000624AA File Offset: 0x000606AA
		public void Deserialize(Stream stream)
		{
			RopeTimerDebugInformation.Deserialize(stream, this);
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x000624B4 File Offset: 0x000606B4
		public static RopeTimerDebugInformation Deserialize(Stream stream, RopeTimerDebugInformation instance)
		{
			return RopeTimerDebugInformation.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001BD9 RID: 7129 RVA: 0x000624C0 File Offset: 0x000606C0
		public static RopeTimerDebugInformation DeserializeLengthDelimited(Stream stream)
		{
			RopeTimerDebugInformation ropeTimerDebugInformation = new RopeTimerDebugInformation();
			RopeTimerDebugInformation.DeserializeLengthDelimited(stream, ropeTimerDebugInformation);
			return ropeTimerDebugInformation;
		}

		// Token: 0x06001BDA RID: 7130 RVA: 0x000624DC File Offset: 0x000606DC
		public static RopeTimerDebugInformation DeserializeLengthDelimited(Stream stream, RopeTimerDebugInformation instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RopeTimerDebugInformation.Deserialize(stream, instance, num);
		}

		// Token: 0x06001BDB RID: 7131 RVA: 0x00062504 File Offset: 0x00060704
		public static RopeTimerDebugInformation Deserialize(Stream stream, RopeTimerDebugInformation instance, long limit)
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
							instance.MicrosecondsRemainingInTurn = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.BaseMicrosecondsInTurn = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.SlushTimeInMicroseconds = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 32)
						{
							instance.TotalMicrosecondsInTurn = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 40)
						{
							instance.OpponentSlushTimeInMicroseconds = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06001BDC RID: 7132 RVA: 0x000625EF File Offset: 0x000607EF
		public void Serialize(Stream stream)
		{
			RopeTimerDebugInformation.Serialize(stream, this);
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x000625F8 File Offset: 0x000607F8
		public static void Serialize(Stream stream, RopeTimerDebugInformation instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.MicrosecondsRemainingInTurn));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.BaseMicrosecondsInTurn));
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SlushTimeInMicroseconds));
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.TotalMicrosecondsInTurn));
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.OpponentSlushTimeInMicroseconds));
		}

		// Token: 0x06001BDE RID: 7134 RVA: 0x00062670 File Offset: 0x00060870
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.MicrosecondsRemainingInTurn)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.BaseMicrosecondsInTurn)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.SlushTimeInMicroseconds)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.TotalMicrosecondsInTurn)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.OpponentSlushTimeInMicroseconds)) + 5U;
		}

		// Token: 0x0200064D RID: 1613
		public enum PacketID
		{
			// Token: 0x0400210E RID: 8462
			ID = 8
		}
	}
}
