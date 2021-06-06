using System;
using System.IO;

namespace PegasusGame
{
	// Token: 0x020001AD RID: 429
	public class UserUI : IProtoBuf
	{
		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001B22 RID: 6946 RVA: 0x0005FF89 File Offset: 0x0005E189
		// (set) Token: 0x06001B23 RID: 6947 RVA: 0x0005FF91 File Offset: 0x0005E191
		public MouseInfo MouseInfo
		{
			get
			{
				return this._MouseInfo;
			}
			set
			{
				this._MouseInfo = value;
				this.HasMouseInfo = (value != null);
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001B24 RID: 6948 RVA: 0x0005FFA4 File Offset: 0x0005E1A4
		// (set) Token: 0x06001B25 RID: 6949 RVA: 0x0005FFAC File Offset: 0x0005E1AC
		public int Emote
		{
			get
			{
				return this._Emote;
			}
			set
			{
				this._Emote = value;
				this.HasEmote = true;
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001B26 RID: 6950 RVA: 0x0005FFBC File Offset: 0x0005E1BC
		// (set) Token: 0x06001B27 RID: 6951 RVA: 0x0005FFC4 File Offset: 0x0005E1C4
		public int PlayerId
		{
			get
			{
				return this._PlayerId;
			}
			set
			{
				this._PlayerId = value;
				this.HasPlayerId = true;
			}
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x0005FFD4 File Offset: 0x0005E1D4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasMouseInfo)
			{
				num ^= this.MouseInfo.GetHashCode();
			}
			if (this.HasEmote)
			{
				num ^= this.Emote.GetHashCode();
			}
			if (this.HasPlayerId)
			{
				num ^= this.PlayerId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x00060038 File Offset: 0x0005E238
		public override bool Equals(object obj)
		{
			UserUI userUI = obj as UserUI;
			return userUI != null && this.HasMouseInfo == userUI.HasMouseInfo && (!this.HasMouseInfo || this.MouseInfo.Equals(userUI.MouseInfo)) && this.HasEmote == userUI.HasEmote && (!this.HasEmote || this.Emote.Equals(userUI.Emote)) && this.HasPlayerId == userUI.HasPlayerId && (!this.HasPlayerId || this.PlayerId.Equals(userUI.PlayerId));
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x000600D9 File Offset: 0x0005E2D9
		public void Deserialize(Stream stream)
		{
			UserUI.Deserialize(stream, this);
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x000600E3 File Offset: 0x0005E2E3
		public static UserUI Deserialize(Stream stream, UserUI instance)
		{
			return UserUI.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x000600F0 File Offset: 0x0005E2F0
		public static UserUI DeserializeLengthDelimited(Stream stream)
		{
			UserUI userUI = new UserUI();
			UserUI.DeserializeLengthDelimited(stream, userUI);
			return userUI;
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x0006010C File Offset: 0x0005E30C
		public static UserUI DeserializeLengthDelimited(Stream stream, UserUI instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UserUI.Deserialize(stream, instance, num);
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x00060134 File Offset: 0x0005E334
		public static UserUI Deserialize(Stream stream, UserUI instance, long limit)
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
					if (num != 16)
					{
						if (num != 24)
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
							instance.PlayerId = (int)ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.Emote = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else if (instance.MouseInfo == null)
				{
					instance.MouseInfo = MouseInfo.DeserializeLengthDelimited(stream);
				}
				else
				{
					MouseInfo.DeserializeLengthDelimited(stream, instance.MouseInfo);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x00060204 File Offset: 0x0005E404
		public void Serialize(Stream stream)
		{
			UserUI.Serialize(stream, this);
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x00060210 File Offset: 0x0005E410
		public static void Serialize(Stream stream, UserUI instance)
		{
			if (instance.HasMouseInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.MouseInfo.GetSerializedSize());
				MouseInfo.Serialize(stream, instance.MouseInfo);
			}
			if (instance.HasEmote)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Emote));
			}
			if (instance.HasPlayerId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.PlayerId));
			}
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x00060284 File Offset: 0x0005E484
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasMouseInfo)
			{
				num += 1U;
				uint serializedSize = this.MouseInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasEmote)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Emote));
			}
			if (this.HasPlayerId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.PlayerId));
			}
			return num;
		}

		// Token: 0x04000A05 RID: 2565
		public bool HasMouseInfo;

		// Token: 0x04000A06 RID: 2566
		private MouseInfo _MouseInfo;

		// Token: 0x04000A07 RID: 2567
		public bool HasEmote;

		// Token: 0x04000A08 RID: 2568
		private int _Emote;

		// Token: 0x04000A09 RID: 2569
		public bool HasPlayerId;

		// Token: 0x04000A0A RID: 2570
		private int _PlayerId;

		// Token: 0x02000644 RID: 1604
		public enum PacketID
		{
			// Token: 0x040020FC RID: 8444
			ID = 15
		}
	}
}
