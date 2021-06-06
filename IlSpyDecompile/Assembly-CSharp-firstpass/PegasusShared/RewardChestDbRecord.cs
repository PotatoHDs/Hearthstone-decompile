using System.Collections.Generic;
using System.IO;

namespace PegasusShared
{
	public class RewardChestDbRecord : IProtoBuf
	{
		public bool HasShowToReturningPlayer;

		private bool _ShowToReturningPlayer;

		private List<LocalizedString> _Strings = new List<LocalizedString>();

		public int Id { get; set; }

		public bool ShowToReturningPlayer
		{
			get
			{
				return _ShowToReturningPlayer;
			}
			set
			{
				_ShowToReturningPlayer = value;
				HasShowToReturningPlayer = true;
			}
		}

		public List<LocalizedString> Strings
		{
			get
			{
				return _Strings;
			}
			set
			{
				_Strings = value;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Id.GetHashCode();
			if (HasShowToReturningPlayer)
			{
				hashCode ^= ShowToReturningPlayer.GetHashCode();
			}
			foreach (LocalizedString @string in Strings)
			{
				hashCode ^= @string.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			RewardChestDbRecord rewardChestDbRecord = obj as RewardChestDbRecord;
			if (rewardChestDbRecord == null)
			{
				return false;
			}
			if (!Id.Equals(rewardChestDbRecord.Id))
			{
				return false;
			}
			if (HasShowToReturningPlayer != rewardChestDbRecord.HasShowToReturningPlayer || (HasShowToReturningPlayer && !ShowToReturningPlayer.Equals(rewardChestDbRecord.ShowToReturningPlayer)))
			{
				return false;
			}
			if (Strings.Count != rewardChestDbRecord.Strings.Count)
			{
				return false;
			}
			for (int i = 0; i < Strings.Count; i++)
			{
				if (!Strings[i].Equals(rewardChestDbRecord.Strings[i]))
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

		public static RewardChestDbRecord Deserialize(Stream stream, RewardChestDbRecord instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RewardChestDbRecord DeserializeLengthDelimited(Stream stream)
		{
			RewardChestDbRecord rewardChestDbRecord = new RewardChestDbRecord();
			DeserializeLengthDelimited(stream, rewardChestDbRecord);
			return rewardChestDbRecord;
		}

		public static RewardChestDbRecord DeserializeLengthDelimited(Stream stream, RewardChestDbRecord instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RewardChestDbRecord Deserialize(Stream stream, RewardChestDbRecord instance, long limit)
		{
			if (instance.Strings == null)
			{
				instance.Strings = new List<LocalizedString>();
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
				case 8:
					instance.Id = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.ShowToReturningPlayer = ProtocolParser.ReadBool(stream);
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					switch (key.Field)
					{
					case 0u:
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					case 100u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.Strings.Add(LocalizedString.DeserializeLengthDelimited(stream));
						}
						break;
					default:
						ProtocolParser.SkipKey(stream, key);
						break;
					}
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

		public static void Serialize(Stream stream, RewardChestDbRecord instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Id);
			if (instance.HasShowToReturningPlayer)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.ShowToReturningPlayer);
			}
			if (instance.Strings.Count <= 0)
			{
				return;
			}
			foreach (LocalizedString @string in instance.Strings)
			{
				stream.WriteByte(162);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt32(stream, @string.GetSerializedSize());
				LocalizedString.Serialize(stream, @string);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Id);
			if (HasShowToReturningPlayer)
			{
				num++;
				num++;
			}
			if (Strings.Count > 0)
			{
				foreach (LocalizedString @string in Strings)
				{
					num += 2;
					uint serializedSize = @string.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num + 1;
		}
	}
}
