using System;

namespace bgs.Shared.Util.platform_win32
{
	public class DPAPITest
	{
		[STAThread]
		private static void Main(string[] args)
		{
			try
			{
				string text = "Hello, world!";
				string entropy = null;
				Console.WriteLine("Plaintext: {0}\r\n", text);
				string text2 = DPAPI.Encrypt(DPAPI.KeyType.UserKey, text, entropy, "My Data");
				Console.WriteLine("Encrypted: {0}\r\n", text2);
				string description;
				string arg = DPAPI.Decrypt(text2, entropy, out description);
				Console.WriteLine("Decrypted: {0} <<<{1}>>>\r\n", arg, description);
			}
			catch (Exception innerException)
			{
				while (innerException != null)
				{
					Console.WriteLine(innerException.Message);
					innerException = innerException.InnerException;
				}
			}
		}
	}
}
