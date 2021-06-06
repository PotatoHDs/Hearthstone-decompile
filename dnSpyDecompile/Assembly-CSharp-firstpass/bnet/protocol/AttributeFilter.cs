using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol
{
	// Token: 0x0200029F RID: 671
	public class AttributeFilter : IProtoBuf
	{
		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x0600264E RID: 9806 RVA: 0x00088CFB File Offset: 0x00086EFB
		// (set) Token: 0x0600264F RID: 9807 RVA: 0x00088D03 File Offset: 0x00086F03
		public AttributeFilter.Types.Operation Op { get; set; }

		// Token: 0x06002650 RID: 9808 RVA: 0x00088D0C File Offset: 0x00086F0C
		public void SetOp(AttributeFilter.Types.Operation val)
		{
			this.Op = val;
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06002651 RID: 9809 RVA: 0x00088D15 File Offset: 0x00086F15
		// (set) Token: 0x06002652 RID: 9810 RVA: 0x00088D1D File Offset: 0x00086F1D
		public List<Attribute> Attribute
		{
			get
			{
				return this._Attribute;
			}
			set
			{
				this._Attribute = value;
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06002653 RID: 9811 RVA: 0x00088D15 File Offset: 0x00086F15
		public List<Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06002654 RID: 9812 RVA: 0x00088D26 File Offset: 0x00086F26
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x06002655 RID: 9813 RVA: 0x00088D33 File Offset: 0x00086F33
		public void AddAttribute(Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x06002656 RID: 9814 RVA: 0x00088D41 File Offset: 0x00086F41
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x00088D4E File Offset: 0x00086F4E
		public void SetAttribute(List<Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x00088D58 File Offset: 0x00086F58
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Op.GetHashCode();
			foreach (Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002659 RID: 9817 RVA: 0x00088DD4 File Offset: 0x00086FD4
		public override bool Equals(object obj)
		{
			AttributeFilter attributeFilter = obj as AttributeFilter;
			if (attributeFilter == null)
			{
				return false;
			}
			if (!this.Op.Equals(attributeFilter.Op))
			{
				return false;
			}
			if (this.Attribute.Count != attributeFilter.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(attributeFilter.Attribute[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x0600265A RID: 9818 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600265B RID: 9819 RVA: 0x00088E62 File Offset: 0x00087062
		public static AttributeFilter ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AttributeFilter>(bs, 0, -1);
		}

		// Token: 0x0600265C RID: 9820 RVA: 0x00088E6C File Offset: 0x0008706C
		public void Deserialize(Stream stream)
		{
			AttributeFilter.Deserialize(stream, this);
		}

		// Token: 0x0600265D RID: 9821 RVA: 0x00088E76 File Offset: 0x00087076
		public static AttributeFilter Deserialize(Stream stream, AttributeFilter instance)
		{
			return AttributeFilter.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600265E RID: 9822 RVA: 0x00088E84 File Offset: 0x00087084
		public static AttributeFilter DeserializeLengthDelimited(Stream stream)
		{
			AttributeFilter attributeFilter = new AttributeFilter();
			AttributeFilter.DeserializeLengthDelimited(stream, attributeFilter);
			return attributeFilter;
		}

		// Token: 0x0600265F RID: 9823 RVA: 0x00088EA0 File Offset: 0x000870A0
		public static AttributeFilter DeserializeLengthDelimited(Stream stream, AttributeFilter instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AttributeFilter.Deserialize(stream, instance, num);
		}

		// Token: 0x06002660 RID: 9824 RVA: 0x00088EC8 File Offset: 0x000870C8
		public static AttributeFilter Deserialize(Stream stream, AttributeFilter instance, long limit)
		{
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<Attribute>();
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
				else if (num != 8)
				{
					if (num != 18)
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
						instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
					}
				}
				else
				{
					instance.Op = (AttributeFilter.Types.Operation)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06002661 RID: 9825 RVA: 0x00088F78 File Offset: 0x00087178
		public void Serialize(Stream stream)
		{
			AttributeFilter.Serialize(stream, this);
		}

		// Token: 0x06002662 RID: 9826 RVA: 0x00088F84 File Offset: 0x00087184
		public static void Serialize(Stream stream, AttributeFilter instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Op));
			if (instance.Attribute.Count > 0)
			{
				foreach (Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.Attribute.Serialize(stream, attribute);
				}
			}
		}

		// Token: 0x06002663 RID: 9827 RVA: 0x00089010 File Offset: 0x00087210
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Op));
			if (this.Attribute.Count > 0)
			{
				foreach (Attribute attribute in this.Attribute)
				{
					num += 1U;
					uint serializedSize = attribute.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			num += 1U;
			return num;
		}

		// Token: 0x040010EE RID: 4334
		private List<Attribute> _Attribute = new List<Attribute>();

		// Token: 0x020006F8 RID: 1784
		public static class Types
		{
			// Token: 0x0200070F RID: 1807
			public enum Operation
			{
				// Token: 0x040022EC RID: 8940
				MATCH_NONE,
				// Token: 0x040022ED RID: 8941
				MATCH_ANY,
				// Token: 0x040022EE RID: 8942
				MATCH_ALL,
				// Token: 0x040022EF RID: 8943
				MATCH_ALL_MOST_SPECIFIC
			}
		}
	}
}
