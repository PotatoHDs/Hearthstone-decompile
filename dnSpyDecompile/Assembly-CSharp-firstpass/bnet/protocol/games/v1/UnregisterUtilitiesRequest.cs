using System;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x0200037E RID: 894
	public class UnregisterUtilitiesRequest : IProtoBuf
	{
		// Token: 0x060038F7 RID: 14583 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x060038F8 RID: 14584 RVA: 0x000BA018 File Offset: 0x000B8218
		public override bool Equals(object obj)
		{
			return obj is UnregisterUtilitiesRequest;
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x060038F9 RID: 14585 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060038FA RID: 14586 RVA: 0x000BA025 File Offset: 0x000B8225
		public static UnregisterUtilitiesRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UnregisterUtilitiesRequest>(bs, 0, -1);
		}

		// Token: 0x060038FB RID: 14587 RVA: 0x000BA02F File Offset: 0x000B822F
		public void Deserialize(Stream stream)
		{
			UnregisterUtilitiesRequest.Deserialize(stream, this);
		}

		// Token: 0x060038FC RID: 14588 RVA: 0x000BA039 File Offset: 0x000B8239
		public static UnregisterUtilitiesRequest Deserialize(Stream stream, UnregisterUtilitiesRequest instance)
		{
			return UnregisterUtilitiesRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060038FD RID: 14589 RVA: 0x000BA044 File Offset: 0x000B8244
		public static UnregisterUtilitiesRequest DeserializeLengthDelimited(Stream stream)
		{
			UnregisterUtilitiesRequest unregisterUtilitiesRequest = new UnregisterUtilitiesRequest();
			UnregisterUtilitiesRequest.DeserializeLengthDelimited(stream, unregisterUtilitiesRequest);
			return unregisterUtilitiesRequest;
		}

		// Token: 0x060038FE RID: 14590 RVA: 0x000BA060 File Offset: 0x000B8260
		public static UnregisterUtilitiesRequest DeserializeLengthDelimited(Stream stream, UnregisterUtilitiesRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UnregisterUtilitiesRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060038FF RID: 14591 RVA: 0x000BA088 File Offset: 0x000B8288
		public static UnregisterUtilitiesRequest Deserialize(Stream stream, UnregisterUtilitiesRequest instance, long limit)
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

		// Token: 0x06003900 RID: 14592 RVA: 0x000BA0F5 File Offset: 0x000B82F5
		public void Serialize(Stream stream)
		{
			UnregisterUtilitiesRequest.Serialize(stream, this);
		}

		// Token: 0x06003901 RID: 14593 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, UnregisterUtilitiesRequest instance)
		{
		}

		// Token: 0x06003902 RID: 14594 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}
	}
}
