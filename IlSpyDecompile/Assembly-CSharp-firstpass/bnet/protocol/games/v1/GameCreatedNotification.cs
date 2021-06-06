using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	public class GameCreatedNotification : IProtoBuf
	{
		public bool HasGameHandle;

		private GameHandle _GameHandle;

		public bool HasErrorId;

		private uint _ErrorId;

		private List<ConnectInfo> _ConnectInfo = new List<ConnectInfo>();

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

		public uint ErrorId
		{
			get
			{
				return _ErrorId;
			}
			set
			{
				_ErrorId = value;
				HasErrorId = true;
			}
		}

		public List<ConnectInfo> ConnectInfo
		{
			get
			{
				return _ConnectInfo;
			}
			set
			{
				_ConnectInfo = value;
			}
		}

		public List<ConnectInfo> ConnectInfoList => _ConnectInfo;

		public int ConnectInfoCount => _ConnectInfo.Count;

		public bool IsInitialized => true;

		public void SetGameHandle(GameHandle val)
		{
			GameHandle = val;
		}

		public void SetErrorId(uint val)
		{
			ErrorId = val;
		}

		public void AddConnectInfo(ConnectInfo val)
		{
			_ConnectInfo.Add(val);
		}

		public void ClearConnectInfo()
		{
			_ConnectInfo.Clear();
		}

		public void SetConnectInfo(List<ConnectInfo> val)
		{
			ConnectInfo = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasGameHandle)
			{
				num ^= GameHandle.GetHashCode();
			}
			if (HasErrorId)
			{
				num ^= ErrorId.GetHashCode();
			}
			foreach (ConnectInfo item in ConnectInfo)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameCreatedNotification gameCreatedNotification = obj as GameCreatedNotification;
			if (gameCreatedNotification == null)
			{
				return false;
			}
			if (HasGameHandle != gameCreatedNotification.HasGameHandle || (HasGameHandle && !GameHandle.Equals(gameCreatedNotification.GameHandle)))
			{
				return false;
			}
			if (HasErrorId != gameCreatedNotification.HasErrorId || (HasErrorId && !ErrorId.Equals(gameCreatedNotification.ErrorId)))
			{
				return false;
			}
			if (ConnectInfo.Count != gameCreatedNotification.ConnectInfo.Count)
			{
				return false;
			}
			for (int i = 0; i < ConnectInfo.Count; i++)
			{
				if (!ConnectInfo[i].Equals(gameCreatedNotification.ConnectInfo[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static GameCreatedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameCreatedNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameCreatedNotification Deserialize(Stream stream, GameCreatedNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameCreatedNotification DeserializeLengthDelimited(Stream stream)
		{
			GameCreatedNotification gameCreatedNotification = new GameCreatedNotification();
			DeserializeLengthDelimited(stream, gameCreatedNotification);
			return gameCreatedNotification;
		}

		public static GameCreatedNotification DeserializeLengthDelimited(Stream stream, GameCreatedNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameCreatedNotification Deserialize(Stream stream, GameCreatedNotification instance, long limit)
		{
			if (instance.ConnectInfo == null)
			{
				instance.ConnectInfo = new List<ConnectInfo>();
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
				case 16:
					instance.ErrorId = ProtocolParser.ReadUInt32(stream);
					continue;
				case 26:
					instance.ConnectInfo.Add(bnet.protocol.games.v1.ConnectInfo.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, GameCreatedNotification instance)
		{
			if (instance.HasGameHandle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
				GameHandle.Serialize(stream, instance.GameHandle);
			}
			if (instance.HasErrorId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.ErrorId);
			}
			if (instance.ConnectInfo.Count <= 0)
			{
				return;
			}
			foreach (ConnectInfo item in instance.ConnectInfo)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				bnet.protocol.games.v1.ConnectInfo.Serialize(stream, item);
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
			if (HasErrorId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(ErrorId);
			}
			if (ConnectInfo.Count > 0)
			{
				foreach (ConnectInfo item in ConnectInfo)
				{
					num++;
					uint serializedSize2 = item.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
				return num;
			}
			return num;
		}
	}
}
