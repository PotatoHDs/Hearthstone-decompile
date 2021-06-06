using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PegasusShared;

namespace PegasusFSG
{
	// Token: 0x0200001E RID: 30
	public class InnkeeperSetupGathering : IProtoBuf
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00006A75 File Offset: 0x00004C75
		// (set) Token: 0x06000152 RID: 338 RVA: 0x00006A7D File Offset: 0x00004C7D
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

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00006A90 File Offset: 0x00004C90
		// (set) Token: 0x06000154 RID: 340 RVA: 0x00006A98 File Offset: 0x00004C98
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

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00006AA1 File Offset: 0x00004CA1
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00006AA9 File Offset: 0x00004CA9
		public long FsgId { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00006AB2 File Offset: 0x00004CB2
		// (set) Token: 0x06000158 RID: 344 RVA: 0x00006ABA File Offset: 0x00004CBA
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

		// Token: 0x06000159 RID: 345 RVA: 0x00006AD0 File Offset: 0x00004CD0
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
			num ^= this.FsgId.GetHashCode();
			if (this.HasPlatform)
			{
				num ^= this.Platform.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00006B70 File Offset: 0x00004D70
		public override bool Equals(object obj)
		{
			InnkeeperSetupGathering innkeeperSetupGathering = obj as InnkeeperSetupGathering;
			if (innkeeperSetupGathering == null)
			{
				return false;
			}
			if (this.HasLocation != innkeeperSetupGathering.HasLocation || (this.HasLocation && !this.Location.Equals(innkeeperSetupGathering.Location)))
			{
				return false;
			}
			if (this.Bssids.Count != innkeeperSetupGathering.Bssids.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Bssids.Count; i++)
			{
				if (!this.Bssids[i].Equals(innkeeperSetupGathering.Bssids[i]))
				{
					return false;
				}
			}
			return this.FsgId.Equals(innkeeperSetupGathering.FsgId) && this.HasPlatform == innkeeperSetupGathering.HasPlatform && (!this.HasPlatform || this.Platform.Equals(innkeeperSetupGathering.Platform));
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00006C49 File Offset: 0x00004E49
		public void Deserialize(Stream stream)
		{
			InnkeeperSetupGathering.Deserialize(stream, this);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00006C53 File Offset: 0x00004E53
		public static InnkeeperSetupGathering Deserialize(Stream stream, InnkeeperSetupGathering instance)
		{
			return InnkeeperSetupGathering.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00006C60 File Offset: 0x00004E60
		public static InnkeeperSetupGathering DeserializeLengthDelimited(Stream stream)
		{
			InnkeeperSetupGathering innkeeperSetupGathering = new InnkeeperSetupGathering();
			InnkeeperSetupGathering.DeserializeLengthDelimited(stream, innkeeperSetupGathering);
			return innkeeperSetupGathering;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00006C7C File Offset: 0x00004E7C
		public static InnkeeperSetupGathering DeserializeLengthDelimited(Stream stream, InnkeeperSetupGathering instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return InnkeeperSetupGathering.Deserialize(stream, instance, num);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00006CA4 File Offset: 0x00004EA4
		public static InnkeeperSetupGathering Deserialize(Stream stream, InnkeeperSetupGathering instance, long limit)
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
						if (num != 10)
						{
							if (num == 18)
							{
								instance.Bssids.Add(ProtocolParser.ReadString(stream));
								continue;
							}
						}
						else
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
						if (num == 24)
						{
							instance.FsgId = (long)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06000160 RID: 352 RVA: 0x00006DC7 File Offset: 0x00004FC7
		public void Serialize(Stream stream)
		{
			InnkeeperSetupGathering.Serialize(stream, this);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00006DD0 File Offset: 0x00004FD0
		public static void Serialize(Stream stream, InnkeeperSetupGathering instance)
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
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.FsgId);
			if (instance.HasPlatform)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.Platform.GetSerializedSize());
				Platform.Serialize(stream, instance.Platform);
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00006EB4 File Offset: 0x000050B4
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
			num += ProtocolParser.SizeOfUInt64((ulong)this.FsgId);
			if (this.HasPlatform)
			{
				num += 1U;
				uint serializedSize2 = this.Platform.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			num += 1U;
			return num;
		}

		// Token: 0x04000061 RID: 97
		public bool HasLocation;

		// Token: 0x04000062 RID: 98
		private GPSCoords _Location;

		// Token: 0x04000063 RID: 99
		private List<string> _Bssids = new List<string>();

		// Token: 0x04000065 RID: 101
		public bool HasPlatform;

		// Token: 0x04000066 RID: 102
		private Platform _Platform;

		// Token: 0x02000554 RID: 1364
		public enum PacketID
		{
			// Token: 0x04001E1A RID: 7706
			ID = 507,
			// Token: 0x04001E1B RID: 7707
			System = 3
		}
	}
}
