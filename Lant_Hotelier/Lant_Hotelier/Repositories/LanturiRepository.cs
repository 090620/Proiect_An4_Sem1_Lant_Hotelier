using Lant_Hotelier.Data;
using Lant_Hotelier.Models;

namespace Lant_Hotelier.Repositories
{
    public class LanturiRepository : GenericRepository<LantHotelier>
    {
        public LanturiRepository(ApplicationDbContext context) : base(context) { }
        
    }
}