using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004CA RID: 1226
	public class DissolveRequest : IProtoBuf
	{
		// Token: 0x17001021 RID: 4129
		// (get) Token: 0x060055E6 RID: 21990 RVA: 0x001078C2 File Offset: 0x00105AC2
		// (set) Token: 0x060055E7 RID: 21991 RVA: 0x001078CA File Offset: 0x00105ACA
		public EntityId AgentId
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

		// Token: 0x060055E8 RID: 21992 RVA: 0x001078DD File Offset: 0x00105ADD
		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		// Token: 0x17001022 RID: 4130
		// (get) Token: 0x060055E9 RID: 21993 RVA: 0x001078E6 File Offset: 0x00105AE6
		// (set) Token: 0x060055EA RID: 21994 RVA: 0x001078EE File Offset: 0x00105AEE
		public uint Reason
		{
			get
			{
				return this._Reason;
			}
			set
			{
				this._Reason = value;
				this.HasReason = true;
			}
		}

		// Token: 0x060055EB RID: 21995 RVA: 0x001078FE File Offset: 0x00105AFE
		public void SetReason(uint val)
		{
			this.Reason = val;
		}

		// Token: 0x060055EC RID: 21996 RVA: 0x00107908 File Offset: 0x00105B08
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			if (this.HasReason)
			{
				num ^= this.Reason.GetHashCode();
			}
			return num;
		}

		// Token: 0x060055ED RID: 21997 RVA: 0x00107954 File Offset: 0x00105B54
		public override bool Equals(object obj)
		{
			DissolveRequest dissolveRequest = obj as DissolveRequest;
			return dissolveRequest != null && this.HasAgentId == dissolveRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(dissolveRequest.AgentId)) && this.HasReason == dissolveRequest.HasReason && (!this.HasReason || this.Reason.Equals(dissolveRequest.Reason));
		}

		// Token: 0x17001023 RID: 4131
		// (get) Token: 0x060055EE RID: 21998 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060055EF RID: 21999 RVA: 0x001079C7 File Offset: 0x00105BC7
		public static DissolveRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<DissolveRequest>(bs, 0, -1);
		}

		// Token: 0x060055F0 RID: 22000 RVA: 0x001079D1 File Offset: 0x00105BD1
		public void Deserialize(Stream stream)
		{
			DissolveRequest.Deserialize(stream, this);
		}

		// Token: 0x060055F1 RID: 22001 RVA: 0x001079DB File Offset: 0x00105BDB
		public static DissolveRequest Deserialize(Stream stream, DissolveRequest instance)
		{
			return DissolveRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060055F2 RID: 22002 RVA: 0x001079E8 File Offset: 0x00105BE8
		public static DissolveRequest DeserializeLengthDelimited(Stream stream)
		{
			DissolveRequest dissolveRequest = new DissolveRequest();
			DissolveRequest.DeserializeLengthDelimited(stream, dissolveRequest);
			return dissolveRequest;
		}

		// Token: 0x060055F3 RID: 22003 RVA: 0x00107A04 File Offset: 0x00105C04
		public static DissolveRequest DeserializeLengthDelimited(Stream stream, DissolveRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DissolveRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060055F4 RID: 22004 RVA: 0x00107A2C File Offset: 0x00105C2C
		public static DissolveRequest Deserialize(Stream stream, DissolveRequest instance, long limit)
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
					if (num != 16)
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
						instance.Reason = ProtocolParser.ReadUInt32(stream);
					}
				}
				else if (instance.AgentId == null)
				{
					instance.AgentId = EntityId.DeserializeLengthDelimited(stream);
				}
				else
				{
					EntityId.DeserializeLengthDelimited(stream, instance.AgentId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060055F5 RID: 22005 RVA: 0x00107ADE File Offset: 0x00105CDE
		public void Serialize(Stream stream)
		{
			DissolveRequest.Serialize(stream, this);
		}

		// Token: 0x060055F6 RID: 22006 RVA: 0x00107AE8 File Offset: 0x00105CE8
		public static void Serialize(Stream stream, DissolveRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.HasReason)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Reason);
			}
		}

		// Token: 0x060055F7 RID: 22007 RVA: 0x00107B40 File Offset: 0x00105D40
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasReason)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Reason);
			}
			return num;
		}

		// Token: 0x04001B0E RID: 6926
		public bool HasAgentId;

		// Token: 0x04001B0F RID: 6927
		private EntityId _AgentId;

		// Token: 0x04001B10 RID: 6928
		public bool HasReason;

		// Token: 0x04001B11 RID: 6929
		private uint _Reason;
	}
}
