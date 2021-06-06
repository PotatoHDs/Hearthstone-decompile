using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	public class MatchmakingResultNotification : IProtoBuf
	{
		public bool HasRequestId;

		private RequestId _RequestId;

		public bool HasResult;

		private uint _Result;

		public bool HasConnectInfo;

		private ConnectInfo _ConnectInfo;

		public bool HasGameHandle;

		private GameHandle _GameHandle;

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

		public uint Result
		{
			get
			{
				return _Result;
			}
			set
			{
				_Result = value;
				HasResult = true;
			}
		}

		public ConnectInfo ConnectInfo
		{
			get
			{
				return _ConnectInfo;
			}
			set
			{
				_ConnectInfo = value;
				HasConnectInfo = value != null;
			}
		}

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

		public bool IsInitialized => true;

		public void SetRequestId(RequestId val)
		{
			RequestId = val;
		}

		public void SetResult(uint val)
		{
			Result = val;
		}

		public void SetConnectInfo(ConnectInfo val)
		{
			ConnectInfo = val;
		}

		public void SetGameHandle(GameHandle val)
		{
			GameHandle = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasRequestId)
			{
				num ^= RequestId.GetHashCode();
			}
			if (HasResult)
			{
				num ^= Result.GetHashCode();
			}
			if (HasConnectInfo)
			{
				num ^= ConnectInfo.GetHashCode();
			}
			if (HasGameHandle)
			{
				num ^= GameHandle.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			MatchmakingResultNotification matchmakingResultNotification = obj as MatchmakingResultNotification;
			if (matchmakingResultNotification == null)
			{
				return false;
			}
			if (HasRequestId != matchmakingResultNotification.HasRequestId || (HasRequestId && !RequestId.Equals(matchmakingResultNotification.RequestId)))
			{
				return false;
			}
			if (HasResult != matchmakingResultNotification.HasResult || (HasResult && !Result.Equals(matchmakingResultNotification.Result)))
			{
				return false;
			}
			if (HasConnectInfo != matchmakingResultNotification.HasConnectInfo || (HasConnectInfo && !ConnectInfo.Equals(matchmakingResultNotification.ConnectInfo)))
			{
				return false;
			}
			if (HasGameHandle != matchmakingResultNotification.HasGameHandle || (HasGameHandle && !GameHandle.Equals(matchmakingResultNotification.GameHandle)))
			{
				return false;
			}
			return true;
		}

		public static MatchmakingResultNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MatchmakingResultNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static MatchmakingResultNotification Deserialize(Stream stream, MatchmakingResultNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static MatchmakingResultNotification DeserializeLengthDelimited(Stream stream)
		{
			MatchmakingResultNotification matchmakingResultNotification = new MatchmakingResultNotification();
			DeserializeLengthDelimited(stream, matchmakingResultNotification);
			return matchmakingResultNotification;
		}

		public static MatchmakingResultNotification DeserializeLengthDelimited(Stream stream, MatchmakingResultNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static MatchmakingResultNotification Deserialize(Stream stream, MatchmakingResultNotification instance, long limit)
		{
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
					if (instance.RequestId == null)
					{
						instance.RequestId = RequestId.DeserializeLengthDelimited(stream);
					}
					else
					{
						RequestId.DeserializeLengthDelimited(stream, instance.RequestId);
					}
					continue;
				case 16:
					instance.Result = ProtocolParser.ReadUInt32(stream);
					continue;
				case 26:
					if (instance.ConnectInfo == null)
					{
						instance.ConnectInfo = ConnectInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						ConnectInfo.DeserializeLengthDelimited(stream, instance.ConnectInfo);
					}
					continue;
				case 34:
					if (instance.GameHandle == null)
					{
						instance.GameHandle = GameHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameHandle.DeserializeLengthDelimited(stream, instance.GameHandle);
					}
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

		public static void Serialize(Stream stream, MatchmakingResultNotification instance)
		{
			if (instance.HasRequestId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.RequestId.GetSerializedSize());
				RequestId.Serialize(stream, instance.RequestId);
			}
			if (instance.HasResult)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Result);
			}
			if (instance.HasConnectInfo)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.ConnectInfo.GetSerializedSize());
				ConnectInfo.Serialize(stream, instance.ConnectInfo);
			}
			if (instance.HasGameHandle)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
				GameHandle.Serialize(stream, instance.GameHandle);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasRequestId)
			{
				num++;
				uint serializedSize = RequestId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasResult)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Result);
			}
			if (HasConnectInfo)
			{
				num++;
				uint serializedSize2 = ConnectInfo.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasGameHandle)
			{
				num++;
				uint serializedSize3 = GameHandle.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}
	}
}
