using NoteQuest.CLI.Interfaces;

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
