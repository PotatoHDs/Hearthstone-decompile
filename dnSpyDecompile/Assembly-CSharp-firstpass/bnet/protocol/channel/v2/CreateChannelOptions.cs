using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.channel.v2.Types;
using bnet.protocol.v2;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000476 RID: 1142
	public class CreateChannelOptions : IProtoBuf
	{
		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x06004EB8 RID: 20152 RVA: 0x000F44FE File Offset: 0x000F26FE
		// (set) Token: 0x06004EB9 RID: 20153 RVA: 0x000F4506 File Offset: 0x000F2706
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

		// Token: 0x06004EBA RID: 20154 RVA: 0x000F4519 File Offset: 0x000F2719
		public void SetType(UniqueChannelType val)
		{
			this.Type = val;
		}

		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x06004EBB RID: 20155 RVA: 0x000F4522 File Offset: 0x000F2722
		// (set) Token: 0x06004EBC RID: 20156 RVA: 0x000F452A File Offset: 0x000F272A
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

		// Token: 0x06004EBD RID: 20157 RVA: 0x000F453D File Offset: 0x000F273D
		public void SetName(string val)
		{
			this.Name = val;
		}

		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x06004EBE RID: 20158 RVA: 0x000F4546 File Offset: 0x000F2746
		// (set) Token: 0x06004EBF RID: 20159 RVA: 0x000F454E File Offset: 0x000F274E
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

		// Token: 0x06004EC0 RID: 20160 RVA: 0x000F455E File Offset: 0x000F275E
		public void SetPrivacyLevel(PrivacyLevel val)
		{
			this.PrivacyLevel = val;
		}

		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x06004EC1 RID: 20161 RVA: 0x000F4567 File Offset: 0x000F2767
		// (set) Token: 0x06004EC2 RID: 20162 RVA: 0x000F456F File Offset: 0x000F276F
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

		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x06004EC3 RID: 20163 RVA: 0x000F4567 File Offset: 0x000F2767
		public List<bnet.protocol.v2.Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x06004EC4 RID: 20164 RVA: 0x000F4578 File Offset: 0x000F2778
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x06004EC5 RID: 20165 RVA: 0x000F4585 File Offset: 0x000F2785
		public void AddAttribute(bnet.protocol.v2.Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x06004EC6 RID: 20166 RVA: 0x000F4593 File Offset: 0x000F2793
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x06004EC7 RID: 20167 RVA: 0x000F45A0 File Offset: 0x000F27A0
		public void SetAttribute(List<bnet.protocol.v2.Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x17000EC1 RID: 3777
		// (get) Token: 0x06004EC8 RID: 20168 RVA: 0x000F45A9 File Offset: 0x000F27A9
		// (set) Token: 0x06004EC9 RID: 20169 RVA: 0x000F45B1 File Offset: 0x000F27B1
		public CreateMemberOptions Member
		{
			get
			{
				return this._Member;
			}
			set
			{
				this._Member = value;
				this.HasMember = (value != null);
			}
		}

		// Token: 0x06004ECA RID: 20170 RVA: 0x000F45C4 File Offset: 0x000F27C4
		public void SetMember(CreateMemberOptions val)
		{
			this.Member = val;
		}

		// Token: 0x06004ECB RID: 20171 RVA: 0x000F45D0 File Offset: 0x000F27D0
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
			if (this.HasMember)
			{
				num ^= this.Member.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004ECC RID: 20172 RVA: 0x000F4694 File Offset: 0x000F2894
		public override bool Equals(object obj)
		{
			CreateChannelOptions createChannelOptions = obj as CreateChannelOptions;
			if (createChannelOptions == null)
			{
				return false;
			}
			if (this.HasType != createChannelOptions.HasType || (this.HasType && !this.Type.Equals(createChannelOptions.Type)))
			{
				return false;
			}
			if (this.HasName != createChannelOptions.HasName || (this.HasName && !this.Name.Equals(createChannelOptions.Name)))
			{
				return false;
			}
			if (this.HasPrivacyLevel != createChannelOptions.HasPrivacyLevel || (this.HasPrivacyLevel && !this.PrivacyLevel.Equals(createChannelOptions.PrivacyLevel)))
			{
				return false;
			}
			if (this.Attribute.Count != createChannelOptions.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(createChannelOptions.Attribute[i]))
				{
					return false;
				}
			}
			return this.HasMember == createChannelOptions.HasMember && (!this.HasMember || this.Member.Equals(createChannelOptions.Member));
		}

		// Token: 0x17000EC2 RID: 3778
		// (get) Token: 0x06004ECD RID: 20173 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004ECE RID: 20174 RVA: 0x000F47B9 File Offset: 0x000F29B9
		public static CreateChannelOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateChannelOptions>(bs, 0, -1);
		}

		// Token: 0x06004ECF RID: 20175 RVA: 0x000F47C3 File Offset: 0x000F29C3
		public void Deserialize(Stream stream)
		{
			CreateChannelOptions.Deserialize(stream, this);
		}

		// Token: 0x06004ED0 RID: 20176 RVA: 0x000F47CD File Offset: 0x000F29CD
		public static CreateChannelOptions Deserialize(Stream stream, CreateChannelOptions instance)
		{
			return CreateChannelOptions.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004ED1 RID: 20177 RVA: 0x000F47D8 File Offset: 0x000F29D8
		public static CreateChannelOptions DeserializeLengthDelimited(Stream stream)
		{
			CreateChannelOptions createChannelOptions = new CreateChannelOptions();
			CreateChannelOptions.DeserializeLengthDelimited(stream, createChannelOptions);
			return createChannelOptions;
		}

		// Token: 0x06004ED2 RID: 20178 RVA: 0x000F47F4 File Offset: 0x000F29F4
		public static CreateChannelOptions DeserializeLengthDelimited(Stream stream, CreateChannelOptions instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CreateChannelOptions.Deserialize(stream, instance, num);
		}

		// Token: 0x06004ED3 RID: 20179 RVA: 0x000F481C File Offset: 0x000F2A1C
		public static CreateChannelOptions Deserialize(Stream stream, CreateChannelOptions instance, long limit)
		{
			instance.PrivacyLevel = PrivacyLevel.PRIVACY_LEVEL_OPEN;
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
				else
				{
					if (num <= 18)
					{
						if (num != 10)
						{
							if (num == 18)
							{
								instance.Name = ProtocolParser.ReadString(stream);
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
						if (num == 24)
						{
							instance.PrivacyLevel = (PrivacyLevel)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 34)
						{
							instance.Attribute.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 42)
						{
							if (instance.Member == null)
							{
								instance.Member = CreateMemberOptions.DeserializeLengthDelimited(stream);
								continue;
							}
							CreateMemberOptions.DeserializeLengthDelimited(stream, instance.Member);
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

		// Token: 0x06004ED4 RID: 20180 RVA: 0x000F4960 File Offset: 0x000F2B60
		public void Serialize(Stream stream)
		{
			CreateChannelOptions.Serialize(stream, this);
		}

		// Token: 0x06004ED5 RID: 20181 RVA: 0x000F496C File Offset: 0x000F2B6C
		public static void Serialize(Stream stream, CreateChannelOptions instance)
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
			if (instance.HasMember)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.Member.GetSerializedSize());
				CreateMemberOptions.Serialize(stream, instance.Member);
			}
		}

		// Token: 0x06004ED6 RID: 20182 RVA: 0x000F4A80 File Offset: 0x000F2C80
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
			if (this.HasMember)
			{
				num += 1U;
				uint serializedSize3 = this.Member.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}

		// Token: 0x04001985 RID: 6533
		public bool HasType;

		// Token: 0x04001986 RID: 6534
		private UniqueChannelType _Type;

		// Token: 0x04001987 RID: 6535
		public bool HasName;

		// Token: 0x04001988 RID: 6536
		private string _Name;

		// Token: 0x04001989 RID: 6537
		public bool HasPrivacyLevel;

		// Token: 0x0400198A RID: 6538
		private PrivacyLevel _PrivacyLevel;

		// Token: 0x0400198B RID: 6539
		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();

		// Token: 0x0400198C RID: 6540
		public bool HasMember;

		// Token: 0x0400198D RID: 6541
		private CreateMemberOptions _Member;
	}
}
