using UniquomeApp.WebApi.Options;

namespace UniquomeApp.WebApi.Services
{
    public class ApiMessageService: IApiMessageService
    {
        private readonly List<ApiMessage> _apiMessages;

        public ApiMessageService(List<ApiMessage> apiMessages)
        {
            _apiMessages = apiMessages;
        }

        public string GetMessage(string messageName)
        {
            var message = _apiMessages.FirstOrDefault(x => x.Name == messageName);
            if (message != null) return message.Message;
            return "Άγνωστο Σφάλμα";
        }
    }
}