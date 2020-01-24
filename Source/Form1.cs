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
using System.IO;

namespace Translator
{
    public partial class Form1 : MaterialForm
    {
        Form f;
        string lang, theme;
        YandexTranslator yt;

        public Form1()
        {
            InitializeComponent();
            yt = new YandexTranslator();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            theme = Properties.Settings.Default.DarkMode;
            if ((theme == "") || (theme == " ") || (theme == "0"))
            {
                materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
                materialSkinManager.ColorScheme = new ColorScheme(Primary.Yellow700, Primary.Yellow800, Primary.Yellow800, Accent.Blue200, TextShade.WHITE);
            }
            if (theme == "1")
            {
                b_w.Checked = true;
                materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
                materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue500, Primary.Blue700, Primary.Blue100, Accent.Yellow200, TextShade.WHITE);
                inputTextBox.BackColor = Color.DarkSlateGray;
                inputTextBox.ForeColor = Color.White;
                outputTextBox.BackColor = Color.DarkSlateGray;
                outputTextBox.ForeColor = Color.White;
            }
        }

        private void B_w_CheckedChanged_1(object sender, EventArgs e)
        {
            if (b_w.Checked)
            {
                //Включение тёмной темы (галочка)
                var materialSkinManager = MaterialSkinManager.Instance;
                materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
                materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue500, Primary.Blue700, Primary.Blue100, Accent.Yellow200, TextShade.WHITE);
                //materialSkinManager.ColorScheme = new ColorScheme(Primary.Cyan500, Primary.Cyan700, Primary.Cyan100, Accent.Yellow200, TextShade.WHITE);
                inputTextBox.BackColor = Color.DarkSlateGray;
                inputTextBox.ForeColor = Color.White;
                outputTextBox.BackColor = Color.DarkSlateGray;
                outputTextBox.ForeColor = Color.White;
                Properties.Settings.Default.DarkMode = "1";
                Properties.Settings.Default.Save();

            }
            if (!b_w.Checked)
            {
                //Выключение тёмной темы (галочка) (включение дефолтной темы)
                var materialSkinManager = MaterialSkinManager.Instance;
                materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
                //materialSkinManager.ColorScheme = new ColorScheme(Primary.Red600, Primary.Red700, Primary.Red400, Accent.Yellow700, TextShade.WHITE);
                materialSkinManager.ColorScheme = new ColorScheme(Primary.Yellow700, Primary.Yellow800, Primary.Yellow800, Accent.Blue200, TextShade.WHITE);
                inputTextBox.BackColor = SystemColors.Control;
                inputTextBox.ForeColor = SystemColors.WindowText;
                outputTextBox.BackColor = SystemColors.Control;
                outputTextBox.ForeColor = SystemColors.WindowText;
                Properties.Settings.Default.DarkMode = "0";
                Properties.Settings.Default.Save();
            }
        }

        private void MaterialRaisedButton1_Click(object sender, EventArgs e)
        {
            if (materialRadioButton1.Checked == true)
            {
                materialSingleLineTextField1.Clear();
                lang = "ru-en";
                outputTextBox.Text = yt.Translate(inputTextBox.Text, lang);
            }
            if (materialRadioButton2.Checked == true)
            {
                materialSingleLineTextField1.Clear();
                lang = "en-ru";
                outputTextBox.Text = yt.Translate(inputTextBox.Text, lang);
            }
            if ((materialRadioButton1.Checked == false) && (materialRadioButton2.Checked == false) && ((materialSingleLineTextField1.Text != "Например: zh - en") || (materialSingleLineTextField1.Text != " ") || (materialSingleLineTextField1.Text != "")))
            {
                lang = materialSingleLineTextField1.Text;
                outputTextBox.Text = yt.Translate(inputTextBox.Text, lang);
            }
            if ((materialRadioButton1.Checked == false) && (materialRadioButton2.Checked == false) && ((materialSingleLineTextField1.Text == "Например: zh - en") || (materialSingleLineTextField1.Text == "") || (materialSingleLineTextField1.Text == " ")))
            {
                MessageBox.Show(
               "Выберете или введите язык перевода!",
               "Ошибка!",
               MessageBoxButtons.OK,
               MessageBoxIcon.Information,
               MessageBoxDefaultButton.Button1,
               MessageBoxOptions.RightAlign);
            }
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

        private void MaterialRaisedButton2_Click(object sender, EventArgs e)
        {
            f = new Form2();
            f.Show();
        }

        private void MaterialRaisedButton5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://translate.yandex.ru");
        }

        private void materialRaisedButton6_Click(object sender, EventArgs e)
        {
            inputTextBox.Text = Clipboard.GetText(); ;
        }

        private void materialRaisedButton7_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.DefaultExt = ".txt";
            savefile.Filter = "Текстовый файл|*.txt";
            if (savefile.ShowDialog() == System.Windows.Forms.DialogResult.OK && savefile.FileName.Length > 0)
            {
                using (StreamWriter sw = new StreamWriter(savefile.FileName, true))
                {
                    sw.Write(outputTextBox.Text);
                    sw.Close();
                }
            }
        }

        private void materialSingleLineTextField1_Enter(object sender, EventArgs e)
        {
            if (materialSingleLineTextField1.Text == "Например: zh-en")
            {
                materialSingleLineTextField1.Clear();
            }
            materialRadioButton1.Checked = false;
            materialRadioButton2.Checked = false; 
        }

        private void materialRaisedButton8_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Zalexanninev15/Yandex-Translate-Lite/blob/master/%D0%9A%D0%BE%D0%B4%D1%8B%20%D1%8F%D0%B7%D1%8B%D0%BA%D0%BE%D0%B2/README.md");
        }
    }
}