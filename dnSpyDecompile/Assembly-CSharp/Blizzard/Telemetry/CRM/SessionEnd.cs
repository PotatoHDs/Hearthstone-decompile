using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.CRM
{
	// Token: 0x02001179 RID: 4473
	public class SessionEnd : IProtoBuf
	{
		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x0600C47B RID: 50299 RVA: 0x003B4666 File Offset: 0x003B2866
		// (set) Token: 0x0600C47C RID: 50300 RVA: 0x003B466E File Offset: 0x003B286E
		public string ApplicationId
		{
			get
			{
				return this._ApplicationId;
			}
			set
			{
				this._ApplicationId = value;
				this.HasApplicationId = (value != null);
			}
		}

		// Token: 0x0600C47D RID: 50301 RVA: 0x003B4684 File Offset: 0x003B2884
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasApplicationId)
			{
				num ^= this.ApplicationId.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C47E RID: 50302 RVA: 0x003B46B4 File Offset: 0x003B28B4
		public override bool Equals(object obj)
		{
			SessionEnd sessionEnd = obj as SessionEnd;
			return sessionEnd != null && this.HasApplicationId == sessionEnd.HasApplicationId && (!this.HasApplicationId || this.ApplicationId.Equals(sessionEnd.ApplicationId));
		}

		// Token: 0x0600C47F RID: 50303 RVA: 0x003B46F9 File Offset: 0x003B28F9
		public void Deserialize(Stream stream)
		{
			SessionEnd.Deserialize(stream, this);
		}

		// Token: 0x0600C480 RID: 50304 RVA: 0x003B4703 File Offset: 0x003B2903
		public static SessionEnd Deserialize(Stream stream, SessionEnd instance)
		{
			return SessionEnd.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C481 RID: 50305 RVA: 0x003B4710 File Offset: 0x003B2910
		public static SessionEnd DeserializeLengthDelimited(Stream stream)
		{
			SessionEnd sessionEnd = new SessionEnd();
			SessionEnd.DeserializeLengthDelimited(stream, sessionEnd);
			return sessionEnd;
		}

		// Token: 0x0600C482 RID: 50306 RVA: 0x003B472C File Offset: 0x003B292C
		public static SessionEnd DeserializeLengthDelimited(Stream stream, SessionEnd instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SessionEnd.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C483 RID: 50307 RVA: 0x003B4754 File Offset: 0x003B2954
		public static SessionEnd Deserialize(Stream stream, SessionEnd instance, long limit)
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
				else
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					uint field = key.Field;
					if (field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					if (field != 30U)
					{
						ProtocolParser.SkipKey(stream, key);
					}
					else if (key.WireType == Wire.LengthDelimited)
					{
						instance.ApplicationId = ProtocolParser.ReadString(stream);
					}
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600C484 RID: 50308 RVA: 0x003B47DF File Offset: 0x003B29DF
		public void Serialize(Stream stream)
		{
			SessionEnd.Serialize(stream, this);
		}

		// Token: 0x0600C485 RID: 50309 RVA: 0x003B47E8 File Offset: 0x003B29E8
		public static void Serialize(Stream stream, SessionEnd instance)
		{
			if (instance.HasApplicationId)
			{
				stream.WriteByte(242);
				stream.WriteByte(1);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ApplicationId));
			}
		}

		// Token: 0x0600C486 RID: 50310 RVA: 0x003B481C File Offset: 0x003B2A1C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasApplicationId)
			{
				num += 2U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ApplicationId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x04009D43 RID: 40259
		public bool HasApplicationId;

		// Token: 0x04009D44 RID: 40260
		private string _ApplicationId;
	}
}
