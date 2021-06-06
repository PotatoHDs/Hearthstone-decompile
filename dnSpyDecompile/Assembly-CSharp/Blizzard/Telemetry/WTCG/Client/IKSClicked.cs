using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011C8 RID: 4552
	public class IKSClicked : IProtoBuf
	{
		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x0600CA5A RID: 51802 RVA: 0x003C987A File Offset: 0x003C7A7A
		// (set) Token: 0x0600CA5B RID: 51803 RVA: 0x003C9882 File Offset: 0x003C7A82
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

		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x0600CA5C RID: 51804 RVA: 0x003C9895 File Offset: 0x003C7A95
		// (set) Token: 0x0600CA5D RID: 51805 RVA: 0x003C989D File Offset: 0x003C7A9D
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

		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x0600CA5E RID: 51806 RVA: 0x003C98B0 File Offset: 0x003C7AB0
		// (set) Token: 0x0600CA5F RID: 51807 RVA: 0x003C98B8 File Offset: 0x003C7AB8
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

		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x0600CA60 RID: 51808 RVA: 0x003C98CB File Offset: 0x003C7ACB
		// (set) Token: 0x0600CA61 RID: 51809 RVA: 0x003C98D3 File Offset: 0x003C7AD3
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

		// Token: 0x0600CA62 RID: 51810 RVA: 0x003C98E8 File Offset: 0x003C7AE8
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

		// Token: 0x0600CA63 RID: 51811 RVA: 0x003C995C File Offset: 0x003C7B5C
		public override bool Equals(object obj)
		{
			IKSClicked iksclicked = obj as IKSClicked;
			return iksclicked != null && this.HasPlayer == iksclicked.HasPlayer && (!this.HasPlayer || this.Player.Equals(iksclicked.Player)) && this.HasDeviceInfo == iksclicked.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(iksclicked.DeviceInfo)) && this.HasIksCampaignName == iksclicked.HasIksCampaignName && (!this.HasIksCampaignName || this.IksCampaignName.Equals(iksclicked.IksCampaignName)) && this.HasIksMediaUrl == iksclicked.HasIksMediaUrl && (!this.HasIksMediaUrl || this.IksMediaUrl.Equals(iksclicked.IksMediaUrl));
		}

		// Token: 0x0600CA64 RID: 51812 RVA: 0x003C9A22 File Offset: 0x003C7C22
		public void Deserialize(Stream stream)
		{
			IKSClicked.Deserialize(stream, this);
		}

		// Token: 0x0600CA65 RID: 51813 RVA: 0x003C9A2C File Offset: 0x003C7C2C
		public static IKSClicked Deserialize(Stream stream, IKSClicked instance)
		{
			return IKSClicked.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CA66 RID: 51814 RVA: 0x003C9A38 File Offset: 0x003C7C38
		public static IKSClicked DeserializeLengthDelimited(Stream stream)
		{
			IKSClicked iksclicked = new IKSClicked();
			IKSClicked.DeserializeLengthDelimited(stream, iksclicked);
			return iksclicked;
		}

		// Token: 0x0600CA67 RID: 51815 RVA: 0x003C9A54 File Offset: 0x003C7C54
		public static IKSClicked DeserializeLengthDelimited(Stream stream, IKSClicked instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return IKSClicked.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CA68 RID: 51816 RVA: 0x003C9A7C File Offset: 0x003C7C7C
		public static IKSClicked Deserialize(Stream stream, IKSClicked instance, long limit)
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

		// Token: 0x0600CA69 RID: 51817 RVA: 0x003C9B87 File Offset: 0x003C7D87
		public void Serialize(Stream stream)
		{
			IKSClicked.Serialize(stream, this);
		}

		// Token: 0x0600CA6A RID: 51818 RVA: 0x003C9B90 File Offset: 0x003C7D90
		public static void Serialize(Stream stream, IKSClicked instance)
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

		// Token: 0x0600CA6B RID: 51819 RVA: 0x003C9C44 File Offset: 0x003C7E44
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

		// Token: 0x04009FBB RID: 40891
		public bool HasPlayer;

		// Token: 0x04009FBC RID: 40892
		private Player _Player;

		// Token: 0x04009FBD RID: 40893
		public bool HasDeviceInfo;

		// Token: 0x04009FBE RID: 40894
		private DeviceInfo _DeviceInfo;

		// Token: 0x04009FBF RID: 40895
		public bool HasIksCampaignName;

		// Token: 0x04009FC0 RID: 40896
		private string _IksCampaignName;

		// Token: 0x04009FC1 RID: 40897
		public bool HasIksMediaUrl;

		// Token: 0x04009FC2 RID: 40898
		private string _IksMediaUrl;
	}
}
