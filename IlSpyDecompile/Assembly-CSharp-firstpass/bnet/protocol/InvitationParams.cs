using System.IO;
using System.Text;
using bnet.protocol.channel.v1;
using bnet.protocol.friends.v1;

namespace bnet.protocol
{
	public class InvitationParams : IProtoBuf
	{
		public bool HasInvitationMessage;

		private string _InvitationMessage;

		public bool HasExpirationTime;

		private ulong _ExpirationTime;

		public bool HasChannelParams;

		private ChannelInvitationParams _ChannelParams;

		public bool HasFriendParams;

		private FriendInvitationParams _FriendParams;

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

		public ChannelInvitationParams ChannelParams
		{
			get
			{
				return _ChannelParams;
			}
			set
			{
				_ChannelParams = value;
				HasChannelParams = value != null;
			}
		}

		public FriendInvitationParams FriendParams
		{
			get
			{
				return _FriendParams;
			}
			set
			{
				_FriendParams = value;
				HasFriendParams = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetInvitationMessage(string val)
		{
			InvitationMessage = val;
		}

		public void SetExpirationTime(ulong val)
		{
			ExpirationTime = val;
		}

		public void SetChannelParams(ChannelInvitationParams val)
		{
			ChannelParams = val;
		}

		public void SetFriendParams(FriendInvitationParams val)
		{
			FriendParams = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasInvitationMessage)
			{
				num ^= InvitationMessage.GetHashCode();
			}
			if (HasExpirationTime)
			{
				num ^= ExpirationTime.GetHashCode();
			}
			if (HasChannelParams)
			{
				num ^= ChannelParams.GetHashCode();
			}
			if (HasFriendParams)
			{
				num ^= FriendParams.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			InvitationParams invitationParams = obj as InvitationParams;
			if (invitationParams == null)
			{
				return false;
			}
			if (HasInvitationMessage != invitationParams.HasInvitationMessage || (HasInvitationMessage && !InvitationMessage.Equals(invitationParams.InvitationMessage)))
			{
				return false;
			}
			if (HasExpirationTime != invitationParams.HasExpirationTime || (HasExpirationTime && !ExpirationTime.Equals(invitationParams.ExpirationTime)))
			{
				return false;
			}
			if (HasChannelParams != invitationParams.HasChannelParams || (HasChannelParams && !ChannelParams.Equals(invitationParams.ChannelParams)))
			{
				return false;
			}
			if (HasFriendParams != invitationParams.HasFriendParams || (HasFriendParams && !FriendParams.Equals(invitationParams.FriendParams)))
			{
				return false;
			}
			return true;
		}

		public static InvitationParams ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<InvitationParams>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static InvitationParams Deserialize(Stream stream, InvitationParams instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static InvitationParams DeserializeLengthDelimited(Stream stream)
		{
			InvitationParams invitationParams = new InvitationParams();
			DeserializeLengthDelimited(stream, invitationParams);
			return invitationParams;
		}

		public static InvitationParams DeserializeLengthDelimited(Stream stream, InvitationParams instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static InvitationParams Deserialize(Stream stream, InvitationParams instance, long limit)
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
				case 10:
					instance.InvitationMessage = ProtocolParser.ReadString(stream);
					continue;
				case 16:
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
							if (instance.ChannelParams == null)
							{
								instance.ChannelParams = ChannelInvitationParams.DeserializeLengthDelimited(stream);
							}
							else
							{
								ChannelInvitationParams.DeserializeLengthDelimited(stream, instance.ChannelParams);
							}
						}
						break;
					case 103u:
						if (key.WireType == Wire.LengthDelimited)
						{
							if (instance.FriendParams == null)
							{
								instance.FriendParams = FriendInvitationParams.DeserializeLengthDelimited(stream);
							}
							else
							{
								FriendInvitationParams.DeserializeLengthDelimited(stream, instance.FriendParams);
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

		public static void Serialize(Stream stream, InvitationParams instance)
		{
			if (instance.HasInvitationMessage)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.InvitationMessage));
			}
			if (instance.HasExpirationTime)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.ExpirationTime);
			}
			if (instance.HasChannelParams)
			{
				stream.WriteByte(202);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt32(stream, instance.ChannelParams.GetSerializedSize());
				ChannelInvitationParams.Serialize(stream, instance.ChannelParams);
			}
			if (instance.HasFriendParams)
			{
				stream.WriteByte(186);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt32(stream, instance.FriendParams.GetSerializedSize());
				FriendInvitationParams.Serialize(stream, instance.FriendParams);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasInvitationMessage)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(InvitationMessage);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasExpirationTime)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(ExpirationTime);
			}
			if (HasChannelParams)
			{
				num += 2;
				uint serializedSize = ChannelParams.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasFriendParams)
			{
				num += 2;
				uint serializedSize2 = FriendParams.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}
	}
}
