using System;
using System.IO;
using System.Text;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000471 RID: 1137
	public class UniqueChannelType : IProtoBuf
	{
		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x06004E3C RID: 20028 RVA: 0x000F2DDF File Offset: 0x000F0FDF
		// (set) Token: 0x06004E3D RID: 20029 RVA: 0x000F2DE7 File Offset: 0x000F0FE7
		public uint Program
		{
			get
			{
				return this._Program;
			}
			set
			{
				this._Program = value;
				this.HasProgram = true;
			}
		}

		// Token: 0x06004E3E RID: 20030 RVA: 0x000F2DF7 File Offset: 0x000F0FF7
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x06004E3F RID: 20031 RVA: 0x000F2E00 File Offset: 0x000F1000
		// (set) Token: 0x06004E40 RID: 20032 RVA: 0x000F2E08 File Offset: 0x000F1008
		public string ChannelType
		{
			get
			{
				return this._ChannelType;
			}
			set
			{
				this._ChannelType = value;
				this.HasChannelType = (value != null);
			}
		}

		// Token: 0x06004E41 RID: 20033 RVA: 0x000F2E1B File Offset: 0x000F101B
		public void SetChannelType(string val)
		{
			this.ChannelType = val;
		}

		// Token: 0x06004E42 RID: 20034 RVA: 0x000F2E24 File Offset: 0x000F1024
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			if (this.HasChannelType)
			{
				num ^= this.ChannelType.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004E43 RID: 20035 RVA: 0x000F2E70 File Offset: 0x000F1070
		public override bool Equals(object obj)
		{
			UniqueChannelType uniqueChannelType = obj as UniqueChannelType;
			return uniqueChannelType != null && this.HasProgram == uniqueChannelType.HasProgram && (!this.HasProgram || this.Program.Equals(uniqueChannelType.Program)) && this.HasChannelType == uniqueChannelType.HasChannelType && (!this.HasChannelType || this.ChannelType.Equals(uniqueChannelType.ChannelType));
		}

		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x06004E44 RID: 20036 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004E45 RID: 20037 RVA: 0x000F2EE3 File Offset: 0x000F10E3
		public static UniqueChannelType ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UniqueChannelType>(bs, 0, -1);
		}

		// Token: 0x06004E46 RID: 20038 RVA: 0x000F2EED File Offset: 0x000F10ED
		public void Deserialize(Stream stream)
		{
			UniqueChannelType.Deserialize(stream, this);
		}

		// Token: 0x06004E47 RID: 20039 RVA: 0x000F2EF7 File Offset: 0x000F10F7
		public static UniqueChannelType Deserialize(Stream stream, UniqueChannelType instance)
		{
			return UniqueChannelType.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004E48 RID: 20040 RVA: 0x000F2F04 File Offset: 0x000F1104
		public static UniqueChannelType DeserializeLengthDelimited(Stream stream)
		{
			UniqueChannelType uniqueChannelType = new UniqueChannelType();
			UniqueChannelType.DeserializeLengthDelimited(stream, uniqueChannelType);
			return uniqueChannelType;
		}

		// Token: 0x06004E49 RID: 20041 RVA: 0x000F2F20 File Offset: 0x000F1120
		public static UniqueChannelType DeserializeLengthDelimited(Stream stream, UniqueChannelType instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UniqueChannelType.Deserialize(stream, instance, num);
		}

		// Token: 0x06004E4A RID: 20042 RVA: 0x000F2F48 File Offset: 0x000F1148
		public static UniqueChannelType Deserialize(Stream stream, UniqueChannelType instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
				else if (num != 21)
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
						instance.ChannelType = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.Program = binaryReader.ReadUInt32();
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06004E4B RID: 20043 RVA: 0x000F2FE7 File Offset: 0x000F11E7
		public void Serialize(Stream stream)
		{
			UniqueChannelType.Serialize(stream, this);
		}

		// Token: 0x06004E4C RID: 20044 RVA: 0x000F2FF0 File Offset: 0x000F11F0
		public static void Serialize(Stream stream, UniqueChannelType instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasProgram)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.Program);
			}
			if (instance.HasChannelType)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ChannelType));
			}
		}

		// Token: 0x06004E4D RID: 20045 RVA: 0x000F3048 File Offset: 0x000F1248
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasProgram)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasChannelType)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ChannelType);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x04001970 RID: 6512
		public bool HasProgram;

		// Token: 0x04001971 RID: 6513
		private uint _Program;

		// Token: 0x04001972 RID: 6514
		public bool HasChannelType;

		// Token: 0x04001973 RID: 6515
		private string _ChannelType;
	}
}
