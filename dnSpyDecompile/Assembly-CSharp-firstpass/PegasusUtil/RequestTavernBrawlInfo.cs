using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x020000E9 RID: 233
	public class RequestTavernBrawlInfo : IProtoBuf
	{
		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000FAD RID: 4013 RVA: 0x00038405 File Offset: 0x00036605
		// (set) Token: 0x06000FAE RID: 4014 RVA: 0x0003840D File Offset: 0x0003660D
		public BrawlType BrawlType { get; set; }

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x00038416 File Offset: 0x00036616
		// (set) Token: 0x06000FB0 RID: 4016 RVA: 0x0003841E File Offset: 0x0003661E
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

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x0003842E File Offset: 0x0003662E
		// (set) Token: 0x06000FB2 RID: 4018 RVA: 0x00038436 File Offset: 0x00036636
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

		// Token: 0x06000FB3 RID: 4019 RVA: 0x0003844C File Offset: 0x0003664C
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

		// Token: 0x06000FB4 RID: 4020 RVA: 0x000384AC File Offset: 0x000366AC
		public override bool Equals(object obj)
		{
			RequestTavernBrawlInfo requestTavernBrawlInfo = obj as RequestTavernBrawlInfo;
			return requestTavernBrawlInfo != null && this.BrawlType.Equals(requestTavernBrawlInfo.BrawlType) && this.HasFsgId == requestTavernBrawlInfo.HasFsgId && (!this.HasFsgId || this.FsgId.Equals(requestTavernBrawlInfo.FsgId)) && this.HasFsgSharedSecretKey == requestTavernBrawlInfo.HasFsgSharedSecretKey && (!this.HasFsgSharedSecretKey || this.FsgSharedSecretKey.Equals(requestTavernBrawlInfo.FsgSharedSecretKey));
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x00038542 File Offset: 0x00036742
		public void Deserialize(Stream stream)
		{
			RequestTavernBrawlInfo.Deserialize(stream, this);
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x0003854C File Offset: 0x0003674C
		public static RequestTavernBrawlInfo Deserialize(Stream stream, RequestTavernBrawlInfo instance)
		{
			return RequestTavernBrawlInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x00038558 File Offset: 0x00036758
		public static RequestTavernBrawlInfo DeserializeLengthDelimited(Stream stream)
		{
			RequestTavernBrawlInfo requestTavernBrawlInfo = new RequestTavernBrawlInfo();
			RequestTavernBrawlInfo.DeserializeLengthDelimited(stream, requestTavernBrawlInfo);
			return requestTavernBrawlInfo;
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x00038574 File Offset: 0x00036774
		public static RequestTavernBrawlInfo DeserializeLengthDelimited(Stream stream, RequestTavernBrawlInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RequestTavernBrawlInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x0003859C File Offset: 0x0003679C
		public static RequestTavernBrawlInfo Deserialize(Stream stream, RequestTavernBrawlInfo instance, long limit)
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

		// Token: 0x06000FBA RID: 4026 RVA: 0x0003866B File Offset: 0x0003686B
		public void Serialize(Stream stream)
		{
			RequestTavernBrawlInfo.Serialize(stream, this);
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x00038674 File Offset: 0x00036874
		public static void Serialize(Stream stream, RequestTavernBrawlInfo instance)
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

		// Token: 0x06000FBC RID: 4028 RVA: 0x000386E4 File Offset: 0x000368E4
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

		// Token: 0x04000508 RID: 1288
		public bool HasFsgId;

		// Token: 0x04000509 RID: 1289
		private long _FsgId;

		// Token: 0x0400050A RID: 1290
		public bool HasFsgSharedSecretKey;

		// Token: 0x0400050B RID: 1291
		private byte[] _FsgSharedSecretKey;

		// Token: 0x020005ED RID: 1517
		public enum PacketID
		{
			// Token: 0x0400200A RID: 8202
			ID = 352,
			// Token: 0x0400200B RID: 8203
			System = 0
		}
	}
}
