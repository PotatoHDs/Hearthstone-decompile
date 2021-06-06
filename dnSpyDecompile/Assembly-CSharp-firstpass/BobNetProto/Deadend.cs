using System;
using System.IO;
using System.Text;

namespace BobNetProto
{
	// Token: 0x020001E4 RID: 484
	public class Deadend : IProtoBuf
	{
		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001EB0 RID: 7856 RVA: 0x0006B54B File Offset: 0x0006974B
		// (set) Token: 0x06001EB1 RID: 7857 RVA: 0x0006B553 File Offset: 0x00069753
		public string Reply1
		{
			get
			{
				return this._Reply1;
			}
			set
			{
				this._Reply1 = value;
				this.HasReply1 = (value != null);
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001EB2 RID: 7858 RVA: 0x0006B566 File Offset: 0x00069766
		// (set) Token: 0x06001EB3 RID: 7859 RVA: 0x0006B56E File Offset: 0x0006976E
		public string Reply2
		{
			get
			{
				return this._Reply2;
			}
			set
			{
				this._Reply2 = value;
				this.HasReply2 = (value != null);
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001EB4 RID: 7860 RVA: 0x0006B581 File Offset: 0x00069781
		// (set) Token: 0x06001EB5 RID: 7861 RVA: 0x0006B589 File Offset: 0x00069789
		public string Reply3
		{
			get
			{
				return this._Reply3;
			}
			set
			{
				this._Reply3 = value;
				this.HasReply3 = (value != null);
			}
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x0006B59C File Offset: 0x0006979C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasReply1)
			{
				num ^= this.Reply1.GetHashCode();
			}
			if (this.HasReply2)
			{
				num ^= this.Reply2.GetHashCode();
			}
			if (this.HasReply3)
			{
				num ^= this.Reply3.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001EB7 RID: 7863 RVA: 0x0006B5F8 File Offset: 0x000697F8
		public override bool Equals(object obj)
		{
			Deadend deadend = obj as Deadend;
			return deadend != null && this.HasReply1 == deadend.HasReply1 && (!this.HasReply1 || this.Reply1.Equals(deadend.Reply1)) && this.HasReply2 == deadend.HasReply2 && (!this.HasReply2 || this.Reply2.Equals(deadend.Reply2)) && this.HasReply3 == deadend.HasReply3 && (!this.HasReply3 || this.Reply3.Equals(deadend.Reply3));
		}

		// Token: 0x06001EB8 RID: 7864 RVA: 0x0006B693 File Offset: 0x00069893
		public void Deserialize(Stream stream)
		{
			Deadend.Deserialize(stream, this);
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x0006B69D File Offset: 0x0006989D
		public static Deadend Deserialize(Stream stream, Deadend instance)
		{
			return Deadend.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x0006B6A8 File Offset: 0x000698A8
		public static Deadend DeserializeLengthDelimited(Stream stream)
		{
			Deadend deadend = new Deadend();
			Deadend.DeserializeLengthDelimited(stream, deadend);
			return deadend;
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x0006B6C4 File Offset: 0x000698C4
		public static Deadend DeserializeLengthDelimited(Stream stream, Deadend instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Deadend.Deserialize(stream, instance, num);
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x0006B6EC File Offset: 0x000698EC
		public static Deadend Deserialize(Stream stream, Deadend instance, long limit)
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
				else if (num != 10)
				{
					if (num != 18)
					{
						if (num != 26)
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
							instance.Reply3 = ProtocolParser.ReadString(stream);
						}
					}
					else
					{
						instance.Reply2 = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.Reply1 = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001EBD RID: 7869 RVA: 0x0006B79A File Offset: 0x0006999A
		public void Serialize(Stream stream)
		{
			Deadend.Serialize(stream, this);
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x0006B7A4 File Offset: 0x000699A4
		public static void Serialize(Stream stream, Deadend instance)
		{
			if (instance.HasReply1)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Reply1));
			}
			if (instance.HasReply2)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Reply2));
			}
			if (instance.HasReply3)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Reply3));
			}
		}

		// Token: 0x06001EBF RID: 7871 RVA: 0x0006B824 File Offset: 0x00069A24
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasReply1)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Reply1);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasReply2)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.Reply2);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasReply3)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.Reply3);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			return num;
		}

		// Token: 0x04000AF2 RID: 2802
		public bool HasReply1;

		// Token: 0x04000AF3 RID: 2803
		private string _Reply1;

		// Token: 0x04000AF4 RID: 2804
		public bool HasReply2;

		// Token: 0x04000AF5 RID: 2805
		private string _Reply2;

		// Token: 0x04000AF6 RID: 2806
		public bool HasReply3;

		// Token: 0x04000AF7 RID: 2807
		private string _Reply3;

		// Token: 0x02000673 RID: 1651
		public enum PacketID
		{
			// Token: 0x04002180 RID: 8576
			ID = 169
		}
	}
}
