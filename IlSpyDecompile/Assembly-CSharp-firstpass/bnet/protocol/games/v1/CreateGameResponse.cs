using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	public class CreateGameResponse : IProtoBuf
	{
		private List<ConnectInfo> _ConnectInfo = new List<ConnectInfo>();

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
			foreach (ConnectInfo item in ConnectInfo)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			CreateGameResponse createGameResponse = obj as CreateGameResponse;
			if (createGameResponse == null)
			{
				return false;
			}
			if (ConnectInfo.Count != createGameResponse.ConnectInfo.Count)
			{
				return false;
			}
			for (int i = 0; i < ConnectInfo.Count; i++)
			{
				if (!ConnectInfo[i].Equals(createGameResponse.ConnectInfo[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static CreateGameResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateGameResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CreateGameResponse Deserialize(Stream stream, CreateGameResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CreateGameResponse DeserializeLengthDelimited(Stream stream)
		{
			CreateGameResponse createGameResponse = new CreateGameResponse();
			DeserializeLengthDelimited(stream, createGameResponse);
			return createGameResponse;
		}

		public static CreateGameResponse DeserializeLengthDelimited(Stream stream, CreateGameResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CreateGameResponse Deserialize(Stream stream, CreateGameResponse instance, long limit)
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

		public static void Serialize(Stream stream, CreateGameResponse instance)
		{
			if (instance.ConnectInfo.Count <= 0)
			{
				return;
			}
			foreach (ConnectInfo item in instance.ConnectInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				bnet.protocol.games.v1.ConnectInfo.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (ConnectInfo.Count > 0)
			{
				foreach (ConnectInfo item in ConnectInfo)
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
