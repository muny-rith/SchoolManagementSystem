using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem
{
    internal interface IStrategyUserControl<T>
    {
        void viewDgv(List<T> pageData);
        void information();
        void insert();
        void update();
        void delete();
        void search(string data);
    }
}
