using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.friends.v2.client
{
	public class UpdateFriendStateNotification : IProtoBuf
	{
		public bool HasAgentAccountId;

		private ulong _AgentAccountId;

		private List<FriendStateAssignment> _Assignment = new List<FriendStateAssignment>();

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

		public List<FriendStateAssignment> Assignment
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

		public List<FriendStateAssignment> AssignmentList => _Assignment;

		public int AssignmentCount => _Assignment.Count;

		public bool IsInitialized => true;

		public void SetAgentAccountId(ulong val)
		{
			AgentAccountId = val;
		}

		public void AddAssignment(FriendStateAssignment val)
		{
			_Assignment.Add(val);
		}

		public void ClearAssignment()
		{
			_Assignment.Clear();
		}

		public void SetAssignment(List<FriendStateAssignment> val)
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
			foreach (FriendStateAssignment item in Assignment)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			UpdateFriendStateNotification updateFriendStateNotification = obj as UpdateFriendStateNotification;
			if (updateFriendStateNotification == null)
			{
				return false;
			}
			if (HasAgentAccountId != updateFriendStateNotification.HasAgentAccountId || (HasAgentAccountId && !AgentAccountId.Equals(updateFriendStateNotification.AgentAccountId)))
			{
				return false;
			}
			if (Assignment.Count != updateFriendStateNotification.Assignment.Count)
			{
				return false;
			}
			for (int i = 0; i < Assignment.Count; i++)
			{
				if (!Assignment[i].Equals(updateFriendStateNotification.Assignment[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static UpdateFriendStateNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateFriendStateNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static UpdateFriendStateNotification Deserialize(Stream stream, UpdateFriendStateNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static UpdateFriendStateNotification DeserializeLengthDelimited(Stream stream)
		{
			UpdateFriendStateNotification updateFriendStateNotification = new UpdateFriendStateNotification();
			DeserializeLengthDelimited(stream, updateFriendStateNotification);
			return updateFriendStateNotification;
		}

		public static UpdateFriendStateNotification DeserializeLengthDelimited(Stream stream, UpdateFriendStateNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static UpdateFriendStateNotification Deserialize(Stream stream, UpdateFriendStateNotification instance, long limit)
		{
			if (instance.Assignment == null)
			{
				instance.Assignment = new List<FriendStateAssignment>();
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
					instance.Assignment.Add(FriendStateAssignment.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, UpdateFriendStateNotification instance)
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
			foreach (FriendStateAssignment item in instance.Assignment)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				FriendStateAssignment.Serialize(stream, item);
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
				foreach (FriendStateAssignment item in Assignment)
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
