using System;
using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x02000072 RID: 114
	public class GetAssetsVersion : IProtoBuf
	{
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x0001A95A File Offset: 0x00018B5A
		// (set) Token: 0x0600072F RID: 1839 RVA: 0x0001A962 File Offset: 0x00018B62
		public Platform Platform
		{
			get
			{
				return this._Platform;
			}
			set
			{
				this._Platform = value;
				this.HasPlatform = (value != null);
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x0001A975 File Offset: 0x00018B75
		// (set) Token: 0x06000731 RID: 1841 RVA: 0x0001A97D File Offset: 0x00018B7D
		public long ClientCollectionVersion
		{
			get
			{
				return this._ClientCollectionVersion;
			}
			set
			{
				this._ClientCollectionVersion = value;
				this.HasClientCollectionVersion = true;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x0001A98D File Offset: 0x00018B8D
		// (set) Token: 0x06000733 RID: 1843 RVA: 0x0001A995 File Offset: 0x00018B95
		public List<GetAssetsVersion.DeckModificationTimes> CachedDeckModificationTimes
		{
			get
			{
				return this._CachedDeckModificationTimes;
			}
			set
			{
				this._CachedDeckModificationTimes = value;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x0001A99E File Offset: 0x00018B9E
		// (set) Token: 0x06000735 RID: 1845 RVA: 0x0001A9A6 File Offset: 0x00018BA6
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

		// Token: 0x06000736 RID: 1846 RVA: 0x0001A9B8 File Offset: 0x00018BB8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasPlatform)
			{
				num ^= this.Platform.GetHashCode();
			}
			if (this.HasClientCollectionVersion)
			{
				num ^= this.ClientCollectionVersion.GetHashCode();
			}
			foreach (GetAssetsVersion.DeckModificationTimes deckModificationTimes in this.CachedDeckModificationTimes)
			{
				num ^= deckModificationTimes.GetHashCode();
			}
			if (this.HasCollectionVersionLastModified)
			{
				num ^= this.CollectionVersionLastModified.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0001AA64 File Offset: 0x00018C64
		public override bool Equals(object obj)
		{
			GetAssetsVersion getAssetsVersion = obj as GetAssetsVersion;
			if (getAssetsVersion == null)
			{
				return false;
			}
			if (this.HasPlatform != getAssetsVersion.HasPlatform || (this.HasPlatform && !this.Platform.Equals(getAssetsVersion.Platform)))
			{
				return false;
			}
			if (this.HasClientCollectionVersion != getAssetsVersion.HasClientCollectionVersion || (this.HasClientCollectionVersion && !this.ClientCollectionVersion.Equals(getAssetsVersion.ClientCollectionVersion)))
			{
				return false;
			}
			if (this.CachedDeckModificationTimes.Count != getAssetsVersion.CachedDeckModificationTimes.Count)
			{
				return false;
			}
			for (int i = 0; i < this.CachedDeckModificationTimes.Count; i++)
			{
				if (!this.CachedDeckModificationTimes[i].Equals(getAssetsVersion.CachedDeckModificationTimes[i]))
				{
					return false;
				}
			}
			return this.HasCollectionVersionLastModified == getAssetsVersion.HasCollectionVersionLastModified && (!this.HasCollectionVersionLastModified || this.CollectionVersionLastModified.Equals(getAssetsVersion.CollectionVersionLastModified));
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0001AB56 File Offset: 0x00018D56
		public void Deserialize(Stream stream)
		{
			GetAssetsVersion.Deserialize(stream, this);
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0001AB60 File Offset: 0x00018D60
		public static GetAssetsVersion Deserialize(Stream stream, GetAssetsVersion instance)
		{
			return GetAssetsVersion.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0001AB6C File Offset: 0x00018D6C
		public static GetAssetsVersion DeserializeLengthDelimited(Stream stream)
		{
			GetAssetsVersion getAssetsVersion = new GetAssetsVersion();
			GetAssetsVersion.DeserializeLengthDelimited(stream, getAssetsVersion);
			return getAssetsVersion;
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0001AB88 File Offset: 0x00018D88
		public static GetAssetsVersion DeserializeLengthDelimited(Stream stream, GetAssetsVersion instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetAssetsVersion.Deserialize(stream, instance, num);
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0001ABB0 File Offset: 0x00018DB0
		public static GetAssetsVersion Deserialize(Stream stream, GetAssetsVersion instance, long limit)
		{
			if (instance.CachedDeckModificationTimes == null)
			{
				instance.CachedDeckModificationTimes = new List<GetAssetsVersion.DeckModificationTimes>();
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
				else
				{
					if (num <= 16)
					{
						if (num != 10)
						{
							if (num == 16)
							{
								instance.ClientCollectionVersion = (long)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else
						{
							if (instance.Platform == null)
							{
								instance.Platform = Platform.DeserializeLengthDelimited(stream);
								continue;
							}
							Platform.DeserializeLengthDelimited(stream, instance.Platform);
							continue;
						}
					}
					else
					{
						if (num == 26)
						{
							instance.CachedDeckModificationTimes.Add(GetAssetsVersion.DeckModificationTimes.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 32)
						{
							instance.CollectionVersionLastModified = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
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

		// Token: 0x0600073D RID: 1853 RVA: 0x0001ACB3 File Offset: 0x00018EB3
		public void Serialize(Stream stream)
		{
			GetAssetsVersion.Serialize(stream, this);
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0001ACBC File Offset: 0x00018EBC
		public static void Serialize(Stream stream, GetAssetsVersion instance)
		{
			if (instance.HasPlatform)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Platform.GetSerializedSize());
				Platform.Serialize(stream, instance.Platform);
			}
			if (instance.HasClientCollectionVersion)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ClientCollectionVersion);
			}
			if (instance.CachedDeckModificationTimes.Count > 0)
			{
				foreach (GetAssetsVersion.DeckModificationTimes deckModificationTimes in instance.CachedDeckModificationTimes)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, deckModificationTimes.GetSerializedSize());
					GetAssetsVersion.DeckModificationTimes.Serialize(stream, deckModificationTimes);
				}
			}
			if (instance.HasCollectionVersionLastModified)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CollectionVersionLastModified);
			}
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0001AD98 File Offset: 0x00018F98
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasPlatform)
			{
				num += 1U;
				uint serializedSize = this.Platform.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasClientCollectionVersion)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.ClientCollectionVersion);
			}
			if (this.CachedDeckModificationTimes.Count > 0)
			{
				foreach (GetAssetsVersion.DeckModificationTimes deckModificationTimes in this.CachedDeckModificationTimes)
				{
					num += 1U;
					uint serializedSize2 = deckModificationTimes.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.HasCollectionVersionLastModified)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.CollectionVersionLastModified);
			}
			return num;
		}

		// Token: 0x0400024C RID: 588
		public bool HasPlatform;

		// Token: 0x0400024D RID: 589
		private Platform _Platform;

		// Token: 0x0400024E RID: 590
		public bool HasClientCollectionVersion;

		// Token: 0x0400024F RID: 591
		private long _ClientCollectionVersion;

		// Token: 0x04000250 RID: 592
		private List<GetAssetsVersion.DeckModificationTimes> _CachedDeckModificationTimes = new List<GetAssetsVersion.DeckModificationTimes>();

		// Token: 0x04000251 RID: 593
		public bool HasCollectionVersionLastModified;

		// Token: 0x04000252 RID: 594
		private long _CollectionVersionLastModified;

		// Token: 0x02000584 RID: 1412
		public enum PacketID
		{
			// Token: 0x04001EE8 RID: 7912
			ID = 303,
			// Token: 0x04001EE9 RID: 7913
			System = 0
		}

		// Token: 0x02000585 RID: 1413
		public class DeckModificationTimes : IProtoBuf
		{
			// Token: 0x1700127D RID: 4733
			// (get) Token: 0x06006179 RID: 24953 RVA: 0x0012667D File Offset: 0x0012487D
			// (set) Token: 0x0600617A RID: 24954 RVA: 0x00126685 File Offset: 0x00124885
			public long DeckId
			{
				get
				{
					return this._DeckId;
				}
				set
				{
					this._DeckId = value;
					this.HasDeckId = true;
				}
			}

			// Token: 0x1700127E RID: 4734
			// (get) Token: 0x0600617B RID: 24955 RVA: 0x00126695 File Offset: 0x00124895
			// (set) Token: 0x0600617C RID: 24956 RVA: 0x0012669D File Offset: 0x0012489D
			public long LastModified
			{
				get
				{
					return this._LastModified;
				}
				set
				{
					this._LastModified = value;
					this.HasLastModified = true;
				}
			}

			// Token: 0x0600617D RID: 24957 RVA: 0x001266B0 File Offset: 0x001248B0
			public override int GetHashCode()
			{
				int num = base.GetType().GetHashCode();
				if (this.HasDeckId)
				{
					num ^= this.DeckId.GetHashCode();
				}
				if (this.HasLastModified)
				{
					num ^= this.LastModified.GetHashCode();
				}
				return num;
			}

			// Token: 0x0600617E RID: 24958 RVA: 0x001266FC File Offset: 0x001248FC
			public override bool Equals(object obj)
			{
				GetAssetsVersion.DeckModificationTimes deckModificationTimes = obj as GetAssetsVersion.DeckModificationTimes;
				return deckModificationTimes != null && this.HasDeckId == deckModificationTimes.HasDeckId && (!this.HasDeckId || this.DeckId.Equals(deckModificationTimes.DeckId)) && this.HasLastModified == deckModificationTimes.HasLastModified && (!this.HasLastModified || this.LastModified.Equals(deckModificationTimes.LastModified));
			}

			// Token: 0x0600617F RID: 24959 RVA: 0x00126772 File Offset: 0x00124972
			public void Deserialize(Stream stream)
			{
				GetAssetsVersion.DeckModificationTimes.Deserialize(stream, this);
			}

			// Token: 0x06006180 RID: 24960 RVA: 0x0012677C File Offset: 0x0012497C
			public static GetAssetsVersion.DeckModificationTimes Deserialize(Stream stream, GetAssetsVersion.DeckModificationTimes instance)
			{
				return GetAssetsVersion.DeckModificationTimes.Deserialize(stream, instance, -1L);
			}

			// Token: 0x06006181 RID: 24961 RVA: 0x00126788 File Offset: 0x00124988
			public static GetAssetsVersion.DeckModificationTimes DeserializeLengthDelimited(Stream stream)
			{
				GetAssetsVersion.DeckModificationTimes deckModificationTimes = new GetAssetsVersion.DeckModificationTimes();
				GetAssetsVersion.DeckModificationTimes.DeserializeLengthDelimited(stream, deckModificationTimes);
				return deckModificationTimes;
			}

			// Token: 0x06006182 RID: 24962 RVA: 0x001267A4 File Offset: 0x001249A4
			public static GetAssetsVersion.DeckModificationTimes DeserializeLengthDelimited(Stream stream, GetAssetsVersion.DeckModificationTimes instance)
			{
				long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
				num += stream.Position;
				return GetAssetsVersion.DeckModificationTimes.Deserialize(stream, instance, num);
			}

			// Token: 0x06006183 RID: 24963 RVA: 0x001267CC File Offset: 0x001249CC
			public static GetAssetsVersion.DeckModificationTimes Deserialize(Stream stream, GetAssetsVersion.DeckModificationTimes instance, long limit)
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
							instance.LastModified = (long)ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.DeckId = (long)ProtocolParser.ReadUInt64(stream);
					}
				}
				if (stream.Position != limit)
				{
					throw new ProtocolBufferException("Read past max limit");
				}
				return instance;
			}

			// Token: 0x06006184 RID: 24964 RVA: 0x00126863 File Offset: 0x00124A63
			public void Serialize(Stream stream)
			{
				GetAssetsVersion.DeckModificationTimes.Serialize(stream, this);
			}

			// Token: 0x06006185 RID: 24965 RVA: 0x0012686C File Offset: 0x00124A6C
			public static void Serialize(Stream stream, GetAssetsVersion.DeckModificationTimes instance)
			{
				if (instance.HasDeckId)
				{
					stream.WriteByte(8);
					ProtocolParser.WriteUInt64(stream, (ulong)instance.DeckId);
				}
				if (instance.HasLastModified)
				{
					stream.WriteByte(16);
					ProtocolParser.WriteUInt64(stream, (ulong)instance.LastModified);
				}
			}

			// Token: 0x06006186 RID: 24966 RVA: 0x001268A8 File Offset: 0x00124AA8
			public uint GetSerializedSize()
			{
				uint num = 0U;
				if (this.HasDeckId)
				{
					num += 1U;
					num += ProtocolParser.SizeOfUInt64((ulong)this.DeckId);
				}
				if (this.HasLastModified)
				{
					num += 1U;
					num += ProtocolParser.SizeOfUInt64((ulong)this.LastModified);
				}
				return num;
			}

			// Token: 0x04001EEA RID: 7914
			public bool HasDeckId;

			// Token: 0x04001EEB RID: 7915
			private long _DeckId;

			// Token: 0x04001EEC RID: 7916
			public bool HasLastModified;

			// Token: 0x04001EED RID: 7917
			private long _LastModified;
		}
	}
}
