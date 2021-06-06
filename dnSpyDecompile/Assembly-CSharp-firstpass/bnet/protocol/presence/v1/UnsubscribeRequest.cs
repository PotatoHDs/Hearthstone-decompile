using System;
using System.IO;

namespace bnet.protocol.presence.v1
{
	// Token: 0x02000337 RID: 823
	public class UnsubscribeRequest : IProtoBuf
	{
		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x060032BF RID: 12991 RVA: 0x000A9A79 File Offset: 0x000A7C79
		// (set) Token: 0x060032C0 RID: 12992 RVA: 0x000A9A81 File Offset: 0x000A7C81
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

		// Token: 0x060032C1 RID: 12993 RVA: 0x000A9A94 File Offset: 0x000A7C94
		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x060032C2 RID: 12994 RVA: 0x000A9A9D File Offset: 0x000A7C9D
		// (set) Token: 0x060032C3 RID: 12995 RVA: 0x000A9AA5 File Offset: 0x000A7CA5
		public EntityId EntityId { get; set; }

		// Token: 0x060032C4 RID: 12996 RVA: 0x000A9AAE File Offset: 0x000A7CAE
		public void SetEntityId(EntityId val)
		{
			this.EntityId = val;
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x060032C5 RID: 12997 RVA: 0x000A9AB7 File Offset: 0x000A7CB7
		// (set) Token: 0x060032C6 RID: 12998 RVA: 0x000A9ABF File Offset: 0x000A7CBF
		public ulong ObjectId
		{
			get
			{
				return this._ObjectId;
			}
			set
			{
				this._ObjectId = value;
				this.HasObjectId = true;
			}
		}

		// Token: 0x060032C7 RID: 12999 RVA: 0x000A9ACF File Offset: 0x000A7CCF
		public void SetObjectId(ulong val)
		{
			this.ObjectId = val;
		}

		// Token: 0x060032C8 RID: 13000 RVA: 0x000A9AD8 File Offset: 0x000A7CD8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			num ^= this.EntityId.GetHashCode();
			if (this.HasObjectId)
			{
				num ^= this.ObjectId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060032C9 RID: 13001 RVA: 0x000A9B30 File Offset: 0x000A7D30
		public override bool Equals(object obj)
		{
			UnsubscribeRequest unsubscribeRequest = obj as UnsubscribeRequest;
			return unsubscribeRequest != null && this.HasAgentId == unsubscribeRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(unsubscribeRequest.AgentId)) && this.EntityId.Equals(unsubscribeRequest.EntityId) && this.HasObjectId == unsubscribeRequest.HasObjectId && (!this.HasObjectId || this.ObjectId.Equals(unsubscribeRequest.ObjectId));
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x060032CA RID: 13002 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060032CB RID: 13003 RVA: 0x000A9BB8 File Offset: 0x000A7DB8
		public static UnsubscribeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UnsubscribeRequest>(bs, 0, -1);
		}

		// Token: 0x060032CC RID: 13004 RVA: 0x000A9BC2 File Offset: 0x000A7DC2
		public void Deserialize(Stream stream)
		{
			UnsubscribeRequest.Deserialize(stream, this);
		}

		// Token: 0x060032CD RID: 13005 RVA: 0x000A9BCC File Offset: 0x000A7DCC
		public static UnsubscribeRequest Deserialize(Stream stream, UnsubscribeRequest instance)
		{
			return UnsubscribeRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060032CE RID: 13006 RVA: 0x000A9BD8 File Offset: 0x000A7DD8
		public static UnsubscribeRequest DeserializeLengthDelimited(Stream stream)
		{
			UnsubscribeRequest unsubscribeRequest = new UnsubscribeRequest();
			UnsubscribeRequest.DeserializeLengthDelimited(stream, unsubscribeRequest);
			return unsubscribeRequest;
		}

		// Token: 0x060032CF RID: 13007 RVA: 0x000A9BF4 File Offset: 0x000A7DF4
		public static UnsubscribeRequest DeserializeLengthDelimited(Stream stream, UnsubscribeRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UnsubscribeRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060032D0 RID: 13008 RVA: 0x000A9C1C File Offset: 0x000A7E1C
		public static UnsubscribeRequest Deserialize(Stream stream, UnsubscribeRequest instance, long limit)
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
						if (num != 24)
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
					else if (instance.EntityId == null)
					{
						instance.EntityId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.EntityId);
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

		// Token: 0x060032D1 RID: 13009 RVA: 0x000A9D04 File Offset: 0x000A7F04
		public void Serialize(Stream stream)
		{
			UnsubscribeRequest.Serialize(stream, this);
		}

		// Token: 0x060032D2 RID: 13010 RVA: 0x000A9D10 File Offset: 0x000A7F10
		public static void Serialize(Stream stream, UnsubscribeRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.EntityId == null)
			{
				throw new ArgumentNullException("EntityId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.EntityId.GetSerializedSize());
			EntityId.Serialize(stream, instance.EntityId);
			if (instance.HasObjectId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, instance.ObjectId);
			}
		}

		// Token: 0x060032D3 RID: 13011 RVA: 0x000A9DA4 File Offset: 0x000A7FA4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			uint serializedSize2 = this.EntityId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			if (this.HasObjectId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.ObjectId);
			}
			return num + 1U;
		}

		// Token: 0x040013E3 RID: 5091
		public bool HasAgentId;

		// Token: 0x040013E4 RID: 5092
		private EntityId _AgentId;

		// Token: 0x040013E6 RID: 5094
		public bool HasObjectId;

		// Token: 0x040013E7 RID: 5095
		private ulong _ObjectId;
	}
}
