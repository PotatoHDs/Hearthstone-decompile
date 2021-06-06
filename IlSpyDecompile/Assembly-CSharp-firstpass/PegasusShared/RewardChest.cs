using System.Collections.Generic;
using System.IO;

namespace PegasusShared
{
	public class RewardChest : IProtoBuf
	{
		public bool HasBag1;

		private RewardBag _Bag1;

		public bool HasBag2;

		private RewardBag _Bag2;

		public bool HasBag3;

		private RewardBag _Bag3;

		public bool HasBag4;

		private RewardBag _Bag4;

		public bool HasBag5;

		private RewardBag _Bag5;

		private List<RewardBag> _Bag = new List<RewardBag>();

		public RewardBag Bag1
		{
			get
			{
				return _Bag1;
			}
			set
			{
				_Bag1 = value;
				HasBag1 = value != null;
			}
		}

		public RewardBag Bag2
		{
			get
			{
				return _Bag2;
			}
			set
			{
				_Bag2 = value;
				HasBag2 = value != null;
			}
		}

		public RewardBag Bag3
		{
			get
			{
				return _Bag3;
			}
			set
			{
				_Bag3 = value;
				HasBag3 = value != null;
			}
		}

		public RewardBag Bag4
		{
			get
			{
				return _Bag4;
			}
			set
			{
				_Bag4 = value;
				HasBag4 = value != null;
			}
		}

		public RewardBag Bag5
		{
			get
			{
				return _Bag5;
			}
			set
			{
				_Bag5 = value;
				HasBag5 = value != null;
			}
		}

		public List<RewardBag> Bag
		{
			get
			{
				return _Bag;
			}
			set
			{
				_Bag = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasBag1)
			{
				num ^= Bag1.GetHashCode();
			}
			if (HasBag2)
			{
				num ^= Bag2.GetHashCode();
			}
			if (HasBag3)
			{
				num ^= Bag3.GetHashCode();
			}
			if (HasBag4)
			{
				num ^= Bag4.GetHashCode();
			}
			if (HasBag5)
			{
				num ^= Bag5.GetHashCode();
			}
			foreach (RewardBag item in Bag)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RewardChest rewardChest = obj as RewardChest;
			if (rewardChest == null)
			{
				return false;
			}
			if (HasBag1 != rewardChest.HasBag1 || (HasBag1 && !Bag1.Equals(rewardChest.Bag1)))
			{
				return false;
			}
			if (HasBag2 != rewardChest.HasBag2 || (HasBag2 && !Bag2.Equals(rewardChest.Bag2)))
			{
				return false;
			}
			if (HasBag3 != rewardChest.HasBag3 || (HasBag3 && !Bag3.Equals(rewardChest.Bag3)))
			{
				return false;
			}
			if (HasBag4 != rewardChest.HasBag4 || (HasBag4 && !Bag4.Equals(rewardChest.Bag4)))
			{
				return false;
			}
			if (HasBag5 != rewardChest.HasBag5 || (HasBag5 && !Bag5.Equals(rewardChest.Bag5)))
			{
				return false;
			}
			if (Bag.Count != rewardChest.Bag.Count)
			{
				return false;
			}
			for (int i = 0; i < Bag.Count; i++)
			{
				if (!Bag[i].Equals(rewardChest.Bag[i]))
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

		public static RewardChest Deserialize(Stream stream, RewardChest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RewardChest DeserializeLengthDelimited(Stream stream)
		{
			RewardChest rewardChest = new RewardChest();
			DeserializeLengthDelimited(stream, rewardChest);
			return rewardChest;
		}

		public static RewardChest DeserializeLengthDelimited(Stream stream, RewardChest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RewardChest Deserialize(Stream stream, RewardChest instance, long limit)
		{
			if (instance.Bag == null)
			{
				instance.Bag = new List<RewardBag>();
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
					if (instance.Bag1 == null)
					{
						instance.Bag1 = RewardBag.DeserializeLengthDelimited(stream);
					}
					else
					{
						RewardBag.DeserializeLengthDelimited(stream, instance.Bag1);
					}
					continue;
				case 18:
					if (instance.Bag2 == null)
					{
						instance.Bag2 = RewardBag.DeserializeLengthDelimited(stream);
					}
					else
					{
						RewardBag.DeserializeLengthDelimited(stream, instance.Bag2);
					}
					continue;
				case 26:
					if (instance.Bag3 == null)
					{
						instance.Bag3 = RewardBag.DeserializeLengthDelimited(stream);
					}
					else
					{
						RewardBag.DeserializeLengthDelimited(stream, instance.Bag3);
					}
					continue;
				case 34:
					if (instance.Bag4 == null)
					{
						instance.Bag4 = RewardBag.DeserializeLengthDelimited(stream);
					}
					else
					{
						RewardBag.DeserializeLengthDelimited(stream, instance.Bag4);
					}
					continue;
				case 42:
					if (instance.Bag5 == null)
					{
						instance.Bag5 = RewardBag.DeserializeLengthDelimited(stream);
					}
					else
					{
						RewardBag.DeserializeLengthDelimited(stream, instance.Bag5);
					}
					continue;
				case 50:
					instance.Bag.Add(RewardBag.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, RewardChest instance)
		{
			if (instance.HasBag1)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Bag1.GetSerializedSize());
				RewardBag.Serialize(stream, instance.Bag1);
			}
			if (instance.HasBag2)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Bag2.GetSerializedSize());
				RewardBag.Serialize(stream, instance.Bag2);
			}
			if (instance.HasBag3)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Bag3.GetSerializedSize());
				RewardBag.Serialize(stream, instance.Bag3);
			}
			if (instance.HasBag4)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.Bag4.GetSerializedSize());
				RewardBag.Serialize(stream, instance.Bag4);
			}
			if (instance.HasBag5)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.Bag5.GetSerializedSize());
				RewardBag.Serialize(stream, instance.Bag5);
			}
			if (instance.Bag.Count <= 0)
			{
				return;
			}
			foreach (RewardBag item in instance.Bag)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				RewardBag.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasBag1)
			{
				num++;
				uint serializedSize = Bag1.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasBag2)
			{
				num++;
				uint serializedSize2 = Bag2.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasBag3)
			{
				num++;
				uint serializedSize3 = Bag3.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (HasBag4)
			{
				num++;
				uint serializedSize4 = Bag4.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (HasBag5)
			{
				num++;
				uint serializedSize5 = Bag5.GetSerializedSize();
				num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
			}
			if (Bag.Count > 0)
			{
				foreach (RewardBag item in Bag)
				{
					num++;
					uint serializedSize6 = item.GetSerializedSize();
					num += serializedSize6 + ProtocolParser.SizeOfUInt32(serializedSize6);
				}
				return num;
			}
			return num;
		}
	}
}
