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
using Microsoft.Win32;

namespace Translator
{
    public partial class Form1 : MaterialForm
    {
[DllImport("user32", CharSet = CharSet.Auto)]
        internal extern static bool PostMessage(IntPtr hWnd, uint Msg, uint WParam, uint LParam);
        [DllImport("user32", CharSet = CharSet.Auto)]
#pragma warning disable CS0108
        internal extern static bool ReleaseCapture();
#pragma warning restore CS0108
        const uint WM_SYSCOMMAND = 0x0112;
        const uint DOMOVE = 0xF012;
        const uint DOSIZE = 0xF008;
        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;
        private bool m_aeroEnabled;
        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;
        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);
        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
            );
        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }
        protected override CreateParams CreateParams
        {
            get
            {
                m_aeroEnabled = CheckAeroEnabled();
                CreateParams cp = base.CreateParams;
                if (!m_aeroEnabled)
                    cp.ClassStyle |= CS_DROPSHADOW; return cp;
            }
        }
        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0; DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1) ? true : false;
            }
            return false;
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCPAINT:
                    if (m_aeroEnabled)
                    {
                        var v = 2;
                        DwmSetWindowAttribute(this.Handle, 2, ref v, 4);
                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 0,
                            rightWidth = 0,
                            topHeight = 0
                        }; DwmExtendFrameIntoClientArea(this.Handle, ref margins);
                    }
                    break;
                default: break;
            }
            base.WndProc(ref m);
        }

        // =======================================================================================================================
        // =======================================================================================================================
        // =======================================================================================================================

        Form f;
        string lang, theme;
        int form2 = 0;
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
                   MessageBoxIcon.Information);
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
                   MessageBoxIcon.Information);
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
                   MessageBoxIcon.Information);
                }
            }
           if ((materialRadioButton1.Checked == false) && (materialRadioButton2.Checked == false) && ((materialSingleLineTextField1.Text == "Например: zh-en") || (materialSingleLineTextField1.Text == "")))
           {
                    MessageBox.Show(
              "Выберете или введите язык перевода!",
              "Информация",
              MessageBoxButtons.OK,
              MessageBoxIcon.Information);
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
        foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "Form2")
                {
                    form2 = 1;
                }
                else { form2 = 0; }
            }
            if (form2 == 0)
            {
                f = new Form2();
                f.Show();
            }
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
