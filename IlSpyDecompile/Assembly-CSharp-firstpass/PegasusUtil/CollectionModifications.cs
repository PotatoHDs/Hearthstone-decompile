using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	public class CollectionModifications : IProtoBuf
	{
		private List<CardModification> _CardModifications = new List<CardModification>();

		public bool HasCollectionVersion;

		private long _CollectionVersion;

		public List<CardModification> CardModifications
		{
			get
			{
				return _CardModifications;
			}
			set
			{
				_CardModifications = value;
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

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (CardModification cardModification in CardModifications)
			{
				num ^= cardModification.GetHashCode();
			}
			if (HasCollectionVersion)
			{
				num ^= CollectionVersion.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			CollectionModifications collectionModifications = obj as CollectionModifications;
			if (collectionModifications == null)
			{
				return false;
			}
			if (CardModifications.Count != collectionModifications.CardModifications.Count)
			{
				return false;
			}
			for (int i = 0; i < CardModifications.Count; i++)
			{
				if (!CardModifications[i].Equals(collectionModifications.CardModifications[i]))
				{
					return false;
				}
			}
			if (HasCollectionVersion != collectionModifications.HasCollectionVersion || (HasCollectionVersion && !CollectionVersion.Equals(collectionModifications.CollectionVersion)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CollectionModifications Deserialize(Stream stream, CollectionModifications instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CollectionModifications DeserializeLengthDelimited(Stream stream)
		{
			CollectionModifications collectionModifications = new CollectionModifications();
			DeserializeLengthDelimited(stream, collectionModifications);
			return collectionModifications;
		}

		public static CollectionModifications DeserializeLengthDelimited(Stream stream, CollectionModifications instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CollectionModifications Deserialize(Stream stream, CollectionModifications instance, long limit)
		{
			if (instance.CardModifications == null)
			{
				instance.CardModifications = new List<CardModification>();
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
					instance.CardModifications.Add(CardModification.DeserializeLengthDelimited(stream));
					continue;
				case 16:
					instance.CollectionVersion = (long)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, CollectionModifications instance)
		{
			if (instance.CardModifications.Count > 0)
			{
				foreach (CardModification cardModification in instance.CardModifications)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, cardModification.GetSerializedSize());
					CardModification.Serialize(stream, cardModification);
				}
			}
			if (instance.HasCollectionVersion)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CollectionVersion);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (CardModifications.Count > 0)
			{
				foreach (CardModification cardModification in CardModifications)
				{
					num++;
					uint serializedSize = cardModification.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasCollectionVersion)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)CollectionVersion);
			}
			return num;
		}
	}
}
