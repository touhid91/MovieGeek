using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MovieGeek.a1.Helpers
{
    public class ChildControl : UserControl
    {
        public delegate void ChildControlDelegate(String SelfUri);
        public event ChildControlDelegate GetDataFromChild;
        protected void Page_Load(object sender, EventArgs e) { }
    }
}
