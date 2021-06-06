using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.report.v1
{
	// Token: 0x02000329 RID: 809
	public class CustomReport : IProtoBuf
	{
		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x0600318D RID: 12685 RVA: 0x000A68AC File Offset: 0x000A4AAC
		// (set) Token: 0x0600318E RID: 12686 RVA: 0x000A68B4 File Offset: 0x000A4AB4
		public string Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
				this.HasType = (value != null);
			}
		}

		// Token: 0x0600318F RID: 12687 RVA: 0x000A68C7 File Offset: 0x000A4AC7
		public void SetType(string val)
		{
			this.Type = val;
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06003190 RID: 12688 RVA: 0x000A68D0 File Offset: 0x000A4AD0
		// (set) Token: 0x06003191 RID: 12689 RVA: 0x000A68D8 File Offset: 0x000A4AD8
		public string ProgramId
		{
			get
			{
				return this._ProgramId;
			}
			set
			{
				this._ProgramId = value;
				this.HasProgramId = (value != null);
			}
		}

		// Token: 0x06003192 RID: 12690 RVA: 0x000A68EB File Offset: 0x000A4AEB
		public void SetProgramId(string val)
		{
			this.ProgramId = val;
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06003193 RID: 12691 RVA: 0x000A68F4 File Offset: 0x000A4AF4
		// (set) Token: 0x06003194 RID: 12692 RVA: 0x000A68FC File Offset: 0x000A4AFC
		public List<Attribute> Attribute
		{
			get
			{
				return this._Attribute;
			}
			set
			{
				this._Attribute = value;
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06003195 RID: 12693 RVA: 0x000A68F4 File Offset: 0x000A4AF4
		public List<Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06003196 RID: 12694 RVA: 0x000A6905 File Offset: 0x000A4B05
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x06003197 RID: 12695 RVA: 0x000A6912 File Offset: 0x000A4B12
		public void AddAttribute(Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x06003198 RID: 12696 RVA: 0x000A6920 File Offset: 0x000A4B20
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x06003199 RID: 12697 RVA: 0x000A692D File Offset: 0x000A4B2D
		public void SetAttribute(List<Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x0600319A RID: 12698 RVA: 0x000A6938 File Offset: 0x000A4B38
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasType)
			{
				num ^= this.Type.GetHashCode();
			}
			if (this.HasProgramId)
			{
				num ^= this.ProgramId.GetHashCode();
			}
			foreach (Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600319B RID: 12699 RVA: 0x000A69C8 File Offset: 0x000A4BC8
		public override bool Equals(object obj)
		{
			CustomReport customReport = obj as CustomReport;
			if (customReport == null)
			{
				return false;
			}
			if (this.HasType != customReport.HasType || (this.HasType && !this.Type.Equals(customReport.Type)))
			{
				return false;
			}
			if (this.HasProgramId != customReport.HasProgramId || (this.HasProgramId && !this.ProgramId.Equals(customReport.ProgramId)))
			{
				return false;
			}
			if (this.Attribute.Count != customReport.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(customReport.Attribute[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x0600319C RID: 12700 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600319D RID: 12701 RVA: 0x000A6A89 File Offset: 0x000A4C89
		public static CustomReport ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CustomReport>(bs, 0, -1);
		}

		// Token: 0x0600319E RID: 12702 RVA: 0x000A6A93 File Offset: 0x000A4C93
		public void Deserialize(Stream stream)
		{
			CustomReport.Deserialize(stream, this);
		}

		// Token: 0x0600319F RID: 12703 RVA: 0x000A6A9D File Offset: 0x000A4C9D
		public static CustomReport Deserialize(Stream stream, CustomReport instance)
		{
			return CustomReport.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060031A0 RID: 12704 RVA: 0x000A6AA8 File Offset: 0x000A4CA8
		public static CustomReport DeserializeLengthDelimited(Stream stream)
		{
			CustomReport customReport = new CustomReport();
			CustomReport.DeserializeLengthDelimited(stream, customReport);
			return customReport;
		}

		// Token: 0x060031A1 RID: 12705 RVA: 0x000A6AC4 File Offset: 0x000A4CC4
		public static CustomReport DeserializeLengthDelimited(Stream stream, CustomReport instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CustomReport.Deserialize(stream, instance, num);
		}

		// Token: 0x060031A2 RID: 12706 RVA: 0x000A6AEC File Offset: 0x000A4CEC
		public static CustomReport Deserialize(Stream stream, CustomReport instance, long limit)
		{
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<Attribute>();
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
						else
						{
							instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
						}
					}
					else
					{
						instance.ProgramId = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.Type = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060031A3 RID: 12707 RVA: 0x000A6BB2 File Offset: 0x000A4DB2
		public void Serialize(Stream stream)
		{
			CustomReport.Serialize(stream, this);
		}

		// Token: 0x060031A4 RID: 12708 RVA: 0x000A6BBC File Offset: 0x000A4DBC
		public static void Serialize(Stream stream, CustomReport instance)
		{
			if (instance.HasType)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Type));
			}
			if (instance.HasProgramId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ProgramId));
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.Attribute.Serialize(stream, attribute);
				}
			}
		}

		// Token: 0x060031A5 RID: 12709 RVA: 0x000A6C80 File Offset: 0x000A4E80
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasType)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Type);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasProgramId)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.ProgramId);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.Attribute.Count > 0)
			{
				foreach (Attribute attribute in this.Attribute)
				{
					num += 1U;
					uint serializedSize = attribute.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x040013B0 RID: 5040
		public bool HasType;

		// Token: 0x040013B1 RID: 5041
		private string _Type;

		// Token: 0x040013B2 RID: 5042
		public bool HasProgramId;

		// Token: 0x040013B3 RID: 5043
		private string _ProgramId;

		// Token: 0x040013B4 RID: 5044
		private List<Attribute> _Attribute = new List<Attribute>();
	}
}
