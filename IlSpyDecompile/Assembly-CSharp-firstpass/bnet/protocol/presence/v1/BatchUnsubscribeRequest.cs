using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.presence.v1
{
	public class BatchUnsubscribeRequest : IProtoBuf
	{
		public bool HasAgentId;

		private EntityId _AgentId;

		private List<EntityId> _EntityId = new List<EntityId>();

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
			if (HasObjectId)
			{
				num ^= ObjectId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			BatchUnsubscribeRequest batchUnsubscribeRequest = obj as BatchUnsubscribeRequest;
			if (batchUnsubscribeRequest == null)
			{
				return false;
			}
			if (HasAgentId != batchUnsubscribeRequest.HasAgentId || (HasAgentId && !AgentId.Equals(batchUnsubscribeRequest.AgentId)))
			{
				return false;
			}
			if (EntityId.Count != batchUnsubscribeRequest.EntityId.Count)
			{
				return false;
			}
			for (int i = 0; i < EntityId.Count; i++)
			{
				if (!EntityId[i].Equals(batchUnsubscribeRequest.EntityId[i]))
				{
					return false;
				}
			}
			if (HasObjectId != batchUnsubscribeRequest.HasObjectId || (HasObjectId && !ObjectId.Equals(batchUnsubscribeRequest.ObjectId)))
			{
				return false;
			}
			return true;
		}

		public static BatchUnsubscribeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<BatchUnsubscribeRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static BatchUnsubscribeRequest Deserialize(Stream stream, BatchUnsubscribeRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static BatchUnsubscribeRequest DeserializeLengthDelimited(Stream stream)
		{
			BatchUnsubscribeRequest batchUnsubscribeRequest = new BatchUnsubscribeRequest();
			DeserializeLengthDelimited(stream, batchUnsubscribeRequest);
			return batchUnsubscribeRequest;
		}

		public static BatchUnsubscribeRequest DeserializeLengthDelimited(Stream stream, BatchUnsubscribeRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static BatchUnsubscribeRequest Deserialize(Stream stream, BatchUnsubscribeRequest instance, long limit)
		{
			if (instance.EntityId == null)
			{
				instance.EntityId = new List<EntityId>();
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
				case 24:
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

		public static void Serialize(Stream stream, BatchUnsubscribeRequest instance)
		{
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
			if (instance.HasObjectId)
			{
				stream.WriteByte(24);
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
			if (HasObjectId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(ObjectId);
			}
			return num;
		}
	}
}
