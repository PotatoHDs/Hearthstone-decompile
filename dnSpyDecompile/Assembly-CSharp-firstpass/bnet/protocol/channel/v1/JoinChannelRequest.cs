using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004BE RID: 1214
	public class JoinChannelRequest : IProtoBuf
	{
		// Token: 0x17000FF3 RID: 4083
		// (get) Token: 0x060054EA RID: 21738 RVA: 0x00105071 File Offset: 0x00103271
		// (set) Token: 0x060054EB RID: 21739 RVA: 0x00105079 File Offset: 0x00103279
		public Identity AgentIdentity
		{
			get
			{
				return this._AgentIdentity;
			}
			set
			{
				this._AgentIdentity = value;
				this.HasAgentIdentity = (value != null);
			}
		}

		// Token: 0x060054EC RID: 21740 RVA: 0x0010508C File Offset: 0x0010328C
		public void SetAgentIdentity(Identity val)
		{
			this.AgentIdentity = val;
		}

		// Token: 0x17000FF4 RID: 4084
		// (get) Token: 0x060054ED RID: 21741 RVA: 0x00105095 File Offset: 0x00103295
		// (set) Token: 0x060054EE RID: 21742 RVA: 0x0010509D File Offset: 0x0010329D
		public EntityId ChannelId { get; set; }

		// Token: 0x060054EF RID: 21743 RVA: 0x001050A6 File Offset: 0x001032A6
		public void SetChannelId(EntityId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000FF5 RID: 4085
		// (get) Token: 0x060054F0 RID: 21744 RVA: 0x001050AF File Offset: 0x001032AF
		// (set) Token: 0x060054F1 RID: 21745 RVA: 0x001050B7 File Offset: 0x001032B7
		public ulong ObjectId { get; set; }

		// Token: 0x060054F2 RID: 21746 RVA: 0x001050C0 File Offset: 0x001032C0
		public void SetObjectId(ulong val)
		{
			this.ObjectId = val;
		}

		// Token: 0x060054F3 RID: 21747 RVA: 0x001050CC File Offset: 0x001032CC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentIdentity)
			{
				num ^= this.AgentIdentity.GetHashCode();
			}
			num ^= this.ChannelId.GetHashCode();
			return num ^ this.ObjectId.GetHashCode();
		}

		// Token: 0x060054F4 RID: 21748 RVA: 0x0010511C File Offset: 0x0010331C
		public override bool Equals(object obj)
		{
			JoinChannelRequest joinChannelRequest = obj as JoinChannelRequest;
			return joinChannelRequest != null && this.HasAgentIdentity == joinChannelRequest.HasAgentIdentity && (!this.HasAgentIdentity || this.AgentIdentity.Equals(joinChannelRequest.AgentIdentity)) && this.ChannelId.Equals(joinChannelRequest.ChannelId) && this.ObjectId.Equals(joinChannelRequest.ObjectId);
		}

		// Token: 0x17000FF6 RID: 4086
		// (get) Token: 0x060054F5 RID: 21749 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060054F6 RID: 21750 RVA: 0x0010518E File Offset: 0x0010338E
		public static JoinChannelRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<JoinChannelRequest>(bs, 0, -1);
		}

		// Token: 0x060054F7 RID: 21751 RVA: 0x00105198 File Offset: 0x00103398
		public void Deserialize(Stream stream)
		{
			JoinChannelRequest.Deserialize(stream, this);
		}

		// Token: 0x060054F8 RID: 21752 RVA: 0x001051A2 File Offset: 0x001033A2
		public static JoinChannelRequest Deserialize(Stream stream, JoinChannelRequest instance)
		{
			return JoinChannelRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060054F9 RID: 21753 RVA: 0x001051B0 File Offset: 0x001033B0
		public static JoinChannelRequest DeserializeLengthDelimited(Stream stream)
		{
			JoinChannelRequest joinChannelRequest = new JoinChannelRequest();
			JoinChannelRequest.DeserializeLengthDelimited(stream, joinChannelRequest);
			return joinChannelRequest;
		}

		// Token: 0x060054FA RID: 21754 RVA: 0x001051CC File Offset: 0x001033CC
		public static JoinChannelRequest DeserializeLengthDelimited(Stream stream, JoinChannelRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return JoinChannelRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060054FB RID: 21755 RVA: 0x001051F4 File Offset: 0x001033F4
		public static JoinChannelRequest Deserialize(Stream stream, JoinChannelRequest instance, long limit)
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
					if (num != 26)
					{
						if (num != 32)
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
							instance.ObjectId = ProtocolParser.ReadUInt64(stream);
						}
					}
					else if (instance.ChannelId == null)
					{
						instance.ChannelId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.ChannelId);
					}
				}
				else if (instance.AgentIdentity == null)
				{
					instance.AgentIdentity = Identity.DeserializeLengthDelimited(stream);
				}
				else
				{
					Identity.DeserializeLengthDelimited(stream, instance.AgentIdentity);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060054FC RID: 21756 RVA: 0x001052DC File Offset: 0x001034DC
		public void Serialize(Stream stream)
		{
			JoinChannelRequest.Serialize(stream, this);
		}

		// Token: 0x060054FD RID: 21757 RVA: 0x001052E8 File Offset: 0x001034E8
		public static void Serialize(Stream stream, JoinChannelRequest instance)
		{
			if (instance.HasAgentIdentity)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentIdentity.GetSerializedSize());
				Identity.Serialize(stream, instance.AgentIdentity);
			}
			if (instance.ChannelId == null)
			{
				throw new ArgumentNullException("ChannelId", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
			EntityId.Serialize(stream, instance.ChannelId);
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, instance.ObjectId);
		}

		// Token: 0x060054FE RID: 21758 RVA: 0x00105374 File Offset: 0x00103574
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentIdentity)
			{
				num += 1U;
				uint serializedSize = this.AgentIdentity.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			uint serializedSize2 = this.ChannelId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			num += ProtocolParser.SizeOfUInt64(this.ObjectId);
			return num + 2U;
		}

		// Token: 0x04001AE2 RID: 6882
		public bool HasAgentIdentity;

		// Token: 0x04001AE3 RID: 6883
		private Identity _AgentIdentity;
	}
}
