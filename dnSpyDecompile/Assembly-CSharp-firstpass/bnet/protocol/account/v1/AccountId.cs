using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x02000521 RID: 1313
	public class AccountId : IProtoBuf
	{
		// Token: 0x170011AB RID: 4523
		// (get) Token: 0x06005DBA RID: 23994 RVA: 0x0011C5F2 File Offset: 0x0011A7F2
		// (set) Token: 0x06005DBB RID: 23995 RVA: 0x0011C5FA File Offset: 0x0011A7FA
		public uint Id { get; set; }

		// Token: 0x06005DBC RID: 23996 RVA: 0x0011C603 File Offset: 0x0011A803
		public void SetId(uint val)
		{
			this.Id = val;
		}

		// Token: 0x06005DBD RID: 23997 RVA: 0x0011C60C File Offset: 0x0011A80C
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Id.GetHashCode();
		}

		// Token: 0x06005DBE RID: 23998 RVA: 0x0011C634 File Offset: 0x0011A834
		public override bool Equals(object obj)
		{
			AccountId accountId = obj as AccountId;
			return accountId != null && this.Id.Equals(accountId.Id);
		}

		// Token: 0x170011AC RID: 4524
		// (get) Token: 0x06005DBF RID: 23999 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005DC0 RID: 24000 RVA: 0x0011C666 File Offset: 0x0011A866
		public static AccountId ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AccountId>(bs, 0, -1);
		}

		// Token: 0x06005DC1 RID: 24001 RVA: 0x0011C670 File Offset: 0x0011A870
		public void Deserialize(Stream stream)
		{
			AccountId.Deserialize(stream, this);
		}

		// Token: 0x06005DC2 RID: 24002 RVA: 0x0011C67A File Offset: 0x0011A87A
		public static AccountId Deserialize(Stream stream, AccountId instance)
		{
			return AccountId.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005DC3 RID: 24003 RVA: 0x0011C688 File Offset: 0x0011A888
		public static AccountId DeserializeLengthDelimited(Stream stream)
		{
			AccountId accountId = new AccountId();
			AccountId.DeserializeLengthDelimited(stream, accountId);
			return accountId;
		}

		// Token: 0x06005DC4 RID: 24004 RVA: 0x0011C6A4 File Offset: 0x0011A8A4
		public static AccountId DeserializeLengthDelimited(Stream stream, AccountId instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AccountId.Deserialize(stream, instance, num);
		}

		// Token: 0x06005DC5 RID: 24005 RVA: 0x0011C6CC File Offset: 0x0011A8CC
		public static AccountId Deserialize(Stream stream, AccountId instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
				else if (num == 13)
				{
					instance.Id = binaryReader.ReadUInt32();
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

		// Token: 0x06005DC6 RID: 24006 RVA: 0x0011C753 File Offset: 0x0011A953
		public void Serialize(Stream stream)
		{
			AccountId.Serialize(stream, this);
		}

		// Token: 0x06005DC7 RID: 24007 RVA: 0x0011C75C File Offset: 0x0011A95C
		public static void Serialize(Stream stream, AccountId instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(13);
			binaryWriter.Write(instance.Id);
		}

		// Token: 0x06005DC8 RID: 24008 RVA: 0x0011C777 File Offset: 0x0011A977
		public uint GetSerializedSize()
		{
			return 0U + 4U + 1U;
		}
	}
}
