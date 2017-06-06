using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Umbe.Web.MessageLogger
{
    public class MessageLoggerHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {

            if (!MessageLoggerManager.IsActive)
            {
                return await base.SendAsync(request, cancellationToken);
            }

            var correlationId = $"{DateTime.Now.Ticks}{System.Threading.Thread.CurrentThread.ManagedThreadId}";

            var requestContent = await request.Content.ReadAsByteArrayAsync();
            var reqMessage = new RequestMessage
            {
                Version = request.Version,
                RequestUri = request.RequestUri,
                Method = request.Method.Method,
                CorrelationId = correlationId,
                Headers = GetHeaders(request.Headers, request.Content.Headers),
                Content = Encoding.UTF8.GetString(requestContent)
            };

            await Task.Run(()=>{
                MessageLoggerManager.DefaultProvider.SerializeRequest(reqMessage);
            });

            var response = await base.SendAsync(request, cancellationToken);

            var responseContent = (response.IsSuccessStatusCode) ? await response.Content.ReadAsByteArrayAsync() : Encoding.UTF8.GetBytes(response.ReasonPhrase);

            var respMessage = new ResponseMessage
            {
                Version = response.Version,
                Status = (int)response.StatusCode,
                CorrelationId = correlationId,
                Headers = GetHeaders(response.Headers, response.Content.Headers),
                Content = Encoding.UTF8.GetString(responseContent)
            };

            await Task.Run(() =>
            {
                MessageLoggerManager.DefaultProvider.SerializeResponse(respMessage);
            });

            return response;
        }

        private Headers GetHeaders(HttpHeaders messageHeaders, HttpHeaders contentHeaders)
        {
            var h = new Headers();

            foreach (var header in messageHeaders)
            {
                h.Add(header.Key, string.Join(";", header.Value));
            }

            foreach (var header in contentHeaders)
            {
                h.Add(header.Key, string.Join(";", header.Value));
            }
            
            return h;
        }
        
    }
}
