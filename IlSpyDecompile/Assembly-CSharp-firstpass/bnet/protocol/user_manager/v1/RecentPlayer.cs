using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.user_manager.v1
{
	public class RecentPlayer : IProtoBuf
	{
		public bool HasProgram;

		private string _Program;

		public bool HasTimestampPlayed;

		private ulong _TimestampPlayed;

		private List<Attribute> _Attributes = new List<Attribute>();

		public bool HasId;

		private uint _Id;

		public bool HasCounter;

		private uint _Counter;

		public EntityId EntityId { get; set; }

		public string Program
		{
			get
			{
				return _Program;
			}
			set
			{
				_Program = value;
				HasProgram = value != null;
			}
		}

		public ulong TimestampPlayed
		{
			get
			{
				return _TimestampPlayed;
			}
			set
			{
				_TimestampPlayed = value;
				HasTimestampPlayed = true;
			}
		}

		public List<Attribute> Attributes
		{
			get
			{
				return _Attributes;
			}
			set
			{
				_Attributes = value;
			}
		}

		public List<Attribute> AttributesList => _Attributes;

		public int AttributesCount => _Attributes.Count;

		public uint Id
		{
			get
			{
				return _Id;
			}
			set
			{
				_Id = value;
				HasId = true;
			}
		}

		public uint Counter
		{
			get
			{
				return _Counter;
			}
			set
			{
				_Counter = value;
				HasCounter = true;
			}
		}

		public bool IsInitialized => true;

		public void SetEntityId(EntityId val)
		{
			EntityId = val;
		}

		public void SetProgram(string val)
		{
			Program = val;
		}

		public void SetTimestampPlayed(ulong val)
		{
			TimestampPlayed = val;
		}

		public void AddAttributes(Attribute val)
		{
			_Attributes.Add(val);
		}

		public void ClearAttributes()
		{
			_Attributes.Clear();
		}

		public void SetAttributes(List<Attribute> val)
		{
			Attributes = val;
		}

		public void SetId(uint val)
		{
			Id = val;
		}

		public void SetCounter(uint val)
		{
			Counter = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= EntityId.GetHashCode();
			if (HasProgram)
			{
				hashCode ^= Program.GetHashCode();
			}
			if (HasTimestampPlayed)
			{
				hashCode ^= TimestampPlayed.GetHashCode();
			}
			foreach (Attribute attribute in Attributes)
			{
				hashCode ^= attribute.GetHashCode();
			}
			if (HasId)
			{
				hashCode ^= Id.GetHashCode();
			}
			if (HasCounter)
			{
				hashCode ^= Counter.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			RecentPlayer recentPlayer = obj as RecentPlayer;
			if (recentPlayer == null)
			{
				return false;
			}
			if (!EntityId.Equals(recentPlayer.EntityId))
			{
				return false;
			}
			if (HasProgram != recentPlayer.HasProgram || (HasProgram && !Program.Equals(recentPlayer.Program)))
			{
				return false;
			}
			if (HasTimestampPlayed != recentPlayer.HasTimestampPlayed || (HasTimestampPlayed && !TimestampPlayed.Equals(recentPlayer.TimestampPlayed)))
			{
				return false;
			}
			if (Attributes.Count != recentPlayer.Attributes.Count)
			{
				return false;
			}
			for (int i = 0; i < Attributes.Count; i++)
			{
				if (!Attributes[i].Equals(recentPlayer.Attributes[i]))
				{
					return false;
				}
			}
			if (HasId != recentPlayer.HasId || (HasId && !Id.Equals(recentPlayer.Id)))
			{
				return false;
			}
			if (HasCounter != recentPlayer.HasCounter || (HasCounter && !Counter.Equals(recentPlayer.Counter)))
			{
				return false;
			}
			return true;
		}

		public static RecentPlayer ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RecentPlayer>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RecentPlayer Deserialize(Stream stream, RecentPlayer instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RecentPlayer DeserializeLengthDelimited(Stream stream)
		{
			RecentPlayer recentPlayer = new RecentPlayer();
			DeserializeLengthDelimited(stream, recentPlayer);
			return recentPlayer;
		}

		public static RecentPlayer DeserializeLengthDelimited(Stream stream, RecentPlayer instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RecentPlayer Deserialize(Stream stream, RecentPlayer instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Attributes == null)
			{
				instance.Attributes = new List<Attribute>();
			}
			instance.Id = 0u;
			instance.Counter = 0u;
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
					instance.Program = ProtocolParser.ReadString(stream);
					continue;
				case 25:
					instance.TimestampPlayed = binaryReader.ReadUInt64();
					continue;
				case 34:
					instance.Attributes.Add(Attribute.DeserializeLengthDelimited(stream));
					continue;
				case 45:
					instance.Id = binaryReader.ReadUInt32();
					continue;
				case 53:
					instance.Counter = binaryReader.ReadUInt32();
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

		public static void Serialize(Stream stream, RecentPlayer instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.EntityId == null)
			{
				throw new ArgumentNullException("EntityId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.EntityId.GetSerializedSize());
			EntityId.Serialize(stream, instance.EntityId);
			if (instance.HasProgram)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Program));
			}
			if (instance.HasTimestampPlayed)
			{
				stream.WriteByte(25);
				binaryWriter.Write(instance.TimestampPlayed);
			}
			if (instance.Attributes.Count > 0)
			{
				foreach (Attribute attribute in instance.Attributes)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					Attribute.Serialize(stream, attribute);
				}
			}
			if (instance.HasId)
			{
				stream.WriteByte(45);
				binaryWriter.Write(instance.Id);
			}
			if (instance.HasCounter)
			{
				stream.WriteByte(53);
				binaryWriter.Write(instance.Counter);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = EntityId.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (HasProgram)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Program);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasTimestampPlayed)
			{
				num++;
				num += 8;
			}
			if (Attributes.Count > 0)
			{
				foreach (Attribute attribute in Attributes)
				{
					num++;
					uint serializedSize2 = attribute.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (HasId)
			{
				num++;
				num += 4;
			}
			if (HasCounter)
			{
				num++;
				num += 4;
			}
			return num + 1;
		}
	}
}
