using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x02000158 RID: 344
	public class AssetKey : IProtoBuf
	{
		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x0600170E RID: 5902 RVA: 0x0004FAD5 File Offset: 0x0004DCD5
		// (set) Token: 0x0600170F RID: 5903 RVA: 0x0004FADD File Offset: 0x0004DCDD
		public AssetType Type { get; set; }

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001710 RID: 5904 RVA: 0x0004FAE6 File Offset: 0x0004DCE6
		// (set) Token: 0x06001711 RID: 5905 RVA: 0x0004FAEE File Offset: 0x0004DCEE
		public int AssetId
		{
			get
			{
				return this._AssetId;
			}
			set
			{
				this._AssetId = value;
				this.HasAssetId = true;
			}
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x0004FB00 File Offset: 0x0004DD00
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Type.GetHashCode();
			if (this.HasAssetId)
			{
				num ^= this.AssetId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x0004FB4C File Offset: 0x0004DD4C
		public override bool Equals(object obj)
		{
			AssetKey assetKey = obj as AssetKey;
			return assetKey != null && this.Type.Equals(assetKey.Type) && this.HasAssetId == assetKey.HasAssetId && (!this.HasAssetId || this.AssetId.Equals(assetKey.AssetId));
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x0004FBB7 File Offset: 0x0004DDB7
		public void Deserialize(Stream stream)
		{
			AssetKey.Deserialize(stream, this);
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x0004FBC1 File Offset: 0x0004DDC1
		public static AssetKey Deserialize(Stream stream, AssetKey instance)
		{
			return AssetKey.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x0004FBCC File Offset: 0x0004DDCC
		public static AssetKey DeserializeLengthDelimited(Stream stream)
		{
			AssetKey assetKey = new AssetKey();
			AssetKey.DeserializeLengthDelimited(stream, assetKey);
			return assetKey;
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x0004FBE8 File Offset: 0x0004DDE8
		public static AssetKey DeserializeLengthDelimited(Stream stream, AssetKey instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AssetKey.Deserialize(stream, instance, num);
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x0004FC10 File Offset: 0x0004DE10
		public static AssetKey Deserialize(Stream stream, AssetKey instance, long limit)
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
				else if (num != 8)
				{
					if (num != 16)
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
						instance.AssetId = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Type = (AssetType)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x0004FCA9 File Offset: 0x0004DEA9
		public void Serialize(Stream stream)
		{
			AssetKey.Serialize(stream, this);
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x0004FCB2 File Offset: 0x0004DEB2
		public static void Serialize(Stream stream, AssetKey instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Type));
			if (instance.HasAssetId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.AssetId));
			}
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x0004FCE8 File Offset: 0x0004DEE8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Type));
			if (this.HasAssetId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.AssetId));
			}
			return num + 1U;
		}

		// Token: 0x04000725 RID: 1829
		public bool HasAssetId;

		// Token: 0x04000726 RID: 1830
		private int _AssetId;
	}
}
