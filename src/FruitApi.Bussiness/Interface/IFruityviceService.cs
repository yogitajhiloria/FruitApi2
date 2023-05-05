using FruitApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitApi.Bussiness.Interface
{
    public interface IFruityviceService
    {
        Task<IEnumerable<Fruits>> GetAllFruits(CancellationToken cancellationToken);

        Task<IEnumerable<Fruits>> RetriveAllFruitsForFamnily(string familyName, CancellationToken cancellationToken);
    }
}
