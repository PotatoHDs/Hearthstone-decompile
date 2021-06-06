using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x02000159 RID: 345
	public class AssetRecordInfo : IProtoBuf
	{
		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x0600171D RID: 5917 RVA: 0x0004FD26 File Offset: 0x0004DF26
		// (set) Token: 0x0600171E RID: 5918 RVA: 0x0004FD2E File Offset: 0x0004DF2E
		public AssetKey Asset { get; set; }

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x0600171F RID: 5919 RVA: 0x0004FD37 File Offset: 0x0004DF37
		// (set) Token: 0x06001720 RID: 5920 RVA: 0x0004FD3F File Offset: 0x0004DF3F
		public uint RecordByteSize { get; set; }

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06001721 RID: 5921 RVA: 0x0004FD48 File Offset: 0x0004DF48
		// (set) Token: 0x06001722 RID: 5922 RVA: 0x0004FD50 File Offset: 0x0004DF50
		public byte[] RecordHash { get; set; }

		// Token: 0x06001723 RID: 5923 RVA: 0x0004FD5C File Offset: 0x0004DF5C
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Asset.GetHashCode() ^ this.RecordByteSize.GetHashCode() ^ this.RecordHash.GetHashCode();
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x0004FD9C File Offset: 0x0004DF9C
		public override bool Equals(object obj)
		{
			AssetRecordInfo assetRecordInfo = obj as AssetRecordInfo;
			return assetRecordInfo != null && this.Asset.Equals(assetRecordInfo.Asset) && this.RecordByteSize.Equals(assetRecordInfo.RecordByteSize) && this.RecordHash.Equals(assetRecordInfo.RecordHash);
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x0004FDF8 File Offset: 0x0004DFF8
		public void Deserialize(Stream stream)
		{
			AssetRecordInfo.Deserialize(stream, this);
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x0004FE02 File Offset: 0x0004E002
		public static AssetRecordInfo Deserialize(Stream stream, AssetRecordInfo instance)
		{
			return AssetRecordInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x0004FE10 File Offset: 0x0004E010
		public static AssetRecordInfo DeserializeLengthDelimited(Stream stream)
		{
			AssetRecordInfo assetRecordInfo = new AssetRecordInfo();
			AssetRecordInfo.DeserializeLengthDelimited(stream, assetRecordInfo);
			return assetRecordInfo;
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x0004FE2C File Offset: 0x0004E02C
		public static AssetRecordInfo DeserializeLengthDelimited(Stream stream, AssetRecordInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AssetRecordInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x0004FE54 File Offset: 0x0004E054
		public static AssetRecordInfo Deserialize(Stream stream, AssetRecordInfo instance, long limit)
		{
			while (limit < 0L || stream.Position < limit)
			{
				int num = stream.ReadByte();
				if (num == -1)
				{
					if (limit >= 0L)
					{
						throw new EndOfStreamException();
					}
					return instance;
				}
				else if (num != 10)
				{
					if (num != 16)
					{
						if (num != 26)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else
						{
							instance.RecordHash = ProtocolParser.ReadBytes(stream);
						}
					}
					else
					{
						instance.RecordByteSize = ProtocolParser.ReadUInt32(stream);
					}
				}
				else if (instance.Asset == null)
				{
					instance.Asset = AssetKey.DeserializeLengthDelimited(stream);
				}
				else
				{
					AssetKey.DeserializeLengthDelimited(stream, instance.Asset);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x0004FF22 File Offset: 0x0004E122
		public void Serialize(Stream stream)
		{
			AssetRecordInfo.Serialize(stream, this);
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x0004FF2C File Offset: 0x0004E12C
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

		// Token: 0x0600172C RID: 5932 RVA: 0x0004FFB8 File Offset: 0x0004E1B8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.Asset.GetSerializedSize();
			return num + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + ProtocolParser.SizeOfUInt32(this.RecordByteSize) + (ProtocolParser.SizeOfUInt32(this.RecordHash.Length) + (uint)this.RecordHash.Length) + 3U;
		}
	}
}
