using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PegasusShared;

namespace PegasusFSG
{
	// Token: 0x02000018 RID: 24
	public class RequestNearbyFSGs : IProtoBuf
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00004CEA File Offset: 0x00002EEA
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00004CF2 File Offset: 0x00002EF2
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

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00004D05 File Offset: 0x00002F05
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x00004D0D File Offset: 0x00002F0D
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

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00004D16 File Offset: 0x00002F16
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x00004D1E File Offset: 0x00002F1E
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

		// Token: 0x060000E7 RID: 231 RVA: 0x00004D34 File Offset: 0x00002F34
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
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

		// Token: 0x060000E8 RID: 232 RVA: 0x00004DC4 File Offset: 0x00002FC4
		public override bool Equals(object obj)
		{
			RequestNearbyFSGs requestNearbyFSGs = obj as RequestNearbyFSGs;
			if (requestNearbyFSGs == null)
			{
				return false;
			}
			if (this.HasLocation != requestNearbyFSGs.HasLocation || (this.HasLocation && !this.Location.Equals(requestNearbyFSGs.Location)))
			{
				return false;
			}
			if (this.Bssids.Count != requestNearbyFSGs.Bssids.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Bssids.Count; i++)
			{
				if (!this.Bssids[i].Equals(requestNearbyFSGs.Bssids[i]))
				{
					return false;
				}
			}
			return this.HasPlatform == requestNearbyFSGs.HasPlatform && (!this.HasPlatform || this.Platform.Equals(requestNearbyFSGs.Platform));
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004E85 File Offset: 0x00003085
		public void Deserialize(Stream stream)
		{
			RequestNearbyFSGs.Deserialize(stream, this);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004E8F File Offset: 0x0000308F
		public static RequestNearbyFSGs Deserialize(Stream stream, RequestNearbyFSGs instance)
		{
			return RequestNearbyFSGs.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004E9C File Offset: 0x0000309C
		public static RequestNearbyFSGs DeserializeLengthDelimited(Stream stream)
		{
			RequestNearbyFSGs requestNearbyFSGs = new RequestNearbyFSGs();
			RequestNearbyFSGs.DeserializeLengthDelimited(stream, requestNearbyFSGs);
			return requestNearbyFSGs;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004EB8 File Offset: 0x000030B8
		public static RequestNearbyFSGs DeserializeLengthDelimited(Stream stream, RequestNearbyFSGs instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RequestNearbyFSGs.Deserialize(stream, instance, num);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004EE0 File Offset: 0x000030E0
		public static RequestNearbyFSGs Deserialize(Stream stream, RequestNearbyFSGs instance, long limit)
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
				else if (num != 10)
				{
					if (num != 18)
					{
						if (num != 26)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else if (instance.Platform == null)
						{
							instance.Platform = Platform.DeserializeLengthDelimited(stream);
						}
						else
						{
							Platform.DeserializeLengthDelimited(stream, instance.Platform);
						}
					}
					else
					{
						instance.Bssids.Add(ProtocolParser.ReadString(stream));
					}
				}
				else if (instance.Location == null)
				{
					instance.Location = GPSCoords.DeserializeLengthDelimited(stream);
				}
				else
				{
					GPSCoords.DeserializeLengthDelimited(stream, instance.Location);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004FE0 File Offset: 0x000031E0
		public void Serialize(Stream stream)
		{
			RequestNearbyFSGs.Serialize(stream, this);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004FEC File Offset: 0x000031EC
		public static void Serialize(Stream stream, RequestNearbyFSGs instance)
		{
			if (instance.HasLocation)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Location.GetSerializedSize());
				GPSCoords.Serialize(stream, instance.Location);
			}
			if (instance.Bssids.Count > 0)
			{
				foreach (string s in instance.Bssids)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(s));
				}
			}
			if (instance.HasPlatform)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Platform.GetSerializedSize());
				Platform.Serialize(stream, instance.Platform);
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000050BC File Offset: 0x000032BC
		public uint GetSerializedSize()
		{
			uint num = 0U;
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
			return num;
		}

		// Token: 0x04000041 RID: 65
		public bool HasLocation;

		// Token: 0x04000042 RID: 66
		private GPSCoords _Location;

		// Token: 0x04000043 RID: 67
		private List<string> _Bssids = new List<string>();

		// Token: 0x04000044 RID: 68
		public bool HasPlatform;

		// Token: 0x04000045 RID: 69
		private Platform _Platform;

		// Token: 0x0200054E RID: 1358
		public enum PacketID
		{
			// Token: 0x04001E0B RID: 7691
			ID = 501,
			// Token: 0x04001E0C RID: 7692
			System = 3
		}
	}
}
