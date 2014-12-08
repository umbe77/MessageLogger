﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbe.Web.MessageLogger
{
    public class RequestMessage : Message
    {
        public string Method { get; set; }
        public Uri RequestUri { get; set; }

        public override string ToString()
        {
            var v = string.Format("{0}.{1}", Version.Major, Version.Minor);

            var message = string.Format(@"{0} {1} HTTP/{2}
{3}
{4}", Method, RequestUri.AbsolutePath, v, Headers, Content);

            return message;
        }

    }
}