using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	public class JoinExistingGameRequest : IProtoBuf
	{
		public bool HasGameHandle;

		private GameHandle _GameHandle;

		public bool HasRequestId;

		private RequestId _RequestId;

		private List<Player> _Player = new List<Player>();

		public GameHandle GameHandle
		{
			get
			{
				return _GameHandle;
			}
			set
			{
				_GameHandle = value;
				HasGameHandle = value != null;
			}
		}

		public RequestId RequestId
		{
			get
			{
				return _RequestId;
			}
			set
			{
				_RequestId = value;
				HasRequestId = value != null;
			}
		}

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

		public bool IsInitialized => true;

		public void SetGameHandle(GameHandle val)
		{
			GameHandle = val;
		}

		public void SetRequestId(RequestId val)
		{
			RequestId = val;
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

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasGameHandle)
			{
				num ^= GameHandle.GetHashCode();
			}
			if (HasRequestId)
			{
				num ^= RequestId.GetHashCode();
			}
			foreach (Player item in Player)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			JoinExistingGameRequest joinExistingGameRequest = obj as JoinExistingGameRequest;
			if (joinExistingGameRequest == null)
			{
				return false;
			}
			if (HasGameHandle != joinExistingGameRequest.HasGameHandle || (HasGameHandle && !GameHandle.Equals(joinExistingGameRequest.GameHandle)))
			{
				return false;
			}
			if (HasRequestId != joinExistingGameRequest.HasRequestId || (HasRequestId && !RequestId.Equals(joinExistingGameRequest.RequestId)))
			{
				return false;
			}
			if (Player.Count != joinExistingGameRequest.Player.Count)
			{
				return false;
			}
			for (int i = 0; i < Player.Count; i++)
			{
				if (!Player[i].Equals(joinExistingGameRequest.Player[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static JoinExistingGameRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<JoinExistingGameRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static JoinExistingGameRequest Deserialize(Stream stream, JoinExistingGameRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static JoinExistingGameRequest DeserializeLengthDelimited(Stream stream)
		{
			JoinExistingGameRequest joinExistingGameRequest = new JoinExistingGameRequest();
			DeserializeLengthDelimited(stream, joinExistingGameRequest);
			return joinExistingGameRequest;
		}

		public static JoinExistingGameRequest DeserializeLengthDelimited(Stream stream, JoinExistingGameRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static JoinExistingGameRequest Deserialize(Stream stream, JoinExistingGameRequest instance, long limit)
		{
			if (instance.Player == null)
			{
				instance.Player = new List<Player>();
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
					if (instance.RequestId == null)
					{
						instance.RequestId = RequestId.DeserializeLengthDelimited(stream);
					}
					else
					{
						RequestId.DeserializeLengthDelimited(stream, instance.RequestId);
					}
					continue;
				case 26:
					instance.Player.Add(bnet.protocol.matchmaking.v1.Player.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, JoinExistingGameRequest instance)
		{
			if (instance.HasGameHandle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
				GameHandle.Serialize(stream, instance.GameHandle);
			}
			if (instance.HasRequestId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.RequestId.GetSerializedSize());
				RequestId.Serialize(stream, instance.RequestId);
			}
			if (instance.Player.Count <= 0)
			{
				return;
			}
			foreach (Player item in instance.Player)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				bnet.protocol.matchmaking.v1.Player.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasGameHandle)
			{
				num++;
				uint serializedSize = GameHandle.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasRequestId)
			{
				num++;
				uint serializedSize2 = RequestId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (Player.Count > 0)
			{
				foreach (Player item in Player)
				{
					num++;
					uint serializedSize3 = item.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
				return num;
			}
			return num;
		}
	}
}
