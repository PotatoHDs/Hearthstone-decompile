using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.account.v1
{
	// Token: 0x0200053E RID: 1342
	public class AuthorizedData : IProtoBuf
	{
		// Token: 0x1700125D RID: 4701
		// (get) Token: 0x060060DC RID: 24796 RVA: 0x00125306 File Offset: 0x00123506
		// (set) Token: 0x060060DD RID: 24797 RVA: 0x0012530E File Offset: 0x0012350E
		public string Data
		{
			get
			{
				return this._Data;
			}
			set
			{
				this._Data = value;
				this.HasData = (value != null);
			}
		}

		// Token: 0x060060DE RID: 24798 RVA: 0x00125321 File Offset: 0x00123521
		public void SetData(string val)
		{
			this.Data = val;
		}

		// Token: 0x1700125E RID: 4702
		// (get) Token: 0x060060DF RID: 24799 RVA: 0x0012532A File Offset: 0x0012352A
		// (set) Token: 0x060060E0 RID: 24800 RVA: 0x00125332 File Offset: 0x00123532
		public List<uint> License
		{
			get
			{
				return this._License;
			}
			set
			{
				this._License = value;
			}
		}

		// Token: 0x1700125F RID: 4703
		// (get) Token: 0x060060E1 RID: 24801 RVA: 0x0012532A File Offset: 0x0012352A
		public List<uint> LicenseList
		{
			get
			{
				return this._License;
			}
		}

		// Token: 0x17001260 RID: 4704
		// (get) Token: 0x060060E2 RID: 24802 RVA: 0x0012533B File Offset: 0x0012353B
		public int LicenseCount
		{
			get
			{
				return this._License.Count;
			}
		}

		// Token: 0x060060E3 RID: 24803 RVA: 0x00125348 File Offset: 0x00123548
		public void AddLicense(uint val)
		{
			this._License.Add(val);
		}

		// Token: 0x060060E4 RID: 24804 RVA: 0x00125356 File Offset: 0x00123556
		public void ClearLicense()
		{
			this._License.Clear();
		}

		// Token: 0x060060E5 RID: 24805 RVA: 0x00125363 File Offset: 0x00123563
		public void SetLicense(List<uint> val)
		{
			this.License = val;
		}

		// Token: 0x060060E6 RID: 24806 RVA: 0x0012536C File Offset: 0x0012356C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasData)
			{
				num ^= this.Data.GetHashCode();
			}
			foreach (uint num2 in this.License)
			{
				num ^= num2.GetHashCode();
			}
			return num;
		}

		// Token: 0x060060E7 RID: 24807 RVA: 0x001253E8 File Offset: 0x001235E8
		public override bool Equals(object obj)
		{
			AuthorizedData authorizedData = obj as AuthorizedData;
			if (authorizedData == null)
			{
				return false;
			}
			if (this.HasData != authorizedData.HasData || (this.HasData && !this.Data.Equals(authorizedData.Data)))
			{
				return false;
			}
			if (this.License.Count != authorizedData.License.Count)
			{
				return false;
			}
			for (int i = 0; i < this.License.Count; i++)
			{
				if (!this.License[i].Equals(authorizedData.License[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17001261 RID: 4705
		// (get) Token: 0x060060E8 RID: 24808 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060060E9 RID: 24809 RVA: 0x00125481 File Offset: 0x00123681
		public static AuthorizedData ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AuthorizedData>(bs, 0, -1);
		}

		// Token: 0x060060EA RID: 24810 RVA: 0x0012548B File Offset: 0x0012368B
		public void Deserialize(Stream stream)
		{
			AuthorizedData.Deserialize(stream, this);
		}

		// Token: 0x060060EB RID: 24811 RVA: 0x00125495 File Offset: 0x00123695
		public static AuthorizedData Deserialize(Stream stream, AuthorizedData instance)
		{
			return AuthorizedData.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060060EC RID: 24812 RVA: 0x001254A0 File Offset: 0x001236A0
		public static AuthorizedData DeserializeLengthDelimited(Stream stream)
		{
			AuthorizedData authorizedData = new AuthorizedData();
			AuthorizedData.DeserializeLengthDelimited(stream, authorizedData);
			return authorizedData;
		}

		// Token: 0x060060ED RID: 24813 RVA: 0x001254BC File Offset: 0x001236BC
		public static AuthorizedData DeserializeLengthDelimited(Stream stream, AuthorizedData instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AuthorizedData.Deserialize(stream, instance, num);
		}

		// Token: 0x060060EE RID: 24814 RVA: 0x001254E4 File Offset: 0x001236E4
		public static AuthorizedData Deserialize(Stream stream, AuthorizedData instance, long limit)
		{
			if (instance.License == null)
			{
				instance.License = new List<uint>();
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
					if (num != 16)
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
						instance.License.Add(ProtocolParser.ReadUInt32(stream));
					}
				}
				else
				{
					instance.Data = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060060EF RID: 24815 RVA: 0x00125594 File Offset: 0x00123794
		public void Serialize(Stream stream)
		{
			AuthorizedData.Serialize(stream, this);
		}

		// Token: 0x060060F0 RID: 24816 RVA: 0x001255A0 File Offset: 0x001237A0
		public static void Serialize(Stream stream, AuthorizedData instance)
		{
			if (instance.HasData)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Data));
			}
			if (instance.License.Count > 0)
			{
				foreach (uint val in instance.License)
				{
					stream.WriteByte(16);
					ProtocolParser.WriteUInt32(stream, val);
				}
			}
		}

		// Token: 0x060060F1 RID: 24817 RVA: 0x00125630 File Offset: 0x00123830
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasData)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Data);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.License.Count > 0)
			{
				foreach (uint val in this.License)
				{
					num += 1U;
					num += ProtocolParser.SizeOfUInt32(val);
				}
			}
			return num;
		}

		// Token: 0x04001DCF RID: 7631
		public bool HasData;

		// Token: 0x04001DD0 RID: 7632
		private string _Data;

		// Token: 0x04001DD1 RID: 7633
		private List<uint> _License = new List<uint>();
	}
}
