using System;
using System.IO;
using System.Text;
using PegasusShared;

namespace PegasusGame
{
	// Token: 0x020001CE RID: 462
	public class SpectatorHandshake : IProtoBuf
	{
		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06001D77 RID: 7543 RVA: 0x00067CD1 File Offset: 0x00065ED1
		// (set) Token: 0x06001D78 RID: 7544 RVA: 0x00067CD9 File Offset: 0x00065ED9
		public int GameHandle { get; set; }

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06001D79 RID: 7545 RVA: 0x00067CE2 File Offset: 0x00065EE2
		// (set) Token: 0x06001D7A RID: 7546 RVA: 0x00067CEA File Offset: 0x00065EEA
		public string Password { get; set; }

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001D7B RID: 7547 RVA: 0x00067CF3 File Offset: 0x00065EF3
		// (set) Token: 0x06001D7C RID: 7548 RVA: 0x00067CFB File Offset: 0x00065EFB
		public string Version { get; set; }

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001D7D RID: 7549 RVA: 0x00067D04 File Offset: 0x00065F04
		// (set) Token: 0x06001D7E RID: 7550 RVA: 0x00067D0C File Offset: 0x00065F0C
		public Platform Platform { get; set; }

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001D7F RID: 7551 RVA: 0x00067D15 File Offset: 0x00065F15
		// (set) Token: 0x06001D80 RID: 7552 RVA: 0x00067D1D File Offset: 0x00065F1D
		public BnetId GameAccountId { get; set; }

		// Token: 0x06001D81 RID: 7553 RVA: 0x00067D28 File Offset: 0x00065F28
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.GameHandle.GetHashCode() ^ this.Password.GetHashCode() ^ this.Version.GetHashCode() ^ this.Platform.GetHashCode() ^ this.GameAccountId.GetHashCode();
		}

		// Token: 0x06001D82 RID: 7554 RVA: 0x00067D80 File Offset: 0x00065F80
		public override bool Equals(object obj)
		{
			SpectatorHandshake spectatorHandshake = obj as SpectatorHandshake;
			return spectatorHandshake != null && this.GameHandle.Equals(spectatorHandshake.GameHandle) && this.Password.Equals(spectatorHandshake.Password) && this.Version.Equals(spectatorHandshake.Version) && this.Platform.Equals(spectatorHandshake.Platform) && this.GameAccountId.Equals(spectatorHandshake.GameAccountId);
		}

		// Token: 0x06001D83 RID: 7555 RVA: 0x00067E06 File Offset: 0x00066006
		public void Deserialize(Stream stream)
		{
			SpectatorHandshake.Deserialize(stream, this);
		}

		// Token: 0x06001D84 RID: 7556 RVA: 0x00067E10 File Offset: 0x00066010
		public static SpectatorHandshake Deserialize(Stream stream, SpectatorHandshake instance)
		{
			return SpectatorHandshake.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001D85 RID: 7557 RVA: 0x00067E1C File Offset: 0x0006601C
		public static SpectatorHandshake DeserializeLengthDelimited(Stream stream)
		{
			SpectatorHandshake spectatorHandshake = new SpectatorHandshake();
			SpectatorHandshake.DeserializeLengthDelimited(stream, spectatorHandshake);
			return spectatorHandshake;
		}

		// Token: 0x06001D86 RID: 7558 RVA: 0x00067E38 File Offset: 0x00066038
		public static SpectatorHandshake DeserializeLengthDelimited(Stream stream, SpectatorHandshake instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SpectatorHandshake.Deserialize(stream, instance, num);
		}

		// Token: 0x06001D87 RID: 7559 RVA: 0x00067E60 File Offset: 0x00066060
		public static SpectatorHandshake Deserialize(Stream stream, SpectatorHandshake instance, long limit)
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
				else
				{
					if (num <= 18)
					{
						if (num == 8)
						{
							instance.GameHandle = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 18)
						{
							instance.Password = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					else
					{
						if (num == 26)
						{
							instance.Version = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num != 34)
						{
							if (num == 42)
							{
								if (instance.GameAccountId == null)
								{
									instance.GameAccountId = BnetId.DeserializeLengthDelimited(stream);
									continue;
								}
								BnetId.DeserializeLengthDelimited(stream, instance.GameAccountId);
								continue;
							}
						}
						else
						{
							if (instance.Platform == null)
							{
								instance.Platform = Platform.DeserializeLengthDelimited(stream);
								continue;
							}
							Platform.DeserializeLengthDelimited(stream, instance.Platform);
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

		// Token: 0x06001D88 RID: 7560 RVA: 0x00067F84 File Offset: 0x00066184
		public void Serialize(Stream stream)
		{
			SpectatorHandshake.Serialize(stream, this);
		}

		// Token: 0x06001D89 RID: 7561 RVA: 0x00067F90 File Offset: 0x00066190
		public static void Serialize(Stream stream, SpectatorHandshake instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.GameHandle));
			if (instance.Password == null)
			{
				throw new ArgumentNullException("Password", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Password));
			if (instance.Version == null)
			{
				throw new ArgumentNullException("Version", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Version));
			if (instance.Platform == null)
			{
				throw new ArgumentNullException("Platform", "Required by proto specification.");
			}
			stream.WriteByte(34);
			ProtocolParser.WriteUInt32(stream, instance.Platform.GetSerializedSize());
			Platform.Serialize(stream, instance.Platform);
			if (instance.GameAccountId == null)
			{
				throw new ArgumentNullException("GameAccountId", "Required by proto specification.");
			}
			stream.WriteByte(42);
			ProtocolParser.WriteUInt32(stream, instance.GameAccountId.GetSerializedSize());
			BnetId.Serialize(stream, instance.GameAccountId);
		}

		// Token: 0x06001D8A RID: 7562 RVA: 0x00068098 File Offset: 0x00066298
		public uint GetSerializedSize()
		{
			uint num = 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.GameHandle));
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Password);
			uint num2 = num + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount);
			uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.Version);
			uint num3 = num2 + (ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2);
			uint serializedSize = this.Platform.GetSerializedSize();
			uint num4 = num3 + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize));
			uint serializedSize2 = this.GameAccountId.GetSerializedSize();
			return num4 + (serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2)) + 5U;
		}

		// Token: 0x02000658 RID: 1624
		public enum PacketID
		{
			// Token: 0x04002147 RID: 8519
			ID = 22
		}
	}
}
