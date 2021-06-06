using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x02000516 RID: 1302
	public class GetGameTimeRemainingInfoRequest : IProtoBuf
	{
		// Token: 0x1700117E RID: 4478
		// (get) Token: 0x06005CCB RID: 23755 RVA: 0x00119E8F File Offset: 0x0011808F
		// (set) Token: 0x06005CCC RID: 23756 RVA: 0x00119E97 File Offset: 0x00118097
		public EntityId GameAccountId
		{
			get
			{
				return this._GameAccountId;
			}
			set
			{
				this._GameAccountId = value;
				this.HasGameAccountId = (value != null);
			}
		}

		// Token: 0x06005CCD RID: 23757 RVA: 0x00119EAA File Offset: 0x001180AA
		public void SetGameAccountId(EntityId val)
		{
			this.GameAccountId = val;
		}

		// Token: 0x1700117F RID: 4479
		// (get) Token: 0x06005CCE RID: 23758 RVA: 0x00119EB3 File Offset: 0x001180B3
		// (set) Token: 0x06005CCF RID: 23759 RVA: 0x00119EBB File Offset: 0x001180BB
		public EntityId AccountId
		{
			get
			{
				return this._AccountId;
			}
			set
			{
				this._AccountId = value;
				this.HasAccountId = (value != null);
			}
		}

		// Token: 0x06005CD0 RID: 23760 RVA: 0x00119ECE File Offset: 0x001180CE
		public void SetAccountId(EntityId val)
		{
			this.AccountId = val;
		}

		// Token: 0x06005CD1 RID: 23761 RVA: 0x00119ED8 File Offset: 0x001180D8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasGameAccountId)
			{
				num ^= this.GameAccountId.GetHashCode();
			}
			if (this.HasAccountId)
			{
				num ^= this.AccountId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005CD2 RID: 23762 RVA: 0x00119F20 File Offset: 0x00118120
		public override bool Equals(object obj)
		{
			GetGameTimeRemainingInfoRequest getGameTimeRemainingInfoRequest = obj as GetGameTimeRemainingInfoRequest;
			return getGameTimeRemainingInfoRequest != null && this.HasGameAccountId == getGameTimeRemainingInfoRequest.HasGameAccountId && (!this.HasGameAccountId || this.GameAccountId.Equals(getGameTimeRemainingInfoRequest.GameAccountId)) && this.HasAccountId == getGameTimeRemainingInfoRequest.HasAccountId && (!this.HasAccountId || this.AccountId.Equals(getGameTimeRemainingInfoRequest.AccountId));
		}

		// Token: 0x17001180 RID: 4480
		// (get) Token: 0x06005CD3 RID: 23763 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005CD4 RID: 23764 RVA: 0x00119F90 File Offset: 0x00118190
		public static GetGameTimeRemainingInfoRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetGameTimeRemainingInfoRequest>(bs, 0, -1);
		}

		// Token: 0x06005CD5 RID: 23765 RVA: 0x00119F9A File Offset: 0x0011819A
		public void Deserialize(Stream stream)
		{
			GetGameTimeRemainingInfoRequest.Deserialize(stream, this);
		}

		// Token: 0x06005CD6 RID: 23766 RVA: 0x00119FA4 File Offset: 0x001181A4
		public static GetGameTimeRemainingInfoRequest Deserialize(Stream stream, GetGameTimeRemainingInfoRequest instance)
		{
			return GetGameTimeRemainingInfoRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005CD7 RID: 23767 RVA: 0x00119FB0 File Offset: 0x001181B0
		public static GetGameTimeRemainingInfoRequest DeserializeLengthDelimited(Stream stream)
		{
			GetGameTimeRemainingInfoRequest getGameTimeRemainingInfoRequest = new GetGameTimeRemainingInfoRequest();
			GetGameTimeRemainingInfoRequest.DeserializeLengthDelimited(stream, getGameTimeRemainingInfoRequest);
			return getGameTimeRemainingInfoRequest;
		}

		// Token: 0x06005CD8 RID: 23768 RVA: 0x00119FCC File Offset: 0x001181CC
		public static GetGameTimeRemainingInfoRequest DeserializeLengthDelimited(Stream stream, GetGameTimeRemainingInfoRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetGameTimeRemainingInfoRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06005CD9 RID: 23769 RVA: 0x00119FF4 File Offset: 0x001181F4
		public static GetGameTimeRemainingInfoRequest Deserialize(Stream stream, GetGameTimeRemainingInfoRequest instance, long limit)
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
					else if (instance.AccountId == null)
					{
						instance.AccountId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.AccountId);
					}
				}
				else if (instance.GameAccountId == null)
				{
					instance.GameAccountId = EntityId.DeserializeLengthDelimited(stream);
				}
				else
				{
					EntityId.DeserializeLengthDelimited(stream, instance.GameAccountId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06005CDA RID: 23770 RVA: 0x0011A0C6 File Offset: 0x001182C6
		public void Serialize(Stream stream)
		{
			GetGameTimeRemainingInfoRequest.Serialize(stream, this);
		}

		// Token: 0x06005CDB RID: 23771 RVA: 0x0011A0D0 File Offset: 0x001182D0
		public static void Serialize(Stream stream, GetGameTimeRemainingInfoRequest instance)
		{
			if (instance.HasGameAccountId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameAccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.GameAccountId);
			}
			if (instance.HasAccountId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.AccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AccountId);
			}
		}

		// Token: 0x06005CDC RID: 23772 RVA: 0x0011A138 File Offset: 0x00118338
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasGameAccountId)
			{
				num += 1U;
				uint serializedSize = this.GameAccountId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasAccountId)
			{
				num += 1U;
				uint serializedSize2 = this.AccountId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x04001CAC RID: 7340
		public bool HasGameAccountId;

		// Token: 0x04001CAD RID: 7341
		private EntityId _GameAccountId;

		// Token: 0x04001CAE RID: 7342
		public bool HasAccountId;

		// Token: 0x04001CAF RID: 7343
		private EntityId _AccountId;
	}
}
