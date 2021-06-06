using System;
using System.IO;
using System.Text;

namespace PegasusUtil
{
	// Token: 0x0200004E RID: 78
	public class UpdateLogin : IProtoBuf
	{
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x00014FE5 File Offset: 0x000131E5
		// (set) Token: 0x06000503 RID: 1283 RVA: 0x00014FED File Offset: 0x000131ED
		public bool ReplyRequired
		{
			get
			{
				return this._ReplyRequired;
			}
			set
			{
				this._ReplyRequired = value;
				this.HasReplyRequired = true;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x00014FFD File Offset: 0x000131FD
		// (set) Token: 0x06000505 RID: 1285 RVA: 0x00015005 File Offset: 0x00013205
		public string Referral
		{
			get
			{
				return this._Referral;
			}
			set
			{
				this._Referral = value;
				this.HasReferral = (value != null);
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x00015018 File Offset: 0x00013218
		// (set) Token: 0x06000507 RID: 1287 RVA: 0x00015020 File Offset: 0x00013220
		public string DeviceModelDeprecated
		{
			get
			{
				return this._DeviceModelDeprecated;
			}
			set
			{
				this._DeviceModelDeprecated = value;
				this.HasDeviceModelDeprecated = (value != null);
			}
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00015034 File Offset: 0x00013234
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasReplyRequired)
			{
				num ^= this.ReplyRequired.GetHashCode();
			}
			if (this.HasReferral)
			{
				num ^= this.Referral.GetHashCode();
			}
			if (this.HasDeviceModelDeprecated)
			{
				num ^= this.DeviceModelDeprecated.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00015094 File Offset: 0x00013294
		public override bool Equals(object obj)
		{
			UpdateLogin updateLogin = obj as UpdateLogin;
			return updateLogin != null && this.HasReplyRequired == updateLogin.HasReplyRequired && (!this.HasReplyRequired || this.ReplyRequired.Equals(updateLogin.ReplyRequired)) && this.HasReferral == updateLogin.HasReferral && (!this.HasReferral || this.Referral.Equals(updateLogin.Referral)) && this.HasDeviceModelDeprecated == updateLogin.HasDeviceModelDeprecated && (!this.HasDeviceModelDeprecated || this.DeviceModelDeprecated.Equals(updateLogin.DeviceModelDeprecated));
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00015132 File Offset: 0x00013332
		public void Deserialize(Stream stream)
		{
			UpdateLogin.Deserialize(stream, this);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0001513C File Offset: 0x0001333C
		public static UpdateLogin Deserialize(Stream stream, UpdateLogin instance)
		{
			return UpdateLogin.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00015148 File Offset: 0x00013348
		public static UpdateLogin DeserializeLengthDelimited(Stream stream)
		{
			UpdateLogin updateLogin = new UpdateLogin();
			UpdateLogin.DeserializeLengthDelimited(stream, updateLogin);
			return updateLogin;
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00015164 File Offset: 0x00013364
		public static UpdateLogin DeserializeLengthDelimited(Stream stream, UpdateLogin instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UpdateLogin.Deserialize(stream, instance, num);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0001518C File Offset: 0x0001338C
		public static UpdateLogin Deserialize(Stream stream, UpdateLogin instance, long limit)
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
					if (num != 18)
					{
						if (num != 26)
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
							instance.DeviceModelDeprecated = ProtocolParser.ReadString(stream);
						}
					}
					else
					{
						instance.Referral = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.ReplyRequired = ProtocolParser.ReadBool(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00015239 File Offset: 0x00013439
		public void Serialize(Stream stream)
		{
			UpdateLogin.Serialize(stream, this);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00015244 File Offset: 0x00013444
		public static void Serialize(Stream stream, UpdateLogin instance)
		{
			if (instance.HasReplyRequired)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.ReplyRequired);
			}
			if (instance.HasReferral)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Referral));
			}
			if (instance.HasDeviceModelDeprecated)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeviceModelDeprecated));
			}
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x000152B8 File Offset: 0x000134B8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasReplyRequired)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasReferral)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Referral);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasDeviceModelDeprecated)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.DeviceModelDeprecated);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}

		// Token: 0x040001E1 RID: 481
		public bool HasReplyRequired;

		// Token: 0x040001E2 RID: 482
		private bool _ReplyRequired;

		// Token: 0x040001E3 RID: 483
		public bool HasReferral;

		// Token: 0x040001E4 RID: 484
		private string _Referral;

		// Token: 0x040001E5 RID: 485
		public bool HasDeviceModelDeprecated;

		// Token: 0x040001E6 RID: 486
		private string _DeviceModelDeprecated;

		// Token: 0x0200055E RID: 1374
		public enum PacketID
		{
			// Token: 0x04001E58 RID: 7768
			ID = 205,
			// Token: 0x04001E59 RID: 7769
			System = 0
		}
	}
}
