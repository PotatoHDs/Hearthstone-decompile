using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011BB RID: 4539
	public class DragDropCancelPlayCard : IProtoBuf
	{
		// Token: 0x17000EFA RID: 3834
		// (get) Token: 0x0600C94F RID: 51535 RVA: 0x003C59D6 File Offset: 0x003C3BD6
		// (set) Token: 0x0600C950 RID: 51536 RVA: 0x003C59DE File Offset: 0x003C3BDE
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

		// Token: 0x17000EFB RID: 3835
		// (get) Token: 0x0600C951 RID: 51537 RVA: 0x003C59F1 File Offset: 0x003C3BF1
		// (set) Token: 0x0600C952 RID: 51538 RVA: 0x003C59F9 File Offset: 0x003C3BF9
		public long ScenarioId
		{
			get
			{
				return this._ScenarioId;
			}
			set
			{
				this._ScenarioId = value;
				this.HasScenarioId = true;
			}
		}

		// Token: 0x17000EFC RID: 3836
		// (get) Token: 0x0600C953 RID: 51539 RVA: 0x003C5A09 File Offset: 0x003C3C09
		// (set) Token: 0x0600C954 RID: 51540 RVA: 0x003C5A11 File Offset: 0x003C3C11
		public string CardType
		{
			get
			{
				return this._CardType;
			}
			set
			{
				this._CardType = value;
				this.HasCardType = (value != null);
			}
		}

		// Token: 0x0600C955 RID: 51541 RVA: 0x003C5A24 File Offset: 0x003C3C24
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			if (this.HasScenarioId)
			{
				num ^= this.ScenarioId.GetHashCode();
			}
			if (this.HasCardType)
			{
				num ^= this.CardType.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C956 RID: 51542 RVA: 0x003C5A84 File Offset: 0x003C3C84
		public override bool Equals(object obj)
		{
			DragDropCancelPlayCard dragDropCancelPlayCard = obj as DragDropCancelPlayCard;
			return dragDropCancelPlayCard != null && this.HasDeviceInfo == dragDropCancelPlayCard.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(dragDropCancelPlayCard.DeviceInfo)) && this.HasScenarioId == dragDropCancelPlayCard.HasScenarioId && (!this.HasScenarioId || this.ScenarioId.Equals(dragDropCancelPlayCard.ScenarioId)) && this.HasCardType == dragDropCancelPlayCard.HasCardType && (!this.HasCardType || this.CardType.Equals(dragDropCancelPlayCard.CardType));
		}

		// Token: 0x0600C957 RID: 51543 RVA: 0x003C5B22 File Offset: 0x003C3D22
		public void Deserialize(Stream stream)
		{
			DragDropCancelPlayCard.Deserialize(stream, this);
		}

		// Token: 0x0600C958 RID: 51544 RVA: 0x003C5B2C File Offset: 0x003C3D2C
		public static DragDropCancelPlayCard Deserialize(Stream stream, DragDropCancelPlayCard instance)
		{
			return DragDropCancelPlayCard.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C959 RID: 51545 RVA: 0x003C5B38 File Offset: 0x003C3D38
		public static DragDropCancelPlayCard DeserializeLengthDelimited(Stream stream)
		{
			DragDropCancelPlayCard dragDropCancelPlayCard = new DragDropCancelPlayCard();
			DragDropCancelPlayCard.DeserializeLengthDelimited(stream, dragDropCancelPlayCard);
			return dragDropCancelPlayCard;
		}

		// Token: 0x0600C95A RID: 51546 RVA: 0x003C5B54 File Offset: 0x003C3D54
		public static DragDropCancelPlayCard DeserializeLengthDelimited(Stream stream, DragDropCancelPlayCard instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DragDropCancelPlayCard.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C95B RID: 51547 RVA: 0x003C5B7C File Offset: 0x003C3D7C
		public static DragDropCancelPlayCard Deserialize(Stream stream, DragDropCancelPlayCard instance, long limit)
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
						if (num != 26)
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
							instance.CardType = ProtocolParser.ReadString(stream);
						}
					}
					else
					{
						instance.ScenarioId = (long)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x0600C95C RID: 51548 RVA: 0x003C5C4A File Offset: 0x003C3E4A
		public void Serialize(Stream stream)
		{
			DragDropCancelPlayCard.Serialize(stream, this);
		}

		// Token: 0x0600C95D RID: 51549 RVA: 0x003C5C54 File Offset: 0x003C3E54
		public static void Serialize(Stream stream, DragDropCancelPlayCard instance)
		{
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasScenarioId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ScenarioId);
			}
			if (instance.HasCardType)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.CardType));
			}
		}

		// Token: 0x0600C95E RID: 51550 RVA: 0x003C5CD0 File Offset: 0x003C3ED0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize = this.DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasScenarioId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.ScenarioId);
			}
			if (this.HasCardType)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.CardType);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x04009F3F RID: 40767
		public bool HasDeviceInfo;

		// Token: 0x04009F40 RID: 40768
		private DeviceInfo _DeviceInfo;

		// Token: 0x04009F41 RID: 40769
		public bool HasScenarioId;

		// Token: 0x04009F42 RID: 40770
		private long _ScenarioId;

		// Token: 0x04009F43 RID: 40771
		public bool HasCardType;

		// Token: 0x04009F44 RID: 40772
		private string _CardType;
	}
}
