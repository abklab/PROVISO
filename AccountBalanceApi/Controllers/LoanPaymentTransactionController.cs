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
        // GET: LoanPaymentTransaction
        [Route("get_transactons")]
        [ResponseType(typeof(Transactions))]
        public IHttpActionResult Get()
        {
            var list = services.GetTransactions();
            return Ok(list);
        }

        /// <summary>
        /// Get By RefNo
        /// </summary>
        /// <param name="ref_number"></param>
        /// <returns></returns>
        
        [ResponseType(typeof(Transactions))]
        [Route("{ref_number}/refno")]
        public IHttpActionResult Get(string ref_number)
        {
            var transaction = services.GetTransactionByRefNo(ref_number);
            return Ok(transaction);
        }

        // POST: api/LoanPaymentTransaction
        [Route("_post/repayments/bankpayment")]
        public IHttpActionResult PostBankAccount([FromBody]BankTransaction banktransaction)
        {
            if (string.IsNullOrWhiteSpace(banktransaction.B_AccountNumber))
                return StatusCode(HttpStatusCode.BadRequest);
            else
            {
                var transaction = new Transactions
                {
                    RefNo = banktransaction.RefNo,
                    Amount = banktransaction.Amount,
                    B_AccountNumber = banktransaction.B_AccountNumber,
                    MomoNumber="n/a",
                    MNO="n/a",
                                   
                };
                var result = services.PostTransaction(transaction);

                string responseTime = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt");

                return result == "Success" ? Ok($"S-100-B {responseTime}") : Ok($"R-500-B {responseTime}");             
            }
        }

        // POST: api/LoanPaymentTransaction
        [Route("_post/repayments/momopayment")]
        public IHttpActionResult PostByMomoNumber([FromBody]MomoTransaction momotransaction)
        {
            
            if ( string.IsNullOrWhiteSpace(momotransaction.MomoNumber))
                return StatusCode(HttpStatusCode.BadRequest);
            else
            {
                var transaction = new Transactions
                {
                    RefNo = momotransaction.RefNo,
                    Amount = momotransaction.Amount,
                    MomoNumber = momotransaction.MomoNumber,
                    MNO = momotransaction.MNO,
                  B_AccountNumber  = "n/a",
                };

                var result = services.PostTransaction(transaction);

                string responseTime = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt");

                return result == "Success" ? Ok($"S-100-M {responseTime}") : Ok($"R-500-M {responseTime}");
            }
        }


    }
}
