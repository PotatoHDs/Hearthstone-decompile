using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.presence.v1
{
	public class QueryRequest : IProtoBuf
	{
		private List<FieldKey> _Key = new List<FieldKey>();

		public bool HasAgentId;

		private EntityId _AgentId;

		public EntityId EntityId { get; set; }

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

		public void SetAgentId(EntityId val)
		{
			AgentId = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= EntityId.GetHashCode();
			foreach (FieldKey item in Key)
			{
				hashCode ^= item.GetHashCode();
			}
			if (HasAgentId)
			{
				hashCode ^= AgentId.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			QueryRequest queryRequest = obj as QueryRequest;
			if (queryRequest == null)
			{
				return false;
			}
			if (!EntityId.Equals(queryRequest.EntityId))
			{
				return false;
			}
			if (Key.Count != queryRequest.Key.Count)
			{
				return false;
			}
			for (int i = 0; i < Key.Count; i++)
			{
				if (!Key[i].Equals(queryRequest.Key[i]))
				{
					return false;
				}
			}
			if (HasAgentId != queryRequest.HasAgentId || (HasAgentId && !AgentId.Equals(queryRequest.AgentId)))
			{
				return false;
			}
			return true;
		}

		public static QueryRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<QueryRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static QueryRequest Deserialize(Stream stream, QueryRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static QueryRequest DeserializeLengthDelimited(Stream stream)
		{
			QueryRequest queryRequest = new QueryRequest();
			DeserializeLengthDelimited(stream, queryRequest);
			return queryRequest;
		}

		public static QueryRequest DeserializeLengthDelimited(Stream stream, QueryRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static QueryRequest Deserialize(Stream stream, QueryRequest instance, long limit)
		{
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
					instance.Key.Add(FieldKey.DeserializeLengthDelimited(stream));
					continue;
				case 26:
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

		public static void Serialize(Stream stream, QueryRequest instance)
		{
			if (instance.EntityId == null)
			{
				throw new ArgumentNullException("EntityId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.EntityId.GetSerializedSize());
			EntityId.Serialize(stream, instance.EntityId);
			if (instance.Key.Count > 0)
			{
				foreach (FieldKey item in instance.Key)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					FieldKey.Serialize(stream, item);
				}
			}
			if (instance.HasAgentId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = EntityId.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (Key.Count > 0)
			{
				foreach (FieldKey item in Key)
				{
					num++;
					uint serializedSize2 = item.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
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
