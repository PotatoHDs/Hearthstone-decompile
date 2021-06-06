using System;
using System.IO;

namespace bnet.protocol.friends.v1
{
	// Token: 0x0200042D RID: 1069
	public class SentInvitationRemovedNotification : IProtoBuf
	{
		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x060047B2 RID: 18354 RVA: 0x000E0241 File Offset: 0x000DE441
		// (set) Token: 0x060047B3 RID: 18355 RVA: 0x000E0249 File Offset: 0x000DE449
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

		// Token: 0x060047B4 RID: 18356 RVA: 0x000E025C File Offset: 0x000DE45C
		public void SetAccountId(EntityId val)
		{
			this.AccountId = val;
		}

		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x060047B5 RID: 18357 RVA: 0x000E0265 File Offset: 0x000DE465
		// (set) Token: 0x060047B6 RID: 18358 RVA: 0x000E026D File Offset: 0x000DE46D
		public ulong InvitationId
		{
			get
			{
				return this._InvitationId;
			}
			set
			{
				this._InvitationId = value;
				this.HasInvitationId = true;
			}
		}

		// Token: 0x060047B7 RID: 18359 RVA: 0x000E027D File Offset: 0x000DE47D
		public void SetInvitationId(ulong val)
		{
			this.InvitationId = val;
		}

		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x060047B8 RID: 18360 RVA: 0x000E0286 File Offset: 0x000DE486
		// (set) Token: 0x060047B9 RID: 18361 RVA: 0x000E028E File Offset: 0x000DE48E
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

		// Token: 0x060047BA RID: 18362 RVA: 0x000E029E File Offset: 0x000DE49E
		public void SetReason(uint val)
		{
			this.Reason = val;
		}

		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x060047BB RID: 18363 RVA: 0x000E02A7 File Offset: 0x000DE4A7
		// (set) Token: 0x060047BC RID: 18364 RVA: 0x000E02AF File Offset: 0x000DE4AF
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

		// Token: 0x060047BD RID: 18365 RVA: 0x000E02C2 File Offset: 0x000DE4C2
		public void SetForward(ObjectAddress val)
		{
			this.Forward = val;
		}

		// Token: 0x060047BE RID: 18366 RVA: 0x000E02CC File Offset: 0x000DE4CC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAccountId)
			{
				num ^= this.AccountId.GetHashCode();
			}
			if (this.HasInvitationId)
			{
				num ^= this.InvitationId.GetHashCode();
			}
			if (this.HasReason)
			{
				num ^= this.Reason.GetHashCode();
			}
			if (this.HasForward)
			{
				num ^= this.Forward.GetHashCode();
			}
			return num;
		}

