using System.IO;

namespace PegasusShared
{
	public class AssetKey : IProtoBuf
	{
		public bool HasAssetId;

		private int _AssetId;

		public AssetType Type { get; set; }

		public int AssetId
		{
			get
			{
				return _AssetId;
			}
			set
			{
				_AssetId = value;
				HasAssetId = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Type.GetHashCode();
			if (HasAssetId)
			{
				hashCode ^= AssetId.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			AssetKey assetKey = obj as AssetKey;
			if (assetKey == null)
			{
				return false;
			}
			if (!Type.Equals(assetKey.Type))
			{
				return false;
			}
			if (HasAssetId != assetKey.HasAssetId || (HasAssetId && !AssetId.Equals(assetKey.AssetId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AssetKey Deserialize(Stream stream, AssetKey instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AssetKey DeserializeLengthDelimited(Stream stream)
		{
			AssetKey assetKey = new AssetKey();
			DeserializeLengthDelimited(stream, assetKey);
			return assetKey;
		}

		public static AssetKey DeserializeLengthDelimited(Stream stream, AssetKey instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AssetKey Deserialize(Stream stream, AssetKey instance, long limit)
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
					instance.Type = (AssetType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.AssetId = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, AssetKey instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Type);
			if (instance.HasAssetId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.AssetId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Type);
			if (HasAssetId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)AssetId);
			}
			return num + 1;
		}
	}
}
