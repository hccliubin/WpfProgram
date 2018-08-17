﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CodeAutoGenerationTool.Provider
{
    interface ITemplateCommand
    {

        string Name { get; }

        string Template(string l, string k, string type = "string");
    }
}