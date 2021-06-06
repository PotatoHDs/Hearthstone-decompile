using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.report.v1
{
	public class BottingReport : IProtoBuf
	{
		public bool HasTarget;

		private GameAccountHandle _Target;

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

		public bool IsInitialized => true;

		public void SetTarget(GameAccountHandle val)
		{
			Target = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasTarget)
			{
				num ^= Target.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			BottingReport bottingReport = obj as BottingReport;
			if (bottingReport == null)
			{
				return false;
			}
			if (HasTarget != bottingReport.HasTarget || (HasTarget && !Target.Equals(bottingReport.Target)))
			{
				return false;
			}
			return true;
		}

		public static BottingReport ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<BottingReport>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static BottingReport Deserialize(Stream stream, BottingReport instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static BottingReport DeserializeLengthDelimited(Stream stream)
		{
			BottingReport bottingReport = new BottingReport();
			DeserializeLengthDelimited(stream, bottingReport);
			return bottingReport;
		}

		public static BottingReport DeserializeLengthDelimited(Stream stream, BottingReport instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static BottingReport Deserialize(Stream stream, BottingReport instance, long limit)
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

		public static void Serialize(Stream stream, BottingReport instance)
		{
			if (instance.HasTarget)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Target.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.Target);
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
			return num;
		}
	}
}
