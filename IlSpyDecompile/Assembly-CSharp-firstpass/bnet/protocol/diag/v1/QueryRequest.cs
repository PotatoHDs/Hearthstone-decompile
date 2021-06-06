using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.diag.v1
{
	public class QueryRequest : IProtoBuf
	{
		private List<string> _Args = new List<string>();

		public string Name { get; set; }

		public List<string> Args
		{
			get
			{
				return _Args;
			}
			set
			{
				_Args = value;
			}
		}

		public List<string> ArgsList => _Args;

		public int ArgsCount => _Args.Count;

		public bool IsInitialized => true;

		public void SetName(string val)
		{
			Name = val;
		}

		public void AddArgs(string val)
		{
			_Args.Add(val);
		}

		public void ClearArgs()
		{
			_Args.Clear();
		}

		public void SetArgs(List<string> val)
		{
			Args = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Name.GetHashCode();
			foreach (string arg in Args)
			{
				hashCode ^= arg.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			QueryRequest queryRequest = obj as QueryRequest;
			if (queryRequest == null)
			{
				return false;
			}
			if (!Name.Equals(queryRequest.Name))
			{
				return false;
			}
			if (Args.Count != queryRequest.Args.Count)
			{
				return false;
			}
			for (int i = 0; i < Args.Count; i++)
			{
				if (!Args[i].Equals(queryRequest.Args[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static QueryRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<QueryRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static QueryRequest Deserialize(Stream stream, QueryRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static QueryRequest DeserializeLengthDelimited(Stream stream)
		{
			QueryRequest queryRequest = new QueryRequest();
			DeserializeLengthDelimited(stream, queryRequest);
			return queryRequest;
		}

		public static QueryRequest DeserializeLengthDelimited(Stream stream, QueryRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static QueryRequest Deserialize(Stream stream, QueryRequest instance, long limit)
		{
			if (instance.Args == null)
			{
				instance.Args = new List<string>();
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
					instance.Args.Add(ProtocolParser.ReadString(stream));
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

		public static void Serialize(Stream stream, QueryRequest instance)
		{
			if (instance.Name == null)
			{
				throw new ArgumentNullException("Name", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			if (instance.Args.Count <= 0)
			{
				return;
			}
			foreach (string arg in instance.Args)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(arg));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(Name);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (Args.Count > 0)
			{
				foreach (string arg in Args)
				{
					num++;
					uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(arg);
					num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
				}
			}
			return num + 1;
		}
	}
}
