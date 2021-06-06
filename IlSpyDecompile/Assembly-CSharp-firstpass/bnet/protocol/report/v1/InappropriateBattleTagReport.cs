using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.report.v1
{
	public class InappropriateBattleTagReport : IProtoBuf
	{
		public bool HasTarget;

		private GameAccountHandle _Target;

		public bool HasBattleTag;

		private string _BattleTag;

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

		public string BattleTag
		{
			get
			{
				return _BattleTag;
			}
			set
			{
				_BattleTag = value;
				HasBattleTag = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetTarget(GameAccountHandle val)
		{
			Target = val;
		}

		public void SetBattleTag(string val)
		{
			BattleTag = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasTarget)
			{
				num ^= Target.GetHashCode();
			}
			if (HasBattleTag)
			{
				num ^= BattleTag.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			InappropriateBattleTagReport inappropriateBattleTagReport = obj as InappropriateBattleTagReport;
			if (inappropriateBattleTagReport == null)
			{
				return false;
			}
			if (HasTarget != inappropriateBattleTagReport.HasTarget || (HasTarget && !Target.Equals(inappropriateBattleTagReport.Target)))
			{
				return false;
			}
			if (HasBattleTag != inappropriateBattleTagReport.HasBattleTag || (HasBattleTag && !BattleTag.Equals(inappropriateBattleTagReport.BattleTag)))
			{
				return false;
			}
			return true;
		}

		public static InappropriateBattleTagReport ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<InappropriateBattleTagReport>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static InappropriateBattleTagReport Deserialize(Stream stream, InappropriateBattleTagReport instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static InappropriateBattleTagReport DeserializeLengthDelimited(Stream stream)
		{
			InappropriateBattleTagReport inappropriateBattleTagReport = new InappropriateBattleTagReport();
			DeserializeLengthDelimited(stream, inappropriateBattleTagReport);
			return inappropriateBattleTagReport;
		}

		public static InappropriateBattleTagReport DeserializeLengthDelimited(Stream stream, InappropriateBattleTagReport instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static InappropriateBattleTagReport Deserialize(Stream stream, InappropriateBattleTagReport instance, long limit)
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
					instance.BattleTag = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, InappropriateBattleTagReport instance)
		{
			if (instance.HasTarget)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Target.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.Target);
			}
			if (instance.HasBattleTag)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BattleTag));
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
			if (HasBattleTag)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(BattleTag);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}
	}
}
