﻿using System;
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
using Microsoft.Win32;

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
            using (RegistryKey reg = Registry.CurrentUser.CreateSubKey(@"Software\Zalexanninev15\Yandex-Translate-Lite"))
            {
                theme = Convert.ToString(reg.GetValue("theme"));
            }
            if ((theme == "") || (theme == "0"))
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
                inputTextBox.BackColor = Color.DarkSlateGray;
                inputTextBox.ForeColor = Color.White;
                outputTextBox.BackColor = Color.DarkSlateGray;
                outputTextBox.ForeColor = Color.White;
                using (RegistryKey reg = Registry.CurrentUser.CreateSubKey(@"Software\Zalexanninev15\Yandex-Translate-Lite"))
                {
                    reg.SetValue("theme", "1");
                }

            }
            if (!b_w.Checked)
            {
                //Выключение тёмной темы (галочка) (включение дефолтной темы)
                var materialSkinManager = MaterialSkinManager.Instance;
                materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
                materialSkinManager.ColorScheme = new ColorScheme(Primary.Yellow700, Primary.Yellow800, Primary.Yellow800, Accent.Blue200, TextShade.WHITE);
                inputTextBox.BackColor = SystemColors.Control;
                inputTextBox.ForeColor = SystemColors.WindowText;
                outputTextBox.BackColor = SystemColors.Control;
                outputTextBox.ForeColor = SystemColors.WindowText;
                using (RegistryKey reg = Registry.CurrentUser.CreateSubKey(@"Software\Zalexanninev15\Yandex-Translate-Lite"))
                {
                    reg.SetValue("theme", "1");
                }
            }
        }

        private void MaterialRaisedButton1_Click(object sender, EventArgs e)
        {

            if (materialRadioButton1.Checked == true)
            {
                    materialSingleLineTextField1.Clear();
                    lang = "ru-en";
                try
                {
                        outputTextBox.Text = yt.Translate(inputTextBox.Text, lang);
                }
                catch
                {
                    MessageBox.Show(
                   "Похоже у вас нет соединения :<",
                   "Информация",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Information,
                   MessageBoxDefaultButton.Button1,
                   MessageBoxOptions.RightAlign);
                }
            }
            if (materialRadioButton2.Checked == true)
                {
                    materialSingleLineTextField1.Clear();
                    lang = "en-ru";
                try
                {
                    outputTextBox.Text = yt.Translate(inputTextBox.Text, lang);
                }
                catch
                {
                    MessageBox.Show(
                   "Похоже у вас нет соединения :<",
                   "Информация",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Information,
                   MessageBoxDefaultButton.Button1,
                   MessageBoxOptions.RightAlign);
                }
            }
            if ((materialRadioButton1.Checked == false) && (materialRadioButton2.Checked == false) && (materialSingleLineTextField1.Text != "Например: zh-en") && (materialSingleLineTextField1.Text != ""))
            {
                    lang = materialSingleLineTextField1.Text;
                try
                {
                    outputTextBox.Text = yt.Translate(inputTextBox.Text, lang);
                }
                catch
                {
                    MessageBox.Show(
                   "Похоже у вас нет соединения :<",
                   "Информация",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Information,
                   MessageBoxDefaultButton.Button1,
                   MessageBoxOptions.RightAlign);
                }
            }
           if ((materialRadioButton1.Checked == false) && (materialRadioButton2.Checked == false) && ((materialSingleLineTextField1.Text == "Например: zh-en") || (materialSingleLineTextField1.Text == "")))
           {
                    MessageBox.Show(
              "Выберете или введите язык перевода!",
              "Информация",
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
            savefile.FileName = "";
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