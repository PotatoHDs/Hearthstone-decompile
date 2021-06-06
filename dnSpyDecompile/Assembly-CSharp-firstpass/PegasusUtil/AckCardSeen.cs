using System;
using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x0200005C RID: 92
	public class AckCardSeen : IProtoBuf
	{
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x000173F6 File Offset: 0x000155F6
		// (set) Token: 0x060005DB RID: 1499 RVA: 0x000173FE File Offset: 0x000155FE
		public List<CardDef> CardDefs
		{
			get
			{
				return this._CardDefs;
			}
			set
			{
				this._CardDefs = value;
			}
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00017408 File Offset: 0x00015608
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (CardDef cardDef in this.CardDefs)
			{
				num ^= cardDef.GetHashCode();
			}
			return num;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001746C File Offset: 0x0001566C
		public override bool Equals(object obj)
		{
			AckCardSeen ackCardSeen = obj as AckCardSeen;
			if (ackCardSeen == null)
			{
				return false;
			}
			if (this.CardDefs.Count != ackCardSeen.CardDefs.Count)
			{
				return false;
			}
			for (int i = 0; i < this.CardDefs.Count; i++)
			{
				if (!this.CardDefs[i].Equals(ackCardSeen.CardDefs[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x000174D7 File Offset: 0x000156D7
		public void Deserialize(Stream stream)
		{
			AckCardSeen.Deserialize(stream, this);
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x000174E1 File Offset: 0x000156E1
		public static AckCardSeen Deserialize(Stream stream, AckCardSeen instance)
		{
			return AckCardSeen.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x000174EC File Offset: 0x000156EC
		public static AckCardSeen DeserializeLengthDelimited(Stream stream)
		{
			AckCardSeen ackCardSeen = new AckCardSeen();
			AckCardSeen.DeserializeLengthDelimited(stream, ackCardSeen);
			return ackCardSeen;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00017508 File Offset: 0x00015708
		public static AckCardSeen DeserializeLengthDelimited(Stream stream, AckCardSeen instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AckCardSeen.Deserialize(stream, instance, num);
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00017530 File Offset: 0x00015730
		public static AckCardSeen Deserialize(Stream stream, AckCardSeen instance, long limit)
		{
			if (instance.CardDefs == null)
			{
				instance.CardDefs = new List<CardDef>();
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
					instance.CardDefs.Add(CardDef.DeserializeLengthDelimited(stream));
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

		// Token: 0x060005E3 RID: 1507 RVA: 0x000175C8 File Offset: 0x000157C8
		public void Serialize(Stream stream)
		{
			AckCardSeen.Serialize(stream, this);
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x000175D4 File Offset: 0x000157D4
		public static void Serialize(Stream stream, AckCardSeen instance)
		{
			if (instance.CardDefs.Count > 0)
			{
				foreach (CardDef cardDef in instance.CardDefs)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, cardDef.GetSerializedSize());
					CardDef.Serialize(stream, cardDef);
				}
			}
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0001764C File Offset: 0x0001584C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.CardDefs.Count > 0)
			{
				foreach (CardDef cardDef in this.CardDefs)
				{
					num += 1U;
					uint serializedSize = cardDef.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x0400020F RID: 527
		private List<CardDef> _CardDefs = new List<CardDef>();

		// Token: 0x0200056E RID: 1390
		public enum PacketID
		{
			// Token: 0x04001EA0 RID: 7840
			ID = 223,
			// Token: 0x04001EA1 RID: 7841
			System = 0
		}
	}
}
