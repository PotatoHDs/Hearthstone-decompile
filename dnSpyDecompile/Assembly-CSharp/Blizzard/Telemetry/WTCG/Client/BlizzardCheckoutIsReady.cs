using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011A2 RID: 4514
	public class BlizzardCheckoutIsReady : IProtoBuf
	{
		// Token: 0x17000EA5 RID: 3749
		// (get) Token: 0x0600C792 RID: 51090 RVA: 0x003BFA51 File Offset: 0x003BDC51
		// (set) Token: 0x0600C793 RID: 51091 RVA: 0x003BFA59 File Offset: 0x003BDC59
		public Player Player
		{
			get
			{
				return this._Player;
			}
			set
			{
				this._Player = value;
				this.HasPlayer = (value != null);
			}
		}

		// Token: 0x17000EA6 RID: 3750
		// (get) Token: 0x0600C794 RID: 51092 RVA: 0x003BFA6C File Offset: 0x003BDC6C
		// (set) Token: 0x0600C795 RID: 51093 RVA: 0x003BFA74 File Offset: 0x003BDC74
		public DeviceInfo DeviceInfo
		{
			get
			{
				return this._DeviceInfo;
			}
			set
			{
				this._DeviceInfo = value;
				this.HasDeviceInfo = (value != null);
			}
		}

		// Token: 0x17000EA7 RID: 3751
		// (get) Token: 0x0600C796 RID: 51094 RVA: 0x003BFA87 File Offset: 0x003BDC87
		// (set) Token: 0x0600C797 RID: 51095 RVA: 0x003BFA8F File Offset: 0x003BDC8F
		public double SecondsShown
		{
			get
			{
				return this._SecondsShown;
			}
			set
			{
				this._SecondsShown = value;
				this.HasSecondsShown = true;
			}
		}

		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x0600C798 RID: 51096 RVA: 0x003BFA9F File Offset: 0x003BDC9F
		// (set) Token: 0x0600C799 RID: 51097 RVA: 0x003BFAA7 File Offset: 0x003BDCA7
		public bool IsReady
		{
			get
			{
				return this._IsReady;
			}
			set
			{
				this._IsReady = value;
				this.HasIsReady = true;
			}
		}

		// Token: 0x0600C79A RID: 51098 RVA: 0x003BFAB8 File Offset: 0x003BDCB8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasPlayer)
			{
				num ^= this.Player.GetHashCode();
			}
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			if (this.HasSecondsShown)
			{
				num ^= this.SecondsShown.GetHashCode();
			}
			if (this.HasIsReady)
			{
				num ^= this.IsReady.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C79B RID: 51099 RVA: 0x003BFB30 File Offset: 0x003BDD30
		public override bool Equals(object obj)
		{
			BlizzardCheckoutIsReady blizzardCheckoutIsReady = obj as BlizzardCheckoutIsReady;
			return blizzardCheckoutIsReady != null && this.HasPlayer == blizzardCheckoutIsReady.HasPlayer && (!this.HasPlayer || this.Player.Equals(blizzardCheckoutIsReady.Player)) && this.HasDeviceInfo == blizzardCheckoutIsReady.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(blizzardCheckoutIsReady.DeviceInfo)) && this.HasSecondsShown == blizzardCheckoutIsReady.HasSecondsShown && (!this.HasSecondsShown || this.SecondsShown.Equals(blizzardCheckoutIsReady.SecondsShown)) && this.HasIsReady == blizzardCheckoutIsReady.HasIsReady && (!this.HasIsReady || this.IsReady.Equals(blizzardCheckoutIsReady.IsReady));
		}

		// Token: 0x0600C79C RID: 51100 RVA: 0x003BFBFC File Offset: 0x003BDDFC
		public void Deserialize(Stream stream)
		{
			BlizzardCheckoutIsReady.Deserialize(stream, this);
		}

		// Token: 0x0600C79D RID: 51101 RVA: 0x003BFC06 File Offset: 0x003BDE06
		public static BlizzardCheckoutIsReady Deserialize(Stream stream, BlizzardCheckoutIsReady instance)
		{
			return BlizzardCheckoutIsReady.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C79E RID: 51102 RVA: 0x003BFC14 File Offset: 0x003BDE14
		public static BlizzardCheckoutIsReady DeserializeLengthDelimited(Stream stream)
		{
			BlizzardCheckoutIsReady blizzardCheckoutIsReady = new BlizzardCheckoutIsReady();
			BlizzardCheckoutIsReady.DeserializeLengthDelimited(stream, blizzardCheckoutIsReady);
			return blizzardCheckoutIsReady;
		}

		// Token: 0x0600C79F RID: 51103 RVA: 0x003BFC30 File Offset: 0x003BDE30
		public static BlizzardCheckoutIsReady DeserializeLengthDelimited(Stream stream, BlizzardCheckoutIsReady instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BlizzardCheckoutIsReady.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C7A0 RID: 51104 RVA: 0x003BFC58 File Offset: 0x003BDE58
		public static BlizzardCheckoutIsReady Deserialize(Stream stream, BlizzardCheckoutIsReady instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
								if (instance.DeviceInfo == null)
								{
									instance.DeviceInfo = DeviceInfo.DeserializeLengthDelimited(stream);
									continue;
								}
								DeviceInfo.DeserializeLengthDelimited(stream, instance.DeviceInfo);
								continue;
							}
						}
						else
						{
							if (instance.Player == null)
							{
								instance.Player = Player.DeserializeLengthDelimited(stream);
								continue;
							}
							Player.DeserializeLengthDelimited(stream, instance.Player);
							continue;
						}
					}
					else
					{
						if (num == 25)
						{
							instance.SecondsShown = binaryReader.ReadDouble();
							continue;
						}
						if (num == 32)
						{
							instance.IsReady = ProtocolParser.ReadBool(stream);
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

		// Token: 0x0600C7A1 RID: 51105 RVA: 0x003BFD6A File Offset: 0x003BDF6A
		public void Serialize(Stream stream)
		{
			BlizzardCheckoutIsReady.Serialize(stream, this);
		}

		// Token: 0x0600C7A2 RID: 51106 RVA: 0x003BFD74 File Offset: 0x003BDF74
		public static void Serialize(Stream stream, BlizzardCheckoutIsReady instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasPlayer)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Player.GetSerializedSize());
				Player.Serialize(stream, instance.Player);
			}
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasSecondsShown)
			{
				stream.WriteByte(25);
				binaryWriter.Write(instance.SecondsShown);
			}
			if (instance.HasIsReady)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.IsReady);
			}
		}

		// Token: 0x0600C7A3 RID: 51107 RVA: 0x003BFE1C File Offset: 0x003BE01C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasPlayer)
			{
				num += 1U;
				uint serializedSize = this.Player.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize2 = this.DeviceInfo.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasSecondsShown)
			{
				num += 1U;
				num += 8U;
			}
			if (this.HasIsReady)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x04009E96 RID: 40598
		public bool HasPlayer;

		// Token: 0x04009E97 RID: 40599
		private Player _Player;

		// Token: 0x04009E98 RID: 40600
		public bool HasDeviceInfo;

		// Token: 0x04009E99 RID: 40601
		private DeviceInfo _DeviceInfo;

		// Token: 0x04009E9A RID: 40602
		public bool HasSecondsShown;

		// Token: 0x04009E9B RID: 40603
		private double _SecondsShown;

		// Token: 0x04009E9C RID: 40604
		public bool HasIsReady;

		// Token: 0x04009E9D RID: 40605
		private bool _IsReady;
	}
}
