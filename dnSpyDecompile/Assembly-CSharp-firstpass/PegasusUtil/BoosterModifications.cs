using System;
using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x020000D6 RID: 214
	public class BoosterModifications : IProtoBuf
	{
		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000E94 RID: 3732 RVA: 0x00035263 File Offset: 0x00033463
		// (set) Token: 0x06000E95 RID: 3733 RVA: 0x0003526B File Offset: 0x0003346B
		public List<BoosterInfo> Modifications
		{
			get
			{
				return this._Modifications;
			}
			set
			{
				this._Modifications = value;
			}
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x00035274 File Offset: 0x00033474
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (BoosterInfo boosterInfo in this.Modifications)
			{
				num ^= boosterInfo.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x000352D8 File Offset: 0x000334D8
		public override bool Equals(object obj)
		{
			BoosterModifications boosterModifications = obj as BoosterModifications;
			if (boosterModifications == null)
			{
				return false;
			}
			if (this.Modifications.Count != boosterModifications.Modifications.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Modifications.Count; i++)
			{
				if (!this.Modifications[i].Equals(boosterModifications.Modifications[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x00035343 File Offset: 0x00033543
		public void Deserialize(Stream stream)
		{
			BoosterModifications.Deserialize(stream, this);
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x0003534D File Offset: 0x0003354D
		public static BoosterModifications Deserialize(Stream stream, BoosterModifications instance)
		{
			return BoosterModifications.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x00035358 File Offset: 0x00033558
		public static BoosterModifications DeserializeLengthDelimited(Stream stream)
		{
			BoosterModifications boosterModifications = new BoosterModifications();
			BoosterModifications.DeserializeLengthDelimited(stream, boosterModifications);
			return boosterModifications;
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x00035374 File Offset: 0x00033574
		public static BoosterModifications DeserializeLengthDelimited(Stream stream, BoosterModifications instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BoosterModifications.Deserialize(stream, instance, num);
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x0003539C File Offset: 0x0003359C
		public static BoosterModifications Deserialize(Stream stream, BoosterModifications instance, long limit)
		{
			if (instance.Modifications == null)
			{
				instance.Modifications = new List<BoosterInfo>();
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
					instance.Modifications.Add(BoosterInfo.DeserializeLengthDelimited(stream));
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

		// Token: 0x06000E9D RID: 3741 RVA: 0x00035434 File Offset: 0x00033634
		public void Serialize(Stream stream)
		{
			BoosterModifications.Serialize(stream, this);
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x00035440 File Offset: 0x00033640
		public static void Serialize(Stream stream, BoosterModifications instance)
		{
			if (instance.Modifications.Count > 0)
			{
				foreach (BoosterInfo boosterInfo in instance.Modifications)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, boosterInfo.GetSerializedSize());
					BoosterInfo.Serialize(stream, boosterInfo);
				}
			}
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x000354B8 File Offset: 0x000336B8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Modifications.Count > 0)
			{
				foreach (BoosterInfo boosterInfo in this.Modifications)
				{
					num += 1U;
					uint serializedSize = boosterInfo.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x040004D1 RID: 1233
		private List<BoosterInfo> _Modifications = new List<BoosterInfo>();
	}
}
