using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.presence.v1
{
	public class UpdateRequest : IProtoBuf
	{
		private List<FieldOperation> _FieldOperation = new List<FieldOperation>();

		public bool HasNoCreate;

		private bool _NoCreate;

		public bool HasAgentId;

		private EntityId _AgentId;

		public EntityId EntityId { get; set; }

		public List<FieldOperation> FieldOperation
		{
			get
			{
				return _FieldOperation;
			}
			set
			{
				_FieldOperation = value;
			}
		}

		public List<FieldOperation> FieldOperationList => _FieldOperation;

		public int FieldOperationCount => _FieldOperation.Count;

		public bool NoCreate
		{
			get
			{
				return _NoCreate;
			}
			set
			{
				_NoCreate = value;
				HasNoCreate = true;
			}
		}

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

		public bool IsInitialized => true;

		public void SetEntityId(EntityId val)
		{
			EntityId = val;
		}

		public void AddFieldOperation(FieldOperation val)
		{
			_FieldOperation.Add(val);
		}

		public void ClearFieldOperation()
		{
			_FieldOperation.Clear();
		}

		public void SetFieldOperation(List<FieldOperation> val)
		{
			FieldOperation = val;
		}

		public void SetNoCreate(bool val)
		{
			NoCreate = val;
		}

		public void SetAgentId(EntityId val)
		{
			AgentId = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= EntityId.GetHashCode();
			foreach (FieldOperation item in FieldOperation)
			{
				hashCode ^= item.GetHashCode();
			}
			if (HasNoCreate)
			{
				hashCode ^= NoCreate.GetHashCode();
			}
			if (HasAgentId)
			{
				hashCode ^= AgentId.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			UpdateRequest updateRequest = obj as UpdateRequest;
			if (updateRequest == null)
			{
				return false;
			}
			if (!EntityId.Equals(updateRequest.EntityId))
			{
				return false;
			}
			if (FieldOperation.Count != updateRequest.FieldOperation.Count)
			{
				return false;
			}
			for (int i = 0; i < FieldOperation.Count; i++)
			{
				if (!FieldOperation[i].Equals(updateRequest.FieldOperation[i]))
				{
					return false;
				}
			}
			if (HasNoCreate != updateRequest.HasNoCreate || (HasNoCreate && !NoCreate.Equals(updateRequest.NoCreate)))
			{
				return false;
			}
			if (HasAgentId != updateRequest.HasAgentId || (HasAgentId && !AgentId.Equals(updateRequest.AgentId)))
			{
				return false;
			}
			return true;
		}

		public static UpdateRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static UpdateRequest Deserialize(Stream stream, UpdateRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static UpdateRequest DeserializeLengthDelimited(Stream stream)
		{
			UpdateRequest updateRequest = new UpdateRequest();
			DeserializeLengthDelimited(stream, updateRequest);
			return updateRequest;
		}

		public static UpdateRequest DeserializeLengthDelimited(Stream stream, UpdateRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static UpdateRequest Deserialize(Stream stream, UpdateRequest instance, long limit)
		{
			if (instance.FieldOperation == null)
			{
				instance.FieldOperation = new List<FieldOperation>();
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
					if (instance.EntityId == null)
					{
						instance.EntityId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.EntityId);
					}
					continue;
				case 18:
					instance.FieldOperation.Add(bnet.protocol.presence.v1.FieldOperation.DeserializeLengthDelimited(stream));
					continue;
				case 24:
					instance.NoCreate = ProtocolParser.ReadBool(stream);
					continue;
				case 34:
					if (instance.AgentId == null)
					{
						instance.AgentId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.AgentId);
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

		public static void Serialize(Stream stream, UpdateRequest instance)
		{
			if (instance.EntityId == null)
			{
				throw new ArgumentNullException("EntityId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.EntityId.GetSerializedSize());
			EntityId.Serialize(stream, instance.EntityId);
			if (instance.FieldOperation.Count > 0)
			{
				foreach (FieldOperation item in instance.FieldOperation)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.presence.v1.FieldOperation.Serialize(stream, item);
				}
			}
			if (instance.HasNoCreate)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.NoCreate);
			}
			if (instance.HasAgentId)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = EntityId.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (FieldOperation.Count > 0)
			{
				foreach (FieldOperation item in FieldOperation)
				{
					num++;
					uint serializedSize2 = item.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (HasNoCreate)
			{
				num++;
				num++;
			}
			if (HasAgentId)
			{
				num++;
				uint serializedSize3 = AgentId.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num + 1;
		}
	}
}
