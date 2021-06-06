using System;
using System.IO;

namespace bnet.protocol.game_utilities.v1
{
	// Token: 0x0200035A RID: 858
	public class PresenceChannelCreatedRequest : IProtoBuf
	{
		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06003618 RID: 13848 RVA: 0x000B2DAF File Offset: 0x000B0FAF
		// (set) Token: 0x06003619 RID: 13849 RVA: 0x000B2DB7 File Offset: 0x000B0FB7
		public EntityId Id { get; set; }

		// Token: 0x0600361A RID: 13850 RVA: 0x000B2DC0 File Offset: 0x000B0FC0
		public void SetId(EntityId val)
		{
			this.Id = val;
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x0600361B RID: 13851 RVA: 0x000B2DC9 File Offset: 0x000B0FC9
		// (set) Token: 0x0600361C RID: 13852 RVA: 0x000B2DD1 File Offset: 0x000B0FD1
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

		// Token: 0x0600361D RID: 13853 RVA: 0x000B2DE4 File Offset: 0x000B0FE4
		public void SetGameAccountId(EntityId val)
		{
			this.GameAccountId = val;
		}

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x0600361E RID: 13854 RVA: 0x000B2DED File Offset: 0x000B0FED
		// (set) Token: 0x0600361F RID: 13855 RVA: 0x000B2DF5 File Offset: 0x000B0FF5
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

		// Token: 0x06003620 RID: 13856 RVA: 0x000B2E08 File Offset: 0x000B1008
		public void SetAccountId(EntityId val)
		{
			this.AccountId = val;
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06003621 RID: 13857 RVA: 0x000B2E11 File Offset: 0x000B1011
		// (set) Token: 0x06003622 RID: 13858 RVA: 0x000B2E19 File Offset: 0x000B1019
		public ProcessId Host
		{
			get
			{
				return this._Host;
			}
			set
			{
				this._Host = value;
				this.HasHost = (value != null);
			}
		}

		// Token: 0x06003623 RID: 13859 RVA: 0x000B2E2C File Offset: 0x000B102C
		public void SetHost(ProcessId val)
		{
			this.Host = val;
		}

		// Token: 0x06003624 RID: 13860 RVA: 0x000B2E38 File Offset: 0x000B1038
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Id.GetHashCode();
			if (this.HasGameAccountId)
			{
				num ^= this.GameAccountId.GetHashCode();
			}
			if (this.HasAccountId)
			{
				num ^= this.AccountId.GetHashCode();
			}
			if (this.HasHost)
			{
				num ^= this.Host.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003625 RID: 13861 RVA: 0x000B2EA4 File Offset: 0x000B10A4
		public override bool Equals(object obj)
		{
			PresenceChannelCreatedRequest presenceChannelCreatedRequest = obj as PresenceChannelCreatedRequest;
			return presenceChannelCreatedRequest != null && this.Id.Equals(presenceChannelCreatedRequest.Id) && this.HasGameAccountId == presenceChannelCreatedRequest.HasGameAccountId && (!this.HasGameAccountId || this.GameAccountId.Equals(presenceChannelCreatedRequest.GameAccountId)) && this.HasAccountId == presenceChannelCreatedRequest.HasAccountId && (!this.HasAccountId || this.AccountId.Equals(presenceChannelCreatedRequest.AccountId)) && this.HasHost == presenceChannelCreatedRequest.HasHost && (!this.HasHost || this.Host.Equals(presenceChannelCreatedRequest.Host));
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06003626 RID: 13862 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003627 RID: 13863 RVA: 0x000B2F54 File Offset: 0x000B1154
		public static PresenceChannelCreatedRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<PresenceChannelCreatedRequest>(bs, 0, -1);
		}

		// Token: 0x06003628 RID: 13864 RVA: 0x000B2F5E File Offset: 0x000B115E
		public void Deserialize(Stream stream)
		{
			PresenceChannelCreatedRequest.Deserialize(stream, this);
		}

		// Token: 0x06003629 RID: 13865 RVA: 0x000B2F68 File Offset: 0x000B1168
		public static PresenceChannelCreatedRequest Deserialize(Stream stream, PresenceChannelCreatedRequest instance)
		{
			return PresenceChannelCreatedRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600362A RID: 13866 RVA: 0x000B2F74 File Offset: 0x000B1174
		public static PresenceChannelCreatedRequest DeserializeLengthDelimited(Stream stream)
		{
			PresenceChannelCreatedRequest presenceChannelCreatedRequest = new PresenceChannelCreatedRequest();
			PresenceChannelCreatedRequest.DeserializeLengthDelimited(stream, presenceChannelCreatedRequest);
			return presenceChannelCreatedRequest;
		}

		// Token: 0x0600362B RID: 13867 RVA: 0x000B2F90 File Offset: 0x000B1190
		public static PresenceChannelCreatedRequest DeserializeLengthDelimited(Stream stream, PresenceChannelCreatedRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PresenceChannelCreatedRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x0600362C RID: 13868 RVA: 0x000B2FB8 File Offset: 0x000B11B8
		public static PresenceChannelCreatedRequest Deserialize(Stream stream, PresenceChannelCreatedRequest instance, long limit)
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
				else
				{
					if (num <= 26)
					{
						if (num != 10)
						{
							if (num == 26)
							{
								if (instance.GameAccountId == null)
								{
									instance.GameAccountId = EntityId.DeserializeLengthDelimited(stream);
									continue;
								}
								EntityId.DeserializeLengthDelimited(stream, instance.GameAccountId);
								continue;
							}
						}
						else
						{
							if (instance.Id == null)
							{
								instance.Id = EntityId.DeserializeLengthDelimited(stream);
								continue;
							}
							EntityId.DeserializeLengthDelimited(stream, instance.Id);
							continue;
						}
					}
					else if (num != 34)
					{
						if (num == 42)
						{
							if (instance.Host == null)
							{
								instance.Host = ProcessId.DeserializeLengthDelimited(stream);
								continue;
							}
							ProcessId.DeserializeLengthDelimited(stream, instance.Host);
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

		// Token: 0x0600362D RID: 13869 RVA: 0x000B30FD File Offset: 0x000B12FD
		public void Serialize(Stream stream)
		{
			PresenceChannelCreatedRequest.Serialize(stream, this);
		}

		// Token: 0x0600362E RID: 13870 RVA: 0x000B3108 File Offset: 0x000B1308
		public static void Serialize(Stream stream, PresenceChannelCreatedRequest instance)
		{
			if (instance.Id == null)
			{
				throw new ArgumentNullException("Id", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Id.GetSerializedSize());
			EntityId.Serialize(stream, instance.Id);
			if (instance.HasGameAccountId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.GameAccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.GameAccountId);
			}
			if (instance.HasAccountId)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.AccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AccountId);
			}
			if (instance.HasHost)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Host);
			}
		}

		// Token: 0x0600362F RID: 13871 RVA: 0x000B31DC File Offset: 0x000B13DC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.Id.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasGameAccountId)
			{
				num += 1U;
				uint serializedSize2 = this.GameAccountId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasAccountId)
			{
				num += 1U;
				uint serializedSize3 = this.AccountId.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.HasHost)
			{
				num += 1U;
				uint serializedSize4 = this.Host.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			return num + 1U;
		}

		// Token: 0x04001484 RID: 5252
		public bool HasGameAccountId;

		// Token: 0x04001485 RID: 5253
		private EntityId _GameAccountId;

		// Token: 0x04001486 RID: 5254
		public bool HasAccountId;

		// Token: 0x04001487 RID: 5255
		private EntityId _AccountId;

		// Token: 0x04001488 RID: 5256
		public bool HasHost;

		// Token: 0x04001489 RID: 5257
		private ProcessId _Host;
	}
}
