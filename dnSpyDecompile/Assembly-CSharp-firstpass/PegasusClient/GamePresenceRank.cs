using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusClient
{
	// Token: 0x0200002A RID: 42
	public class GamePresenceRank : IProtoBuf
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000217 RID: 535 RVA: 0x000091DC File Offset: 0x000073DC
		// (set) Token: 0x06000218 RID: 536 RVA: 0x000091E4 File Offset: 0x000073E4
		public GamePresenceRankData StandardDeprecated
		{
			get
			{
				return this._StandardDeprecated;
			}
			set
			{
				this._StandardDeprecated = value;
				this.HasStandardDeprecated = (value != null);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000219 RID: 537 RVA: 0x000091F7 File Offset: 0x000073F7
		// (set) Token: 0x0600021A RID: 538 RVA: 0x000091FF File Offset: 0x000073FF
		public GamePresenceRankData WildDeprecated
		{
			get
			{
				return this._WildDeprecated;
			}
			set
			{
				this._WildDeprecated = value;
				this.HasWildDeprecated = (value != null);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00009212 File Offset: 0x00007412
		// (set) Token: 0x0600021C RID: 540 RVA: 0x0000921A File Offset: 0x0000741A
		public List<GamePresenceRankData> Values
		{
			get
			{
				return this._Values;
			}
			set
			{
				this._Values = value;
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00009224 File Offset: 0x00007424
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasStandardDeprecated)
			{
				num ^= this.StandardDeprecated.GetHashCode();
			}
			if (this.HasWildDeprecated)
			{
				num ^= this.WildDeprecated.GetHashCode();
			}
			foreach (GamePresenceRankData gamePresenceRankData in this.Values)
			{
				num ^= gamePresenceRankData.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x000092B4 File Offset: 0x000074B4
		public override bool Equals(object obj)
		{
			GamePresenceRank gamePresenceRank = obj as GamePresenceRank;
			if (gamePresenceRank == null)
			{
				return false;
			}
			if (this.HasStandardDeprecated != gamePresenceRank.HasStandardDeprecated || (this.HasStandardDeprecated && !this.StandardDeprecated.Equals(gamePresenceRank.StandardDeprecated)))
			{
				return false;
			}
			if (this.HasWildDeprecated != gamePresenceRank.HasWildDeprecated || (this.HasWildDeprecated && !this.WildDeprecated.Equals(gamePresenceRank.WildDeprecated)))
			{
				return false;
			}
			if (this.Values.Count != gamePresenceRank.Values.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Values.Count; i++)
			{
				if (!this.Values[i].Equals(gamePresenceRank.Values[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00009375 File Offset: 0x00007575
		public void Deserialize(Stream stream)
		{
			GamePresenceRank.Deserialize(stream, this);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000937F File Offset: 0x0000757F
		public static GamePresenceRank Deserialize(Stream stream, GamePresenceRank instance)
		{
			return GamePresenceRank.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000938C File Offset: 0x0000758C
		public static GamePresenceRank DeserializeLengthDelimited(Stream stream)
		{
			GamePresenceRank gamePresenceRank = new GamePresenceRank();
			GamePresenceRank.DeserializeLengthDelimited(stream, gamePresenceRank);
			return gamePresenceRank;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x000093A8 File Offset: 0x000075A8
		public static GamePresenceRank DeserializeLengthDelimited(Stream stream, GamePresenceRank instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GamePresenceRank.Deserialize(stream, instance, num);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x000093D0 File Offset: 0x000075D0
		public static GamePresenceRank Deserialize(Stream stream, GamePresenceRank instance, long limit)
		{
			if (instance.Values == null)
			{
				instance.Values = new List<GamePresenceRankData>();
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
					if (num != 18)
					{
						if (num != 26)
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
							instance.Values.Add(GamePresenceRankData.DeserializeLengthDelimited(stream));
						}
					}
					else if (instance.WildDeprecated == null)
					{
						instance.WildDeprecated = GamePresenceRankData.DeserializeLengthDelimited(stream);
					}
					else
					{
						GamePresenceRankData.DeserializeLengthDelimited(stream, instance.WildDeprecated);
					}
				}
				else if (instance.StandardDeprecated == null)
				{
					instance.StandardDeprecated = GamePresenceRankData.DeserializeLengthDelimited(stream);
				}
				else
				{
					GamePresenceRankData.DeserializeLengthDelimited(stream, instance.StandardDeprecated);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x000094D0 File Offset: 0x000076D0
		public void Serialize(Stream stream)
		{
			GamePresenceRank.Serialize(stream, this);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x000094DC File Offset: 0x000076DC
		public static void Serialize(Stream stream, GamePresenceRank instance)
		{
			if (instance.HasStandardDeprecated)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.StandardDeprecated.GetSerializedSize());
				GamePresenceRankData.Serialize(stream, instance.StandardDeprecated);
			}
			if (instance.HasWildDeprecated)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.WildDeprecated.GetSerializedSize());
				GamePresenceRankData.Serialize(stream, instance.WildDeprecated);
			}
			if (instance.Values.Count > 0)
			{
				foreach (GamePresenceRankData gamePresenceRankData in instance.Values)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, gamePresenceRankData.GetSerializedSize());
					GamePresenceRankData.Serialize(stream, gamePresenceRankData);
				}
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x000095AC File Offset: 0x000077AC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasStandardDeprecated)
			{
				num += 1U;
				uint serializedSize = this.StandardDeprecated.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasWildDeprecated)
			{
				num += 1U;
				uint serializedSize2 = this.WildDeprecated.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.Values.Count > 0)
			{
				foreach (GamePresenceRankData gamePresenceRankData in this.Values)
				{
					num += 1U;
					uint serializedSize3 = gamePresenceRankData.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			return num;
		}

		// Token: 0x0400008E RID: 142
		public bool HasStandardDeprecated;

		// Token: 0x0400008F RID: 143
		private GamePresenceRankData _StandardDeprecated;

		// Token: 0x04000090 RID: 144
		public bool HasWildDeprecated;

		// Token: 0x04000091 RID: 145
		private GamePresenceRankData _WildDeprecated;

		// Token: 0x04000092 RID: 146
		private List<GamePresenceRankData> _Values = new List<GamePresenceRankData>();
	}
}
