using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class FlowPerformanceShop : IProtoBuf
	{
		public enum ShopType
		{
			GENERAL_STORE = 1,
			ARENA_STORE,
			ADVENTURE_STORE,
			TAVERN_BRAWL_STORE,
			ADVENTURE_STORE_WING_PURCHASE_WIDGET,
			ADVENTURE_STORE_FULL_PURCHASE_WIDGET,
			DUELS_STORE
		}

		public bool HasFlowId;

		private string _FlowId;

		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasPlayer;

		private Player _Player;

		public bool HasShopType_;

		private ShopType _ShopType_;

		public string FlowId
		{
			get
			{
				return _FlowId;
			}
			set
			{
				_FlowId = value;
				HasFlowId = value != null;
			}
		}

		public DeviceInfo DeviceInfo
		{
			get
			{
				return _DeviceInfo;
			}
			set
			{
				_DeviceInfo = value;
				HasDeviceInfo = value != null;
			}
		}

		public Player Player
		{
			get
			{
				return _Player;
			}
			set
			{
				_Player = value;
				HasPlayer = value != null;
			}
		}

		public ShopType ShopType_
		{
			get
			{
				return _ShopType_;
			}
			set
			{
				_ShopType_ = value;
				HasShopType_ = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasFlowId)
			{
				num ^= FlowId.GetHashCode();
			}
			if (HasDeviceInfo)
			{
				num ^= DeviceInfo.GetHashCode();
			}
			if (HasPlayer)
			{
				num ^= Player.GetHashCode();
			}
			if (HasShopType_)
			{
				num ^= ShopType_.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			FlowPerformanceShop flowPerformanceShop = obj as FlowPerformanceShop;
			if (flowPerformanceShop == null)
			{
				return false;
			}
			if (HasFlowId != flowPerformanceShop.HasFlowId || (HasFlowId && !FlowId.Equals(flowPerformanceShop.FlowId)))
			{
				return false;
			}
			if (HasDeviceInfo != flowPerformanceShop.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(flowPerformanceShop.DeviceInfo)))
			{
				return false;
			}
			if (HasPlayer != flowPerformanceShop.HasPlayer || (HasPlayer && !Player.Equals(flowPerformanceShop.Player)))
			{
				return false;
			}
			if (HasShopType_ != flowPerformanceShop.HasShopType_ || (HasShopType_ && !ShopType_.Equals(flowPerformanceShop.ShopType_)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static FlowPerformanceShop Deserialize(Stream stream, FlowPerformanceShop instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FlowPerformanceShop DeserializeLengthDelimited(Stream stream)
		{
			FlowPerformanceShop flowPerformanceShop = new FlowPerformanceShop();
			DeserializeLengthDelimited(stream, flowPerformanceShop);
			return flowPerformanceShop;
		}

		public static FlowPerformanceShop DeserializeLengthDelimited(Stream stream, FlowPerformanceShop instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FlowPerformanceShop Deserialize(Stream stream, FlowPerformanceShop instance, long limit)
		{
			instance.ShopType_ = ShopType.GENERAL_STORE;
			while (true)
			{
				if (limit >= 0 && stream.Position >= limit)
				{
					if (stream.Position == limit)
					{
						break;
					}
					throw new ProtocolBufferException("Read past max limit");
				}
				int num = stream.ReadByte();
				switch (num)
				{
				case -1:
					break;
				case 10:
					instance.FlowId = ProtocolParser.ReadString(stream);
					continue;
				case 18:
					if (instance.DeviceInfo == null)
					{
						instance.DeviceInfo = DeviceInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						DeviceInfo.DeserializeLengthDelimited(stream, instance.DeviceInfo);
					}
					continue;
				case 26:
					if (instance.Player == null)
					{
						instance.Player = Player.DeserializeLengthDelimited(stream);
					}
					else
					{
						Player.DeserializeLengthDelimited(stream, instance.Player);
					}
					continue;
				case 32:
					instance.ShopType_ = (ShopType)ProtocolParser.ReadUInt64(stream);
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
					continue;
				}
				}
				if (limit < 0)
				{
					break;
				}
				throw new EndOfStreamException();
			}
			return instance;
		}

		public void Serialize(Stream stream)
		{
			Serialize(stream, this);
		}

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
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ShopType_);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasFlowId)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(FlowId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasDeviceInfo)
			{
				num++;
				uint serializedSize = DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasPlayer)
			{
				num++;
				uint serializedSize2 = Player.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasShopType_)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ShopType_);
			}
			return num;
		}
	}
}
