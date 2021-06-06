using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x02000049 RID: 73
	public class PlayerRecord : IProtoBuf
	{
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x00013435 File Offset: 0x00011635
		// (set) Token: 0x06000490 RID: 1168 RVA: 0x0001343D File Offset: 0x0001163D
		public GameType Type { get; set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x00013446 File Offset: 0x00011646
		// (set) Token: 0x06000492 RID: 1170 RVA: 0x0001344E File Offset: 0x0001164E
		public int Data
		{
			get
			{
				return this._Data;
			}
			set
			{
				this._Data = value;
				this.HasData = true;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x0001345E File Offset: 0x0001165E
		// (set) Token: 0x06000494 RID: 1172 RVA: 0x00013466 File Offset: 0x00011666
		public int Wins { get; set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x0001346F File Offset: 0x0001166F
		// (set) Token: 0x06000496 RID: 1174 RVA: 0x00013477 File Offset: 0x00011677
		public int Losses { get; set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x00013480 File Offset: 0x00011680
		// (set) Token: 0x06000498 RID: 1176 RVA: 0x00013488 File Offset: 0x00011688
		public int Ties
		{
			get
			{
				return this._Ties;
			}
			set
			{
				this._Ties = value;
				this.HasTies = true;
			}
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00013498 File Offset: 0x00011698
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Type.GetHashCode();
			if (this.HasData)
			{
				num ^= this.Data.GetHashCode();
			}
			num ^= this.Wins.GetHashCode();
			num ^= this.Losses.GetHashCode();
			if (this.HasTies)
			{
				num ^= this.Ties.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00013520 File Offset: 0x00011720
		public override bool Equals(object obj)
		{
			PlayerRecord playerRecord = obj as PlayerRecord;
			return playerRecord != null && this.Type.Equals(playerRecord.Type) && this.HasData == playerRecord.HasData && (!this.HasData || this.Data.Equals(playerRecord.Data)) && this.Wins.Equals(playerRecord.Wins) && this.Losses.Equals(playerRecord.Losses) && this.HasTies == playerRecord.HasTies && (!this.HasTies || this.Ties.Equals(playerRecord.Ties));
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x000135E9 File Offset: 0x000117E9
		public void Deserialize(Stream stream)
		{
			PlayerRecord.Deserialize(stream, this);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x000135F3 File Offset: 0x000117F3
		public static PlayerRecord Deserialize(Stream stream, PlayerRecord instance)
		{
			return PlayerRecord.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00013600 File Offset: 0x00011800
		public static PlayerRecord DeserializeLengthDelimited(Stream stream)
		{
			PlayerRecord playerRecord = new PlayerRecord();
			PlayerRecord.DeserializeLengthDelimited(stream, playerRecord);
			return playerRecord;
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0001361C File Offset: 0x0001181C
		public static PlayerRecord DeserializeLengthDelimited(Stream stream, PlayerRecord instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PlayerRecord.Deserialize(stream, instance, num);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00013644 File Offset: 0x00011844
		public static PlayerRecord Deserialize(Stream stream, PlayerRecord instance, long limit)
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
				else
				{
					if (num <= 16)
					{
						if (num == 8)
						{
							instance.Type = (GameType)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.Data = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.Wins = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 32)
						{
							instance.Losses = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 40)
						{
							instance.Ties = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x060004A0 RID: 1184 RVA: 0x0001372F File Offset: 0x0001192F
		public void Serialize(Stream stream)
		{
			PlayerRecord.Serialize(stream, this);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00013738 File Offset: 0x00011938
		public static void Serialize(Stream stream, PlayerRecord instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Type));
			if (instance.HasData)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Data));
			}
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Wins));
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Losses));
			if (instance.HasTies)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Ties));
			}
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x000137C0 File Offset: 0x000119C0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Type));
			if (this.HasData)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Data));
			}
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Wins));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Losses));
			if (this.HasTies)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Ties));
			}
			return num + 3U;
		}

		// Token: 0x040001B2 RID: 434
		public bool HasData;

		// Token: 0x040001B3 RID: 435
		private int _Data;

		// Token: 0x040001B6 RID: 438
		public bool HasTies;

		// Token: 0x040001B7 RID: 439
		private int _Ties;
	}
}
