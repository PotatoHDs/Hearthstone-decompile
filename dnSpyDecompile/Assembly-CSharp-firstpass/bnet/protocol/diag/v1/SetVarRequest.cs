using System;
using System.IO;
using System.Text;

namespace bnet.protocol.diag.v1
{
	// Token: 0x02000438 RID: 1080
	public class SetVarRequest : IProtoBuf
	{
		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x06004907 RID: 18695 RVA: 0x000E44DA File Offset: 0x000E26DA
		// (set) Token: 0x06004908 RID: 18696 RVA: 0x000E44E2 File Offset: 0x000E26E2
		public string Name { get; set; }

		// Token: 0x06004909 RID: 18697 RVA: 0x000E44EB File Offset: 0x000E26EB
		public void SetName(string val)
		{
			this.Name = val;
		}

		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x0600490A RID: 18698 RVA: 0x000E44F4 File Offset: 0x000E26F4
		// (set) Token: 0x0600490B RID: 18699 RVA: 0x000E44FC File Offset: 0x000E26FC
		public string Value { get; set; }

		// Token: 0x0600490C RID: 18700 RVA: 0x000E4505 File Offset: 0x000E2705
		public void SetValue(string val)
		{
			this.Value = val;
		}

		// Token: 0x0600490D RID: 18701 RVA: 0x000E450E File Offset: 0x000E270E
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Name.GetHashCode() ^ this.Value.GetHashCode();
		}

		// Token: 0x0600490E RID: 18702 RVA: 0x000E4534 File Offset: 0x000E2734
		public override bool Equals(object obj)
		{
			SetVarRequest setVarRequest = obj as SetVarRequest;
			return setVarRequest != null && this.Name.Equals(setVarRequest.Name) && this.Value.Equals(setVarRequest.Value);
		}

		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x0600490F RID: 18703 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004910 RID: 18704 RVA: 0x000E4578 File Offset: 0x000E2778
		public static SetVarRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SetVarRequest>(bs, 0, -1);
		}

		// Token: 0x06004911 RID: 18705 RVA: 0x000E4582 File Offset: 0x000E2782
		public void Deserialize(Stream stream)
		{
			SetVarRequest.Deserialize(stream, this);
		}

		// Token: 0x06004912 RID: 18706 RVA: 0x000E458C File Offset: 0x000E278C
		public static SetVarRequest Deserialize(Stream stream, SetVarRequest instance)
		{
			return SetVarRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004913 RID: 18707 RVA: 0x000E4598 File Offset: 0x000E2798
		public static SetVarRequest DeserializeLengthDelimited(Stream stream)
		{
			SetVarRequest setVarRequest = new SetVarRequest();
			SetVarRequest.DeserializeLengthDelimited(stream, setVarRequest);
			return setVarRequest;
		}

		// Token: 0x06004914 RID: 18708 RVA: 0x000E45B4 File Offset: 0x000E27B4
		public static SetVarRequest DeserializeLengthDelimited(Stream stream, SetVarRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SetVarRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004915 RID: 18709 RVA: 0x000E45DC File Offset: 0x000E27DC
		public static SetVarRequest Deserialize(Stream stream, SetVarRequest instance, long limit)
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
						instance.Value = ProtocolParser.ReadString(stream);
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

		// Token: 0x06004916 RID: 18710 RVA: 0x000E4674 File Offset: 0x000E2874
		public void Serialize(Stream stream)
		{
			SetVarRequest.Serialize(stream, this);
		}

		// Token: 0x06004917 RID: 18711 RVA: 0x000E4680 File Offset: 0x000E2880
		public static void Serialize(Stream stream, SetVarRequest instance)
		{
			if (instance.Name == null)
			{
				throw new ArgumentNullException("Name", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			if (instance.Value == null)
			{
				throw new ArgumentNullException("Value", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Value));
		}

		// Token: 0x06004918 RID: 18712 RVA: 0x000E46FC File Offset: 0x000E28FC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
			uint num2 = num + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount);
			uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.Value);
			return num2 + (ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2) + 2U;
		}
	}
}
