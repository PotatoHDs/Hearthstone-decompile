using System.IO;

namespace bnet.protocol.authentication.v1
{
	public class ModuleNotification : IProtoBuf
	{
		public bool HasModuleId;

		private int _ModuleId;

		public bool HasResult;

		private uint _Result;

		public int ModuleId
		{
			get
			{
				return _ModuleId;
			}
			set
			{
				_ModuleId = value;
				HasModuleId = true;
			}
		}

		public uint Result
		{
			get
			{
				return _Result;
			}
			set
			{
				_Result = value;
				HasResult = true;
			}
		}

		public bool IsInitialized => true;

		public void SetModuleId(int val)
		{
			ModuleId = val;
		}

		public void SetResult(uint val)
		{
			Result = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasModuleId)
			{
				num ^= ModuleId.GetHashCode();
			}
			if (HasResult)
			{
				num ^= Result.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ModuleNotification moduleNotification = obj as ModuleNotification;
			if (moduleNotification == null)
			{
				return false;
			}
			if (HasModuleId != moduleNotification.HasModuleId || (HasModuleId && !ModuleId.Equals(moduleNotification.ModuleId)))
			{
				return false;
			}
			if (HasResult != moduleNotification.HasResult || (HasResult && !Result.Equals(moduleNotification.Result)))
			{
				return false;
			}
			return true;
		}

		public static ModuleNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ModuleNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ModuleNotification Deserialize(Stream stream, ModuleNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ModuleNotification DeserializeLengthDelimited(Stream stream)
		{
			ModuleNotification moduleNotification = new ModuleNotification();
			DeserializeLengthDelimited(stream, moduleNotification);
			return moduleNotification;
		}

		public static ModuleNotification DeserializeLengthDelimited(Stream stream, ModuleNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ModuleNotification Deserialize(Stream stream, ModuleNotification instance, long limit)
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
				case 16:
					instance.ModuleId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.Result = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, ModuleNotification instance)
		{
			if (instance.HasModuleId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ModuleId);
			}
			if (instance.HasResult)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.Result);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasModuleId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ModuleId);
			}
			if (HasResult)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Result);
			}
			return num;
		}
	}
}
