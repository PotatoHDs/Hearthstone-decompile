using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	public class MatchmakingEventInfo : IProtoBuf
	{
		private List<Player> _Player = new List<Player>();

		private List<RequestId> _RequestId = new List<RequestId>();

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

		public List<RequestId> RequestId
		{
			get
			{
				return _RequestId;
			}
			set
			{
				_RequestId = value;
			}
		}

		public List<RequestId> RequestIdList => _RequestId;

		public int RequestIdCount => _RequestId.Count;

		public bool IsInitialized => true;

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

		public void AddRequestId(RequestId val)
		{
			_RequestId.Add(val);
		}

		public void ClearRequestId()
		{
			_RequestId.Clear();
		}

		public void SetRequestId(List<RequestId> val)
		{
			RequestId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (Player item in Player)
			{
				num ^= item.GetHashCode();
			}
			foreach (RequestId item2 in RequestId)
			{
				num ^= item2.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			MatchmakingEventInfo matchmakingEventInfo = obj as MatchmakingEventInfo;
			if (matchmakingEventInfo == null)
			{
				return false;
			}
			if (Player.Count != matchmakingEventInfo.Player.Count)
			{
				return false;
			}
			for (int i = 0; i < Player.Count; i++)
			{
				if (!Player[i].Equals(matchmakingEventInfo.Player[i]))
				{
					return false;
				}
			}
			if (RequestId.Count != matchmakingEventInfo.RequestId.Count)
			{
				return false;
			}
			for (int j = 0; j < RequestId.Count; j++)
			{
				if (!RequestId[j].Equals(matchmakingEventInfo.RequestId[j]))
				{
					return false;
				}
			}
			return true;
		}

		public static MatchmakingEventInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MatchmakingEventInfo>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static MatchmakingEventInfo Deserialize(Stream stream, MatchmakingEventInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static MatchmakingEventInfo DeserializeLengthDelimited(Stream stream)
		{
			MatchmakingEventInfo matchmakingEventInfo = new MatchmakingEventInfo();
			DeserializeLengthDelimited(stream, matchmakingEventInfo);
			return matchmakingEventInfo;
		}

		public static MatchmakingEventInfo DeserializeLengthDelimited(Stream stream, MatchmakingEventInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static MatchmakingEventInfo Deserialize(Stream stream, MatchmakingEventInfo instance, long limit)
		{
			if (instance.Player == null)
			{
				instance.Player = new List<Player>();
			}
			if (instance.RequestId == null)
			{
				instance.RequestId = new List<RequestId>();
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
					instance.Player.Add(bnet.protocol.matchmaking.v1.Player.DeserializeLengthDelimited(stream));
					continue;
				case 18:
					instance.RequestId.Add(bnet.protocol.matchmaking.v1.RequestId.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, MatchmakingEventInfo instance)
		{
			if (instance.Player.Count > 0)
			{
				foreach (Player item in instance.Player)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.matchmaking.v1.Player.Serialize(stream, item);
				}
			}
			if (instance.RequestId.Count <= 0)
			{
				return;
			}
			foreach (RequestId item2 in instance.RequestId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, item2.GetSerializedSize());
				bnet.protocol.matchmaking.v1.RequestId.Serialize(stream, item2);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Player.Count > 0)
			{
				foreach (Player item in Player)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (RequestId.Count > 0)
			{
				foreach (RequestId item2 in RequestId)
				{
					num++;
					uint serializedSize2 = item2.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
				return num;
			}
			return num;
		}
	}
}
