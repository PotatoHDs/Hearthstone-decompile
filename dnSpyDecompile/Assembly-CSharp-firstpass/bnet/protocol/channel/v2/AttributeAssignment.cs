using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.v2;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000473 RID: 1139
	public class AttributeAssignment : IProtoBuf
	{
		// Token: 0x17000EA7 RID: 3751
		// (get) Token: 0x06004E66 RID: 20070 RVA: 0x000F350B File Offset: 0x000F170B
		// (set) Token: 0x06004E67 RID: 20071 RVA: 0x000F3513 File Offset: 0x000F1713
		public GameAccountHandle MemberId
		{
			get
			{
				return this._MemberId;
			}
			set
			{
				this._MemberId = value;
				this.HasMemberId = (value != null);
			}
		}

		// Token: 0x06004E68 RID: 20072 RVA: 0x000F3526 File Offset: 0x000F1726
		public void SetMemberId(GameAccountHandle val)
		{
			this.MemberId = val;
		}

		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x06004E69 RID: 20073 RVA: 0x000F352F File Offset: 0x000F172F
		// (set) Token: 0x06004E6A RID: 20074 RVA: 0x000F3537 File Offset: 0x000F1737
		public List<bnet.protocol.v2.Attribute> Attribute
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

		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x06004E6B RID: 20075 RVA: 0x000F352F File Offset: 0x000F172F
		public List<bnet.protocol.v2.Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x06004E6C RID: 20076 RVA: 0x000F3540 File Offset: 0x000F1740
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x06004E6D RID: 20077 RVA: 0x000F354D File Offset: 0x000F174D
		public void AddAttribute(bnet.protocol.v2.Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x06004E6E RID: 20078 RVA: 0x000F355B File Offset: 0x000F175B
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x06004E6F RID: 20079 RVA: 0x000F3568 File Offset: 0x000F1768
		public void SetAttribute(List<bnet.protocol.v2.Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x06004E70 RID: 20080 RVA: 0x000F3574 File Offset: 0x000F1774
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasMemberId)
			{
				num ^= this.MemberId.GetHashCode();
			}
			foreach (bnet.protocol.v2.Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004E71 RID: 20081 RVA: 0x000F35EC File Offset: 0x000F17EC
		public override bool Equals(object obj)
		{
			AttributeAssignment attributeAssignment = obj as AttributeAssignment;
			if (attributeAssignment == null)
			{
				return false;
			}
			if (this.HasMemberId != attributeAssignment.HasMemberId || (this.HasMemberId && !this.MemberId.Equals(attributeAssignment.MemberId)))
			{
				return false;
			}
			if (this.Attribute.Count != attributeAssignment.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(attributeAssignment.Attribute[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x06004E72 RID: 20082 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004E73 RID: 20083 RVA: 0x000F3682 File Offset: 0x000F1882
		public static AttributeAssignment ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AttributeAssignment>(bs, 0, -1);
		}

		// Token: 0x06004E74 RID: 20084 RVA: 0x000F368C File Offset: 0x000F188C
		public void Deserialize(Stream stream)
		{
			AttributeAssignment.Deserialize(stream, this);
		}

		// Token: 0x06004E75 RID: 20085 RVA: 0x000F3696 File Offset: 0x000F1896
		public static AttributeAssignment Deserialize(Stream stream, AttributeAssignment instance)
		{
			return AttributeAssignment.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004E76 RID: 20086 RVA: 0x000F36A4 File Offset: 0x000F18A4
		public static AttributeAssignment DeserializeLengthDelimited(Stream stream)
		{
			AttributeAssignment attributeAssignment = new AttributeAssignment();
			AttributeAssignment.DeserializeLengthDelimited(stream, attributeAssignment);
			return attributeAssignment;
		}

		// Token: 0x06004E77 RID: 20087 RVA: 0x000F36C0 File Offset: 0x000F18C0
		public static AttributeAssignment DeserializeLengthDelimited(Stream stream, AttributeAssignment instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AttributeAssignment.Deserialize(stream, instance, num);
		}

		// Token: 0x06004E78 RID: 20088 RVA: 0x000F36E8 File Offset: 0x000F18E8
		public static AttributeAssignment Deserialize(Stream stream, AttributeAssignment instance, long limit)
		{
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<bnet.protocol.v2.Attribute>();
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
				else if (num != 10)
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
				else if (instance.MemberId == null)
				{
					instance.MemberId = GameAccountHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameAccountHandle.DeserializeLengthDelimited(stream, instance.MemberId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06004E79 RID: 20089 RVA: 0x000F37B2 File Offset: 0x000F19B2
		public void Serialize(Stream stream)
		{
			AttributeAssignment.Serialize(stream, this);
		}

		// Token: 0x06004E7A RID: 20090 RVA: 0x000F37BC File Offset: 0x000F19BC
		public static void Serialize(Stream stream, AttributeAssignment instance)
		{
			if (instance.HasMemberId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.MemberId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.MemberId);
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.v2.Attribute.Serialize(stream, attribute);
				}
			}
		}

		// Token: 0x06004E7B RID: 20091 RVA: 0x000F3860 File Offset: 0x000F1A60
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasMemberId)
			{
				num += 1U;
				uint serializedSize = this.MemberId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in this.Attribute)
				{
					num += 1U;
					uint serializedSize2 = attribute.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			return num;
		}

		// Token: 0x04001977 RID: 6519
		public bool HasMemberId;

		// Token: 0x04001978 RID: 6520
		private GameAccountHandle _MemberId;

		// Token: 0x04001979 RID: 6521
		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();
	}
}
