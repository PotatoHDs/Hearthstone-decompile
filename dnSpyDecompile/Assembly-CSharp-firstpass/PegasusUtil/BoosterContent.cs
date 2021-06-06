using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000A7 RID: 167
	public class BoosterContent : IProtoBuf
	{
		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x00029D67 File Offset: 0x00027F67
		// (set) Token: 0x06000B3E RID: 2878 RVA: 0x00029D6F File Offset: 0x00027F6F
		public List<BoosterCard> List
		{
			get
			{
				return this._List;
			}
			set
			{
				this._List = value;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x00029D78 File Offset: 0x00027F78
		// (set) Token: 0x06000B40 RID: 2880 RVA: 0x00029D80 File Offset: 0x00027F80
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

		// Token: 0x06000B41 RID: 2881 RVA: 0x00029D90 File Offset: 0x00027F90
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (BoosterCard boosterCard in this.List)
			{
				num ^= boosterCard.GetHashCode();
			}
			if (this.HasCollectionVersion)
			{
				num ^= this.CollectionVersion.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x00029E0C File Offset: 0x0002800C
		public override bool Equals(object obj)
		{
			BoosterContent boosterContent = obj as BoosterContent;
			if (boosterContent == null)
			{
				return false;
			}
			if (this.List.Count != boosterContent.List.Count)
			{
				return false;
			}
			for (int i = 0; i < this.List.Count; i++)
			{
				if (!this.List[i].Equals(boosterContent.List[i]))
				{
					return false;
				}
			}
			return this.HasCollectionVersion == boosterContent.HasCollectionVersion && (!this.HasCollectionVersion || this.CollectionVersion.Equals(boosterContent.CollectionVersion));
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x00029EA5 File Offset: 0x000280A5
		public void Deserialize(Stream stream)
		{
			BoosterContent.Deserialize(stream, this);
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00029EAF File Offset: 0x000280AF
		public static BoosterContent Deserialize(Stream stream, BoosterContent instance)
		{
			return BoosterContent.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x00029EBC File Offset: 0x000280BC
		public static BoosterContent DeserializeLengthDelimited(Stream stream)
		{
			BoosterContent boosterContent = new BoosterContent();
			BoosterContent.DeserializeLengthDelimited(stream, boosterContent);
			return boosterContent;
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x00029ED8 File Offset: 0x000280D8
		public static BoosterContent DeserializeLengthDelimited(Stream stream, BoosterContent instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BoosterContent.Deserialize(stream, instance, num);
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00029F00 File Offset: 0x00028100
		public static BoosterContent Deserialize(Stream stream, BoosterContent instance, long limit)
		{
			if (instance.List == null)
			{
				instance.List = new List<BoosterCard>();
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
					instance.List.Add(BoosterCard.DeserializeLengthDelimited(stream));
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x00029FB0 File Offset: 0x000281B0
		public void Serialize(Stream stream)
		{
			BoosterContent.Serialize(stream, this);
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00029FBC File Offset: 0x000281BC
		public static void Serialize(Stream stream, BoosterContent instance)
		{
			if (instance.List.Count > 0)
			{
				foreach (BoosterCard boosterCard in instance.List)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, boosterCard.GetSerializedSize());
					BoosterCard.Serialize(stream, boosterCard);
				}
			}
			if (instance.HasCollectionVersion)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CollectionVersion);
			}
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x0002A050 File Offset: 0x00028250
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.List.Count > 0)
			{
				foreach (BoosterCard boosterCard in this.List)
				{
					num += 1U;
					uint serializedSize = boosterCard.GetSerializedSize();
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

		// Token: 0x040003CC RID: 972
		private List<BoosterCard> _List = new List<BoosterCard>();

		// Token: 0x040003CD RID: 973
		public bool HasCollectionVersion;

		// Token: 0x040003CE RID: 974
		private long _CollectionVersion;

		// Token: 0x020005B0 RID: 1456
		public enum PacketID
		{
			// Token: 0x04001F66 RID: 8038
			ID = 226
		}
	}
}
