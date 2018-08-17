using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace HeBianGu.Control.UserControls.PanoramaControl
{
    /// <summary>
    /// The minimum specification that a tile needs to support  
    /// </summary>
    public interface IPanoramaTile
    {
        ICommand TileClickedCommand { get; }
    }
}
