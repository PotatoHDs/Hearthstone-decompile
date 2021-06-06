using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.matchmaking.v1
{
	public class AddPlayersResultNotification : IProtoBuf
	{
		public bool HasGameHandle;

		private GameHandle _GameHandle;

		private List<GameAccountHandle> _GameAccount = new List<GameAccountHandle>();

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

		public List<GameAccountHandle> GameAccount
		{
			get
			{
				return _GameAccount;
			}
			set
			{
				_GameAccount = value;
			}
		}

		public List<GameAccountHandle> GameAccountList => _GameAccount;

		public int GameAccountCount => _GameAccount.Count;

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

		public void AddGameAccount(GameAccountHandle val)
		{
			_GameAccount.Add(val);
		}

		public void ClearGameAccount()
		{
			_GameAccount.Clear();
		}

		public void SetGameAccount(List<GameAccountHandle> val)
		{
			GameAccount = val;
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
			foreach (GameAccountHandle item in GameAccount)
			{
				num ^= item.GetHashCode();
			}
			if (HasErrorId)
			{
				num ^= ErrorId.GetHashCode();
			}
			foreach (ConnectInfo item2 in ConnectInfo)
			{
				num ^= item2.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AddPlayersResultNotification addPlayersResultNotification = obj as AddPlayersResultNotification;
			if (addPlayersResultNotification == null)
			{
				return false;
			}
			if (HasGameHandle != addPlayersResultNotification.HasGameHandle || (HasGameHandle && !GameHandle.Equals(addPlayersResultNotification.GameHandle)))
			{
				return false;
			}
			if (GameAccount.Count != addPlayersResultNotification.GameAccount.Count)
			{
				return false;
			}
			for (int i = 0; i < GameAccount.Count; i++)
			{
				if (!GameAccount[i].Equals(addPlayersResultNotification.GameAccount[i]))
				{
					return false;
				}
			}
			if (HasErrorId != addPlayersResultNotification.HasErrorId || (HasErrorId && !ErrorId.Equals(addPlayersResultNotification.ErrorId)))
			{
				return false;
			}
			if (ConnectInfo.Count != addPlayersResultNotification.ConnectInfo.Count)
			{
				return false;
			}
			for (int j = 0; j < ConnectInfo.Count; j++)
			{
				if (!ConnectInfo[j].Equals(addPlayersResultNotification.ConnectInfo[j]))
				{
					return false;
				}
			}
			return true;
		}

		public static AddPlayersResultNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AddPlayersResultNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AddPlayersResultNotification Deserialize(Stream stream, AddPlayersResultNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AddPlayersResultNotification DeserializeLengthDelimited(Stream stream)
		{
			AddPlayersResultNotification addPlayersResultNotification = new AddPlayersResultNotification();
			DeserializeLengthDelimited(stream, addPlayersResultNotification);
			return addPlayersResultNotification;
		}

		public static AddPlayersResultNotification DeserializeLengthDelimited(Stream stream, AddPlayersResultNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AddPlayersResultNotification Deserialize(Stream stream, AddPlayersResultNotification instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.GameAccount == null)
			{
				instance.GameAccount = new List<GameAccountHandle>();
			}
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
				case 18:
					instance.GameAccount.Add(GameAccountHandle.DeserializeLengthDelimited(stream));
					continue;
				case 29:
					instance.ErrorId = binaryReader.ReadUInt32();
					continue;
				case 34:
					instance.ConnectInfo.Add(bnet.protocol.matchmaking.v1.ConnectInfo.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, AddPlayersResultNotification instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasGameHandle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
				GameHandle.Serialize(stream, instance.GameHandle);
			}
			if (instance.GameAccount.Count > 0)
			{
				foreach (GameAccountHandle item in instance.GameAccount)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					GameAccountHandle.Serialize(stream, item);
				}
			}
			if (instance.HasErrorId)
			{
				stream.WriteByte(29);
				binaryWriter.Write(instance.ErrorId);
			}
			if (instance.ConnectInfo.Count <= 0)
			{
				return;
			}
			foreach (ConnectInfo item2 in instance.ConnectInfo)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, item2.GetSerializedSize());
				bnet.protocol.matchmaking.v1.ConnectInfo.Serialize(stream, item2);
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
			if (GameAccount.Count > 0)
			{
				foreach (GameAccountHandle item in GameAccount)
				{
					num++;
					uint serializedSize2 = item.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (HasErrorId)
			{
				num++;
				num += 4;
			}
			if (ConnectInfo.Count > 0)
			{
				foreach (ConnectInfo item2 in ConnectInfo)
				{
					num++;
					uint serializedSize3 = item2.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
				return num;
			}
			return num;
		}
	}
}
