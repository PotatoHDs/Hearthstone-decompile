using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x0200007E RID: 126
	public class PVPDRSessionInfoRequest : IProtoBuf
	{
		// Token: 0x060007DA RID: 2010 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0001C842 File Offset: 0x0001AA42
		public override bool Equals(object obj)
		{
			return obj is PVPDRSessionInfoRequest;
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0001C84F File Offset: 0x0001AA4F
		public void Deserialize(Stream stream)
		{
			PVPDRSessionInfoRequest.Deserialize(stream, this);
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0001C859 File Offset: 0x0001AA59
		public static PVPDRSessionInfoRequest Deserialize(Stream stream, PVPDRSessionInfoRequest instance)
		{
			return PVPDRSessionInfoRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0001C864 File Offset: 0x0001AA64
		public static PVPDRSessionInfoRequest DeserializeLengthDelimited(Stream stream)
		{
			PVPDRSessionInfoRequest pvpdrsessionInfoRequest = new PVPDRSessionInfoRequest();
			PVPDRSessionInfoRequest.DeserializeLengthDelimited(stream, pvpdrsessionInfoRequest);
			return pvpdrsessionInfoRequest;
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0001C880 File Offset: 0x0001AA80
		public static PVPDRSessionInfoRequest DeserializeLengthDelimited(Stream stream, PVPDRSessionInfoRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PVPDRSessionInfoRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0001C8A8 File Offset: 0x0001AAA8
		public static PVPDRSessionInfoRequest Deserialize(Stream stream, PVPDRSessionInfoRequest instance, long limit)
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

		// Token: 0x060007E1 RID: 2017 RVA: 0x0001C915 File Offset: 0x0001AB15
		public void Serialize(Stream stream)
		{
			PVPDRSessionInfoRequest.Serialize(stream, this);
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, PVPDRSessionInfoRequest instance)
		{
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x02000591 RID: 1425
		public enum PacketID
		{
			// Token: 0x04001F0F RID: 7951
			ID = 376,
			// Token: 0x04001F10 RID: 7952
			System = 0
		}
	}
}
