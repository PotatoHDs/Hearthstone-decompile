using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.channel.v2.Types;
using bnet.protocol.v2;

namespace bnet.protocol.channel.v2
{
	public class CreateChannelOptions : IProtoBuf
	{
		public bool HasType;

		private UniqueChannelType _Type;

		public bool HasName;

		private string _Name;

		public bool HasPrivacyLevel;

		private PrivacyLevel _PrivacyLevel;

		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();

		public bool HasMember;

		private CreateMemberOptions _Member;

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

		public CreateMemberOptions Member
		{
			get
			{
				return _Member;
			}
			set
			{
				_Member = value;
				HasMember = value != null;
			}
		}

		public bool IsInitialized => true;

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

		public void SetMember(CreateMemberOptions val)
		{
			Member = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
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
			if (HasMember)
			{
				num ^= Member.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			CreateChannelOptions createChannelOptions = obj as CreateChannelOptions;
			if (createChannelOptions == null)
			{
				return false;
			}
			if (HasType != createChannelOptions.HasType || (HasType && !Type.Equals(createChannelOptions.Type)))
			{
				return false;
			}
			if (HasName != createChannelOptions.HasName || (HasName && !Name.Equals(createChannelOptions.Name)))
			{
				return false;
			}
			if (HasPrivacyLevel != createChannelOptions.HasPrivacyLevel || (HasPrivacyLevel && !PrivacyLevel.Equals(createChannelOptions.PrivacyLevel)))
			{
				return false;
			}
			if (Attribute.Count != createChannelOptions.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < Attribute.Count; i++)
			{
				if (!Attribute[i].Equals(createChannelOptions.Attribute[i]))
				{
					return false;
				}
			}
			if (HasMember != createChannelOptions.HasMember || (HasMember && !Member.Equals(createChannelOptions.Member)))
			{
				return false;
			}
			return true;
		}

		public static CreateChannelOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateChannelOptions>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CreateChannelOptions Deserialize(Stream stream, CreateChannelOptions instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CreateChannelOptions DeserializeLengthDelimited(Stream stream)
		{
			CreateChannelOptions createChannelOptions = new CreateChannelOptions();
			DeserializeLengthDelimited(stream, createChannelOptions);
			return createChannelOptions;
		}

		public static CreateChannelOptions DeserializeLengthDelimited(Stream stream, CreateChannelOptions instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CreateChannelOptions Deserialize(Stream stream, CreateChannelOptions instance, long limit)
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
					if (instance.Type == null)
					{
						instance.Type = UniqueChannelType.DeserializeLengthDelimited(stream);
					}
					else
					{
						UniqueChannelType.DeserializeLengthDelimited(stream, instance.Type);
					}
					continue;
				case 18:
					instance.Name = ProtocolParser.ReadString(stream);
					continue;
				case 24:
					instance.PrivacyLevel = (PrivacyLevel)ProtocolParser.ReadUInt64(stream);
					continue;
				case 34:
					instance.Attribute.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
					continue;
				case 42:
					if (instance.Member == null)
					{
						instance.Member = CreateMemberOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						CreateMemberOptions.DeserializeLengthDelimited(stream, instance.Member);
					}
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
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
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PrivacyLevel);
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute item in instance.Attribute)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.v2.Attribute.Serialize(stream, item);
				}
			}
			if (instance.HasMember)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.Member.GetSerializedSize());
				CreateMemberOptions.Serialize(stream, instance.Member);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasType)
			{
				num++;
				uint serializedSize = Type.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
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
					uint serializedSize2 = item.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (HasMember)
			{
				num++;
				uint serializedSize3 = Member.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}
	}
}
