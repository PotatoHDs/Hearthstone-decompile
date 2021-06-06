using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011C9 RID: 4553
	public class IKSIgnored : IProtoBuf
	{
		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x0600CA6D RID: 51821 RVA: 0x003C9CED File Offset: 0x003C7EED
		// (set) Token: 0x0600CA6E RID: 51822 RVA: 0x003C9CF5 File Offset: 0x003C7EF5
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

		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x0600CA6F RID: 51823 RVA: 0x003C9D08 File Offset: 0x003C7F08
		// (set) Token: 0x0600CA70 RID: 51824 RVA: 0x003C9D10 File Offset: 0x003C7F10
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

		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x0600CA71 RID: 51825 RVA: 0x003C9D23 File Offset: 0x003C7F23
		// (set) Token: 0x0600CA72 RID: 51826 RVA: 0x003C9D2B File Offset: 0x003C7F2B
		public string IksCampaignName
		{
			get
			{
				return this._IksCampaignName;
			}
			set
			{
				this._IksCampaignName = value;
				this.HasIksCampaignName = (value != null);
			}
		}

		// Token: 0x17000F3F RID: 3903
		// (get) Token: 0x0600CA73 RID: 51827 RVA: 0x003C9D3E File Offset: 0x003C7F3E
		// (set) Token: 0x0600CA74 RID: 51828 RVA: 0x003C9D46 File Offset: 0x003C7F46
		public string IksMediaUrl
		{
			get
			{
				return this._IksMediaUrl;
			}
			set
			{
				this._IksMediaUrl = value;
				this.HasIksMediaUrl = (value != null);
			}
		}

		// Token: 0x0600CA75 RID: 51829 RVA: 0x003C9D5C File Offset: 0x003C7F5C
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
			if (this.HasIksCampaignName)
			{
				num ^= this.IksCampaignName.GetHashCode();
			}
			if (this.HasIksMediaUrl)
			{
				num ^= this.IksMediaUrl.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CA76 RID: 51830 RVA: 0x003C9DD0 File Offset: 0x003C7FD0
		public override bool Equals(object obj)
		{
			IKSIgnored iksignored = obj as IKSIgnored;
			return iksignored != null && this.HasPlayer == iksignored.HasPlayer && (!this.HasPlayer || this.Player.Equals(iksignored.Player)) && this.HasDeviceInfo == iksignored.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(iksignored.DeviceInfo)) && this.HasIksCampaignName == iksignored.HasIksCampaignName && (!this.HasIksCampaignName || this.IksCampaignName.Equals(iksignored.IksCampaignName)) && this.HasIksMediaUrl == iksignored.HasIksMediaUrl && (!this.HasIksMediaUrl || this.IksMediaUrl.Equals(iksignored.IksMediaUrl));
		}

		// Token: 0x0600CA77 RID: 51831 RVA: 0x003C9E96 File Offset: 0x003C8096
		public void Deserialize(Stream stream)
		{
			IKSIgnored.Deserialize(stream, this);
		}

		// Token: 0x0600CA78 RID: 51832 RVA: 0x003C9EA0 File Offset: 0x003C80A0
		public static IKSIgnored Deserialize(Stream stream, IKSIgnored instance)
		{
			return IKSIgnored.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CA79 RID: 51833 RVA: 0x003C9EAC File Offset: 0x003C80AC
		public static IKSIgnored DeserializeLengthDelimited(Stream stream)
		{
			IKSIgnored iksignored = new IKSIgnored();
			IKSIgnored.DeserializeLengthDelimited(stream, iksignored);
			return iksignored;
		}

		// Token: 0x0600CA7A RID: 51834 RVA: 0x003C9EC8 File Offset: 0x003C80C8
		public static IKSIgnored DeserializeLengthDelimited(Stream stream, IKSIgnored instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return IKSIgnored.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CA7B RID: 51835 RVA: 0x003C9EF0 File Offset: 0x003C80F0
		public static IKSIgnored Deserialize(Stream stream, IKSIgnored instance, long limit)
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
						if (num == 26)
						{
							instance.IksCampaignName = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 34)
						{
							instance.IksMediaUrl = ProtocolParser.ReadString(stream);
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

		// Token: 0x0600CA7C RID: 51836 RVA: 0x003C9FFB File Offset: 0x003C81FB
		public void Serialize(Stream stream)
		{
			IKSIgnored.Serialize(stream, this);
		}

		// Token: 0x0600CA7D RID: 51837 RVA: 0x003CA004 File Offset: 0x003C8204
		public static void Serialize(Stream stream, IKSIgnored instance)
		{
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
			if (instance.HasIksCampaignName)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.IksCampaignName));
			}
			if (instance.HasIksMediaUrl)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.IksMediaUrl));
			}
		}

		// Token: 0x0600CA7E RID: 51838 RVA: 0x003CA0B8 File Offset: 0x003C82B8
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
			if (this.HasIksCampaignName)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.IksCampaignName);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasIksMediaUrl)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.IksMediaUrl);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}

		// Token: 0x04009FC3 RID: 40899
		public bool HasPlayer;

		// Token: 0x04009FC4 RID: 40900
		private Player _Player;

		// Token: 0x04009FC5 RID: 40901
		public bool HasDeviceInfo;

		// Token: 0x04009FC6 RID: 40902
		private DeviceInfo _DeviceInfo;

		// Token: 0x04009FC7 RID: 40903
		public bool HasIksCampaignName;

		// Token: 0x04009FC8 RID: 40904
		private string _IksCampaignName;

		// Token: 0x04009FC9 RID: 40905
		public bool HasIksMediaUrl;

		// Token: 0x04009FCA RID: 40906
		private string _IksMediaUrl;
	}
}
