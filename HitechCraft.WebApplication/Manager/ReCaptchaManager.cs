namespace HitechCraft.WebApplication.Manager
{
    using System.Net;
    using Common.Models.Enum;
    using Common.Models.Json;
    using Common.Models.Json.MinecraftLauncher;
    using Newtonsoft.Json;

    public static class ReCaptchaManager
    {
        public static JsonStatusData ValidateReCaptcha(string response)
        {
            //ReCaptcha secret key
            const string secret = "6LcdfycTAAAAAE1D3Kn3lhmZVptvI3TwbJM3Vfxo";

            var client = new WebClient();
            var reply =
                client.DownloadString(
                    string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

            var captchaResponse = JsonConvert.DeserializeObject<JsonCaptchaResponse>(reply);

            if (!captchaResponse.Success)
            {
                return new JsonStatusData()
                {
                    Message = "Ошибка валидации ReCaptcha",
                    Status = JsonStatus.NO
                };
            }

            return new JsonStatusData()
            {
                Message = "Успешно",
                Status = JsonStatus.YES
            };
        }

    }
}