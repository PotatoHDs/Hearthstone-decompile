using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.account.v1
{
	public class GameAccountList : IProtoBuf
	{
		public bool HasRegion;

		private uint _Region;

		private List<GameAccountHandle> _Handle = new List<GameAccountHandle>();

		public uint Region
		{
			get
			{
				return _Region;
			}
			set
			{
				_Region = value;
				HasRegion = true;
			}
		}

		public List<GameAccountHandle> Handle
		{
			get
			{
				return _Handle;
			}
			set
			{
				_Handle = value;
			}
		}

		public List<GameAccountHandle> HandleList => _Handle;

		public int HandleCount => _Handle.Count;

		public bool IsInitialized => true;

		public void SetRegion(uint val)
		{
			Region = val;
		}

		public void AddHandle(GameAccountHandle val)
		{
			_Handle.Add(val);
		}

		public void ClearHandle()
		{
			_Handle.Clear();
		}

		public void SetHandle(List<GameAccountHandle> val)
		{
			Handle = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasRegion)
			{
				num ^= Region.GetHashCode();
			}
			foreach (GameAccountHandle item in Handle)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameAccountList gameAccountList = obj as GameAccountList;
			if (gameAccountList == null)
			{
				return false;
			}
			if (HasRegion != gameAccountList.HasRegion || (HasRegion && !Region.Equals(gameAccountList.Region)))
			{
				return false;
			}
			if (Handle.Count != gameAccountList.Handle.Count)
			{
				return false;
			}
			for (int i = 0; i < Handle.Count; i++)
			{
				if (!Handle[i].Equals(gameAccountList.Handle[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static GameAccountList ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameAccountList>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameAccountList Deserialize(Stream stream, GameAccountList instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameAccountList DeserializeLengthDelimited(Stream stream)
		{
			GameAccountList gameAccountList = new GameAccountList();
			DeserializeLengthDelimited(stream, gameAccountList);
			return gameAccountList;
		}

		public static GameAccountList DeserializeLengthDelimited(Stream stream, GameAccountList instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameAccountList Deserialize(Stream stream, GameAccountList instance, long limit)
		{
			if (instance.Handle == null)
			{
				instance.Handle = new List<GameAccountHandle>();
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
				case 24:
					instance.Region = ProtocolParser.ReadUInt32(stream);
					continue;
				case 34:
					instance.Handle.Add(GameAccountHandle.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, GameAccountList instance)
		{
			if (instance.HasRegion)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.Region);
			}
			if (instance.Handle.Count <= 0)
			{
				return;
			}
			foreach (GameAccountHandle item in instance.Handle)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				GameAccountHandle.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasRegion)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Region);
			}
			if (Handle.Count > 0)
			{
				foreach (GameAccountHandle item in Handle)
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
