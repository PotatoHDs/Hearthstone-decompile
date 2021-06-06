using System;
using System.IO;
using System.Text;

namespace bnet.protocol
{
	// Token: 0x020002A2 RID: 674
	public class Provider : IProtoBuf
	{
		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06002694 RID: 9876 RVA: 0x0008979C File Offset: 0x0008799C
		// (set) Token: 0x06002695 RID: 9877 RVA: 0x000897A4 File Offset: 0x000879A4
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				this._Name = value;
				this.HasName = (value != null);
			}
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x000897B7 File Offset: 0x000879B7
		public void SetName(string val)
		{
			this.Name = val;
		}

		// Token: 0x06002697 RID: 9879 RVA: 0x000897C0 File Offset: 0x000879C0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasName)
			{
				num ^= this.Name.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002698 RID: 9880 RVA: 0x000897F0 File Offset: 0x000879F0
		public override bool Equals(object obj)
		{
			Provider provider = obj as Provider;
			return provider != null && this.HasName == provider.HasName && (!this.HasName || this.Name.Equals(provider.Name));
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06002699 RID: 9881 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600269A RID: 9882 RVA: 0x00089835 File Offset: 0x00087A35
		public static Provider ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Provider>(bs, 0, -1);
		}

		// Token: 0x0600269B RID: 9883 RVA: 0x0008983F File Offset: 0x00087A3F
		public void Deserialize(Stream stream)
		{
			Provider.Deserialize(stream, this);
		}

		// Token: 0x0600269C RID: 9884 RVA: 0x00089849 File Offset: 0x00087A49
		public static Provider Deserialize(Stream stream, Provider instance)
		{
			return Provider.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600269D RID: 9885 RVA: 0x00089854 File Offset: 0x00087A54
		public static Provider DeserializeLengthDelimited(Stream stream)
		{
			Provider provider = new Provider();
			Provider.DeserializeLengthDelimited(stream, provider);
			return provider;
		}

		// Token: 0x0600269E RID: 9886 RVA: 0x00089870 File Offset: 0x00087A70
		public static Provider DeserializeLengthDelimited(Stream stream, Provider instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Provider.Deserialize(stream, instance, num);
		}

		// Token: 0x0600269F RID: 9887 RVA: 0x00089898 File Offset: 0x00087A98
		public static Provider Deserialize(Stream stream, Provider instance, long limit)
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
				else if (num == 10)
				{
					instance.Name = ProtocolParser.ReadString(stream);
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

		// Token: 0x060026A0 RID: 9888 RVA: 0x00089918 File Offset: 0x00087B18
		public void Serialize(Stream stream)
		{
			Provider.Serialize(stream, this);
		}

		// Token: 0x060026A1 RID: 9889 RVA: 0x00089921 File Offset: 0x00087B21
		public static void Serialize(Stream stream, Provider instance)
		{
			if (instance.HasName)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			}
		}

		// Token: 0x060026A2 RID: 9890 RVA: 0x0008994C File Offset: 0x00087B4C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasName)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x040010FA RID: 4346
		public bool HasName;

		// Token: 0x040010FB RID: 4347
		private string _Name;
	}
}
