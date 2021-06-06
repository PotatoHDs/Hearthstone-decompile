using System;
using System.IO;

namespace PegasusGame
{
	// Token: 0x020001CA RID: 458
	public class PowerHistoryData : IProtoBuf
	{
		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001D23 RID: 7459 RVA: 0x00066771 File Offset: 0x00064971
		// (set) Token: 0x06001D24 RID: 7460 RVA: 0x00066779 File Offset: 0x00064979
		public PowerHistoryEntity FullEntity
		{
			get
			{
				return this._FullEntity;
			}
			set
			{
				this._FullEntity = value;
				this.HasFullEntity = (value != null);
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06001D25 RID: 7461 RVA: 0x0006678C File Offset: 0x0006498C
		// (set) Token: 0x06001D26 RID: 7462 RVA: 0x00066794 File Offset: 0x00064994
		public PowerHistoryEntity ShowEntity
		{
			get
			{
				return this._ShowEntity;
			}
			set
			{
				this._ShowEntity = value;
				this.HasShowEntity = (value != null);
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001D27 RID: 7463 RVA: 0x000667A7 File Offset: 0x000649A7
		// (set) Token: 0x06001D28 RID: 7464 RVA: 0x000667AF File Offset: 0x000649AF
		public PowerHistoryHide HideEntity
		{
			get
			{
				return this._HideEntity;
			}
			set
			{
				this._HideEntity = value;
				this.HasHideEntity = (value != null);
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06001D29 RID: 7465 RVA: 0x000667C2 File Offset: 0x000649C2
		// (set) Token: 0x06001D2A RID: 7466 RVA: 0x000667CA File Offset: 0x000649CA
		public PowerHistoryTagChange TagChange
		{
			get
			{
				return this._TagChange;
			}
			set
			{
				this._TagChange = value;
				this.HasTagChange = (value != null);
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06001D2B RID: 7467 RVA: 0x000667DD File Offset: 0x000649DD
		// (set) Token: 0x06001D2C RID: 7468 RVA: 0x000667E5 File Offset: 0x000649E5
		public PowerHistoryCreateGame CreateGame
		{
			get
			{
				return this._CreateGame;
			}
			set
			{
				this._CreateGame = value;
				this.HasCreateGame = (value != null);
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06001D2D RID: 7469 RVA: 0x000667F8 File Offset: 0x000649F8
		// (set) Token: 0x06001D2E RID: 7470 RVA: 0x00066800 File Offset: 0x00064A00
		public PowerHistoryStart PowerStart
		{
			get
			{
				return this._PowerStart;
			}
			set
			{
				this._PowerStart = value;
				this.HasPowerStart = (value != null);
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06001D2F RID: 7471 RVA: 0x00066813 File Offset: 0x00064A13
		// (set) Token: 0x06001D30 RID: 7472 RVA: 0x0006681B File Offset: 0x00064A1B
		public PowerHistoryEnd PowerEnd
		{
			get
			{
				return this._PowerEnd;
			}
			set
			{
				this._PowerEnd = value;
				this.HasPowerEnd = (value != null);
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06001D31 RID: 7473 RVA: 0x0006682E File Offset: 0x00064A2E
		// (set) Token: 0x06001D32 RID: 7474 RVA: 0x00066836 File Offset: 0x00064A36
		public PowerHistoryMetaData MetaData
		{
			get
			{
				return this._MetaData;
			}
			set
			{
				this._MetaData = value;
				this.HasMetaData = (value != null);
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001D33 RID: 7475 RVA: 0x00066849 File Offset: 0x00064A49
		// (set) Token: 0x06001D34 RID: 7476 RVA: 0x00066851 File Offset: 0x00064A51
		public PowerHistoryEntity ChangeEntity
		{
			get
			{
				return this._ChangeEntity;
			}
			set
			{
				this._ChangeEntity = value;
				this.HasChangeEntity = (value != null);
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001D35 RID: 7477 RVA: 0x00066864 File Offset: 0x00064A64
		// (set) Token: 0x06001D36 RID: 7478 RVA: 0x0006686C File Offset: 0x00064A6C
		public PowerHistoryResetGame ResetGame
		{
			get
			{
				return this._ResetGame;
			}
			set
			{
				this._ResetGame = value;
				this.HasResetGame = (value != null);
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001D37 RID: 7479 RVA: 0x0006687F File Offset: 0x00064A7F
		// (set) Token: 0x06001D38 RID: 7480 RVA: 0x00066887 File Offset: 0x00064A87
		public PowerHistorySubSpellStart SubSpellStart
		{
			get
			{
				return this._SubSpellStart;
			}
			set
			{
				this._SubSpellStart = value;
				this.HasSubSpellStart = (value != null);
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001D39 RID: 7481 RVA: 0x0006689A File Offset: 0x00064A9A
		// (set) Token: 0x06001D3A RID: 7482 RVA: 0x000668A2 File Offset: 0x00064AA2
		public PowerHistorySubSpellEnd SubSpellEnd
		{
			get
			{
				return this._SubSpellEnd;
			}
			set
			{
				this._SubSpellEnd = value;
				this.HasSubSpellEnd = (value != null);
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06001D3B RID: 7483 RVA: 0x000668B5 File Offset: 0x00064AB5
		// (set) Token: 0x06001D3C RID: 7484 RVA: 0x000668BD File Offset: 0x00064ABD
		public PowerHistoryVoTask VoSpell
		{
			get
			{
				return this._VoSpell;
			}
			set
			{
				this._VoSpell = value;
				this.HasVoSpell = (value != null);
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06001D3D RID: 7485 RVA: 0x000668D0 File Offset: 0x00064AD0
		// (set) Token: 0x06001D3E RID: 7486 RVA: 0x000668D8 File Offset: 0x00064AD8
		public PowerHistoryCachedTagForDormantChange CachedTagForDormantChange
		{
			get
			{
				return this._CachedTagForDormantChange;
			}
			set
			{
				this._CachedTagForDormantChange = value;
				this.HasCachedTagForDormantChange = (value != null);
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06001D3F RID: 7487 RVA: 0x000668EB File Offset: 0x00064AEB
		// (set) Token: 0x06001D40 RID: 7488 RVA: 0x000668F3 File Offset: 0x00064AF3
		public PowerHistoryShuffleDeck ShuffleDeck
		{
			get
			{
				return this._ShuffleDeck;
			}
			set
			{
				this._ShuffleDeck = value;
				this.HasShuffleDeck = (value != null);
			}
		}

		// Token: 0x06001D41 RID: 7489 RVA: 0x00066908 File Offset: 0x00064B08
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasFullEntity)
			{
				num ^= this.FullEntity.GetHashCode();
			}
			if (this.HasShowEntity)
			{
				num ^= this.ShowEntity.GetHashCode();
			}
			if (this.HasHideEntity)
			{
				num ^= this.HideEntity.GetHashCode();
			}
			if (this.HasTagChange)
			{
				num ^= this.TagChange.GetHashCode();
			}
			if (this.HasCreateGame)
			{
				num ^= this.CreateGame.GetHashCode();
			}
			if (this.HasPowerStart)
			{
				num ^= this.PowerStart.GetHashCode();
			}
			if (this.HasPowerEnd)
			{
				num ^= this.PowerEnd.GetHashCode();
			}
			if (this.HasMetaData)
			{
				num ^= this.MetaData.GetHashCode();
			}
			if (this.HasChangeEntity)
			{
				num ^= this.ChangeEntity.GetHashCode();
			}
			if (this.HasResetGame)
			{
				num ^= this.ResetGame.GetHashCode();
			}
			if (this.HasSubSpellStart)
			{
				num ^= this.SubSpellStart.GetHashCode();
			}
			if (this.HasSubSpellEnd)
			{
				num ^= this.SubSpellEnd.GetHashCode();
			}
			if (this.HasVoSpell)
			{
				num ^= this.VoSpell.GetHashCode();
			}
			if (this.HasCachedTagForDormantChange)
			{
				num ^= this.CachedTagForDormantChange.GetHashCode();
			}
			if (this.HasShuffleDeck)
			{
				num ^= this.ShuffleDeck.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001D42 RID: 7490 RVA: 0x00066A6C File Offset: 0x00064C6C
		public override bool Equals(object obj)
		{
			PowerHistoryData powerHistoryData = obj as PowerHistoryData;
			return powerHistoryData != null && this.HasFullEntity == powerHistoryData.HasFullEntity && (!this.HasFullEntity || this.FullEntity.Equals(powerHistoryData.FullEntity)) && this.HasShowEntity == powerHistoryData.HasShowEntity && (!this.HasShowEntity || this.ShowEntity.Equals(powerHistoryData.ShowEntity)) && this.HasHideEntity == powerHistoryData.HasHideEntity && (!this.HasHideEntity || this.HideEntity.Equals(powerHistoryData.HideEntity)) && this.HasTagChange == powerHistoryData.HasTagChange && (!this.HasTagChange || this.TagChange.Equals(powerHistoryData.TagChange)) && this.HasCreateGame == powerHistoryData.HasCreateGame && (!this.HasCreateGame || this.CreateGame.Equals(powerHistoryData.CreateGame)) && this.HasPowerStart == powerHistoryData.HasPowerStart && (!this.HasPowerStart || this.PowerStart.Equals(powerHistoryData.PowerStart)) && this.HasPowerEnd == powerHistoryData.HasPowerEnd && (!this.HasPowerEnd || this.PowerEnd.Equals(powerHistoryData.PowerEnd)) && this.HasMetaData == powerHistoryData.HasMetaData && (!this.HasMetaData || this.MetaData.Equals(powerHistoryData.MetaData)) && this.HasChangeEntity == powerHistoryData.HasChangeEntity && (!this.HasChangeEntity || this.ChangeEntity.Equals(powerHistoryData.ChangeEntity)) && this.HasResetGame == powerHistoryData.HasResetGame && (!this.HasResetGame || this.ResetGame.Equals(powerHistoryData.ResetGame)) && this.HasSubSpellStart == powerHistoryData.HasSubSpellStart && (!this.HasSubSpellStart || this.SubSpellStart.Equals(powerHistoryData.SubSpellStart)) && this.HasSubSpellEnd == powerHistoryData.HasSubSpellEnd && (!this.HasSubSpellEnd || this.SubSpellEnd.Equals(powerHistoryData.SubSpellEnd)) && this.HasVoSpell == powerHistoryData.HasVoSpell && (!this.HasVoSpell || this.VoSpell.Equals(powerHistoryData.VoSpell)) && this.HasCachedTagForDormantChange == powerHistoryData.HasCachedTagForDormantChange && (!this.HasCachedTagForDormantChange || this.CachedTagForDormantChange.Equals(powerHistoryData.CachedTagForDormantChange)) && this.HasShuffleDeck == powerHistoryData.HasShuffleDeck && (!this.HasShuffleDeck || this.ShuffleDeck.Equals(powerHistoryData.ShuffleDeck));
		}

		// Token: 0x06001D43 RID: 7491 RVA: 0x00066D0B File Offset: 0x00064F0B
		public void Deserialize(Stream stream)
		{
			PowerHistoryData.Deserialize(stream, this);
		}

		// Token: 0x06001D44 RID: 7492 RVA: 0x00066D15 File Offset: 0x00064F15
		public static PowerHistoryData Deserialize(Stream stream, PowerHistoryData instance)
		{
			return PowerHistoryData.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001D45 RID: 7493 RVA: 0x00066D20 File Offset: 0x00064F20
		public static PowerHistoryData DeserializeLengthDelimited(Stream stream)
		{
			PowerHistoryData powerHistoryData = new PowerHistoryData();
			PowerHistoryData.DeserializeLengthDelimited(stream, powerHistoryData);
			return powerHistoryData;
		}

		// Token: 0x06001D46 RID: 7494 RVA: 0x00066D3C File Offset: 0x00064F3C
		public static PowerHistoryData DeserializeLengthDelimited(Stream stream, PowerHistoryData instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PowerHistoryData.Deserialize(stream, instance, num);
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x00066D64 File Offset: 0x00064F64
		public static PowerHistoryData Deserialize(Stream stream, PowerHistoryData instance, long limit)
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
					if (num <= 58)
					{
						if (num <= 26)
						{
							if (num != 10)
							{
								if (num != 18)
								{
									if (num == 26)
									{
										if (instance.HideEntity == null)
										{
											instance.HideEntity = PowerHistoryHide.DeserializeLengthDelimited(stream);
											continue;
										}
										PowerHistoryHide.DeserializeLengthDelimited(stream, instance.HideEntity);
										continue;
									}
								}
								else
								{
									if (instance.ShowEntity == null)
									{
										instance.ShowEntity = PowerHistoryEntity.DeserializeLengthDelimited(stream);
										continue;
									}
									PowerHistoryEntity.DeserializeLengthDelimited(stream, instance.ShowEntity);
									continue;
								}
							}
							else
							{
								if (instance.FullEntity == null)
								{
									instance.FullEntity = PowerHistoryEntity.DeserializeLengthDelimited(stream);
									continue;
								}
								PowerHistoryEntity.DeserializeLengthDelimited(stream, instance.FullEntity);
								continue;
							}
						}
						else if (num <= 42)
						{
							if (num != 34)
							{
								if (num == 42)
								{
									if (instance.CreateGame == null)
									{
										instance.CreateGame = PowerHistoryCreateGame.DeserializeLengthDelimited(stream);
										continue;
									}
									PowerHistoryCreateGame.DeserializeLengthDelimited(stream, instance.CreateGame);
									continue;
								}
							}
							else
							{
								if (instance.TagChange == null)
								{
									instance.TagChange = PowerHistoryTagChange.DeserializeLengthDelimited(stream);
									continue;
								}
								PowerHistoryTagChange.DeserializeLengthDelimited(stream, instance.TagChange);
								continue;
							}
						}
						else if (num != 50)
						{
							if (num == 58)
							{
								if (instance.PowerEnd == null)
								{
									instance.PowerEnd = PowerHistoryEnd.DeserializeLengthDelimited(stream);
									continue;
								}
								PowerHistoryEnd.DeserializeLengthDelimited(stream, instance.PowerEnd);
								continue;
							}
						}
						else
						{
							if (instance.PowerStart == null)
							{
								instance.PowerStart = PowerHistoryStart.DeserializeLengthDelimited(stream);
								continue;
							}
							PowerHistoryStart.DeserializeLengthDelimited(stream, instance.PowerStart);
							continue;
						}
					}
					else if (num <= 90)
					{
						if (num <= 74)
						{
							if (num != 66)
							{
								if (num == 74)
								{
									if (instance.ChangeEntity == null)
									{
										instance.ChangeEntity = PowerHistoryEntity.DeserializeLengthDelimited(stream);
										continue;
									}
									PowerHistoryEntity.DeserializeLengthDelimited(stream, instance.ChangeEntity);
									continue;
								}
							}
							else
							{
								if (instance.MetaData == null)
								{
									instance.MetaData = PowerHistoryMetaData.DeserializeLengthDelimited(stream);
									continue;
								}
								PowerHistoryMetaData.DeserializeLengthDelimited(stream, instance.MetaData);
								continue;
							}
						}
						else if (num != 82)
						{
							if (num == 90)
							{
								if (instance.SubSpellStart == null)
								{
									instance.SubSpellStart = PowerHistorySubSpellStart.DeserializeLengthDelimited(stream);
									continue;
								}
								PowerHistorySubSpellStart.DeserializeLengthDelimited(stream, instance.SubSpellStart);
								continue;
							}
						}
						else
						{
							if (instance.ResetGame == null)
							{
								instance.ResetGame = PowerHistoryResetGame.DeserializeLengthDelimited(stream);
								continue;
							}
							PowerHistoryResetGame.DeserializeLengthDelimited(stream, instance.ResetGame);
							continue;
						}
					}
					else if (num <= 106)
					{
						if (num != 98)
						{
							if (num == 106)
							{
								if (instance.VoSpell == null)
								{
									instance.VoSpell = PowerHistoryVoTask.DeserializeLengthDelimited(stream);
									continue;
								}
								PowerHistoryVoTask.DeserializeLengthDelimited(stream, instance.VoSpell);
								continue;
							}
						}
						else
						{
							if (instance.SubSpellEnd == null)
							{
								instance.SubSpellEnd = PowerHistorySubSpellEnd.DeserializeLengthDelimited(stream);
								continue;
							}
							PowerHistorySubSpellEnd.DeserializeLengthDelimited(stream, instance.SubSpellEnd);
							continue;
						}
					}
					else if (num != 114)
					{
						if (num == 122)
						{
							if (instance.ShuffleDeck == null)
							{
								instance.ShuffleDeck = PowerHistoryShuffleDeck.DeserializeLengthDelimited(stream);
								continue;
							}
							PowerHistoryShuffleDeck.DeserializeLengthDelimited(stream, instance.ShuffleDeck);
							continue;
						}
					}
					else
					{
						if (instance.CachedTagForDormantChange == null)
						{
							instance.CachedTagForDormantChange = PowerHistoryCachedTagForDormantChange.DeserializeLengthDelimited(stream);
							continue;
						}
						PowerHistoryCachedTagForDormantChange.DeserializeLengthDelimited(stream, instance.CachedTagForDormantChange);
						continue;
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

		// Token: 0x06001D48 RID: 7496 RVA: 0x00067118 File Offset: 0x00065318
		public void Serialize(Stream stream)
		{
			PowerHistoryData.Serialize(stream, this);
		}

		// Token: 0x06001D49 RID: 7497 RVA: 0x00067124 File Offset: 0x00065324
		public static void Serialize(Stream stream, PowerHistoryData instance)
		{
			if (instance.HasFullEntity)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.FullEntity.GetSerializedSize());
				PowerHistoryEntity.Serialize(stream, instance.FullEntity);
			}
			if (instance.HasShowEntity)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ShowEntity.GetSerializedSize());
				PowerHistoryEntity.Serialize(stream, instance.ShowEntity);
			}
			if (instance.HasHideEntity)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.HideEntity.GetSerializedSize());
				PowerHistoryHide.Serialize(stream, instance.HideEntity);
			}
			if (instance.HasTagChange)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.TagChange.GetSerializedSize());
				PowerHistoryTagChange.Serialize(stream, instance.TagChange);
			}
			if (instance.HasCreateGame)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.CreateGame.GetSerializedSize());
				PowerHistoryCreateGame.Serialize(stream, instance.CreateGame);
			}
			if (instance.HasPowerStart)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.PowerStart.GetSerializedSize());
				PowerHistoryStart.Serialize(stream, instance.PowerStart);
			}
			if (instance.HasPowerEnd)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteUInt32(stream, instance.PowerEnd.GetSerializedSize());
				PowerHistoryEnd.Serialize(stream, instance.PowerEnd);
			}
			if (instance.HasMetaData)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteUInt32(stream, instance.MetaData.GetSerializedSize());
				PowerHistoryMetaData.Serialize(stream, instance.MetaData);
			}
			if (instance.HasChangeEntity)
			{
				stream.WriteByte(74);
				ProtocolParser.WriteUInt32(stream, instance.ChangeEntity.GetSerializedSize());
				PowerHistoryEntity.Serialize(stream, instance.ChangeEntity);
			}
			if (instance.HasResetGame)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteUInt32(stream, instance.ResetGame.GetSerializedSize());
				PowerHistoryResetGame.Serialize(stream, instance.ResetGame);
			}
			if (instance.HasSubSpellStart)
			{
				stream.WriteByte(90);
				ProtocolParser.WriteUInt32(stream, instance.SubSpellStart.GetSerializedSize());
				PowerHistorySubSpellStart.Serialize(stream, instance.SubSpellStart);
			}
			if (instance.HasSubSpellEnd)
			{
				stream.WriteByte(98);
				ProtocolParser.WriteUInt32(stream, instance.SubSpellEnd.GetSerializedSize());
				PowerHistorySubSpellEnd.Serialize(stream, instance.SubSpellEnd);
			}
			if (instance.HasVoSpell)
			{
				stream.WriteByte(106);
				ProtocolParser.WriteUInt32(stream, instance.VoSpell.GetSerializedSize());
				PowerHistoryVoTask.Serialize(stream, instance.VoSpell);
			}
			if (instance.HasCachedTagForDormantChange)
			{
				stream.WriteByte(114);
				ProtocolParser.WriteUInt32(stream, instance.CachedTagForDormantChange.GetSerializedSize());
				PowerHistoryCachedTagForDormantChange.Serialize(stream, instance.CachedTagForDormantChange);
			}
			if (instance.HasShuffleDeck)
			{
				stream.WriteByte(122);
				ProtocolParser.WriteUInt32(stream, instance.ShuffleDeck.GetSerializedSize());
				PowerHistoryShuffleDeck.Serialize(stream, instance.ShuffleDeck);
			}
		}

		// Token: 0x06001D4A RID: 7498 RVA: 0x000673D4 File Offset: 0x000655D4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasFullEntity)
			{
				num += 1U;
				uint serializedSize = this.FullEntity.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasShowEntity)
			{
				num += 1U;
				uint serializedSize2 = this.ShowEntity.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasHideEntity)
			{
				num += 1U;
				uint serializedSize3 = this.HideEntity.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.HasTagChange)
			{
				num += 1U;
				uint serializedSize4 = this.TagChange.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (this.HasCreateGame)
			{
				num += 1U;
				uint serializedSize5 = this.CreateGame.GetSerializedSize();
				num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
			}
			if (this.HasPowerStart)
			{
				num += 1U;
				uint serializedSize6 = this.PowerStart.GetSerializedSize();
				num += serializedSize6 + ProtocolParser.SizeOfUInt32(serializedSize6);
			}
			if (this.HasPowerEnd)
			{
				num += 1U;
				uint serializedSize7 = this.PowerEnd.GetSerializedSize();
				num += serializedSize7 + ProtocolParser.SizeOfUInt32(serializedSize7);
			}
			if (this.HasMetaData)
			{
				num += 1U;
				uint serializedSize8 = this.MetaData.GetSerializedSize();
				num += serializedSize8 + ProtocolParser.SizeOfUInt32(serializedSize8);
			}
			if (this.HasChangeEntity)
			{
				num += 1U;
				uint serializedSize9 = this.ChangeEntity.GetSerializedSize();
				num += serializedSize9 + ProtocolParser.SizeOfUInt32(serializedSize9);
			}
			if (this.HasResetGame)
			{
				num += 1U;
				uint serializedSize10 = this.ResetGame.GetSerializedSize();
				num += serializedSize10 + ProtocolParser.SizeOfUInt32(serializedSize10);
			}
			if (this.HasSubSpellStart)
			{
				num += 1U;
				uint serializedSize11 = this.SubSpellStart.GetSerializedSize();
				num += serializedSize11 + ProtocolParser.SizeOfUInt32(serializedSize11);
			}
			if (this.HasSubSpellEnd)
			{
				num += 1U;
				uint serializedSize12 = this.SubSpellEnd.GetSerializedSize();
				num += serializedSize12 + ProtocolParser.SizeOfUInt32(serializedSize12);
			}
			if (this.HasVoSpell)
			{
				num += 1U;
				uint serializedSize13 = this.VoSpell.GetSerializedSize();
				num += serializedSize13 + ProtocolParser.SizeOfUInt32(serializedSize13);
			}
			if (this.HasCachedTagForDormantChange)
			{
				num += 1U;
				uint serializedSize14 = this.CachedTagForDormantChange.GetSerializedSize();
				num += serializedSize14 + ProtocolParser.SizeOfUInt32(serializedSize14);
			}
			if (this.HasShuffleDeck)
			{
				num += 1U;
				uint serializedSize15 = this.ShuffleDeck.GetSerializedSize();
				num += serializedSize15 + ProtocolParser.SizeOfUInt32(serializedSize15);
			}
			return num;
		}

		// Token: 0x04000A8E RID: 2702
		public bool HasFullEntity;

		// Token: 0x04000A8F RID: 2703
		private PowerHistoryEntity _FullEntity;

		// Token: 0x04000A90 RID: 2704
		public bool HasShowEntity;

		// Token: 0x04000A91 RID: 2705
		private PowerHistoryEntity _ShowEntity;

		// Token: 0x04000A92 RID: 2706
		public bool HasHideEntity;

		// Token: 0x04000A93 RID: 2707
		private PowerHistoryHide _HideEntity;

		// Token: 0x04000A94 RID: 2708
		public bool HasTagChange;

		// Token: 0x04000A95 RID: 2709
		private PowerHistoryTagChange _TagChange;

		// Token: 0x04000A96 RID: 2710
		public bool HasCreateGame;

		// Token: 0x04000A97 RID: 2711
		private PowerHistoryCreateGame _CreateGame;

		// Token: 0x04000A98 RID: 2712
		public bool HasPowerStart;

		// Token: 0x04000A99 RID: 2713
		private PowerHistoryStart _PowerStart;

		// Token: 0x04000A9A RID: 2714
		public bool HasPowerEnd;

		// Token: 0x04000A9B RID: 2715
		private PowerHistoryEnd _PowerEnd;

		// Token: 0x04000A9C RID: 2716
		public bool HasMetaData;

		// Token: 0x04000A9D RID: 2717
		private PowerHistoryMetaData _MetaData;

		// Token: 0x04000A9E RID: 2718
		public bool HasChangeEntity;

		// Token: 0x04000A9F RID: 2719
		private PowerHistoryEntity _ChangeEntity;

		// Token: 0x04000AA0 RID: 2720
		public bool HasResetGame;

		// Token: 0x04000AA1 RID: 2721
		private PowerHistoryResetGame _ResetGame;

		// Token: 0x04000AA2 RID: 2722
		public bool HasSubSpellStart;

		// Token: 0x04000AA3 RID: 2723
		private PowerHistorySubSpellStart _SubSpellStart;

		// Token: 0x04000AA4 RID: 2724
		public bool HasSubSpellEnd;

		// Token: 0x04000AA5 RID: 2725
		private PowerHistorySubSpellEnd _SubSpellEnd;

		// Token: 0x04000AA6 RID: 2726
		public bool HasVoSpell;

		// Token: 0x04000AA7 RID: 2727
		private PowerHistoryVoTask _VoSpell;

		// Token: 0x04000AA8 RID: 2728
		public bool HasCachedTagForDormantChange;

		// Token: 0x04000AA9 RID: 2729
		private PowerHistoryCachedTagForDormantChange _CachedTagForDormantChange;

		// Token: 0x04000AAA RID: 2730
		public bool HasShuffleDeck;

		// Token: 0x04000AAB RID: 2731
		private PowerHistoryShuffleDeck _ShuffleDeck;
	}
}
