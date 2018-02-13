namespace Loteria {
    public class Resultado<TJogo> where TJogo : IBilhete<TJogo> {
        public bool Premiado { get; }
        public string Premio { get; }
        public TJogo Bilhete { get; }

        public Resultado (bool premiado, string premio, TJogo bilhete) {
            this.Premiado = premiado;
            this.Premio = premio;
            this.Bilhete = bilhete;
        }
    }
}