using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem
{
    internal interface IIterator<T>
    {
        public T current();
        public void next();
        public bool hasNext();
    }
}
