using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011C3 RID: 4547
	public class FlowPerformanceShop : IProtoBuf
	{
		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x0600C9FF RID: 51711 RVA: 0x003C8481 File Offset: 0x003C6681
		// (set) Token: 0x0600CA00 RID: 51712 RVA: 0x003C8489 File Offset: 0x003C6689
		public string FlowId
		{
			get
			{
				return this._FlowId;
			}
			set
			{
				this._FlowId = value;
				this.HasFlowId = (value != null);
			}
		}

		// Token: 0x17000F27 RID: 3879
		// (get) Token: 0x0600CA01 RID: 51713 RVA: 0x003C849C File Offset: 0x003C669C
		// (set) Token: 0x0600CA02 RID: 51714 RVA: 0x003C84A4 File Offset: 0x003C66A4
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

		// Token: 0x17000F28 RID: 3880
		// (get) Token: 0x0600CA03 RID: 51715 RVA: 0x003C84B7 File Offset: 0x003C66B7
		// (set) Token: 0x0600CA04 RID: 51716 RVA: 0x003C84BF File Offset: 0x003C66BF
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

		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x0600CA05 RID: 51717 RVA: 0x003C84D2 File Offset: 0x003C66D2
		// (set) Token: 0x0600CA06 RID: 51718 RVA: 0x003C84DA File Offset: 0x003C66DA
		public FlowPerformanceShop.ShopType ShopType_
		{
			get
			{
				return this._ShopType_;
			}
			set
			{
				this._ShopType_ = value;
				this.HasShopType_ = true;
			}
		}

		// Token: 0x0600CA07 RID: 51719 RVA: 0x003C84EC File Offset: 0x003C66EC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasFlowId)
			{
				num ^= this.FlowId.GetHashCode();
			}
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			if (this.HasPlayer)
			{
				num ^= this.Player.GetHashCode();
			}
			if (this.HasShopType_)
			{
				num ^= this.ShopType_.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CA08 RID: 51720 RVA: 0x003C8568 File Offset: 0x003C6768
		public override bool Equals(object obj)
		{
			FlowPerformanceShop flowPerformanceShop = obj as FlowPerformanceShop;
			return flowPerformanceShop != null && this.HasFlowId == flowPerformanceShop.HasFlowId && (!this.HasFlowId || this.FlowId.Equals(flowPerformanceShop.FlowId)) && this.HasDeviceInfo == flowPerformanceShop.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(flowPerformanceShop.DeviceInfo)) && this.HasPlayer == flowPerformanceShop.HasPlayer && (!this.HasPlayer || this.Player.Equals(flowPerformanceShop.Player)) && this.HasShopType_ == flowPerformanceShop.HasShopType_ && (!this.HasShopType_ || this.ShopType_.Equals(flowPerformanceShop.ShopType_));
		}

		// Token: 0x0600CA09 RID: 51721 RVA: 0x003C863C File Offset: 0x003C683C
		public void Deserialize(Stream stream)
		{
			FlowPerformanceShop.Deserialize(stream, this);
		}

		// Token: 0x0600CA0A RID: 51722 RVA: 0x003C8646 File Offset: 0x003C6846
		public static FlowPerformanceShop Deserialize(Stream stream, FlowPerformanceShop instance)
		{
			return FlowPerformanceShop.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CA0B RID: 51723 RVA: 0x003C8654 File Offset: 0x003C6854
		public static FlowPerformanceShop DeserializeLengthDelimited(Stream stream)
		{
			FlowPerformanceShop flowPerformanceShop = new FlowPerformanceShop();
			FlowPerformanceShop.DeserializeLengthDelimited(stream, flowPerformanceShop);
			return flowPerformanceShop;
		}

		// Token: 0x0600CA0C RID: 51724 RVA: 0x003C8670 File Offset: 0x003C6870
		public static FlowPerformanceShop DeserializeLengthDelimited(Stream stream, FlowPerformanceShop instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FlowPerformanceShop.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CA0D RID: 51725 RVA: 0x003C8698 File Offset: 0x003C6898
		public static FlowPerformanceShop Deserialize(Stream stream, FlowPerformanceShop instance, long limit)
		{
			instance.ShopType_ = FlowPerformanceShop.ShopType.GENERAL_STORE;
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
						if (num == 10)
						{
							instance.FlowId = ProtocolParser.ReadString(stream);
							continue;
						}
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
					else if (num != 26)
					{
						if (num == 32)
						{
							instance.ShopType_ = (FlowPerformanceShop.ShopType)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x0600CA0E RID: 51726 RVA: 0x003C87AB File Offset: 0x003C69AB
		public void Serialize(Stream stream)
		{
			FlowPerformanceShop.Serialize(stream, this);
		}

		// Token: 0x0600CA0F RID: 51727 RVA: 0x003C87B4 File Offset: 0x003C69B4
		public static void Serialize(Stream stream, FlowPerformanceShop instance)
		{
			if (instance.HasFlowId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FlowId));
			}
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasPlayer)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Player.GetSerializedSize());
				Player.Serialize(stream, instance.Player);
			}
			if (instance.HasShopType_)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ShopType_));
			}
		}

		// Token: 0x0600CA10 RID: 51728 RVA: 0x003C8860 File Offset: 0x003C6A60
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasFlowId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.FlowId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize = this.DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasPlayer)
			{
				num += 1U;
				uint serializedSize2 = this.Player.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasShopType_)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ShopType_));
			}
			return num;
		}

		// Token: 0x04009F97 RID: 40855
		public bool HasFlowId;

		// Token: 0x04009F98 RID: 40856
		private string _FlowId;

		// Token: 0x04009F99 RID: 40857
		public bool HasDeviceInfo;

		// Token: 0x04009F9A RID: 40858
		private DeviceInfo _DeviceInfo;

		// Token: 0x04009F9B RID: 40859
		public bool HasPlayer;

		// Token: 0x04009F9C RID: 40860
		private Player _Player;

		// Token: 0x04009F9D RID: 40861
		public bool HasShopType_;

		// Token: 0x04009F9E RID: 40862
		private FlowPerformanceShop.ShopType _ShopType_;

		// Token: 0x02002943 RID: 10563
		public enum ShopType
		{
			// Token: 0x0400FC61 RID: 64609
			GENERAL_STORE = 1,
			// Token: 0x0400FC62 RID: 64610
			ARENA_STORE,
			// Token: 0x0400FC63 RID: 64611
			ADVENTURE_STORE,
			// Token: 0x0400FC64 RID: 64612
			TAVERN_BRAWL_STORE,
			// Token: 0x0400FC65 RID: 64613
			ADVENTURE_STORE_WING_PURCHASE_WIDGET,
			// Token: 0x0400FC66 RID: 64614
			ADVENTURE_STORE_FULL_PURCHASE_WIDGET,
			// Token: 0x0400FC67 RID: 64615
			DUELS_STORE
		}
	}
}
