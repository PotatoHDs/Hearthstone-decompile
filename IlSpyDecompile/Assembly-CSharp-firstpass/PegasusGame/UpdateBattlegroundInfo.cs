using System.IO;
using System.Text;

namespace PegasusGame
{
	public class UpdateBattlegroundInfo : IProtoBuf
	{
		public enum PacketID
		{
			ID = 53
		}

		public bool HasBattlegroundDenyList;

		private string _BattlegroundDenyList;

		public bool HasBattlegroundMinionPool;

		private string _BattlegroundMinionPool;

		public string BattlegroundDenyList
		{
			get
			{
				return _BattlegroundDenyList;
			}
			set
			{
				_BattlegroundDenyList = value;
				HasBattlegroundDenyList = value != null;
			}
		}

		public string BattlegroundMinionPool
		{
			get
			{
				return _BattlegroundMinionPool;
			}
			set
			{
				_BattlegroundMinionPool = value;
				HasBattlegroundMinionPool = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasBattlegroundDenyList)
			{
				num ^= BattlegroundDenyList.GetHashCode();
			}
			if (HasBattlegroundMinionPool)
			{
				num ^= BattlegroundMinionPool.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			UpdateBattlegroundInfo updateBattlegroundInfo = obj as UpdateBattlegroundInfo;
			if (updateBattlegroundInfo == null)
			{
				return false;
			}
			if (HasBattlegroundDenyList != updateBattlegroundInfo.HasBattlegroundDenyList || (HasBattlegroundDenyList && !BattlegroundDenyList.Equals(updateBattlegroundInfo.BattlegroundDenyList)))
			{
				return false;
			}
			if (HasBattlegroundMinionPool != updateBattlegroundInfo.HasBattlegroundMinionPool || (HasBattlegroundMinionPool && !BattlegroundMinionPool.Equals(updateBattlegroundInfo.BattlegroundMinionPool)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static UpdateBattlegroundInfo Deserialize(Stream stream, UpdateBattlegroundInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static UpdateBattlegroundInfo DeserializeLengthDelimited(Stream stream)
		{
			UpdateBattlegroundInfo updateBattlegroundInfo = new UpdateBattlegroundInfo();
			DeserializeLengthDelimited(stream, updateBattlegroundInfo);
			return updateBattlegroundInfo;
		}

		public static UpdateBattlegroundInfo DeserializeLengthDelimited(Stream stream, UpdateBattlegroundInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static UpdateBattlegroundInfo Deserialize(Stream stream, UpdateBattlegroundInfo instance, long limit)
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
					instance.BattlegroundDenyList = ProtocolParser.ReadString(stream);
					continue;
				case 18:
					instance.BattlegroundMinionPool = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, UpdateBattlegroundInfo instance)
		{
			if (instance.HasBattlegroundDenyList)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BattlegroundDenyList));
			}
			if (instance.HasBattlegroundMinionPool)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BattlegroundMinionPool));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasBattlegroundDenyList)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(BattlegroundDenyList);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasBattlegroundMinionPool)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(BattlegroundMinionPool);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}
	}
}
