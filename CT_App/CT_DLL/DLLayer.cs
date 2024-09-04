using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CT_App.Models;

namespace CT_App.CT_DLL
{
    public class DLLayer
    {
        #region Comments
        OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\CT_DB.accdb;Jet OLEDB:Database Password=*3455*00;");
        OdbcConnection conne = new OdbcConnection(@"Dsn=RETAILMasterSHOPS;uid=sa;Pwd=Ajwahana$@$;");
        private string connAcc = (@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\CT_DB.accdb;Jet OLEDB:Database Password=*3455*00;");
        private string connSql = (@"Dsn=RETAILMasterSHOPS;uid=sa;Pwd=Ajwahana$@$;");
        #endregion


        //------------------------------ Market / Mamo --------------------------
        //----------------------------------------------------------------------- 
        public List<DataTable> GetMarketData()
        {
            string[] queries = {
                $"SELECT M_ID AS [ID], M_Date AS [Date], M_Amount AS [Amount] FROM Market ORDER BY M_Date DESC",
                $"SELECT Mem_ID as [ID], Mem_Date as [Date], Giv_TK as [Given], R_InvTK as [Main], C_InvTK as [CAmt], Ret_TK as [Return] FROM MarketMemos ORDER BY Mem_Date DESC"
            };
            var dataTables = new List<DataTable>();
            foreach (var query in queries)
            {
                DataTable dataTable = new DataTable();
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                {
                    adapter.Fill(dataTable);
                }
                dataTables.Add(dataTable);
            }
            return dataTables;
        }
        public List<DataTable> GetDataAllInstaTable()
        {
            string[] queries = {
                $"SELECT I_ID AS [ID], InsPay_Date AS [Date], InsPay AS [PayAmt] FROM Installment WHERE Take_Data = 'INS' ORDER BY [ID] DESC",
                $"SELECT I_ID AS [ID], I_Date AS [Date], Take_Total AS [Total], Take_Anot AS [Anot], Take_Mine AS [Mine] FROM Installment WHERE Take_Data = 'NPD' ORDER BY [ID] DESC",
                $"SELECT B_Next_ODO AS [ODO], B_Chng_Date AS [Date], B_ID AS [ID] FROM BikeInfo ORDER BY B_Chng_Date DESC"
            };
            var dataTables = new List<DataTable>();
            foreach (var query in queries)
            {
                DataTable dataTable = new DataTable();
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                {
                    adapter.Fill(dataTable);
                }
                dataTables.Add(dataTable);
            }
            return dataTables;
        }
        public List<DataTable> GetDataAllDailySavTable()
        {
            string[] queries = {
                $"SELECT DS_ID AS [ID], DS_Date AS [Date], NotTaken FROM DailySaving WHERE [DS_Data] = 'NTKN' ORDER BY [DS_Date] DESC"
            };
            var dataTables = new List<DataTable>();
            foreach (var query in queries)
            {
                DataTable dataTable = new DataTable();
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                {
                    adapter.Fill(dataTable);
                }
                dataTables.Add(dataTable);
            }
            return dataTables;
        }
        public List<DataTable> GetDataAllCrTable()
        {
            string[] queries = {
                $"SELECT InGiven AS [ID], Given_To AS [Name], Total_Given AS [GTK], Given_Date AS [GDT] FROM Given WHERE [GDT_V] = 'NDV' ORDER BY [ID] DESC",
                $"SELECT InTake AS [ID], Take_To AS [Name], Total_Take AS [TTK], Take_Date AS [TDT] FROM Teken WHERE [TDT_V] = 'NDV' ORDER BY [ID] DESC",
                $"SELECT InExpense AS [ID], Expense_To AS [Name], Expense_Amount AS [ETK], Expense_Date AS [EDT] FROM TariffAmt WHERE [EDT_V] = 'NDV' ORDER BY [ID] DESC",
                $"SELECT InSaving AS [ID], Saving_To AS [Name], Saving_Amount AS [STK], Saving_Date AS [SDT] FROM Saving WHERE [SDT_V] = 'NDV' ORDER BY [ID] DESC",
                $"SELECT InUnrated AS [ID], Unrated_To AS [Name], Unrated_Amount AS [UTK], Unrated_Date AS [UDT] FROM Unrated WHERE [UDT_V] = 'NDV' ORDER BY [ID] DESC"
            };
            var dataTables = new List<DataTable>();
            foreach (var query in queries)
            {
                DataTable dataTable = new DataTable();
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                {
                    adapter.Fill(dataTable);
                }
                dataTables.Add(dataTable);
            }
            return dataTables;
        }
        public List<DataTable> GetDataAllCutGridTable()
        {
            string[] queries = {
                $"SELECT D_ID AS [ID], D_Date AS [Date], NotTaken FROM Daily WHERE [D_Data] = 'NTKN' ORDER BY [D_Date] DESC",
                $"SELECT C_ID AS [ID], C_Date AS [Date], C_Amount AS [Amount] FROM DailyCut ORDER BY [C_Date] DESC",
                $"SELECT DA_ID AS [ID], DA_Date AS [Date], NotTaken FROM DailyAnt WHERE Da_Data = 'NTKN' ORDER BY Da_Date DESC"
            };
            var dataTables = new List<DataTable>();
            foreach (var query in queries)
            {
                DataTable dataTable = new DataTable();
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                {
                    adapter.Fill(dataTable);
                }
                dataTables.Add(dataTable);
            }
            return dataTables;
        }

        public float GetTotalForDail()
        {
            float totalForDail = 0;
            string query = $"SELECT SUM(NotTaken) FROM Daily WHERE [D_Data]='NTKN'";
            using (OleDbConnection conn = new OleDbConnection(this.conn.ConnectionString))
            {
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value)
                    {
                        totalForDail = Convert.ToSingle(dataTable.Rows[0][0]);
                    }
                }
            }
            return totalForDail;
        }
        public float GetTotalForDailCut()
        {
            float totalForDailCut = 0;
            string query = $"SELECT SUM(C_Amount) FROM DailyCut";
            using (OleDbConnection conn = new OleDbConnection(this.conn.ConnectionString))
            {
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value)
                    {
                        totalForDailCut = Convert.ToSingle(dataTable.Rows[0][0]);
                    }
                }
            }
            return totalForDailCut;
        }
        public float GetTotalForDailAnt()
        {
            float totalForDailAnt = 0;
            string query = $"SELECT SUM(NotTaken) FROM DailyAnt WHERE [DA_Data]='NTKN'";
            using (OleDbConnection conn = new OleDbConnection(this.conn.ConnectionString))
            {
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value)
                    {
                        totalForDailAnt = Convert.ToSingle(dataTable.Rows[0][0]);
                    }
                }
            }
            return totalForDailAnt;
        }
        public float GetTotalForDailySav()
        {
            float totalForDailySav = 0;
            string query = $"SELECT SUM(NotTaken) FROM DailySaving WHERE [DS_Data]='NTKN'";
            using (OleDbConnection conn = new OleDbConnection(this.conn.ConnectionString))
            {
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value)
                    {
                        totalForDailySav = Convert.ToSingle(dataTable.Rows[0][0]);
                    }
                }
            }
            return totalForDailySav;
        }
        public float GetTotalForInstl()
        {
            float totalForInstl = 0;
            string query = $"SELECT SUM(InsPay) FROM Installment";
            using (OleDbConnection conn = new OleDbConnection(this.conn.ConnectionString))
            {
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value)
                    {
                        totalForInstl = Convert.ToSingle(dataTable.Rows[0][0]);
                    }
                }
            }
            return totalForInstl;
        }

        public float GetTotalMarketAmount()
        {
            float totalMarket = 0;
            string query = $"SELECT SUM(M_Amount) FROM Market";
            using (OleDbConnection conn = new OleDbConnection(this.conn.ConnectionString))
            {
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value)
                    {
                        totalMarket = Convert.ToSingle(dataTable.Rows[0][0]);
                    }
                }
            }
            return totalMarket;
        }
        public float GetSumOfGivenAmount()
        {
            float totalGiven = 0;
            string query = $"SELECT SUM(Total_Given) FROM Given WHERE [GDT_V] = 'NDV'";
            using (OleDbConnection conn = new OleDbConnection(this.conn.ConnectionString))
            {
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value)
                    {
                        totalGiven = Convert.ToSingle(dataTable.Rows[0][0]);
                    }
                }
            }
            return totalGiven;
        }
        public float GetSumOfTekenAmount()
        {
            float totalTeken = 0;
            string query = $"SELECT SUM(Total_Take) FROM Teken WHERE [TDT_V] = 'NDV'";
            using (OleDbConnection conn = new OleDbConnection(this.conn.ConnectionString))
            {
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value)
                    {
                        totalTeken = Convert.ToSingle(dataTable.Rows[0][0]);
                    }
                }
            }
            return totalTeken;
        }
        public float GetSumOfTariffAmount()
        {
            float totalTariff = 0;
            string query = $"SELECT SUM(Expense_Amount) FROM TariffAmt WHERE [EDT_V] = 'NDV'";
            using (OleDbConnection conn = new OleDbConnection(this.conn.ConnectionString))
            {
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value)
                    {
                        totalTariff = Convert.ToSingle(dataTable.Rows[0][0]);
                    }
                }
            }
            return totalTariff;
        }
        public float GetSumOfSavingAmount()
        {
            float totalSaving = 0;
            string query = $"SELECT SUM(Saving_Amount) FROM Saving WHERE [SDT_V] = 'NDV'";
            using (OleDbConnection conn = new OleDbConnection(this.conn.ConnectionString))
            {
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value)
                    {
                        totalSaving = Convert.ToSingle(dataTable.Rows[0][0]);
                    }
                }
            }
            return totalSaving;
        }
        public float GetSumOfUnratedAmount()
        {
            float totalUnrated = 0;
            string query = $"SELECT SUM(Unrated_Amount) FROM Unrated WHERE [UDT_V] = 'NDV'";
            using (OleDbConnection conn = new OleDbConnection(this.conn.ConnectionString))
            {
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value)
                    {
                        totalUnrated = Convert.ToSingle(dataTable.Rows[0][0]);
                    }
                }
            }
            return totalUnrated;
        }
        public string GetSumOfDailyAmount()
        {
            string takenDate = "";
            string query = $"SELECT Max(TakenDate) FROM Daily WHERE [D_Data] = 'TKN'";
            using (OleDbConnection conn = new OleDbConnection(this.conn.ConnectionString))
            {
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value)
                    {
                        takenDate = Convert.ToString(dataTable.Rows[0][0]);
                    }
                }
            }
            return takenDate;
        }
        public string GetSumOfDailyAntAmount()
        {
            string takenDate = "";
            string query = $"SELECT Max(TakenDate) FROM DailyAnt WHERE [DA_Data] = 'TKN'";
            using (OleDbConnection conn = new OleDbConnection(this.conn.ConnectionString))
            {
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value)
                    {
                        takenDate = Convert.ToString(dataTable.Rows[0][0]);
                    }
                }
            }
            return takenDate;
        }
        public string GetSumOfDailySavingAmount()
        {
            string takenDate = "";
            string query = $"SELECT Max(DS_InBankDate) FROM DailySaving WHERE [DS_Data] = 'TKN'";
            using (OleDbConnection conn = new OleDbConnection(this.conn.ConnectionString))
            {
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value)
                    {
                        takenDate = Convert.ToString(dataTable.Rows[0][0]);
                    }
                }
            }
            return takenDate;
        }

        //------------------------------ Market / Mamo --------------------------
        //-----------------------------------------------------------------------
        public bool insrtMarket(Market market)
        {
            string query = $"INSERT INTO Market(M_ID, M_Date, M_Amount, M_Insrt_Person) VALUES (?, ?, ?, ?)";
            using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
            {
                cmd.Parameters.AddWithValue("?", market.M_ID);
                cmd.Parameters.AddWithValue("?", market.M_Date);
                cmd.Parameters.AddWithValue("?", market.M_Amount);
                cmd.Parameters.AddWithValue("?", market.M_Insrt_Person);
                this.conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                this.conn.Close();
                return rowsAffected > 0;

            }
        }
        public bool updtMarket(Market market)
        {
            string query = $"UPDATE Market SET M_Amount = ?, M_Date = ?, M_Updt_Person = ? WHERE M_ID = ?";
            using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
            {
                cmd.Parameters.AddWithValue("?", market.M_Amount);
                cmd.Parameters.AddWithValue("?", market.M_Date);
                cmd.Parameters.AddWithValue("?", market.M_Updt_Person);
                cmd.Parameters.AddWithValue("?", market.M_ID);
                this.conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                this.conn.Close();
                return rowsAffected > 0;
            }
        }
        public bool insrtUtoM(Market market)
        {
            string query = $"INSERT INTO Market(M_ID, M_Date, M_Amount, M_Insrt_Person) VALUES (?, ?, ?, ?)";
            using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
            {
                cmd.Parameters.AddWithValue("?", market.M_ID);
                cmd.Parameters.AddWithValue("?", market.M_Date);
                cmd.Parameters.AddWithValue("?", market.M_Amount);
                cmd.Parameters.AddWithValue("?", market.M_Insrt_Person);
                this.conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                this.conn.Close();
                return rowsAffected > 0;

            }
        }

        public bool insrtMrktMemos(MarketMemos marketMemos)
        {
            string query = $"INSERT INTO MarketMemos(Mem_ID,Mem_Date,R_InvTK,C_InvTK,Giv_TK,Ret_TK,I_N01,I_N02,I_N03,I_N04,I_N05,I_N06,I_N07,I_N08,I_N09,I_N10,I_N11,I_N12,I_N13,I_N14,I_N15,I_N16,I_P01,I_P02,I_P03,I_P04,I_P05,I_P06,I_P07,I_P08,I_P09,I_P10,I_P11,I_P12,I_P13,I_P14,I_P15,I_P16,I_Q01,I_Q02,I_Q03,I_Q04,I_Q05,I_Q06,I_Q07,I_Q08,I_Q09,I_Q10,I_Q11,I_Q12,I_Q13,I_Q14,I_Q15,I_Q16,I_ST01,I_ST02,I_ST03,I_ST04,I_ST05,I_ST06,I_ST07,I_ST08,I_ST09,I_ST10,I_ST11,I_ST12,I_ST13,I_ST14,I_ST15,I_ST16,R_Inv01,R_Inv02,R_Inv03,R_Inv04,R_Inv05,R_Inv06,R_Inv07,R_Inv08,R_Inv09,R_Inv10,R_Inv11,R_Inv12,R_Inv13,R_Inv14,R_Inv15,R_Inv16,R_Inv17,R_Inv18,R_Inv19,R_Inv20,R_Inv21,R_Inv22,R_Inv23,R_Inv24,Mem_Insrt_Person) " +
                            "VALUES (@Mem_ID, @Mem_Date, @R_InvTK, @C_InvTK, @Giv_TK, @Ret_TK, @I_N01, @I_N02, @I_N03, @I_N04, @I_N05, @I_N06, @I_N07, @I_N08, @I_N09, @I_N10, @I_N11, @I_N12, @I_N13, @I_N14, @I_N15, @I_N16, @I_P01, @I_P02, @I_P03, @I_P04, @I_P05, @I_P06, @I_P07, @I_P08, @I_P09, @I_P10, @I_P11, @I_P12, @I_P13, @I_P14, @I_P15, @I_P16, @I_Q01, @I_Q02, @I_Q03, @I_Q04, @I_Q05, @I_Q06, @I_Q07, @I_Q08, @I_Q09, @I_Q10, @I_Q11, @I_Q12, @I_Q13, @I_Q14, @I_Q15, @I_Q16, @I_ST01, @I_ST02, @I_ST03, @I_ST04, @I_ST05, @I_ST06, @I_ST07, @I_ST08, @I_ST09, @I_ST10, @I_ST11, @I_ST12, @I_ST13, @I_ST14, @I_ST15, @I_ST16, @R_Inv01, @R_Inv02, @R_Inv03, @R_Inv04, @R_Inv05, @R_Inv06, @R_Inv07, @R_Inv08, @R_Inv09, @R_Inv10, @R_Inv11, @R_Inv12, @R_Inv13, @R_Inv14, @R_Inv15, @R_Inv16, @R_Inv17, @R_Inv18, @R_Inv19, @R_Inv20, @R_Inv21, @R_Inv22, @R_Inv23, @R_Inv24, @Mem_Insrt_Person)";
            using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
            {
                cmd.Parameters.AddWithValue("@Mem_ID", marketMemos.Mem_ID);
                cmd.Parameters.AddWithValue("@Mem_Date", marketMemos.Mem_Date);
                cmd.Parameters.AddWithValue("@R_InvTK", marketMemos.R_InvTK);
                cmd.Parameters.AddWithValue("@C_InvTK", marketMemos.C_InvTK);
                cmd.Parameters.AddWithValue("@Giv_TK", marketMemos.Giv_TK);
                cmd.Parameters.AddWithValue("@Ret_TK", marketMemos.Ret_TK);

                cmd.Parameters.AddWithValue("@I_N01", marketMemos.I_N01);
                cmd.Parameters.AddWithValue("@I_N02", marketMemos.I_N02);
                cmd.Parameters.AddWithValue("@I_N03", marketMemos.I_N03);
                cmd.Parameters.AddWithValue("@I_N04", marketMemos.I_N04);
                cmd.Parameters.AddWithValue("@I_N05", marketMemos.I_N05);
                cmd.Parameters.AddWithValue("@I_N06", marketMemos.I_N06);
                cmd.Parameters.AddWithValue("@I_N07", marketMemos.I_N07);
                cmd.Parameters.AddWithValue("@I_N08", marketMemos.I_N08);
                cmd.Parameters.AddWithValue("@I_N09", marketMemos.I_N09);
                cmd.Parameters.AddWithValue("@I_N10", marketMemos.I_N10);
                cmd.Parameters.AddWithValue("@I_N11", marketMemos.I_N11);
                cmd.Parameters.AddWithValue("@I_N12", marketMemos.I_N12);
                cmd.Parameters.AddWithValue("@I_N13", marketMemos.I_N13);
                cmd.Parameters.AddWithValue("@I_N14", marketMemos.I_N14);
                cmd.Parameters.AddWithValue("@I_N15", marketMemos.I_N15);
                cmd.Parameters.AddWithValue("@I_N16", marketMemos.I_N16);

                cmd.Parameters.AddWithValue("@I_P01", marketMemos.I_P01);
                cmd.Parameters.AddWithValue("@I_P02", marketMemos.I_P02);
                cmd.Parameters.AddWithValue("@I_P03", marketMemos.I_P03);
                cmd.Parameters.AddWithValue("@I_P04", marketMemos.I_P04);
                cmd.Parameters.AddWithValue("@I_P05", marketMemos.I_P05);
                cmd.Parameters.AddWithValue("@I_P06", marketMemos.I_P06);
                cmd.Parameters.AddWithValue("@I_P07", marketMemos.I_P07);
                cmd.Parameters.AddWithValue("@I_P08", marketMemos.I_P08);
                cmd.Parameters.AddWithValue("@I_P09", marketMemos.I_P09);
                cmd.Parameters.AddWithValue("@I_P10", marketMemos.I_P10);
                cmd.Parameters.AddWithValue("@I_P11", marketMemos.I_P11);
                cmd.Parameters.AddWithValue("@I_P12", marketMemos.I_P12);
                cmd.Parameters.AddWithValue("@I_P13", marketMemos.I_P13);
                cmd.Parameters.AddWithValue("@I_P14", marketMemos.I_P14);
                cmd.Parameters.AddWithValue("@I_P15", marketMemos.I_P15);
                cmd.Parameters.AddWithValue("@I_P16", marketMemos.I_P16);

                cmd.Parameters.AddWithValue("@I_Q01", marketMemos.I_Q01);
                cmd.Parameters.AddWithValue("@I_Q02", marketMemos.I_Q02);
                cmd.Parameters.AddWithValue("@I_Q03", marketMemos.I_Q03);
                cmd.Parameters.AddWithValue("@I_Q04", marketMemos.I_Q04);
                cmd.Parameters.AddWithValue("@I_Q05", marketMemos.I_Q05);
                cmd.Parameters.AddWithValue("@I_Q06", marketMemos.I_Q06);
                cmd.Parameters.AddWithValue("@I_Q07", marketMemos.I_Q07);
                cmd.Parameters.AddWithValue("@I_Q08", marketMemos.I_Q08);
                cmd.Parameters.AddWithValue("@I_Q09", marketMemos.I_Q09);
                cmd.Parameters.AddWithValue("@I_Q10", marketMemos.I_Q10);
                cmd.Parameters.AddWithValue("@I_Q11", marketMemos.I_Q11);
                cmd.Parameters.AddWithValue("@I_Q12", marketMemos.I_Q12);
                cmd.Parameters.AddWithValue("@I_Q13", marketMemos.I_Q13);
                cmd.Parameters.AddWithValue("@I_Q14", marketMemos.I_Q14);
                cmd.Parameters.AddWithValue("@I_Q15", marketMemos.I_Q15);
                cmd.Parameters.AddWithValue("@I_Q16", marketMemos.I_Q16);

                cmd.Parameters.AddWithValue("@I_ST01", marketMemos.I_ST01);
                cmd.Parameters.AddWithValue("@I_ST02", marketMemos.I_ST02);
                cmd.Parameters.AddWithValue("@I_ST03", marketMemos.I_ST03);
                cmd.Parameters.AddWithValue("@I_ST04", marketMemos.I_ST04);
                cmd.Parameters.AddWithValue("@I_ST05", marketMemos.I_ST05);
                cmd.Parameters.AddWithValue("@I_ST06", marketMemos.I_ST06);
                cmd.Parameters.AddWithValue("@I_ST07", marketMemos.I_ST07);
                cmd.Parameters.AddWithValue("@I_ST08", marketMemos.I_ST08);
                cmd.Parameters.AddWithValue("@I_ST09", marketMemos.I_ST09);
                cmd.Parameters.AddWithValue("@I_ST10", marketMemos.I_ST10);
                cmd.Parameters.AddWithValue("@I_ST11", marketMemos.I_ST11);
                cmd.Parameters.AddWithValue("@I_ST12", marketMemos.I_ST12);
                cmd.Parameters.AddWithValue("@I_ST13", marketMemos.I_ST13);
                cmd.Parameters.AddWithValue("@I_ST14", marketMemos.I_ST14);
                cmd.Parameters.AddWithValue("@I_ST15", marketMemos.I_ST15);
                cmd.Parameters.AddWithValue("@I_ST16", marketMemos.I_ST16);

                cmd.Parameters.AddWithValue("@R_Inv01", marketMemos.R_Inv01);
                cmd.Parameters.AddWithValue("@R_Inv02", marketMemos.R_Inv02);
                cmd.Parameters.AddWithValue("@R_Inv03", marketMemos.R_Inv03);
                cmd.Parameters.AddWithValue("@R_Inv04", marketMemos.R_Inv04);
                cmd.Parameters.AddWithValue("@R_Inv05", marketMemos.R_Inv05);
                cmd.Parameters.AddWithValue("@R_Inv06", marketMemos.R_Inv06);
                cmd.Parameters.AddWithValue("@R_Inv07", marketMemos.R_Inv07);
                cmd.Parameters.AddWithValue("@R_Inv08", marketMemos.R_Inv08);
                cmd.Parameters.AddWithValue("@R_Inv09", marketMemos.R_Inv09);
                cmd.Parameters.AddWithValue("@R_Inv10", marketMemos.R_Inv10);
                cmd.Parameters.AddWithValue("@R_Inv11", marketMemos.R_Inv11);
                cmd.Parameters.AddWithValue("@R_Inv12", marketMemos.R_Inv12);
                cmd.Parameters.AddWithValue("@R_Inv13", marketMemos.R_Inv13);
                cmd.Parameters.AddWithValue("@R_Inv14", marketMemos.R_Inv14);
                cmd.Parameters.AddWithValue("@R_Inv15", marketMemos.R_Inv15);
                cmd.Parameters.AddWithValue("@R_Inv16", marketMemos.R_Inv16);
                cmd.Parameters.AddWithValue("@R_Inv17", marketMemos.R_Inv17);
                cmd.Parameters.AddWithValue("@R_Inv18", marketMemos.R_Inv18);
                cmd.Parameters.AddWithValue("@R_Inv19", marketMemos.R_Inv19);
                cmd.Parameters.AddWithValue("@R_Inv20", marketMemos.R_Inv20);
                cmd.Parameters.AddWithValue("@R_Inv21", marketMemos.R_Inv21);
                cmd.Parameters.AddWithValue("@R_Inv22", marketMemos.R_Inv22);
                cmd.Parameters.AddWithValue("@R_Inv23", marketMemos.R_Inv23);
                cmd.Parameters.AddWithValue("@R_Inv24", marketMemos.R_Inv24);

                cmd.Parameters.AddWithValue("@Mem_Insrt_Person", marketMemos.Mem_Insrt_Person);
                this.conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                this.conn.Close();
                return rowsAffected > 0;
            }
        }
        public bool updtMrktMemos(MarketMemos marketMemos)
        {
            string query = $"UPDATE MarketMemos SET R_InvTK = ?,C_InvTK = ?,Giv_TK = ?,Ret_TK = ?,I_N01 = ? ,I_N02 = ? ,I_N03 = ? ,I_N04 = ? ,I_N05 = ? ,I_N06 = ? ,I_N07 = ? ,I_N08 = ? ,I_N09 = ? ,I_N10 = ? ,I_N11 = ? ,I_N12 = ? ,I_N13 = ? ,I_N14 = ? ,I_N15 = ? ,I_N16 = ? ,I_P01 = ? ,I_P02 = ? ,I_P03 = ? ,I_P04 = ? ,I_P05 = ? ,I_P06 = ? ,I_P07 = ? ,I_P08 = ? ,I_P09 = ? ,I_P10 = ? ,I_P11 = ? ,I_P12 = ? ,I_P13 = ? ,I_P14 = ? ,I_P15 = ? ,I_P16 = ? ,I_Q01 = ? ,I_Q02 = ? ,I_Q03 = ? ,I_Q04 = ? ,I_Q05 = ? ,I_Q06 = ? ,I_Q07 = ? ,I_Q08 = ? ,I_Q09 = ? ,I_Q10 = ? ,I_Q11 = ? ,I_Q12 = ? ,I_Q13 = ? ,I_Q14 = ? ,I_Q15 = ? ,I_Q16 = ? ,I_ST01 = ? ,I_ST02 = ? ,I_ST03 = ? ,I_ST04 = ? ,I_ST05 = ? ,I_ST06 = ? ,I_ST07 = ? ,I_ST08 = ? ,I_ST09 = ? ,I_ST10 = ? ,I_ST11 = ? ,I_ST12 = ? ,I_ST13 = ? ,I_ST14 = ? ,I_ST15 = ? ,I_ST16 = ? ,R_Inv01 = ? ,R_Inv02 = ? ,R_Inv03 = ? ,R_Inv04 = ? ,R_Inv05 = ? ,R_Inv06 = ? ,R_Inv07 = ? ,R_Inv08 = ? ,R_Inv09 = ? ,R_Inv10 = ? ,R_Inv11 = ? ,R_Inv12 = ? ,R_Inv13 = ? ,R_Inv14 = ? ,R_Inv15 = ? ,R_Inv16 = ? ,R_Inv17 = ? ,R_Inv18 = ? ,R_Inv19 = ? ,R_Inv20 = ? ,R_Inv21 = ? ,R_Inv22 = ? ,R_Inv23 = ? ,R_Inv24 = ? ,Mem_Updt_Person = ? WHERE Mem_ID = ?";
            using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
            {
                cmd.Parameters.AddWithValue("?", marketMemos.R_InvTK);
                cmd.Parameters.AddWithValue("?", marketMemos.C_InvTK);
                cmd.Parameters.AddWithValue("?", marketMemos.Giv_TK);
                cmd.Parameters.AddWithValue("?", marketMemos.Ret_TK);

                cmd.Parameters.AddWithValue("?", marketMemos.I_N01);
                cmd.Parameters.AddWithValue("?", marketMemos.I_N02);
                cmd.Parameters.AddWithValue("?", marketMemos.I_N03);
                cmd.Parameters.AddWithValue("?", marketMemos.I_N04);
                cmd.Parameters.AddWithValue("?", marketMemos.I_N05);
                cmd.Parameters.AddWithValue("?", marketMemos.I_N06);
                cmd.Parameters.AddWithValue("?", marketMemos.I_N07);
                cmd.Parameters.AddWithValue("?", marketMemos.I_N08);
                cmd.Parameters.AddWithValue("?", marketMemos.I_N09);
                cmd.Parameters.AddWithValue("?", marketMemos.I_N10);
                cmd.Parameters.AddWithValue("?", marketMemos.I_N11);
                cmd.Parameters.AddWithValue("?", marketMemos.I_N12);
                cmd.Parameters.AddWithValue("?", marketMemos.I_N13);
                cmd.Parameters.AddWithValue("?", marketMemos.I_N14);
                cmd.Parameters.AddWithValue("?", marketMemos.I_N15);
                cmd.Parameters.AddWithValue("?", marketMemos.I_N16);

                cmd.Parameters.AddWithValue("?", marketMemos.I_P01);
                cmd.Parameters.AddWithValue("?", marketMemos.I_P02);
                cmd.Parameters.AddWithValue("?", marketMemos.I_P03);
                cmd.Parameters.AddWithValue("?", marketMemos.I_P04);
                cmd.Parameters.AddWithValue("?", marketMemos.I_P05);
                cmd.Parameters.AddWithValue("?", marketMemos.I_P06);
                cmd.Parameters.AddWithValue("?", marketMemos.I_P07);
                cmd.Parameters.AddWithValue("?", marketMemos.I_P08);
                cmd.Parameters.AddWithValue("?", marketMemos.I_P09);
                cmd.Parameters.AddWithValue("?", marketMemos.I_P10);
                cmd.Parameters.AddWithValue("?", marketMemos.I_P11);
                cmd.Parameters.AddWithValue("?", marketMemos.I_P12);
                cmd.Parameters.AddWithValue("?", marketMemos.I_P13);
                cmd.Parameters.AddWithValue("?", marketMemos.I_P14);
                cmd.Parameters.AddWithValue("?", marketMemos.I_P15);
                cmd.Parameters.AddWithValue("?", marketMemos.I_P16);

                cmd.Parameters.AddWithValue("?", marketMemos.I_Q01);
                cmd.Parameters.AddWithValue("?", marketMemos.I_Q02);
                cmd.Parameters.AddWithValue("?", marketMemos.I_Q03);
                cmd.Parameters.AddWithValue("?", marketMemos.I_Q04);
                cmd.Parameters.AddWithValue("?", marketMemos.I_Q05);
                cmd.Parameters.AddWithValue("?", marketMemos.I_Q06);
                cmd.Parameters.AddWithValue("?", marketMemos.I_Q07);
                cmd.Parameters.AddWithValue("?", marketMemos.I_Q08);
                cmd.Parameters.AddWithValue("?", marketMemos.I_Q09);
                cmd.Parameters.AddWithValue("?", marketMemos.I_Q10);
                cmd.Parameters.AddWithValue("?", marketMemos.I_Q11);
                cmd.Parameters.AddWithValue("?", marketMemos.I_Q12);
                cmd.Parameters.AddWithValue("?", marketMemos.I_Q13);
                cmd.Parameters.AddWithValue("?", marketMemos.I_Q14);
                cmd.Parameters.AddWithValue("?", marketMemos.I_Q15);
                cmd.Parameters.AddWithValue("?", marketMemos.I_Q16);

                cmd.Parameters.AddWithValue("?", marketMemos.I_ST01);
                cmd.Parameters.AddWithValue("?", marketMemos.I_ST02);
                cmd.Parameters.AddWithValue("?", marketMemos.I_ST03);
                cmd.Parameters.AddWithValue("?", marketMemos.I_ST04);
                cmd.Parameters.AddWithValue("?", marketMemos.I_ST05);
                cmd.Parameters.AddWithValue("?", marketMemos.I_ST06);
                cmd.Parameters.AddWithValue("?", marketMemos.I_ST07);
                cmd.Parameters.AddWithValue("?", marketMemos.I_ST08);
                cmd.Parameters.AddWithValue("?", marketMemos.I_ST09);
                cmd.Parameters.AddWithValue("?", marketMemos.I_ST10);
                cmd.Parameters.AddWithValue("?", marketMemos.I_ST11);
                cmd.Parameters.AddWithValue("?", marketMemos.I_ST12);
                cmd.Parameters.AddWithValue("?", marketMemos.I_ST13);
                cmd.Parameters.AddWithValue("?", marketMemos.I_ST14);
                cmd.Parameters.AddWithValue("?", marketMemos.I_ST15);
                cmd.Parameters.AddWithValue("?", marketMemos.I_ST16);

                cmd.Parameters.AddWithValue("?", marketMemos.R_Inv01);
                cmd.Parameters.AddWithValue("?", marketMemos.R_Inv02);
                cmd.Parameters.AddWithValue("?", marketMemos.R_Inv03);
                cmd.Parameters.AddWithValue("?", marketMemos.R_Inv04);
                cmd.Parameters.AddWithValue("?", marketMemos.R_Inv05);
                cmd.Parameters.AddWithValue("?", marketMemos.R_Inv06);
                cmd.Parameters.AddWithValue("?", marketMemos.R_Inv07);
                cmd.Parameters.AddWithValue("?", marketMemos.R_Inv08);
                cmd.Parameters.AddWithValue("?", marketMemos.R_Inv09);
                cmd.Parameters.AddWithValue("?", marketMemos.R_Inv10);
                cmd.Parameters.AddWithValue("?", marketMemos.R_Inv11);
                cmd.Parameters.AddWithValue("?", marketMemos.R_Inv12);
                cmd.Parameters.AddWithValue("?", marketMemos.R_Inv13);
                cmd.Parameters.AddWithValue("?", marketMemos.R_Inv14);
                cmd.Parameters.AddWithValue("?", marketMemos.R_Inv15);
                cmd.Parameters.AddWithValue("?", marketMemos.R_Inv16);
                cmd.Parameters.AddWithValue("?", marketMemos.R_Inv17);
                cmd.Parameters.AddWithValue("?", marketMemos.R_Inv18);
                cmd.Parameters.AddWithValue("?", marketMemos.R_Inv19);
                cmd.Parameters.AddWithValue("?", marketMemos.R_Inv20);
                cmd.Parameters.AddWithValue("?", marketMemos.R_Inv21);
                cmd.Parameters.AddWithValue("?", marketMemos.R_Inv22);
                cmd.Parameters.AddWithValue("?", marketMemos.R_Inv23);
                cmd.Parameters.AddWithValue("?", marketMemos.R_Inv24);

                cmd.Parameters.AddWithValue("?", marketMemos.Mem_Updt_Person);
                cmd.Parameters.AddWithValue("?", marketMemos.Mem_ID);
                this.conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                this.conn.Close();
                return rowsAffected > 0;
            }
        }
        public bool delMrktMemos(string Mem_ID, MarketMemos marketMemos)
        {
            string query1 = $"UPDATE MarketMemos SET Mem_Del_Person = ? WHERE Mem_ID = ?";
            string query2 = $"INSERT INTO MarketMemosDel SELECT * FROM MarketMemos WHERE Mem_ID = ?";
            string query3 = $"DELETE FROM MarketMemos WHERE Mem_ID = ?";
            using (OleDbConnection connect = new OleDbConnection(this.conn.ConnectionString))
            {
                connect.Open();
                using (OleDbCommand cmd1 = new OleDbCommand(query1, connect))
                {
                    cmd1.Parameters.AddWithValue("?", marketMemos.Mem_Del_Person);
                    cmd1.Parameters.AddWithValue("?", marketMemos.Mem_ID);
                    int rowsAffected1 = cmd1.ExecuteNonQuery();
                    if (rowsAffected1 <= 0) return false;
                }
                using (OleDbCommand cmd2 = new OleDbCommand(query2, connect))
                {
                    cmd2.Parameters.AddWithValue("?", marketMemos.Mem_ID);
                    int rowsAffected2 = cmd2.ExecuteNonQuery();
                    if (rowsAffected2 <= 0) return false;
                }
                using (OleDbCommand cmd3 = new OleDbCommand(query3, connect))
                {
                    cmd3.Parameters.AddWithValue("?", marketMemos.Mem_ID);
                    int rowsAffected3 = cmd3.ExecuteNonQuery();
                    if (rowsAffected3 <= 0) return false;
                }
                connect.Close();
            }
            return true;
        }

        //------------------------------- Installment ---------------------------
        //-----------------------------------------------------------------------
        public bool insrtDailySaving(DailySaving dailySaving)
        {
            string query = $"INSERT INTO DailySaving (DS_ID, DS_Date, DS_FPAmount, DS_SPAmount, DS_TPAmount, NotTaken, DS_Data, DS_Insrt_Person) VALUES (?, ?, ?, ?, ?, ?, ?, ?)";
            using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
            {
                cmd.Parameters.AddWithValue("?", dailySaving.DS_ID);
                cmd.Parameters.AddWithValue("?", dailySaving.DS_Date);
                cmd.Parameters.AddWithValue("?", dailySaving.DS_FPAmount);
                cmd.Parameters.AddWithValue("?", dailySaving.DS_SPAmount);
                cmd.Parameters.AddWithValue("?", dailySaving.DS_TPAmount);
                cmd.Parameters.AddWithValue("?", dailySaving.NotTaken);
                cmd.Parameters.AddWithValue("?", dailySaving.DS_Data);
                cmd.Parameters.AddWithValue("?", dailySaving.DS_Insrt_Person);
                this.conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                this.conn.Close();
                return rowsAffected > 0;

            }
        }
        public bool updtDailySaving(DailySaving dailySaving)
        {
            string query = $"UPDATE DailySaving SET DS_FPAmount = ?, DS_Date = ?, DS_SPAmount = ?, DS_TPAmount = ?, NotTaken = ?, DS_Updt_Person = ? WHERE DS_ID = ?";
            using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
            {
                cmd.Parameters.AddWithValue("?", dailySaving.DS_FPAmount);
                cmd.Parameters.AddWithValue("?", dailySaving.DS_Date);
                cmd.Parameters.AddWithValue("?", dailySaving.DS_SPAmount);
                cmd.Parameters.AddWithValue("?", dailySaving.DS_TPAmount);
                cmd.Parameters.AddWithValue("?", dailySaving.NotTaken);
                cmd.Parameters.AddWithValue("?", dailySaving.DS_Updt_Person);
                cmd.Parameters.AddWithValue("?", dailySaving.DS_ID);
                this.conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                this.conn.Close();
                return rowsAffected > 0;
            }
        }
        public bool delDailySaving(DailySaving dailySaving)
        {
            string query = $"UPDATE DailySaving SET DS_Data = ?, DS_InBankDate = ?, DS_Del_Person = ? WHERE DS_ID = ?";
            using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
            {
                cmd.Parameters.AddWithValue("?", dailySaving.DS_Data);
                cmd.Parameters.AddWithValue("?", dailySaving.DS_InBankDate);
                cmd.Parameters.AddWithValue("?", dailySaving.DS_Del_Person);
                cmd.Parameters.AddWithValue("?", dailySaving.DS_ID);
                this.conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                this.conn.Close();
                return rowsAffected > 0;
            }
        }
        public bool delReDailySaving(DailySaving dailySaving)
        {
            string query = $"DELETE FROM DailySaving WHERE DS_ID = ?";
            using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
            {
                cmd.Parameters.AddWithValue("?", dailySaving.DS_ID);
                this.conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                this.conn.Close();
                return rowsAffected > 0;
            }
        }

        public bool insrtInstallment(Installment installment) //Details Work On When Take Loan & it's Payment.
        {
            string query = $"INSERT INTO Installment (I_ID, InsPay_Date, InsPay, Take_Data, I_Insrt_Person) VALUES (?, ?, ?, ?, ?)";
            using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
            {
                cmd.Parameters.AddWithValue("?", installment.I_ID);
                cmd.Parameters.AddWithValue("?", installment.InsPay_Date);
                cmd.Parameters.AddWithValue("?", installment.InsPay);
                cmd.Parameters.AddWithValue("?", installment.Take_Data);
                cmd.Parameters.AddWithValue("?", installment.I_Insrt_Person);
                this.conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                this.conn.Close();
                return rowsAffected > 0;
            }
        }
        public bool updtInstallment(Installment installment) //Update Details Work On When Take Loan & it's Payment.
        {
            string query = $"UPDATE Installment SET InsPay_Date = ?, I_Updt_Person = ? WHERE I_ID = ?";
            using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
            {
                cmd.Parameters.AddWithValue("?", installment.InsPay_Date);
                cmd.Parameters.AddWithValue("?", installment.I_Updt_Person);
                cmd.Parameters.AddWithValue("?", installment.I_ID);
                this.conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                this.conn.Close();
                return rowsAffected > 0;
            }
        }

        public bool insrInstallment(Installment installment) //Details Work On When Take Loan & it's Payment.
        {
            string query = $"INSERT INTO Installment (I_ID, I_Date, Take_Total, Take_Anot, Take_Mine, InsPerMonth, PerMonthPay, Take_Data) VALUES (?, ?, ?, ?, ?, ?, ?, ?)";
            using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
            {
                cmd.Parameters.AddWithValue("?", installment.I_ID);
                cmd.Parameters.AddWithValue("?", installment.I_Date);
                cmd.Parameters.AddWithValue("?", installment.Take_Total);
                cmd.Parameters.AddWithValue("?", installment.Take_Anot);
                cmd.Parameters.AddWithValue("?", installment.Take_Mine);
                cmd.Parameters.AddWithValue("?", installment.InsPerMonth);
                cmd.Parameters.AddWithValue("?", installment.PerMonthPay);
                cmd.Parameters.AddWithValue("?", installment.Take_Data);
                this.conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                this.conn.Close();
                return rowsAffected > 0;
            }
        }
        public bool udtInstallment(Installment installment) //Update Details Work On When Take Loan & it's Payment.
        {
            string query = $"UPDATE Installment SET Take_Data = ? WHERE I_ID = ?";
            using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
            {
                cmd.Parameters.AddWithValue("?", installment.Take_Data);
                cmd.Parameters.AddWithValue("?", installment.I_ID);
                this.conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                this.conn.Close();
                return rowsAffected > 0;
            }
        }

        public bool insrtBikeInfo(BikeInfo bikeInfo)
        {
            string query = $"INSERT INTO BikeInfo (B_ID, B_Chng_Date, B_KM_ODO, B_Mobile_Go, B_Next_ODO, B_Insrt_Person) VALUES (?, ?, ?, ?, ?, ?)";
            using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
            {
                cmd.Parameters.AddWithValue("?", bikeInfo.B_ID);
                cmd.Parameters.AddWithValue("?", bikeInfo.B_Chng_Date);
                cmd.Parameters.AddWithValue("?", bikeInfo.B_KM_ODO);
                cmd.Parameters.AddWithValue("?", bikeInfo.B_Mobile_Go);
                cmd.Parameters.AddWithValue("?", bikeInfo.B_Next_ODO);
                cmd.Parameters.AddWithValue("?", bikeInfo.B_Insrt_Person);
                this.conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                this.conn.Close();
                return rowsAffected > 0;
            }
        }

        //-------------------------------- Cr. Card -----------------------------
        //-----------------------------------------------------------------------
        public bool insrtGiven(Given given)
        {
            string query = $"INSERT INTO Given (InGiven, Total_Given, Given_To, ThroughBy_Given, Given_Date, Remarks_Given, GDT_V, G_Insrt_Person) VALUES (?, ?, ?, ?, ?, ?, ?, ?)";
            using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
            {
                cmd.Parameters.AddWithValue("?", given.InGiven);
                cmd.Parameters.AddWithValue("?", given.Total_Given);
                cmd.Parameters.AddWithValue("?", given.Given_To);
                cmd.Parameters.AddWithValue("?", given.ThroughBy_Given);
                cmd.Parameters.AddWithValue("?", given.Given_Date);
                cmd.Parameters.AddWithValue("?", given.Remarks_Given);
                cmd.Parameters.AddWithValue("?", given.GDT_V);
                cmd.Parameters.AddWithValue("?", given.G_Insrt_Person);
                this.conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                this.conn.Close();
                return rowsAffected > 0;
            }
        }
        public bool insrtTeken(Teken teken)
        {
            string query = $"INSERT INTO Teken (InTake, Total_Take, Take_To, ThroughBy_Take, Take_Date, Remarks_Take, TDT_V, T_Insrt_Person) VALUES (?, ?, ?, ?, ?, ?, ?, ?)";
            using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
            {
                cmd.Parameters.AddWithValue("?", teken.InTake);
                cmd.Parameters.AddWithValue("?", teken.Total_Take);
                cmd.Parameters.AddWithValue("?", teken.Take_To);
                cmd.Parameters.AddWithValue("?", teken.ThroughBy_Take);
                cmd.Parameters.AddWithValue("?", teken.Take_Date);
                cmd.Parameters.AddWithValue("?", teken.Remarks_Take);
                cmd.Parameters.AddWithValue("?", teken.TDT_V);
                cmd.Parameters.AddWithValue("?", teken.T_Insrt_Person);
                this.conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                this.conn.Close();
                return rowsAffected > 0;
            }
        }
        public bool insrtTariffAmt(TariffAmt tariff)
        {
            string query = $"INSERT INTO TariffAmt (InExpense, Expense_Amount, Expense_To, ThroughBy_Expense, Expense_Date, Remarks_Expense, EDT_V, E_Insrt_Person) VALUES (?, ?, ?, ?, ?, ?, ?, ?)";
            using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
            {
                cmd.Parameters.AddWithValue("?", tariff.InExpense);
                cmd.Parameters.AddWithValue("?", tariff.Expense_Amount);
                cmd.Parameters.AddWithValue("?", tariff.Expense_To);
                cmd.Parameters.AddWithValue("?", tariff.ThroughBy_Expense);
                cmd.Parameters.AddWithValue("?", tariff.Expense_Date);
                cmd.Parameters.AddWithValue("?", tariff.Remarks_Expense);
                cmd.Parameters.AddWithValue("?", tariff.EDT_V);
                cmd.Parameters.AddWithValue("?", tariff.E_Insrt_Person);
                this.conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                this.conn.Close();
                return rowsAffected > 0;
            }
        }
        public bool insrtSaving(Saving saving)
        {
            string query = $"INSERT INTO Saving (InSaving, Saving_Amount, Saving_To, ThroughBy_Saving, Saving_Date, Remarks_Saving, SDT_V, Saving_Bank, S_Insrt_Person) VALUES (?, ?, ?, ?, ?, ?, 'NDV', ?, ?)";
            using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
            {
                cmd.Parameters.AddWithValue("?", saving.InSaving);
                cmd.Parameters.AddWithValue("?", saving.Saving_Amount);
                cmd.Parameters.AddWithValue("?", saving.Saving_To);
                cmd.Parameters.AddWithValue("?", saving.ThroughBy_Saving);
                cmd.Parameters.AddWithValue("?", saving.Saving_Date);
                cmd.Parameters.AddWithValue("?", saving.Remarks_Saving);
                cmd.Parameters.AddWithValue("?", saving.SDT_V);
                cmd.Parameters.AddWithValue("?", saving.Saving_Bank);
                cmd.Parameters.AddWithValue("?", saving.S_Insrt_Person);
                this.conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                this.conn.Close();
                return rowsAffected > 0;
            }
        }
        public bool insrtUnrated(Unrated unrated)
        {
            string query = $"INSERT INTO Unrated (InUnrated, Unrated_Amount, Unrated_To, ThroughBy_Unrated, Unrated_Date, Remarks_Unrated, UDT_V, U_Insrt_Person) VALUES (?, ?, ?, ?, ?, ?, 'NDV', ?)";
            using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
            {
                cmd.Parameters.AddWithValue("?", unrated.InUnrated);
                cmd.Parameters.AddWithValue("?", unrated.Unrated_Amount);
                cmd.Parameters.AddWithValue("?", unrated.Unrated_To);
                cmd.Parameters.AddWithValue("?", unrated.ThroughBy_Unrated);
                cmd.Parameters.AddWithValue("?", unrated.Unrated_Date);
                cmd.Parameters.AddWithValue("?", unrated.Remarks_Unrated);
                cmd.Parameters.AddWithValue("?", unrated.UDT_V);
                cmd.Parameters.AddWithValue("?", unrated.U_Insrt_Person);
                this.conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                this.conn.Close();
                return rowsAffected > 0;
            }
        }

        public bool insrtupdtGiven(Given given)
        {
            string query1 = $"UPDATE Given SET Total_Given = ?, GDT_V_Date = ?, G_Updt_Person = ? WHERE InGiven = ?";
            string query2 = $"INSERT INTO GivenUpdt(InGiven, Was_Given, Now_Given, Total_Given, Given_To, GDT_V_Date) VALUES (?, ?, ?, ?, ?, ?)";
            using (OleDbConnection connect = new OleDbConnection(this.conn.ConnectionString))
            {
                connect.Open();
                using (OleDbCommand cmd1 = new OleDbCommand(query1, connect))
                {
                    cmd1.Parameters.AddWithValue("?", given.Total_Given);
                    cmd1.Parameters.AddWithValue("?", given.GDT_V_Date);
                    cmd1.Parameters.AddWithValue("?", given.G_Updt_Person);
                    cmd1.Parameters.AddWithValue("?", given.InGiven);
                    int rowsAffected1 = cmd1.ExecuteNonQuery();
                    if (rowsAffected1 <= 0) return false;
                }
                using (OleDbCommand cmd2 = new OleDbCommand(query2, connect))
                {
                    cmd2.Parameters.AddWithValue("?", given.InGiven);
                    cmd2.Parameters.AddWithValue("?", given.Was_Given_UD);
                    cmd2.Parameters.AddWithValue("?", given.Now_Given_UD);
                    cmd2.Parameters.AddWithValue("?", given.Total_Given_UD);
                    cmd2.Parameters.AddWithValue("?", given.Given_To_UD);
                    cmd2.Parameters.AddWithValue("?", given.GDT_V_Date_UD);
                    int rowsAffected2 = cmd2.ExecuteNonQuery();
                    if (rowsAffected2 <= 0) return false;
                }
                connect.Close();
            }
            return true;
        }
        public bool insrtupdtTeken(Teken teken)
        {
            string query1 = $"UPDATE Teken SET Total_Take = ?, TDT_V_Date = ?, T_Updt_Person = ? WHERE InTake = ?";
            string query2 = $"INSERT INTO TekenUpdt(InTake ,Was_Take ,Now_Take ,Total_Take ,Take_To ,TDT_V_Date) VALUES (?, ?, ?, ?, ?, ?)";
            using (OleDbConnection connect = new OleDbConnection(this.conn.ConnectionString))
            {
                connect.Open();
                using (OleDbCommand cmd1 = new OleDbCommand(query1, connect))
                {
                    cmd1.Parameters.AddWithValue("?", teken.Total_Take);
                    cmd1.Parameters.AddWithValue("?", teken.TDT_V_Date);
                    cmd1.Parameters.AddWithValue("?", teken.T_Updt_Person);
                    cmd1.Parameters.AddWithValue("?", teken.InTake);
                    int rowsAffected1 = cmd1.ExecuteNonQuery();
                    if (rowsAffected1 <= 0) return false;
                }
                using (OleDbCommand cmd2 = new OleDbCommand(query2, connect))
                {
                    cmd2.Parameters.AddWithValue("?", teken.InTake);
                    cmd2.Parameters.AddWithValue("?", teken.Was_Take_UD);
                    cmd2.Parameters.AddWithValue("?", teken.Now_Take_UD);
                    cmd2.Parameters.AddWithValue("?", teken.Total_Take_UD);
                    cmd2.Parameters.AddWithValue("?", teken.Take_To_UD);
                    cmd2.Parameters.AddWithValue("?", teken.TDT_V_Date_UD);
                    int rowsAffected2 = cmd2.ExecuteNonQuery();
                    if (rowsAffected2 <= 0) return false;
                }
                connect.Close();
            }
            return true;
        }
        public bool insrtupdtTariffAmt(TariffAmt tariff)
        {
            string query1 = $"UPDATE TariffAmt SET Expense_Amount = ?, EDT_V_Date = ?, E_Updt_Person = ? WHERE InExpense = ?";
            string query2 = $"INSERT INTO TariffAmtUpdt(InExpense ,Was_Expense ,Now_Expense ,Expense_Amount ,Expense_To ,EDT_V_Date) VALUES (?, ?, ?, ?, ?, ?)";
            using (OleDbConnection connect = new OleDbConnection(this.conn.ConnectionString))
            {
                connect.Open();
                using (OleDbCommand cmd1 = new OleDbCommand(query1, connect))
                {
                    cmd1.Parameters.AddWithValue("?", tariff.Expense_Amount);
                    cmd1.Parameters.AddWithValue("?", tariff.EDT_V_Date);
                    cmd1.Parameters.AddWithValue("?", tariff.E_Updt_Person);
                    cmd1.Parameters.AddWithValue("?", tariff.InExpense);
                    int rowsAffected1 = cmd1.ExecuteNonQuery();
                    if (rowsAffected1 <= 0) return false;
                }
                using (OleDbCommand cmd2 = new OleDbCommand(query2, connect))
                {
                    cmd2.Parameters.AddWithValue("?", tariff.InExpense);
                    cmd2.Parameters.AddWithValue("?", tariff.Was_Expense_UD);
                    cmd2.Parameters.AddWithValue("?", tariff.Now_Expense_UD);
                    cmd2.Parameters.AddWithValue("?", tariff.Expense_Amount_UD);
                    cmd2.Parameters.AddWithValue("?", tariff.Expense_To_UD);
                    cmd2.Parameters.AddWithValue("?", tariff.EDT_V_Date_UD);
                    int rowsAffected2 = cmd2.ExecuteNonQuery();
                    if (rowsAffected2 <= 0) return false;
                }
                connect.Close();
            }
            return true;
        }
        public bool insrtupdtSaving(Saving saving)
        {
            string query1 = $"UPDATE Saving SET Saving_Amount = ?, SDT_V_Date = ?, S_Updt_Person = ? WHERE InSaving = ?";
            string query2 = $"INSERT INTO SavingUpdt(InSaving, Was_Saving, Now_Saving, Saving_Amount, Saving_To, SDT_V_Date) VALUES (?, ?, ?, ?, ?, ?)";
            using (OleDbConnection connect = new OleDbConnection(this.conn.ConnectionString))
            {
                connect.Open();
                using (OleDbCommand cmd1 = new OleDbCommand(query1, connect))
                {
                    cmd1.Parameters.AddWithValue("?", saving.Saving_Amount);
                    cmd1.Parameters.AddWithValue("?", saving.SDT_V_Date);
                    cmd1.Parameters.AddWithValue("?", saving.S_Updt_Person);
                    cmd1.Parameters.AddWithValue("?", saving.InSaving);
                    int rowsAffected1 = cmd1.ExecuteNonQuery();
                    if (rowsAffected1 <= 0) return false;
                }
                using (OleDbCommand cmd2 = new OleDbCommand(query2, connect))
                {
                    cmd2.Parameters.AddWithValue("?", saving.InSaving);
                    cmd2.Parameters.AddWithValue("?", saving.Was_Saving_UD);
                    cmd2.Parameters.AddWithValue("?", saving.Now_Saving_UD);
                    cmd2.Parameters.AddWithValue("?", saving.Saving_Amount_UD);
                    cmd2.Parameters.AddWithValue("?", saving.Saving_To_UD);
                    cmd2.Parameters.AddWithValue("?", saving.SDT_V_Date_UD);
                    int rowsAffected2 = cmd2.ExecuteNonQuery();
                    if (rowsAffected2 <= 0) return false;
                }
                connect.Close();
            }
            return true;
        }
        public bool insrtupdtUnrated(Unrated unrated)
        {
            string query1 = $"UPDATE Unrated SET Unrated_Amount = ?, UDT_V_Date = ?, U_Updt_Person = ? WHERE InUnrated = ?";
            string query2 = $"INSERT INTO UnratedUpdt(InUnrated, Was_Unrated, Now_Unrated, Unrated_Amount, Unrated_To, UDT_V_Date) VALUES (?, ?, ?, ?, ?, ?)";
            using (OleDbConnection connect = new OleDbConnection(this.conn.ConnectionString))
            {
                connect.Open();
                using (OleDbCommand cmd1 = new OleDbCommand(query1, connect))
                {
                    cmd1.Parameters.AddWithValue("?", unrated.Unrated_Amount);
                    cmd1.Parameters.AddWithValue("?", unrated.UDT_V_Date);
                    cmd1.Parameters.AddWithValue("?", unrated.U_Updt_Person);
                    cmd1.Parameters.AddWithValue("?", unrated.InUnrated);
                    int rowsAffected1 = cmd1.ExecuteNonQuery();
                    if (rowsAffected1 <= 0) return false;
                }
                using (OleDbCommand cmd2 = new OleDbCommand(query2, connect))
                {
                    cmd2.Parameters.AddWithValue("?", unrated.InUnrated);
                    cmd2.Parameters.AddWithValue("?", unrated.Was_Unrated_UD);
                    cmd2.Parameters.AddWithValue("?", unrated.Now_Unrated_UD);
                    cmd2.Parameters.AddWithValue("?", unrated.Unrated_Amount_UD);
                    cmd2.Parameters.AddWithValue("?", unrated.Unrated_To_UD);
                    cmd2.Parameters.AddWithValue("?", unrated.UDT_V_Date_UD);
                    int rowsAffected2 = cmd2.ExecuteNonQuery();
                    if (rowsAffected2 <= 0) return false;
                }
                connect.Close();
            }
            return true;
        }

        internal bool delGiven(Given given)
        {
            string query = $"UPDATE Given SET GDT_V = ?, DDT_V_Date = ?, G_Del_Person = ? WHERE InGiven = ?";
            using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
            {
                cmd.Parameters.AddWithValue("?", given.GDT_V);
                cmd.Parameters.AddWithValue("?", given.DDT_V_Date);
                cmd.Parameters.AddWithValue("?", given.G_Del_Person);
                cmd.Parameters.AddWithValue("?", given.InGiven);
                this.conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                this.conn.Close();
                return rowsAffected > 0;
            }
        }
        internal bool delTeken(Teken teken)
        {
            string query = $"UPDATE Teken SET TDT_V = ?, DDT_V_Date = ?, T_Del_Person = ? WHERE InTake = ?";
            using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
            {
                cmd.Parameters.AddWithValue("?", teken.TDT_V);
                cmd.Parameters.AddWithValue("?", teken.DDT_V_Date);
                cmd.Parameters.AddWithValue("?", teken.T_Del_Person);
                cmd.Parameters.AddWithValue("?", teken.InTake);
                this.conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                this.conn.Close();
                return rowsAffected > 0;
            }
        }
        internal bool delTariffAmt(TariffAmt tariff)
        {
            string query = $"UPDATE TariffAmt SET EDT_V = ?, DDT_V_Date = ?, E_Del_Person = ? WHERE InExpense = ?";
            using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
            {
                cmd.Parameters.AddWithValue("?", tariff.EDT_V);
                cmd.Parameters.AddWithValue("?", tariff.DDT_V_Date);
                cmd.Parameters.AddWithValue("?", tariff.E_Del_Person);
                cmd.Parameters.AddWithValue("?", tariff.InExpense);
                this.conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                this.conn.Close();
                return rowsAffected > 0;
            }
        }
        internal bool delSaving(Saving saving)
        {
            string query = $"UPDATE Saving SET SDT_V = ?, DDT_V_Date = ?, S_Del_Person = ? WHERE InSaving = ?";
            using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
            {
                cmd.Parameters.AddWithValue("?", saving.SDT_V);
                cmd.Parameters.AddWithValue("?", saving.DDT_V_Date);
                cmd.Parameters.AddWithValue("?", saving.S_Del_Person);
                cmd.Parameters.AddWithValue("?", saving.InSaving);
                this.conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                this.conn.Close();
                return rowsAffected > 0;
            }
        }
        public bool delUnrated(Unrated unrated)
        {
            string query = $"UPDATE Unrated SET UDT_V = ?, DDT_V_Date = ?, U_Del_Person = ? WHERE InUnrated = ?";
            using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
            {
                cmd.Parameters.AddWithValue("?", unrated.UDT_V);
                cmd.Parameters.AddWithValue("?", unrated.DDT_V_Date);
                cmd.Parameters.AddWithValue("?", unrated.U_Del_Person);
                cmd.Parameters.AddWithValue("?", unrated.InUnrated);
                this.conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                this.conn.Close();
                return rowsAffected > 0;
            }
        }

        //------------------------------ Daily / Achive -------------------------
        //-----------------------------------------------------------------------
        public bool insrtDaily(Daily daily)
        {
            string query = $"INSERT INTO Daily (D_ID, D_Date, D_FPAmount, D_SPAmount, NotTaken, D_Data, D_Insrt_Person) VALUES (?, ?, ?, ?, ?, ?, ?)";
            using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
            {
                cmd.Parameters.AddWithValue("?", daily.D_ID);
                cmd.Parameters.AddWithValue("?", daily.D_Date);
                cmd.Parameters.AddWithValue("?", daily.D_FPAmount);
                cmd.Parameters.AddWithValue("?", daily.D_SPAmount);
                cmd.Parameters.AddWithValue("?", daily.NotTaken);
                cmd.Parameters.AddWithValue("?", daily.D_Data);
                cmd.Parameters.AddWithValue("?", daily.D_Insrt_Person);
                this.conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                this.conn.Close();
                return rowsAffected > 0;
            }
        }
        public bool updtDaily(Daily daily)
        {
            string query = $"UPDATE Daily SET D_FPAmount = ?, D_SPAmount = ?, NotTaken = ?, D_Date = ?, D_Updt_Person = ? WHERE D_ID = ?";
            using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
            {
                cmd.Parameters.AddWithValue("?", daily.D_FPAmount);
                cmd.Parameters.AddWithValue("?", daily.D_SPAmount);
                cmd.Parameters.AddWithValue("?", daily.NotTaken);
                cmd.Parameters.AddWithValue("?", daily.D_Date);
                cmd.Parameters.AddWithValue("?", daily.D_Updt_Person);
                cmd.Parameters.AddWithValue("?", daily.D_ID);
                this.conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                this.conn.Close();
                return rowsAffected > 0;
            }
        }
        public bool delDaily(Daily daily)
        {
            string query = $"UPDATE Daily SET D_Data = ?, TakenDate = ?, D_Del_Person = ? WHERE D_ID = ?";
            using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
            {
                cmd.Parameters.AddWithValue("?", daily.D_Data);
                cmd.Parameters.AddWithValue("?", daily.D_Date);
                cmd.Parameters.AddWithValue("?", daily.D_Insrt_Person);
                cmd.Parameters.AddWithValue("?", daily.D_ID);
                this.conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                this.conn.Close();
                return rowsAffected > 0;
            }
        }

        public bool InsertDailyCut(DailyCut dailyCut)
        {
            string query = $"INSERT INTO DailyCut (C_ID, C_Date, C_Amount, C_Insrt_Person) VALUES (?, ?, ?, ?)";
            using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
            {
                cmd.Parameters.AddWithValue("?", dailyCut.C_ID);
                cmd.Parameters.AddWithValue("?", dailyCut.C_Date);
                cmd.Parameters.AddWithValue("?", dailyCut.C_Amount);
                cmd.Parameters.AddWithValue("?", dailyCut.C_Insrt_Person);
                this.conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                this.conn.Close();
                return rowsAffected > 0;
            }
        }
        public bool UpdateDailyCut(DailyCut dailyCut)
        {
            string query = $"UPDATE DailyCut SET C_Amount = ?, C_Date = ?, C_Updt_Person = ? WHERE C_ID = ?";
            using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
            {
                cmd.Parameters.AddWithValue("?", dailyCut.C_Amount);
                cmd.Parameters.AddWithValue("?", dailyCut.C_Date);
                cmd.Parameters.AddWithValue("?", dailyCut.C_Updt_Person);
                cmd.Parameters.AddWithValue("?", dailyCut.C_ID);
                this.conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                this.conn.Close();
                return rowsAffected > 0;
            }
        }
        public bool deleteDailyCut(string D_ID, string C_ID, DailyCut deldailyCut)
        {
            string query = $"DELETE FROM Daily WHERE D_ID = ?";
            string query2 = $"DELETE FROM DailyCut WHERE C_ID = ?";
            using (OleDbConnection conn = new OleDbConnection(this.conn.ConnectionString))
            {
                using (OleDbCommand cmd1 = new OleDbCommand(query, conn))
                {
                    this.conn.Open();
                    cmd1.Parameters.AddWithValue("?", D_ID);
                    int rowsAffected1 = cmd1.ExecuteNonQuery();
                    this.conn.Close();
                    if (rowsAffected1 == 0)
                    {
                        return false;
                    }
                }
                using (OleDbCommand cmd2 = new OleDbCommand(query2, conn))
                {
                    cmd2.Parameters.AddWithValue("?", C_ID);
                    this.conn.Open();
                    int rowsAffected2 = cmd2.ExecuteNonQuery();
                    this.conn.Close();
                    return rowsAffected2 > 0;
                }
            }
        }

        public bool insrtDailyAnt(DailyAnt dailyAnt)
        {
            string query = $"INSERT INTO DailyAnt (DA_ID, DA_Date, DA_FPAmount, DA_SPAmount, NotTaken, DA_Data, DA_Insrt_Person) VALUES (?, ?, ?, ?, ?, ?, ?)";
            using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
            {
                cmd.Parameters.AddWithValue("?", dailyAnt.DA_ID);
                cmd.Parameters.AddWithValue("?", dailyAnt.DA_Date);
                cmd.Parameters.AddWithValue("?", dailyAnt.DA_FPAmount);
                cmd.Parameters.AddWithValue("?", dailyAnt.DA_SPAmount);
                cmd.Parameters.AddWithValue("?", dailyAnt.NotTaken);
                cmd.Parameters.AddWithValue("?", dailyAnt.DA_Data);
                cmd.Parameters.AddWithValue("?", dailyAnt.DA_Insrt_Person);
                this.conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                this.conn.Close();
                return rowsAffected > 0;
            }
        }
        public bool updtDailyAnt(DailyAnt dailyAnt)
        {
            string query = $"UPDATE DailyAnt SET DA_FPAmount = ?, DA_SPAmount = ?, NotTaken = ?, DA_Date = ?, DA_Updt_Person = ? WHERE DA_ID = ?";
            using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
            {
                cmd.Parameters.AddWithValue("?", dailyAnt.DA_FPAmount);
                cmd.Parameters.AddWithValue("?", dailyAnt.DA_SPAmount);
                cmd.Parameters.AddWithValue("?", dailyAnt.NotTaken);
                cmd.Parameters.AddWithValue("?", dailyAnt.DA_Date);
                cmd.Parameters.AddWithValue("?", dailyAnt.DA_Updt_Person);
                cmd.Parameters.AddWithValue("?", dailyAnt.DA_ID);
                this.conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                this.conn.Close();
                return rowsAffected > 0;
            }
        }
        public bool delDailyAnt(DailyAnt dailyAnt)
        {
            string query = $"UPDATE DailyAnt SET DA_Data = ?, TakenDate = ?, DA_Del_Person = ? WHERE DA_ID = ?";
            using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
            {
                cmd.Parameters.AddWithValue("?", dailyAnt.DA_Data);
                cmd.Parameters.AddWithValue("?", dailyAnt.TakenDate);
                cmd.Parameters.AddWithValue("?", dailyAnt.DA_Del_Person);
                cmd.Parameters.AddWithValue("?", dailyAnt.DA_ID);
                this.conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                this.conn.Close();
                return rowsAffected > 0;
            }
        }
        public bool delReDailyAnt(DailyAnt dailyAnt)
        {
            string query = $"DELETE FROM DailyAnt WHERE DA_ID = ?";
            using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
            {
                cmd.Parameters.AddWithValue("?", dailyAnt.DA_ID);
                this.conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                this.conn.Close();
                return rowsAffected > 0;
            }
        }

        //------------------------------ Sync Data to SQL -------------------------
        //-------------------------------------------------------------------------
        public void DeleteAllDataInSQL()
        {
            string insCom = "BEGIN " +
                                    $"DELETE FROM BikeInfo; DELETE FROM Daily; DELETE FROM DailyAnt; DELETE FROM DailyCut; DELETE FROM DailySaving; DELETE FROM TariffAmt; DELETE FROM Given; DELETE FROM Images; DELETE FROM Installment; DELETE FROM Market; DELETE FROM MarketMemos; DELETE FROM MarketMemosDel; DELETE FROM Saving; DELETE FROM Teken; DELETE FROM Unrated; " +
                                "END;";
            using (OdbcConnection sqlConn = new OdbcConnection(connSql))
            {
                sqlConn.Open();
                using (OdbcTransaction transaction = sqlConn.BeginTransaction())
                {
                    using (OdbcCommand sqlInsComm = new OdbcCommand(insCom, sqlConn, transaction))
                    {
                        sqlInsComm.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                using (OleDbConnection accConn = new OleDbConnection(connAcc))
                {
                    accConn.Open();
                    this.marketSync();
                    this.marketMemosSync();
                    this.marketMemosDelSync();
                    this.dailySavingSync();
                    this.installmentSync();
                    this.installmentPaySync();
                    this.bikeInfoSync();
                    this.givenSync();
                    this.tekenSync();
                    this.expenseSync();
                    this.savingSync();
                    this.unratedSync();
                    this.dailySync();
                    this.dailyAntSync();
                    this.dailyCutSync();
                    accConn.Close();
                }
                sqlConn.Close();
            }
        }

        public void SyncMarkMemData()
        {
            using (OdbcConnection sqlConn = new OdbcConnection(connSql))
            {
                sqlConn.Open();
                using (OleDbConnection accConn = new OleDbConnection(connAcc))
                {
                    accConn.Open();
                    this.marketSync();
                    this.marketMemosSync();
                    this.marketMemosDelSync();
                    //this.imagesSync();
                    accConn.Close();
                }
                sqlConn.Close();
            }
        }
        public void SyncInstallData()
        {
            using (OdbcConnection sqlConn = new OdbcConnection(connSql))
            {
                sqlConn.Open();
                using (OleDbConnection accConn = new OleDbConnection(connAcc))
                {
                    accConn.Open();
                    this.dailySavingSync();
                    this.installmentSync();
                    this.installmentPaySync();
                    this.bikeInfoSync();
                    accConn.Close();
                }
                sqlConn.Close();
            }
        }
        public void SyncCrCardData()
        {
            using (OdbcConnection sqlConn = new OdbcConnection(connSql))
            {
                sqlConn.Open();
                using (OleDbConnection accConn = new OleDbConnection(connAcc))
                {
                    accConn.Open();
                    this.givenSync();
                    this.tekenSync();
                    this.expenseSync();
                    this.savingSync();
                    this.unratedSync();
                    this.givenUpdtSync();
                    this.tekenUpdtSync();
                    this.expenseUpdtSync();
                    this.savingUpdtSync();
                    this.unratedUpdtSync();
                    accConn.Close();
                }
                sqlConn.Close();
            }
        }
        public void SyncDailyAchiveData()
        {
            using (OdbcConnection sqlConn = new OdbcConnection(connSql))
            {
                sqlConn.Open();
                using (OleDbConnection accConn = new OleDbConnection(connAcc))
                {
                    accConn.Open();
                    this.dailySync();
                    this.dailyAntSync();
                    this.dailyCutSync();
                    accConn.Close();
                }
                sqlConn.Close();
            }
        }


        //----------------Access to SQL Data Insert Event Work-------------------
        //-----------------------------------------------------------------------
        private void marketSync()
        {
            string insCom = "IF NOT EXISTS (SELECT * FROM Market WHERE M_ID = ?) " +
                                "BEGIN " +
                                    $"INSERT INTO Market (M_ID,M_Date,M_Amount,M_Insrt_Person,M_Updt_Person,M_Del_Person) VALUES (?, ?, ?, ?, ?, ?) " +
                                "END ";
            using (OleDbConnection accConn = new OleDbConnection(connAcc))
            {
                accConn.Open();
                string selCom = "SELECT * FROM Market";
                OleDbCommand command = new OleDbCommand(selCom, accConn);
                OleDbDataReader reader = command.ExecuteReader();
                using (OdbcConnection sqlConn = new OdbcConnection(connSql))
                {
                    sqlConn.Open();
                    string checkTableQuery = "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Market') " +
                                             "BEGIN " +
                                                $"CREATE TABLE Market (M_ID nvarchar(250) NULL, M_Date datetime NULL, M_Amount float, M_Insrt_Person nvarchar(250), M_Updt_Person nvarchar(250), M_Del_Person nvarchar(250)) " +
                                             "END";
                    using (OdbcCommand checkTableCommand = new OdbcCommand(checkTableQuery, sqlConn))
                    {
                        checkTableCommand.ExecuteNonQuery();
                    }
                    while (reader.Read())
                    {
                        using (OdbcCommand sqlInsComm = new OdbcCommand(insCom, sqlConn))
                        {
                            sqlInsComm.Parameters.AddWithValue("?", reader["M_ID"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["M_ID"]); // For IF NOT EXISTS
                            sqlInsComm.Parameters.AddWithValue("?", reader["M_Date"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["M_Amount"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["M_Insrt_Person"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["M_Updt_Person"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["M_Del_Person"]);
                            sqlInsComm.ExecuteNonQuery();
                        }
                    }
                    sqlConn.Close();
                }
                accConn.Close();
            }
        }
        private void marketMemosSync()
        {
            string insCom = "IF NOT EXISTS (SELECT * FROM MarketMemos WHERE Mem_ID = ?) " +
                                "BEGIN " +
                                    $"INSERT INTO MarketMemos (Mem_ID,Mem_Date,R_InvTK,C_InvTK,Giv_TK,Ret_TK,I_N01,I_N02,I_N03,I_N04,I_N05,I_N06,I_N07,I_N08,I_N09,I_N10,I_N11,I_N12,I_N13,I_N14,I_N15,I_N16,I_P01,I_P02,I_P03,I_P04,I_P05,I_P06,I_P07,I_P08,I_P09,I_P10,I_P11,I_P12,I_P13,I_P14,I_P15,I_P16,I_Q01,I_Q02,I_Q03,I_Q04,I_Q05,I_Q06,I_Q07,I_Q08,I_Q09,I_Q10,I_Q11,I_Q12,I_Q13,I_Q14,I_Q15,I_Q16,I_ST01,I_ST02,I_ST03,I_ST04,I_ST05,I_ST06,I_ST07,I_ST08,I_ST09,I_ST10,I_ST11,I_ST12,I_ST13,I_ST14,I_ST15,I_ST16,R_Inv01,R_Inv02,R_Inv03,R_Inv04,R_Inv05,R_Inv06,R_Inv07,R_Inv08,R_Inv09,R_Inv10,R_Inv11,R_Inv12,R_Inv13,R_Inv14,R_Inv15,R_Inv16,R_Inv17,R_Inv18,R_Inv19,R_Inv20,R_Inv21,R_Inv22,R_Inv23,R_Inv24,Mem_Insrt_Person,Mem_Updt_Person,Mem_Del_Person) " +
                                    "VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?) " +
                                "END ";
            using (OleDbConnection accConn = new OleDbConnection(connAcc))
            {
                accConn.Open();
                string selCom = "SELECT * FROM MarketMemos";
                OleDbCommand command = new OleDbCommand(selCom, accConn);
                OleDbDataReader reader = command.ExecuteReader();
                using (OdbcConnection sqlConn = new OdbcConnection(connSql))
                {
                    sqlConn.Open();
                    string checkTableQuery = "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'MarketMemos') " +
                                             "BEGIN " +
                                                $"CREATE TABLE MarketMemos (Mem_ID nvarchar(250) NULL,Mem_Date datetime NULL,R_InvTK float NULL,C_InvTK float NULL,Giv_TK float NULL,Ret_TK float NULL,I_N01 nvarchar(250) NULL,I_N02 nvarchar(250) NULL,I_N03 nvarchar(250) NULL,I_N04 nvarchar(250) NULL,I_N05 nvarchar(250) NULL,I_N06 nvarchar(250) NULL,I_N07 nvarchar(250) NULL,I_N08 nvarchar(250) NULL,I_N09 nvarchar(250) NULL,I_N10 nvarchar(250) NULL,I_N11 nvarchar(250) NULL,I_N12 nvarchar(250) NULL,I_N13 nvarchar(250) NULL,I_N14 nvarchar(250) NULL,I_N15 nvarchar(250) NULL,I_N16 nvarchar(250) NULL,I_P01 float NULL,I_P02 float NULL,I_P03 float NULL,I_P04 float NULL,I_P05 float NULL,I_P06 float NULL,I_P07 float NULL,I_P08 float NULL,I_P09 float NULL,I_P10 float NULL,I_P11 float NULL,I_P12 float NULL,I_P13 float NULL,I_P14 float NULL,I_P15 float NULL,I_P16 float NULL,I_Q01 float NULL,I_Q02 float NULL,I_Q03 float NULL,I_Q04 float NULL,I_Q05 float NULL,I_Q06 float NULL,I_Q07 float NULL,I_Q08 float NULL,I_Q09 float NULL,I_Q10 float NULL,I_Q11 float NULL,I_Q12 float NULL,I_Q13 float NULL,I_Q14 float NULL,I_Q15 float NULL,I_Q16 float NULL,I_ST01 float NULL,I_ST02 float NULL,I_ST03 float NULL,I_ST04 float NULL,I_ST05 float NULL,I_ST06 float NULL,I_ST07 float NULL,I_ST08 float NULL,I_ST09 float NULL,I_ST10 float NULL,I_ST11 float NULL,I_ST12 float NULL,I_ST13 float NULL,I_ST14 float NULL,I_ST15 float NULL,I_ST16 float NULL,R_Inv01 float NULL,R_Inv02 float NULL,R_Inv03 float NULL,R_Inv04 float NULL,R_Inv05 float NULL,R_Inv06 float NULL,R_Inv07 float NULL,R_Inv08 float NULL,R_Inv09 float NULL,R_Inv10 float NULL,R_Inv11 float NULL,R_Inv12 float NULL,R_Inv13 float NULL,R_Inv14 float NULL,R_Inv15 float NULL,R_Inv16 float NULL,R_Inv17 float NULL,R_Inv18 float NULL,R_Inv19 float NULL,R_Inv20 float NULL,R_Inv21 float NULL,R_Inv22 float NULL,R_Inv23 float NULL,R_Inv24 float NULL,Mem_Insrt_Person nvarchar(250) NULL,Mem_Updt_Person nvarchar(250) NULL,Mem_Del_Person nvarchar(250) NULL) " +
                                             "END";
                    using (OdbcCommand checkTableCommand = new OdbcCommand(checkTableQuery, sqlConn))
                    {
                        checkTableCommand.ExecuteNonQuery();
                    }
                    while (reader.Read())
                    {
                        using (OdbcCommand sqlInsComm = new OdbcCommand(insCom, sqlConn))
                        {
                            sqlInsComm.Parameters.AddWithValue("?", reader["Mem_ID"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Mem_ID"]); // For IF NOT EXISTS
                            sqlInsComm.Parameters.AddWithValue("?", reader["Mem_Date"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_InvTK"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["C_InvTK"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Giv_TK"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Ret_TK"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_N01"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_N02"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_N03"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_N04"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_N05"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_N06"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_N07"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_N08"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_N09"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_N10"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_N11"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_N12"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_N13"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_N14"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_N15"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_N16"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_P01"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_P02"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_P03"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_P04"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_P05"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_P06"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_P07"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_P08"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_P09"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_P10"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_P11"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_P12"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_P13"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_P14"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_P15"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_P16"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_Q01"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_Q02"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_Q03"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_Q04"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_Q05"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_Q06"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_Q07"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_Q08"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_Q09"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_Q10"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_Q11"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_Q12"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_Q13"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_Q14"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_Q15"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_Q16"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_ST01"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_ST02"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_ST03"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_ST04"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_ST05"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_ST06"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_ST07"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_ST08"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_ST09"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_ST10"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_ST11"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_ST12"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_ST13"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_ST14"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_ST15"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_ST16"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv01"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv02"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv03"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv04"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv05"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv06"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv07"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv08"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv09"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv10"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv11"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv12"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv13"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv14"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv15"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv16"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv17"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv18"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv19"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv20"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv21"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv22"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv23"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv24"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Mem_Insrt_Person"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Mem_Updt_Person"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Mem_Del_Person"]);
                            sqlInsComm.ExecuteNonQuery();
                        }
                    }
                    sqlConn.Close();
                }
                accConn.Close();
            }
        }
        private void marketMemosDelSync()
        {
            string insCom = "IF NOT EXISTS (SELECT * FROM MarketMemosDel WHERE Mem_ID = ?) " +
                                "BEGIN " +
                                    $"INSERT INTO MarketMemosDel (Mem_ID,Mem_Date,R_InvTK,C_InvTK,Giv_TK,Ret_TK,I_N01,I_N02,I_N03,I_N04,I_N05,I_N06,I_N07,I_N08,I_N09,I_N10,I_N11,I_N12,I_N13,I_N14,I_N15,I_N16,I_P01,I_P02,I_P03,I_P04,I_P05,I_P06,I_P07,I_P08,I_P09,I_P10,I_P11,I_P12,I_P13,I_P14,I_P15,I_P16,I_Q01,I_Q02,I_Q03,I_Q04,I_Q05,I_Q06,I_Q07,I_Q08,I_Q09,I_Q10,I_Q11,I_Q12,I_Q13,I_Q14,I_Q15,I_Q16,I_ST01,I_ST02,I_ST03,I_ST04,I_ST05,I_ST06,I_ST07,I_ST08,I_ST09,I_ST10,I_ST11,I_ST12,I_ST13,I_ST14,I_ST15,I_ST16,R_Inv01,R_Inv02,R_Inv03,R_Inv04,R_Inv05,R_Inv06,R_Inv07,R_Inv08,R_Inv09,R_Inv10,R_Inv11,R_Inv12,R_Inv13,R_Inv14,R_Inv15,R_Inv16,R_Inv17,R_Inv18,R_Inv19,R_Inv20,R_Inv21,R_Inv22,R_Inv23,R_Inv24,Mem_Insrt_Person,Mem_Updt_Person,Mem_Del_Person) " +
                                    "VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?) " +
                                "END ";
            using (OleDbConnection accConn = new OleDbConnection(connAcc))
            {
                accConn.Open();
                string selCom = "SELECT * FROM MarketMemosDel";
                OleDbCommand command = new OleDbCommand(selCom, accConn);
                OleDbDataReader reader = command.ExecuteReader();
                using (OdbcConnection sqlConn = new OdbcConnection(connSql))
                {
                    sqlConn.Open();
                    string checkTableQuery = "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'MarketMemosDel') " +
                                             "BEGIN " +
                                                $"CREATE TABLE MarketMemosDel (Mem_ID nvarchar(250) NULL,Mem_Date datetime NULL,R_InvTK float NULL,C_InvTK float NULL,Giv_TK float NULL,Ret_TK float NULL,I_N01 nvarchar(250) NULL,I_N02 nvarchar(250) NULL,I_N03 nvarchar(250) NULL,I_N04 nvarchar(250) NULL,I_N05 nvarchar(250) NULL,I_N06 nvarchar(250) NULL,I_N07 nvarchar(250) NULL,I_N08 nvarchar(250) NULL,I_N09 nvarchar(250) NULL,I_N10 nvarchar(250) NULL,I_N11 nvarchar(250) NULL,I_N12 nvarchar(250) NULL,I_N13 nvarchar(250) NULL,I_N14 nvarchar(250) NULL,I_N15 nvarchar(250) NULL,I_N16 nvarchar(250) NULL,I_P01 float NULL,I_P02 float NULL,I_P03 float NULL,I_P04 float NULL,I_P05 float NULL,I_P06 float NULL,I_P07 float NULL,I_P08 float NULL,I_P09 float NULL,I_P10 float NULL,I_P11 float NULL,I_P12 float NULL,I_P13 float NULL,I_P14 float NULL,I_P15 float NULL,I_P16 float NULL,I_Q01 float NULL,I_Q02 float NULL,I_Q03 float NULL,I_Q04 float NULL,I_Q05 float NULL,I_Q06 float NULL,I_Q07 float NULL,I_Q08 float NULL,I_Q09 float NULL,I_Q10 float NULL,I_Q11 float NULL,I_Q12 float NULL,I_Q13 float NULL,I_Q14 float NULL,I_Q15 float NULL,I_Q16 float NULL,I_ST01 float NULL,I_ST02 float NULL,I_ST03 float NULL,I_ST04 float NULL,I_ST05 float NULL,I_ST06 float NULL,I_ST07 float NULL,I_ST08 float NULL,I_ST09 float NULL,I_ST10 float NULL,I_ST11 float NULL,I_ST12 float NULL,I_ST13 float NULL,I_ST14 float NULL,I_ST15 float NULL,I_ST16 float NULL,R_Inv01 float NULL,R_Inv02 float NULL,R_Inv03 float NULL,R_Inv04 float NULL,R_Inv05 float NULL,R_Inv06 float NULL,R_Inv07 float NULL,R_Inv08 float NULL,R_Inv09 float NULL,R_Inv10 float NULL,R_Inv11 float NULL,R_Inv12 float NULL,R_Inv13 float NULL,R_Inv14 float NULL,R_Inv15 float NULL,R_Inv16 float NULL,R_Inv17 float NULL,R_Inv18 float NULL,R_Inv19 float NULL,R_Inv20 float NULL,R_Inv21 float NULL,R_Inv22 float NULL,R_Inv23 float NULL,R_Inv24 float NULL,Mem_Insrt_Person nvarchar(250) NULL,Mem_Updt_Person nvarchar(250) NULL,Mem_Del_Person nvarchar(250) NULL) " +
                                             "END";
                    using (OdbcCommand checkTableCommand = new OdbcCommand(checkTableQuery, sqlConn))
                    {
                        checkTableCommand.ExecuteNonQuery();
                    }
                    while (reader.Read())
                    {
                        using (OdbcCommand sqlInsComm = new OdbcCommand(insCom, sqlConn))
                        {
                            sqlInsComm.Parameters.AddWithValue("?", reader["Mem_ID"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Mem_ID"]); // For IF NOT EXISTS
                            sqlInsComm.Parameters.AddWithValue("?", reader["Mem_Date"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_InvTK"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["C_InvTK"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Giv_TK"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Ret_TK"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_N01"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_N02"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_N03"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_N04"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_N05"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_N06"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_N07"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_N08"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_N09"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_N10"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_N11"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_N12"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_N13"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_N14"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_N15"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_N16"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_P01"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_P02"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_P03"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_P04"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_P05"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_P06"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_P07"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_P08"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_P09"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_P10"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_P11"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_P12"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_P13"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_P14"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_P15"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_P16"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_Q01"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_Q02"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_Q03"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_Q04"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_Q05"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_Q06"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_Q07"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_Q08"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_Q09"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_Q10"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_Q11"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_Q12"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_Q13"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_Q14"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_Q15"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_Q16"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_ST01"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_ST02"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_ST03"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_ST04"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_ST05"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_ST06"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_ST07"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_ST08"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_ST09"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_ST10"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_ST11"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_ST12"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_ST13"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_ST14"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_ST15"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_ST16"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv01"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv02"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv03"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv04"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv05"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv06"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv07"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv08"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv09"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv10"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv11"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv12"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv13"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv14"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv15"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv16"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv17"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv18"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv19"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv20"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv21"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv22"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv23"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["R_Inv24"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Mem_Insrt_Person"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Mem_Updt_Person"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Mem_Del_Person"]);
                            sqlInsComm.ExecuteNonQuery();
                        }
                    }
                    sqlConn.Close();
                }
                accConn.Close();
            }
        }
        private void dailySavingSync()
        {
            string insCom = "IF NOT EXISTS (SELECT * FROM DailySaving WHERE DS_ID = ?) " +
                                "BEGIN " +
                                    $"INSERT INTO DailySaving (DS_ID,DS_Date,DS_FPAmount,DS_SPAmount,DS_TPAmount,NotTaken,DS_Data,DS_InBankDate,DS_Insrt_Person,DS_Updt_Person,DS_Del_Person) VALUES (?,?,?,?,?,?,?,?,?,?,?)" +
                                "END";
            using (OleDbConnection accConn = new OleDbConnection(connAcc))
            {
                accConn.Open();
                string selCom = "SELECT * FROM DailySaving";
                OleDbCommand command = new OleDbCommand(selCom, accConn);
                OleDbDataReader reader = command.ExecuteReader();
                using (OdbcConnection sqlConn = new OdbcConnection(connSql))
                {
                    sqlConn.Open();
                    string checkTableQuery = "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'DailySaving') " +
                                             "BEGIN " +
                                                $"CREATE TABLE DailySaving (DS_ID nvarchar(250) NULL,DS_Date datetime NULL,DS_FPAmount float NULL,DS_SPAmount float NULL,DS_TPAmount float NULL,NotTaken nvarchar(250) NULL,DS_Data nvarchar(250) NULL,DS_InBankDate datetime NULL,DS_Insrt_Person nvarchar(250) NULL,DS_Updt_Person nvarchar(250) NULL,DS_Del_Person nvarchar(250) NULL) " +
                                             "END";
                    using (OdbcCommand checkTableCommand = new OdbcCommand(checkTableQuery, sqlConn))
                    {
                        checkTableCommand.ExecuteNonQuery();
                    }
                    while (reader.Read())
                    {
                        using (OdbcCommand sqlInsComm = new OdbcCommand(insCom, sqlConn))
                        {
                            sqlInsComm.Parameters.AddWithValue("?", reader["DS_ID"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["DS_ID"]); // For IF NOT EXISTS
                            sqlInsComm.Parameters.AddWithValue("?", reader["DS_Date"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["DS_FPAmount"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["DS_SPAmount"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["DS_TPAmount"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["NotTaken"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["DS_Data"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["DS_InBankDate"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["DS_Insrt_Person"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["DS_Updt_Person"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["DS_Del_Person"]);
                            sqlInsComm.ExecuteNonQuery();
                        }
                    }
                    sqlConn.Close();
                }
                accConn.Close();
            }
        }
        private void installmentSync()
        {
            string insCom = "IF NOT EXISTS (SELECT * FROM Installment WHERE I_ID = ?) " +
                                "BEGIN " +
                                    $"INSERT INTO Installment (I_ID,I_Date,Take_Total,Take_Anot,Take_Mine,Take_Data,InsPerMonth,PerMonthPay,InsPay,InsPay_Date,I_Insrt_Person,I_Updt_Person,I_Del_Person) VALUES (?,?,?,?,?,?,?,?,?,?,?)" +
                                "END";
            using (OleDbConnection accConn = new OleDbConnection(connAcc))
            {
                accConn.Open();
                string selCom = "SELECT * FROM Installment";
                OleDbCommand command = new OleDbCommand(selCom, accConn);
                OleDbDataReader reader = command.ExecuteReader();
                using (OdbcConnection sqlConn = new OdbcConnection(connSql))
                {
                    sqlConn.Open();
                    string checkTableQuery = "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Installment') " +
                                             "BEGIN " +
                                                $"CREATE TABLE Installment (I_ID nvarchar(250) NULL,I_Date datetime NULL,Take_Total float NULL,Take_Anot float NULL,Take_Mine float NULL,Take_Data nvarchar(250) NULL,InsPerMonth float NULL,PerMonthPay float NULL,InsPay float NULL,InsPay_Date datetime NULL,I_Insrt_Person nvarchar(250) NULL,I_Updt_Person nvarchar(250) NULL,I_Del_Person nvarchar(250) NULL) " +
                                             "END";
                    using (OdbcCommand checkTableCommand = new OdbcCommand(checkTableQuery, sqlConn))
                    {
                        checkTableCommand.ExecuteNonQuery();
                    }
                    while (reader.Read())
                    {
                        using (OdbcCommand sqlInsComm = new OdbcCommand(insCom, sqlConn))
                        {
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_ID"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_ID"]); // For IF NOT EXISTS
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_Date"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Take_Total"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Take_Anot"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Take_Mine"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Take_Data"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["InsPerMonth"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["PerMonthPay"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["InsPay"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["InsPay_Date"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_Insrt_Person"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_Updt_Person"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["I_Del_Person"]);
                            sqlInsComm.ExecuteNonQuery();
                        }
                    }
                    sqlConn.Close();
                }
                accConn.Close();
            }
        }
        private void installmentPaySync()
        {
            //Work Later
        }
        private void bikeInfoSync()
        {
            string insCom = "IF NOT EXISTS (SELECT * FROM BikeInfo WHERE B_ID = ?) " +
                                "BEGIN " +
                                    $"INSERT INTO BikeInfo (B_ID,B_Chng_Date,B_KM_ODO,B_Mobile_Go,B_Next_ODO,B_Insrt_Person,B_Updt_Person) VALUES (?, ?, ?, ?, ?, ?, ?) " +
                                "END ";
            using (OleDbConnection accConn = new OleDbConnection(connAcc))
            {
                accConn.Open();
                string selCom = "SELECT * FROM BikeInfo";
                OleDbCommand command = new OleDbCommand(selCom, accConn);
                OleDbDataReader reader = command.ExecuteReader();
                using (OdbcConnection sqlConn = new OdbcConnection(connSql))
                {
                    sqlConn.Open();
                    string checkTableQuery = "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'BikeInfo') " +
                                             "BEGIN " +
                                                $"CREATE TABLE BikeInfo (B_ID nvarchar(250) NULL,B_Chng_Date datetime NULL,B_KM_ODO nvarchar(250) NULL,B_Mobile_Go nvarchar(250) NULL,B_Next_ODO nvarchar(250) NULL,B_Insrt_Person nvarchar(250) NULL,B_Updt_Person nvarchar(250) NULL) " +
                                             "END";
                    using (OdbcCommand checkTableCommand = new OdbcCommand(checkTableQuery, sqlConn))
                    {
                        checkTableCommand.ExecuteNonQuery();
                    }
                    while (reader.Read())
                    {
                        using (OdbcCommand sqlInsComm = new OdbcCommand(insCom, sqlConn))
                        {
                            sqlInsComm.Parameters.AddWithValue("?", reader["B_ID"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["B_ID"]); // For IF NOT EXISTS
                            sqlInsComm.Parameters.AddWithValue("?", reader["B_Chng_Date"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["B_KM_ODO"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["B_Mobile_Go"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["B_Next_ODO"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["B_Insrt_Person"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["B_Updt_Person"]);
                            sqlInsComm.ExecuteNonQuery();
                        }
                    }
                    sqlConn.Close();
                }
                accConn.Close();
            }
        }
        private void givenSync()
        {
            string insCom = "IF NOT EXISTS (SELECT * FROM Given WHERE InGiven = ?) " +
                                "BEGIN " +
                                    $"INSERT INTO Given (InGiven,Total_Given,Given_To,ThroughBy_Given,Given_Date,Remarks_Given,GDT_V,GDT_V_Date,DDT_V_Date,G_Insrt_Person,G_Updt_Person,G_Del_Person) VALUES (?,?,?,?,?,?,?,?,?,?,?,?)" +
                                "END";
            using (OleDbConnection accConn = new OleDbConnection(connAcc))
            {
                accConn.Open();
                string selCom = "SELECT * FROM Given";
                OleDbCommand command = new OleDbCommand(selCom, accConn);
                OleDbDataReader reader = command.ExecuteReader();
                using (OdbcConnection sqlConn = new OdbcConnection(connSql))
                {
                    sqlConn.Open();
                    string checkTableQuery = "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Given') " +
                                             "BEGIN " +
                                                $"CREATE TABLE Given (InGiven nvarchar(250) NULL,Total_Given float NULL,Given_To nvarchar(250) NULL,ThroughBy_Given nvarchar(250) NULL,Given_Date datetime NULL,Remarks_Given nvarchar(250) NULL,GDT_V nvarchar(250) NULL,GDT_V_Date datetime NULL,DDT_V_Date datetime NULL,G_Insrt_Person nvarchar(250) NULL,G_Updt_Person nvarchar(250) NULL,G_Del_Person nvarchar(250) NULL) " +
                                             "END";
                    using (OdbcCommand checkTableCommand = new OdbcCommand(checkTableQuery, sqlConn))
                    {
                        checkTableCommand.ExecuteNonQuery();
                    }
                    while (reader.Read())
                    {
                        using (OdbcCommand sqlInsComm = new OdbcCommand(insCom, sqlConn))
                        {
                            sqlInsComm.Parameters.AddWithValue("?", reader["InGiven"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["InGiven"]); // For IF NOT EXISTS
                            sqlInsComm.Parameters.AddWithValue("?", reader["Total_Given"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Given_To"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["ThroughBy_Given"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Given_Date"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Remarks_Given"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["GDT_V"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["GDT_V_Date"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["DDT_V_Date"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["G_Insrt_Person"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["G_Updt_Person"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["G_Del_Person"]);
                            sqlInsComm.ExecuteNonQuery();
                        }
                    }
                    sqlConn.Close();
                }
                accConn.Close();
            }
        }
        private void givenUpdtSync()
        {
            string insCom = "IF NOT EXISTS (SELECT * FROM GivenUpdt WHERE InGiven = ?) " +
                                "BEGIN " +
                                    $"INSERT INTO GivenUpdt (InGiven,Was_Given,Now_Given,Total_Given,Given_To,GDT_V_Date) VALUES (?,?,?,?,?,?)" +
                                "END";
            using (OleDbConnection accConn = new OleDbConnection(connAcc))
            {
                accConn.Open();
                string selCom = "SELECT * FROM GivenUpdt";
                OleDbCommand command = new OleDbCommand(selCom, accConn);
                OleDbDataReader reader = command.ExecuteReader();
                using (OdbcConnection sqlConn = new OdbcConnection(connSql))
                {
                    sqlConn.Open();
                    string checkTableQuery = "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'GivenUpdt') " +
                                             "BEGIN " +
                                                $"CREATE TABLE GivenUpdt (InGiven nvarchar(250) NULL,Was_Given float NULL,Now_Given float NULL,Total_Given float NULL,Given_To nvarchar(250) NULL,GDT_V_Date datetime NULL) " +
                                             "END";
                    using (OdbcCommand checkTableCommand = new OdbcCommand(checkTableQuery, sqlConn))
                    {
                        checkTableCommand.ExecuteNonQuery();
                    }
                    while (reader.Read())
                    {
                        using (OdbcCommand sqlInsComm = new OdbcCommand(insCom, sqlConn))
                        {
                            sqlInsComm.Parameters.AddWithValue("?", reader["InGiven"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["InGiven"]); // For IF NOT EXISTS
                            sqlInsComm.Parameters.AddWithValue("?", reader["Was_Given"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Now_Given"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Total_Given"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Given_To"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["GDT_V_Date"]);

                            sqlInsComm.ExecuteNonQuery();
                        }
                    }
                    sqlConn.Close();
                }
                accConn.Close();
            }
        }
        private void tekenSync()
        {
            string insCom = "IF NOT EXISTS (SELECT * FROM Teken WHERE InTake = ?) " +
                                "BEGIN " +
                                    $"INSERT INTO Teken (InTake,Total_Take,Take_To,ThroughBy_Take,Take_Date,Remarks_Take,TDT_V,TDT_V_Date,DDT_V_Date,T_Insrt_Person,T_Updt_Person,T_Del_Person) VALUES (?,?,?,?,?,?,?,?,?,?,?,?)" +
                                "END";
            using (OleDbConnection accConn = new OleDbConnection(connAcc))
            {
                accConn.Open();
                string selCom = "SELECT * FROM Teken";
                OleDbCommand command = new OleDbCommand(selCom, accConn);
                OleDbDataReader reader = command.ExecuteReader();
                using (OdbcConnection sqlConn = new OdbcConnection(connSql))
                {
                    sqlConn.Open();
                    string checkTableQuery = "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Teken') " +
                                             "BEGIN " +
                                                $"CREATE TABLE Teken (InTake nvarchar(250) NULL,Total_Take float NULL,Take_To nvarchar(250) NULL,ThroughBy_Take nvarchar(250) NULL,Take_Date datetime NULL,Remarks_Take nvarchar(250) NULL,TDT_V nvarchar(250) NULL,TDT_V_Date datetime NULL,DDT_V_Date datetime NULL,T_Insrt_Person nvarchar(250) NULL,T_Updt_Person nvarchar(250) NULL,T_Del_Person nvarchar(250) NULL) " +
                                             "END";
                    using (OdbcCommand checkTableCommand = new OdbcCommand(checkTableQuery, sqlConn))
                    {
                        checkTableCommand.ExecuteNonQuery();
                    }
                    while (reader.Read())
                    {
                        using (OdbcCommand sqlInsComm = new OdbcCommand(insCom, sqlConn))
                        {
                            sqlInsComm.Parameters.AddWithValue("?", reader["InTake"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["InTake"]); // For IF NOT EXISTS
                            sqlInsComm.Parameters.AddWithValue("?", reader["Total_Take"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Take_To"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["ThroughBy_Take"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Take_Date"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Remarks_Take"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["TDT_V"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["TDT_V_Date"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["DDT_V_Date"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["T_Insrt_Person"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["T_Updt_Person"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["T_Del_Person"]);
                            sqlInsComm.ExecuteNonQuery();
                        }
                    }
                    sqlConn.Close();
                }
                accConn.Close();
            }
        }
        private void tekenUpdtSync()
        {
            string insCom = "IF NOT EXISTS (SELECT * FROM TekenUpdt WHERE InTake = ?) " +
                                "BEGIN " +
                                    $"INSERT INTO TekenUpdt (InTake,Was_Take,Now_Take,Total_Take,Take_To,TDT_V_Date) VALUES (?,?,?,?,?,?)" +
                                "END";
            using (OleDbConnection accConn = new OleDbConnection(connAcc))
            {
                accConn.Open();
                string selCom = "SELECT * FROM TekenUpdt";
                OleDbCommand command = new OleDbCommand(selCom, accConn);
                OleDbDataReader reader = command.ExecuteReader();
                using (OdbcConnection sqlConn = new OdbcConnection(connSql))
                {
                    sqlConn.Open();
                    string checkTableQuery = "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TekenUpdt') " +
                                             "BEGIN " +
                                                $"CREATE TABLE TekenUpdt (InTake nvarchar(250) NULL,Was_Take float NULL,Now_Take float NULL,Total_Take float NULL,Take_To nvarchar(250) NULL,TDT_V_Date datetime NULL) " +
                                             "END";
                    using (OdbcCommand checkTableCommand = new OdbcCommand(checkTableQuery, sqlConn))
                    {
                        checkTableCommand.ExecuteNonQuery();
                    }
                    while (reader.Read())
                    {
                        using (OdbcCommand sqlInsComm = new OdbcCommand(insCom, sqlConn))
                        {
                            sqlInsComm.Parameters.AddWithValue("?", reader["InTake"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["InTake"]); // For IF NOT EXISTS
                            sqlInsComm.Parameters.AddWithValue("?", reader["Was_Take"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Now_Take"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Total_Take"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Take_To"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["TDT_V_Date"]);
                            sqlInsComm.ExecuteNonQuery();
                        }
                    }
                    sqlConn.Close();
                }
                accConn.Close();
            }
        }
        private void expenseSync()
        {
            string insCom = "IF NOT EXISTS (SELECT * FROM TariffAmt WHERE InExpense = ?) " +
                                "BEGIN " +
                                    $"INSERT INTO TariffAmt (InExpense,Expense_Amount,Expense_To,ThroughBy_Expense,Expense_Date,Remarks_Expense,EDT_V,EDT_V_Date,DDT_V_Date,E_Insrt_Person,E_Updt_Person,E_Del_Person) VALUES (?,?,?,?,?,?,?,?,?,?,?,?)" +
                                "END";
            using (OleDbConnection accConn = new OleDbConnection(connAcc))
            {
                accConn.Open();
                string selCom = "SELECT * FROM TariffAmt";
                OleDbCommand command = new OleDbCommand(selCom, accConn);
                OleDbDataReader reader = command.ExecuteReader();
                using (OdbcConnection sqlConn = new OdbcConnection(connSql))
                {
                    sqlConn.Open();
                    string checkTableQuery = "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TariffAmt') " +
                                             "BEGIN " +
                                                $"CREATE TABLE TariffAmt (InExpense nvarchar(250) NULL,Expense_Amount float NULL,Expense_To nvarchar(250) NULL,ThroughBy_Expense nvarchar(250) NULL,Expense_Date datetime NULL,Remarks_Expense nvarchar(250) NULL,EDT_V nvarchar(250) NULL,EDT_V_Date datetime NULL,DDT_V_Date datetime NULL,E_Insrt_Person nvarchar(250) NULL,E_Updt_Person nvarchar(250) NULL,E_Del_Person nvarchar(250) NULL) " +
                                             "END";
                    using (OdbcCommand checkTableCommand = new OdbcCommand(checkTableQuery, sqlConn))
                    {
                        checkTableCommand.ExecuteNonQuery();
                    }
                    while (reader.Read())
                    {
                        using (OdbcCommand sqlInsComm = new OdbcCommand(insCom, sqlConn))
                        {
                            sqlInsComm.Parameters.AddWithValue("?", reader["InExpense"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["InExpense"]); // For IF NOT EXISTS
                            sqlInsComm.Parameters.AddWithValue("?", reader["Expense_Amount"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Expense_To"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["ThroughBy_Expense"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Expense_Date"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Remarks_Expense"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["EDT_V"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["EDT_V_Date"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["DDT_V_Date"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["E_Insrt_Person"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["E_Updt_Person"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["E_Del_Person"]);
                            sqlInsComm.ExecuteNonQuery();
                        }
                    }
                    sqlConn.Close();
                }
                accConn.Close();
            }
        }
        private void expenseUpdtSync()
        {
            string insCom = "IF NOT EXISTS (SELECT * FROM TariffAmtUpdt WHERE InExpense = ?) " +
                                "BEGIN " +
                                    $"INSERT INTO TariffAmtUpdt (InExpense,Was_Expense,Now_Expense,Expense_Amount,Expense_To,EDT_V_Date) VALUES (?,?,?,?,?,?)" +
                                "END";
            using (OleDbConnection accConn = new OleDbConnection(connAcc))
            {
                accConn.Open();
                string selCom = "SELECT * FROM TariffAmtUpdt";
                OleDbCommand command = new OleDbCommand(selCom, accConn);
                OleDbDataReader reader = command.ExecuteReader();
                using (OdbcConnection sqlConn = new OdbcConnection(connSql))
                {
                    sqlConn.Open();
                    string checkTableQuery = "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TariffAmtUpdt') " +
                                             "BEGIN " +
                                                $"CREATE TABLE TariffAmtUpdt (InExpense nvarchar(250) NULL,Was_Expense float NULL,Now_Expense float NULL,Expense_Amount float NULL,Expense_To nvarchar(250) NULL,EDT_V_Date datetime NULL) " +
                                             "END";
                    using (OdbcCommand checkTableCommand = new OdbcCommand(checkTableQuery, sqlConn))
                    {
                        checkTableCommand.ExecuteNonQuery();
                    }
                    while (reader.Read())
                    {
                        using (OdbcCommand sqlInsComm = new OdbcCommand(insCom, sqlConn))
                        {
                            sqlInsComm.Parameters.AddWithValue("?", reader["InExpense"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["InExpense"]); // For IF NOT EXISTS
                            sqlInsComm.Parameters.AddWithValue("?", reader["Was_Expense"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Now_Expense"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Expense_Amount"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Expense_To"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["EDT_V_Date"]);
                            sqlInsComm.ExecuteNonQuery();
                        }
                    }
                    sqlConn.Close();
                }
                accConn.Close();
            }
        }
        private void savingSync()
        {
            string insCom = "IF NOT EXISTS (SELECT * FROM Saving WHERE InSaving = ?) " +
                                "BEGIN " +
                                    $"INSERT INTO Saving (InSaving,Saving_Amount,Saving_To,ThroughBy_Saving,Saving_Date,Remarks_Saving,SDT_V,SDT_V_Date,DDT_V_Date,Saving_Bank,S_Insrt_Person,S_Updt_Person,S_Del_Person) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?)" +
                                "END";
            using (OleDbConnection accConn = new OleDbConnection(connAcc))
            {
                accConn.Open();
                string selCom = "SELECT * FROM Saving";
                OleDbCommand command = new OleDbCommand(selCom, accConn);
                OleDbDataReader reader = command.ExecuteReader();
                using (OdbcConnection sqlConn = new OdbcConnection(connSql))
                {
                    sqlConn.Open();
                    string checkTableQuery = "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Saving') " +
                                             "BEGIN " +
                                                $"CREATE TABLE Saving (InSaving nvarchar(250) NULL,Saving_Amount float NULL,Saving_To nvarchar(250) NULL,ThroughBy_Saving nvarchar(250) NULL,Saving_Date datetime NULL,Remarks_Saving nvarchar(250) NULL,SDT_V nvarchar(250) NULL,SDT_V_Date datetime NULL,DDT_V_Date datetime NULL,Saving_Bank nvarchar(250) NULL,S_Insrt_Person nvarchar(250) NULL,S_Updt_Person nvarchar(250) NULL,S_Del_Person nvarchar(250) NULL) " +
                                             "END";
                    using (OdbcCommand checkTableCommand = new OdbcCommand(checkTableQuery, sqlConn))
                    {
                        checkTableCommand.ExecuteNonQuery();
                    }
                    while (reader.Read())
                    {
                        using (OdbcCommand sqlInsComm = new OdbcCommand(insCom, sqlConn))
                        {
                            sqlInsComm.Parameters.AddWithValue("?", reader["InSaving"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["InSaving"]); // For IF NOT EXISTS
                            sqlInsComm.Parameters.AddWithValue("?", reader["Saving_Amount"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Saving_To"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["ThroughBy_Saving"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Saving_Date"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Remarks_Saving"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["SDT_V"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["SDT_V_Date"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["DDT_V_Date"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Saving_Bank"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["S_Insrt_Person"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["S_Updt_Person"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["S_Del_Person"]);
                            sqlInsComm.ExecuteNonQuery();
                        }
                    }
                    sqlConn.Close();
                }
                accConn.Close();
            }
        }
        private void savingUpdtSync()
        {
            string insCom = "IF NOT EXISTS (SELECT * FROM SavingUpdt WHERE InSaving = ?) " +
                                "BEGIN " +
                                    $"INSERT INTO SavingUpdt (InSaving,Was_Saving,Now_Saving,Saving_Amount,Saving_To,SDT_V_Date) VALUES (?,?,?,?,?,?)" +
                                "END";
            using (OleDbConnection accConn = new OleDbConnection(connAcc))
            {
                accConn.Open();
                string selCom = "SELECT * FROM SavingUpdt";
                OleDbCommand command = new OleDbCommand(selCom, accConn);
                OleDbDataReader reader = command.ExecuteReader();
                using (OdbcConnection sqlConn = new OdbcConnection(connSql))
                {
                    sqlConn.Open();
                    string checkTableQuery = "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SavingUpdt') " +
                                             "BEGIN " +
                                                $"CREATE TABLE SavingUpdt (InSaving nvarchar(250) NULL,Was_Saving float NULL,Now_Saving float NULL,Saving_Amount float NULL,Saving_To nvarchar(250) NULL,SDT_V_Date datetime NULL) " +
                                             "END";
                    using (OdbcCommand checkTableCommand = new OdbcCommand(checkTableQuery, sqlConn))
                    {
                        checkTableCommand.ExecuteNonQuery();
                    }
                    while (reader.Read())
                    {
                        using (OdbcCommand sqlInsComm = new OdbcCommand(insCom, sqlConn))
                        {
                            sqlInsComm.Parameters.AddWithValue("?", reader["InSaving"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["InSaving"]); // For IF NOT EXISTS
                            sqlInsComm.Parameters.AddWithValue("?", reader["Was_Saving"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Now_Saving"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Saving_Amount"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Saving_To"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["SDT_V_Date"]);
                            sqlInsComm.ExecuteNonQuery();
                        }
                    }
                    sqlConn.Close();
                }
                accConn.Close();
            }
        }
        private void unratedSync()
        {
            string insCom = "IF NOT EXISTS (SELECT * FROM Unrated WHERE InUnrated = ?) " +
                                "BEGIN " +
                                    $"INSERT INTO Unrated (InUnrated,Unrated_Amount,Unrated_To,ThroughBy_Unrated,Unrated_Date,Remarks_Unrated,UDT_V,UDT_V_Date,DDT_V_Date,U_Insrt_Person,U_Updt_Person,U_Del_Person) VALUES (?,?,?,?,?,?,?,?,?,?,?,?)" +
                                "END";
            using (OleDbConnection accConn = new OleDbConnection(connAcc))
            {
                accConn.Open();
                string selCom = "SELECT * FROM Unrated";
                OleDbCommand command = new OleDbCommand(selCom, accConn);
                OleDbDataReader reader = command.ExecuteReader();
                using (OdbcConnection sqlConn = new OdbcConnection(connSql))
                {
                    sqlConn.Open();
                    string checkTableQuery = "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Unrated') " +
                                             "BEGIN " +
                                                $"CREATE TABLE Unrated (InUnrated nvarchar(250) NULL,Unrated_Amount float NULL,Unrated_To nvarchar(250) NULL,ThroughBy_Unrated nvarchar(250) NULL,Unrated_Date datetime NULL,Remarks_Unrated nvarchar(250) NULL,UDT_V nvarchar(250) NULL,UDT_V_Date datetime NULL,DDT_V_Date datetime NULL,U_Insrt_Person nvarchar(250) NULL,U_Updt_Person nvarchar(250) NULL,U_Del_Person nvarchar(250) NULL) " +
                                             "END";
                    using (OdbcCommand checkTableCommand = new OdbcCommand(checkTableQuery, sqlConn))
                    {
                        checkTableCommand.ExecuteNonQuery();
                    }
                    while (reader.Read())
                    {
                        using (OdbcCommand sqlInsComm = new OdbcCommand(insCom, sqlConn))
                        {
                            sqlInsComm.Parameters.AddWithValue("?", reader["InUnrated"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["InUnrated"]); // For IF NOT EXISTS
                            sqlInsComm.Parameters.AddWithValue("?", reader["Unrated_Amount"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Unrated_To"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["ThroughBy_Unrated"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Unrated_Date"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Remarks_Unrated"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["UDT_V"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["UDT_V_Date"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["DDT_V_Date"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["U_Insrt_Person"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["U_Updt_Person"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["U_Del_Person"]);
                            sqlInsComm.ExecuteNonQuery();
                        }
                    }
                    sqlConn.Close();
                }
                accConn.Close();
            }
        }
        private void unratedUpdtSync()
        {
            string insCom = "IF NOT EXISTS (SELECT * FROM UnratedUpdt WHERE InUnrated = ?) " +
                                "BEGIN " +
                                    $"INSERT INTO UnratedUpdt (InUnrated,Was_Unrated,Now_Unrated,Unrated_Amount,Unrated_To,UDT_V_Date) VALUES (?,?,?,?,?,?)" +
                                "END";
            using (OleDbConnection accConn = new OleDbConnection(connAcc))
            {
                accConn.Open();
                string selCom = "SELECT * FROM UnratedUpdt";
                OleDbCommand command = new OleDbCommand(selCom, accConn);
                OleDbDataReader reader = command.ExecuteReader();
                using (OdbcConnection sqlConn = new OdbcConnection(connSql))
                {
                    sqlConn.Open();
                    string checkTableQuery = "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'UnratedUpdt') " +
                                             "BEGIN " +
                                                $"CREATE TABLE UnratedUpdt (InUnrated nvarchar(250) NULL,Was_Unrated float NULL,Now_Unrated float NULL,Unrated_Amount float NULL,Unrated_To nvarchar(250) NULL,UDT_V_Date datetime NULL) " +
                                             "END";
                    using (OdbcCommand checkTableCommand = new OdbcCommand(checkTableQuery, sqlConn))
                    {
                        checkTableCommand.ExecuteNonQuery();
                    }
                    while (reader.Read())
                    {
                        using (OdbcCommand sqlInsComm = new OdbcCommand(insCom, sqlConn))
                        {
                            sqlInsComm.Parameters.AddWithValue("?", reader["InUnrated"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["InUnrated"]); // For IF NOT EXISTS
                            sqlInsComm.Parameters.AddWithValue("?", reader["Was_Unrated"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Now_Unrated"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Unrated_Amount"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["Unrated_To"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["UDT_V_Date"]);
                            sqlInsComm.ExecuteNonQuery();
                        }
                    }
                    sqlConn.Close();
                }
                accConn.Close();
            }
        }
        private void dailySync()
        {
            string insCom = "IF NOT EXISTS (SELECT * FROM Daily WHERE D_ID = ?) " +
                                "BEGIN " +
                                    $"INSERT INTO Daily (D_ID,D_Date,D_FPAmount,D_SPAmount,NotTaken,D_Data,TakenDate,D_Insrt_Person,D_Updt_Person,D_Del_Person) VALUES (?,?,?,?,?,?,?,?,?,?)" +
                                "END";
            using (OleDbConnection accConn = new OleDbConnection(connAcc))
            {
                accConn.Open();
                string selCom = "SELECT * FROM Daily";
                OleDbCommand command = new OleDbCommand(selCom, accConn);
                OleDbDataReader reader = command.ExecuteReader();
                using (OdbcConnection sqlConn = new OdbcConnection(connSql))
                {
                    sqlConn.Open();
                    string checkTableQuery = "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Daily') " +
                                             "BEGIN " +
                                                $"CREATE TABLE Daily (D_ID nvarchar(250) NULL,D_Date datetime NULL,D_FPAmount float NULL,D_SPAmount float NULL,NotTaken float NULL,D_Data nvarchar(250) NULL,TakenDate datetime NULL,D_Insrt_Person nvarchar(250) NULL,D_Updt_Person nvarchar(250) NULL,D_Del_Person nvarchar(250) NULL) " +
                                             "END";
                    using (OdbcCommand checkTableCommand = new OdbcCommand(checkTableQuery, sqlConn))
                    {
                        checkTableCommand.ExecuteNonQuery();
                    }
                    while (reader.Read())
                    {
                        using (OdbcCommand sqlInsComm = new OdbcCommand(insCom, sqlConn))
                        {
                            sqlInsComm.Parameters.AddWithValue("?", reader["D_ID"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["D_ID"]); // For IF NOT EXISTS
                            sqlInsComm.Parameters.AddWithValue("?", reader["D_Date"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["D_FPAmount"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["D_SPAmount"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["NotTaken"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["D_Data"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["TakenDate"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["D_Insrt_Person"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["D_Updt_Person"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["D_Del_Person"]);
                            sqlInsComm.ExecuteNonQuery();
                        }
                    }
                    sqlConn.Close();
                }
                accConn.Close();
            }
        }
        private void dailyCutSync()
        {
            string insCom = "IF NOT EXISTS (SELECT * FROM DailyCut WHERE C_ID = ?) " +
                                "BEGIN " +
                                    $"INSERT INTO DailyCut (C_ID,C_Date,C_Amount,C_Insrt_Person,C_Updt_Person,C_Del_Person) VALUES (?,?,?,?,?,?)" +
                                "END";
            using (OleDbConnection accConn = new OleDbConnection(connAcc))
            {
                accConn.Open();
                string selCom = "SELECT * FROM DailyCut";
                OleDbCommand command = new OleDbCommand(selCom, accConn);
                OleDbDataReader reader = command.ExecuteReader();
                using (OdbcConnection sqlConn = new OdbcConnection(connSql))
                {
                    sqlConn.Open();
                    string checkTableQuery = "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'DailyCut') " +
                                             "BEGIN " +
                                                $"CREATE TABLE DailyCut (C_ID nvarchar(250) NULL,C_Date datetime NULL,C_Amount float NULL,C_Insrt_Person nvarchar(250) NULL,C_Updt_Person nvarchar(250) NULL,C_Del_Person nvarchar(250) NULL) " +
                                             "END";
                    using (OdbcCommand checkTableCommand = new OdbcCommand(checkTableQuery, sqlConn))
                    {
                        checkTableCommand.ExecuteNonQuery();
                    }
                    while (reader.Read())
                    {
                        using (OdbcCommand sqlInsComm = new OdbcCommand(insCom, sqlConn))
                        {
                            sqlInsComm.Parameters.AddWithValue("?", reader["C_ID"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["C_ID"]); // For IF NOT EXISTS
                            sqlInsComm.Parameters.AddWithValue("?", reader["C_Date"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["C_Amount"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["C_Insrt_Person"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["C_Updt_Person"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["C_Del_Person"]);
                            sqlInsComm.ExecuteNonQuery();
                        }
                    }
                    sqlConn.Close();
                }
                accConn.Close();
            }
        }
        private void dailyAntSync()
        {
            string insCom = "IF NOT EXISTS (SELECT * FROM DailyAnt WHERE DA_ID = ?) " +
                                "BEGIN " +
                                    $"INSERT INTO DailyAnt (DA_ID,DA_Date,DA_FPAmount,DA_SPAmount,NotTaken,DA_Data,TakenDate,DA_Insrt_Person,DA_Updt_Person,DA_Del_Person) VALUES (?,?,?,?,?,?,?,?,?,?)" +
                                "END";
            using (OleDbConnection accConn = new OleDbConnection(connAcc))
            {
                accConn.Open();
                string selCom = "SELECT * FROM DailyAnt";
                OleDbCommand command = new OleDbCommand(selCom, accConn);
                OleDbDataReader reader = command.ExecuteReader();
                using (OdbcConnection sqlConn = new OdbcConnection(connSql))
                {
                    sqlConn.Open();
                    string checkTableQuery = "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'DailyAnt') " +
                                             "BEGIN " +
                                                $"CREATE TABLE DailyAnt (DA_ID nvarchar(250) NULL,DA_Date datetime NULL,DA_FPAmount float NULL,DA_SPAmount float NULL,NotTaken float NULL,DA_Data nvarchar(250) NULL,TakenDate datetime NULL,DA_Insrt_Person nvarchar(250) NULL,DA_Updt_Person nvarchar(250) NULL,DA_Del_Person nvarchar(250) NULL) " +
                                             "END";
                    using (OdbcCommand checkTableCommand = new OdbcCommand(checkTableQuery, sqlConn))
                    {
                        checkTableCommand.ExecuteNonQuery();
                    }
                    while (reader.Read())
                    {
                        using (OdbcCommand sqlInsComm = new OdbcCommand(insCom, sqlConn))
                        {
                            sqlInsComm.Parameters.AddWithValue("?", reader["DA_ID"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["DA_ID"]); // For IF NOT EXISTS
                            sqlInsComm.Parameters.AddWithValue("?", reader["DA_Date"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["DA_FPAmount"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["DA_SPAmount"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["NotTaken"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["DA_Data"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["TakenDate"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["DA_Insrt_Person"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["DA_Updt_Person"]);
                            sqlInsComm.Parameters.AddWithValue("?", reader["DA_Del_Person"]);
                            sqlInsComm.ExecuteNonQuery();
                        }
                    }
                    sqlConn.Close();
                }
                accConn.Close();
            }
        }
        private void imagesSync()
        {
            //Do it later
        }

        //--------------------------All DataGridView Events----------------------
        //-----------------------------------------------------------------------
        public DataTable GetMarketData(string marketId)
        {
            using (OleDbConnection conn = new OleDbConnection(this.conn.ConnectionString))
            {
                conn.Open();
                string query = "SELECT M_ID, M_Amount FROM Market WHERE M_ID = ?";
                using (OleDbDataAdapter oleDbDatadt = new OleDbDataAdapter(query, conn))
                {
                    oleDbDatadt.SelectCommand.Parameters.AddWithValue("?", marketId);
                    DataTable dataTabledt = new DataTable();
                    oleDbDatadt.Fill(dataTabledt);
                    conn.Close();
                    return dataTabledt;
                }
            }
        }
        public DataTable GetInstallmentData(string installmentId)
        {
            using (OleDbConnection conn = new OleDbConnection(this.conn.ConnectionString))
            {
                conn.Open();
                string query = "SELECT I_ID, InsPay FROM Installment WHERE I_ID = ?";
                using (OleDbDataAdapter oleDbDatadt = new OleDbDataAdapter(query, conn))
                {
                    oleDbDatadt.SelectCommand.Parameters.AddWithValue("?", installmentId);
                    DataTable dataTabledt = new DataTable();
                    oleDbDatadt.Fill(dataTabledt);
                    conn.Close();
                    return dataTabledt;
                }
            }
        }
        public DataTable GetGivenData(string givenId)
        {
            using (OleDbConnection conn = new OleDbConnection(this.conn.ConnectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Given WHERE InGiven = ?";
                using (OleDbDataAdapter oleDbDatadt = new OleDbDataAdapter(query, conn))
                {
                    oleDbDatadt.SelectCommand.Parameters.AddWithValue("?", givenId);
                    DataTable dataTable = new DataTable();
                    oleDbDatadt.Fill(dataTable);
                    conn.Close();
                    return dataTable;
                }
            }
        }
        public DataTable GetDailyData(string dailyId)
        {
            using (OleDbConnection conn = new OleDbConnection(this.conn.ConnectionString))
            {
                conn.Open();
                string query = "SELECT D_ID, D_FPAmount, D_SPAmount, D_Data, NotTaken FROM Daily WHERE D_ID = ?";
                using (OleDbDataAdapter oleDbDatadt = new OleDbDataAdapter(query, conn))
                {
                    oleDbDatadt.SelectCommand.Parameters.AddWithValue("?", dailyId);
                    DataTable dataTable = new DataTable();
                    oleDbDatadt.Fill(dataTable);
                    conn.Close();
                    return dataTable;
                }
            }
        }
        public DataTable GetDailyCutData(string dailycutId)
        {
            using (OleDbConnection conn = new OleDbConnection(this.conn.ConnectionString))
            {
                conn.Open();
                string query = "SELECT C_ID, C_Amount FROM DailyCut WHERE C_ID = ?";
                using (OleDbDataAdapter oleDbDatadt = new OleDbDataAdapter(query, conn))
                {
                    oleDbDatadt.SelectCommand.Parameters.AddWithValue("?", dailycutId);
                    DataTable dataTable = new DataTable();
                    oleDbDatadt.Fill(dataTable);
                    conn.Close();
                    return dataTable;
                }
            }
        }
        public DataTable GetInstallmntData(string installmntId)
        {
            using (OleDbConnection conn = new OleDbConnection(this.conn.ConnectionString))
            {
                conn.Open();
                string query = "SELECT I_ID, Take_Anot, Take_Mine FROM Installment WHERE I_ID = ?";
                using (OleDbDataAdapter oleDbDatadt = new OleDbDataAdapter(query, conn))
                {
                    oleDbDatadt.SelectCommand.Parameters.AddWithValue("?", installmntId);
                    DataTable dataTablein = new DataTable();
                    oleDbDatadt.Fill(dataTablein);
                    conn.Close();
                    return dataTablein;
                }
            }
        }
        public DataTable GetIntakeData(string takeId)
        {
            using (OleDbConnection conn = new OleDbConnection(this.conn.ConnectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Teken WHERE InTake = ?";
                using (OleDbDataAdapter oleDbDatadt = new OleDbDataAdapter(query, conn))
                {
                    oleDbDatadt.SelectCommand.Parameters.AddWithValue("?", takeId);
                    DataTable dataTable = new DataTable();
                    oleDbDatadt.Fill(dataTable);
                    conn.Close();
                    return dataTable;
                }
            }
        }
        public DataTable GetExpenseData(string expenseId)
        {
            using (OleDbConnection conn = new OleDbConnection(this.conn.ConnectionString))
            {
                conn.Open();
                string query = "SELECT * FROM TariffAmt WHERE InExpense = ?";
                using (OleDbDataAdapter oleDbDatadt = new OleDbDataAdapter(query, conn))
                {
                    oleDbDatadt.SelectCommand.Parameters.AddWithValue("?", expenseId);
                    DataTable dataTable = new DataTable();
                    oleDbDatadt.Fill(dataTable);
                    conn.Close();
                    return dataTable;
                }
            }
        }
        public DataTable GetSavingData(string savingId)
        {
            using (OleDbConnection conn = new OleDbConnection(this.conn.ConnectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Saving WHERE InSaving = ?";
                using (OleDbDataAdapter oleDbDatadt = new OleDbDataAdapter(query, conn))
                {
                    oleDbDatadt.SelectCommand.Parameters.AddWithValue("?", savingId);
                    DataTable dataTable = new DataTable();
                    oleDbDatadt.Fill(dataTable);
                    conn.Close();
                    return dataTable;
                }
            }
        }
        public DataTable GetUnratedData(string unratedId)
        {
            using (OleDbConnection conn = new OleDbConnection(this.conn.ConnectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Unrated WHERE InUnrated = ?";
                using (OleDbDataAdapter oleDbDatadt = new OleDbDataAdapter(query, conn))
                {
                    oleDbDatadt.SelectCommand.Parameters.AddWithValue("?", unratedId);
                    DataTable dataTable = new DataTable();
                    oleDbDatadt.Fill(dataTable);
                    conn.Close();
                    return dataTable;
                }
            }
        }
        public DataTable GetMarketMemoData(string memoId)
        {
            using (OleDbConnection conn = new OleDbConnection(this.conn.ConnectionString))
            {
                conn.Open();
                string query = "SELECT * FROM MarketMemos WHERE Mem_ID = ?";
                using (OleDbDataAdapter oleDbDatadt = new OleDbDataAdapter(query, conn))
                {
                    oleDbDatadt.SelectCommand.Parameters.AddWithValue("?", memoId);
                    DataTable dataTable = new DataTable();
                    oleDbDatadt.Fill(dataTable);
                    conn.Close();
                    return dataTable;
                }
            }
        }
        public DataTable GetbikeInfoData(string bikeinfoId)
        {
            using (OleDbConnection conn = new OleDbConnection(this.conn.ConnectionString))
            {
                conn.Open();
                string query = "SELECT B_Next_ODO FROM BikeInfo WHERE B_ID = ?";
                using (OleDbDataAdapter oleDbDatadt = new OleDbDataAdapter(query, conn))
                {
                    oleDbDatadt.SelectCommand.Parameters.AddWithValue("?", bikeinfoId);
                    DataTable dataTable = new DataTable();
                    oleDbDatadt.Fill(dataTable);
                    conn.Close();
                    return dataTable;
                }
            }
        }
        public DataTable GetDailyAntData(string dailyAntId)
        {
            using (OleDbConnection conn = new OleDbConnection(this.conn.ConnectionString))
            {
                conn.Open();
                string query = "SELECT DA_ID, DA_FPAmount, DA_SPAmount, DA_Data, NotTaken FROM DailyAnt WHERE DA_ID = ?";
                using (OleDbDataAdapter oleDbDatadt = new OleDbDataAdapter(query, conn))
                {
                    oleDbDatadt.SelectCommand.Parameters.AddWithValue("?", dailyAntId);
                    DataTable dataTable = new DataTable();
                    oleDbDatadt.Fill(dataTable);
                    conn.Close();
                    return dataTable;
                }
            }
        }
        public DataTable GetDailySaviData(string dailySaviId)
        {
            using (OleDbConnection conn = new OleDbConnection(this.conn.ConnectionString))
            {
                conn.Open();
                string query = "SELECT DS_ID, DS_FPAmount, DS_TPAmount, DS_Data, NotTaken FROM DailySaving WHERE DS_ID = ?";
                using (OleDbDataAdapter oleDbDatadt = new OleDbDataAdapter(query, conn))
                {
                    oleDbDatadt.SelectCommand.Parameters.AddWithValue("?", dailySaviId);
                    DataTable dataTable = new DataTable();
                    oleDbDatadt.Fill(dataTable);
                    conn.Close();
                    return dataTable;
                }
            }
        }
        public DataTable GetImageData()
        {
            using (OleDbConnection conn = new OleDbConnection(this.conn.ConnectionString))
            {
                conn.Open();
                string query = "SELECT Img_ID as [ID] FROM Images";
                using (OleDbDataAdapter oleDbDatadt = new OleDbDataAdapter(query, conn))
                {
                    DataTable dataTable = new DataTable();
                    oleDbDatadt.Fill(dataTable);
                    conn.Close();
                    return dataTable;
                }
            }
        }

        //--------------------------All Search Query Events----------------------
        //-----------------------------------------------------------------------
        public DataSet GetGivenDetailData (string givenTo)
        {
            using (OleDbConnection conn = new OleDbConnection(this.conn.ConnectionString))
            {
                conn.Open();
                DataSet takenDataSet = new DataSet();
                string totalQuery = "SELECT SUM(Total_Given) as Total, Given_To FROM Given WHERE Given_To LIKE ? AND GDT_V = 'NDV' GROUP BY Given_To";
                using (OleDbDataAdapter totalAdapter = new OleDbDataAdapter(totalQuery, conn))
                {
                    totalAdapter.SelectCommand.Parameters.AddWithValue("?", "%" + givenTo + "%");
                    totalAdapter.Fill(takenDataSet, "TotalGiven");
                }
                string detailsQuery = "SELECT TOP 500 Given_To as Name, Total_Given as GAmount, Given_Date as GDate, ThroughBy_Given as GUsing, GDT_V_Date as LUpDT, Remarks_Given as Remarks FROM Given WHERE Given_To LIKE ? AND GDT_V = 'NDV' ORDER BY Given_Date DESC";
                using (OleDbDataAdapter detailsAdapter = new OleDbDataAdapter(detailsQuery, conn))
                {
                    detailsAdapter.SelectCommand.Parameters.AddWithValue("?", "%" + givenTo + "%");
                    detailsAdapter.Fill(takenDataSet, "GivenDetails");
                }
                return takenDataSet;
            }
        }
        public DataSet GetTakenDetailData (string takeTo)
        {
            using (OleDbConnection conn = new OleDbConnection(this.conn.ConnectionString))
            {
                conn.Open();
                DataSet takenDataSet = new DataSet();
                string totalQuery = "SELECT SUM(Total_Take) as Total, Take_To FROM Teken WHERE Take_To LIKE ? AND TDT_V = 'NDV' GROUP BY Take_To";
                using (OleDbDataAdapter totalAdapter = new OleDbDataAdapter(totalQuery, conn))
                {
                    totalAdapter.SelectCommand.Parameters.AddWithValue("?", "%" + takeTo + "%");
                    totalAdapter.Fill(takenDataSet, "TotalTaken");
                }
                string detailsQuery = "SELECT TOP 500 Take_To as Name, Total_Take as TAmount, Take_Date as TDate, ThroughBy_Take as TUsing, TDT_V_Date as LUpDT, Remarks_Take as Remarks FROM Teken WHERE Take_To LIKE ? AND TDT_V = 'NDV' ORDER BY Take_Date DESC";
                using (OleDbDataAdapter detailsAdapter = new OleDbDataAdapter(detailsQuery, conn))
                {
                    detailsAdapter.SelectCommand.Parameters.AddWithValue("?", "%" + takeTo + "%");
                    detailsAdapter.Fill(takenDataSet, "TakenDetails");
                }
                return takenDataSet;
            }
        }
        public DataSet GetExpenseDetailData (string expenseTo)
        {
            using (OleDbConnection conn = new OleDbConnection(this.conn.ConnectionString))
            {
                conn.Open();
                DataSet expensesDataSet = new DataSet();
                string totalQuery = "SELECT SUM(Expense_Amount) as Total, Expense_To FROM TariffAmt WHERE Expense_To LIKE ? AND EDT_V = 'NDV' GROUP BY Expense_To";
                using (OleDbDataAdapter totalAdapter = new OleDbDataAdapter(totalQuery, conn))
                {
                    totalAdapter.SelectCommand.Parameters.AddWithValue("?", "%" + expenseTo + "%");
                    totalAdapter.Fill(expensesDataSet, "TotalExpense");
                }
                string detailsQuery = "SELECT TOP 500 Expense_To as Name, Expense_Amount as EAmount, Expense_Date as EDate, ThroughBy_Expense as EUsing, EDT_V_Date as LUpDT, Remarks_Expense as Remarks FROM TariffAmt WHERE Expense_To LIKE ? AND EDT_V = 'NDV' ORDER BY Expense_Date DESC";
                using (OleDbDataAdapter detailsAdapter = new OleDbDataAdapter(detailsQuery, conn))
                {
                    detailsAdapter.SelectCommand.Parameters.AddWithValue("?", "%" + expenseTo + "%");
                    detailsAdapter.Fill(expensesDataSet, "ExpenseDetails");
                }
                return expensesDataSet;
            }
        }
        public DataSet GetSavingsDetailData(string savingTo)
        {
            using (OleDbConnection conn = new OleDbConnection(this.conn.ConnectionString))
            {
                conn.Open();
                DataSet savingsDataSet = new DataSet();
                string totalQuery = "SELECT SUM(Saving_Amount) as Total, Saving_To FROM Saving WHERE Saving_To LIKE ? AND SDT_V = 'NDV' GROUP BY Saving_To";
                using (OleDbDataAdapter totalAdapter = new OleDbDataAdapter(totalQuery, conn))
                {
                    totalAdapter.SelectCommand.Parameters.AddWithValue("?", "%" + savingTo + "%");
                    totalAdapter.Fill(savingsDataSet, "TotalSavings");
                }
                string detailsQuery = "SELECT TOP 500 Saving_To as Name, Saving_Amount as SAmount, Saving_Date as SDate, ThroughBy_Saving as SUsing, SDT_V_Date as LUpDT, Remarks_Saving as Remarks FROM Saving WHERE Saving_To LIKE ? AND SDT_V = 'NDV' ORDER BY Saving_Date DESC";
                using (OleDbDataAdapter detailsAdapter = new OleDbDataAdapter(detailsQuery, conn))
                {
                    detailsAdapter.SelectCommand.Parameters.AddWithValue("?", "%" + savingTo + "%");
                    detailsAdapter.Fill(savingsDataSet, "SavingsDetails");
                }
                return savingsDataSet;
            }
        }
        public DataSet GetUnratedDetailData(string unratedTo)
        {
            using (OleDbConnection conn = new OleDbConnection(this.conn.ConnectionString))
            {
                conn.Open();
                DataSet savingsDataSet = new DataSet();
                string totalQuery = "SELECT SUM(Unrated_Amount) as Total, Unrated_To FROM Unrated WHERE Unrated_To LIKE ? AND UDT_V = 'NDV' GROUP BY Unrated_To";
                using (OleDbDataAdapter totalAdapter = new OleDbDataAdapter(totalQuery, conn))
                {
                    totalAdapter.SelectCommand.Parameters.AddWithValue("?", "%" + unratedTo + "%");
                    totalAdapter.Fill(savingsDataSet, "TotalUnrated");
                }
                string detailsQuery = "SELECT TOP 500 Unrated_To as Name, Unrated_Amount as UAmount, Unrated_Date as UDate, ThroughBy_Unrated as TUsing, UDT_V_Date as LUpDT, Remarks_Unrated as Remarks FROM Unrated WHERE Unrated_To LIKE ? AND UDT_V = 'NDV' ORDER BY Unrated_Date DESC";
                using (OleDbDataAdapter detailsAdapter = new OleDbDataAdapter(detailsQuery, conn))
                {
                    detailsAdapter.SelectCommand.Parameters.AddWithValue("?", "%" + unratedTo + "%");
                    detailsAdapter.Fill(savingsDataSet, "UnratedDetails");
                }
                return savingsDataSet;
            }
        }
    }
}
