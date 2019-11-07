using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restautrant
{
    public partial class Core : Form
    {
        protected LKSN2018Entities1 db = new LKSN2018Entities1();
        public Core()
        {
            InitializeComponent();
        }
    }

    public static class Extension
    {
        private static Random random = new Random();
        private static readonly string characters = "QWERTYUIOPASDFGHJKLZXCVBNM123456789";

        public static bool IsAnyEmpty(this Control.ControlCollection controls, params Control[] exception)
        {
            foreach (Control control in controls)
            {
                if (!exception.Contains(control) && control is TextBox && control.Text.Trim() == "")
                {
                    return true;
                }
            }
            return false;
        }

        public static string ToMD5(this string text)
        {
            MD5 sha = MD5.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(text));
            return BitConverter.ToString(bytes).Replace("-", "");
        }

        public static bool IsNumber(this string text)
        {
            if (text.All(Char.IsNumber) && text.Trim() != "")
            {
                return true;
            }
            return false;
        }

        public static void ClearFields(this Control.ControlCollection controls, params Control[] exception)
        {
            foreach (Control control in controls)
            {
                if (!exception.Contains(control) && control is TextBox)
                {
                    control.Text = "";
                }
            }
        }

        public static bool IsEmail(this string text)
        {
            var email = new EmailAddressAttribute();
            if (email.IsValid(text))
            {
                return true;
            }
            return false;
        }
    }
}
