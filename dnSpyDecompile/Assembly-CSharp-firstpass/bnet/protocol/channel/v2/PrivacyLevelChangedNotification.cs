using System;
using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.channel.v2.Types;

namespace bnet.protocol.channel.v2
{
	// Token: 0x0200046A RID: 1130
	public class PrivacyLevelChangedNotification : IProtoBuf
	{
		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x06004D8D RID: 19853 RVA: 0x000F0CAF File Offset: 0x000EEEAF
		// (set) Token: 0x06004D8E RID: 19854 RVA: 0x000F0CB7 File Offset: 0x000EEEB7
		public GameAccountHandle AgentId
		{
			get
			{
				return this._AgentId;
			}
			set
			{
				this._AgentId = value;
				this.HasAgentId = (value != null);
			}
		}

		// Token: 0x06004D8F RID: 19855 RVA: 0x000F0CCA File Offset: 0x000EEECA
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x06004D90 RID: 19856 RVA: 0x000F0CD3 File Offset: 0x000EEED3
		// (set) Token: 0x06004D91 RID: 19857 RVA: 0x000F0CDB File Offset: 0x000EEEDB
		public GameAccountHandle SubscriberId
		{
			get
			{
				return this._SubscriberId;
			}
			set
			{
				this._SubscriberId = value;
				this.HasSubscriberId = (value != null);
			}
		}

		// Token: 0x06004D92 RID: 19858 RVA: 0x000F0CEE File Offset: 0x000EEEEE
		public void SetSubscriberId(GameAccountHandle val)
		{
			this.SubscriberId = val;
		}

		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x06004D93 RID: 19859 RVA: 0x000F0CF7 File Offset: 0x000EEEF7
		// (set) Token: 0x06004D94 RID: 19860 RVA: 0x000F0CFF File Offset: 0x000EEEFF
		public ChannelId ChannelId
		{
			get
			{
				return this._ChannelId;
			}
			set
			{
				this._ChannelId = value;
				this.HasChannelId = (value != null);
			}
		}

		// Token: 0x06004D95 RID: 19861 RVA: 0x000F0D12 File Offset: 0x000EEF12
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x06004D96 RID: 19862 RVA: 0x000F0D1B File Offset: 0x000EEF1B
		// (set) Token: 0x06004D97 RID: 19863 RVA: 0x000F0D23 File Offset: 0x000EEF23
		public PrivacyLevel PrivacyLevel
		{
			get
			{
				return this._PrivacyLevel;
			}
			set
			{
				this._PrivacyLevel = value;
				this.HasPrivacyLevel = true;
			}
		}

		// Token: 0x06004D98 RID: 19864 RVA: 0x000F0D33 File Offset: 0x000EEF33
		public void SetPrivacyLevel(PrivacyLevel val)
		{
			this.PrivacyLevel = val;
		}

