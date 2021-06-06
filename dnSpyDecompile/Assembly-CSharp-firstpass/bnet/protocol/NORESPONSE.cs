using System;
using System.IO;

namespace bnet.protocol
{
	// Token: 0x020002AE RID: 686
	public class NORESPONSE : IProtoBuf
	{
		// Token: 0x060027E9 RID: 10217 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x060027EA RID: 10218 RVA: 0x0008D8DC File Offset: 0x0008BADC
		public override bool Equals(object obj)
		{
			return obj is NORESPONSE;
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x060027EB RID: 10219 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060027EC RID: 10220 RVA: 0x0008D8E9 File Offset: 0x0008BAE9
		public static NORESPONSE ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<NORESPONSE>(bs, 0, -1);
		}

		// Token: 0x060027ED RID: 10221 RVA: 0x0008D8F3 File Offset: 0x0008BAF3
		public void Deserialize(Stream stream)
		{
			NORESPONSE.Deserialize(stream, this);
		}

		// Token: 0x060027EE RID: 10222 RVA: 0x0008D8FD File Offset: 0x0008BAFD
		public static NORESPONSE Deserialize(Stream stream, NORESPONSE instance)
		{
			return NORESPONSE.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060027EF RID: 10223 RVA: 0x0008D908 File Offset: 0x0008BB08
		public static NORESPONSE DeserializeLengthDelimited(Stream stream)
		{
			NORESPONSE noresponse = new NORESPONSE();
			NORESPONSE.DeserializeLengthDelimited(stream, noresponse);
			return noresponse;
		}

		// Token: 0x060027F0 RID: 10224 RVA: 0x0008D924 File Offset: 0x0008BB24
		public static NORESPONSE DeserializeLengthDelimited(Stream stream, NORESPONSE instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return NORESPONSE.Deserialize(stream, instance, num);
		}

		// Token: 0x060027F1 RID: 10225 RVA: 0x0008D94C File Offset: 0x0008BB4C
		public static NORESPONSE Deserialize(Stream stream, NORESPONSE instance, long limit)
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

		// Token: 0x060027F2 RID: 10226 RVA: 0x0008D9B9 File Offset: 0x0008BBB9
		public void Serialize(Stream stream)
		{
			NORESPONSE.Serialize(stream, this);
		}

		// Token: 0x060027F3 RID: 10227 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, NORESPONSE instance)
		{
		}

		// Token: 0x060027F4 RID: 10228 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}
	}
}
