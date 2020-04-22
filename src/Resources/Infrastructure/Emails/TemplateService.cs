using Mustache;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Infrastructure.Emails
{
    public class TemplateService : ITemplateService
    {

        public string Load(string templateFullPath)
        {
            var template = string.Empty;

            if (File.Exists(templateFullPath))
            {
                try
                {
                    using (var sr = new StreamReader(templateFullPath))
                    {
                        template = sr.ReadToEnd();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return template;
        }

        public string Parse(string template, object data)
        {
            try
            {
                var compiler = new FormatCompiler();
                var generator = compiler.Compile(template);
                var result = generator.Render(data);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
