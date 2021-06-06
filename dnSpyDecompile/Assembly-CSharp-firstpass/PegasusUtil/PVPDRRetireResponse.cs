using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x02000113 RID: 275
	public class PVPDRRetireResponse : IProtoBuf
	{
		// Token: 0x17000375 RID: 885
		// (get) Token: 0x0600123B RID: 4667 RVA: 0x00040511 File Offset: 0x0003E711
		// (set) Token: 0x0600123C RID: 4668 RVA: 0x00040519 File Offset: 0x0003E719
		public ErrorCode ErrorCode { get; set; }

		// Token: 0x0600123D RID: 4669 RVA: 0x00040524 File Offset: 0x0003E724
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.ErrorCode.GetHashCode();
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x00040554 File Offset: 0x0003E754
		public override bool Equals(object obj)
		{
			PVPDRRetireResponse pvpdrretireResponse = obj as PVPDRRetireResponse;
			return pvpdrretireResponse != null && this.ErrorCode.Equals(pvpdrretireResponse.ErrorCode);
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x00040591 File Offset: 0x0003E791
		public void Deserialize(Stream stream)
		{
			PVPDRRetireResponse.Deserialize(stream, this);
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x0004059B File Offset: 0x0003E79B
		public static PVPDRRetireResponse Deserialize(Stream stream, PVPDRRetireResponse instance)
		{
			return PVPDRRetireResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x000405A8 File Offset: 0x0003E7A8
		public static PVPDRRetireResponse DeserializeLengthDelimited(Stream stream)
		{
			PVPDRRetireResponse pvpdrretireResponse = new PVPDRRetireResponse();
			PVPDRRetireResponse.DeserializeLengthDelimited(stream, pvpdrretireResponse);
			return pvpdrretireResponse;
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x000405C4 File Offset: 0x0003E7C4
		public static PVPDRRetireResponse DeserializeLengthDelimited(Stream stream, PVPDRRetireResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PVPDRRetireResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x000405EC File Offset: 0x0003E7EC
		public static PVPDRRetireResponse Deserialize(Stream stream, PVPDRRetireResponse instance, long limit)
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
				else if (num == 8)
				{
					instance.ErrorCode = (ErrorCode)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06001244 RID: 4676 RVA: 0x0004066C File Offset: 0x0003E86C
		public void Serialize(Stream stream)
		{
			PVPDRRetireResponse.Serialize(stream, this);
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x00040675 File Offset: 0x0003E875
		public static void Serialize(Stream stream, PVPDRRetireResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ErrorCode));
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x0004068B File Offset: 0x0003E88B
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.ErrorCode)) + 1U;
		}

		// Token: 0x02000613 RID: 1555
		public enum PacketID
		{
			// Token: 0x04002074 RID: 8308
			ID = 381
		}
	}
}
