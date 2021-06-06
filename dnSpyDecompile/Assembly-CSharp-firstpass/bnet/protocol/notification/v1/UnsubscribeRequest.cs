using System;
using System.IO;

namespace bnet.protocol.notification.v1
{
	// Token: 0x02000347 RID: 839
	public class UnsubscribeRequest : IProtoBuf
	{
		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06003432 RID: 13362 RVA: 0x000AD737 File Offset: 0x000AB937
		// (set) Token: 0x06003433 RID: 13363 RVA: 0x000AD73F File Offset: 0x000AB93F
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

		// Token: 0x06003434 RID: 13364 RVA: 0x000AD752 File Offset: 0x000AB952
		public void SetSubscription(Subscription val)
		{
			this.Subscription = val;
		}

		// Token: 0x06003435 RID: 13365 RVA: 0x000AD75C File Offset: 0x000AB95C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasSubscription)
			{
				num ^= this.Subscription.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003436 RID: 13366 RVA: 0x000AD78C File Offset: 0x000AB98C
		public override bool Equals(object obj)
		{
			UnsubscribeRequest unsubscribeRequest = obj as UnsubscribeRequest;
			return unsubscribeRequest != null && this.HasSubscription == unsubscribeRequest.HasSubscription && (!this.HasSubscription || this.Subscription.Equals(unsubscribeRequest.Subscription));
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06003437 RID: 13367 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003438 RID: 13368 RVA: 0x000AD7D1 File Offset: 0x000AB9D1
		public static UnsubscribeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UnsubscribeRequest>(bs, 0, -1);
		}

		// Token: 0x06003439 RID: 13369 RVA: 0x000AD7DB File Offset: 0x000AB9DB
		public void Deserialize(Stream stream)
		{
			UnsubscribeRequest.Deserialize(stream, this);
		}

		// Token: 0x0600343A RID: 13370 RVA: 0x000AD7E5 File Offset: 0x000AB9E5
		public static UnsubscribeRequest Deserialize(Stream stream, UnsubscribeRequest instance)
		{
			return UnsubscribeRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600343B RID: 13371 RVA: 0x000AD7F0 File Offset: 0x000AB9F0
		public static UnsubscribeRequest DeserializeLengthDelimited(Stream stream)
		{
			UnsubscribeRequest unsubscribeRequest = new UnsubscribeRequest();
			UnsubscribeRequest.DeserializeLengthDelimited(stream, unsubscribeRequest);
			return unsubscribeRequest;
		}

		// Token: 0x0600343C RID: 13372 RVA: 0x000AD80C File Offset: 0x000ABA0C
		public static UnsubscribeRequest DeserializeLengthDelimited(Stream stream, UnsubscribeRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UnsubscribeRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x0600343D RID: 13373 RVA: 0x000AD834 File Offset: 0x000ABA34
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

		// Token: 0x0600343E RID: 13374 RVA: 0x000AD8CE File Offset: 0x000ABACE
		public void Serialize(Stream stream)
		{
			UnsubscribeRequest.Serialize(stream, this);
		}

		// Token: 0x0600343F RID: 13375 RVA: 0x000AD8D7 File Offset: 0x000ABAD7
		public static void Serialize(Stream stream, UnsubscribeRequest instance)
		{
			if (instance.HasSubscription)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Subscription.GetSerializedSize());
				Subscription.Serialize(stream, instance.Subscription);
			}
		}

		// Token: 0x06003440 RID: 13376 RVA: 0x000AD908 File Offset: 0x000ABB08
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

		// Token: 0x0400141E RID: 5150
		public bool HasSubscription;

		// Token: 0x0400141F RID: 5151
		private Subscription _Subscription;
	}
}
