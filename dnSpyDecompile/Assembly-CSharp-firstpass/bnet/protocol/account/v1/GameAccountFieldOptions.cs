using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x0200052B RID: 1323
	public class GameAccountFieldOptions : IProtoBuf
	{
		// Token: 0x170011DD RID: 4573
		// (get) Token: 0x06005EAE RID: 24238 RVA: 0x0011EC74 File Offset: 0x0011CE74
		// (set) Token: 0x06005EAF RID: 24239 RVA: 0x0011EC7C File Offset: 0x0011CE7C
		public bool AllFields
		{
			get
			{
				return this._AllFields;
			}
			set
			{
				this._AllFields = value;
				this.HasAllFields = true;
			}
		}

		// Token: 0x06005EB0 RID: 24240 RVA: 0x0011EC8C File Offset: 0x0011CE8C
		public void SetAllFields(bool val)
		{
			this.AllFields = val;
		}

		// Token: 0x170011DE RID: 4574
		// (get) Token: 0x06005EB1 RID: 24241 RVA: 0x0011EC95 File Offset: 0x0011CE95
		// (set) Token: 0x06005EB2 RID: 24242 RVA: 0x0011EC9D File Offset: 0x0011CE9D
		public bool FieldGameLevelInfo
		{
			get
			{
				return this._FieldGameLevelInfo;
			}
			set
			{
				this._FieldGameLevelInfo = value;
				this.HasFieldGameLevelInfo = true;
			}
		}

		// Token: 0x06005EB3 RID: 24243 RVA: 0x0011ECAD File Offset: 0x0011CEAD
		public void SetFieldGameLevelInfo(bool val)
		{
			this.FieldGameLevelInfo = val;
		}

		// Token: 0x170011DF RID: 4575
		// (get) Token: 0x06005EB4 RID: 24244 RVA: 0x0011ECB6 File Offset: 0x0011CEB6
		// (set) Token: 0x06005EB5 RID: 24245 RVA: 0x0011ECBE File Offset: 0x0011CEBE
		public bool FieldGameTimeInfo
		{
			get
			{
				return this._FieldGameTimeInfo;
			}
			set
			{
				this._FieldGameTimeInfo = value;
				this.HasFieldGameTimeInfo = true;
			}
		}

		// Token: 0x06005EB6 RID: 24246 RVA: 0x0011ECCE File Offset: 0x0011CECE
		public void SetFieldGameTimeInfo(bool val)
		{
			this.FieldGameTimeInfo = val;
		}

		// Token: 0x170011E0 RID: 4576
		// (get) Token: 0x06005EB7 RID: 24247 RVA: 0x0011ECD7 File Offset: 0x0011CED7
		// (set) Token: 0x06005EB8 RID: 24248 RVA: 0x0011ECDF File Offset: 0x0011CEDF
		public bool FieldGameStatus
		{
			get
			{
				return this._FieldGameStatus;
			}
			set
			{
				this._FieldGameStatus = value;
				this.HasFieldGameStatus = true;
			}
		}

		// Token: 0x06005EB9 RID: 24249 RVA: 0x0011ECEF File Offset: 0x0011CEEF
		public void SetFieldGameStatus(bool val)
		{
			this.FieldGameStatus = val;
		}

		// Token: 0x170011E1 RID: 4577
		// (get) Token: 0x06005EBA RID: 24250 RVA: 0x0011ECF8 File Offset: 0x0011CEF8
		// (set) Token: 0x06005EBB RID: 24251 RVA: 0x0011ED00 File Offset: 0x0011CF00
		public bool FieldRafInfo
		{
			get
			{
				return this._FieldRafInfo;
			}
			set
			{
				this._FieldRafInfo = value;
				this.HasFieldRafInfo = true;
			}
		}

		// Token: 0x06005EBC RID: 24252 RVA: 0x0011ED10 File Offset: 0x0011CF10
		public void SetFieldRafInfo(bool val)
		{
			this.FieldRafInfo = val;
		}

		// Token: 0x06005EBD RID: 24253 RVA: 0x0011ED1C File Offset: 0x0011CF1C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAllFields)
			{
				num ^= this.AllFields.GetHashCode();
			}
			if (this.HasFieldGameLevelInfo)
			{
				num ^= this.FieldGameLevelInfo.GetHashCode();
			}
			if (this.HasFieldGameTimeInfo)
			{
				num ^= this.FieldGameTimeInfo.GetHashCode();
			}
			if (this.HasFieldGameStatus)
			{
				num ^= this.FieldGameStatus.GetHashCode();
			}
			if (this.HasFieldRafInfo)
			{
				num ^= this.FieldRafInfo.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005EBE RID: 24254 RVA: 0x0011EDB4 File Offset: 0x0011CFB4
		public override bool Equals(object obj)
		{
			GameAccountFieldOptions gameAccountFieldOptions = obj as GameAccountFieldOptions;
			return gameAccountFieldOptions != null && this.HasAllFields == gameAccountFieldOptions.HasAllFields && (!this.HasAllFields || this.AllFields.Equals(gameAccountFieldOptions.AllFields)) && this.HasFieldGameLevelInfo == gameAccountFieldOptions.HasFieldGameLevelInfo && (!this.HasFieldGameLevelInfo || this.FieldGameLevelInfo.Equals(gameAccountFieldOptions.FieldGameLevelInfo)) && this.HasFieldGameTimeInfo == gameAccountFieldOptions.HasFieldGameTimeInfo && (!this.HasFieldGameTimeInfo || this.FieldGameTimeInfo.Equals(gameAccountFieldOptions.FieldGameTimeInfo)) && this.HasFieldGameStatus == gameAccountFieldOptions.HasFieldGameStatus && (!this.HasFieldGameStatus || this.FieldGameStatus.Equals(gameAccountFieldOptions.FieldGameStatus)) && this.HasFieldRafInfo == gameAccountFieldOptions.HasFieldRafInfo && (!this.HasFieldRafInfo || this.FieldRafInfo.Equals(gameAccountFieldOptions.FieldRafInfo));
		}

		// Token: 0x170011E2 RID: 4578
		// (get) Token: 0x06005EBF RID: 24255 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005EC0 RID: 24256 RVA: 0x0011EEB4 File Offset: 0x0011D0B4
		public static GameAccountFieldOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameAccountFieldOptions>(bs, 0, -1);
		}

		// Token: 0x06005EC1 RID: 24257 RVA: 0x0011EEBE File Offset: 0x0011D0BE
		public void Deserialize(Stream stream)
		{
			GameAccountFieldOptions.Deserialize(stream, this);
		}

		// Token: 0x06005EC2 RID: 24258 RVA: 0x0011EEC8 File Offset: 0x0011D0C8
		public static GameAccountFieldOptions Deserialize(Stream stream, GameAccountFieldOptions instance)
		{
			return GameAccountFieldOptions.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005EC3 RID: 24259 RVA: 0x0011EED4 File Offset: 0x0011D0D4
		public static GameAccountFieldOptions DeserializeLengthDelimited(Stream stream)
		{
			GameAccountFieldOptions gameAccountFieldOptions = new GameAccountFieldOptions();
			GameAccountFieldOptions.DeserializeLengthDelimited(stream, gameAccountFieldOptions);
			return gameAccountFieldOptions;
		}

		// Token: 0x06005EC4 RID: 24260 RVA: 0x0011EEF0 File Offset: 0x0011D0F0
		public static GameAccountFieldOptions DeserializeLengthDelimited(Stream stream, GameAccountFieldOptions instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameAccountFieldOptions.Deserialize(stream, instance, num);
		}

		// Token: 0x06005EC5 RID: 24261 RVA: 0x0011EF18 File Offset: 0x0011D118
		public static GameAccountFieldOptions Deserialize(Stream stream, GameAccountFieldOptions instance, long limit)
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
					if (num <= 16)
					{
						if (num == 8)
						{
							instance.AllFields = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 16)
						{
							instance.FieldGameLevelInfo = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.FieldGameTimeInfo = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 32)
						{
							instance.FieldGameStatus = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 40)
						{
							instance.FieldRafInfo = ProtocolParser.ReadBool(stream);
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

		// Token: 0x06005EC6 RID: 24262 RVA: 0x0011EFFE File Offset: 0x0011D1FE
		public void Serialize(Stream stream)
		{
			GameAccountFieldOptions.Serialize(stream, this);
		}

		// Token: 0x06005EC7 RID: 24263 RVA: 0x0011F008 File Offset: 0x0011D208
		public static void Serialize(Stream stream, GameAccountFieldOptions instance)
		{
			if (instance.HasAllFields)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.AllFields);
			}
			if (instance.HasFieldGameLevelInfo)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.FieldGameLevelInfo);
			}
			if (instance.HasFieldGameTimeInfo)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.FieldGameTimeInfo);
			}
			if (instance.HasFieldGameStatus)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.FieldGameStatus);
			}
			if (instance.HasFieldRafInfo)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.FieldRafInfo);
			}
		}

		// Token: 0x06005EC8 RID: 24264 RVA: 0x0011F0A0 File Offset: 0x0011D2A0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAllFields)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasFieldGameLevelInfo)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasFieldGameTimeInfo)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasFieldGameStatus)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasFieldRafInfo)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x04001D1D RID: 7453
		public bool HasAllFields;

		// Token: 0x04001D1E RID: 7454
		private bool _AllFields;

		// Token: 0x04001D1F RID: 7455
		public bool HasFieldGameLevelInfo;

		// Token: 0x04001D20 RID: 7456
		private bool _FieldGameLevelInfo;

		// Token: 0x04001D21 RID: 7457
		public bool HasFieldGameTimeInfo;

		// Token: 0x04001D22 RID: 7458
		private bool _FieldGameTimeInfo;

		// Token: 0x04001D23 RID: 7459
		public bool HasFieldGameStatus;

		// Token: 0x04001D24 RID: 7460
		private bool _FieldGameStatus;

		// Token: 0x04001D25 RID: 7461
		public bool HasFieldRafInfo;

		// Token: 0x04001D26 RID: 7462
		private bool _FieldRafInfo;
	}
}
