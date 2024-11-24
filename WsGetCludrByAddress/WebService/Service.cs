using AutoMapper;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using WsGetCludrByAddress.WebService.Entities;
using WsGetCludrByAddress.WebService.Entities.Config;

namespace WsGetCludrByAddress.WebService {
    public class Service {
        private IHttpClientFactory _httpClientFactory = null;
        private ContentSettings _settings = null;
        private IMapper _mapper = null;

        public Service(
            IHttpClientFactory httpClientFactory,
            IOptions<ContentSettings> options,
            IMapper mapper) 
            => (_httpClientFactory, _settings, _mapper) 
            = (httpClientFactory, options.Value, mapper);

        public object GetInfoAsync(string query) {
            using HttpClient client = _httpClientFactory.CreateClient("DaData");

            var content = new StringContent(
                $"[\"{query}]\"]",
                Encoding.UTF8,
                _settings.Type);

            var json = client
                .PostAsync("", content).Result.Content
                .ReadAsStringAsync().Result;
            Response response = JsonSerializer.Deserialize<Response[]>(json)[0];
            var address = _mapper.Map<Address>(response);
            response = null;
            return address;
        }
    }
}
