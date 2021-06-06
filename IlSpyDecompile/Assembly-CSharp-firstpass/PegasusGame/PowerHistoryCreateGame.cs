using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PegasusGame
{
	public class PowerHistoryCreateGame : IProtoBuf
	{
		private List<Player> _Players = new List<Player>();

		private List<SharedPlayerInfo> _PlayerInfos = new List<SharedPlayerInfo>();

		public bool HasGameUuid;

		private string _GameUuid;

		public Entity GameEntity { get; set; }

		public List<Player> Players
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

		public List<SharedPlayerInfo> PlayerInfos
		{
			get
			{
				return _PlayerInfos;
			}
			set
			{
				_PlayerInfos = value;
			}
		}

		public string GameUuid
		{
			get
			{
				return _GameUuid;
			}
			set
			{
				_GameUuid = value;
				HasGameUuid = value != null;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= GameEntity.GetHashCode();
			foreach (Player player in Players)
			{
				hashCode ^= player.GetHashCode();
			}
			foreach (SharedPlayerInfo playerInfo in PlayerInfos)
			{
				hashCode ^= playerInfo.GetHashCode();
			}
			if (HasGameUuid)
			{
				hashCode ^= GameUuid.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			PowerHistoryCreateGame powerHistoryCreateGame = obj as PowerHistoryCreateGame;
			if (powerHistoryCreateGame == null)
			{
				return false;
			}
			if (!GameEntity.Equals(powerHistoryCreateGame.GameEntity))
			{
				return false;
			}
			if (Players.Count != powerHistoryCreateGame.Players.Count)
			{
				return false;
			}
			for (int i = 0; i < Players.Count; i++)
			{
				if (!Players[i].Equals(powerHistoryCreateGame.Players[i]))
				{
					return false;
				}
			}
			if (PlayerInfos.Count != powerHistoryCreateGame.PlayerInfos.Count)
			{
				return false;
			}
			for (int j = 0; j < PlayerInfos.Count; j++)
			{
				if (!PlayerInfos[j].Equals(powerHistoryCreateGame.PlayerInfos[j]))
				{
					return false;
				}
			}
			if (HasGameUuid != powerHistoryCreateGame.HasGameUuid || (HasGameUuid && !GameUuid.Equals(powerHistoryCreateGame.GameUuid)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PowerHistoryCreateGame Deserialize(Stream stream, PowerHistoryCreateGame instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PowerHistoryCreateGame DeserializeLengthDelimited(Stream stream)
		{
			PowerHistoryCreateGame powerHistoryCreateGame = new PowerHistoryCreateGame();
			DeserializeLengthDelimited(stream, powerHistoryCreateGame);
			return powerHistoryCreateGame;
		}

		public static PowerHistoryCreateGame DeserializeLengthDelimited(Stream stream, PowerHistoryCreateGame instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PowerHistoryCreateGame Deserialize(Stream stream, PowerHistoryCreateGame instance, long limit)
		{
			if (instance.Players == null)
			{
				instance.Players = new List<Player>();
			}
			if (instance.PlayerInfos == null)
			{
				instance.PlayerInfos = new List<SharedPlayerInfo>();
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
					if (instance.GameEntity == null)
					{
						instance.GameEntity = Entity.DeserializeLengthDelimited(stream);
					}
					else
					{
						Entity.DeserializeLengthDelimited(stream, instance.GameEntity);
					}
					continue;
				case 18:
					instance.Players.Add(Player.DeserializeLengthDelimited(stream));
					continue;
				case 26:
					instance.PlayerInfos.Add(SharedPlayerInfo.DeserializeLengthDelimited(stream));
					continue;
				case 34:
					instance.GameUuid = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, PowerHistoryCreateGame instance)
		{
			if (instance.GameEntity == null)
			{
				throw new ArgumentNullException("GameEntity", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameEntity.GetSerializedSize());
			Entity.Serialize(stream, instance.GameEntity);
			if (instance.Players.Count > 0)
			{
				foreach (Player player in instance.Players)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, player.GetSerializedSize());
					Player.Serialize(stream, player);
				}
			}
			if (instance.PlayerInfos.Count > 0)
			{
				foreach (SharedPlayerInfo playerInfo in instance.PlayerInfos)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, playerInfo.GetSerializedSize());
					SharedPlayerInfo.Serialize(stream, playerInfo);
				}
			}
			if (instance.HasGameUuid)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.GameUuid));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = GameEntity.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (Players.Count > 0)
			{
				foreach (Player player in Players)
				{
					num++;
					uint serializedSize2 = player.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (PlayerInfos.Count > 0)
			{
				foreach (SharedPlayerInfo playerInfo in PlayerInfos)
				{
					num++;
					uint serializedSize3 = playerInfo.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			if (HasGameUuid)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(GameUuid);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num + 1;
		}
	}
}
