using System;
using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x020000E0 RID: 224
	public class ClientStaticAssetsResponse : IProtoBuf
	{
		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000F3A RID: 3898 RVA: 0x00037467 File Offset: 0x00035667
		// (set) Token: 0x06000F3B RID: 3899 RVA: 0x0003746F File Offset: 0x0003566F
		public List<AssetRecordInfo> AssetsToGet
		{
			get
			{
				return this._AssetsToGet;
			}
			set
			{
				this._AssetsToGet = value;
			}
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x00037478 File Offset: 0x00035678
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (AssetRecordInfo assetRecordInfo in this.AssetsToGet)
			{
				num ^= assetRecordInfo.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x000374DC File Offset: 0x000356DC
		public override bool Equals(object obj)
		{
			ClientStaticAssetsResponse clientStaticAssetsResponse = obj as ClientStaticAssetsResponse;
			if (clientStaticAssetsResponse == null)
			{
				return false;
			}
			if (this.AssetsToGet.Count != clientStaticAssetsResponse.AssetsToGet.Count)
			{
				return false;
			}
			for (int i = 0; i < this.AssetsToGet.Count; i++)
			{
				if (!this.AssetsToGet[i].Equals(clientStaticAssetsResponse.AssetsToGet[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x00037547 File Offset: 0x00035747
		public void Deserialize(Stream stream)
		{
			ClientStaticAssetsResponse.Deserialize(stream, this);
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x00037551 File Offset: 0x00035751
		public static ClientStaticAssetsResponse Deserialize(Stream stream, ClientStaticAssetsResponse instance)
		{
			return ClientStaticAssetsResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x0003755C File Offset: 0x0003575C
		public static ClientStaticAssetsResponse DeserializeLengthDelimited(Stream stream)
		{
			ClientStaticAssetsResponse clientStaticAssetsResponse = new ClientStaticAssetsResponse();
			ClientStaticAssetsResponse.DeserializeLengthDelimited(stream, clientStaticAssetsResponse);
			return clientStaticAssetsResponse;
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x00037578 File Offset: 0x00035778
		public static ClientStaticAssetsResponse DeserializeLengthDelimited(Stream stream, ClientStaticAssetsResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ClientStaticAssetsResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x000375A0 File Offset: 0x000357A0
		public static ClientStaticAssetsResponse Deserialize(Stream stream, ClientStaticAssetsResponse instance, long limit)
		{
			if (instance.AssetsToGet == null)
			{
				instance.AssetsToGet = new List<AssetRecordInfo>();
			}
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
				else if (num == 10)
				{
					instance.AssetsToGet.Add(AssetRecordInfo.DeserializeLengthDelimited(stream));
				}
				else
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x00037638 File Offset: 0x00035838
		public void Serialize(Stream stream)
		{
			ClientStaticAssetsResponse.Serialize(stream, this);
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x00037644 File Offset: 0x00035844
		public static void Serialize(Stream stream, ClientStaticAssetsResponse instance)
		{
			if (instance.AssetsToGet.Count > 0)
			{
				foreach (AssetRecordInfo assetRecordInfo in instance.AssetsToGet)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, assetRecordInfo.GetSerializedSize());
					AssetRecordInfo.Serialize(stream, assetRecordInfo);
				}
			}
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x000376BC File Offset: 0x000358BC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.AssetsToGet.Count > 0)
			{
				foreach (AssetRecordInfo assetRecordInfo in this.AssetsToGet)
				{
					num += 1U;
					uint serializedSize = assetRecordInfo.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x040004FB RID: 1275
		private List<AssetRecordInfo> _AssetsToGet = new List<AssetRecordInfo>();

		// Token: 0x020005E4 RID: 1508
		public enum PacketID
		{
			// Token: 0x04001FF4 RID: 8180
			ID = 341
		}
	}
}
