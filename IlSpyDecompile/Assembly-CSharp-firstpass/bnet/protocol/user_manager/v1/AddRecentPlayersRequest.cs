using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.user_manager.v1
{
	public class AddRecentPlayersRequest : IProtoBuf
	{
		private List<RecentPlayer> _Players = new List<RecentPlayer>();

		public bool HasAgentId;

		private EntityId _AgentId;

		public bool HasProgram;

		private uint _Program;

		public List<RecentPlayer> Players
		{
			get
			{
				return _Players;
			}
			set
			{
				_Players = value;
			}
		}

		public List<RecentPlayer> PlayersList => _Players;

		public int PlayersCount => _Players.Count;

		public EntityId AgentId
		{
			get
			{
				return _AgentId;
			}
			set
			{
				_AgentId = value;
				HasAgentId = value != null;
			}
		}

		public uint Program
		{
			get
			{
				return _Program;
			}
			set
			{
				_Program = value;
				HasProgram = true;
			}
		}

		public bool IsInitialized => true;

		public void AddPlayers(RecentPlayer val)
		{
			_Players.Add(val);
		}

		public void ClearPlayers()
		{
			_Players.Clear();
		}

		public void SetPlayers(List<RecentPlayer> val)
		{
			Players = val;
		}

		public void SetAgentId(EntityId val)
		{
			AgentId = val;
		}

		public void SetProgram(uint val)
		{
			Program = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (RecentPlayer player in Players)
			{
				num ^= player.GetHashCode();
			}
			if (HasAgentId)
			{
				num ^= AgentId.GetHashCode();
			}
			if (HasProgram)
			{
				num ^= Program.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AddRecentPlayersRequest addRecentPlayersRequest = obj as AddRecentPlayersRequest;
			if (addRecentPlayersRequest == null)
			{
				return false;
			}
			if (Players.Count != addRecentPlayersRequest.Players.Count)
			{
				return false;
			}
			for (int i = 0; i < Players.Count; i++)
			{
				if (!Players[i].Equals(addRecentPlayersRequest.Players[i]))
				{
					return false;
				}
			}
			if (HasAgentId != addRecentPlayersRequest.HasAgentId || (HasAgentId && !AgentId.Equals(addRecentPlayersRequest.AgentId)))
			{
				return false;
			}
			if (HasProgram != addRecentPlayersRequest.HasProgram || (HasProgram && !Program.Equals(addRecentPlayersRequest.Program)))
			{
				return false;
			}
			return true;
		}

		public static AddRecentPlayersRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AddRecentPlayersRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AddRecentPlayersRequest Deserialize(Stream stream, AddRecentPlayersRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AddRecentPlayersRequest DeserializeLengthDelimited(Stream stream)
		{
			AddRecentPlayersRequest addRecentPlayersRequest = new AddRecentPlayersRequest();
			DeserializeLengthDelimited(stream, addRecentPlayersRequest);
			return addRecentPlayersRequest;
		}

		public static AddRecentPlayersRequest DeserializeLengthDelimited(Stream stream, AddRecentPlayersRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AddRecentPlayersRequest Deserialize(Stream stream, AddRecentPlayersRequest instance, long limit)
		{
			if (instance.Players == null)
			{
				instance.Players = new List<RecentPlayer>();
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
					instance.Players.Add(RecentPlayer.DeserializeLengthDelimited(stream));
					continue;
				case 18:
					if (instance.AgentId == null)
					{
						instance.AgentId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.AgentId);
					}
					continue;
				case 24:
					instance.Program = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, AddRecentPlayersRequest instance)
		{
			if (instance.Players.Count > 0)
			{
				foreach (RecentPlayer player in instance.Players)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, player.GetSerializedSize());
					RecentPlayer.Serialize(stream, player);
				}
			}
			if (instance.HasAgentId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.Program);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Players.Count > 0)
			{
				foreach (RecentPlayer player in Players)
				{
					num++;
					uint serializedSize = player.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasAgentId)
			{
				num++;
				uint serializedSize2 = AgentId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasProgram)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Program);
			}
			return num;
		}
	}
}
