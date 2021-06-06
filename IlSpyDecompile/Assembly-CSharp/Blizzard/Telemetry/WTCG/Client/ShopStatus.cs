using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class ShopStatus : IProtoBuf
	{
		public bool HasPlayer;

		private Player _Player;

		public bool HasError;

		private string _Error;

		public bool HasTimeInHubSec;

		private double _TimeInHubSec;

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

		public string Error
		{
			get
			{
				return _Error;
			}
			set
			{
				_Error = value;
				HasError = value != null;
			}
		}

		public double TimeInHubSec
		{
			get
			{
				return _TimeInHubSec;
			}
			set
			{
				_TimeInHubSec = value;
				HasTimeInHubSec = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasPlayer)
			{
				num ^= Player.GetHashCode();
			}
			if (HasError)
			{
				num ^= Error.GetHashCode();
			}
			if (HasTimeInHubSec)
			{
				num ^= TimeInHubSec.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ShopStatus shopStatus = obj as ShopStatus;
			if (shopStatus == null)
			{
				return false;
			}
			if (HasPlayer != shopStatus.HasPlayer || (HasPlayer && !Player.Equals(shopStatus.Player)))
			{
				return false;
			}
			if (HasError != shopStatus.HasError || (HasError && !Error.Equals(shopStatus.Error)))
			{
				return false;
			}
			if (HasTimeInHubSec != shopStatus.HasTimeInHubSec || (HasTimeInHubSec && !TimeInHubSec.Equals(shopStatus.TimeInHubSec)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ShopStatus Deserialize(Stream stream, ShopStatus instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ShopStatus DeserializeLengthDelimited(Stream stream)
		{
			ShopStatus shopStatus = new ShopStatus();
			DeserializeLengthDelimited(stream, shopStatus);
			return shopStatus;
		}

		public static ShopStatus DeserializeLengthDelimited(Stream stream, ShopStatus instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ShopStatus Deserialize(Stream stream, ShopStatus instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
					if (instance.Player == null)
					{
						instance.Player = Player.DeserializeLengthDelimited(stream);
					}
					else
					{
						Player.DeserializeLengthDelimited(stream, instance.Player);
					}
					continue;
				case 18:
					instance.Error = ProtocolParser.ReadString(stream);
					continue;
				case 25:
					instance.TimeInHubSec = binaryReader.ReadDouble();
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

		public static void Serialize(Stream stream, ShopStatus instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasPlayer)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Player.GetSerializedSize());
				Player.Serialize(stream, instance.Player);
			}
			if (instance.HasError)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Error));
			}
			if (instance.HasTimeInHubSec)
			{
				stream.WriteByte(25);
				binaryWriter.Write(instance.TimeInHubSec);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasPlayer)
			{
				num++;
				uint serializedSize = Player.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasError)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Error);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasTimeInHubSec)
			{
				num++;
				num += 8;
			}
			return num;
		}
	}
}
