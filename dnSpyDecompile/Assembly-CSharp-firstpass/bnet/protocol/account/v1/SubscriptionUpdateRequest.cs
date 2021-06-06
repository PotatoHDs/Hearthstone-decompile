using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x02000509 RID: 1289
	public class SubscriptionUpdateRequest : IProtoBuf
	{
		// Token: 0x1700114F RID: 4431
		// (get) Token: 0x06005BC2 RID: 23490 RVA: 0x001174C1 File Offset: 0x001156C1
		// (set) Token: 0x06005BC3 RID: 23491 RVA: 0x001174C9 File Offset: 0x001156C9
		public List<SubscriberReference> Ref
		{
			get
			{
				return this._Ref;
			}
			set
			{
				this._Ref = value;
			}
		}

		// Token: 0x17001150 RID: 4432
		// (get) Token: 0x06005BC4 RID: 23492 RVA: 0x001174C1 File Offset: 0x001156C1
		public List<SubscriberReference> RefList
		{
			get
			{
				return this._Ref;
			}
		}

		// Token: 0x17001151 RID: 4433
		// (get) Token: 0x06005BC5 RID: 23493 RVA: 0x001174D2 File Offset: 0x001156D2
		public int RefCount
		{
			get
			{
				return this._Ref.Count;
			}
		}

		// Token: 0x06005BC6 RID: 23494 RVA: 0x001174DF File Offset: 0x001156DF
		public void AddRef(SubscriberReference val)
		{
			this._Ref.Add(val);
		}

		// Token: 0x06005BC7 RID: 23495 RVA: 0x001174ED File Offset: 0x001156ED
		public void ClearRef()
		{
			this._Ref.Clear();
		}

		// Token: 0x06005BC8 RID: 23496 RVA: 0x001174FA File Offset: 0x001156FA
		public void SetRef(List<SubscriberReference> val)
		{
			this.Ref = val;
		}

		// Token: 0x06005BC9 RID: 23497 RVA: 0x00117504 File Offset: 0x00115704
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (SubscriberReference subscriberReference in this.Ref)
			{
				num ^= subscriberReference.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005BCA RID: 23498 RVA: 0x00117568 File Offset: 0x00115768
		public override bool Equals(object obj)
		{
			SubscriptionUpdateRequest subscriptionUpdateRequest = obj as SubscriptionUpdateRequest;
			if (subscriptionUpdateRequest == null)
			{
				return false;
			}
			if (this.Ref.Count != subscriptionUpdateRequest.Ref.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Ref.Count; i++)
			{
				if (!this.Ref[i].Equals(subscriptionUpdateRequest.Ref[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17001152 RID: 4434
		// (get) Token: 0x06005BCB RID: 23499 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005BCC RID: 23500 RVA: 0x001175D3 File Offset: 0x001157D3
		public static SubscriptionUpdateRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscriptionUpdateRequest>(bs, 0, -1);
		}

		// Token: 0x06005BCD RID: 23501 RVA: 0x001175DD File Offset: 0x001157DD
		public void Deserialize(Stream stream)
		{
			SubscriptionUpdateRequest.Deserialize(stream, this);
		}

		// Token: 0x06005BCE RID: 23502 RVA: 0x001175E7 File Offset: 0x001157E7
		public static SubscriptionUpdateRequest Deserialize(Stream stream, SubscriptionUpdateRequest instance)
		{
			return SubscriptionUpdateRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005BCF RID: 23503 RVA: 0x001175F4 File Offset: 0x001157F4
		public static SubscriptionUpdateRequest DeserializeLengthDelimited(Stream stream)
		{
			SubscriptionUpdateRequest subscriptionUpdateRequest = new SubscriptionUpdateRequest();
			SubscriptionUpdateRequest.DeserializeLengthDelimited(stream, subscriptionUpdateRequest);
			return subscriptionUpdateRequest;
		}

		// Token: 0x06005BD0 RID: 23504 RVA: 0x00117610 File Offset: 0x00115810
		public static SubscriptionUpdateRequest DeserializeLengthDelimited(Stream stream, SubscriptionUpdateRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubscriptionUpdateRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06005BD1 RID: 23505 RVA: 0x00117638 File Offset: 0x00115838
		public static SubscriptionUpdateRequest Deserialize(Stream stream, SubscriptionUpdateRequest instance, long limit)
		{
			if (instance.Ref == null)
			{
				instance.Ref = new List<SubscriberReference>();
			}
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
				else if (num == 18)
				{
					instance.Ref.Add(SubscriberReference.DeserializeLengthDelimited(stream));
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

		// Token: 0x06005BD2 RID: 23506 RVA: 0x001176D0 File Offset: 0x001158D0
		public void Serialize(Stream stream)
		{
			SubscriptionUpdateRequest.Serialize(stream, this);
		}

		// Token: 0x06005BD3 RID: 23507 RVA: 0x001176DC File Offset: 0x001158DC
		public static void Serialize(Stream stream, SubscriptionUpdateRequest instance)
		{
			if (instance.Ref.Count > 0)
			{
				foreach (SubscriberReference subscriberReference in instance.Ref)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, subscriberReference.GetSerializedSize());
					SubscriberReference.Serialize(stream, subscriberReference);
				}
			}
		}

		// Token: 0x06005BD4 RID: 23508 RVA: 0x00117754 File Offset: 0x00115954
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Ref.Count > 0)
			{
				foreach (SubscriberReference subscriberReference in this.Ref)
				{
					num += 1U;
					uint serializedSize = subscriberReference.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x04001C77 RID: 7287
		private List<SubscriberReference> _Ref = new List<SubscriberReference>();
	}
}
