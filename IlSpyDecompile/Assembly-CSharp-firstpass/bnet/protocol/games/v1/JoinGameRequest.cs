using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	public class JoinGameRequest : IProtoBuf
	{
		private List<Player> _Player = new List<Player>();

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

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= GameHandle.GetHashCode();
			foreach (Player item in Player)
			{
				hashCode ^= item.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			JoinGameRequest joinGameRequest = obj as JoinGameRequest;
			if (joinGameRequest == null)
			{
				return false;
			}
			if (!GameHandle.Equals(joinGameRequest.GameHandle))
			{
				return false;
			}
			if (Player.Count != joinGameRequest.Player.Count)
			{
				return false;
			}
			for (int i = 0; i < Player.Count; i++)
			{
				if (!Player[i].Equals(joinGameRequest.Player[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static JoinGameRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<JoinGameRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static JoinGameRequest Deserialize(Stream stream, JoinGameRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static JoinGameRequest DeserializeLengthDelimited(Stream stream)
		{
			JoinGameRequest joinGameRequest = new JoinGameRequest();
			DeserializeLengthDelimited(stream, joinGameRequest);
			return joinGameRequest;
		}

		public static JoinGameRequest DeserializeLengthDelimited(Stream stream, JoinGameRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static JoinGameRequest Deserialize(Stream stream, JoinGameRequest instance, long limit)
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
					instance.Player.Add(bnet.protocol.games.v1.Player.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, JoinGameRequest instance)
		{
			if (instance.GameHandle == null)
			{
				throw new ArgumentNullException("GameHandle", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
			GameHandle.Serialize(stream, instance.GameHandle);
			if (instance.Player.Count <= 0)
			{
				return;
			}
			foreach (Player item in instance.Player)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				bnet.protocol.games.v1.Player.Serialize(stream, item);
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
			return num + 1;
		}
	}
}
