using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x0200009B RID: 155
	public class Achieves : IProtoBuf
	{
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x000266E7 File Offset: 0x000248E7
		// (set) Token: 0x06000A6A RID: 2666 RVA: 0x000266EF File Offset: 0x000248EF
		public List<Achieve> List
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

		// Token: 0x06000A6B RID: 2667 RVA: 0x000266F8 File Offset: 0x000248F8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (Achieve achieve in this.List)
			{
				num ^= achieve.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0002675C File Offset: 0x0002495C
		public override bool Equals(object obj)
		{
			Achieves achieves = obj as Achieves;
			if (achieves == null)
			{
				return false;
			}
			if (this.List.Count != achieves.List.Count)
			{
				return false;
			}
			for (int i = 0; i < this.List.Count; i++)
			{
				if (!this.List[i].Equals(achieves.List[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x000267C7 File Offset: 0x000249C7
		public void Deserialize(Stream stream)
		{
			Achieves.Deserialize(stream, this);
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x000267D1 File Offset: 0x000249D1
		public static Achieves Deserialize(Stream stream, Achieves instance)
		{
			return Achieves.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x000267DC File Offset: 0x000249DC
		public static Achieves DeserializeLengthDelimited(Stream stream)
		{
			Achieves achieves = new Achieves();
			Achieves.DeserializeLengthDelimited(stream, achieves);
			return achieves;
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x000267F8 File Offset: 0x000249F8
		public static Achieves DeserializeLengthDelimited(Stream stream, Achieves instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Achieves.Deserialize(stream, instance, num);
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00026820 File Offset: 0x00024A20
		public static Achieves Deserialize(Stream stream, Achieves instance, long limit)
		{
			if (instance.List == null)
			{
				instance.List = new List<Achieve>();
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
					instance.List.Add(Achieve.DeserializeLengthDelimited(stream));
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

		// Token: 0x06000A72 RID: 2674 RVA: 0x000268B8 File Offset: 0x00024AB8
		public void Serialize(Stream stream)
		{
			Achieves.Serialize(stream, this);
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x000268C4 File Offset: 0x00024AC4
		public static void Serialize(Stream stream, Achieves instance)
		{
			if (instance.List.Count > 0)
			{
				foreach (Achieve achieve in instance.List)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, achieve.GetSerializedSize());
					Achieve.Serialize(stream, achieve);
				}
			}
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0002693C File Offset: 0x00024B3C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.List.Count > 0)
			{
				foreach (Achieve achieve in this.List)
				{
					num += 1U;
					uint serializedSize = achieve.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x04000390 RID: 912
		private List<Achieve> _List = new List<Achieve>();
	}
}
