using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004C1 RID: 1217
	public class SubscribeChannelResponse : IProtoBuf
	{
		// Token: 0x17001000 RID: 4096
		// (get) Token: 0x0600552F RID: 21807 RVA: 0x00105B9F File Offset: 0x00103D9F
		// (set) Token: 0x06005530 RID: 21808 RVA: 0x00105BA7 File Offset: 0x00103DA7
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

		// Token: 0x06005531 RID: 21809 RVA: 0x00105BB7 File Offset: 0x00103DB7
		public void SetObjectId(ulong val)
		{
			this.ObjectId = val;
		}

		// Token: 0x06005532 RID: 21810 RVA: 0x00105BC0 File Offset: 0x00103DC0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasObjectId)
			{
				num ^= this.ObjectId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005533 RID: 21811 RVA: 0x00105BF4 File Offset: 0x00103DF4
		public override bool Equals(object obj)
		{
			SubscribeChannelResponse subscribeChannelResponse = obj as SubscribeChannelResponse;
			return subscribeChannelResponse != null && this.HasObjectId == subscribeChannelResponse.HasObjectId && (!this.HasObjectId || this.ObjectId.Equals(subscribeChannelResponse.ObjectId));
		}

		// Token: 0x17001001 RID: 4097
		// (get) Token: 0x06005534 RID: 21812 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005535 RID: 21813 RVA: 0x00105C3C File Offset: 0x00103E3C
		public static SubscribeChannelResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeChannelResponse>(bs, 0, -1);
		}

		// Token: 0x06005536 RID: 21814 RVA: 0x00105C46 File Offset: 0x00103E46
		public void Deserialize(Stream stream)
		{
			SubscribeChannelResponse.Deserialize(stream, this);
		}

		// Token: 0x06005537 RID: 21815 RVA: 0x00105C50 File Offset: 0x00103E50
		public static SubscribeChannelResponse Deserialize(Stream stream, SubscribeChannelResponse instance)
		{
			return SubscribeChannelResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005538 RID: 21816 RVA: 0x00105C5C File Offset: 0x00103E5C
		public static SubscribeChannelResponse DeserializeLengthDelimited(Stream stream)
		{
			SubscribeChannelResponse subscribeChannelResponse = new SubscribeChannelResponse();
			SubscribeChannelResponse.DeserializeLengthDelimited(stream, subscribeChannelResponse);
			return subscribeChannelResponse;
		}

		// Token: 0x06005539 RID: 21817 RVA: 0x00105C78 File Offset: 0x00103E78
		public static SubscribeChannelResponse DeserializeLengthDelimited(Stream stream, SubscribeChannelResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubscribeChannelResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x0600553A RID: 21818 RVA: 0x00105CA0 File Offset: 0x00103EA0
		public static SubscribeChannelResponse Deserialize(Stream stream, SubscribeChannelResponse instance, long limit)
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
				else if (num == 8)
				{
					instance.ObjectId = ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x0600553B RID: 21819 RVA: 0x00105D1F File Offset: 0x00103F1F
		public void Serialize(Stream stream)
		{
			SubscribeChannelResponse.Serialize(stream, this);
		}

		// Token: 0x0600553C RID: 21820 RVA: 0x00105D28 File Offset: 0x00103F28
		public static void Serialize(Stream stream, SubscribeChannelResponse instance)
		{
			if (instance.HasObjectId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.ObjectId);
			}
		}

		// Token: 0x0600553D RID: 21821 RVA: 0x00105D48 File Offset: 0x00103F48
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasObjectId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.ObjectId);
			}
			return num;
		}

		// Token: 0x04001AF2 RID: 6898
		public bool HasObjectId;

		// Token: 0x04001AF3 RID: 6899
		private ulong _ObjectId;
	}
}
