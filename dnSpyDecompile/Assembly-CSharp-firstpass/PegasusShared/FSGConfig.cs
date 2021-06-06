using System;
using System.IO;
using System.Text;

namespace PegasusShared
{
	// Token: 0x02000162 RID: 354
	public class FSGConfig : IProtoBuf
	{
		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001832 RID: 6194 RVA: 0x00054B51 File Offset: 0x00052D51
		// (set) Token: 0x06001833 RID: 6195 RVA: 0x00054B59 File Offset: 0x00052D59
		public long FsgId { get; set; }

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001834 RID: 6196 RVA: 0x00054B62 File Offset: 0x00052D62
		// (set) Token: 0x06001835 RID: 6197 RVA: 0x00054B6A File Offset: 0x00052D6A
		public long UnixStartTimeWithSlush { get; set; }

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001836 RID: 6198 RVA: 0x00054B73 File Offset: 0x00052D73
		// (set) Token: 0x06001837 RID: 6199 RVA: 0x00054B7B File Offset: 0x00052D7B
		public long UnixEndTimeWithSlush { get; set; }

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001838 RID: 6200 RVA: 0x00054B84 File Offset: 0x00052D84
		// (set) Token: 0x06001839 RID: 6201 RVA: 0x00054B8C File Offset: 0x00052D8C
		public string TavernName { get; set; }

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x0600183A RID: 6202 RVA: 0x00054B95 File Offset: 0x00052D95
		// (set) Token: 0x0600183B RID: 6203 RVA: 0x00054B9D File Offset: 0x00052D9D
		public TavernSignData SignData { get; set; }

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x0600183C RID: 6204 RVA: 0x00054BA6 File Offset: 0x00052DA6
		// (set) Token: 0x0600183D RID: 6205 RVA: 0x00054BAE File Offset: 0x00052DAE
		public long UnixOfficialStartTime { get; set; }

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x0600183E RID: 6206 RVA: 0x00054BB7 File Offset: 0x00052DB7
		// (set) Token: 0x0600183F RID: 6207 RVA: 0x00054BBF File Offset: 0x00052DBF
		public long UnixOfficialEndTime { get; set; }

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001840 RID: 6208 RVA: 0x00054BC8 File Offset: 0x00052DC8
		// (set) Token: 0x06001841 RID: 6209 RVA: 0x00054BD0 File Offset: 0x00052DD0
		public long PatronCount { get; set; }

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001842 RID: 6210 RVA: 0x00054BD9 File Offset: 0x00052DD9
		// (set) Token: 0x06001843 RID: 6211 RVA: 0x00054BE1 File Offset: 0x00052DE1
		public bool IsInnkeeper
		{
			get
			{
				return this._IsInnkeeper;
			}
			set
			{
				this._IsInnkeeper = value;
				this.HasIsInnkeeper = true;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001844 RID: 6212 RVA: 0x00054BF1 File Offset: 0x00052DF1
		// (set) Token: 0x06001845 RID: 6213 RVA: 0x00054BF9 File Offset: 0x00052DF9
		public bool IsSetupComplete
		{
			get
			{
				return this._IsSetupComplete;
			}
			set
			{
				this._IsSetupComplete = value;
				this.HasIsSetupComplete = true;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001846 RID: 6214 RVA: 0x00054C09 File Offset: 0x00052E09
		// (set) Token: 0x06001847 RID: 6215 RVA: 0x00054C11 File Offset: 0x00052E11
		public BnetId FsgInnkeeperAccountId { get; set; }

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001848 RID: 6216 RVA: 0x00054C1A File Offset: 0x00052E1A
		// (set) Token: 0x06001849 RID: 6217 RVA: 0x00054C22 File Offset: 0x00052E22
		public bool IsLargeScaleFsg
		{
			get
			{
				return this._IsLargeScaleFsg;
			}
			set
			{
				this._IsLargeScaleFsg = value;
				this.HasIsLargeScaleFsg = true;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x0600184A RID: 6218 RVA: 0x00054C32 File Offset: 0x00052E32
		// (set) Token: 0x0600184B RID: 6219 RVA: 0x00054C3A File Offset: 0x00052E3A
		public string FsgName
		{
			get
			{
				return this._FsgName;
			}
			set
			{
				this._FsgName = value;
				this.HasFsgName = (value != null);
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x0600184C RID: 6220 RVA: 0x00054C4D File Offset: 0x00052E4D
		// (set) Token: 0x0600184D RID: 6221 RVA: 0x00054C55 File Offset: 0x00052E55
		public long TavernId
		{
			get
			{
				return this._TavernId;
			}
			set
			{
				this._TavernId = value;
				this.HasTavernId = true;
			}
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x00054C68 File Offset: 0x00052E68
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.FsgId.GetHashCode();
			num ^= this.UnixStartTimeWithSlush.GetHashCode();
			num ^= this.UnixEndTimeWithSlush.GetHashCode();
			num ^= this.TavernName.GetHashCode();
			num ^= this.SignData.GetHashCode();
			num ^= this.UnixOfficialStartTime.GetHashCode();
			num ^= this.UnixOfficialEndTime.GetHashCode();
			num ^= this.PatronCount.GetHashCode();
			if (this.HasIsInnkeeper)
			{
				num ^= this.IsInnkeeper.GetHashCode();
			}
			if (this.HasIsSetupComplete)
			{
				num ^= this.IsSetupComplete.GetHashCode();
			}
			num ^= this.FsgInnkeeperAccountId.GetHashCode();
			if (this.HasIsLargeScaleFsg)
			{
				num ^= this.IsLargeScaleFsg.GetHashCode();
			}
			if (this.HasFsgName)
			{
				num ^= this.FsgName.GetHashCode();
			}
			if (this.HasTavernId)
			{
				num ^= this.TavernId.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x00054D8C File Offset: 0x00052F8C
		public override bool Equals(object obj)
		{
			FSGConfig fsgconfig = obj as FSGConfig;
			return fsgconfig != null && this.FsgId.Equals(fsgconfig.FsgId) && this.UnixStartTimeWithSlush.Equals(fsgconfig.UnixStartTimeWithSlush) && this.UnixEndTimeWithSlush.Equals(fsgconfig.UnixEndTimeWithSlush) && this.TavernName.Equals(fsgconfig.TavernName) && this.SignData.Equals(fsgconfig.SignData) && this.UnixOfficialStartTime.Equals(fsgconfig.UnixOfficialStartTime) && this.UnixOfficialEndTime.Equals(fsgconfig.UnixOfficialEndTime) && this.PatronCount.Equals(fsgconfig.PatronCount) && this.HasIsInnkeeper == fsgconfig.HasIsInnkeeper && (!this.HasIsInnkeeper || this.IsInnkeeper.Equals(fsgconfig.IsInnkeeper)) && this.HasIsSetupComplete == fsgconfig.HasIsSetupComplete && (!this.HasIsSetupComplete || this.IsSetupComplete.Equals(fsgconfig.IsSetupComplete)) && this.FsgInnkeeperAccountId.Equals(fsgconfig.FsgInnkeeperAccountId) && this.HasIsLargeScaleFsg == fsgconfig.HasIsLargeScaleFsg && (!this.HasIsLargeScaleFsg || this.IsLargeScaleFsg.Equals(fsgconfig.IsLargeScaleFsg)) && this.HasFsgName == fsgconfig.HasFsgName && (!this.HasFsgName || this.FsgName.Equals(fsgconfig.FsgName)) && this.HasTavernId == fsgconfig.HasTavernId && (!this.HasTavernId || this.TavernId.Equals(fsgconfig.TavernId));
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x00054F58 File Offset: 0x00053158
		public void Deserialize(Stream stream)
		{
			FSGConfig.Deserialize(stream, this);
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x00054F62 File Offset: 0x00053162
		public static FSGConfig Deserialize(Stream stream, FSGConfig instance)
		{
			return FSGConfig.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x00054F70 File Offset: 0x00053170
		public static FSGConfig DeserializeLengthDelimited(Stream stream)
		{
			FSGConfig fsgconfig = new FSGConfig();
			FSGConfig.DeserializeLengthDelimited(stream, fsgconfig);
			return fsgconfig;
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x00054F8C File Offset: 0x0005318C
		public static FSGConfig DeserializeLengthDelimited(Stream stream, FSGConfig instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FSGConfig.Deserialize(stream, instance, num);
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x00054FB4 File Offset: 0x000531B4
		public static FSGConfig Deserialize(Stream stream, FSGConfig instance, long limit)
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
					if (num <= 56)
					{
						if (num <= 24)
						{
							if (num == 8)
							{
								instance.FsgId = (long)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 16)
							{
								instance.UnixStartTimeWithSlush = (long)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 24)
							{
								instance.UnixEndTimeWithSlush = (long)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else if (num <= 42)
						{
							if (num == 34)
							{
								instance.TavernName = ProtocolParser.ReadString(stream);
								continue;
							}
							if (num == 42)
							{
								if (instance.SignData == null)
								{
									instance.SignData = TavernSignData.DeserializeLengthDelimited(stream);
									continue;
								}
								TavernSignData.DeserializeLengthDelimited(stream, instance.SignData);
								continue;
							}
						}
						else
						{
							if (num == 48)
							{
								instance.UnixOfficialStartTime = (long)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 56)
							{
								instance.PatronCount = (long)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
					}
					else if (num <= 80)
					{
						if (num == 64)
						{
							instance.IsInnkeeper = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 72)
						{
							instance.IsSetupComplete = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 80)
						{
							instance.UnixOfficialEndTime = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else if (num <= 96)
					{
						if (num != 90)
						{
							if (num == 96)
							{
								instance.IsLargeScaleFsg = ProtocolParser.ReadBool(stream);
								continue;
							}
						}
						else
						{
							if (instance.FsgInnkeeperAccountId == null)
							{
								instance.FsgInnkeeperAccountId = BnetId.DeserializeLengthDelimited(stream);
								continue;
							}
							BnetId.DeserializeLengthDelimited(stream, instance.FsgInnkeeperAccountId);
							continue;
						}
					}
					else
					{
						if (num == 106)
						{
							instance.FsgName = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 112)
						{
							instance.TavernId = (long)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06001855 RID: 6229 RVA: 0x000551F2 File Offset: 0x000533F2
		public void Serialize(Stream stream)
		{
			FSGConfig.Serialize(stream, this);
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x000551FC File Offset: 0x000533FC
		public static void Serialize(Stream stream, FSGConfig instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.FsgId);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.UnixStartTimeWithSlush);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.UnixEndTimeWithSlush);
			if (instance.TavernName == null)
			{
				throw new ArgumentNullException("TavernName", "Required by proto specification.");
			}
			stream.WriteByte(34);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.TavernName));
			if (instance.SignData == null)
			{
				throw new ArgumentNullException("SignData", "Required by proto specification.");
			}
			stream.WriteByte(42);
			ProtocolParser.WriteUInt32(stream, instance.SignData.GetSerializedSize());
			TavernSignData.Serialize(stream, instance.SignData);
			stream.WriteByte(48);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.UnixOfficialStartTime);
			stream.WriteByte(80);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.UnixOfficialEndTime);
			stream.WriteByte(56);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.PatronCount);
			if (instance.HasIsInnkeeper)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteBool(stream, instance.IsInnkeeper);
			}
			if (instance.HasIsSetupComplete)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteBool(stream, instance.IsSetupComplete);
			}
			if (instance.FsgInnkeeperAccountId == null)
			{
				throw new ArgumentNullException("FsgInnkeeperAccountId", "Required by proto specification.");
			}
			stream.WriteByte(90);
			ProtocolParser.WriteUInt32(stream, instance.FsgInnkeeperAccountId.GetSerializedSize());
			BnetId.Serialize(stream, instance.FsgInnkeeperAccountId);
			if (instance.HasIsLargeScaleFsg)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteBool(stream, instance.IsLargeScaleFsg);
			}
			if (instance.HasFsgName)
			{
				stream.WriteByte(106);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FsgName));
			}
			if (instance.HasTavernId)
			{
				stream.WriteByte(112);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.TavernId);
			}
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x000553C8 File Offset: 0x000535C8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)this.FsgId);
			num += ProtocolParser.SizeOfUInt64((ulong)this.UnixStartTimeWithSlush);
			num += ProtocolParser.SizeOfUInt64((ulong)this.UnixEndTimeWithSlush);
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.TavernName);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			uint serializedSize = this.SignData.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			num += ProtocolParser.SizeOfUInt64((ulong)this.UnixOfficialStartTime);
			num += ProtocolParser.SizeOfUInt64((ulong)this.UnixOfficialEndTime);
			num += ProtocolParser.SizeOfUInt64((ulong)this.PatronCount);
			if (this.HasIsInnkeeper)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasIsSetupComplete)
			{
				num += 1U;
				num += 1U;
			}
			uint serializedSize2 = this.FsgInnkeeperAccountId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			if (this.HasIsLargeScaleFsg)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasFsgName)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.FsgName);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasTavernId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.TavernId);
			}
			return num + 9U;
		}

		// Token: 0x040007C2 RID: 1986
		public bool HasIsInnkeeper;

		// Token: 0x040007C3 RID: 1987
		private bool _IsInnkeeper;

		// Token: 0x040007C4 RID: 1988
		public bool HasIsSetupComplete;

		// Token: 0x040007C5 RID: 1989
		private bool _IsSetupComplete;

		// Token: 0x040007C7 RID: 1991
		public bool HasIsLargeScaleFsg;

		// Token: 0x040007C8 RID: 1992
		private bool _IsLargeScaleFsg;

		// Token: 0x040007C9 RID: 1993
		public bool HasFsgName;

		// Token: 0x040007CA RID: 1994
		private string _FsgName;

		// Token: 0x040007CB RID: 1995
		public bool HasTavernId;

		// Token: 0x040007CC RID: 1996
		private long _TavernId;
	}
}
