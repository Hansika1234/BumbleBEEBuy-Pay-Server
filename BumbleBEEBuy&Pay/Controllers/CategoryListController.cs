using BumbleBEEBuy_Pay.Model.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace BumbleBEEBuy_Pay.Controllers
{
   [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryListController : ControllerBase
    {

        BumbleBeeDbContext dbContext = new BumbleBeeDbContext();
        DataSet dataset = new DataSet();

        [HttpGet]
       /// [Route("getCategoryDetails")]
        public ActionResult<IEnumerable<TblCategory>> get()
        {
            try
            {
                string StoreProc = "exec Sp_GetCategory ";
                //var doctor = new SqlParameter("Lname");
                var dataset = dbContext.TblCategories.FromSqlRaw(StoreProc).ToList();
                return dataset;
            }
            catch (DbException ex)
            {
                throw ex;
            }
        }










    }
}
