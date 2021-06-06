using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x02000114 RID: 276
	public class PVPDRSessionStartResponse : IProtoBuf
	{
		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06001248 RID: 4680 RVA: 0x0004069D File Offset: 0x0003E89D
		// (set) Token: 0x06001249 RID: 4681 RVA: 0x000406A5 File Offset: 0x0003E8A5
		public ErrorCode ErrorCode { get; set; }

		// Token: 0x0600124A RID: 4682 RVA: 0x000406B0 File Offset: 0x0003E8B0
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.ErrorCode.GetHashCode();
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x000406E0 File Offset: 0x0003E8E0
		public override bool Equals(object obj)
		{
			PVPDRSessionStartResponse pvpdrsessionStartResponse = obj as PVPDRSessionStartResponse;
			return pvpdrsessionStartResponse != null && this.ErrorCode.Equals(pvpdrsessionStartResponse.ErrorCode);
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x0004071D File Offset: 0x0003E91D
		public void Deserialize(Stream stream)
		{
			PVPDRSessionStartResponse.Deserialize(stream, this);
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x00040727 File Offset: 0x0003E927
		public static PVPDRSessionStartResponse Deserialize(Stream stream, PVPDRSessionStartResponse instance)
		{
			return PVPDRSessionStartResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x00040734 File Offset: 0x0003E934
		public static PVPDRSessionStartResponse DeserializeLengthDelimited(Stream stream)
		{
			PVPDRSessionStartResponse pvpdrsessionStartResponse = new PVPDRSessionStartResponse();
			PVPDRSessionStartResponse.DeserializeLengthDelimited(stream, pvpdrsessionStartResponse);
			return pvpdrsessionStartResponse;
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x00040750 File Offset: 0x0003E950
		public static PVPDRSessionStartResponse DeserializeLengthDelimited(Stream stream, PVPDRSessionStartResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PVPDRSessionStartResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x00040778 File Offset: 0x0003E978
		public static PVPDRSessionStartResponse Deserialize(Stream stream, PVPDRSessionStartResponse instance, long limit)
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

		// Token: 0x06001251 RID: 4689 RVA: 0x000407F8 File Offset: 0x0003E9F8
		public void Serialize(Stream stream)
		{
			PVPDRSessionStartResponse.Serialize(stream, this);
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x00040801 File Offset: 0x0003EA01
		public static void Serialize(Stream stream, PVPDRSessionStartResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ErrorCode));
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x00040817 File Offset: 0x0003EA17
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.ErrorCode)) + 1U;
		}

		// Token: 0x02000614 RID: 1556
		public enum PacketID
		{
			// Token: 0x04002076 RID: 8310
			ID = 383
		}
	}
}
