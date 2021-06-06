using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000086 RID: 134
	public class ValidateAchieveResponse : IProtoBuf
	{
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x0001D1AD File Offset: 0x0001B3AD
		// (set) Token: 0x0600083B RID: 2107 RVA: 0x0001D1B5 File Offset: 0x0001B3B5
		public int Achieve { get; set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x0001D1BE File Offset: 0x0001B3BE
		// (set) Token: 0x0600083D RID: 2109 RVA: 0x0001D1C6 File Offset: 0x0001B3C6
		public bool Success
		{
			get
			{
				return this._Success;
			}
			set
			{
				this._Success = value;
				this.HasSuccess = true;
			}
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0001D1D8 File Offset: 0x0001B3D8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Achieve.GetHashCode();
			if (this.HasSuccess)
			{
				num ^= this.Success.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0001D21C File Offset: 0x0001B41C
		public override bool Equals(object obj)
		{
			ValidateAchieveResponse validateAchieveResponse = obj as ValidateAchieveResponse;
			return validateAchieveResponse != null && this.Achieve.Equals(validateAchieveResponse.Achieve) && this.HasSuccess == validateAchieveResponse.HasSuccess && (!this.HasSuccess || this.Success.Equals(validateAchieveResponse.Success));
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0001D27C File Offset: 0x0001B47C
		public void Deserialize(Stream stream)
		{
			ValidateAchieveResponse.Deserialize(stream, this);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0001D286 File Offset: 0x0001B486
		public static ValidateAchieveResponse Deserialize(Stream stream, ValidateAchieveResponse instance)
		{
			return ValidateAchieveResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0001D294 File Offset: 0x0001B494
		public static ValidateAchieveResponse DeserializeLengthDelimited(Stream stream)
		{
			ValidateAchieveResponse validateAchieveResponse = new ValidateAchieveResponse();
			ValidateAchieveResponse.DeserializeLengthDelimited(stream, validateAchieveResponse);
			return validateAchieveResponse;
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0001D2B0 File Offset: 0x0001B4B0
		public static ValidateAchieveResponse DeserializeLengthDelimited(Stream stream, ValidateAchieveResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ValidateAchieveResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0001D2D8 File Offset: 0x0001B4D8
		public static ValidateAchieveResponse Deserialize(Stream stream, ValidateAchieveResponse instance, long limit)
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
				else if (num != 8)
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
						instance.Success = ProtocolParser.ReadBool(stream);
					}
				}
				else
				{
					instance.Achieve = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0001D370 File Offset: 0x0001B570
		public void Serialize(Stream stream)
		{
			ValidateAchieveResponse.Serialize(stream, this);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0001D379 File Offset: 0x0001B579
		public static void Serialize(Stream stream, ValidateAchieveResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Achieve));
			if (instance.HasSuccess)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.Success);
			}
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0001D3AC File Offset: 0x0001B5AC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Achieve));
			if (this.HasSuccess)
			{
				num += 1U;
				num += 1U;
			}
			return num + 1U;
		}

		// Token: 0x0400026E RID: 622
		public bool HasSuccess;

		// Token: 0x0400026F RID: 623
		private bool _Success;

		// Token: 0x02000599 RID: 1433
		public enum PacketID
		{
			// Token: 0x04001F26 RID: 7974
			ID = 285
		}
	}
}
