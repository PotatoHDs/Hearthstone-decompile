using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.friends.v1
{
	public class ViewFriendsResponse : IProtoBuf
	{
		private List<FriendOfFriend> _Friends = new List<FriendOfFriend>();

		public List<FriendOfFriend> Friends
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

		public List<FriendOfFriend> FriendsList => _Friends;

		public int FriendsCount => _Friends.Count;

		public bool IsInitialized => true;

		public void AddFriends(FriendOfFriend val)
		{
			_Friends.Add(val);
		}

		public void ClearFriends()
		{
			_Friends.Clear();
		}

		public void SetFriends(List<FriendOfFriend> val)
		{
			Friends = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (FriendOfFriend friend in Friends)
			{
				num ^= friend.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ViewFriendsResponse viewFriendsResponse = obj as ViewFriendsResponse;
			if (viewFriendsResponse == null)
			{
				return false;
			}
			if (Friends.Count != viewFriendsResponse.Friends.Count)
			{
				return false;
			}
			for (int i = 0; i < Friends.Count; i++)
			{
				if (!Friends[i].Equals(viewFriendsResponse.Friends[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static ViewFriendsResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ViewFriendsResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ViewFriendsResponse Deserialize(Stream stream, ViewFriendsResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ViewFriendsResponse DeserializeLengthDelimited(Stream stream)
		{
			ViewFriendsResponse viewFriendsResponse = new ViewFriendsResponse();
			DeserializeLengthDelimited(stream, viewFriendsResponse);
			return viewFriendsResponse;
		}

		public static ViewFriendsResponse DeserializeLengthDelimited(Stream stream, ViewFriendsResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ViewFriendsResponse Deserialize(Stream stream, ViewFriendsResponse instance, long limit)
		{
			if (instance.Friends == null)
			{
				instance.Friends = new List<FriendOfFriend>();
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
					instance.Friends.Add(FriendOfFriend.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, ViewFriendsResponse instance)
		{
			if (instance.Friends.Count <= 0)
			{
				return;
			}
			foreach (FriendOfFriend friend in instance.Friends)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, friend.GetSerializedSize());
				FriendOfFriend.Serialize(stream, friend);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Friends.Count > 0)
			{
				foreach (FriendOfFriend friend in Friends)
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
