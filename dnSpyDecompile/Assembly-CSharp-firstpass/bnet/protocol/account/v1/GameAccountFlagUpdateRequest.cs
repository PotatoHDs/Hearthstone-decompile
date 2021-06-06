using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x02000508 RID: 1288
	public class GameAccountFlagUpdateRequest : IProtoBuf
	{
		// Token: 0x1700114B RID: 4427
		// (get) Token: 0x06005BAC RID: 23468 RVA: 0x00117147 File Offset: 0x00115347
		// (set) Token: 0x06005BAD RID: 23469 RVA: 0x0011714F File Offset: 0x0011534F
		public GameAccountHandle GameAccount
		{
			get
			{
				return this._GameAccount;
			}
			set
			{
				this._GameAccount = value;
				this.HasGameAccount = (value != null);
			}
		}

		// Token: 0x06005BAE RID: 23470 RVA: 0x00117162 File Offset: 0x00115362
		public void SetGameAccount(GameAccountHandle val)
		{
			this.GameAccount = val;
		}

		// Token: 0x1700114C RID: 4428
		// (get) Token: 0x06005BAF RID: 23471 RVA: 0x0011716B File Offset: 0x0011536B
		// (set) Token: 0x06005BB0 RID: 23472 RVA: 0x00117173 File Offset: 0x00115373
		public ulong Flag
		{
			get
			{
				return this._Flag;
			}
			set
			{
				this._Flag = value;
				this.HasFlag = true;
			}
		}

		// Token: 0x06005BB1 RID: 23473 RVA: 0x00117183 File Offset: 0x00115383
		public void SetFlag(ulong val)
		{
			this.Flag = val;
		}

		// Token: 0x1700114D RID: 4429
		// (get) Token: 0x06005BB2 RID: 23474 RVA: 0x0011718C File Offset: 0x0011538C
		// (set) Token: 0x06005BB3 RID: 23475 RVA: 0x00117194 File Offset: 0x00115394
		public bool Active
		{
			get
			{
				return this._Active;
			}
			set
			{
				this._Active = value;
				this.HasActive = true;
			}
		}

		// Token: 0x06005BB4 RID: 23476 RVA: 0x001171A4 File Offset: 0x001153A4
		public void SetActive(bool val)
		{
			this.Active = val;
		}

		// Token: 0x06005BB5 RID: 23477 RVA: 0x001171B0 File Offset: 0x001153B0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasGameAccount)
			{
				num ^= this.GameAccount.GetHashCode();
			}
			if (this.HasFlag)
			{
				num ^= this.Flag.GetHashCode();
			}
			if (this.HasActive)
			{
				num ^= this.Active.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005BB6 RID: 23478 RVA: 0x00117214 File Offset: 0x00115414
		public override bool Equals(object obj)
		{
			GameAccountFlagUpdateRequest gameAccountFlagUpdateRequest = obj as GameAccountFlagUpdateRequest;
			return gameAccountFlagUpdateRequest != null && this.HasGameAccount == gameAccountFlagUpdateRequest.HasGameAccount && (!this.HasGameAccount || this.GameAccount.Equals(gameAccountFlagUpdateRequest.GameAccount)) && this.HasFlag == gameAccountFlagUpdateRequest.HasFlag && (!this.HasFlag || this.Flag.Equals(gameAccountFlagUpdateRequest.Flag)) && this.HasActive == gameAccountFlagUpdateRequest.HasActive && (!this.HasActive || this.Active.Equals(gameAccountFlagUpdateRequest.Active));
		}

		// Token: 0x1700114E RID: 4430
		// (get) Token: 0x06005BB7 RID: 23479 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005BB8 RID: 23480 RVA: 0x001172B5 File Offset: 0x001154B5
		public static GameAccountFlagUpdateRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameAccountFlagUpdateRequest>(bs, 0, -1);
		}

		// Token: 0x06005BB9 RID: 23481 RVA: 0x001172BF File Offset: 0x001154BF
		public void Deserialize(Stream stream)
		{
			GameAccountFlagUpdateRequest.Deserialize(stream, this);
		}

		// Token: 0x06005BBA RID: 23482 RVA: 0x001172C9 File Offset: 0x001154C9
		public static GameAccountFlagUpdateRequest Deserialize(Stream stream, GameAccountFlagUpdateRequest instance)
		{
			return GameAccountFlagUpdateRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005BBB RID: 23483 RVA: 0x001172D4 File Offset: 0x001154D4
		public static GameAccountFlagUpdateRequest DeserializeLengthDelimited(Stream stream)
		{
			GameAccountFlagUpdateRequest gameAccountFlagUpdateRequest = new GameAccountFlagUpdateRequest();
			GameAccountFlagUpdateRequest.DeserializeLengthDelimited(stream, gameAccountFlagUpdateRequest);
			return gameAccountFlagUpdateRequest;
		}

		// Token: 0x06005BBC RID: 23484 RVA: 0x001172F0 File Offset: 0x001154F0
		public static GameAccountFlagUpdateRequest DeserializeLengthDelimited(Stream stream, GameAccountFlagUpdateRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameAccountFlagUpdateRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06005BBD RID: 23485 RVA: 0x00117318 File Offset: 0x00115518
		public static GameAccountFlagUpdateRequest Deserialize(Stream stream, GameAccountFlagUpdateRequest instance, long limit)
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
							instance.Active = ProtocolParser.ReadBool(stream);
						}
					}
					else
					{
						instance.Flag = ProtocolParser.ReadUInt64(stream);
					}
				}
				else if (instance.GameAccount == null)
				{
					instance.GameAccount = GameAccountHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameAccountHandle.DeserializeLengthDelimited(stream, instance.GameAccount);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06005BBE RID: 23486 RVA: 0x001173E6 File Offset: 0x001155E6
		public void Serialize(Stream stream)
		{
			GameAccountFlagUpdateRequest.Serialize(stream, this);
		}

		// Token: 0x06005BBF RID: 23487 RVA: 0x001173F0 File Offset: 0x001155F0
		public static void Serialize(Stream stream, GameAccountFlagUpdateRequest instance)
		{
			if (instance.HasGameAccount)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameAccount.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.GameAccount);
			}
			if (instance.HasFlag)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.Flag);
			}
			if (instance.HasActive)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.Active);
			}
		}

		// Token: 0x06005BC0 RID: 23488 RVA: 0x00117464 File Offset: 0x00115664
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasGameAccount)
			{
				num += 1U;
				uint serializedSize = this.GameAccount.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasFlag)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.Flag);
			}
			if (this.HasActive)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x04001C71 RID: 7281
		public bool HasGameAccount;

		// Token: 0x04001C72 RID: 7282
		private GameAccountHandle _GameAccount;

		// Token: 0x04001C73 RID: 7283
		public bool HasFlag;

		// Token: 0x04001C74 RID: 7284
		private ulong _Flag;

		// Token: 0x04001C75 RID: 7285
		public bool HasActive;

		// Token: 0x04001C76 RID: 7286
		private bool _Active;
	}
}
