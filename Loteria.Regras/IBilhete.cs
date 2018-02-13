namespace Loteria
{
    public interface IBilhete<TJogo> where TJogo : IBilhete<TJogo> {        
        Resultado<TJogo> Conferir(TJogo resultado);
    }
}
