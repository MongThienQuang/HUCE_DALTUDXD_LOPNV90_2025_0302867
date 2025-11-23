using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DALTUDXD_LOPNV90_2025_030287.View
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtTenLoaiGach_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void dgSan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbtengach_DropDownOpened(object sender, EventArgs e)
        {
            cbtengach.ItemsSource = new List<string>
            {
              "Gạch men, gạch gốm bán sứ ( Ceramic tile )",
              "Gạch đá hoa cương ( Granite tile )",
              "Gạch xương sứ, bóng kiếng ( Porcelain tile )",
              "Gạch đất nung ( brick tile )",
              "Gạch bông ( Cement tile )",
              "Gạch đá hạt mài ( Terrazzo )",
              "Gạch đá cẩm thạch ( Marble tile )",
              "Gạch kính ( Glass tile )",
              "Gạch giả gỗ hoặc gỗ tự nhiên ( Wood tile )",
              "Gạch đá tự nhiên ( Natural Stone tile )",
              "Gạch sợi nén ( Laminate tile )",
              "Gạch Nhựa ( Vinyl tile )",
              "Ván sàn tre, trúc ( Bamboo tile )",
              "Sàn gỗ bần ( Cork tile )",
              "Gạch thảm mô-đun ( carpet tile )",
              "Sàn cao su ( Rubber tile )",
              "Sàn Epoxy"
            };
        }
        private void cbKichThuoc_DropDownOpened(object sender, EventArgs e)
        {
            cbKichThuoc.ItemsSource = new List<string>
            {
              "300x300",
              "400x400",
              "600x600",
              "800x800",
              "1200x600"
    };
        }


    }
}
