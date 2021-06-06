using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.games.v1
{
	public class GameFactoryDescription : IProtoBuf
	{
		public bool HasName;

		private string _Name;

		private List<Attribute> _Attribute = new List<Attribute>();

		private List<GameStatsBucket> _StatsBucket = new List<GameStatsBucket>();

		public bool HasUnseededId;

		private ulong _UnseededId;

		public bool HasAllowQueueing;

		private bool _AllowQueueing;

		public bool HasRequiresPlayerRating;

		private bool _RequiresPlayerRating;

		public bool HasRequiresQueuePriority;

		private bool _RequiresQueuePriority;

		public ulong Id { get; set; }

		public string Name
		{
			get
			{
				return _Name;
			}
			set
			{
				_Name = value;
				HasName = value != null;
			}
		}

		public List<Attribute> Attribute
		{
			get
			{
				return _Attribute;
			}
			set
			{
				_Attribute = value;
			}
		}

		public List<Attribute> AttributeList => _Attribute;

		public int AttributeCount => _Attribute.Count;

		public List<GameStatsBucket> StatsBucket
		{
			get
			{
				return _StatsBucket;
			}
			set
			{
				_StatsBucket = value;
			}
		}

		public List<GameStatsBucket> StatsBucketList => _StatsBucket;

		public int StatsBucketCount => _StatsBucket.Count;

		public ulong UnseededId
		{
			get
			{
				return _UnseededId;
			}
			set
			{
				_UnseededId = value;
				HasUnseededId = true;
			}
		}

		public bool AllowQueueing
		{
			get
			{
				return _AllowQueueing;
			}
			set
			{
				_AllowQueueing = value;
				HasAllowQueueing = true;
			}
		}

		public bool RequiresPlayerRating
		{
			get
			{
				return _RequiresPlayerRating;
			}
			set
			{
				_RequiresPlayerRating = value;
				HasRequiresPlayerRating = true;
			}
		}

		public bool RequiresQueuePriority
		{
			get
			{
				return _RequiresQueuePriority;
			}
			set
			{
				_RequiresQueuePriority = value;
				HasRequiresQueuePriority = true;
			}
		}

		public bool IsInitialized => true;

		public void SetId(ulong val)
		{
			Id = val;
		}

		public void SetName(string val)
		{
			Name = val;
		}

		public void AddAttribute(Attribute val)
		{
			_Attribute.Add(val);
		}

		public void ClearAttribute()
		{
			_Attribute.Clear();
		}

		public void SetAttribute(List<Attribute> val)
		{
			Attribute = val;
		}

		public void AddStatsBucket(GameStatsBucket val)
		{
			_StatsBucket.Add(val);
		}

		public void ClearStatsBucket()
		{
			_StatsBucket.Clear();
		}

		public void SetStatsBucket(List<GameStatsBucket> val)
		{
			StatsBucket = val;
		}

		public void SetUnseededId(ulong val)
		{
			UnseededId = val;
		}

		public void SetAllowQueueing(bool val)
		{
			AllowQueueing = val;
		}

		public void SetRequiresPlayerRating(bool val)
		{
			RequiresPlayerRating = val;
		}

		public void SetRequiresQueuePriority(bool val)
		{
			RequiresQueuePriority = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Id.GetHashCode();
			if (HasName)
			{
				hashCode ^= Name.GetHashCode();
			}
			foreach (Attribute item in Attribute)
			{
				hashCode ^= item.GetHashCode();
			}
			foreach (GameStatsBucket item2 in StatsBucket)
			{
				hashCode ^= item2.GetHashCode();
			}
			if (HasUnseededId)
			{
				hashCode ^= UnseededId.GetHashCode();
			}
			if (HasAllowQueueing)
			{
				hashCode ^= AllowQueueing.GetHashCode();
			}
			if (HasRequiresPlayerRating)
			{
				hashCode ^= RequiresPlayerRating.GetHashCode();
			}
			if (HasRequiresQueuePriority)
			{
				hashCode ^= RequiresQueuePriority.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			GameFactoryDescription gameFactoryDescription = obj as GameFactoryDescription;
			if (gameFactoryDescription == null)
			{
				return false;
			}
			if (!Id.Equals(gameFactoryDescription.Id))
			{
				return false;
			}
			if (HasName != gameFactoryDescription.HasName || (HasName && !Name.Equals(gameFactoryDescription.Name)))
			{
				return false;
			}
			if (Attribute.Count != gameFactoryDescription.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < Attribute.Count; i++)
			{
				if (!Attribute[i].Equals(gameFactoryDescription.Attribute[i]))
				{
					return false;
				}
			}
			if (StatsBucket.Count != gameFactoryDescription.StatsBucket.Count)
			{
				return false;
			}
			for (int j = 0; j < StatsBucket.Count; j++)
			{
				if (!StatsBucket[j].Equals(gameFactoryDescription.StatsBucket[j]))
				{
					return false;
				}
			}
			if (HasUnseededId != gameFactoryDescription.HasUnseededId || (HasUnseededId && !UnseededId.Equals(gameFactoryDescription.UnseededId)))
			{
				return false;
			}
			if (HasAllowQueueing != gameFactoryDescription.HasAllowQueueing || (HasAllowQueueing && !AllowQueueing.Equals(gameFactoryDescription.AllowQueueing)))
			{
				return false;
			}
			if (HasRequiresPlayerRating != gameFactoryDescription.HasRequiresPlayerRating || (HasRequiresPlayerRating && !RequiresPlayerRating.Equals(gameFactoryDescription.RequiresPlayerRating)))
			{
				return false;
			}
			if (HasRequiresQueuePriority != gameFactoryDescription.HasRequiresQueuePriority || (HasRequiresQueuePriority && !RequiresQueuePriority.Equals(gameFactoryDescription.RequiresQueuePriority)))
			{
				return false;
			}
			return true;
		}

		public static GameFactoryDescription ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameFactoryDescription>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameFactoryDescription Deserialize(Stream stream, GameFactoryDescription instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameFactoryDescription DeserializeLengthDelimited(Stream stream)
		{
			GameFactoryDescription gameFactoryDescription = new GameFactoryDescription();
			DeserializeLengthDelimited(stream, gameFactoryDescription);
			return gameFactoryDescription;
		}

		public static GameFactoryDescription DeserializeLengthDelimited(Stream stream, GameFactoryDescription instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameFactoryDescription Deserialize(Stream stream, GameFactoryDescription instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<Attribute>();
			}
			if (instance.StatsBucket == null)
			{
				instance.StatsBucket = new List<GameStatsBucket>();
			}
			instance.UnseededId = 0uL;
			instance.AllowQueueing = true;
			instance.RequiresPlayerRating = false;
			instance.RequiresQueuePriority = false;
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
				case 9:
					instance.Id = binaryReader.ReadUInt64();
					continue;
				case 18:
					instance.Name = ProtocolParser.ReadString(stream);
					continue;
				case 26:
					instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
					continue;
				case 34:
					instance.StatsBucket.Add(GameStatsBucket.DeserializeLengthDelimited(stream));
					continue;
				case 41:
					instance.UnseededId = binaryReader.ReadUInt64();
					continue;
				case 48:
					instance.AllowQueueing = ProtocolParser.ReadBool(stream);
					continue;
				case 56:
					instance.RequiresPlayerRating = ProtocolParser.ReadBool(stream);
					continue;
				case 64:
					instance.RequiresQueuePriority = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, GameFactoryDescription instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(9);
			binaryWriter.Write(instance.Id);
			if (instance.HasName)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (Attribute item in instance.Attribute)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.Attribute.Serialize(stream, item);
				}
			}
			if (instance.StatsBucket.Count > 0)
			{
				foreach (GameStatsBucket item2 in instance.StatsBucket)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, item2.GetSerializedSize());
					GameStatsBucket.Serialize(stream, item2);
				}
			}
			if (instance.HasUnseededId)
			{
				stream.WriteByte(41);
				binaryWriter.Write(instance.UnseededId);
			}
			if (instance.HasAllowQueueing)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.AllowQueueing);
			}
			if (instance.HasRequiresPlayerRating)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.RequiresPlayerRating);
			}
			if (instance.HasRequiresQueuePriority)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteBool(stream, instance.RequiresQueuePriority);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += 8;
			if (HasName)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (Attribute.Count > 0)
			{
				foreach (Attribute item in Attribute)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (StatsBucket.Count > 0)
			{
				foreach (GameStatsBucket item2 in StatsBucket)
				{
					num++;
					uint serializedSize2 = item2.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (HasUnseededId)
			{
				num++;
				num += 8;
			}
			if (HasAllowQueueing)
			{
				num++;
				num++;
			}
			if (HasRequiresPlayerRating)
			{
				num++;
				num++;
			}
			if (HasRequiresQueuePriority)
			{
				num++;
				num++;
			}
			return num + 1;
		}
	}
}
