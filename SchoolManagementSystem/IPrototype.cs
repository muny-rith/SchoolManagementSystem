using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem
{
    internal interface IPrototype<T>
    {
        public T clone();
    }
}
