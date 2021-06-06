using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	// Token: 0x0200044A RID: 1098
	public class CreateChannelRequest : IProtoBuf
	{
		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x06004ACF RID: 19151 RVA: 0x000E935A File Offset: 0x000E755A
		// (set) Token: 0x06004AD0 RID: 19152 RVA: 0x000E9362 File Offset: 0x000E7562
		public GameAccountHandle AgentId
		{
			get
			{
				return this._AgentId;
			}
			set
			{
				this._AgentId = value;
				this.HasAgentId = (value != null);
			}
		}

		// Token: 0x06004AD1 RID: 19153 RVA: 0x000E9375 File Offset: 0x000E7575
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x06004AD2 RID: 19154 RVA: 0x000E937E File Offset: 0x000E757E
		// (set) Token: 0x06004AD3 RID: 19155 RVA: 0x000E9386 File Offset: 0x000E7586
		public CreateChannelOptions Options
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

		// Token: 0x06004AD4 RID: 19156 RVA: 0x000E9399 File Offset: 0x000E7599
		public void SetOptions(CreateChannelOptions val)
		{
			this.Options = val;
		}

		// Token: 0x06004AD5 RID: 19157 RVA: 0x000E93A4 File Offset: 0x000E75A4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			if (this.HasOptions)
			{
				num ^= this.Options.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004AD6 RID: 19158 RVA: 0x000E93EC File Offset: 0x000E75EC
		public override bool Equals(object obj)
		{
			CreateChannelRequest createChannelRequest = obj as CreateChannelRequest;
			return createChannelRequest != null && this.HasAgentId == createChannelRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(createChannelRequest.AgentId)) && this.HasOptions == createChannelRequest.HasOptions && (!this.HasOptions || this.Options.Equals(createChannelRequest.Options));
		}

		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x06004AD7 RID: 19159 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004AD8 RID: 19160 RVA: 0x000E945C File Offset: 0x000E765C
		public static CreateChannelRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateChannelRequest>(bs, 0, -1);
		}

		// Token: 0x06004AD9 RID: 19161 RVA: 0x000E9466 File Offset: 0x000E7666
		public void Deserialize(Stream stream)
		{
			CreateChannelRequest.Deserialize(stream, this);
		}

		// Token: 0x06004ADA RID: 19162 RVA: 0x000E9470 File Offset: 0x000E7670
		public static CreateChannelRequest Deserialize(Stream stream, CreateChannelRequest instance)
		{
			return CreateChannelRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004ADB RID: 19163 RVA: 0x000E947C File Offset: 0x000E767C
		public static CreateChannelRequest DeserializeLengthDelimited(Stream stream)
		{
			CreateChannelRequest createChannelRequest = new CreateChannelRequest();
			CreateChannelRequest.DeserializeLengthDelimited(stream, createChannelRequest);
			return createChannelRequest;
		}

		// Token: 0x06004ADC RID: 19164 RVA: 0x000E9498 File Offset: 0x000E7698
		public static CreateChannelRequest DeserializeLengthDelimited(Stream stream, CreateChannelRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CreateChannelRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004ADD RID: 19165 RVA: 0x000E94C0 File Offset: 0x000E76C0
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
						instance.Options = CreateChannelOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						CreateChannelOptions.DeserializeLengthDelimited(stream, instance.Options);
					}
				}
				else if (instance.AgentId == null)
				{
					instance.AgentId = GameAccountHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameAccountHandle.DeserializeLengthDelimited(stream, instance.AgentId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06004ADE RID: 19166 RVA: 0x000E9592 File Offset: 0x000E7792
		public void Serialize(Stream stream)
		{
			CreateChannelRequest.Serialize(stream, this);
		}

		// Token: 0x06004ADF RID: 19167 RVA: 0x000E959C File Offset: 0x000E779C
		public static void Serialize(Stream stream, CreateChannelRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.AgentId);
			}
			if (instance.HasOptions)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				CreateChannelOptions.Serialize(stream, instance.Options);
			}
		}

		// Token: 0x06004AE0 RID: 19168 RVA: 0x000E9604 File Offset: 0x000E7804
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
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

		// Token: 0x04001888 RID: 6280
		public bool HasAgentId;

		// Token: 0x04001889 RID: 6281
		private GameAccountHandle _AgentId;

		// Token: 0x0400188A RID: 6282
		public bool HasOptions;

		// Token: 0x0400188B RID: 6283
		private CreateChannelOptions _Options;
	}
}
