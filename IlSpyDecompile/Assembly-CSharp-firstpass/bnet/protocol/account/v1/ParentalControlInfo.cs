using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.account.v1
{
	public class ParentalControlInfo : IProtoBuf
	{
		public bool HasTimezone;

		private string _Timezone;

		public bool HasMinutesPerDay;

		private uint _MinutesPerDay;

		public bool HasMinutesPerWeek;

		private uint _MinutesPerWeek;

		public bool HasCanReceiveVoice;

		private bool _CanReceiveVoice;

		public bool HasCanSendVoice;

		private bool _CanSendVoice;

		private List<bool> _PlaySchedule = new List<bool>();

		public bool HasCanJoinGroup;

		private bool _CanJoinGroup;

		public bool HasCanUseProfile;

		private bool _CanUseProfile;

		public string Timezone
		{
			get
			{
				return _Timezone;
			}
			set
			{
				_Timezone = value;
				HasTimezone = value != null;
			}
		}

		public uint MinutesPerDay
		{
			get
			{
				return _MinutesPerDay;
			}
			set
			{
				_MinutesPerDay = value;
				HasMinutesPerDay = true;
			}
		}

		public uint MinutesPerWeek
		{
			get
			{
				return _MinutesPerWeek;
			}
			set
			{
				_MinutesPerWeek = value;
				HasMinutesPerWeek = true;
			}
		}

		public bool CanReceiveVoice
		{
			get
			{
				return _CanReceiveVoice;
			}
			set
			{
				_CanReceiveVoice = value;
				HasCanReceiveVoice = true;
			}
		}

		public bool CanSendVoice
		{
			get
			{
				return _CanSendVoice;
			}
			set
			{
				_CanSendVoice = value;
				HasCanSendVoice = true;
			}
		}

		public List<bool> PlaySchedule
		{
			get
			{
				return _PlaySchedule;
			}
			set
			{
				_PlaySchedule = value;
			}
		}

		public List<bool> PlayScheduleList => _PlaySchedule;

		public int PlayScheduleCount => _PlaySchedule.Count;

		public bool CanJoinGroup
		{
			get
			{
				return _CanJoinGroup;
			}
			set
			{
				_CanJoinGroup = value;
				HasCanJoinGroup = true;
			}
		}

		public bool CanUseProfile
		{
			get
			{
				return _CanUseProfile;
			}
			set
			{
				_CanUseProfile = value;
				HasCanUseProfile = true;
			}
		}

		public bool IsInitialized => true;

		public void SetTimezone(string val)
		{
			Timezone = val;
		}

		public void SetMinutesPerDay(uint val)
		{
			MinutesPerDay = val;
		}

		public void SetMinutesPerWeek(uint val)
		{
			MinutesPerWeek = val;
		}

		public void SetCanReceiveVoice(bool val)
		{
			CanReceiveVoice = val;
		}

		public void SetCanSendVoice(bool val)
		{
			CanSendVoice = val;
		}

		public void AddPlaySchedule(bool val)
		{
			_PlaySchedule.Add(val);
		}

		public void ClearPlaySchedule()
		{
			_PlaySchedule.Clear();
		}

		public void SetPlaySchedule(List<bool> val)
		{
			PlaySchedule = val;
		}

		public void SetCanJoinGroup(bool val)
		{
			CanJoinGroup = val;
		}

		public void SetCanUseProfile(bool val)
		{
			CanUseProfile = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasTimezone)
			{
				num ^= Timezone.GetHashCode();
			}
			if (HasMinutesPerDay)
			{
				num ^= MinutesPerDay.GetHashCode();
			}
			if (HasMinutesPerWeek)
			{
				num ^= MinutesPerWeek.GetHashCode();
			}
			if (HasCanReceiveVoice)
			{
				num ^= CanReceiveVoice.GetHashCode();
			}
			if (HasCanSendVoice)
			{
				num ^= CanSendVoice.GetHashCode();
			}
			foreach (bool item in PlaySchedule)
			{
				num ^= item.GetHashCode();
			}
			if (HasCanJoinGroup)
			{
				num ^= CanJoinGroup.GetHashCode();
			}
			if (HasCanUseProfile)
			{
				num ^= CanUseProfile.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ParentalControlInfo parentalControlInfo = obj as ParentalControlInfo;
			if (parentalControlInfo == null)
			{
				return false;
			}
			if (HasTimezone != parentalControlInfo.HasTimezone || (HasTimezone && !Timezone.Equals(parentalControlInfo.Timezone)))
			{
				return false;
			}
			if (HasMinutesPerDay != parentalControlInfo.HasMinutesPerDay || (HasMinutesPerDay && !MinutesPerDay.Equals(parentalControlInfo.MinutesPerDay)))
			{
				return false;
			}
			if (HasMinutesPerWeek != parentalControlInfo.HasMinutesPerWeek || (HasMinutesPerWeek && !MinutesPerWeek.Equals(parentalControlInfo.MinutesPerWeek)))
			{
				return false;
			}
			if (HasCanReceiveVoice != parentalControlInfo.HasCanReceiveVoice || (HasCanReceiveVoice && !CanReceiveVoice.Equals(parentalControlInfo.CanReceiveVoice)))
			{
				return false;
			}
			if (HasCanSendVoice != parentalControlInfo.HasCanSendVoice || (HasCanSendVoice && !CanSendVoice.Equals(parentalControlInfo.CanSendVoice)))
			{
				return false;
			}
			if (PlaySchedule.Count != parentalControlInfo.PlaySchedule.Count)
			{
				return false;
			}
			for (int i = 0; i < PlaySchedule.Count; i++)
			{
				if (!PlaySchedule[i].Equals(parentalControlInfo.PlaySchedule[i]))
				{
					return false;
				}
			}
			if (HasCanJoinGroup != parentalControlInfo.HasCanJoinGroup || (HasCanJoinGroup && !CanJoinGroup.Equals(parentalControlInfo.CanJoinGroup)))
			{
				return false;
			}
			if (HasCanUseProfile != parentalControlInfo.HasCanUseProfile || (HasCanUseProfile && !CanUseProfile.Equals(parentalControlInfo.CanUseProfile)))
			{
				return false;
			}
			return true;
		}

		public static ParentalControlInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ParentalControlInfo>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ParentalControlInfo Deserialize(Stream stream, ParentalControlInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ParentalControlInfo DeserializeLengthDelimited(Stream stream)
		{
			ParentalControlInfo parentalControlInfo = new ParentalControlInfo();
			DeserializeLengthDelimited(stream, parentalControlInfo);
			return parentalControlInfo;
		}

		public static ParentalControlInfo DeserializeLengthDelimited(Stream stream, ParentalControlInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ParentalControlInfo Deserialize(Stream stream, ParentalControlInfo instance, long limit)
		{
			if (instance.PlaySchedule == null)
			{
				instance.PlaySchedule = new List<bool>();
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
				case 26:
					instance.Timezone = ProtocolParser.ReadString(stream);
					continue;
				case 32:
					instance.MinutesPerDay = ProtocolParser.ReadUInt32(stream);
					continue;
				case 40:
					instance.MinutesPerWeek = ProtocolParser.ReadUInt32(stream);
					continue;
				case 48:
					instance.CanReceiveVoice = ProtocolParser.ReadBool(stream);
					continue;
				case 56:
					instance.CanSendVoice = ProtocolParser.ReadBool(stream);
					continue;
				case 64:
					instance.PlaySchedule.Add(ProtocolParser.ReadBool(stream));
					continue;
				case 72:
					instance.CanJoinGroup = ProtocolParser.ReadBool(stream);
					continue;
				case 80:
					instance.CanUseProfile = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, ParentalControlInfo instance)
		{
			if (instance.HasTimezone)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Timezone));
			}
			if (instance.HasMinutesPerDay)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt32(stream, instance.MinutesPerDay);
			}
			if (instance.HasMinutesPerWeek)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt32(stream, instance.MinutesPerWeek);
			}
			if (instance.HasCanReceiveVoice)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.CanReceiveVoice);
			}
			if (instance.HasCanSendVoice)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.CanSendVoice);
			}
			if (instance.PlaySchedule.Count > 0)
			{
				foreach (bool item in instance.PlaySchedule)
				{
					stream.WriteByte(64);
					ProtocolParser.WriteBool(stream, item);
				}
			}
			if (instance.HasCanJoinGroup)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteBool(stream, instance.CanJoinGroup);
			}
			if (instance.HasCanUseProfile)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteBool(stream, instance.CanUseProfile);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasTimezone)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Timezone);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasMinutesPerDay)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(MinutesPerDay);
			}
			if (HasMinutesPerWeek)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(MinutesPerWeek);
			}
			if (HasCanReceiveVoice)
			{
				num++;
				num++;
			}
			if (HasCanSendVoice)
			{
				num++;
				num++;
			}
			if (PlaySchedule.Count > 0)
			{
				foreach (bool item in PlaySchedule)
				{
					_ = item;
					num++;
					num++;
				}
			}
			if (HasCanJoinGroup)
			{
				num++;
				num++;
			}
			if (HasCanUseProfile)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
