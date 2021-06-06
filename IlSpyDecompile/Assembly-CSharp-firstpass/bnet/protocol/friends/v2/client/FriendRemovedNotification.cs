using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.friends.v2.client
{
	public class FriendRemovedNotification : IProtoBuf
	{
		public bool HasAgentAccountId;

		private ulong _AgentAccountId;

		private List<RemovedFriendAssignment> _Assignment = new List<RemovedFriendAssignment>();

		public ulong AgentAccountId
		{
			get
			{
				return _AgentAccountId;
			}
			set
			{
				_AgentAccountId = value;
				HasAgentAccountId = true;
			}
		}

		public List<RemovedFriendAssignment> Assignment
		{
			get
			{
				return _Assignment;
			}
			set
			{
				_Assignment = value;
			}
		}

		public List<RemovedFriendAssignment> AssignmentList => _Assignment;

		public int AssignmentCount => _Assignment.Count;

		public bool IsInitialized => true;

		public void SetAgentAccountId(ulong val)
		{
			AgentAccountId = val;
		}

		public void AddAssignment(RemovedFriendAssignment val)
		{
			_Assignment.Add(val);
		}

		public void ClearAssignment()
		{
			_Assignment.Clear();
		}

		public void SetAssignment(List<RemovedFriendAssignment> val)
		{
			Assignment = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAgentAccountId)
			{
				num ^= AgentAccountId.GetHashCode();
			}
			foreach (RemovedFriendAssignment item in Assignment)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			FriendRemovedNotification friendRemovedNotification = obj as FriendRemovedNotification;
			if (friendRemovedNotification == null)
			{
				return false;
			}
			if (HasAgentAccountId != friendRemovedNotification.HasAgentAccountId || (HasAgentAccountId && !AgentAccountId.Equals(friendRemovedNotification.AgentAccountId)))
			{
				return false;
			}
			if (Assignment.Count != friendRemovedNotification.Assignment.Count)
			{
				return false;
			}
			for (int i = 0; i < Assignment.Count; i++)
			{
				if (!Assignment[i].Equals(friendRemovedNotification.Assignment[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static FriendRemovedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FriendRemovedNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static FriendRemovedNotification Deserialize(Stream stream, FriendRemovedNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FriendRemovedNotification DeserializeLengthDelimited(Stream stream)
		{
			FriendRemovedNotification friendRemovedNotification = new FriendRemovedNotification();
			DeserializeLengthDelimited(stream, friendRemovedNotification);
			return friendRemovedNotification;
		}

		public static FriendRemovedNotification DeserializeLengthDelimited(Stream stream, FriendRemovedNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FriendRemovedNotification Deserialize(Stream stream, FriendRemovedNotification instance, long limit)
		{
			if (instance.Assignment == null)
			{
				instance.Assignment = new List<RemovedFriendAssignment>();
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
				case 8:
					instance.AgentAccountId = ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					instance.Assignment.Add(RemovedFriendAssignment.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, FriendRemovedNotification instance)
		{
			if (instance.HasAgentAccountId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.AgentAccountId);
			}
			if (instance.Assignment.Count <= 0)
			{
				return;
			}
			foreach (RemovedFriendAssignment item in instance.Assignment)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				RemovedFriendAssignment.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAgentAccountId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(AgentAccountId);
			}
			if (Assignment.Count > 0)
			{
				foreach (RemovedFriendAssignment item in Assignment)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
				return num;
			}
			return num;
		}
	}
}
