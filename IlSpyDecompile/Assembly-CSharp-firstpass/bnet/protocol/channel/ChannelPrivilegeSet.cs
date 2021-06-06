using System.IO;

namespace bnet.protocol.channel
{
	public class ChannelPrivilegeSet : IProtoBuf
	{
		public bool HasCanInvite;

		private bool _CanInvite;

		public bool HasCanInviteWithReservation;

		private bool _CanInviteWithReservation;

		public bool HasCanRevokeOtherInvitation;

		private bool _CanRevokeOtherInvitation;

		public bool HasCanRevokeOwnInvitation;

		private bool _CanRevokeOwnInvitation;

		public bool HasCanKick;

		private bool _CanKick;

		public bool HasCanDissolve;

		private bool _CanDissolve;

		public bool HasCanSetPrivacy;

		private bool _CanSetPrivacy;

		public bool HasCanSendMessage;

		private bool _CanSendMessage;

		public bool HasCanReceiveMessage;

		private bool _CanReceiveMessage;

		public bool HasCanSetAttribute;

		private bool _CanSetAttribute;

		public bool HasCanSetOtherMemberAttribute;

		private bool _CanSetOtherMemberAttribute;

		public bool HasCanSetOwnMemberAttribute;

		private bool _CanSetOwnMemberAttribute;

		public bool HasCanEnterGame;

		private bool _CanEnterGame;

		public bool HasCanSuggest;

		private bool _CanSuggest;

		public bool HasCanApprove;

		private bool _CanApprove;

		public bool CanInvite
		{
			get
			{
				return _CanInvite;
			}
			set
			{
				_CanInvite = value;
				HasCanInvite = true;
			}
		}

		public bool CanInviteWithReservation
		{
			get
			{
				return _CanInviteWithReservation;
			}
			set
			{
				_CanInviteWithReservation = value;
				HasCanInviteWithReservation = true;
			}
		}

		public bool CanRevokeOtherInvitation
		{
			get
			{
				return _CanRevokeOtherInvitation;
			}
			set
			{
				_CanRevokeOtherInvitation = value;
				HasCanRevokeOtherInvitation = true;
			}
		}

		public bool CanRevokeOwnInvitation
		{
			get
			{
				return _CanRevokeOwnInvitation;
			}
			set
			{
				_CanRevokeOwnInvitation = value;
				HasCanRevokeOwnInvitation = true;
			}
		}

		public bool CanKick
		{
			get
			{
				return _CanKick;
			}
			set
			{
				_CanKick = value;
				HasCanKick = true;
			}
		}

		public bool CanDissolve
		{
			get
			{
				return _CanDissolve;
			}
			set
			{
				_CanDissolve = value;
				HasCanDissolve = true;
			}
		}

		public bool CanSetPrivacy
		{
			get
			{
				return _CanSetPrivacy;
			}
			set
			{
				_CanSetPrivacy = value;
				HasCanSetPrivacy = true;
			}
		}

		public bool CanSendMessage
		{
			get
			{
				return _CanSendMessage;
			}
			set
			{
				_CanSendMessage = value;
				HasCanSendMessage = true;
			}
		}

		public bool CanReceiveMessage
		{
			get
			{
				return _CanReceiveMessage;
			}
			set
			{
				_CanReceiveMessage = value;
				HasCanReceiveMessage = true;
			}
		}

		public bool CanSetAttribute
		{
			get
			{
				return _CanSetAttribute;
			}
			set
			{
				_CanSetAttribute = value;
				HasCanSetAttribute = true;
			}
		}

		public bool CanSetOtherMemberAttribute
		{
			get
			{
				return _CanSetOtherMemberAttribute;
			}
			set
			{
				_CanSetOtherMemberAttribute = value;
				HasCanSetOtherMemberAttribute = true;
			}
		}

		public bool CanSetOwnMemberAttribute
		{
			get
			{
				return _CanSetOwnMemberAttribute;
			}
			set
			{
				_CanSetOwnMemberAttribute = value;
				HasCanSetOwnMemberAttribute = true;
			}
		}

		public bool CanEnterGame
		{
			get
			{
				return _CanEnterGame;
			}
			set
			{
				_CanEnterGame = value;
				HasCanEnterGame = true;
			}
		}

