using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.account.v1
{
	// Token: 0x0200052F RID: 1327
	public class ParentalControlInfo : IProtoBuf
	{
		// Token: 0x17001205 RID: 4613
		// (get) Token: 0x06005F4A RID: 24394 RVA: 0x00120C03 File Offset: 0x0011EE03
		// (set) Token: 0x06005F4B RID: 24395 RVA: 0x00120C0B File Offset: 0x0011EE0B
		public string Timezone
		{
			get
			{
				return this._Timezone;
			}
			set
			{
				this._Timezone = value;
				this.HasTimezone = (value != null);
			}
		}

		// Token: 0x06005F4C RID: 24396 RVA: 0x00120C1E File Offset: 0x0011EE1E
		public void SetTimezone(string val)
		{
			this.Timezone = val;
		}

		// Token: 0x17001206 RID: 4614
		// (get) Token: 0x06005F4D RID: 24397 RVA: 0x00120C27 File Offset: 0x0011EE27
		// (set) Token: 0x06005F4E RID: 24398 RVA: 0x00120C2F File Offset: 0x0011EE2F
		public uint MinutesPerDay
		{
			get
			{
				return this._MinutesPerDay;
			}
			set
			{
				this._MinutesPerDay = value;
				this.HasMinutesPerDay = true;
			}
		}

		// Token: 0x06005F4F RID: 24399 RVA: 0x00120C3F File Offset: 0x0011EE3F
		public void SetMinutesPerDay(uint val)
		{
			this.MinutesPerDay = val;
		}

		// Token: 0x17001207 RID: 4615
		// (get) Token: 0x06005F50 RID: 24400 RVA: 0x00120C48 File Offset: 0x0011EE48
		// (set) Token: 0x06005F51 RID: 24401 RVA: 0x00120C50 File Offset: 0x0011EE50
		public uint MinutesPerWeek
		{
			get
			{
				return this._MinutesPerWeek;
			}
			set
			{
				this._MinutesPerWeek = value;
				this.HasMinutesPerWeek = true;
			}
		}

		// Token: 0x06005F52 RID: 24402 RVA: 0x00120C60 File Offset: 0x0011EE60
		public void SetMinutesPerWeek(uint val)
		{
			this.MinutesPerWeek = val;
		}

		// Token: 0x17001208 RID: 4616
		// (get) Token: 0x06005F53 RID: 24403 RVA: 0x00120C69 File Offset: 0x0011EE69
		// (set) Token: 0x06005F54 RID: 24404 RVA: 0x00120C71 File Offset: 0x0011EE71
		public bool CanReceiveVoice
		{
			get
			{
				return this._CanReceiveVoice;
			}
			set
			{
				this._CanReceiveVoice = value;
				this.HasCanReceiveVoice = true;
			}
		}

		// Token: 0x06005F55 RID: 24405 RVA: 0x00120C81 File Offset: 0x0011EE81
		public void SetCanReceiveVoice(bool val)
		{
			this.CanReceiveVoice = val;
		}

		// Token: 0x17001209 RID: 4617
		// (get) Token: 0x06005F56 RID: 24406 RVA: 0x00120C8A File Offset: 0x0011EE8A
		// (set) Token: 0x06005F57 RID: 24407 RVA: 0x00120C92 File Offset: 0x0011EE92
		public bool CanSendVoice
		{
			get
			{
				return this._CanSendVoice;
			}
			set
			{
				this._CanSendVoice = value;
				this.HasCanSendVoice = true;
			}
		}

		// Token: 0x06005F58 RID: 24408 RVA: 0x00120CA2 File Offset: 0x0011EEA2
		public void SetCanSendVoice(bool val)
		{
			this.CanSendVoice = val;
		}

		// Token: 0x1700120A RID: 4618
		// (get) Token: 0x06005F59 RID: 24409 RVA: 0x00120CAB File Offset: 0x0011EEAB
		// (set) Token: 0x06005F5A RID: 24410 RVA: 0x00120CB3 File Offset: 0x0011EEB3
		public List<bool> PlaySchedule
		{
			get
			{
				return this._PlaySchedule;
			}
			set
			{
				this._PlaySchedule = value;
			}
		}

		// Token: 0x1700120B RID: 4619
		// (get) Token: 0x06005F5B RID: 24411 RVA: 0x00120CAB File Offset: 0x0011EEAB
		public List<bool> PlayScheduleList
		{
			get
			{
				return this._PlaySchedule;
			}
		}

		// Token: 0x1700120C RID: 4620
		// (get) Token: 0x06005F5C RID: 24412 RVA: 0x00120CBC File Offset: 0x0011EEBC
		public int PlayScheduleCount
		{
			get
			{
				return this._PlaySchedule.Count;
			}
		}

		// Token: 0x06005F5D RID: 24413 RVA: 0x00120CC9 File Offset: 0x0011EEC9
		public void AddPlaySchedule(bool val)
		{
			this._PlaySchedule.Add(val);
		}

		// Token: 0x06005F5E RID: 24414 RVA: 0x00120CD7 File Offset: 0x0011EED7
		public void ClearPlaySchedule()
		{
			this._PlaySchedule.Clear();
		}

		// Token: 0x06005F5F RID: 24415 RVA: 0x00120CE4 File Offset: 0x0011EEE4
		public void SetPlaySchedule(List<bool> val)
		{
			this.PlaySchedule = val;
		}

		// Token: 0x1700120D RID: 4621
		// (get) Token: 0x06005F60 RID: 24416 RVA: 0x00120CED File Offset: 0x0011EEED
		// (set) Token: 0x06005F61 RID: 24417 RVA: 0x00120CF5 File Offset: 0x0011EEF5
		public bool CanJoinGroup
		{
			get
			{
				return this._CanJoinGroup;
			}
			set
			{
				this._CanJoinGroup = value;
				this.HasCanJoinGroup = true;
			}
		}

		// Token: 0x06005F62 RID: 24418 RVA: 0x00120D05 File Offset: 0x0011EF05
		public void SetCanJoinGroup(bool val)
		{
			this.CanJoinGroup = val;
		}

		// Token: 0x1700120E RID: 4622
		// (get) Token: 0x06005F63 RID: 24419 RVA: 0x00120D0E File Offset: 0x0011EF0E
		// (set) Token: 0x06005F64 RID: 24420 RVA: 0x00120D16 File Offset: 0x0011EF16
		public bool CanUseProfile
		{
			get
			{
				return this._CanUseProfile;
			}
			set
			{
				this._CanUseProfile = value;
				this.HasCanUseProfile = true;
			}
		}

		// Token: 0x06005F65 RID: 24421 RVA: 0x00120D26 File Offset: 0x0011EF26
		public void SetCanUseProfile(bool val)
		{
			this.CanUseProfile = val;
		}

		// Token: 0x06005F66 RID: 24422 RVA: 0x00120D30 File Offset: 0x0011EF30
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasTimezone)
			{
				num ^= this.Timezone.GetHashCode();
			}
			if (this.HasMinutesPerDay)
			{
				num ^= this.MinutesPerDay.GetHashCode();
			}
			if (this.HasMinutesPerWeek)
			{
				num ^= this.MinutesPerWeek.GetHashCode();
			}
			if (this.HasCanReceiveVoice)
			{
				num ^= this.CanReceiveVoice.GetHashCode();
			}
			if (this.HasCanSendVoice)
			{
				num ^= this.CanSendVoice.GetHashCode();
			}
			foreach (bool flag in this.PlaySchedule)
			{
				num ^= flag.GetHashCode();
			}
			if (this.HasCanJoinGroup)
			{
				num ^= this.CanJoinGroup.GetHashCode();
			}
			if (this.HasCanUseProfile)
			{
				num ^= this.CanUseProfile.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005F67 RID: 24423 RVA: 0x00120E40 File Offset: 0x0011F040
		public override bool Equals(object obj)
		{
			ParentalControlInfo parentalControlInfo = obj as ParentalControlInfo;
			if (parentalControlInfo == null)
			{
				return false;
			}
			if (this.HasTimezone != parentalControlInfo.HasTimezone || (this.HasTimezone && !this.Timezone.Equals(parentalControlInfo.Timezone)))
			{
				return false;
			}
			if (this.HasMinutesPerDay != parentalControlInfo.HasMinutesPerDay || (this.HasMinutesPerDay && !this.MinutesPerDay.Equals(parentalControlInfo.MinutesPerDay)))
			{
				return false;
			}
			if (this.HasMinutesPerWeek != parentalControlInfo.HasMinutesPerWeek || (this.HasMinutesPerWeek && !this.MinutesPerWeek.Equals(parentalControlInfo.MinutesPerWeek)))
			{
				return false;
			}
			if (this.HasCanReceiveVoice != parentalControlInfo.HasCanReceiveVoice || (this.HasCanReceiveVoice && !this.CanReceiveVoice.Equals(parentalControlInfo.CanReceiveVoice)))
			{
				return false;
			}
			if (this.HasCanSendVoice != parentalControlInfo.HasCanSendVoice || (this.HasCanSendVoice && !this.CanSendVoice.Equals(parentalControlInfo.CanSendVoice)))
			{
				return false;
			}
			if (this.PlaySchedule.Count != parentalControlInfo.PlaySchedule.Count)
			{
				return false;
			}
			for (int i = 0; i < this.PlaySchedule.Count; i++)
			{
				if (!this.PlaySchedule[i].Equals(parentalControlInfo.PlaySchedule[i]))
				{
					return false;
				}
			}
			return this.HasCanJoinGroup == parentalControlInfo.HasCanJoinGroup && (!this.HasCanJoinGroup || this.CanJoinGroup.Equals(parentalControlInfo.CanJoinGroup)) && this.HasCanUseProfile == parentalControlInfo.HasCanUseProfile && (!this.HasCanUseProfile || this.CanUseProfile.Equals(parentalControlInfo.CanUseProfile));
		}

		// Token: 0x1700120F RID: 4623
		// (get) Token: 0x06005F68 RID: 24424 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005F69 RID: 24425 RVA: 0x00120FED File Offset: 0x0011F1ED
		public static ParentalControlInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ParentalControlInfo>(bs, 0, -1);
		}

		// Token: 0x06005F6A RID: 24426 RVA: 0x00120FF7 File Offset: 0x0011F1F7
		public void Deserialize(Stream stream)
		{
			ParentalControlInfo.Deserialize(stream, this);
		}

		// Token: 0x06005F6B RID: 24427 RVA: 0x00121001 File Offset: 0x0011F201
		public static ParentalControlInfo Deserialize(Stream stream, ParentalControlInfo instance)
		{
			return ParentalControlInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005F6C RID: 24428 RVA: 0x0012100C File Offset: 0x0011F20C
		public static ParentalControlInfo DeserializeLengthDelimited(Stream stream)
		{
			ParentalControlInfo parentalControlInfo = new ParentalControlInfo();
			ParentalControlInfo.DeserializeLengthDelimited(stream, parentalControlInfo);
			return parentalControlInfo;
		}

		// Token: 0x06005F6D RID: 24429 RVA: 0x00121028 File Offset: 0x0011F228
		public static ParentalControlInfo DeserializeLengthDelimited(Stream stream, ParentalControlInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ParentalControlInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x06005F6E RID: 24430 RVA: 0x00121050 File Offset: 0x0011F250
		public static ParentalControlInfo Deserialize(Stream stream, ParentalControlInfo instance, long limit)
		{
			if (instance.PlaySchedule == null)
			{
				instance.PlaySchedule = new List<bool>();
			}
			while (limit < 0L || stream.Position < limit)
			{
				int num = stream.ReadByte();
				if (num == -1)
				{
					if (limit >= 0L)
					{
						throw new EndOfStreamException();
					}
					return instance;
				}
				else
				{
					if (num <= 48)
					{
						if (num <= 32)
						{
							if (num == 26)
							{
								instance.Timezone = ProtocolParser.ReadString(stream);
								continue;
							}
							if (num == 32)
							{
								instance.MinutesPerDay = ProtocolParser.ReadUInt32(stream);
								continue;
							}
						}
						else
						{
							if (num == 40)
							{
								instance.MinutesPerWeek = ProtocolParser.ReadUInt32(stream);
								continue;
							}
							if (num == 48)
							{
								instance.CanReceiveVoice = ProtocolParser.ReadBool(stream);
								continue;
							}
						}
					}
					else if (num <= 64)
					{
						if (num == 56)
						{
							instance.CanSendVoice = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 64)
						{
							instance.PlaySchedule.Add(ProtocolParser.ReadBool(stream));
							continue;
						}
					}
					else
					{
						if (num == 72)
						{
							instance.CanJoinGroup = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 80)
						{
							instance.CanUseProfile = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06005F6F RID: 24431 RVA: 0x001211B4 File Offset: 0x0011F3B4
		public void Serialize(Stream stream)
		{
			ParentalControlInfo.Serialize(stream, this);
		}

		// Token: 0x06005F70 RID: 24432 RVA: 0x001211C0 File Offset: 0x0011F3C0
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
				foreach (bool val in instance.PlaySchedule)
				{
					stream.WriteByte(64);
					ProtocolParser.WriteBool(stream, val);
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

		// Token: 0x06005F71 RID: 24433 RVA: 0x001212F8 File Offset: 0x0011F4F8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasTimezone)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Timezone);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasMinutesPerDay)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.MinutesPerDay);
			}
			if (this.HasMinutesPerWeek)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.MinutesPerWeek);
			}
			if (this.HasCanReceiveVoice)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasCanSendVoice)
			{
				num += 1U;
				num += 1U;
			}
			if (this.PlaySchedule.Count > 0)
			{
				foreach (bool flag in this.PlaySchedule)
				{
					num += 1U;
					num += 1U;
				}
			}
			if (this.HasCanJoinGroup)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasCanUseProfile)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x04001D5B RID: 7515
		public bool HasTimezone;

		// Token: 0x04001D5C RID: 7516
		private string _Timezone;

		// Token: 0x04001D5D RID: 7517
		public bool HasMinutesPerDay;

		// Token: 0x04001D5E RID: 7518
		private uint _MinutesPerDay;

		// Token: 0x04001D5F RID: 7519
		public bool HasMinutesPerWeek;

		// Token: 0x04001D60 RID: 7520
		private uint _MinutesPerWeek;

		// Token: 0x04001D61 RID: 7521
		public bool HasCanReceiveVoice;

		// Token: 0x04001D62 RID: 7522
		private bool _CanReceiveVoice;

		// Token: 0x04001D63 RID: 7523
		public bool HasCanSendVoice;

		// Token: 0x04001D64 RID: 7524
		private bool _CanSendVoice;

		// Token: 0x04001D65 RID: 7525
		private List<bool> _PlaySchedule = new List<bool>();

		// Token: 0x04001D66 RID: 7526
		public bool HasCanJoinGroup;

		// Token: 0x04001D67 RID: 7527
		private bool _CanJoinGroup;

		// Token: 0x04001D68 RID: 7528
		public bool HasCanUseProfile;

		// Token: 0x04001D69 RID: 7529
		private bool _CanUseProfile;
	}
}
