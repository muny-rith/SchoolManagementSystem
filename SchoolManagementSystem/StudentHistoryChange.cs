using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem
{
    internal class StudentHistoryChange
    {
        private List<StudentMemento> mementos = new List<StudentMemento> { };
        private int index = 0;

        public void addMemento(StudentMemento memento)
        {
            //index++;
            mementos.Add(memento);
        }
        public StudentMemento GetMemento()
        {
            if(mementos.Count == 0)
            {
                return null;
            }
            return mementos.Last();
        }
    }
}
