using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusGame
{
	// Token: 0x02000194 RID: 404
	public class SubOption : IProtoBuf
	{
		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x0600191B RID: 6427 RVA: 0x000583AD File Offset: 0x000565AD
		// (set) Token: 0x0600191C RID: 6428 RVA: 0x000583B5 File Offset: 0x000565B5
		public int Id { get; set; }

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x0600191D RID: 6429 RVA: 0x000583BE File Offset: 0x000565BE
		// (set) Token: 0x0600191E RID: 6430 RVA: 0x000583C6 File Offset: 0x000565C6
		public List<TargetOption> Targets
		{
			get
			{
				return this._Targets;
			}
			set
			{
				this._Targets = value;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x0600191F RID: 6431 RVA: 0x000583CF File Offset: 0x000565CF
		// (set) Token: 0x06001920 RID: 6432 RVA: 0x000583D7 File Offset: 0x000565D7
		public int PlayError { get; set; }

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001921 RID: 6433 RVA: 0x000583E0 File Offset: 0x000565E0
		// (set) Token: 0x06001922 RID: 6434 RVA: 0x000583E8 File Offset: 0x000565E8
		public int PlayErrorParam
		{
			get
			{
				return this._PlayErrorParam;
			}
			set
			{
				this._PlayErrorParam = value;
				this.HasPlayErrorParam = true;
			}
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x000583F8 File Offset: 0x000565F8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Id.GetHashCode();
			foreach (TargetOption targetOption in this.Targets)
			{
				num ^= targetOption.GetHashCode();
			}
			num ^= this.PlayError.GetHashCode();
			if (this.HasPlayErrorParam)
			{
				num ^= this.PlayErrorParam.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x00058498 File Offset: 0x00056698
		public override bool Equals(object obj)
		{
			SubOption subOption = obj as SubOption;
			if (subOption == null)
			{
				return false;
			}
			if (!this.Id.Equals(subOption.Id))
			{
				return false;
			}
			if (this.Targets.Count != subOption.Targets.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Targets.Count; i++)
			{
				if (!this.Targets[i].Equals(subOption.Targets[i]))
				{
					return false;
				}
			}
			return this.PlayError.Equals(subOption.PlayError) && this.HasPlayErrorParam == subOption.HasPlayErrorParam && (!this.HasPlayErrorParam || this.PlayErrorParam.Equals(subOption.PlayErrorParam));
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x00058561 File Offset: 0x00056761
		public void Deserialize(Stream stream)
		{
			SubOption.Deserialize(stream, this);
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x0005856B File Offset: 0x0005676B
		public static SubOption Deserialize(Stream stream, SubOption instance)
		{
			return SubOption.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x00058578 File Offset: 0x00056778
		public static SubOption DeserializeLengthDelimited(Stream stream)
		{
			SubOption subOption = new SubOption();
			SubOption.DeserializeLengthDelimited(stream, subOption);
			return subOption;
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x00058594 File Offset: 0x00056794
		public static SubOption DeserializeLengthDelimited(Stream stream, SubOption instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubOption.Deserialize(stream, instance, num);
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x000585BC File Offset: 0x000567BC
		public static SubOption Deserialize(Stream stream, SubOption instance, long limit)
		{
			if (instance.Targets == null)
			{
				instance.Targets = new List<TargetOption>();
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
				else
				{
					if (num <= 26)
					{
						if (num == 8)
						{
							instance.Id = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 26)
						{
							instance.Targets.Add(TargetOption.DeserializeLengthDelimited(stream));
							continue;
						}
					}
					else
					{
						if (num == 32)
						{
							instance.PlayError = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 40)
						{
							instance.PlayErrorParam = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
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

		// Token: 0x0600192A RID: 6442 RVA: 0x000586A7 File Offset: 0x000568A7
		public void Serialize(Stream stream)
		{
			SubOption.Serialize(stream, this);
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x000586B0 File Offset: 0x000568B0
		public static void Serialize(Stream stream, SubOption instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Id));
			if (instance.Targets.Count > 0)
			{
				foreach (TargetOption targetOption in instance.Targets)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, targetOption.GetSerializedSize());
					TargetOption.Serialize(stream, targetOption);
				}
			}
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.PlayError));
			if (instance.HasPlayErrorParam)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.PlayErrorParam));
			}
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x0005876C File Offset: 0x0005696C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Id));
			if (this.Targets.Count > 0)
			{
				foreach (TargetOption targetOption in this.Targets)
				{
					num += 1U;
					uint serializedSize = targetOption.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.PlayError));
			if (this.HasPlayErrorParam)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.PlayErrorParam));
			}
			num += 2U;
			return num;
		}

		// Token: 0x04000963 RID: 2403
		private List<TargetOption> _Targets = new List<TargetOption>();

		// Token: 0x04000965 RID: 2405
		public bool HasPlayErrorParam;

		// Token: 0x04000966 RID: 2406
		private int _PlayErrorParam;
	}
}
