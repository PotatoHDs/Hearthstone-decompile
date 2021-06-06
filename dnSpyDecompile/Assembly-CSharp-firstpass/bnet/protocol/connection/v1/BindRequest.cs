using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.connection.v1
{
	// Token: 0x02000440 RID: 1088
	public class BindRequest : IProtoBuf
	{
		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x060049C5 RID: 18885 RVA: 0x000E647E File Offset: 0x000E467E
		// (set) Token: 0x060049C6 RID: 18886 RVA: 0x000E6486 File Offset: 0x000E4686
		public List<uint> DeprecatedImportedServiceHash
		{
			get
			{
				return this._DeprecatedImportedServiceHash;
			}
			set
			{
				this._DeprecatedImportedServiceHash = value;
			}
		}

		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x060049C7 RID: 18887 RVA: 0x000E647E File Offset: 0x000E467E
		public List<uint> DeprecatedImportedServiceHashList
		{
			get
			{
				return this._DeprecatedImportedServiceHash;
			}
		}

		// Token: 0x17000DC0 RID: 3520
		// (get) Token: 0x060049C8 RID: 18888 RVA: 0x000E648F File Offset: 0x000E468F
		public int DeprecatedImportedServiceHashCount
		{
			get
			{
				return this._DeprecatedImportedServiceHash.Count;
			}
		}

		// Token: 0x060049C9 RID: 18889 RVA: 0x000E649C File Offset: 0x000E469C
		public void AddDeprecatedImportedServiceHash(uint val)
		{
			this._DeprecatedImportedServiceHash.Add(val);
		}

		// Token: 0x060049CA RID: 18890 RVA: 0x000E64AA File Offset: 0x000E46AA
		public void ClearDeprecatedImportedServiceHash()
		{
			this._DeprecatedImportedServiceHash.Clear();
		}

		// Token: 0x060049CB RID: 18891 RVA: 0x000E64B7 File Offset: 0x000E46B7
		public void SetDeprecatedImportedServiceHash(List<uint> val)
		{
			this.DeprecatedImportedServiceHash = val;
		}

		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x060049CC RID: 18892 RVA: 0x000E64C0 File Offset: 0x000E46C0
		// (set) Token: 0x060049CD RID: 18893 RVA: 0x000E64C8 File Offset: 0x000E46C8
		public List<BoundService> DeprecatedExportedService
		{
			get
			{
				return this._DeprecatedExportedService;
			}
			set
			{
				this._DeprecatedExportedService = value;
			}
		}

		// Token: 0x17000DC2 RID: 3522
		// (get) Token: 0x060049CE RID: 18894 RVA: 0x000E64C0 File Offset: 0x000E46C0
		public List<BoundService> DeprecatedExportedServiceList
		{
			get
			{
				return this._DeprecatedExportedService;
			}
		}

		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x060049CF RID: 18895 RVA: 0x000E64D1 File Offset: 0x000E46D1
		public int DeprecatedExportedServiceCount
		{
			get
			{
				return this._DeprecatedExportedService.Count;
			}
		}

		// Token: 0x060049D0 RID: 18896 RVA: 0x000E64DE File Offset: 0x000E46DE
		public void AddDeprecatedExportedService(BoundService val)
		{
			this._DeprecatedExportedService.Add(val);
		}

		// Token: 0x060049D1 RID: 18897 RVA: 0x000E64EC File Offset: 0x000E46EC
		public void ClearDeprecatedExportedService()
		{
			this._DeprecatedExportedService.Clear();
		}

		// Token: 0x060049D2 RID: 18898 RVA: 0x000E64F9 File Offset: 0x000E46F9
		public void SetDeprecatedExportedService(List<BoundService> val)
		{
			this.DeprecatedExportedService = val;
		}

		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x060049D3 RID: 18899 RVA: 0x000E6502 File Offset: 0x000E4702
		// (set) Token: 0x060049D4 RID: 18900 RVA: 0x000E650A File Offset: 0x000E470A
		public List<BoundService> ExportedService
		{
			get
			{
				return this._ExportedService;
			}
			set
			{
				this._ExportedService = value;
			}
		}

		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x060049D5 RID: 18901 RVA: 0x000E6502 File Offset: 0x000E4702
		public List<BoundService> ExportedServiceList
		{
			get
			{
				return this._ExportedService;
			}
		}

		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x060049D6 RID: 18902 RVA: 0x000E6513 File Offset: 0x000E4713
		public int ExportedServiceCount
		{
			get
			{
				return this._ExportedService.Count;
			}
		}

		// Token: 0x060049D7 RID: 18903 RVA: 0x000E6520 File Offset: 0x000E4720
		public void AddExportedService(BoundService val)
		{
			this._ExportedService.Add(val);
		}

		// Token: 0x060049D8 RID: 18904 RVA: 0x000E652E File Offset: 0x000E472E
		public void ClearExportedService()
		{
			this._ExportedService.Clear();
		}

		// Token: 0x060049D9 RID: 18905 RVA: 0x000E653B File Offset: 0x000E473B
		public void SetExportedService(List<BoundService> val)
		{
			this.ExportedService = val;
		}

		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x060049DA RID: 18906 RVA: 0x000E6544 File Offset: 0x000E4744
		// (set) Token: 0x060049DB RID: 18907 RVA: 0x000E654C File Offset: 0x000E474C
		public List<BoundService> ImportedService
		{
			get
			{
				return this._ImportedService;
			}
			set
			{
				this._ImportedService = value;
			}
		}

		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x060049DC RID: 18908 RVA: 0x000E6544 File Offset: 0x000E4744
		public List<BoundService> ImportedServiceList
		{
			get
			{
				return this._ImportedService;
			}
		}

		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x060049DD RID: 18909 RVA: 0x000E6555 File Offset: 0x000E4755
		public int ImportedServiceCount
		{
			get
			{
				return this._ImportedService.Count;
			}
		}

		// Token: 0x060049DE RID: 18910 RVA: 0x000E6562 File Offset: 0x000E4762
		public void AddImportedService(BoundService val)
		{
			this._ImportedService.Add(val);
		}

		// Token: 0x060049DF RID: 18911 RVA: 0x000E6570 File Offset: 0x000E4770
		public void ClearImportedService()
		{
			this._ImportedService.Clear();
		}

		// Token: 0x060049E0 RID: 18912 RVA: 0x000E657D File Offset: 0x000E477D
		public void SetImportedService(List<BoundService> val)
		{
			this.ImportedService = val;
		}

		// Token: 0x060049E1 RID: 18913 RVA: 0x000E6588 File Offset: 0x000E4788
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (uint num2 in this.DeprecatedImportedServiceHash)
			{
				num ^= num2.GetHashCode();
			}
			foreach (BoundService boundService in this.DeprecatedExportedService)
			{
				num ^= boundService.GetHashCode();
			}
			foreach (BoundService boundService2 in this.ExportedService)
			{
				num ^= boundService2.GetHashCode();
			}
			foreach (BoundService boundService3 in this.ImportedService)
			{
				num ^= boundService3.GetHashCode();
			}
			return num;
		}

		// Token: 0x060049E2 RID: 18914 RVA: 0x000E66C0 File Offset: 0x000E48C0
		public override bool Equals(object obj)
		{
			BindRequest bindRequest = obj as BindRequest;
			if (bindRequest == null)
			{
				return false;
			}
			if (this.DeprecatedImportedServiceHash.Count != bindRequest.DeprecatedImportedServiceHash.Count)
			{
				return false;
			}
			for (int i = 0; i < this.DeprecatedImportedServiceHash.Count; i++)
			{
				if (!this.DeprecatedImportedServiceHash[i].Equals(bindRequest.DeprecatedImportedServiceHash[i]))
				{
					return false;
				}
			}
			if (this.DeprecatedExportedService.Count != bindRequest.DeprecatedExportedService.Count)
			{
				return false;
			}
			for (int j = 0; j < this.DeprecatedExportedService.Count; j++)
			{
				if (!this.DeprecatedExportedService[j].Equals(bindRequest.DeprecatedExportedService[j]))
				{
					return false;
				}
			}
			if (this.ExportedService.Count != bindRequest.ExportedService.Count)
			{
				return false;
			}
			for (int k = 0; k < this.ExportedService.Count; k++)
			{
				if (!this.ExportedService[k].Equals(bindRequest.ExportedService[k]))
				{
					return false;
				}
			}
			if (this.ImportedService.Count != bindRequest.ImportedService.Count)
			{
				return false;
			}
			for (int l = 0; l < this.ImportedService.Count; l++)
			{
				if (!this.ImportedService[l].Equals(bindRequest.ImportedService[l]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x060049E3 RID: 18915 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060049E4 RID: 18916 RVA: 0x000E682D File Offset: 0x000E4A2D
		public static BindRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<BindRequest>(bs, 0, -1);
		}

		// Token: 0x060049E5 RID: 18917 RVA: 0x000E6837 File Offset: 0x000E4A37
		public void Deserialize(Stream stream)
		{
			BindRequest.Deserialize(stream, this);
		}

		// Token: 0x060049E6 RID: 18918 RVA: 0x000E6841 File Offset: 0x000E4A41
		public static BindRequest Deserialize(Stream stream, BindRequest instance)
		{
			return BindRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060049E7 RID: 18919 RVA: 0x000E684C File Offset: 0x000E4A4C
		public static BindRequest DeserializeLengthDelimited(Stream stream)
		{
			BindRequest bindRequest = new BindRequest();
			BindRequest.DeserializeLengthDelimited(stream, bindRequest);
			return bindRequest;
		}

		// Token: 0x060049E8 RID: 18920 RVA: 0x000E6868 File Offset: 0x000E4A68
		public static BindRequest DeserializeLengthDelimited(Stream stream, BindRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BindRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060049E9 RID: 18921 RVA: 0x000E6890 File Offset: 0x000E4A90
		public static BindRequest Deserialize(Stream stream, BindRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.DeprecatedImportedServiceHash == null)
			{
				instance.DeprecatedImportedServiceHash = new List<uint>();
			}
			if (instance.DeprecatedExportedService == null)
			{
				instance.DeprecatedExportedService = new List<BoundService>();
			}
			if (instance.ExportedService == null)
			{
				instance.ExportedService = new List<BoundService>();
			}
			if (instance.ImportedService == null)
			{
				instance.ImportedService = new List<BoundService>();
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
								instance.DeprecatedExportedService.Add(BoundService.DeserializeLengthDelimited(stream));
								continue;
							}
						}
						else
						{
							long num2 = (long)((ulong)ProtocolParser.ReadUInt32(stream));
							num2 += stream.Position;
							while (stream.Position < num2)
							{
								instance.DeprecatedImportedServiceHash.Add(binaryReader.ReadUInt32());
							}
							if (stream.Position != num2)
							{
								throw new ProtocolBufferException("Read too many bytes in packed data");
							}
							continue;
						}
					}
					else
					{
						if (num == 26)
						{
							instance.ExportedService.Add(BoundService.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 34)
						{
							instance.ImportedService.Add(BoundService.DeserializeLengthDelimited(stream));
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

		// Token: 0x060049EA RID: 18922 RVA: 0x000E6A04 File Offset: 0x000E4C04
		public void Serialize(Stream stream)
		{
			BindRequest.Serialize(stream, this);
		}

		// Token: 0x060049EB RID: 18923 RVA: 0x000E6A10 File Offset: 0x000E4C10
		public static void Serialize(Stream stream, BindRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.DeprecatedImportedServiceHash.Count > 0)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, (uint)(4 * instance.DeprecatedImportedServiceHash.Count));
				foreach (uint value in instance.DeprecatedImportedServiceHash)
				{
					binaryWriter.Write(value);
				}
			}
			if (instance.DeprecatedExportedService.Count > 0)
			{
				foreach (BoundService boundService in instance.DeprecatedExportedService)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, boundService.GetSerializedSize());
					BoundService.Serialize(stream, boundService);
				}
			}
			if (instance.ExportedService.Count > 0)
			{
				foreach (BoundService boundService2 in instance.ExportedService)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, boundService2.GetSerializedSize());
					BoundService.Serialize(stream, boundService2);
				}
			}
			if (instance.ImportedService.Count > 0)
			{
				foreach (BoundService boundService3 in instance.ImportedService)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, boundService3.GetSerializedSize());
					BoundService.Serialize(stream, boundService3);
				}
			}
		}

		// Token: 0x060049EC RID: 18924 RVA: 0x000E6BC8 File Offset: 0x000E4DC8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.DeprecatedImportedServiceHash.Count > 0)
			{
				num += 1U;
				uint num2 = num;
				foreach (uint num3 in this.DeprecatedImportedServiceHash)
				{
					num += 4U;
				}
				num += ProtocolParser.SizeOfUInt32(num - num2);
			}
			if (this.DeprecatedExportedService.Count > 0)
			{
				foreach (BoundService boundService in this.DeprecatedExportedService)
				{
					num += 1U;
					uint serializedSize = boundService.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.ExportedService.Count > 0)
			{
				foreach (BoundService boundService2 in this.ExportedService)
				{
					num += 1U;
					uint serializedSize2 = boundService2.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.ImportedService.Count > 0)
			{
				foreach (BoundService boundService3 in this.ImportedService)
				{
					num += 1U;
					uint serializedSize3 = boundService3.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			return num;
		}

		// Token: 0x04001849 RID: 6217
		private List<uint> _DeprecatedImportedServiceHash = new List<uint>();

		// Token: 0x0400184A RID: 6218
		private List<BoundService> _DeprecatedExportedService = new List<BoundService>();

		// Token: 0x0400184B RID: 6219
		private List<BoundService> _ExportedService = new List<BoundService>();

		// Token: 0x0400184C RID: 6220
		private List<BoundService> _ImportedService = new List<BoundService>();
	}
}
