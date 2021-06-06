using System;
using System.IO;
using System.Text;
using PegasusShared;

namespace SpectatorProto
{
	// Token: 0x0200002E RID: 46
	public class PartyServerInfo : IProtoBuf
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000A4EE File Offset: 0x000086EE
		// (set) Token: 0x0600025D RID: 605 RVA: 0x0000A4F6 File Offset: 0x000086F6
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

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000A509 File Offset: 0x00008709
		// (set) Token: 0x0600025F RID: 607 RVA: 0x0000A511 File Offset: 0x00008711
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

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000A521 File Offset: 0x00008721
		// (set) Token: 0x06000261 RID: 609 RVA: 0x0000A529 File Offset: 0x00008729
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

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000A539 File Offset: 0x00008739
		// (set) Token: 0x06000263 RID: 611 RVA: 0x0000A541 File Offset: 0x00008741
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

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000A554 File Offset: 0x00008754
		// (set) Token: 0x06000265 RID: 613 RVA: 0x0000A55C File Offset: 0x0000875C
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

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000A56C File Offset: 0x0000876C
		// (set) Token: 0x06000267 RID: 615 RVA: 0x0000A574 File Offset: 0x00008774
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

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000A584 File Offset: 0x00008784
		// (set) Token: 0x06000269 RID: 617 RVA: 0x0000A58C File Offset: 0x0000878C
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

		// Token: 0x0600026A RID: 618 RVA: 0x0000A59C File Offset: 0x0000879C
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
			return num;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000A66C File Offset: 0x0000886C
		public override bool Equals(object obj)
		{
			PartyServerInfo partyServerInfo = obj as PartyServerInfo;
			return partyServerInfo != null && this.HasServerIpAddress == partyServerInfo.HasServerIpAddress && (!this.HasServerIpAddress || this.ServerIpAddress.Equals(partyServerInfo.ServerIpAddress)) && this.HasServerPort == partyServerInfo.HasServerPort && (!this.HasServerPort || this.ServerPort.Equals(partyServerInfo.ServerPort)) && this.HasGameHandle == partyServerInfo.HasGameHandle && (!this.HasGameHandle || this.GameHandle.Equals(partyServerInfo.GameHandle)) && this.HasSecretKey == partyServerInfo.HasSecretKey && (!this.HasSecretKey || this.SecretKey.Equals(partyServerInfo.SecretKey)) && this.HasGameType == partyServerInfo.HasGameType && (!this.HasGameType || this.GameType.Equals(partyServerInfo.GameType)) && this.HasFormatType == partyServerInfo.HasFormatType && (!this.HasFormatType || this.FormatType.Equals(partyServerInfo.FormatType)) && this.HasMissionId == partyServerInfo.HasMissionId && (!this.HasMissionId || this.MissionId.Equals(partyServerInfo.MissionId));
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000A7D9 File Offset: 0x000089D9
		public void Deserialize(Stream stream)
		{
			PartyServerInfo.Deserialize(stream, this);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000A7E3 File Offset: 0x000089E3
		public static PartyServerInfo Deserialize(Stream stream, PartyServerInfo instance)
		{
			return PartyServerInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000A7F0 File Offset: 0x000089F0
		public static PartyServerInfo DeserializeLengthDelimited(Stream stream)
		{
			PartyServerInfo partyServerInfo = new PartyServerInfo();
			PartyServerInfo.DeserializeLengthDelimited(stream, partyServerInfo);
			return partyServerInfo;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000A80C File Offset: 0x00008A0C
		public static PartyServerInfo DeserializeLengthDelimited(Stream stream, PartyServerInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PartyServerInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000A834 File Offset: 0x00008A34
		public static PartyServerInfo Deserialize(Stream stream, PartyServerInfo instance, long limit)
		{
			instance.GameType = GameType.GT_UNKNOWN;
			instance.FormatType = FormatType.FT_UNKNOWN;
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
					else if (num <= 40)
					{
						if (num == 34)
						{
							instance.SecretKey = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 40)
						{
							instance.GameType = (GameType)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 48)
						{
							instance.MissionId = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 56)
						{
							instance.FormatType = (FormatType)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06000271 RID: 625 RVA: 0x0000A96C File Offset: 0x00008B6C
		public void Serialize(Stream stream)
		{
			PartyServerInfo.Serialize(stream, this);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000A978 File Offset: 0x00008B78
		public static void Serialize(Stream stream, PartyServerInfo instance)
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
			if (instance.HasGameType)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.GameType));
			}
			if (instance.HasFormatType)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.FormatType));
			}
			if (instance.HasMissionId)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.MissionId));
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000AA64 File Offset: 0x00008C64
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
			return num;
		}

		// Token: 0x040000B3 RID: 179
		public bool HasServerIpAddress;

		// Token: 0x040000B4 RID: 180
		private string _ServerIpAddress;

		// Token: 0x040000B5 RID: 181
		public bool HasServerPort;

		// Token: 0x040000B6 RID: 182
		private uint _ServerPort;

		// Token: 0x040000B7 RID: 183
		public bool HasGameHandle;

		// Token: 0x040000B8 RID: 184
		private int _GameHandle;

		// Token: 0x040000B9 RID: 185
		public bool HasSecretKey;

		// Token: 0x040000BA RID: 186
		private string _SecretKey;

		// Token: 0x040000BB RID: 187
		public bool HasGameType;

		// Token: 0x040000BC RID: 188
		private GameType _GameType;

		// Token: 0x040000BD RID: 189
		public bool HasFormatType;

		// Token: 0x040000BE RID: 190
		private FormatType _FormatType;

		// Token: 0x040000BF RID: 191
		public bool HasMissionId;

		// Token: 0x040000C0 RID: 192
		private int _MissionId;
	}
}
