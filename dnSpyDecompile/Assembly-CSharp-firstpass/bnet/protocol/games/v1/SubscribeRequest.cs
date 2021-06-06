using System;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x0200037F RID: 895
	public class SubscribeRequest : IProtoBuf
	{
		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x06003904 RID: 14596 RVA: 0x000BA0FE File Offset: 0x000B82FE
		// (set) Token: 0x06003905 RID: 14597 RVA: 0x000BA106 File Offset: 0x000B8306
		public ulong ObjectId { get; set; }

		// Token: 0x06003906 RID: 14598 RVA: 0x000BA10F File Offset: 0x000B830F
		public void SetObjectId(ulong val)
		{
			this.ObjectId = val;
		}

		// Token: 0x06003907 RID: 14599 RVA: 0x000BA118 File Offset: 0x000B8318
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.ObjectId.GetHashCode();
		}

		// Token: 0x06003908 RID: 14600 RVA: 0x000BA140 File Offset: 0x000B8340
		public override bool Equals(object obj)
		{
			SubscribeRequest subscribeRequest = obj as SubscribeRequest;
			return subscribeRequest != null && this.ObjectId.Equals(subscribeRequest.ObjectId);
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06003909 RID: 14601 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600390A RID: 14602 RVA: 0x000BA172 File Offset: 0x000B8372
		public static SubscribeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeRequest>(bs, 0, -1);
		}

		// Token: 0x0600390B RID: 14603 RVA: 0x000BA17C File Offset: 0x000B837C
		public void Deserialize(Stream stream)
		{
			SubscribeRequest.Deserialize(stream, this);
		}

		// Token: 0x0600390C RID: 14604 RVA: 0x000BA186 File Offset: 0x000B8386
		public static SubscribeRequest Deserialize(Stream stream, SubscribeRequest instance)
		{
			return SubscribeRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600390D RID: 14605 RVA: 0x000BA194 File Offset: 0x000B8394
		public static SubscribeRequest DeserializeLengthDelimited(Stream stream)
		{
			SubscribeRequest subscribeRequest = new SubscribeRequest();
			SubscribeRequest.DeserializeLengthDelimited(stream, subscribeRequest);
			return subscribeRequest;
		}

		// Token: 0x0600390E RID: 14606 RVA: 0x000BA1B0 File Offset: 0x000B83B0
		public static SubscribeRequest DeserializeLengthDelimited(Stream stream, SubscribeRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubscribeRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x0600390F RID: 14607 RVA: 0x000BA1D8 File Offset: 0x000B83D8
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
				else if (num == 8)
				{
					instance.ObjectId = ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06003910 RID: 14608 RVA: 0x000BA257 File Offset: 0x000B8457
		public void Serialize(Stream stream)
		{
			SubscribeRequest.Serialize(stream, this);
		}

		// Token: 0x06003911 RID: 14609 RVA: 0x000BA260 File Offset: 0x000B8460
		public static void Serialize(Stream stream, SubscribeRequest instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.ObjectId);
		}

		// Token: 0x06003912 RID: 14610 RVA: 0x000BA275 File Offset: 0x000B8475
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64(this.ObjectId) + 1U;
		}
	}
}
