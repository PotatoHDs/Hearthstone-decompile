using System;
using System.IO;

namespace PegasusGame
{
	// Token: 0x020001B2 RID: 434
	public class GameSetup : IProtoBuf
	{
		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001B69 RID: 7017 RVA: 0x00060B9A File Offset: 0x0005ED9A
		// (set) Token: 0x06001B6A RID: 7018 RVA: 0x00060BA2 File Offset: 0x0005EDA2
		public int Board { get; set; }

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06001B6B RID: 7019 RVA: 0x00060BAB File Offset: 0x0005EDAB
		// (set) Token: 0x06001B6C RID: 7020 RVA: 0x00060BB3 File Offset: 0x0005EDB3
		public int MaxSecretZoneSizePerPlayer { get; set; }

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001B6D RID: 7021 RVA: 0x00060BBC File Offset: 0x0005EDBC
		// (set) Token: 0x06001B6E RID: 7022 RVA: 0x00060BC4 File Offset: 0x0005EDC4
		public int MaxSecretsPerPlayer { get; set; }

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001B6F RID: 7023 RVA: 0x00060BCD File Offset: 0x0005EDCD
		// (set) Token: 0x06001B70 RID: 7024 RVA: 0x00060BD5 File Offset: 0x0005EDD5
		public int MaxQuestsPerPlayer { get; set; }

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001B71 RID: 7025 RVA: 0x00060BDE File Offset: 0x0005EDDE
		// (set) Token: 0x06001B72 RID: 7026 RVA: 0x00060BE6 File Offset: 0x0005EDE6
		public int MaxFriendlyMinionsPerPlayer { get; set; }

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001B73 RID: 7027 RVA: 0x00060BEF File Offset: 0x0005EDEF
		// (set) Token: 0x06001B74 RID: 7028 RVA: 0x00060BF7 File Offset: 0x0005EDF7
		public uint KeepAliveFrequencySeconds
		{
			get
			{
				return this._KeepAliveFrequencySeconds;
			}
			set
			{
				this._KeepAliveFrequencySeconds = value;
				this.HasKeepAliveFrequencySeconds = true;
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001B75 RID: 7029 RVA: 0x00060C07 File Offset: 0x0005EE07
		// (set) Token: 0x06001B76 RID: 7030 RVA: 0x00060C0F File Offset: 0x0005EE0F
		public uint DisconnectWhenStuckSeconds
		{
			get
			{
				return this._DisconnectWhenStuckSeconds;
			}
			set
			{
				this._DisconnectWhenStuckSeconds = value;
				this.HasDisconnectWhenStuckSeconds = true;
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001B77 RID: 7031 RVA: 0x00060C1F File Offset: 0x0005EE1F
		// (set) Token: 0x06001B78 RID: 7032 RVA: 0x00060C27 File Offset: 0x0005EE27
		public uint KeepAliveRetry
		{
			get
			{
				return this._KeepAliveRetry;
			}
			set
			{
				this._KeepAliveRetry = value;
				this.HasKeepAliveRetry = true;
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001B79 RID: 7033 RVA: 0x00060C37 File Offset: 0x0005EE37
		// (set) Token: 0x06001B7A RID: 7034 RVA: 0x00060C3F File Offset: 0x0005EE3F
		public uint KeepAliveWaitForInternetSeconds
		{
			get
			{
				return this._KeepAliveWaitForInternetSeconds;
			}
			set
			{
				this._KeepAliveWaitForInternetSeconds = value;
				this.HasKeepAliveWaitForInternetSeconds = true;
			}
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x00060C50 File Offset: 0x0005EE50
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Board.GetHashCode();
			num ^= this.MaxSecretZoneSizePerPlayer.GetHashCode();
			num ^= this.MaxSecretsPerPlayer.GetHashCode();
			num ^= this.MaxQuestsPerPlayer.GetHashCode();
			num ^= this.MaxFriendlyMinionsPerPlayer.GetHashCode();
			if (this.HasKeepAliveFrequencySeconds)
			{
				num ^= this.KeepAliveFrequencySeconds.GetHashCode();
			}
			if (this.HasDisconnectWhenStuckSeconds)
			{
				num ^= this.DisconnectWhenStuckSeconds.GetHashCode();
			}
			if (this.HasKeepAliveRetry)
			{
				num ^= this.KeepAliveRetry.GetHashCode();
			}
			if (this.HasKeepAliveWaitForInternetSeconds)
			{
				num ^= this.KeepAliveWaitForInternetSeconds.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x00060D24 File Offset: 0x0005EF24
		public override bool Equals(object obj)
		{
			GameSetup gameSetup = obj as GameSetup;
			return gameSetup != null && this.Board.Equals(gameSetup.Board) && this.MaxSecretZoneSizePerPlayer.Equals(gameSetup.MaxSecretZoneSizePerPlayer) && this.MaxSecretsPerPlayer.Equals(gameSetup.MaxSecretsPerPlayer) && this.MaxQuestsPerPlayer.Equals(gameSetup.MaxQuestsPerPlayer) && this.MaxFriendlyMinionsPerPlayer.Equals(gameSetup.MaxFriendlyMinionsPerPlayer) && this.HasKeepAliveFrequencySeconds == gameSetup.HasKeepAliveFrequencySeconds && (!this.HasKeepAliveFrequencySeconds || this.KeepAliveFrequencySeconds.Equals(gameSetup.KeepAliveFrequencySeconds)) && this.HasDisconnectWhenStuckSeconds == gameSetup.HasDisconnectWhenStuckSeconds && (!this.HasDisconnectWhenStuckSeconds || this.DisconnectWhenStuckSeconds.Equals(gameSetup.DisconnectWhenStuckSeconds)) && this.HasKeepAliveRetry == gameSetup.HasKeepAliveRetry && (!this.HasKeepAliveRetry || this.KeepAliveRetry.Equals(gameSetup.KeepAliveRetry)) && this.HasKeepAliveWaitForInternetSeconds == gameSetup.HasKeepAliveWaitForInternetSeconds && (!this.HasKeepAliveWaitForInternetSeconds || this.KeepAliveWaitForInternetSeconds.Equals(gameSetup.KeepAliveWaitForInternetSeconds));
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x00060E6E File Offset: 0x0005F06E
		public void Deserialize(Stream stream)
		{
			GameSetup.Deserialize(stream, this);
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x00060E78 File Offset: 0x0005F078
		public static GameSetup Deserialize(Stream stream, GameSetup instance)
		{
			return GameSetup.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x00060E84 File Offset: 0x0005F084
		public static GameSetup DeserializeLengthDelimited(Stream stream)
		{
			GameSetup gameSetup = new GameSetup();
			GameSetup.DeserializeLengthDelimited(stream, gameSetup);
			return gameSetup;
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x00060EA0 File Offset: 0x0005F0A0
		public static GameSetup DeserializeLengthDelimited(Stream stream, GameSetup instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameSetup.Deserialize(stream, instance, num);
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x00060EC8 File Offset: 0x0005F0C8
		public static GameSetup Deserialize(Stream stream, GameSetup instance, long limit)
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
					if (num <= 32)
					{
						if (num <= 16)
						{
							if (num == 8)
							{
								instance.Board = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 16)
							{
								instance.MaxSecretZoneSizePerPlayer = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else
						{
							if (num == 24)
							{
								instance.MaxSecretsPerPlayer = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 32)
							{
								instance.MaxQuestsPerPlayer = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
					}
					else if (num <= 48)
					{
						if (num == 40)
						{
							instance.MaxFriendlyMinionsPerPlayer = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 48)
						{
							instance.KeepAliveFrequencySeconds = ProtocolParser.ReadUInt32(stream);
							continue;
						}
					}
					else
					{
						if (num == 56)
						{
							instance.DisconnectWhenStuckSeconds = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num == 64)
						{
							instance.KeepAliveRetry = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num == 72)
						{
							instance.KeepAliveWaitForInternetSeconds = ProtocolParser.ReadUInt32(stream);
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

		// Token: 0x06001B82 RID: 7042 RVA: 0x00061034 File Offset: 0x0005F234
		public void Serialize(Stream stream)
		{
			GameSetup.Serialize(stream, this);
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x00061040 File Offset: 0x0005F240
		public static void Serialize(Stream stream, GameSetup instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Board));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.MaxSecretZoneSizePerPlayer));
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.MaxSecretsPerPlayer));
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.MaxQuestsPerPlayer));
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.MaxFriendlyMinionsPerPlayer));
			if (instance.HasKeepAliveFrequencySeconds)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt32(stream, instance.KeepAliveFrequencySeconds);
			}
			if (instance.HasDisconnectWhenStuckSeconds)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt32(stream, instance.DisconnectWhenStuckSeconds);
			}
			if (instance.HasKeepAliveRetry)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt32(stream, instance.KeepAliveRetry);
			}
			if (instance.HasKeepAliveWaitForInternetSeconds)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt32(stream, instance.KeepAliveWaitForInternetSeconds);
			}
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x00061128 File Offset: 0x0005F328
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Board));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.MaxSecretZoneSizePerPlayer));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.MaxSecretsPerPlayer));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.MaxQuestsPerPlayer));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.MaxFriendlyMinionsPerPlayer));
			if (this.HasKeepAliveFrequencySeconds)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.KeepAliveFrequencySeconds);
			}
			if (this.HasDisconnectWhenStuckSeconds)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.DisconnectWhenStuckSeconds);
			}
			if (this.HasKeepAliveRetry)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.KeepAliveRetry);
			}
			if (this.HasKeepAliveWaitForInternetSeconds)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.KeepAliveWaitForInternetSeconds);
			}
			return num + 5U;
		}

		// Token: 0x04000A18 RID: 2584
		public bool HasKeepAliveFrequencySeconds;

		// Token: 0x04000A19 RID: 2585
		private uint _KeepAliveFrequencySeconds;

		// Token: 0x04000A1A RID: 2586
		public bool HasDisconnectWhenStuckSeconds;

		// Token: 0x04000A1B RID: 2587
		private uint _DisconnectWhenStuckSeconds;

		// Token: 0x04000A1C RID: 2588
		public bool HasKeepAliveRetry;

		// Token: 0x04000A1D RID: 2589
		private uint _KeepAliveRetry;

		// Token: 0x04000A1E RID: 2590
		public bool HasKeepAliveWaitForInternetSeconds;

		// Token: 0x04000A1F RID: 2591
		private uint _KeepAliveWaitForInternetSeconds;

		// Token: 0x02000649 RID: 1609
		public enum PacketID
		{
			// Token: 0x04002106 RID: 8454
			ID = 16
		}
	}
}
