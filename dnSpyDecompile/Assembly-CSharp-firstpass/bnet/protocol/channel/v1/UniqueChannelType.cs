using System;
using System.IO;
using System.Text;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004E1 RID: 1249
	public class UniqueChannelType : IProtoBuf
	{
		// Token: 0x1700109D RID: 4253
		// (get) Token: 0x06005830 RID: 22576 RVA: 0x0010E3C5 File Offset: 0x0010C5C5
		// (set) Token: 0x06005831 RID: 22577 RVA: 0x0010E3CD File Offset: 0x0010C5CD
		public uint ServiceType
		{
			get
			{
				return this._ServiceType;
			}
			set
			{
				this._ServiceType = value;
				this.HasServiceType = true;
			}
		}

		// Token: 0x06005832 RID: 22578 RVA: 0x0010E3DD File Offset: 0x0010C5DD
		public void SetServiceType(uint val)
		{
			this.ServiceType = val;
		}

		// Token: 0x1700109E RID: 4254
		// (get) Token: 0x06005833 RID: 22579 RVA: 0x0010E3E6 File Offset: 0x0010C5E6
		// (set) Token: 0x06005834 RID: 22580 RVA: 0x0010E3EE File Offset: 0x0010C5EE
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

		// Token: 0x06005835 RID: 22581 RVA: 0x0010E3FE File Offset: 0x0010C5FE
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x1700109F RID: 4255
		// (get) Token: 0x06005836 RID: 22582 RVA: 0x0010E407 File Offset: 0x0010C607
		// (set) Token: 0x06005837 RID: 22583 RVA: 0x0010E40F File Offset: 0x0010C60F
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

		// Token: 0x06005838 RID: 22584 RVA: 0x0010E422 File Offset: 0x0010C622
		public void SetChannelType(string val)
		{
			this.ChannelType = val;
		}

		// Token: 0x06005839 RID: 22585 RVA: 0x0010E42C File Offset: 0x0010C62C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasServiceType)
			{
				num ^= this.ServiceType.GetHashCode();
			}
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

		// Token: 0x0600583A RID: 22586 RVA: 0x0010E490 File Offset: 0x0010C690
		public override bool Equals(object obj)
		{
			UniqueChannelType uniqueChannelType = obj as UniqueChannelType;
			return uniqueChannelType != null && this.HasServiceType == uniqueChannelType.HasServiceType && (!this.HasServiceType || this.ServiceType.Equals(uniqueChannelType.ServiceType)) && this.HasProgram == uniqueChannelType.HasProgram && (!this.HasProgram || this.Program.Equals(uniqueChannelType.Program)) && this.HasChannelType == uniqueChannelType.HasChannelType && (!this.HasChannelType || this.ChannelType.Equals(uniqueChannelType.ChannelType));
		}

		// Token: 0x170010A0 RID: 4256
		// (get) Token: 0x0600583B RID: 22587 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600583C RID: 22588 RVA: 0x0010E531 File Offset: 0x0010C731
		public static UniqueChannelType ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UniqueChannelType>(bs, 0, -1);
		}

		// Token: 0x0600583D RID: 22589 RVA: 0x0010E53B File Offset: 0x0010C73B
		public void Deserialize(Stream stream)
		{
			UniqueChannelType.Deserialize(stream, this);
		}

		// Token: 0x0600583E RID: 22590 RVA: 0x0010E545 File Offset: 0x0010C745
		public static UniqueChannelType Deserialize(Stream stream, UniqueChannelType instance)
		{
			return UniqueChannelType.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600583F RID: 22591 RVA: 0x0010E550 File Offset: 0x0010C750
		public static UniqueChannelType DeserializeLengthDelimited(Stream stream)
		{
			UniqueChannelType uniqueChannelType = new UniqueChannelType();
			UniqueChannelType.DeserializeLengthDelimited(stream, uniqueChannelType);
			return uniqueChannelType;
		}

		// Token: 0x06005840 RID: 22592 RVA: 0x0010E56C File Offset: 0x0010C76C
		public static UniqueChannelType DeserializeLengthDelimited(Stream stream, UniqueChannelType instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UniqueChannelType.Deserialize(stream, instance, num);
		}

		// Token: 0x06005841 RID: 22593 RVA: 0x0010E594 File Offset: 0x0010C794
		public static UniqueChannelType Deserialize(Stream stream, UniqueChannelType instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.ChannelType = "default";
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
					if (num != 21)
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
				else
				{
					instance.ServiceType = ProtocolParser.ReadUInt32(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06005842 RID: 22594 RVA: 0x0010E653 File Offset: 0x0010C853
		public void Serialize(Stream stream)
		{
			UniqueChannelType.Serialize(stream, this);
		}

		// Token: 0x06005843 RID: 22595 RVA: 0x0010E65C File Offset: 0x0010C85C
		public static void Serialize(Stream stream, UniqueChannelType instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasServiceType)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.ServiceType);
			}
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

		// Token: 0x06005844 RID: 22596 RVA: 0x0010E6D0 File Offset: 0x0010C8D0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasServiceType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.ServiceType);
			}
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

		// Token: 0x04001BA5 RID: 7077
		public bool HasServiceType;

		// Token: 0x04001BA6 RID: 7078
		private uint _ServiceType;

		// Token: 0x04001BA7 RID: 7079
		public bool HasProgram;

		// Token: 0x04001BA8 RID: 7080
		private uint _Program;

		// Token: 0x04001BA9 RID: 7081
		public bool HasChannelType;

		// Token: 0x04001BAA RID: 7082
		private string _ChannelType;
	}
}
