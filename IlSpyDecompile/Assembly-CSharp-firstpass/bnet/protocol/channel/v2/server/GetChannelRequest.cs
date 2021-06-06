using System.IO;
using bnet.protocol.channel.v1;

namespace bnet.protocol.channel.v2.server
{
	public class GetChannelRequest : IProtoBuf
	{
		public bool HasChannelId;

		private bnet.protocol.channel.v1.ChannelId _ChannelId;

		public bool HasFetchAttributes;

		private bool _FetchAttributes;

		public bool HasFetchMembers;

		private bool _FetchMembers;

		public bool HasFetchInvitations;

		private bool _FetchInvitations;

		public bool HasFetchRoles;

		private bool _FetchRoles;

		public bnet.protocol.channel.v1.ChannelId ChannelId
		{
			get
			{
				return _ChannelId;
			}
			set
			{
				_ChannelId = value;
				HasChannelId = value != null;
			}
		}

		public bool FetchAttributes
		{
			get
			{
				return _FetchAttributes;
			}
			set
			{
				_FetchAttributes = value;
				HasFetchAttributes = true;
			}
		}

		public bool FetchMembers
		{
			get
			{
				return _FetchMembers;
			}
			set
			{
				_FetchMembers = value;
				HasFetchMembers = true;
			}
		}

		public bool FetchInvitations
		{
			get
			{
				return _FetchInvitations;
			}
			set
			{
				_FetchInvitations = value;
				HasFetchInvitations = true;
			}
		}

		public bool FetchRoles
		{
			get
			{
				return _FetchRoles;
			}
			set
			{
				_FetchRoles = value;
				HasFetchRoles = true;
			}
		}

		public bool IsInitialized => true;

		public void SetChannelId(bnet.protocol.channel.v1.ChannelId val)
		{
			ChannelId = val;
		}

		public void SetFetchAttributes(bool val)
		{
			FetchAttributes = val;
		}

		public void SetFetchMembers(bool val)
		{
			FetchMembers = val;
		}

		public void SetFetchInvitations(bool val)
		{
			FetchInvitations = val;
		}

		public void SetFetchRoles(bool val)
		{
			FetchRoles = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasChannelId)
			{
				num ^= ChannelId.GetHashCode();
			}
			if (HasFetchAttributes)
			{
				num ^= FetchAttributes.GetHashCode();
			}
			if (HasFetchMembers)
			{
				num ^= FetchMembers.GetHashCode();
			}
			if (HasFetchInvitations)
			{
				num ^= FetchInvitations.GetHashCode();
			}
			if (HasFetchRoles)
			{
				num ^= FetchRoles.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetChannelRequest getChannelRequest = obj as GetChannelRequest;
			if (getChannelRequest == null)
			{
				return false;
			}
			if (HasChannelId != getChannelRequest.HasChannelId || (HasChannelId && !ChannelId.Equals(getChannelRequest.ChannelId)))
			{
				return false;
			}
			if (HasFetchAttributes != getChannelRequest.HasFetchAttributes || (HasFetchAttributes && !FetchAttributes.Equals(getChannelRequest.FetchAttributes)))
			{
				return false;
			}
			if (HasFetchMembers != getChannelRequest.HasFetchMembers || (HasFetchMembers && !FetchMembers.Equals(getChannelRequest.FetchMembers)))
			{
				return false;
			}
			if (HasFetchInvitations != getChannelRequest.HasFetchInvitations || (HasFetchInvitations && !FetchInvitations.Equals(getChannelRequest.FetchInvitations)))
			{
				return false;
			}
			if (HasFetchRoles != getChannelRequest.HasFetchRoles || (HasFetchRoles && !FetchRoles.Equals(getChannelRequest.FetchRoles)))
			{
				return false;
			}
			return true;
		}

		public static GetChannelRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetChannelRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetChannelRequest Deserialize(Stream stream, GetChannelRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetChannelRequest DeserializeLengthDelimited(Stream stream)
		{
			GetChannelRequest getChannelRequest = new GetChannelRequest();
			DeserializeLengthDelimited(stream, getChannelRequest);
			return getChannelRequest;
		}

		public static GetChannelRequest DeserializeLengthDelimited(Stream stream, GetChannelRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetChannelRequest Deserialize(Stream stream, GetChannelRequest instance, long limit)
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
				case 18:
					if (instance.ChannelId == null)
					{
						instance.ChannelId = bnet.protocol.channel.v1.ChannelId.DeserializeLengthDelimited(stream);
					}
					else
					{
						bnet.protocol.channel.v1.ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
					}
					continue;
				case 40:
					instance.FetchAttributes = ProtocolParser.ReadBool(stream);
					continue;
				case 48:
					instance.FetchMembers = ProtocolParser.ReadBool(stream);
					continue;
				case 56:
					instance.FetchInvitations = ProtocolParser.ReadBool(stream);
					continue;
				case 64:
					instance.FetchRoles = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, GetChannelRequest instance)
		{
			if (instance.HasChannelId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				bnet.protocol.channel.v1.ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasFetchAttributes)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.FetchAttributes);
			}
			if (instance.HasFetchMembers)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.FetchMembers);
			}
			if (instance.HasFetchInvitations)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.FetchInvitations);
			}
			if (instance.HasFetchRoles)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteBool(stream, instance.FetchRoles);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasChannelId)
			{
				num++;
				uint serializedSize = ChannelId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasFetchAttributes)
			{
				num++;
				num++;
			}
			if (HasFetchMembers)
			{
				num++;
				num++;
			}
			if (HasFetchInvitations)
			{
				num++;
				num++;
			}
			if (HasFetchRoles)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
