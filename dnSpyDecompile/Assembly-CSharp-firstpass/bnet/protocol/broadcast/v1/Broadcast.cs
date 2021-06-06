using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.broadcast.v1
{
	// Token: 0x020004E8 RID: 1256
	public class Broadcast : IProtoBuf
	{
		// Token: 0x170010C2 RID: 4290
		// (get) Token: 0x060058DB RID: 22747 RVA: 0x0011010F File Offset: 0x0010E30F
		// (set) Token: 0x060058DC RID: 22748 RVA: 0x00110117 File Offset: 0x0010E317
		public string Name { get; set; }

		// Token: 0x060058DD RID: 22749 RVA: 0x00110120 File Offset: 0x0010E320
		public void SetName(string val)
		{
			this.Name = val;
		}

		// Token: 0x170010C3 RID: 4291
		// (get) Token: 0x060058DE RID: 22750 RVA: 0x00110129 File Offset: 0x0010E329
		// (set) Token: 0x060058DF RID: 22751 RVA: 0x00110131 File Offset: 0x0010E331
		public List<Attribute> PayloadAttribute
		{
			get
			{
				return this._PayloadAttribute;
			}
			set
			{
				this._PayloadAttribute = value;
			}
		}

		// Token: 0x170010C4 RID: 4292
		// (get) Token: 0x060058E0 RID: 22752 RVA: 0x00110129 File Offset: 0x0010E329
		public List<Attribute> PayloadAttributeList
		{
			get
			{
				return this._PayloadAttribute;
			}
		}

		// Token: 0x170010C5 RID: 4293
		// (get) Token: 0x060058E1 RID: 22753 RVA: 0x0011013A File Offset: 0x0010E33A
		public int PayloadAttributeCount
		{
			get
			{
				return this._PayloadAttribute.Count;
			}
		}

		// Token: 0x060058E2 RID: 22754 RVA: 0x00110147 File Offset: 0x0010E347
		public void AddPayloadAttribute(Attribute val)
		{
			this._PayloadAttribute.Add(val);
		}

		// Token: 0x060058E3 RID: 22755 RVA: 0x00110155 File Offset: 0x0010E355
		public void ClearPayloadAttribute()
		{
			this._PayloadAttribute.Clear();
		}

		// Token: 0x060058E4 RID: 22756 RVA: 0x00110162 File Offset: 0x0010E362
		public void SetPayloadAttribute(List<Attribute> val)
		{
			this.PayloadAttribute = val;
		}

		// Token: 0x170010C6 RID: 4294
		// (get) Token: 0x060058E5 RID: 22757 RVA: 0x0011016B File Offset: 0x0010E36B
		// (set) Token: 0x060058E6 RID: 22758 RVA: 0x00110173 File Offset: 0x0010E373
		public List<BroadcastFilter> Filter
		{
			get
			{
				return this._Filter;
			}
			set
			{
				this._Filter = value;
			}
		}

		// Token: 0x170010C7 RID: 4295
		// (get) Token: 0x060058E7 RID: 22759 RVA: 0x0011016B File Offset: 0x0010E36B
		public List<BroadcastFilter> FilterList
		{
			get
			{
				return this._Filter;
			}
		}

		// Token: 0x170010C8 RID: 4296
		// (get) Token: 0x060058E8 RID: 22760 RVA: 0x0011017C File Offset: 0x0010E37C
		public int FilterCount
		{
			get
			{
				return this._Filter.Count;
			}
		}

		// Token: 0x060058E9 RID: 22761 RVA: 0x00110189 File Offset: 0x0010E389
		public void AddFilter(BroadcastFilter val)
		{
			this._Filter.Add(val);
		}

		// Token: 0x060058EA RID: 22762 RVA: 0x00110197 File Offset: 0x0010E397
		public void ClearFilter()
		{
			this._Filter.Clear();
		}

		// Token: 0x060058EB RID: 22763 RVA: 0x001101A4 File Offset: 0x0010E3A4
		public void SetFilter(List<BroadcastFilter> val)
		{
			this.Filter = val;
		}

		// Token: 0x170010C9 RID: 4297
		// (get) Token: 0x060058EC RID: 22764 RVA: 0x001101AD File Offset: 0x0010E3AD
		// (set) Token: 0x060058ED RID: 22765 RVA: 0x001101B5 File Offset: 0x0010E3B5
		public bool SendOnce
		{
			get
			{
				return this._SendOnce;
			}
			set
			{
				this._SendOnce = value;
				this.HasSendOnce = true;
			}
		}

		// Token: 0x060058EE RID: 22766 RVA: 0x001101C5 File Offset: 0x0010E3C5
		public void SetSendOnce(bool val)
		{
			this.SendOnce = val;
		}

		// Token: 0x060058EF RID: 22767 RVA: 0x001101D0 File Offset: 0x0010E3D0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Name.GetHashCode();
			foreach (Attribute attribute in this.PayloadAttribute)
			{
				num ^= attribute.GetHashCode();
			}
			foreach (BroadcastFilter broadcastFilter in this.Filter)
			{
				num ^= broadcastFilter.GetHashCode();
			}
			if (this.HasSendOnce)
			{
				num ^= this.SendOnce.GetHashCode();
			}
			return num;
		}

		// Token: 0x060058F0 RID: 22768 RVA: 0x001102A0 File Offset: 0x0010E4A0
		public override bool Equals(object obj)
		{
			Broadcast broadcast = obj as Broadcast;
			if (broadcast == null)
			{
				return false;
			}
			if (!this.Name.Equals(broadcast.Name))
			{
				return false;
			}
			if (this.PayloadAttribute.Count != broadcast.PayloadAttribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.PayloadAttribute.Count; i++)
			{
				if (!this.PayloadAttribute[i].Equals(broadcast.PayloadAttribute[i]))
				{
					return false;
				}
			}
			if (this.Filter.Count != broadcast.Filter.Count)
			{
				return false;
			}
			for (int j = 0; j < this.Filter.Count; j++)
			{
				if (!this.Filter[j].Equals(broadcast.Filter[j]))
				{
					return false;
				}
			}
			return this.HasSendOnce == broadcast.HasSendOnce && (!this.HasSendOnce || this.SendOnce.Equals(broadcast.SendOnce));
		}

		// Token: 0x170010CA RID: 4298
		// (get) Token: 0x060058F1 RID: 22769 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060058F2 RID: 22770 RVA: 0x0011039F File Offset: 0x0010E59F
		public static Broadcast ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Broadcast>(bs, 0, -1);
		}

		// Token: 0x060058F3 RID: 22771 RVA: 0x001103A9 File Offset: 0x0010E5A9
		public void Deserialize(Stream stream)
		{
			Broadcast.Deserialize(stream, this);
		}

		// Token: 0x060058F4 RID: 22772 RVA: 0x001103B3 File Offset: 0x0010E5B3
		public static Broadcast Deserialize(Stream stream, Broadcast instance)
		{
			return Broadcast.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060058F5 RID: 22773 RVA: 0x001103C0 File Offset: 0x0010E5C0
		public static Broadcast DeserializeLengthDelimited(Stream stream)
		{
			Broadcast broadcast = new Broadcast();
			Broadcast.DeserializeLengthDelimited(stream, broadcast);
			return broadcast;
		}

		// Token: 0x060058F6 RID: 22774 RVA: 0x001103DC File Offset: 0x0010E5DC
		public static Broadcast DeserializeLengthDelimited(Stream stream, Broadcast instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Broadcast.Deserialize(stream, instance, num);
		}

		// Token: 0x060058F7 RID: 22775 RVA: 0x00110404 File Offset: 0x0010E604
		public static Broadcast Deserialize(Stream stream, Broadcast instance, long limit)
		{
			if (instance.PayloadAttribute == null)
			{
				instance.PayloadAttribute = new List<Attribute>();
			}
			if (instance.Filter == null)
			{
				instance.Filter = new List<BroadcastFilter>();
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
				else
				{
					if (num <= 18)
					{
						if (num == 10)
						{
							instance.Name = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 18)
						{
							instance.PayloadAttribute.Add(Attribute.DeserializeLengthDelimited(stream));
							continue;
						}
					}
					else
					{
						if (num == 26)
						{
							instance.Filter.Add(BroadcastFilter.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 32)
						{
							instance.SendOnce = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
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

		// Token: 0x060058F8 RID: 22776 RVA: 0x00110505 File Offset: 0x0010E705
		public void Serialize(Stream stream)
		{
			Broadcast.Serialize(stream, this);
		}

		// Token: 0x060058F9 RID: 22777 RVA: 0x00110510 File Offset: 0x0010E710
		public static void Serialize(Stream stream, Broadcast instance)
		{
			if (instance.Name == null)
			{
				throw new ArgumentNullException("Name", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			if (instance.PayloadAttribute.Count > 0)
			{
				foreach (Attribute attribute in instance.PayloadAttribute)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					Attribute.Serialize(stream, attribute);
				}
			}
			if (instance.Filter.Count > 0)
			{
				foreach (BroadcastFilter broadcastFilter in instance.Filter)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, broadcastFilter.GetSerializedSize());
					BroadcastFilter.Serialize(stream, broadcastFilter);
				}
			}
			if (instance.HasSendOnce)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.SendOnce);
			}
		}

		// Token: 0x060058FA RID: 22778 RVA: 0x0011063C File Offset: 0x0010E83C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (this.PayloadAttribute.Count > 0)
			{
				foreach (Attribute attribute in this.PayloadAttribute)
				{
					num += 1U;
					uint serializedSize = attribute.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.Filter.Count > 0)
			{
				foreach (BroadcastFilter broadcastFilter in this.Filter)
				{
					num += 1U;
					uint serializedSize2 = broadcastFilter.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.HasSendOnce)
			{
				num += 1U;
				num += 1U;
			}
			num += 1U;
			return num;
		}

		// Token: 0x04001BC9 RID: 7113
		private List<Attribute> _PayloadAttribute = new List<Attribute>();

		// Token: 0x04001BCA RID: 7114
		private List<BroadcastFilter> _Filter = new List<BroadcastFilter>();

		// Token: 0x04001BCB RID: 7115
		public bool HasSendOnce;

		// Token: 0x04001BCC RID: 7116
		private bool _SendOnce;
	}
}
