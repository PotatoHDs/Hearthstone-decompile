using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000053 RID: 83
	public class GetDeckContents : IProtoBuf
	{
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x00015A1D File Offset: 0x00013C1D
		// (set) Token: 0x06000546 RID: 1350 RVA: 0x00015A25 File Offset: 0x00013C25
		public List<long> DeckId
		{
			get
			{
				return this._DeckId;
			}
			set
			{
				this._DeckId = value;
			}
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00015A30 File Offset: 0x00013C30
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (long num2 in this.DeckId)
			{
				num ^= num2.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00015A94 File Offset: 0x00013C94
		public override bool Equals(object obj)
		{
			GetDeckContents getDeckContents = obj as GetDeckContents;
			if (getDeckContents == null)
			{
				return false;
			}
			if (this.DeckId.Count != getDeckContents.DeckId.Count)
			{
				return false;
			}
			for (int i = 0; i < this.DeckId.Count; i++)
			{
				if (!this.DeckId[i].Equals(getDeckContents.DeckId[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00015B02 File Offset: 0x00013D02
		public void Deserialize(Stream stream)
		{
			GetDeckContents.Deserialize(stream, this);
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00015B0C File Offset: 0x00013D0C
		public static GetDeckContents Deserialize(Stream stream, GetDeckContents instance)
		{
			return GetDeckContents.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00015B18 File Offset: 0x00013D18
		public static GetDeckContents DeserializeLengthDelimited(Stream stream)
		{
			GetDeckContents getDeckContents = new GetDeckContents();
			GetDeckContents.DeserializeLengthDelimited(stream, getDeckContents);
			return getDeckContents;
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00015B34 File Offset: 0x00013D34
		public static GetDeckContents DeserializeLengthDelimited(Stream stream, GetDeckContents instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetDeckContents.Deserialize(stream, instance, num);
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00015B5C File Offset: 0x00013D5C
		public static GetDeckContents Deserialize(Stream stream, GetDeckContents instance, long limit)
		{
			if (instance.DeckId == null)
			{
				instance.DeckId = new List<long>();
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
					instance.DeckId.Add((long)ProtocolParser.ReadUInt64(stream));
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

		// Token: 0x0600054E RID: 1358 RVA: 0x00015BF3 File Offset: 0x00013DF3
		public void Serialize(Stream stream)
		{
			GetDeckContents.Serialize(stream, this);
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00015BFC File Offset: 0x00013DFC
		public static void Serialize(Stream stream, GetDeckContents instance)
		{
			if (instance.DeckId.Count > 0)
			{
				foreach (long val in instance.DeckId)
				{
					stream.WriteByte(8);
					ProtocolParser.WriteUInt64(stream, (ulong)val);
				}
			}
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00015C64 File Offset: 0x00013E64
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.DeckId.Count > 0)
			{
				foreach (long val in this.DeckId)
				{
					num += 1U;
					num += ProtocolParser.SizeOfUInt64((ulong)val);
				}
			}
			return num;
		}

		// Token: 0x040001EA RID: 490
		private List<long> _DeckId = new List<long>();

		// Token: 0x02000565 RID: 1381
		public enum PacketID
		{
			// Token: 0x04001E87 RID: 7815
			ID = 214,
			// Token: 0x04001E88 RID: 7816
			System = 0
		}
	}
}
