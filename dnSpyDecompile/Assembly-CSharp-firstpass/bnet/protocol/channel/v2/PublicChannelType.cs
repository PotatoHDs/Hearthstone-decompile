using System;
using System.IO;
using System.Text;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000477 RID: 1143
	public class PublicChannelType : IProtoBuf
	{
		// Token: 0x17000EC3 RID: 3779
		// (get) Token: 0x06004ED8 RID: 20184 RVA: 0x000F4B93 File Offset: 0x000F2D93
		// (set) Token: 0x06004ED9 RID: 20185 RVA: 0x000F4B9B File Offset: 0x000F2D9B
		public UniqueChannelType Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
				this.HasType = (value != null);
			}
		}

		// Token: 0x06004EDA RID: 20186 RVA: 0x000F4BAE File Offset: 0x000F2DAE
		public void SetType(UniqueChannelType val)
		{
			this.Type = val;
		}

		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x06004EDB RID: 20187 RVA: 0x000F4BB7 File Offset: 0x000F2DB7
		// (set) Token: 0x06004EDC RID: 20188 RVA: 0x000F4BBF File Offset: 0x000F2DBF
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

		// Token: 0x06004EDD RID: 20189 RVA: 0x000F4BD2 File Offset: 0x000F2DD2
		public void SetName(string val)
		{
			this.Name = val;
		}

		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x06004EDE RID: 20190 RVA: 0x000F4BDB File Offset: 0x000F2DDB
		// (set) Token: 0x06004EDF RID: 20191 RVA: 0x000F4BE3 File Offset: 0x000F2DE3
		public string Identity
		{
			get
			{
				return this._Identity;
			}
			set
			{
				this._Identity = value;
				this.HasIdentity = (value != null);
			}
		}

		// Token: 0x06004EE0 RID: 20192 RVA: 0x000F4BF6 File Offset: 0x000F2DF6
		public void SetIdentity(string val)
		{
			this.Identity = val;
		}

		// Token: 0x06004EE1 RID: 20193 RVA: 0x000F4C00 File Offset: 0x000F2E00
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasType)
			{
				num ^= this.Type.GetHashCode();
			}
			if (this.HasName)
			{
				num ^= this.Name.GetHashCode();
			}
			if (this.HasIdentity)
			{
				num ^= this.Identity.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004EE2 RID: 20194 RVA: 0x000F4C5C File Offset: 0x000F2E5C
		public override bool Equals(object obj)
		{
			PublicChannelType publicChannelType = obj as PublicChannelType;
			return publicChannelType != null && this.HasType == publicChannelType.HasType && (!this.HasType || this.Type.Equals(publicChannelType.Type)) && this.HasName == publicChannelType.HasName && (!this.HasName || this.Name.Equals(publicChannelType.Name)) && this.HasIdentity == publicChannelType.HasIdentity && (!this.HasIdentity || this.Identity.Equals(publicChannelType.Identity));
		}

		// Token: 0x17000EC6 RID: 3782
		// (get) Token: 0x06004EE3 RID: 20195 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004EE4 RID: 20196 RVA: 0x000F4CF7 File Offset: 0x000F2EF7
		public static PublicChannelType ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<PublicChannelType>(bs, 0, -1);
		}

		// Token: 0x06004EE5 RID: 20197 RVA: 0x000F4D01 File Offset: 0x000F2F01
		public void Deserialize(Stream stream)
		{
			PublicChannelType.Deserialize(stream, this);
		}

		// Token: 0x06004EE6 RID: 20198 RVA: 0x000F4D0B File Offset: 0x000F2F0B
		public static PublicChannelType Deserialize(Stream stream, PublicChannelType instance)
		{
			return PublicChannelType.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004EE7 RID: 20199 RVA: 0x000F4D18 File Offset: 0x000F2F18
		public static PublicChannelType DeserializeLengthDelimited(Stream stream)
		{
			PublicChannelType publicChannelType = new PublicChannelType();
			PublicChannelType.DeserializeLengthDelimited(stream, publicChannelType);
			return publicChannelType;
		}

		// Token: 0x06004EE8 RID: 20200 RVA: 0x000F4D34 File Offset: 0x000F2F34
		public static PublicChannelType DeserializeLengthDelimited(Stream stream, PublicChannelType instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PublicChannelType.Deserialize(stream, instance, num);
		}

		// Token: 0x06004EE9 RID: 20201 RVA: 0x000F4D5C File Offset: 0x000F2F5C
		public static PublicChannelType Deserialize(Stream stream, PublicChannelType instance, long limit)
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
							instance.Identity = ProtocolParser.ReadString(stream);
						}
					}
					else
					{
						instance.Name = ProtocolParser.ReadString(stream);
					}
				}
				else if (instance.Type == null)
				{
					instance.Type = UniqueChannelType.DeserializeLengthDelimited(stream);
				}
				else
				{
					UniqueChannelType.DeserializeLengthDelimited(stream, instance.Type);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06004EEA RID: 20202 RVA: 0x000F4E2A File Offset: 0x000F302A
		public void Serialize(Stream stream)
		{
			PublicChannelType.Serialize(stream, this);
		}

		// Token: 0x06004EEB RID: 20203 RVA: 0x000F4E34 File Offset: 0x000F3034
		public static void Serialize(Stream stream, PublicChannelType instance)
		{
			if (instance.HasType)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Type.GetSerializedSize());
				UniqueChannelType.Serialize(stream, instance.Type);
			}
			if (instance.HasName)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			}
			if (instance.HasIdentity)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Identity));
			}
		}

		// Token: 0x06004EEC RID: 20204 RVA: 0x000F4EBC File Offset: 0x000F30BC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasType)
			{
				num += 1U;
				uint serializedSize = this.Type.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasName)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasIdentity)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.Identity);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}

		// Token: 0x0400198E RID: 6542
		public bool HasType;

		// Token: 0x0400198F RID: 6543
		private UniqueChannelType _Type;

		// Token: 0x04001990 RID: 6544
		public bool HasName;

		// Token: 0x04001991 RID: 6545
		private string _Name;

		// Token: 0x04001992 RID: 6546
		public bool HasIdentity;

		// Token: 0x04001993 RID: 6547
		private string _Identity;
	}
}
