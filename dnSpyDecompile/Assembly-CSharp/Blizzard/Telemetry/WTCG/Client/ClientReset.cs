using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011AC RID: 4524
	public class ClientReset : IProtoBuf
	{
		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x0600C83E RID: 51262 RVA: 0x003C1F2F File Offset: 0x003C012F
		// (set) Token: 0x0600C83F RID: 51263 RVA: 0x003C1F37 File Offset: 0x003C0137
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

		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x0600C840 RID: 51264 RVA: 0x003C1F4A File Offset: 0x003C014A
		// (set) Token: 0x0600C841 RID: 51265 RVA: 0x003C1F52 File Offset: 0x003C0152
		public bool ForceLogin
		{
			get
			{
				return this._ForceLogin;
			}
			set
			{
				this._ForceLogin = value;
				this.HasForceLogin = true;
			}
		}

		// Token: 0x17000EC6 RID: 3782
		// (get) Token: 0x0600C842 RID: 51266 RVA: 0x003C1F62 File Offset: 0x003C0162
		// (set) Token: 0x0600C843 RID: 51267 RVA: 0x003C1F6A File Offset: 0x003C016A
		public bool ForceNoAccountTutorial
		{
			get
			{
				return this._ForceNoAccountTutorial;
			}
			set
			{
				this._ForceNoAccountTutorial = value;
				this.HasForceNoAccountTutorial = true;
			}
		}

		// Token: 0x0600C844 RID: 51268 RVA: 0x003C1F7C File Offset: 0x003C017C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			if (this.HasForceLogin)
			{
				num ^= this.ForceLogin.GetHashCode();
			}
			if (this.HasForceNoAccountTutorial)
			{
				num ^= this.ForceNoAccountTutorial.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C845 RID: 51269 RVA: 0x003C1FE0 File Offset: 0x003C01E0
		public override bool Equals(object obj)
		{
			ClientReset clientReset = obj as ClientReset;
			return clientReset != null && this.HasDeviceInfo == clientReset.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(clientReset.DeviceInfo)) && this.HasForceLogin == clientReset.HasForceLogin && (!this.HasForceLogin || this.ForceLogin.Equals(clientReset.ForceLogin)) && this.HasForceNoAccountTutorial == clientReset.HasForceNoAccountTutorial && (!this.HasForceNoAccountTutorial || this.ForceNoAccountTutorial.Equals(clientReset.ForceNoAccountTutorial));
		}

		// Token: 0x0600C846 RID: 51270 RVA: 0x003C2081 File Offset: 0x003C0281
		public void Deserialize(Stream stream)
		{
			ClientReset.Deserialize(stream, this);
		}

		// Token: 0x0600C847 RID: 51271 RVA: 0x003C208B File Offset: 0x003C028B
		public static ClientReset Deserialize(Stream stream, ClientReset instance)
		{
			return ClientReset.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C848 RID: 51272 RVA: 0x003C2098 File Offset: 0x003C0298
		public static ClientReset DeserializeLengthDelimited(Stream stream)
		{
			ClientReset clientReset = new ClientReset();
			ClientReset.DeserializeLengthDelimited(stream, clientReset);
			return clientReset;
		}

		// Token: 0x0600C849 RID: 51273 RVA: 0x003C20B4 File Offset: 0x003C02B4
		public static ClientReset DeserializeLengthDelimited(Stream stream, ClientReset instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ClientReset.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C84A RID: 51274 RVA: 0x003C20DC File Offset: 0x003C02DC
		public static ClientReset Deserialize(Stream stream, ClientReset instance, long limit)
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
							instance.ForceNoAccountTutorial = ProtocolParser.ReadBool(stream);
						}
					}
					else
					{
						instance.ForceLogin = ProtocolParser.ReadBool(stream);
					}
				}
				else if (instance.DeviceInfo == null)
				{
					instance.DeviceInfo = DeviceInfo.DeserializeLengthDelimited(stream);
				}
				else
				{
					DeviceInfo.DeserializeLengthDelimited(stream, instance.DeviceInfo);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600C84B RID: 51275 RVA: 0x003C21AA File Offset: 0x003C03AA
		public void Serialize(Stream stream)
		{
			ClientReset.Serialize(stream, this);
		}

		// Token: 0x0600C84C RID: 51276 RVA: 0x003C21B4 File Offset: 0x003C03B4
		public static void Serialize(Stream stream, ClientReset instance)
		{
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasForceLogin)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.ForceLogin);
			}
			if (instance.HasForceNoAccountTutorial)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.ForceNoAccountTutorial);
			}
		}

		// Token: 0x0600C84D RID: 51277 RVA: 0x003C2228 File Offset: 0x003C0428
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize = this.DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasForceLogin)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasForceNoAccountTutorial)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x04009ED3 RID: 40659
		public bool HasDeviceInfo;

		// Token: 0x04009ED4 RID: 40660
		private DeviceInfo _DeviceInfo;

		// Token: 0x04009ED5 RID: 40661
		public bool HasForceLogin;

		// Token: 0x04009ED6 RID: 40662
		private bool _ForceLogin;

		// Token: 0x04009ED7 RID: 40663
		public bool HasForceNoAccountTutorial;

		// Token: 0x04009ED8 RID: 40664
		private bool _ForceNoAccountTutorial;
	}
}
