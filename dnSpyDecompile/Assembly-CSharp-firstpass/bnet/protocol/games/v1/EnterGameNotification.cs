using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.games.v1.Types;
using bnet.protocol.games.v2;

namespace bnet.protocol.games.v1
{
	// Token: 0x0200039C RID: 924
	public class EnterGameNotification : IProtoBuf
	{
		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x06003B6A RID: 15210 RVA: 0x000C013F File Offset: 0x000BE33F
		// (set) Token: 0x06003B6B RID: 15211 RVA: 0x000C0147 File Offset: 0x000BE347
		public GameHandle GameHandle
		{
			get
			{
				return this._GameHandle;
			}
			set
			{
				this._GameHandle = value;
				this.HasGameHandle = (value != null);
			}
		}

		// Token: 0x06003B6C RID: 15212 RVA: 0x000C015A File Offset: 0x000BE35A
		public void SetGameHandle(GameHandle val)
		{
			this.GameHandle = val;
		}

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x06003B6D RID: 15213 RVA: 0x000C0163 File Offset: 0x000BE363
		// (set) Token: 0x06003B6E RID: 15214 RVA: 0x000C016B File Offset: 0x000BE36B
		public PrivacyLevel PrivacyLevel
		{
			get
			{
				return this._PrivacyLevel;
			}
			set
			{
				this._PrivacyLevel = value;
				this.HasPrivacyLevel = true;
			}
		}

		// Token: 0x06003B6F RID: 15215 RVA: 0x000C017B File Offset: 0x000BE37B
		public void SetPrivacyLevel(PrivacyLevel val)
		{
			this.PrivacyLevel = val;
		}

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x06003B70 RID: 15216 RVA: 0x000C0184 File Offset: 0x000BE384
		// (set) Token: 0x06003B71 RID: 15217 RVA: 0x000C018C File Offset: 0x000BE38C
		public List<Assignment> Assignments
		{
			get
			{
				return this._Assignments;
			}
			set
			{
				this._Assignments = value;
			}
		}

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06003B72 RID: 15218 RVA: 0x000C0184 File Offset: 0x000BE384
		public List<Assignment> AssignmentsList
		{
			get
			{
				return this._Assignments;
			}
		}

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06003B73 RID: 15219 RVA: 0x000C0195 File Offset: 0x000BE395
		public int AssignmentsCount
		{
			get
			{
				return this._Assignments.Count;
			}
		}

		// Token: 0x06003B74 RID: 15220 RVA: 0x000C01A2 File Offset: 0x000BE3A2
		public void AddAssignments(Assignment val)
		{
			this._Assignments.Add(val);
		}

		// Token: 0x06003B75 RID: 15221 RVA: 0x000C01B0 File Offset: 0x000BE3B0
		public void ClearAssignments()
		{
			this._Assignments.Clear();
		}

		// Token: 0x06003B76 RID: 15222 RVA: 0x000C01BD File Offset: 0x000BE3BD
		public void SetAssignments(List<Assignment> val)
		{
			this.Assignments = val;
		}

