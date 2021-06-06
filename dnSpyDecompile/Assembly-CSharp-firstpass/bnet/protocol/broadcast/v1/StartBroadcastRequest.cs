using System;
using System.IO;
using System.Text;

namespace bnet.protocol.broadcast.v1
{
	// Token: 0x020004E4 RID: 1252
	public class StartBroadcastRequest : IProtoBuf
	{
		// Token: 0x170010A8 RID: 4264
		// (get) Token: 0x0600586F RID: 22639 RVA: 0x0010ED5C File Offset: 0x0010CF5C
		// (set) Token: 0x06005870 RID: 22640 RVA: 0x0010ED64 File Offset: 0x0010CF64
		public Broadcast Broadcast
		{
			get
			{
				return this._Broadcast;
			}
			set
			{
				this._Broadcast = value;
				this.HasBroadcast = (value != null);
			}
		}

		// Token: 0x06005871 RID: 22641 RVA: 0x0010ED77 File Offset: 0x0010CF77
		public void SetBroadcast(Broadcast val)
		{
			this.Broadcast = val;
		}

		// Token: 0x170010A9 RID: 4265
		// (get) Token: 0x06005872 RID: 22642 RVA: 0x0010ED80 File Offset: 0x0010CF80
		// (set) Token: 0x06005873 RID: 22643 RVA: 0x0010ED88 File Offset: 0x0010CF88
		public string Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
				this.HasId = (value != null);
			}
		}

		// Token: 0x06005874 RID: 22644 RVA: 0x0010ED9B File Offset: 0x0010CF9B
		public void SetId(string val)
		{
			this.Id = val;
		}

		// Token: 0x06005875 RID: 22645 RVA: 0x0010EDA4 File Offset: 0x0010CFA4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasBroadcast)
			{
				num ^= this.Broadcast.GetHashCode();
			}
			if (this.HasId)
			{
				num ^= this.Id.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005876 RID: 22646 RVA: 0x0010EDEC File Offset: 0x0010CFEC
		public override bool Equals(object obj)
		{
			StartBroadcastRequest startBroadcastRequest = obj as StartBroadcastRequest;
			return startBroadcastRequest != null && this.HasBroadcast == startBroadcastRequest.HasBroadcast && (!this.HasBroadcast || this.Broadcast.Equals(startBroadcastRequest.Broadcast)) && this.HasId == startBroadcastRequest.HasId && (!this.HasId || this.Id.Equals(startBroadcastRequest.Id));
		}

		// Token: 0x170010AA RID: 4266
		// (get) Token: 0x06005877 RID: 22647 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005878 RID: 22648 RVA: 0x0010EE5C File Offset: 0x0010D05C
		public static StartBroadcastRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<StartBroadcastRequest>(bs, 0, -1);
		}

		// Token: 0x06005879 RID: 22649 RVA: 0x0010EE66 File Offset: 0x0010D066
		public void Deserialize(Stream stream)
		{
			StartBroadcastRequest.Deserialize(stream, this);
		}

		// Token: 0x0600587A RID: 22650 RVA: 0x0010EE70 File Offset: 0x0010D070
		public static StartBroadcastRequest Deserialize(Stream stream, StartBroadcastRequest instance)
		{
			return StartBroadcastRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600587B RID: 22651 RVA: 0x0010EE7C File Offset: 0x0010D07C
		public static StartBroadcastRequest DeserializeLengthDelimited(Stream stream)
		{
			StartBroadcastRequest startBroadcastRequest = new StartBroadcastRequest();
			StartBroadcastRequest.DeserializeLengthDelimited(stream, startBroadcastRequest);
			return startBroadcastRequest;
		}

		// Token: 0x0600587C RID: 22652 RVA: 0x0010EE98 File Offset: 0x0010D098
		public static StartBroadcastRequest DeserializeLengthDelimited(Stream stream, StartBroadcastRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return StartBroadcastRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x0600587D RID: 22653 RVA: 0x0010EEC0 File Offset: 0x0010D0C0
		public static StartBroadcastRequest Deserialize(Stream stream, StartBroadcastRequest instance, long limit)
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
					else
					{
						instance.Id = ProtocolParser.ReadString(stream);
					}
				}
				else if (instance.Broadcast == null)
				{
					instance.Broadcast = Broadcast.DeserializeLengthDelimited(stream);
				}
				else
				{
					Broadcast.DeserializeLengthDelimited(stream, instance.Broadcast);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600587E RID: 22654 RVA: 0x0010EF72 File Offset: 0x0010D172
		public void Serialize(Stream stream)
		{
			StartBroadcastRequest.Serialize(stream, this);
		}

		// Token: 0x0600587F RID: 22655 RVA: 0x0010EF7C File Offset: 0x0010D17C
		public static void Serialize(Stream stream, StartBroadcastRequest instance)
		{
			if (instance.HasBroadcast)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Broadcast.GetSerializedSize());
				Broadcast.Serialize(stream, instance.Broadcast);
			}
			if (instance.HasId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Id));
			}
		}

		// Token: 0x06005880 RID: 22656 RVA: 0x0010EFDC File Offset: 0x0010D1DC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasBroadcast)
			{
				num += 1U;
				uint serializedSize = this.Broadcast.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Id);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x04001BB5 RID: 7093
		public bool HasBroadcast;

		// Token: 0x04001BB6 RID: 7094
		private Broadcast _Broadcast;

		// Token: 0x04001BB7 RID: 7095
		public bool HasId;

		// Token: 0x04001BB8 RID: 7096
		private string _Id;
	}
}
