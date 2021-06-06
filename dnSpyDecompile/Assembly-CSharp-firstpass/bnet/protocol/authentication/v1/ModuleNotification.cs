using System;
using System.IO;

namespace bnet.protocol.authentication.v1
{
	// Token: 0x020004EA RID: 1258
	public class ModuleNotification : IProtoBuf
	{
		// Token: 0x170010CE RID: 4302
		// (get) Token: 0x0600590F RID: 22799 RVA: 0x00110A18 File Offset: 0x0010EC18
		// (set) Token: 0x06005910 RID: 22800 RVA: 0x00110A20 File Offset: 0x0010EC20
		public int ModuleId
		{
			get
			{
				return this._ModuleId;
			}
			set
			{
				this._ModuleId = value;
				this.HasModuleId = true;
			}
		}

		// Token: 0x06005911 RID: 22801 RVA: 0x00110A30 File Offset: 0x0010EC30
		public void SetModuleId(int val)
		{
			this.ModuleId = val;
		}

		// Token: 0x170010CF RID: 4303
		// (get) Token: 0x06005912 RID: 22802 RVA: 0x00110A39 File Offset: 0x0010EC39
		// (set) Token: 0x06005913 RID: 22803 RVA: 0x00110A41 File Offset: 0x0010EC41
		public uint Result
		{
			get
			{
				return this._Result;
			}
			set
			{
				this._Result = value;
				this.HasResult = true;
			}
		}

		// Token: 0x06005914 RID: 22804 RVA: 0x00110A51 File Offset: 0x0010EC51
		public void SetResult(uint val)
		{
			this.Result = val;
		}

		// Token: 0x06005915 RID: 22805 RVA: 0x00110A5C File Offset: 0x0010EC5C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasModuleId)
			{
				num ^= this.ModuleId.GetHashCode();
			}
			if (this.HasResult)
			{
				num ^= this.Result.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005916 RID: 22806 RVA: 0x00110AA8 File Offset: 0x0010ECA8
		public override bool Equals(object obj)
		{
			ModuleNotification moduleNotification = obj as ModuleNotification;
			return moduleNotification != null && this.HasModuleId == moduleNotification.HasModuleId && (!this.HasModuleId || this.ModuleId.Equals(moduleNotification.ModuleId)) && this.HasResult == moduleNotification.HasResult && (!this.HasResult || this.Result.Equals(moduleNotification.Result));
		}

		// Token: 0x170010D0 RID: 4304
		// (get) Token: 0x06005917 RID: 22807 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005918 RID: 22808 RVA: 0x00110B1E File Offset: 0x0010ED1E
		public static ModuleNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ModuleNotification>(bs, 0, -1);
		}

		// Token: 0x06005919 RID: 22809 RVA: 0x00110B28 File Offset: 0x0010ED28
		public void Deserialize(Stream stream)
		{
			ModuleNotification.Deserialize(stream, this);
		}

		// Token: 0x0600591A RID: 22810 RVA: 0x00110B32 File Offset: 0x0010ED32
		public static ModuleNotification Deserialize(Stream stream, ModuleNotification instance)
		{
			return ModuleNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600591B RID: 22811 RVA: 0x00110B40 File Offset: 0x0010ED40
		public static ModuleNotification DeserializeLengthDelimited(Stream stream)
		{
			ModuleNotification moduleNotification = new ModuleNotification();
			ModuleNotification.DeserializeLengthDelimited(stream, moduleNotification);
			return moduleNotification;
		}

		// Token: 0x0600591C RID: 22812 RVA: 0x00110B5C File Offset: 0x0010ED5C
		public static ModuleNotification DeserializeLengthDelimited(Stream stream, ModuleNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ModuleNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x0600591D RID: 22813 RVA: 0x00110B84 File Offset: 0x0010ED84
		public static ModuleNotification Deserialize(Stream stream, ModuleNotification instance, long limit)
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
				else if (num != 16)
				{
					if (num != 24)
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
						instance.Result = ProtocolParser.ReadUInt32(stream);
					}
				}
				else
				{
					instance.ModuleId = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600591E RID: 22814 RVA: 0x00110C1D File Offset: 0x0010EE1D
		public void Serialize(Stream stream)
		{
			ModuleNotification.Serialize(stream, this);
		}

		// Token: 0x0600591F RID: 22815 RVA: 0x00110C26 File Offset: 0x0010EE26
		public static void Serialize(Stream stream, ModuleNotification instance)
		{
			if (instance.HasModuleId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ModuleId));
			}
			if (instance.HasResult)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.Result);
			}
		}

		// Token: 0x06005920 RID: 22816 RVA: 0x00110C64 File Offset: 0x0010EE64
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasModuleId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ModuleId));
			}
			if (this.HasResult)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Result);
			}
			return num;
		}

		// Token: 0x04001BD0 RID: 7120
		public bool HasModuleId;

		// Token: 0x04001BD1 RID: 7121
		private int _ModuleId;

		// Token: 0x04001BD2 RID: 7122
		public bool HasResult;

		// Token: 0x04001BD3 RID: 7123
		private uint _Result;
	}
}
