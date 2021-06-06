using System;
using System.IO;

namespace bnet.protocol.notification.v1
{
	// Token: 0x02000346 RID: 838
	public class SubscribeRequest : IProtoBuf
	{
		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06003422 RID: 13346 RVA: 0x000AD533 File Offset: 0x000AB733
		// (set) Token: 0x06003423 RID: 13347 RVA: 0x000AD53B File Offset: 0x000AB73B
		public Subscription Subscription
		{
			get
			{
				return this._Subscription;
			}
			set
			{
				this._Subscription = value;
				this.HasSubscription = (value != null);
			}
		}

		// Token: 0x06003424 RID: 13348 RVA: 0x000AD54E File Offset: 0x000AB74E
		public void SetSubscription(Subscription val)
		{
			this.Subscription = val;
		}

		// Token: 0x06003425 RID: 13349 RVA: 0x000AD558 File Offset: 0x000AB758
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasSubscription)
			{
				num ^= this.Subscription.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003426 RID: 13350 RVA: 0x000AD588 File Offset: 0x000AB788
		public override bool Equals(object obj)
		{
			SubscribeRequest subscribeRequest = obj as SubscribeRequest;
			return subscribeRequest != null && this.HasSubscription == subscribeRequest.HasSubscription && (!this.HasSubscription || this.Subscription.Equals(subscribeRequest.Subscription));
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06003427 RID: 13351 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003428 RID: 13352 RVA: 0x000AD5CD File Offset: 0x000AB7CD
		public static SubscribeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeRequest>(bs, 0, -1);
		}

		// Token: 0x06003429 RID: 13353 RVA: 0x000AD5D7 File Offset: 0x000AB7D7
		public void Deserialize(Stream stream)
		{
			SubscribeRequest.Deserialize(stream, this);
		}

		// Token: 0x0600342A RID: 13354 RVA: 0x000AD5E1 File Offset: 0x000AB7E1
		public static SubscribeRequest Deserialize(Stream stream, SubscribeRequest instance)
		{
			return SubscribeRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600342B RID: 13355 RVA: 0x000AD5EC File Offset: 0x000AB7EC
		public static SubscribeRequest DeserializeLengthDelimited(Stream stream)
		{
			SubscribeRequest subscribeRequest = new SubscribeRequest();
			SubscribeRequest.DeserializeLengthDelimited(stream, subscribeRequest);
			return subscribeRequest;
		}

		// Token: 0x0600342C RID: 13356 RVA: 0x000AD608 File Offset: 0x000AB808
		public static SubscribeRequest DeserializeLengthDelimited(Stream stream, SubscribeRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubscribeRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x0600342D RID: 13357 RVA: 0x000AD630 File Offset: 0x000AB830
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
				else if (num == 10)
				{
					if (instance.Subscription == null)
					{
						instance.Subscription = Subscription.DeserializeLengthDelimited(stream);
					}
					else
					{
						Subscription.DeserializeLengthDelimited(stream, instance.Subscription);
					}
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

		// Token: 0x0600342E RID: 13358 RVA: 0x000AD6CA File Offset: 0x000AB8CA
		public void Serialize(Stream stream)
		{
			SubscribeRequest.Serialize(stream, this);
		}

		// Token: 0x0600342F RID: 13359 RVA: 0x000AD6D3 File Offset: 0x000AB8D3
		public static void Serialize(Stream stream, SubscribeRequest instance)
		{
			if (instance.HasSubscription)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Subscription.GetSerializedSize());
				Subscription.Serialize(stream, instance.Subscription);
			}
		}

		// Token: 0x06003430 RID: 13360 RVA: 0x000AD704 File Offset: 0x000AB904
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasSubscription)
			{
				num += 1U;
				uint serializedSize = this.Subscription.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x0400141C RID: 5148
		public bool HasSubscription;

		// Token: 0x0400141D RID: 5149
		private Subscription _Subscription;
	}
}
