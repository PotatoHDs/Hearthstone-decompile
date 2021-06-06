using System;
using System.IO;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000478 RID: 1144
	public class GetPublicChannelTypesOptions : IProtoBuf
	{
		// Token: 0x17000EC7 RID: 3783
		// (get) Token: 0x06004EEE RID: 20206 RVA: 0x000F4F3F File Offset: 0x000F313F
		// (set) Token: 0x06004EEF RID: 20207 RVA: 0x000F4F47 File Offset: 0x000F3147
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

		// Token: 0x06004EF0 RID: 20208 RVA: 0x000F4F5A File Offset: 0x000F315A
		public void SetType(UniqueChannelType val)
		{
			this.Type = val;
		}

		// Token: 0x06004EF1 RID: 20209 RVA: 0x000F4F64 File Offset: 0x000F3164
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasType)
			{
				num ^= this.Type.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004EF2 RID: 20210 RVA: 0x000F4F94 File Offset: 0x000F3194
		public override bool Equals(object obj)
		{
			GetPublicChannelTypesOptions getPublicChannelTypesOptions = obj as GetPublicChannelTypesOptions;
			return getPublicChannelTypesOptions != null && this.HasType == getPublicChannelTypesOptions.HasType && (!this.HasType || this.Type.Equals(getPublicChannelTypesOptions.Type));
		}

		// Token: 0x17000EC8 RID: 3784
		// (get) Token: 0x06004EF3 RID: 20211 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004EF4 RID: 20212 RVA: 0x000F4FD9 File Offset: 0x000F31D9
		public static GetPublicChannelTypesOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetPublicChannelTypesOptions>(bs, 0, -1);
		}

		// Token: 0x06004EF5 RID: 20213 RVA: 0x000F4FE3 File Offset: 0x000F31E3
		public void Deserialize(Stream stream)
		{
			GetPublicChannelTypesOptions.Deserialize(stream, this);
		}

		// Token: 0x06004EF6 RID: 20214 RVA: 0x000F4FED File Offset: 0x000F31ED
		public static GetPublicChannelTypesOptions Deserialize(Stream stream, GetPublicChannelTypesOptions instance)
		{
			return GetPublicChannelTypesOptions.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004EF7 RID: 20215 RVA: 0x000F4FF8 File Offset: 0x000F31F8
		public static GetPublicChannelTypesOptions DeserializeLengthDelimited(Stream stream)
		{
			GetPublicChannelTypesOptions getPublicChannelTypesOptions = new GetPublicChannelTypesOptions();
			GetPublicChannelTypesOptions.DeserializeLengthDelimited(stream, getPublicChannelTypesOptions);
			return getPublicChannelTypesOptions;
		}

		// Token: 0x06004EF8 RID: 20216 RVA: 0x000F5014 File Offset: 0x000F3214
		public static GetPublicChannelTypesOptions DeserializeLengthDelimited(Stream stream, GetPublicChannelTypesOptions instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetPublicChannelTypesOptions.Deserialize(stream, instance, num);
		}

		// Token: 0x06004EF9 RID: 20217 RVA: 0x000F503C File Offset: 0x000F323C
		public static GetPublicChannelTypesOptions Deserialize(Stream stream, GetPublicChannelTypesOptions instance, long limit)
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
					if (instance.Type == null)
					{
						instance.Type = UniqueChannelType.DeserializeLengthDelimited(stream);
					}
					else
					{
						UniqueChannelType.DeserializeLengthDelimited(stream, instance.Type);
					}
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

		// Token: 0x06004EFA RID: 20218 RVA: 0x000F50D6 File Offset: 0x000F32D6
		public void Serialize(Stream stream)
		{
			GetPublicChannelTypesOptions.Serialize(stream, this);
		}

		// Token: 0x06004EFB RID: 20219 RVA: 0x000F50DF File Offset: 0x000F32DF
		public static void Serialize(Stream stream, GetPublicChannelTypesOptions instance)
		{
			if (instance.HasType)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Type.GetSerializedSize());
				UniqueChannelType.Serialize(stream, instance.Type);
			}
		}

		// Token: 0x06004EFC RID: 20220 RVA: 0x000F5110 File Offset: 0x000F3310
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasType)
			{
				num += 1U;
				uint serializedSize = this.Type.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x04001994 RID: 6548
		public bool HasType;

		// Token: 0x04001995 RID: 6549
		private UniqueChannelType _Type;
	}
}
