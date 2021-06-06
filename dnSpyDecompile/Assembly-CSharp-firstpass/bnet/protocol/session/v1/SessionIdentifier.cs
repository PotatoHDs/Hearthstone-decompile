using System;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.session.v1
{
	// Token: 0x0200031B RID: 795
	public class SessionIdentifier : IProtoBuf
	{
		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06003082 RID: 12418 RVA: 0x000A39BC File Offset: 0x000A1BBC
		// (set) Token: 0x06003083 RID: 12419 RVA: 0x000A39C4 File Offset: 0x000A1BC4
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

		// Token: 0x06003084 RID: 12420 RVA: 0x000A39D7 File Offset: 0x000A1BD7
		public void SetGameAccount(GameAccountHandle val)
		{
			this.GameAccount = val;
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06003085 RID: 12421 RVA: 0x000A39E0 File Offset: 0x000A1BE0
		// (set) Token: 0x06003086 RID: 12422 RVA: 0x000A39E8 File Offset: 0x000A1BE8
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

		// Token: 0x06003087 RID: 12423 RVA: 0x000A39FB File Offset: 0x000A1BFB
		public void SetSessionId(string val)
		{
			this.SessionId = val;
		}

		// Token: 0x06003088 RID: 12424 RVA: 0x000A3A04 File Offset: 0x000A1C04
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasGameAccount)
			{
				num ^= this.GameAccount.GetHashCode();
			}
			if (this.HasSessionId)
			{
				num ^= this.SessionId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003089 RID: 12425 RVA: 0x000A3A4C File Offset: 0x000A1C4C
		public override bool Equals(object obj)
		{
			SessionIdentifier sessionIdentifier = obj as SessionIdentifier;
			return sessionIdentifier != null && this.HasGameAccount == sessionIdentifier.HasGameAccount && (!this.HasGameAccount || this.GameAccount.Equals(sessionIdentifier.GameAccount)) && this.HasSessionId == sessionIdentifier.HasSessionId && (!this.HasSessionId || this.SessionId.Equals(sessionIdentifier.SessionId));
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x0600308A RID: 12426 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600308B RID: 12427 RVA: 0x000A3ABC File Offset: 0x000A1CBC
		public static SessionIdentifier ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SessionIdentifier>(bs, 0, -1);
		}

		// Token: 0x0600308C RID: 12428 RVA: 0x000A3AC6 File Offset: 0x000A1CC6
		public void Deserialize(Stream stream)
		{
			SessionIdentifier.Deserialize(stream, this);
		}

		// Token: 0x0600308D RID: 12429 RVA: 0x000A3AD0 File Offset: 0x000A1CD0
		public static SessionIdentifier Deserialize(Stream stream, SessionIdentifier instance)
		{
			return SessionIdentifier.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600308E RID: 12430 RVA: 0x000A3ADC File Offset: 0x000A1CDC
		public static SessionIdentifier DeserializeLengthDelimited(Stream stream)
		{
			SessionIdentifier sessionIdentifier = new SessionIdentifier();
			SessionIdentifier.DeserializeLengthDelimited(stream, sessionIdentifier);
			return sessionIdentifier;
		}

		// Token: 0x0600308F RID: 12431 RVA: 0x000A3AF8 File Offset: 0x000A1CF8
		public static SessionIdentifier DeserializeLengthDelimited(Stream stream, SessionIdentifier instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SessionIdentifier.Deserialize(stream, instance, num);
		}

		// Token: 0x06003090 RID: 12432 RVA: 0x000A3B20 File Offset: 0x000A1D20
		public static SessionIdentifier Deserialize(Stream stream, SessionIdentifier instance, long limit)
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
					else
					{
						instance.SessionId = ProtocolParser.ReadString(stream);
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

		// Token: 0x06003091 RID: 12433 RVA: 0x000A3BD2 File Offset: 0x000A1DD2
		public void Serialize(Stream stream)
		{
			SessionIdentifier.Serialize(stream, this);
		}

		// Token: 0x06003092 RID: 12434 RVA: 0x000A3BDC File Offset: 0x000A1DDC
		public static void Serialize(Stream stream, SessionIdentifier instance)
		{
			if (instance.HasGameAccount)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameAccount.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.GameAccount);
			}
			if (instance.HasSessionId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SessionId));
			}
		}

		// Token: 0x06003093 RID: 12435 RVA: 0x000A3C3C File Offset: 0x000A1E3C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasGameAccount)
			{
				num += 1U;
				uint serializedSize = this.GameAccount.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasSessionId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.SessionId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x04001351 RID: 4945
		public bool HasGameAccount;

		// Token: 0x04001352 RID: 4946
		private GameAccountHandle _GameAccount;

		// Token: 0x04001353 RID: 4947
		public bool HasSessionId;

		// Token: 0x04001354 RID: 4948
		private string _SessionId;
	}
}
