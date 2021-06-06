using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.diag.v1
{
	// Token: 0x0200043A RID: 1082
	public class Row : IProtoBuf
	{
		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x06004931 RID: 18737 RVA: 0x000E4B07 File Offset: 0x000E2D07
		// (set) Token: 0x06004932 RID: 18738 RVA: 0x000E4B0F File Offset: 0x000E2D0F
		public List<string> Values
		{
			get
			{
				return this._Values;
			}
			set
			{
				this._Values = value;
			}
		}

		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x06004933 RID: 18739 RVA: 0x000E4B07 File Offset: 0x000E2D07
		public List<string> ValuesList
		{
			get
			{
				return this._Values;
			}
		}

		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x06004934 RID: 18740 RVA: 0x000E4B18 File Offset: 0x000E2D18
		public int ValuesCount
		{
			get
			{
				return this._Values.Count;
			}
		}

		// Token: 0x06004935 RID: 18741 RVA: 0x000E4B25 File Offset: 0x000E2D25
		public void AddValues(string val)
		{
			this._Values.Add(val);
		}

		// Token: 0x06004936 RID: 18742 RVA: 0x000E4B33 File Offset: 0x000E2D33
		public void ClearValues()
		{
			this._Values.Clear();
		}

		// Token: 0x06004937 RID: 18743 RVA: 0x000E4B40 File Offset: 0x000E2D40
		public void SetValues(List<string> val)
		{
			this.Values = val;
		}

		// Token: 0x06004938 RID: 18744 RVA: 0x000E4B4C File Offset: 0x000E2D4C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (string text in this.Values)
			{
				num ^= text.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004939 RID: 18745 RVA: 0x000E4BB0 File Offset: 0x000E2DB0
		public override bool Equals(object obj)
		{
			Row row = obj as Row;
			if (row == null)
			{
				return false;
			}
			if (this.Values.Count != row.Values.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Values.Count; i++)
			{
				if (!this.Values[i].Equals(row.Values[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x0600493A RID: 18746 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600493B RID: 18747 RVA: 0x000E4C1B File Offset: 0x000E2E1B
		public static Row ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Row>(bs, 0, -1);
		}

		// Token: 0x0600493C RID: 18748 RVA: 0x000E4C25 File Offset: 0x000E2E25
		public void Deserialize(Stream stream)
		{
			Row.Deserialize(stream, this);
		}

		// Token: 0x0600493D RID: 18749 RVA: 0x000E4C2F File Offset: 0x000E2E2F
		public static Row Deserialize(Stream stream, Row instance)
		{
			return Row.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600493E RID: 18750 RVA: 0x000E4C3C File Offset: 0x000E2E3C
		public static Row DeserializeLengthDelimited(Stream stream)
		{
			Row row = new Row();
			Row.DeserializeLengthDelimited(stream, row);
			return row;
		}

		// Token: 0x0600493F RID: 18751 RVA: 0x000E4C58 File Offset: 0x000E2E58
		public static Row DeserializeLengthDelimited(Stream stream, Row instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Row.Deserialize(stream, instance, num);
		}

		// Token: 0x06004940 RID: 18752 RVA: 0x000E4C80 File Offset: 0x000E2E80
		public static Row Deserialize(Stream stream, Row instance, long limit)
		{
			if (instance.Values == null)
			{
				instance.Values = new List<string>();
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
					instance.Values.Add(ProtocolParser.ReadString(stream));
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

		// Token: 0x06004941 RID: 18753 RVA: 0x000E4D18 File Offset: 0x000E2F18
		public void Serialize(Stream stream)
		{
			Row.Serialize(stream, this);
		}

		// Token: 0x06004942 RID: 18754 RVA: 0x000E4D24 File Offset: 0x000E2F24
		public static void Serialize(Stream stream, Row instance)
		{
			if (instance.Values.Count > 0)
			{
				foreach (string s in instance.Values)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(s));
				}
			}
		}

		// Token: 0x06004943 RID: 18755 RVA: 0x000E4D98 File Offset: 0x000E2F98
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Values.Count > 0)
			{
				foreach (string s in this.Values)
				{
					num += 1U;
					uint byteCount = (uint)Encoding.UTF8.GetByteCount(s);
					num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
				}
			}
			return num;
		}

		// Token: 0x0400182D RID: 6189
		private List<string> _Values = new List<string>();
	}
}
