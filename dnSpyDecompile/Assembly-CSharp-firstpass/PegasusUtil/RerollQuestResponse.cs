using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000102 RID: 258
	public class RerollQuestResponse : IProtoBuf
	{
		// Token: 0x1700033A RID: 826
		// (get) Token: 0x0600110A RID: 4362 RVA: 0x0003BFCF File Offset: 0x0003A1CF
		// (set) Token: 0x0600110B RID: 4363 RVA: 0x0003BFD7 File Offset: 0x0003A1D7
		public int RerolledQuestId
		{
			get
			{
				return this._RerolledQuestId;
			}
			set
			{
				this._RerolledQuestId = value;
				this.HasRerolledQuestId = true;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x0600110C RID: 4364 RVA: 0x0003BFE7 File Offset: 0x0003A1E7
		// (set) Token: 0x0600110D RID: 4365 RVA: 0x0003BFEF File Offset: 0x0003A1EF
		public int GrantedQuestId
		{
			get
			{
				return this._GrantedQuestId;
			}
			set
			{
				this._GrantedQuestId = value;
				this.HasGrantedQuestId = true;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x0600110E RID: 4366 RVA: 0x0003BFFF File Offset: 0x0003A1FF
		// (set) Token: 0x0600110F RID: 4367 RVA: 0x0003C007 File Offset: 0x0003A207
		public bool Success
		{
			get
			{
				return this._Success;
			}
			set
			{
				this._Success = value;
				this.HasSuccess = true;
			}
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x0003C018 File Offset: 0x0003A218
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasRerolledQuestId)
			{
				num ^= this.RerolledQuestId.GetHashCode();
			}
			if (this.HasGrantedQuestId)
			{
				num ^= this.GrantedQuestId.GetHashCode();
			}
			if (this.HasSuccess)
			{
				num ^= this.Success.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x0003C080 File Offset: 0x0003A280
		public override bool Equals(object obj)
		{
			RerollQuestResponse rerollQuestResponse = obj as RerollQuestResponse;
			return rerollQuestResponse != null && this.HasRerolledQuestId == rerollQuestResponse.HasRerolledQuestId && (!this.HasRerolledQuestId || this.RerolledQuestId.Equals(rerollQuestResponse.RerolledQuestId)) && this.HasGrantedQuestId == rerollQuestResponse.HasGrantedQuestId && (!this.HasGrantedQuestId || this.GrantedQuestId.Equals(rerollQuestResponse.GrantedQuestId)) && this.HasSuccess == rerollQuestResponse.HasSuccess && (!this.HasSuccess || this.Success.Equals(rerollQuestResponse.Success));
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x0003C124 File Offset: 0x0003A324
		public void Deserialize(Stream stream)
		{
			RerollQuestResponse.Deserialize(stream, this);
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x0003C12E File Offset: 0x0003A32E
		public static RerollQuestResponse Deserialize(Stream stream, RerollQuestResponse instance)
		{
			return RerollQuestResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x0003C13C File Offset: 0x0003A33C
		public static RerollQuestResponse DeserializeLengthDelimited(Stream stream)
		{
			RerollQuestResponse rerollQuestResponse = new RerollQuestResponse();
			RerollQuestResponse.DeserializeLengthDelimited(stream, rerollQuestResponse);
			return rerollQuestResponse;
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x0003C158 File Offset: 0x0003A358
		public static RerollQuestResponse DeserializeLengthDelimited(Stream stream, RerollQuestResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RerollQuestResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x0003C180 File Offset: 0x0003A380
		public static RerollQuestResponse Deserialize(Stream stream, RerollQuestResponse instance, long limit)
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
							instance.Success = ProtocolParser.ReadBool(stream);
						}
					}
					else
					{
						instance.GrantedQuestId = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.RerolledQuestId = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x0003C22F File Offset: 0x0003A42F
		public void Serialize(Stream stream)
		{
			RerollQuestResponse.Serialize(stream, this);
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x0003C238 File Offset: 0x0003A438
		public static void Serialize(Stream stream, RerollQuestResponse instance)
		{
			if (instance.HasRerolledQuestId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.RerolledQuestId));
			}
			if (instance.HasGrantedQuestId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.GrantedQuestId));
			}
			if (instance.HasSuccess)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.Success);
			}
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x0003C29C File Offset: 0x0003A49C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasRerolledQuestId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.RerolledQuestId));
			}
			if (this.HasGrantedQuestId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.GrantedQuestId));
			}
			if (this.HasSuccess)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x0400053B RID: 1339
		public bool HasRerolledQuestId;

		// Token: 0x0400053C RID: 1340
		private int _RerolledQuestId;

		// Token: 0x0400053D RID: 1341
		public bool HasGrantedQuestId;

		// Token: 0x0400053E RID: 1342
		private int _GrantedQuestId;

		// Token: 0x0400053F RID: 1343
		public bool HasSuccess;

		// Token: 0x04000540 RID: 1344
		private bool _Success;

		// Token: 0x02000604 RID: 1540
		public enum PacketID
		{
			// Token: 0x04002049 RID: 8265
			ID = 607,
			// Token: 0x0400204A RID: 8266
			System = 0
		}
	}
}
