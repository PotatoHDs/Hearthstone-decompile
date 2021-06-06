using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x0200007F RID: 127
	public class PVPDRStatsInfoRequest : IProtoBuf
	{
		// Token: 0x060007E5 RID: 2021 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0001C91E File Offset: 0x0001AB1E
		public override bool Equals(object obj)
		{
			return obj is PVPDRStatsInfoRequest;
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x0001C92B File Offset: 0x0001AB2B
		public void Deserialize(Stream stream)
		{
			PVPDRStatsInfoRequest.Deserialize(stream, this);
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0001C935 File Offset: 0x0001AB35
		public static PVPDRStatsInfoRequest Deserialize(Stream stream, PVPDRStatsInfoRequest instance)
		{
			return PVPDRStatsInfoRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0001C940 File Offset: 0x0001AB40
		public static PVPDRStatsInfoRequest DeserializeLengthDelimited(Stream stream)
		{
			PVPDRStatsInfoRequest pvpdrstatsInfoRequest = new PVPDRStatsInfoRequest();
			PVPDRStatsInfoRequest.DeserializeLengthDelimited(stream, pvpdrstatsInfoRequest);
			return pvpdrstatsInfoRequest;
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0001C95C File Offset: 0x0001AB5C
		public static PVPDRStatsInfoRequest DeserializeLengthDelimited(Stream stream, PVPDRStatsInfoRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PVPDRStatsInfoRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x0001C984 File Offset: 0x0001AB84
		public static PVPDRStatsInfoRequest Deserialize(Stream stream, PVPDRStatsInfoRequest instance, long limit)
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

		// Token: 0x060007EC RID: 2028 RVA: 0x0001C9F1 File Offset: 0x0001ABF1
		public void Serialize(Stream stream)
		{
			PVPDRStatsInfoRequest.Serialize(stream, this);
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, PVPDRStatsInfoRequest instance)
		{
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x02000592 RID: 1426
		public enum PacketID
		{
			// Token: 0x04001F12 RID: 7954
			ID = 378,
			// Token: 0x04001F13 RID: 7955
			System = 0
		}
	}
}
