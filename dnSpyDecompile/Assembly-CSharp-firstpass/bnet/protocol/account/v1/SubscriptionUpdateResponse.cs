using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x0200050A RID: 1290
	public class SubscriptionUpdateResponse : IProtoBuf
	{
		// Token: 0x17001153 RID: 4435
		// (get) Token: 0x06005BD6 RID: 23510 RVA: 0x001177DB File Offset: 0x001159DB
		// (set) Token: 0x06005BD7 RID: 23511 RVA: 0x001177E3 File Offset: 0x001159E3
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

		// Token: 0x17001154 RID: 4436
		// (get) Token: 0x06005BD8 RID: 23512 RVA: 0x001177DB File Offset: 0x001159DB
		public List<SubscriberReference> RefList
		{
			get
			{
				return this._Ref;
			}
		}

		// Token: 0x17001155 RID: 4437
		// (get) Token: 0x06005BD9 RID: 23513 RVA: 0x001177EC File Offset: 0x001159EC
		public int RefCount
		{
			get
			{
				return this._Ref.Count;
			}
		}

		// Token: 0x06005BDA RID: 23514 RVA: 0x001177F9 File Offset: 0x001159F9
		public void AddRef(SubscriberReference val)
		{
			this._Ref.Add(val);
		}

		// Token: 0x06005BDB RID: 23515 RVA: 0x00117807 File Offset: 0x00115A07
		public void ClearRef()
		{
			this._Ref.Clear();
		}

		// Token: 0x06005BDC RID: 23516 RVA: 0x00117814 File Offset: 0x00115A14
		public void SetRef(List<SubscriberReference> val)
		{
			this.Ref = val;
		}

		// Token: 0x06005BDD RID: 23517 RVA: 0x00117820 File Offset: 0x00115A20
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (SubscriberReference subscriberReference in this.Ref)
			{
				num ^= subscriberReference.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005BDE RID: 23518 RVA: 0x00117884 File Offset: 0x00115A84
		public override bool Equals(object obj)
		{
			SubscriptionUpdateResponse subscriptionUpdateResponse = obj as SubscriptionUpdateResponse;
			if (subscriptionUpdateResponse == null)
			{
				return false;
			}
			if (this.Ref.Count != subscriptionUpdateResponse.Ref.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Ref.Count; i++)
			{
				if (!this.Ref[i].Equals(subscriptionUpdateResponse.Ref[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17001156 RID: 4438
		// (get) Token: 0x06005BDF RID: 23519 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005BE0 RID: 23520 RVA: 0x001178EF File Offset: 0x00115AEF
		public static SubscriptionUpdateResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscriptionUpdateResponse>(bs, 0, -1);
		}

		// Token: 0x06005BE1 RID: 23521 RVA: 0x001178F9 File Offset: 0x00115AF9
		public void Deserialize(Stream stream)
		{
			SubscriptionUpdateResponse.Deserialize(stream, this);
		}

		// Token: 0x06005BE2 RID: 23522 RVA: 0x00117903 File Offset: 0x00115B03
		public static SubscriptionUpdateResponse Deserialize(Stream stream, SubscriptionUpdateResponse instance)
		{
			return SubscriptionUpdateResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005BE3 RID: 23523 RVA: 0x00117910 File Offset: 0x00115B10
		public static SubscriptionUpdateResponse DeserializeLengthDelimited(Stream stream)
		{
			SubscriptionUpdateResponse subscriptionUpdateResponse = new SubscriptionUpdateResponse();
			SubscriptionUpdateResponse.DeserializeLengthDelimited(stream, subscriptionUpdateResponse);
			return subscriptionUpdateResponse;
		}

		// Token: 0x06005BE4 RID: 23524 RVA: 0x0011792C File Offset: 0x00115B2C
		public static SubscriptionUpdateResponse DeserializeLengthDelimited(Stream stream, SubscriptionUpdateResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubscriptionUpdateResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06005BE5 RID: 23525 RVA: 0x00117954 File Offset: 0x00115B54
		public static SubscriptionUpdateResponse Deserialize(Stream stream, SubscriptionUpdateResponse instance, long limit)
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
				else if (num == 10)
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

		// Token: 0x06005BE6 RID: 23526 RVA: 0x001179EC File Offset: 0x00115BEC
		public void Serialize(Stream stream)
		{
			SubscriptionUpdateResponse.Serialize(stream, this);
		}

		// Token: 0x06005BE7 RID: 23527 RVA: 0x001179F8 File Offset: 0x00115BF8
		public static void Serialize(Stream stream, SubscriptionUpdateResponse instance)
		{
			if (instance.Ref.Count > 0)
			{
				foreach (SubscriberReference subscriberReference in instance.Ref)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, subscriberReference.GetSerializedSize());
					SubscriberReference.Serialize(stream, subscriberReference);
				}
			}
		}

		// Token: 0x06005BE8 RID: 23528 RVA: 0x00117A70 File Offset: 0x00115C70
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

		// Token: 0x04001C78 RID: 7288
		private List<SubscriberReference> _Ref = new List<SubscriberReference>();
	}
}
