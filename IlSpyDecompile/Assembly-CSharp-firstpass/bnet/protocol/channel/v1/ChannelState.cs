using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.presence.v1;

namespace bnet.protocol.channel.v1
{
	public class ChannelState : IProtoBuf
	{
		public static class Types
		{
			public enum PrivacyLevel
			{
				PRIVACY_LEVEL_OPEN = 1,
				PRIVACY_LEVEL_OPEN_INVITATION_AND_FRIEND,
				PRIVACY_LEVEL_OPEN_INVITATION,
				PRIVACY_LEVEL_CLOSED
			}
		}

		public bool HasMaxMembers;

		private uint _MaxMembers;

		public bool HasMinMembers;

		private uint _MinMembers;

		private List<Attribute> _Attribute = new List<Attribute>();

		private List<Invitation> _Invitation = new List<Invitation>();

		public bool HasReason;

		private uint _Reason;

		public bool HasPrivacyLevel;

		private Types.PrivacyLevel _PrivacyLevel;

		public bool HasName;

		private string _Name;

		public bool HasChannelType;

		private string _ChannelType;

		public bool HasProgram;

		private uint _Program;

		public bool HasSubscribeToPresence;

		private bool _SubscribeToPresence;

		public bool HasChannelState_;

		private ChatChannelState _ChannelState_;

		public bool HasPresence;

		private bnet.protocol.presence.v1.ChannelState _Presence;

		public uint MaxMembers
		{
			get
			{
				return _MaxMembers;
			}
			set
			{
				_MaxMembers = value;
				HasMaxMembers = true;
			}
		}

		public uint MinMembers
		{
			get
			{
				return _MinMembers;
			}
			set
			{
				_MinMembers = value;
				HasMinMembers = true;
			}
		}

		public List<Attribute> Attribute
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

		public List<Attribute> AttributeList => _Attribute;

		public int AttributeCount => _Attribute.Count;

		public List<Invitation> Invitation
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

		public List<Invitation> InvitationList => _Invitation;

		public int InvitationCount => _Invitation.Count;

		public uint Reason
		{
			get
			{
				return _Reason;
			}
			set
			{
				_Reason = value;
				HasReason = true;
			}
		}

		public Types.PrivacyLevel PrivacyLevel
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

		public string ChannelType
		{
			get
			{
				return _ChannelType;
			}
			set
			{
				_ChannelType = value;
				HasChannelType = value != null;
			}
		}

		public uint Program
		{
			get
			{
				return _Program;
			}
			set
			{
				_Program = value;
				HasProgram = true;
			}
		}

		public bool SubscribeToPresence
		{
			get
			{
				return _SubscribeToPresence;
			}
			set
			{
				_SubscribeToPresence = value;
				HasSubscribeToPresence = true;
			}
		}

		public ChatChannelState ChannelState_
		{
			get
			{
				return _ChannelState_;
			}
			set
			{
				_ChannelState_ = value;
				HasChannelState_ = value != null;
			}
		}

