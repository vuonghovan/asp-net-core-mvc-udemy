using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Emails
{
    public interface ITemplateService
    {
        string Load(string pathToTemplate);
        string Parse(string pathToTemplate, object data);
    }
}
