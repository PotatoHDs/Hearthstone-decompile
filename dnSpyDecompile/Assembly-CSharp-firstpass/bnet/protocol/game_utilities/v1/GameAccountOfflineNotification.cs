using System;
using System.IO;
using System.Text;

namespace bnet.protocol.game_utilities.v1
{
	// Token: 0x0200035E RID: 862
	public class GameAccountOfflineNotification : IProtoBuf
	{
		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06003672 RID: 13938 RVA: 0x000B3D3E File Offset: 0x000B1F3E
		// (set) Token: 0x06003673 RID: 13939 RVA: 0x000B3D46 File Offset: 0x000B1F46
		public EntityId GameAccountId { get; set; }

		// Token: 0x06003674 RID: 13940 RVA: 0x000B3D4F File Offset: 0x000B1F4F
		public void SetGameAccountId(EntityId val)
		{
			this.GameAccountId = val;
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06003675 RID: 13941 RVA: 0x000B3D58 File Offset: 0x000B1F58
		// (set) Token: 0x06003676 RID: 13942 RVA: 0x000B3D60 File Offset: 0x000B1F60
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

		// Token: 0x06003677 RID: 13943 RVA: 0x000B3D73 File Offset: 0x000B1F73
		public void SetHost(ProcessId val)
		{
			this.Host = val;
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06003678 RID: 13944 RVA: 0x000B3D7C File Offset: 0x000B1F7C
		// (set) Token: 0x06003679 RID: 13945 RVA: 0x000B3D84 File Offset: 0x000B1F84
		public string SessionId
		{
			get
			{
				return this._SessionId;
			}
			set
			{
				this._SessionId = value;
				this.HasSessionId = (value != null);
			}
		}

		// Token: 0x0600367A RID: 13946 RVA: 0x000B3D97 File Offset: 0x000B1F97
		public void SetSessionId(string val)
		{
			this.SessionId = val;
		}

		// Token: 0x0600367B RID: 13947 RVA: 0x000B3DA0 File Offset: 0x000B1FA0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.GameAccountId.GetHashCode();
			if (this.HasHost)
			{
				num ^= this.Host.GetHashCode();
			}
			if (this.HasSessionId)
			{
				num ^= this.SessionId.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600367C RID: 13948 RVA: 0x000B3DF4 File Offset: 0x000B1FF4
		public override bool Equals(object obj)
		{
			GameAccountOfflineNotification gameAccountOfflineNotification = obj as GameAccountOfflineNotification;
			return gameAccountOfflineNotification != null && this.GameAccountId.Equals(gameAccountOfflineNotification.GameAccountId) && this.HasHost == gameAccountOfflineNotification.HasHost && (!this.HasHost || this.Host.Equals(gameAccountOfflineNotification.Host)) && this.HasSessionId == gameAccountOfflineNotification.HasSessionId && (!this.HasSessionId || this.SessionId.Equals(gameAccountOfflineNotification.SessionId));
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x0600367D RID: 13949 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600367E RID: 13950 RVA: 0x000B3E79 File Offset: 0x000B2079
		public static GameAccountOfflineNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameAccountOfflineNotification>(bs, 0, -1);
		}

		// Token: 0x0600367F RID: 13951 RVA: 0x000B3E83 File Offset: 0x000B2083
		public void Deserialize(Stream stream)
		{
			GameAccountOfflineNotification.Deserialize(stream, this);
		}

		// Token: 0x06003680 RID: 13952 RVA: 0x000B3E8D File Offset: 0x000B208D
		public static GameAccountOfflineNotification Deserialize(Stream stream, GameAccountOfflineNotification instance)
		{
			return GameAccountOfflineNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003681 RID: 13953 RVA: 0x000B3E98 File Offset: 0x000B2098
		public static GameAccountOfflineNotification DeserializeLengthDelimited(Stream stream)
		{
			GameAccountOfflineNotification gameAccountOfflineNotification = new GameAccountOfflineNotification();
			GameAccountOfflineNotification.DeserializeLengthDelimited(stream, gameAccountOfflineNotification);
			return gameAccountOfflineNotification;
		}

		// Token: 0x06003682 RID: 13954 RVA: 0x000B3EB4 File Offset: 0x000B20B4
		public static GameAccountOfflineNotification DeserializeLengthDelimited(Stream stream, GameAccountOfflineNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameAccountOfflineNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06003683 RID: 13955 RVA: 0x000B3EDC File Offset: 0x000B20DC
		public static GameAccountOfflineNotification Deserialize(Stream stream, GameAccountOfflineNotification instance, long limit)
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
						if (num != 26)
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
							instance.SessionId = ProtocolParser.ReadString(stream);
						}
					}
					else if (instance.Host == null)
					{
						instance.Host = ProcessId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProcessId.DeserializeLengthDelimited(stream, instance.Host);
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

		// Token: 0x06003684 RID: 13956 RVA: 0x000B3FC4 File Offset: 0x000B21C4
		public void Serialize(Stream stream)
		{
			GameAccountOfflineNotification.Serialize(stream, this);
		}

		// Token: 0x06003685 RID: 13957 RVA: 0x000B3FD0 File Offset: 0x000B21D0
		public static void Serialize(Stream stream, GameAccountOfflineNotification instance)
		{
			if (instance.GameAccountId == null)
			{
				throw new ArgumentNullException("GameAccountId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameAccountId.GetSerializedSize());
			EntityId.Serialize(stream, instance.GameAccountId);
			if (instance.HasHost)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Host);
			}
			if (instance.HasSessionId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SessionId));
			}
		}

		// Token: 0x06003686 RID: 13958 RVA: 0x000B4070 File Offset: 0x000B2270
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.GameAccountId.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasHost)
			{
				num += 1U;
				uint serializedSize2 = this.Host.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasSessionId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.SessionId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num + 1U;
		}

		// Token: 0x04001494 RID: 5268
		public bool HasHost;

		// Token: 0x04001495 RID: 5269
		private ProcessId _Host;

		// Token: 0x04001496 RID: 5270
		public bool HasSessionId;

		// Token: 0x04001497 RID: 5271
		private string _SessionId;
	}
}
