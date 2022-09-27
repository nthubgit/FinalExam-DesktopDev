using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FinalExam2195426.Business;

namespace FinalExam2195426.Validation
{
    public static class Validator
    {
        public static bool IsValidID(TextBox text)
        {
            int tempID;
            if ((text.TextLength != 7) || !((Int32.TryParse(text.Text, out tempID))))
            //if ((text.TextLength != 7))
            {
                MessageBox.Show("Student ID must be 7 digits.", "Invalid Student ID");
                text.Clear();
                text.Focus();
                return false;
            }
            return true;

        }

        public static bool IsValidName(TextBox text)
        {
            for (int i = 0; i < text.TextLength; i++)
            {
                if (char.IsDigit(text.Text, i) || (char.IsWhiteSpace(text.Text, i)))
                {
                    MessageBox.Show("First Name and Last Name cannot contain spaces or numbers.", "Invalid Name(s)");
                    text.Clear();
                    text.Focus();
                    return false;
                }

            }
            return true;

        }
    }
}
