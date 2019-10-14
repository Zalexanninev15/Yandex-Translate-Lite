using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Animations;
using MaterialSkin.Controls;
using MaterialSkin;

namespace Translator
{
    public partial class Form2 : MaterialForm
    {
        public Form2()
        {
            InitializeComponent();

            // Дефолтная тема
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);

            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Red500, Primary.Red700, Primary.Red100, Accent.Blue200, TextShade.WHITE);
            // 1 - под заголовком, 2 - заголовок, 3 - ?, 4 - элементы выбора
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void MaterialRaisedButton1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Zalexanninev15/Yandex-Translate-Lite");
        }
    }
}
