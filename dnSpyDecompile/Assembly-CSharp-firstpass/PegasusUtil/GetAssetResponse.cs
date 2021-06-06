using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000C2 RID: 194
	public class GetAssetResponse : IProtoBuf
	{
		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000D58 RID: 3416 RVA: 0x00031833 File Offset: 0x0002FA33
		// (set) Token: 0x06000D59 RID: 3417 RVA: 0x0003183B File Offset: 0x0002FA3B
		public List<AssetResponse> Responses
		{
			get
			{
				return this._Responses;
			}
			set
			{
				this._Responses = value;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000D5A RID: 3418 RVA: 0x00031844 File Offset: 0x0002FA44
		// (set) Token: 0x06000D5B RID: 3419 RVA: 0x0003184C File Offset: 0x0002FA4C
		public int ClientToken { get; set; }

		// Token: 0x06000D5C RID: 3420 RVA: 0x00031858 File Offset: 0x0002FA58
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (AssetResponse assetResponse in this.Responses)
			{
				num ^= assetResponse.GetHashCode();
			}
			num ^= this.ClientToken.GetHashCode();
			return num;
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x000318CC File Offset: 0x0002FACC
		public override bool Equals(object obj)
		{
			GetAssetResponse getAssetResponse = obj as GetAssetResponse;
			if (getAssetResponse == null)
			{
				return false;
			}
			if (this.Responses.Count != getAssetResponse.Responses.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Responses.Count; i++)
			{
				if (!this.Responses[i].Equals(getAssetResponse.Responses[i]))
				{
					return false;
				}
			}
			return this.ClientToken.Equals(getAssetResponse.ClientToken);
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x0003194F File Offset: 0x0002FB4F
		public void Deserialize(Stream stream)
		{
			GetAssetResponse.Deserialize(stream, this);
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x00031959 File Offset: 0x0002FB59
		public static GetAssetResponse Deserialize(Stream stream, GetAssetResponse instance)
		{
			return GetAssetResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x00031964 File Offset: 0x0002FB64
		public static GetAssetResponse DeserializeLengthDelimited(Stream stream)
		{
			GetAssetResponse getAssetResponse = new GetAssetResponse();
			GetAssetResponse.DeserializeLengthDelimited(stream, getAssetResponse);
			return getAssetResponse;
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x00031980 File Offset: 0x0002FB80
		public static GetAssetResponse DeserializeLengthDelimited(Stream stream, GetAssetResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetAssetResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x000319A8 File Offset: 0x0002FBA8
		public static GetAssetResponse Deserialize(Stream stream, GetAssetResponse instance, long limit)
		{
			if (instance.Responses == null)
			{
				instance.Responses = new List<AssetResponse>();
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
				else if (num != 10)
				{
					if (num != 24)
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
						instance.ClientToken = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Responses.Add(AssetResponse.DeserializeLengthDelimited(stream));
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x00031A59 File Offset: 0x0002FC59
		public void Serialize(Stream stream)
		{
			GetAssetResponse.Serialize(stream, this);
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x00031A64 File Offset: 0x0002FC64
		public static void Serialize(Stream stream, GetAssetResponse instance)
		{
			if (instance.Responses.Count > 0)
			{
				foreach (AssetResponse assetResponse in instance.Responses)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, assetResponse.GetSerializedSize());
					AssetResponse.Serialize(stream, assetResponse);
				}
			}
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ClientToken));
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x00031AF0 File Offset: 0x0002FCF0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Responses.Count > 0)
			{
				foreach (AssetResponse assetResponse in this.Responses)
				{
					num += 1U;
					uint serializedSize = assetResponse.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ClientToken));
			num += 1U;
			return num;
		}

		// Token: 0x04000495 RID: 1173
		private List<AssetResponse> _Responses = new List<AssetResponse>();

		// Token: 0x020005D1 RID: 1489
		public enum PacketID
		{
			// Token: 0x04001FBB RID: 8123
			ID = 322
		}
	}
}
