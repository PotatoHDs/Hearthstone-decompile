using System;
using System.IO;

namespace bnet.protocol.friends.v1
{
	// Token: 0x0200041B RID: 1051
	public class UnsubscribeRequest : IProtoBuf
	{
		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x06004637 RID: 17975 RVA: 0x000DC494 File Offset: 0x000DA694
		// (set) Token: 0x06004638 RID: 17976 RVA: 0x000DC49C File Offset: 0x000DA69C
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

		// Token: 0x06004639 RID: 17977 RVA: 0x000DC4AF File Offset: 0x000DA6AF
		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x0600463A RID: 17978 RVA: 0x000DC4B8 File Offset: 0x000DA6B8
		// (set) Token: 0x0600463B RID: 17979 RVA: 0x000DC4C0 File Offset: 0x000DA6C0
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

		// Token: 0x0600463C RID: 17980 RVA: 0x000DC4D0 File Offset: 0x000DA6D0
		public void SetObjectId(ulong val)
		{
			this.ObjectId = val;
		}

		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x0600463D RID: 17981 RVA: 0x000DC4D9 File Offset: 0x000DA6D9
		// (set) Token: 0x0600463E RID: 17982 RVA: 0x000DC4E1 File Offset: 0x000DA6E1
		public ObjectAddress Forward
		{
			get
			{
				return this._Forward;
			}
			set
			{
				this._Forward = value;
				this.HasForward = (value != null);
			}
		}

		// Token: 0x0600463F RID: 17983 RVA: 0x000DC4F4 File Offset: 0x000DA6F4
		public void SetForward(ObjectAddress val)
		{
			this.Forward = val;
		}

		// Token: 0x06004640 RID: 17984 RVA: 0x000DC500 File Offset: 0x000DA700
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
			if (this.HasForward)
			{
				num ^= this.Forward.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004641 RID: 17985 RVA: 0x000DC560 File Offset: 0x000DA760
		public override bool Equals(object obj)
		{
			UnsubscribeRequest unsubscribeRequest = obj as UnsubscribeRequest;
			return unsubscribeRequest != null && this.HasAgentId == unsubscribeRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(unsubscribeRequest.AgentId)) && this.HasObjectId == unsubscribeRequest.HasObjectId && (!this.HasObjectId || this.ObjectId.Equals(unsubscribeRequest.ObjectId)) && this.HasForward == unsubscribeRequest.HasForward && (!this.HasForward || this.Forward.Equals(unsubscribeRequest.Forward));
		}

		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x06004642 RID: 17986 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004643 RID: 17987 RVA: 0x000DC5FE File Offset: 0x000DA7FE
		public static UnsubscribeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UnsubscribeRequest>(bs, 0, -1);
		}

		// Token: 0x06004644 RID: 17988 RVA: 0x000DC608 File Offset: 0x000DA808
		public void Deserialize(Stream stream)
		{
			UnsubscribeRequest.Deserialize(stream, this);
		}

		// Token: 0x06004645 RID: 17989 RVA: 0x000DC612 File Offset: 0x000DA812
		public static UnsubscribeRequest Deserialize(Stream stream, UnsubscribeRequest instance)
		{
			return UnsubscribeRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004646 RID: 17990 RVA: 0x000DC620 File Offset: 0x000DA820
		public static UnsubscribeRequest DeserializeLengthDelimited(Stream stream)
		{
			UnsubscribeRequest unsubscribeRequest = new UnsubscribeRequest();
			UnsubscribeRequest.DeserializeLengthDelimited(stream, unsubscribeRequest);
			return unsubscribeRequest;
		}

		// Token: 0x06004647 RID: 17991 RVA: 0x000DC63C File Offset: 0x000DA83C
		public static UnsubscribeRequest DeserializeLengthDelimited(Stream stream, UnsubscribeRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UnsubscribeRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004648 RID: 17992 RVA: 0x000DC664 File Offset: 0x000DA864
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
						if (num != 26)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else if (instance.Forward == null)
						{
							instance.Forward = ObjectAddress.DeserializeLengthDelimited(stream);
						}
						else
						{
							ObjectAddress.DeserializeLengthDelimited(stream, instance.Forward);
						}
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

		// Token: 0x06004649 RID: 17993 RVA: 0x000DC74C File Offset: 0x000DA94C
		public void Serialize(Stream stream)
		{
			UnsubscribeRequest.Serialize(stream, this);
		}

		// Token: 0x0600464A RID: 17994 RVA: 0x000DC758 File Offset: 0x000DA958
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
			if (instance.HasForward)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Forward.GetSerializedSize());
				ObjectAddress.Serialize(stream, instance.Forward);
			}
		}

		// Token: 0x0600464B RID: 17995 RVA: 0x000DC7DC File Offset: 0x000DA9DC
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
			if (this.HasForward)
			{
				num += 1U;
				uint serializedSize2 = this.Forward.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x04001790 RID: 6032
		public bool HasAgentId;

		// Token: 0x04001791 RID: 6033
		private EntityId _AgentId;

		// Token: 0x04001792 RID: 6034
		public bool HasObjectId;

		// Token: 0x04001793 RID: 6035
		private ulong _ObjectId;

		// Token: 0x04001794 RID: 6036
		public bool HasForward;

		// Token: 0x04001795 RID: 6037
		private ObjectAddress _Forward;
	}
}
