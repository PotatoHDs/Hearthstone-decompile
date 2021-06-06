using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000050 RID: 80
	public class SetOptions : IProtoBuf
	{
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x000154CA File Offset: 0x000136CA
		// (set) Token: 0x06000521 RID: 1313 RVA: 0x000154D2 File Offset: 0x000136D2
		public List<ClientOption> Options
		{
			get
			{
				return this._Options;
			}
			set
			{
				this._Options = value;
			}
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x000154DC File Offset: 0x000136DC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (ClientOption clientOption in this.Options)
			{
				num ^= clientOption.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00015540 File Offset: 0x00013740
		public override bool Equals(object obj)
		{
			SetOptions setOptions = obj as SetOptions;
			if (setOptions == null)
			{
				return false;
			}
			if (this.Options.Count != setOptions.Options.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Options.Count; i++)
			{
				if (!this.Options[i].Equals(setOptions.Options[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x000155AB File Offset: 0x000137AB
		public void Deserialize(Stream stream)
		{
			SetOptions.Deserialize(stream, this);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x000155B5 File Offset: 0x000137B5
		public static SetOptions Deserialize(Stream stream, SetOptions instance)
		{
			return SetOptions.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x000155C0 File Offset: 0x000137C0
		public static SetOptions DeserializeLengthDelimited(Stream stream)
		{
			SetOptions setOptions = new SetOptions();
			SetOptions.DeserializeLengthDelimited(stream, setOptions);
			return setOptions;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x000155DC File Offset: 0x000137DC
		public static SetOptions DeserializeLengthDelimited(Stream stream, SetOptions instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SetOptions.Deserialize(stream, instance, num);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00015604 File Offset: 0x00013804
		public static SetOptions Deserialize(Stream stream, SetOptions instance, long limit)
		{
			if (instance.Options == null)
			{
				instance.Options = new List<ClientOption>();
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
					instance.Options.Add(ClientOption.DeserializeLengthDelimited(stream));
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

		// Token: 0x06000529 RID: 1321 RVA: 0x0001569C File Offset: 0x0001389C
		public void Serialize(Stream stream)
		{
			SetOptions.Serialize(stream, this);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x000156A8 File Offset: 0x000138A8
		public static void Serialize(Stream stream, SetOptions instance)
		{
			if (instance.Options.Count > 0)
			{
				foreach (ClientOption clientOption in instance.Options)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, clientOption.GetSerializedSize());
					ClientOption.Serialize(stream, clientOption);
				}
			}
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00015720 File Offset: 0x00013920
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Options.Count > 0)
			{
				foreach (ClientOption clientOption in this.Options)
				{
					num += 1U;
					uint serializedSize = clientOption.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x040001E8 RID: 488
		private List<ClientOption> _Options = new List<ClientOption>();

		// Token: 0x02000560 RID: 1376
		public enum PacketID
		{
			// Token: 0x04001E5E RID: 7774
			ID = 239,
			// Token: 0x04001E5F RID: 7775
			System = 0
		}

		// Token: 0x02000561 RID: 1377
		public enum MaxOptionCount
		{
			// Token: 0x04001E61 RID: 7777
			LIMIT = 51
		}
	}
}
