using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipBoardToNotePadApp
{

    public class NotePadItem
    {
        private string _title;
        /// <summary> 说明  </summary>
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
            }
        }


        private string _content;
        /// <summary> 说明  </summary>
        public string Content
        {
            get { return _content; }
            set
            {
                _content = value;
            }
        }

        private string _date;
        /// <summary> 说明  </summary>
        public string Date
        {
            get { return _date; }

            set
            {
                _date = value;
            }
        }

        private string _type;
        /// <summary> 说明  </summary>
        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
            }
        }
    }
}
