using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerDoc.Formatter
{
    public class XmlFormater : XmlSerializerOutputFormatter
    {
        public override bool CanWriteResult(OutputFormatterCanWriteContext context)
        {
            return context.ContentType.HasValue && context.ContentType.Value.Contains("/xml");
        }
    }
}
