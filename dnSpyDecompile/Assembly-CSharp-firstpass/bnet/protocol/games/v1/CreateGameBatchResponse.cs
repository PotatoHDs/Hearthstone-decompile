using System;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x020003AC RID: 940
	public class CreateGameBatchResponse : IProtoBuf
	{
		// Token: 0x06003D20 RID: 15648 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x06003D21 RID: 15649 RVA: 0x000C4FF3 File Offset: 0x000C31F3
		public override bool Equals(object obj)
		{
			return obj is CreateGameBatchResponse;
		}

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x06003D22 RID: 15650 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003D23 RID: 15651 RVA: 0x000C5000 File Offset: 0x000C3200
		public static CreateGameBatchResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateGameBatchResponse>(bs, 0, -1);
		}

		// Token: 0x06003D24 RID: 15652 RVA: 0x000C500A File Offset: 0x000C320A
		public void Deserialize(Stream stream)
		{
			CreateGameBatchResponse.Deserialize(stream, this);
		}

		// Token: 0x06003D25 RID: 15653 RVA: 0x000C5014 File Offset: 0x000C3214
		public static CreateGameBatchResponse Deserialize(Stream stream, CreateGameBatchResponse instance)
		{
			return CreateGameBatchResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003D26 RID: 15654 RVA: 0x000C5020 File Offset: 0x000C3220
		public static CreateGameBatchResponse DeserializeLengthDelimited(Stream stream)
		{
			CreateGameBatchResponse createGameBatchResponse = new CreateGameBatchResponse();
			CreateGameBatchResponse.DeserializeLengthDelimited(stream, createGameBatchResponse);
			return createGameBatchResponse;
		}

		// Token: 0x06003D27 RID: 15655 RVA: 0x000C503C File Offset: 0x000C323C
		public static CreateGameBatchResponse DeserializeLengthDelimited(Stream stream, CreateGameBatchResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CreateGameBatchResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06003D28 RID: 15656 RVA: 0x000C5064 File Offset: 0x000C3264
		public static CreateGameBatchResponse Deserialize(Stream stream, CreateGameBatchResponse instance, long limit)
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

		// Token: 0x06003D29 RID: 15657 RVA: 0x000C50D1 File Offset: 0x000C32D1
		public void Serialize(Stream stream)
		{
			CreateGameBatchResponse.Serialize(stream, this);
		}

		// Token: 0x06003D2A RID: 15658 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, CreateGameBatchResponse instance)
		{
		}

		// Token: 0x06003D2B RID: 15659 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}
	}
}