		// Token: 0x06004D99 RID: 19865 RVA: 0x000F0D3C File Offset: 0x000EEF3C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			if (this.HasSubscriberId)
			{
				num ^= this.SubscriberId.GetHashCode();
			}
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			if (this.HasPrivacyLevel)
			{
				num ^= this.PrivacyLevel.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004D9A RID: 19866 RVA: 0x000F0DB8 File Offset: 0x000EEFB8
		public override bool Equals(object obj)
		{
			PrivacyLevelChangedNotification privacyLevelChangedNotification = obj as PrivacyLevelChangedNotification;
			return privacyLevelChangedNotification != null && this.HasAgentId == privacyLevelChangedNotification.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(privacyLevelChangedNotification.AgentId)) && this.HasSubscriberId == privacyLevelChangedNotification.HasSubscriberId && (!this.HasSubscriberId || this.SubscriberId.Equals(privacyLevelChangedNotification.SubscriberId)) && this.HasChannelId == privacyLevelChangedNotification.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(privacyLevelChangedNotification.ChannelId)) && this.HasPrivacyLevel == privacyLevelChangedNotification.HasPrivacyLevel && (!this.HasPrivacyLevel || this.PrivacyLevel.Equals(privacyLevelChangedNotification.PrivacyLevel));
		}

		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x06004D9B RID: 19867 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004D9C RID: 19868 RVA: 0x000F0E8C File Offset: 0x000EF08C
		public static PrivacyLevelChangedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<PrivacyLevelChangedNotification>(bs, 0, -1);
		}

		// Token: 0x06004D9D RID: 19869 RVA: 0x000F0E96 File Offset: 0x000EF096
		public void Deserialize(Stream stream)
		{
			PrivacyLevelChangedNotification.Deserialize(stream, this);
		}

		// Token: 0x06004D9E RID: 19870 RVA: 0x000F0EA0 File Offset: 0x000EF0A0
		public static PrivacyLevelChangedNotification Deserialize(Stream stream, PrivacyLevelChangedNotification instance)
		{
			return PrivacyLevelChangedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004D9F RID: 19871 RVA: 0x000F0EAC File Offset: 0x000EF0AC
		public static PrivacyLevelChangedNotification DeserializeLengthDelimited(Stream stream)
		{
			PrivacyLevelChangedNotification privacyLevelChangedNotification = new PrivacyLevelChangedNotification();
			PrivacyLevelChangedNotification.DeserializeLengthDelimited(stream, privacyLevelChangedNotification);
			return privacyLevelChangedNotification;
		}

		// Token: 0x06004DA0 RID: 19872 RVA: 0x000F0EC8 File Offset: 0x000EF0C8
		public static PrivacyLevelChangedNotification DeserializeLengthDelimited(Stream stream, PrivacyLevelChangedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PrivacyLevelChangedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06004DA1 RID: 19873 RVA: 0x000F0EF0 File Offset: 0x000EF0F0
		public static PrivacyLevelChangedNotification Deserialize(Stream stream, PrivacyLevelChangedNotification instance, long limit)
		{
			instance.PrivacyLevel = PrivacyLevel.PRIVACY_LEVEL_OPEN;
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
					if (num <= 18)
					{
						if (num != 10)
						{
							if (num == 18)
							{
								if (instance.SubscriberId == null)
								{
									instance.SubscriberId = GameAccountHandle.DeserializeLengthDelimited(stream);
									continue;
								}
								GameAccountHandle.DeserializeLengthDelimited(stream, instance.SubscriberId);
								continue;
							}
						}
						else
						{
							if (instance.AgentId == null)
							{
								instance.AgentId = GameAccountHandle.DeserializeLengthDelimited(stream);
								continue;
							}
							GameAccountHandle.DeserializeLengthDelimited(stream, instance.AgentId);
							continue;
						}
					}
					else if (num != 26)
					{
						if (num == 32)
						{
							instance.PrivacyLevel = (PrivacyLevel)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (instance.ChannelId == null)
						{
							instance.ChannelId = ChannelId.DeserializeLengthDelimited(stream);
							continue;
						}
						ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
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

		// Token: 0x06004DA2 RID: 19874 RVA: 0x000F1023 File Offset: 0x000EF223
		public void Serialize(Stream stream)
		{
			PrivacyLevelChangedNotification.Serialize(stream, this);
		}

		// Token: 0x06004DA3 RID: 19875 RVA: 0x000F102C File Offset: 0x000EF22C
		public static void Serialize(Stream stream, PrivacyLevelChangedNotification instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.AgentId);
			}
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.SubscriberId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.SubscriberId);
			}
			if (instance.HasChannelId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasPrivacyLevel)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.PrivacyLevel));
			}
		}

		// Token: 0x06004DA4 RID: 19876 RVA: 0x000F10E0 File Offset: 0x000EF2E0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasSubscriberId)
			{
				num += 1U;
				uint serializedSize2 = this.SubscriberId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasChannelId)
			{
				num += 1U;
				uint serializedSize3 = this.ChannelId.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.HasPrivacyLevel)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.PrivacyLevel));
			}
			return num;
		}

		// Token: 0x04001938 RID: 6456
		public bool HasAgentId;

		// Token: 0x04001939 RID: 6457
		private GameAccountHandle _AgentId;

		// Token: 0x0400193A RID: 6458
		public bool HasSubscriberId;

		// Token: 0x0400193B RID: 6459
		private GameAccountHandle _SubscriberId;

		// Token: 0x0400193C RID: 6460
		public bool HasChannelId;

		// Token: 0x0400193D RID: 6461
		private ChannelId _ChannelId;

		// Token: 0x0400193E RID: 6462
		public bool HasPrivacyLevel;

		// Token: 0x0400193F RID: 6463
		private PrivacyLevel _PrivacyLevel;
	}
}
