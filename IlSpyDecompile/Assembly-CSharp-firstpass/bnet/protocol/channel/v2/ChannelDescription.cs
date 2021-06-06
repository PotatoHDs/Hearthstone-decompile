using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.channel.v2.Types;
using bnet.protocol.v2;

namespace bnet.protocol.channel.v2
{
	public class ChannelDescription : IProtoBuf
	{
		public bool HasId;

		private ChannelId _Id;

		public bool HasType;

		private UniqueChannelType _Type;

		public bool HasName;

		private string _Name;

		public bool HasPrivacyLevel;

		private PrivacyLevel _PrivacyLevel;

		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();

		public bool HasMemberCount;

		private uint _MemberCount;

		public bool HasPublicChannelState;

		private PublicChannelState _PublicChannelState;

		public ChannelId Id
		{
			get
			{
				return _Id;
			}
			set
			{
				_Id = value;
				HasId = value != null;
			}
		}

		public UniqueChannelType Type
		{
			get
			{
				return _Type;
			}
			set
			{
				_Type = value;
				HasType = value != null;
			}
		}

		public string Name
		{
			get
			{
				return _Name;
			}
			set
			{
				_Name = value;
				HasName = value != null;
			}
		}

		public PrivacyLevel PrivacyLevel
		{
			get
			{
				return _PrivacyLevel;
			}
			set
			{
				_PrivacyLevel = value;
				HasPrivacyLevel = true;
			}
		}

		public List<bnet.protocol.v2.Attribute> Attribute
		{
			get
			{
				return _Attribute;
			}
			set
			{
				_Attribute = value;
			}
		}

		public List<bnet.protocol.v2.Attribute> AttributeList => _Attribute;

		public int AttributeCount => _Attribute.Count;

		public uint MemberCount
		{
			get
			{
				return _MemberCount;
			}
			set
			{
				_MemberCount = value;
				HasMemberCount = true;
			}
		}

		public PublicChannelState PublicChannelState
		{
			get
			{
				return _PublicChannelState;
			}
			set
			{
				_PublicChannelState = value;
				HasPublicChannelState = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetId(ChannelId val)
		{
			Id = val;
		}

		public void SetType(UniqueChannelType val)
		{
			Type = val;
		}

		public void SetName(string val)
		{
			Name = val;
		}

		public void SetPrivacyLevel(PrivacyLevel val)
		{
			PrivacyLevel = val;
		}

		public void AddAttribute(bnet.protocol.v2.Attribute val)
		{
			_Attribute.Add(val);
		}

		public void ClearAttribute()
		{
			_Attribute.Clear();
		}

		public void SetAttribute(List<bnet.protocol.v2.Attribute> val)
		{
			Attribute = val;
		}

		public void SetMemberCount(uint val)
		{
			MemberCount = val;
		}

		public void SetPublicChannelState(PublicChannelState val)
		{
			PublicChannelState = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasId)
			{
				num ^= Id.GetHashCode();
			}
			if (HasType)
			{
				num ^= Type.GetHashCode();
			}
			if (HasName)
			{
				num ^= Name.GetHashCode();
			}
			if (HasPrivacyLevel)
			{
				num ^= PrivacyLevel.GetHashCode();
			}
			foreach (bnet.protocol.v2.Attribute item in Attribute)
			{
				num ^= item.GetHashCode();
			}
			if (HasMemberCount)
			{
				num ^= MemberCount.GetHashCode();
			}
			if (HasPublicChannelState)
			{
				num ^= PublicChannelState.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ChannelDescription channelDescription = obj as ChannelDescription;
			if (channelDescription == null)
			{
				return false;
			}
			if (HasId != channelDescription.HasId || (HasId && !Id.Equals(channelDescription.Id)))
			{
				return false;
			}
			if (HasType != channelDescription.HasType || (HasType && !Type.Equals(channelDescription.Type)))
			{
				return false;
			}
			if (HasName != channelDescription.HasName || (HasName && !Name.Equals(channelDescription.Name)))
			{
				return false;
			}
			if (HasPrivacyLevel != channelDescription.HasPrivacyLevel || (HasPrivacyLevel && !PrivacyLevel.Equals(channelDescription.PrivacyLevel)))
			{
				return false;
			}
			if (Attribute.Count != channelDescription.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < Attribute.Count; i++)
			{
				if (!Attribute[i].Equals(channelDescription.Attribute[i]))
				{
					return false;
				}
			}
			if (HasMemberCount != channelDescription.HasMemberCount || (HasMemberCount && !MemberCount.Equals(channelDescription.MemberCount)))
			{
				return false;
			}
			if (HasPublicChannelState != channelDescription.HasPublicChannelState || (HasPublicChannelState && !PublicChannelState.Equals(channelDescription.PublicChannelState)))
			{
				return false;
			}
			return true;
		}

		public static ChannelDescription ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelDescription>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ChannelDescription Deserialize(Stream stream, ChannelDescription instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ChannelDescription DeserializeLengthDelimited(Stream stream)
		{
			ChannelDescription channelDescription = new ChannelDescription();
			DeserializeLengthDelimited(stream, channelDescription);
			return channelDescription;
		}

		public static ChannelDescription DeserializeLengthDelimited(Stream stream, ChannelDescription instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ChannelDescription Deserialize(Stream stream, ChannelDescription instance, long limit)
		{
			instance.PrivacyLevel = PrivacyLevel.PRIVACY_LEVEL_OPEN;
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<bnet.protocol.v2.Attribute>();
			}
			while (true)
			{
				if (limit >= 0 && stream.Position >= limit)
				{
					if (stream.Position == limit)
					{
						break;
					}
					throw new ProtocolBufferException("Read past max limit");
				}
				int num = stream.ReadByte();
				switch (num)
				{
				case -1:
					break;
				case 10:
					if (instance.Id == null)
					{
						instance.Id = ChannelId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelId.DeserializeLengthDelimited(stream, instance.Id);
					}
					continue;
				case 18:
					if (instance.Type == null)
					{
						instance.Type = UniqueChannelType.DeserializeLengthDelimited(stream);
					}
					else
					{
						UniqueChannelType.DeserializeLengthDelimited(stream, instance.Type);
					}
					continue;
				case 26:
					instance.Name = ProtocolParser.ReadString(stream);
					continue;
				case 32:
					instance.PrivacyLevel = (PrivacyLevel)ProtocolParser.ReadUInt64(stream);
					continue;
				case 42:
					instance.Attribute.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
					continue;
				case 48:
					instance.MemberCount = ProtocolParser.ReadUInt32(stream);
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					switch (key.Field)
					{
					case 0u:
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					case 110u:
						if (key.WireType == Wire.LengthDelimited)
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
						break;
					default:
						ProtocolParser.SkipKey(stream, key);
						break;
					}
					continue;
				}
				}
				if (limit < 0)
				{
					break;
				}
				throw new EndOfStreamException();
			}
			return instance;
		}

		public void Serialize(Stream stream)
		{
			Serialize(stream, this);
		}

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
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PrivacyLevel);
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute item in instance.Attribute)
				{
					stream.WriteByte(42);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.v2.Attribute.Serialize(stream, item);
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

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasId)
			{
				num++;
				uint serializedSize = Id.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasType)
			{
				num++;
				uint serializedSize2 = Type.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasName)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasPrivacyLevel)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)PrivacyLevel);
			}
			if (Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute item in Attribute)
				{
					num++;
					uint serializedSize3 = item.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			if (HasMemberCount)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(MemberCount);
			}
			if (HasPublicChannelState)
			{
				num += 2;
				uint serializedSize4 = PublicChannelState.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			return num;
		}
	}
}
