namespace NGS_Studio.Data
{
	public class Constants
	{
		public const string Username = "Xamarin";
		public const string Password = "password";
		public const string TWILIO_ACCOUNT_SID = "";
		public const string TWILIO_AUTH_TOKEN  = "";
		public const string ERROR = "ERROR";
		enum UserType
		{
			Owner,
			Barber,
			Client
		}
	}
	public class TwilioAccountDetails
	{
		public string AccountSid { get; }

		public string AuthToken { get; }
	}

}
