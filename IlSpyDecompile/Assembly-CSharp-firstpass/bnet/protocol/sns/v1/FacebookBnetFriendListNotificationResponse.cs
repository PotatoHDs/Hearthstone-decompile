using System.IO;

namespace bnet.protocol.sns.v1
{
	public class FacebookBnetFriendListNotificationResponse : IProtoBuf
	{
		public bool HasContinue;

		private bool _Continue;

		public bool Continue
		{
			get
			{
				return _Continue;
			}
			set
			{
				_Continue = value;
				HasContinue = true;
			}
		}

		public bool IsInitialized => true;

		public void SetContinue(bool val)
		{
			Continue = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasContinue)
			{
				num ^= Continue.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			FacebookBnetFriendListNotificationResponse facebookBnetFriendListNotificationResponse = obj as FacebookBnetFriendListNotificationResponse;
			if (facebookBnetFriendListNotificationResponse == null)
			{
				return false;
			}
			if (HasContinue != facebookBnetFriendListNotificationResponse.HasContinue || (HasContinue && !Continue.Equals(facebookBnetFriendListNotificationResponse.Continue)))
			{
				return false;
			}
			return true;
		}

		public static FacebookBnetFriendListNotificationResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FacebookBnetFriendListNotificationResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static FacebookBnetFriendListNotificationResponse Deserialize(Stream stream, FacebookBnetFriendListNotificationResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FacebookBnetFriendListNotificationResponse DeserializeLengthDelimited(Stream stream)
		{
			FacebookBnetFriendListNotificationResponse facebookBnetFriendListNotificationResponse = new FacebookBnetFriendListNotificationResponse();
			DeserializeLengthDelimited(stream, facebookBnetFriendListNotificationResponse);
			return facebookBnetFriendListNotificationResponse;
		}

		public static FacebookBnetFriendListNotificationResponse DeserializeLengthDelimited(Stream stream, FacebookBnetFriendListNotificationResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FacebookBnetFriendListNotificationResponse Deserialize(Stream stream, FacebookBnetFriendListNotificationResponse instance, long limit)
		{
			instance.Continue = true;
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
					instance.Continue = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, FacebookBnetFriendListNotificationResponse instance)
		{
			if (instance.HasContinue)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.Continue);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasContinue)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
