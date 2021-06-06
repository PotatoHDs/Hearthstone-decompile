using System.Collections.Generic;
using System.IO;
using bnet.protocol.v2;

namespace bnet.protocol.game_utilities.v2.client
{
	public class ProcessTaskResponse : IProtoBuf
	{
		private List<bnet.protocol.v2.Attribute> _Result = new List<bnet.protocol.v2.Attribute>();

		public List<bnet.protocol.v2.Attribute> Result
		{
			get
			{
				return _Result;
			}
			set
			{
				_Result = value;
			}
		}

		public List<bnet.protocol.v2.Attribute> ResultList => _Result;

		public int ResultCount => _Result.Count;

		public bool IsInitialized => true;

		public void AddResult(bnet.protocol.v2.Attribute val)
		{
			_Result.Add(val);
		}

		public void ClearResult()
		{
			_Result.Clear();
		}

		public void SetResult(List<bnet.protocol.v2.Attribute> val)
		{
			Result = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (bnet.protocol.v2.Attribute item in Result)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ProcessTaskResponse processTaskResponse = obj as ProcessTaskResponse;
			if (processTaskResponse == null)
			{
				return false;
			}
			if (Result.Count != processTaskResponse.Result.Count)
			{
				return false;
			}
			for (int i = 0; i < Result.Count; i++)
			{
				if (!Result[i].Equals(processTaskResponse.Result[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static ProcessTaskResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ProcessTaskResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ProcessTaskResponse Deserialize(Stream stream, ProcessTaskResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ProcessTaskResponse DeserializeLengthDelimited(Stream stream)
		{
			ProcessTaskResponse processTaskResponse = new ProcessTaskResponse();
			DeserializeLengthDelimited(stream, processTaskResponse);
			return processTaskResponse;
		}

		public static ProcessTaskResponse DeserializeLengthDelimited(Stream stream, ProcessTaskResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ProcessTaskResponse Deserialize(Stream stream, ProcessTaskResponse instance, long limit)
		{
			if (instance.Result == null)
			{
				instance.Result = new List<bnet.protocol.v2.Attribute>();
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
					instance.Result.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, ProcessTaskResponse instance)
		{
			if (instance.Result.Count <= 0)
			{
				return;
			}
			foreach (bnet.protocol.v2.Attribute item in instance.Result)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				bnet.protocol.v2.Attribute.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Result.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute item in Result)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
				return num;
			}
			return num;
		}
	}
}
