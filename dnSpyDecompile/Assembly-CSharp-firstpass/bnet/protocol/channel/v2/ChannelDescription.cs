using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.channel.v2.Types;
using bnet.protocol.v2;

namespace bnet.protocol.channel.v2
{
	// Token: 0x0200047E RID: 1150
	public class ChannelDescription : IProtoBuf
	{
		// Token: 0x17000EF8 RID: 3832
		// (get) Token: 0x06004FAB RID: 20395 RVA: 0x000F74F5 File Offset: 0x000F56F5
		// (set) Token: 0x06004FAC RID: 20396 RVA: 0x000F74FD File Offset: 0x000F56FD
		public ChannelId Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
				this.HasId = (value != null);
			}
		}

		// Token: 0x06004FAD RID: 20397 RVA: 0x000F7510 File Offset: 0x000F5710
		public void SetId(ChannelId val)
		{
			this.Id = val;
		}

		// Token: 0x17000EF9 RID: 3833
		// (get) Token: 0x06004FAE RID: 20398 RVA: 0x000F7519 File Offset: 0x000F5719
		// (set) Token: 0x06004FAF RID: 20399 RVA: 0x000F7521 File Offset: 0x000F5721
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

		// Token: 0x06004FB0 RID: 20400 RVA: 0x000F7534 File Offset: 0x000F5734
		public void SetType(UniqueChannelType val)
		{
			this.Type = val;
		}

		// Token: 0x17000EFA RID: 3834
		// (get) Token: 0x06004FB1 RID: 20401 RVA: 0x000F753D File Offset: 0x000F573D
		// (set) Token: 0x06004FB2 RID: 20402 RVA: 0x000F7545 File Offset: 0x000F5745
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

		// Token: 0x06004FB3 RID: 20403 RVA: 0x000F7558 File Offset: 0x000F5758
		public void SetName(string val)
		{
			this.Name = val;
		}

		// Token: 0x17000EFB RID: 3835
		// (get) Token: 0x06004FB4 RID: 20404 RVA: 0x000F7561 File Offset: 0x000F5761
		// (set) Token: 0x06004FB5 RID: 20405 RVA: 0x000F7569 File Offset: 0x000F5769
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

		// Token: 0x06004FB6 RID: 20406 RVA: 0x000F7579 File Offset: 0x000F5779
		public void SetPrivacyLevel(PrivacyLevel val)
		{
			this.PrivacyLevel = val;
		}

		// Token: 0x17000EFC RID: 3836
		// (get) Token: 0x06004FB7 RID: 20407 RVA: 0x000F7582 File Offset: 0x000F5782
		// (set) Token: 0x06004FB8 RID: 20408 RVA: 0x000F758A File Offset: 0x000F578A
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

		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x06004FB9 RID: 20409 RVA: 0x000F7582 File Offset: 0x000F5782
		public List<bnet.protocol.v2.Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000EFE RID: 3838
		// (get) Token: 0x06004FBA RID: 20410 RVA: 0x000F7593 File Offset: 0x000F5793
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x06004FBB RID: 20411 RVA: 0x000F75A0 File Offset: 0x000F57A0
		public void AddAttribute(bnet.protocol.v2.Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x06004FBC RID: 20412 RVA: 0x000F75AE File Offset: 0x000F57AE
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x06004FBD RID: 20413 RVA: 0x000F75BB File Offset: 0x000F57BB
		public void SetAttribute(List<bnet.protocol.v2.Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x17000EFF RID: 3839
		// (get) Token: 0x06004FBE RID: 20414 RVA: 0x000F75C4 File Offset: 0x000F57C4
		// (set) Token: 0x06004FBF RID: 20415 RVA: 0x000F75CC File Offset: 0x000F57CC
		public uint MemberCount
		{
			get
			{
				return this._MemberCount;
			}
			set
			{
				this._MemberCount = value;
				this.HasMemberCount = true;
			}
		}

		// Token: 0x06004FC0 RID: 20416 RVA: 0x000F75DC File Offset: 0x000F57DC
		public void SetMemberCount(uint val)
		{
			this.MemberCount = val;
		}

		// Token: 0x17000F00 RID: 3840
		// (get) Token: 0x06004FC1 RID: 20417 RVA: 0x000F75E5 File Offset: 0x000F57E5
		// (set) Token: 0x06004FC2 RID: 20418 RVA: 0x000F75ED File Offset: 0x000F57ED
		public PublicChannelState PublicChannelState
		{
			get
			{
				return this._PublicChannelState;
			}
			set
			{
				this._PublicChannelState = value;
				this.HasPublicChannelState = (value != null);
			}
		}

		// Token: 0x06004FC3 RID: 20419 RVA: 0x000F7600 File Offset: 0x000F5800
		public void SetPublicChannelState(PublicChannelState val)
		{
			this.PublicChannelState = val;
		}

		// Token: 0x06004FC4 RID: 20420 RVA: 0x000F760C File Offset: 0x000F580C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasId)
			{
				num ^= this.Id.GetHashCode();
			}
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
			if (this.HasMemberCount)
			{
				num ^= this.MemberCount.GetHashCode();
			}
			if (this.HasPublicChannelState)
			{
				num ^= this.PublicChannelState.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004FC5 RID: 20421 RVA: 0x000F7700 File Offset: 0x000F5900
		public override bool Equals(object obj)
		{
			ChannelDescription channelDescription = obj as ChannelDescription;
			if (channelDescription == null)
			{
				return false;
			}
			if (this.HasId != channelDescription.HasId || (this.HasId && !this.Id.Equals(channelDescription.Id)))
			{
				return false;
			}
			if (this.HasType != channelDescription.HasType || (this.HasType && !this.Type.Equals(channelDescription.Type)))
			{
				return false;
			}
			if (this.HasName != channelDescription.HasName || (this.HasName && !this.Name.Equals(channelDescription.Name)))
			{
				return false;
			}
			if (this.HasPrivacyLevel != channelDescription.HasPrivacyLevel || (this.HasPrivacyLevel && !this.PrivacyLevel.Equals(channelDescription.PrivacyLevel)))
			{
				return false;
			}
			if (this.Attribute.Count != channelDescription.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(channelDescription.Attribute[i]))
				{
					return false;
				}
			}
			return this.HasMemberCount == channelDescription.HasMemberCount && (!this.HasMemberCount || this.MemberCount.Equals(channelDescription.MemberCount)) && this.HasPublicChannelState == channelDescription.HasPublicChannelState && (!this.HasPublicChannelState || this.PublicChannelState.Equals(channelDescription.PublicChannelState));
		}

		// Token: 0x17000F01 RID: 3841
		// (get) Token: 0x06004FC6 RID: 20422 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004FC7 RID: 20423 RVA: 0x000F787E File Offset: 0x000F5A7E
		public static ChannelDescription ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelDescription>(bs, 0, -1);
		}

		// Token: 0x06004FC8 RID: 20424 RVA: 0x000F7888 File Offset: 0x000F5A88
		public void Deserialize(Stream stream)
		{
			ChannelDescription.Deserialize(stream, this);
		}

		// Token: 0x06004FC9 RID: 20425 RVA: 0x000F7892 File Offset: 0x000F5A92
		public static ChannelDescription Deserialize(Stream stream, ChannelDescription instance)
		{
			return ChannelDescription.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004FCA RID: 20426 RVA: 0x000F78A0 File Offset: 0x000F5AA0
		public static ChannelDescription DeserializeLengthDelimited(Stream stream)
		{
			ChannelDescription channelDescription = new ChannelDescription();
			ChannelDescription.DeserializeLengthDelimited(stream, channelDescription);
			return channelDescription;
		}

		// Token: 0x06004FCB RID: 20427 RVA: 0x000F78BC File Offset: 0x000F5ABC
		public static ChannelDescription DeserializeLengthDelimited(Stream stream, ChannelDescription instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChannelDescription.Deserialize(stream, instance, num);
		}

		// Token: 0x06004FCC RID: 20428 RVA: 0x000F78E4 File Offset: 0x000F5AE4
		public static ChannelDescription Deserialize(Stream stream, ChannelDescription instance, long limit)
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
					if (num <= 26)
					{
						if (num != 10)
						{
							if (num != 18)
							{
								if (num == 26)
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
							if (instance.Id == null)
							{
								instance.Id = ChannelId.DeserializeLengthDelimited(stream);
								continue;
							}
							ChannelId.DeserializeLengthDelimited(stream, instance.Id);
							continue;
						}
					}
					else
					{
						if (num == 32)
						{
							instance.PrivacyLevel = (PrivacyLevel)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 42)
						{
							instance.Attribute.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 48)
						{
							instance.MemberCount = ProtocolParser.ReadUInt32(stream);
							continue;
						}
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					uint field = key.Field;
					if (field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					if (field != 110U)
					{
						ProtocolParser.SkipKey(stream, key);
					}
					else if (key.WireType == Wire.LengthDelimited)
					{
						if (instance.PublicChannelState == null)
						{
							instance.PublicChannelState = PublicChannelState.DeserializeLengthDelimited(stream);
						}
						else
						{
							PublicChannelState.DeserializeLengthDelimited(stream, instance.PublicChannelState);
						}
					}
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06004FCD RID: 20429 RVA: 0x000F7A85 File Offset: 0x000F5C85
		public void Serialize(Stream stream)
		{
			ChannelDescription.Serialize(stream, this);
		}

		// Token: 0x06004FCE RID: 20430 RVA: 0x000F7A90 File Offset: 0x000F5C90
		public static void Serialize(Stream stream, ChannelDescription instance)
		{
			if (instance.HasId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Id.GetSerializedSize());
				ChannelId.Serialize(stream, instance.Id);
			}
			if (instance.HasType)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Type.GetSerializedSize());
				UniqueChannelType.Serialize(stream, instance.Type);
			}
			if (instance.HasName)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			}
			if (instance.HasPrivacyLevel)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.PrivacyLevel));
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(42);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.v2.Attribute.Serialize(stream, attribute);
				}
			}
			if (instance.HasMemberCount)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt32(stream, instance.MemberCount);
			}
			if (instance.HasPublicChannelState)
			{
				stream.WriteByte(242);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt32(stream, instance.PublicChannelState.GetSerializedSize());
				PublicChannelState.Serialize(stream, instance.PublicChannelState);
			}
		}

		// Token: 0x06004FCF RID: 20431 RVA: 0x000F7BF8 File Offset: 0x000F5DF8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasId)
			{
				num += 1U;
				uint serializedSize = this.Id.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasType)
			{
				num += 1U;
				uint serializedSize2 = this.Type.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
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
					uint serializedSize3 = attribute.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			if (this.HasMemberCount)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.MemberCount);
			}
			if (this.HasPublicChannelState)
			{
				num += 2U;
				uint serializedSize4 = this.PublicChannelState.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			return num;
		}

		// Token: 0x040019BD RID: 6589
		public bool HasId;

		// Token: 0x040019BE RID: 6590
		private ChannelId _Id;

		// Token: 0x040019BF RID: 6591
		public bool HasType;

		// Token: 0x040019C0 RID: 6592
		private UniqueChannelType _Type;

		// Token: 0x040019C1 RID: 6593
		public bool HasName;

		// Token: 0x040019C2 RID: 6594
		private string _Name;

		// Token: 0x040019C3 RID: 6595
		public bool HasPrivacyLevel;

		// Token: 0x040019C4 RID: 6596
		private PrivacyLevel _PrivacyLevel;

		// Token: 0x040019C5 RID: 6597
		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();

		// Token: 0x040019C6 RID: 6598
		public bool HasMemberCount;

		// Token: 0x040019C7 RID: 6599
		private uint _MemberCount;

		// Token: 0x040019C8 RID: 6600
		public bool HasPublicChannelState;

		// Token: 0x040019C9 RID: 6601
		private PublicChannelState _PublicChannelState;
	}
}
