using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x02000518 RID: 1304
	public class GetCAISInfoRequest : IProtoBuf
	{
		// Token: 0x17001183 RID: 4483
		// (get) Token: 0x06005CEE RID: 23790 RVA: 0x0011A393 File Offset: 0x00118593
		// (set) Token: 0x06005CEF RID: 23791 RVA: 0x0011A39B File Offset: 0x0011859B
		public EntityId EntityId
		{
			get
			{
				return this._EntityId;
			}
			set
			{
				this._EntityId = value;
				this.HasEntityId = (value != null);
			}
		}

		// Token: 0x06005CF0 RID: 23792 RVA: 0x0011A3AE File Offset: 0x001185AE
		public void SetEntityId(EntityId val)
		{
			this.EntityId = val;
		}

		// Token: 0x06005CF1 RID: 23793 RVA: 0x0011A3B8 File Offset: 0x001185B8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasEntityId)
			{
				num ^= this.EntityId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005CF2 RID: 23794 RVA: 0x0011A3E8 File Offset: 0x001185E8
		public override bool Equals(object obj)
		{
			GetCAISInfoRequest getCAISInfoRequest = obj as GetCAISInfoRequest;
			return getCAISInfoRequest != null && this.HasEntityId == getCAISInfoRequest.HasEntityId && (!this.HasEntityId || this.EntityId.Equals(getCAISInfoRequest.EntityId));
		}

		// Token: 0x17001184 RID: 4484
		// (get) Token: 0x06005CF3 RID: 23795 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005CF4 RID: 23796 RVA: 0x0011A42D File Offset: 0x0011862D
		public static GetCAISInfoRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetCAISInfoRequest>(bs, 0, -1);
		}

		// Token: 0x06005CF5 RID: 23797 RVA: 0x0011A437 File Offset: 0x00118637
		public void Deserialize(Stream stream)
		{
			GetCAISInfoRequest.Deserialize(stream, this);
		}

		// Token: 0x06005CF6 RID: 23798 RVA: 0x0011A441 File Offset: 0x00118641
		public static GetCAISInfoRequest Deserialize(Stream stream, GetCAISInfoRequest instance)
		{
			return GetCAISInfoRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005CF7 RID: 23799 RVA: 0x0011A44C File Offset: 0x0011864C
		public static GetCAISInfoRequest DeserializeLengthDelimited(Stream stream)
		{
			GetCAISInfoRequest getCAISInfoRequest = new GetCAISInfoRequest();
			GetCAISInfoRequest.DeserializeLengthDelimited(stream, getCAISInfoRequest);
			return getCAISInfoRequest;
		}

		// Token: 0x06005CF8 RID: 23800 RVA: 0x0011A468 File Offset: 0x00118668
		public static GetCAISInfoRequest DeserializeLengthDelimited(Stream stream, GetCAISInfoRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetCAISInfoRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06005CF9 RID: 23801 RVA: 0x0011A490 File Offset: 0x00118690
		public static GetCAISInfoRequest Deserialize(Stream stream, GetCAISInfoRequest instance, long limit)
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
					if (instance.EntityId == null)
					{
						instance.EntityId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.EntityId);
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

		// Token: 0x06005CFA RID: 23802 RVA: 0x0011A52A File Offset: 0x0011872A
		public void Serialize(Stream stream)
		{
			GetCAISInfoRequest.Serialize(stream, this);
		}

		// Token: 0x06005CFB RID: 23803 RVA: 0x0011A533 File Offset: 0x00118733
		public static void Serialize(Stream stream, GetCAISInfoRequest instance)
		{
			if (instance.HasEntityId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.EntityId.GetSerializedSize());
				EntityId.Serialize(stream, instance.EntityId);
			}
		}

		// Token: 0x06005CFC RID: 23804 RVA: 0x0011A564 File Offset: 0x00118764
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasEntityId)
			{
				num += 1U;
				uint serializedSize = this.EntityId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x04001CB2 RID: 7346
		public bool HasEntityId;

		// Token: 0x04001CB3 RID: 7347
		private EntityId _EntityId;
	}
}
