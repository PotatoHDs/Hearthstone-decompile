using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.friends.v2.client
{
	public class FriendsState : IProtoBuf
	{
		private List<ReceivedInvitation> _ReceivedInvitations = new List<ReceivedInvitation>();

		private List<SentInvitation> _SentInvitations = new List<SentInvitation>();

		public List<ReceivedInvitation> ReceivedInvitations
		{
			get
			{
				return _ReceivedInvitations;
			}
			set
			{
				_ReceivedInvitations = value;
			}
		}

		public List<ReceivedInvitation> ReceivedInvitationsList => _ReceivedInvitations;

		public int ReceivedInvitationsCount => _ReceivedInvitations.Count;

		public List<SentInvitation> SentInvitations
		{
			get
			{
				return _SentInvitations;
			}
			set
			{
				_SentInvitations = value;
			}
		}

		public List<SentInvitation> SentInvitationsList => _SentInvitations;

		public int SentInvitationsCount => _SentInvitations.Count;

		public bool IsInitialized => true;

		public void AddReceivedInvitations(ReceivedInvitation val)
		{
			_ReceivedInvitations.Add(val);
		}

		public void ClearReceivedInvitations()
		{
			_ReceivedInvitations.Clear();
		}

		public void SetReceivedInvitations(List<ReceivedInvitation> val)
		{
			ReceivedInvitations = val;
		}

		public void AddSentInvitations(SentInvitation val)
		{
			_SentInvitations.Add(val);
		}

		public void ClearSentInvitations()
		{
			_SentInvitations.Clear();
		}

		public void SetSentInvitations(List<SentInvitation> val)
		{
			SentInvitations = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (ReceivedInvitation receivedInvitation in ReceivedInvitations)
			{
				num ^= receivedInvitation.GetHashCode();
			}
			foreach (SentInvitation sentInvitation in SentInvitations)
			{
				num ^= sentInvitation.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			FriendsState friendsState = obj as FriendsState;
			if (friendsState == null)
			{
				return false;
			}
			if (ReceivedInvitations.Count != friendsState.ReceivedInvitations.Count)
			{
				return false;
			}
			for (int i = 0; i < ReceivedInvitations.Count; i++)
			{
				if (!ReceivedInvitations[i].Equals(friendsState.ReceivedInvitations[i]))
				{
					return false;
				}
			}
			if (SentInvitations.Count != friendsState.SentInvitations.Count)
			{
				return false;
			}
			for (int j = 0; j < SentInvitations.Count; j++)
			{
				if (!SentInvitations[j].Equals(friendsState.SentInvitations[j]))
				{
					return false;
				}
			}
			return true;
		}

		public static FriendsState ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FriendsState>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static FriendsState Deserialize(Stream stream, FriendsState instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FriendsState DeserializeLengthDelimited(Stream stream)
		{
			FriendsState friendsState = new FriendsState();
			DeserializeLengthDelimited(stream, friendsState);
			return friendsState;
		}

		public static FriendsState DeserializeLengthDelimited(Stream stream, FriendsState instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FriendsState Deserialize(Stream stream, FriendsState instance, long limit)
		{
			if (instance.ReceivedInvitations == null)
			{
				instance.ReceivedInvitations = new List<ReceivedInvitation>();
			}
			if (instance.SentInvitations == null)
			{
				instance.SentInvitations = new List<SentInvitation>();
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
				case 18:
					instance.ReceivedInvitations.Add(ReceivedInvitation.DeserializeLengthDelimited(stream));
					continue;
				case 26:
					instance.SentInvitations.Add(SentInvitation.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, FriendsState instance)
		{
			if (instance.ReceivedInvitations.Count > 0)
			{
				foreach (ReceivedInvitation receivedInvitation in instance.ReceivedInvitations)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, receivedInvitation.GetSerializedSize());
					ReceivedInvitation.Serialize(stream, receivedInvitation);
				}
			}
			if (instance.SentInvitations.Count <= 0)
			{
				return;
			}
			foreach (SentInvitation sentInvitation in instance.SentInvitations)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, sentInvitation.GetSerializedSize());
				SentInvitation.Serialize(stream, sentInvitation);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (ReceivedInvitations.Count > 0)
			{
				foreach (ReceivedInvitation receivedInvitation in ReceivedInvitations)
				{
					num++;
					uint serializedSize = receivedInvitation.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (SentInvitations.Count > 0)
			{
				foreach (SentInvitation sentInvitation in SentInvitations)
				{
					num++;
					uint serializedSize2 = sentInvitation.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
				return num;
			}
			return num;
		}
	}
}
