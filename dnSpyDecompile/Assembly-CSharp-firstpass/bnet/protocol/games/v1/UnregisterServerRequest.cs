using System;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x0200037B RID: 891
	public class UnregisterServerRequest : IProtoBuf
	{
		// Token: 0x060038C3 RID: 14531 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x060038C4 RID: 14532 RVA: 0x000B99B0 File Offset: 0x000B7BB0
		public override bool Equals(object obj)
		{
			return obj is UnregisterServerRequest;
		}

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x060038C5 RID: 14533 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060038C6 RID: 14534 RVA: 0x000B99BD File Offset: 0x000B7BBD
		public static UnregisterServerRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UnregisterServerRequest>(bs, 0, -1);
		}

		// Token: 0x060038C7 RID: 14535 RVA: 0x000B99C7 File Offset: 0x000B7BC7
		public void Deserialize(Stream stream)
		{
			UnregisterServerRequest.Deserialize(stream, this);
		}

		// Token: 0x060038C8 RID: 14536 RVA: 0x000B99D1 File Offset: 0x000B7BD1
		public static UnregisterServerRequest Deserialize(Stream stream, UnregisterServerRequest instance)
		{
			return UnregisterServerRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060038C9 RID: 14537 RVA: 0x000B99DC File Offset: 0x000B7BDC
		public static UnregisterServerRequest DeserializeLengthDelimited(Stream stream)
		{
			UnregisterServerRequest unregisterServerRequest = new UnregisterServerRequest();
			UnregisterServerRequest.DeserializeLengthDelimited(stream, unregisterServerRequest);
			return unregisterServerRequest;
		}

		// Token: 0x060038CA RID: 14538 RVA: 0x000B99F8 File Offset: 0x000B7BF8
		public static UnregisterServerRequest DeserializeLengthDelimited(Stream stream, UnregisterServerRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UnregisterServerRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060038CB RID: 14539 RVA: 0x000B9A20 File Offset: 0x000B7C20
		public static UnregisterServerRequest Deserialize(Stream stream, UnregisterServerRequest instance, long limit)
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

		// Token: 0x060038CC RID: 14540 RVA: 0x000B9A8D File Offset: 0x000B7C8D
		public void Serialize(Stream stream)
		{
			UnregisterServerRequest.Serialize(stream, this);
		}

		// Token: 0x060038CD RID: 14541 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, UnregisterServerRequest instance)
		{
		}

		// Token: 0x060038CE RID: 14542 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}
	}
}
