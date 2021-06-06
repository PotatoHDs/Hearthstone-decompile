using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.presence.v1
{
	public class SubscribeRequest : IProtoBuf
	{
		public bool HasAgentId;

		private EntityId _AgentId;

		private List<uint> _Program = new List<uint>();

		private List<FieldKey> _Key = new List<FieldKey>();

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

		public EntityId EntityId { get; set; }

		public ulong ObjectId { get; set; }

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

		public bool IsInitialized => true;

		public void SetAgentId(EntityId val)
		{
			AgentId = val;
		}

		public void SetEntityId(EntityId val)
		{
			EntityId = val;
		}

		public void SetObjectId(ulong val)
		{
			ObjectId = val;
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

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAgentId)
			{
				num ^= AgentId.GetHashCode();
			}
			num ^= EntityId.GetHashCode();
			num ^= ObjectId.GetHashCode();
			foreach (uint item in Program)
			{
				num ^= item.GetHashCode();
			}
			foreach (FieldKey item2 in Key)
			{
				num ^= item2.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SubscribeRequest subscribeRequest = obj as SubscribeRequest;
			if (subscribeRequest == null)
			{
				return false;
			}
			if (HasAgentId != subscribeRequest.HasAgentId || (HasAgentId && !AgentId.Equals(subscribeRequest.AgentId)))
			{
				return false;
			}
			if (!EntityId.Equals(subscribeRequest.EntityId))
			{
				return false;
			}
			if (!ObjectId.Equals(subscribeRequest.ObjectId))
			{
				return false;
			}
			if (Program.Count != subscribeRequest.Program.Count)
			{
				return false;
			}
			for (int i = 0; i < Program.Count; i++)
			{
				if (!Program[i].Equals(subscribeRequest.Program[i]))
				{
					return false;
				}
			}
			if (Key.Count != subscribeRequest.Key.Count)
			{
				return false;
			}
			for (int j = 0; j < Key.Count; j++)
			{
				if (!Key[j].Equals(subscribeRequest.Key[j]))
				{
					return false;
				}
			}
			return true;
		}

		public static SubscribeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SubscribeRequest Deserialize(Stream stream, SubscribeRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SubscribeRequest DeserializeLengthDelimited(Stream stream)
		{
			SubscribeRequest subscribeRequest = new SubscribeRequest();
			DeserializeLengthDelimited(stream, subscribeRequest);
			return subscribeRequest;
		}

		public static SubscribeRequest DeserializeLengthDelimited(Stream stream, SubscribeRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SubscribeRequest Deserialize(Stream stream, SubscribeRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
						instance.AgentId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.AgentId);
					}
					continue;
				case 18:
					if (instance.EntityId == null)
					{
						instance.EntityId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.EntityId);
					}
					continue;
				case 24:
					instance.ObjectId = ProtocolParser.ReadUInt64(stream);
					continue;
				case 37:
					instance.Program.Add(binaryReader.ReadUInt32());
					continue;
				case 50:
					instance.Key.Add(FieldKey.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, SubscribeRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.EntityId == null)
			{
				throw new ArgumentNullException("EntityId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.EntityId.GetSerializedSize());
			EntityId.Serialize(stream, instance.EntityId);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, instance.ObjectId);
			if (instance.Program.Count > 0)
			{
				foreach (uint item in instance.Program)
				{
					stream.WriteByte(37);
					binaryWriter.Write(item);
				}
			}
			if (instance.Key.Count <= 0)
			{
				return;
			}
			foreach (FieldKey item2 in instance.Key)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, item2.GetSerializedSize());
				FieldKey.Serialize(stream, item2);
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
			uint serializedSize2 = EntityId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			num += ProtocolParser.SizeOfUInt64(ObjectId);
			if (Program.Count > 0)
			{
				foreach (uint item in Program)
				{
					_ = item;
					num++;
					num += 4;
				}
			}
			if (Key.Count > 0)
			{
				foreach (FieldKey item2 in Key)
				{
					num++;
					uint serializedSize3 = item2.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			return num + 2;
		}
	}
}
