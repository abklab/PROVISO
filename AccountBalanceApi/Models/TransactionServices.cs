using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace AccountBalanceApi.Models
{
    public class TransactionServices
    {
        #region Data Access Operations

        string connectionString = ConfigurationManager.ConnectionStrings["PROVISIO_ConnectionString"].ToString();

        SqlConnection connection;

        DataTable GetData(SqlCommand cmd)
        {
            DataTable data = new DataTable();
            string msg = "";
            try
            {
                connection = new SqlConnection(connectionString);
                using (connection)
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(data);

                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return data;
        }

        string Save(SqlCommand cmd)
        {
            connection = new SqlConnection(connectionString);
            string msg = "";
            try
            {
                using (connection)
                {

                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    int i = cmd.ExecuteNonQuery();
                    msg = i > 0 ? "Success" : "Fail";

                }
                return msg;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            finally
            {
                connection.Close();

                cmd.Dispose();
            }
            return msg;
        }
        #endregion
        //Get All
        public IEnumerable<Transactions> GetTransactions()
        {
            try
            {
                using (var command = new SqlCommand())
                {
                    var transactionslist = new List<Transactions>();

                    command.CommandText = "[usp_Get_All_Transactions]";

                    var dt = GetData(command);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            var transaction = new Transactions
                            {
                                EntryID = (int)r["EntryID"],
                                RefNo = r["RefNo"].ToString(),
                                Amount = Convert.ToDecimal(r["Amount"]),
                                B_AccountNumber = r["B_AccountNumber"].ToString(),                               
                                MomoNumber = r["MomoNumber"].ToString(),
                                MNO = r["MNO"].ToString(),                                
                                LastUpdated = r["LastUpdated"].ToString()
                            };
                            transactionslist.Add(transaction);
                        }
                    }
                    return transactionslist;

                }


            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            return null;
        }
        //Get Transaction by RefNo
        public BankTransaction GetBankTransactionByRefNo(string ref_number)
        {
            using (var command = new SqlCommand())
            {
                command.CommandText = "usp_Get_Bank_Transaction_RefNo";
                command.Parameters.AddWithValue("@refNo", ref_number);

                var dt = GetData(command);

                if (dt != null && dt.Rows.Count > 0)
                {

                    var r = dt.Rows[0];

                    var transaction = new BankTransaction
                    {
                        EntryID = (int)r["EntryID"],
                        RefNo = r["RefNo"].ToString(),
                        Amount = Convert.ToDecimal(r["Amount"]),
                        B_AccountNumber = r["B_AccountNumber"].ToString(),
                       
                        LastUpdated = r["LastUpdated"].ToString()
                    };
                    return transaction;
                }

                return null;
            }
        }

        public MomoTransaction GetMomoTransactionByRefNo(string ref_number)
        {
            using (var command = new SqlCommand())
            {
                command.CommandText = "usp_Get_Momo_Transaction_RefNo";
                command.Parameters.AddWithValue("@refNo", ref_number);

                var dt = GetData(command);

                if (dt != null && dt.Rows.Count > 0)
                {

                    var r = dt.Rows[0];

                    var transaction = new MomoTransaction
                    {
                        EntryID = (int)r["EntryID"],
                        RefNo = r["RefNo"].ToString(),
                        Amount = Convert.ToDecimal(r["Amount"]),                      
                        MomoNumber = r["MomoNumber"].ToString(),
                        MNO = r["MNO"].ToString(),
                        LastUpdated = r["LastUpdated"].ToString()
                    };
                    return transaction;
                }

                return null;
            }
        }

        //Add or Post Transactions
        public string PostBankTransaction(BankTransaction transaction)
        {
            string msg = "";
            try
            {
               
                var last_accessed = DateTime.Now;
                using (var command = new SqlCommand())
                {
                    command.CommandText = "[usp_Bank_LoanRepayment]";

                    command.Parameters.AddWithValue("@RefNo ", transaction.RefNo);
                    command.Parameters.AddWithValue("@B_AccountNumber ", transaction.B_AccountNumber);
                    command.Parameters.AddWithValue("@Amount", transaction.Amount);

                    var result = Save(command);

                    msg = result;
                }
            }
            catch (Exception ex)
            {
                 msg = ex.Message;
            }
            return msg;
        }

        //Add or Post Transactions
        public string PostMomoTransaction(MomoTransaction transaction)
        {
            string msg = "";
            try
            {

                var last_accessed = DateTime.Now;
                using (var command = new SqlCommand())
                {
                    command.CommandText = "[usp_Momo_LoanRepayment]";

                    command.Parameters.AddWithValue("@RefNo ", transaction.RefNo);
                    command.Parameters.AddWithValue("@MomoNumber ", transaction.MomoNumber);
                    command.Parameters.AddWithValue("@MNO ", transaction.MNO);
                    command.Parameters.AddWithValue("@Amount", transaction.Amount);

                    var result = Save(command);

                    msg = result;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }

    }
}