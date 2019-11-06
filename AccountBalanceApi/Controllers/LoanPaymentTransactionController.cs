using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using AccountBalanceApi.Models;

namespace AccountBalanceApi.Controllers
{
    [RoutePrefix("provisio/api/v1000/_transactions")]
    public class LoanPaymentTransactionController : ApiController

    {
        TransactionServices services = new TransactionServices();


        //    // GET: LoanPaymentTransaction
        //    [Route("get_transactons")]
        //    [ResponseType(typeof(Transactions))]
        //    public IHttpActionResult Get()
        //    {
        //        var list = services.GetTransactions();
        //        return Ok(list);
        //    }

        /// <summary>
        /// Get Bank repayment By RefNo
        /// </summary>
        /// <param name="ref_number"></param>
        /// <returns>Momo repayment Account</returns>       
        [ResponseType(typeof(MomoTransaction))]
        [Route("maccount/{ref_number}/mp-500")]
        public IHttpActionResult GetByMomoNumber(string ref_number)
        {
            var transaction = services.GetMomoTransactionByRefNo(ref_number);
            if (transaction == null)
                return StatusCode(HttpStatusCode.NotFound);
            return Ok(transaction);
        }

        /// <summary>
        /// Get Bank Repayment By RefNo
        /// </summary>
        /// <param name="ref_number"></param>
        /// <returns>Bank Account</returns>   
        [ResponseType(typeof(BankTransaction))]
        [Route("baccount/{ref_number}/bp-100")]
        public IHttpActionResult GetByBankAccount(string ref_number)
        {
            var transaction = services.GetBankTransactionByRefNo(ref_number);
            if (transaction == null)
                return StatusCode(HttpStatusCode.NotFound);
            return Ok(transaction);
        }


        // POST: api/LoanPaymentTransaction
        [Route("_bankpost/bp-100/repayments")]
        public IHttpActionResult PostBankAccount([FromBody]BankTransaction banktransaction)
        {
            if (string.IsNullOrWhiteSpace(banktransaction.B_AccountNumber))
                return StatusCode(HttpStatusCode.BadRequest);
            else
            {
                var result = services.PostBankTransaction(banktransaction);

                string responseTime = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt");

                return result == "Success" ? Ok($"S-100-B {responseTime}") : Ok($"R-500-B {responseTime}");
            }
        }


        // POST: api/LoanPaymentTransaction
        [Route("_momopost/mp-500/repayments")]
        public IHttpActionResult PostByMomoNumber([FromBody]MomoTransaction momotransaction)
        {

            if (string.IsNullOrWhiteSpace(momotransaction.MomoNumber))
                return StatusCode(HttpStatusCode.BadRequest);
            else
            {
                var result = services.PostMomoTransaction(momotransaction);

                string responseTime = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt");

                return result == "Success" ? Ok($"S-100-M {responseTime}") : Ok($"R-500-M {responseTime}");
            }
        }


    }
}
