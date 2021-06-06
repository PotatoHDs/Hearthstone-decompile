using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000D8 RID: 216
	public class CardBackModifications : IProtoBuf
	{
		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x0003576F File Offset: 0x0003396F
		// (set) Token: 0x06000EB1 RID: 3761 RVA: 0x00035777 File Offset: 0x00033977
		public List<CardBackModification> CardBackModifications_
		{
			get
			{
				return this._CardBackModifications_;
			}
			set
			{
				this._CardBackModifications_ = value;
			}
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x00035780 File Offset: 0x00033980
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (CardBackModification cardBackModification in this.CardBackModifications_)
			{
				num ^= cardBackModification.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x000357E4 File Offset: 0x000339E4
		public override bool Equals(object obj)
		{
			CardBackModifications cardBackModifications = obj as CardBackModifications;
			if (cardBackModifications == null)
			{
				return false;
			}
			if (this.CardBackModifications_.Count != cardBackModifications.CardBackModifications_.Count)
			{
				return false;
			}
			for (int i = 0; i < this.CardBackModifications_.Count; i++)
			{
				if (!this.CardBackModifications_[i].Equals(cardBackModifications.CardBackModifications_[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x0003584F File Offset: 0x00033A4F
		public void Deserialize(Stream stream)
		{
			CardBackModifications.Deserialize(stream, this);
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x00035859 File Offset: 0x00033A59
		public static CardBackModifications Deserialize(Stream stream, CardBackModifications instance)
		{
			return CardBackModifications.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x00035864 File Offset: 0x00033A64
		public static CardBackModifications DeserializeLengthDelimited(Stream stream)
		{
			CardBackModifications cardBackModifications = new CardBackModifications();
			CardBackModifications.DeserializeLengthDelimited(stream, cardBackModifications);
			return cardBackModifications;
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x00035880 File Offset: 0x00033A80
		public static CardBackModifications DeserializeLengthDelimited(Stream stream, CardBackModifications instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CardBackModifications.Deserialize(stream, instance, num);
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x000358A8 File Offset: 0x00033AA8
		public static CardBackModifications Deserialize(Stream stream, CardBackModifications instance, long limit)
		{
			if (instance.CardBackModifications_ == null)
			{
				instance.CardBackModifications_ = new List<CardBackModification>();
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
					instance.CardBackModifications_.Add(CardBackModification.DeserializeLengthDelimited(stream));
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

		// Token: 0x06000EB9 RID: 3769 RVA: 0x00035940 File Offset: 0x00033B40
		public void Serialize(Stream stream)
		{
			CardBackModifications.Serialize(stream, this);
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x0003594C File Offset: 0x00033B4C
		public static void Serialize(Stream stream, CardBackModifications instance)
		{
			if (instance.CardBackModifications_.Count > 0)
			{
				foreach (CardBackModification cardBackModification in instance.CardBackModifications_)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, cardBackModification.GetSerializedSize());
					CardBackModification.Serialize(stream, cardBackModification);
				}
			}
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x000359C4 File Offset: 0x00033BC4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.CardBackModifications_.Count > 0)
			{
				foreach (CardBackModification cardBackModification in this.CardBackModifications_)
				{
					num += 1U;
					uint serializedSize = cardBackModification.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x040004D5 RID: 1237
		private List<CardBackModification> _CardBackModifications_ = new List<CardBackModification>();
	}
}
