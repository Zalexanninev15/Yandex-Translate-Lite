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
    public partial class Form1 : MaterialForm
    {
        Form f;
        YandexTranslator yt;

        public Form1()
        {
            InitializeComponent();

            yt = new YandexTranslator();

            // Дефолтная тема
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);

            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Red500, Primary.Red700, Primary.Red100, Accent.Blue200, TextShade.WHITE);
            // 1 - под заголовком, 2 - заголовок, 3 - ?, 4 - элементы выбора

        }

        private void MaterialRadioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void B_w_CheckedChanged_1(object sender, EventArgs e)
        {
            if (b_w.Checked)
            {
                //Включение тёмной темы (галочка)
                b_w.Text = "ТЁМНАЯ ТЕМА";
                var materialSkinManager = MaterialSkinManager.Instance;
                materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
                materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue500, Primary.Blue700, Primary.Blue100, Accent.Yellow200, TextShade.WHITE);
                //materialSkinManager.ColorScheme = new ColorScheme(Primary.Cyan500, Primary.Cyan700, Primary.Cyan100, Accent.Yellow200, TextShade.WHITE);
            }
            if (!b_w.Checked)
            {
                //Выключение тёмной темы (галочка) (включение дефолтной темы)
                b_w.Text = "ТЁМНАЯ ТЕМА";
                var materialSkinManager = MaterialSkinManager.Instance;
                materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
                //materialSkinManager.ColorScheme = new ColorScheme(Primary.Red600, Primary.Red700, Primary.Red400, Accent.Yellow700, TextShade.WHITE);
                materialSkinManager.ColorScheme = new ColorScheme(Primary.Red500, Primary.Red700, Primary.Red100, Accent.Blue200, TextShade.WHITE);
            }
        }

        private void MaterialFlatButton1_Click(object sender, EventArgs e)
        {
            inputTextBox.Clear();
            outputTextBox.Clear();
        }

        private void MaterialFlatButton2_Click_1(object sender, EventArgs e)
        {
            f = new Form2();
            f.Show();
            b_w.Checked = false;
        }

        private void MaterialFlatButton4_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetText(outputTextBox.Text);
        }

        private void MaterialRaisedButton1_Click(object sender, EventArgs e)
        {
            string lang;

            if (materialRadioButton1.Checked == true)
            {
                lang = "ru-en";
            }
            else
            {
                lang = "en-ru";
            }

            outputTextBox.Text = yt.Translate(inputTextBox.Text, lang);
        }

        private void MaterialRaisedButton3_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetText(outputTextBox.Text);
        }

        private void MaterialRaisedButton4_Click(object sender, EventArgs e)
        {
            inputTextBox.Clear();
            outputTextBox.Clear();
        }

        private void MaterialRadioButton2_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void MaterialRaisedButton2_Click(object sender, EventArgs e)
        {
            f = new Form2();
            f.Show();
            b_w.Checked = false;
        }

        private void MaterialRaisedButton5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://translate.yandex.ru");
        }

        private void materialRaisedButton6_Click(object sender, EventArgs e)
        {
            inputTextBox.Text = Clipboard.GetText(); ;
        }
    }
}