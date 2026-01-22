namespace GateAPI.Application.Common.Models
{
    public class ListaPaginada(IEnumerable<object> lista, int total)
    {
        public IEnumerable<object> Lista { get; private set; } = lista;
        public int Total { get; private set; } = total;
    }
}
