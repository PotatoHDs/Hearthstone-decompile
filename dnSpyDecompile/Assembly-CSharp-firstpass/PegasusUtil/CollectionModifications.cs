using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000D5 RID: 213
	public class CollectionModifications : IProtoBuf
	{
		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000E85 RID: 3717 RVA: 0x00034ECD File Offset: 0x000330CD
		// (set) Token: 0x06000E86 RID: 3718 RVA: 0x00034ED5 File Offset: 0x000330D5
		public List<CardModification> CardModifications
		{
			get
			{
				return this._CardModifications;
			}
			set
			{
				this._CardModifications = value;
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000E87 RID: 3719 RVA: 0x00034EDE File Offset: 0x000330DE
		// (set) Token: 0x06000E88 RID: 3720 RVA: 0x00034EE6 File Offset: 0x000330E6
		public long CollectionVersion
		{
			get
			{
				return this._CollectionVersion;
			}
			set
			{
				this._CollectionVersion = value;
				this.HasCollectionVersion = true;
			}
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x00034EF8 File Offset: 0x000330F8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (CardModification cardModification in this.CardModifications)
			{
				num ^= cardModification.GetHashCode();
			}
			if (this.HasCollectionVersion)
			{
				num ^= this.CollectionVersion.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x00034F74 File Offset: 0x00033174
		public override bool Equals(object obj)
		{
			CollectionModifications collectionModifications = obj as CollectionModifications;
			if (collectionModifications == null)
			{
				return false;
			}
			if (this.CardModifications.Count != collectionModifications.CardModifications.Count)
			{
				return false;
			}
			for (int i = 0; i < this.CardModifications.Count; i++)
			{
				if (!this.CardModifications[i].Equals(collectionModifications.CardModifications[i]))
				{
					return false;
				}
			}
			return this.HasCollectionVersion == collectionModifications.HasCollectionVersion && (!this.HasCollectionVersion || this.CollectionVersion.Equals(collectionModifications.CollectionVersion));
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x0003500D File Offset: 0x0003320D
		public void Deserialize(Stream stream)
		{
			CollectionModifications.Deserialize(stream, this);
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x00035017 File Offset: 0x00033217
		public static CollectionModifications Deserialize(Stream stream, CollectionModifications instance)
		{
			return CollectionModifications.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x00035024 File Offset: 0x00033224
		public static CollectionModifications DeserializeLengthDelimited(Stream stream)
		{
			CollectionModifications collectionModifications = new CollectionModifications();
			CollectionModifications.DeserializeLengthDelimited(stream, collectionModifications);
			return collectionModifications;
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x00035040 File Offset: 0x00033240
		public static CollectionModifications DeserializeLengthDelimited(Stream stream, CollectionModifications instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CollectionModifications.Deserialize(stream, instance, num);
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x00035068 File Offset: 0x00033268
		public static CollectionModifications Deserialize(Stream stream, CollectionModifications instance, long limit)
		{
			if (instance.CardModifications == null)
			{
				instance.CardModifications = new List<CardModification>();
			}
			instance.CollectionVersion = 0L;
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
						instance.CollectionVersion = (long)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.CardModifications.Add(CardModification.DeserializeLengthDelimited(stream));
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x00035120 File Offset: 0x00033320
		public void Serialize(Stream stream)
		{
			CollectionModifications.Serialize(stream, this);
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0003512C File Offset: 0x0003332C
		public static void Serialize(Stream stream, CollectionModifications instance)
		{
			if (instance.CardModifications.Count > 0)
			{
				foreach (CardModification cardModification in instance.CardModifications)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, cardModification.GetSerializedSize());
					CardModification.Serialize(stream, cardModification);
				}
			}
			if (instance.HasCollectionVersion)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CollectionVersion);
			}
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x000351C0 File Offset: 0x000333C0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.CardModifications.Count > 0)
			{
				foreach (CardModification cardModification in this.CardModifications)
				{
					num += 1U;
					uint serializedSize = cardModification.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasCollectionVersion)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.CollectionVersion);
			}
			return num;
		}

		// Token: 0x040004CE RID: 1230
		private List<CardModification> _CardModifications = new List<CardModification>();

		// Token: 0x040004CF RID: 1231
		public bool HasCollectionVersion;

		// Token: 0x040004D0 RID: 1232
		private long _CollectionVersion;
	}
}
