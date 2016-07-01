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

using System.Xml.Linq;


namespace WpfApplication2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            XDocument doc = XDocument.Load("a.xml");

            load_level(doc.Root, this.treeView.Items);
        }

        private string get_attributes(XElement el)
        {
            List<string> values = new List<string>();
            foreach (XAttribute at in el.Attributes())
            {
                values.Add(at.Name + "=\"" + at.Value + "\"");
            }

            if (values.Count == 0)
                return "";
            return "  ["+String.Join(", ",values)+"]";
        }

        private void load_level(XElement root, ItemCollection obj)
        {
            foreach (XElement el in root.Elements())
            {
                TreeViewItem item = new TreeViewItem();
                item.Header = el.Name.ToString()+this.get_attributes(el);

                item.MouseDoubleClick += item_MouseDoubleClick;

                obj.Add(item);

                load_level(el, item.Items);
            }
        }

        void item_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;
            MessageBox.Show(item.Header.ToString());
        }

    }
}
