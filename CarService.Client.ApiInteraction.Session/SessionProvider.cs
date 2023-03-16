using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Client.ApiInteraction.Session
{
    public class SessionProvider
    {
        public Session Session { get; private set; } = new Session();

        public bool IsIdOfCurrentSession(string sessionId) => Guid.Parse(sessionId) == Session.Id;
    }

    public class Session
    {
        public Guid Id { get; private set; }

        public Session()
        {
            Id = Guid.NewGuid();
        }
    }
}
