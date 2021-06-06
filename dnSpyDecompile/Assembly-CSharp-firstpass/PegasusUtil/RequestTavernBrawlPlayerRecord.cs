using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x020000EA RID: 234
	public class RequestTavernBrawlPlayerRecord : IProtoBuf
	{
		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000FBE RID: 4030 RVA: 0x00038746 File Offset: 0x00036946
		// (set) Token: 0x06000FBF RID: 4031 RVA: 0x0003874E File Offset: 0x0003694E
		public BrawlType BrawlType { get; set; }

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x00038757 File Offset: 0x00036957
		// (set) Token: 0x06000FC1 RID: 4033 RVA: 0x0003875F File Offset: 0x0003695F
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

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000FC2 RID: 4034 RVA: 0x0003876F File Offset: 0x0003696F
		// (set) Token: 0x06000FC3 RID: 4035 RVA: 0x00038777 File Offset: 0x00036977
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

		// Token: 0x06000FC4 RID: 4036 RVA: 0x0003878C File Offset: 0x0003698C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.BrawlType.GetHashCode();
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

		// Token: 0x06000FC5 RID: 4037 RVA: 0x000387EC File Offset: 0x000369EC
		public override bool Equals(object obj)
		{
			RequestTavernBrawlPlayerRecord requestTavernBrawlPlayerRecord = obj as RequestTavernBrawlPlayerRecord;
			return requestTavernBrawlPlayerRecord != null && this.BrawlType.Equals(requestTavernBrawlPlayerRecord.BrawlType) && this.HasFsgId == requestTavernBrawlPlayerRecord.HasFsgId && (!this.HasFsgId || this.FsgId.Equals(requestTavernBrawlPlayerRecord.FsgId)) && this.HasFsgSharedSecretKey == requestTavernBrawlPlayerRecord.HasFsgSharedSecretKey && (!this.HasFsgSharedSecretKey || this.FsgSharedSecretKey.Equals(requestTavernBrawlPlayerRecord.FsgSharedSecretKey));
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x00038882 File Offset: 0x00036A82
		public void Deserialize(Stream stream)
		{
			RequestTavernBrawlPlayerRecord.Deserialize(stream, this);
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x0003888C File Offset: 0x00036A8C
		public static RequestTavernBrawlPlayerRecord Deserialize(Stream stream, RequestTavernBrawlPlayerRecord instance)
		{
			return RequestTavernBrawlPlayerRecord.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x00038898 File Offset: 0x00036A98
		public static RequestTavernBrawlPlayerRecord DeserializeLengthDelimited(Stream stream)
		{
			RequestTavernBrawlPlayerRecord requestTavernBrawlPlayerRecord = new RequestTavernBrawlPlayerRecord();
			RequestTavernBrawlPlayerRecord.DeserializeLengthDelimited(stream, requestTavernBrawlPlayerRecord);
			return requestTavernBrawlPlayerRecord;
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x000388B4 File Offset: 0x00036AB4
		public static RequestTavernBrawlPlayerRecord DeserializeLengthDelimited(Stream stream, RequestTavernBrawlPlayerRecord instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RequestTavernBrawlPlayerRecord.Deserialize(stream, instance, num);
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x000388DC File Offset: 0x00036ADC
		public static RequestTavernBrawlPlayerRecord Deserialize(Stream stream, RequestTavernBrawlPlayerRecord instance, long limit)
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
				else if (num == 16)
				{
					instance.BrawlType = (BrawlType)ProtocolParser.ReadUInt64(stream);
				}
				else
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
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x000389AB File Offset: 0x00036BAB
		public void Serialize(Stream stream)
		{
			RequestTavernBrawlPlayerRecord.Serialize(stream, this);
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x000389B4 File Offset: 0x00036BB4
		public static void Serialize(Stream stream, RequestTavernBrawlPlayerRecord instance)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.BrawlType));
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

		// Token: 0x06000FCD RID: 4045 RVA: 0x00038A24 File Offset: 0x00036C24
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.BrawlType));
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
			return num + 1U;
		}

		// Token: 0x0400050D RID: 1293
		public bool HasFsgId;

		// Token: 0x0400050E RID: 1294
		private long _FsgId;

		// Token: 0x0400050F RID: 1295
		public bool HasFsgSharedSecretKey;

		// Token: 0x04000510 RID: 1296
		private byte[] _FsgSharedSecretKey;

		// Token: 0x020005EE RID: 1518
		public enum PacketID
		{
			// Token: 0x0400200D RID: 8205
			ID = 353,
			// Token: 0x0400200E RID: 8206
			System = 0
		}
	}
}
