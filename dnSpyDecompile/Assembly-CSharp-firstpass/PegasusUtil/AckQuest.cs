using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000FF RID: 255
	public class AckQuest : IProtoBuf
	{
		// Token: 0x17000338 RID: 824
		// (get) Token: 0x060010E5 RID: 4325 RVA: 0x0003BB67 File Offset: 0x00039D67
		// (set) Token: 0x060010E6 RID: 4326 RVA: 0x0003BB6F File Offset: 0x00039D6F
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

		// Token: 0x060010E7 RID: 4327 RVA: 0x0003BB80 File Offset: 0x00039D80
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasQuestId)
			{
				num ^= this.QuestId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x0003BBB4 File Offset: 0x00039DB4
		public override bool Equals(object obj)
		{
			AckQuest ackQuest = obj as AckQuest;
			return ackQuest != null && this.HasQuestId == ackQuest.HasQuestId && (!this.HasQuestId || this.QuestId.Equals(ackQuest.QuestId));
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x0003BBFC File Offset: 0x00039DFC
		public void Deserialize(Stream stream)
		{
			AckQuest.Deserialize(stream, this);
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x0003BC06 File Offset: 0x00039E06
		public static AckQuest Deserialize(Stream stream, AckQuest instance)
		{
			return AckQuest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x0003BC14 File Offset: 0x00039E14
		public static AckQuest DeserializeLengthDelimited(Stream stream)
		{
			AckQuest ackQuest = new AckQuest();
			AckQuest.DeserializeLengthDelimited(stream, ackQuest);
			return ackQuest;
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x0003BC30 File Offset: 0x00039E30
		public static AckQuest DeserializeLengthDelimited(Stream stream, AckQuest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AckQuest.Deserialize(stream, instance, num);
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x0003BC58 File Offset: 0x00039E58
		public static AckQuest Deserialize(Stream stream, AckQuest instance, long limit)
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

		// Token: 0x060010EE RID: 4334 RVA: 0x0003BCD8 File Offset: 0x00039ED8
		public void Serialize(Stream stream)
		{
			AckQuest.Serialize(stream, this);
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x0003BCE1 File Offset: 0x00039EE1
		public static void Serialize(Stream stream, AckQuest instance)
		{
			if (instance.HasQuestId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.QuestId));
			}
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x0003BD00 File Offset: 0x00039F00
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

		// Token: 0x04000537 RID: 1335
		public bool HasQuestId;

		// Token: 0x04000538 RID: 1336
		private int _QuestId;

		// Token: 0x02000601 RID: 1537
		public enum PacketID
		{
			// Token: 0x04002040 RID: 8256
			ID = 604,
			// Token: 0x04002041 RID: 8257
			System = 0
		}
	}
}
