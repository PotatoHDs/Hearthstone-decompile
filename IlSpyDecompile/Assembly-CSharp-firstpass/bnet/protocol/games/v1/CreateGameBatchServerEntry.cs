using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	public class CreateGameBatchServerEntry : IProtoBuf
	{
		public bool HasHost;

		private ProcessId _Host;

		private List<CreateGameRequest> _CreateRequests = new List<CreateGameRequest>();

		public ProcessId Host
		{
			get
			{
				return _Host;
			}
			set
			{
				_Host = value;
				HasHost = value != null;
			}
		}

		public List<CreateGameRequest> CreateRequests
		{
			get
			{
				return _CreateRequests;
			}
			set
			{
				_CreateRequests = value;
			}
		}

		public List<CreateGameRequest> CreateRequestsList => _CreateRequests;

		public int CreateRequestsCount => _CreateRequests.Count;

		public bool IsInitialized => true;

		public void SetHost(ProcessId val)
		{
			Host = val;
		}

		public void AddCreateRequests(CreateGameRequest val)
		{
			_CreateRequests.Add(val);
		}

		public void ClearCreateRequests()
		{
			_CreateRequests.Clear();
		}

		public void SetCreateRequests(List<CreateGameRequest> val)
		{
			CreateRequests = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasHost)
			{
				num ^= Host.GetHashCode();
			}
			foreach (CreateGameRequest createRequest in CreateRequests)
			{
				num ^= createRequest.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			CreateGameBatchServerEntry createGameBatchServerEntry = obj as CreateGameBatchServerEntry;
			if (createGameBatchServerEntry == null)
			{
				return false;
			}
			if (HasHost != createGameBatchServerEntry.HasHost || (HasHost && !Host.Equals(createGameBatchServerEntry.Host)))
			{
				return false;
			}
			if (CreateRequests.Count != createGameBatchServerEntry.CreateRequests.Count)
			{
				return false;
			}
			for (int i = 0; i < CreateRequests.Count; i++)
			{
				if (!CreateRequests[i].Equals(createGameBatchServerEntry.CreateRequests[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static CreateGameBatchServerEntry ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateGameBatchServerEntry>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CreateGameBatchServerEntry Deserialize(Stream stream, CreateGameBatchServerEntry instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CreateGameBatchServerEntry DeserializeLengthDelimited(Stream stream)
		{
			CreateGameBatchServerEntry createGameBatchServerEntry = new CreateGameBatchServerEntry();
			DeserializeLengthDelimited(stream, createGameBatchServerEntry);
			return createGameBatchServerEntry;
		}

		public static CreateGameBatchServerEntry DeserializeLengthDelimited(Stream stream, CreateGameBatchServerEntry instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CreateGameBatchServerEntry Deserialize(Stream stream, CreateGameBatchServerEntry instance, long limit)
		{
			if (instance.CreateRequests == null)
			{
				instance.CreateRequests = new List<CreateGameRequest>();
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
					if (instance.Host == null)
					{
						instance.Host = ProcessId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProcessId.DeserializeLengthDelimited(stream, instance.Host);
					}
					continue;
				case 18:
					instance.CreateRequests.Add(CreateGameRequest.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, CreateGameBatchServerEntry instance)
		{
			if (instance.HasHost)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Host);
			}
			if (instance.CreateRequests.Count <= 0)
			{
				return;
			}
			foreach (CreateGameRequest createRequest in instance.CreateRequests)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, createRequest.GetSerializedSize());
				CreateGameRequest.Serialize(stream, createRequest);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasHost)
			{
				num++;
				uint serializedSize = Host.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (CreateRequests.Count > 0)
			{
				foreach (CreateGameRequest createRequest in CreateRequests)
				{
					num++;
					uint serializedSize2 = createRequest.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
				return num;
			}
			return num;
		}
	}
}
