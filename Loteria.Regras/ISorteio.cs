namespace Loteria
{
    public interface ISorteio<TJogo> where TJogo : IBilhete<TJogo> {
        TJogo Sortear();
    }
}
