using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Animations;
using MaterialSkin.Controls;
using MaterialSkin;
using System.Net.NetworkInformation;

namespace Translator
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Ping ping = new Ping();
                PingReply pingReply = null;
                pingReply = ping.Send("yandex.ru");
                if ((pingReply.Status != IPStatus.HardwareError) || (pingReply.Status != IPStatus.IcmpError))
                {
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new Form1());
                }
            }
            catch { MessageBox.Show("Нет соединения с Интернетом!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
    }
