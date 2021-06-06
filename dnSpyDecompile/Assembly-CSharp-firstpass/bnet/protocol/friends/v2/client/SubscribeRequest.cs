using System;
using System.IO;

namespace bnet.protocol.friends.v2.client
{
	// Token: 0x020003F9 RID: 1017
	public class SubscribeRequest : IProtoBuf
	{
		// Token: 0x0600438A RID: 17290 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x0600438B RID: 17291 RVA: 0x000D5C3F File Offset: 0x000D3E3F
		public override bool Equals(object obj)
		{
			return obj is SubscribeRequest;
		}

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x0600438C RID: 17292 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600438D RID: 17293 RVA: 0x000D5C4C File Offset: 0x000D3E4C
		public static SubscribeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeRequest>(bs, 0, -1);
		}

		// Token: 0x0600438E RID: 17294 RVA: 0x000D5C56 File Offset: 0x000D3E56
		public void Deserialize(Stream stream)
		{
			SubscribeRequest.Deserialize(stream, this);
		}

		// Token: 0x0600438F RID: 17295 RVA: 0x000D5C60 File Offset: 0x000D3E60
		public static SubscribeRequest Deserialize(Stream stream, SubscribeRequest instance)
		{
			return SubscribeRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004390 RID: 17296 RVA: 0x000D5C6C File Offset: 0x000D3E6C
		public static SubscribeRequest DeserializeLengthDelimited(Stream stream)
		{
			SubscribeRequest subscribeRequest = new SubscribeRequest();
			SubscribeRequest.DeserializeLengthDelimited(stream, subscribeRequest);
			return subscribeRequest;
		}

		// Token: 0x06004391 RID: 17297 RVA: 0x000D5C88 File Offset: 0x000D3E88
		public static SubscribeRequest DeserializeLengthDelimited(Stream stream, SubscribeRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubscribeRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004392 RID: 17298 RVA: 0x000D5CB0 File Offset: 0x000D3EB0
		public static SubscribeRequest Deserialize(Stream stream, SubscribeRequest instance, long limit)
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

		// Token: 0x06004393 RID: 17299 RVA: 0x000D5D1D File Offset: 0x000D3F1D
		public void Serialize(Stream stream)
		{
			SubscribeRequest.Serialize(stream, this);
		}

		// Token: 0x06004394 RID: 17300 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, SubscribeRequest instance)
		{
		}

		// Token: 0x06004395 RID: 17301 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}
	}
}
