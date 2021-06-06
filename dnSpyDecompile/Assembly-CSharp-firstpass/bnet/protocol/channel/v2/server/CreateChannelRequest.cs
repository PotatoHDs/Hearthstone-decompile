using System;
using System.IO;

namespace bnet.protocol.channel.v2.server
{
	// Token: 0x02000493 RID: 1171
	public class CreateChannelRequest : IProtoBuf
	{
		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x06005193 RID: 20883 RVA: 0x000FCF47 File Offset: 0x000FB147
		// (set) Token: 0x06005194 RID: 20884 RVA: 0x000FCF4F File Offset: 0x000FB14F
		public CreateChannelServerOptions Options
		{
			get
			{
				return this._Options;
			}
			set
			{
				this._Options = value;
				this.HasOptions = (value != null);
			}
		}

		// Token: 0x06005195 RID: 20885 RVA: 0x000FCF62 File Offset: 0x000FB162
		public void SetOptions(CreateChannelServerOptions val)
		{
			this.Options = val;
		}

		// Token: 0x06005196 RID: 20886 RVA: 0x000FCF6C File Offset: 0x000FB16C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasOptions)
			{
				num ^= this.Options.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005197 RID: 20887 RVA: 0x000FCF9C File Offset: 0x000FB19C
		public override bool Equals(object obj)
		{
			CreateChannelRequest createChannelRequest = obj as CreateChannelRequest;
			return createChannelRequest != null && this.HasOptions == createChannelRequest.HasOptions && (!this.HasOptions || this.Options.Equals(createChannelRequest.Options));
		}

		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x06005198 RID: 20888 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005199 RID: 20889 RVA: 0x000FCFE1 File Offset: 0x000FB1E1
		public static CreateChannelRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateChannelRequest>(bs, 0, -1);
		}

		// Token: 0x0600519A RID: 20890 RVA: 0x000FCFEB File Offset: 0x000FB1EB
		public void Deserialize(Stream stream)
		{
			CreateChannelRequest.Deserialize(stream, this);
		}

		// Token: 0x0600519B RID: 20891 RVA: 0x000FCFF5 File Offset: 0x000FB1F5
		public static CreateChannelRequest Deserialize(Stream stream, CreateChannelRequest instance)
		{
			return CreateChannelRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600519C RID: 20892 RVA: 0x000FD000 File Offset: 0x000FB200
		public static CreateChannelRequest DeserializeLengthDelimited(Stream stream)
		{
			CreateChannelRequest createChannelRequest = new CreateChannelRequest();
			CreateChannelRequest.DeserializeLengthDelimited(stream, createChannelRequest);
			return createChannelRequest;
		}

		// Token: 0x0600519D RID: 20893 RVA: 0x000FD01C File Offset: 0x000FB21C
		public static CreateChannelRequest DeserializeLengthDelimited(Stream stream, CreateChannelRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CreateChannelRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x0600519E RID: 20894 RVA: 0x000FD044 File Offset: 0x000FB244
		public static CreateChannelRequest Deserialize(Stream stream, CreateChannelRequest instance, long limit)
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
					if (instance.Options == null)
					{
						instance.Options = CreateChannelServerOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						CreateChannelServerOptions.DeserializeLengthDelimited(stream, instance.Options);
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

		// Token: 0x0600519F RID: 20895 RVA: 0x000FD0DE File Offset: 0x000FB2DE
		public void Serialize(Stream stream)
		{
			CreateChannelRequest.Serialize(stream, this);
		}

		// Token: 0x060051A0 RID: 20896 RVA: 0x000FD0E7 File Offset: 0x000FB2E7
		public static void Serialize(Stream stream, CreateChannelRequest instance)
		{
			if (instance.HasOptions)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				CreateChannelServerOptions.Serialize(stream, instance.Options);
			}
		}

		// Token: 0x060051A1 RID: 20897 RVA: 0x000FD118 File Offset: 0x000FB318
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasOptions)
			{
				num += 1U;
				uint serializedSize = this.Options.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x04001A3B RID: 6715
		public bool HasOptions;

		// Token: 0x04001A3C RID: 6716
		private CreateChannelServerOptions _Options;
	}
}
