using System;
using System.Collections.Generic;
using System.IO;

namespace SpectatorProto
{
	// Token: 0x0200002F RID: 47
	public class PartyQuestInfo : IProtoBuf
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000AB4A File Offset: 0x00008D4A
		// (set) Token: 0x06000276 RID: 630 RVA: 0x0000AB52 File Offset: 0x00008D52
		public List<int> QuestIds
		{
			get
			{
				return this._QuestIds;
			}
			set
			{
				this._QuestIds = value;
			}
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000AB5C File Offset: 0x00008D5C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (int num2 in this.QuestIds)
			{
				num ^= num2.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000ABC0 File Offset: 0x00008DC0
		public override bool Equals(object obj)
		{
			PartyQuestInfo partyQuestInfo = obj as PartyQuestInfo;
			if (partyQuestInfo == null)
			{
				return false;
			}
			if (this.QuestIds.Count != partyQuestInfo.QuestIds.Count)
			{
				return false;
			}
			for (int i = 0; i < this.QuestIds.Count; i++)
			{
				if (!this.QuestIds[i].Equals(partyQuestInfo.QuestIds[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000AC2E File Offset: 0x00008E2E
		public void Deserialize(Stream stream)
		{
			PartyQuestInfo.Deserialize(stream, this);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000AC38 File Offset: 0x00008E38
		public static PartyQuestInfo Deserialize(Stream stream, PartyQuestInfo instance)
		{
			return PartyQuestInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000AC44 File Offset: 0x00008E44
		public static PartyQuestInfo DeserializeLengthDelimited(Stream stream)
		{
			PartyQuestInfo partyQuestInfo = new PartyQuestInfo();
			PartyQuestInfo.DeserializeLengthDelimited(stream, partyQuestInfo);
			return partyQuestInfo;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000AC60 File Offset: 0x00008E60
		public static PartyQuestInfo DeserializeLengthDelimited(Stream stream, PartyQuestInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PartyQuestInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000AC88 File Offset: 0x00008E88
		public static PartyQuestInfo Deserialize(Stream stream, PartyQuestInfo instance, long limit)
		{
			if (instance.QuestIds == null)
			{
				instance.QuestIds = new List<int>();
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
				else if (num == 8)
				{
					instance.QuestIds.Add((int)ProtocolParser.ReadUInt64(stream));
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

		// Token: 0x0600027E RID: 638 RVA: 0x0000AD20 File Offset: 0x00008F20
		public void Serialize(Stream stream)
		{
			PartyQuestInfo.Serialize(stream, this);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000AD2C File Offset: 0x00008F2C
		public static void Serialize(Stream stream, PartyQuestInfo instance)
		{
			if (instance.QuestIds.Count > 0)
			{
				foreach (int num in instance.QuestIds)
				{
					stream.WriteByte(8);
					ProtocolParser.WriteUInt64(stream, (ulong)((long)num));
				}
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000AD98 File Offset: 0x00008F98
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.QuestIds.Count > 0)
			{
				foreach (int num2 in this.QuestIds)
				{
					num += 1U;
					num += ProtocolParser.SizeOfUInt64((ulong)((long)num2));
				}
			}
			return num;
		}

		// Token: 0x040000C1 RID: 193
		private List<int> _QuestIds = new List<int>();
	}
}
