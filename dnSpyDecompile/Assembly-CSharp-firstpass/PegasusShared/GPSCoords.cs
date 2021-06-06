using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x02000164 RID: 356
	public class GPSCoords : IProtoBuf
	{
		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001868 RID: 6248 RVA: 0x00055776 File Offset: 0x00053976
		// (set) Token: 0x06001869 RID: 6249 RVA: 0x0005577E File Offset: 0x0005397E
		public double Latitude { get; set; }

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x0600186A RID: 6250 RVA: 0x00055787 File Offset: 0x00053987
		// (set) Token: 0x0600186B RID: 6251 RVA: 0x0005578F File Offset: 0x0005398F
		public double Longitude
		{
			get
			{
				return this._Longitude;
			}
			set
			{
				this._Longitude = value;
				this.HasLongitude = true;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x0600186C RID: 6252 RVA: 0x0005579F File Offset: 0x0005399F
		// (set) Token: 0x0600186D RID: 6253 RVA: 0x000557A7 File Offset: 0x000539A7
		public double Accuracy
		{
			get
			{
				return this._Accuracy;
			}
			set
			{
				this._Accuracy = value;
				this.HasAccuracy = true;
			}
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x000557B8 File Offset: 0x000539B8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Latitude.GetHashCode();
			if (this.HasLongitude)
			{
				num ^= this.Longitude.GetHashCode();
			}
			if (this.HasAccuracy)
			{
				num ^= this.Accuracy.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x00055818 File Offset: 0x00053A18
		public override bool Equals(object obj)
		{
			GPSCoords gpscoords = obj as GPSCoords;
			return gpscoords != null && this.Latitude.Equals(gpscoords.Latitude) && this.HasLongitude == gpscoords.HasLongitude && (!this.HasLongitude || this.Longitude.Equals(gpscoords.Longitude)) && this.HasAccuracy == gpscoords.HasAccuracy && (!this.HasAccuracy || this.Accuracy.Equals(gpscoords.Accuracy));
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x000558A6 File Offset: 0x00053AA6
		public void Deserialize(Stream stream)
		{
			GPSCoords.Deserialize(stream, this);
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x000558B0 File Offset: 0x00053AB0
		public static GPSCoords Deserialize(Stream stream, GPSCoords instance)
		{
			return GPSCoords.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x000558BC File Offset: 0x00053ABC
		public static GPSCoords DeserializeLengthDelimited(Stream stream)
		{
			GPSCoords gpscoords = new GPSCoords();
			GPSCoords.DeserializeLengthDelimited(stream, gpscoords);
			return gpscoords;
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x000558D8 File Offset: 0x00053AD8
		public static GPSCoords DeserializeLengthDelimited(Stream stream, GPSCoords instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GPSCoords.Deserialize(stream, instance, num);
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x00055900 File Offset: 0x00053B00
		public static GPSCoords Deserialize(Stream stream, GPSCoords instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
				else if (num != 9)
				{
					if (num != 17)
					{
						if (num != 25)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else
						{
							instance.Accuracy = binaryReader.ReadDouble();
						}
					}
					else
					{
						instance.Longitude = binaryReader.ReadDouble();
					}
				}
				else
				{
					instance.Latitude = binaryReader.ReadDouble();
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x000559B5 File Offset: 0x00053BB5
		public void Serialize(Stream stream)
		{
			GPSCoords.Serialize(stream, this);
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x000559C0 File Offset: 0x00053BC0
		public static void Serialize(Stream stream, GPSCoords instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(9);
			binaryWriter.Write(instance.Latitude);
			if (instance.HasLongitude)
			{
				stream.WriteByte(17);
				binaryWriter.Write(instance.Longitude);
			}
			if (instance.HasAccuracy)
			{
				stream.WriteByte(25);
				binaryWriter.Write(instance.Accuracy);
			}
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x00055A20 File Offset: 0x00053C20
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += 8U;
			if (this.HasLongitude)
			{
				num += 1U;
				num += 8U;
			}
			if (this.HasAccuracy)
			{
				num += 1U;
				num += 8U;
			}
			return num + 1U;
		}

		// Token: 0x040007D0 RID: 2000
		public bool HasLongitude;

		// Token: 0x040007D1 RID: 2001
		private double _Longitude;

		// Token: 0x040007D2 RID: 2002
		public bool HasAccuracy;

		// Token: 0x040007D3 RID: 2003
		private double _Accuracy;
	}
}
