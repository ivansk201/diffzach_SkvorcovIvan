using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace difzacehet
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Score1 { get; set; }
        public int Score2 { get; set; }
        public int Score3 { get; set; }      
        public Student(int id, string name, int score1, int score2, int score3)
        {
            Id = id;
            Name = name;
            Score1 = score1;
            Score2 = score2;
            Score3 = score3;
        }
        public int GetSumScore()
        {
            return Score1 + Score2 + Score3;
        }
       
    }
}
