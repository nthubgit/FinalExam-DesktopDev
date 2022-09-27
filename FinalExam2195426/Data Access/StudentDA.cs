using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using FinalExam2195426.Business;
using FinalExam2195426.GUI;

namespace FinalExam2195426.Data_Access
{
    public static class StudentDA
    {
        private static string filePath = Application.StartupPath + @"\Students.dat";
        private static string fileTemp = Application.StartupPath + @"\Temp.dat";
        public static void Save(Student pupil)
        {
            StreamWriter sWriter = new StreamWriter(filePath, true);
            sWriter.WriteLine(pupil.StudentID + "," + pupil.FirstName + "," + pupil.LastName + "," + pupil.PhoneNumber);
            sWriter.Close();
            MessageBox.Show("Student data saved.");
        }
        public static void ListStudents(ListView listViewStudent)
        {
            ///check to see if dat exists - TBD
            StreamReader sReader = new StreamReader(filePath);
            listViewStudent.Items.Clear();

            string line = sReader.ReadLine();
            while (line != null)
            {
                string[] fields = line.Split(',');
                ListViewItem item = new ListViewItem(fields[0]);
                item.SubItems.Add(fields[1]);
                item.SubItems.Add(fields[2]);
                item.SubItems.Add(fields[3]);
                listViewStudent.Items.Add(item);
                line = sReader.ReadLine(); 
            }
            sReader.Close();
        }
        public static List<Student> ListStudents()
        {
            List<Student> listS = new List<Student>();
            ///check to see if exists - TBA
            StreamReader sReader = new StreamReader(filePath);

            string line = sReader.ReadLine();
            while (line != null)
            {
                string[] fields = line.Split(',');
                Student pupil = new Student();
                pupil.StudentID = Convert.ToInt32((fields[0]));
                pupil.FirstName = (fields[1]);
                pupil.LastName = (fields[2]);
                pupil.PhoneNumber = (fields[3]);
                listS.Add(pupil);
                line = sReader.ReadLine();
            }
            sReader.Close();
            return listS;
        }

        public static Student Search(int pupilID)
        {
            Student pupil = new Student();

            StreamReader sReader = new StreamReader(filePath);
            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');
                if (pupilID == Convert.ToInt32(fields[0]))
                {
                    pupil.StudentID = Convert.ToInt32(fields[0]);
                    pupil.FirstName = fields[1];
                    pupil.LastName = fields[2];
                    pupil.PhoneNumber = fields[3];
                    sReader.Close();
                    return pupil;
                }
                line = sReader.ReadLine(); 
            }
            sReader.Close();
            return null;
        }

        public static bool Delete(int pupilID)
        {
            StreamReader sReader = new StreamReader(filePath);
            string line = sReader.ReadLine();
            StreamWriter sWriter = new StreamWriter(fileTemp, true);
            bool found = false;
            while (line != null)
            {

                string[] fields = line.Split(',');
                if ((pupilID) != (Convert.ToInt32(fields[0])))
                {

                    sWriter.WriteLine(fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3]);

                }
                else
                {
                    found = true;
                }
                line = sReader.ReadLine();
            }
            sReader.Close();
            sWriter.Close();

            if (found == true) 
            { 
                File.Delete(filePath); // Problem here : solved, see the Search method
                File.Move(fileTemp, filePath);
                MessageBox.Show("Student record deleted successfully.", "Deletion");
                return found;
            }
            else
            {
                File.Delete(filePath); //still need to swap temp since writing was done
                File.Move(fileTemp, filePath);
                MessageBox.Show("No such Student ID found. Only saved entries in the .dat can be deleted. If you want to clear yellow entries added by \"Add To List\", press \"Clear Temp Items\"", "Failed");
                return found;
                ;           }

        }
        

    }
}
