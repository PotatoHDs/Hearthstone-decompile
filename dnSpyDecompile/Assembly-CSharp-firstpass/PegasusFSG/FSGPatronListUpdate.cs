using System;
using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusFSG
{
	// Token: 0x02000023 RID: 35
	public class FSGPatronListUpdate : IProtoBuf
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x000078F2 File Offset: 0x00005AF2
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x000078FA File Offset: 0x00005AFA
		public List<FSGPatron> AddedPatrons
		{
			get
			{
				return this._AddedPatrons;
			}
			set
			{
				this._AddedPatrons = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00007903 File Offset: 0x00005B03
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x0000790B File Offset: 0x00005B0B
		public List<FSGPatron> RemovedPatrons
		{
			get
			{
				return this._RemovedPatrons;
			}
			set
			{
				this._RemovedPatrons = value;
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00007914 File Offset: 0x00005B14
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (FSGPatron fsgpatron in this.AddedPatrons)
			{
				num ^= fsgpatron.GetHashCode();
			}
			foreach (FSGPatron fsgpatron2 in this.RemovedPatrons)
			{
				num ^= fsgpatron2.GetHashCode();
			}
			return num;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000079BC File Offset: 0x00005BBC
		public override bool Equals(object obj)
		{
			FSGPatronListUpdate fsgpatronListUpdate = obj as FSGPatronListUpdate;
			if (fsgpatronListUpdate == null)
			{
				return false;
			}
			if (this.AddedPatrons.Count != fsgpatronListUpdate.AddedPatrons.Count)
			{
				return false;
			}
			for (int i = 0; i < this.AddedPatrons.Count; i++)
			{
				if (!this.AddedPatrons[i].Equals(fsgpatronListUpdate.AddedPatrons[i]))
				{
					return false;
				}
			}
			if (this.RemovedPatrons.Count != fsgpatronListUpdate.RemovedPatrons.Count)
			{
				return false;
			}
			for (int j = 0; j < this.RemovedPatrons.Count; j++)
			{
				if (!this.RemovedPatrons[j].Equals(fsgpatronListUpdate.RemovedPatrons[j]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00007A78 File Offset: 0x00005C78
		public void Deserialize(Stream stream)
		{
			FSGPatronListUpdate.Deserialize(stream, this);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00007A82 File Offset: 0x00005C82
		public static FSGPatronListUpdate Deserialize(Stream stream, FSGPatronListUpdate instance)
		{
			return FSGPatronListUpdate.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00007A90 File Offset: 0x00005C90
		public static FSGPatronListUpdate DeserializeLengthDelimited(Stream stream)
		{
			FSGPatronListUpdate fsgpatronListUpdate = new FSGPatronListUpdate();
			FSGPatronListUpdate.DeserializeLengthDelimited(stream, fsgpatronListUpdate);
			return fsgpatronListUpdate;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00007AAC File Offset: 0x00005CAC
		public static FSGPatronListUpdate DeserializeLengthDelimited(Stream stream, FSGPatronListUpdate instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FSGPatronListUpdate.Deserialize(stream, instance, num);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00007AD4 File Offset: 0x00005CD4
		public static FSGPatronListUpdate Deserialize(Stream stream, FSGPatronListUpdate instance, long limit)
		{
			if (instance.AddedPatrons == null)
			{
				instance.AddedPatrons = new List<FSGPatron>();
			}
			if (instance.RemovedPatrons == null)
			{
				instance.RemovedPatrons = new List<FSGPatron>();
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
						instance.RemovedPatrons.Add(FSGPatron.DeserializeLengthDelimited(stream));
					}
				}
				else
				{
					instance.AddedPatrons.Add(FSGPatron.DeserializeLengthDelimited(stream));
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00007B9C File Offset: 0x00005D9C
		public void Serialize(Stream stream)
		{
			FSGPatronListUpdate.Serialize(stream, this);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00007BA8 File Offset: 0x00005DA8
		public static void Serialize(Stream stream, FSGPatronListUpdate instance)
		{
			if (instance.AddedPatrons.Count > 0)
			{
				foreach (FSGPatron fsgpatron in instance.AddedPatrons)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, fsgpatron.GetSerializedSize());
					FSGPatron.Serialize(stream, fsgpatron);
				}
			}
			if (instance.RemovedPatrons.Count > 0)
			{
				foreach (FSGPatron fsgpatron2 in instance.RemovedPatrons)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, fsgpatron2.GetSerializedSize());
					FSGPatron.Serialize(stream, fsgpatron2);
				}
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00007C84 File Offset: 0x00005E84
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.AddedPatrons.Count > 0)
			{
				foreach (FSGPatron fsgpatron in this.AddedPatrons)
				{
					num += 1U;
					uint serializedSize = fsgpatron.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.RemovedPatrons.Count > 0)
			{
				foreach (FSGPatron fsgpatron2 in this.RemovedPatrons)
				{
					num += 1U;
					uint serializedSize2 = fsgpatron2.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			return num;
		}

		// Token: 0x04000073 RID: 115
		private List<FSGPatron> _AddedPatrons = new List<FSGPatron>();

		// Token: 0x04000074 RID: 116
		private List<FSGPatron> _RemovedPatrons = new List<FSGPatron>();

		// Token: 0x02000559 RID: 1369
		public enum PacketID
		{
			// Token: 0x04001E27 RID: 7719
			ID = 512,
			// Token: 0x04001E28 RID: 7720
			System = 3
		}
	}
}
