using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000080 RID: 128
	public class PVPDRRetireRequest : IProtoBuf
	{
		// Token: 0x060007F0 RID: 2032 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x0001C9FA File Offset: 0x0001ABFA
		public override bool Equals(object obj)
		{
			return obj is PVPDRRetireRequest;
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0001CA07 File Offset: 0x0001AC07
		public void Deserialize(Stream stream)
		{
			PVPDRRetireRequest.Deserialize(stream, this);
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0001CA11 File Offset: 0x0001AC11
		public static PVPDRRetireRequest Deserialize(Stream stream, PVPDRRetireRequest instance)
		{
			return PVPDRRetireRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0001CA1C File Offset: 0x0001AC1C
		public static PVPDRRetireRequest DeserializeLengthDelimited(Stream stream)
		{
			PVPDRRetireRequest pvpdrretireRequest = new PVPDRRetireRequest();
			PVPDRRetireRequest.DeserializeLengthDelimited(stream, pvpdrretireRequest);
			return pvpdrretireRequest;
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0001CA38 File Offset: 0x0001AC38
		public static PVPDRRetireRequest DeserializeLengthDelimited(Stream stream, PVPDRRetireRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PVPDRRetireRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0001CA60 File Offset: 0x0001AC60
		public static PVPDRRetireRequest Deserialize(Stream stream, PVPDRRetireRequest instance, long limit)
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

		// Token: 0x060007F7 RID: 2039 RVA: 0x0001CACD File Offset: 0x0001ACCD
		public void Serialize(Stream stream)
		{
			PVPDRRetireRequest.Serialize(stream, this);
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, PVPDRRetireRequest instance)
		{
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x02000593 RID: 1427
		public enum PacketID
		{
			// Token: 0x04001F15 RID: 7957
			ID = 380,
			// Token: 0x04001F16 RID: 7958
			System = 0
		}
	}
}
