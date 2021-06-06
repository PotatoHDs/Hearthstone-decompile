using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011CB RID: 4555
	public class InGameMessageHandlerCalled : IProtoBuf
	{
		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x0600CA99 RID: 51865 RVA: 0x003CA82A File Offset: 0x003C8A2A
		// (set) Token: 0x0600CA9A RID: 51866 RVA: 0x003CA832 File Offset: 0x003C8A32
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

		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x0600CA9B RID: 51867 RVA: 0x003CA845 File Offset: 0x003C8A45
		// (set) Token: 0x0600CA9C RID: 51868 RVA: 0x003CA84D File Offset: 0x003C8A4D
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

		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x0600CA9D RID: 51869 RVA: 0x003CA860 File Offset: 0x003C8A60
		// (set) Token: 0x0600CA9E RID: 51870 RVA: 0x003CA868 File Offset: 0x003C8A68
		public string MessageType
		{
			get
			{
				return this._MessageType;
			}
			set
			{
				this._MessageType = value;
				this.HasMessageType = (value != null);
			}
		}

		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x0600CA9F RID: 51871 RVA: 0x003CA87B File Offset: 0x003C8A7B
		// (set) Token: 0x0600CAA0 RID: 51872 RVA: 0x003CA883 File Offset: 0x003C8A83
		public string Title
		{
			get
			{
				return this._Title;
			}
			set
			{
				this._Title = value;
				this.HasTitle = (value != null);
			}
		}

		// Token: 0x17000F4B RID: 3915
		// (get) Token: 0x0600CAA1 RID: 51873 RVA: 0x003CA896 File Offset: 0x003C8A96
		// (set) Token: 0x0600CAA2 RID: 51874 RVA: 0x003CA89E File Offset: 0x003C8A9E
		public string Uid
		{
			get
			{
				return this._Uid;
			}
			set
			{
				this._Uid = value;
				this.HasUid = (value != null);
			}
		}

		// Token: 0x0600CAA3 RID: 51875 RVA: 0x003CA8B4 File Offset: 0x003C8AB4
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
			if (this.HasMessageType)
			{
				num ^= this.MessageType.GetHashCode();
			}
			if (this.HasTitle)
			{
				num ^= this.Title.GetHashCode();
			}
			if (this.HasUid)
			{
				num ^= this.Uid.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CAA4 RID: 51876 RVA: 0x003CA93C File Offset: 0x003C8B3C
		public override bool Equals(object obj)
		{
			InGameMessageHandlerCalled inGameMessageHandlerCalled = obj as InGameMessageHandlerCalled;
			return inGameMessageHandlerCalled != null && this.HasPlayer == inGameMessageHandlerCalled.HasPlayer && (!this.HasPlayer || this.Player.Equals(inGameMessageHandlerCalled.Player)) && this.HasDeviceInfo == inGameMessageHandlerCalled.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(inGameMessageHandlerCalled.DeviceInfo)) && this.HasMessageType == inGameMessageHandlerCalled.HasMessageType && (!this.HasMessageType || this.MessageType.Equals(inGameMessageHandlerCalled.MessageType)) && this.HasTitle == inGameMessageHandlerCalled.HasTitle && (!this.HasTitle || this.Title.Equals(inGameMessageHandlerCalled.Title)) && this.HasUid == inGameMessageHandlerCalled.HasUid && (!this.HasUid || this.Uid.Equals(inGameMessageHandlerCalled.Uid));
		}

		// Token: 0x0600CAA5 RID: 51877 RVA: 0x003CAA2D File Offset: 0x003C8C2D
		public void Deserialize(Stream stream)
		{
			InGameMessageHandlerCalled.Deserialize(stream, this);
		}

		// Token: 0x0600CAA6 RID: 51878 RVA: 0x003CAA37 File Offset: 0x003C8C37
		public static InGameMessageHandlerCalled Deserialize(Stream stream, InGameMessageHandlerCalled instance)
		{
			return InGameMessageHandlerCalled.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CAA7 RID: 51879 RVA: 0x003CAA44 File Offset: 0x003C8C44
		public static InGameMessageHandlerCalled DeserializeLengthDelimited(Stream stream)
		{
			InGameMessageHandlerCalled inGameMessageHandlerCalled = new InGameMessageHandlerCalled();
			InGameMessageHandlerCalled.DeserializeLengthDelimited(stream, inGameMessageHandlerCalled);
			return inGameMessageHandlerCalled;
		}

		// Token: 0x0600CAA8 RID: 51880 RVA: 0x003CAA60 File Offset: 0x003C8C60
		public static InGameMessageHandlerCalled DeserializeLengthDelimited(Stream stream, InGameMessageHandlerCalled instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return InGameMessageHandlerCalled.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CAA9 RID: 51881 RVA: 0x003CAA88 File Offset: 0x003C8C88
		public static InGameMessageHandlerCalled Deserialize(Stream stream, InGameMessageHandlerCalled instance, long limit)
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
							instance.MessageType = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 34)
						{
							instance.Title = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 82)
						{
							instance.Uid = ProtocolParser.ReadString(stream);
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

		// Token: 0x0600CAAA RID: 51882 RVA: 0x003CABAC File Offset: 0x003C8DAC
		public void Serialize(Stream stream)
		{
			InGameMessageHandlerCalled.Serialize(stream, this);
		}

		// Token: 0x0600CAAB RID: 51883 RVA: 0x003CABB8 File Offset: 0x003C8DB8
		public static void Serialize(Stream stream, InGameMessageHandlerCalled instance)
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
			if (instance.HasMessageType)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.MessageType));
			}
			if (instance.HasTitle)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Title));
			}
			if (instance.HasUid)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Uid));
			}
		}

		// Token: 0x0600CAAC RID: 51884 RVA: 0x003CAC94 File Offset: 0x003C8E94
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
			if (this.HasMessageType)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.MessageType);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasTitle)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.Title);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasUid)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.Uid);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			return num;
		}

		// Token: 0x04009FD9 RID: 40921
		public bool HasPlayer;

		// Token: 0x04009FDA RID: 40922
		private Player _Player;

		// Token: 0x04009FDB RID: 40923
		public bool HasDeviceInfo;

		// Token: 0x04009FDC RID: 40924
		private DeviceInfo _DeviceInfo;

		// Token: 0x04009FDD RID: 40925
		public bool HasMessageType;

		// Token: 0x04009FDE RID: 40926
		private string _MessageType;

		// Token: 0x04009FDF RID: 40927
		public bool HasTitle;

		// Token: 0x04009FE0 RID: 40928
		private string _Title;

		// Token: 0x04009FE1 RID: 40929
		public bool HasUid;

		// Token: 0x04009FE2 RID: 40930
		private string _Uid;
	}
}
