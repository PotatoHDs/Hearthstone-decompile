using System;
using System.IO;

namespace bnet.protocol.connection.v1
{
	// Token: 0x02000444 RID: 1092
	public class DisconnectRequest : IProtoBuf
	{
		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x06004A31 RID: 18993 RVA: 0x000E78E9 File Offset: 0x000E5AE9
		// (set) Token: 0x06004A32 RID: 18994 RVA: 0x000E78F1 File Offset: 0x000E5AF1
		public uint ErrorCode { get; set; }

		// Token: 0x06004A33 RID: 18995 RVA: 0x000E78FA File Offset: 0x000E5AFA
		public void SetErrorCode(uint val)
		{
			this.ErrorCode = val;
		}

		// Token: 0x06004A34 RID: 18996 RVA: 0x000E7904 File Offset: 0x000E5B04
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.ErrorCode.GetHashCode();
		}

		// Token: 0x06004A35 RID: 18997 RVA: 0x000E792C File Offset: 0x000E5B2C
		public override bool Equals(object obj)
		{
			DisconnectRequest disconnectRequest = obj as DisconnectRequest;
			return disconnectRequest != null && this.ErrorCode.Equals(disconnectRequest.ErrorCode);
		}

		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x06004A36 RID: 18998 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004A37 RID: 18999 RVA: 0x000E795E File Offset: 0x000E5B5E
		public static DisconnectRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<DisconnectRequest>(bs, 0, -1);
		}

		// Token: 0x06004A38 RID: 19000 RVA: 0x000E7968 File Offset: 0x000E5B68
		public void Deserialize(Stream stream)
		{
			DisconnectRequest.Deserialize(stream, this);
		}

		// Token: 0x06004A39 RID: 19001 RVA: 0x000E7972 File Offset: 0x000E5B72
		public static DisconnectRequest Deserialize(Stream stream, DisconnectRequest instance)
		{
			return DisconnectRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004A3A RID: 19002 RVA: 0x000E7980 File Offset: 0x000E5B80
		public static DisconnectRequest DeserializeLengthDelimited(Stream stream)
		{
			DisconnectRequest disconnectRequest = new DisconnectRequest();
			DisconnectRequest.DeserializeLengthDelimited(stream, disconnectRequest);
			return disconnectRequest;
		}

		// Token: 0x06004A3B RID: 19003 RVA: 0x000E799C File Offset: 0x000E5B9C
		public static DisconnectRequest DeserializeLengthDelimited(Stream stream, DisconnectRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DisconnectRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004A3C RID: 19004 RVA: 0x000E79C4 File Offset: 0x000E5BC4
		public static DisconnectRequest Deserialize(Stream stream, DisconnectRequest instance, long limit)
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
					instance.ErrorCode = ProtocolParser.ReadUInt32(stream);
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

		// Token: 0x06004A3D RID: 19005 RVA: 0x000E7A43 File Offset: 0x000E5C43
		public void Serialize(Stream stream)
		{
			DisconnectRequest.Serialize(stream, this);
		}

		// Token: 0x06004A3E RID: 19006 RVA: 0x000E7A4C File Offset: 0x000E5C4C
		public static void Serialize(Stream stream, DisconnectRequest instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.ErrorCode);
		}

		// Token: 0x06004A3F RID: 19007 RVA: 0x000E7A61 File Offset: 0x000E5C61
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt32(this.ErrorCode) + 1U;
		}
	}
}
