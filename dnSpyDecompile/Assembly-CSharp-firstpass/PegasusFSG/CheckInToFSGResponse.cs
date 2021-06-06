using System;
using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusFSG
{
	// Token: 0x0200001C RID: 28
	public class CheckInToFSGResponse : IProtoBuf
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600012B RID: 299 RVA: 0x0000615D File Offset: 0x0000435D
		// (set) Token: 0x0600012C RID: 300 RVA: 0x00006165 File Offset: 0x00004365
		public ErrorCode ErrorCode { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600012D RID: 301 RVA: 0x0000616E File Offset: 0x0000436E
		// (set) Token: 0x0600012E RID: 302 RVA: 0x00006176 File Offset: 0x00004376
		public long FsgId { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600012F RID: 303 RVA: 0x0000617F File Offset: 0x0000437F
		// (set) Token: 0x06000130 RID: 304 RVA: 0x00006187 File Offset: 0x00004387
		public List<FSGPatron> FsgAttendees
		{
			get
			{
				return this._FsgAttendees;
			}
			set
			{
				this._FsgAttendees = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00006190 File Offset: 0x00004390
		// (set) Token: 0x06000132 RID: 306 RVA: 0x00006198 File Offset: 0x00004398
		public TavernBrawlPlayerRecord PlayerRecord
		{
			get
			{
				return this._PlayerRecord;
			}
			set
			{
				this._PlayerRecord = value;
				this.HasPlayerRecord = (value != null);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000133 RID: 307 RVA: 0x000061AB File Offset: 0x000043AB
		// (set) Token: 0x06000134 RID: 308 RVA: 0x000061B3 File Offset: 0x000043B3
		public byte[] FsgSharedSecretKey
		{
			get
			{
				return this._FsgSharedSecretKey;
			}
			set
			{
				this._FsgSharedSecretKey = value;
				this.HasFsgSharedSecretKey = (value != null);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000135 RID: 309 RVA: 0x000061C6 File Offset: 0x000043C6
		// (set) Token: 0x06000136 RID: 310 RVA: 0x000061CE File Offset: 0x000043CE
		public List<int> InnkeeperSelectedBrawlLibraryItemId
		{
			get
			{
				return this._InnkeeperSelectedBrawlLibraryItemId;
			}
			set
			{
				this._InnkeeperSelectedBrawlLibraryItemId = value;
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000061D8 File Offset: 0x000043D8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ErrorCode.GetHashCode();
			num ^= this.FsgId.GetHashCode();
			foreach (FSGPatron fsgpatron in this.FsgAttendees)
			{
				num ^= fsgpatron.GetHashCode();
			}
			if (this.HasPlayerRecord)
			{
				num ^= this.PlayerRecord.GetHashCode();
			}
			if (this.HasFsgSharedSecretKey)
			{
				num ^= this.FsgSharedSecretKey.GetHashCode();
			}
			foreach (int num2 in this.InnkeeperSelectedBrawlLibraryItemId)
			{
				num ^= num2.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000062D8 File Offset: 0x000044D8
		public override bool Equals(object obj)
		{
			CheckInToFSGResponse checkInToFSGResponse = obj as CheckInToFSGResponse;
			if (checkInToFSGResponse == null)
			{
				return false;
			}
			if (!this.ErrorCode.Equals(checkInToFSGResponse.ErrorCode))
			{
				return false;
			}
			if (!this.FsgId.Equals(checkInToFSGResponse.FsgId))
			{
				return false;
			}
			if (this.FsgAttendees.Count != checkInToFSGResponse.FsgAttendees.Count)
			{
				return false;
			}
			for (int i = 0; i < this.FsgAttendees.Count; i++)
			{
				if (!this.FsgAttendees[i].Equals(checkInToFSGResponse.FsgAttendees[i]))
				{
					return false;
				}
			}
			if (this.HasPlayerRecord != checkInToFSGResponse.HasPlayerRecord || (this.HasPlayerRecord && !this.PlayerRecord.Equals(checkInToFSGResponse.PlayerRecord)))
			{
				return false;
			}
			if (this.HasFsgSharedSecretKey != checkInToFSGResponse.HasFsgSharedSecretKey || (this.HasFsgSharedSecretKey && !this.FsgSharedSecretKey.Equals(checkInToFSGResponse.FsgSharedSecretKey)))
			{
				return false;
			}
			if (this.InnkeeperSelectedBrawlLibraryItemId.Count != checkInToFSGResponse.InnkeeperSelectedBrawlLibraryItemId.Count)
			{
				return false;
			}
			for (int j = 0; j < this.InnkeeperSelectedBrawlLibraryItemId.Count; j++)
			{
				if (!this.InnkeeperSelectedBrawlLibraryItemId[j].Equals(checkInToFSGResponse.InnkeeperSelectedBrawlLibraryItemId[j]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000642F File Offset: 0x0000462F
		public void Deserialize(Stream stream)
		{
			CheckInToFSGResponse.Deserialize(stream, this);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00006439 File Offset: 0x00004639
		public static CheckInToFSGResponse Deserialize(Stream stream, CheckInToFSGResponse instance)
		{
			return CheckInToFSGResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00006444 File Offset: 0x00004644
		public static CheckInToFSGResponse DeserializeLengthDelimited(Stream stream)
		{
			CheckInToFSGResponse checkInToFSGResponse = new CheckInToFSGResponse();
			CheckInToFSGResponse.DeserializeLengthDelimited(stream, checkInToFSGResponse);
			return checkInToFSGResponse;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00006460 File Offset: 0x00004660
		public static CheckInToFSGResponse DeserializeLengthDelimited(Stream stream, CheckInToFSGResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CheckInToFSGResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00006488 File Offset: 0x00004688
		public static CheckInToFSGResponse Deserialize(Stream stream, CheckInToFSGResponse instance, long limit)
		{
			if (instance.FsgAttendees == null)
			{
				instance.FsgAttendees = new List<FSGPatron>();
			}
			if (instance.InnkeeperSelectedBrawlLibraryItemId == null)
			{
				instance.InnkeeperSelectedBrawlLibraryItemId = new List<int>();
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
					if (num <= 26)
					{
						if (num == 8)
						{
							instance.ErrorCode = (ErrorCode)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.FsgId = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 26)
						{
							instance.FsgAttendees.Add(FSGPatron.DeserializeLengthDelimited(stream));
							continue;
						}
					}
					else if (num != 34)
					{
						if (num == 42)
						{
							instance.FsgSharedSecretKey = ProtocolParser.ReadBytes(stream);
							continue;
						}
						if (num == 48)
						{
							instance.InnkeeperSelectedBrawlLibraryItemId.Add((int)ProtocolParser.ReadUInt64(stream));
							continue;
						}
					}
					else
					{
						if (instance.PlayerRecord == null)
						{
							instance.PlayerRecord = TavernBrawlPlayerRecord.DeserializeLengthDelimited(stream);
							continue;
						}
						TavernBrawlPlayerRecord.DeserializeLengthDelimited(stream, instance.PlayerRecord);
						continue;
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

		// Token: 0x0600013E RID: 318 RVA: 0x000065D9 File Offset: 0x000047D9
		public void Serialize(Stream stream)
		{
			CheckInToFSGResponse.Serialize(stream, this);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000065E4 File Offset: 0x000047E4
		public static void Serialize(Stream stream, CheckInToFSGResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ErrorCode));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.FsgId);
			if (instance.FsgAttendees.Count > 0)
			{
				foreach (FSGPatron fsgpatron in instance.FsgAttendees)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, fsgpatron.GetSerializedSize());
					FSGPatron.Serialize(stream, fsgpatron);
				}
			}
			if (instance.HasPlayerRecord)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.PlayerRecord.GetSerializedSize());
				TavernBrawlPlayerRecord.Serialize(stream, instance.PlayerRecord);
			}
			if (instance.HasFsgSharedSecretKey)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, instance.FsgSharedSecretKey);
			}
			if (instance.InnkeeperSelectedBrawlLibraryItemId.Count > 0)
			{
				foreach (int num in instance.InnkeeperSelectedBrawlLibraryItemId)
				{
					stream.WriteByte(48);
					ProtocolParser.WriteUInt64(stream, (ulong)((long)num));
				}
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00006724 File Offset: 0x00004924
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ErrorCode));
			num += ProtocolParser.SizeOfUInt64((ulong)this.FsgId);
			if (this.FsgAttendees.Count > 0)
			{
				foreach (FSGPatron fsgpatron in this.FsgAttendees)
				{
					num += 1U;
					uint serializedSize = fsgpatron.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasPlayerRecord)
			{
				num += 1U;
				uint serializedSize2 = this.PlayerRecord.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasFsgSharedSecretKey)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.FsgSharedSecretKey.Length) + (uint)this.FsgSharedSecretKey.Length;
			}
			if (this.InnkeeperSelectedBrawlLibraryItemId.Count > 0)
			{
				foreach (int num2 in this.InnkeeperSelectedBrawlLibraryItemId)
				{
					num += 1U;
					num += ProtocolParser.SizeOfUInt64((ulong)((long)num2));
				}
			}
			num += 2U;
			return num;
		}

		// Token: 0x04000059 RID: 89
		private List<FSGPatron> _FsgAttendees = new List<FSGPatron>();

		// Token: 0x0400005A RID: 90
		public bool HasPlayerRecord;

		// Token: 0x0400005B RID: 91
		private TavernBrawlPlayerRecord _PlayerRecord;

		// Token: 0x0400005C RID: 92
		public bool HasFsgSharedSecretKey;

		// Token: 0x0400005D RID: 93
		private byte[] _FsgSharedSecretKey;

		// Token: 0x0400005E RID: 94
		private List<int> _InnkeeperSelectedBrawlLibraryItemId = new List<int>();

		// Token: 0x02000552 RID: 1362
		public enum PacketID
		{
			// Token: 0x04001E16 RID: 7702
			ID = 505
		}
	}
}
