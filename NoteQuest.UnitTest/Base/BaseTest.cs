using NoteQuest.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteQuest.UnitTest.Base
{
    public abstract class BaseTest
    {
        public IContainer Container { get; set; }

        public BaseTest()
        {
            Container = new Container();
        }
    }
}
