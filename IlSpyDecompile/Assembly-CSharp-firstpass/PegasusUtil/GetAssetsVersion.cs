using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class GetAssetsVersion : IProtoBuf
	{
		public enum PacketID
		{
			ID = 303,
			System = 0
		}

		public class DeckModificationTimes : IProtoBuf
		{
			public bool HasDeckId;

			private long _DeckId;

			public bool HasLastModified;

			private long _LastModified;

			public long DeckId
			{
				get
				{
					return _DeckId;
				}
				set
				{
					_DeckId = value;
					HasDeckId = true;
				}
			}

			public long LastModified
			{
				get
				{
					return _LastModified;
				}
				set
				{
					_LastModified = value;
					HasLastModified = true;
				}
			}

			public override int GetHashCode()
			{
				int num = GetType().GetHashCode();
				if (HasDeckId)
				{
					num ^= DeckId.GetHashCode();
				}
				if (HasLastModified)
				{
					num ^= LastModified.GetHashCode();
				}
				return num;
			}

			public override bool Equals(object obj)
			{
				DeckModificationTimes deckModificationTimes = obj as DeckModificationTimes;
				if (deckModificationTimes == null)
				{
					return false;
				}
				if (HasDeckId != deckModificationTimes.HasDeckId || (HasDeckId && !DeckId.Equals(deckModificationTimes.DeckId)))
				{
					return false;
				}
				if (HasLastModified != deckModificationTimes.HasLastModified || (HasLastModified && !LastModified.Equals(deckModificationTimes.LastModified)))
				{
					return false;
				}
				return true;
			}

			public void Deserialize(Stream stream)
			{
				Deserialize(stream, this);
			}

			public static DeckModificationTimes Deserialize(Stream stream, DeckModificationTimes instance)
			{
				return Deserialize(stream, instance, -1L);
			}

			public static DeckModificationTimes DeserializeLengthDelimited(Stream stream)
			{
				DeckModificationTimes deckModificationTimes = new DeckModificationTimes();
				DeserializeLengthDelimited(stream, deckModificationTimes);
				return deckModificationTimes;
			}

			public static DeckModificationTimes DeserializeLengthDelimited(Stream stream, DeckModificationTimes instance)
			{
				long num = ProtocolParser.ReadUInt32(stream);
				num += stream.Position;
				return Deserialize(stream, instance, num);
			}

			public static DeckModificationTimes Deserialize(Stream stream, DeckModificationTimes instance, long limit)
			{
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
					case 8:
						instance.DeckId = (long)ProtocolParser.ReadUInt64(stream);
						continue;
					case 16:
						instance.LastModified = (long)ProtocolParser.ReadUInt64(stream);
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

			public static void Serialize(Stream stream, DeckModificationTimes instance)
			{
				if (instance.HasDeckId)
				{
					stream.WriteByte(8);
					ProtocolParser.WriteUInt64(stream, (ulong)instance.DeckId);
				}
				if (instance.HasLastModified)
				{
					stream.WriteByte(16);
					ProtocolParser.WriteUInt64(stream, (ulong)instance.LastModified);
				}
			}

			public uint GetSerializedSize()
			{
				uint num = 0u;
				if (HasDeckId)
				{
					num++;
					num += ProtocolParser.SizeOfUInt64((ulong)DeckId);
				}
				if (HasLastModified)
				{
					num++;
					num += ProtocolParser.SizeOfUInt64((ulong)LastModified);
				}
				return num;
			}
		}

		public bool HasPlatform;

		private Platform _Platform;

		public bool HasClientCollectionVersion;

		private long _ClientCollectionVersion;

		private List<DeckModificationTimes> _CachedDeckModificationTimes = new List<DeckModificationTimes>();

		public bool HasCollectionVersionLastModified;

		private long _CollectionVersionLastModified;

		public Platform Platform
		{
			get
			{
				return _Platform;
			}
			set
			{
				_Platform = value;
				HasPlatform = value != null;
			}
		}

		public long ClientCollectionVersion
		{
			get
			{
				return _ClientCollectionVersion;
			}
			set
			{
				_ClientCollectionVersion = value;
				HasClientCollectionVersion = true;
			}
		}

		public List<DeckModificationTimes> CachedDeckModificationTimes
		{
			get
			{
				return _CachedDeckModificationTimes;
			}
			set
			{
				_CachedDeckModificationTimes = value;
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
			if (HasPlatform)
			{
				num ^= Platform.GetHashCode();
			}
			if (HasClientCollectionVersion)
			{
				num ^= ClientCollectionVersion.GetHashCode();
			}
			foreach (DeckModificationTimes cachedDeckModificationTime in CachedDeckModificationTimes)
			{
				num ^= cachedDeckModificationTime.GetHashCode();
			}
			if (HasCollectionVersionLastModified)
			{
				num ^= CollectionVersionLastModified.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetAssetsVersion getAssetsVersion = obj as GetAssetsVersion;
			if (getAssetsVersion == null)
			{
				return false;
			}
			if (HasPlatform != getAssetsVersion.HasPlatform || (HasPlatform && !Platform.Equals(getAssetsVersion.Platform)))
			{
				return false;
			}
			if (HasClientCollectionVersion != getAssetsVersion.HasClientCollectionVersion || (HasClientCollectionVersion && !ClientCollectionVersion.Equals(getAssetsVersion.ClientCollectionVersion)))
			{
				return false;
			}
			if (CachedDeckModificationTimes.Count != getAssetsVersion.CachedDeckModificationTimes.Count)
			{
				return false;
			}
			for (int i = 0; i < CachedDeckModificationTimes.Count; i++)
			{
				if (!CachedDeckModificationTimes[i].Equals(getAssetsVersion.CachedDeckModificationTimes[i]))
				{
					return false;
				}
			}
			if (HasCollectionVersionLastModified != getAssetsVersion.HasCollectionVersionLastModified || (HasCollectionVersionLastModified && !CollectionVersionLastModified.Equals(getAssetsVersion.CollectionVersionLastModified)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetAssetsVersion Deserialize(Stream stream, GetAssetsVersion instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetAssetsVersion DeserializeLengthDelimited(Stream stream)
		{
			GetAssetsVersion getAssetsVersion = new GetAssetsVersion();
			DeserializeLengthDelimited(stream, getAssetsVersion);
			return getAssetsVersion;
		}

		public static GetAssetsVersion DeserializeLengthDelimited(Stream stream, GetAssetsVersion instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetAssetsVersion Deserialize(Stream stream, GetAssetsVersion instance, long limit)
		{
			if (instance.CachedDeckModificationTimes == null)
			{
				instance.CachedDeckModificationTimes = new List<DeckModificationTimes>();
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
					if (instance.Platform == null)
					{
						instance.Platform = Platform.DeserializeLengthDelimited(stream);
					}
					else
					{
						Platform.DeserializeLengthDelimited(stream, instance.Platform);
					}
					continue;
				case 16:
					instance.ClientCollectionVersion = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 26:
					instance.CachedDeckModificationTimes.Add(DeckModificationTimes.DeserializeLengthDelimited(stream));
					continue;
				case 32:
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

		public static void Serialize(Stream stream, GetAssetsVersion instance)
		{
			if (instance.HasPlatform)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Platform.GetSerializedSize());
				Platform.Serialize(stream, instance.Platform);
			}
			if (instance.HasClientCollectionVersion)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ClientCollectionVersion);
			}
			if (instance.CachedDeckModificationTimes.Count > 0)
			{
				foreach (DeckModificationTimes cachedDeckModificationTime in instance.CachedDeckModificationTimes)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, cachedDeckModificationTime.GetSerializedSize());
					DeckModificationTimes.Serialize(stream, cachedDeckModificationTime);
				}
			}
			if (instance.HasCollectionVersionLastModified)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CollectionVersionLastModified);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasPlatform)
			{
				num++;
				uint serializedSize = Platform.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasClientCollectionVersion)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ClientCollectionVersion);
			}
			if (CachedDeckModificationTimes.Count > 0)
			{
				foreach (DeckModificationTimes cachedDeckModificationTime in CachedDeckModificationTimes)
				{
					num++;
					uint serializedSize2 = cachedDeckModificationTime.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
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
