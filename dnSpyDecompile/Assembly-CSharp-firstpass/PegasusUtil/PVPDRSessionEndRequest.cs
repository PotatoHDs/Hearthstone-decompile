using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000082 RID: 130
	public class PVPDRSessionEndRequest : IProtoBuf
	{
		// Token: 0x06000808 RID: 2056 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0001CC90 File Offset: 0x0001AE90
		public override bool Equals(object obj)
		{
			return obj is PVPDRSessionEndRequest;
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0001CC9D File Offset: 0x0001AE9D
		public void Deserialize(Stream stream)
		{
			PVPDRSessionEndRequest.Deserialize(stream, this);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0001CCA7 File Offset: 0x0001AEA7
		public static PVPDRSessionEndRequest Deserialize(Stream stream, PVPDRSessionEndRequest instance)
		{
			return PVPDRSessionEndRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0001CCB4 File Offset: 0x0001AEB4
		public static PVPDRSessionEndRequest DeserializeLengthDelimited(Stream stream)
		{
			PVPDRSessionEndRequest pvpdrsessionEndRequest = new PVPDRSessionEndRequest();
			PVPDRSessionEndRequest.DeserializeLengthDelimited(stream, pvpdrsessionEndRequest);
			return pvpdrsessionEndRequest;
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0001CCD0 File Offset: 0x0001AED0
		public static PVPDRSessionEndRequest DeserializeLengthDelimited(Stream stream, PVPDRSessionEndRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PVPDRSessionEndRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0001CCF8 File Offset: 0x0001AEF8
		public static PVPDRSessionEndRequest Deserialize(Stream stream, PVPDRSessionEndRequest instance, long limit)
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

		// Token: 0x0600080F RID: 2063 RVA: 0x0001CD65 File Offset: 0x0001AF65
		public void Serialize(Stream stream)
		{
			PVPDRSessionEndRequest.Serialize(stream, this);
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, PVPDRSessionEndRequest instance)
		{
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x02000595 RID: 1429
		public enum PacketID
		{
			// Token: 0x04001F1B RID: 7963
			ID = 388,
			// Token: 0x04001F1C RID: 7964
			System = 0
		}
	}
}
