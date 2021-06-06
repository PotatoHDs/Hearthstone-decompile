using System;
using System.IO;

namespace bnet.protocol.games.v1
{
	public class FinishGameRequest : IProtoBuf
	{
		public bool HasHost;

		private ProcessId _Host;

		public bool HasReason;

		private uint _Reason;

		public GameHandle GameHandle { get; set; }

		public ProcessId Host
		{
			get
			{
				return _Host;
			}
			set
			{
				_Host = value;
				HasHost = value != null;
			}
		}

		public uint Reason
		{
			get
			{
				return _Reason;
			}
			set
			{
				_Reason = value;
				HasReason = true;
			}
		}

		public bool IsInitialized => true;

		public void SetGameHandle(GameHandle val)
		{
			GameHandle = val;
		}

		public void SetHost(ProcessId val)
		{
			Host = val;
		}

		public void SetReason(uint val)
		{
			Reason = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= GameHandle.GetHashCode();
			if (HasHost)
			{
				hashCode ^= Host.GetHashCode();
			}
			if (HasReason)
			{
				hashCode ^= Reason.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			FinishGameRequest finishGameRequest = obj as FinishGameRequest;
			if (finishGameRequest == null)
			{
				return false;
			}
			if (!GameHandle.Equals(finishGameRequest.GameHandle))
			{
				return false;
			}
			if (HasHost != finishGameRequest.HasHost || (HasHost && !Host.Equals(finishGameRequest.Host)))
			{
				return false;
			}
			if (HasReason != finishGameRequest.HasReason || (HasReason && !Reason.Equals(finishGameRequest.Reason)))
			{
				return false;
			}
			return true;
		}

		public static FinishGameRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FinishGameRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static FinishGameRequest Deserialize(Stream stream, FinishGameRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FinishGameRequest DeserializeLengthDelimited(Stream stream)
		{
			FinishGameRequest finishGameRequest = new FinishGameRequest();
			DeserializeLengthDelimited(stream, finishGameRequest);
			return finishGameRequest;
		}

		public static FinishGameRequest DeserializeLengthDelimited(Stream stream, FinishGameRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FinishGameRequest Deserialize(Stream stream, FinishGameRequest instance, long limit)
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
					if (instance.GameHandle == null)
					{
						instance.GameHandle = GameHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameHandle.DeserializeLengthDelimited(stream, instance.GameHandle);
					}
					continue;
				case 18:
					if (instance.Host == null)
					{
						instance.Host = ProcessId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProcessId.DeserializeLengthDelimited(stream, instance.Host);
					}
					continue;
				case 24:
					instance.Reason = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, FinishGameRequest instance)
		{
			if (instance.GameHandle == null)
			{
				throw new ArgumentNullException("GameHandle", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
			GameHandle.Serialize(stream, instance.GameHandle);
			if (instance.HasHost)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Host);
			}
			if (instance.HasReason)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.Reason);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = GameHandle.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (HasHost)
			{
				num++;
				uint serializedSize2 = Host.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasReason)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Reason);
			}
			return num + 1;
		}
	}
}
