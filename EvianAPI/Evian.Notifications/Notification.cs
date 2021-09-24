using Newtonsoft.Json;
using System.Collections.Generic;

namespace Evian.Notifications
{
    public class Notification
    {
        public List<Error> Errors { get; set; } = new List<Error>();
        public bool HasErrors => 0 != Errors.Count;
        public bool HasNotifications => HasErrors;

        public string Get()
        {
            return JsonConvert.SerializeObject(new { innerMessage = Errors.ToArray() });
        }

        public override string ToString()
        {
            var erros = "";
            foreach (var error in Errors)
                erros += error.ToString() + " ";
            return erros;
        }
    }
}
