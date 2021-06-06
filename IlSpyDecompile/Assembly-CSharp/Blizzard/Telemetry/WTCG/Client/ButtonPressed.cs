using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class ButtonPressed : IProtoBuf
	{
		public bool HasButtonName;

		private string _ButtonName;

		public string ButtonName
		{
			get
			{
				return _ButtonName;
			}
			set
			{
				_ButtonName = value;
				HasButtonName = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasButtonName)
			{
				num ^= ButtonName.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ButtonPressed buttonPressed = obj as ButtonPressed;
			if (buttonPressed == null)
			{
				return false;
			}
			if (HasButtonName != buttonPressed.HasButtonName || (HasButtonName && !ButtonName.Equals(buttonPressed.ButtonName)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ButtonPressed Deserialize(Stream stream, ButtonPressed instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ButtonPressed DeserializeLengthDelimited(Stream stream)
		{
			ButtonPressed buttonPressed = new ButtonPressed();
			DeserializeLengthDelimited(stream, buttonPressed);
			return buttonPressed;
		}

		public static ButtonPressed DeserializeLengthDelimited(Stream stream, ButtonPressed instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ButtonPressed Deserialize(Stream stream, ButtonPressed instance, long limit)
		{
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
					instance.ButtonName = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, ButtonPressed instance)
		{
			if (instance.HasButtonName)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ButtonName));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasButtonName)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(ButtonName);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}
	}
}
