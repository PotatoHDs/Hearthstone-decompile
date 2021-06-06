using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class AttributionGameRoundEnd : IProtoBuf
	{
		public bool HasApplicationId;

		private string _ApplicationId;

		public bool HasDeviceType;

		private string _DeviceType;

		public bool HasFirstInstallDate;

		private ulong _FirstInstallDate;

		public bool HasBundleId;

		private string _BundleId;

		public bool HasGameMode;

		private string _GameMode;

		public bool HasResult;

		private string _Result;

		public bool HasFormatType;

		private FormatType _FormatType;

		public string ApplicationId
		{
			get
			{
				return _ApplicationId;
			}
			set
			{
				_ApplicationId = value;
				HasApplicationId = value != null;
			}
		}

		public string DeviceType
		{
			get
			{
				return _DeviceType;
			}
			set
			{
				_DeviceType = value;
				HasDeviceType = value != null;
			}
		}

		public ulong FirstInstallDate
		{
			get
			{
				return _FirstInstallDate;
			}
			set
			{
				_FirstInstallDate = value;
				HasFirstInstallDate = true;
			}
		}

		public string BundleId
		{
			get
			{
				return _BundleId;
			}
			set
			{
				_BundleId = value;
				HasBundleId = value != null;
			}
		}

		public string GameMode
		{
			get
			{
				return _GameMode;
			}
			set
			{
				_GameMode = value;
				HasGameMode = value != null;
			}
		}

		public string Result
		{
			get
			{
				return _Result;
			}
			set
			{
				_Result = value;
				HasResult = value != null;
			}
		}

		public FormatType FormatType
		{
			get
			{
				return _FormatType;
			}
			set
			{
				_FormatType = value;
				HasFormatType = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasApplicationId)
			{
				num ^= ApplicationId.GetHashCode();
			}
			if (HasDeviceType)
			{
				num ^= DeviceType.GetHashCode();
			}
			if (HasFirstInstallDate)
			{
				num ^= FirstInstallDate.GetHashCode();
			}
			if (HasBundleId)
			{
				num ^= BundleId.GetHashCode();
			}
			if (HasGameMode)
			{
				num ^= GameMode.GetHashCode();
			}
			if (HasResult)
			{
				num ^= Result.GetHashCode();
			}
			if (HasFormatType)
			{
				num ^= FormatType.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AttributionGameRoundEnd attributionGameRoundEnd = obj as AttributionGameRoundEnd;
			if (attributionGameRoundEnd == null)
			{
				return false;
			}
			if (HasApplicationId != attributionGameRoundEnd.HasApplicationId || (HasApplicationId && !ApplicationId.Equals(attributionGameRoundEnd.ApplicationId)))
			{
				return false;
			}
			if (HasDeviceType != attributionGameRoundEnd.HasDeviceType || (HasDeviceType && !DeviceType.Equals(attributionGameRoundEnd.DeviceType)))
			{
				return false;
			}
			if (HasFirstInstallDate != attributionGameRoundEnd.HasFirstInstallDate || (HasFirstInstallDate && !FirstInstallDate.Equals(attributionGameRoundEnd.FirstInstallDate)))
			{
				return false;
			}
			if (HasBundleId != attributionGameRoundEnd.HasBundleId || (HasBundleId && !BundleId.Equals(attributionGameRoundEnd.BundleId)))
			{
				return false;
			}
			if (HasGameMode != attributionGameRoundEnd.HasGameMode || (HasGameMode && !GameMode.Equals(attributionGameRoundEnd.GameMode)))
			{
				return false;
			}
			if (HasResult != attributionGameRoundEnd.HasResult || (HasResult && !Result.Equals(attributionGameRoundEnd.Result)))
			{
				return false;
			}
			if (HasFormatType != attributionGameRoundEnd.HasFormatType || (HasFormatType && !FormatType.Equals(attributionGameRoundEnd.FormatType)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AttributionGameRoundEnd Deserialize(Stream stream, AttributionGameRoundEnd instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AttributionGameRoundEnd DeserializeLengthDelimited(Stream stream)
		{
			AttributionGameRoundEnd attributionGameRoundEnd = new AttributionGameRoundEnd();
			DeserializeLengthDelimited(stream, attributionGameRoundEnd);
			return attributionGameRoundEnd;
		}

		public static AttributionGameRoundEnd DeserializeLengthDelimited(Stream stream, AttributionGameRoundEnd instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AttributionGameRoundEnd Deserialize(Stream stream, AttributionGameRoundEnd instance, long limit)
		{
			instance.FormatType = FormatType.FT_UNKNOWN;
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
					instance.GameMode = ProtocolParser.ReadString(stream);
					continue;
				case 18:
					instance.Result = ProtocolParser.ReadString(stream);
					continue;
				case 40:
					instance.FormatType = (FormatType)ProtocolParser.ReadUInt64(stream);
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					switch (key.Field)
					{
					case 0u:
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					case 100u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.ApplicationId = ProtocolParser.ReadString(stream);
						}
						break;
					case 101u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.DeviceType = ProtocolParser.ReadString(stream);
						}
						break;
					case 102u:
						if (key.WireType == Wire.Varint)
						{
							instance.FirstInstallDate = ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 103u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.BundleId = ProtocolParser.ReadString(stream);
						}
						break;
					default:
						ProtocolParser.SkipKey(stream, key);
						break;
					}
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

		public static void Serialize(Stream stream, AttributionGameRoundEnd instance)
		{
			if (instance.HasApplicationId)
			{
				stream.WriteByte(162);
				stream.WriteByte(6);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ApplicationId));
			}
			if (instance.HasDeviceType)
			{
				stream.WriteByte(170);
				stream.WriteByte(6);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeviceType));
			}
			if (instance.HasFirstInstallDate)
			{
				stream.WriteByte(176);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt64(stream, instance.FirstInstallDate);
			}
			if (instance.HasBundleId)
			{
				stream.WriteByte(186);
				stream.WriteByte(6);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BundleId));
			}
			if (instance.HasGameMode)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.GameMode));
			}
			if (instance.HasResult)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Result));
			}
			if (instance.HasFormatType)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.FormatType);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasApplicationId)
			{
				num += 2;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(ApplicationId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasDeviceType)
			{
				num += 2;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(DeviceType);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasFirstInstallDate)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64(FirstInstallDate);
			}
			if (HasBundleId)
			{
				num += 2;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(BundleId);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (HasGameMode)
			{
				num++;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(GameMode);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (HasResult)
			{
				num++;
				uint byteCount5 = (uint)Encoding.UTF8.GetByteCount(Result);
				num += ProtocolParser.SizeOfUInt32(byteCount5) + byteCount5;
			}
			if (HasFormatType)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)FormatType);
			}
			return num;
		}
	}
}
