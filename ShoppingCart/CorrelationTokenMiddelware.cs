using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart
{
    public class CorrelationTokenMiddelware
    {
        private readonly Func<IDictionary<string, object>, Task> next;
        private static string CORRELATION_ID_HEADER = "CorrelationId";

        public CorrelationTokenMiddelware(Func<IDictionary<string, object>, Task> next)
        {
            this.next = next;
        }
        public async Task Invoke(IDictionary<string, object> ctx)
        {
            var _headers = ctx["owin.RequestHeaders"] as IDictionary<string, string>;

            if (_headers != null && _headers.ContainsKey(CORRELATION_ID_HEADER) && _headers[CORRELATION_ID_HEADER] != null && _headers[CORRELATION_ID_HEADER].Length > 0)
                return;

            _headers[CORRELATION_ID_HEADER] = Guid.NewGuid().ToString();

            await next(ctx);

            Console.WriteLine("appende");
        }
    }
}
