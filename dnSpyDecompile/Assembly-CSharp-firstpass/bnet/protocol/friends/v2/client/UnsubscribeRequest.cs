using System;
using System.IO;

namespace bnet.protocol.friends.v2.client
{
	// Token: 0x020003FB RID: 1019
	public class UnsubscribeRequest : IProtoBuf
	{
		// Token: 0x060043A7 RID: 17319 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x060043A8 RID: 17320 RVA: 0x000D5F2B File Offset: 0x000D412B
		public override bool Equals(object obj)
		{
			return obj is UnsubscribeRequest;
		}

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x060043A9 RID: 17321 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060043AA RID: 17322 RVA: 0x000D5F38 File Offset: 0x000D4138
		public static UnsubscribeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UnsubscribeRequest>(bs, 0, -1);
		}

		// Token: 0x060043AB RID: 17323 RVA: 0x000D5F42 File Offset: 0x000D4142
		public void Deserialize(Stream stream)
		{
			UnsubscribeRequest.Deserialize(stream, this);
		}

		// Token: 0x060043AC RID: 17324 RVA: 0x000D5F4C File Offset: 0x000D414C
		public static UnsubscribeRequest Deserialize(Stream stream, UnsubscribeRequest instance)
		{
			return UnsubscribeRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060043AD RID: 17325 RVA: 0x000D5F58 File Offset: 0x000D4158
		public static UnsubscribeRequest DeserializeLengthDelimited(Stream stream)
		{
			UnsubscribeRequest unsubscribeRequest = new UnsubscribeRequest();
			UnsubscribeRequest.DeserializeLengthDelimited(stream, unsubscribeRequest);
			return unsubscribeRequest;
		}

		// Token: 0x060043AE RID: 17326 RVA: 0x000D5F74 File Offset: 0x000D4174
		public static UnsubscribeRequest DeserializeLengthDelimited(Stream stream, UnsubscribeRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UnsubscribeRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060043AF RID: 17327 RVA: 0x000D5F9C File Offset: 0x000D419C
		public static UnsubscribeRequest Deserialize(Stream stream, UnsubscribeRequest instance, long limit)
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

		// Token: 0x060043B0 RID: 17328 RVA: 0x000D6009 File Offset: 0x000D4209
		public void Serialize(Stream stream)
		{
			UnsubscribeRequest.Serialize(stream, this);
		}

		// Token: 0x060043B1 RID: 17329 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, UnsubscribeRequest instance)
		{
		}

		// Token: 0x060043B2 RID: 17330 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}
	}
}
