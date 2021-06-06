using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	public class CreateGameBatchRequest : IProtoBuf
	{
		private List<CreateGameBatchServerEntry> _CreateRequestsPerServer = new List<CreateGameBatchServerEntry>();

		public List<CreateGameBatchServerEntry> CreateRequestsPerServer
		{
			get
			{
				return _CreateRequestsPerServer;
			}
			set
			{
				_CreateRequestsPerServer = value;
			}
		}

		public List<CreateGameBatchServerEntry> CreateRequestsPerServerList => _CreateRequestsPerServer;

		public int CreateRequestsPerServerCount => _CreateRequestsPerServer.Count;

		public bool IsInitialized => true;

		public void AddCreateRequestsPerServer(CreateGameBatchServerEntry val)
		{
			_CreateRequestsPerServer.Add(val);
		}

		public void ClearCreateRequestsPerServer()
		{
			_CreateRequestsPerServer.Clear();
		}

		public void SetCreateRequestsPerServer(List<CreateGameBatchServerEntry> val)
		{
			CreateRequestsPerServer = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (CreateGameBatchServerEntry item in CreateRequestsPerServer)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			CreateGameBatchRequest createGameBatchRequest = obj as CreateGameBatchRequest;
			if (createGameBatchRequest == null)
			{
				return false;
			}
			if (CreateRequestsPerServer.Count != createGameBatchRequest.CreateRequestsPerServer.Count)
			{
				return false;
			}
			for (int i = 0; i < CreateRequestsPerServer.Count; i++)
			{
				if (!CreateRequestsPerServer[i].Equals(createGameBatchRequest.CreateRequestsPerServer[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static CreateGameBatchRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateGameBatchRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CreateGameBatchRequest Deserialize(Stream stream, CreateGameBatchRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CreateGameBatchRequest DeserializeLengthDelimited(Stream stream)
		{
			CreateGameBatchRequest createGameBatchRequest = new CreateGameBatchRequest();
			DeserializeLengthDelimited(stream, createGameBatchRequest);
			return createGameBatchRequest;
		}

		public static CreateGameBatchRequest DeserializeLengthDelimited(Stream stream, CreateGameBatchRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CreateGameBatchRequest Deserialize(Stream stream, CreateGameBatchRequest instance, long limit)
		{
			if (instance.CreateRequestsPerServer == null)
			{
				instance.CreateRequestsPerServer = new List<CreateGameBatchServerEntry>();
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
					instance.CreateRequestsPerServer.Add(CreateGameBatchServerEntry.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, CreateGameBatchRequest instance)
		{
			if (instance.CreateRequestsPerServer.Count <= 0)
			{
				return;
			}
			foreach (CreateGameBatchServerEntry item in instance.CreateRequestsPerServer)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				CreateGameBatchServerEntry.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (CreateRequestsPerServer.Count > 0)
			{
				foreach (CreateGameBatchServerEntry item in CreateRequestsPerServer)
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
