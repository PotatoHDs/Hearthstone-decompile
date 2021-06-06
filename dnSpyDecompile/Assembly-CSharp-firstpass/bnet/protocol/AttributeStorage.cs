using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol
{
	// Token: 0x020002B7 RID: 695
	public class AttributeStorage : IProtoBuf
	{
		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x060028DA RID: 10458 RVA: 0x000900E3 File Offset: 0x0008E2E3
		// (set) Token: 0x060028DB RID: 10459 RVA: 0x000900EB File Offset: 0x0008E2EB
		public uint Version
		{
			get
			{
				return this._Version;
			}
			set
			{
				this._Version = value;
				this.HasVersion = true;
			}
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x000900FB File Offset: 0x0008E2FB
		public void SetVersion(uint val)
		{
			this.Version = val;
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x060028DD RID: 10461 RVA: 0x00090104 File Offset: 0x0008E304
		// (set) Token: 0x060028DE RID: 10462 RVA: 0x0009010C File Offset: 0x0008E30C
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

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x060028DF RID: 10463 RVA: 0x00090104 File Offset: 0x0008E304
		public List<Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x060028E0 RID: 10464 RVA: 0x00090115 File Offset: 0x0008E315
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x060028E1 RID: 10465 RVA: 0x00090122 File Offset: 0x0008E322
		public void AddAttribute(Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x060028E2 RID: 10466 RVA: 0x00090130 File Offset: 0x0008E330
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x060028E3 RID: 10467 RVA: 0x0009013D File Offset: 0x0008E33D
		public void SetAttribute(List<Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x00090148 File Offset: 0x0008E348
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasVersion)
			{
				num ^= this.Version.GetHashCode();
			}
			foreach (Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			return num;
		}

		// Token: 0x060028E5 RID: 10469 RVA: 0x000901C4 File Offset: 0x0008E3C4
		public override bool Equals(object obj)
		{
			AttributeStorage attributeStorage = obj as AttributeStorage;
			if (attributeStorage == null)
			{
				return false;
			}
			if (this.HasVersion != attributeStorage.HasVersion || (this.HasVersion && !this.Version.Equals(attributeStorage.Version)))
			{
				return false;
			}
			if (this.Attribute.Count != attributeStorage.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(attributeStorage.Attribute[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x060028E6 RID: 10470 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x0009025D File Offset: 0x0008E45D
		public static AttributeStorage ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AttributeStorage>(bs, 0, -1);
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x00090267 File Offset: 0x0008E467
		public void Deserialize(Stream stream)
		{
			AttributeStorage.Deserialize(stream, this);
		}

		// Token: 0x060028E9 RID: 10473 RVA: 0x00090271 File Offset: 0x0008E471
		public static AttributeStorage Deserialize(Stream stream, AttributeStorage instance)
		{
			return AttributeStorage.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060028EA RID: 10474 RVA: 0x0009027C File Offset: 0x0008E47C
		public static AttributeStorage DeserializeLengthDelimited(Stream stream)
		{
			AttributeStorage attributeStorage = new AttributeStorage();
			AttributeStorage.DeserializeLengthDelimited(stream, attributeStorage);
			return attributeStorage;
		}

		// Token: 0x060028EB RID: 10475 RVA: 0x00090298 File Offset: 0x0008E498
		public static AttributeStorage DeserializeLengthDelimited(Stream stream, AttributeStorage instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AttributeStorage.Deserialize(stream, instance, num);
		}

		// Token: 0x060028EC RID: 10476 RVA: 0x000902C0 File Offset: 0x0008E4C0
		public static AttributeStorage Deserialize(Stream stream, AttributeStorage instance, long limit)
		{
			instance.Version = 0U;
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
					instance.Version = ProtocolParser.ReadUInt32(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060028ED RID: 10477 RVA: 0x00090376 File Offset: 0x0008E576
		public void Serialize(Stream stream)
		{
			AttributeStorage.Serialize(stream, this);
		}

		// Token: 0x060028EE RID: 10478 RVA: 0x00090380 File Offset: 0x0008E580
		public static void Serialize(Stream stream, AttributeStorage instance)
		{
			if (instance.HasVersion)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.Version);
			}
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

		// Token: 0x060028EF RID: 10479 RVA: 0x00090410 File Offset: 0x0008E610
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasVersion)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Version);
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

		// Token: 0x0400118B RID: 4491
		public bool HasVersion;

		// Token: 0x0400118C RID: 4492
		private uint _Version;

		// Token: 0x0400118D RID: 4493
		private List<Attribute> _Attribute = new List<Attribute>();
	}
}
