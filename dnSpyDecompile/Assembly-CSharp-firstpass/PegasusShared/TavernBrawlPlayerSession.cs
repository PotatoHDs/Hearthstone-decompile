using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x0200015A RID: 346
	public class TavernBrawlPlayerSession : IProtoBuf
	{
		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x0600172E RID: 5934 RVA: 0x00050000 File Offset: 0x0004E200
		// (set) Token: 0x0600172F RID: 5935 RVA: 0x00050008 File Offset: 0x0004E208
		public ErrorCode ErrorCode
		{
			get
			{
				return this._ErrorCode;
			}
			set
			{
				this._ErrorCode = value;
				this.HasErrorCode = true;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001730 RID: 5936 RVA: 0x00050018 File Offset: 0x0004E218
		// (set) Token: 0x06001731 RID: 5937 RVA: 0x00050020 File Offset: 0x0004E220
		public int SeasonId { get; set; }

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001732 RID: 5938 RVA: 0x00050029 File Offset: 0x0004E229
		// (set) Token: 0x06001733 RID: 5939 RVA: 0x00050031 File Offset: 0x0004E231
		public int Wins { get; set; }

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001734 RID: 5940 RVA: 0x0005003A File Offset: 0x0004E23A
		// (set) Token: 0x06001735 RID: 5941 RVA: 0x00050042 File Offset: 0x0004E242
		public int Losses { get; set; }

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06001736 RID: 5942 RVA: 0x0005004B File Offset: 0x0004E24B
		// (set) Token: 0x06001737 RID: 5943 RVA: 0x00050053 File Offset: 0x0004E253
		public RewardChest Chest
		{
			get
			{
				return this._Chest;
			}
			set
			{
				this._Chest = value;
				this.HasChest = (value != null);
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06001738 RID: 5944 RVA: 0x00050066 File Offset: 0x0004E266
		// (set) Token: 0x06001739 RID: 5945 RVA: 0x0005006E File Offset: 0x0004E26E
		public bool DeckLocked { get; set; }

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x0600173A RID: 5946 RVA: 0x00050077 File Offset: 0x0004E277
		// (set) Token: 0x0600173B RID: 5947 RVA: 0x0005007F File Offset: 0x0004E27F
		public uint SessionCount
		{
			get
			{
				return this._SessionCount;
			}
			set
			{
				this._SessionCount = value;
				this.HasSessionCount = true;
			}
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x00050090 File Offset: 0x0004E290
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasErrorCode)
			{
				num ^= this.ErrorCode.GetHashCode();
			}
			num ^= this.SeasonId.GetHashCode();
			num ^= this.Wins.GetHashCode();
			num ^= this.Losses.GetHashCode();
			if (this.HasChest)
			{
				num ^= this.Chest.GetHashCode();
			}
			num ^= this.DeckLocked.GetHashCode();
			if (this.HasSessionCount)
			{
				num ^= this.SessionCount.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x00050140 File Offset: 0x0004E340
		public override bool Equals(object obj)
		{
			TavernBrawlPlayerSession tavernBrawlPlayerSession = obj as TavernBrawlPlayerSession;
			return tavernBrawlPlayerSession != null && this.HasErrorCode == tavernBrawlPlayerSession.HasErrorCode && (!this.HasErrorCode || this.ErrorCode.Equals(tavernBrawlPlayerSession.ErrorCode)) && this.SeasonId.Equals(tavernBrawlPlayerSession.SeasonId) && this.Wins.Equals(tavernBrawlPlayerSession.Wins) && this.Losses.Equals(tavernBrawlPlayerSession.Losses) && this.HasChest == tavernBrawlPlayerSession.HasChest && (!this.HasChest || this.Chest.Equals(tavernBrawlPlayerSession.Chest)) && this.DeckLocked.Equals(tavernBrawlPlayerSession.DeckLocked) && this.HasSessionCount == tavernBrawlPlayerSession.HasSessionCount && (!this.HasSessionCount || this.SessionCount.Equals(tavernBrawlPlayerSession.SessionCount));
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x0005024D File Offset: 0x0004E44D
		public void Deserialize(Stream stream)
		{
			TavernBrawlPlayerSession.Deserialize(stream, this);
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x00050257 File Offset: 0x0004E457
		public static TavernBrawlPlayerSession Deserialize(Stream stream, TavernBrawlPlayerSession instance)
		{
			return TavernBrawlPlayerSession.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x00050264 File Offset: 0x0004E464
		public static TavernBrawlPlayerSession DeserializeLengthDelimited(Stream stream)
		{
			TavernBrawlPlayerSession tavernBrawlPlayerSession = new TavernBrawlPlayerSession();
			TavernBrawlPlayerSession.DeserializeLengthDelimited(stream, tavernBrawlPlayerSession);
			return tavernBrawlPlayerSession;
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x00050280 File Offset: 0x0004E480
		public static TavernBrawlPlayerSession DeserializeLengthDelimited(Stream stream, TavernBrawlPlayerSession instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return TavernBrawlPlayerSession.Deserialize(stream, instance, num);
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x000502A8 File Offset: 0x0004E4A8
		public static TavernBrawlPlayerSession Deserialize(Stream stream, TavernBrawlPlayerSession instance, long limit)
		{
			instance.ErrorCode = ErrorCode.ERROR_OK;
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
							instance.ErrorCode = (ErrorCode)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.SeasonId = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 24)
						{
							instance.Wins = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else if (num <= 42)
					{
						if (num == 32)
						{
							instance.Losses = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 42)
						{
							if (instance.Chest == null)
							{
								instance.Chest = RewardChest.DeserializeLengthDelimited(stream);
								continue;
							}
							RewardChest.DeserializeLengthDelimited(stream, instance.Chest);
							continue;
						}
					}
					else
					{
						if (num == 48)
						{
							instance.DeckLocked = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 56)
						{
							instance.SessionCount = ProtocolParser.ReadUInt32(stream);
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

		// Token: 0x06001743 RID: 5955 RVA: 0x000503FB File Offset: 0x0004E5FB
		public void Serialize(Stream stream)
		{
			TavernBrawlPlayerSession.Serialize(stream, this);
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x00050404 File Offset: 0x0004E604
		public static void Serialize(Stream stream, TavernBrawlPlayerSession instance)
		{
			if (instance.HasErrorCode)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ErrorCode));
			}
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SeasonId));
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Wins));
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Losses));
			if (instance.HasChest)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.Chest.GetSerializedSize());
				RewardChest.Serialize(stream, instance.Chest);
			}
			stream.WriteByte(48);
			ProtocolParser.WriteBool(stream, instance.DeckLocked);
			if (instance.HasSessionCount)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt32(stream, instance.SessionCount);
			}
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x000504CC File Offset: 0x0004E6CC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasErrorCode)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ErrorCode));
			}
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.SeasonId));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Wins));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Losses));
			if (this.HasChest)
			{
				num += 1U;
				uint serializedSize = this.Chest.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			num += 1U;
			if (this.HasSessionCount)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.SessionCount);
			}
			return num + 4U;
		}

		// Token: 0x0400072A RID: 1834
		public bool HasErrorCode;

		// Token: 0x0400072B RID: 1835
		private ErrorCode _ErrorCode;

		// Token: 0x0400072F RID: 1839
		public bool HasChest;

		// Token: 0x04000730 RID: 1840
		private RewardChest _Chest;

		// Token: 0x04000732 RID: 1842
		public bool HasSessionCount;

		// Token: 0x04000733 RID: 1843
		private uint _SessionCount;
	}
}
