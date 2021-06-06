using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PegasusShared;

namespace PegasusFSG
{
	// Token: 0x02000019 RID: 25
	public class CheckInToFSG : IProtoBuf
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00005193 File Offset: 0x00003393
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x0000519B File Offset: 0x0000339B
		public long FsgId { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x000051A4 File Offset: 0x000033A4
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x000051AC File Offset: 0x000033AC
		public GPSCoords Location
		{
			get
			{
				return this._Location;
			}
			set
			{
				this._Location = value;
				this.HasLocation = (value != null);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x000051BF File Offset: 0x000033BF
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x000051C7 File Offset: 0x000033C7
		public List<string> Bssids
		{
			get
			{
				return this._Bssids;
			}
			set
			{
				this._Bssids = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000051D0 File Offset: 0x000033D0
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x000051D8 File Offset: 0x000033D8
		public Platform Platform
		{
			get
			{
				return this._Platform;
			}
			set
			{
				this._Platform = value;
				this.HasPlatform = (value != null);
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000051EC File Offset: 0x000033EC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.FsgId.GetHashCode();
			if (this.HasLocation)
			{
				num ^= this.Location.GetHashCode();
			}
			foreach (string text in this.Bssids)
			{
				num ^= text.GetHashCode();
			}
			if (this.HasPlatform)
			{
				num ^= this.Platform.GetHashCode();
			}
			return num;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000528C File Offset: 0x0000348C
		public override bool Equals(object obj)
		{
			CheckInToFSG checkInToFSG = obj as CheckInToFSG;
			if (checkInToFSG == null)
			{
				return false;
			}
			if (!this.FsgId.Equals(checkInToFSG.FsgId))
			{
				return false;
			}
			if (this.HasLocation != checkInToFSG.HasLocation || (this.HasLocation && !this.Location.Equals(checkInToFSG.Location)))
			{
				return false;
			}
			if (this.Bssids.Count != checkInToFSG.Bssids.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Bssids.Count; i++)
			{
				if (!this.Bssids[i].Equals(checkInToFSG.Bssids[i]))
				{
					return false;
				}
			}
			return this.HasPlatform == checkInToFSG.HasPlatform && (!this.HasPlatform || this.Platform.Equals(checkInToFSG.Platform));
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005365 File Offset: 0x00003565
		public void Deserialize(Stream stream)
		{
			CheckInToFSG.Deserialize(stream, this);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000536F File Offset: 0x0000356F
		public static CheckInToFSG Deserialize(Stream stream, CheckInToFSG instance)
		{
			return CheckInToFSG.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000537C File Offset: 0x0000357C
		public static CheckInToFSG DeserializeLengthDelimited(Stream stream)
		{
			CheckInToFSG checkInToFSG = new CheckInToFSG();
			CheckInToFSG.DeserializeLengthDelimited(stream, checkInToFSG);
			return checkInToFSG;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005398 File Offset: 0x00003598
		public static CheckInToFSG DeserializeLengthDelimited(Stream stream, CheckInToFSG instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CheckInToFSG.Deserialize(stream, instance, num);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000053C0 File Offset: 0x000035C0
		public static CheckInToFSG Deserialize(Stream stream, CheckInToFSG instance, long limit)
		{
			if (instance.Bssids == null)
			{
				instance.Bssids = new List<string>();
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
					if (num <= 18)
					{
						if (num == 8)
						{
							instance.FsgId = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 18)
						{
							if (instance.Location == null)
							{
								instance.Location = GPSCoords.DeserializeLengthDelimited(stream);
								continue;
							}
							GPSCoords.DeserializeLengthDelimited(stream, instance.Location);
							continue;
						}
					}
					else
					{
						if (num == 26)
						{
							instance.Bssids.Add(ProtocolParser.ReadString(stream));
							continue;
						}
						if (num == 34)
						{
							if (instance.Platform == null)
							{
								instance.Platform = Platform.DeserializeLengthDelimited(stream);
								continue;
							}
							Platform.DeserializeLengthDelimited(stream, instance.Platform);
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

		// Token: 0x06000101 RID: 257 RVA: 0x000054E2 File Offset: 0x000036E2
		public void Serialize(Stream stream)
		{
			CheckInToFSG.Serialize(stream, this);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000054EC File Offset: 0x000036EC
		public static void Serialize(Stream stream, CheckInToFSG instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.FsgId);
			if (instance.HasLocation)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Location.GetSerializedSize());
				GPSCoords.Serialize(stream, instance.Location);
			}
			if (instance.Bssids.Count > 0)
			{
				foreach (string s in instance.Bssids)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(s));
				}
			}
			if (instance.HasPlatform)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.Platform.GetSerializedSize());
				Platform.Serialize(stream, instance.Platform);
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000055CC File Offset: 0x000037CC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)this.FsgId);
			if (this.HasLocation)
			{
				num += 1U;
				uint serializedSize = this.Location.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.Bssids.Count > 0)
			{
				foreach (string s in this.Bssids)
				{
					num += 1U;
					uint byteCount = (uint)Encoding.UTF8.GetByteCount(s);
					num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
				}
			}
			if (this.HasPlatform)
			{
				num += 1U;
				uint serializedSize2 = this.Platform.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			num += 1U;
			return num;
		}

		// Token: 0x04000047 RID: 71
		public bool HasLocation;

		// Token: 0x04000048 RID: 72
		private GPSCoords _Location;

		// Token: 0x04000049 RID: 73
		private List<string> _Bssids = new List<string>();

		// Token: 0x0400004A RID: 74
		public bool HasPlatform;

		// Token: 0x0400004B RID: 75
		private Platform _Platform;

		// Token: 0x0200054F RID: 1359
		public enum PacketID
		{
			// Token: 0x04001E0E RID: 7694
			ID = 502,
			// Token: 0x04001E0F RID: 7695
			System = 3
		}
	}
}
