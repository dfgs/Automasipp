using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Automasipp.Desktop.Views
{
    public enum ArrowOrientation { Left, Right };   
    public partial class CommandView : UserControl
    {


        public static readonly DependencyProperty ArrowOrientationProperty = DependencyProperty.Register("ArrowOrientation", typeof(ArrowOrientation), typeof(CommandView), new PropertyMetadata(ArrowOrientation.Left));
        public ArrowOrientation ArrowOrientation
        {
            get { return (ArrowOrientation)GetValue(ArrowOrientationProperty); }
            set { SetValue(ArrowOrientationProperty, value); }
        }





        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(CommandView), new PropertyMetadata("Text"));
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }



        public CommandView()
        {
            InitializeComponent();
        }
    }
}
