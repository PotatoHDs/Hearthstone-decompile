using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x02000514 RID: 1300
	public class GetGameSessionInfoRequest : IProtoBuf
	{
		// Token: 0x1700117A RID: 4474
		// (get) Token: 0x06005CAB RID: 23723 RVA: 0x00119A87 File Offset: 0x00117C87
		// (set) Token: 0x06005CAC RID: 23724 RVA: 0x00119A8F File Offset: 0x00117C8F
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

		// Token: 0x06005CAD RID: 23725 RVA: 0x00119AA2 File Offset: 0x00117CA2
		public void SetEntityId(EntityId val)
		{
			this.EntityId = val;
		}

		// Token: 0x06005CAE RID: 23726 RVA: 0x00119AAC File Offset: 0x00117CAC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasEntityId)
			{
				num ^= this.EntityId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005CAF RID: 23727 RVA: 0x00119ADC File Offset: 0x00117CDC
		public override bool Equals(object obj)
		{
			GetGameSessionInfoRequest getGameSessionInfoRequest = obj as GetGameSessionInfoRequest;
			return getGameSessionInfoRequest != null && this.HasEntityId == getGameSessionInfoRequest.HasEntityId && (!this.HasEntityId || this.EntityId.Equals(getGameSessionInfoRequest.EntityId));
		}

		// Token: 0x1700117B RID: 4475
		// (get) Token: 0x06005CB0 RID: 23728 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005CB1 RID: 23729 RVA: 0x00119B21 File Offset: 0x00117D21
		public static GetGameSessionInfoRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetGameSessionInfoRequest>(bs, 0, -1);
		}

		// Token: 0x06005CB2 RID: 23730 RVA: 0x00119B2B File Offset: 0x00117D2B
		public void Deserialize(Stream stream)
		{
			GetGameSessionInfoRequest.Deserialize(stream, this);
		}

		// Token: 0x06005CB3 RID: 23731 RVA: 0x00119B35 File Offset: 0x00117D35
		public static GetGameSessionInfoRequest Deserialize(Stream stream, GetGameSessionInfoRequest instance)
		{
			return GetGameSessionInfoRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005CB4 RID: 23732 RVA: 0x00119B40 File Offset: 0x00117D40
		public static GetGameSessionInfoRequest DeserializeLengthDelimited(Stream stream)
		{
			GetGameSessionInfoRequest getGameSessionInfoRequest = new GetGameSessionInfoRequest();
			GetGameSessionInfoRequest.DeserializeLengthDelimited(stream, getGameSessionInfoRequest);
			return getGameSessionInfoRequest;
		}

		// Token: 0x06005CB5 RID: 23733 RVA: 0x00119B5C File Offset: 0x00117D5C
		public static GetGameSessionInfoRequest DeserializeLengthDelimited(Stream stream, GetGameSessionInfoRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetGameSessionInfoRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06005CB6 RID: 23734 RVA: 0x00119B84 File Offset: 0x00117D84
		public static GetGameSessionInfoRequest Deserialize(Stream stream, GetGameSessionInfoRequest instance, long limit)
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

		// Token: 0x06005CB7 RID: 23735 RVA: 0x00119C1E File Offset: 0x00117E1E
		public void Serialize(Stream stream)
		{
			GetGameSessionInfoRequest.Serialize(stream, this);
		}

		// Token: 0x06005CB8 RID: 23736 RVA: 0x00119C27 File Offset: 0x00117E27
		public static void Serialize(Stream stream, GetGameSessionInfoRequest instance)
		{
			if (instance.HasEntityId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.EntityId.GetSerializedSize());
				EntityId.Serialize(stream, instance.EntityId);
			}
		}

		// Token: 0x06005CB9 RID: 23737 RVA: 0x00119C58 File Offset: 0x00117E58
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

		// Token: 0x04001CA8 RID: 7336
		public bool HasEntityId;

		// Token: 0x04001CA9 RID: 7337
		private EntityId _EntityId;
	}
}
