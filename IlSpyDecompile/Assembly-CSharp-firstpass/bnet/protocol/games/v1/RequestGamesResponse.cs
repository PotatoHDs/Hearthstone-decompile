using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	public class RequestGamesResponse : IProtoBuf
	{
		private List<GameResponseEntry> _GameResponse = new List<GameResponseEntry>();

		public List<GameResponseEntry> GameResponse
		{
			get
			{
				return _GameResponse;
			}
			set
			{
				_GameResponse = value;
			}
		}

		public List<GameResponseEntry> GameResponseList => _GameResponse;

		public int GameResponseCount => _GameResponse.Count;

		public bool IsInitialized => true;

		public void AddGameResponse(GameResponseEntry val)
		{
			_GameResponse.Add(val);
		}

		public void ClearGameResponse()
		{
			_GameResponse.Clear();
		}

		public void SetGameResponse(List<GameResponseEntry> val)
		{
			GameResponse = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (GameResponseEntry item in GameResponse)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RequestGamesResponse requestGamesResponse = obj as RequestGamesResponse;
			if (requestGamesResponse == null)
			{
				return false;
			}
			if (GameResponse.Count != requestGamesResponse.GameResponse.Count)
			{
				return false;
			}
			for (int i = 0; i < GameResponse.Count; i++)
			{
				if (!GameResponse[i].Equals(requestGamesResponse.GameResponse[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static RequestGamesResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RequestGamesResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RequestGamesResponse Deserialize(Stream stream, RequestGamesResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RequestGamesResponse DeserializeLengthDelimited(Stream stream)
		{
			RequestGamesResponse requestGamesResponse = new RequestGamesResponse();
			DeserializeLengthDelimited(stream, requestGamesResponse);
			return requestGamesResponse;
		}

		public static RequestGamesResponse DeserializeLengthDelimited(Stream stream, RequestGamesResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RequestGamesResponse Deserialize(Stream stream, RequestGamesResponse instance, long limit)
		{
			if (instance.GameResponse == null)
			{
				instance.GameResponse = new List<GameResponseEntry>();
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
					instance.GameResponse.Add(GameResponseEntry.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, RequestGamesResponse instance)
		{
			if (instance.GameResponse.Count <= 0)
			{
				return;
			}
			foreach (GameResponseEntry item in instance.GameResponse)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				GameResponseEntry.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (GameResponse.Count > 0)
			{
				foreach (GameResponseEntry item in GameResponse)
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
