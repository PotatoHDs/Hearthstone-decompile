using System;
using System.IO;

namespace PegasusShared
{
	public class AssetRecordInfo : IProtoBuf
	{
		public AssetKey Asset { get; set; }

		public uint RecordByteSize { get; set; }

		public byte[] RecordHash { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Asset.GetHashCode() ^ RecordByteSize.GetHashCode() ^ RecordHash.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			AssetRecordInfo assetRecordInfo = obj as AssetRecordInfo;
			if (assetRecordInfo == null)
			{
				return false;
			}
			if (!Asset.Equals(assetRecordInfo.Asset))
			{
				return false;
			}
			if (!RecordByteSize.Equals(assetRecordInfo.RecordByteSize))
			{
				return false;
			}
			if (!RecordHash.Equals(assetRecordInfo.RecordHash))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AssetRecordInfo Deserialize(Stream stream, AssetRecordInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AssetRecordInfo DeserializeLengthDelimited(Stream stream)
		{
			AssetRecordInfo assetRecordInfo = new AssetRecordInfo();
			DeserializeLengthDelimited(stream, assetRecordInfo);
			return assetRecordInfo;
		}

		public static AssetRecordInfo DeserializeLengthDelimited(Stream stream, AssetRecordInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AssetRecordInfo Deserialize(Stream stream, AssetRecordInfo instance, long limit)
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
				case 10:
					if (instance.Asset == null)
					{
						instance.Asset = AssetKey.DeserializeLengthDelimited(stream);
					}
					else
					{
						AssetKey.DeserializeLengthDelimited(stream, instance.Asset);
					}
					continue;
				case 16:
					instance.RecordByteSize = ProtocolParser.ReadUInt32(stream);
					continue;
				case 26:
					instance.RecordHash = ProtocolParser.ReadBytes(stream);
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

		public static void Serialize(Stream stream, AssetRecordInfo instance)
		{
			if (instance.Asset == null)
			{
				throw new ArgumentNullException("Asset", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Asset.GetSerializedSize());
			AssetKey.Serialize(stream, instance.Asset);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt32(stream, instance.RecordByteSize);
			if (instance.RecordHash == null)
			{
				throw new ArgumentNullException("RecordHash", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteBytes(stream, instance.RecordHash);
		}

		public uint GetSerializedSize()
		{
			uint serializedSize = Asset.GetSerializedSize();
			return (uint)((int)(0 + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + ProtocolParser.SizeOfUInt32(RecordByteSize)) + ((int)ProtocolParser.SizeOfUInt32(RecordHash.Length) + RecordHash.Length) + 3);
		}
	}
}
