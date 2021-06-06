using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011CC RID: 4556
	public class InitialClientStateOutOfOrder : IProtoBuf
	{
		// Token: 0x17000F4C RID: 3916
		// (get) Token: 0x0600CAAE RID: 51886 RVA: 0x003CAD68 File Offset: 0x003C8F68
		// (set) Token: 0x0600CAAF RID: 51887 RVA: 0x003CAD70 File Offset: 0x003C8F70
		public int CountNotificationsAchieve
		{
			get
			{
				return this._CountNotificationsAchieve;
			}
			set
			{
				this._CountNotificationsAchieve = value;
				this.HasCountNotificationsAchieve = true;
			}
		}

		// Token: 0x17000F4D RID: 3917
		// (get) Token: 0x0600CAB0 RID: 51888 RVA: 0x003CAD80 File Offset: 0x003C8F80
		// (set) Token: 0x0600CAB1 RID: 51889 RVA: 0x003CAD88 File Offset: 0x003C8F88
		public int CountNotificationsNotice
		{
			get
			{
				return this._CountNotificationsNotice;
			}
			set
			{
				this._CountNotificationsNotice = value;
				this.HasCountNotificationsNotice = true;
			}
		}

		// Token: 0x17000F4E RID: 3918
		// (get) Token: 0x0600CAB2 RID: 51890 RVA: 0x003CAD98 File Offset: 0x003C8F98
		// (set) Token: 0x0600CAB3 RID: 51891 RVA: 0x003CADA0 File Offset: 0x003C8FA0
		public int CountNotificationsCollection
		{
			get
			{
				return this._CountNotificationsCollection;
			}
			set
			{
				this._CountNotificationsCollection = value;
				this.HasCountNotificationsCollection = true;
			}
		}

		// Token: 0x17000F4F RID: 3919
		// (get) Token: 0x0600CAB4 RID: 51892 RVA: 0x003CADB0 File Offset: 0x003C8FB0
		// (set) Token: 0x0600CAB5 RID: 51893 RVA: 0x003CADB8 File Offset: 0x003C8FB8
		public int CountNotificationsCurrency
		{
			get
			{
				return this._CountNotificationsCurrency;
			}
			set
			{
				this._CountNotificationsCurrency = value;
				this.HasCountNotificationsCurrency = true;
			}
		}

		// Token: 0x17000F50 RID: 3920
		// (get) Token: 0x0600CAB6 RID: 51894 RVA: 0x003CADC8 File Offset: 0x003C8FC8
		// (set) Token: 0x0600CAB7 RID: 51895 RVA: 0x003CADD0 File Offset: 0x003C8FD0
		public int CountNotificationsBooster
		{
			get
			{
				return this._CountNotificationsBooster;
			}
			set
			{
				this._CountNotificationsBooster = value;
				this.HasCountNotificationsBooster = true;
			}
		}

		// Token: 0x17000F51 RID: 3921
		// (get) Token: 0x0600CAB8 RID: 51896 RVA: 0x003CADE0 File Offset: 0x003C8FE0
		// (set) Token: 0x0600CAB9 RID: 51897 RVA: 0x003CADE8 File Offset: 0x003C8FE8
		public int CountNotificationsHeroxp
		{
			get
			{
				return this._CountNotificationsHeroxp;
			}
			set
			{
				this._CountNotificationsHeroxp = value;
				this.HasCountNotificationsHeroxp = true;
			}
		}

		// Token: 0x17000F52 RID: 3922
		// (get) Token: 0x0600CABA RID: 51898 RVA: 0x003CADF8 File Offset: 0x003C8FF8
		// (set) Token: 0x0600CABB RID: 51899 RVA: 0x003CAE00 File Offset: 0x003C9000
		public int CountNotificationsPlayerRecord
		{
			get
			{
				return this._CountNotificationsPlayerRecord;
			}
			set
			{
				this._CountNotificationsPlayerRecord = value;
				this.HasCountNotificationsPlayerRecord = true;
			}
		}

		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x0600CABC RID: 51900 RVA: 0x003CAE10 File Offset: 0x003C9010
		// (set) Token: 0x0600CABD RID: 51901 RVA: 0x003CAE18 File Offset: 0x003C9018
		public int CountNotificationsArenaSession
		{
			get
			{
				return this._CountNotificationsArenaSession;
			}
			set
			{
				this._CountNotificationsArenaSession = value;
				this.HasCountNotificationsArenaSession = true;
			}
		}

		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x0600CABE RID: 51902 RVA: 0x003CAE28 File Offset: 0x003C9028
		// (set) Token: 0x0600CABF RID: 51903 RVA: 0x003CAE30 File Offset: 0x003C9030
		public int CountNotificationsCardBack
		{
			get
			{
				return this._CountNotificationsCardBack;
			}
			set
			{
				this._CountNotificationsCardBack = value;
				this.HasCountNotificationsCardBack = true;
			}
		}

		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x0600CAC0 RID: 51904 RVA: 0x003CAE40 File Offset: 0x003C9040
		// (set) Token: 0x0600CAC1 RID: 51905 RVA: 0x003CAE48 File Offset: 0x003C9048
		public DeviceInfo DeviceInfo
		{
			get
			{
				return this._DeviceInfo;
			}
			set
			{
				this._DeviceInfo = value;
				this.HasDeviceInfo = (value != null);
			}
		}

		// Token: 0x0600CAC2 RID: 51906 RVA: 0x003CAE5C File Offset: 0x003C905C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasCountNotificationsAchieve)
			{
				num ^= this.CountNotificationsAchieve.GetHashCode();
			}
			if (this.HasCountNotificationsNotice)
			{
				num ^= this.CountNotificationsNotice.GetHashCode();
			}
			if (this.HasCountNotificationsCollection)
			{
				num ^= this.CountNotificationsCollection.GetHashCode();
			}
			if (this.HasCountNotificationsCurrency)
			{
				num ^= this.CountNotificationsCurrency.GetHashCode();
			}
			if (this.HasCountNotificationsBooster)
			{
				num ^= this.CountNotificationsBooster.GetHashCode();
			}
			if (this.HasCountNotificationsHeroxp)
			{
				num ^= this.CountNotificationsHeroxp.GetHashCode();
			}
			if (this.HasCountNotificationsPlayerRecord)
			{
				num ^= this.CountNotificationsPlayerRecord.GetHashCode();
			}
			if (this.HasCountNotificationsArenaSession)
			{
				num ^= this.CountNotificationsArenaSession.GetHashCode();
			}
			if (this.HasCountNotificationsCardBack)
			{
				num ^= this.CountNotificationsCardBack.GetHashCode();
			}
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CAC3 RID: 51907 RVA: 0x003CAF70 File Offset: 0x003C9170
		public override bool Equals(object obj)
		{
			InitialClientStateOutOfOrder initialClientStateOutOfOrder = obj as InitialClientStateOutOfOrder;
			return initialClientStateOutOfOrder != null && this.HasCountNotificationsAchieve == initialClientStateOutOfOrder.HasCountNotificationsAchieve && (!this.HasCountNotificationsAchieve || this.CountNotificationsAchieve.Equals(initialClientStateOutOfOrder.CountNotificationsAchieve)) && this.HasCountNotificationsNotice == initialClientStateOutOfOrder.HasCountNotificationsNotice && (!this.HasCountNotificationsNotice || this.CountNotificationsNotice.Equals(initialClientStateOutOfOrder.CountNotificationsNotice)) && this.HasCountNotificationsCollection == initialClientStateOutOfOrder.HasCountNotificationsCollection && (!this.HasCountNotificationsCollection || this.CountNotificationsCollection.Equals(initialClientStateOutOfOrder.CountNotificationsCollection)) && this.HasCountNotificationsCurrency == initialClientStateOutOfOrder.HasCountNotificationsCurrency && (!this.HasCountNotificationsCurrency || this.CountNotificationsCurrency.Equals(initialClientStateOutOfOrder.CountNotificationsCurrency)) && this.HasCountNotificationsBooster == initialClientStateOutOfOrder.HasCountNotificationsBooster && (!this.HasCountNotificationsBooster || this.CountNotificationsBooster.Equals(initialClientStateOutOfOrder.CountNotificationsBooster)) && this.HasCountNotificationsHeroxp == initialClientStateOutOfOrder.HasCountNotificationsHeroxp && (!this.HasCountNotificationsHeroxp || this.CountNotificationsHeroxp.Equals(initialClientStateOutOfOrder.CountNotificationsHeroxp)) && this.HasCountNotificationsPlayerRecord == initialClientStateOutOfOrder.HasCountNotificationsPlayerRecord && (!this.HasCountNotificationsPlayerRecord || this.CountNotificationsPlayerRecord.Equals(initialClientStateOutOfOrder.CountNotificationsPlayerRecord)) && this.HasCountNotificationsArenaSession == initialClientStateOutOfOrder.HasCountNotificationsArenaSession && (!this.HasCountNotificationsArenaSession || this.CountNotificationsArenaSession.Equals(initialClientStateOutOfOrder.CountNotificationsArenaSession)) && this.HasCountNotificationsCardBack == initialClientStateOutOfOrder.HasCountNotificationsCardBack && (!this.HasCountNotificationsCardBack || this.CountNotificationsCardBack.Equals(initialClientStateOutOfOrder.CountNotificationsCardBack)) && this.HasDeviceInfo == initialClientStateOutOfOrder.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(initialClientStateOutOfOrder.DeviceInfo));
		}

		// Token: 0x0600CAC4 RID: 51908 RVA: 0x003CB153 File Offset: 0x003C9353
		public void Deserialize(Stream stream)
		{
			InitialClientStateOutOfOrder.Deserialize(stream, this);
		}

		// Token: 0x0600CAC5 RID: 51909 RVA: 0x003CB15D File Offset: 0x003C935D
		public static InitialClientStateOutOfOrder Deserialize(Stream stream, InitialClientStateOutOfOrder instance)
		{
			return InitialClientStateOutOfOrder.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CAC6 RID: 51910 RVA: 0x003CB168 File Offset: 0x003C9368
		public static InitialClientStateOutOfOrder DeserializeLengthDelimited(Stream stream)
		{
			InitialClientStateOutOfOrder initialClientStateOutOfOrder = new InitialClientStateOutOfOrder();
			InitialClientStateOutOfOrder.DeserializeLengthDelimited(stream, initialClientStateOutOfOrder);
			return initialClientStateOutOfOrder;
		}

		// Token: 0x0600CAC7 RID: 51911 RVA: 0x003CB184 File Offset: 0x003C9384
		public static InitialClientStateOutOfOrder DeserializeLengthDelimited(Stream stream, InitialClientStateOutOfOrder instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return InitialClientStateOutOfOrder.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CAC8 RID: 51912 RVA: 0x003CB1AC File Offset: 0x003C93AC
		public static InitialClientStateOutOfOrder Deserialize(Stream stream, InitialClientStateOutOfOrder instance, long limit)
		{
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
					if (num <= 40)
					{
						if (num <= 16)
						{
							if (num != 10)
							{
								if (num == 16)
								{
									instance.CountNotificationsAchieve = (int)ProtocolParser.ReadUInt64(stream);
									continue;
								}
							}
							else
							{
								if (instance.DeviceInfo == null)
								{
									instance.DeviceInfo = DeviceInfo.DeserializeLengthDelimited(stream);
									continue;
								}
								DeviceInfo.DeserializeLengthDelimited(stream, instance.DeviceInfo);
								continue;
							}
						}
						else
						{
							if (num == 24)
							{
								instance.CountNotificationsNotice = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 32)
							{
								instance.CountNotificationsCollection = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 40)
							{
								instance.CountNotificationsCurrency = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
					}
					else if (num <= 56)
					{
						if (num == 48)
						{
							instance.CountNotificationsBooster = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 56)
						{
							instance.CountNotificationsHeroxp = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 64)
						{
							instance.CountNotificationsPlayerRecord = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 72)
						{
							instance.CountNotificationsArenaSession = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 80)
						{
							instance.CountNotificationsCardBack = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x0600CAC9 RID: 51913 RVA: 0x003CB350 File Offset: 0x003C9550
		public void Serialize(Stream stream)
		{
			InitialClientStateOutOfOrder.Serialize(stream, this);
		}

		// Token: 0x0600CACA RID: 51914 RVA: 0x003CB35C File Offset: 0x003C955C
		public static void Serialize(Stream stream, InitialClientStateOutOfOrder instance)
		{
			if (instance.HasCountNotificationsAchieve)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CountNotificationsAchieve));
			}
			if (instance.HasCountNotificationsNotice)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CountNotificationsNotice));
			}
			if (instance.HasCountNotificationsCollection)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CountNotificationsCollection));
			}
			if (instance.HasCountNotificationsCurrency)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CountNotificationsCurrency));
			}
			if (instance.HasCountNotificationsBooster)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CountNotificationsBooster));
			}
			if (instance.HasCountNotificationsHeroxp)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CountNotificationsHeroxp));
			}
			if (instance.HasCountNotificationsPlayerRecord)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CountNotificationsPlayerRecord));
			}
			if (instance.HasCountNotificationsArenaSession)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CountNotificationsArenaSession));
			}
			if (instance.HasCountNotificationsCardBack)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CountNotificationsCardBack));
			}
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
		}

		// Token: 0x0600CACB RID: 51915 RVA: 0x003CB49C File Offset: 0x003C969C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasCountNotificationsAchieve)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.CountNotificationsAchieve));
			}
			if (this.HasCountNotificationsNotice)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.CountNotificationsNotice));
			}
			if (this.HasCountNotificationsCollection)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.CountNotificationsCollection));
			}
			if (this.HasCountNotificationsCurrency)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.CountNotificationsCurrency));
			}
			if (this.HasCountNotificationsBooster)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.CountNotificationsBooster));
			}
			if (this.HasCountNotificationsHeroxp)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.CountNotificationsHeroxp));
			}
			if (this.HasCountNotificationsPlayerRecord)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.CountNotificationsPlayerRecord));
			}
			if (this.HasCountNotificationsArenaSession)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.CountNotificationsArenaSession));
			}
			if (this.HasCountNotificationsCardBack)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.CountNotificationsCardBack));
			}
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize = this.DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x04009FE3 RID: 40931
		public bool HasCountNotificationsAchieve;

		// Token: 0x04009FE4 RID: 40932
		private int _CountNotificationsAchieve;

		// Token: 0x04009FE5 RID: 40933
		public bool HasCountNotificationsNotice;

		// Token: 0x04009FE6 RID: 40934
		private int _CountNotificationsNotice;

		// Token: 0x04009FE7 RID: 40935
		public bool HasCountNotificationsCollection;

		// Token: 0x04009FE8 RID: 40936
		private int _CountNotificationsCollection;

		// Token: 0x04009FE9 RID: 40937
		public bool HasCountNotificationsCurrency;

		// Token: 0x04009FEA RID: 40938
		private int _CountNotificationsCurrency;

		// Token: 0x04009FEB RID: 40939
		public bool HasCountNotificationsBooster;

		// Token: 0x04009FEC RID: 40940
		private int _CountNotificationsBooster;

		// Token: 0x04009FED RID: 40941
		public bool HasCountNotificationsHeroxp;

		// Token: 0x04009FEE RID: 40942
		private int _CountNotificationsHeroxp;

		// Token: 0x04009FEF RID: 40943
		public bool HasCountNotificationsPlayerRecord;

		// Token: 0x04009FF0 RID: 40944
		private int _CountNotificationsPlayerRecord;

		// Token: 0x04009FF1 RID: 40945
		public bool HasCountNotificationsArenaSession;

		// Token: 0x04009FF2 RID: 40946
		private int _CountNotificationsArenaSession;

		// Token: 0x04009FF3 RID: 40947
		public bool HasCountNotificationsCardBack;

		// Token: 0x04009FF4 RID: 40948
		private int _CountNotificationsCardBack;

		// Token: 0x04009FF5 RID: 40949
		public bool HasDeviceInfo;

		// Token: 0x04009FF6 RID: 40950
		private DeviceInfo _DeviceInfo;
	}
}
