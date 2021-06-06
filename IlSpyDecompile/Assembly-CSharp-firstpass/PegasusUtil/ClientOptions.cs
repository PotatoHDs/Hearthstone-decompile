using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	public class ClientOptions : IProtoBuf
	{
		private List<ClientOption> _Options = new List<ClientOption>();

		public bool HasFailed;

		private bool _Failed;

		public List<ClientOption> Options
		{
			get
			{
				return _Options;
			}
			set
			{
				_Options = value;
			}
		}

		public bool Failed
		{
			get
			{
				return _Failed;
			}
			set
			{
				_Failed = value;
				HasFailed = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (ClientOption option in Options)
			{
				num ^= option.GetHashCode();
			}
			if (HasFailed)
			{
				num ^= Failed.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ClientOptions clientOptions = obj as ClientOptions;
			if (clientOptions == null)
			{
				return false;
			}
			if (Options.Count != clientOptions.Options.Count)
			{
				return false;
			}
			for (int i = 0; i < Options.Count; i++)
			{
				if (!Options[i].Equals(clientOptions.Options[i]))
				{
					return false;
				}
			}
			if (HasFailed != clientOptions.HasFailed || (HasFailed && !Failed.Equals(clientOptions.Failed)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ClientOptions Deserialize(Stream stream, ClientOptions instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ClientOptions DeserializeLengthDelimited(Stream stream)
		{
			ClientOptions clientOptions = new ClientOptions();
			DeserializeLengthDelimited(stream, clientOptions);
			return clientOptions;
		}

		public static ClientOptions DeserializeLengthDelimited(Stream stream, ClientOptions instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ClientOptions Deserialize(Stream stream, ClientOptions instance, long limit)
		{
			if (instance.Options == null)
			{
				instance.Options = new List<ClientOption>();
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
					instance.Options.Add(ClientOption.DeserializeLengthDelimited(stream));
					continue;
				case 16:
					instance.Failed = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, ClientOptions instance)
		{
			if (instance.Options.Count > 0)
			{
				foreach (ClientOption option in instance.Options)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, option.GetSerializedSize());
					ClientOption.Serialize(stream, option);
				}
			}
			if (instance.HasFailed)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.Failed);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Options.Count > 0)
			{
				foreach (ClientOption option in Options)
				{
					num++;
					uint serializedSize = option.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasFailed)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
