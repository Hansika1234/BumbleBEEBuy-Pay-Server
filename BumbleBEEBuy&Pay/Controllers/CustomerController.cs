using BumbleBEEBuy_Pay.Model.DB;
using BumbleBEEBuy_Pay.Model.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Numerics;
using System.Security.Claims;
using System.Text;

namespace BumbleBEEBuy_Pay.Controllers
{
   [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        BumbleBeeDbContext dbContext = new BumbleBeeDbContext();
        DataSet dataset = new DataSet();

       [HttpGet]
       // [Route("getCustomerDetails")]
        public ActionResult<IEnumerable<TblCustomer>> get()
        {
            try
            {
                string StoreProc = "exec Sp_GetCustomerDetails ";
                //var doctor = new SqlParameter("Lname");
                var dataset = dbContext.TblCustomers.FromSqlRaw(StoreProc).ToList();
                return dataset;
            }
            catch (DbException ex)
            {
                throw ex;
            }
        }

        //Customer Register
        [HttpPost]
        //[Route("SaveDoctor")]
        public async Task<string> Create([FromBody] TblCustomer customer)
        {
            //string strpass = encryptpass(TextBox3.Text);

            try
            {

                //string strpass = encryptpass(customer.CusPassword);

                string StoreProc = "exec Sp_SaveCutomerDetails @CusFName,@CusLname,@CusDOB,@CusEmail,@Cusgender,@CusNIC,@CusMobile,@CusPassword,@CusRegisterdate,@Active";

                //encrypt password
                string strpass = encryptpass(customer.CusPassword);
                var CusFirstName = new Microsoft.Data.SqlClient.SqlParameter("@CusFName",customer.CusFirstName);
                var CusLastName = new Microsoft.Data.SqlClient.SqlParameter("@CusLname",customer.CusLastName);
                var CusDateofBirth = new Microsoft.Data.SqlClient.SqlParameter("@CusDOB",customer.CusDateofBirth);
                var CusEmail = new Microsoft.Data.SqlClient.SqlParameter("@CusEmail",customer.CusEmail);
                var CusGender = new Microsoft.Data.SqlClient.SqlParameter("@Cusgender",customer.CusGender);
                var CusNic = new Microsoft.Data.SqlClient.SqlParameter("@CusNIC",customer.CusNic);
                var CusMobileNo = new Microsoft.Data.SqlClient.SqlParameter("@CusMobile",customer.CusMobileNo);
                var CusPassword = new Microsoft.Data.SqlClient.SqlParameter("@CusPassword",strpass);
                var CusRegistrationDate = new Microsoft.Data.SqlClient.SqlParameter("@CusRegisterdate",customer.CusRegistrationDate);
                var CusIsActive = new Microsoft.Data.SqlClient.SqlParameter("@Active",customer.CusIsActive);

                //var Result = new Microsoft.Data.SqlClient.SqlParameter("@Result", SqlDbType.Int);
                //Result.Direction = ParameterDirection.Output;

                await dbContext.Database.ExecuteSqlRawAsync(StoreProc,
                                                               CusFirstName,
                                                               CusLastName,
                                                               CusDateofBirth,
                                                               CusEmail,
                                                               CusGender,
                                                               CusNic,
                                                               CusMobileNo,
                                                               CusPassword,
                                                               CusRegistrationDate,
                                                               CusIsActive
                                                               );
                //Database is API Command

                //return Result.Value.ToString();

                //return Ok;

                return "Customer created successfully";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Encrypted
        private string encryptpass(string? cusPassword)
        {
            string msg = "";
            byte[] encode = new byte[cusPassword.Length];
            encode = Encoding.UTF8.GetBytes(cusPassword);
            msg = Convert.ToBase64String(encode);
            return msg;
        }

        //public string encryptpass(string password)
        //{
        //    string msg = "";
        //    byte[] encode = new byte[password.Length];
        //    encode = Encoding.UTF8.GetBytes(password);
        //    msg = Convert.ToBase64String(encode);
        //    return msg;
        //}


        //delete customer
        [HttpDelete("{id}")]
        public async Task<string> delete(int id)
        {
            try
            {
                string StoreProc = "exec Sp_DeleteCustomer @id";

                var CusID = new Microsoft.Data.SqlClient.SqlParameter("@id",id);

                await dbContext.Database.ExecuteSqlRawAsync(StoreProc,
                                                               CusID
                                                               );
                return "Customer Deleted successfully";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //update Register
        [HttpPost]
        public async Task<string> update([FromBody] TblCustomer customer)
        {
            try
            {
                string StoreProc = "exec Sp_UpdateProduct @CusID,@CusFName,@CusLname,@CusDOB,@CusEmail,@Cusgender,@CusNIC,@CusMobile,@CusPassword,@CusRegisterdate,@Active";

                 var CusID = new Microsoft.Data.SqlClient.SqlParameter("@CusID",customer.CusId);
                var CusFirstName = new Microsoft.Data.SqlClient.SqlParameter("@CusFName",customer.CusFirstName);
                var CusLastName = new Microsoft.Data.SqlClient.SqlParameter("@CusLname",customer.CusLastName);
                var CusDateofBirth = new Microsoft.Data.SqlClient.SqlParameter("@CusDOB",customer.CusDateofBirth);
                var CusEmail = new Microsoft.Data.SqlClient.SqlParameter("@CusEmail",customer.CusEmail);
                var CusGender = new Microsoft.Data.SqlClient.SqlParameter("@Cusgender",customer.CusGender);
                var CusNic = new Microsoft.Data.SqlClient.SqlParameter("@CusNIC",customer.CusNic);
                var CusMobileNo = new Microsoft.Data.SqlClient.SqlParameter("@CusMobile",customer.CusMobileNo);
                var CusPassword = new Microsoft.Data.SqlClient.SqlParameter("@CusPassword",customer.CusPassword);
                var CusRegistrationDate = new Microsoft.Data.SqlClient.SqlParameter("@CusRegisterdate",customer.CusRegistrationDate);
                var CusIsActive = new Microsoft.Data.SqlClient.SqlParameter("@Active",customer.CusIsActive);

                await dbContext.Database.ExecuteSqlRawAsync(StoreProc,
                                                               CusID,
                                                               CusFirstName,
                                                               CusLastName,
                                                               CusDateofBirth,
                                                               CusEmail,
                                                               CusGender,
                                                               CusNic,
                                                               CusMobileNo,
                                                               CusPassword,
                                                               CusRegistrationDate,
                                                               CusIsActive
                                                               );
          
                return "Customer Updated successfully";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update1(int id,[FromBody] TblCustomer customer)
        {
            //// Use LINQ to retrieve the existing entity from the database based on its ID or other unique identifier
            //var existingEntity = await dbContext.TblCustomers.FindAsync(id);

            //if (existingEntity == null)
            //{
            //    return NotFound();
            //}

            //// Map the updated data from the model onto the existing entity
            //existingEntity.CusFirstName= customer.CusFirstName;
            //existingEntity.CusLastName = customer.CusLastName;
            //existingEntity.CusEmail = customer.CusEmail;
            //existingEntity.CusNic = customer.CusNic;
            //existingEntity.CusMobileNo = customer.CusMobileNo;
            //existingEntity.CusPassword = customer.CusPassword;

            //// Save the changes to the database
            //await dbContext.SaveChangesAsync();

            //return Ok();
            if (customer == null || customer.CusId != id)
            {
                return BadRequest();
            }

            var existingEntity = await dbContext.TblCustomers.FirstOrDefaultAsync(t => t.CusId == id);
            if (existingEntity == null)
            {
                return NotFound();
            }

            existingEntity.CusFirstName = customer.CusFirstName;
            existingEntity.CusLastName = customer.CusLastName;
            existingEntity.CusEmail = customer.CusEmail;
            existingEntity.CusNic = customer.CusNic;
            existingEntity.CusMobileNo = customer.CusMobileNo;
            existingEntity.CusPassword = customer.CusPassword;

            dbContext.TblCustomers.Update(existingEntity);
            dbContext.SaveChanges();
            return  new NoContentResult();
        }

       
        [HttpGet("{id}")]
        public async Task<IActionResult> getbyID(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var existingEntity = await dbContext.TblCustomers.FirstOrDefaultAsync(t => t.CusId == id);
            if (existingEntity == null)
            {
                return NotFound();
            }

     
            return Ok(existingEntity);
        }

        //Decrypted
        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] TblCustomer cusnlog)
        {
            LoginResponseDTO loginRespond = new LoginResponseDTO();

         string x = EncodePasswordToBase64(cusnlog.CusPassword);
            // TODO: Encrypt password
            var user = dbContext.TblCustomers
                .Where(t =>
                    t.CusEmail == cusnlog.CusEmail &&
                    t.CusPassword == x

                 )
                 .FirstOrDefault();

            if (user == null)
            {
                return BadRequest("User name or Password Error");
            }
            else
            {
                loginRespond.Mesaage = "User Logged correct";
                return Ok(loginRespond);
            }
           
        }

    }
}