		public bool CanSuggest
		{
			get
			{
				return _CanSuggest;
			}
			set
			{
				_CanSuggest = value;
				HasCanSuggest = true;
			}
		}

		public bool CanApprove
		{
			get
			{
				return _CanApprove;
			}
			set
			{
				_CanApprove = value;
				HasCanApprove = true;
			}
		}

		public bool IsInitialized => true;

		public void SetCanInvite(bool val)
		{
			CanInvite = val;
		}

		public void SetCanInviteWithReservation(bool val)
		{
			CanInviteWithReservation = val;
		}

		public void SetCanRevokeOtherInvitation(bool val)
		{
			CanRevokeOtherInvitation = val;
		}

		public void SetCanRevokeOwnInvitation(bool val)
		{
			CanRevokeOwnInvitation = val;
		}

		public void SetCanKick(bool val)
		{
			CanKick = val;
		}

		public void SetCanDissolve(bool val)
		{
			CanDissolve = val;
		}

		public void SetCanSetPrivacy(bool val)
		{
			CanSetPrivacy = val;
		}

		public void SetCanSendMessage(bool val)
		{
			CanSendMessage = val;
		}

		public void SetCanReceiveMessage(bool val)
		{
			CanReceiveMessage = val;
		}

		public void SetCanSetAttribute(bool val)
		{
			CanSetAttribute = val;
		}

		public void SetCanSetOtherMemberAttribute(bool val)
		{
			CanSetOtherMemberAttribute = val;
		}

		public void SetCanSetOwnMemberAttribute(bool val)
		{
			CanSetOwnMemberAttribute = val;
		}

		public void SetCanEnterGame(bool val)
		{
			CanEnterGame = val;
		}

		public void SetCanSuggest(bool val)
		{
			CanSuggest = val;
		}

