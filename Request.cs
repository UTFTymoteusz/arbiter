using System;
using System.Net;
using System.Collections.Generic;

namespace Arbiter
{
    public class Request
    {
        private const int COOKIE_MAX = 8192 * 2;

        public string Method;
        public Uri Uri;
        public Uri RewrittenUri;
        public string Version;

        public Dictionary<string, string> Headers = new Dictionary<string, string>();
        public Dictionary<string, string> Parameters = new Dictionary<string, string>();

        public Site Site;
        public ClampedStream Stream;
        public Stream SocketStream;

        public EndPoint EndPoint;

        private Dictionary<string, Cookie> _cookies;

        public Cookie? GetCookie(string name)
        {
            if (_cookies == null)
                _cookies = ParseCookies();

            if (!_cookies.TryGetValue(name, out Cookie cookie))
                return default;

            return cookie;
        }

        Dictionary<string, Cookie> ParseCookies()
        {
            var cookies = new Dictionary<string, Cookie>();

            if (!Headers.TryGetValue("cookie", out string cookieHeader))
                return cookies;

            if (cookieHeader.Length > COOKIE_MAX)
                return cookies;

            foreach (string rawCookie in cookieHeader.Split(';'))
            {
                if (rawCookie.Length > COOKIE_MAX)
                    continue;

                string[] cookieParts = rawCookie.Trim().Split('=');

                if (cookieParts.Length != 2)
                    continue;

                string key = cookieParts[0];
                string value = cookieParts[1];

                cookies[key] = new Cookie(key, value);
            }

            return cookies;
        }
    }
}