using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011E3 RID: 4579
	public class Player : IProtoBuf
	{
		// Token: 0x17000F9E RID: 3998
		// (get) Token: 0x0600CC4F RID: 52303 RVA: 0x003D06C6 File Offset: 0x003CE8C6
		// (set) Token: 0x0600CC50 RID: 52304 RVA: 0x003D06CE File Offset: 0x003CE8CE
		public long BattleNetIdLo
		{
			get
			{
				return this._BattleNetIdLo;
			}
			set
			{
				this._BattleNetIdLo = value;
				this.HasBattleNetIdLo = true;
			}
		}

		// Token: 0x17000F9F RID: 3999
		// (get) Token: 0x0600CC51 RID: 52305 RVA: 0x003D06DE File Offset: 0x003CE8DE
		// (set) Token: 0x0600CC52 RID: 52306 RVA: 0x003D06E6 File Offset: 0x003CE8E6
		public long GameAccountId
		{
			get
			{
				return this._GameAccountId;
			}
			set
			{
				this._GameAccountId = value;
				this.HasGameAccountId = true;
			}
		}

		// Token: 0x17000FA0 RID: 4000
		// (get) Token: 0x0600CC53 RID: 52307 RVA: 0x003D06F6 File Offset: 0x003CE8F6
		// (set) Token: 0x0600CC54 RID: 52308 RVA: 0x003D06FE File Offset: 0x003CE8FE
		public string BnetRegion
		{
			get
			{
				return this._BnetRegion;
			}
			set
			{
				this._BnetRegion = value;
				this.HasBnetRegion = (value != null);
			}
		}

		// Token: 0x17000FA1 RID: 4001
		// (get) Token: 0x0600CC55 RID: 52309 RVA: 0x003D0711 File Offset: 0x003CE911
		// (set) Token: 0x0600CC56 RID: 52310 RVA: 0x003D0719 File Offset: 0x003CE919
		public string Locale
		{
			get
			{
				return this._Locale;
			}
			set
			{
				this._Locale = value;
				this.HasLocale = (value != null);
			}
		}

		// Token: 0x0600CC57 RID: 52311 RVA: 0x003D072C File Offset: 0x003CE92C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasBattleNetIdLo)
			{
				num ^= this.BattleNetIdLo.GetHashCode();
			}
			if (this.HasGameAccountId)
			{
				num ^= this.GameAccountId.GetHashCode();
			}
			if (this.HasBnetRegion)
			{
				num ^= this.BnetRegion.GetHashCode();
			}
			if (this.HasLocale)
			{
				num ^= this.Locale.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CC58 RID: 52312 RVA: 0x003D07A4 File Offset: 0x003CE9A4
		public override bool Equals(object obj)
		{
			Player player = obj as Player;
			return player != null && this.HasBattleNetIdLo == player.HasBattleNetIdLo && (!this.HasBattleNetIdLo || this.BattleNetIdLo.Equals(player.BattleNetIdLo)) && this.HasGameAccountId == player.HasGameAccountId && (!this.HasGameAccountId || this.GameAccountId.Equals(player.GameAccountId)) && this.HasBnetRegion == player.HasBnetRegion && (!this.HasBnetRegion || this.BnetRegion.Equals(player.BnetRegion)) && this.HasLocale == player.HasLocale && (!this.HasLocale || this.Locale.Equals(player.Locale));
		}

		// Token: 0x0600CC59 RID: 52313 RVA: 0x003D0870 File Offset: 0x003CEA70
		public void Deserialize(Stream stream)
		{
			Player.Deserialize(stream, this);
		}

		// Token: 0x0600CC5A RID: 52314 RVA: 0x003D087A File Offset: 0x003CEA7A
		public static Player Deserialize(Stream stream, Player instance)
		{
			return Player.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CC5B RID: 52315 RVA: 0x003D0888 File Offset: 0x003CEA88
		public static Player DeserializeLengthDelimited(Stream stream)
		{
			Player player = new Player();
			Player.DeserializeLengthDelimited(stream, player);
			return player;
		}

		// Token: 0x0600CC5C RID: 52316 RVA: 0x003D08A4 File Offset: 0x003CEAA4
		public static Player DeserializeLengthDelimited(Stream stream, Player instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Player.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CC5D RID: 52317 RVA: 0x003D08CC File Offset: 0x003CEACC
		public static Player Deserialize(Stream stream, Player instance, long limit)
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
							instance.BattleNetIdLo = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.GameAccountId = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 26)
						{
							instance.BnetRegion = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 34)
						{
							instance.Locale = ProtocolParser.ReadString(stream);
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

		// Token: 0x0600CC5E RID: 52318 RVA: 0x003D099C File Offset: 0x003CEB9C
		public void Serialize(Stream stream)
		{
			Player.Serialize(stream, this);
		}

		// Token: 0x0600CC5F RID: 52319 RVA: 0x003D09A8 File Offset: 0x003CEBA8
		public static void Serialize(Stream stream, Player instance)
		{
			if (instance.HasBattleNetIdLo)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.BattleNetIdLo);
			}
			if (instance.HasGameAccountId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.GameAccountId);
			}
			if (instance.HasBnetRegion)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BnetRegion));
			}
			if (instance.HasLocale)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Locale));
			}
		}

		// Token: 0x0600CC60 RID: 52320 RVA: 0x003D0A38 File Offset: 0x003CEC38
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasBattleNetIdLo)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.BattleNetIdLo);
			}
			if (this.HasGameAccountId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.GameAccountId);
			}
			if (this.HasBnetRegion)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.BnetRegion);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasLocale)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.Locale);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}

		// Token: 0x0400A087 RID: 41095
		public bool HasBattleNetIdLo;

		// Token: 0x0400A088 RID: 41096
		private long _BattleNetIdLo;

		// Token: 0x0400A089 RID: 41097
		public bool HasGameAccountId;

		// Token: 0x0400A08A RID: 41098
		private long _GameAccountId;

		// Token: 0x0400A08B RID: 41099
		public bool HasBnetRegion;

		// Token: 0x0400A08C RID: 41100
		private string _BnetRegion;

		// Token: 0x0400A08D RID: 41101
		public bool HasLocale;

		// Token: 0x0400A08E RID: 41102
		private string _Locale;
	}
}
