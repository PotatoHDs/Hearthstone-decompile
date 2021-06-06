using System;
using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x02000099 RID: 153
	public class Collection : IProtoBuf
	{
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000A4B RID: 2635 RVA: 0x00025FD3 File Offset: 0x000241D3
		// (set) Token: 0x06000A4C RID: 2636 RVA: 0x00025FDB File Offset: 0x000241DB
		public List<CardStack> Stacks
		{
			get
			{
				return this._Stacks;
			}
			set
			{
				this._Stacks = value;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x00025FE4 File Offset: 0x000241E4
		// (set) Token: 0x06000A4E RID: 2638 RVA: 0x00025FEC File Offset: 0x000241EC
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

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x00025FFC File Offset: 0x000241FC
		// (set) Token: 0x06000A50 RID: 2640 RVA: 0x00026004 File Offset: 0x00024204
		public long CollectionVersionLastModified
		{
			get
			{
				return this._CollectionVersionLastModified;
			}
			set
			{
				this._CollectionVersionLastModified = value;
				this.HasCollectionVersionLastModified = true;
			}
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x00026014 File Offset: 0x00024214
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (CardStack cardStack in this.Stacks)
			{
				num ^= cardStack.GetHashCode();
			}
			if (this.HasCollectionVersion)
			{
				num ^= this.CollectionVersion.GetHashCode();
			}
			if (this.HasCollectionVersionLastModified)
			{
				num ^= this.CollectionVersionLastModified.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x000260A8 File Offset: 0x000242A8
		public override bool Equals(object obj)
		{
			Collection collection = obj as Collection;
			if (collection == null)
			{
				return false;
			}
			if (this.Stacks.Count != collection.Stacks.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Stacks.Count; i++)
			{
				if (!this.Stacks[i].Equals(collection.Stacks[i]))
				{
					return false;
				}
			}
			return this.HasCollectionVersion == collection.HasCollectionVersion && (!this.HasCollectionVersion || this.CollectionVersion.Equals(collection.CollectionVersion)) && this.HasCollectionVersionLastModified == collection.HasCollectionVersionLastModified && (!this.HasCollectionVersionLastModified || this.CollectionVersionLastModified.Equals(collection.CollectionVersionLastModified));
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0002616F File Offset: 0x0002436F
		public void Deserialize(Stream stream)
		{
			Collection.Deserialize(stream, this);
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x00026179 File Offset: 0x00024379
		public static Collection Deserialize(Stream stream, Collection instance)
		{
			return Collection.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x00026184 File Offset: 0x00024384
		public static Collection DeserializeLengthDelimited(Stream stream)
		{
			Collection collection = new Collection();
			Collection.DeserializeLengthDelimited(stream, collection);
			return collection;
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x000261A0 File Offset: 0x000243A0
		public static Collection DeserializeLengthDelimited(Stream stream, Collection instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Collection.Deserialize(stream, instance, num);
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x000261C8 File Offset: 0x000243C8
		public static Collection Deserialize(Stream stream, Collection instance, long limit)
		{
			if (instance.Stacks == null)
			{
				instance.Stacks = new List<CardStack>();
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
						if (num != 24)
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
							instance.CollectionVersionLastModified = (long)ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.CollectionVersion = (long)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Stacks.Add(CardStack.DeserializeLengthDelimited(stream));
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x00026296 File Offset: 0x00024496
		public void Serialize(Stream stream)
		{
			Collection.Serialize(stream, this);
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x000262A0 File Offset: 0x000244A0
		public static void Serialize(Stream stream, Collection instance)
		{
			if (instance.Stacks.Count > 0)
			{
				foreach (CardStack cardStack in instance.Stacks)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, cardStack.GetSerializedSize());
					CardStack.Serialize(stream, cardStack);
				}
			}
			if (instance.HasCollectionVersion)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CollectionVersion);
			}
			if (instance.HasCollectionVersionLastModified)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CollectionVersionLastModified);
			}
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00026350 File Offset: 0x00024550
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Stacks.Count > 0)
			{
				foreach (CardStack cardStack in this.Stacks)
				{
					num += 1U;
					uint serializedSize = cardStack.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasCollectionVersion)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.CollectionVersion);
			}
			if (this.HasCollectionVersionLastModified)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.CollectionVersionLastModified);
			}
			return num;
		}

		// Token: 0x0400038A RID: 906
		private List<CardStack> _Stacks = new List<CardStack>();

		// Token: 0x0400038B RID: 907
		public bool HasCollectionVersion;

		// Token: 0x0400038C RID: 908
		private long _CollectionVersion;

		// Token: 0x0400038D RID: 909
		public bool HasCollectionVersionLastModified;

		// Token: 0x0400038E RID: 910
		private long _CollectionVersionLastModified;
	}
}
