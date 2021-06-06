using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011B5 RID: 4533
	public class DeckPickerToCollection : IProtoBuf
	{
		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x0600C8E1 RID: 51425 RVA: 0x003C41B4 File Offset: 0x003C23B4
		// (set) Token: 0x0600C8E2 RID: 51426 RVA: 0x003C41BC File Offset: 0x003C23BC
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

		// Token: 0x17000EE5 RID: 3813
		// (get) Token: 0x0600C8E3 RID: 51427 RVA: 0x003C41CF File Offset: 0x003C23CF
		// (set) Token: 0x0600C8E4 RID: 51428 RVA: 0x003C41D7 File Offset: 0x003C23D7
		public DeckPickerToCollection.Path Path_
		{
			get
			{
				return this._Path_;
			}
			set
			{
				this._Path_ = value;
				this.HasPath_ = true;
			}
		}

		// Token: 0x0600C8E5 RID: 51429 RVA: 0x003C41E8 File Offset: 0x003C23E8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			if (this.HasPath_)
			{
				num ^= this.Path_.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C8E6 RID: 51430 RVA: 0x003C4238 File Offset: 0x003C2438
		public override bool Equals(object obj)
		{
			DeckPickerToCollection deckPickerToCollection = obj as DeckPickerToCollection;
			return deckPickerToCollection != null && this.HasDeviceInfo == deckPickerToCollection.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(deckPickerToCollection.DeviceInfo)) && this.HasPath_ == deckPickerToCollection.HasPath_ && (!this.HasPath_ || this.Path_.Equals(deckPickerToCollection.Path_));
		}

		// Token: 0x0600C8E7 RID: 51431 RVA: 0x003C42B6 File Offset: 0x003C24B6
		public void Deserialize(Stream stream)
		{
			DeckPickerToCollection.Deserialize(stream, this);
		}

		// Token: 0x0600C8E8 RID: 51432 RVA: 0x003C42C0 File Offset: 0x003C24C0
		public static DeckPickerToCollection Deserialize(Stream stream, DeckPickerToCollection instance)
		{
			return DeckPickerToCollection.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C8E9 RID: 51433 RVA: 0x003C42CC File Offset: 0x003C24CC
		public static DeckPickerToCollection DeserializeLengthDelimited(Stream stream)
		{
			DeckPickerToCollection deckPickerToCollection = new DeckPickerToCollection();
			DeckPickerToCollection.DeserializeLengthDelimited(stream, deckPickerToCollection);
			return deckPickerToCollection;
		}

		// Token: 0x0600C8EA RID: 51434 RVA: 0x003C42E8 File Offset: 0x003C24E8
		public static DeckPickerToCollection DeserializeLengthDelimited(Stream stream, DeckPickerToCollection instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DeckPickerToCollection.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C8EB RID: 51435 RVA: 0x003C4310 File Offset: 0x003C2510
		public static DeckPickerToCollection Deserialize(Stream stream, DeckPickerToCollection instance, long limit)
		{
			instance.Path_ = DeckPickerToCollection.Path.DECK_PICKER_BUTTON;
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else
					{
						instance.Path_ = (DeckPickerToCollection.Path)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x0600C8EC RID: 51436 RVA: 0x003C43CA File Offset: 0x003C25CA
		public void Serialize(Stream stream)
		{
			DeckPickerToCollection.Serialize(stream, this);
		}

		// Token: 0x0600C8ED RID: 51437 RVA: 0x003C43D4 File Offset: 0x003C25D4
		public static void Serialize(Stream stream, DeckPickerToCollection instance)
		{
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasPath_)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Path_));
			}
		}

		// Token: 0x0600C8EE RID: 51438 RVA: 0x003C442C File Offset: 0x003C262C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize = this.DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasPath_)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Path_));
			}
			return num;
		}

		// Token: 0x04009F13 RID: 40723
		public bool HasDeviceInfo;

		// Token: 0x04009F14 RID: 40724
		private DeviceInfo _DeviceInfo;

		// Token: 0x04009F15 RID: 40725
		public bool HasPath_;

		// Token: 0x04009F16 RID: 40726
		private DeckPickerToCollection.Path _Path_;

		// Token: 0x0200293E RID: 10558
		public enum Path
		{
			// Token: 0x0400FC47 RID: 64583
			DECK_PICKER_BUTTON = 1,
			// Token: 0x0400FC48 RID: 64584
			BACK_TO_BOX
		}
	}
}
