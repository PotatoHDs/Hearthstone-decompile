using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PegasusShared;

namespace HSCachedDeckCompletion
{
	// Token: 0x02000016 RID: 22
	public class HSCachedDeckCompletionRequest : IProtoBuf
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00003FD2 File Offset: 0x000021D2
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x00003FDA File Offset: 0x000021DA
		public long PlayerId
		{
			get
			{
				return this._PlayerId;
			}
			set
			{
				this._PlayerId = value;
				this.HasPlayerId = true;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00003FEA File Offset: 0x000021EA
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00003FF2 File Offset: 0x000021F2
		public long BnetAccountId
		{
			get
			{
				return this._BnetAccountId;
			}
			set
			{
				this._BnetAccountId = value;
				this.HasBnetAccountId = true;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00004002 File Offset: 0x00002202
		// (set) Token: 0x060000BA RID: 186 RVA: 0x0000400A File Offset: 0x0000220A
		public int HeroClass
		{
			get
			{
				return this._HeroClass;
			}
			set
			{
				this._HeroClass = value;
				this.HasHeroClass = true;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000BB RID: 187 RVA: 0x0000401A File Offset: 0x0000221A
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00004022 File Offset: 0x00002222
		public List<SmartDeckCardData> InsertedCard
		{
			get
			{
				return this._InsertedCard;
			}
			set
			{
				this._InsertedCard = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000BD RID: 189 RVA: 0x0000402B File Offset: 0x0000222B
		// (set) Token: 0x060000BE RID: 190 RVA: 0x00004033 File Offset: 0x00002233
		public long DeckId
		{
			get
			{
				return this._DeckId;
			}
			set
			{
				this._DeckId = value;
				this.HasDeckId = true;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00004043 File Offset: 0x00002243
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x0000404B File Offset: 0x0000224B
		public FormatType FormatType
		{
			get
			{
				return this._FormatType;
			}
			set
			{
				this._FormatType = value;
				this.HasFormatType = true;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x0000405B File Offset: 0x0000225B
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00004063 File Offset: 0x00002263
		public string Version
		{
			get
			{
				return this._Version;
			}
			set
			{
				this._Version = value;
				this.HasVersion = (value != null);
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004078 File Offset: 0x00002278
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasPlayerId)
			{
				num ^= this.PlayerId.GetHashCode();
			}
			if (this.HasBnetAccountId)
			{
				num ^= this.BnetAccountId.GetHashCode();
			}
			if (this.HasHeroClass)
			{
				num ^= this.HeroClass.GetHashCode();
			}
			foreach (SmartDeckCardData smartDeckCardData in this.InsertedCard)
			{
				num ^= smartDeckCardData.GetHashCode();
			}
			if (this.HasDeckId)
			{
				num ^= this.DeckId.GetHashCode();
			}
			if (this.HasFormatType)
			{
				num ^= this.FormatType.GetHashCode();
			}
			if (this.HasVersion)
			{
				num ^= this.Version.GetHashCode();
			}
			return num;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004178 File Offset: 0x00002378
		public override bool Equals(object obj)
		{
			HSCachedDeckCompletionRequest hscachedDeckCompletionRequest = obj as HSCachedDeckCompletionRequest;
			if (hscachedDeckCompletionRequest == null)
			{
				return false;
			}
			if (this.HasPlayerId != hscachedDeckCompletionRequest.HasPlayerId || (this.HasPlayerId && !this.PlayerId.Equals(hscachedDeckCompletionRequest.PlayerId)))
			{
				return false;
			}
			if (this.HasBnetAccountId != hscachedDeckCompletionRequest.HasBnetAccountId || (this.HasBnetAccountId && !this.BnetAccountId.Equals(hscachedDeckCompletionRequest.BnetAccountId)))
			{
				return false;
			}
			if (this.HasHeroClass != hscachedDeckCompletionRequest.HasHeroClass || (this.HasHeroClass && !this.HeroClass.Equals(hscachedDeckCompletionRequest.HeroClass)))
			{
				return false;
			}
			if (this.InsertedCard.Count != hscachedDeckCompletionRequest.InsertedCard.Count)
			{
				return false;
			}
			for (int i = 0; i < this.InsertedCard.Count; i++)
			{
				if (!this.InsertedCard[i].Equals(hscachedDeckCompletionRequest.InsertedCard[i]))
				{
					return false;
				}
			}
			return this.HasDeckId == hscachedDeckCompletionRequest.HasDeckId && (!this.HasDeckId || this.DeckId.Equals(hscachedDeckCompletionRequest.DeckId)) && this.HasFormatType == hscachedDeckCompletionRequest.HasFormatType && (!this.HasFormatType || this.FormatType.Equals(hscachedDeckCompletionRequest.FormatType)) && this.HasVersion == hscachedDeckCompletionRequest.HasVersion && (!this.HasVersion || this.Version.Equals(hscachedDeckCompletionRequest.Version));
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004300 File Offset: 0x00002500
		public void Deserialize(Stream stream)
		{
			HSCachedDeckCompletionRequest.Deserialize(stream, this);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000430A File Offset: 0x0000250A
		public static HSCachedDeckCompletionRequest Deserialize(Stream stream, HSCachedDeckCompletionRequest instance)
		{
			return HSCachedDeckCompletionRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004318 File Offset: 0x00002518
		public static HSCachedDeckCompletionRequest DeserializeLengthDelimited(Stream stream)
		{
			HSCachedDeckCompletionRequest hscachedDeckCompletionRequest = new HSCachedDeckCompletionRequest();
			HSCachedDeckCompletionRequest.DeserializeLengthDelimited(stream, hscachedDeckCompletionRequest);
			return hscachedDeckCompletionRequest;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004334 File Offset: 0x00002534
		public static HSCachedDeckCompletionRequest DeserializeLengthDelimited(Stream stream, HSCachedDeckCompletionRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return HSCachedDeckCompletionRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000435C File Offset: 0x0000255C
		public static HSCachedDeckCompletionRequest Deserialize(Stream stream, HSCachedDeckCompletionRequest instance, long limit)
		{
			if (instance.InsertedCard == null)
			{
				instance.InsertedCard = new List<SmartDeckCardData>();
			}
			instance.FormatType = FormatType.FT_UNKNOWN;
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
				else
				{
					if (num <= 24)
					{
						if (num == 8)
						{
							instance.PlayerId = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.BnetAccountId = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 24)
						{
							instance.HeroClass = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else if (num <= 40)
					{
						if (num == 34)
						{
							instance.InsertedCard.Add(SmartDeckCardData.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 40)
						{
							instance.DeckId = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 48)
						{
							instance.FormatType = (FormatType)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 58)
						{
							instance.Version = ProtocolParser.ReadString(stream);
							continue;
						}
					}
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

		// Token: 0x060000CA RID: 202 RVA: 0x000044A2 File Offset: 0x000026A2
		public void Serialize(Stream stream)
		{
			HSCachedDeckCompletionRequest.Serialize(stream, this);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000044AC File Offset: 0x000026AC
		public static void Serialize(Stream stream, HSCachedDeckCompletionRequest instance)
		{
			if (instance.HasPlayerId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PlayerId);
			}
			if (instance.HasBnetAccountId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.BnetAccountId);
			}
			if (instance.HasHeroClass)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.HeroClass));
			}
			if (instance.InsertedCard.Count > 0)
			{
				foreach (SmartDeckCardData smartDeckCardData in instance.InsertedCard)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, smartDeckCardData.GetSerializedSize());
					SmartDeckCardData.Serialize(stream, smartDeckCardData);
				}
			}
			if (instance.HasDeckId)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DeckId);
			}
			if (instance.HasFormatType)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.FormatType));
			}
			if (instance.HasVersion)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Version));
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000045D4 File Offset: 0x000027D4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasPlayerId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.PlayerId);
			}
			if (this.HasBnetAccountId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.BnetAccountId);
			}
			if (this.HasHeroClass)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.HeroClass));
			}
			if (this.InsertedCard.Count > 0)
			{
				foreach (SmartDeckCardData smartDeckCardData in this.InsertedCard)
				{
					num += 1U;
					uint serializedSize = smartDeckCardData.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasDeckId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.DeckId);
			}
			if (this.HasFormatType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.FormatType));
			}
			if (this.HasVersion)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Version);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x0400002E RID: 46
		public bool HasPlayerId;

		// Token: 0x0400002F RID: 47
		private long _PlayerId;

		// Token: 0x04000030 RID: 48
		public bool HasBnetAccountId;

		// Token: 0x04000031 RID: 49
		private long _BnetAccountId;

		// Token: 0x04000032 RID: 50
		public bool HasHeroClass;

		// Token: 0x04000033 RID: 51
		private int _HeroClass;

		// Token: 0x04000034 RID: 52
		private List<SmartDeckCardData> _InsertedCard = new List<SmartDeckCardData>();

		// Token: 0x04000035 RID: 53
		public bool HasDeckId;

		// Token: 0x04000036 RID: 54
		private long _DeckId;

		// Token: 0x04000037 RID: 55
		public bool HasFormatType;

		// Token: 0x04000038 RID: 56
		private FormatType _FormatType;

		// Token: 0x04000039 RID: 57
		public bool HasVersion;

		// Token: 0x0400003A RID: 58
		private string _Version;
	}
}
