using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.notification.v1
{
	// Token: 0x0200034A RID: 842
	public class Subscription : IProtoBuf
	{
		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06003468 RID: 13416 RVA: 0x000ADF17 File Offset: 0x000AC117
		// (set) Token: 0x06003469 RID: 13417 RVA: 0x000ADF1F File Offset: 0x000AC11F
		public List<Target> Target
		{
			get
			{
				return this._Target;
			}
			set
			{
				this._Target = value;
			}
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x0600346A RID: 13418 RVA: 0x000ADF17 File Offset: 0x000AC117
		public List<Target> TargetList
		{
			get
			{
				return this._Target;
			}
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x0600346B RID: 13419 RVA: 0x000ADF28 File Offset: 0x000AC128
		public int TargetCount
		{
			get
			{
				return this._Target.Count;
			}
		}

		// Token: 0x0600346C RID: 13420 RVA: 0x000ADF35 File Offset: 0x000AC135
		public void AddTarget(Target val)
		{
			this._Target.Add(val);
		}

		// Token: 0x0600346D RID: 13421 RVA: 0x000ADF43 File Offset: 0x000AC143
		public void ClearTarget()
		{
			this._Target.Clear();
		}

		// Token: 0x0600346E RID: 13422 RVA: 0x000ADF50 File Offset: 0x000AC150
		public void SetTarget(List<Target> val)
		{
			this.Target = val;
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x0600346F RID: 13423 RVA: 0x000ADF59 File Offset: 0x000AC159
		// (set) Token: 0x06003470 RID: 13424 RVA: 0x000ADF61 File Offset: 0x000AC161
		public Identity Subscriber
		{
			get
			{
				return this._Subscriber;
			}
			set
			{
				this._Subscriber = value;
				this.HasSubscriber = (value != null);
			}
		}

		// Token: 0x06003471 RID: 13425 RVA: 0x000ADF74 File Offset: 0x000AC174
		public void SetSubscriber(Identity val)
		{
			this.Subscriber = val;
		}

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06003472 RID: 13426 RVA: 0x000ADF7D File Offset: 0x000AC17D
		// (set) Token: 0x06003473 RID: 13427 RVA: 0x000ADF85 File Offset: 0x000AC185
		public bool DeliveryRequired
		{
			get
			{
				return this._DeliveryRequired;
			}
			set
			{
				this._DeliveryRequired = value;
				this.HasDeliveryRequired = true;
			}
		}

		// Token: 0x06003474 RID: 13428 RVA: 0x000ADF95 File Offset: 0x000AC195
		public void SetDeliveryRequired(bool val)
		{
			this.DeliveryRequired = val;
		}

		// Token: 0x06003475 RID: 13429 RVA: 0x000ADFA0 File Offset: 0x000AC1A0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (Target target in this.Target)
			{
				num ^= target.GetHashCode();
			}
			if (this.HasSubscriber)
			{
				num ^= this.Subscriber.GetHashCode();
			}
			if (this.HasDeliveryRequired)
			{
				num ^= this.DeliveryRequired.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003476 RID: 13430 RVA: 0x000AE034 File Offset: 0x000AC234
		public override bool Equals(object obj)
		{
			Subscription subscription = obj as Subscription;
			if (subscription == null)
			{
				return false;
			}
			if (this.Target.Count != subscription.Target.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Target.Count; i++)
			{
				if (!this.Target[i].Equals(subscription.Target[i]))
				{
					return false;
				}
			}
			return this.HasSubscriber == subscription.HasSubscriber && (!this.HasSubscriber || this.Subscriber.Equals(subscription.Subscriber)) && this.HasDeliveryRequired == subscription.HasDeliveryRequired && (!this.HasDeliveryRequired || this.DeliveryRequired.Equals(subscription.DeliveryRequired));
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06003477 RID: 13431 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003478 RID: 13432 RVA: 0x000AE0F8 File Offset: 0x000AC2F8
		public static Subscription ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Subscription>(bs, 0, -1);
		}

		// Token: 0x06003479 RID: 13433 RVA: 0x000AE102 File Offset: 0x000AC302
		public void Deserialize(Stream stream)
		{
			Subscription.Deserialize(stream, this);
		}

		// Token: 0x0600347A RID: 13434 RVA: 0x000AE10C File Offset: 0x000AC30C
		public static Subscription Deserialize(Stream stream, Subscription instance)
		{
			return Subscription.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600347B RID: 13435 RVA: 0x000AE118 File Offset: 0x000AC318
		public static Subscription DeserializeLengthDelimited(Stream stream)
		{
			Subscription subscription = new Subscription();
			Subscription.DeserializeLengthDelimited(stream, subscription);
			return subscription;
		}

		// Token: 0x0600347C RID: 13436 RVA: 0x000AE134 File Offset: 0x000AC334
		public static Subscription DeserializeLengthDelimited(Stream stream, Subscription instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Subscription.Deserialize(stream, instance, num);
		}

		// Token: 0x0600347D RID: 13437 RVA: 0x000AE15C File Offset: 0x000AC35C
		public static Subscription Deserialize(Stream stream, Subscription instance, long limit)
		{
			if (instance.Target == null)
			{
				instance.Target = new List<Target>();
			}
			instance.DeliveryRequired = false;
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
				else if (num != 10)
				{
					if (num != 18)
					{
						if (num != 24)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else
						{
							instance.DeliveryRequired = ProtocolParser.ReadBool(stream);
						}
					}
					else if (instance.Subscriber == null)
					{
						instance.Subscriber = Identity.DeserializeLengthDelimited(stream);
					}
					else
					{
						Identity.DeserializeLengthDelimited(stream, instance.Subscriber);
					}
				}
				else
				{
					instance.Target.Add(bnet.protocol.notification.v1.Target.DeserializeLengthDelimited(stream));
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600347E RID: 13438 RVA: 0x000AE249 File Offset: 0x000AC449
		public void Serialize(Stream stream)
		{
			Subscription.Serialize(stream, this);
		}

		// Token: 0x0600347F RID: 13439 RVA: 0x000AE254 File Offset: 0x000AC454
		public static void Serialize(Stream stream, Subscription instance)
		{
			if (instance.Target.Count > 0)
			{
				foreach (Target target in instance.Target)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, target.GetSerializedSize());
					bnet.protocol.notification.v1.Target.Serialize(stream, target);
				}
			}
			if (instance.HasSubscriber)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Subscriber.GetSerializedSize());
				Identity.Serialize(stream, instance.Subscriber);
			}
			if (instance.HasDeliveryRequired)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.DeliveryRequired);
			}
		}

		// Token: 0x06003480 RID: 13440 RVA: 0x000AE314 File Offset: 0x000AC514
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Target.Count > 0)
			{
				foreach (Target target in this.Target)
				{
					num += 1U;
					uint serializedSize = target.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasSubscriber)
			{
				num += 1U;
				uint serializedSize2 = this.Subscriber.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasDeliveryRequired)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x04001428 RID: 5160
		private List<Target> _Target = new List<Target>();

		// Token: 0x04001429 RID: 5161
		public bool HasSubscriber;

		// Token: 0x0400142A RID: 5162
		private Identity _Subscriber;

		// Token: 0x0400142B RID: 5163
		public bool HasDeliveryRequired;

		// Token: 0x0400142C RID: 5164
		private bool _DeliveryRequired;
	}
}