		public void SetCanApprove(bool val)
		{
			CanApprove = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasCanInvite)
			{
				num ^= CanInvite.GetHashCode();
			}
			if (HasCanInviteWithReservation)
			{
				num ^= CanInviteWithReservation.GetHashCode();
			}
			if (HasCanRevokeOtherInvitation)
			{
				num ^= CanRevokeOtherInvitation.GetHashCode();
			}
			if (HasCanRevokeOwnInvitation)
			{
				num ^= CanRevokeOwnInvitation.GetHashCode();
			}
			if (HasCanKick)
			{
				num ^= CanKick.GetHashCode();
			}
			if (HasCanDissolve)
			{
				num ^= CanDissolve.GetHashCode();
			}
			if (HasCanSetPrivacy)
			{
				num ^= CanSetPrivacy.GetHashCode();
			}
			if (HasCanSendMessage)
			{
				num ^= CanSendMessage.GetHashCode();
			}
			if (HasCanReceiveMessage)
			{
				num ^= CanReceiveMessage.GetHashCode();
			}
			if (HasCanSetAttribute)
			{
				num ^= CanSetAttribute.GetHashCode();
			}
			if (HasCanSetOtherMemberAttribute)
			{
				num ^= CanSetOtherMemberAttribute.GetHashCode();
			}
			if (HasCanSetOwnMemberAttribute)
			{
				num ^= CanSetOwnMemberAttribute.GetHashCode();
			}
			if (HasCanEnterGame)
			{
				num ^= CanEnterGame.GetHashCode();
			}
			if (HasCanSuggest)
			{
				num ^= CanSuggest.GetHashCode();
			}
			if (HasCanApprove)
			{
				num ^= CanApprove.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ChannelPrivilegeSet channelPrivilegeSet = obj as ChannelPrivilegeSet;
			if (channelPrivilegeSet == null)
			{
				return false;
			}
			if (HasCanInvite != channelPrivilegeSet.HasCanInvite || (HasCanInvite && !CanInvite.Equals(channelPrivilegeSet.CanInvite)))
			{
				return false;
			}
			if (HasCanInviteWithReservation != channelPrivilegeSet.HasCanInviteWithReservation || (HasCanInviteWithReservation && !CanInviteWithReservation.Equals(channelPrivilegeSet.CanInviteWithReservation)))
			{
				return false;
			}
			if (HasCanRevokeOtherInvitation != channelPrivilegeSet.HasCanRevokeOtherInvitation || (HasCanRevokeOtherInvitation && !CanRevokeOtherInvitation.Equals(channelPrivilegeSet.CanRevokeOtherInvitation)))
			{
				return false;
			}
			if (HasCanRevokeOwnInvitation != channelPrivilegeSet.HasCanRevokeOwnInvitation || (HasCanRevokeOwnInvitation && !CanRevokeOwnInvitation.Equals(channelPrivilegeSet.CanRevokeOwnInvitation)))
			{
				return false;
			}
			if (HasCanKick != channelPrivilegeSet.HasCanKick || (HasCanKick && !CanKick.Equals(channelPrivilegeSet.CanKick)))
			{
				return false;
			}
			if (HasCanDissolve != channelPrivilegeSet.HasCanDissolve || (HasCanDissolve && !CanDissolve.Equals(channelPrivilegeSet.CanDissolve)))
			{
				return false;
			}
			if (HasCanSetPrivacy != channelPrivilegeSet.HasCanSetPrivacy || (HasCanSetPrivacy && !CanSetPrivacy.Equals(channelPrivilegeSet.CanSetPrivacy)))
			{
				return false;
			}
			if (HasCanSendMessage != channelPrivilegeSet.HasCanSendMessage || (HasCanSendMessage && !CanSendMessage.Equals(channelPrivilegeSet.CanSendMessage)))
			{
				return false;
			}
			if (HasCanReceiveMessage != channelPrivilegeSet.HasCanReceiveMessage || (HasCanReceiveMessage && !CanReceiveMessage.Equals(channelPrivilegeSet.CanReceiveMessage)))
			{
				return false;
			}
			if (HasCanSetAttribute != channelPrivilegeSet.HasCanSetAttribute || (HasCanSetAttribute && !CanSetAttribute.Equals(channelPrivilegeSet.CanSetAttribute)))
			{
				return false;
			}
			if (HasCanSetOtherMemberAttribute != channelPrivilegeSet.HasCanSetOtherMemberAttribute || (HasCanSetOtherMemberAttribute && !CanSetOtherMemberAttribute.Equals(channelPrivilegeSet.CanSetOtherMemberAttribute)))
			{
				return false;
			}
			if (HasCanSetOwnMemberAttribute != channelPrivilegeSet.HasCanSetOwnMemberAttribute || (HasCanSetOwnMemberAttribute && !CanSetOwnMemberAttribute.Equals(channelPrivilegeSet.CanSetOwnMemberAttribute)))
			{
				return false;
			}
			if (HasCanEnterGame != channelPrivilegeSet.HasCanEnterGame || (HasCanEnterGame && !CanEnterGame.Equals(channelPrivilegeSet.CanEnterGame)))
			{
				return false;
			}
			if (HasCanSuggest != channelPrivilegeSet.HasCanSuggest || (HasCanSuggest && !CanSuggest.Equals(channelPrivilegeSet.CanSuggest)))
			{
				return false;
			}
			if (HasCanApprove != channelPrivilegeSet.HasCanApprove || (HasCanApprove && !CanApprove.Equals(channelPrivilegeSet.CanApprove)))
			{
				return false;
			}
			return true;
		}

