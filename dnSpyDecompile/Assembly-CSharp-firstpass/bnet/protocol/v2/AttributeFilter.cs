using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.v2
{
	// Token: 0x020002CF RID: 719
	public class AttributeFilter : IProtoBuf
	{
		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06002A2B RID: 10795 RVA: 0x00093313 File Offset: 0x00091513
		// (set) Token: 0x06002A2C RID: 10796 RVA: 0x0009331B File Offset: 0x0009151B
		public AttributeFilter.Types.Operation Op
		{
			get
			{
				return this._Op;
			}
			set
			{
				this._Op = value;
				this.HasOp = true;
			}
		}

		// Token: 0x06002A2D RID: 10797 RVA: 0x0009332B File Offset: 0x0009152B
		public void SetOp(AttributeFilter.Types.Operation val)
		{
			this.Op = val;
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06002A2E RID: 10798 RVA: 0x00093334 File Offset: 0x00091534
		// (set) Token: 0x06002A2F RID: 10799 RVA: 0x0009333C File Offset: 0x0009153C
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

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06002A30 RID: 10800 RVA: 0x00093334 File Offset: 0x00091534
		public List<Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06002A31 RID: 10801 RVA: 0x00093345 File Offset: 0x00091545
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x06002A32 RID: 10802 RVA: 0x00093352 File Offset: 0x00091552
		public void AddAttribute(Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x06002A33 RID: 10803 RVA: 0x00093360 File Offset: 0x00091560
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x06002A34 RID: 10804 RVA: 0x0009336D File Offset: 0x0009156D
		public void SetAttribute(List<Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x06002A35 RID: 10805 RVA: 0x00093378 File Offset: 0x00091578
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasOp)
			{
				num ^= this.Op.GetHashCode();
			}
			foreach (Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002A36 RID: 10806 RVA: 0x000933FC File Offset: 0x000915FC
		public override bool Equals(object obj)
		{
			AttributeFilter attributeFilter = obj as AttributeFilter;
			if (attributeFilter == null)
			{
				return false;
			}
			if (this.HasOp != attributeFilter.HasOp || (this.HasOp && !this.Op.Equals(attributeFilter.Op)))
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

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06002A37 RID: 10807 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002A38 RID: 10808 RVA: 0x000934A0 File Offset: 0x000916A0
		public static AttributeFilter ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AttributeFilter>(bs, 0, -1);
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x000934AA File Offset: 0x000916AA
		public void Deserialize(Stream stream)
		{
			AttributeFilter.Deserialize(stream, this);
		}

		// Token: 0x06002A3A RID: 10810 RVA: 0x000934B4 File Offset: 0x000916B4
		public static AttributeFilter Deserialize(Stream stream, AttributeFilter instance)
		{
			return AttributeFilter.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002A3B RID: 10811 RVA: 0x000934C0 File Offset: 0x000916C0
		public static AttributeFilter DeserializeLengthDelimited(Stream stream)
		{
			AttributeFilter attributeFilter = new AttributeFilter();
			AttributeFilter.DeserializeLengthDelimited(stream, attributeFilter);
			return attributeFilter;
		}

		// Token: 0x06002A3C RID: 10812 RVA: 0x000934DC File Offset: 0x000916DC
		public static AttributeFilter DeserializeLengthDelimited(Stream stream, AttributeFilter instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AttributeFilter.Deserialize(stream, instance, num);
		}

		// Token: 0x06002A3D RID: 10813 RVA: 0x00093504 File Offset: 0x00091704
		public static AttributeFilter Deserialize(Stream stream, AttributeFilter instance, long limit)
		{
			instance.Op = AttributeFilter.Types.Operation.MATCH_NONE;
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
						instance.Attribute.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
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

		// Token: 0x06002A3E RID: 10814 RVA: 0x000935BB File Offset: 0x000917BB
		public void Serialize(Stream stream)
		{
			AttributeFilter.Serialize(stream, this);
		}

		// Token: 0x06002A3F RID: 10815 RVA: 0x000935C4 File Offset: 0x000917C4
		public static void Serialize(Stream stream, AttributeFilter instance)
		{
			if (instance.HasOp)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Op));
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.v2.Attribute.Serialize(stream, attribute);
				}
			}
		}

		// Token: 0x06002A40 RID: 10816 RVA: 0x00093658 File Offset: 0x00091858
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasOp)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Op));
			}
			if (this.Attribute.Count > 0)
			{
				foreach (Attribute attribute in this.Attribute)
				{
					num += 1U;
					uint serializedSize = attribute.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x040011EF RID: 4591
		public bool HasOp;

		// Token: 0x040011F0 RID: 4592
		private AttributeFilter.Types.Operation _Op;

		// Token: 0x040011F1 RID: 4593
		private List<Attribute> _Attribute = new List<Attribute>();

		// Token: 0x020006F9 RID: 1785
		public static class Types
		{
			// Token: 0x02000710 RID: 1808
			public enum Operation
			{
				// Token: 0x040022F1 RID: 8945
				MATCH_NONE,
				// Token: 0x040022F2 RID: 8946
				MATCH_ANY,
				// Token: 0x040022F3 RID: 8947
				MATCH_ALL,
				// Token: 0x040022F4 RID: 8948
				MATCH_ALL_MOST_SPECIFIC
			}
		}
	}
}
