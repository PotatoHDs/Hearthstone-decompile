using System;
using System.IO;
using System.Text;

namespace bnet.protocol.game_utilities.v1
{
	// Token: 0x0200035D RID: 861
	public class GameAccountOnlineNotification : IProtoBuf
	{
		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x0600365C RID: 13916 RVA: 0x000B3993 File Offset: 0x000B1B93
		// (set) Token: 0x0600365D RID: 13917 RVA: 0x000B399B File Offset: 0x000B1B9B
		public EntityId GameAccountId { get; set; }

		// Token: 0x0600365E RID: 13918 RVA: 0x000B39A4 File Offset: 0x000B1BA4
		public void SetGameAccountId(EntityId val)
		{
			this.GameAccountId = val;
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x0600365F RID: 13919 RVA: 0x000B39AD File Offset: 0x000B1BAD
		// (set) Token: 0x06003660 RID: 13920 RVA: 0x000B39B5 File Offset: 0x000B1BB5
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

		// Token: 0x06003661 RID: 13921 RVA: 0x000B39C8 File Offset: 0x000B1BC8
		public void SetHost(ProcessId val)
		{
			this.Host = val;
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06003662 RID: 13922 RVA: 0x000B39D1 File Offset: 0x000B1BD1
		// (set) Token: 0x06003663 RID: 13923 RVA: 0x000B39D9 File Offset: 0x000B1BD9
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

		// Token: 0x06003664 RID: 13924 RVA: 0x000B39EC File Offset: 0x000B1BEC
		public void SetSessionId(string val)
		{
			this.SessionId = val;
		}

		// Token: 0x06003665 RID: 13925 RVA: 0x000B39F8 File Offset: 0x000B1BF8
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

		// Token: 0x06003666 RID: 13926 RVA: 0x000B3A4C File Offset: 0x000B1C4C
		public override bool Equals(object obj)
		{
			GameAccountOnlineNotification gameAccountOnlineNotification = obj as GameAccountOnlineNotification;
			return gameAccountOnlineNotification != null && this.GameAccountId.Equals(gameAccountOnlineNotification.GameAccountId) && this.HasHost == gameAccountOnlineNotification.HasHost && (!this.HasHost || this.Host.Equals(gameAccountOnlineNotification.Host)) && this.HasSessionId == gameAccountOnlineNotification.HasSessionId && (!this.HasSessionId || this.SessionId.Equals(gameAccountOnlineNotification.SessionId));
		}

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06003667 RID: 13927 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003668 RID: 13928 RVA: 0x000B3AD1 File Offset: 0x000B1CD1
		public static GameAccountOnlineNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameAccountOnlineNotification>(bs, 0, -1);
		}

		// Token: 0x06003669 RID: 13929 RVA: 0x000B3ADB File Offset: 0x000B1CDB
		public void Deserialize(Stream stream)
		{
			GameAccountOnlineNotification.Deserialize(stream, this);
		}

		// Token: 0x0600366A RID: 13930 RVA: 0x000B3AE5 File Offset: 0x000B1CE5
		public static GameAccountOnlineNotification Deserialize(Stream stream, GameAccountOnlineNotification instance)
		{
			return GameAccountOnlineNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600366B RID: 13931 RVA: 0x000B3AF0 File Offset: 0x000B1CF0
		public static GameAccountOnlineNotification DeserializeLengthDelimited(Stream stream)
		{
			GameAccountOnlineNotification gameAccountOnlineNotification = new GameAccountOnlineNotification();
			GameAccountOnlineNotification.DeserializeLengthDelimited(stream, gameAccountOnlineNotification);
			return gameAccountOnlineNotification;
		}

		// Token: 0x0600366C RID: 13932 RVA: 0x000B3B0C File Offset: 0x000B1D0C
		public static GameAccountOnlineNotification DeserializeLengthDelimited(Stream stream, GameAccountOnlineNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameAccountOnlineNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x0600366D RID: 13933 RVA: 0x000B3B34 File Offset: 0x000B1D34
		public static GameAccountOnlineNotification Deserialize(Stream stream, GameAccountOnlineNotification instance, long limit)
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

		// Token: 0x0600366E RID: 13934 RVA: 0x000B3C1C File Offset: 0x000B1E1C
		public void Serialize(Stream stream)
		{
			GameAccountOnlineNotification.Serialize(stream, this);
		}

		// Token: 0x0600366F RID: 13935 RVA: 0x000B3C28 File Offset: 0x000B1E28
		public static void Serialize(Stream stream, GameAccountOnlineNotification instance)
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

		// Token: 0x06003670 RID: 13936 RVA: 0x000B3CC8 File Offset: 0x000B1EC8
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

		// Token: 0x0400148F RID: 5263
		public bool HasHost;

		// Token: 0x04001490 RID: 5264
		private ProcessId _Host;

		// Token: 0x04001491 RID: 5265
		public bool HasSessionId;

		// Token: 0x04001492 RID: 5266
		private string _SessionId;
	}
}
