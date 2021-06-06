using System;
using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusFSG
{
	// Token: 0x0200001B RID: 27
	public class RequestNearbyFSGsResponse : IProtoBuf
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00005931 File Offset: 0x00003B31
		// (set) Token: 0x06000115 RID: 277 RVA: 0x00005939 File Offset: 0x00003B39
		public ErrorCode ErrorCode { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00005942 File Offset: 0x00003B42
		// (set) Token: 0x06000117 RID: 279 RVA: 0x0000594A File Offset: 0x00003B4A
		public List<FSGConfig> FSGs
		{
			get
			{
				return this._FSGs;
			}
			set
			{
				this._FSGs = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00005953 File Offset: 0x00003B53
		// (set) Token: 0x06000119 RID: 281 RVA: 0x0000595B File Offset: 0x00003B5B
		public long CheckedInFsgId
		{
			get
			{
				return this._CheckedInFsgId;
			}
			set
			{
				this._CheckedInFsgId = value;
				this.HasCheckedInFsgId = true;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600011A RID: 282 RVA: 0x0000596B File Offset: 0x00003B6B
		// (set) Token: 0x0600011B RID: 283 RVA: 0x00005973 File Offset: 0x00003B73
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

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600011C RID: 284 RVA: 0x0000597C File Offset: 0x00003B7C
		// (set) Token: 0x0600011D RID: 285 RVA: 0x00005984 File Offset: 0x00003B84
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

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00005997 File Offset: 0x00003B97
		// (set) Token: 0x0600011F RID: 287 RVA: 0x0000599F File Offset: 0x00003B9F
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

		// Token: 0x06000120 RID: 288 RVA: 0x000059A8 File Offset: 0x00003BA8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ErrorCode.GetHashCode();
			foreach (FSGConfig fsgconfig in this.FSGs)
			{
				num ^= fsgconfig.GetHashCode();
			}
			if (this.HasCheckedInFsgId)
			{
				num ^= this.CheckedInFsgId.GetHashCode();
			}
			foreach (FSGPatron fsgpatron in this.FsgAttendees)
			{
				num ^= fsgpatron.GetHashCode();
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

		// Token: 0x06000121 RID: 289 RVA: 0x00005AE0 File Offset: 0x00003CE0
		public override bool Equals(object obj)
		{
			RequestNearbyFSGsResponse requestNearbyFSGsResponse = obj as RequestNearbyFSGsResponse;
			if (requestNearbyFSGsResponse == null)
			{
				return false;
			}
			if (!this.ErrorCode.Equals(requestNearbyFSGsResponse.ErrorCode))
			{
				return false;
			}
			if (this.FSGs.Count != requestNearbyFSGsResponse.FSGs.Count)
			{
				return false;
			}
			for (int i = 0; i < this.FSGs.Count; i++)
			{
				if (!this.FSGs[i].Equals(requestNearbyFSGsResponse.FSGs[i]))
				{
					return false;
				}
			}
			if (this.HasCheckedInFsgId != requestNearbyFSGsResponse.HasCheckedInFsgId || (this.HasCheckedInFsgId && !this.CheckedInFsgId.Equals(requestNearbyFSGsResponse.CheckedInFsgId)))
			{
				return false;
			}
			if (this.FsgAttendees.Count != requestNearbyFSGsResponse.FsgAttendees.Count)
			{
				return false;
			}
			for (int j = 0; j < this.FsgAttendees.Count; j++)
			{
				if (!this.FsgAttendees[j].Equals(requestNearbyFSGsResponse.FsgAttendees[j]))
				{
					return false;
				}
			}
			if (this.HasFsgSharedSecretKey != requestNearbyFSGsResponse.HasFsgSharedSecretKey || (this.HasFsgSharedSecretKey && !this.FsgSharedSecretKey.Equals(requestNearbyFSGsResponse.FsgSharedSecretKey)))
			{
				return false;
			}
			if (this.InnkeeperSelectedBrawlLibraryItemId.Count != requestNearbyFSGsResponse.InnkeeperSelectedBrawlLibraryItemId.Count)
			{
				return false;
			}
			for (int k = 0; k < this.InnkeeperSelectedBrawlLibraryItemId.Count; k++)
			{
				if (!this.InnkeeperSelectedBrawlLibraryItemId[k].Equals(requestNearbyFSGsResponse.InnkeeperSelectedBrawlLibraryItemId[k]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00005C79 File Offset: 0x00003E79
		public void Deserialize(Stream stream)
		{
			RequestNearbyFSGsResponse.Deserialize(stream, this);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005C83 File Offset: 0x00003E83
		public static RequestNearbyFSGsResponse Deserialize(Stream stream, RequestNearbyFSGsResponse instance)
		{
			return RequestNearbyFSGsResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00005C90 File Offset: 0x00003E90
		public static RequestNearbyFSGsResponse DeserializeLengthDelimited(Stream stream)
		{
			RequestNearbyFSGsResponse requestNearbyFSGsResponse = new RequestNearbyFSGsResponse();
			RequestNearbyFSGsResponse.DeserializeLengthDelimited(stream, requestNearbyFSGsResponse);
			return requestNearbyFSGsResponse;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00005CAC File Offset: 0x00003EAC
		public static RequestNearbyFSGsResponse DeserializeLengthDelimited(Stream stream, RequestNearbyFSGsResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RequestNearbyFSGsResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00005CD4 File Offset: 0x00003ED4
		public static RequestNearbyFSGsResponse Deserialize(Stream stream, RequestNearbyFSGsResponse instance, long limit)
		{
			if (instance.FSGs == null)
			{
				instance.FSGs = new List<FSGConfig>();
			}
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
					if (num <= 24)
					{
						if (num == 8)
						{
							instance.ErrorCode = (ErrorCode)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 18)
						{
							instance.FSGs.Add(FSGConfig.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 24)
						{
							instance.CheckedInFsgId = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 34)
						{
							instance.FsgAttendees.Add(FSGPatron.DeserializeLengthDelimited(stream));
							continue;
						}
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

		// Token: 0x06000127 RID: 295 RVA: 0x00005E20 File Offset: 0x00004020
		public void Serialize(Stream stream)
		{
			RequestNearbyFSGsResponse.Serialize(stream, this);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005E2C File Offset: 0x0000402C
		public static void Serialize(Stream stream, RequestNearbyFSGsResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ErrorCode));
			if (instance.FSGs.Count > 0)
			{
				foreach (FSGConfig fsgconfig in instance.FSGs)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, fsgconfig.GetSerializedSize());
					FSGConfig.Serialize(stream, fsgconfig);
				}
			}
			if (instance.HasCheckedInFsgId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CheckedInFsgId);
			}
			if (instance.FsgAttendees.Count > 0)
			{
				foreach (FSGPatron fsgpatron in instance.FsgAttendees)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, fsgpatron.GetSerializedSize());
					FSGPatron.Serialize(stream, fsgpatron);
				}
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

		// Token: 0x06000129 RID: 297 RVA: 0x00005FB0 File Offset: 0x000041B0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ErrorCode));
			if (this.FSGs.Count > 0)
			{
				foreach (FSGConfig fsgconfig in this.FSGs)
				{
					num += 1U;
					uint serializedSize = fsgconfig.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasCheckedInFsgId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.CheckedInFsgId);
			}
			if (this.FsgAttendees.Count > 0)
			{
				foreach (FSGPatron fsgpatron in this.FsgAttendees)
				{
					num += 1U;
					uint serializedSize2 = fsgpatron.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
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
			num += 1U;
			return num;
		}

		// Token: 0x04000050 RID: 80
		private List<FSGConfig> _FSGs = new List<FSGConfig>();

		// Token: 0x04000051 RID: 81
		public bool HasCheckedInFsgId;

		// Token: 0x04000052 RID: 82
		private long _CheckedInFsgId;

		// Token: 0x04000053 RID: 83
		private List<FSGPatron> _FsgAttendees = new List<FSGPatron>();

		// Token: 0x04000054 RID: 84
		public bool HasFsgSharedSecretKey;

		// Token: 0x04000055 RID: 85
		private byte[] _FsgSharedSecretKey;

		// Token: 0x04000056 RID: 86
		private List<int> _InnkeeperSelectedBrawlLibraryItemId = new List<int>();

		// Token: 0x02000551 RID: 1361
		public enum PacketID
		{
			// Token: 0x04001E14 RID: 7700
			ID = 504
		}
	}
}
