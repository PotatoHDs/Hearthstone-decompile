using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	public class GetFindGameRequestsResponse : IProtoBuf
	{
		private List<FindGameRequest> _FindGameRequest = new List<FindGameRequest>();

		public bool HasQueueDepth;

		private uint _QueueDepth;

		public List<FindGameRequest> FindGameRequest
		{
			get
			{
				return _FindGameRequest;
			}
			set
			{
				_FindGameRequest = value;
			}
		}

		public List<FindGameRequest> FindGameRequestList => _FindGameRequest;

		public int FindGameRequestCount => _FindGameRequest.Count;

		public uint QueueDepth
		{
			get
			{
				return _QueueDepth;
			}
			set
			{
				_QueueDepth = value;
				HasQueueDepth = true;
			}
		}

		public bool IsInitialized => true;

		public void AddFindGameRequest(FindGameRequest val)
		{
			_FindGameRequest.Add(val);
		}

		public void ClearFindGameRequest()
		{
			_FindGameRequest.Clear();
		}

		public void SetFindGameRequest(List<FindGameRequest> val)
		{
			FindGameRequest = val;
		}

		public void SetQueueDepth(uint val)
		{
			QueueDepth = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (FindGameRequest item in FindGameRequest)
			{
				num ^= item.GetHashCode();
			}
			if (HasQueueDepth)
			{
				num ^= QueueDepth.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetFindGameRequestsResponse getFindGameRequestsResponse = obj as GetFindGameRequestsResponse;
			if (getFindGameRequestsResponse == null)
			{
				return false;
			}
			if (FindGameRequest.Count != getFindGameRequestsResponse.FindGameRequest.Count)
			{
				return false;
			}
			for (int i = 0; i < FindGameRequest.Count; i++)
			{
				if (!FindGameRequest[i].Equals(getFindGameRequestsResponse.FindGameRequest[i]))
				{
					return false;
				}
			}
			if (HasQueueDepth != getFindGameRequestsResponse.HasQueueDepth || (HasQueueDepth && !QueueDepth.Equals(getFindGameRequestsResponse.QueueDepth)))
			{
				return false;
			}
			return true;
		}

		public static GetFindGameRequestsResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetFindGameRequestsResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetFindGameRequestsResponse Deserialize(Stream stream, GetFindGameRequestsResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetFindGameRequestsResponse DeserializeLengthDelimited(Stream stream)
		{
			GetFindGameRequestsResponse getFindGameRequestsResponse = new GetFindGameRequestsResponse();
			DeserializeLengthDelimited(stream, getFindGameRequestsResponse);
			return getFindGameRequestsResponse;
		}

		public static GetFindGameRequestsResponse DeserializeLengthDelimited(Stream stream, GetFindGameRequestsResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetFindGameRequestsResponse Deserialize(Stream stream, GetFindGameRequestsResponse instance, long limit)
		{
			if (instance.FindGameRequest == null)
			{
				instance.FindGameRequest = new List<FindGameRequest>();
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
					instance.FindGameRequest.Add(bnet.protocol.games.v1.FindGameRequest.DeserializeLengthDelimited(stream));
					continue;
				case 16:
					instance.QueueDepth = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, GetFindGameRequestsResponse instance)
		{
			if (instance.FindGameRequest.Count > 0)
			{
				foreach (FindGameRequest item in instance.FindGameRequest)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.games.v1.FindGameRequest.Serialize(stream, item);
				}
			}
			if (instance.HasQueueDepth)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.QueueDepth);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (FindGameRequest.Count > 0)
			{
				foreach (FindGameRequest item in FindGameRequest)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasQueueDepth)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(QueueDepth);
			}
			return num;
		}
	}
}
