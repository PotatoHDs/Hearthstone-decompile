using System;
using System.IO;
using System.Text;
using PegasusShared;

namespace PegasusGame
{
	// Token: 0x020001A7 RID: 423
	public class Handshake : IProtoBuf
	{
		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001ABC RID: 6844 RVA: 0x0005EDE1 File Offset: 0x0005CFE1
		// (set) Token: 0x06001ABD RID: 6845 RVA: 0x0005EDE9 File Offset: 0x0005CFE9
		public int GameHandle { get; set; }

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001ABE RID: 6846 RVA: 0x0005EDF2 File Offset: 0x0005CFF2
		// (set) Token: 0x06001ABF RID: 6847 RVA: 0x0005EDFA File Offset: 0x0005CFFA
		public string Password { get; set; }

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001AC0 RID: 6848 RVA: 0x0005EE03 File Offset: 0x0005D003
		// (set) Token: 0x06001AC1 RID: 6849 RVA: 0x0005EE0B File Offset: 0x0005D00B
		public long ClientHandle { get; set; }

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001AC2 RID: 6850 RVA: 0x0005EE14 File Offset: 0x0005D014
		// (set) Token: 0x06001AC3 RID: 6851 RVA: 0x0005EE1C File Offset: 0x0005D01C
		public int Mission
		{
			get
			{
				return this._Mission;
			}
			set
			{
				this._Mission = value;
				this.HasMission = true;
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001AC4 RID: 6852 RVA: 0x0005EE2C File Offset: 0x0005D02C
		// (set) Token: 0x06001AC5 RID: 6853 RVA: 0x0005EE34 File Offset: 0x0005D034
		public string Version { get; set; }

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001AC6 RID: 6854 RVA: 0x0005EE3D File Offset: 0x0005D03D
		// (set) Token: 0x06001AC7 RID: 6855 RVA: 0x0005EE45 File Offset: 0x0005D045
		public Platform Platform { get; set; }

		// Token: 0x06001AC8 RID: 6856 RVA: 0x0005EE50 File Offset: 0x0005D050
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.GameHandle.GetHashCode();
			num ^= this.Password.GetHashCode();
			num ^= this.ClientHandle.GetHashCode();
			if (this.HasMission)
			{
				num ^= this.Mission.GetHashCode();
			}
			num ^= this.Version.GetHashCode();
			return num ^ this.Platform.GetHashCode();
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x0005EED0 File Offset: 0x0005D0D0
		public override bool Equals(object obj)
		{
			Handshake handshake = obj as Handshake;
			return handshake != null && this.GameHandle.Equals(handshake.GameHandle) && this.Password.Equals(handshake.Password) && this.ClientHandle.Equals(handshake.ClientHandle) && this.HasMission == handshake.HasMission && (!this.HasMission || this.Mission.Equals(handshake.Mission)) && this.Version.Equals(handshake.Version) && this.Platform.Equals(handshake.Platform);
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x0005EF87 File Offset: 0x0005D187
		public void Deserialize(Stream stream)
		{
			Handshake.Deserialize(stream, this);
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x0005EF91 File Offset: 0x0005D191
		public static Handshake Deserialize(Stream stream, Handshake instance)
		{
			return Handshake.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x0005EF9C File Offset: 0x0005D19C
		public static Handshake DeserializeLengthDelimited(Stream stream)
		{
			Handshake handshake = new Handshake();
			Handshake.DeserializeLengthDelimited(stream, handshake);
			return handshake;
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x0005EFB8 File Offset: 0x0005D1B8
		public static Handshake DeserializeLengthDelimited(Stream stream, Handshake instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Handshake.Deserialize(stream, instance, num);
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x0005EFE0 File Offset: 0x0005D1E0
		public static Handshake Deserialize(Stream stream, Handshake instance, long limit)
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
					if (num <= 24)
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
						if (num == 24)
						{
							instance.ClientHandle = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 32)
						{
							instance.Mission = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 42)
						{
							instance.Version = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 58)
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

		// Token: 0x06001ACF RID: 6863 RVA: 0x0005F0FE File Offset: 0x0005D2FE
		public void Serialize(Stream stream)
		{
			Handshake.Serialize(stream, this);
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x0005F108 File Offset: 0x0005D308
		public static void Serialize(Stream stream, Handshake instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.GameHandle));
			if (instance.Password == null)
			{
				throw new ArgumentNullException("Password", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Password));
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ClientHandle);
			if (instance.HasMission)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Mission));
			}
			if (instance.Version == null)
			{
				throw new ArgumentNullException("Version", "Required by proto specification.");
			}
			stream.WriteByte(42);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Version));
			if (instance.Platform == null)
			{
				throw new ArgumentNullException("Platform", "Required by proto specification.");
			}
			stream.WriteByte(58);
			ProtocolParser.WriteUInt32(stream, instance.Platform.GetSerializedSize());
			Platform.Serialize(stream, instance.Platform);
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x0005F204 File Offset: 0x0005D404
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.GameHandle));
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Password);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			num += ProtocolParser.SizeOfUInt64((ulong)this.ClientHandle);
			if (this.HasMission)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Mission));
			}
			uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.Version);
			num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			uint serializedSize = this.Platform.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			return num + 5U;
		}

		// Token: 0x040009F3 RID: 2547
		public bool HasMission;

		// Token: 0x040009F4 RID: 2548
		private int _Mission;

		// Token: 0x0200063F RID: 1599
		public enum PacketID
		{
			// Token: 0x040020F2 RID: 8434
			ID = 168
		}
	}
}
