using System;
using System.IO;

namespace bnet.protocol.friends.v2.client
{
	// Token: 0x020003FC RID: 1020
	public class GetStateRequest : IProtoBuf
	{
		// Token: 0x060043B4 RID: 17332 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x060043B5 RID: 17333 RVA: 0x000D6012 File Offset: 0x000D4212
		public override bool Equals(object obj)
		{
			return obj is GetStateRequest;
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x060043B6 RID: 17334 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060043B7 RID: 17335 RVA: 0x000D601F File Offset: 0x000D421F
		public static GetStateRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetStateRequest>(bs, 0, -1);
		}

		// Token: 0x060043B8 RID: 17336 RVA: 0x000D6029 File Offset: 0x000D4229
		public void Deserialize(Stream stream)
		{
			GetStateRequest.Deserialize(stream, this);
		}

		// Token: 0x060043B9 RID: 17337 RVA: 0x000D6033 File Offset: 0x000D4233
		public static GetStateRequest Deserialize(Stream stream, GetStateRequest instance)
		{
			return GetStateRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060043BA RID: 17338 RVA: 0x000D6040 File Offset: 0x000D4240
		public static GetStateRequest DeserializeLengthDelimited(Stream stream)
		{
			GetStateRequest getStateRequest = new GetStateRequest();
			GetStateRequest.DeserializeLengthDelimited(stream, getStateRequest);
			return getStateRequest;
		}

		// Token: 0x060043BB RID: 17339 RVA: 0x000D605C File Offset: 0x000D425C
		public static GetStateRequest DeserializeLengthDelimited(Stream stream, GetStateRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetStateRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060043BC RID: 17340 RVA: 0x000D6084 File Offset: 0x000D4284
		public static GetStateRequest Deserialize(Stream stream, GetStateRequest instance, long limit)
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

		// Token: 0x060043BD RID: 17341 RVA: 0x000D60F1 File Offset: 0x000D42F1
		public void Serialize(Stream stream)
		{
			GetStateRequest.Serialize(stream, this);
		}

		// Token: 0x060043BE RID: 17342 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, GetStateRequest instance)
		{
		}

		// Token: 0x060043BF RID: 17343 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}
	}
}
