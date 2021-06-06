using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.presence.v1
{
	public class PresenceState : IProtoBuf
	{
		public bool HasEntityId;

		private EntityId _EntityId;

		private List<FieldOperation> _FieldOperation = new List<FieldOperation>();

		public EntityId EntityId
		{
			get
			{
				return _EntityId;
			}
			set
			{
				_EntityId = value;
				HasEntityId = value != null;
			}
		}

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

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasEntityId)
			{
				num ^= EntityId.GetHashCode();
			}
			foreach (FieldOperation item in FieldOperation)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			PresenceState presenceState = obj as PresenceState;
			if (presenceState == null)
			{
				return false;
			}
			if (HasEntityId != presenceState.HasEntityId || (HasEntityId && !EntityId.Equals(presenceState.EntityId)))
			{
				return false;
			}
			if (FieldOperation.Count != presenceState.FieldOperation.Count)
			{
				return false;
			}
			for (int i = 0; i < FieldOperation.Count; i++)
			{
				if (!FieldOperation[i].Equals(presenceState.FieldOperation[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static PresenceState ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<PresenceState>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PresenceState Deserialize(Stream stream, PresenceState instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PresenceState DeserializeLengthDelimited(Stream stream)
		{
			PresenceState presenceState = new PresenceState();
			DeserializeLengthDelimited(stream, presenceState);
			return presenceState;
		}

		public static PresenceState DeserializeLengthDelimited(Stream stream, PresenceState instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PresenceState Deserialize(Stream stream, PresenceState instance, long limit)
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

		public static void Serialize(Stream stream, PresenceState instance)
		{
			if (instance.HasEntityId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.EntityId.GetSerializedSize());
				EntityId.Serialize(stream, instance.EntityId);
			}
			if (instance.FieldOperation.Count <= 0)
			{
				return;
			}
			foreach (FieldOperation item in instance.FieldOperation)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				bnet.protocol.presence.v1.FieldOperation.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasEntityId)
			{
				num++;
				uint serializedSize = EntityId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (FieldOperation.Count > 0)
			{
				foreach (FieldOperation item in FieldOperation)
				{
					num++;
					uint serializedSize2 = item.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
				return num;
			}
			return num;
		}
	}
}
