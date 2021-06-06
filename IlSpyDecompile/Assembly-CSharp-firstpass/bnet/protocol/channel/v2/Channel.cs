using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.channel.v2.Types;
using bnet.protocol.v2;

namespace bnet.protocol.channel.v2
{
	public class Channel : IProtoBuf
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

		private List<Member> _Member = new List<Member>();

		private List<ChannelInvitation> _Invitation = new List<ChannelInvitation>();

		public bool HasRoleSet;

		private ChannelRoleSet _RoleSet;

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

		public List<Member> Member
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

		public List<Member> MemberList => _Member;

		public int MemberCount => _Member.Count;

		public List<ChannelInvitation> Invitation
		{
			get
			{
				return _Invitation;
			}
			set
			{
				_Invitation = value;
			}
		}

		public List<ChannelInvitation> InvitationList => _Invitation;

		public int InvitationCount => _Invitation.Count;

		public ChannelRoleSet RoleSet
		{
			get
			{
				return _RoleSet;
			}
			set
			{
				_RoleSet = value;
				HasRoleSet = value != null;
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

		public void AddMember(Member val)
		{
			_Member.Add(val);
		}

		public void ClearMember()
		{
			_Member.Clear();
		}

		public void SetMember(List<Member> val)
		{
			Member = val;
		}

		public void AddInvitation(ChannelInvitation val)
		{
			_Invitation.Add(val);
		}

		public void ClearInvitation()
		{
			_Invitation.Clear();
		}

		public void SetInvitation(List<ChannelInvitation> val)
		{
			Invitation = val;
		}

		public void SetRoleSet(ChannelRoleSet val)
		{
			RoleSet = val;
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
			foreach (Member item2 in Member)
			{
				num ^= item2.GetHashCode();
			}
			foreach (ChannelInvitation item3 in Invitation)
			{
				num ^= item3.GetHashCode();
			}
			if (HasRoleSet)
			{
				num ^= RoleSet.GetHashCode();
			}
			if (HasPublicChannelState)
			{
				num ^= PublicChannelState.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			Channel channel = obj as Channel;
			if (channel == null)
			{
				return false;
			}
			if (HasId != channel.HasId || (HasId && !Id.Equals(channel.Id)))
			{
				return false;
			}
			if (HasType != channel.HasType || (HasType && !Type.Equals(channel.Type)))
			{
				return false;
			}
			if (HasName != channel.HasName || (HasName && !Name.Equals(channel.Name)))
			{
				return false;
			}
			if (HasPrivacyLevel != channel.HasPrivacyLevel || (HasPrivacyLevel && !PrivacyLevel.Equals(channel.PrivacyLevel)))
			{
				return false;
			}
			if (Attribute.Count != channel.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < Attribute.Count; i++)
			{
				if (!Attribute[i].Equals(channel.Attribute[i]))
				{
					return false;
				}
			}
			if (Member.Count != channel.Member.Count)
			{
				return false;
			}
			for (int j = 0; j < Member.Count; j++)
			{
				if (!Member[j].Equals(channel.Member[j]))
				{
					return false;
				}
			}
			if (Invitation.Count != channel.Invitation.Count)
			{
				return false;
			}
			for (int k = 0; k < Invitation.Count; k++)
			{
				if (!Invitation[k].Equals(channel.Invitation[k]))
				{
					return false;
				}
			}
			if (HasRoleSet != channel.HasRoleSet || (HasRoleSet && !RoleSet.Equals(channel.RoleSet)))
			{
				return false;
			}
			if (HasPublicChannelState != channel.HasPublicChannelState || (HasPublicChannelState && !PublicChannelState.Equals(channel.PublicChannelState)))
			{
				return false;
			}
			return true;
		}

		public static Channel ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Channel>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Channel Deserialize(Stream stream, Channel instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Channel DeserializeLengthDelimited(Stream stream)
		{
			Channel channel = new Channel();
			DeserializeLengthDelimited(stream, channel);
			return channel;
		}

		public static Channel DeserializeLengthDelimited(Stream stream, Channel instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Channel Deserialize(Stream stream, Channel instance, long limit)
		{
			instance.PrivacyLevel = PrivacyLevel.PRIVACY_LEVEL_OPEN;
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<bnet.protocol.v2.Attribute>();
			}
			if (instance.Member == null)
			{
				instance.Member = new List<Member>();
			}
			if (instance.Invitation == null)
			{
				instance.Invitation = new List<ChannelInvitation>();
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
				case 50:
					instance.Member.Add(bnet.protocol.channel.v2.Member.DeserializeLengthDelimited(stream));
					continue;
				case 58:
					instance.Invitation.Add(ChannelInvitation.DeserializeLengthDelimited(stream));
					continue;
				case 66:
					if (instance.RoleSet == null)
					{
						instance.RoleSet = ChannelRoleSet.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelRoleSet.DeserializeLengthDelimited(stream, instance.RoleSet);
					}
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

		public static void Serialize(Stream stream, Channel instance)
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
			if (instance.Member.Count > 0)
			{
				foreach (Member item2 in instance.Member)
				{
					stream.WriteByte(50);
					ProtocolParser.WriteUInt32(stream, item2.GetSerializedSize());
					bnet.protocol.channel.v2.Member.Serialize(stream, item2);
				}
			}
			if (instance.Invitation.Count > 0)
			{
				foreach (ChannelInvitation item3 in instance.Invitation)
				{
					stream.WriteByte(58);
					ProtocolParser.WriteUInt32(stream, item3.GetSerializedSize());
					ChannelInvitation.Serialize(stream, item3);
				}
			}
			if (instance.HasRoleSet)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteUInt32(stream, instance.RoleSet.GetSerializedSize());
				ChannelRoleSet.Serialize(stream, instance.RoleSet);
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
			if (Member.Count > 0)
			{
				foreach (Member item2 in Member)
				{
					num++;
					uint serializedSize4 = item2.GetSerializedSize();
					num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
				}
			}
			if (Invitation.Count > 0)
			{
				foreach (ChannelInvitation item3 in Invitation)
				{
					num++;
					uint serializedSize5 = item3.GetSerializedSize();
					num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
				}
			}
			if (HasRoleSet)
			{
				num++;
				uint serializedSize6 = RoleSet.GetSerializedSize();
				num += serializedSize6 + ProtocolParser.SizeOfUInt32(serializedSize6);
			}
			if (HasPublicChannelState)
			{
				num += 2;
				uint serializedSize7 = PublicChannelState.GetSerializedSize();
				num += serializedSize7 + ProtocolParser.SizeOfUInt32(serializedSize7);
			}
			return num;
		}
	}
}
