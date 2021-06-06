using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	public class SetOptions : IProtoBuf
	{
		public enum PacketID
		{
			ID = 239,
			System = 0
		}

		public enum MaxOptionCount
		{
			LIMIT = 51
		}

		private List<ClientOption> _Options = new List<ClientOption>();

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

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (ClientOption option in Options)
			{
				num ^= option.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SetOptions setOptions = obj as SetOptions;
			if (setOptions == null)
			{
				return false;
			}
			if (Options.Count != setOptions.Options.Count)
			{
				return false;
			}
			for (int i = 0; i < Options.Count; i++)
			{
				if (!Options[i].Equals(setOptions.Options[i]))
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

		public static SetOptions Deserialize(Stream stream, SetOptions instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SetOptions DeserializeLengthDelimited(Stream stream)
		{
			SetOptions setOptions = new SetOptions();
			DeserializeLengthDelimited(stream, setOptions);
			return setOptions;
		}

		public static SetOptions DeserializeLengthDelimited(Stream stream, SetOptions instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SetOptions Deserialize(Stream stream, SetOptions instance, long limit)
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

		public static void Serialize(Stream stream, SetOptions instance)
		{
			if (instance.Options.Count <= 0)
			{
				return;
			}
			foreach (ClientOption option in instance.Options)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, option.GetSerializedSize());
				ClientOption.Serialize(stream, option);
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
				return num;
			}
			return num;
		}
	}
}
