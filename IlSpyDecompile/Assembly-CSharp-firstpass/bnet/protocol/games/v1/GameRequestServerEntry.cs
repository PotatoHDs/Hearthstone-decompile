using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	public class GameRequestServerEntry : IProtoBuf
	{
		public bool HasHost;

		private HostRoute _Host;

		private List<GameRequestEntry> _GameRequests = new List<GameRequestEntry>();

		public HostRoute Host
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

		public List<GameRequestEntry> GameRequests
		{
			get
			{
				return _GameRequests;
			}
			set
			{
				_GameRequests = value;
			}
		}

		public List<GameRequestEntry> GameRequestsList => _GameRequests;

		public int GameRequestsCount => _GameRequests.Count;

		public bool IsInitialized => true;

		public void SetHost(HostRoute val)
		{
			Host = val;
		}

		public void AddGameRequests(GameRequestEntry val)
		{
			_GameRequests.Add(val);
		}

		public void ClearGameRequests()
		{
			_GameRequests.Clear();
		}

		public void SetGameRequests(List<GameRequestEntry> val)
		{
			GameRequests = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasHost)
			{
				num ^= Host.GetHashCode();
			}
			foreach (GameRequestEntry gameRequest in GameRequests)
			{
				num ^= gameRequest.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameRequestServerEntry gameRequestServerEntry = obj as GameRequestServerEntry;
			if (gameRequestServerEntry == null)
			{
				return false;
			}
			if (HasHost != gameRequestServerEntry.HasHost || (HasHost && !Host.Equals(gameRequestServerEntry.Host)))
			{
				return false;
			}
			if (GameRequests.Count != gameRequestServerEntry.GameRequests.Count)
			{
				return false;
			}
			for (int i = 0; i < GameRequests.Count; i++)
			{
				if (!GameRequests[i].Equals(gameRequestServerEntry.GameRequests[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static GameRequestServerEntry ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameRequestServerEntry>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameRequestServerEntry Deserialize(Stream stream, GameRequestServerEntry instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameRequestServerEntry DeserializeLengthDelimited(Stream stream)
		{
			GameRequestServerEntry gameRequestServerEntry = new GameRequestServerEntry();
			DeserializeLengthDelimited(stream, gameRequestServerEntry);
			return gameRequestServerEntry;
		}

		public static GameRequestServerEntry DeserializeLengthDelimited(Stream stream, GameRequestServerEntry instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameRequestServerEntry Deserialize(Stream stream, GameRequestServerEntry instance, long limit)
		{
			if (instance.GameRequests == null)
			{
				instance.GameRequests = new List<GameRequestEntry>();
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
					if (instance.Host == null)
					{
						instance.Host = HostRoute.DeserializeLengthDelimited(stream);
					}
					else
					{
						HostRoute.DeserializeLengthDelimited(stream, instance.Host);
					}
					continue;
				case 18:
					instance.GameRequests.Add(GameRequestEntry.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, GameRequestServerEntry instance)
		{
			if (instance.HasHost)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
				HostRoute.Serialize(stream, instance.Host);
			}
			if (instance.GameRequests.Count <= 0)
			{
				return;
			}
			foreach (GameRequestEntry gameRequest in instance.GameRequests)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, gameRequest.GetSerializedSize());
				GameRequestEntry.Serialize(stream, gameRequest);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasHost)
			{
				num++;
				uint serializedSize = Host.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (GameRequests.Count > 0)
			{
				foreach (GameRequestEntry gameRequest in GameRequests)
				{
					num++;
					uint serializedSize2 = gameRequest.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
				return num;
			}
			return num;
		}
	}
}
