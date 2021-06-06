using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class Collection : IProtoBuf
	{
		private List<CardStack> _Stacks = new List<CardStack>();

		public bool HasCollectionVersion;

		private long _CollectionVersion;

		public bool HasCollectionVersionLastModified;

		private long _CollectionVersionLastModified;

		public List<CardStack> Stacks
		{
			get
			{
				return _Stacks;
			}
			set
			{
				_Stacks = value;
			}
		}

		public long CollectionVersion
		{
			get
			{
				return _CollectionVersion;
			}
			set
			{
				_CollectionVersion = value;
				HasCollectionVersion = true;
			}
		}

		public long CollectionVersionLastModified
		{
			get
			{
				return _CollectionVersionLastModified;
			}
			set
			{
				_CollectionVersionLastModified = value;
				HasCollectionVersionLastModified = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (CardStack stack in Stacks)
			{
				num ^= stack.GetHashCode();
			}
			if (HasCollectionVersion)
			{
				num ^= CollectionVersion.GetHashCode();
			}
			if (HasCollectionVersionLastModified)
			{
				num ^= CollectionVersionLastModified.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			Collection collection = obj as Collection;
			if (collection == null)
			{
				return false;
			}
			if (Stacks.Count != collection.Stacks.Count)
			{
				return false;
			}
			for (int i = 0; i < Stacks.Count; i++)
			{
				if (!Stacks[i].Equals(collection.Stacks[i]))
				{
					return false;
				}
			}
			if (HasCollectionVersion != collection.HasCollectionVersion || (HasCollectionVersion && !CollectionVersion.Equals(collection.CollectionVersion)))
			{
				return false;
			}
			if (HasCollectionVersionLastModified != collection.HasCollectionVersionLastModified || (HasCollectionVersionLastModified && !CollectionVersionLastModified.Equals(collection.CollectionVersionLastModified)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Collection Deserialize(Stream stream, Collection instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Collection DeserializeLengthDelimited(Stream stream)
		{
			Collection collection = new Collection();
			DeserializeLengthDelimited(stream, collection);
			return collection;
		}

		public static Collection DeserializeLengthDelimited(Stream stream, Collection instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Collection Deserialize(Stream stream, Collection instance, long limit)
		{
			if (instance.Stacks == null)
			{
				instance.Stacks = new List<CardStack>();
			}
			instance.CollectionVersion = 0L;
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
					instance.Stacks.Add(CardStack.DeserializeLengthDelimited(stream));
					continue;
				case 16:
					instance.CollectionVersion = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.CollectionVersionLastModified = (long)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, Collection instance)
		{
			if (instance.Stacks.Count > 0)
			{
				foreach (CardStack stack in instance.Stacks)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, stack.GetSerializedSize());
					CardStack.Serialize(stream, stack);
				}
			}
			if (instance.HasCollectionVersion)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CollectionVersion);
			}
			if (instance.HasCollectionVersionLastModified)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CollectionVersionLastModified);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Stacks.Count > 0)
			{
				foreach (CardStack stack in Stacks)
				{
					num++;
					uint serializedSize = stack.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasCollectionVersion)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)CollectionVersion);
			}
			if (HasCollectionVersionLastModified)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)CollectionVersionLastModified);
			}
			return num;
		}
	}
}
