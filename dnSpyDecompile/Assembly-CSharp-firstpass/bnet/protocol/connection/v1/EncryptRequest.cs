using System;
using System.IO;

namespace bnet.protocol.connection.v1
{
	// Token: 0x02000446 RID: 1094
	public class EncryptRequest : IProtoBuf
	{
		// Token: 0x06004A54 RID: 19028 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x06004A55 RID: 19029 RVA: 0x000E7CDA File Offset: 0x000E5EDA
		public override bool Equals(object obj)
		{
			return obj is EncryptRequest;
		}

		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x06004A56 RID: 19030 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004A57 RID: 19031 RVA: 0x000E7CE7 File Offset: 0x000E5EE7
		public static EncryptRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<EncryptRequest>(bs, 0, -1);
		}

		// Token: 0x06004A58 RID: 19032 RVA: 0x000E7CF1 File Offset: 0x000E5EF1
		public void Deserialize(Stream stream)
		{
			EncryptRequest.Deserialize(stream, this);
		}

		// Token: 0x06004A59 RID: 19033 RVA: 0x000E7CFB File Offset: 0x000E5EFB
		public static EncryptRequest Deserialize(Stream stream, EncryptRequest instance)
		{
			return EncryptRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004A5A RID: 19034 RVA: 0x000E7D08 File Offset: 0x000E5F08
		public static EncryptRequest DeserializeLengthDelimited(Stream stream)
		{
			EncryptRequest encryptRequest = new EncryptRequest();
			EncryptRequest.DeserializeLengthDelimited(stream, encryptRequest);
			return encryptRequest;
		}

		// Token: 0x06004A5B RID: 19035 RVA: 0x000E7D24 File Offset: 0x000E5F24
		public static EncryptRequest DeserializeLengthDelimited(Stream stream, EncryptRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return EncryptRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004A5C RID: 19036 RVA: 0x000E7D4C File Offset: 0x000E5F4C
		public static EncryptRequest Deserialize(Stream stream, EncryptRequest instance, long limit)
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

		// Token: 0x06004A5D RID: 19037 RVA: 0x000E7DB9 File Offset: 0x000E5FB9
		public void Serialize(Stream stream)
		{
			EncryptRequest.Serialize(stream, this);
		}

		// Token: 0x06004A5E RID: 19038 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, EncryptRequest instance)
		{
		}

		// Token: 0x06004A5F RID: 19039 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}
	}
}
