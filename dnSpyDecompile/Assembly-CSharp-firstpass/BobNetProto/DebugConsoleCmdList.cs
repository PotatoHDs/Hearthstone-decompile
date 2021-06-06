using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BobNetProto
{
	// Token: 0x020001DF RID: 479
	public class DebugConsoleCmdList : IProtoBuf
	{
		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001E6D RID: 7789 RVA: 0x0006A72E File Offset: 0x0006892E
		// (set) Token: 0x06001E6E RID: 7790 RVA: 0x0006A736 File Offset: 0x00068936
		public List<DebugConsoleCmdList.DebugConsoleCmd> Commands
		{
			get
			{
				return this._Commands;
			}
			set
			{
				this._Commands = value;
			}
		}

		// Token: 0x06001E6F RID: 7791 RVA: 0x0006A740 File Offset: 0x00068940
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (DebugConsoleCmdList.DebugConsoleCmd debugConsoleCmd in this.Commands)
			{
				num ^= debugConsoleCmd.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001E70 RID: 7792 RVA: 0x0006A7A4 File Offset: 0x000689A4
		public override bool Equals(object obj)
		{
			DebugConsoleCmdList debugConsoleCmdList = obj as DebugConsoleCmdList;
			if (debugConsoleCmdList == null)
			{
				return false;
			}
			if (this.Commands.Count != debugConsoleCmdList.Commands.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Commands.Count; i++)
			{
				if (!this.Commands[i].Equals(debugConsoleCmdList.Commands[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001E71 RID: 7793 RVA: 0x0006A80F File Offset: 0x00068A0F
		public void Deserialize(Stream stream)
		{
			DebugConsoleCmdList.Deserialize(stream, this);
		}

		// Token: 0x06001E72 RID: 7794 RVA: 0x0006A819 File Offset: 0x00068A19
		public static DebugConsoleCmdList Deserialize(Stream stream, DebugConsoleCmdList instance)
		{
			return DebugConsoleCmdList.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001E73 RID: 7795 RVA: 0x0006A824 File Offset: 0x00068A24
		public static DebugConsoleCmdList DeserializeLengthDelimited(Stream stream)
		{
			DebugConsoleCmdList debugConsoleCmdList = new DebugConsoleCmdList();
			DebugConsoleCmdList.DeserializeLengthDelimited(stream, debugConsoleCmdList);
			return debugConsoleCmdList;
		}

		// Token: 0x06001E74 RID: 7796 RVA: 0x0006A840 File Offset: 0x00068A40
		public static DebugConsoleCmdList DeserializeLengthDelimited(Stream stream, DebugConsoleCmdList instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DebugConsoleCmdList.Deserialize(stream, instance, num);
		}

		// Token: 0x06001E75 RID: 7797 RVA: 0x0006A868 File Offset: 0x00068A68
		public static DebugConsoleCmdList Deserialize(Stream stream, DebugConsoleCmdList instance, long limit)
		{
			if (instance.Commands == null)
			{
				instance.Commands = new List<DebugConsoleCmdList.DebugConsoleCmd>();
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
					instance.Commands.Add(DebugConsoleCmdList.DebugConsoleCmd.DeserializeLengthDelimited(stream));
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

		// Token: 0x06001E76 RID: 7798 RVA: 0x0006A900 File Offset: 0x00068B00
		public void Serialize(Stream stream)
		{
			DebugConsoleCmdList.Serialize(stream, this);
		}

		// Token: 0x06001E77 RID: 7799 RVA: 0x0006A90C File Offset: 0x00068B0C
		public static void Serialize(Stream stream, DebugConsoleCmdList instance)
		{
			if (instance.Commands.Count > 0)
			{
				foreach (DebugConsoleCmdList.DebugConsoleCmd debugConsoleCmd in instance.Commands)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, debugConsoleCmd.GetSerializedSize());
					DebugConsoleCmdList.DebugConsoleCmd.Serialize(stream, debugConsoleCmd);
				}
			}
		}

		// Token: 0x06001E78 RID: 7800 RVA: 0x0006A984 File Offset: 0x00068B84
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Commands.Count > 0)
			{
				foreach (DebugConsoleCmdList.DebugConsoleCmd debugConsoleCmd in this.Commands)
				{
					num += 1U;
					uint serializedSize = debugConsoleCmd.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x04000AEA RID: 2794
		private List<DebugConsoleCmdList.DebugConsoleCmd> _Commands = new List<DebugConsoleCmdList.DebugConsoleCmd>();

		// Token: 0x02000669 RID: 1641
		public enum PacketID
		{
			// Token: 0x0400216C RID: 8556
			ID = 146
		}

		// Token: 0x0200066A RID: 1642
		public class DebugConsoleCmd : IProtoBuf
		{
			// Token: 0x1700127F RID: 4735
			// (get) Token: 0x06006188 RID: 24968 RVA: 0x001268EC File Offset: 0x00124AEC
			// (set) Token: 0x06006189 RID: 24969 RVA: 0x001268F4 File Offset: 0x00124AF4
			public string Name { get; set; }

			// Token: 0x17001280 RID: 4736
			// (get) Token: 0x0600618A RID: 24970 RVA: 0x001268FD File Offset: 0x00124AFD
			// (set) Token: 0x0600618B RID: 24971 RVA: 0x00126905 File Offset: 0x00124B05
			public List<DebugConsoleCmdList.DebugConsoleCmd.DebugConsoleCmdParam> Params
			{
				get
				{
					return this._Params;
				}
				set
				{
					this._Params = value;
				}
			}

			// Token: 0x0600618C RID: 24972 RVA: 0x00126910 File Offset: 0x00124B10
			public override int GetHashCode()
			{
				int num = base.GetType().GetHashCode();
				num ^= this.Name.GetHashCode();
				foreach (DebugConsoleCmdList.DebugConsoleCmd.DebugConsoleCmdParam debugConsoleCmdParam in this.Params)
				{
					num ^= debugConsoleCmdParam.GetHashCode();
				}
				return num;
			}

			// Token: 0x0600618D RID: 24973 RVA: 0x00126980 File Offset: 0x00124B80
			public override bool Equals(object obj)
			{
				DebugConsoleCmdList.DebugConsoleCmd debugConsoleCmd = obj as DebugConsoleCmdList.DebugConsoleCmd;
				if (debugConsoleCmd == null)
				{
					return false;
				}
				if (!this.Name.Equals(debugConsoleCmd.Name))
				{
					return false;
				}
				if (this.Params.Count != debugConsoleCmd.Params.Count)
				{
					return false;
				}
				for (int i = 0; i < this.Params.Count; i++)
				{
					if (!this.Params[i].Equals(debugConsoleCmd.Params[i]))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x0600618E RID: 24974 RVA: 0x00126A00 File Offset: 0x00124C00
			public void Deserialize(Stream stream)
			{
				DebugConsoleCmdList.DebugConsoleCmd.Deserialize(stream, this);
			}

			// Token: 0x0600618F RID: 24975 RVA: 0x00126A0A File Offset: 0x00124C0A
			public static DebugConsoleCmdList.DebugConsoleCmd Deserialize(Stream stream, DebugConsoleCmdList.DebugConsoleCmd instance)
			{
				return DebugConsoleCmdList.DebugConsoleCmd.Deserialize(stream, instance, -1L);
			}

			// Token: 0x06006190 RID: 24976 RVA: 0x00126A18 File Offset: 0x00124C18
			public static DebugConsoleCmdList.DebugConsoleCmd DeserializeLengthDelimited(Stream stream)
			{
				DebugConsoleCmdList.DebugConsoleCmd debugConsoleCmd = new DebugConsoleCmdList.DebugConsoleCmd();
				DebugConsoleCmdList.DebugConsoleCmd.DeserializeLengthDelimited(stream, debugConsoleCmd);
				return debugConsoleCmd;
			}

			// Token: 0x06006191 RID: 24977 RVA: 0x00126A34 File Offset: 0x00124C34
			public static DebugConsoleCmdList.DebugConsoleCmd DeserializeLengthDelimited(Stream stream, DebugConsoleCmdList.DebugConsoleCmd instance)
			{
				long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
				num += stream.Position;
				return DebugConsoleCmdList.DebugConsoleCmd.Deserialize(stream, instance, num);
			}

			// Token: 0x06006192 RID: 24978 RVA: 0x00126A5C File Offset: 0x00124C5C
			public static DebugConsoleCmdList.DebugConsoleCmd Deserialize(Stream stream, DebugConsoleCmdList.DebugConsoleCmd instance, long limit)
			{
				if (instance.Params == null)
				{
					instance.Params = new List<DebugConsoleCmdList.DebugConsoleCmd.DebugConsoleCmdParam>();
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
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else
						{
							instance.Params.Add(DebugConsoleCmdList.DebugConsoleCmd.DebugConsoleCmdParam.DeserializeLengthDelimited(stream));
						}
					}
					else
					{
						instance.Name = ProtocolParser.ReadString(stream);
					}
				}
				if (stream.Position != limit)
				{
					throw new ProtocolBufferException("Read past max limit");
				}
				return instance;
			}

			// Token: 0x06006193 RID: 24979 RVA: 0x00126B0C File Offset: 0x00124D0C
			public void Serialize(Stream stream)
			{
				DebugConsoleCmdList.DebugConsoleCmd.Serialize(stream, this);
			}

			// Token: 0x06006194 RID: 24980 RVA: 0x00126B18 File Offset: 0x00124D18
			public static void Serialize(Stream stream, DebugConsoleCmdList.DebugConsoleCmd instance)
			{
				if (instance.Name == null)
				{
					throw new ArgumentNullException("Name", "Required by proto specification.");
				}
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
				if (instance.Params.Count > 0)
				{
					foreach (DebugConsoleCmdList.DebugConsoleCmd.DebugConsoleCmdParam debugConsoleCmdParam in instance.Params)
					{
						stream.WriteByte(18);
						ProtocolParser.WriteUInt32(stream, debugConsoleCmdParam.GetSerializedSize());
						DebugConsoleCmdList.DebugConsoleCmd.DebugConsoleCmdParam.Serialize(stream, debugConsoleCmdParam);
					}
				}
			}

			// Token: 0x06006195 RID: 24981 RVA: 0x00126BC4 File Offset: 0x00124DC4
			public uint GetSerializedSize()
			{
				uint num = 0U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
				if (this.Params.Count > 0)
				{
					foreach (DebugConsoleCmdList.DebugConsoleCmd.DebugConsoleCmdParam debugConsoleCmdParam in this.Params)
					{
						num += 1U;
						uint serializedSize = debugConsoleCmdParam.GetSerializedSize();
						num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
					}
				}
				num += 1U;
				return num;
			}

			// Token: 0x0400216E RID: 8558
			private List<DebugConsoleCmdList.DebugConsoleCmd.DebugConsoleCmdParam> _Params = new List<DebugConsoleCmdList.DebugConsoleCmd.DebugConsoleCmdParam>();

			// Token: 0x02000709 RID: 1801
			public class DebugConsoleCmdParam : IProtoBuf
			{
				// Token: 0x170012C5 RID: 4805
				// (get) Token: 0x06006359 RID: 25433 RVA: 0x0012932C File Offset: 0x0012752C
				// (set) Token: 0x0600635A RID: 25434 RVA: 0x00129334 File Offset: 0x00127534
				public string ParamType { get; set; }

				// Token: 0x170012C6 RID: 4806
				// (get) Token: 0x0600635B RID: 25435 RVA: 0x0012933D File Offset: 0x0012753D
				// (set) Token: 0x0600635C RID: 25436 RVA: 0x00129345 File Offset: 0x00127545
				public string ParamName { get; set; }

				// Token: 0x0600635D RID: 25437 RVA: 0x0012934E File Offset: 0x0012754E
				public override int GetHashCode()
				{
					return base.GetType().GetHashCode() ^ this.ParamType.GetHashCode() ^ this.ParamName.GetHashCode();
				}

				// Token: 0x0600635E RID: 25438 RVA: 0x00129374 File Offset: 0x00127574
				public override bool Equals(object obj)
				{
					DebugConsoleCmdList.DebugConsoleCmd.DebugConsoleCmdParam debugConsoleCmdParam = obj as DebugConsoleCmdList.DebugConsoleCmd.DebugConsoleCmdParam;
					return debugConsoleCmdParam != null && this.ParamType.Equals(debugConsoleCmdParam.ParamType) && this.ParamName.Equals(debugConsoleCmdParam.ParamName);
				}

				// Token: 0x0600635F RID: 25439 RVA: 0x001293B8 File Offset: 0x001275B8
				public void Deserialize(Stream stream)
				{
					DebugConsoleCmdList.DebugConsoleCmd.DebugConsoleCmdParam.Deserialize(stream, this);
				}

				// Token: 0x06006360 RID: 25440 RVA: 0x001293C2 File Offset: 0x001275C2
				public static DebugConsoleCmdList.DebugConsoleCmd.DebugConsoleCmdParam Deserialize(Stream stream, DebugConsoleCmdList.DebugConsoleCmd.DebugConsoleCmdParam instance)
				{
					return DebugConsoleCmdList.DebugConsoleCmd.DebugConsoleCmdParam.Deserialize(stream, instance, -1L);
				}

				// Token: 0x06006361 RID: 25441 RVA: 0x001293D0 File Offset: 0x001275D0
				public static DebugConsoleCmdList.DebugConsoleCmd.DebugConsoleCmdParam DeserializeLengthDelimited(Stream stream)
				{
					DebugConsoleCmdList.DebugConsoleCmd.DebugConsoleCmdParam debugConsoleCmdParam = new DebugConsoleCmdList.DebugConsoleCmd.DebugConsoleCmdParam();
					DebugConsoleCmdList.DebugConsoleCmd.DebugConsoleCmdParam.DeserializeLengthDelimited(stream, debugConsoleCmdParam);
					return debugConsoleCmdParam;
				}

				// Token: 0x06006362 RID: 25442 RVA: 0x001293EC File Offset: 0x001275EC
				public static DebugConsoleCmdList.DebugConsoleCmd.DebugConsoleCmdParam DeserializeLengthDelimited(Stream stream, DebugConsoleCmdList.DebugConsoleCmd.DebugConsoleCmdParam instance)
				{
					long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
					num += stream.Position;
					return DebugConsoleCmdList.DebugConsoleCmd.DebugConsoleCmdParam.Deserialize(stream, instance, num);
				}

				// Token: 0x06006363 RID: 25443 RVA: 0x00129414 File Offset: 0x00127614
				public static DebugConsoleCmdList.DebugConsoleCmd.DebugConsoleCmdParam Deserialize(Stream stream, DebugConsoleCmdList.DebugConsoleCmd.DebugConsoleCmdParam instance, long limit)
				{
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
								Key key = ProtocolParser.ReadKey((byte)num, stream);
								if (key.Field == 0U)
								{
									throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
								}
								ProtocolParser.SkipKey(stream, key);
							}
							else
							{
								instance.ParamName = ProtocolParser.ReadString(stream);
							}
						}
						else
						{
							instance.ParamType = ProtocolParser.ReadString(stream);
						}
					}
					if (stream.Position != limit)
					{
						throw new ProtocolBufferException("Read past max limit");
					}
					return instance;
				}

				// Token: 0x06006364 RID: 25444 RVA: 0x001294AC File Offset: 0x001276AC
				public void Serialize(Stream stream)
				{
					DebugConsoleCmdList.DebugConsoleCmd.DebugConsoleCmdParam.Serialize(stream, this);
				}

				// Token: 0x06006365 RID: 25445 RVA: 0x001294B8 File Offset: 0x001276B8
				public static void Serialize(Stream stream, DebugConsoleCmdList.DebugConsoleCmd.DebugConsoleCmdParam instance)
				{
					if (instance.ParamType == null)
					{
						throw new ArgumentNullException("ParamType", "Required by proto specification.");
					}
					stream.WriteByte(10);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ParamType));
					if (instance.ParamName == null)
					{
						throw new ArgumentNullException("ParamName", "Required by proto specification.");
					}
					stream.WriteByte(18);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ParamName));
				}

				// Token: 0x06006366 RID: 25446 RVA: 0x00129534 File Offset: 0x00127734
				public uint GetSerializedSize()
				{
					uint num = 0U;
					uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ParamType);
					uint num2 = num + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount);
					uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.ParamName);
					return num2 + (ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2) + 2U;
				}
			}
		}
	}
}
