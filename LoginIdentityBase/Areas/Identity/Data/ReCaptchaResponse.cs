namespace LoginIdentityBase.Areas.Identity.Data
{
    public class ReCaptchaResponse
    {
        public bool Success { get; set; }
        public string Challenge_ts { get; set; } // timestamp
        public string Hostname { get; set; }
        public List<string> ErrorCodes { get; set; }
    }
}
