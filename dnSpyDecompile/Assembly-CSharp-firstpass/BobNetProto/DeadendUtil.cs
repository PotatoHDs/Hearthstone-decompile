using System;
using System.IO;
using System.Text;

namespace BobNetProto
{
	// Token: 0x020001E5 RID: 485
	public class DeadendUtil : IProtoBuf
	{
		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001EC1 RID: 7873 RVA: 0x0006B8AC File Offset: 0x00069AAC
		// (set) Token: 0x06001EC2 RID: 7874 RVA: 0x0006B8B4 File Offset: 0x00069AB4
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

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001EC3 RID: 7875 RVA: 0x0006B8C7 File Offset: 0x00069AC7
		// (set) Token: 0x06001EC4 RID: 7876 RVA: 0x0006B8CF File Offset: 0x00069ACF
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

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06001EC5 RID: 7877 RVA: 0x0006B8E2 File Offset: 0x00069AE2
		// (set) Token: 0x06001EC6 RID: 7878 RVA: 0x0006B8EA File Offset: 0x00069AEA
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

		// Token: 0x06001EC7 RID: 7879 RVA: 0x0006B900 File Offset: 0x00069B00
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

		// Token: 0x06001EC8 RID: 7880 RVA: 0x0006B95C File Offset: 0x00069B5C
		public override bool Equals(object obj)
		{
			DeadendUtil deadendUtil = obj as DeadendUtil;
			return deadendUtil != null && this.HasReply1 == deadendUtil.HasReply1 && (!this.HasReply1 || this.Reply1.Equals(deadendUtil.Reply1)) && this.HasReply2 == deadendUtil.HasReply2 && (!this.HasReply2 || this.Reply2.Equals(deadendUtil.Reply2)) && this.HasReply3 == deadendUtil.HasReply3 && (!this.HasReply3 || this.Reply3.Equals(deadendUtil.Reply3));
		}

		// Token: 0x06001EC9 RID: 7881 RVA: 0x0006B9F7 File Offset: 0x00069BF7
		public void Deserialize(Stream stream)
		{
			DeadendUtil.Deserialize(stream, this);
		}

		// Token: 0x06001ECA RID: 7882 RVA: 0x0006BA01 File Offset: 0x00069C01
		public static DeadendUtil Deserialize(Stream stream, DeadendUtil instance)
		{
			return DeadendUtil.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001ECB RID: 7883 RVA: 0x0006BA0C File Offset: 0x00069C0C
		public static DeadendUtil DeserializeLengthDelimited(Stream stream)
		{
			DeadendUtil deadendUtil = new DeadendUtil();
			DeadendUtil.DeserializeLengthDelimited(stream, deadendUtil);
			return deadendUtil;
		}

		// Token: 0x06001ECC RID: 7884 RVA: 0x0006BA28 File Offset: 0x00069C28
		public static DeadendUtil DeserializeLengthDelimited(Stream stream, DeadendUtil instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DeadendUtil.Deserialize(stream, instance, num);
		}

		// Token: 0x06001ECD RID: 7885 RVA: 0x0006BA50 File Offset: 0x00069C50
		public static DeadendUtil Deserialize(Stream stream, DeadendUtil instance, long limit)
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

		// Token: 0x06001ECE RID: 7886 RVA: 0x0006BAFE File Offset: 0x00069CFE
		public void Serialize(Stream stream)
		{
			DeadendUtil.Serialize(stream, this);
		}

		// Token: 0x06001ECF RID: 7887 RVA: 0x0006BB08 File Offset: 0x00069D08
		public static void Serialize(Stream stream, DeadendUtil instance)
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

		// Token: 0x06001ED0 RID: 7888 RVA: 0x0006BB88 File Offset: 0x00069D88
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

		// Token: 0x04000AF8 RID: 2808
		public bool HasReply1;

		// Token: 0x04000AF9 RID: 2809
		private string _Reply1;

		// Token: 0x04000AFA RID: 2810
		public bool HasReply2;

		// Token: 0x04000AFB RID: 2811
		private string _Reply2;

		// Token: 0x04000AFC RID: 2812
		public bool HasReply3;

		// Token: 0x04000AFD RID: 2813
		private string _Reply3;

		// Token: 0x02000674 RID: 1652
		public enum PacketID
		{
			// Token: 0x04002182 RID: 8578
			ID = 167
		}
	}
}
