using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{

    // The 'Component' abstract class
    abstract class LibraryItem

    {
        private int _numCopies;

        // Property

        public int NumCopies //count of items
        {
            get { return _numCopies; }
            set { _numCopies = value; }
        }

        public abstract void Display(); //Show info about item
    }

}