		public static ChannelPrivilegeSet ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelPrivilegeSet>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ChannelPrivilegeSet Deserialize(Stream stream, ChannelPrivilegeSet instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ChannelPrivilegeSet DeserializeLengthDelimited(Stream stream)
		{
			ChannelPrivilegeSet channelPrivilegeSet = new ChannelPrivilegeSet();
			DeserializeLengthDelimited(stream, channelPrivilegeSet);
			return channelPrivilegeSet;
		}

		public static ChannelPrivilegeSet DeserializeLengthDelimited(Stream stream, ChannelPrivilegeSet instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ChannelPrivilegeSet Deserialize(Stream stream, ChannelPrivilegeSet instance, long limit)
		{
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
					instance.CanInvite = ProtocolParser.ReadBool(stream);
					continue;
				case 16:
					instance.CanInviteWithReservation = ProtocolParser.ReadBool(stream);
					continue;
				case 24:
					instance.CanRevokeOtherInvitation = ProtocolParser.ReadBool(stream);
					continue;
				case 32:
					instance.CanRevokeOwnInvitation = ProtocolParser.ReadBool(stream);
					continue;
				case 40:
					instance.CanKick = ProtocolParser.ReadBool(stream);
					continue;
				case 48:
					instance.CanDissolve = ProtocolParser.ReadBool(stream);
					continue;
				case 56:
					instance.CanSetPrivacy = ProtocolParser.ReadBool(stream);
					continue;
				case 64:
					instance.CanSendMessage = ProtocolParser.ReadBool(stream);
					continue;
				case 72:
					instance.CanReceiveMessage = ProtocolParser.ReadBool(stream);
					continue;
				case 80:
					instance.CanSetAttribute = ProtocolParser.ReadBool(stream);
					continue;
				case 88:
					instance.CanSetOtherMemberAttribute = ProtocolParser.ReadBool(stream);
					continue;
				case 96:
					instance.CanSetOwnMemberAttribute = ProtocolParser.ReadBool(stream);
					continue;
				case 104:
					instance.CanEnterGame = ProtocolParser.ReadBool(stream);
					continue;
				case 112:
					instance.CanSuggest = ProtocolParser.ReadBool(stream);
					continue;
				case 120:
					instance.CanApprove = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, ChannelPrivilegeSet instance)
		{
			if (instance.HasCanInvite)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.CanInvite);
			}
			if (instance.HasCanInviteWithReservation)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.CanInviteWithReservation);
			}
			if (instance.HasCanRevokeOtherInvitation)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.CanRevokeOtherInvitation);
			}
			if (instance.HasCanRevokeOwnInvitation)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.CanRevokeOwnInvitation);
			}
			if (instance.HasCanKick)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.CanKick);
			}
			if (instance.HasCanDissolve)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.CanDissolve);
			}
			if (instance.HasCanSetPrivacy)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.CanSetPrivacy);
			}
			if (instance.HasCanSendMessage)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteBool(stream, instance.CanSendMessage);
			}
			if (instance.HasCanReceiveMessage)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteBool(stream, instance.CanReceiveMessage);
			}
			if (instance.HasCanSetAttribute)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteBool(stream, instance.CanSetAttribute);
			}
			if (instance.HasCanSetOtherMemberAttribute)
			{
				stream.WriteByte(88);
				ProtocolParser.WriteBool(stream, instance.CanSetOtherMemberAttribute);
			}
			if (instance.HasCanSetOwnMemberAttribute)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteBool(stream, instance.CanSetOwnMemberAttribute);
			}
			if (instance.HasCanEnterGame)
			{
				stream.WriteByte(104);
				ProtocolParser.WriteBool(stream, instance.CanEnterGame);
			}
			if (instance.HasCanSuggest)
			{
				stream.WriteByte(112);
				ProtocolParser.WriteBool(stream, instance.CanSuggest);
			}
			if (instance.HasCanApprove)
			{
				stream.WriteByte(120);
				ProtocolParser.WriteBool(stream, instance.CanApprove);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasCanInvite)
			{
				num++;
				num++;
			}
			if (HasCanInviteWithReservation)
			{
				num++;
				num++;
			}
			if (HasCanRevokeOtherInvitation)
			{
				num++;
				num++;
			}
			if (HasCanRevokeOwnInvitation)
			{
				num++;
				num++;
			}
			if (HasCanKick)
			{
				num++;
				num++;
			}
			if (HasCanDissolve)
			{
				num++;
				num++;
			}
			if (HasCanSetPrivacy)
			{
				num++;
				num++;
			}
			if (HasCanSendMessage)
			{
				num++;
				num++;
			}
			if (HasCanReceiveMessage)
			{
				num++;
				num++;
			}
			if (HasCanSetAttribute)
			{
				num++;
				num++;
			}
			if (HasCanSetOtherMemberAttribute)
			{
				num++;
				num++;
			}
			if (HasCanSetOwnMemberAttribute)
			{
				num++;
				num++;
			}
			if (HasCanEnterGame)
			{
				num++;
				num++;
			}
			if (HasCanSuggest)
			{
				num++;
				num++;
			}
			if (HasCanApprove)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
