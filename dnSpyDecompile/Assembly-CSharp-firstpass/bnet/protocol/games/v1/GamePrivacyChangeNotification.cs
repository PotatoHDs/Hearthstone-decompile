using System;
using System.IO;
using bnet.protocol.games.v1.Types;
using bnet.protocol.games.v2;

namespace bnet.protocol.games.v1
{
	// Token: 0x02000395 RID: 917
	public class GamePrivacyChangeNotification : IProtoBuf
	{
		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06003ACF RID: 15055 RVA: 0x000BE701 File Offset: 0x000BC901
		// (set) Token: 0x06003AD0 RID: 15056 RVA: 0x000BE709 File Offset: 0x000BC909
		public GameHandle GameHandle
		{
			get
			{
				return this._GameHandle;
			}
			set
			{
				this._GameHandle = value;
				this.HasGameHandle = (value != null);
			}
		}

		// Token: 0x06003AD1 RID: 15057 RVA: 0x000BE71C File Offset: 0x000BC91C
		public void SetGameHandle(GameHandle val)
		{
			this.GameHandle = val;
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06003AD2 RID: 15058 RVA: 0x000BE725 File Offset: 0x000BC925
		// (set) Token: 0x06003AD3 RID: 15059 RVA: 0x000BE72D File Offset: 0x000BC92D
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

		// Token: 0x06003AD4 RID: 15060 RVA: 0x000BE73D File Offset: 0x000BC93D
		public void SetPrivacyLevel(PrivacyLevel val)
		{
			this.PrivacyLevel = val;
		}

		// Token: 0x06003AD5 RID: 15061 RVA: 0x000BE748 File Offset: 0x000BC948
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasGameHandle)
			{
				num ^= this.GameHandle.GetHashCode();
			}
			if (this.HasPrivacyLevel)
			{
				num ^= this.PrivacyLevel.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003AD6 RID: 15062 RVA: 0x000BE798 File Offset: 0x000BC998
		public override bool Equals(object obj)
		{
			GamePrivacyChangeNotification gamePrivacyChangeNotification = obj as GamePrivacyChangeNotification;
			return gamePrivacyChangeNotification != null && this.HasGameHandle == gamePrivacyChangeNotification.HasGameHandle && (!this.HasGameHandle || this.GameHandle.Equals(gamePrivacyChangeNotification.GameHandle)) && this.HasPrivacyLevel == gamePrivacyChangeNotification.HasPrivacyLevel && (!this.HasPrivacyLevel || this.PrivacyLevel.Equals(gamePrivacyChangeNotification.PrivacyLevel));
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x06003AD7 RID: 15063 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003AD8 RID: 15064 RVA: 0x000BE816 File Offset: 0x000BCA16
		public static GamePrivacyChangeNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GamePrivacyChangeNotification>(bs, 0, -1);
		}

		// Token: 0x06003AD9 RID: 15065 RVA: 0x000BE820 File Offset: 0x000BCA20
		public void Deserialize(Stream stream)
		{
			GamePrivacyChangeNotification.Deserialize(stream, this);
		}

		// Token: 0x06003ADA RID: 15066 RVA: 0x000BE82A File Offset: 0x000BCA2A
		public static GamePrivacyChangeNotification Deserialize(Stream stream, GamePrivacyChangeNotification instance)
		{
			return GamePrivacyChangeNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003ADB RID: 15067 RVA: 0x000BE838 File Offset: 0x000BCA38
		public static GamePrivacyChangeNotification DeserializeLengthDelimited(Stream stream)
		{
			GamePrivacyChangeNotification gamePrivacyChangeNotification = new GamePrivacyChangeNotification();
			GamePrivacyChangeNotification.DeserializeLengthDelimited(stream, gamePrivacyChangeNotification);
			return gamePrivacyChangeNotification;
		}

		// Token: 0x06003ADC RID: 15068 RVA: 0x000BE854 File Offset: 0x000BCA54
		public static GamePrivacyChangeNotification DeserializeLengthDelimited(Stream stream, GamePrivacyChangeNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GamePrivacyChangeNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06003ADD RID: 15069 RVA: 0x000BE87C File Offset: 0x000BCA7C
		public static GamePrivacyChangeNotification Deserialize(Stream stream, GamePrivacyChangeNotification instance, long limit)
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
				else if (num != 10)
				{
					if (num != 16)
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
						instance.PrivacyLevel = (PrivacyLevel)ProtocolParser.ReadUInt64(stream);
					}
				}
				else if (instance.GameHandle == null)
				{
					instance.GameHandle = GameHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameHandle.DeserializeLengthDelimited(stream, instance.GameHandle);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003ADE RID: 15070 RVA: 0x000BE936 File Offset: 0x000BCB36
		public void Serialize(Stream stream)
		{
			GamePrivacyChangeNotification.Serialize(stream, this);
		}

		// Token: 0x06003ADF RID: 15071 RVA: 0x000BE940 File Offset: 0x000BCB40
		public static void Serialize(Stream stream, GamePrivacyChangeNotification instance)
		{
			if (instance.HasGameHandle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
				GameHandle.Serialize(stream, instance.GameHandle);
			}
			if (instance.HasPrivacyLevel)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.PrivacyLevel));
			}
		}

		// Token: 0x06003AE0 RID: 15072 RVA: 0x000BE998 File Offset: 0x000BCB98
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasGameHandle)
			{
				num += 1U;
				uint serializedSize = this.GameHandle.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasPrivacyLevel)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.PrivacyLevel));
			}
			return num;
		}

		// Token: 0x0400154E RID: 5454
		public bool HasGameHandle;

		// Token: 0x0400154F RID: 5455
		private GameHandle _GameHandle;

		// Token: 0x04001550 RID: 5456
		public bool HasPrivacyLevel;

		// Token: 0x04001551 RID: 5457
		private PrivacyLevel _PrivacyLevel;
	}
}
