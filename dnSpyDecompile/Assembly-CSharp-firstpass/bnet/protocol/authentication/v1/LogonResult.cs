using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.authentication.v1
{
	// Token: 0x020004ED RID: 1261
	public class LogonResult : IProtoBuf
	{
		// Token: 0x170010E0 RID: 4320
		// (get) Token: 0x06005963 RID: 22883 RVA: 0x001118D4 File Offset: 0x0010FAD4
		// (set) Token: 0x06005964 RID: 22884 RVA: 0x001118DC File Offset: 0x0010FADC
		public uint ErrorCode { get; set; }

		// Token: 0x06005965 RID: 22885 RVA: 0x001118E5 File Offset: 0x0010FAE5
		public void SetErrorCode(uint val)
		{
			this.ErrorCode = val;
		}

		// Token: 0x170010E1 RID: 4321
		// (get) Token: 0x06005966 RID: 22886 RVA: 0x001118EE File Offset: 0x0010FAEE
		// (set) Token: 0x06005967 RID: 22887 RVA: 0x001118F6 File Offset: 0x0010FAF6
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

		// Token: 0x06005968 RID: 22888 RVA: 0x00111909 File Offset: 0x0010FB09
		public void SetAccountId(EntityId val)
		{
			this.AccountId = val;
		}

		// Token: 0x170010E2 RID: 4322
		// (get) Token: 0x06005969 RID: 22889 RVA: 0x00111912 File Offset: 0x0010FB12
		// (set) Token: 0x0600596A RID: 22890 RVA: 0x0011191A File Offset: 0x0010FB1A
		public List<EntityId> GameAccountId
		{
			get
			{
				return this._GameAccountId;
			}
			set
			{
				this._GameAccountId = value;
			}
		}

		// Token: 0x170010E3 RID: 4323
		// (get) Token: 0x0600596B RID: 22891 RVA: 0x00111912 File Offset: 0x0010FB12
		public List<EntityId> GameAccountIdList
		{
			get
			{
				return this._GameAccountId;
			}
		}

		// Token: 0x170010E4 RID: 4324
		// (get) Token: 0x0600596C RID: 22892 RVA: 0x00111923 File Offset: 0x0010FB23
		public int GameAccountIdCount
		{
			get
			{
				return this._GameAccountId.Count;
			}
		}

		// Token: 0x0600596D RID: 22893 RVA: 0x00111930 File Offset: 0x0010FB30
		public void AddGameAccountId(EntityId val)
		{
			this._GameAccountId.Add(val);
		}

		// Token: 0x0600596E RID: 22894 RVA: 0x0011193E File Offset: 0x0010FB3E
		public void ClearGameAccountId()
		{
			this._GameAccountId.Clear();
		}

		// Token: 0x0600596F RID: 22895 RVA: 0x0011194B File Offset: 0x0010FB4B
		public void SetGameAccountId(List<EntityId> val)
		{
			this.GameAccountId = val;
		}

		// Token: 0x170010E5 RID: 4325
		// (get) Token: 0x06005970 RID: 22896 RVA: 0x00111954 File Offset: 0x0010FB54
		// (set) Token: 0x06005971 RID: 22897 RVA: 0x0011195C File Offset: 0x0010FB5C
		public string Email
		{
			get
			{
				return this._Email;
			}
			set
			{
				this._Email = value;
				this.HasEmail = (value != null);
			}
		}

		// Token: 0x06005972 RID: 22898 RVA: 0x0011196F File Offset: 0x0010FB6F
		public void SetEmail(string val)
		{
			this.Email = val;
		}

		// Token: 0x170010E6 RID: 4326
		// (get) Token: 0x06005973 RID: 22899 RVA: 0x00111978 File Offset: 0x0010FB78
		// (set) Token: 0x06005974 RID: 22900 RVA: 0x00111980 File Offset: 0x0010FB80
		public List<uint> AvailableRegion
		{
			get
			{
				return this._AvailableRegion;
			}
			set
			{
				this._AvailableRegion = value;
			}
		}

		// Token: 0x170010E7 RID: 4327
		// (get) Token: 0x06005975 RID: 22901 RVA: 0x00111978 File Offset: 0x0010FB78
		public List<uint> AvailableRegionList
		{
			get
			{
				return this._AvailableRegion;
			}
		}

		// Token: 0x170010E8 RID: 4328
		// (get) Token: 0x06005976 RID: 22902 RVA: 0x00111989 File Offset: 0x0010FB89
		public int AvailableRegionCount
		{
			get
			{
				return this._AvailableRegion.Count;
			}
		}

		// Token: 0x06005977 RID: 22903 RVA: 0x00111996 File Offset: 0x0010FB96
		public void AddAvailableRegion(uint val)
		{
			this._AvailableRegion.Add(val);
		}

		// Token: 0x06005978 RID: 22904 RVA: 0x001119A4 File Offset: 0x0010FBA4
		public void ClearAvailableRegion()
		{
			this._AvailableRegion.Clear();
		}

		// Token: 0x06005979 RID: 22905 RVA: 0x001119B1 File Offset: 0x0010FBB1
		public void SetAvailableRegion(List<uint> val)
		{
			this.AvailableRegion = val;
		}

		// Token: 0x170010E9 RID: 4329
		// (get) Token: 0x0600597A RID: 22906 RVA: 0x001119BA File Offset: 0x0010FBBA
		// (set) Token: 0x0600597B RID: 22907 RVA: 0x001119C2 File Offset: 0x0010FBC2
		public uint ConnectedRegion
		{
			get
			{
				return this._ConnectedRegion;
			}
			set
			{
				this._ConnectedRegion = value;
				this.HasConnectedRegion = true;
			}
		}

		// Token: 0x0600597C RID: 22908 RVA: 0x001119D2 File Offset: 0x0010FBD2
		public void SetConnectedRegion(uint val)
		{
			this.ConnectedRegion = val;
		}

		// Token: 0x170010EA RID: 4330
		// (get) Token: 0x0600597D RID: 22909 RVA: 0x001119DB File Offset: 0x0010FBDB
		// (set) Token: 0x0600597E RID: 22910 RVA: 0x001119E3 File Offset: 0x0010FBE3
		public string BattleTag
		{
			get
			{
				return this._BattleTag;
			}
			set
			{
				this._BattleTag = value;
				this.HasBattleTag = (value != null);
			}
		}

		// Token: 0x0600597F RID: 22911 RVA: 0x001119F6 File Offset: 0x0010FBF6
		public void SetBattleTag(string val)
		{
			this.BattleTag = val;
		}

		// Token: 0x170010EB RID: 4331
		// (get) Token: 0x06005980 RID: 22912 RVA: 0x001119FF File Offset: 0x0010FBFF
		// (set) Token: 0x06005981 RID: 22913 RVA: 0x00111A07 File Offset: 0x0010FC07
		public string GeoipCountry
		{
			get
			{
				return this._GeoipCountry;
			}
			set
			{
				this._GeoipCountry = value;
				this.HasGeoipCountry = (value != null);
			}
		}

		// Token: 0x06005982 RID: 22914 RVA: 0x00111A1A File Offset: 0x0010FC1A
		public void SetGeoipCountry(string val)
		{
			this.GeoipCountry = val;
		}

		// Token: 0x170010EC RID: 4332
		// (get) Token: 0x06005983 RID: 22915 RVA: 0x00111A23 File Offset: 0x0010FC23
		// (set) Token: 0x06005984 RID: 22916 RVA: 0x00111A2B File Offset: 0x0010FC2B
		public byte[] SessionKey
		{
			get
			{
				return this._SessionKey;
			}
			set
			{
				this._SessionKey = value;
				this.HasSessionKey = (value != null);
			}
		}

		// Token: 0x06005985 RID: 22917 RVA: 0x00111A3E File Offset: 0x0010FC3E
		public void SetSessionKey(byte[] val)
		{
			this.SessionKey = val;
		}

		// Token: 0x170010ED RID: 4333
		// (get) Token: 0x06005986 RID: 22918 RVA: 0x00111A47 File Offset: 0x0010FC47
		// (set) Token: 0x06005987 RID: 22919 RVA: 0x00111A4F File Offset: 0x0010FC4F
		public bool RestrictedMode
		{
			get
			{
				return this._RestrictedMode;
			}
			set
			{
				this._RestrictedMode = value;
				this.HasRestrictedMode = true;
			}
		}

		// Token: 0x06005988 RID: 22920 RVA: 0x00111A5F File Offset: 0x0010FC5F
		public void SetRestrictedMode(bool val)
		{
			this.RestrictedMode = val;
		}

		// Token: 0x170010EE RID: 4334
		// (get) Token: 0x06005989 RID: 22921 RVA: 0x00111A68 File Offset: 0x0010FC68
		// (set) Token: 0x0600598A RID: 22922 RVA: 0x00111A70 File Offset: 0x0010FC70
		public string ClientId
		{
			get
			{
				return this._ClientId;
			}
			set
			{
				this._ClientId = value;
				this.HasClientId = (value != null);
			}
		}

		// Token: 0x0600598B RID: 22923 RVA: 0x00111A83 File Offset: 0x0010FC83
		public void SetClientId(string val)
		{
			this.ClientId = val;
		}

		// Token: 0x0600598C RID: 22924 RVA: 0x00111A8C File Offset: 0x0010FC8C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ErrorCode.GetHashCode();
			if (this.HasAccountId)
			{
				num ^= this.AccountId.GetHashCode();
			}
			foreach (EntityId entityId in this.GameAccountId)
			{
				num ^= entityId.GetHashCode();
			}
			if (this.HasEmail)
			{
				num ^= this.Email.GetHashCode();
			}
			foreach (uint num2 in this.AvailableRegion)
			{
				num ^= num2.GetHashCode();
			}
			if (this.HasConnectedRegion)
			{
				num ^= this.ConnectedRegion.GetHashCode();
			}
			if (this.HasBattleTag)
			{
				num ^= this.BattleTag.GetHashCode();
			}
			if (this.HasGeoipCountry)
			{
				num ^= this.GeoipCountry.GetHashCode();
			}
			if (this.HasSessionKey)
			{
				num ^= this.SessionKey.GetHashCode();
			}
			if (this.HasRestrictedMode)
			{
				num ^= this.RestrictedMode.GetHashCode();
			}
			if (this.HasClientId)
			{
				num ^= this.ClientId.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600598D RID: 22925 RVA: 0x00111C00 File Offset: 0x0010FE00
		public override bool Equals(object obj)
		{
			LogonResult logonResult = obj as LogonResult;
			if (logonResult == null)
			{
				return false;
			}
			if (!this.ErrorCode.Equals(logonResult.ErrorCode))
			{
				return false;
			}
			if (this.HasAccountId != logonResult.HasAccountId || (this.HasAccountId && !this.AccountId.Equals(logonResult.AccountId)))
			{
				return false;
			}
			if (this.GameAccountId.Count != logonResult.GameAccountId.Count)
			{
				return false;
			}
			for (int i = 0; i < this.GameAccountId.Count; i++)
			{
				if (!this.GameAccountId[i].Equals(logonResult.GameAccountId[i]))
				{
					return false;
				}
			}
			if (this.HasEmail != logonResult.HasEmail || (this.HasEmail && !this.Email.Equals(logonResult.Email)))
			{
				return false;
			}
			if (this.AvailableRegion.Count != logonResult.AvailableRegion.Count)
			{
				return false;
			}
			for (int j = 0; j < this.AvailableRegion.Count; j++)
			{
				if (!this.AvailableRegion[j].Equals(logonResult.AvailableRegion[j]))
				{
					return false;
				}
			}
			return this.HasConnectedRegion == logonResult.HasConnectedRegion && (!this.HasConnectedRegion || this.ConnectedRegion.Equals(logonResult.ConnectedRegion)) && this.HasBattleTag == logonResult.HasBattleTag && (!this.HasBattleTag || this.BattleTag.Equals(logonResult.BattleTag)) && this.HasGeoipCountry == logonResult.HasGeoipCountry && (!this.HasGeoipCountry || this.GeoipCountry.Equals(logonResult.GeoipCountry)) && this.HasSessionKey == logonResult.HasSessionKey && (!this.HasSessionKey || this.SessionKey.Equals(logonResult.SessionKey)) && this.HasRestrictedMode == logonResult.HasRestrictedMode && (!this.HasRestrictedMode || this.RestrictedMode.Equals(logonResult.RestrictedMode)) && this.HasClientId == logonResult.HasClientId && (!this.HasClientId || this.ClientId.Equals(logonResult.ClientId));
		}

		// Token: 0x170010EF RID: 4335
		// (get) Token: 0x0600598E RID: 22926 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600598F RID: 22927 RVA: 0x00111E36 File Offset: 0x00110036
		public static LogonResult ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<LogonResult>(bs, 0, -1);
		}

		// Token: 0x06005990 RID: 22928 RVA: 0x00111E40 File Offset: 0x00110040
		public void Deserialize(Stream stream)
		{
			LogonResult.Deserialize(stream, this);
		}

		// Token: 0x06005991 RID: 22929 RVA: 0x00111E4A File Offset: 0x0011004A
		public static LogonResult Deserialize(Stream stream, LogonResult instance)
		{
			return LogonResult.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005992 RID: 22930 RVA: 0x00111E58 File Offset: 0x00110058
		public static LogonResult DeserializeLengthDelimited(Stream stream)
		{
			LogonResult logonResult = new LogonResult();
			LogonResult.DeserializeLengthDelimited(stream, logonResult);
			return logonResult;
		}

		// Token: 0x06005993 RID: 22931 RVA: 0x00111E74 File Offset: 0x00110074
		public static LogonResult DeserializeLengthDelimited(Stream stream, LogonResult instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return LogonResult.Deserialize(stream, instance, num);
		}

		// Token: 0x06005994 RID: 22932 RVA: 0x00111E9C File Offset: 0x0011009C
		public static LogonResult Deserialize(Stream stream, LogonResult instance, long limit)
		{
			if (instance.GameAccountId == null)
			{
				instance.GameAccountId = new List<EntityId>();
			}
			if (instance.AvailableRegion == null)
			{
				instance.AvailableRegion = new List<uint>();
			}
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
					if (num <= 40)
					{
						if (num <= 18)
						{
							if (num == 8)
							{
								instance.ErrorCode = ProtocolParser.ReadUInt32(stream);
								continue;
							}
							if (num == 18)
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
							if (num == 26)
							{
								instance.GameAccountId.Add(EntityId.DeserializeLengthDelimited(stream));
								continue;
							}
							if (num == 34)
							{
								instance.Email = ProtocolParser.ReadString(stream);
								continue;
							}
							if (num == 40)
							{
								instance.AvailableRegion.Add(ProtocolParser.ReadUInt32(stream));
								continue;
							}
						}
					}
					else if (num <= 66)
					{
						if (num == 48)
						{
							instance.ConnectedRegion = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num == 58)
						{
							instance.BattleTag = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 66)
						{
							instance.GeoipCountry = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					else
					{
						if (num == 74)
						{
							instance.SessionKey = ProtocolParser.ReadBytes(stream);
							continue;
						}
						if (num == 80)
						{
							instance.RestrictedMode = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 90)
						{
							instance.ClientId = ProtocolParser.ReadString(stream);
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

		// Token: 0x06005995 RID: 22933 RVA: 0x0011208B File Offset: 0x0011028B
		public void Serialize(Stream stream)
		{
			LogonResult.Serialize(stream, this);
		}

		// Token: 0x06005996 RID: 22934 RVA: 0x00112094 File Offset: 0x00110294
		public static void Serialize(Stream stream, LogonResult instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.ErrorCode);
			if (instance.HasAccountId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.AccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AccountId);
			}
			if (instance.GameAccountId.Count > 0)
			{
				foreach (EntityId entityId in instance.GameAccountId)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, entityId.GetSerializedSize());
					EntityId.Serialize(stream, entityId);
				}
			}
			if (instance.HasEmail)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Email));
			}
			if (instance.AvailableRegion.Count > 0)
			{
				foreach (uint val in instance.AvailableRegion)
				{
					stream.WriteByte(40);
					ProtocolParser.WriteUInt32(stream, val);
				}
			}
			if (instance.HasConnectedRegion)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt32(stream, instance.ConnectedRegion);
			}
			if (instance.HasBattleTag)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BattleTag));
			}
			if (instance.HasGeoipCountry)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.GeoipCountry));
			}
			if (instance.HasSessionKey)
			{
				stream.WriteByte(74);
				ProtocolParser.WriteBytes(stream, instance.SessionKey);
			}
			if (instance.HasRestrictedMode)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteBool(stream, instance.RestrictedMode);
			}
			if (instance.HasClientId)
			{
				stream.WriteByte(90);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ClientId));
			}
		}

		// Token: 0x06005997 RID: 22935 RVA: 0x00112290 File Offset: 0x00110490
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt32(this.ErrorCode);
			if (this.HasAccountId)
			{
				num += 1U;
				uint serializedSize = this.AccountId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.GameAccountId.Count > 0)
			{
				foreach (EntityId entityId in this.GameAccountId)
				{
					num += 1U;
					uint serializedSize2 = entityId.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.HasEmail)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Email);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.AvailableRegion.Count > 0)
			{
				foreach (uint val in this.AvailableRegion)
				{
					num += 1U;
					num += ProtocolParser.SizeOfUInt32(val);
				}
			}
			if (this.HasConnectedRegion)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.ConnectedRegion);
			}
			if (this.HasBattleTag)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.BattleTag);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasGeoipCountry)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.GeoipCountry);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (this.HasSessionKey)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.SessionKey.Length) + (uint)this.SessionKey.Length;
			}
			if (this.HasRestrictedMode)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasClientId)
			{
				num += 1U;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(this.ClientId);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			num += 1U;
			return num;
		}

		// Token: 0x04001BEE RID: 7150
		public bool HasAccountId;

		// Token: 0x04001BEF RID: 7151
		private EntityId _AccountId;

		// Token: 0x04001BF0 RID: 7152
		private List<EntityId> _GameAccountId = new List<EntityId>();

		// Token: 0x04001BF1 RID: 7153
		public bool HasEmail;

		// Token: 0x04001BF2 RID: 7154
		private string _Email;

		// Token: 0x04001BF3 RID: 7155
		private List<uint> _AvailableRegion = new List<uint>();

		// Token: 0x04001BF4 RID: 7156
		public bool HasConnectedRegion;

		// Token: 0x04001BF5 RID: 7157
		private uint _ConnectedRegion;

		// Token: 0x04001BF6 RID: 7158
		public bool HasBattleTag;

		// Token: 0x04001BF7 RID: 7159
		private string _BattleTag;

		// Token: 0x04001BF8 RID: 7160
		public bool HasGeoipCountry;

		// Token: 0x04001BF9 RID: 7161
		private string _GeoipCountry;

		// Token: 0x04001BFA RID: 7162
		public bool HasSessionKey;

		// Token: 0x04001BFB RID: 7163
		private byte[] _SessionKey;

		// Token: 0x04001BFC RID: 7164
		public bool HasRestrictedMode;

		// Token: 0x04001BFD RID: 7165
		private bool _RestrictedMode;

		// Token: 0x04001BFE RID: 7166
		public bool HasClientId;

		// Token: 0x04001BFF RID: 7167
		private string _ClientId;
	}
}
