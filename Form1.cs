using System;
using System.Collections.Generic;

using System.Data;

using System.Linq;

using System.Windows.Forms;
using System.IO;

namespace difzacehet
{
    public partial class Form1 : Form
    {
        private List<Student> students = new List<Student>();
        public Form1()
        {
            InitializeComponent();
            panel1.Visible = false;
            numericUpDown1.Visible = false;
            button3.Enabled = false;
            button2.Enabled = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Файл (*.txt |*.txt|Все файлы (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                try
                {
                    string[] lines = File.ReadAllLines(fileName);
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split(',');
                        int id = int.Parse(parts[0]);
                        string name = parts[1];
                        int score1 = int.Parse(parts[2]);
                        int score2 = int.Parse(parts[3]);
                        int score3 = int.Parse(parts[4]);
                        Student newStudent = new Student(id, name, score1, score2, score3);
                        students.Add(newStudent);
                    }
                    MessageBox.Show("Данные загружены из файла.", "Успешно");
                    button3.Enabled = true;
                    button2.Enabled = true;
                }
                catch 
                {
                    MessageBox.Show($"Ошибка загрузки данных из файла");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            
        }
        

        private void button3_Click(object sender, EventArgs e)
        {
            numericUpDown1.Visible = true;
            int n = (int)numericUpDown1.Value;
            var topN = students.OrderByDescending(s => s.GetSumScore()).Take(n).ToList();
            listBox1.Items.Clear();
            foreach (var student in topN)
            {
                listBox1.Items.Add($"Индификатор: {student.Id}, Имя: {student.Name}, набрал баллов: {student.GetSumScore()} ({student.Score1}+{student.Score2}+{student.Score3})");
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {

            try
            {
                string name = textBox2.Text;
                int score1 = int.Parse(textBox3.Text);
                int score2 = int.Parse(textBox4.Text);
                int score3 = int.Parse(textBox5.Text);

                Random rnd = new Random();
                int id;
                do
                {
                    id = rnd.Next(1000, 9999);
                } while (students.Any(s => s.Id == id));

                Student newStudent = new Student(id, name, score1, score2, score3);
                if (score1 >= 0 && score1 < 999)
                {
                    if (score2 >= 0 && score2 < 999)
                    {
                        if (score3 >= 0 && score3 < 999)
                        {
                            if (!students.Contains(newStudent))
                            {
                                students.Add(newStudent);
                                SaveToFile();
                            }                          
                        }
                        else { MessageBox.Show("Вы ввели неточное значение баллов 3", "Ошибка"); }
                    }
                    else { MessageBox.Show("Вы ввели неточное значение баллов 2", "Ошибка"); }
                }
                else { MessageBox.Show("Вы ввели неточное значение баллов 1", "Ошибка"); }
               
            }
            catch
            {
                MessageBox.Show("Вы ввели неправильные значения в баллы", "Ошибка");
            }
        }
                   
              
        
        private void SaveToFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter= "Файл (*.txt |*.txt|Все файлы (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog.FileName;
                try
                {
                    using (StreamWriter rt = new StreamWriter(fileName))
                    {
                        foreach (Student student in students)
                        {
                            rt.WriteLine($"{student.Id}, {student.Name}, {student.Score1}, {student.Score2}, {student.Score3}");
                        } 
                    }
                    MessageBox.Show("Данны сохранены в файл", "Успешно");
                    panel1.Visible = false;
                }
                catch
                {
                    MessageBox.Show($"Ошибка загрузки данных из файла");
                }
            }
        }
    }
}
