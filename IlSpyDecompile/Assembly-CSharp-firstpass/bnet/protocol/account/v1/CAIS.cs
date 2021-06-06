using System.IO;

namespace bnet.protocol.account.v1
{
	public class CAIS : IProtoBuf
	{
		public bool HasPlayedMinutes;

		private uint _PlayedMinutes;

		public bool HasRestedMinutes;

		private uint _RestedMinutes;

		public bool HasLastHeardTime;

		private ulong _LastHeardTime;

		public uint PlayedMinutes
		{
			get
			{
				return _PlayedMinutes;
			}
			set
			{
				_PlayedMinutes = value;
				HasPlayedMinutes = true;
			}
		}

		public uint RestedMinutes
		{
			get
			{
				return _RestedMinutes;
			}
			set
			{
				_RestedMinutes = value;
				HasRestedMinutes = true;
			}
		}

		public ulong LastHeardTime
		{
			get
			{
				return _LastHeardTime;
			}
			set
			{
				_LastHeardTime = value;
				HasLastHeardTime = true;
			}
		}

		public bool IsInitialized => true;

		public void SetPlayedMinutes(uint val)
		{
			PlayedMinutes = val;
		}

		public void SetRestedMinutes(uint val)
		{
			RestedMinutes = val;
		}

		public void SetLastHeardTime(ulong val)
		{
			LastHeardTime = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasPlayedMinutes)
			{
				num ^= PlayedMinutes.GetHashCode();
			}
			if (HasRestedMinutes)
			{
				num ^= RestedMinutes.GetHashCode();
			}
			if (HasLastHeardTime)
			{
				num ^= LastHeardTime.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			CAIS cAIS = obj as CAIS;
			if (cAIS == null)
			{
				return false;
			}
			if (HasPlayedMinutes != cAIS.HasPlayedMinutes || (HasPlayedMinutes && !PlayedMinutes.Equals(cAIS.PlayedMinutes)))
			{
				return false;
			}
			if (HasRestedMinutes != cAIS.HasRestedMinutes || (HasRestedMinutes && !RestedMinutes.Equals(cAIS.RestedMinutes)))
			{
				return false;
			}
			if (HasLastHeardTime != cAIS.HasLastHeardTime || (HasLastHeardTime && !LastHeardTime.Equals(cAIS.LastHeardTime)))
			{
				return false;
			}
			return true;
		}

		public static CAIS ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CAIS>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CAIS Deserialize(Stream stream, CAIS instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CAIS DeserializeLengthDelimited(Stream stream)
		{
			CAIS cAIS = new CAIS();
			DeserializeLengthDelimited(stream, cAIS);
			return cAIS;
		}

		public static CAIS DeserializeLengthDelimited(Stream stream, CAIS instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CAIS Deserialize(Stream stream, CAIS instance, long limit)
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
				case 8:
					instance.PlayedMinutes = ProtocolParser.ReadUInt32(stream);
					continue;
				case 16:
					instance.RestedMinutes = ProtocolParser.ReadUInt32(stream);
					continue;
				case 24:
					instance.LastHeardTime = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, CAIS instance)
		{
			if (instance.HasPlayedMinutes)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.PlayedMinutes);
			}
			if (instance.HasRestedMinutes)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.RestedMinutes);
			}
			if (instance.HasLastHeardTime)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, instance.LastHeardTime);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasPlayedMinutes)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(PlayedMinutes);
			}
			if (HasRestedMinutes)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(RestedMinutes);
			}
			if (HasLastHeardTime)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(LastHeardTime);
			}
			return num;
		}
	}
}
