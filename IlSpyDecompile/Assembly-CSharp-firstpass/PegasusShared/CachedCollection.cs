using System.Collections.Generic;
using System.IO;

namespace PegasusShared
{
	public class CachedCollection : IProtoBuf
	{
		private List<CachedCard> _CardCollection = new List<CachedCard>();

		public List<CachedCard> CardCollection
		{
			get
			{
				return _CardCollection;
			}
			set
			{
				_CardCollection = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (CachedCard item in CardCollection)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			CachedCollection cachedCollection = obj as CachedCollection;
			if (cachedCollection == null)
			{
				return false;
			}
			if (CardCollection.Count != cachedCollection.CardCollection.Count)
			{
				return false;
			}
			for (int i = 0; i < CardCollection.Count; i++)
			{
				if (!CardCollection[i].Equals(cachedCollection.CardCollection[i]))
				{
					return false;
				}
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CachedCollection Deserialize(Stream stream, CachedCollection instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CachedCollection DeserializeLengthDelimited(Stream stream)
		{
			CachedCollection cachedCollection = new CachedCollection();
			DeserializeLengthDelimited(stream, cachedCollection);
			return cachedCollection;
		}

		public static CachedCollection DeserializeLengthDelimited(Stream stream, CachedCollection instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CachedCollection Deserialize(Stream stream, CachedCollection instance, long limit)
		{
			if (instance.CardCollection == null)
			{
				instance.CardCollection = new List<CachedCard>();
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
					instance.CardCollection.Add(CachedCard.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, CachedCollection instance)
		{
			if (instance.CardCollection.Count <= 0)
			{
				return;
			}
			foreach (CachedCard item in instance.CardCollection)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				CachedCard.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (CardCollection.Count > 0)
			{
				foreach (CachedCard item in CardCollection)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
				return num;
			}
			return num;
		}
	}
}
