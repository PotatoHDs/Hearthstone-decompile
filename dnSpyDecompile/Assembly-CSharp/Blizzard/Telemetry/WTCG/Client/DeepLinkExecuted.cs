using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011B7 RID: 4535
	public class DeepLinkExecuted : IProtoBuf
	{
		// Token: 0x17000EE8 RID: 3816
		// (get) Token: 0x0600C8FF RID: 51455 RVA: 0x003C472F File Offset: 0x003C292F
		// (set) Token: 0x0600C900 RID: 51456 RVA: 0x003C4737 File Offset: 0x003C2937
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

		// Token: 0x17000EE9 RID: 3817
		// (get) Token: 0x0600C901 RID: 51457 RVA: 0x003C474A File Offset: 0x003C294A
		// (set) Token: 0x0600C902 RID: 51458 RVA: 0x003C4752 File Offset: 0x003C2952
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

		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x0600C903 RID: 51459 RVA: 0x003C4765 File Offset: 0x003C2965
		// (set) Token: 0x0600C904 RID: 51460 RVA: 0x003C476D File Offset: 0x003C296D
		public string DeepLink
		{
			get
			{
				return this._DeepLink;
			}
			set
			{
				this._DeepLink = value;
				this.HasDeepLink = (value != null);
			}
		}

		// Token: 0x17000EEB RID: 3819
		// (get) Token: 0x0600C905 RID: 51461 RVA: 0x003C4780 File Offset: 0x003C2980
		// (set) Token: 0x0600C906 RID: 51462 RVA: 0x003C4788 File Offset: 0x003C2988
		public string Source
		{
			get
			{
				return this._Source;
			}
			set
			{
				this._Source = value;
				this.HasSource = (value != null);
			}
		}

		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x0600C907 RID: 51463 RVA: 0x003C479B File Offset: 0x003C299B
		// (set) Token: 0x0600C908 RID: 51464 RVA: 0x003C47A3 File Offset: 0x003C29A3
		public bool Completed
		{
			get
			{
				return this._Completed;
			}
			set
			{
				this._Completed = value;
				this.HasCompleted = true;
			}
		}

		// Token: 0x0600C909 RID: 51465 RVA: 0x003C47B4 File Offset: 0x003C29B4
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
			if (this.HasDeepLink)
			{
				num ^= this.DeepLink.GetHashCode();
			}
			if (this.HasSource)
			{
				num ^= this.Source.GetHashCode();
			}
			if (this.HasCompleted)
			{
				num ^= this.Completed.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C90A RID: 51466 RVA: 0x003C4840 File Offset: 0x003C2A40
		public override bool Equals(object obj)
		{
			DeepLinkExecuted deepLinkExecuted = obj as DeepLinkExecuted;
			return deepLinkExecuted != null && this.HasPlayer == deepLinkExecuted.HasPlayer && (!this.HasPlayer || this.Player.Equals(deepLinkExecuted.Player)) && this.HasDeviceInfo == deepLinkExecuted.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(deepLinkExecuted.DeviceInfo)) && this.HasDeepLink == deepLinkExecuted.HasDeepLink && (!this.HasDeepLink || this.DeepLink.Equals(deepLinkExecuted.DeepLink)) && this.HasSource == deepLinkExecuted.HasSource && (!this.HasSource || this.Source.Equals(deepLinkExecuted.Source)) && this.HasCompleted == deepLinkExecuted.HasCompleted && (!this.HasCompleted || this.Completed.Equals(deepLinkExecuted.Completed));
		}

		// Token: 0x0600C90B RID: 51467 RVA: 0x003C4934 File Offset: 0x003C2B34
		public void Deserialize(Stream stream)
		{
			DeepLinkExecuted.Deserialize(stream, this);
		}

		// Token: 0x0600C90C RID: 51468 RVA: 0x003C493E File Offset: 0x003C2B3E
		public static DeepLinkExecuted Deserialize(Stream stream, DeepLinkExecuted instance)
		{
			return DeepLinkExecuted.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C90D RID: 51469 RVA: 0x003C494C File Offset: 0x003C2B4C
		public static DeepLinkExecuted DeserializeLengthDelimited(Stream stream)
		{
			DeepLinkExecuted deepLinkExecuted = new DeepLinkExecuted();
			DeepLinkExecuted.DeserializeLengthDelimited(stream, deepLinkExecuted);
			return deepLinkExecuted;
		}

		// Token: 0x0600C90E RID: 51470 RVA: 0x003C4968 File Offset: 0x003C2B68
		public static DeepLinkExecuted DeserializeLengthDelimited(Stream stream, DeepLinkExecuted instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DeepLinkExecuted.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C90F RID: 51471 RVA: 0x003C4990 File Offset: 0x003C2B90
		public static DeepLinkExecuted Deserialize(Stream stream, DeepLinkExecuted instance, long limit)
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
							instance.DeepLink = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 34)
						{
							instance.Source = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 40)
						{
							instance.Completed = ProtocolParser.ReadBool(stream);
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

		// Token: 0x0600C910 RID: 51472 RVA: 0x003C4AB4 File Offset: 0x003C2CB4
		public void Serialize(Stream stream)
		{
			DeepLinkExecuted.Serialize(stream, this);
		}

		// Token: 0x0600C911 RID: 51473 RVA: 0x003C4AC0 File Offset: 0x003C2CC0
		public static void Serialize(Stream stream, DeepLinkExecuted instance)
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
			if (instance.HasDeepLink)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeepLink));
			}
			if (instance.HasSource)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Source));
			}
			if (instance.HasCompleted)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.Completed);
			}
		}

		// Token: 0x0600C912 RID: 51474 RVA: 0x003C4B90 File Offset: 0x003C2D90
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
			if (this.HasDeepLink)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.DeepLink);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasSource)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.Source);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasCompleted)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x04009F1B RID: 40731
		public bool HasPlayer;

		// Token: 0x04009F1C RID: 40732
		private Player _Player;

		// Token: 0x04009F1D RID: 40733
		public bool HasDeviceInfo;

		// Token: 0x04009F1E RID: 40734
		private DeviceInfo _DeviceInfo;

		// Token: 0x04009F1F RID: 40735
		public bool HasDeepLink;

		// Token: 0x04009F20 RID: 40736
		private string _DeepLink;

		// Token: 0x04009F21 RID: 40737
		public bool HasSource;

		// Token: 0x04009F22 RID: 40738
		private string _Source;

		// Token: 0x04009F23 RID: 40739
		public bool HasCompleted;

		// Token: 0x04009F24 RID: 40740
		private bool _Completed;
	}
}
