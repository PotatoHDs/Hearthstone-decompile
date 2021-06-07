using System;
using UnityEngine;

namespace Blizzard.MobileAuth
{
	[Serializable]
	public struct Configuration
	{
		public string authenticationURLScheme;

		public string sharedKeychainGroup;

		public string keychainAppName;

		public Region[] regions;

		public Color primaryColor;

		public Color secondaryColor;

		public Color textColor;

		public string appID;

		public bool isChinaBuild;

		public string buildID;

		public bool androidIsSecure;

		public bool loggingEnabled;

		public Configuration(string authenticationURLScheme, string sharedKeychainGroup, string keychainAppName, Region[] regions, Color primaryColor, Color secondaryColor, Color textColor, string appID, bool isChinaBuild, string buildID = null, bool androidIsSecure = true, bool loggingEnabled = false)
		{
			this.authenticationURLScheme = authenticationURLScheme;
			this.sharedKeychainGroup = sharedKeychainGroup;
			this.keychainAppName = keychainAppName;
			this.regions = regions;
			this.primaryColor = primaryColor;
			this.secondaryColor = secondaryColor;
			this.textColor = textColor;
			this.appID = appID;
			this.isChinaBuild = isChinaBuild;
			this.buildID = buildID;
			this.androidIsSecure = androidIsSecure;
			this.loggingEnabled = loggingEnabled;
		}

		public override string ToString()
		{
			return string.Concat(base.ToString(), ": \nauthenticationUrlScheme: ", authenticationURLScheme, "\nsharedKeychainGroup: ", sharedKeychainGroup, "\nkeychainAppName: ", keychainAppName, "\nregions: ", regions, "\nprimaryColor: ", primaryColor, "\nsecondaryColor: ", secondaryColor, "\ntextColor: ", textColor, "\nappId: ", appID, "\nisChinaBuild: ", isChinaBuild.ToString(), "\nbuildId: ", buildID, "\nandroidIsSecure: ", androidIsSecure.ToString(), "\nloggingEnabled: ", loggingEnabled.ToString());
		}
	}
}
