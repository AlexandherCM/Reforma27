using Condominios.Models.ViewModels.Catalogos;
using Condominios.Models;

namespace Condominios.Data.Repositories.Catalogos
{
    public abstract class Catalogo
    {
        protected readonly Context context;
        protected CatalogoViewModel viewModel = new();
        public Catalogo(Context context)
        {
            this.context = context;
        }
    }
}
