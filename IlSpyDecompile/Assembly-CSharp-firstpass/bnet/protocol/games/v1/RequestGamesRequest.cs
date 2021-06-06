using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	public class RequestGamesRequest : IProtoBuf
	{
		private List<GameRequestServerEntry> _GameRequestsPerServer = new List<GameRequestServerEntry>();

		public List<GameRequestServerEntry> GameRequestsPerServer
		{
			get
			{
				return _GameRequestsPerServer;
			}
			set
			{
				_GameRequestsPerServer = value;
			}
		}

		public List<GameRequestServerEntry> GameRequestsPerServerList => _GameRequestsPerServer;

		public int GameRequestsPerServerCount => _GameRequestsPerServer.Count;

		public bool IsInitialized => true;

		public void AddGameRequestsPerServer(GameRequestServerEntry val)
		{
			_GameRequestsPerServer.Add(val);
		}

		public void ClearGameRequestsPerServer()
		{
			_GameRequestsPerServer.Clear();
		}

		public void SetGameRequestsPerServer(List<GameRequestServerEntry> val)
		{
			GameRequestsPerServer = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (GameRequestServerEntry item in GameRequestsPerServer)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RequestGamesRequest requestGamesRequest = obj as RequestGamesRequest;
			if (requestGamesRequest == null)
			{
				return false;
			}
			if (GameRequestsPerServer.Count != requestGamesRequest.GameRequestsPerServer.Count)
			{
				return false;
			}
			for (int i = 0; i < GameRequestsPerServer.Count; i++)
			{
				if (!GameRequestsPerServer[i].Equals(requestGamesRequest.GameRequestsPerServer[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static RequestGamesRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RequestGamesRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RequestGamesRequest Deserialize(Stream stream, RequestGamesRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RequestGamesRequest DeserializeLengthDelimited(Stream stream)
		{
			RequestGamesRequest requestGamesRequest = new RequestGamesRequest();
			DeserializeLengthDelimited(stream, requestGamesRequest);
			return requestGamesRequest;
		}

		public static RequestGamesRequest DeserializeLengthDelimited(Stream stream, RequestGamesRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RequestGamesRequest Deserialize(Stream stream, RequestGamesRequest instance, long limit)
		{
			if (instance.GameRequestsPerServer == null)
			{
				instance.GameRequestsPerServer = new List<GameRequestServerEntry>();
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
					instance.GameRequestsPerServer.Add(GameRequestServerEntry.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, RequestGamesRequest instance)
		{
			if (instance.GameRequestsPerServer.Count <= 0)
			{
				return;
			}
			foreach (GameRequestServerEntry item in instance.GameRequestsPerServer)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				GameRequestServerEntry.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (GameRequestsPerServer.Count > 0)
			{
				foreach (GameRequestServerEntry item in GameRequestsPerServer)
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
