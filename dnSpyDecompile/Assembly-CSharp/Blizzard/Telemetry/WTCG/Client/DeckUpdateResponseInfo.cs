using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011B6 RID: 4534
	public class DeckUpdateResponseInfo : IProtoBuf
	{
		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x0600C8F0 RID: 51440 RVA: 0x003C447A File Offset: 0x003C267A
		// (set) Token: 0x0600C8F1 RID: 51441 RVA: 0x003C4482 File Offset: 0x003C2682
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

		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x0600C8F2 RID: 51442 RVA: 0x003C4495 File Offset: 0x003C2695
		// (set) Token: 0x0600C8F3 RID: 51443 RVA: 0x003C449D File Offset: 0x003C269D
		public float Duration
		{
			get
			{
				return this._Duration;
			}
			set
			{
				this._Duration = value;
				this.HasDuration = true;
			}
		}

		// Token: 0x0600C8F4 RID: 51444 RVA: 0x003C44B0 File Offset: 0x003C26B0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			if (this.HasDuration)
			{
				num ^= this.Duration.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C8F5 RID: 51445 RVA: 0x003C44FC File Offset: 0x003C26FC
		public override bool Equals(object obj)
		{
			DeckUpdateResponseInfo deckUpdateResponseInfo = obj as DeckUpdateResponseInfo;
			return deckUpdateResponseInfo != null && this.HasDeviceInfo == deckUpdateResponseInfo.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(deckUpdateResponseInfo.DeviceInfo)) && this.HasDuration == deckUpdateResponseInfo.HasDuration && (!this.HasDuration || this.Duration.Equals(deckUpdateResponseInfo.Duration));
		}

		// Token: 0x0600C8F6 RID: 51446 RVA: 0x003C456F File Offset: 0x003C276F
		public void Deserialize(Stream stream)
		{
			DeckUpdateResponseInfo.Deserialize(stream, this);
		}

		// Token: 0x0600C8F7 RID: 51447 RVA: 0x003C4579 File Offset: 0x003C2779
		public static DeckUpdateResponseInfo Deserialize(Stream stream, DeckUpdateResponseInfo instance)
		{
			return DeckUpdateResponseInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C8F8 RID: 51448 RVA: 0x003C4584 File Offset: 0x003C2784
		public static DeckUpdateResponseInfo DeserializeLengthDelimited(Stream stream)
		{
			DeckUpdateResponseInfo deckUpdateResponseInfo = new DeckUpdateResponseInfo();
			DeckUpdateResponseInfo.DeserializeLengthDelimited(stream, deckUpdateResponseInfo);
			return deckUpdateResponseInfo;
		}

		// Token: 0x0600C8F9 RID: 51449 RVA: 0x003C45A0 File Offset: 0x003C27A0
		public static DeckUpdateResponseInfo DeserializeLengthDelimited(Stream stream, DeckUpdateResponseInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DeckUpdateResponseInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C8FA RID: 51450 RVA: 0x003C45C8 File Offset: 0x003C27C8
		public static DeckUpdateResponseInfo Deserialize(Stream stream, DeckUpdateResponseInfo instance, long limit)
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
				else if (num != 10)
				{
					if (num != 21)
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
						instance.Duration = binaryReader.ReadSingle();
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

		// Token: 0x0600C8FB RID: 51451 RVA: 0x003C4681 File Offset: 0x003C2881
		public void Serialize(Stream stream)
		{
			DeckUpdateResponseInfo.Serialize(stream, this);
		}

		// Token: 0x0600C8FC RID: 51452 RVA: 0x003C468C File Offset: 0x003C288C
		public static void Serialize(Stream stream, DeckUpdateResponseInfo instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasDuration)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.Duration);
			}
		}

		// Token: 0x0600C8FD RID: 51453 RVA: 0x003C46EC File Offset: 0x003C28EC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize = this.DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasDuration)
			{
				num += 1U;
				num += 4U;
			}
			return num;
		}

		// Token: 0x04009F17 RID: 40727
		public bool HasDeviceInfo;

		// Token: 0x04009F18 RID: 40728
		private DeviceInfo _DeviceInfo;

		// Token: 0x04009F19 RID: 40729
		public bool HasDuration;

		// Token: 0x04009F1A RID: 40730
		private float _Duration;
	}
}
