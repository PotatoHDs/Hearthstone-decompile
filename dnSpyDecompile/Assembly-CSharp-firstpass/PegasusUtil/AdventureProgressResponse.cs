using System;
using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x020000BB RID: 187
	public class AdventureProgressResponse : IProtoBuf
	{
		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x00030006 File Offset: 0x0002E206
		// (set) Token: 0x06000CE4 RID: 3300 RVA: 0x0003000E File Offset: 0x0002E20E
		public List<AdventureProgress> List
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

		// Token: 0x06000CE5 RID: 3301 RVA: 0x00030018 File Offset: 0x0002E218
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (AdventureProgress adventureProgress in this.List)
			{
				num ^= adventureProgress.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0003007C File Offset: 0x0002E27C
		public override bool Equals(object obj)
		{
			AdventureProgressResponse adventureProgressResponse = obj as AdventureProgressResponse;
			if (adventureProgressResponse == null)
			{
				return false;
			}
			if (this.List.Count != adventureProgressResponse.List.Count)
			{
				return false;
			}
			for (int i = 0; i < this.List.Count; i++)
			{
				if (!this.List[i].Equals(adventureProgressResponse.List[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x000300E7 File Offset: 0x0002E2E7
		public void Deserialize(Stream stream)
		{
			AdventureProgressResponse.Deserialize(stream, this);
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x000300F1 File Offset: 0x0002E2F1
		public static AdventureProgressResponse Deserialize(Stream stream, AdventureProgressResponse instance)
		{
			return AdventureProgressResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x000300FC File Offset: 0x0002E2FC
		public static AdventureProgressResponse DeserializeLengthDelimited(Stream stream)
		{
			AdventureProgressResponse adventureProgressResponse = new AdventureProgressResponse();
			AdventureProgressResponse.DeserializeLengthDelimited(stream, adventureProgressResponse);
			return adventureProgressResponse;
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x00030118 File Offset: 0x0002E318
		public static AdventureProgressResponse DeserializeLengthDelimited(Stream stream, AdventureProgressResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AdventureProgressResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x00030140 File Offset: 0x0002E340
		public static AdventureProgressResponse Deserialize(Stream stream, AdventureProgressResponse instance, long limit)
		{
			if (instance.List == null)
			{
				instance.List = new List<AdventureProgress>();
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
					instance.List.Add(AdventureProgress.DeserializeLengthDelimited(stream));
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

		// Token: 0x06000CEC RID: 3308 RVA: 0x000301D8 File Offset: 0x0002E3D8
		public void Serialize(Stream stream)
		{
			AdventureProgressResponse.Serialize(stream, this);
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x000301E4 File Offset: 0x0002E3E4
		public static void Serialize(Stream stream, AdventureProgressResponse instance)
		{
			if (instance.List.Count > 0)
			{
				foreach (AdventureProgress adventureProgress in instance.List)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, adventureProgress.GetSerializedSize());
					AdventureProgress.Serialize(stream, adventureProgress);
				}
			}
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x0003025C File Offset: 0x0002E45C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.List.Count > 0)
			{
				foreach (AdventureProgress adventureProgress in this.List)
				{
					num += 1U;
					uint serializedSize = adventureProgress.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x04000472 RID: 1138
		private List<AdventureProgress> _List = new List<AdventureProgress>();

		// Token: 0x020005C8 RID: 1480
		public enum PacketID
		{
			// Token: 0x04001FA4 RID: 8100
			ID = 306
		}
	}
}
