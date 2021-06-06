using System;
using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x02000076 RID: 118
	public class GetAssetRequest : IProtoBuf
	{
		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x0001B551 File Offset: 0x00019751
		// (set) Token: 0x0600076F RID: 1903 RVA: 0x0001B559 File Offset: 0x00019759
		public List<AssetKey> Requests
		{
			get
			{
				return this._Requests;
			}
			set
			{
				this._Requests = value;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x0001B562 File Offset: 0x00019762
		// (set) Token: 0x06000771 RID: 1905 RVA: 0x0001B56A File Offset: 0x0001976A
		public int ClientToken { get; set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000772 RID: 1906 RVA: 0x0001B573 File Offset: 0x00019773
		// (set) Token: 0x06000773 RID: 1907 RVA: 0x0001B57B File Offset: 0x0001977B
		public long FsgId
		{
			get
			{
				return this._FsgId;
			}
			set
			{
				this._FsgId = value;
				this.HasFsgId = true;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x0001B58B File Offset: 0x0001978B
		// (set) Token: 0x06000775 RID: 1909 RVA: 0x0001B593 File Offset: 0x00019793
		public byte[] FsgSharedSecretKey
		{
			get
			{
				return this._FsgSharedSecretKey;
			}
			set
			{
				this._FsgSharedSecretKey = value;
				this.HasFsgSharedSecretKey = (value != null);
			}
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0001B5A8 File Offset: 0x000197A8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (AssetKey assetKey in this.Requests)
			{
				num ^= assetKey.GetHashCode();
			}
			num ^= this.ClientToken.GetHashCode();
			if (this.HasFsgId)
			{
				num ^= this.FsgId.GetHashCode();
			}
			if (this.HasFsgSharedSecretKey)
			{
				num ^= this.FsgSharedSecretKey.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0001B64C File Offset: 0x0001984C
		public override bool Equals(object obj)
		{
			GetAssetRequest getAssetRequest = obj as GetAssetRequest;
			if (getAssetRequest == null)
			{
				return false;
			}
			if (this.Requests.Count != getAssetRequest.Requests.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Requests.Count; i++)
			{
				if (!this.Requests[i].Equals(getAssetRequest.Requests[i]))
				{
					return false;
				}
			}
			return this.ClientToken.Equals(getAssetRequest.ClientToken) && this.HasFsgId == getAssetRequest.HasFsgId && (!this.HasFsgId || this.FsgId.Equals(getAssetRequest.FsgId)) && this.HasFsgSharedSecretKey == getAssetRequest.HasFsgSharedSecretKey && (!this.HasFsgSharedSecretKey || this.FsgSharedSecretKey.Equals(getAssetRequest.FsgSharedSecretKey));
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0001B728 File Offset: 0x00019928
		public void Deserialize(Stream stream)
		{
			GetAssetRequest.Deserialize(stream, this);
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0001B732 File Offset: 0x00019932
		public static GetAssetRequest Deserialize(Stream stream, GetAssetRequest instance)
		{
			return GetAssetRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0001B740 File Offset: 0x00019940
		public static GetAssetRequest DeserializeLengthDelimited(Stream stream)
		{
			GetAssetRequest getAssetRequest = new GetAssetRequest();
			GetAssetRequest.DeserializeLengthDelimited(stream, getAssetRequest);
			return getAssetRequest;
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0001B75C File Offset: 0x0001995C
		public static GetAssetRequest DeserializeLengthDelimited(Stream stream, GetAssetRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetAssetRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0001B784 File Offset: 0x00019984
		public static GetAssetRequest Deserialize(Stream stream, GetAssetRequest instance, long limit)
		{
			if (instance.Requests == null)
			{
				instance.Requests = new List<AssetKey>();
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
					if (num != 16)
					{
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						uint field = key.Field;
						if (field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						if (field != 100U)
						{
							if (field != 101U)
							{
								ProtocolParser.SkipKey(stream, key);
							}
							else if (key.WireType == Wire.LengthDelimited)
							{
								instance.FsgSharedSecretKey = ProtocolParser.ReadBytes(stream);
							}
						}
						else if (key.WireType == Wire.Varint)
						{
							instance.FsgId = (long)ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.ClientToken = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Requests.Add(AssetKey.DeserializeLengthDelimited(stream));
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0001B880 File Offset: 0x00019A80
		public void Serialize(Stream stream)
		{
			GetAssetRequest.Serialize(stream, this);
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0001B88C File Offset: 0x00019A8C
		public static void Serialize(Stream stream, GetAssetRequest instance)
		{
			if (instance.Requests.Count > 0)
			{
				foreach (AssetKey assetKey in instance.Requests)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, assetKey.GetSerializedSize());
					AssetKey.Serialize(stream, assetKey);
				}
			}
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ClientToken));
			if (instance.HasFsgId)
			{
				stream.WriteByte(160);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.FsgId);
			}
			if (instance.HasFsgSharedSecretKey)
			{
				stream.WriteByte(170);
				stream.WriteByte(6);
				ProtocolParser.WriteBytes(stream, instance.FsgSharedSecretKey);
			}
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0001B964 File Offset: 0x00019B64
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Requests.Count > 0)
			{
				foreach (AssetKey assetKey in this.Requests)
				{
					num += 1U;
					uint serializedSize = assetKey.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ClientToken));
			if (this.HasFsgId)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.FsgId);
			}
			if (this.HasFsgSharedSecretKey)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt32(this.FsgSharedSecretKey.Length) + (uint)this.FsgSharedSecretKey.Length;
			}
			num += 1U;
			return num;
		}

		// Token: 0x0400025A RID: 602
		private List<AssetKey> _Requests = new List<AssetKey>();

		// Token: 0x0400025C RID: 604
		public bool HasFsgId;

		// Token: 0x0400025D RID: 605
		private long _FsgId;

		// Token: 0x0400025E RID: 606
		public bool HasFsgSharedSecretKey;

		// Token: 0x0400025F RID: 607
		private byte[] _FsgSharedSecretKey;

		// Token: 0x02000589 RID: 1417
		public enum PacketID
		{
			// Token: 0x04001EF8 RID: 7928
			ID = 321,
			// Token: 0x04001EF9 RID: 7929
			System = 0
		}
	}
}
