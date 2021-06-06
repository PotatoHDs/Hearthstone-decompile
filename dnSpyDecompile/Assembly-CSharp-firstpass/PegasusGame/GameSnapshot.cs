using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PegasusShared;

namespace PegasusGame
{
	// Token: 0x020001A6 RID: 422
	public class GameSnapshot : IProtoBuf
	{
		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001A81 RID: 6785 RVA: 0x0005D78F File Offset: 0x0005B98F
		// (set) Token: 0x06001A82 RID: 6786 RVA: 0x0005D797 File Offset: 0x0005B997
		public uint InitialSeed { get; set; }

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001A83 RID: 6787 RVA: 0x0005D7A0 File Offset: 0x0005B9A0
		// (set) Token: 0x06001A84 RID: 6788 RVA: 0x0005D7A8 File Offset: 0x0005B9A8
		public int ScenarioId { get; set; }

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001A85 RID: 6789 RVA: 0x0005D7B1 File Offset: 0x0005B9B1
		// (set) Token: 0x06001A86 RID: 6790 RVA: 0x0005D7B9 File Offset: 0x0005B9B9
		public int BoardId { get; set; }

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001A87 RID: 6791 RVA: 0x0005D7C2 File Offset: 0x0005B9C2
		// (set) Token: 0x06001A88 RID: 6792 RVA: 0x0005D7CA File Offset: 0x0005B9CA
		public string Game { get; set; }

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001A89 RID: 6793 RVA: 0x0005D7D3 File Offset: 0x0005B9D3
		// (set) Token: 0x06001A8A RID: 6794 RVA: 0x0005D7DB File Offset: 0x0005B9DB
		public bool IsExpertAi { get; set; }

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001A8B RID: 6795 RVA: 0x0005D7E4 File Offset: 0x0005B9E4
		// (set) Token: 0x06001A8C RID: 6796 RVA: 0x0005D7EC File Offset: 0x0005B9EC
		public GameType GameType { get; set; }

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001A8D RID: 6797 RVA: 0x0005D7F5 File Offset: 0x0005B9F5
		// (set) Token: 0x06001A8E RID: 6798 RVA: 0x0005D7FD File Offset: 0x0005B9FD
		public FormatType FormatType { get; set; }

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001A8F RID: 6799 RVA: 0x0005D806 File Offset: 0x0005BA06
		// (set) Token: 0x06001A90 RID: 6800 RVA: 0x0005D80E File Offset: 0x0005BA0E
		public int Players { get; set; }

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001A91 RID: 6801 RVA: 0x0005D817 File Offset: 0x0005BA17
		// (set) Token: 0x06001A92 RID: 6802 RVA: 0x0005D81F File Offset: 0x0005BA1F
		public List<string> PlayerEntity
		{
			get
			{
				return this._PlayerEntity;
			}
			set
			{
				this._PlayerEntity = value;
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001A93 RID: 6803 RVA: 0x0005D828 File Offset: 0x0005BA28
		// (set) Token: 0x06001A94 RID: 6804 RVA: 0x0005D830 File Offset: 0x0005BA30
		public string HeroGuid1 { get; set; }

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001A95 RID: 6805 RVA: 0x0005D839 File Offset: 0x0005BA39
		// (set) Token: 0x06001A96 RID: 6806 RVA: 0x0005D841 File Offset: 0x0005BA41
		public int HeroPremium1 { get; set; }

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06001A97 RID: 6807 RVA: 0x0005D84A File Offset: 0x0005BA4A
		// (set) Token: 0x06001A98 RID: 6808 RVA: 0x0005D852 File Offset: 0x0005BA52
		public List<string> DeckGuid1
		{
			get
			{
				return this._DeckGuid1;
			}
			set
			{
				this._DeckGuid1 = value;
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001A99 RID: 6809 RVA: 0x0005D85B File Offset: 0x0005BA5B
		// (set) Token: 0x06001A9A RID: 6810 RVA: 0x0005D863 File Offset: 0x0005BA63
		public List<int> DeckPremium1
		{
			get
			{
				return this._DeckPremium1;
			}
			set
			{
				this._DeckPremium1 = value;
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001A9B RID: 6811 RVA: 0x0005D86C File Offset: 0x0005BA6C
		// (set) Token: 0x06001A9C RID: 6812 RVA: 0x0005D874 File Offset: 0x0005BA74
		public int CardBack1 { get; set; }

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001A9D RID: 6813 RVA: 0x0005D87D File Offset: 0x0005BA7D
		// (set) Token: 0x06001A9E RID: 6814 RVA: 0x0005D885 File Offset: 0x0005BA85
		public string HeroGuid2 { get; set; }

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001A9F RID: 6815 RVA: 0x0005D88E File Offset: 0x0005BA8E
		// (set) Token: 0x06001AA0 RID: 6816 RVA: 0x0005D896 File Offset: 0x0005BA96
		public int HeroPremium2 { get; set; }

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001AA1 RID: 6817 RVA: 0x0005D89F File Offset: 0x0005BA9F
		// (set) Token: 0x06001AA2 RID: 6818 RVA: 0x0005D8A7 File Offset: 0x0005BAA7
		public List<string> DeckGuid2
		{
			get
			{
				return this._DeckGuid2;
			}
			set
			{
				this._DeckGuid2 = value;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001AA3 RID: 6819 RVA: 0x0005D8B0 File Offset: 0x0005BAB0
		// (set) Token: 0x06001AA4 RID: 6820 RVA: 0x0005D8B8 File Offset: 0x0005BAB8
		public List<int> DeckPremium2
		{
			get
			{
				return this._DeckPremium2;
			}
			set
			{
				this._DeckPremium2 = value;
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06001AA5 RID: 6821 RVA: 0x0005D8C1 File Offset: 0x0005BAC1
		// (set) Token: 0x06001AA6 RID: 6822 RVA: 0x0005D8C9 File Offset: 0x0005BAC9
		public int CardBack2 { get; set; }

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06001AA7 RID: 6823 RVA: 0x0005D8D2 File Offset: 0x0005BAD2
		// (set) Token: 0x06001AA8 RID: 6824 RVA: 0x0005D8DA File Offset: 0x0005BADA
		public List<PendingEvent> Events
		{
			get
			{
				return this._Events;
			}
			set
			{
				this._Events = value;
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001AA9 RID: 6825 RVA: 0x0005D8E3 File Offset: 0x0005BAE3
		// (set) Token: 0x06001AAA RID: 6826 RVA: 0x0005D8EB File Offset: 0x0005BAEB
		public ulong GameHash
		{
			get
			{
				return this._GameHash;
			}
			set
			{
				this._GameHash = value;
				this.HasGameHash = true;
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001AAB RID: 6827 RVA: 0x0005D8FB File Offset: 0x0005BAFB
		// (set) Token: 0x06001AAC RID: 6828 RVA: 0x0005D903 File Offset: 0x0005BB03
		public ulong EventTimingHash
		{
			get
			{
				return this._EventTimingHash;
			}
			set
			{
				this._EventTimingHash = value;
				this.HasEventTimingHash = true;
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001AAD RID: 6829 RVA: 0x0005D913 File Offset: 0x0005BB13
		// (set) Token: 0x06001AAE RID: 6830 RVA: 0x0005D91B File Offset: 0x0005BB1B
		public long OriginalGameId
		{
			get
			{
				return this._OriginalGameId;
			}
			set
			{
				this._OriginalGameId = value;
				this.HasOriginalGameId = true;
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001AAF RID: 6831 RVA: 0x0005D92B File Offset: 0x0005BB2B
		// (set) Token: 0x06001AB0 RID: 6832 RVA: 0x0005D933 File Offset: 0x0005BB33
		public long GameStartTime
		{
			get
			{
				return this._GameStartTime;
			}
			set
			{
				this._GameStartTime = value;
				this.HasGameStartTime = true;
			}
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x0005D944 File Offset: 0x0005BB44
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.InitialSeed.GetHashCode();
			num ^= this.ScenarioId.GetHashCode();
			num ^= this.BoardId.GetHashCode();
			num ^= this.Game.GetHashCode();
			num ^= this.IsExpertAi.GetHashCode();
			num ^= this.GameType.GetHashCode();
			num ^= this.FormatType.GetHashCode();
			num ^= this.Players.GetHashCode();
			foreach (string text in this.PlayerEntity)
			{
				num ^= text.GetHashCode();
			}
			num ^= this.HeroGuid1.GetHashCode();
			num ^= this.HeroPremium1.GetHashCode();
			foreach (string text2 in this.DeckGuid1)
			{
				num ^= text2.GetHashCode();
			}
			foreach (int num2 in this.DeckPremium1)
			{
				num ^= num2.GetHashCode();
			}
			num ^= this.CardBack1.GetHashCode();
			num ^= this.HeroGuid2.GetHashCode();
			num ^= this.HeroPremium2.GetHashCode();
			foreach (string text3 in this.DeckGuid2)
			{
				num ^= text3.GetHashCode();
			}
			foreach (int num3 in this.DeckPremium2)
			{
				num ^= num3.GetHashCode();
			}
			num ^= this.CardBack2.GetHashCode();
			foreach (PendingEvent pendingEvent in this.Events)
			{
				num ^= pendingEvent.GetHashCode();
			}
			if (this.HasGameHash)
			{
				num ^= this.GameHash.GetHashCode();
			}
			if (this.HasEventTimingHash)
			{
				num ^= this.EventTimingHash.GetHashCode();
			}
			if (this.HasOriginalGameId)
			{
				num ^= this.OriginalGameId.GetHashCode();
			}
			if (this.HasGameStartTime)
			{
				num ^= this.GameStartTime.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x0005DC68 File Offset: 0x0005BE68
		public override bool Equals(object obj)
		{
			GameSnapshot gameSnapshot = obj as GameSnapshot;
			if (gameSnapshot == null)
			{
				return false;
			}
			if (!this.InitialSeed.Equals(gameSnapshot.InitialSeed))
			{
				return false;
			}
			if (!this.ScenarioId.Equals(gameSnapshot.ScenarioId))
			{
				return false;
			}
			if (!this.BoardId.Equals(gameSnapshot.BoardId))
			{
				return false;
			}
			if (!this.Game.Equals(gameSnapshot.Game))
			{
				return false;
			}
			if (!this.IsExpertAi.Equals(gameSnapshot.IsExpertAi))
			{
				return false;
			}
			if (!this.GameType.Equals(gameSnapshot.GameType))
			{
				return false;
			}
			if (!this.FormatType.Equals(gameSnapshot.FormatType))
			{
				return false;
			}
			if (!this.Players.Equals(gameSnapshot.Players))
			{
				return false;
			}
			if (this.PlayerEntity.Count != gameSnapshot.PlayerEntity.Count)
			{
				return false;
			}
			for (int i = 0; i < this.PlayerEntity.Count; i++)
			{
				if (!this.PlayerEntity[i].Equals(gameSnapshot.PlayerEntity[i]))
				{
					return false;
				}
			}
			if (!this.HeroGuid1.Equals(gameSnapshot.HeroGuid1))
			{
				return false;
			}
			if (!this.HeroPremium1.Equals(gameSnapshot.HeroPremium1))
			{
				return false;
			}
			if (this.DeckGuid1.Count != gameSnapshot.DeckGuid1.Count)
			{
				return false;
			}
			for (int j = 0; j < this.DeckGuid1.Count; j++)
			{
				if (!this.DeckGuid1[j].Equals(gameSnapshot.DeckGuid1[j]))
				{
					return false;
				}
			}
			if (this.DeckPremium1.Count != gameSnapshot.DeckPremium1.Count)
			{
				return false;
			}
			for (int k = 0; k < this.DeckPremium1.Count; k++)
			{
				if (!this.DeckPremium1[k].Equals(gameSnapshot.DeckPremium1[k]))
				{
					return false;
				}
			}
			if (!this.CardBack1.Equals(gameSnapshot.CardBack1))
			{
				return false;
			}
			if (!this.HeroGuid2.Equals(gameSnapshot.HeroGuid2))
			{
				return false;
			}
			if (!this.HeroPremium2.Equals(gameSnapshot.HeroPremium2))
			{
				return false;
			}
			if (this.DeckGuid2.Count != gameSnapshot.DeckGuid2.Count)
			{
				return false;
			}
			for (int l = 0; l < this.DeckGuid2.Count; l++)
			{
				if (!this.DeckGuid2[l].Equals(gameSnapshot.DeckGuid2[l]))
				{
					return false;
				}
			}
			if (this.DeckPremium2.Count != gameSnapshot.DeckPremium2.Count)
			{
				return false;
			}
			for (int m = 0; m < this.DeckPremium2.Count; m++)
			{
				if (!this.DeckPremium2[m].Equals(gameSnapshot.DeckPremium2[m]))
				{
					return false;
				}
			}
			if (!this.CardBack2.Equals(gameSnapshot.CardBack2))
			{
				return false;
			}
			if (this.Events.Count != gameSnapshot.Events.Count)
			{
				return false;
			}
			for (int n = 0; n < this.Events.Count; n++)
			{
				if (!this.Events[n].Equals(gameSnapshot.Events[n]))
				{
					return false;
				}
			}
			return this.HasGameHash == gameSnapshot.HasGameHash && (!this.HasGameHash || this.GameHash.Equals(gameSnapshot.GameHash)) && this.HasEventTimingHash == gameSnapshot.HasEventTimingHash && (!this.HasEventTimingHash || this.EventTimingHash.Equals(gameSnapshot.EventTimingHash)) && this.HasOriginalGameId == gameSnapshot.HasOriginalGameId && (!this.HasOriginalGameId || this.OriginalGameId.Equals(gameSnapshot.OriginalGameId)) && this.HasGameStartTime == gameSnapshot.HasGameStartTime && (!this.HasGameStartTime || this.GameStartTime.Equals(gameSnapshot.GameStartTime));
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x0005E0AD File Offset: 0x0005C2AD
		public void Deserialize(Stream stream)
		{
			GameSnapshot.Deserialize(stream, this);
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x0005E0B7 File Offset: 0x0005C2B7
		public static GameSnapshot Deserialize(Stream stream, GameSnapshot instance)
		{
			return GameSnapshot.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x0005E0C4 File Offset: 0x0005C2C4
		public static GameSnapshot DeserializeLengthDelimited(Stream stream)
		{
			GameSnapshot gameSnapshot = new GameSnapshot();
			GameSnapshot.DeserializeLengthDelimited(stream, gameSnapshot);
			return gameSnapshot;
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x0005E0E0 File Offset: 0x0005C2E0
		public static GameSnapshot DeserializeLengthDelimited(Stream stream, GameSnapshot instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameSnapshot.Deserialize(stream, instance, num);
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x0005E108 File Offset: 0x0005C308
		public static GameSnapshot Deserialize(Stream stream, GameSnapshot instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.PlayerEntity == null)
			{
				instance.PlayerEntity = new List<string>();
			}
			if (instance.DeckGuid1 == null)
			{
				instance.DeckGuid1 = new List<string>();
			}
			if (instance.DeckPremium1 == null)
			{
				instance.DeckPremium1 = new List<int>();
			}
			if (instance.DeckGuid2 == null)
			{
				instance.DeckGuid2 = new List<string>();
			}
			if (instance.DeckPremium2 == null)
			{
				instance.DeckPremium2 = new List<int>();
			}
			if (instance.Events == null)
			{
				instance.Events = new List<PendingEvent>();
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
				else
				{
					if (num <= 48)
					{
						if (num <= 24)
						{
							if (num == 13)
							{
								instance.InitialSeed = binaryReader.ReadUInt32();
								continue;
							}
							if (num == 16)
							{
								instance.ScenarioId = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 24)
							{
								instance.BoardId = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else
						{
							if (num == 34)
							{
								instance.Game = ProtocolParser.ReadString(stream);
								continue;
							}
							if (num == 40)
							{
								instance.IsExpertAi = ProtocolParser.ReadBool(stream);
								continue;
							}
							if (num == 48)
							{
								instance.GameType = (GameType)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
					}
					else if (num <= 82)
					{
						if (num == 64)
						{
							instance.FormatType = (FormatType)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 72)
						{
							instance.Players = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 82)
						{
							instance.PlayerEntity.Add(ProtocolParser.ReadString(stream));
							continue;
						}
					}
					else if (num <= 104)
					{
						if (num == 98)
						{
							instance.HeroGuid1 = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 104)
						{
							instance.HeroPremium1 = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 114)
						{
							instance.DeckGuid1.Add(ProtocolParser.ReadString(stream));
							continue;
						}
						if (num == 120)
						{
							instance.DeckPremium1.Add((int)ProtocolParser.ReadUInt64(stream));
							continue;
						}
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					uint field = key.Field;
					if (field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					switch (field)
					{
					case 16U:
						if (key.WireType == Wire.Varint)
						{
							instance.CardBack1 = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 17U:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.HeroGuid2 = ProtocolParser.ReadString(stream);
						}
						break;
					case 18U:
						if (key.WireType == Wire.Varint)
						{
							instance.HeroPremium2 = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 19U:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.DeckGuid2.Add(ProtocolParser.ReadString(stream));
						}
						break;
					case 20U:
						if (key.WireType == Wire.Varint)
						{
							instance.DeckPremium2.Add((int)ProtocolParser.ReadUInt64(stream));
						}
						break;
					case 21U:
						if (key.WireType == Wire.Varint)
						{
							instance.CardBack2 = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 22U:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.Events.Add(PendingEvent.DeserializeLengthDelimited(stream));
						}
						break;
					case 23U:
						if (key.WireType == Wire.Varint)
						{
							instance.GameHash = ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 24U:
						if (key.WireType == Wire.Varint)
						{
							instance.EventTimingHash = ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 25U:
						if (key.WireType == Wire.Varint)
						{
							instance.OriginalGameId = (long)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 26U:
						if (key.WireType == Wire.Varint)
						{
							instance.GameStartTime = (long)ProtocolParser.ReadUInt64(stream);
						}
						break;
					default:
						ProtocolParser.SkipKey(stream, key);
						break;
					}
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x0005E503 File Offset: 0x0005C703
		public void Serialize(Stream stream)
		{
			GameSnapshot.Serialize(stream, this);
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x0005E50C File Offset: 0x0005C70C
		public static void Serialize(Stream stream, GameSnapshot instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(13);
			binaryWriter.Write(instance.InitialSeed);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ScenarioId));
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.BoardId));
			if (instance.Game == null)
			{
				throw new ArgumentNullException("Game", "Required by proto specification.");
			}
			stream.WriteByte(34);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Game));
			stream.WriteByte(40);
			ProtocolParser.WriteBool(stream, instance.IsExpertAi);
			stream.WriteByte(48);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.GameType));
			stream.WriteByte(64);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.FormatType));
			stream.WriteByte(72);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Players));
			if (instance.PlayerEntity.Count > 0)
			{
				foreach (string s in instance.PlayerEntity)
				{
					stream.WriteByte(82);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(s));
				}
			}
			if (instance.HeroGuid1 == null)
			{
				throw new ArgumentNullException("HeroGuid1", "Required by proto specification.");
			}
			stream.WriteByte(98);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.HeroGuid1));
			stream.WriteByte(104);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.HeroPremium1));
			if (instance.DeckGuid1.Count > 0)
			{
				foreach (string s2 in instance.DeckGuid1)
				{
					stream.WriteByte(114);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(s2));
				}
			}
			if (instance.DeckPremium1.Count > 0)
			{
				foreach (int num in instance.DeckPremium1)
				{
					stream.WriteByte(120);
					ProtocolParser.WriteUInt64(stream, (ulong)((long)num));
				}
			}
			stream.WriteByte(128);
			stream.WriteByte(1);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CardBack1));
			if (instance.HeroGuid2 == null)
			{
				throw new ArgumentNullException("HeroGuid2", "Required by proto specification.");
			}
			stream.WriteByte(138);
			stream.WriteByte(1);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.HeroGuid2));
			stream.WriteByte(144);
			stream.WriteByte(1);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.HeroPremium2));
			if (instance.DeckGuid2.Count > 0)
			{
				foreach (string s3 in instance.DeckGuid2)
				{
					stream.WriteByte(154);
					stream.WriteByte(1);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(s3));
				}
			}
			if (instance.DeckPremium2.Count > 0)
			{
				foreach (int num2 in instance.DeckPremium2)
				{
					stream.WriteByte(160);
					stream.WriteByte(1);
					ProtocolParser.WriteUInt64(stream, (ulong)((long)num2));
				}
			}
			stream.WriteByte(168);
			stream.WriteByte(1);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CardBack2));
			if (instance.Events.Count > 0)
			{
				foreach (PendingEvent pendingEvent in instance.Events)
				{
					stream.WriteByte(178);
					stream.WriteByte(1);
					ProtocolParser.WriteUInt32(stream, pendingEvent.GetSerializedSize());
					PendingEvent.Serialize(stream, pendingEvent);
				}
			}
			if (instance.HasGameHash)
			{
				stream.WriteByte(184);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, instance.GameHash);
			}
			if (instance.HasEventTimingHash)
			{
				stream.WriteByte(192);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, instance.EventTimingHash);
			}
			if (instance.HasOriginalGameId)
			{
				stream.WriteByte(200);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.OriginalGameId);
			}
			if (instance.HasGameStartTime)
			{
				stream.WriteByte(208);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.GameStartTime);
			}
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x0005E9D0 File Offset: 0x0005CBD0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += 4U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ScenarioId));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.BoardId));
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Game);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			num += 1U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.GameType));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.FormatType));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Players));
			if (this.PlayerEntity.Count > 0)
			{
				foreach (string s in this.PlayerEntity)
				{
					num += 1U;
					uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(s);
					num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
				}
			}
			uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.HeroGuid1);
			num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.HeroPremium1));
			if (this.DeckGuid1.Count > 0)
			{
				foreach (string s2 in this.DeckGuid1)
				{
					num += 1U;
					uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(s2);
					num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
				}
			}
			if (this.DeckPremium1.Count > 0)
			{
				foreach (int num2 in this.DeckPremium1)
				{
					num += 1U;
					num += ProtocolParser.SizeOfUInt64((ulong)((long)num2));
				}
			}
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.CardBack1));
			uint byteCount5 = (uint)Encoding.UTF8.GetByteCount(this.HeroGuid2);
			num += ProtocolParser.SizeOfUInt32(byteCount5) + byteCount5;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.HeroPremium2));
			if (this.DeckGuid2.Count > 0)
			{
				foreach (string s3 in this.DeckGuid2)
				{
					num += 2U;
					uint byteCount6 = (uint)Encoding.UTF8.GetByteCount(s3);
					num += ProtocolParser.SizeOfUInt32(byteCount6) + byteCount6;
				}
			}
			if (this.DeckPremium2.Count > 0)
			{
				foreach (int num3 in this.DeckPremium2)
				{
					num += 2U;
					num += ProtocolParser.SizeOfUInt64((ulong)((long)num3));
				}
			}
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.CardBack2));
			if (this.Events.Count > 0)
			{
				foreach (PendingEvent pendingEvent in this.Events)
				{
					num += 2U;
					uint serializedSize = pendingEvent.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasGameHash)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64(this.GameHash);
			}
			if (this.HasEventTimingHash)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64(this.EventTimingHash);
			}
			if (this.HasOriginalGameId)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.OriginalGameId);
			}
			if (this.HasGameStartTime)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.GameStartTime);
			}
			num += 18U;
			return num;
		}

		// Token: 0x040009DC RID: 2524
		private List<string> _PlayerEntity = new List<string>();

		// Token: 0x040009DF RID: 2527
		private List<string> _DeckGuid1 = new List<string>();

		// Token: 0x040009E0 RID: 2528
		private List<int> _DeckPremium1 = new List<int>();

		// Token: 0x040009E4 RID: 2532
		private List<string> _DeckGuid2 = new List<string>();

		// Token: 0x040009E5 RID: 2533
		private List<int> _DeckPremium2 = new List<int>();

		// Token: 0x040009E7 RID: 2535
		private List<PendingEvent> _Events = new List<PendingEvent>();

		// Token: 0x040009E8 RID: 2536
		public bool HasGameHash;

		// Token: 0x040009E9 RID: 2537
		private ulong _GameHash;

		// Token: 0x040009EA RID: 2538
		public bool HasEventTimingHash;

		// Token: 0x040009EB RID: 2539
		private ulong _EventTimingHash;

		// Token: 0x040009EC RID: 2540
		public bool HasOriginalGameId;

		// Token: 0x040009ED RID: 2541
		private long _OriginalGameId;

		// Token: 0x040009EE RID: 2542
		public bool HasGameStartTime;

		// Token: 0x040009EF RID: 2543
		private long _GameStartTime;
	}
}
