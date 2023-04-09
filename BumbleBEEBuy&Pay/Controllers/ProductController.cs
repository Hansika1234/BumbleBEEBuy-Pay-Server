using BumbleBEEBuy_Pay.Model.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BumbleBEEBuy_Pay.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        BumbleBeeDbContext dbContext = new BumbleBeeDbContext();
        DataSet dataset = new DataSet();

    [HttpGet]
        public ActionResult<IEnumerable<TblProduct>> get()
        {
            try
            {
                string StoreProc = "exec Sp_GetProductDetails ";
                var dataset = dbContext.TblProducts.FromSqlRaw(StoreProc).ToList();
                return dataset;
            }
            catch (DbException ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<string> Create([FromBody] TblProduct product)
        {
            try
            {
                string StoreProc = "exec Sp_AddNewProductDetails @Category,@ProductName,@Productbrand,@unitprice";

                var Category = new Microsoft.Data.SqlClient.SqlParameter("@Category", product.Category);
                var ProductName = new Microsoft.Data.SqlClient.SqlParameter("@ProductName", product.ProductName);
                var ProductBrand = new Microsoft.Data.SqlClient.SqlParameter("@Productbrand", product.ProductBrand);
                var UnitPrice = new Microsoft.Data.SqlClient.SqlParameter("@unitprice", product.UnitPrice);

                await dbContext.Database.ExecuteSqlRawAsync(StoreProc,
                                                               Category,
                                                               ProductName,
                                                               ProductBrand,
                                                               UnitPrice
                                                               );

                return "Product created successfully";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //delete customer
        [HttpDelete("{id}")]
        public async Task<string> delete(int id)
        {
            try
            {
                string StoreProc = "exec Sp_DeleteProduct @id";

                var CusID = new Microsoft.Data.SqlClient.SqlParameter("@id",id);

                await dbContext.Database.ExecuteSqlRawAsync(StoreProc,
                                                               CusID
                                                               );
                return "Product Deleted successfully";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //get product
        [HttpGet("{id}")]
        public async Task<IActionResult> getbyID(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var existingEntity = await dbContext.TblProducts.FirstOrDefaultAsync(t => t.ProductId == id);
            if (existingEntity == null)
            {
                return NotFound();
            }


            return Ok(existingEntity);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> update1(int id, [FromBody] TblProduct product)
        {
            
            if (product == null || product.ProductId != id)
            {
                return BadRequest();
            }

            var existingEntity = await dbContext.TblProducts.FirstOrDefaultAsync(t => t.ProductId == id);
            if (existingEntity == null)
            {
                return NotFound();
            }

            existingEntity.ProductName = product.ProductName;
            existingEntity.Category = product.Category;
            existingEntity.ProductBrand = product.ProductBrand;
            existingEntity.UnitPrice = product.UnitPrice;
         

            dbContext.TblProducts.Update(existingEntity);
            dbContext.SaveChanges();
            return new NoContentResult();
        }


        //[HttpPost]
        //public async Task<IActionResult> update([FromBody] TblProduct product)
        //{

        //    if (product == null || product.ProductId != id)
        //    {
        //        return BadRequest();
        //    }

        //    var existingEntity = await dbContext.TblProducts.FirstOrDefaultAsync(t => t.ProductId == id);
        //    if (existingEntity == null)
        //    {
        //        return NotFound();
        //    }

        //    existingEntity.ProductName = product.ProductName;
        //    existingEntity.Category = product.Category;
        //    existingEntity.ProductBrand = product.ProductBrand;
        //    existingEntity.UnitPrice = product.UnitPrice;


        //    dbContext.TblProducts.Update(existingEntity);
        //    dbContext.SaveChanges();
        //    return new NoContentResult();
        //}



        //[HttpPost]
        //public IActionResult LogingCustomer(UserLogin objuserlogin)
        //{
        //    if (dbContext.Patients.Where(u => u.PEmail == objuserlogin.PEmail && u.PPassword == objuserlogin.PPassword).Any())
        //    {
        //        try
        //        {
        //            Patient user = dbContext.Patients.
        //            Where(u => u.PEmail == objuserlogin.PEmail && u.PPassword == objuserlogin.PPassword).FirstOrDefault();

        //            // var result = dbContex.SaveChanges();

        //            return Ok("Loging Successfully !..");
        //        }
        //        catch
        //        {
        //            return Ok(JsonConvert.SerializeObject("Invalida userName Or Password"));
        //        }
        //    }
        //    else
        //    {
        //        return Ok(JsonConvert.SerializeObject("Wrong userName 0R Password"));
        //    }
        //}

        [HttpPost]
        public async Task<string> update([FromBody] TblProduct product)
        {
            try
            {
                string StoreProc = "exec Sp_UpdateProduct @id,@Category,@ProductName,@Productbrand,@unitprice";

                var ProductID = new Microsoft.Data.SqlClient.SqlParameter("@id",product.ProductId);
                var Category = new Microsoft.Data.SqlClient.SqlParameter("@Category",product.Category);
                var ProductName = new Microsoft.Data.SqlClient.SqlParameter("@ProductName",product.ProductName);
                var ProductBrand = new Microsoft.Data.SqlClient.SqlParameter("@Productbrand",product.ProductBrand);
                var UnitPrice = new Microsoft.Data.SqlClient.SqlParameter("@unitprice",product.UnitPrice);

                await dbContext.Database.ExecuteSqlRawAsync(StoreProc,
                                                               ProductID,
                                                               Category,
                                                               ProductName,
                                                               ProductBrand,
                                                               UnitPrice
                                                               );

                return "Product updated successfully";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
