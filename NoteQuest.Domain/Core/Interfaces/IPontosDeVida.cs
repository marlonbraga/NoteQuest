namespace NoteQuest.Domain.Core.Interfaces
{
    public interface IPontosDeVida
    {
        public int Pv { get; }
        public int PvMaximo { get; }
        public void Alterar(int Pv);
        public void RecuperarTudo();
        public void AlterarMaximo(int Pv);
        public void ReceberDano(int pv, out bool morreu);
    }
}
