using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	public class JoinGameResponse : IProtoBuf
	{
		public bool HasRequestId;

		private ulong _RequestId;

		public bool HasQueued;

		private bool _Queued;

		private List<ConnectInfo> _ConnectInfo = new List<ConnectInfo>();

		public ulong RequestId
		{
			get
			{
				return _RequestId;
			}
			set
			{
				_RequestId = value;
				HasRequestId = true;
			}
		}

		public bool Queued
		{
			get
			{
				return _Queued;
			}
			set
			{
				_Queued = value;
				HasQueued = true;
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

		public void SetRequestId(ulong val)
		{
			RequestId = val;
		}

		public void SetQueued(bool val)
		{
			Queued = val;
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
			if (HasRequestId)
			{
				num ^= RequestId.GetHashCode();
			}
			if (HasQueued)
			{
				num ^= Queued.GetHashCode();
			}
			foreach (ConnectInfo item in ConnectInfo)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			JoinGameResponse joinGameResponse = obj as JoinGameResponse;
			if (joinGameResponse == null)
			{
				return false;
			}
			if (HasRequestId != joinGameResponse.HasRequestId || (HasRequestId && !RequestId.Equals(joinGameResponse.RequestId)))
			{
				return false;
			}
			if (HasQueued != joinGameResponse.HasQueued || (HasQueued && !Queued.Equals(joinGameResponse.Queued)))
			{
				return false;
			}
			if (ConnectInfo.Count != joinGameResponse.ConnectInfo.Count)
			{
				return false;
			}
			for (int i = 0; i < ConnectInfo.Count; i++)
			{
				if (!ConnectInfo[i].Equals(joinGameResponse.ConnectInfo[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static JoinGameResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<JoinGameResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static JoinGameResponse Deserialize(Stream stream, JoinGameResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static JoinGameResponse DeserializeLengthDelimited(Stream stream)
		{
			JoinGameResponse joinGameResponse = new JoinGameResponse();
			DeserializeLengthDelimited(stream, joinGameResponse);
			return joinGameResponse;
		}

		public static JoinGameResponse DeserializeLengthDelimited(Stream stream, JoinGameResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static JoinGameResponse Deserialize(Stream stream, JoinGameResponse instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.Queued = false;
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
				case 9:
					instance.RequestId = binaryReader.ReadUInt64();
					continue;
				case 16:
					instance.Queued = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, JoinGameResponse instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasRequestId)
			{
				stream.WriteByte(9);
				binaryWriter.Write(instance.RequestId);
			}
			if (instance.HasQueued)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.Queued);
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
			if (HasRequestId)
			{
				num++;
				num += 8;
			}
			if (HasQueued)
			{
				num++;
				num++;
			}
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
