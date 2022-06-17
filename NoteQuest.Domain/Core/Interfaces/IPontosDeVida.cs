namespace NoteQuest.Domain.Core.Interfaces
{
    public interface IPontosDeVida
    {
        public int Pv { get; set; }
        public int PvMaximo { get; set; }
        public void Alterar(int Pv);
        public void RecuperarTudo();
        public void AlterarMaximo(int Pv);
    }
}
