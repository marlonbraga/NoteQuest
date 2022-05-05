using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoteQuest.Domain.Core;

namespace NoteQuest.UnitTest
{
    [TestClass]
    public class D6Test
    {
        [TestMethod]
        public void D6_Rolagem_Sucesso()
        {
            int resultado;
            for (int i = 0; i < 50; i++)
            {
                resultado = D6.Rolagem();

                Assert.IsTrue(resultado <= 6, $"D6: {resultado}");
                Assert.IsTrue(resultado >= 1, $"D6: {resultado}");
            }
        }
    }
}