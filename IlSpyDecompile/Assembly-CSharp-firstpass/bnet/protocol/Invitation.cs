using System;
using System.IO;
using System.Text;
using bnet.protocol.channel.v1;

namespace bnet.protocol
{
	public class Invitation : IProtoBuf
	{
		public bool HasInviterName;

		private string _InviterName;

		public bool HasInviteeName;

		private string _InviteeName;

		public bool HasInvitationMessage;

		private string _InvitationMessage;

		public bool HasCreationTime;

		private ulong _CreationTime;

		public bool HasExpirationTime;

		private ulong _ExpirationTime;

		public bool HasChannelInvitation;

		private ChannelInvitation _ChannelInvitation;

		public ulong Id { get; set; }

		public Identity InviterIdentity { get; set; }

		public Identity InviteeIdentity { get; set; }

		public string InviterName
		{
			get
			{
				return _InviterName;
			}
			set
			{
				_InviterName = value;
				HasInviterName = value != null;
			}
		}

		public string InviteeName
		{
			get
			{
				return _InviteeName;
			}
			set
			{
				_InviteeName = value;
				HasInviteeName = value != null;
			}
		}

		public string InvitationMessage
		{
			get
			{
				return _InvitationMessage;
			}
			set
			{
				_InvitationMessage = value;
				HasInvitationMessage = value != null;
			}
		}

		public ulong CreationTime
		{
			get
			{
				return _CreationTime;
			}
			set
			{
				_CreationTime = value;
				HasCreationTime = true;
			}
		}

		public ulong ExpirationTime
		{
			get
			{
				return _ExpirationTime;
			}
			set
			{
				_ExpirationTime = value;
				HasExpirationTime = true;
			}
		}

