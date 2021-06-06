using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PegasusShared;

namespace SpectatorProto
{
	// Token: 0x0200002C RID: 44
	public class JoinInfo : IProtoBuf
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000967B File Offset: 0x0000787B
		// (set) Token: 0x06000229 RID: 553 RVA: 0x00009683 File Offset: 0x00007883
		public string ServerIpAddress
		{
			get
			{
				return this._ServerIpAddress;
			}
			set
			{
				this._ServerIpAddress = value;
				this.HasServerIpAddress = (value != null);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00009696 File Offset: 0x00007896
		// (set) Token: 0x0600022B RID: 555 RVA: 0x0000969E File Offset: 0x0000789E
		public uint ServerPort
		{
			get
			{
				return this._ServerPort;
			}
			set
			{
				this._ServerPort = value;
				this.HasServerPort = true;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600022C RID: 556 RVA: 0x000096AE File Offset: 0x000078AE
		// (set) Token: 0x0600022D RID: 557 RVA: 0x000096B6 File Offset: 0x000078B6
		public int GameHandle
		{
			get
			{
				return this._GameHandle;
			}
			set
			{
				this._GameHandle = value;
				this.HasGameHandle = true;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600022E RID: 558 RVA: 0x000096C6 File Offset: 0x000078C6
		// (set) Token: 0x0600022F RID: 559 RVA: 0x000096CE File Offset: 0x000078CE
		public string SecretKey
		{
			get
			{
				return this._SecretKey;
			}
			set
			{
				this._SecretKey = value;
				this.HasSecretKey = (value != null);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000230 RID: 560 RVA: 0x000096E1 File Offset: 0x000078E1
		// (set) Token: 0x06000231 RID: 561 RVA: 0x000096E9 File Offset: 0x000078E9
		public bool IsJoinable
		{
			get
			{
				return this._IsJoinable;
			}
			set
			{
				this._IsJoinable = value;
				this.HasIsJoinable = true;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000232 RID: 562 RVA: 0x000096F9 File Offset: 0x000078F9
		// (set) Token: 0x06000233 RID: 563 RVA: 0x00009701 File Offset: 0x00007901
		public int CurrentNumSpectators
		{
			get
			{
				return this._CurrentNumSpectators;
			}
			set
			{
				this._CurrentNumSpectators = value;
				this.HasCurrentNumSpectators = true;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00009711 File Offset: 0x00007911
		// (set) Token: 0x06000235 RID: 565 RVA: 0x00009719 File Offset: 0x00007919
		public int MaxNumSpectators
		{
			get
			{
				return this._MaxNumSpectators;
			}
			set
			{
				this._MaxNumSpectators = value;
				this.HasMaxNumSpectators = true;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00009729 File Offset: 0x00007929
		// (set) Token: 0x06000237 RID: 567 RVA: 0x00009731 File Offset: 0x00007931
		public GameType GameType
		{
			get
			{
				return this._GameType;
			}
			set
			{
				this._GameType = value;
				this.HasGameType = true;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00009741 File Offset: 0x00007941
		// (set) Token: 0x06000239 RID: 569 RVA: 0x00009749 File Offset: 0x00007949
		public FormatType FormatType
		{
			get
			{
				return this._FormatType;
			}
			set
			{
				this._FormatType = value;
				this.HasFormatType = true;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00009759 File Offset: 0x00007959
		// (set) Token: 0x0600023B RID: 571 RVA: 0x00009761 File Offset: 0x00007961
		public int MissionId
		{
			get
			{
				return this._MissionId;
			}
			set
			{
				this._MissionId = value;
				this.HasMissionId = true;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00009771 File Offset: 0x00007971
		// (set) Token: 0x0600023D RID: 573 RVA: 0x00009779 File Offset: 0x00007979
		public int BrawlLibraryItemId
		{
			get
			{
				return this._BrawlLibraryItemId;
			}
			set
			{
				this._BrawlLibraryItemId = value;
				this.HasBrawlLibraryItemId = true;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00009789 File Offset: 0x00007989
		// (set) Token: 0x0600023F RID: 575 RVA: 0x00009791 File Offset: 0x00007991
		public List<BnetId> SpectatedPlayers
		{
			get
			{
				return this._SpectatedPlayers;
			}
			set
			{
				this._SpectatedPlayers = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000979A File Offset: 0x0000799A
		// (set) Token: 0x06000241 RID: 577 RVA: 0x000097A2 File Offset: 0x000079A2
		public BnetId PartyId
		{
			get
			{
				return this._PartyId;
			}
			set
			{
				this._PartyId = value;
				this.HasPartyId = (value != null);
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x000097B8 File Offset: 0x000079B8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasServerIpAddress)
			{
				num ^= this.ServerIpAddress.GetHashCode();
			}
			if (this.HasServerPort)
			{
				num ^= this.ServerPort.GetHashCode();
			}
			if (this.HasGameHandle)
			{
				num ^= this.GameHandle.GetHashCode();
			}
			if (this.HasSecretKey)
			{
				num ^= this.SecretKey.GetHashCode();
			}
			if (this.HasIsJoinable)
			{
				num ^= this.IsJoinable.GetHashCode();
			}
			if (this.HasCurrentNumSpectators)
			{
				num ^= this.CurrentNumSpectators.GetHashCode();
			}
			if (this.HasMaxNumSpectators)
			{
				num ^= this.MaxNumSpectators.GetHashCode();
			}
			if (this.HasGameType)
			{
				num ^= this.GameType.GetHashCode();
			}
			if (this.HasFormatType)
			{
				num ^= this.FormatType.GetHashCode();
			}
			if (this.HasMissionId)
			{
				num ^= this.MissionId.GetHashCode();
			}
			if (this.HasBrawlLibraryItemId)
			{
				num ^= this.BrawlLibraryItemId.GetHashCode();
			}
			foreach (BnetId bnetId in this.SpectatedPlayers)
			{
				num ^= bnetId.GetHashCode();
			}
			if (this.HasPartyId)
			{
				num ^= this.PartyId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00009950 File Offset: 0x00007B50
		public override bool Equals(object obj)
		{
			JoinInfo joinInfo = obj as JoinInfo;
			if (joinInfo == null)
			{
				return false;
			}
			if (this.HasServerIpAddress != joinInfo.HasServerIpAddress || (this.HasServerIpAddress && !this.ServerIpAddress.Equals(joinInfo.ServerIpAddress)))
			{
				return false;
			}
			if (this.HasServerPort != joinInfo.HasServerPort || (this.HasServerPort && !this.ServerPort.Equals(joinInfo.ServerPort)))
			{
				return false;
			}
			if (this.HasGameHandle != joinInfo.HasGameHandle || (this.HasGameHandle && !this.GameHandle.Equals(joinInfo.GameHandle)))
			{
				return false;
			}
			if (this.HasSecretKey != joinInfo.HasSecretKey || (this.HasSecretKey && !this.SecretKey.Equals(joinInfo.SecretKey)))
			{
				return false;
			}
			if (this.HasIsJoinable != joinInfo.HasIsJoinable || (this.HasIsJoinable && !this.IsJoinable.Equals(joinInfo.IsJoinable)))
			{
				return false;
			}
			if (this.HasCurrentNumSpectators != joinInfo.HasCurrentNumSpectators || (this.HasCurrentNumSpectators && !this.CurrentNumSpectators.Equals(joinInfo.CurrentNumSpectators)))
			{
				return false;
			}
			if (this.HasMaxNumSpectators != joinInfo.HasMaxNumSpectators || (this.HasMaxNumSpectators && !this.MaxNumSpectators.Equals(joinInfo.MaxNumSpectators)))
			{
				return false;
			}
			if (this.HasGameType != joinInfo.HasGameType || (this.HasGameType && !this.GameType.Equals(joinInfo.GameType)))
			{
				return false;
			}
			if (this.HasFormatType != joinInfo.HasFormatType || (this.HasFormatType && !this.FormatType.Equals(joinInfo.FormatType)))
			{
				return false;
			}
			if (this.HasMissionId != joinInfo.HasMissionId || (this.HasMissionId && !this.MissionId.Equals(joinInfo.MissionId)))
			{
				return false;
			}
			if (this.HasBrawlLibraryItemId != joinInfo.HasBrawlLibraryItemId || (this.HasBrawlLibraryItemId && !this.BrawlLibraryItemId.Equals(joinInfo.BrawlLibraryItemId)))
			{
				return false;
			}
			if (this.SpectatedPlayers.Count != joinInfo.SpectatedPlayers.Count)
			{
				return false;
			}
			for (int i = 0; i < this.SpectatedPlayers.Count; i++)
			{
				if (!this.SpectatedPlayers[i].Equals(joinInfo.SpectatedPlayers[i]))
				{
					return false;
				}
			}
			return this.HasPartyId == joinInfo.HasPartyId && (!this.HasPartyId || this.PartyId.Equals(joinInfo.PartyId));
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00009BF8 File Offset: 0x00007DF8
		public void Deserialize(Stream stream)
		{
			JoinInfo.Deserialize(stream, this);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00009C02 File Offset: 0x00007E02
		public static JoinInfo Deserialize(Stream stream, JoinInfo instance)
		{
			return JoinInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00009C10 File Offset: 0x00007E10
		public static JoinInfo DeserializeLengthDelimited(Stream stream)
		{
			JoinInfo joinInfo = new JoinInfo();
			JoinInfo.DeserializeLengthDelimited(stream, joinInfo);
			return joinInfo;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00009C2C File Offset: 0x00007E2C
		public static JoinInfo DeserializeLengthDelimited(Stream stream, JoinInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return JoinInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00009C54 File Offset: 0x00007E54
		public static JoinInfo Deserialize(Stream stream, JoinInfo instance, long limit)
		{
			instance.GameType = GameType.GT_UNKNOWN;
			instance.FormatType = FormatType.FT_UNKNOWN;
			if (instance.SpectatedPlayers == null)
			{
				instance.SpectatedPlayers = new List<BnetId>();
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
					if (num <= 48)
					{
						if (num <= 24)
						{
							if (num == 10)
							{
								instance.ServerIpAddress = ProtocolParser.ReadString(stream);
								continue;
							}
							if (num == 16)
							{
								instance.ServerPort = ProtocolParser.ReadUInt32(stream);
								continue;
							}
							if (num == 24)
							{
								instance.GameHandle = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else
						{
							if (num == 34)
							{
								instance.SecretKey = ProtocolParser.ReadString(stream);
								continue;
							}
							if (num == 40)
							{
								instance.IsJoinable = ProtocolParser.ReadBool(stream);
								continue;
							}
							if (num == 48)
							{
								instance.CurrentNumSpectators = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
					}
					else if (num <= 72)
					{
						if (num == 56)
						{
							instance.MaxNumSpectators = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 64)
						{
							instance.GameType = (GameType)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 72)
						{
							instance.MissionId = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else if (num <= 90)
					{
						if (num == 82)
						{
							instance.SpectatedPlayers.Add(BnetId.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 90)
						{
							if (instance.PartyId == null)
							{
								instance.PartyId = BnetId.DeserializeLengthDelimited(stream);
								continue;
							}
							BnetId.DeserializeLengthDelimited(stream, instance.PartyId);
							continue;
						}
					}
					else
					{
						if (num == 96)
						{
							instance.FormatType = (FormatType)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 104)
						{
							instance.BrawlLibraryItemId = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06000249 RID: 585 RVA: 0x00009E83 File Offset: 0x00008083
		public void Serialize(Stream stream)
		{
			JoinInfo.Serialize(stream, this);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00009E8C File Offset: 0x0000808C
		public static void Serialize(Stream stream, JoinInfo instance)
		{
			if (instance.HasServerIpAddress)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ServerIpAddress));
			}
			if (instance.HasServerPort)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.ServerPort);
			}
			if (instance.HasGameHandle)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.GameHandle));
			}
			if (instance.HasSecretKey)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SecretKey));
			}
			if (instance.HasIsJoinable)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.IsJoinable);
			}
			if (instance.HasCurrentNumSpectators)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CurrentNumSpectators));
			}
			if (instance.HasMaxNumSpectators)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.MaxNumSpectators));
			}
			if (instance.HasGameType)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.GameType));
			}
			if (instance.HasFormatType)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.FormatType));
			}
			if (instance.HasMissionId)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.MissionId));
			}
			if (instance.HasBrawlLibraryItemId)
			{
				stream.WriteByte(104);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.BrawlLibraryItemId));
			}
			if (instance.SpectatedPlayers.Count > 0)
			{
				foreach (BnetId bnetId in instance.SpectatedPlayers)
				{
					stream.WriteByte(82);
					ProtocolParser.WriteUInt32(stream, bnetId.GetSerializedSize());
					BnetId.Serialize(stream, bnetId);
				}
			}
			if (instance.HasPartyId)
			{
				stream.WriteByte(90);
				ProtocolParser.WriteUInt32(stream, instance.PartyId.GetSerializedSize());
				BnetId.Serialize(stream, instance.PartyId);
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000A080 File Offset: 0x00008280
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasServerIpAddress)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ServerIpAddress);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasServerPort)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.ServerPort);
			}
			if (this.HasGameHandle)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.GameHandle));
			}
			if (this.HasSecretKey)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.SecretKey);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasIsJoinable)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasCurrentNumSpectators)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.CurrentNumSpectators));
			}
			if (this.HasMaxNumSpectators)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.MaxNumSpectators));
			}
			if (this.HasGameType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.GameType));
			}
			if (this.HasFormatType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.FormatType));
			}
			if (this.HasMissionId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.MissionId));
			}
			if (this.HasBrawlLibraryItemId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.BrawlLibraryItemId));
			}
			if (this.SpectatedPlayers.Count > 0)
			{
				foreach (BnetId bnetId in this.SpectatedPlayers)
				{
					num += 1U;
					uint serializedSize = bnetId.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasPartyId)
			{
				num += 1U;
				uint serializedSize2 = this.PartyId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x04000098 RID: 152
		public bool HasServerIpAddress;

		// Token: 0x04000099 RID: 153
		private string _ServerIpAddress;

		// Token: 0x0400009A RID: 154
		public bool HasServerPort;

		// Token: 0x0400009B RID: 155
		private uint _ServerPort;

		// Token: 0x0400009C RID: 156
		public bool HasGameHandle;

		// Token: 0x0400009D RID: 157
		private int _GameHandle;

		// Token: 0x0400009E RID: 158
		public bool HasSecretKey;

		// Token: 0x0400009F RID: 159
		private string _SecretKey;

		// Token: 0x040000A0 RID: 160
		public bool HasIsJoinable;

		// Token: 0x040000A1 RID: 161
		private bool _IsJoinable;

		// Token: 0x040000A2 RID: 162
		public bool HasCurrentNumSpectators;

		// Token: 0x040000A3 RID: 163
		private int _CurrentNumSpectators;

		// Token: 0x040000A4 RID: 164
		public bool HasMaxNumSpectators;

		// Token: 0x040000A5 RID: 165
		private int _MaxNumSpectators;

		// Token: 0x040000A6 RID: 166
		public bool HasGameType;

		// Token: 0x040000A7 RID: 167
		private GameType _GameType;

		// Token: 0x040000A8 RID: 168
		public bool HasFormatType;

		// Token: 0x040000A9 RID: 169
		private FormatType _FormatType;

		// Token: 0x040000AA RID: 170
		public bool HasMissionId;

		// Token: 0x040000AB RID: 171
		private int _MissionId;

		// Token: 0x040000AC RID: 172
		public bool HasBrawlLibraryItemId;

		// Token: 0x040000AD RID: 173
		private int _BrawlLibraryItemId;

		// Token: 0x040000AE RID: 174
		private List<BnetId> _SpectatedPlayers = new List<BnetId>();

		// Token: 0x040000AF RID: 175
		public bool HasPartyId;

		// Token: 0x040000B0 RID: 176
		private BnetId _PartyId;
	}
}
