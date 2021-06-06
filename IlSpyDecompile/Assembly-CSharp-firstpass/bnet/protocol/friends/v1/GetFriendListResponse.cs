using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.friends.v1
{
	public class GetFriendListResponse : IProtoBuf
	{
		private List<Friend> _Friends = new List<Friend>();

		public List<Friend> Friends
		{
			get
			{
				return _Friends;
			}
			set
			{
				_Friends = value;
			}
		}

		public List<Friend> FriendsList => _Friends;

		public int FriendsCount => _Friends.Count;

		public bool IsInitialized => true;

		public void AddFriends(Friend val)
		{
			_Friends.Add(val);
		}

		public void ClearFriends()
		{
			_Friends.Clear();
		}

		public void SetFriends(List<Friend> val)
		{
			Friends = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (Friend friend in Friends)
			{
				num ^= friend.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetFriendListResponse getFriendListResponse = obj as GetFriendListResponse;
			if (getFriendListResponse == null)
			{
				return false;
			}
			if (Friends.Count != getFriendListResponse.Friends.Count)
			{
				return false;
			}
			for (int i = 0; i < Friends.Count; i++)
			{
				if (!Friends[i].Equals(getFriendListResponse.Friends[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static GetFriendListResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetFriendListResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetFriendListResponse Deserialize(Stream stream, GetFriendListResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetFriendListResponse DeserializeLengthDelimited(Stream stream)
		{
			GetFriendListResponse getFriendListResponse = new GetFriendListResponse();
			DeserializeLengthDelimited(stream, getFriendListResponse);
			return getFriendListResponse;
		}

		public static GetFriendListResponse DeserializeLengthDelimited(Stream stream, GetFriendListResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetFriendListResponse Deserialize(Stream stream, GetFriendListResponse instance, long limit)
		{
			if (instance.Friends == null)
			{
				instance.Friends = new List<Friend>();
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
					instance.Friends.Add(Friend.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, GetFriendListResponse instance)
		{
			if (instance.Friends.Count <= 0)
			{
				return;
			}
			foreach (Friend friend in instance.Friends)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, friend.GetSerializedSize());
				Friend.Serialize(stream, friend);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Friends.Count > 0)
			{
				foreach (Friend friend in Friends)
				{
					num++;
					uint serializedSize = friend.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
				return num;
			}
			return num;
		}
	}
}
