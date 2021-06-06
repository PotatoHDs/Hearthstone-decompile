using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BobNetProto
{
	public class DebugConsoleCmdList : IProtoBuf
	{
		public enum PacketID
		{
			ID = 146
		}

		public class DebugConsoleCmd : IProtoBuf
		{
			public class DebugConsoleCmdParam : IProtoBuf
			{
				public string ParamType { get; set; }

				public string ParamName { get; set; }

				public override int GetHashCode()
				{
					return GetType().GetHashCode() ^ ParamType.GetHashCode() ^ ParamName.GetHashCode();
				}

				public override bool Equals(object obj)
				{
					DebugConsoleCmdParam debugConsoleCmdParam = obj as DebugConsoleCmdParam;
					if (debugConsoleCmdParam == null)
					{
						return false;
					}
					if (!ParamType.Equals(debugConsoleCmdParam.ParamType))
					{
						return false;
					}
					if (!ParamName.Equals(debugConsoleCmdParam.ParamName))
					{
						return false;
					}
					return true;
				}

				public void Deserialize(Stream stream)
				{
					Deserialize(stream, this);
				}

				public static DebugConsoleCmdParam Deserialize(Stream stream, DebugConsoleCmdParam instance)
				{
					return Deserialize(stream, instance, -1L);
				}

				public static DebugConsoleCmdParam DeserializeLengthDelimited(Stream stream)
				{
					DebugConsoleCmdParam debugConsoleCmdParam = new DebugConsoleCmdParam();
					DeserializeLengthDelimited(stream, debugConsoleCmdParam);
					return debugConsoleCmdParam;
				}

				public static DebugConsoleCmdParam DeserializeLengthDelimited(Stream stream, DebugConsoleCmdParam instance)
				{
					long num = ProtocolParser.ReadUInt32(stream);
					num += stream.Position;
					return Deserialize(stream, instance, num);
				}

				public static DebugConsoleCmdParam Deserialize(Stream stream, DebugConsoleCmdParam instance, long limit)
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
							instance.ParamType = ProtocolParser.ReadString(stream);
							continue;
						case 18:
							instance.ParamName = ProtocolParser.ReadString(stream);
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

				public static void Serialize(Stream stream, DebugConsoleCmdParam instance)
				{
					if (instance.ParamType == null)
					{
						throw new ArgumentNullException("ParamType", "Required by proto specification.");
					}
					stream.WriteByte(10);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ParamType));
					if (instance.ParamName == null)
					{
						throw new ArgumentNullException("ParamName", "Required by proto specification.");
					}
					stream.WriteByte(18);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ParamName));
				}

				public uint GetSerializedSize()
				{
					uint byteCount = (uint)Encoding.UTF8.GetByteCount(ParamType);
					uint num = 0 + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount);
					uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(ParamName);
					return num + (ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2) + 2;
				}
			}

			private List<DebugConsoleCmdParam> _Params = new List<DebugConsoleCmdParam>();

			public string Name { get; set; }

			public List<DebugConsoleCmdParam> Params
			{
				get
				{
					return _Params;
				}
				set
				{
					_Params = value;
				}
			}

			public override int GetHashCode()
			{
				int hashCode = GetType().GetHashCode();
				hashCode ^= Name.GetHashCode();
				foreach (DebugConsoleCmdParam param in Params)
				{
					hashCode ^= param.GetHashCode();
				}
				return hashCode;
			}

			public override bool Equals(object obj)
			{
				DebugConsoleCmd debugConsoleCmd = obj as DebugConsoleCmd;
				if (debugConsoleCmd == null)
				{
					return false;
				}
				if (!Name.Equals(debugConsoleCmd.Name))
				{
					return false;
				}
				if (Params.Count != debugConsoleCmd.Params.Count)
				{
					return false;
				}
				for (int i = 0; i < Params.Count; i++)
				{
					if (!Params[i].Equals(debugConsoleCmd.Params[i]))
					{
						return false;
					}
				}
				return true;
			}

			public void Deserialize(Stream stream)
			{
				Deserialize(stream, this);
			}

			public static DebugConsoleCmd Deserialize(Stream stream, DebugConsoleCmd instance)
			{
				return Deserialize(stream, instance, -1L);
			}

			public static DebugConsoleCmd DeserializeLengthDelimited(Stream stream)
			{
				DebugConsoleCmd debugConsoleCmd = new DebugConsoleCmd();
				DeserializeLengthDelimited(stream, debugConsoleCmd);
				return debugConsoleCmd;
			}

			public static DebugConsoleCmd DeserializeLengthDelimited(Stream stream, DebugConsoleCmd instance)
			{
				long num = ProtocolParser.ReadUInt32(stream);
				num += stream.Position;
				return Deserialize(stream, instance, num);
			}

			public static DebugConsoleCmd Deserialize(Stream stream, DebugConsoleCmd instance, long limit)
			{
				if (instance.Params == null)
				{
					instance.Params = new List<DebugConsoleCmdParam>();
				}
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
						instance.Name = ProtocolParser.ReadString(stream);
						continue;
					case 18:
						instance.Params.Add(DebugConsoleCmdParam.DeserializeLengthDelimited(stream));
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

			public static void Serialize(Stream stream, DebugConsoleCmd instance)
			{
				if (instance.Name == null)
				{
					throw new ArgumentNullException("Name", "Required by proto specification.");
				}
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
				if (instance.Params.Count <= 0)
				{
					return;
				}
				foreach (DebugConsoleCmdParam param in instance.Params)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, param.GetSerializedSize());
					DebugConsoleCmdParam.Serialize(stream, param);
				}
			}

			public uint GetSerializedSize()
			{
				uint num = 0u;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
				if (Params.Count > 0)
				{
					foreach (DebugConsoleCmdParam param in Params)
					{
						num++;
						uint serializedSize = param.GetSerializedSize();
						num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
					}
				}
				return num + 1;
			}
		}

		private List<DebugConsoleCmd> _Commands = new List<DebugConsoleCmd>();

		public List<DebugConsoleCmd> Commands
		{
			get
			{
				return _Commands;
			}
			set
			{
				_Commands = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (DebugConsoleCmd command in Commands)
			{
				num ^= command.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			DebugConsoleCmdList debugConsoleCmdList = obj as DebugConsoleCmdList;
			if (debugConsoleCmdList == null)
			{
				return false;
			}
			if (Commands.Count != debugConsoleCmdList.Commands.Count)
			{
				return false;
			}
			for (int i = 0; i < Commands.Count; i++)
			{
				if (!Commands[i].Equals(debugConsoleCmdList.Commands[i]))
				{
					return false;
				}
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DebugConsoleCmdList Deserialize(Stream stream, DebugConsoleCmdList instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DebugConsoleCmdList DeserializeLengthDelimited(Stream stream)
		{
			DebugConsoleCmdList debugConsoleCmdList = new DebugConsoleCmdList();
			DeserializeLengthDelimited(stream, debugConsoleCmdList);
			return debugConsoleCmdList;
		}

		public static DebugConsoleCmdList DeserializeLengthDelimited(Stream stream, DebugConsoleCmdList instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DebugConsoleCmdList Deserialize(Stream stream, DebugConsoleCmdList instance, long limit)
		{
			if (instance.Commands == null)
			{
				instance.Commands = new List<DebugConsoleCmd>();
			}
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
					instance.Commands.Add(DebugConsoleCmd.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, DebugConsoleCmdList instance)
		{
			if (instance.Commands.Count <= 0)
			{
				return;
			}
			foreach (DebugConsoleCmd command in instance.Commands)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, command.GetSerializedSize());
				DebugConsoleCmd.Serialize(stream, command);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Commands.Count > 0)
			{
				foreach (DebugConsoleCmd command in Commands)
				{
					num++;
					uint serializedSize = command.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
				return num;
			}
			return num;
		}
	}
}
