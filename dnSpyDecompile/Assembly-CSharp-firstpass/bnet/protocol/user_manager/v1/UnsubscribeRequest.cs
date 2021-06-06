using System;
using System.IO;

namespace bnet.protocol.user_manager.v1
{
	// Token: 0x020002EE RID: 750
	public class UnsubscribeRequest : IProtoBuf
	{
		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06002CA1 RID: 11425 RVA: 0x00099721 File Offset: 0x00097921
		// (set) Token: 0x06002CA2 RID: 11426 RVA: 0x00099729 File Offset: 0x00097929
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

		// Token: 0x06002CA3 RID: 11427 RVA: 0x0009973C File Offset: 0x0009793C
		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06002CA4 RID: 11428 RVA: 0x00099745 File Offset: 0x00097945
		// (set) Token: 0x06002CA5 RID: 11429 RVA: 0x0009974D File Offset: 0x0009794D
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

		// Token: 0x06002CA6 RID: 11430 RVA: 0x0009975D File Offset: 0x0009795D
		public void SetObjectId(ulong val)
		{
			this.ObjectId = val;
		}

		// Token: 0x06002CA7 RID: 11431 RVA: 0x00099768 File Offset: 0x00097968
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			if (this.HasObjectId)
			{
				num ^= this.ObjectId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002CA8 RID: 11432 RVA: 0x000997B4 File Offset: 0x000979B4
		public override bool Equals(object obj)
		{
			UnsubscribeRequest unsubscribeRequest = obj as UnsubscribeRequest;
			return unsubscribeRequest != null && this.HasAgentId == unsubscribeRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(unsubscribeRequest.AgentId)) && this.HasObjectId == unsubscribeRequest.HasObjectId && (!this.HasObjectId || this.ObjectId.Equals(unsubscribeRequest.ObjectId));
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06002CA9 RID: 11433 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002CAA RID: 11434 RVA: 0x00099827 File Offset: 0x00097A27
		public static UnsubscribeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UnsubscribeRequest>(bs, 0, -1);
		}

		// Token: 0x06002CAB RID: 11435 RVA: 0x00099831 File Offset: 0x00097A31
		public void Deserialize(Stream stream)
		{
			UnsubscribeRequest.Deserialize(stream, this);
		}

		// Token: 0x06002CAC RID: 11436 RVA: 0x0009983B File Offset: 0x00097A3B
		public static UnsubscribeRequest Deserialize(Stream stream, UnsubscribeRequest instance)
		{
			return UnsubscribeRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002CAD RID: 11437 RVA: 0x00099848 File Offset: 0x00097A48
		public static UnsubscribeRequest DeserializeLengthDelimited(Stream stream)
		{
			UnsubscribeRequest unsubscribeRequest = new UnsubscribeRequest();
			UnsubscribeRequest.DeserializeLengthDelimited(stream, unsubscribeRequest);
			return unsubscribeRequest;
		}

		// Token: 0x06002CAE RID: 11438 RVA: 0x00099864 File Offset: 0x00097A64
		public static UnsubscribeRequest DeserializeLengthDelimited(Stream stream, UnsubscribeRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UnsubscribeRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06002CAF RID: 11439 RVA: 0x0009988C File Offset: 0x00097A8C
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
						instance.ObjectId = ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06002CB0 RID: 11440 RVA: 0x0009993E File Offset: 0x00097B3E
		public void Serialize(Stream stream)
		{
			UnsubscribeRequest.Serialize(stream, this);
		}

		// Token: 0x06002CB1 RID: 11441 RVA: 0x00099948 File Offset: 0x00097B48
		public static void Serialize(Stream stream, UnsubscribeRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.HasObjectId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.ObjectId);
			}
		}

		// Token: 0x06002CB2 RID: 11442 RVA: 0x000999A0 File Offset: 0x00097BA0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasObjectId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.ObjectId);
			}
			return num;
		}

		// Token: 0x04001268 RID: 4712
		public bool HasAgentId;

		// Token: 0x04001269 RID: 4713
		private EntityId _AgentId;

		// Token: 0x0400126A RID: 4714
		public bool HasObjectId;

		// Token: 0x0400126B RID: 4715
		private ulong _ObjectId;
	}
}
