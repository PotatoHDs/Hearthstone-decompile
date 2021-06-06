using System;
using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x0200009C RID: 156
	public class Boosters : IProtoBuf
	{
		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000A76 RID: 2678 RVA: 0x000269C3 File Offset: 0x00024BC3
		// (set) Token: 0x06000A77 RID: 2679 RVA: 0x000269CB File Offset: 0x00024BCB
		public List<BoosterInfo> List
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

		// Token: 0x06000A78 RID: 2680 RVA: 0x000269D4 File Offset: 0x00024BD4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (BoosterInfo boosterInfo in this.List)
			{
				num ^= boosterInfo.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x00026A38 File Offset: 0x00024C38
		public override bool Equals(object obj)
		{
			Boosters boosters = obj as Boosters;
			if (boosters == null)
			{
				return false;
			}
			if (this.List.Count != boosters.List.Count)
			{
				return false;
			}
			for (int i = 0; i < this.List.Count; i++)
			{
				if (!this.List[i].Equals(boosters.List[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00026AA3 File Offset: 0x00024CA3
		public void Deserialize(Stream stream)
		{
			Boosters.Deserialize(stream, this);
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x00026AAD File Offset: 0x00024CAD
		public static Boosters Deserialize(Stream stream, Boosters instance)
		{
			return Boosters.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x00026AB8 File Offset: 0x00024CB8
		public static Boosters DeserializeLengthDelimited(Stream stream)
		{
			Boosters boosters = new Boosters();
			Boosters.DeserializeLengthDelimited(stream, boosters);
			return boosters;
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x00026AD4 File Offset: 0x00024CD4
		public static Boosters DeserializeLengthDelimited(Stream stream, Boosters instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Boosters.Deserialize(stream, instance, num);
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x00026AFC File Offset: 0x00024CFC
		public static Boosters Deserialize(Stream stream, Boosters instance, long limit)
		{
			if (instance.List == null)
			{
				instance.List = new List<BoosterInfo>();
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
					instance.List.Add(BoosterInfo.DeserializeLengthDelimited(stream));
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

		// Token: 0x06000A7F RID: 2687 RVA: 0x00026B94 File Offset: 0x00024D94
		public void Serialize(Stream stream)
		{
			Boosters.Serialize(stream, this);
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x00026BA0 File Offset: 0x00024DA0
		public static void Serialize(Stream stream, Boosters instance)
		{
			if (instance.List.Count > 0)
			{
				foreach (BoosterInfo boosterInfo in instance.List)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, boosterInfo.GetSerializedSize());
					BoosterInfo.Serialize(stream, boosterInfo);
				}
			}
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x00026C18 File Offset: 0x00024E18
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.List.Count > 0)
			{
				foreach (BoosterInfo boosterInfo in this.List)
				{
					num += 1U;
					uint serializedSize = boosterInfo.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x04000391 RID: 913
		private List<BoosterInfo> _List = new List<BoosterInfo>();
	}
}
