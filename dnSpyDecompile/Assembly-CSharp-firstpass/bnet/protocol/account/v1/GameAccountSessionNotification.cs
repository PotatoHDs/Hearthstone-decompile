using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x02000520 RID: 1312
	public class GameAccountSessionNotification : IProtoBuf
	{
		// Token: 0x170011A8 RID: 4520
		// (get) Token: 0x06005DA7 RID: 23975 RVA: 0x0011C2F3 File Offset: 0x0011A4F3
		// (set) Token: 0x06005DA8 RID: 23976 RVA: 0x0011C2FB File Offset: 0x0011A4FB
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

		// Token: 0x06005DA9 RID: 23977 RVA: 0x0011C30E File Offset: 0x0011A50E
		public void SetGameAccount(GameAccountHandle val)
		{
			this.GameAccount = val;
		}

		// Token: 0x170011A9 RID: 4521
		// (get) Token: 0x06005DAA RID: 23978 RVA: 0x0011C317 File Offset: 0x0011A517
		// (set) Token: 0x06005DAB RID: 23979 RVA: 0x0011C31F File Offset: 0x0011A51F
		public GameSessionUpdateInfo SessionInfo
		{
			get
			{
				return this._SessionInfo;
			}
			set
			{
				this._SessionInfo = value;
				this.HasSessionInfo = (value != null);
			}
		}

		// Token: 0x06005DAC RID: 23980 RVA: 0x0011C332 File Offset: 0x0011A532
		public void SetSessionInfo(GameSessionUpdateInfo val)
		{
			this.SessionInfo = val;
		}

		// Token: 0x06005DAD RID: 23981 RVA: 0x0011C33C File Offset: 0x0011A53C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasGameAccount)
			{
				num ^= this.GameAccount.GetHashCode();
			}
			if (this.HasSessionInfo)
			{
				num ^= this.SessionInfo.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005DAE RID: 23982 RVA: 0x0011C384 File Offset: 0x0011A584
		public override bool Equals(object obj)
		{
			GameAccountSessionNotification gameAccountSessionNotification = obj as GameAccountSessionNotification;
			return gameAccountSessionNotification != null && this.HasGameAccount == gameAccountSessionNotification.HasGameAccount && (!this.HasGameAccount || this.GameAccount.Equals(gameAccountSessionNotification.GameAccount)) && this.HasSessionInfo == gameAccountSessionNotification.HasSessionInfo && (!this.HasSessionInfo || this.SessionInfo.Equals(gameAccountSessionNotification.SessionInfo));
		}

		// Token: 0x170011AA RID: 4522
		// (get) Token: 0x06005DAF RID: 23983 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005DB0 RID: 23984 RVA: 0x0011C3F4 File Offset: 0x0011A5F4
		public static GameAccountSessionNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameAccountSessionNotification>(bs, 0, -1);
		}

		// Token: 0x06005DB1 RID: 23985 RVA: 0x0011C3FE File Offset: 0x0011A5FE
		public void Deserialize(Stream stream)
		{
			GameAccountSessionNotification.Deserialize(stream, this);
		}

		// Token: 0x06005DB2 RID: 23986 RVA: 0x0011C408 File Offset: 0x0011A608
		public static GameAccountSessionNotification Deserialize(Stream stream, GameAccountSessionNotification instance)
		{
			return GameAccountSessionNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005DB3 RID: 23987 RVA: 0x0011C414 File Offset: 0x0011A614
		public static GameAccountSessionNotification DeserializeLengthDelimited(Stream stream)
		{
			GameAccountSessionNotification gameAccountSessionNotification = new GameAccountSessionNotification();
			GameAccountSessionNotification.DeserializeLengthDelimited(stream, gameAccountSessionNotification);
			return gameAccountSessionNotification;
		}

		// Token: 0x06005DB4 RID: 23988 RVA: 0x0011C430 File Offset: 0x0011A630
		public static GameAccountSessionNotification DeserializeLengthDelimited(Stream stream, GameAccountSessionNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameAccountSessionNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06005DB5 RID: 23989 RVA: 0x0011C458 File Offset: 0x0011A658
		public static GameAccountSessionNotification Deserialize(Stream stream, GameAccountSessionNotification instance, long limit)
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else if (instance.SessionInfo == null)
					{
						instance.SessionInfo = GameSessionUpdateInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameSessionUpdateInfo.DeserializeLengthDelimited(stream, instance.SessionInfo);
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

		// Token: 0x06005DB6 RID: 23990 RVA: 0x0011C52A File Offset: 0x0011A72A
		public void Serialize(Stream stream)
		{
			GameAccountSessionNotification.Serialize(stream, this);
		}

		// Token: 0x06005DB7 RID: 23991 RVA: 0x0011C534 File Offset: 0x0011A734
		public static void Serialize(Stream stream, GameAccountSessionNotification instance)
		{
			if (instance.HasGameAccount)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameAccount.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.GameAccount);
			}
			if (instance.HasSessionInfo)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.SessionInfo.GetSerializedSize());
				GameSessionUpdateInfo.Serialize(stream, instance.SessionInfo);
			}
		}

		// Token: 0x06005DB8 RID: 23992 RVA: 0x0011C59C File Offset: 0x0011A79C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasGameAccount)
			{
				num += 1U;
				uint serializedSize = this.GameAccount.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasSessionInfo)
			{
				num += 1U;
				uint serializedSize2 = this.SessionInfo.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x04001CDD RID: 7389
		public bool HasGameAccount;

		// Token: 0x04001CDE RID: 7390
		private GameAccountHandle _GameAccount;

		// Token: 0x04001CDF RID: 7391
		public bool HasSessionInfo;

		// Token: 0x04001CE0 RID: 7392
		private GameSessionUpdateInfo _SessionInfo;
	}
}
