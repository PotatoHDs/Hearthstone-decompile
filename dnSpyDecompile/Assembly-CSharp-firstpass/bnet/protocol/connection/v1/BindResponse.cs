using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.connection.v1
{
	// Token: 0x02000441 RID: 1089
	public class BindResponse : IProtoBuf
	{
		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x060049EE RID: 18926 RVA: 0x000E6D9C File Offset: 0x000E4F9C
		// (set) Token: 0x060049EF RID: 18927 RVA: 0x000E6DA4 File Offset: 0x000E4FA4
		public List<uint> ImportedServiceId
		{
			get
			{
				return this._ImportedServiceId;
			}
			set
			{
				this._ImportedServiceId = value;
			}
		}

		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x060049F0 RID: 18928 RVA: 0x000E6D9C File Offset: 0x000E4F9C
		public List<uint> ImportedServiceIdList
		{
			get
			{
				return this._ImportedServiceId;
			}
		}

		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x060049F1 RID: 18929 RVA: 0x000E6DAD File Offset: 0x000E4FAD
		public int ImportedServiceIdCount
		{
			get
			{
				return this._ImportedServiceId.Count;
			}
		}

		// Token: 0x060049F2 RID: 18930 RVA: 0x000E6DBA File Offset: 0x000E4FBA
		public void AddImportedServiceId(uint val)
		{
			this._ImportedServiceId.Add(val);
		}

		// Token: 0x060049F3 RID: 18931 RVA: 0x000E6DC8 File Offset: 0x000E4FC8
		public void ClearImportedServiceId()
		{
			this._ImportedServiceId.Clear();
		}

		// Token: 0x060049F4 RID: 18932 RVA: 0x000E6DD5 File Offset: 0x000E4FD5
		public void SetImportedServiceId(List<uint> val)
		{
			this.ImportedServiceId = val;
		}

		// Token: 0x060049F5 RID: 18933 RVA: 0x000E6DE0 File Offset: 0x000E4FE0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (uint num2 in this.ImportedServiceId)
			{
				num ^= num2.GetHashCode();
			}
			return num;
		}

		// Token: 0x060049F6 RID: 18934 RVA: 0x000E6E44 File Offset: 0x000E5044
		public override bool Equals(object obj)
		{
			BindResponse bindResponse = obj as BindResponse;
			if (bindResponse == null)
			{
				return false;
			}
			if (this.ImportedServiceId.Count != bindResponse.ImportedServiceId.Count)
			{
				return false;
			}
			for (int i = 0; i < this.ImportedServiceId.Count; i++)
			{
				if (!this.ImportedServiceId[i].Equals(bindResponse.ImportedServiceId[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x060049F7 RID: 18935 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060049F8 RID: 18936 RVA: 0x000E6EB2 File Offset: 0x000E50B2
		public static BindResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<BindResponse>(bs, 0, -1);
		}

		// Token: 0x060049F9 RID: 18937 RVA: 0x000E6EBC File Offset: 0x000E50BC
		public void Deserialize(Stream stream)
		{
			BindResponse.Deserialize(stream, this);
		}

		// Token: 0x060049FA RID: 18938 RVA: 0x000E6EC6 File Offset: 0x000E50C6
		public static BindResponse Deserialize(Stream stream, BindResponse instance)
		{
			return BindResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060049FB RID: 18939 RVA: 0x000E6ED4 File Offset: 0x000E50D4
		public static BindResponse DeserializeLengthDelimited(Stream stream)
		{
			BindResponse bindResponse = new BindResponse();
			BindResponse.DeserializeLengthDelimited(stream, bindResponse);
			return bindResponse;
		}

		// Token: 0x060049FC RID: 18940 RVA: 0x000E6EF0 File Offset: 0x000E50F0
		public static BindResponse DeserializeLengthDelimited(Stream stream, BindResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BindResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x060049FD RID: 18941 RVA: 0x000E6F18 File Offset: 0x000E5118
		public static BindResponse Deserialize(Stream stream, BindResponse instance, long limit)
		{
			if (instance.ImportedServiceId == null)
			{
				instance.ImportedServiceId = new List<uint>();
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
				else if (num == 10)
				{
					long num2 = (long)((ulong)ProtocolParser.ReadUInt32(stream));
					num2 += stream.Position;
					while (stream.Position < num2)
					{
						instance.ImportedServiceId.Add(ProtocolParser.ReadUInt32(stream));
					}
					if (stream.Position != num2)
					{
						throw new ProtocolBufferException("Read too many bytes in packed data");
					}
				}
				else
				{
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

		// Token: 0x060049FE RID: 18942 RVA: 0x000E6FE4 File Offset: 0x000E51E4
		public void Serialize(Stream stream)
		{
			BindResponse.Serialize(stream, this);
		}

		// Token: 0x060049FF RID: 18943 RVA: 0x000E6FF0 File Offset: 0x000E51F0
		public static void Serialize(Stream stream, BindResponse instance)
		{
			if (instance.ImportedServiceId.Count > 0)
			{
				stream.WriteByte(10);
				uint num = 0U;
				foreach (uint val in instance.ImportedServiceId)
				{
					num += ProtocolParser.SizeOfUInt32(val);
				}
				ProtocolParser.WriteUInt32(stream, num);
				foreach (uint val2 in instance.ImportedServiceId)
				{
					ProtocolParser.WriteUInt32(stream, val2);
				}
			}
		}

		// Token: 0x06004A00 RID: 18944 RVA: 0x000E70A8 File Offset: 0x000E52A8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.ImportedServiceId.Count > 0)
			{
				num += 1U;
				uint num2 = num;
				foreach (uint val in this.ImportedServiceId)
				{
					num += ProtocolParser.SizeOfUInt32(val);
				}
				num += ProtocolParser.SizeOfUInt32(num - num2);
			}
			return num;
		}

		// Token: 0x0400184D RID: 6221
		private List<uint> _ImportedServiceId = new List<uint>();
	}
}
