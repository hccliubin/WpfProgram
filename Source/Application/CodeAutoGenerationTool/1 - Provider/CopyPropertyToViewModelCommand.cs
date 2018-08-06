using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CodeAutoGenerationTool.Provider
{
    class CopyPropertyToViewModelCommand : ITemplateCommand
    {
        public string Name { get => "生成ViewModel"; }

        public string Template(string l,string k)
        {
            string ss = @"    string _" + l.ToLower() + @";
            /// <summary> " + k + @" </summary>
            public string " + l + @"
            {
                get
                {
                    return _" + l.ToLower() + @";
                }
                set
                {
                    _" + l.ToLower() + @" = value;

                    RaisePropertyChanged();
                }
            }";

            return ss;
        }
    }
}
