using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000FB RID: 251
	public class BattlegroundsPremiumStatusResponse : IProtoBuf
	{
		// Token: 0x17000331 RID: 817
		// (get) Token: 0x060010AB RID: 4267 RVA: 0x0003AC03 File Offset: 0x00038E03
		// (set) Token: 0x060010AC RID: 4268 RVA: 0x0003AC0B File Offset: 0x00038E0B
		public List<BattlegroundSeasonPremiumStatus> SeasonPremiumStatus
		{
			get
			{
				return this._SeasonPremiumStatus;
			}
			set
			{
				this._SeasonPremiumStatus = value;
			}
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x0003AC14 File Offset: 0x00038E14
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (BattlegroundSeasonPremiumStatus battlegroundSeasonPremiumStatus in this.SeasonPremiumStatus)
			{
				num ^= battlegroundSeasonPremiumStatus.GetHashCode();
			}
			return num;
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x0003AC78 File Offset: 0x00038E78
		public override bool Equals(object obj)
		{
			BattlegroundsPremiumStatusResponse battlegroundsPremiumStatusResponse = obj as BattlegroundsPremiumStatusResponse;
			if (battlegroundsPremiumStatusResponse == null)
			{
				return false;
			}
			if (this.SeasonPremiumStatus.Count != battlegroundsPremiumStatusResponse.SeasonPremiumStatus.Count)
			{
				return false;
			}
			for (int i = 0; i < this.SeasonPremiumStatus.Count; i++)
			{
				if (!this.SeasonPremiumStatus[i].Equals(battlegroundsPremiumStatusResponse.SeasonPremiumStatus[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x0003ACE3 File Offset: 0x00038EE3
		public void Deserialize(Stream stream)
		{
			BattlegroundsPremiumStatusResponse.Deserialize(stream, this);
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x0003ACED File Offset: 0x00038EED
		public static BattlegroundsPremiumStatusResponse Deserialize(Stream stream, BattlegroundsPremiumStatusResponse instance)
		{
			return BattlegroundsPremiumStatusResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x0003ACF8 File Offset: 0x00038EF8
		public static BattlegroundsPremiumStatusResponse DeserializeLengthDelimited(Stream stream)
		{
			BattlegroundsPremiumStatusResponse battlegroundsPremiumStatusResponse = new BattlegroundsPremiumStatusResponse();
			BattlegroundsPremiumStatusResponse.DeserializeLengthDelimited(stream, battlegroundsPremiumStatusResponse);
			return battlegroundsPremiumStatusResponse;
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x0003AD14 File Offset: 0x00038F14
		public static BattlegroundsPremiumStatusResponse DeserializeLengthDelimited(Stream stream, BattlegroundsPremiumStatusResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BattlegroundsPremiumStatusResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x0003AD3C File Offset: 0x00038F3C
		public static BattlegroundsPremiumStatusResponse Deserialize(Stream stream, BattlegroundsPremiumStatusResponse instance, long limit)
		{
			if (instance.SeasonPremiumStatus == null)
			{
				instance.SeasonPremiumStatus = new List<BattlegroundSeasonPremiumStatus>();
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
				else if (num == 10)
				{
					instance.SeasonPremiumStatus.Add(BattlegroundSeasonPremiumStatus.DeserializeLengthDelimited(stream));
				}
				else
				{
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

		// Token: 0x060010B4 RID: 4276 RVA: 0x0003ADD4 File Offset: 0x00038FD4
		public void Serialize(Stream stream)
		{
			BattlegroundsPremiumStatusResponse.Serialize(stream, this);
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x0003ADE0 File Offset: 0x00038FE0
		public static void Serialize(Stream stream, BattlegroundsPremiumStatusResponse instance)
		{
			if (instance.SeasonPremiumStatus.Count > 0)
			{
				foreach (BattlegroundSeasonPremiumStatus battlegroundSeasonPremiumStatus in instance.SeasonPremiumStatus)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, battlegroundSeasonPremiumStatus.GetSerializedSize());
					BattlegroundSeasonPremiumStatus.Serialize(stream, battlegroundSeasonPremiumStatus);
				}
			}
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x0003AE58 File Offset: 0x00039058
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.SeasonPremiumStatus.Count > 0)
			{
				foreach (BattlegroundSeasonPremiumStatus battlegroundSeasonPremiumStatus in this.SeasonPremiumStatus)
				{
					num += 1U;
					uint serializedSize = battlegroundSeasonPremiumStatus.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x0400052F RID: 1327
		private List<BattlegroundSeasonPremiumStatus> _SeasonPremiumStatus = new List<BattlegroundSeasonPremiumStatus>();

		// Token: 0x020005FD RID: 1533
		public enum PacketID
		{
			// Token: 0x04002034 RID: 8244
			ID = 375,
			// Token: 0x04002035 RID: 8245
			System = 0
		}
	}
}
