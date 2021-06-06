using System;
using System.IO;
using System.Text;

namespace PegasusGame
{
	// Token: 0x020001D8 RID: 472
	public class UpdateBattlegroundInfo : IProtoBuf
	{
		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001E21 RID: 7713 RVA: 0x00069DFD File Offset: 0x00067FFD
		// (set) Token: 0x06001E22 RID: 7714 RVA: 0x00069E05 File Offset: 0x00068005
		public string BattlegroundDenyList
		{
			get
			{
				return this._BattlegroundDenyList;
			}
			set
			{
				this._BattlegroundDenyList = value;
				this.HasBattlegroundDenyList = (value != null);
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001E23 RID: 7715 RVA: 0x00069E18 File Offset: 0x00068018
		// (set) Token: 0x06001E24 RID: 7716 RVA: 0x00069E20 File Offset: 0x00068020
		public string BattlegroundMinionPool
		{
			get
			{
				return this._BattlegroundMinionPool;
			}
			set
			{
				this._BattlegroundMinionPool = value;
				this.HasBattlegroundMinionPool = (value != null);
			}
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x00069E34 File Offset: 0x00068034
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasBattlegroundDenyList)
			{
				num ^= this.BattlegroundDenyList.GetHashCode();
			}
			if (this.HasBattlegroundMinionPool)
			{
				num ^= this.BattlegroundMinionPool.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001E26 RID: 7718 RVA: 0x00069E7C File Offset: 0x0006807C
		public override bool Equals(object obj)
		{
			UpdateBattlegroundInfo updateBattlegroundInfo = obj as UpdateBattlegroundInfo;
			return updateBattlegroundInfo != null && this.HasBattlegroundDenyList == updateBattlegroundInfo.HasBattlegroundDenyList && (!this.HasBattlegroundDenyList || this.BattlegroundDenyList.Equals(updateBattlegroundInfo.BattlegroundDenyList)) && this.HasBattlegroundMinionPool == updateBattlegroundInfo.HasBattlegroundMinionPool && (!this.HasBattlegroundMinionPool || this.BattlegroundMinionPool.Equals(updateBattlegroundInfo.BattlegroundMinionPool));
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x00069EEC File Offset: 0x000680EC
		public void Deserialize(Stream stream)
		{
			UpdateBattlegroundInfo.Deserialize(stream, this);
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x00069EF6 File Offset: 0x000680F6
		public static UpdateBattlegroundInfo Deserialize(Stream stream, UpdateBattlegroundInfo instance)
		{
			return UpdateBattlegroundInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x00069F04 File Offset: 0x00068104
		public static UpdateBattlegroundInfo DeserializeLengthDelimited(Stream stream)
		{
			UpdateBattlegroundInfo updateBattlegroundInfo = new UpdateBattlegroundInfo();
			UpdateBattlegroundInfo.DeserializeLengthDelimited(stream, updateBattlegroundInfo);
			return updateBattlegroundInfo;
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x00069F20 File Offset: 0x00068120
		public static UpdateBattlegroundInfo DeserializeLengthDelimited(Stream stream, UpdateBattlegroundInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UpdateBattlegroundInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x00069F48 File Offset: 0x00068148
		public static UpdateBattlegroundInfo Deserialize(Stream stream, UpdateBattlegroundInfo instance, long limit)
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
				else if (num != 10)
				{
					if (num != 18)
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
						instance.BattlegroundMinionPool = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.BattlegroundDenyList = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x00069FE0 File Offset: 0x000681E0
		public void Serialize(Stream stream)
		{
			UpdateBattlegroundInfo.Serialize(stream, this);
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x00069FEC File Offset: 0x000681EC
		public static void Serialize(Stream stream, UpdateBattlegroundInfo instance)
		{
			if (instance.HasBattlegroundDenyList)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BattlegroundDenyList));
			}
			if (instance.HasBattlegroundMinionPool)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BattlegroundMinionPool));
			}
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x0006A048 File Offset: 0x00068248
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasBattlegroundDenyList)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.BattlegroundDenyList);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasBattlegroundMinionPool)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.BattlegroundMinionPool);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}

		// Token: 0x04000AE1 RID: 2785
		public bool HasBattlegroundDenyList;

		// Token: 0x04000AE2 RID: 2786
		private string _BattlegroundDenyList;

		// Token: 0x04000AE3 RID: 2787
		public bool HasBattlegroundMinionPool;

		// Token: 0x04000AE4 RID: 2788
		private string _BattlegroundMinionPool;

		// Token: 0x02000663 RID: 1635
		public enum PacketID
		{
			// Token: 0x04002160 RID: 8544
			ID = 53
		}
	}
}
