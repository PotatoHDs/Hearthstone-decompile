using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000107 RID: 263
	public class AckAchievement : IProtoBuf
	{
		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06001157 RID: 4439 RVA: 0x0003CFAE File Offset: 0x0003B1AE
		// (set) Token: 0x06001158 RID: 4440 RVA: 0x0003CFB6 File Offset: 0x0003B1B6
		public int AchievementId
		{
			get
			{
				return this._AchievementId;
			}
			set
			{
				this._AchievementId = value;
				this.HasAchievementId = true;
			}
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x0003CFC8 File Offset: 0x0003B1C8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAchievementId)
			{
				num ^= this.AchievementId.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x0003CFFC File Offset: 0x0003B1FC
		public override bool Equals(object obj)
		{
			AckAchievement ackAchievement = obj as AckAchievement;
			return ackAchievement != null && this.HasAchievementId == ackAchievement.HasAchievementId && (!this.HasAchievementId || this.AchievementId.Equals(ackAchievement.AchievementId));
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x0003D044 File Offset: 0x0003B244
		public void Deserialize(Stream stream)
		{
			AckAchievement.Deserialize(stream, this);
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x0003D04E File Offset: 0x0003B24E
		public static AckAchievement Deserialize(Stream stream, AckAchievement instance)
		{
			return AckAchievement.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x0003D05C File Offset: 0x0003B25C
		public static AckAchievement DeserializeLengthDelimited(Stream stream)
		{
			AckAchievement ackAchievement = new AckAchievement();
			AckAchievement.DeserializeLengthDelimited(stream, ackAchievement);
			return ackAchievement;
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x0003D078 File Offset: 0x0003B278
		public static AckAchievement DeserializeLengthDelimited(Stream stream, AckAchievement instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AckAchievement.Deserialize(stream, instance, num);
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x0003D0A0 File Offset: 0x0003B2A0
		public static AckAchievement Deserialize(Stream stream, AckAchievement instance, long limit)
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
				else if (num == 8)
				{
					instance.AchievementId = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06001160 RID: 4448 RVA: 0x0003D120 File Offset: 0x0003B320
		public void Serialize(Stream stream)
		{
			AckAchievement.Serialize(stream, this);
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x0003D129 File Offset: 0x0003B329
		public static void Serialize(Stream stream, AckAchievement instance)
		{
			if (instance.HasAchievementId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.AchievementId));
			}
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x0003D148 File Offset: 0x0003B348
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAchievementId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.AchievementId));
			}
			return num;
		}

		// Token: 0x0400054E RID: 1358
		public bool HasAchievementId;

		// Token: 0x0400054F RID: 1359
		private int _AchievementId;

		// Token: 0x02000609 RID: 1545
		public enum PacketID
		{
			// Token: 0x04002057 RID: 8279
			ID = 612,
			// Token: 0x04002058 RID: 8280
			System = 0
		}
	}
}
