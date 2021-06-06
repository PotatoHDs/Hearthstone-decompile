using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.report.v1
{
	public class HarassmentReport : IProtoBuf
	{
		public bool HasTarget;

		private GameAccountHandle _Target;

		public bool HasText;

		private string _Text;

		public GameAccountHandle Target
		{
			get
			{
				return _Target;
			}
			set
			{
				_Target = value;
				HasTarget = value != null;
			}
		}

		public string Text
		{
			get
			{
				return _Text;
			}
			set
			{
				_Text = value;
				HasText = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetTarget(GameAccountHandle val)
		{
			Target = val;
		}

		public void SetText(string val)
		{
			Text = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasTarget)
			{
				num ^= Target.GetHashCode();
			}
			if (HasText)
			{
				num ^= Text.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			HarassmentReport harassmentReport = obj as HarassmentReport;
			if (harassmentReport == null)
			{
				return false;
			}
			if (HasTarget != harassmentReport.HasTarget || (HasTarget && !Target.Equals(harassmentReport.Target)))
			{
				return false;
			}
			if (HasText != harassmentReport.HasText || (HasText && !Text.Equals(harassmentReport.Text)))
			{
				return false;
			}
			return true;
		}

		public static HarassmentReport ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<HarassmentReport>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static HarassmentReport Deserialize(Stream stream, HarassmentReport instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static HarassmentReport DeserializeLengthDelimited(Stream stream)
		{
			HarassmentReport harassmentReport = new HarassmentReport();
			DeserializeLengthDelimited(stream, harassmentReport);
			return harassmentReport;
		}

		public static HarassmentReport DeserializeLengthDelimited(Stream stream, HarassmentReport instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static HarassmentReport Deserialize(Stream stream, HarassmentReport instance, long limit)
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
					if (instance.Target == null)
					{
						instance.Target = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.Target);
					}
					continue;
				case 18:
					instance.Text = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, HarassmentReport instance)
		{
			if (instance.HasTarget)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Target.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.Target);
			}
			if (instance.HasText)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Text));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasTarget)
			{
				num++;
				uint serializedSize = Target.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasText)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Text);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}
	}
}