		public bnet.protocol.presence.v1.ChannelState Presence
		{
			get
			{
				return _Presence;
			}
			set
			{
				_Presence = value;
				HasPresence = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetMaxMembers(uint val)
		{
			MaxMembers = val;
		}

		public void SetMinMembers(uint val)
		{
			MinMembers = val;
		}

		public void AddAttribute(Attribute val)
		{
			_Attribute.Add(val);
		}

		public void ClearAttribute()
		{
			_Attribute.Clear();
		}

		public void SetAttribute(List<Attribute> val)
		{
			Attribute = val;
		}

		public void AddInvitation(Invitation val)
		{
			_Invitation.Add(val);
		}

		public void ClearInvitation()
		{
			_Invitation.Clear();
		}

		public void SetInvitation(List<Invitation> val)
		{
			Invitation = val;
		}

		public void SetReason(uint val)
		{
			Reason = val;
		}

		public void SetPrivacyLevel(Types.PrivacyLevel val)
		{
			PrivacyLevel = val;
		}

		public void SetName(string val)
		{
			Name = val;
		}

		public void SetChannelType(string val)
		{
			ChannelType = val;
		}

		public void SetProgram(uint val)
		{
			Program = val;
		}

		public void SetSubscribeToPresence(bool val)
		{
			SubscribeToPresence = val;
		}

		public void SetChannelState_(ChatChannelState val)
		{
			ChannelState_ = val;
		}

		public void SetPresence(bnet.protocol.presence.v1.ChannelState val)
		{
			Presence = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasMaxMembers)
			{
				num ^= MaxMembers.GetHashCode();
			}
			if (HasMinMembers)
			{
				num ^= MinMembers.GetHashCode();
			}
			foreach (Attribute item in Attribute)
			{
				num ^= item.GetHashCode();
			}
			foreach (Invitation item2 in Invitation)
			{
				num ^= item2.GetHashCode();
			}
			if (HasReason)
			{
				num ^= Reason.GetHashCode();
			}
			if (HasPrivacyLevel)
			{
				num ^= PrivacyLevel.GetHashCode();
			}
			if (HasName)
			{
				num ^= Name.GetHashCode();
			}
			if (HasChannelType)
			{
				num ^= ChannelType.GetHashCode();
			}
			if (HasProgram)
			{
				num ^= Program.GetHashCode();
			}
			if (HasSubscribeToPresence)
			{
				num ^= SubscribeToPresence.GetHashCode();
			}
			if (HasChannelState_)
			{
				num ^= ChannelState_.GetHashCode();
			}
			if (HasPresence)
			{
				num ^= Presence.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ChannelState channelState = obj as ChannelState;
			if (channelState == null)
			{
				return false;
			}
			if (HasMaxMembers != channelState.HasMaxMembers || (HasMaxMembers && !MaxMembers.Equals(channelState.MaxMembers)))
			{
				return false;
			}
			if (HasMinMembers != channelState.HasMinMembers || (HasMinMembers && !MinMembers.Equals(channelState.MinMembers)))
			{
				return false;
			}
			if (Attribute.Count != channelState.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < Attribute.Count; i++)
			{
				if (!Attribute[i].Equals(channelState.Attribute[i]))
				{
					return false;
				}
			}
			if (Invitation.Count != channelState.Invitation.Count)
			{
				return false;
			}
			for (int j = 0; j < Invitation.Count; j++)
			{
				if (!Invitation[j].Equals(channelState.Invitation[j]))
				{
					return false;
				}
			}
			if (HasReason != channelState.HasReason || (HasReason && !Reason.Equals(channelState.Reason)))
			{
				return false;
			}
			if (HasPrivacyLevel != channelState.HasPrivacyLevel || (HasPrivacyLevel && !PrivacyLevel.Equals(channelState.PrivacyLevel)))
			{
				return false;
			}
			if (HasName != channelState.HasName || (HasName && !Name.Equals(channelState.Name)))
			{
				return false;
			}
			if (HasChannelType != channelState.HasChannelType || (HasChannelType && !ChannelType.Equals(channelState.ChannelType)))
			{
				return false;
			}
			if (HasProgram != channelState.HasProgram || (HasProgram && !Program.Equals(channelState.Program)))
			{
				return false;
			}
			if (HasSubscribeToPresence != channelState.HasSubscribeToPresence || (HasSubscribeToPresence && !SubscribeToPresence.Equals(channelState.SubscribeToPresence)))
			{
				return false;
			}
			if (HasChannelState_ != channelState.HasChannelState_ || (HasChannelState_ && !ChannelState_.Equals(channelState.ChannelState_)))
			{
				return false;
			}
			if (HasPresence != channelState.HasPresence || (HasPresence && !Presence.Equals(channelState.Presence)))
			{
				return false;
			}
			return true;
		}

		public static ChannelState ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelState>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ChannelState Deserialize(Stream stream, ChannelState instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ChannelState DeserializeLengthDelimited(Stream stream)
		{
			ChannelState channelState = new ChannelState();
			DeserializeLengthDelimited(stream, channelState);
			return channelState;
		}

		public static ChannelState DeserializeLengthDelimited(Stream stream, ChannelState instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ChannelState Deserialize(Stream stream, ChannelState instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<Attribute>();
			}
			if (instance.Invitation == null)
			{
				instance.Invitation = new List<Invitation>();
			}
			instance.PrivacyLevel = Types.PrivacyLevel.PRIVACY_LEVEL_OPEN;
			instance.ChannelType = "default";
			instance.SubscribeToPresence = true;
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
				case 8:
					instance.MaxMembers = ProtocolParser.ReadUInt32(stream);
					continue;
				case 16:
					instance.MinMembers = ProtocolParser.ReadUInt32(stream);
					continue;
				case 26:
					instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
					continue;
				case 34:
					instance.Invitation.Add(bnet.protocol.Invitation.DeserializeLengthDelimited(stream));
					continue;
				case 48:
					instance.Reason = ProtocolParser.ReadUInt32(stream);
					continue;
				case 56:
					instance.PrivacyLevel = (Types.PrivacyLevel)ProtocolParser.ReadUInt64(stream);
					continue;
				case 66:
					instance.Name = ProtocolParser.ReadString(stream);
					continue;
				case 82:
					instance.ChannelType = ProtocolParser.ReadString(stream);
					continue;
				case 93:
					instance.Program = binaryReader.ReadUInt32();
					continue;
				case 104:
					instance.SubscribeToPresence = ProtocolParser.ReadBool(stream);
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					switch (key.Field)
					{
					case 0u:
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					case 100u:
						if (key.WireType == Wire.LengthDelimited)
						{
							if (instance.ChannelState_ == null)
							{
								instance.ChannelState_ = ChatChannelState.DeserializeLengthDelimited(stream);
							}
							else
							{
								ChatChannelState.DeserializeLengthDelimited(stream, instance.ChannelState_);
							}
						}
						break;
					case 101u:
						if (key.WireType == Wire.LengthDelimited)
						{
							if (instance.Presence == null)
							{
								instance.Presence = bnet.protocol.presence.v1.ChannelState.DeserializeLengthDelimited(stream);
							}
							else
							{
								bnet.protocol.presence.v1.ChannelState.DeserializeLengthDelimited(stream, instance.Presence);
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

		public static void Serialize(Stream stream, ChannelState instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasMaxMembers)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.MaxMembers);
			}
			if (instance.HasMinMembers)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.MinMembers);
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (Attribute item in instance.Attribute)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.Attribute.Serialize(stream, item);
				}
			}
			if (instance.Invitation.Count > 0)
			{
				foreach (Invitation item2 in instance.Invitation)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, item2.GetSerializedSize());
					bnet.protocol.Invitation.Serialize(stream, item2);
				}
			}
			if (instance.HasReason)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt32(stream, instance.Reason);
			}
			if (instance.HasPrivacyLevel)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PrivacyLevel);
			}
			if (instance.HasName)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			}
			if (instance.HasChannelType)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ChannelType));
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(93);
				binaryWriter.Write(instance.Program);
			}
			if (instance.HasSubscribeToPresence)
			{
				stream.WriteByte(104);
				ProtocolParser.WriteBool(stream, instance.SubscribeToPresence);
			}
			if (instance.HasChannelState_)
			{
				stream.WriteByte(162);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt32(stream, instance.ChannelState_.GetSerializedSize());
				ChatChannelState.Serialize(stream, instance.ChannelState_);
			}
			if (instance.HasPresence)
			{
				stream.WriteByte(170);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt32(stream, instance.Presence.GetSerializedSize());
				bnet.protocol.presence.v1.ChannelState.Serialize(stream, instance.Presence);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasMaxMembers)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(MaxMembers);
			}
			if (HasMinMembers)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(MinMembers);
			}
			if (Attribute.Count > 0)
			{
				foreach (Attribute item in Attribute)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (Invitation.Count > 0)
			{
				foreach (Invitation item2 in Invitation)
				{
					num++;
					uint serializedSize2 = item2.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (HasReason)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Reason);
			}
			if (HasPrivacyLevel)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)PrivacyLevel);
			}
			if (HasName)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasChannelType)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(ChannelType);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasProgram)
			{
				num++;
				num += 4;
			}
			if (HasSubscribeToPresence)
			{
				num++;
				num++;
			}
			if (HasChannelState_)
			{
				num += 2;
				uint serializedSize3 = ChannelState_.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (HasPresence)
			{
				num += 2;
				uint serializedSize4 = Presence.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			return num;
		}
	}
}