		// Token: 0x06003B77 RID: 15223 RVA: 0x000C01C8 File Offset: 0x000BE3C8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasGameHandle)
			{
				num ^= this.GameHandle.GetHashCode();
			}
			if (this.HasPrivacyLevel)
			{
				num ^= this.PrivacyLevel.GetHashCode();
			}
			foreach (Assignment assignment in this.Assignments)
			{
				num ^= assignment.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003B78 RID: 15224 RVA: 0x000C0260 File Offset: 0x000BE460
		public override bool Equals(object obj)
		{
			EnterGameNotification enterGameNotification = obj as EnterGameNotification;
			if (enterGameNotification == null)
			{
				return false;
			}
			if (this.HasGameHandle != enterGameNotification.HasGameHandle || (this.HasGameHandle && !this.GameHandle.Equals(enterGameNotification.GameHandle)))
			{
				return false;
			}
			if (this.HasPrivacyLevel != enterGameNotification.HasPrivacyLevel || (this.HasPrivacyLevel && !this.PrivacyLevel.Equals(enterGameNotification.PrivacyLevel)))
			{
				return false;
			}
			if (this.Assignments.Count != enterGameNotification.Assignments.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Assignments.Count; i++)
			{
				if (!this.Assignments[i].Equals(enterGameNotification.Assignments[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06003B79 RID: 15225 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003B7A RID: 15226 RVA: 0x000C032F File Offset: 0x000BE52F
		public static EnterGameNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<EnterGameNotification>(bs, 0, -1);
		}

		// Token: 0x06003B7B RID: 15227 RVA: 0x000C0339 File Offset: 0x000BE539
		public void Deserialize(Stream stream)
		{
			EnterGameNotification.Deserialize(stream, this);
		}

		// Token: 0x06003B7C RID: 15228 RVA: 0x000C0343 File Offset: 0x000BE543
		public static EnterGameNotification Deserialize(Stream stream, EnterGameNotification instance)
		{
			return EnterGameNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003B7D RID: 15229 RVA: 0x000C0350 File Offset: 0x000BE550
		public static EnterGameNotification DeserializeLengthDelimited(Stream stream)
		{
			EnterGameNotification enterGameNotification = new EnterGameNotification();
			EnterGameNotification.DeserializeLengthDelimited(stream, enterGameNotification);
			return enterGameNotification;
		}

		// Token: 0x06003B7E RID: 15230 RVA: 0x000C036C File Offset: 0x000BE56C
		public static EnterGameNotification DeserializeLengthDelimited(Stream stream, EnterGameNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return EnterGameNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06003B7F RID: 15231 RVA: 0x000C0394 File Offset: 0x000BE594
		public static EnterGameNotification Deserialize(Stream stream, EnterGameNotification instance, long limit)
		{
			instance.PrivacyLevel = PrivacyLevel.PRIVACY_LEVEL_OPEN;
			if (instance.Assignments == null)
			{
				instance.Assignments = new List<Assignment>();
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
					if (num != 16)
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
							instance.Assignments.Add(Assignment.DeserializeLengthDelimited(stream));
						}
					}
					else
					{
						instance.PrivacyLevel = (PrivacyLevel)ProtocolParser.ReadUInt64(stream);
					}
				}
				else if (instance.GameHandle == null)
				{
					instance.GameHandle = GameHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameHandle.DeserializeLengthDelimited(stream, instance.GameHandle);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003B80 RID: 15232 RVA: 0x000C0482 File Offset: 0x000BE682
		public void Serialize(Stream stream)
		{
			EnterGameNotification.Serialize(stream, this);
		}

		// Token: 0x06003B81 RID: 15233 RVA: 0x000C048C File Offset: 0x000BE68C
		public static void Serialize(Stream stream, EnterGameNotification instance)
		{
			if (instance.HasGameHandle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
				GameHandle.Serialize(stream, instance.GameHandle);
			}
			if (instance.HasPrivacyLevel)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.PrivacyLevel));
			}
			if (instance.Assignments.Count > 0)
			{
				foreach (Assignment assignment in instance.Assignments)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, assignment.GetSerializedSize());
					Assignment.Serialize(stream, assignment);
				}
			}
		}

		// Token: 0x06003B82 RID: 15234 RVA: 0x000C054C File Offset: 0x000BE74C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasGameHandle)
			{
				num += 1U;
				uint serializedSize = this.GameHandle.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasPrivacyLevel)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.PrivacyLevel));
			}
			if (this.Assignments.Count > 0)
			{
				foreach (Assignment assignment in this.Assignments)
				{
					num += 1U;
					uint serializedSize2 = assignment.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			return num;
		}

		// Token: 0x04001575 RID: 5493
		public bool HasGameHandle;

		// Token: 0x04001576 RID: 5494
		private GameHandle _GameHandle;

		// Token: 0x04001577 RID: 5495
		public bool HasPrivacyLevel;

		// Token: 0x04001578 RID: 5496
		private PrivacyLevel _PrivacyLevel;

		// Token: 0x04001579 RID: 5497
		private List<Assignment> _Assignments = new List<Assignment>();
	}
}
