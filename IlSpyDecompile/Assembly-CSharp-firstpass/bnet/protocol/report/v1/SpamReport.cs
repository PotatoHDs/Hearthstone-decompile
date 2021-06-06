using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.report.v1
{
	public class SpamReport : IProtoBuf
	{
		public static class Types
		{
			public enum SpamSource
			{
				OTHER = 1,
				FRIEND_INVITATION,
				WHISPER,
				CHAT
			}
		}

		public bool HasTarget;

		private GameAccountHandle _Target;

		public bool HasSource;

		private Types.SpamSource _Source;

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

		public Types.SpamSource Source
		{
			get
			{
				return _Source;
			}
			set
			{
				_Source = value;
				HasSource = true;
			}
		}

		public bool IsInitialized => true;

		public void SetTarget(GameAccountHandle val)
		{
			Target = val;
		}

		public void SetSource(Types.SpamSource val)
		{
			Source = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasTarget)
			{
				num ^= Target.GetHashCode();
			}
			if (HasSource)
			{
				num ^= Source.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SpamReport spamReport = obj as SpamReport;
			if (spamReport == null)
			{
				return false;
			}
			if (HasTarget != spamReport.HasTarget || (HasTarget && !Target.Equals(spamReport.Target)))
			{
				return false;
			}
			if (HasSource != spamReport.HasSource || (HasSource && !Source.Equals(spamReport.Source)))
			{
				return false;
			}
			return true;
		}

		public static SpamReport ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SpamReport>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SpamReport Deserialize(Stream stream, SpamReport instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SpamReport DeserializeLengthDelimited(Stream stream)
		{
			SpamReport spamReport = new SpamReport();
			DeserializeLengthDelimited(stream, spamReport);
			return spamReport;
		}

		public static SpamReport DeserializeLengthDelimited(Stream stream, SpamReport instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SpamReport Deserialize(Stream stream, SpamReport instance, long limit)
		{
			instance.Source = Types.SpamSource.OTHER;
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
				case 16:
					instance.Source = (Types.SpamSource)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, SpamReport instance)
		{
			if (instance.HasTarget)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Target.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.Target);
			}
			if (instance.HasSource)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Source);
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
			if (HasSource)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Source);
			}
			return num;
		}
	}
}