		// Token: 0x060047BF RID: 18367 RVA: 0x000E0344 File Offset: 0x000DE544
		public override bool Equals(object obj)
		{
			SentInvitationRemovedNotification sentInvitationRemovedNotification = obj as SentInvitationRemovedNotification;
			return sentInvitationRemovedNotification != null && this.HasAccountId == sentInvitationRemovedNotification.HasAccountId && (!this.HasAccountId || this.AccountId.Equals(sentInvitationRemovedNotification.AccountId)) && this.HasInvitationId == sentInvitationRemovedNotification.HasInvitationId && (!this.HasInvitationId || this.InvitationId.Equals(sentInvitationRemovedNotification.InvitationId)) && this.HasReason == sentInvitationRemovedNotification.HasReason && (!this.HasReason || this.Reason.Equals(sentInvitationRemovedNotification.Reason)) && this.HasForward == sentInvitationRemovedNotification.HasForward && (!this.HasForward || this.Forward.Equals(sentInvitationRemovedNotification.Forward));
		}

		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x060047C0 RID: 18368 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060047C1 RID: 18369 RVA: 0x000E0410 File Offset: 0x000DE610
		public static SentInvitationRemovedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SentInvitationRemovedNotification>(bs, 0, -1);
		}

		// Token: 0x060047C2 RID: 18370 RVA: 0x000E041A File Offset: 0x000DE61A
		public void Deserialize(Stream stream)
		{
			SentInvitationRemovedNotification.Deserialize(stream, this);
		}

		// Token: 0x060047C3 RID: 18371 RVA: 0x000E0424 File Offset: 0x000DE624
		public static SentInvitationRemovedNotification Deserialize(Stream stream, SentInvitationRemovedNotification instance)
		{
			return SentInvitationRemovedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060047C4 RID: 18372 RVA: 0x000E0430 File Offset: 0x000DE630
		public static SentInvitationRemovedNotification DeserializeLengthDelimited(Stream stream)
		{
			SentInvitationRemovedNotification sentInvitationRemovedNotification = new SentInvitationRemovedNotification();
			SentInvitationRemovedNotification.DeserializeLengthDelimited(stream, sentInvitationRemovedNotification);
			return sentInvitationRemovedNotification;
		}

		// Token: 0x060047C5 RID: 18373 RVA: 0x000E044C File Offset: 0x000DE64C
		public static SentInvitationRemovedNotification DeserializeLengthDelimited(Stream stream, SentInvitationRemovedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SentInvitationRemovedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x060047C6 RID: 18374 RVA: 0x000E0474 File Offset: 0x000DE674
		public static SentInvitationRemovedNotification Deserialize(Stream stream, SentInvitationRemovedNotification instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
				else
				{
					if (num <= 17)
					{
						if (num != 10)
						{
							if (num == 17)
							{
								instance.InvitationId = binaryReader.ReadUInt64();
								continue;
							}
						}
						else
						{
							if (instance.AccountId == null)
							{
								instance.AccountId = EntityId.DeserializeLengthDelimited(stream);
								continue;
							}
							EntityId.DeserializeLengthDelimited(stream, instance.AccountId);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.Reason = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num == 34)
						{
							if (instance.Forward == null)
							{
								instance.Forward = ObjectAddress.DeserializeLengthDelimited(stream);
								continue;
							}
							ObjectAddress.DeserializeLengthDelimited(stream, instance.Forward);
							continue;
						}
					}
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

		// Token: 0x060047C7 RID: 18375 RVA: 0x000E0586 File Offset: 0x000DE786
		public void Serialize(Stream stream)
		{
			SentInvitationRemovedNotification.Serialize(stream, this);
		}

		// Token: 0x060047C8 RID: 18376 RVA: 0x000E0590 File Offset: 0x000DE790
		public static void Serialize(Stream stream, SentInvitationRemovedNotification instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasAccountId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AccountId);
			}
			if (instance.HasInvitationId)
			{
				stream.WriteByte(17);
				binaryWriter.Write(instance.InvitationId);
			}
			if (instance.HasReason)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.Reason);
			}
			if (instance.HasForward)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.Forward.GetSerializedSize());
				ObjectAddress.Serialize(stream, instance.Forward);
			}
		}

		// Token: 0x060047C9 RID: 18377 RVA: 0x000E0638 File Offset: 0x000DE838
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAccountId)
			{
				num += 1U;
				uint serializedSize = this.AccountId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasInvitationId)
			{
				num += 1U;
				num += 8U;
			}
			if (this.HasReason)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Reason);
			}
			if (this.HasForward)
			{
				num += 1U;
				uint serializedSize2 = this.Forward.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x040017D7 RID: 6103
		public bool HasAccountId;

		// Token: 0x040017D8 RID: 6104
		private EntityId _AccountId;

		// Token: 0x040017D9 RID: 6105
		public bool HasInvitationId;

		// Token: 0x040017DA RID: 6106
		private ulong _InvitationId;

		// Token: 0x040017DB RID: 6107
		public bool HasReason;

		// Token: 0x040017DC RID: 6108
		private uint _Reason;

		// Token: 0x040017DD RID: 6109
		public bool HasForward;

		// Token: 0x040017DE RID: 6110
		private ObjectAddress _Forward;
	}
}
