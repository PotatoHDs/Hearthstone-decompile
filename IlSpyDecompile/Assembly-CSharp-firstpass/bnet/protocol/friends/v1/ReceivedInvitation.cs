using System;
using System.IO;
using System.Text;

namespace bnet.protocol.friends.v1
{
	public class ReceivedInvitation : IProtoBuf
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

		public bool HasProgram;

		private uint _Program;

		public bool HasFriendInvitation;

		private FriendInvitation _FriendInvitation;

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

		public FriendInvitation FriendInvitation
		{
			get
			{
				return _FriendInvitation;
			}
			set
			{
				_FriendInvitation = value;
				HasFriendInvitation = value != null;
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

		public void SetProgram(uint val)
		{
			Program = val;
		}

		public void SetFriendInvitation(FriendInvitation val)
		{
			FriendInvitation = val;
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
			if (HasProgram)
			{
				hashCode ^= Program.GetHashCode();
			}
			if (HasFriendInvitation)
			{
				hashCode ^= FriendInvitation.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			ReceivedInvitation receivedInvitation = obj as ReceivedInvitation;
			if (receivedInvitation == null)
			{
				return false;
			}
			if (!Id.Equals(receivedInvitation.Id))
			{
				return false;
			}
			if (!InviterIdentity.Equals(receivedInvitation.InviterIdentity))
			{
				return false;
			}
			if (!InviteeIdentity.Equals(receivedInvitation.InviteeIdentity))
			{
				return false;
			}
			if (HasInviterName != receivedInvitation.HasInviterName || (HasInviterName && !InviterName.Equals(receivedInvitation.InviterName)))
			{
				return false;
			}
			if (HasInviteeName != receivedInvitation.HasInviteeName || (HasInviteeName && !InviteeName.Equals(receivedInvitation.InviteeName)))
			{
				return false;
			}
			if (HasInvitationMessage != receivedInvitation.HasInvitationMessage || (HasInvitationMessage && !InvitationMessage.Equals(receivedInvitation.InvitationMessage)))
			{
				return false;
			}
			if (HasCreationTime != receivedInvitation.HasCreationTime || (HasCreationTime && !CreationTime.Equals(receivedInvitation.CreationTime)))
			{
				return false;
			}
			if (HasExpirationTime != receivedInvitation.HasExpirationTime || (HasExpirationTime && !ExpirationTime.Equals(receivedInvitation.ExpirationTime)))
			{
				return false;
			}
			if (HasProgram != receivedInvitation.HasProgram || (HasProgram && !Program.Equals(receivedInvitation.Program)))
			{
				return false;
			}
			if (HasFriendInvitation != receivedInvitation.HasFriendInvitation || (HasFriendInvitation && !FriendInvitation.Equals(receivedInvitation.FriendInvitation)))
			{
				return false;
			}
			return true;
		}

		public static ReceivedInvitation ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ReceivedInvitation>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ReceivedInvitation Deserialize(Stream stream, ReceivedInvitation instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ReceivedInvitation DeserializeLengthDelimited(Stream stream)
		{
			ReceivedInvitation receivedInvitation = new ReceivedInvitation();
			DeserializeLengthDelimited(stream, receivedInvitation);
			return receivedInvitation;
		}

		public static ReceivedInvitation DeserializeLengthDelimited(Stream stream, ReceivedInvitation instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ReceivedInvitation Deserialize(Stream stream, ReceivedInvitation instance, long limit)
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
				case 77:
					instance.Program = binaryReader.ReadUInt32();
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					switch (key.Field)
					{
					case 0u:
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					case 103u:
						if (key.WireType == Wire.LengthDelimited)
						{
							if (instance.FriendInvitation == null)
							{
								instance.FriendInvitation = FriendInvitation.DeserializeLengthDelimited(stream);
							}
							else
							{
								FriendInvitation.DeserializeLengthDelimited(stream, instance.FriendInvitation);
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

		public static void Serialize(Stream stream, ReceivedInvitation instance)
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
			if (instance.HasProgram)
			{
				stream.WriteByte(77);
				binaryWriter.Write(instance.Program);
			}
			if (instance.HasFriendInvitation)
			{
				stream.WriteByte(186);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt32(stream, instance.FriendInvitation.GetSerializedSize());
				FriendInvitation.Serialize(stream, instance.FriendInvitation);
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
			if (HasProgram)
			{
				num++;
				num += 4;
			}
			if (HasFriendInvitation)
			{
				num += 2;
				uint serializedSize3 = FriendInvitation.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num + 3;
		}
	}
}
