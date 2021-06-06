using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.friends.v2.client
{
	public class GetFriendsResponse : IProtoBuf
	{
		private List<Friend> _Friend = new List<Friend>();

		public bool HasContinuation;

		private ulong _Continuation;

		public List<Friend> Friend
		{
			get
			{
				return _Friend;
			}
			set
			{
				_Friend = value;
			}
		}

		public List<Friend> FriendList => _Friend;

		public int FriendCount => _Friend.Count;

		public ulong Continuation
		{
			get
			{
				return _Continuation;
			}
			set
			{
				_Continuation = value;
				HasContinuation = true;
			}
		}

		public bool IsInitialized => true;

		public void AddFriend(Friend val)
		{
			_Friend.Add(val);
		}

		public void ClearFriend()
		{
			_Friend.Clear();
		}

		public void SetFriend(List<Friend> val)
		{
			Friend = val;
		}

		public void SetContinuation(ulong val)
		{
			Continuation = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (Friend item in Friend)
			{
				num ^= item.GetHashCode();
			}
			if (HasContinuation)
			{
				num ^= Continuation.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetFriendsResponse getFriendsResponse = obj as GetFriendsResponse;
			if (getFriendsResponse == null)
			{
				return false;
			}
			if (Friend.Count != getFriendsResponse.Friend.Count)
			{
				return false;
			}
			for (int i = 0; i < Friend.Count; i++)
			{
				if (!Friend[i].Equals(getFriendsResponse.Friend[i]))
				{
					return false;
				}
			}
			if (HasContinuation != getFriendsResponse.HasContinuation || (HasContinuation && !Continuation.Equals(getFriendsResponse.Continuation)))
			{
				return false;
			}
			return true;
		}

		public static GetFriendsResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetFriendsResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetFriendsResponse Deserialize(Stream stream, GetFriendsResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetFriendsResponse DeserializeLengthDelimited(Stream stream)
		{
			GetFriendsResponse getFriendsResponse = new GetFriendsResponse();
			DeserializeLengthDelimited(stream, getFriendsResponse);
			return getFriendsResponse;
		}

		public static GetFriendsResponse DeserializeLengthDelimited(Stream stream, GetFriendsResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetFriendsResponse Deserialize(Stream stream, GetFriendsResponse instance, long limit)
		{
			if (instance.Friend == null)
			{
				instance.Friend = new List<Friend>();
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
					instance.Friend.Add(bnet.protocol.friends.v2.client.Friend.DeserializeLengthDelimited(stream));
					continue;
				case 16:
					instance.Continuation = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, GetFriendsResponse instance)
		{
			if (instance.Friend.Count > 0)
			{
				foreach (Friend item in instance.Friend)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.friends.v2.client.Friend.Serialize(stream, item);
				}
			}
			if (instance.HasContinuation)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.Continuation);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Friend.Count > 0)
			{
				foreach (Friend item in Friend)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasContinuation)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(Continuation);
			}
			return num;
		}
	}
}
