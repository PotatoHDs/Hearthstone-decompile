using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	public class AddPlayersRequest : IProtoBuf
	{
		private List<Player> _Player = new List<Player>();

		private List<Assignment> _Assignment = new List<Assignment>();

		public bool HasHost;

		private ProcessId _Host;

		public GameHandle GameHandle { get; set; }

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
			foreach (Player item in Player)
			{
				hashCode ^= item.GetHashCode();
			}
			foreach (Assignment item2 in Assignment)
			{
				hashCode ^= item2.GetHashCode();
			}
			if (HasHost)
			{
				hashCode ^= Host.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			AddPlayersRequest addPlayersRequest = obj as AddPlayersRequest;
			if (addPlayersRequest == null)
			{
				return false;
			}
			if (!GameHandle.Equals(addPlayersRequest.GameHandle))
			{
				return false;
			}
			if (Player.Count != addPlayersRequest.Player.Count)
			{
				return false;
			}
			for (int i = 0; i < Player.Count; i++)
			{
				if (!Player[i].Equals(addPlayersRequest.Player[i]))
				{
					return false;
				}
			}
			if (Assignment.Count != addPlayersRequest.Assignment.Count)
			{
				return false;
			}
			for (int j = 0; j < Assignment.Count; j++)
			{
				if (!Assignment[j].Equals(addPlayersRequest.Assignment[j]))
				{
					return false;
				}
			}
			if (HasHost != addPlayersRequest.HasHost || (HasHost && !Host.Equals(addPlayersRequest.Host)))
			{
				return false;
			}
			return true;
		}

		public static AddPlayersRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AddPlayersRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AddPlayersRequest Deserialize(Stream stream, AddPlayersRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AddPlayersRequest DeserializeLengthDelimited(Stream stream)
		{
			AddPlayersRequest addPlayersRequest = new AddPlayersRequest();
			DeserializeLengthDelimited(stream, addPlayersRequest);
			return addPlayersRequest;
		}

		public static AddPlayersRequest DeserializeLengthDelimited(Stream stream, AddPlayersRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AddPlayersRequest Deserialize(Stream stream, AddPlayersRequest instance, long limit)
		{
			if (instance.Player == null)
			{
				instance.Player = new List<Player>();
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
					instance.Player.Add(bnet.protocol.games.v1.Player.DeserializeLengthDelimited(stream));
					continue;
				case 26:
					instance.Assignment.Add(bnet.protocol.games.v1.Assignment.DeserializeLengthDelimited(stream));
					continue;
				case 34:
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

		public static void Serialize(Stream stream, AddPlayersRequest instance)
		{
			if (instance.GameHandle == null)
			{
				throw new ArgumentNullException("GameHandle", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
			GameHandle.Serialize(stream, instance.GameHandle);
			if (instance.Player.Count > 0)
			{
				foreach (Player item in instance.Player)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.games.v1.Player.Serialize(stream, item);
				}
			}
			if (instance.Assignment.Count > 0)
			{
				foreach (Assignment item2 in instance.Assignment)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, item2.GetSerializedSize());
					bnet.protocol.games.v1.Assignment.Serialize(stream, item2);
				}
			}
			if (instance.HasHost)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Host);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = GameHandle.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (Player.Count > 0)
			{
				foreach (Player item in Player)
				{
					num++;
					uint serializedSize2 = item.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (Assignment.Count > 0)
			{
				foreach (Assignment item2 in Assignment)
				{
					num++;
					uint serializedSize3 = item2.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			if (HasHost)
			{
				num++;
				uint serializedSize4 = Host.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			return num + 1;
		}
	}
}
