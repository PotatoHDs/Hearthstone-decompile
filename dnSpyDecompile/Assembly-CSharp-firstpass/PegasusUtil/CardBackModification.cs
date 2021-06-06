using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000D7 RID: 215
	public class CardBackModification : IProtoBuf
	{
		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000EA1 RID: 3745 RVA: 0x0003553F File Offset: 0x0003373F
		// (set) Token: 0x06000EA2 RID: 3746 RVA: 0x00035547 File Offset: 0x00033747
		public int AssetCardBackId { get; set; }

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000EA3 RID: 3747 RVA: 0x00035550 File Offset: 0x00033750
		// (set) Token: 0x06000EA4 RID: 3748 RVA: 0x00035558 File Offset: 0x00033758
		public bool AutoSetAsFavorite
		{
			get
			{
				return this._AutoSetAsFavorite;
			}
			set
			{
				this._AutoSetAsFavorite = value;
				this.HasAutoSetAsFavorite = true;
			}
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x00035568 File Offset: 0x00033768
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.AssetCardBackId.GetHashCode();
			if (this.HasAutoSetAsFavorite)
			{
				num ^= this.AutoSetAsFavorite.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x000355AC File Offset: 0x000337AC
		public override bool Equals(object obj)
		{
			CardBackModification cardBackModification = obj as CardBackModification;
			return cardBackModification != null && this.AssetCardBackId.Equals(cardBackModification.AssetCardBackId) && this.HasAutoSetAsFavorite == cardBackModification.HasAutoSetAsFavorite && (!this.HasAutoSetAsFavorite || this.AutoSetAsFavorite.Equals(cardBackModification.AutoSetAsFavorite));
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x0003560C File Offset: 0x0003380C
		public void Deserialize(Stream stream)
		{
			CardBackModification.Deserialize(stream, this);
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x00035616 File Offset: 0x00033816
		public static CardBackModification Deserialize(Stream stream, CardBackModification instance)
		{
			return CardBackModification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x00035624 File Offset: 0x00033824
		public static CardBackModification DeserializeLengthDelimited(Stream stream)
		{
			CardBackModification cardBackModification = new CardBackModification();
			CardBackModification.DeserializeLengthDelimited(stream, cardBackModification);
			return cardBackModification;
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x00035640 File Offset: 0x00033840
		public static CardBackModification DeserializeLengthDelimited(Stream stream, CardBackModification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CardBackModification.Deserialize(stream, instance, num);
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x00035668 File Offset: 0x00033868
		public static CardBackModification Deserialize(Stream stream, CardBackModification instance, long limit)
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
				else if (num != 8)
				{
					if (num != 16)
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
						instance.AutoSetAsFavorite = ProtocolParser.ReadBool(stream);
					}
				}
				else
				{
					instance.AssetCardBackId = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x00035700 File Offset: 0x00033900
		public void Serialize(Stream stream)
		{
			CardBackModification.Serialize(stream, this);
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x00035709 File Offset: 0x00033909
		public static void Serialize(Stream stream, CardBackModification instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.AssetCardBackId));
			if (instance.HasAutoSetAsFavorite)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.AutoSetAsFavorite);
			}
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x0003573C File Offset: 0x0003393C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.AssetCardBackId));
			if (this.HasAutoSetAsFavorite)
			{
				num += 1U;
				num += 1U;
			}
			return num + 1U;
		}

		// Token: 0x040004D3 RID: 1235
		public bool HasAutoSetAsFavorite;

		// Token: 0x040004D4 RID: 1236
		private bool _AutoSetAsFavorite;
	}
}
