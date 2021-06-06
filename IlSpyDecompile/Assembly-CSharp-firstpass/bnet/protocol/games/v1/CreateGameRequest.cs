using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	public class CreateGameRequest : IProtoBuf
	{
		private List<Attribute> _CreationAttributes = new List<Attribute>();

		private List<Player> _Player = new List<Player>();

		private List<Team> _Team = new List<Team>();

		private List<Assignment> _Assignment = new List<Assignment>();

		public bool HasHost;

		private ProcessId _Host;

		public GameHandle GameHandle { get; set; }

		public List<Attribute> CreationAttributes
		{
			get
			{
				return _CreationAttributes;
			}
			set
			{
				_CreationAttributes = value;
			}
		}

		public List<Attribute> CreationAttributesList => _CreationAttributes;

		public int CreationAttributesCount => _CreationAttributes.Count;

		public List<Player> Player
		{
			get
			{
				return _Player;
			}
			set
			{
				_Player = value;
			}
		}

		public List<Player> PlayerList => _Player;

		public int PlayerCount => _Player.Count;

		public List<Team> Team
		{
			get
			{
				return _Team;
			}
			set
			{
				_Team = value;
			}
		}

		public List<Team> TeamList => _Team;

		public int TeamCount => _Team.Count;

		public List<Assignment> Assignment
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

		public List<Assignment> AssignmentList => _Assignment;

		public int AssignmentCount => _Assignment.Count;

		public ProcessId Host
		{
			get
			{
				return _Host;
			}
			set
			{
				_Host = value;
				HasHost = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetGameHandle(GameHandle val)
		{
			GameHandle = val;
		}

		public void AddCreationAttributes(Attribute val)
		{
			_CreationAttributes.Add(val);
		}

		public void ClearCreationAttributes()
		{
			_CreationAttributes.Clear();
		}

		public void SetCreationAttributes(List<Attribute> val)
		{
			CreationAttributes = val;
		}

		public void AddPlayer(Player val)
		{
			_Player.Add(val);
		}

		public void ClearPlayer()
		{
			_Player.Clear();
		}

		public void SetPlayer(List<Player> val)
		{
			Player = val;
		}

		public void AddTeam(Team val)
		{
			_Team.Add(val);
		}

		public void ClearTeam()
		{
			_Team.Clear();
		}

		public void SetTeam(List<Team> val)
		{
			Team = val;
		}

		public void AddAssignment(Assignment val)
		{
			_Assignment.Add(val);
		}

		public void ClearAssignment()
		{
			_Assignment.Clear();
		}

		public void SetAssignment(List<Assignment> val)
		{
			Assignment = val;
		}

		public void SetHost(ProcessId val)
		{
			Host = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= GameHandle.GetHashCode();
			foreach (Attribute creationAttribute in CreationAttributes)
			{
				hashCode ^= creationAttribute.GetHashCode();
			}
			foreach (Player item in Player)
			{
				hashCode ^= item.GetHashCode();
			}
			foreach (Team item2 in Team)
			{
				hashCode ^= item2.GetHashCode();
			}
			foreach (Assignment item3 in Assignment)
			{
				hashCode ^= item3.GetHashCode();
			}
			if (HasHost)
			{
				hashCode ^= Host.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			CreateGameRequest createGameRequest = obj as CreateGameRequest;
			if (createGameRequest == null)
			{
				return false;
			}
			if (!GameHandle.Equals(createGameRequest.GameHandle))
			{
				return false;
			}
			if (CreationAttributes.Count != createGameRequest.CreationAttributes.Count)
			{
				return false;
			}
			for (int i = 0; i < CreationAttributes.Count; i++)
			{
				if (!CreationAttributes[i].Equals(createGameRequest.CreationAttributes[i]))
				{
					return false;
				}
			}
			if (Player.Count != createGameRequest.Player.Count)
			{
				return false;
			}
			for (int j = 0; j < Player.Count; j++)
			{
				if (!Player[j].Equals(createGameRequest.Player[j]))
				{
					return false;
				}
			}
			if (Team.Count != createGameRequest.Team.Count)
			{
				return false;
			}
			for (int k = 0; k < Team.Count; k++)
			{
				if (!Team[k].Equals(createGameRequest.Team[k]))
				{
					return false;
				}
			}
			if (Assignment.Count != createGameRequest.Assignment.Count)
			{
				return false;
			}
			for (int l = 0; l < Assignment.Count; l++)
			{
				if (!Assignment[l].Equals(createGameRequest.Assignment[l]))
				{
					return false;
				}
			}
			if (HasHost != createGameRequest.HasHost || (HasHost && !Host.Equals(createGameRequest.Host)))
			{
				return false;
			}
			return true;
		}

		public static CreateGameRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateGameRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CreateGameRequest Deserialize(Stream stream, CreateGameRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CreateGameRequest DeserializeLengthDelimited(Stream stream)
		{
			CreateGameRequest createGameRequest = new CreateGameRequest();
			DeserializeLengthDelimited(stream, createGameRequest);
			return createGameRequest;
		}

		public static CreateGameRequest DeserializeLengthDelimited(Stream stream, CreateGameRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CreateGameRequest Deserialize(Stream stream, CreateGameRequest instance, long limit)
		{
			if (instance.CreationAttributes == null)
			{
				instance.CreationAttributes = new List<Attribute>();
			}
			if (instance.Player == null)
			{
				instance.Player = new List<Player>();
			}
			if (instance.Team == null)
			{
				instance.Team = new List<Team>();
			}
			if (instance.Assignment == null)
			{
				instance.Assignment = new List<Assignment>();
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
					if (instance.GameHandle == null)
					{
						instance.GameHandle = GameHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameHandle.DeserializeLengthDelimited(stream, instance.GameHandle);
					}
					continue;
				case 18:
					instance.CreationAttributes.Add(Attribute.DeserializeLengthDelimited(stream));
					continue;
				case 34:
					instance.Player.Add(bnet.protocol.games.v1.Player.DeserializeLengthDelimited(stream));
					continue;
				case 42:
					instance.Team.Add(bnet.protocol.games.v1.Team.DeserializeLengthDelimited(stream));
					continue;
				case 50:
					instance.Assignment.Add(bnet.protocol.games.v1.Assignment.DeserializeLengthDelimited(stream));
					continue;
				case 58:
					if (instance.Host == null)
					{
						instance.Host = ProcessId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProcessId.DeserializeLengthDelimited(stream, instance.Host);
					}
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

		public static void Serialize(Stream stream, CreateGameRequest instance)
		{
			if (instance.GameHandle == null)
			{
				throw new ArgumentNullException("GameHandle", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
			GameHandle.Serialize(stream, instance.GameHandle);
			if (instance.CreationAttributes.Count > 0)
			{
				foreach (Attribute creationAttribute in instance.CreationAttributes)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, creationAttribute.GetSerializedSize());
					Attribute.Serialize(stream, creationAttribute);
				}
			}
			if (instance.Player.Count > 0)
			{
				foreach (Player item in instance.Player)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.games.v1.Player.Serialize(stream, item);
				}
			}
			if (instance.Team.Count > 0)
			{
				foreach (Team item2 in instance.Team)
				{
					stream.WriteByte(42);
					ProtocolParser.WriteUInt32(stream, item2.GetSerializedSize());
					bnet.protocol.games.v1.Team.Serialize(stream, item2);
				}
			}
			if (instance.Assignment.Count > 0)
			{
				foreach (Assignment item3 in instance.Assignment)
				{
					stream.WriteByte(50);
					ProtocolParser.WriteUInt32(stream, item3.GetSerializedSize());
					bnet.protocol.games.v1.Assignment.Serialize(stream, item3);
				}
			}
			if (instance.HasHost)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Host);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = GameHandle.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (CreationAttributes.Count > 0)
			{
				foreach (Attribute creationAttribute in CreationAttributes)
				{
					num++;
					uint serializedSize2 = creationAttribute.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (Player.Count > 0)
			{
				foreach (Player item in Player)
				{
					num++;
					uint serializedSize3 = item.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			if (Team.Count > 0)
			{
				foreach (Team item2 in Team)
				{
					num++;
					uint serializedSize4 = item2.GetSerializedSize();
					num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
				}
			}
			if (Assignment.Count > 0)
			{
				foreach (Assignment item3 in Assignment)
				{
					num++;
					uint serializedSize5 = item3.GetSerializedSize();
					num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
				}
			}
			if (HasHost)
			{
				num++;
				uint serializedSize6 = Host.GetSerializedSize();
				num += serializedSize6 + ProtocolParser.SizeOfUInt32(serializedSize6);
			}
			return num + 1;
		}
	}
}