		public ChannelInvitation ChannelInvitation
		{
			get
			{
				return _ChannelInvitation;
			}
			set
			{
				_ChannelInvitation = value;
				HasChannelInvitation = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetId(ulong val)
		{
			Id = val;
		}

		public void SetInviterIdentity(Identity val)
		{
			InviterIdentity = val;
		}

		public void SetInviteeIdentity(Identity val)
		{
			InviteeIdentity = val;
		}

		public void SetInviterName(string val)
		{
			InviterName = val;
		}

		public void SetInviteeName(string val)
		{
			InviteeName = val;
		}

		public void SetInvitationMessage(string val)
		{
			InvitationMessage = val;
		}

		public void SetCreationTime(ulong val)
		{
			CreationTime = val;
		}

		public void SetExpirationTime(ulong val)
		{
			ExpirationTime = val;
		}

		public void SetChannelInvitation(ChannelInvitation val)
		{
			ChannelInvitation = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Id.GetHashCode();
			hashCode ^= InviterIdentity.GetHashCode();
			hashCode ^= InviteeIdentity.GetHashCode();
			if (HasInviterName)
			{
				hashCode ^= InviterName.GetHashCode();
			}
			if (HasInviteeName)
			{
				hashCode ^= InviteeName.GetHashCode();
			}
			if (HasInvitationMessage)
			{
				hashCode ^= InvitationMessage.GetHashCode();
			}
			if (HasCreationTime)
			{
				hashCode ^= CreationTime.GetHashCode();
			}
			if (HasExpirationTime)
			{
				hashCode ^= ExpirationTime.GetHashCode();
			}
			if (HasChannelInvitation)
			{
				hashCode ^= ChannelInvitation.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			Invitation invitation = obj as Invitation;
			if (invitation == null)
			{
				return false;
			}
			if (!Id.Equals(invitation.Id))
			{
				return false;
			}
			if (!InviterIdentity.Equals(invitation.InviterIdentity))
			{
				return false;
			}
			if (!InviteeIdentity.Equals(invitation.InviteeIdentity))
			{
				return false;
			}
			if (HasInviterName != invitation.HasInviterName || (HasInviterName && !InviterName.Equals(invitation.InviterName)))
			{
				return false;
			}
			if (HasInviteeName != invitation.HasInviteeName || (HasInviteeName && !InviteeName.Equals(invitation.InviteeName)))
			{
				return false;
			}
			if (HasInvitationMessage != invitation.HasInvitationMessage || (HasInvitationMessage && !InvitationMessage.Equals(invitation.InvitationMessage)))
			{
				return false;
			}
			if (HasCreationTime != invitation.HasCreationTime || (HasCreationTime && !CreationTime.Equals(invitation.CreationTime)))
			{
				return false;
			}
			if (HasExpirationTime != invitation.HasExpirationTime || (HasExpirationTime && !ExpirationTime.Equals(invitation.ExpirationTime)))
			{
				return false;
			}
			if (HasChannelInvitation != invitation.HasChannelInvitation || (HasChannelInvitation && !ChannelInvitation.Equals(invitation.ChannelInvitation)))
			{
				return false;
			}
			return true;
		}

		public static Invitation ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Invitation>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Invitation Deserialize(Stream stream, Invitation instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Invitation DeserializeLengthDelimited(Stream stream)
		{
			Invitation invitation = new Invitation();
			DeserializeLengthDelimited(stream, invitation);
			return invitation;
		}

		public static Invitation DeserializeLengthDelimited(Stream stream, Invitation instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Invitation Deserialize(Stream stream, Invitation instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
				case 9:
					instance.Id = binaryReader.ReadUInt64();
					continue;
				case 18:
					if (instance.InviterIdentity == null)
					{
						instance.InviterIdentity = Identity.DeserializeLengthDelimited(stream);
					}
					else
					{
						Identity.DeserializeLengthDelimited(stream, instance.InviterIdentity);
					}
					continue;
				case 26:
					if (instance.InviteeIdentity == null)
					{
						instance.InviteeIdentity = Identity.DeserializeLengthDelimited(stream);
					}
					else
					{
						Identity.DeserializeLengthDelimited(stream, instance.InviteeIdentity);
					}
					continue;
				case 34:
					instance.InviterName = ProtocolParser.ReadString(stream);
					continue;
				case 42:
					instance.InviteeName = ProtocolParser.ReadString(stream);
					continue;
				case 50:
					instance.InvitationMessage = ProtocolParser.ReadString(stream);
					continue;
				case 56:
					instance.CreationTime = ProtocolParser.ReadUInt64(stream);
					continue;
				case 64:
					instance.ExpirationTime = ProtocolParser.ReadUInt64(stream);
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					switch (key.Field)
					{
					case 0u:
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					case 105u:
						if (key.WireType == Wire.LengthDelimited)
						{
							if (instance.ChannelInvitation == null)
							{
								instance.ChannelInvitation = ChannelInvitation.DeserializeLengthDelimited(stream);
							}
							else
							{
								ChannelInvitation.DeserializeLengthDelimited(stream, instance.ChannelInvitation);
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

		public static void Serialize(Stream stream, Invitation instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(9);
			binaryWriter.Write(instance.Id);
			if (instance.InviterIdentity == null)
			{
				throw new ArgumentNullException("InviterIdentity", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.InviterIdentity.GetSerializedSize());
			Identity.Serialize(stream, instance.InviterIdentity);
			if (instance.InviteeIdentity == null)
			{
				throw new ArgumentNullException("InviteeIdentity", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteUInt32(stream, instance.InviteeIdentity.GetSerializedSize());
			Identity.Serialize(stream, instance.InviteeIdentity);
			if (instance.HasInviterName)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.InviterName));
			}
			if (instance.HasInviteeName)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.InviteeName));
			}
			if (instance.HasInvitationMessage)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.InvitationMessage));
			}
			if (instance.HasCreationTime)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, instance.CreationTime);
			}
			if (instance.HasExpirationTime)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, instance.ExpirationTime);
			}
			if (instance.HasChannelInvitation)
			{
				stream.WriteByte(202);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt32(stream, instance.ChannelInvitation.GetSerializedSize());
				ChannelInvitation.Serialize(stream, instance.ChannelInvitation);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += 8;
			uint serializedSize = InviterIdentity.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			uint serializedSize2 = InviteeIdentity.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			if (HasInviterName)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(InviterName);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasInviteeName)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(InviteeName);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasInvitationMessage)
			{
				num++;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(InvitationMessage);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (HasCreationTime)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(CreationTime);
			}
			if (HasExpirationTime)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(ExpirationTime);
			}
			if (HasChannelInvitation)
			{
				num += 2;
				uint serializedSize3 = ChannelInvitation.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num + 3;
		}
	}
}
