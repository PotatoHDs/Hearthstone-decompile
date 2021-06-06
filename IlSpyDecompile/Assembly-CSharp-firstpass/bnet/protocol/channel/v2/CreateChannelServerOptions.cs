using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.channel.v2.Types;
using bnet.protocol.v2;

namespace bnet.protocol.channel.v2
{
	public class CreateChannelServerOptions : IProtoBuf
	{
		public bool HasType;

		private UniqueChannelType _Type;

		public bool HasName;

		private string _Name;

		public bool HasPrivacyLevel;

		private PrivacyLevel _PrivacyLevel;

		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();

		private List<CreateMemberOptions> _Member = new List<CreateMemberOptions>();

		public bool HasCollectionId;

		private string _CollectionId;

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

		public List<CreateMemberOptions> Member
		{
			get
			{
				return _Member;
			}
			set
			{
				_Member = value;
			}
		}

		public List<CreateMemberOptions> MemberList => _Member;

		public int MemberCount => _Member.Count;

		public string CollectionId
		{
			get
			{
				return _CollectionId;
			}
			set
			{
				_CollectionId = value;
				HasCollectionId = value != null;
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

		public void AddMember(CreateMemberOptions val)
		{
			_Member.Add(val);
		}

		public void ClearMember()
		{
			_Member.Clear();
		}

		public void SetMember(List<CreateMemberOptions> val)
		{
			Member = val;
		}

		public void SetCollectionId(string val)
		{
			CollectionId = val;
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
			foreach (CreateMemberOptions item2 in Member)
			{
				num ^= item2.GetHashCode();
			}
			if (HasCollectionId)
			{
				num ^= CollectionId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			CreateChannelServerOptions createChannelServerOptions = obj as CreateChannelServerOptions;
			if (createChannelServerOptions == null)
			{
				return false;
			}
			if (HasType != createChannelServerOptions.HasType || (HasType && !Type.Equals(createChannelServerOptions.Type)))
			{
				return false;
			}
			if (HasName != createChannelServerOptions.HasName || (HasName && !Name.Equals(createChannelServerOptions.Name)))
			{
				return false;
			}
			if (HasPrivacyLevel != createChannelServerOptions.HasPrivacyLevel || (HasPrivacyLevel && !PrivacyLevel.Equals(createChannelServerOptions.PrivacyLevel)))
			{
				return false;
			}
			if (Attribute.Count != createChannelServerOptions.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < Attribute.Count; i++)
			{
				if (!Attribute[i].Equals(createChannelServerOptions.Attribute[i]))
				{
					return false;
				}
			}
			if (Member.Count != createChannelServerOptions.Member.Count)
			{
				return false;
			}
			for (int j = 0; j < Member.Count; j++)
			{
				if (!Member[j].Equals(createChannelServerOptions.Member[j]))
				{
					return false;
				}
			}
			if (HasCollectionId != createChannelServerOptions.HasCollectionId || (HasCollectionId && !CollectionId.Equals(createChannelServerOptions.CollectionId)))
			{
				return false;
			}
			return true;
		}

		public static CreateChannelServerOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateChannelServerOptions>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CreateChannelServerOptions Deserialize(Stream stream, CreateChannelServerOptions instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CreateChannelServerOptions DeserializeLengthDelimited(Stream stream)
		{
			CreateChannelServerOptions createChannelServerOptions = new CreateChannelServerOptions();
			DeserializeLengthDelimited(stream, createChannelServerOptions);
			return createChannelServerOptions;
		}

		public static CreateChannelServerOptions DeserializeLengthDelimited(Stream stream, CreateChannelServerOptions instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

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
					instance.Member.Add(CreateMemberOptions.DeserializeLengthDelimited(stream));
					continue;
				case 50:
					instance.CollectionId = ProtocolParser.ReadString(stream);
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
			if (instance.Member.Count > 0)
			{
				foreach (CreateMemberOptions item2 in instance.Member)
				{
					stream.WriteByte(42);
					ProtocolParser.WriteUInt32(stream, item2.GetSerializedSize());
					CreateMemberOptions.Serialize(stream, item2);
				}
			}
			if (instance.HasCollectionId)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.CollectionId));
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
			if (Member.Count > 0)
			{
				foreach (CreateMemberOptions item2 in Member)
				{
					num++;
					uint serializedSize3 = item2.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			if (HasCollectionId)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(CollectionId);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}
	}
}
