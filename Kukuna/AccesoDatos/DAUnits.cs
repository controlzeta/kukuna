using AccesoDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class DAUnits
    {
        public List<Unit> GetUnits()
        {
            try
            {
                using (var context = new SqlServerDbContext())
                {
                    return context.Units.OrderBy(i => i.UnitId).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
