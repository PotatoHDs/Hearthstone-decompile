using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.presence.v1
{
	public class BatchSubscribeRequest : IProtoBuf
	{
		public bool HasAgentId;

		private EntityId _AgentId;

		private List<EntityId> _EntityId = new List<EntityId>();

		private List<uint> _Program = new List<uint>();

		private List<FieldKey> _Key = new List<FieldKey>();

		public bool HasObjectId;

		private ulong _ObjectId;

		public EntityId AgentId
		{
			get
			{
				return _AgentId;
			}
			set
			{
				_AgentId = value;
				HasAgentId = value != null;
			}
		}

		public List<EntityId> EntityId
		{
			get
			{
				return _EntityId;
			}
			set
			{
				_EntityId = value;
			}
		}

		public List<EntityId> EntityIdList => _EntityId;

		public int EntityIdCount => _EntityId.Count;

		public List<uint> Program
		{
			get
			{
				return _Program;
			}
			set
			{
				_Program = value;
			}
		}

		public List<uint> ProgramList => _Program;

		public int ProgramCount => _Program.Count;

		public List<FieldKey> Key
		{
			get
			{
				return _Key;
			}
			set
			{
				_Key = value;
			}
		}

		public List<FieldKey> KeyList => _Key;

		public int KeyCount => _Key.Count;

		public ulong ObjectId
		{
			get
			{
				return _ObjectId;
			}
			set
			{
				_ObjectId = value;
				HasObjectId = true;
			}
		}

		public bool IsInitialized => true;

		public void SetAgentId(EntityId val)
		{
			AgentId = val;
		}

		public void AddEntityId(EntityId val)
		{
			_EntityId.Add(val);
		}

		public void ClearEntityId()
		{
			_EntityId.Clear();
		}

		public void SetEntityId(List<EntityId> val)
		{
			EntityId = val;
		}

		public void AddProgram(uint val)
		{
			_Program.Add(val);
		}

		public void ClearProgram()
		{
			_Program.Clear();
		}

		public void SetProgram(List<uint> val)
		{
			Program = val;
		}

		public void AddKey(FieldKey val)
		{
			_Key.Add(val);
		}

		public void ClearKey()
		{
			_Key.Clear();
		}

		public void SetKey(List<FieldKey> val)
		{
			Key = val;
		}

		public void SetObjectId(ulong val)
		{
			ObjectId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAgentId)
			{
				num ^= AgentId.GetHashCode();
			}
			foreach (EntityId item in EntityId)
			{
				num ^= item.GetHashCode();
			}
			foreach (uint item2 in Program)
			{
				num ^= item2.GetHashCode();
			}
			foreach (FieldKey item3 in Key)
			{
				num ^= item3.GetHashCode();
			}
			if (HasObjectId)
			{
				num ^= ObjectId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			BatchSubscribeRequest batchSubscribeRequest = obj as BatchSubscribeRequest;
			if (batchSubscribeRequest == null)
			{
				return false;
			}
			if (HasAgentId != batchSubscribeRequest.HasAgentId || (HasAgentId && !AgentId.Equals(batchSubscribeRequest.AgentId)))
			{
				return false;
			}
			if (EntityId.Count != batchSubscribeRequest.EntityId.Count)
			{
				return false;
			}
			for (int i = 0; i < EntityId.Count; i++)
			{
				if (!EntityId[i].Equals(batchSubscribeRequest.EntityId[i]))
				{
					return false;
				}
			}
			if (Program.Count != batchSubscribeRequest.Program.Count)
			{
				return false;
			}
			for (int j = 0; j < Program.Count; j++)
			{
				if (!Program[j].Equals(batchSubscribeRequest.Program[j]))
				{
					return false;
				}
			}
			if (Key.Count != batchSubscribeRequest.Key.Count)
			{
				return false;
			}
			for (int k = 0; k < Key.Count; k++)
			{
				if (!Key[k].Equals(batchSubscribeRequest.Key[k]))
				{
					return false;
				}
			}
			if (HasObjectId != batchSubscribeRequest.HasObjectId || (HasObjectId && !ObjectId.Equals(batchSubscribeRequest.ObjectId)))
			{
				return false;
			}
			return true;
		}

		public static BatchSubscribeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<BatchSubscribeRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static BatchSubscribeRequest Deserialize(Stream stream, BatchSubscribeRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static BatchSubscribeRequest DeserializeLengthDelimited(Stream stream)
		{
			BatchSubscribeRequest batchSubscribeRequest = new BatchSubscribeRequest();
			DeserializeLengthDelimited(stream, batchSubscribeRequest);
			return batchSubscribeRequest;
		}

		public static BatchSubscribeRequest DeserializeLengthDelimited(Stream stream, BatchSubscribeRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static BatchSubscribeRequest Deserialize(Stream stream, BatchSubscribeRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.EntityId == null)
			{
				instance.EntityId = new List<EntityId>();
			}
			if (instance.Program == null)
			{
				instance.Program = new List<uint>();
			}
			if (instance.Key == null)
			{
				instance.Key = new List<FieldKey>();
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
					if (instance.AgentId == null)
					{
						instance.AgentId = bnet.protocol.EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						bnet.protocol.EntityId.DeserializeLengthDelimited(stream, instance.AgentId);
					}
					continue;
				case 18:
					instance.EntityId.Add(bnet.protocol.EntityId.DeserializeLengthDelimited(stream));
					continue;
				case 29:
					instance.Program.Add(binaryReader.ReadUInt32());
					continue;
				case 34:
					instance.Key.Add(FieldKey.DeserializeLengthDelimited(stream));
					continue;
				case 40:
					instance.ObjectId = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, BatchSubscribeRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				bnet.protocol.EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.EntityId.Count > 0)
			{
				foreach (EntityId item in instance.EntityId)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.EntityId.Serialize(stream, item);
				}
			}
			if (instance.Program.Count > 0)
			{
				foreach (uint item2 in instance.Program)
				{
					stream.WriteByte(29);
					binaryWriter.Write(item2);
				}
			}
			if (instance.Key.Count > 0)
			{
				foreach (FieldKey item3 in instance.Key)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, item3.GetSerializedSize());
					FieldKey.Serialize(stream, item3);
				}
			}
			if (instance.HasObjectId)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, instance.ObjectId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAgentId)
			{
				num++;
				uint serializedSize = AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (EntityId.Count > 0)
			{
				foreach (EntityId item in EntityId)
				{
					num++;
					uint serializedSize2 = item.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (Program.Count > 0)
			{
				foreach (uint item2 in Program)
				{
					_ = item2;
					num++;
					num += 4;
				}
			}
			if (Key.Count > 0)
			{
				foreach (FieldKey item3 in Key)
				{
					num++;
					uint serializedSize3 = item3.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			if (HasObjectId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(ObjectId);
			}
			return num;
		}
	}
}
