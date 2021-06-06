using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.friends.v2.client
{
	public class ViewFriendsResponse : IProtoBuf
	{
		private List<FriendOfFriend> _Friend = new List<FriendOfFriend>();

		public bool HasContinuation;

		private ulong _Continuation;

		public List<FriendOfFriend> Friend
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

		public List<FriendOfFriend> FriendList => _Friend;

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

		public void AddFriend(FriendOfFriend val)
		{
			_Friend.Add(val);
		}

		public void ClearFriend()
		{
			_Friend.Clear();
		}

		public void SetFriend(List<FriendOfFriend> val)
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
			foreach (FriendOfFriend item in Friend)
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
			ViewFriendsResponse viewFriendsResponse = obj as ViewFriendsResponse;
			if (viewFriendsResponse == null)
			{
				return false;
			}
			if (Friend.Count != viewFriendsResponse.Friend.Count)
			{
				return false;
			}
			for (int i = 0; i < Friend.Count; i++)
			{
				if (!Friend[i].Equals(viewFriendsResponse.Friend[i]))
				{
					return false;
				}
			}
			if (HasContinuation != viewFriendsResponse.HasContinuation || (HasContinuation && !Continuation.Equals(viewFriendsResponse.Continuation)))
			{
				return false;
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
			if (instance.Friend == null)
			{
				instance.Friend = new List<FriendOfFriend>();
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
					instance.Friend.Add(FriendOfFriend.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, ViewFriendsResponse instance)
		{
			if (instance.Friend.Count > 0)
			{
				foreach (FriendOfFriend item in instance.Friend)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					FriendOfFriend.Serialize(stream, item);
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
				foreach (FriendOfFriend item in Friend)
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
