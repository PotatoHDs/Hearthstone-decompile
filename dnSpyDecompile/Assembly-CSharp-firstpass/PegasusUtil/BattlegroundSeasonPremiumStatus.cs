using System;
using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x020000FA RID: 250
	public class BattlegroundSeasonPremiumStatus : IProtoBuf
	{
		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06001098 RID: 4248 RVA: 0x0003A7B9 File Offset: 0x000389B9
		// (set) Token: 0x06001099 RID: 4249 RVA: 0x0003A7C1 File Offset: 0x000389C1
		public uint SeasonID { get; set; }

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x0600109A RID: 4250 RVA: 0x0003A7CA File Offset: 0x000389CA
		// (set) Token: 0x0600109B RID: 4251 RVA: 0x0003A7D2 File Offset: 0x000389D2
		public uint PackType { get; set; }

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x0600109C RID: 4252 RVA: 0x0003A7DB File Offset: 0x000389DB
		// (set) Token: 0x0600109D RID: 4253 RVA: 0x0003A7E3 File Offset: 0x000389E3
		public uint NumPacksOpened { get; set; }

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x0600109E RID: 4254 RVA: 0x0003A7EC File Offset: 0x000389EC
		// (set) Token: 0x0600109F RID: 4255 RVA: 0x0003A7F4 File Offset: 0x000389F4
		public List<BattlegroundSeasonRewardType> PremiumRewardUnlocked
		{
			get
			{
				return this._PremiumRewardUnlocked;
			}
			set
			{
				this._PremiumRewardUnlocked = value;
			}
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x0003A800 File Offset: 0x00038A00
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.SeasonID.GetHashCode();
			num ^= this.PackType.GetHashCode();
			num ^= this.NumPacksOpened.GetHashCode();
			foreach (BattlegroundSeasonRewardType battlegroundSeasonRewardType in this.PremiumRewardUnlocked)
			{
				num ^= battlegroundSeasonRewardType.GetHashCode();
			}
			return num;
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x0003A89C File Offset: 0x00038A9C
		public override bool Equals(object obj)
		{
			BattlegroundSeasonPremiumStatus battlegroundSeasonPremiumStatus = obj as BattlegroundSeasonPremiumStatus;
			if (battlegroundSeasonPremiumStatus == null)
			{
				return false;
			}
			if (!this.SeasonID.Equals(battlegroundSeasonPremiumStatus.SeasonID))
			{
				return false;
			}
			if (!this.PackType.Equals(battlegroundSeasonPremiumStatus.PackType))
			{
				return false;
			}
			if (!this.NumPacksOpened.Equals(battlegroundSeasonPremiumStatus.NumPacksOpened))
			{
				return false;
			}
			if (this.PremiumRewardUnlocked.Count != battlegroundSeasonPremiumStatus.PremiumRewardUnlocked.Count)
			{
				return false;
			}
			for (int i = 0; i < this.PremiumRewardUnlocked.Count; i++)
			{
				if (!this.PremiumRewardUnlocked[i].Equals(battlegroundSeasonPremiumStatus.PremiumRewardUnlocked[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x0003A95D File Offset: 0x00038B5D
		public void Deserialize(Stream stream)
		{
			BattlegroundSeasonPremiumStatus.Deserialize(stream, this);
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x0003A967 File Offset: 0x00038B67
		public static BattlegroundSeasonPremiumStatus Deserialize(Stream stream, BattlegroundSeasonPremiumStatus instance)
		{
			return BattlegroundSeasonPremiumStatus.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x0003A974 File Offset: 0x00038B74
		public static BattlegroundSeasonPremiumStatus DeserializeLengthDelimited(Stream stream)
		{
			BattlegroundSeasonPremiumStatus battlegroundSeasonPremiumStatus = new BattlegroundSeasonPremiumStatus();
			BattlegroundSeasonPremiumStatus.DeserializeLengthDelimited(stream, battlegroundSeasonPremiumStatus);
			return battlegroundSeasonPremiumStatus;
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x0003A990 File Offset: 0x00038B90
		public static BattlegroundSeasonPremiumStatus DeserializeLengthDelimited(Stream stream, BattlegroundSeasonPremiumStatus instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BattlegroundSeasonPremiumStatus.Deserialize(stream, instance, num);
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x0003A9B8 File Offset: 0x00038BB8
		public static BattlegroundSeasonPremiumStatus Deserialize(Stream stream, BattlegroundSeasonPremiumStatus instance, long limit)
		{
			if (instance.PremiumRewardUnlocked == null)
			{
				instance.PremiumRewardUnlocked = new List<BattlegroundSeasonRewardType>();
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
					if (num <= 16)
					{
						if (num == 8)
						{
							instance.SeasonID = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num == 16)
						{
							instance.PackType = ProtocolParser.ReadUInt32(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.NumPacksOpened = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num == 32)
						{
							instance.PremiumRewardUnlocked.Add((BattlegroundSeasonRewardType)ProtocolParser.ReadUInt64(stream));
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

		// Token: 0x060010A7 RID: 4263 RVA: 0x0003AAA1 File Offset: 0x00038CA1
		public void Serialize(Stream stream)
		{
			BattlegroundSeasonPremiumStatus.Serialize(stream, this);
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x0003AAAC File Offset: 0x00038CAC
		public static void Serialize(Stream stream, BattlegroundSeasonPremiumStatus instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.SeasonID);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt32(stream, instance.PackType);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt32(stream, instance.NumPacksOpened);
			if (instance.PremiumRewardUnlocked.Count > 0)
			{
				foreach (BattlegroundSeasonRewardType battlegroundSeasonRewardType in instance.PremiumRewardUnlocked)
				{
					stream.WriteByte(32);
					ProtocolParser.WriteUInt64(stream, (ulong)((long)battlegroundSeasonRewardType));
				}
			}
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x0003AB54 File Offset: 0x00038D54
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt32(this.SeasonID);
			num += ProtocolParser.SizeOfUInt32(this.PackType);
			num += ProtocolParser.SizeOfUInt32(this.NumPacksOpened);
			if (this.PremiumRewardUnlocked.Count > 0)
			{
				foreach (BattlegroundSeasonRewardType battlegroundSeasonRewardType in this.PremiumRewardUnlocked)
				{
					num += 1U;
					num += ProtocolParser.SizeOfUInt64((ulong)((long)battlegroundSeasonRewardType));
				}
			}
			num += 3U;
			return num;
		}

		// Token: 0x0400052E RID: 1326
		private List<BattlegroundSeasonRewardType> _PremiumRewardUnlocked = new List<BattlegroundSeasonRewardType>();
	}
}
