using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000101 RID: 257
	public class RerollQuest : IProtoBuf
	{
		// Token: 0x17000339 RID: 825
		// (get) Token: 0x060010FD RID: 4349 RVA: 0x0003BE0A File Offset: 0x0003A00A
		// (set) Token: 0x060010FE RID: 4350 RVA: 0x0003BE12 File Offset: 0x0003A012
		public int QuestId
		{
			get
			{
				return this._QuestId;
			}
			set
			{
				this._QuestId = value;
				this.HasQuestId = true;
			}
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x0003BE24 File Offset: 0x0003A024
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasQuestId)
			{
				num ^= this.QuestId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x0003BE58 File Offset: 0x0003A058
		public override bool Equals(object obj)
		{
			RerollQuest rerollQuest = obj as RerollQuest;
			return rerollQuest != null && this.HasQuestId == rerollQuest.HasQuestId && (!this.HasQuestId || this.QuestId.Equals(rerollQuest.QuestId));
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x0003BEA0 File Offset: 0x0003A0A0
		public void Deserialize(Stream stream)
		{
			RerollQuest.Deserialize(stream, this);
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x0003BEAA File Offset: 0x0003A0AA
		public static RerollQuest Deserialize(Stream stream, RerollQuest instance)
		{
			return RerollQuest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x0003BEB8 File Offset: 0x0003A0B8
		public static RerollQuest DeserializeLengthDelimited(Stream stream)
		{
			RerollQuest rerollQuest = new RerollQuest();
			RerollQuest.DeserializeLengthDelimited(stream, rerollQuest);
			return rerollQuest;
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x0003BED4 File Offset: 0x0003A0D4
		public static RerollQuest DeserializeLengthDelimited(Stream stream, RerollQuest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RerollQuest.Deserialize(stream, instance, num);
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x0003BEFC File Offset: 0x0003A0FC
		public static RerollQuest Deserialize(Stream stream, RerollQuest instance, long limit)
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
					instance.QuestId = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06001106 RID: 4358 RVA: 0x0003BF7C File Offset: 0x0003A17C
		public void Serialize(Stream stream)
		{
			RerollQuest.Serialize(stream, this);
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x0003BF85 File Offset: 0x0003A185
		public static void Serialize(Stream stream, RerollQuest instance)
		{
			if (instance.HasQuestId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.QuestId));
			}
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x0003BFA4 File Offset: 0x0003A1A4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasQuestId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.QuestId));
			}
			return num;
		}

		// Token: 0x04000539 RID: 1337
		public bool HasQuestId;

		// Token: 0x0400053A RID: 1338
		private int _QuestId;

		// Token: 0x02000603 RID: 1539
		public enum PacketID
		{
			// Token: 0x04002046 RID: 8262
			ID = 606,
			// Token: 0x04002047 RID: 8263
			System = 0
		}
	}
}
