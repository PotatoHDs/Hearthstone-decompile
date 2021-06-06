using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.channel.v2.Types;
using bnet.protocol.v2;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000486 RID: 1158
	public class CreateChannelServerOptions : IProtoBuf
	{
		// Token: 0x17000F24 RID: 3876
		// (get) Token: 0x06005079 RID: 20601 RVA: 0x000F9B17 File Offset: 0x000F7D17
		// (set) Token: 0x0600507A RID: 20602 RVA: 0x000F9B1F File Offset: 0x000F7D1F
		public UniqueChannelType Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
				this.HasType = (value != null);
			}
		}

		// Token: 0x0600507B RID: 20603 RVA: 0x000F9B32 File Offset: 0x000F7D32
		public void SetType(UniqueChannelType val)
		{
			this.Type = val;
		}

		// Token: 0x17000F25 RID: 3877
		// (get) Token: 0x0600507C RID: 20604 RVA: 0x000F9B3B File Offset: 0x000F7D3B
		// (set) Token: 0x0600507D RID: 20605 RVA: 0x000F9B43 File Offset: 0x000F7D43
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				this._Name = value;
				this.HasName = (value != null);
			}
		}

		// Token: 0x0600507E RID: 20606 RVA: 0x000F9B56 File Offset: 0x000F7D56
		public void SetName(string val)
		{
			this.Name = val;
		}

		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x0600507F RID: 20607 RVA: 0x000F9B5F File Offset: 0x000F7D5F
		// (set) Token: 0x06005080 RID: 20608 RVA: 0x000F9B67 File Offset: 0x000F7D67
		public PrivacyLevel PrivacyLevel
		{
			get
			{
				return this._PrivacyLevel;
			}
			set
			{
				this._PrivacyLevel = value;
				this.HasPrivacyLevel = true;
			}
		}

		// Token: 0x06005081 RID: 20609 RVA: 0x000F9B77 File Offset: 0x000F7D77
		public void SetPrivacyLevel(PrivacyLevel val)
		{
			this.PrivacyLevel = val;
		}

		// Token: 0x17000F27 RID: 3879
		// (get) Token: 0x06005082 RID: 20610 RVA: 0x000F9B80 File Offset: 0x000F7D80
		// (set) Token: 0x06005083 RID: 20611 RVA: 0x000F9B88 File Offset: 0x000F7D88
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

		// Token: 0x17000F28 RID: 3880
		// (get) Token: 0x06005084 RID: 20612 RVA: 0x000F9B80 File Offset: 0x000F7D80
		public List<bnet.protocol.v2.Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x06005085 RID: 20613 RVA: 0x000F9B91 File Offset: 0x000F7D91
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x06005086 RID: 20614 RVA: 0x000F9B9E File Offset: 0x000F7D9E
		public void AddAttribute(bnet.protocol.v2.Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x06005087 RID: 20615 RVA: 0x000F9BAC File Offset: 0x000F7DAC
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x06005088 RID: 20616 RVA: 0x000F9BB9 File Offset: 0x000F7DB9
		public void SetAttribute(List<bnet.protocol.v2.Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x17000F2A RID: 3882
		// (get) Token: 0x06005089 RID: 20617 RVA: 0x000F9BC2 File Offset: 0x000F7DC2
		// (set) Token: 0x0600508A RID: 20618 RVA: 0x000F9BCA File Offset: 0x000F7DCA
		public List<CreateMemberOptions> Member
		{
			get
			{
				return this._Member;
			}
			set
			{
				this._Member = value;
			}
		}

		// Token: 0x17000F2B RID: 3883
		// (get) Token: 0x0600508B RID: 20619 RVA: 0x000F9BC2 File Offset: 0x000F7DC2
		public List<CreateMemberOptions> MemberList
		{
			get
			{
				return this._Member;
			}
		}

		// Token: 0x17000F2C RID: 3884
		// (get) Token: 0x0600508C RID: 20620 RVA: 0x000F9BD3 File Offset: 0x000F7DD3
		public int MemberCount
		{
			get
			{
				return this._Member.Count;
			}
		}

		// Token: 0x0600508D RID: 20621 RVA: 0x000F9BE0 File Offset: 0x000F7DE0
		public void AddMember(CreateMemberOptions val)
		{
			this._Member.Add(val);
		}

		// Token: 0x0600508E RID: 20622 RVA: 0x000F9BEE File Offset: 0x000F7DEE
		public void ClearMember()
		{
			this._Member.Clear();
		}

		// Token: 0x0600508F RID: 20623 RVA: 0x000F9BFB File Offset: 0x000F7DFB
		public void SetMember(List<CreateMemberOptions> val)
		{
			this.Member = val;
		}

		// Token: 0x17000F2D RID: 3885
		// (get) Token: 0x06005090 RID: 20624 RVA: 0x000F9C04 File Offset: 0x000F7E04
		// (set) Token: 0x06005091 RID: 20625 RVA: 0x000F9C0C File Offset: 0x000F7E0C
		public string CollectionId
		{
			get
			{
				return this._CollectionId;
			}
			set
			{
				this._CollectionId = value;
				this.HasCollectionId = (value != null);
			}
		}

		// Token: 0x06005092 RID: 20626 RVA: 0x000F9C1F File Offset: 0x000F7E1F
		public void SetCollectionId(string val)
		{
			this.CollectionId = val;
		}

		// Token: 0x06005093 RID: 20627 RVA: 0x000F9C28 File Offset: 0x000F7E28
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasType)
			{
				num ^= this.Type.GetHashCode();
			}
			if (this.HasName)
			{
				num ^= this.Name.GetHashCode();
			}
			if (this.HasPrivacyLevel)
			{
				num ^= this.PrivacyLevel.GetHashCode();
			}
			foreach (bnet.protocol.v2.Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			foreach (CreateMemberOptions createMemberOptions in this.Member)
			{
				num ^= createMemberOptions.GetHashCode();
			}
			if (this.HasCollectionId)
			{
				num ^= this.CollectionId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005094 RID: 20628 RVA: 0x000F9D34 File Offset: 0x000F7F34
		public override bool Equals(object obj)
		{
			CreateChannelServerOptions createChannelServerOptions = obj as CreateChannelServerOptions;
			if (createChannelServerOptions == null)
			{
				return false;
			}
			if (this.HasType != createChannelServerOptions.HasType || (this.HasType && !this.Type.Equals(createChannelServerOptions.Type)))
			{
				return false;
			}
			if (this.HasName != createChannelServerOptions.HasName || (this.HasName && !this.Name.Equals(createChannelServerOptions.Name)))
			{
				return false;
			}
			if (this.HasPrivacyLevel != createChannelServerOptions.HasPrivacyLevel || (this.HasPrivacyLevel && !this.PrivacyLevel.Equals(createChannelServerOptions.PrivacyLevel)))
			{
				return false;
			}
			if (this.Attribute.Count != createChannelServerOptions.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(createChannelServerOptions.Attribute[i]))
				{
					return false;
				}
			}
			if (this.Member.Count != createChannelServerOptions.Member.Count)
			{
				return false;
			}
			for (int j = 0; j < this.Member.Count; j++)
			{
				if (!this.Member[j].Equals(createChannelServerOptions.Member[j]))
				{
					return false;
				}
			}
			return this.HasCollectionId == createChannelServerOptions.HasCollectionId && (!this.HasCollectionId || this.CollectionId.Equals(createChannelServerOptions.CollectionId));
		}

		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x06005095 RID: 20629 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005096 RID: 20630 RVA: 0x000F9EAA File Offset: 0x000F80AA
		public static CreateChannelServerOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateChannelServerOptions>(bs, 0, -1);
		}

		// Token: 0x06005097 RID: 20631 RVA: 0x000F9EB4 File Offset: 0x000F80B4
		public void Deserialize(Stream stream)
		{
			CreateChannelServerOptions.Deserialize(stream, this);
		}

		// Token: 0x06005098 RID: 20632 RVA: 0x000F9EBE File Offset: 0x000F80BE
		public static CreateChannelServerOptions Deserialize(Stream stream, CreateChannelServerOptions instance)
		{
			return CreateChannelServerOptions.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005099 RID: 20633 RVA: 0x000F9ECC File Offset: 0x000F80CC
		public static CreateChannelServerOptions DeserializeLengthDelimited(Stream stream)
		{
			CreateChannelServerOptions createChannelServerOptions = new CreateChannelServerOptions();
			CreateChannelServerOptions.DeserializeLengthDelimited(stream, createChannelServerOptions);
			return createChannelServerOptions;
		}

		// Token: 0x0600509A RID: 20634 RVA: 0x000F9EE8 File Offset: 0x000F80E8
		public static CreateChannelServerOptions DeserializeLengthDelimited(Stream stream, CreateChannelServerOptions instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CreateChannelServerOptions.Deserialize(stream, instance, num);
		}

		// Token: 0x0600509B RID: 20635 RVA: 0x000F9F10 File Offset: 0x000F8110
		public static CreateChannelServerOptions Deserialize(Stream stream, CreateChannelServerOptions instance, long limit)
		{
			instance.PrivacyLevel = PrivacyLevel.PRIVACY_LEVEL_OPEN;
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<bnet.protocol.v2.Attribute>();
			}
			if (instance.Member == null)
			{
				instance.Member = new List<CreateMemberOptions>();
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
					if (num <= 24)
					{
						if (num != 10)
						{
							if (num == 18)
							{
								instance.Name = ProtocolParser.ReadString(stream);
								continue;
							}
							if (num == 24)
							{
								instance.PrivacyLevel = (PrivacyLevel)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else
						{
							if (instance.Type == null)
							{
								instance.Type = UniqueChannelType.DeserializeLengthDelimited(stream);
								continue;
							}
							UniqueChannelType.DeserializeLengthDelimited(stream, instance.Type);
							continue;
						}
					}
					else
					{
						if (num == 34)
						{
							instance.Attribute.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 42)
						{
							instance.Member.Add(CreateMemberOptions.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 50)
						{
							instance.CollectionId = ProtocolParser.ReadString(stream);
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

		// Token: 0x0600509C RID: 20636 RVA: 0x000FA06B File Offset: 0x000F826B
		public void Serialize(Stream stream)
		{
			CreateChannelServerOptions.Serialize(stream, this);
		}

		// Token: 0x0600509D RID: 20637 RVA: 0x000FA074 File Offset: 0x000F8274
		public static void Serialize(Stream stream, CreateChannelServerOptions instance)
		{
			if (instance.HasType)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Type.GetSerializedSize());
				UniqueChannelType.Serialize(stream, instance.Type);
			}
			if (instance.HasName)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			}
			if (instance.HasPrivacyLevel)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.PrivacyLevel));
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.v2.Attribute.Serialize(stream, attribute);
				}
			}
			if (instance.Member.Count > 0)
			{
				foreach (CreateMemberOptions createMemberOptions in instance.Member)
				{
					stream.WriteByte(42);
					ProtocolParser.WriteUInt32(stream, createMemberOptions.GetSerializedSize());
					CreateMemberOptions.Serialize(stream, createMemberOptions);
				}
			}
			if (instance.HasCollectionId)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.CollectionId));
			}
		}

		// Token: 0x0600509E RID: 20638 RVA: 0x000FA1E4 File Offset: 0x000F83E4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasType)
			{
				num += 1U;
				uint serializedSize = this.Type.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasName)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasPrivacyLevel)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.PrivacyLevel));
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
			if (this.Member.Count > 0)
			{
				foreach (CreateMemberOptions createMemberOptions in this.Member)
				{
					num += 1U;
					uint serializedSize3 = createMemberOptions.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			if (this.HasCollectionId)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.CollectionId);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}

		// Token: 0x040019F6 RID: 6646
		public bool HasType;

		// Token: 0x040019F7 RID: 6647
		private UniqueChannelType _Type;

		// Token: 0x040019F8 RID: 6648
		public bool HasName;

		// Token: 0x040019F9 RID: 6649
		private string _Name;

		// Token: 0x040019FA RID: 6650
		public bool HasPrivacyLevel;

		// Token: 0x040019FB RID: 6651
		private PrivacyLevel _PrivacyLevel;

		// Token: 0x040019FC RID: 6652
		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();

		// Token: 0x040019FD RID: 6653
		private List<CreateMemberOptions> _Member = new List<CreateMemberOptions>();

		// Token: 0x040019FE RID: 6654
		public bool HasCollectionId;

		// Token: 0x040019FF RID: 6655
		private string _CollectionId;
	}
}
