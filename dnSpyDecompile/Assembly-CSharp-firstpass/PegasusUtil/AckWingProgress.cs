using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000084 RID: 132
	public class AckWingProgress : IProtoBuf
	{
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x0001CE4A File Offset: 0x0001B04A
		// (set) Token: 0x0600081F RID: 2079 RVA: 0x0001CE52 File Offset: 0x0001B052
		public int Wing { get; set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x0001CE5B File Offset: 0x0001B05B
		// (set) Token: 0x06000821 RID: 2081 RVA: 0x0001CE63 File Offset: 0x0001B063
		public int Ack { get; set; }

		// Token: 0x06000822 RID: 2082 RVA: 0x0001CE6C File Offset: 0x0001B06C
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Wing.GetHashCode() ^ this.Ack.GetHashCode();
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0001CEA4 File Offset: 0x0001B0A4
		public override bool Equals(object obj)
		{
			AckWingProgress ackWingProgress = obj as AckWingProgress;
			return ackWingProgress != null && this.Wing.Equals(ackWingProgress.Wing) && this.Ack.Equals(ackWingProgress.Ack);
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0001CEEE File Offset: 0x0001B0EE
		public void Deserialize(Stream stream)
		{
			AckWingProgress.Deserialize(stream, this);
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0001CEF8 File Offset: 0x0001B0F8
		public static AckWingProgress Deserialize(Stream stream, AckWingProgress instance)
		{
			return AckWingProgress.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0001CF04 File Offset: 0x0001B104
		public static AckWingProgress DeserializeLengthDelimited(Stream stream)
		{
			AckWingProgress ackWingProgress = new AckWingProgress();
			AckWingProgress.DeserializeLengthDelimited(stream, ackWingProgress);
			return ackWingProgress;
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0001CF20 File Offset: 0x0001B120
		public static AckWingProgress DeserializeLengthDelimited(Stream stream, AckWingProgress instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AckWingProgress.Deserialize(stream, instance, num);
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0001CF48 File Offset: 0x0001B148
		public static AckWingProgress Deserialize(Stream stream, AckWingProgress instance, long limit)
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
				else if (num != 8)
				{
					if (num != 16)
					{
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else
					{
						instance.Ack = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Wing = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0001CFE1 File Offset: 0x0001B1E1
		public void Serialize(Stream stream)
		{
			AckWingProgress.Serialize(stream, this);
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0001CFEA File Offset: 0x0001B1EA
		public static void Serialize(Stream stream, AckWingProgress instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Wing));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Ack));
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0001D015 File Offset: 0x0001B215
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.Wing)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.Ack)) + 2U;
		}

		// Token: 0x02000597 RID: 1431
		public enum PacketID
		{
			// Token: 0x04001F20 RID: 7968
			ID = 308,
			// Token: 0x04001F21 RID: 7969
			System = 0
		}
	}
}
