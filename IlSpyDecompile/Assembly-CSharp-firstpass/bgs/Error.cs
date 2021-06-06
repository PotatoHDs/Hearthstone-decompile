namespace bgs
{
	public struct Error
	{
		public BattleNetErrors EnumVal { get; private set; }

		public uint Code => (uint)EnumVal;

		public string Name => EnumVal.ToString();

		public Error(BattleNetErrors code)
		{
			this = default(Error);
			EnumVal = code;
		}

		public static implicit operator Error(BattleNetErrors code)
		{
			return new Error(code);
		}

		public static implicit operator Error(uint code)
		{
			return new Error((BattleNetErrors)code);
		}

		public static bool operator ==(Error a, BattleNetErrors b)
		{
			return a.EnumVal == b;
		}

		public static bool operator !=(Error a, BattleNetErrors b)
		{
			return a.EnumVal != b;
		}

		public static bool operator ==(Error a, uint b)
		{
			return a.EnumVal == (BattleNetErrors)b;
		}

		public static bool operator !=(Error a, uint b)
		{
			return a.EnumVal != (BattleNetErrors)b;
		}

		public override string ToString()
		{
			return $"{Code} {Name}";
		}

		public override bool Equals(object obj)
		{
			if (obj is BattleNetErrors)
			{
				return EnumVal == (BattleNetErrors)obj;
			}
			if (obj is Error)
			{
				return EnumVal == ((Error)obj).EnumVal;
			}
			if (obj is int)
			{
				return EnumVal == (BattleNetErrors)(int)obj;
			}
			if (obj is uint)
			{
				return EnumVal == (BattleNetErrors)(uint)obj;
			}
			if (obj is long)
			{
				return (long)EnumVal == (long)obj;
			}
			if (obj is ulong)
			{
				return (ulong)EnumVal == (ulong)obj;
			}
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return Code.GetHashCode();
		}
	}
}
