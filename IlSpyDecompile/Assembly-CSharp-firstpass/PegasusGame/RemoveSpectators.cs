using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusGame
{
	public class RemoveSpectators : IProtoBuf
	{
		public enum PacketID
		{
			ID = 26
		}

		private List<BnetId> _TargetGameaccountIds = new List<BnetId>();

		public bool HasKickAllSpectators;

		private bool _KickAllSpectators;

		public bool HasRegenerateSpectatorPassword;

		private bool _RegenerateSpectatorPassword;

		public List<BnetId> TargetGameaccountIds
		{
			get
			{
				return _TargetGameaccountIds;
			}
			set
			{
				_TargetGameaccountIds = value;
			}
		}

		public bool KickAllSpectators
		{
			get
			{
				return _KickAllSpectators;
			}
			set
			{
				_KickAllSpectators = value;
				HasKickAllSpectators = true;
			}
		}

		public bool RegenerateSpectatorPassword
		{
			get
			{
				return _RegenerateSpectatorPassword;
			}
			set
			{
				_RegenerateSpectatorPassword = value;
				HasRegenerateSpectatorPassword = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (BnetId targetGameaccountId in TargetGameaccountIds)
			{
				num ^= targetGameaccountId.GetHashCode();
			}
			if (HasKickAllSpectators)
			{
				num ^= KickAllSpectators.GetHashCode();
			}
			if (HasRegenerateSpectatorPassword)
			{
				num ^= RegenerateSpectatorPassword.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RemoveSpectators removeSpectators = obj as RemoveSpectators;
			if (removeSpectators == null)
			{
				return false;
			}
			if (TargetGameaccountIds.Count != removeSpectators.TargetGameaccountIds.Count)
			{
				return false;
			}
			for (int i = 0; i < TargetGameaccountIds.Count; i++)
			{
				if (!TargetGameaccountIds[i].Equals(removeSpectators.TargetGameaccountIds[i]))
				{
					return false;
				}
			}
			if (HasKickAllSpectators != removeSpectators.HasKickAllSpectators || (HasKickAllSpectators && !KickAllSpectators.Equals(removeSpectators.KickAllSpectators)))
			{
				return false;
			}
			if (HasRegenerateSpectatorPassword != removeSpectators.HasRegenerateSpectatorPassword || (HasRegenerateSpectatorPassword && !RegenerateSpectatorPassword.Equals(removeSpectators.RegenerateSpectatorPassword)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RemoveSpectators Deserialize(Stream stream, RemoveSpectators instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RemoveSpectators DeserializeLengthDelimited(Stream stream)
		{
			RemoveSpectators removeSpectators = new RemoveSpectators();
			DeserializeLengthDelimited(stream, removeSpectators);
			return removeSpectators;
		}

		public static RemoveSpectators DeserializeLengthDelimited(Stream stream, RemoveSpectators instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RemoveSpectators Deserialize(Stream stream, RemoveSpectators instance, long limit)
		{
			if (instance.TargetGameaccountIds == null)
			{
				instance.TargetGameaccountIds = new List<BnetId>();
			}
			instance.KickAllSpectators = false;
			instance.RegenerateSpectatorPassword = false;
			while (true)
			{
				if (limit >= 0 && stream.Position >= limit)
				{
					if (stream.Position == limit)
					{
						break;
					}
					throw new ProtocolBufferException("Read past max limit");
				}
				int num = stream.ReadByte();
				switch (num)
				{
				case -1:
					break;
				case 10:
					instance.TargetGameaccountIds.Add(BnetId.DeserializeLengthDelimited(stream));
					continue;
				case 16:
					instance.KickAllSpectators = ProtocolParser.ReadBool(stream);
					continue;
				case 24:
					instance.RegenerateSpectatorPassword = ProtocolParser.ReadBool(stream);
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
					continue;
				}
				}
				if (limit < 0)
				{
					break;
				}
				throw new EndOfStreamException();
			}
			return instance;
		}

		public void Serialize(Stream stream)
		{
			Serialize(stream, this);
		}

		public static void Serialize(Stream stream, RemoveSpectators instance)
		{
			if (instance.TargetGameaccountIds.Count > 0)
			{
				foreach (BnetId targetGameaccountId in instance.TargetGameaccountIds)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, targetGameaccountId.GetSerializedSize());
					BnetId.Serialize(stream, targetGameaccountId);
				}
			}
			if (instance.HasKickAllSpectators)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.KickAllSpectators);
			}
			if (instance.HasRegenerateSpectatorPassword)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.RegenerateSpectatorPassword);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (TargetGameaccountIds.Count > 0)
			{
				foreach (BnetId targetGameaccountId in TargetGameaccountIds)
				{
					num++;
					uint serializedSize = targetGameaccountId.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasKickAllSpectators)
			{
				num++;
				num++;
			}
			if (HasRegenerateSpectatorPassword)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
