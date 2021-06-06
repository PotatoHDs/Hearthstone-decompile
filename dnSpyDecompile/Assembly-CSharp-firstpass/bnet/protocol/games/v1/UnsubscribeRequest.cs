using System;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x02000381 RID: 897
	public class UnsubscribeRequest : IProtoBuf
	{
		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x06003924 RID: 14628 RVA: 0x000BA45A File Offset: 0x000B865A
		// (set) Token: 0x06003925 RID: 14629 RVA: 0x000BA462 File Offset: 0x000B8662
		public ulong SubscriptionId { get; set; }

		// Token: 0x06003926 RID: 14630 RVA: 0x000BA46B File Offset: 0x000B866B
		public void SetSubscriptionId(ulong val)
		{
			this.SubscriptionId = val;
		}

		// Token: 0x06003927 RID: 14631 RVA: 0x000BA474 File Offset: 0x000B8674
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.SubscriptionId.GetHashCode();
		}

		// Token: 0x06003928 RID: 14632 RVA: 0x000BA49C File Offset: 0x000B869C
		public override bool Equals(object obj)
		{
			UnsubscribeRequest unsubscribeRequest = obj as UnsubscribeRequest;
			return unsubscribeRequest != null && this.SubscriptionId.Equals(unsubscribeRequest.SubscriptionId);
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06003929 RID: 14633 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600392A RID: 14634 RVA: 0x000BA4CE File Offset: 0x000B86CE
		public static UnsubscribeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UnsubscribeRequest>(bs, 0, -1);
		}

		// Token: 0x0600392B RID: 14635 RVA: 0x000BA4D8 File Offset: 0x000B86D8
		public void Deserialize(Stream stream)
		{
			UnsubscribeRequest.Deserialize(stream, this);
		}

		// Token: 0x0600392C RID: 14636 RVA: 0x000BA4E2 File Offset: 0x000B86E2
		public static UnsubscribeRequest Deserialize(Stream stream, UnsubscribeRequest instance)
		{
			return UnsubscribeRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600392D RID: 14637 RVA: 0x000BA4F0 File Offset: 0x000B86F0
		public static UnsubscribeRequest DeserializeLengthDelimited(Stream stream)
		{
			UnsubscribeRequest unsubscribeRequest = new UnsubscribeRequest();
			UnsubscribeRequest.DeserializeLengthDelimited(stream, unsubscribeRequest);
			return unsubscribeRequest;
		}

		// Token: 0x0600392E RID: 14638 RVA: 0x000BA50C File Offset: 0x000B870C
		public static UnsubscribeRequest DeserializeLengthDelimited(Stream stream, UnsubscribeRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UnsubscribeRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x0600392F RID: 14639 RVA: 0x000BA534 File Offset: 0x000B8734
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
				else if (num == 8)
				{
					instance.SubscriptionId = ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06003930 RID: 14640 RVA: 0x000BA5B3 File Offset: 0x000B87B3
		public void Serialize(Stream stream)
		{
			UnsubscribeRequest.Serialize(stream, this);
		}

		// Token: 0x06003931 RID: 14641 RVA: 0x000BA5BC File Offset: 0x000B87BC
		public static void Serialize(Stream stream, UnsubscribeRequest instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.SubscriptionId);
		}

		// Token: 0x06003932 RID: 14642 RVA: 0x000BA5D1 File Offset: 0x000B87D1
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64(this.SubscriptionId) + 1U;
		}
	}
}
