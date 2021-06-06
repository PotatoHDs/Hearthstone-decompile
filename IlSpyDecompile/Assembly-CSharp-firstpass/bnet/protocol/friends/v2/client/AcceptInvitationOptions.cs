using System.IO;
using bnet.protocol.friends.v2.client.Types;

namespace bnet.protocol.friends.v2.client
{
	public class AcceptInvitationOptions : IProtoBuf
	{
		public bool HasLevel;

		private FriendLevel _Level;

		public FriendLevel Level
		{
			get
			{
				return _Level;
			}
			set
			{
				_Level = value;
				HasLevel = true;
			}
		}

		public bool IsInitialized => true;

		public void SetLevel(FriendLevel val)
		{
			Level = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasLevel)
			{
				num ^= Level.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AcceptInvitationOptions acceptInvitationOptions = obj as AcceptInvitationOptions;
			if (acceptInvitationOptions == null)
			{
				return false;
			}
			if (HasLevel != acceptInvitationOptions.HasLevel || (HasLevel && !Level.Equals(acceptInvitationOptions.Level)))
			{
				return false;
			}
			return true;
		}

		public static AcceptInvitationOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AcceptInvitationOptions>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AcceptInvitationOptions Deserialize(Stream stream, AcceptInvitationOptions instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AcceptInvitationOptions DeserializeLengthDelimited(Stream stream)
		{
			AcceptInvitationOptions acceptInvitationOptions = new AcceptInvitationOptions();
			DeserializeLengthDelimited(stream, acceptInvitationOptions);
			return acceptInvitationOptions;
		}

		public static AcceptInvitationOptions DeserializeLengthDelimited(Stream stream, AcceptInvitationOptions instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AcceptInvitationOptions Deserialize(Stream stream, AcceptInvitationOptions instance, long limit)
		{
			instance.Level = FriendLevel.FRIEND_LEVEL_BATTLE_TAG;
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
					instance.Level = (FriendLevel)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, AcceptInvitationOptions instance)
		{
			if (instance.HasLevel)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Level);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasLevel)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Level);
			}
			return num;
		}
	}
}
