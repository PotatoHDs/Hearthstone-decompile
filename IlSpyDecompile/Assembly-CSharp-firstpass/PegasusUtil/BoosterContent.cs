using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	public class BoosterContent : IProtoBuf
	{
		public enum PacketID
		{
			ID = 226
		}

		private List<BoosterCard> _List = new List<BoosterCard>();

		public bool HasCollectionVersion;

		private long _CollectionVersion;

		public List<BoosterCard> List
		{
			get
			{
				return _List;
			}
			set
			{
				_List = value;
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
			foreach (BoosterCard item in List)
			{
				num ^= item.GetHashCode();
			}
			if (HasCollectionVersion)
			{
				num ^= CollectionVersion.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			BoosterContent boosterContent = obj as BoosterContent;
			if (boosterContent == null)
			{
				return false;
			}
			if (List.Count != boosterContent.List.Count)
			{
				return false;
			}
			for (int i = 0; i < List.Count; i++)
			{
				if (!List[i].Equals(boosterContent.List[i]))
				{
					return false;
				}
			}
			if (HasCollectionVersion != boosterContent.HasCollectionVersion || (HasCollectionVersion && !CollectionVersion.Equals(boosterContent.CollectionVersion)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static BoosterContent Deserialize(Stream stream, BoosterContent instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static BoosterContent DeserializeLengthDelimited(Stream stream)
		{
			BoosterContent boosterContent = new BoosterContent();
			DeserializeLengthDelimited(stream, boosterContent);
			return boosterContent;
		}

		public static BoosterContent DeserializeLengthDelimited(Stream stream, BoosterContent instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static BoosterContent Deserialize(Stream stream, BoosterContent instance, long limit)
		{
			if (instance.List == null)
			{
				instance.List = new List<BoosterCard>();
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
					instance.List.Add(BoosterCard.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, BoosterContent instance)
		{
			if (instance.List.Count > 0)
			{
				foreach (BoosterCard item in instance.List)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					BoosterCard.Serialize(stream, item);
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
			if (List.Count > 0)
			{
				foreach (BoosterCard item in List)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
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
