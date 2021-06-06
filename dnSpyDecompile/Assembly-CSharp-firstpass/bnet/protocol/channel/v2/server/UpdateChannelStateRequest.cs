using System;
using System.IO;
using bnet.protocol.channel.v1;

namespace bnet.protocol.channel.v2.server
{
	// Token: 0x0200049D RID: 1181
	public class UpdateChannelStateRequest : IProtoBuf
	{
		// Token: 0x17000F82 RID: 3970
		// (get) Token: 0x0600524F RID: 21071 RVA: 0x000FEA6B File Offset: 0x000FCC6B
		// (set) Token: 0x06005250 RID: 21072 RVA: 0x000FEA73 File Offset: 0x000FCC73
		public ChannelId ChannelId
		{
			get
			{
				return this._ChannelId;
			}
			set
			{
				this._ChannelId = value;
				this.HasChannelId = (value != null);
			}
		}

		// Token: 0x06005251 RID: 21073 RVA: 0x000FEA86 File Offset: 0x000FCC86
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000F83 RID: 3971
		// (get) Token: 0x06005252 RID: 21074 RVA: 0x000FEA8F File Offset: 0x000FCC8F
		// (set) Token: 0x06005253 RID: 21075 RVA: 0x000FEA97 File Offset: 0x000FCC97
		public ChannelStateOptions Options
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

		// Token: 0x06005254 RID: 21076 RVA: 0x000FEAAA File Offset: 0x000FCCAA
		public void SetOptions(ChannelStateOptions val)
		{
			this.Options = val;
		}

		// Token: 0x06005255 RID: 21077 RVA: 0x000FEAB4 File Offset: 0x000FCCB4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			if (this.HasOptions)
			{
				num ^= this.Options.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005256 RID: 21078 RVA: 0x000FEAFC File Offset: 0x000FCCFC
		public override bool Equals(object obj)
		{
			UpdateChannelStateRequest updateChannelStateRequest = obj as UpdateChannelStateRequest;
			return updateChannelStateRequest != null && this.HasChannelId == updateChannelStateRequest.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(updateChannelStateRequest.ChannelId)) && this.HasOptions == updateChannelStateRequest.HasOptions && (!this.HasOptions || this.Options.Equals(updateChannelStateRequest.Options));
		}

		// Token: 0x17000F84 RID: 3972
		// (get) Token: 0x06005257 RID: 21079 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005258 RID: 21080 RVA: 0x000FEB6C File Offset: 0x000FCD6C
		public static UpdateChannelStateRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateChannelStateRequest>(bs, 0, -1);
		}

		// Token: 0x06005259 RID: 21081 RVA: 0x000FEB76 File Offset: 0x000FCD76
		public void Deserialize(Stream stream)
		{
			UpdateChannelStateRequest.Deserialize(stream, this);
		}

		// Token: 0x0600525A RID: 21082 RVA: 0x000FEB80 File Offset: 0x000FCD80
		public static UpdateChannelStateRequest Deserialize(Stream stream, UpdateChannelStateRequest instance)
		{
			return UpdateChannelStateRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600525B RID: 21083 RVA: 0x000FEB8C File Offset: 0x000FCD8C
		public static UpdateChannelStateRequest DeserializeLengthDelimited(Stream stream)
		{
			UpdateChannelStateRequest updateChannelStateRequest = new UpdateChannelStateRequest();
			UpdateChannelStateRequest.DeserializeLengthDelimited(stream, updateChannelStateRequest);
			return updateChannelStateRequest;
		}

		// Token: 0x0600525C RID: 21084 RVA: 0x000FEBA8 File Offset: 0x000FCDA8
		public static UpdateChannelStateRequest DeserializeLengthDelimited(Stream stream, UpdateChannelStateRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UpdateChannelStateRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x0600525D RID: 21085 RVA: 0x000FEBD0 File Offset: 0x000FCDD0
		public static UpdateChannelStateRequest Deserialize(Stream stream, UpdateChannelStateRequest instance, long limit)
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else if (instance.Options == null)
					{
						instance.Options = ChannelStateOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelStateOptions.DeserializeLengthDelimited(stream, instance.Options);
					}
				}
				else if (instance.ChannelId == null)
				{
					instance.ChannelId = ChannelId.DeserializeLengthDelimited(stream);
				}
				else
				{
					ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600525E RID: 21086 RVA: 0x000FECA2 File Offset: 0x000FCEA2
		public void Serialize(Stream stream)
		{
			UpdateChannelStateRequest.Serialize(stream, this);
		}

		// Token: 0x0600525F RID: 21087 RVA: 0x000FECAC File Offset: 0x000FCEAC
		public static void Serialize(Stream stream, UpdateChannelStateRequest instance)
		{
			if (instance.HasChannelId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasOptions)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				ChannelStateOptions.Serialize(stream, instance.Options);
			}
		}

		// Token: 0x06005260 RID: 21088 RVA: 0x000FED14 File Offset: 0x000FCF14
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasChannelId)
			{
				num += 1U;
				uint serializedSize = this.ChannelId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasOptions)
			{
				num += 1U;
				uint serializedSize2 = this.Options.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x04001A5E RID: 6750
		public bool HasChannelId;

		// Token: 0x04001A5F RID: 6751
		private ChannelId _ChannelId;

		// Token: 0x04001A60 RID: 6752
		public bool HasOptions;

		// Token: 0x04001A61 RID: 6753
		private ChannelStateOptions _Options;
	}
}
