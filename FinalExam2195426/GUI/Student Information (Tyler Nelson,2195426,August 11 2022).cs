using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FinalExam2195426.Business;
using FinalExam2195426.Data_Access;
using FinalExam2195426.Validation;

namespace FinalExam2195426.GUI
{
    public partial class Student_Information__Tyler_Nelson_2195426_August_11_2022_ : Form
    {
        List<Student> listS = new List<Student>();
        public Student_Information__Tyler_Nelson_2195426_August_11_2022_()
        {
            InitializeComponent();
        }

        private void Student_Information__Tyler_Nelson_2195426_August_11_2022__Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonAddToList_Click(object sender, EventArgs e)
        {
            Student pupil = new Student();
            if ((Validator.IsValidID(textBoxStudentID) && (Validator.IsValidName(textBoxFirstName)) && (Validator.IsValidName(textBoxLastName))))
            {
                listViewStudent.Items.Clear();
                pupil.StudentID = Convert.ToInt32(textBoxStudentID.Text);
                pupil.FirstName = textBoxFirstName.Text;
                pupil.LastName = textBoxLastName.Text;
                pupil.PhoneNumber = maskedTextBoxPhoneNumber.Text;
                //Add to the list
                listS.Add(pupil);

                StudentDA.ListStudents(listViewStudent);
                //First adds from DB to ListView, then from List to ListView
                listS.ForEach(s =>
                {
                    ListViewItem item = new ListViewItem(s.StudentID + "");
                    item.BackColor = Color.Yellow;
                    item.SubItems.Add(s.FirstName);
                    item.SubItems.Add(s.LastName);
                    item.SubItems.Add(s.PhoneNumber);
                    listViewStudent.Items.Add(item);
                });
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if ((Validator.IsValidID(textBoxStudentID) && (Validator.IsValidName(textBoxFirstName)) && (Validator.IsValidName(textBoxLastName))))
            {
                //List<Student> listS = StudentDA.ListStudents();

                Student pupil = new Student();

                pupil.StudentID = Convert.ToInt32(textBoxStudentID.Text);
                pupil.FirstName = textBoxFirstName.Text;
                pupil.LastName = textBoxLastName.Text;
                pupil.PhoneNumber = maskedTextBoxPhoneNumber.Text;

                StudentDA.Save(pupil);
                listS.Clear(); //need to clear listS or dupes will occur when listing
                StudentDA.ListStudents(listViewStudent);
            }
   
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show("Are you sure to exit the application?", "Confirmation",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void buttonListStudents_Click(object sender, EventArgs e)
        {
            listViewStudent.Items.Clear();
            StudentDA.ListStudents(listViewStudent);
            //First adds from DB to ListView, then from List to ListView
            listS.ForEach(s =>
            {
                ListViewItem item = new ListViewItem(s.StudentID + "");
                item.BackColor = Color.Yellow;
                item.SubItems.Add(s.FirstName);
                item.SubItems.Add(s.LastName);
                item.SubItems.Add(s.PhoneNumber);
                listViewStudent.Items.Add(item);
            });
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (Validator.IsValidID(textBoxStudentID)){
                bool needRefresh = StudentDA.Delete(Convert.ToInt32(textBoxStudentID.Text));
                if (needRefresh)
                {
                    StudentDA.ListStudents(listViewStudent);
                }
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            int choice = comboBoxSearchBy.SelectedIndex;

            switch (choice)
            {
                case -1: // IF the user NOT select any search option

                    MessageBox.Show("Please select a Search criteria.", "Error");
                    break;

                case 0:
                    Student pupil = StudentDA.Search(Convert.ToInt32(textBoxInputInfo.Text));

                    if (pupil != null)
                    {
                        textBoxStudentID.Text = (pupil.StudentID).ToString();
                        textBoxFirstName.Text = pupil.FirstName;
                        textBoxLastName.Text = pupil.LastName;
                        maskedTextBoxPhoneNumber.Text = pupil.PhoneNumber;
                    }
                    else
                    {

                        MessageBox.Show("Student not found.", "Failed");
                    }
                    break;




                default:   // IF the user NOT select an option on the  search combo box
                    break;
            }
        }

        private void buttonClearAddToList_Click(object sender, EventArgs e)
        {
            listS.Clear();
            StudentDA.ListStudents(listViewStudent);
        }
    }
}
