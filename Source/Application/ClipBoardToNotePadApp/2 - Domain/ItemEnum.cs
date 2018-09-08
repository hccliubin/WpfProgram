using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipBoardToNotePadApp
{
    enum ItemEnum
    {
        [DescriptionAttribute(ModuleService.CSharp)]
        CSharp = 0,
        [DescriptionAttribute(ModuleService.Java)]
        Java,
        [DescriptionAttribute(ModuleService.Python)]
        Python,
        [DescriptionAttribute(ModuleService.Save)]
        Save,
        [DescriptionAttribute(ModuleService.Temp)]
        Temp,
        [DescriptionAttribute(ModuleService.Message)]
        Message,
        [DescriptionAttribute(ModuleService.Work)]
        Work,
        [DescriptionAttribute(ModuleService.Secret)]
        Secret,
        [DescriptionAttribute(ModuleService.File)]
        File,
        [DescriptionAttribute(ModuleService.Url)]
        Url
}
}
