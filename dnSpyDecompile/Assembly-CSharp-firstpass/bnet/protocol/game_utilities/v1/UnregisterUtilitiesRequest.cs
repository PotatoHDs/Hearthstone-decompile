using System;
using System.IO;

namespace bnet.protocol.game_utilities.v1
{
	// Token: 0x02000365 RID: 869
	public class UnregisterUtilitiesRequest : IProtoBuf
	{
		// Token: 0x060036F9 RID: 14073 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x060036FA RID: 14074 RVA: 0x000B515C File Offset: 0x000B335C
		public override bool Equals(object obj)
		{
			return obj is UnregisterUtilitiesRequest;
		}

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x060036FB RID: 14075 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060036FC RID: 14076 RVA: 0x000B5169 File Offset: 0x000B3369
		public static UnregisterUtilitiesRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UnregisterUtilitiesRequest>(bs, 0, -1);
		}

		// Token: 0x060036FD RID: 14077 RVA: 0x000B5173 File Offset: 0x000B3373
		public void Deserialize(Stream stream)
		{
			UnregisterUtilitiesRequest.Deserialize(stream, this);
		}

		// Token: 0x060036FE RID: 14078 RVA: 0x000B517D File Offset: 0x000B337D
		public static UnregisterUtilitiesRequest Deserialize(Stream stream, UnregisterUtilitiesRequest instance)
		{
			return UnregisterUtilitiesRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060036FF RID: 14079 RVA: 0x000B5188 File Offset: 0x000B3388
		public static UnregisterUtilitiesRequest DeserializeLengthDelimited(Stream stream)
		{
			UnregisterUtilitiesRequest unregisterUtilitiesRequest = new UnregisterUtilitiesRequest();
			UnregisterUtilitiesRequest.DeserializeLengthDelimited(stream, unregisterUtilitiesRequest);
			return unregisterUtilitiesRequest;
		}

		// Token: 0x06003700 RID: 14080 RVA: 0x000B51A4 File Offset: 0x000B33A4
		public static UnregisterUtilitiesRequest DeserializeLengthDelimited(Stream stream, UnregisterUtilitiesRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UnregisterUtilitiesRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06003701 RID: 14081 RVA: 0x000B51CC File Offset: 0x000B33CC
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

		// Token: 0x06003702 RID: 14082 RVA: 0x000B5239 File Offset: 0x000B3439
		public void Serialize(Stream stream)
		{
			UnregisterUtilitiesRequest.Serialize(stream, this);
		}

		// Token: 0x06003703 RID: 14083 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, UnregisterUtilitiesRequest instance)
		{
		}

		// Token: 0x06003704 RID: 14084 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}
	}
}
