using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CT_App.Models;
using MySql.Data.MySqlClient;

namespace CT_App.CT_DLL
{
	public class DLLayer
	{
		#region Comments
		OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\CT_DB.accdb;Jet OLEDB:Database Password=*3455*00;");
		OdbcConnection conne = new OdbcConnection(@"Dsn=RETAILMasterSHOPS;uid=sa;Pwd=Ajwahana$@$;");
		private string connAcc = (@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\CT_DB.accdb;Jet OLEDB:Database Password=*3455*00;");
		private string connSql = (@"Dsn=RETAILMasterSHOPS;uid=sa;pwd=Ajwahana$@$;");
		private string connMySql = (@"Server=127.0.0.1;Database=ct_db;Uid=root;Pwd=Ajwahana$@$;port=3306;Connection Timeout=30;");
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
			string query = $"SELECT SUM(NotTaken) FROM Daily WHERE [D_Data] = 'NTKN'";
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
			string query = $"SELECT SUM(NotTaken) FROM DailyAnt WHERE [DA_Data] = 'NTKN'";
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
			string query = $"SELECT SUM(NotTaken) FROM DailySaving WHERE [DS_Data] = 'NTKN'";
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
			string query = $"INSERT INTO MarketMemos(Mem_ID, Mem_Date, R_InvTK, C_InvTK, Giv_TK, Ret_TK, I_N01, I_N02, I_N03, I_N04, I_N05, I_N06, I_N07, I_N08, I_N09, I_N10, I_N11, I_N12, I_N13, I_N14, I_N15, I_N16, I_P01, I_P02, I_P03, I_P04, I_P05, I_P06, I_P07, I_P08, I_P09, I_P10, I_P11, I_P12, I_P13, I_P14, I_P15, I_P16, I_Q01, I_Q02, I_Q03, I_Q04, I_Q05, I_Q06, I_Q07, I_Q08, I_Q09, I_Q10, I_Q11, I_Q12, I_Q13, I_Q14, I_Q15, I_Q16, I_ST01, I_ST02, I_ST03, I_ST04, I_ST05, I_ST06, I_ST07, I_ST08, I_ST09, I_ST10, I_ST11, I_ST12, I_ST13, I_ST14, I_ST15, I_ST16, R_Inv01, R_Inv02, R_Inv03, R_Inv04, R_Inv05, R_Inv06, R_Inv07, R_Inv08, R_Inv09, R_Inv10, R_Inv11, R_Inv12, R_Inv13, R_Inv14, R_Inv15, R_Inv16, R_Inv17, R_Inv18, R_Inv19, R_Inv20, R_Inv21, R_Inv22, R_Inv23, R_Inv24, Mem_Insrt_Person) " +
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
			string query = $"INSERT INTO Saving (InSaving, Saving_Amount, Saving_To, ThroughBy_Saving, Saving_Date, Remarks_Saving, SDT_V, Saving_Bank, S_Insrt_Person) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)";
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
			string query = $"INSERT INTO Unrated (InUnrated, Unrated_Amount, Unrated_To, ThroughBy_Unrated, Unrated_Date, Remarks_Unrated, UDT_V, U_Insrt_Person) VALUES (?, ?, ?, ?, ?, ?, ?, ?)";
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
			string query2 = $"INSERT INTO GivenUpdt (InGiven, Was_Given, Now_Given, Total_Given, Given_To, GDT_V_Date) VALUES (?, ?, ?, ?, ?, ?)";
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
			string query2 = $"INSERT INTO TekenUpdt (InTake ,Was_Take ,Now_Take ,Total_Take ,Take_To ,TDT_V_Date) VALUES (?, ?, ?, ?, ?, ?)";
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
			string query2 = $"INSERT INTO TariffAmtUpdt (InExpense ,Was_Expense ,Now_Expense ,Expense_Amount ,Expense_To ,EDT_V_Date) VALUES (?, ?, ?, ?, ?, ?)";
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
			string query2 = $"INSERT INTO SavingUpdt (InSaving, Was_Saving, Now_Saving, Saving_Amount, Saving_To, SDT_V_Date) VALUES (?, ?, ?, ?, ?, ?)";
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
			string query2 = $"INSERT INTO UnratedUpdt (InUnrated, Was_Unrated, Now_Unrated, Unrated_Amount, Unrated_To, UDT_V_Date) VALUES (?, ?, ?, ?, ?, ?)";
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
        public bool updtNTDaily(Daily daily)
        {
            string query = $"UPDATE Daily SET NotTaken = ? WHERE D_ID = ?";
            using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
            {
                cmd.Parameters.AddWithValue("?", daily.NotTaken);
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

		//---------------------------------- Monthly ------------------------------
		//-------------------------------------------------------------------------
		public List<DataTable> GetMonthlyData()
		{
			string[] queries = {
				$"SELECT MT_ID as [ID],MT_Date as [Date],MT_TotalTK as [TotalTK],MT_Giv_TK as [GivenTK],MT_LS_TK as [G/T] FROM MonthlyTaken ORDER by MT_Date DESC"
				//$"SELECT MT_ID,MT_Date,MT_TotalTK,MT_Giv_TK,MT_LS_TK,T01,T02,T03,T04,T05,T06,T07,T08,T09,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26,T27,T28,T29,T30,T31,T32,T33,T34,T35,T36,T37,T38,T39,T40,T41,T42,T43,T44,T45,T46,T47,T48,T49,T50,T51,T52,T53,T54,T55,T56,T57,T58,T59,T60,T61,T62,T63,T64,T65,T66,T67,T68,T69,T70,T71,T72,T73,T74,T75,T76,T77,T78,T79,T80,T81,T82,T83,T84,T85,T86,T87,T88,T89,T90,T91,T92,T93,T94,T95,T96,T97,T98,T99,T100,T101,T102,T103,T104,T105,T106,T107,T108,T109,T110,T111,T112,T113,T114,T115,T116,T117,T118,T119,T120,MTDT_V,MT_Insrt_Person,MT_Updt_Person,MT_Del_Person FROM MonthlyTaken"
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
		public bool insrtMonthlyTake(MonthlyTake monthlyTake)
		{
			string query = $"INSERT INTO MonthlyTaken(MT_ID,MT_Date,MT_TotalTK,MT_Giv_TK,MT_LS_TK,T01,T02,T03,T04,T05,T06,T07,T08,T09,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26,T27,T28,T29,T30,T31,T32,T33,T34,T35,T36,T37,T38,T39,T40,T41,T42,T43,T44,T45,T46,T47,T48,T49,T50,T51,T52,T53,T54,T55,T56,T57,T58,T59,T60,T61,T62,T63,T64,T65,T66,T67,T68,T69,T70,T71,T72,T73,T74,T75,T76,T77,T78,T79,T80,T81,T82,T83,T84,T85,T86,T87,T88,T89,T90,T91,T92,T93,T94,T95,T96,T97,T98,T99,T100,MTDT_V,MT_Insrt_Person) " +
							"VALUES (@MT_ID,@MT_Date,@MT_TotalTK,@MT_Giv_TK,@MT_LS_TK,@T01,@T02,@T03,@T04,@T05,@T06,@T07,@T08,@T09,@T10,@T11,@T12,@T13,@T14,@T15,@T16,@T17,@T18,@T19,@T20,@T21,@T22,@T23,@T24,@T25,@T26,@T27,@T28,@T29,@T30,@T31,@T32,@T33,@T34,@T35,@T36,@T37,@T38,@T39,@T40,@T41,@T42,@T43,@T44,@T45,@T46,@T47,@T48,@T49,@T50,@T51,@T52,@T53,@T54,@T55,@T56,@T57,@T58,@T59,@T60,@T61,@T62,@T63,@T64,@T65,@T66,@T67,@T68,@T69,@T70,@T71,@T72,@T73,@T74,@T75,@T76,@T77,@T78,@T79,@T80,@T81,@T82,@T83,@T84,@T85,@T86,@T87,@T88,@T89,@T90,@T91,@T92,@T93,@T94,@T95,@T96,@T97,@T98,@T99,@T100,@MTDT_V,@MT_Insrt_Person)";
			using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
			{
				cmd.Parameters.AddWithValue("@MT_ID", monthlyTake.MT_ID);
				cmd.Parameters.AddWithValue("@MT_Date", monthlyTake.MT_Date);
				cmd.Parameters.AddWithValue("@MT_TotalTK", monthlyTake.MT_TotalTK);
				cmd.Parameters.AddWithValue("@MT_Giv_TK", monthlyTake.MT_Giv_TK);
				cmd.Parameters.AddWithValue("@MT_LS_TK", monthlyTake.MT_LS_TK);
				cmd.Parameters.AddWithValue("@T01", monthlyTake.T01);
				cmd.Parameters.AddWithValue("@T02", monthlyTake.T02);
				cmd.Parameters.AddWithValue("@T03", monthlyTake.T03);
				cmd.Parameters.AddWithValue("@T04", monthlyTake.T04);
				cmd.Parameters.AddWithValue("@T05", monthlyTake.T05);
				cmd.Parameters.AddWithValue("@T06", monthlyTake.T06);
				cmd.Parameters.AddWithValue("@T07", monthlyTake.T07);
				cmd.Parameters.AddWithValue("@T08", monthlyTake.T08);
				cmd.Parameters.AddWithValue("@T09", monthlyTake.T09);
				cmd.Parameters.AddWithValue("@T10", monthlyTake.T10);
				cmd.Parameters.AddWithValue("@T11", monthlyTake.T11);
				cmd.Parameters.AddWithValue("@T12", monthlyTake.T12);
				cmd.Parameters.AddWithValue("@T13", monthlyTake.T13);
				cmd.Parameters.AddWithValue("@T14", monthlyTake.T14);
				cmd.Parameters.AddWithValue("@T15", monthlyTake.T15);
				cmd.Parameters.AddWithValue("@T16", monthlyTake.T16);
				cmd.Parameters.AddWithValue("@T17", monthlyTake.T17);
				cmd.Parameters.AddWithValue("@T18", monthlyTake.T18);
				cmd.Parameters.AddWithValue("@T19", monthlyTake.T19);
				cmd.Parameters.AddWithValue("@T20", monthlyTake.T20);
				cmd.Parameters.AddWithValue("@T21", monthlyTake.T21);
				cmd.Parameters.AddWithValue("@T22", monthlyTake.T22);
				cmd.Parameters.AddWithValue("@T23", monthlyTake.T23);
				cmd.Parameters.AddWithValue("@T24", monthlyTake.T24);
				cmd.Parameters.AddWithValue("@T25", monthlyTake.T25);
				cmd.Parameters.AddWithValue("@T26", monthlyTake.T26);
				cmd.Parameters.AddWithValue("@T27", monthlyTake.T27);
				cmd.Parameters.AddWithValue("@T28", monthlyTake.T28);
				cmd.Parameters.AddWithValue("@T29", monthlyTake.T29);
				cmd.Parameters.AddWithValue("@T30", monthlyTake.T30);
				cmd.Parameters.AddWithValue("@T31", monthlyTake.T31);
				cmd.Parameters.AddWithValue("@T32", monthlyTake.T32);
				cmd.Parameters.AddWithValue("@T33", monthlyTake.T33);
				cmd.Parameters.AddWithValue("@T34", monthlyTake.T34);
				cmd.Parameters.AddWithValue("@T35", monthlyTake.T35);
				cmd.Parameters.AddWithValue("@T36", monthlyTake.T36);
				cmd.Parameters.AddWithValue("@T37", monthlyTake.T37);
				cmd.Parameters.AddWithValue("@T38", monthlyTake.T38);
				cmd.Parameters.AddWithValue("@T39", monthlyTake.T39);
				cmd.Parameters.AddWithValue("@T40", monthlyTake.T40);
				cmd.Parameters.AddWithValue("@T41", monthlyTake.T41);
				cmd.Parameters.AddWithValue("@T42", monthlyTake.T42);
				cmd.Parameters.AddWithValue("@T43", monthlyTake.T43);
				cmd.Parameters.AddWithValue("@T44", monthlyTake.T44);
				cmd.Parameters.AddWithValue("@T45", monthlyTake.T45);
				cmd.Parameters.AddWithValue("@T46", monthlyTake.T46);
				cmd.Parameters.AddWithValue("@T47", monthlyTake.T47);
				cmd.Parameters.AddWithValue("@T48", monthlyTake.T48);
				cmd.Parameters.AddWithValue("@T49", monthlyTake.T49);
				cmd.Parameters.AddWithValue("@T50", monthlyTake.T50);
				cmd.Parameters.AddWithValue("@T51", monthlyTake.T51);
				cmd.Parameters.AddWithValue("@T52", monthlyTake.T52);
				cmd.Parameters.AddWithValue("@T53", monthlyTake.T53);
				cmd.Parameters.AddWithValue("@T54", monthlyTake.T54);
				cmd.Parameters.AddWithValue("@T55", monthlyTake.T55);
				cmd.Parameters.AddWithValue("@T56", monthlyTake.T56);
				cmd.Parameters.AddWithValue("@T57", monthlyTake.T57);
				cmd.Parameters.AddWithValue("@T58", monthlyTake.T58);
				cmd.Parameters.AddWithValue("@T59", monthlyTake.T59);
				cmd.Parameters.AddWithValue("@T60", monthlyTake.T60);
				cmd.Parameters.AddWithValue("@T61", monthlyTake.T61);
				cmd.Parameters.AddWithValue("@T62", monthlyTake.T62);
				cmd.Parameters.AddWithValue("@T63", monthlyTake.T63);
				cmd.Parameters.AddWithValue("@T64", monthlyTake.T64);
				cmd.Parameters.AddWithValue("@T65", monthlyTake.T65);
				cmd.Parameters.AddWithValue("@T66", monthlyTake.T66);
				cmd.Parameters.AddWithValue("@T67", monthlyTake.T67);
				cmd.Parameters.AddWithValue("@T68", monthlyTake.T68);
				cmd.Parameters.AddWithValue("@T69", monthlyTake.T69);
				cmd.Parameters.AddWithValue("@T70", monthlyTake.T70);
				cmd.Parameters.AddWithValue("@T71", monthlyTake.T71);
				cmd.Parameters.AddWithValue("@T72", monthlyTake.T72);
				cmd.Parameters.AddWithValue("@T73", monthlyTake.T73);
				cmd.Parameters.AddWithValue("@T74", monthlyTake.T74);
				cmd.Parameters.AddWithValue("@T75", monthlyTake.T75);
				cmd.Parameters.AddWithValue("@T76", monthlyTake.T76);
				cmd.Parameters.AddWithValue("@T77", monthlyTake.T77);
				cmd.Parameters.AddWithValue("@T78", monthlyTake.T78);
				cmd.Parameters.AddWithValue("@T79", monthlyTake.T79);
				cmd.Parameters.AddWithValue("@T80", monthlyTake.T80);
				cmd.Parameters.AddWithValue("@T81", monthlyTake.T81);
				cmd.Parameters.AddWithValue("@T82", monthlyTake.T82);
				cmd.Parameters.AddWithValue("@T83", monthlyTake.T83);
				cmd.Parameters.AddWithValue("@T84", monthlyTake.T84);
				cmd.Parameters.AddWithValue("@T85", monthlyTake.T85);
				cmd.Parameters.AddWithValue("@T86", monthlyTake.T86);
				cmd.Parameters.AddWithValue("@T87", monthlyTake.T87);
				cmd.Parameters.AddWithValue("@T88", monthlyTake.T88);
				cmd.Parameters.AddWithValue("@T89", monthlyTake.T89);
				cmd.Parameters.AddWithValue("@T90", monthlyTake.T90);
				cmd.Parameters.AddWithValue("@T91", monthlyTake.T91);
				cmd.Parameters.AddWithValue("@T92", monthlyTake.T92);
				cmd.Parameters.AddWithValue("@T93", monthlyTake.T93);
				cmd.Parameters.AddWithValue("@T94", monthlyTake.T94);
				cmd.Parameters.AddWithValue("@T95", monthlyTake.T95);
				cmd.Parameters.AddWithValue("@T96", monthlyTake.T96);
				cmd.Parameters.AddWithValue("@T97", monthlyTake.T97);
				cmd.Parameters.AddWithValue("@T98", monthlyTake.T98);
				cmd.Parameters.AddWithValue("@T99", monthlyTake.T99);
				cmd.Parameters.AddWithValue("@T100", monthlyTake.T100);
				cmd.Parameters.AddWithValue("@MTDT_V", monthlyTake.MTDT_V);
				cmd.Parameters.AddWithValue("@MT_Insrt_Person", monthlyTake.MT_Insrt_Person);
				this.conn.Open();
				int rowsAffected = cmd.ExecuteNonQuery();
				this.conn.Close();
				return rowsAffected > 0;
			}
		}
		public bool updtMonthlyTake(MonthlyTake monthlyTake)
		{
			string query = $"UPDATE MonthlyTaken SET MT_TotalTK = ?,MT_Giv_TK = ?,MT_LS_TK = ?,T01 = ?,T02 = ?,T03 = ?,T04 = ?,T05 = ?,T06 = ?,T07 = ?,T08 = ?,T09 = ?,T10 = ?,T11 = ?,T12 = ?,T13 = ?,T14 = ?,T15 = ?,T16 = ?,T17 = ?,T18 = ?,T19 = ?,T20 = ?,T21 = ?,T22 = ?,T23 = ?,T24 = ?,T25 = ?,T26 = ?,T27 = ?,T28 = ?,T29 = ?,T30 = ?,T31 = ?,T32 = ?,T33 = ?,T34 = ?,T35 = ?,T36 = ?,T37 = ?,T38 = ?,T39 = ?,T40 = ?,T41 = ?,T42 = ?,T43 = ?,T44 = ?,T45 = ?,T46 = ?,T47 = ?,T48 = ?,T49 = ?,T50 = ?,T51 = ?,T52 = ?,T53 = ?,T54 = ?,T55 = ?,T56 = ?,T57 = ?,T58 = ?,T59 = ?,T60 = ?,T61 = ?,T62 = ?,T63 = ?,T64 = ?,T65 = ?,T66 = ?,T67 = ?,T68 = ?,T69 = ?,T70 = ?,T71 = ?,T72 = ?,T73 = ?,T74 = ?,T75 = ?,T76 = ?,T77 = ?,T78 = ?,T79 = ?,T80 = ?,T81 = ?,T82 = ?,T83 = ?,T84 = ?,T85 = ?,T86 = ?,T87 = ?,T88 = ?,T89 = ?,T90 = ?,T91 = ?,T92 = ?,T93 = ?,T94 = ?,T95 = ?,T96 = ?,T97 = ?,T98 = ?,T99 = ?,T100 = ?,MTDT_V = ?,MT_Updt_Person = ? WHERE MT_ID = ?";
			using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
			{;
				cmd.Parameters.AddWithValue("@MT_TotalTK", monthlyTake.MT_TotalTK);
				cmd.Parameters.AddWithValue("@MT_Giv_TK", monthlyTake.MT_Giv_TK);
				cmd.Parameters.AddWithValue("@MT_LS_TK", monthlyTake.MT_LS_TK);
				cmd.Parameters.AddWithValue("?", monthlyTake.T01);
				cmd.Parameters.AddWithValue("?", monthlyTake.T02);
				cmd.Parameters.AddWithValue("?", monthlyTake.T03);
				cmd.Parameters.AddWithValue("?", monthlyTake.T04);
				cmd.Parameters.AddWithValue("?", monthlyTake.T05);
				cmd.Parameters.AddWithValue("?", monthlyTake.T06);
				cmd.Parameters.AddWithValue("?", monthlyTake.T07);
				cmd.Parameters.AddWithValue("?", monthlyTake.T08);
				cmd.Parameters.AddWithValue("?", monthlyTake.T09);
				cmd.Parameters.AddWithValue("?", monthlyTake.T10);
				cmd.Parameters.AddWithValue("?", monthlyTake.T11);
				cmd.Parameters.AddWithValue("?", monthlyTake.T12);
				cmd.Parameters.AddWithValue("?", monthlyTake.T13);
				cmd.Parameters.AddWithValue("?", monthlyTake.T14);
				cmd.Parameters.AddWithValue("?", monthlyTake.T15);
				cmd.Parameters.AddWithValue("?", monthlyTake.T16);
				cmd.Parameters.AddWithValue("?", monthlyTake.T17);
				cmd.Parameters.AddWithValue("?", monthlyTake.T18);
				cmd.Parameters.AddWithValue("?", monthlyTake.T19);
				cmd.Parameters.AddWithValue("?", monthlyTake.T20);
				cmd.Parameters.AddWithValue("?", monthlyTake.T21);
				cmd.Parameters.AddWithValue("?", monthlyTake.T22);
				cmd.Parameters.AddWithValue("?", monthlyTake.T23);
				cmd.Parameters.AddWithValue("?", monthlyTake.T24);
				cmd.Parameters.AddWithValue("?", monthlyTake.T25);
				cmd.Parameters.AddWithValue("?", monthlyTake.T26);
				cmd.Parameters.AddWithValue("?", monthlyTake.T27);
				cmd.Parameters.AddWithValue("?", monthlyTake.T28);
				cmd.Parameters.AddWithValue("?", monthlyTake.T29);
				cmd.Parameters.AddWithValue("?", monthlyTake.T30);
				cmd.Parameters.AddWithValue("?", monthlyTake.T31);
				cmd.Parameters.AddWithValue("?", monthlyTake.T32);
				cmd.Parameters.AddWithValue("?", monthlyTake.T33);
				cmd.Parameters.AddWithValue("?", monthlyTake.T34);
				cmd.Parameters.AddWithValue("?", monthlyTake.T35);
				cmd.Parameters.AddWithValue("?", monthlyTake.T36);
				cmd.Parameters.AddWithValue("?", monthlyTake.T37);
				cmd.Parameters.AddWithValue("?", monthlyTake.T38);
				cmd.Parameters.AddWithValue("?", monthlyTake.T39);
				cmd.Parameters.AddWithValue("?", monthlyTake.T40);
				cmd.Parameters.AddWithValue("?", monthlyTake.T41);
				cmd.Parameters.AddWithValue("?", monthlyTake.T42);
				cmd.Parameters.AddWithValue("?", monthlyTake.T43);
				cmd.Parameters.AddWithValue("?", monthlyTake.T44);
				cmd.Parameters.AddWithValue("?", monthlyTake.T45);
				cmd.Parameters.AddWithValue("?", monthlyTake.T46);
				cmd.Parameters.AddWithValue("?", monthlyTake.T47);
				cmd.Parameters.AddWithValue("?", monthlyTake.T48);
				cmd.Parameters.AddWithValue("?", monthlyTake.T49);
				cmd.Parameters.AddWithValue("?", monthlyTake.T50);
				cmd.Parameters.AddWithValue("?", monthlyTake.T51);
				cmd.Parameters.AddWithValue("?", monthlyTake.T52);
				cmd.Parameters.AddWithValue("?", monthlyTake.T53);
				cmd.Parameters.AddWithValue("?", monthlyTake.T54);
				cmd.Parameters.AddWithValue("?", monthlyTake.T55);
				cmd.Parameters.AddWithValue("?", monthlyTake.T56);
				cmd.Parameters.AddWithValue("?", monthlyTake.T57);
				cmd.Parameters.AddWithValue("?", monthlyTake.T58);
				cmd.Parameters.AddWithValue("?", monthlyTake.T59);
				cmd.Parameters.AddWithValue("?", monthlyTake.T60);
				cmd.Parameters.AddWithValue("?", monthlyTake.T61);
				cmd.Parameters.AddWithValue("?", monthlyTake.T62);
				cmd.Parameters.AddWithValue("?", monthlyTake.T63);
				cmd.Parameters.AddWithValue("?", monthlyTake.T64);
				cmd.Parameters.AddWithValue("?", monthlyTake.T65);
				cmd.Parameters.AddWithValue("?", monthlyTake.T66);
				cmd.Parameters.AddWithValue("?", monthlyTake.T67);
				cmd.Parameters.AddWithValue("?", monthlyTake.T68);
				cmd.Parameters.AddWithValue("?", monthlyTake.T69);
				cmd.Parameters.AddWithValue("?", monthlyTake.T70);
				cmd.Parameters.AddWithValue("?", monthlyTake.T71);
				cmd.Parameters.AddWithValue("?", monthlyTake.T72);
				cmd.Parameters.AddWithValue("?", monthlyTake.T73);
				cmd.Parameters.AddWithValue("?", monthlyTake.T74);
				cmd.Parameters.AddWithValue("?", monthlyTake.T75);
				cmd.Parameters.AddWithValue("?", monthlyTake.T76);
				cmd.Parameters.AddWithValue("?", monthlyTake.T77);
				cmd.Parameters.AddWithValue("?", monthlyTake.T78);
				cmd.Parameters.AddWithValue("?", monthlyTake.T79);
				cmd.Parameters.AddWithValue("?", monthlyTake.T80);
				cmd.Parameters.AddWithValue("?", monthlyTake.T81);
				cmd.Parameters.AddWithValue("?", monthlyTake.T82);
				cmd.Parameters.AddWithValue("?", monthlyTake.T83);
				cmd.Parameters.AddWithValue("?", monthlyTake.T84);
				cmd.Parameters.AddWithValue("?", monthlyTake.T85);
				cmd.Parameters.AddWithValue("?", monthlyTake.T86);
				cmd.Parameters.AddWithValue("?", monthlyTake.T87);
				cmd.Parameters.AddWithValue("?", monthlyTake.T88);
				cmd.Parameters.AddWithValue("?", monthlyTake.T89);
				cmd.Parameters.AddWithValue("?", monthlyTake.T90);
				cmd.Parameters.AddWithValue("?", monthlyTake.T91);
				cmd.Parameters.AddWithValue("?", monthlyTake.T92);
				cmd.Parameters.AddWithValue("?", monthlyTake.T93);
				cmd.Parameters.AddWithValue("?", monthlyTake.T94);
				cmd.Parameters.AddWithValue("?", monthlyTake.T95);
				cmd.Parameters.AddWithValue("?", monthlyTake.T96);
				cmd.Parameters.AddWithValue("?", monthlyTake.T97);
				cmd.Parameters.AddWithValue("?", monthlyTake.T98);
				cmd.Parameters.AddWithValue("?", monthlyTake.T99);
				cmd.Parameters.AddWithValue("?", monthlyTake.T100);
				cmd.Parameters.AddWithValue("?", monthlyTake.MTDT_V);
				cmd.Parameters.AddWithValue("?", monthlyTake.MT_Updt_Person);
				cmd.Parameters.AddWithValue("?", monthlyTake.MT_ID);
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
					//this.imagesSync();
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
					this.sp_marketSync();
					this.sp_marketMemosSync();
					this.sp_marketMemosDelSync();
					//this.sp_imagesSync();
					this.sp_dailySavingSync();
					this.sp_installmentSync();
					this.sp_installmentPaySync();
					this.sp_bikeInfoSync();
					this.sp_givenSync();
					this.sp_tekenSync();
					this.sp_expenseSync();
					this.sp_savingSync();
					this.sp_unratedSync();
					this.sp_givenUpdtSync();
					this.sp_tekenUpdtSync();
					this.sp_expenseUpdtSync();
					this.sp_savingUpdtSync();
					this.sp_unratedUpdtSync();
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
					this.sp_marketSync();
					this.sp_marketMemosSync();
					this.sp_marketMemosDelSync();
					this.sp_imagesSync();
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
					this.sp_dailySavingSync();
					this.sp_installmentSync();
					this.sp_installmentPaySync();
					this.sp_bikeInfoSync();
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
					this.sp_givenSync();
					this.sp_tekenSync();
					this.sp_expenseSync();
					this.sp_savingSync();
					this.sp_unratedSync();
					this.sp_givenUpdtSync();
					this.sp_tekenUpdtSync();
					this.sp_expenseUpdtSync();
					this.sp_savingUpdtSync();
					this.sp_unratedUpdtSync();
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
					this.sp_dailySync();
					this.sp_dailyAntSync();
					this.sp_dailyCutSync();
					accConn.Close();
				}
				sqlConn.Close();
			}
		}
		public void SyncMonthlyData()
		{
			using (OdbcConnection sqlConn = new OdbcConnection(connSql))
			{
				sqlConn.Open();
				using (OleDbConnection accConn = new OleDbConnection(connAcc))
				{
					accConn.Open();
					this.monthlySync();
					this.sp_monthlySync();
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
			string insCom = "IF NOT EXISTS (SELECT * FROM Images WHERE Img_ID = ?) " +
								"BEGIN " +
									$"INSERT INTO Images (Img_ID,ImageData) VALUES (?,?)" +
								"END";
			using (OleDbConnection accConn = new OleDbConnection(connAcc))
			{
				accConn.Open();
				string selCom = "SELECT * FROM Images";
				OleDbCommand command = new OleDbCommand(selCom, accConn);
				OleDbDataReader reader = command.ExecuteReader();
				using (OdbcConnection sqlConn = new OdbcConnection(connSql))
				{
					sqlConn.Open();
					string checkTableQuery = "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Images') " +
											 "BEGIN " +
												$"CREATE TABLE Images (Img_ID nvarchar(250) NULL,ImageData nvarchar(250) NULL) " +
											 "END";
					using (OdbcCommand checkTableCommand = new OdbcCommand(checkTableQuery, sqlConn))
					{
						checkTableCommand.ExecuteNonQuery();
					}
					while (reader.Read())
					{
						using (OdbcCommand sqlInsComm = new OdbcCommand(insCom, sqlConn))
						{
							sqlInsComm.Parameters.AddWithValue("?", reader["Img_ID"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["Img_ID"]); // For IF NOT EXISTS
							sqlInsComm.Parameters.AddWithValue("?", reader["ImageData"]);
							sqlInsComm.ExecuteNonQuery();
						}
					}
					sqlConn.Close();
				}
				accConn.Close();
			}
		}
		private void monthlySync()
		{
			string insCom = "IF NOT EXISTS (SELECT * FROM MonthlyTaken WHERE MT_ID = ?) " +
								"BEGIN " +
									$"INSERT INTO MonthlyTaken (MT_ID,MT_Date,MT_TotalTK,MT_Giv_TK,MT_LS_TK,T01,T02,T03,T04,T05,T06,T07,T08,T09,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26,T27,T28,T29,T30,T31,T32,T33,T34,T35,T36,T37,T38,T39,T40,T41,T42,T43,T44,T45,T46,T47,T48,T49,T50,T51,T52,T53,T54,T55,T56,T57,T58,T59,T60,T61,T62,T63,T64,T65,T66,T67,T68,T69,T70,T71,T72,T73,T74,T75,T76,T77,T78,T79,T80,T81,T82,T83,T84,T85,T86,T87,T88,T89,T90,T91,T92,T93,T94,T95,T96,T97,T98,T99,T100,T101,T102,T103,T104,T105,T106,T107,T108,T109,T110,T111,T112,T113,T114,T115,T116,T117,T118,T119,T120,MTDT_V,MT_Insrt_Person,MT_Updt_Person,MT_Del_Person) " +
									$"VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)" +
								"END";
			using (OleDbConnection accConn = new OleDbConnection(connAcc))
			{
				accConn.Open();
				string selCom = "SELECT * FROM MonthlyTaken";
				OleDbCommand command = new OleDbCommand(selCom, accConn);
				OleDbDataReader reader = command.ExecuteReader();
				using (OdbcConnection sqlConn = new OdbcConnection(connSql))
				{
					sqlConn.Open();
					string checkTableQuery = "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'MonthlyTaken') " +
											 "BEGIN " +
												$"CREATE TABLE MonthlyTaken (MT_ID nvarchar(250) NULL,MT_Date datetime NULL,MT_TotalTK float NULL,MT_Giv_TK float NULL,MT_LS_TK float NULL,T01 float NULL,T02 float NULL,T03 float NULL,T04 float NULL,T05 float NULL,T06 float NULL,T07 float NULL,T08 float NULL,T09 float NULL,T10 float NULL,T11 float NULL,T12 float NULL,T13 float NULL,T14 float NULL,T15 float NULL,T16 float NULL,T17 float NULL,T18 float NULL,T19 float NULL,T20 float NULL,T21 float NULL,T22 float NULL,T23 float NULL,T24 float NULL,T25 float NULL,T26 float NULL,T27 float NULL,T28 float NULL,T29 float NULL,T30 float NULL,T31 float NULL,T32 float NULL,T33 float NULL,T34 float NULL,T35 float NULL,T36 float NULL,T37 float NULL,T38 float NULL,T39 float NULL,T40 float NULL,T41 float NULL,T42 float NULL,T43 float NULL,T44 float NULL,T45 float NULL,T46 float NULL,T47 float NULL,T48 float NULL,T49 float NULL,T50 float NULL,T51 float NULL,T52 float NULL,T53 float NULL,T54 float NULL,T55 float NULL,T56 float NULL,T57 float NULL,T58 float NULL,T59 float NULL,T60 float NULL,T61 float NULL,T62 float NULL,T63 float NULL,T64 float NULL,T65 float NULL,T66 float NULL,T67 float NULL,T68 float NULL,T69 float NULL,T70 float NULL,T71 float NULL,T72 float NULL,T73 float NULL,T74 float NULL,T75 float NULL,T76 float NULL,T77 float NULL,T78 float NULL,T79 float NULL,T80 float NULL,T81 float NULL,T82 float NULL,T83 float NULL,T84 float NULL,T85 float NULL,T86 float NULL,T87 float NULL,T88 float NULL,T89 float NULL,T90 float NULL,T91 float NULL,T92 float NULL,T93 float NULL,T94 float NULL,T95 float NULL,T96 float NULL,T97 float NULL,T98 float NULL,T99 float NULL,T100 float NULL,T101 float NULL,T102 float NULL,T103 float NULL,T104 float NULL,T105 float NULL,T106 float NULL,T107 float NULL,T108 float NULL,T109 float NULL,T110 float NULL,T111 float NULL,T112 float NULL,T113 float NULL,T114 float NULL,T115 float NULL,T116 float NULL,T117 float NULL,T118 float NULL,T119 float NULL,T120 float NULL,MTDT_V nvarchar(250) NULL,MT_Insrt_Person nvarchar(250) NULL,MT_Updt_Person nvarchar(250) NULL,MT_Del_Person nvarchar(250) NULL) " +
											 "END";
					using (OdbcCommand checkTableCommand = new OdbcCommand(checkTableQuery, sqlConn))
					{
						checkTableCommand.ExecuteNonQuery();
					}
					while (reader.Read())
					{
						using (OdbcCommand sqlInsComm = new OdbcCommand(insCom, sqlConn))
						{
							sqlInsComm.Parameters.AddWithValue("?", reader["MT_ID"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["MT_Date"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["MT_TotalTK"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["MT_Giv_TK"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["MT_LS_TK"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T01"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T02"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T03"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T04"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T05"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T06"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T07"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T08"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T09"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T10"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T11"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T12"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T13"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T14"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T15"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T16"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T17"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T18"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T19"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T20"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T21"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T22"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T23"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T24"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T25"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T26"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T27"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T28"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T29"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T30"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T31"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T32"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T33"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T34"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T35"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T36"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T37"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T38"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T39"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T40"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T41"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T42"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T43"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T44"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T45"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T46"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T47"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T48"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T49"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T50"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T51"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T52"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T53"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T54"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T55"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T56"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T57"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T58"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T59"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T60"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T61"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T62"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T63"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T64"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T65"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T66"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T67"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T68"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T69"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T70"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T71"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T72"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T73"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T74"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T75"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T76"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T77"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T78"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T79"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T80"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T81"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T82"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T83"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T84"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T85"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T86"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T87"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T88"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T89"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T90"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T91"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T92"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T93"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T94"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T95"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T96"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T97"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T98"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T99"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T100"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T101"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T102"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T103"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T104"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T105"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T106"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T107"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T108"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T109"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T110"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T111"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T112"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T113"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T114"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T115"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T116"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T117"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T118"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T119"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["T120"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["MTDT_V"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["MT_Insrt_Person"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["MT_Updt_Person"]);
							sqlInsComm.Parameters.AddWithValue("?", reader["MT_Del_Person"]);
							sqlInsComm.ExecuteNonQuery();
						}
					}
					sqlConn.Close();
				}
				accConn.Close();
			}
		}

		//----------------Access to MySQL Data Insert Event Work-----------------
		//-----------------------------------------------------------------------
		private void sp_marketSync()
		{
			using (OleDbConnection accConn = new OleDbConnection(connAcc))
			{
				accConn.Open();
				string selCom = "SELECT * FROM Market";
				OleDbCommand command = new OleDbCommand(selCom, accConn);
				OleDbDataReader reader = command.ExecuteReader();
				using (MySqlConnection mysqlConn = new MySqlConnection(connMySql))
				{
					mysqlConn.Open();
					string crtProc = @"CREATE PROCEDURE IF NOT EXISTS sp_marketSync ( IN p_M_ID VARCHAR(250), IN p_M_Date DATE, IN p_M_Amount FLOAT, IN p_M_Insrt_Person VARCHAR(250), IN p_M_Updt_Person VARCHAR(250), IN p_M_Del_Person VARCHAR(250) )
										BEGIN
											IF NOT EXISTS (
													SELECT * FROM information_schema.tables WHERE table_name = 'Market'
												) THEN
													CREATE TABLE Market ( M_ID NVARCHAR(250) NULL, M_Date DATE NULL, M_Amount FLOAT DEFAULT 0, M_Insrt_Person NVARCHAR(250) NULL, M_Updt_Person NVARCHAR(250) NULL, M_Del_Person NVARCHAR(250) NULL );
												END IF;
												IF NOT EXISTS (
													SELECT * FROM Market WHERE M_ID = p_M_ID
												) THEN         
													INSERT INTO Market ( M_ID, M_Date, M_Amount, M_Insrt_Person, M_Updt_Person, M_Del_Person ) 
																VALUES ( p_M_ID, p_M_Date, p_M_Amount, p_M_Insrt_Person, p_M_Updt_Person, p_M_Del_Person );
											ELSE
													UPDATE Market SET M_Date = p_M_Date, M_Amount = p_M_Amount, M_Insrt_Person = p_M_Insrt_Person, M_Updt_Person = p_M_Updt_Person, M_Del_Person = p_M_Del_Person WHERE M_ID = p_M_ID;        
											END IF;
										END";
					using (MySqlCommand procCmd = new MySqlCommand(crtProc, mysqlConn))
					{
						procCmd.ExecuteNonQuery();
					}
					while (reader.Read())
					{
						using (MySqlCommand sqlCmd = new MySqlCommand("sp_marketSync", mysqlConn))
						{
							sqlCmd.CommandType = CommandType.StoredProcedure;
							sqlCmd.Parameters.AddWithValue("p_M_ID", reader["M_ID"]);
							sqlCmd.Parameters.AddWithValue("p_M_Date", reader["M_Date"]);
							sqlCmd.Parameters.AddWithValue("p_M_Amount", reader["M_Amount"]);
							sqlCmd.Parameters.AddWithValue("p_M_Insrt_Person", reader["M_Insrt_Person"]);
							sqlCmd.Parameters.AddWithValue("p_M_Updt_Person", reader["M_Updt_Person"]);
							sqlCmd.Parameters.AddWithValue("p_M_Del_Person", reader["M_Del_Person"]);
							sqlCmd.ExecuteNonQuery();
						}
					}
					mysqlConn.Close();
				}
				accConn.Close();
			}
		}
		private void sp_marketMemosSync()
		{
			using (OleDbConnection accConn = new OleDbConnection(connAcc))
			{
				accConn.Open();
				string selCom = "SELECT * FROM MarketMemos";
				OleDbCommand command = new OleDbCommand(selCom, accConn);
				OleDbDataReader reader = command.ExecuteReader();
				using (MySqlConnection mysqlConn = new MySqlConnection(connMySql))
				{
					mysqlConn.Open();
					string crtProc = @"CREATE PROCEDURE IF NOT EXISTS sp_marketMemosSync ( IN p_Mem_ID VARCHAR(250), IN p_Mem_Date DATE, IN p_R_InvTK FLOAT, IN p_C_InvTK FLOAT, IN p_Giv_TK FLOAT, IN p_Ret_TK FLOAT, IN p_I_N01 VARCHAR(250), IN p_I_N02 VARCHAR(250), IN p_I_N03 VARCHAR(250), IN p_I_N04 VARCHAR(250), IN p_I_N05 VARCHAR(250), IN p_I_N06 VARCHAR(250), IN p_I_N07 VARCHAR(250), IN p_I_N08 VARCHAR(250), IN p_I_N09 VARCHAR(250), IN p_I_N10 VARCHAR(250), IN p_I_N11 VARCHAR(250), IN p_I_N12 VARCHAR(250), IN p_I_N13 VARCHAR(250), IN p_I_N14 VARCHAR(250), IN p_I_N15 VARCHAR(250), IN p_I_N16 VARCHAR(250), IN p_I_P01 FLOAT, IN p_I_P02 FLOAT, IN p_I_P03 FLOAT, IN p_I_P04 FLOAT, IN p_I_P05 FLOAT, IN p_I_P06 FLOAT, IN p_I_P07 FLOAT, IN p_I_P08 FLOAT, IN p_I_P09 FLOAT, IN p_I_P10 FLOAT, IN p_I_P11 FLOAT, IN p_I_P12 FLOAT, IN p_I_P13 FLOAT, IN p_I_P14 FLOAT, IN p_I_P15 FLOAT, IN p_I_P16 FLOAT, IN p_I_Q01 FLOAT, IN p_I_Q02 FLOAT, IN p_I_Q03 FLOAT, IN p_I_Q04 FLOAT, IN p_I_Q05 FLOAT, IN p_I_Q06 FLOAT, IN p_I_Q07 FLOAT, IN p_I_Q08 FLOAT, IN p_I_Q09 FLOAT, IN p_I_Q10 FLOAT, IN p_I_Q11 FLOAT, IN p_I_Q12 FLOAT, IN p_I_Q13 FLOAT, IN p_I_Q14 FLOAT, IN p_I_Q15 FLOAT, IN p_I_Q16 FLOAT, IN p_I_ST01 FLOAT, IN p_I_ST02 FLOAT, IN p_I_ST03 FLOAT, IN p_I_ST04 FLOAT, IN p_I_ST05 FLOAT, IN p_I_ST06 FLOAT, IN p_I_ST07 FLOAT, IN p_I_ST08 FLOAT, IN p_I_ST09 FLOAT, IN p_I_ST10 FLOAT, IN p_I_ST11 FLOAT, IN p_I_ST12 FLOAT, IN p_I_ST13 FLOAT, IN p_I_ST14 FLOAT, IN p_I_ST15 FLOAT, IN p_I_ST16 FLOAT, IN p_R_Inv01 VARCHAR(250), IN p_R_Inv02 VARCHAR(250), IN p_R_Inv03 VARCHAR(250), IN p_R_Inv04 VARCHAR(250), IN p_R_Inv05 VARCHAR(250), IN p_R_Inv06 VARCHAR(250), IN p_R_Inv07 VARCHAR(250), IN p_R_Inv08 VARCHAR(250), IN p_R_Inv09 VARCHAR(250), IN p_R_Inv10 VARCHAR(250),  IN p_R_Inv11 VARCHAR(250), IN p_R_Inv12 VARCHAR(250), IN p_R_Inv13 VARCHAR(250), IN p_R_Inv14 VARCHAR(250), IN p_R_Inv15 VARCHAR(250), IN p_R_Inv16 VARCHAR(250), IN p_R_Inv17 VARCHAR(250), IN p_R_Inv18 VARCHAR(250), IN p_R_Inv19 VARCHAR(250), IN p_R_Inv20 VARCHAR(250), IN p_R_Inv21 VARCHAR(250), IN p_R_Inv22 VARCHAR(250), IN p_R_Inv23 VARCHAR(250), IN p_R_Inv24 VARCHAR(250), IN p_Mem_Insrt_Person VARCHAR(250), IN p_Mem_Updt_Person VARCHAR(250), IN p_Mem_Del_Person VARCHAR(250) )
										BEGIN
											IF NOT EXISTS (
													SELECT * FROM information_schema.tables WHERE table_name = 'MarketMemos'
												) THEN
													CREATE TABLE MarketMemos ( Mem_ID NVARCHAR(250) NULL, Mem_Date DATETIME, R_InvTK FLOAT DEFAULT 0, C_InvTK FLOAT DEFAULT 0, Giv_TK FLOAT DEFAULT 0, Ret_TK FLOAT DEFAULT 0, I_N01 NVARCHAR(250) NULL, I_N02 NVARCHAR(250) NULL, I_N03 NVARCHAR(250) NULL, I_N04 NVARCHAR(250) NULL, I_N05 NVARCHAR(250) NULL, I_N06 NVARCHAR(250) NULL, I_N07 NVARCHAR(250) NULL, I_N08 NVARCHAR(250) NULL, I_N09 NVARCHAR(250) NULL, I_N10 NVARCHAR(250) NULL, I_N11 NVARCHAR(250) NULL, I_N12 NVARCHAR(250) NULL, I_N13 NVARCHAR(250) NULL, I_N14 NVARCHAR(250) NULL, I_N15 NVARCHAR(250) NULL, I_N16 NVARCHAR(250) NULL, I_P01 FLOAT DEFAULT 0, I_P02 FLOAT DEFAULT 0, I_P03 FLOAT DEFAULT 0, I_P04 FLOAT DEFAULT 0, I_P05 FLOAT DEFAULT 0, I_P06 FLOAT DEFAULT 0, I_P07 FLOAT DEFAULT 0, I_P08 FLOAT DEFAULT 0, I_P09 FLOAT DEFAULT 0, I_P10 FLOAT DEFAULT 0, I_P11 FLOAT DEFAULT 0, I_P12 FLOAT DEFAULT 0, I_P13 FLOAT DEFAULT 0, I_P14 FLOAT DEFAULT 0, I_P15 FLOAT DEFAULT 0, I_P16 FLOAT DEFAULT 0, I_Q01 FLOAT DEFAULT 0, I_Q02 FLOAT DEFAULT 0, I_Q03 FLOAT DEFAULT 0, I_Q04 FLOAT DEFAULT 0, I_Q05 FLOAT DEFAULT 0, I_Q06 FLOAT DEFAULT 0, I_Q07 FLOAT DEFAULT 0, I_Q08 FLOAT DEFAULT 0, I_Q09 FLOAT DEFAULT 0, I_Q10 FLOAT DEFAULT 0, I_Q11 FLOAT DEFAULT 0, I_Q12 FLOAT DEFAULT 0, I_Q13 FLOAT DEFAULT 0, I_Q14 FLOAT DEFAULT 0, I_Q15 FLOAT DEFAULT 0, I_Q16 FLOAT DEFAULT 0, I_ST01 FLOAT DEFAULT 0, I_ST02 FLOAT DEFAULT 0, I_ST03 FLOAT DEFAULT 0, I_ST04 FLOAT DEFAULT 0, I_ST05 FLOAT DEFAULT 0, I_ST06 FLOAT DEFAULT 0, I_ST07 FLOAT DEFAULT 0, I_ST08 FLOAT DEFAULT 0, I_ST09 FLOAT DEFAULT 0, I_ST10 FLOAT DEFAULT 0, I_ST11 FLOAT DEFAULT 0, I_ST12 FLOAT DEFAULT 0, I_ST13 FLOAT DEFAULT 0, I_ST14 FLOAT DEFAULT 0, I_ST15 FLOAT DEFAULT 0, I_ST16 FLOAT DEFAULT 0, R_Inv01 NVARCHAR(250) NULL, R_Inv02 NVARCHAR(250) NULL, R_Inv03 NVARCHAR(250) NULL, R_Inv04 NVARCHAR(250) NULL, R_Inv05 NVARCHAR(250) NULL, R_Inv06 NVARCHAR(250) NULL, R_Inv07 NVARCHAR(250) NULL, R_Inv08 NVARCHAR(250) NULL, R_Inv09 NVARCHAR(250) NULL, R_Inv10 NVARCHAR(250) NULL, R_Inv11 NVARCHAR(250) NULL, R_Inv12 NVARCHAR(250) NULL, R_Inv13 NVARCHAR(250) NULL, R_Inv14 NVARCHAR(250) NULL, R_Inv15 NVARCHAR(250) NULL, R_Inv16 NVARCHAR(250) NULL, R_Inv17 NVARCHAR(250) NULL, R_Inv18 NVARCHAR(250) NULL, R_Inv19 NVARCHAR(250) NULL, R_Inv20 NVARCHAR(250) NULL, R_Inv21 NVARCHAR(250) NULL, R_Inv22 NVARCHAR(250) NULL, R_Inv23 NVARCHAR(250) NULL, R_Inv24 NVARCHAR(250) NULL, Mem_Insrt_Person NVARCHAR(250) NULL, Mem_Updt_Person NVARCHAR(250) NULL, Mem_Del_Person NVARCHAR(250) NULL );
												END IF;
												IF NOT EXISTS (
													SELECT * FROM MarketMemos WHERE Mem_ID = p_Mem_ID
												) THEN    
													INSERT INTO MarketMemos ( Mem_ID, Mem_Date, R_InvTK, C_InvTK, Giv_TK, Ret_TK, I_N01, I_N02, I_N03, I_N04, I_N05, I_N06, I_N07, I_N08, I_N09, I_N10, I_N11, I_N12, I_N13, I_N14, I_N15, I_N16, I_P01, I_P02, I_P03, I_P04, I_P05, I_P06, I_P07, I_P08, I_P09, I_P10, I_P11, I_P12, I_P13, I_P14, I_P15, I_P16, I_Q01, I_Q02, I_Q03, I_Q04, I_Q05, I_Q06, I_Q07, I_Q08, I_Q09, I_Q10, I_Q11, I_Q12, I_Q13, I_Q14, I_Q15, I_Q16, I_ST01, I_ST02, I_ST03, I_ST04, I_ST05, I_ST06, I_ST07, I_ST08, I_ST09, I_ST10, I_ST11, I_ST12, I_ST13, I_ST14, I_ST15, I_ST16, R_Inv01, R_Inv02, R_Inv03, R_Inv04, R_Inv05, R_Inv06, R_Inv07, R_Inv08, R_Inv09, R_Inv10, R_Inv11, R_Inv12, R_Inv13, R_Inv14, R_Inv15, R_Inv16, R_Inv17, R_Inv18, R_Inv19, R_Inv20, R_Inv21, R_Inv22, R_Inv23, R_Inv24, Mem_Insrt_Person, Mem_Updt_Person, Mem_Del_Person ) 
																	 VALUES ( p_Mem_ID, p_Mem_Date, p_R_InvTK, p_C_InvTK, p_Giv_TK, p_Ret_TK, p_I_N01, p_I_N02, p_I_N03, p_I_N04, p_I_N05, p_I_N06, p_I_N07, p_I_N08, p_I_N09, p_I_N10, p_I_N11, p_I_N12, p_I_N13, p_I_N14, p_I_N15, p_I_N16, p_I_P01, p_I_P02, p_I_P03, p_I_P04, p_I_P05, p_I_P06, p_I_P07, p_I_P08, p_I_P09, p_I_P10, p_I_P11, p_I_P12, p_I_P13, p_I_P14, p_I_P15, p_I_P16, p_I_Q01, p_I_Q02, p_I_Q03, p_I_Q04, p_I_Q05, p_I_Q06, p_I_Q07, p_I_Q08, p_I_Q09, p_I_Q10, p_I_Q11, p_I_Q12, p_I_Q13, p_I_Q14, p_I_Q15,p_I_Q16, p_I_ST01, p_I_ST02, p_I_ST03, p_I_ST04, p_I_ST05, p_I_ST06, p_I_ST07, p_I_ST08, p_I_ST09, p_I_ST10, p_I_ST11, p_I_ST12, p_I_ST13, p_I_ST14, p_I_ST15, p_I_ST16, p_R_Inv01, p_R_Inv02, p_R_Inv03, p_R_Inv04, p_R_Inv05, p_R_Inv06, p_R_Inv07, p_R_Inv08, p_R_Inv09, p_R_Inv10, p_R_Inv11, p_R_Inv12, p_R_Inv13, p_R_Inv14, p_R_Inv15, p_R_Inv16, p_R_Inv17, p_R_Inv18, p_R_Inv19, p_R_Inv20, p_R_Inv21, p_R_Inv22, p_R_Inv23, p_R_Inv24, p_Mem_Insrt_Person, p_Mem_Updt_Person, p_Mem_Del_Person );
											ELSE
													UPDATE MarketMemos SET  Mem_ID = p_Mem_ID, Mem_Date = p_Mem_Date, R_InvTK = p_R_InvTK, C_InvTK = p_C_InvTK, Giv_TK = p_Giv_TK, Ret_TK = p_Ret_TK, I_N01 = p_I_N01, I_N02 = p_I_N02, I_N03 = p_I_N03, I_N04 = p_I_N04, I_N05 = p_I_N05, I_N06 = p_I_N06, I_N07 = p_I_N07, I_N08 = p_I_N08, I_N09 = p_I_N09, I_N10 = p_I_N10, I_N11 = p_I_N11, I_N12 = p_I_N12, I_N13 = p_I_N13, I_N14 = p_I_N14, I_N15 = p_I_N15, I_N16 = p_I_N16, I_P01 = p_I_P01, I_P02 = p_I_P02, I_P03 = p_I_P03, I_P04 = p_I_P04, I_P05 = p_I_P05, I_P06 = p_I_P06, I_P07 = p_I_P07, I_P08 = p_I_P08, I_P09 = p_I_P09, I_P10 = p_I_P10, I_P11 = p_I_P11, I_P12 = p_I_P12, I_P13 = p_I_P13, I_P14 = p_I_P14, I_P15 = p_I_P15, I_P16 = p_I_P16, I_Q01 = p_I_Q01, I_Q02 = p_I_Q02, I_Q03 = p_I_Q03, I_Q04 = p_I_Q04, I_Q05 = p_I_Q05, I_Q06 = p_I_Q06, I_Q07 = p_I_Q07, I_Q08 = p_I_Q08, I_Q09 = p_I_Q09, I_Q10 = p_I_Q10, I_Q11 = p_I_Q11, I_Q12 = p_I_Q12, I_Q13 = p_I_Q13, I_Q14 = p_I_Q14, I_Q15 = p_I_Q15, I_Q16 = p_I_Q16, I_ST01 = p_I_ST01, I_ST02 = p_I_ST02, I_ST03 = p_I_ST03, I_ST04 = p_I_ST04, I_ST05 = p_I_ST05, I_ST06 = p_I_ST06, I_ST07 = p_I_ST07, I_ST08 = p_I_ST08, I_ST09 = p_I_ST09, I_ST10 = p_I_ST10, I_ST11 = p_I_ST11, I_ST12 = p_I_ST12, I_ST13 = p_I_ST13, I_ST14 = p_I_ST14, I_ST15 = p_I_ST15, I_ST16 = p_I_ST16, R_Inv01 = p_R_Inv01, R_Inv02 = p_R_Inv02, R_Inv03 = p_R_Inv03, R_Inv04 = p_R_Inv04, R_Inv05 = p_R_Inv05, R_Inv06 = p_R_Inv06, R_Inv07 = p_R_Inv07, R_Inv08 = p_R_Inv08, R_Inv09 = p_R_Inv09, R_Inv10 = p_R_Inv10, R_Inv11 = p_R_Inv11, R_Inv12 = p_R_Inv12, R_Inv13 = p_R_Inv13, R_Inv14 = p_R_Inv14, R_Inv15 = p_R_Inv15, R_Inv16 = p_R_Inv16, R_Inv17 = p_R_Inv17, R_Inv18 = p_R_Inv18, R_Inv19 = p_R_Inv19, R_Inv20 = p_R_Inv20, R_Inv21 = p_R_Inv21, R_Inv22 = p_R_Inv22, R_Inv23 = p_R_Inv23, R_Inv24 = p_R_Inv24, Mem_Insrt_Person = p_Mem_Insrt_Person, Mem_Updt_Person = p_Mem_Updt_Person, Mem_Del_Person = p_Mem_Del_Person;
												END IF;
										END";
					using (MySqlCommand procCmd = new MySqlCommand(crtProc, mysqlConn))
					{
						procCmd.ExecuteNonQuery();
					}
					while (reader.Read())
					{
						using (MySqlCommand sqlCmd = new MySqlCommand("sp_marketMemosSync", mysqlConn))
						{
							sqlCmd.CommandType = CommandType.StoredProcedure;
							sqlCmd.Parameters.AddWithValue("p_Mem_ID", reader["Mem_ID"]);
							sqlCmd.Parameters.AddWithValue("p_Mem_Date", reader["Mem_Date"]);
							sqlCmd.Parameters.AddWithValue("p_R_InvTK", reader["R_InvTK"]);
							sqlCmd.Parameters.AddWithValue("p_C_InvTK", reader["C_InvTK"]);
							sqlCmd.Parameters.AddWithValue("p_Giv_TK", reader["Giv_TK"]);
							sqlCmd.Parameters.AddWithValue("p_Ret_TK", reader["Ret_TK"]);
							sqlCmd.Parameters.AddWithValue("p_I_N01", reader["I_N01"]);
							sqlCmd.Parameters.AddWithValue("p_I_N02", reader["I_N02"]);
							sqlCmd.Parameters.AddWithValue("p_I_N03", reader["I_N03"]);
							sqlCmd.Parameters.AddWithValue("p_I_N04", reader["I_N04"]);
							sqlCmd.Parameters.AddWithValue("p_I_N05", reader["I_N05"]);
							sqlCmd.Parameters.AddWithValue("p_I_N06", reader["I_N06"]);
							sqlCmd.Parameters.AddWithValue("p_I_N07", reader["I_N07"]);
							sqlCmd.Parameters.AddWithValue("p_I_N08", reader["I_N08"]);
							sqlCmd.Parameters.AddWithValue("p_I_N09", reader["I_N09"]);
							sqlCmd.Parameters.AddWithValue("p_I_N10", reader["I_N10"]);
							sqlCmd.Parameters.AddWithValue("p_I_N11", reader["I_N11"]);
							sqlCmd.Parameters.AddWithValue("p_I_N12", reader["I_N12"]);
							sqlCmd.Parameters.AddWithValue("p_I_N13", reader["I_N13"]);
							sqlCmd.Parameters.AddWithValue("p_I_N14", reader["I_N14"]);
							sqlCmd.Parameters.AddWithValue("p_I_N15", reader["I_N15"]);
							sqlCmd.Parameters.AddWithValue("p_I_N16", reader["I_N16"]);
							sqlCmd.Parameters.AddWithValue("p_I_P01", reader["I_P01"]);
							sqlCmd.Parameters.AddWithValue("p_I_P02", reader["I_P02"]);
							sqlCmd.Parameters.AddWithValue("p_I_P03", reader["I_P03"]);
							sqlCmd.Parameters.AddWithValue("p_I_P04", reader["I_P04"]);
							sqlCmd.Parameters.AddWithValue("p_I_P05", reader["I_P05"]);
							sqlCmd.Parameters.AddWithValue("p_I_P06", reader["I_P06"]);
							sqlCmd.Parameters.AddWithValue("p_I_P07", reader["I_P07"]);
							sqlCmd.Parameters.AddWithValue("p_I_P08", reader["I_P08"]);
							sqlCmd.Parameters.AddWithValue("p_I_P09", reader["I_P09"]);
							sqlCmd.Parameters.AddWithValue("p_I_P10", reader["I_P10"]);
							sqlCmd.Parameters.AddWithValue("p_I_P11", reader["I_P11"]);
							sqlCmd.Parameters.AddWithValue("p_I_P12", reader["I_P12"]);
							sqlCmd.Parameters.AddWithValue("p_I_P13", reader["I_P13"]);
							sqlCmd.Parameters.AddWithValue("p_I_P14", reader["I_P14"]);
							sqlCmd.Parameters.AddWithValue("p_I_P15", reader["I_P15"]);
							sqlCmd.Parameters.AddWithValue("p_I_P16", reader["I_P16"]);
							sqlCmd.Parameters.AddWithValue("p_I_Q01", reader["I_Q01"]);
							sqlCmd.Parameters.AddWithValue("p_I_Q02", reader["I_Q02"]);
							sqlCmd.Parameters.AddWithValue("p_I_Q03", reader["I_Q03"]);
							sqlCmd.Parameters.AddWithValue("p_I_Q04", reader["I_Q04"]);
							sqlCmd.Parameters.AddWithValue("p_I_Q05", reader["I_Q05"]);
							sqlCmd.Parameters.AddWithValue("p_I_Q06", reader["I_Q06"]);
							sqlCmd.Parameters.AddWithValue("p_I_Q07", reader["I_Q07"]);
							sqlCmd.Parameters.AddWithValue("p_I_Q08", reader["I_Q08"]);
							sqlCmd.Parameters.AddWithValue("p_I_Q09", reader["I_Q09"]);
							sqlCmd.Parameters.AddWithValue("p_I_Q10", reader["I_Q10"]);
							sqlCmd.Parameters.AddWithValue("p_I_Q11", reader["I_Q11"]);
							sqlCmd.Parameters.AddWithValue("p_I_Q12", reader["I_Q12"]);
							sqlCmd.Parameters.AddWithValue("p_I_Q13", reader["I_Q13"]);
							sqlCmd.Parameters.AddWithValue("p_I_Q14", reader["I_Q14"]);
							sqlCmd.Parameters.AddWithValue("p_I_Q15", reader["I_Q15"]);
							sqlCmd.Parameters.AddWithValue("p_I_Q16", reader["I_Q16"]);
							sqlCmd.Parameters.AddWithValue("p_I_ST01", reader["I_ST01"]);
							sqlCmd.Parameters.AddWithValue("p_I_ST02", reader["I_ST02"]);
							sqlCmd.Parameters.AddWithValue("p_I_ST03", reader["I_ST03"]);
							sqlCmd.Parameters.AddWithValue("p_I_ST04", reader["I_ST04"]);
							sqlCmd.Parameters.AddWithValue("p_I_ST05", reader["I_ST05"]);
							sqlCmd.Parameters.AddWithValue("p_I_ST06", reader["I_ST06"]);
							sqlCmd.Parameters.AddWithValue("p_I_ST07", reader["I_ST07"]);
							sqlCmd.Parameters.AddWithValue("p_I_ST08", reader["I_ST08"]);
							sqlCmd.Parameters.AddWithValue("p_I_ST09", reader["I_ST09"]);
							sqlCmd.Parameters.AddWithValue("p_I_ST10", reader["I_ST10"]);
							sqlCmd.Parameters.AddWithValue("p_I_ST11", reader["I_ST11"]);
							sqlCmd.Parameters.AddWithValue("p_I_ST12", reader["I_ST12"]);
							sqlCmd.Parameters.AddWithValue("p_I_ST13", reader["I_ST13"]);
							sqlCmd.Parameters.AddWithValue("p_I_ST14", reader["I_ST14"]);
							sqlCmd.Parameters.AddWithValue("p_I_ST15", reader["I_ST15"]);
							sqlCmd.Parameters.AddWithValue("p_I_ST16", reader["I_ST16"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv01", reader["R_Inv01"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv02", reader["R_Inv02"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv03", reader["R_Inv03"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv04", reader["R_Inv04"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv05", reader["R_Inv05"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv06", reader["R_Inv06"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv07", reader["R_Inv07"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv08", reader["R_Inv08"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv09", reader["R_Inv09"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv10", reader["R_Inv10"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv11", reader["R_Inv11"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv12", reader["R_Inv12"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv13", reader["R_Inv13"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv14", reader["R_Inv14"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv15", reader["R_Inv15"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv16", reader["R_Inv16"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv17", reader["R_Inv17"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv18", reader["R_Inv18"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv19", reader["R_Inv19"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv20", reader["R_Inv20"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv21", reader["R_Inv21"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv22", reader["R_Inv22"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv23", reader["R_Inv23"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv24", reader["R_Inv24"]);
							sqlCmd.Parameters.AddWithValue("p_Mem_Insrt_Person", reader["Mem_Insrt_Person"]);
							sqlCmd.Parameters.AddWithValue("p_Mem_Updt_Person", reader["Mem_Updt_Person"]);
							sqlCmd.Parameters.AddWithValue("p_Mem_Del_Person", reader["Mem_Del_Person"]);
							sqlCmd.ExecuteNonQuery();
						}
					}
					mysqlConn.Close();
				}
				accConn.Close();
			}
		}
		private void sp_marketMemosDelSync()
		{
			using (OleDbConnection accConn = new OleDbConnection(connAcc))
			{
				accConn.Open();
				string selCom = "SELECT * FROM MarketMemosDel";
				OleDbCommand command = new OleDbCommand(selCom, accConn);
				OleDbDataReader reader = command.ExecuteReader();
				using (MySqlConnection mysqlConn = new MySqlConnection(connMySql))
				{
					mysqlConn.Open();
					string crtProc = @"CREATE PROCEDURE IF NOT EXISTS sp_marketMemosDelSync ( IN p_Mem_ID VARCHAR(250), IN p_Mem_Date DATE, IN p_R_InvTK FLOAT, IN p_C_InvTK FLOAT, IN p_Giv_TK FLOAT, IN p_Ret_TK FLOAT, IN p_I_N01 VARCHAR(250), IN p_I_N02 VARCHAR(250), IN p_I_N03 VARCHAR(250), IN p_I_N04 VARCHAR(250), IN p_I_N05 VARCHAR(250), IN p_I_N06 VARCHAR(250), IN p_I_N07 VARCHAR(250), IN p_I_N08 VARCHAR(250), IN p_I_N09 VARCHAR(250), IN p_I_N10 VARCHAR(250), IN p_I_N11 VARCHAR(250), IN p_I_N12 VARCHAR(250), IN p_I_N13 VARCHAR(250), IN p_I_N14 VARCHAR(250), IN p_I_N15 VARCHAR(250), IN p_I_N16 VARCHAR(250), IN p_I_P01 FLOAT, IN p_I_P02 FLOAT, IN p_I_P03 FLOAT, IN p_I_P04 FLOAT, IN p_I_P05 FLOAT, IN p_I_P06 FLOAT, IN p_I_P07 FLOAT, IN p_I_P08 FLOAT, IN p_I_P09 FLOAT, IN p_I_P10 FLOAT, IN p_I_P11 FLOAT, IN p_I_P12 FLOAT, IN p_I_P13 FLOAT, IN p_I_P14 FLOAT, IN p_I_P15 FLOAT, IN p_I_P16 FLOAT, IN p_I_Q01 FLOAT, IN p_I_Q02 FLOAT, IN p_I_Q03 FLOAT, IN p_I_Q04 FLOAT, IN p_I_Q05 FLOAT, IN p_I_Q06 FLOAT, IN p_I_Q07 FLOAT, IN p_I_Q08 FLOAT, IN p_I_Q09 FLOAT, IN p_I_Q10 FLOAT, IN p_I_Q11 FLOAT, IN p_I_Q12 FLOAT, IN p_I_Q13 FLOAT, IN p_I_Q14 FLOAT, IN p_I_Q15 FLOAT, IN p_I_Q16 FLOAT, IN p_I_ST01 FLOAT, IN p_I_ST02 FLOAT, IN p_I_ST03 FLOAT, IN p_I_ST04 FLOAT, IN p_I_ST05 FLOAT, IN p_I_ST06 FLOAT, IN p_I_ST07 FLOAT, IN p_I_ST08 FLOAT, IN p_I_ST09 FLOAT, IN p_I_ST10 FLOAT, IN p_I_ST11 FLOAT, IN p_I_ST12 FLOAT, IN p_I_ST13 FLOAT, IN p_I_ST14 FLOAT, IN p_I_ST15 FLOAT, IN p_I_ST16 FLOAT, IN p_R_Inv01 VARCHAR(250), IN p_R_Inv02 VARCHAR(250), IN p_R_Inv03 VARCHAR(250), IN p_R_Inv04 VARCHAR(250), IN p_R_Inv05 VARCHAR(250), IN p_R_Inv06 VARCHAR(250), IN p_R_Inv07 VARCHAR(250), IN p_R_Inv08 VARCHAR(250), IN p_R_Inv09 VARCHAR(250), IN p_R_Inv10 VARCHAR(250),  IN p_R_Inv11 VARCHAR(250), IN p_R_Inv12 VARCHAR(250), IN p_R_Inv13 VARCHAR(250), IN p_R_Inv14 VARCHAR(250), IN p_R_Inv15 VARCHAR(250), IN p_R_Inv16 VARCHAR(250), IN p_R_Inv17 VARCHAR(250), IN p_R_Inv18 VARCHAR(250), IN p_R_Inv19 VARCHAR(250), IN p_R_Inv20 VARCHAR(250), IN p_R_Inv21 VARCHAR(250), IN p_R_Inv22 VARCHAR(250), IN p_R_Inv23 VARCHAR(250), IN p_R_Inv24 VARCHAR(250), IN p_Mem_Insrt_Person VARCHAR(250), IN p_Mem_Updt_Person VARCHAR(250), IN p_Mem_Del_Person VARCHAR(250) )
										BEGIN
											IF NOT EXISTS (
													SELECT * FROM information_schema.tables WHERE table_name = 'MarketMemosDel'
												) THEN
													CREATE TABLE MarketMemosDel ( Mem_ID NVARCHAR(250) NULL, Mem_Date DATETIME, R_InvTK FLOAT DEFAULT 0, C_InvTK FLOAT DEFAULT 0, Giv_TK FLOAT DEFAULT 0, Ret_TK FLOAT DEFAULT 0, I_N01 NVARCHAR(250) NULL, I_N02 NVARCHAR(250) NULL, I_N03 NVARCHAR(250) NULL, I_N04 NVARCHAR(250) NULL, I_N05 NVARCHAR(250) NULL, I_N06 NVARCHAR(250) NULL, I_N07 NVARCHAR(250) NULL, I_N08 NVARCHAR(250) NULL, I_N09 NVARCHAR(250) NULL, I_N10 NVARCHAR(250) NULL, I_N11 NVARCHAR(250) NULL, I_N12 NVARCHAR(250) NULL, I_N13 NVARCHAR(250) NULL, I_N14 NVARCHAR(250) NULL, I_N15 NVARCHAR(250) NULL, I_N16 NVARCHAR(250) NULL, I_P01 FLOAT DEFAULT 0, I_P02 FLOAT DEFAULT 0, I_P03 FLOAT DEFAULT 0, I_P04 FLOAT DEFAULT 0, I_P05 FLOAT DEFAULT 0, I_P06 FLOAT DEFAULT 0, I_P07 FLOAT DEFAULT 0, I_P08 FLOAT DEFAULT 0, I_P09 FLOAT DEFAULT 0, I_P10 FLOAT DEFAULT 0, I_P11 FLOAT DEFAULT 0, I_P12 FLOAT DEFAULT 0, I_P13 FLOAT DEFAULT 0, I_P14 FLOAT DEFAULT 0, I_P15 FLOAT DEFAULT 0, I_P16 FLOAT DEFAULT 0, I_Q01 FLOAT DEFAULT 0, I_Q02 FLOAT DEFAULT 0, I_Q03 FLOAT DEFAULT 0, I_Q04 FLOAT DEFAULT 0, I_Q05 FLOAT DEFAULT 0, I_Q06 FLOAT DEFAULT 0, I_Q07 FLOAT DEFAULT 0, I_Q08 FLOAT DEFAULT 0, I_Q09 FLOAT DEFAULT 0, I_Q10 FLOAT DEFAULT 0, I_Q11 FLOAT DEFAULT 0, I_Q12 FLOAT DEFAULT 0, I_Q13 FLOAT DEFAULT 0, I_Q14 FLOAT DEFAULT 0, I_Q15 FLOAT DEFAULT 0, I_Q16 FLOAT DEFAULT 0, I_ST01 FLOAT DEFAULT 0, I_ST02 FLOAT DEFAULT 0, I_ST03 FLOAT DEFAULT 0, I_ST04 FLOAT DEFAULT 0, I_ST05 FLOAT DEFAULT 0, I_ST06 FLOAT DEFAULT 0, I_ST07 FLOAT DEFAULT 0, I_ST08 FLOAT DEFAULT 0, I_ST09 FLOAT DEFAULT 0, I_ST10 FLOAT DEFAULT 0, I_ST11 FLOAT DEFAULT 0, I_ST12 FLOAT DEFAULT 0, I_ST13 FLOAT DEFAULT 0, I_ST14 FLOAT DEFAULT 0, I_ST15 FLOAT DEFAULT 0, I_ST16 FLOAT DEFAULT 0, R_Inv01 NVARCHAR(250) NULL, R_Inv02 NVARCHAR(250) NULL, R_Inv03 NVARCHAR(250) NULL, R_Inv04 NVARCHAR(250) NULL, R_Inv05 NVARCHAR(250) NULL, R_Inv06 NVARCHAR(250) NULL, R_Inv07 NVARCHAR(250) NULL, R_Inv08 NVARCHAR(250) NULL, R_Inv09 NVARCHAR(250) NULL, R_Inv10 NVARCHAR(250) NULL, R_Inv11 NVARCHAR(250) NULL, R_Inv12 NVARCHAR(250) NULL, R_Inv13 NVARCHAR(250) NULL, R_Inv14 NVARCHAR(250) NULL, R_Inv15 NVARCHAR(250) NULL, R_Inv16 NVARCHAR(250) NULL, R_Inv17 NVARCHAR(250) NULL, R_Inv18 NVARCHAR(250) NULL, R_Inv19 NVARCHAR(250) NULL, R_Inv20 NVARCHAR(250) NULL, R_Inv21 NVARCHAR(250) NULL, R_Inv22 NVARCHAR(250) NULL, R_Inv23 NVARCHAR(250) NULL, R_Inv24 NVARCHAR(250) NULL, Mem_Insrt_Person NVARCHAR(250) NULL, Mem_Updt_Person NVARCHAR(250) NULL, Mem_Del_Person NVARCHAR(250) NULL );
												END IF;
												IF NOT EXISTS (
													SELECT * FROM MarketMemosDel WHERE Mem_ID = p_Mem_ID
												) THEN    
													INSERT INTO MarketMemosDel ( Mem_ID, Mem_Date, R_InvTK, C_InvTK, Giv_TK, Ret_TK, I_N01, I_N02, I_N03, I_N04, I_N05, I_N06, I_N07, I_N08, I_N09, I_N10, I_N11, I_N12, I_N13, I_N14, I_N15, I_N16, I_P01, I_P02, I_P03, I_P04, I_P05, I_P06, I_P07, I_P08, I_P09, I_P10, I_P11, I_P12, I_P13, I_P14, I_P15, I_P16, I_Q01, I_Q02, I_Q03, I_Q04, I_Q05, I_Q06, I_Q07, I_Q08, I_Q09, I_Q10, I_Q11, I_Q12, I_Q13, I_Q14, I_Q15, I_Q16, I_ST01, I_ST02, I_ST03, I_ST04, I_ST05, I_ST06, I_ST07, I_ST08, I_ST09, I_ST10, I_ST11, I_ST12, I_ST13, I_ST14, I_ST15, I_ST16, R_Inv01, R_Inv02, R_Inv03, R_Inv04, R_Inv05, R_Inv06, R_Inv07, R_Inv08, R_Inv09, R_Inv10, R_Inv11, R_Inv12, R_Inv13, R_Inv14, R_Inv15, R_Inv16, R_Inv17, R_Inv18, R_Inv19, R_Inv20, R_Inv21, R_Inv22, R_Inv23, R_Inv24, Mem_Insrt_Person, Mem_Updt_Person, Mem_Del_Person ) 
																		VALUES ( p_Mem_ID, p_Mem_Date, p_R_InvTK, p_C_InvTK, p_Giv_TK, p_Ret_TK, p_I_N01, p_I_N02, p_I_N03, p_I_N04, p_I_N05, p_I_N06, p_I_N07, p_I_N08, p_I_N09, p_I_N10, p_I_N11, p_I_N12, p_I_N13, p_I_N14, p_I_N15, p_I_N16, p_I_P01, p_I_P02, p_I_P03, p_I_P04, p_I_P05, p_I_P06, p_I_P07, p_I_P08, p_I_P09, p_I_P10, p_I_P11, p_I_P12, p_I_P13, p_I_P14, p_I_P15, p_I_P16, p_I_Q01, p_I_Q02, p_I_Q03, p_I_Q04, p_I_Q05, p_I_Q06, p_I_Q07, p_I_Q08, p_I_Q09, p_I_Q10, p_I_Q11, p_I_Q12, p_I_Q13, p_I_Q14, p_I_Q15,p_I_Q16, p_I_ST01, p_I_ST02, p_I_ST03, p_I_ST04, p_I_ST05, p_I_ST06, p_I_ST07, p_I_ST08, p_I_ST09, p_I_ST10, p_I_ST11, p_I_ST12, p_I_ST13, p_I_ST14, p_I_ST15, p_I_ST16, p_R_Inv01, p_R_Inv02, p_R_Inv03, p_R_Inv04, p_R_Inv05, p_R_Inv06, p_R_Inv07, p_R_Inv08, p_R_Inv09, p_R_Inv10, p_R_Inv11, p_R_Inv12, p_R_Inv13, p_R_Inv14, p_R_Inv15, p_R_Inv16, p_R_Inv17, p_R_Inv18, p_R_Inv19, p_R_Inv20, p_R_Inv21, p_R_Inv22, p_R_Inv23, p_R_Inv24, p_Mem_Insrt_Person, p_Mem_Updt_Person, p_Mem_Del_Person );
											ELSE
													UPDATE MarketMemosDel SET  Mem_ID = p_Mem_ID, Mem_Date = p_Mem_Date, R_InvTK = p_R_InvTK, C_InvTK = p_C_InvTK, Giv_TK = p_Giv_TK, Ret_TK = p_Ret_TK, I_N01 = p_I_N01, I_N02 = p_I_N02, I_N03 = p_I_N03, I_N04 = p_I_N04, I_N05 = p_I_N05, I_N06 = p_I_N06, I_N07 = p_I_N07, I_N08 = p_I_N08, I_N09 = p_I_N09, I_N10 = p_I_N10, I_N11 = p_I_N11, I_N12 = p_I_N12, I_N13 = p_I_N13, I_N14 = p_I_N14, I_N15 = p_I_N15, I_N16 = p_I_N16, I_P01 = p_I_P01, I_P02 = p_I_P02, I_P03 = p_I_P03, I_P04 = p_I_P04, I_P05 = p_I_P05, I_P06 = p_I_P06, I_P07 = p_I_P07, I_P08 = p_I_P08, I_P09 = p_I_P09, I_P10 = p_I_P10, I_P11 = p_I_P11, I_P12 = p_I_P12, I_P13 = p_I_P13, I_P14 = p_I_P14, I_P15 = p_I_P15, I_P16 = p_I_P16, I_Q01 = p_I_Q01, I_Q02 = p_I_Q02, I_Q03 = p_I_Q03, I_Q04 = p_I_Q04, I_Q05 = p_I_Q05, I_Q06 = p_I_Q06, I_Q07 = p_I_Q07, I_Q08 = p_I_Q08, I_Q09 = p_I_Q09, I_Q10 = p_I_Q10, I_Q11 = p_I_Q11, I_Q12 = p_I_Q12, I_Q13 = p_I_Q13, I_Q14 = p_I_Q14, I_Q15 = p_I_Q15, I_Q16 = p_I_Q16, I_ST01 = p_I_ST01, I_ST02 = p_I_ST02, I_ST03 = p_I_ST03, I_ST04 = p_I_ST04, I_ST05 = p_I_ST05, I_ST06 = p_I_ST06, I_ST07 = p_I_ST07, I_ST08 = p_I_ST08, I_ST09 = p_I_ST09, I_ST10 = p_I_ST10, I_ST11 = p_I_ST11, I_ST12 = p_I_ST12, I_ST13 = p_I_ST13, I_ST14 = p_I_ST14, I_ST15 = p_I_ST15, I_ST16 = p_I_ST16, R_Inv01 = p_R_Inv01, R_Inv02 = p_R_Inv02, R_Inv03 = p_R_Inv03, R_Inv04 = p_R_Inv04, R_Inv05 = p_R_Inv05, R_Inv06 = p_R_Inv06, R_Inv07 = p_R_Inv07, R_Inv08 = p_R_Inv08, R_Inv09 = p_R_Inv09, R_Inv10 = p_R_Inv10, R_Inv11 = p_R_Inv11, R_Inv12 = p_R_Inv12, R_Inv13 = p_R_Inv13, R_Inv14 = p_R_Inv14, R_Inv15 = p_R_Inv15, R_Inv16 = p_R_Inv16, R_Inv17 = p_R_Inv17, R_Inv18 = p_R_Inv18, R_Inv19 = p_R_Inv19, R_Inv20 = p_R_Inv20, R_Inv21 = p_R_Inv21, R_Inv22 = p_R_Inv22, R_Inv23 = p_R_Inv23, R_Inv24 = p_R_Inv24, Mem_Insrt_Person = p_Mem_Insrt_Person, Mem_Updt_Person = p_Mem_Updt_Person, Mem_Del_Person = p_Mem_Del_Person;
												END IF;
										END";
					using (MySqlCommand procCmd = new MySqlCommand(crtProc, mysqlConn))
					{
						procCmd.ExecuteNonQuery();
					}
					while (reader.Read())
					{
						using (MySqlCommand sqlCmd = new MySqlCommand("sp_marketMemosDelSync", mysqlConn))
						{
							sqlCmd.CommandType = CommandType.StoredProcedure;
							sqlCmd.Parameters.AddWithValue("p_Mem_ID", reader["Mem_ID"]);
							sqlCmd.Parameters.AddWithValue("p_Mem_Date", reader["Mem_Date"]);
							sqlCmd.Parameters.AddWithValue("p_R_InvTK", reader["R_InvTK"]);
							sqlCmd.Parameters.AddWithValue("p_C_InvTK", reader["C_InvTK"]);
							sqlCmd.Parameters.AddWithValue("p_Giv_TK", reader["Giv_TK"]);
							sqlCmd.Parameters.AddWithValue("p_Ret_TK", reader["Ret_TK"]);
							sqlCmd.Parameters.AddWithValue("p_I_N01", reader["I_N01"]);
							sqlCmd.Parameters.AddWithValue("p_I_N02", reader["I_N02"]);
							sqlCmd.Parameters.AddWithValue("p_I_N03", reader["I_N03"]);
							sqlCmd.Parameters.AddWithValue("p_I_N04", reader["I_N04"]);
							sqlCmd.Parameters.AddWithValue("p_I_N05", reader["I_N05"]);
							sqlCmd.Parameters.AddWithValue("p_I_N06", reader["I_N06"]);
							sqlCmd.Parameters.AddWithValue("p_I_N07", reader["I_N07"]);
							sqlCmd.Parameters.AddWithValue("p_I_N08", reader["I_N08"]);
							sqlCmd.Parameters.AddWithValue("p_I_N09", reader["I_N09"]);
							sqlCmd.Parameters.AddWithValue("p_I_N10", reader["I_N10"]);
							sqlCmd.Parameters.AddWithValue("p_I_N11", reader["I_N11"]);
							sqlCmd.Parameters.AddWithValue("p_I_N12", reader["I_N12"]);
							sqlCmd.Parameters.AddWithValue("p_I_N13", reader["I_N13"]);
							sqlCmd.Parameters.AddWithValue("p_I_N14", reader["I_N14"]);
							sqlCmd.Parameters.AddWithValue("p_I_N15", reader["I_N15"]);
							sqlCmd.Parameters.AddWithValue("p_I_N16", reader["I_N16"]);
							sqlCmd.Parameters.AddWithValue("p_I_P01", reader["I_P01"]);
							sqlCmd.Parameters.AddWithValue("p_I_P02", reader["I_P02"]);
							sqlCmd.Parameters.AddWithValue("p_I_P03", reader["I_P03"]);
							sqlCmd.Parameters.AddWithValue("p_I_P04", reader["I_P04"]);
							sqlCmd.Parameters.AddWithValue("p_I_P05", reader["I_P05"]);
							sqlCmd.Parameters.AddWithValue("p_I_P06", reader["I_P06"]);
							sqlCmd.Parameters.AddWithValue("p_I_P07", reader["I_P07"]);
							sqlCmd.Parameters.AddWithValue("p_I_P08", reader["I_P08"]);
							sqlCmd.Parameters.AddWithValue("p_I_P09", reader["I_P09"]);
							sqlCmd.Parameters.AddWithValue("p_I_P10", reader["I_P10"]);
							sqlCmd.Parameters.AddWithValue("p_I_P11", reader["I_P11"]);
							sqlCmd.Parameters.AddWithValue("p_I_P12", reader["I_P12"]);
							sqlCmd.Parameters.AddWithValue("p_I_P13", reader["I_P13"]);
							sqlCmd.Parameters.AddWithValue("p_I_P14", reader["I_P14"]);
							sqlCmd.Parameters.AddWithValue("p_I_P15", reader["I_P15"]);
							sqlCmd.Parameters.AddWithValue("p_I_P16", reader["I_P16"]);
							sqlCmd.Parameters.AddWithValue("p_I_Q01", reader["I_Q01"]);
							sqlCmd.Parameters.AddWithValue("p_I_Q02", reader["I_Q02"]);
							sqlCmd.Parameters.AddWithValue("p_I_Q03", reader["I_Q03"]);
							sqlCmd.Parameters.AddWithValue("p_I_Q04", reader["I_Q04"]);
							sqlCmd.Parameters.AddWithValue("p_I_Q05", reader["I_Q05"]);
							sqlCmd.Parameters.AddWithValue("p_I_Q06", reader["I_Q06"]);
							sqlCmd.Parameters.AddWithValue("p_I_Q07", reader["I_Q07"]);
							sqlCmd.Parameters.AddWithValue("p_I_Q08", reader["I_Q08"]);
							sqlCmd.Parameters.AddWithValue("p_I_Q09", reader["I_Q09"]);
							sqlCmd.Parameters.AddWithValue("p_I_Q10", reader["I_Q10"]);
							sqlCmd.Parameters.AddWithValue("p_I_Q11", reader["I_Q11"]);
							sqlCmd.Parameters.AddWithValue("p_I_Q12", reader["I_Q12"]);
							sqlCmd.Parameters.AddWithValue("p_I_Q13", reader["I_Q13"]);
							sqlCmd.Parameters.AddWithValue("p_I_Q14", reader["I_Q14"]);
							sqlCmd.Parameters.AddWithValue("p_I_Q15", reader["I_Q15"]);
							sqlCmd.Parameters.AddWithValue("p_I_Q16", reader["I_Q16"]);
							sqlCmd.Parameters.AddWithValue("p_I_ST01", reader["I_ST01"]);
							sqlCmd.Parameters.AddWithValue("p_I_ST02", reader["I_ST02"]);
							sqlCmd.Parameters.AddWithValue("p_I_ST03", reader["I_ST03"]);
							sqlCmd.Parameters.AddWithValue("p_I_ST04", reader["I_ST04"]);
							sqlCmd.Parameters.AddWithValue("p_I_ST05", reader["I_ST05"]);
							sqlCmd.Parameters.AddWithValue("p_I_ST06", reader["I_ST06"]);
							sqlCmd.Parameters.AddWithValue("p_I_ST07", reader["I_ST07"]);
							sqlCmd.Parameters.AddWithValue("p_I_ST08", reader["I_ST08"]);
							sqlCmd.Parameters.AddWithValue("p_I_ST09", reader["I_ST09"]);
							sqlCmd.Parameters.AddWithValue("p_I_ST10", reader["I_ST10"]);
							sqlCmd.Parameters.AddWithValue("p_I_ST11", reader["I_ST11"]);
							sqlCmd.Parameters.AddWithValue("p_I_ST12", reader["I_ST12"]);
							sqlCmd.Parameters.AddWithValue("p_I_ST13", reader["I_ST13"]);
							sqlCmd.Parameters.AddWithValue("p_I_ST14", reader["I_ST14"]);
							sqlCmd.Parameters.AddWithValue("p_I_ST15", reader["I_ST15"]);
							sqlCmd.Parameters.AddWithValue("p_I_ST16", reader["I_ST16"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv01", reader["R_Inv01"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv02", reader["R_Inv02"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv03", reader["R_Inv03"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv04", reader["R_Inv04"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv05", reader["R_Inv05"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv06", reader["R_Inv06"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv07", reader["R_Inv07"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv08", reader["R_Inv08"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv09", reader["R_Inv09"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv10", reader["R_Inv10"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv11", reader["R_Inv11"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv12", reader["R_Inv12"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv13", reader["R_Inv13"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv14", reader["R_Inv14"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv15", reader["R_Inv15"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv16", reader["R_Inv16"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv17", reader["R_Inv17"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv18", reader["R_Inv18"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv19", reader["R_Inv19"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv20", reader["R_Inv20"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv21", reader["R_Inv21"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv22", reader["R_Inv22"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv23", reader["R_Inv23"]);
							sqlCmd.Parameters.AddWithValue("p_R_Inv24", reader["R_Inv24"]);
							sqlCmd.Parameters.AddWithValue("p_Mem_Insrt_Person", reader["Mem_Insrt_Person"]);
							sqlCmd.Parameters.AddWithValue("p_Mem_Updt_Person", reader["Mem_Updt_Person"]);
							sqlCmd.Parameters.AddWithValue("p_Mem_Del_Person", reader["Mem_Del_Person"]);
							sqlCmd.ExecuteNonQuery();
						}
					}
					mysqlConn.Close();
				}
				accConn.Close();
			}
		}
		private void sp_dailySavingSync()
		{
            using (OleDbConnection accConn = new OleDbConnection(connAcc))
            {
                accConn.Open();
                string selCom = "SELECT * FROM DailySaving";
                OleDbCommand command = new OleDbCommand(selCom, accConn);
                OleDbDataReader reader = command.ExecuteReader();
                using (MySqlConnection mysqlConn = new MySqlConnection(connMySql))
                {
                    mysqlConn.Open();
                    string crtProc = @"CREATE PROCEDURE IF NOT EXISTS sp_dailySavingSync ( IN p_DS_ID VARCHAR(250), IN p_DS_Date DATE, IN p_DS_FPAmount FLOAT, IN p_DS_SPAmount FLOAT, IN p_DS_TPAmount FLOAT, IN p_NotTaken FLOAT, IN p_DS_Data VARCHAR(250), IN p_DS_InBankDate VARCHAR(250), IN p_DS_Insrt_Person VARCHAR(250), IN p_DS_Updt_Person VARCHAR(250), IN p_DS_Del_Person VARCHAR(250) )
										BEGIN
											IF NOT EXISTS (
													SELECT * FROM information_schema.tables WHERE table_name = 'DailySaving'
												) THEN
													CREATE TABLE DailySaving ( DS_ID NVARCHAR(250) NULL, DS_Date DATE, DS_FPAmount FLOAT DEFAULT 0, DS_SPAmount FLOAT DEFAULT 0, DS_TPAmount FLOAT DEFAULT 0, NotTaken FLOAT DEFAULT 0, DS_Data NVARCHAR(250) NULL, DS_InBankDate NVARCHAR(250) NULL, DS_Insrt_Person NVARCHAR(250) NULL, DS_Updt_Person NVARCHAR(250) NULL, DS_Del_Person NVARCHAR(250) NULL );
												END IF;
												IF NOT EXISTS (
													SELECT * FROM DailySaving WHERE DS_ID = p_DS_ID
												) THEN         
													INSERT INTO DailySaving ( DS_ID, DS_Date, DS_FPAmount, DS_SPAmount, DS_TPAmount, NotTaken, DS_Data, DS_InBankDate, DS_Insrt_Person, DS_Updt_Person, DS_Del_Person ) 
																	 VALUES ( p_DS_ID, p_DS_Date, p_DS_FPAmount, p_DS_SPAmount, p_DS_TPAmount, p_NotTaken, p_DS_Data, p_DS_InBankDate, p_DS_Insrt_Person, p_DS_Updt_Person, p_DS_Del_Person );
											ELSE
													UPDATE DailySaving SET DS_ID = p_DS_ID, DS_Date = p_DS_Date, DS_FPAmount = p_DS_FPAmount, DS_SPAmount = p_DS_SPAmount, DS_TPAmount = p_DS_TPAmount, NotTaken = p_NotTaken, DS_Data = p_DS_Data, DS_InBankDate = p_DS_InBankDate, DS_Insrt_Person = p_DS_Insrt_Person, DS_Updt_Person = p_DS_Updt_Person, DS_Del_Person = p_DS_Del_Person;
											END IF;
										END";
                    using (MySqlCommand procCmd = new MySqlCommand(crtProc, mysqlConn))
                    {
                        procCmd.ExecuteNonQuery();
                    }
                    while (reader.Read())
                    {
                        using (MySqlCommand sqlCmd = new MySqlCommand("sp_dailySavingSync", mysqlConn))
                        {
                            sqlCmd.Parameters.AddWithValue("p_DS_ID", reader["DS_ID"]);
                            sqlCmd.Parameters.AddWithValue("p_DS_Date", reader["DS_Date"]);
                            sqlCmd.Parameters.AddWithValue("p_DS_FPAmount", reader["DS_FPAmount"]);
                            sqlCmd.Parameters.AddWithValue("p_DS_SPAmount", reader["DS_SPAmount"]);
                            sqlCmd.Parameters.AddWithValue("p_DS_TPAmount", reader["DS_TPAmount"]);
                            sqlCmd.Parameters.AddWithValue("p_NotTaken", reader["NotTaken"]);
                            sqlCmd.Parameters.AddWithValue("p_DS_Data", reader["DS_Data"]);
                            sqlCmd.Parameters.AddWithValue("p_DS_InBankDate", reader["DS_InBankDate"]);
                            sqlCmd.Parameters.AddWithValue("p_DS_Insrt_Person", reader["DS_Insrt_Person"]);
                            sqlCmd.Parameters.AddWithValue("p_DS_Updt_Person", reader["DS_Updt_Person"]);
                            sqlCmd.Parameters.AddWithValue("p_DS_Del_Person", reader["DS_Del_Person"]);
                            sqlCmd.ExecuteNonQuery();
                        }
                    }
                    mysqlConn.Close();
                }
                accConn.Close();
            }
        }
		private void sp_installmentSync()
		{
			using (OleDbConnection accConn = new OleDbConnection(connAcc))
			{
				accConn.Open();
				string selCom = "SELECT * FROM Installment";
				OleDbCommand command = new OleDbCommand(selCom, accConn);
				OleDbDataReader reader = command.ExecuteReader();
				using (MySqlConnection mysqlConn = new MySqlConnection(connMySql))
				{
					mysqlConn.Open();
					string crtProc = @"CREATE PROCEDURE IF NOT EXISTS sp_installmentSync ( IN p_I_ID varchar(250), IN p_I_Date datetime, IN p_Take_Total float, IN p_Take_Anot float, IN p_Take_Mine float, IN p_Take_Data varchar(250), IN p_InsPerMonth varchar(250), IN p_PerMonthPay varchar(250), IN p_InsPay float, IN p_InsPay_Date datetime, IN p_I_Insrt_Person varchar(250), IN p_I_Updt_Person varchar(250), IN p_I_Del_Person varchar(250) )
										BEGIN
											IF NOT EXISTS (
													SELECT * FROM information_schema.tables WHERE table_name = 'Installment'
												) THEN
													CREATE TABLE Installment ( I_ID NVARCHAR(250) NULL, I_Date datetime, Take_Total FLOAT DEFAULT 0, Take_Anot FLOAT DEFAULT 0, Take_Mine FLOAT DEFAULT 0, Take_Data NVARCHAR(250) NULL, InsPerMonth NVARCHAR(250) NULL, PerMonthPay NVARCHAR(250) NULL, InsPay FLOAT DEFAULT 0, InsPay_Date datetime, I_Insrt_Person NVARCHAR(250) NULL, I_Updt_Person NVARCHAR(250) NULL, I_Del_Person NVARCHAR(250) NULL );
												END IF;
												IF NOT EXISTS (
													SELECT * FROM Installment WHERE I_ID = p_I_ID
												) THEN         
													INSERT INTO Installment ( I_ID,I_Date,Take_Total,Take_Anot,Take_Mine,Take_Data,InsPerMonth,PerMonthPay,InsPay,InsPay_Date,I_Insrt_Person,I_Updt_Person,I_Del_Person ) 
																	 VALUES ( p_I_ID,p_I_Date,p_Take_Total,p_Take_Anot,p_Take_Mine,p_Take_Data,p_InsPerMonth,p_PerMonthPay,p_InsPay,p_InsPay_Date,p_I_Insrt_Person,p_I_Updt_Person,p_I_Del_Person );																
											ELSE
													UPDATE Installment SET I_ID = p_I_ID, I_Date = p_I_Date, Take_Total = p_Take_Total, Take_Anot = p_Take_Anot, Take_Mine = p_Take_Mine, Take_Data = p_Take_Data, InsPerMonth = p_InsPerMonth, PerMonthPay = p_PerMonthPay, InsPay = p_InsPay, InsPay_Date = p_InsPay_Date, I_Insrt_Person = p_I_Insrt_Person, I_Updt_Person = p_I_Updt_Person, I_Del_Person = p_I_Del_Person ;        
											END IF;
										END";
					using (MySqlCommand procCmd = new MySqlCommand(crtProc, mysqlConn))
					{
						procCmd.ExecuteNonQuery();
					}
					while (reader.Read())
					{
						using (MySqlCommand sqlCmd = new MySqlCommand("sp_installmentSync", mysqlConn))
						{
							sqlCmd.Parameters.AddWithValue("p_I_ID, ", reader["I_ID"]);
							sqlCmd.Parameters.AddWithValue("p_I_Date, ", reader["I_Date"]);
							sqlCmd.Parameters.AddWithValue("p_Take_Total,", reader["Take_Total"]);
							sqlCmd.Parameters.AddWithValue("p_Take_Anot,", reader["Take_Anot"]);
							sqlCmd.Parameters.AddWithValue("p_Take_Mine,", reader["Take_Mine"]);
							sqlCmd.Parameters.AddWithValue("p_Take_Data,", reader["Take_Data"]);
							sqlCmd.Parameters.AddWithValue("p_InsPerMonth,", reader["InsPerMonth"]);
							sqlCmd.Parameters.AddWithValue("p_PerMonthPay,", reader["PerMonthPay"]);
							sqlCmd.Parameters.AddWithValue("p_InsPay,", reader["InsPay"]);
							sqlCmd.Parameters.AddWithValue("p_InsPay_Date,", reader["InsPay_Date"]);
							sqlCmd.Parameters.AddWithValue("p_I_Insrt_Person,", reader["I_Insrt_Person"]);
							sqlCmd.Parameters.AddWithValue("p_I_Updt_Person,", reader["I_Updt_Person"]);
							sqlCmd.Parameters.AddWithValue("p_I_Del_Person", reader["I_Del_Person"]);
							sqlCmd.ExecuteNonQuery();
						}
					}
					mysqlConn.Close();
				}
				accConn.Close();
			}
		}
		private void sp_installmentPaySync()
		{
			//Work Later
		}
		private void sp_bikeInfoSync()
		{
			using (OleDbConnection accConn = new OleDbConnection(connAcc))
			{
				accConn.Open();
				string selCom = "SELECT * FROM BikeInfo";
				OleDbCommand command = new OleDbCommand(selCom, accConn);
				OleDbDataReader reader = command.ExecuteReader();
				using (MySqlConnection mysqlConn = new MySqlConnection(connMySql))
				{
					mysqlConn.Open();
					string crtProc = @"CREATE PROCEDURE IF NOT EXISTS sp_bikeInfoSync ( IN p_B_ID VARCHAR(250), IN p_B_Chng_Date DATE, IN p_B_KM_ODO VARCHAR(250), IN p_B_Mobile_Go VARCHAR(250), IN p_B_Next_ODO VARCHAR(250), IN p_B_Insrt_Person VARCHAR(250), IN p_B_Updt_Person VARCHAR(250) )
										BEGIN
											IF NOT EXISTS (
													SELECT * FROM information_schema.tables WHERE table_name = 'BikeInfo'
												) THEN
													CREATE TABLE BikeInfo ( B_ID NVARCHAR(250) NULL, B_Chng_Date DATETIME NULL, B_KM_ODO NVARCHAR(250) NULL, B_Mobile_Go NVARCHAR(250) NULL, B_Next_ODO NVARCHAR(250) NULL, B_Insrt_Person NVARCHAR(250) NULL, B_Updt_Person NVARCHAR(250) NULL );
												END IF;
												IF NOT EXISTS (
													SELECT * FROM BikeInfo WHERE B_ID = p_B_ID
												) THEN         
													INSERT INTO BikeInfo ( B_ID, B_Chng_Date, B_KM_ODO, B_Mobile_Go, B_Next_ODO, B_Insrt_Person, B_Updt_Person ) 
																  VALUES ( p_B_ID, p_B_Chng_Date, p_B_KM_ODO, p_B_Mobile_Go, p_B_Next_ODO, p_B_Insrt_Person, p_B_Updt_Person );																
											ELSE
													UPDATE BikeInfo SET B_ID = p_B_ID, B_Chng_Date = p_B_Chng_Date, B_KM_ODO = p_B_KM_ODO, B_Mobile_Go = p_B_Mobile_Go, B_Next_ODO = p_B_Next_ODO, B_Insrt_Person = p_B_Insrt_Person, B_Updt_Person = p_B_Updt_Person ;        
											END IF;
										END";
					using (MySqlCommand procCmd = new MySqlCommand(crtProc, mysqlConn))
					{
						procCmd.ExecuteNonQuery();
					}
					while (reader.Read())
					{
						using (MySqlCommand mysqlCmd = new MySqlCommand("sp_bikeInfoSync", mysqlConn))
						{
							mysqlCmd.CommandType = CommandType.StoredProcedure;
							mysqlCmd.Parameters.AddWithValue("p_B_ID", reader["B_ID"]);
							mysqlCmd.Parameters.AddWithValue("p_B_Chng_Date", reader["B_Chng_Date"]);
							mysqlCmd.Parameters.AddWithValue("p_B_KM_ODO", reader["B_KM_ODO"]);
							mysqlCmd.Parameters.AddWithValue("p_B_Mobile_Go", reader["B_Mobile_Go"]);
							mysqlCmd.Parameters.AddWithValue("p_B_Next_ODO", reader["B_Next_ODO"]);
							mysqlCmd.Parameters.AddWithValue("p_B_Insrt_Person", reader["B_Insrt_Person"]);
							mysqlCmd.Parameters.AddWithValue("p_B_Updt_Person", reader["B_Updt_Person"]);
							mysqlCmd.ExecuteNonQuery();
						}
					}
					mysqlConn.Close();
				}
				accConn.Close();
			}
		}
		private void sp_givenSync()
		{
			using (OleDbConnection accConn = new OleDbConnection(connAcc))
			{
				accConn.Open();
				string selCom = "SELECT * FROM Given";
				OleDbCommand command = new OleDbCommand(selCom, accConn);
				OleDbDataReader reader = command.ExecuteReader();
				using (MySqlConnection mysqlConn = new MySqlConnection(connMySql))
				{
					mysqlConn.Open();
					string crtProc = @"CREATE PROCEDURE IF NOT EXISTS sp_givenSync ( IN p_InGiven varchar(250), IN p_Total_Given float, IN p_Given_To varchar(250), IN p_ThroughBy_Given varchar(250), IN p_Given_Date datetime, IN p_Remarks_Given varchar(250), IN p_GDT_V varchar(250), IN p_GDT_V_Date datetime, IN p_DDT_V_Date datetime, IN p_G_Insrt_Person varchar(250), IN p_G_Updt_Person varchar(250), IN p_G_Del_Person varchar(250) )
										BEGIN
											IF NOT EXISTS (
													SELECT * FROM information_schema.tables WHERE table_name = 'BikeInfo'
												) THEN
													CREATE TABLE Given ( InGiven NVARCHAR(250) NULL, Total_Given FLOAT DEFAULT 0, Given_To NVARCHAR(250) NULL, ThroughBy_Given NVARCHAR(250) NULL, Given_Date datetime, Remarks_Given NVARCHAR(250) NULL, GDT_V NVARCHAR(250) NULL, GDT_V_Date datetime, DDT_V_Date datetime, G_Insrt_Person NVARCHAR(250) NULL, G_Updt_Person NVARCHAR(250) NULL, G_Del_Person NVARCHAR(250) NULL );
												END IF;
												IF NOT EXISTS (
													SELECT * FROM Given WHERE InGiven = p_InGiven
												) THEN         
													INSERT INTO Given ( InGiven,Total_Given,Given_To,ThroughBy_Give,Given_Date,Remarks_Given,GDT_V,GDT_V_Date,DDT_V_Date,G_Insrt_Person,G_Updt_Person,G_Del_Person ) 
															   VALUES ( p_InGiven,p_Total_Given,p_Given_To,p_ThroughBy_Give,p_Given_Date,p_Remarks_Given,p_GDT_V,p_GDT_V_Date,p_DDT_V_Date,p_G_Insrt_Person,p_G_Updt_Person,p_G_Del_Person );															
											ELSE
													UPDATE Given SET InGiven = p_InGiven, Total_Given = p_Total_Given, Given_To = p_Given_To, ThroughBy_Given = p_ThroughBy_Given, Given_Date = p_Given_Date, Remarks_Given = p_Remarks_Given, GDT_V = p_GDT_V, GDT_V_Date = p_GDT_V_Date, DDT_V_Date = p_DDT_V_Date, G_Insrt_Person = p_G_Insrt_Person, G_Updt_Person = p_G_Updt_Person, G_Del_Person = p_G_Del_Person ;        
											END IF;
										END";
					using (MySqlCommand procCmd = new MySqlCommand(crtProc, mysqlConn))
					{
						procCmd.ExecuteNonQuery();
					}
					while (reader.Read())
					{
						using (MySqlCommand mysqlCmd = new MySqlCommand("sp_givenSync", mysqlConn))
						{
							mysqlCmd.Parameters.AddWithValue("p_InGiven", reader["InGiven"]);
							mysqlCmd.Parameters.AddWithValue("p_Total_Given", reader["Total_Given"]);
							mysqlCmd.Parameters.AddWithValue("p_Given_To", reader["Given_To"]);
							mysqlCmd.Parameters.AddWithValue("p_ThroughBy_Give", reader["ThroughBy_Given"]);
							mysqlCmd.Parameters.AddWithValue("p_Given_Date", reader["Given_Date"]);
							mysqlCmd.Parameters.AddWithValue("p_Remarks_Given", reader["Remarks_Given"]);
							mysqlCmd.Parameters.AddWithValue("p_GDT_V", reader["GDT_V"]);
							mysqlCmd.Parameters.AddWithValue("p_GDT_V_Date", reader["GDT_V_Date"]);
							mysqlCmd.Parameters.AddWithValue("p_DDT_V_Date", reader["DDT_V_Date"]);
							mysqlCmd.Parameters.AddWithValue("p_G_Insrt_Person", reader["G_Insrt_Person"]);
							mysqlCmd.Parameters.AddWithValue("p_G_Updt_Person", reader["G_Updt_Person"]);
							mysqlCmd.Parameters.AddWithValue("p_G_Del_Person", reader["G_Del_Person"]);
							mysqlCmd.ExecuteNonQuery();
						}
					}
					mysqlConn.Close();
				}
				accConn.Close();
			}
		}
		private void sp_givenUpdtSync()
		{
			using (OleDbConnection accConn = new OleDbConnection(connAcc))
			{
				accConn.Open();
				string selCom = "SELECT * FROM GivenUpdt";
				OleDbCommand command = new OleDbCommand(selCom, accConn);
				OleDbDataReader reader = command.ExecuteReader();
				using (MySqlConnection mysqlConn = new MySqlConnection(connMySql))
				{
					mysqlConn.Open();
					string crtProc = @"CREATE PROCEDURE IF NOT EXISTS sp_givenUpdtSync ( IN p_InGiven varchar(250), IN p_Was_Given float, IN p_Now_Given float, IN p_Total_Given float, IN p_Given_To varchar(250), IN p_GDT_V_Date datetime )
										BEGIN
											IF NOT EXISTS (
													SELECT * FROM information_schema.tables WHERE table_name = 'GivenUpdt'
												) THEN
													CREATE TABLE GivenUpdt ( InGiven NVARCHAR(250) NULL, Was_Given FLOAT DEFAULT 0, Now_Given FLOAT DEFAULT 0, Total_Given FLOAT DEFAULT 0, Given_To NVARCHAR(250) NULL, GDT_V_Date datetime );
												END IF;
												IF NOT EXISTS (
													SELECT * FROM GivenUpdt WHERE InGiven = p_InGiven
												) THEN         
													INSERT INTO GivenUpdt ( InGiven,Was_Given,Now_Given,Total_Given,Given_To,GDT_V_Date )			
																   VALUES ( p_InGiven,p_Was_Given,p_Now_Given,p_Total_Given,p_Given_To,p_GDT_V_Date );															
											ELSE
													UPDATE GivenUpdt SET InGiven = p_InGiven, Was_Given = p_Was_Given, Now_Given = p_Now_Given, Total_Given = p_Total_Given, Given_To = p_Given_To, GDT_V_Date = p_GDT_V_Date ;        
											END IF;
										END";
					using (MySqlCommand procCmd = new MySqlCommand(crtProc, mysqlConn))
					{
						procCmd.ExecuteNonQuery();
					}
					while (reader.Read())
					{
						using (MySqlCommand mysqlCmd = new MySqlCommand("sp_givenupdtSync", mysqlConn))
						{
							mysqlCmd.Parameters.AddWithValue("p_InGiven", reader["InGiven"]);
							mysqlCmd.Parameters.AddWithValue("p_Was_Given", reader["Was_Given"]);
							mysqlCmd.Parameters.AddWithValue("p_Now_Given", reader["Now_Given"]);
							mysqlCmd.Parameters.AddWithValue("p_Total_Given", reader["Total_Given"]);
							mysqlCmd.Parameters.AddWithValue("p_Given_To", reader["Given_To"]);
							mysqlCmd.Parameters.AddWithValue("p_GDT_V_Date", reader["GDT_V_Date"]);
							mysqlCmd.ExecuteNonQuery();
						}
					}
					mysqlConn.Close();
				}
				accConn.Close();
			}
		}
		private void sp_tekenSync()
		{
			using (OleDbConnection accConn = new OleDbConnection(connAcc))
			{
				accConn.Open();
				string selCom = "SELECT * FROM Teken";
				OleDbCommand command = new OleDbCommand(selCom, accConn);
				OleDbDataReader reader = command.ExecuteReader();
				using (MySqlConnection mysqlConn = new MySqlConnection(connMySql))
				{
					mysqlConn.Open();
					string crtProc = @"CREATE PROCEDURE IF NOT EXISTS sp_tekenSync ( IN P_InTake varchar(250), IN P_Total_Take float, IN P_Take_To varchar(250), IN P_ThroughBy_Take varchar(250), IN P_Take_Date datetime, IN P_Remarks_Take varchar(250), IN P_TDT_V varchar(250), IN P_TDT_V_Date datetime, IN P_DDT_V_Date datetime, IN P_T_Insrt_Person varchar(250), IN P_T_Updt_Person varchar(250), IN P_T_Del_Person varchar(250) )
										BEGIN
											IF NOT EXISTS (
													SELECT * FROM information_schema.tables WHERE table_name = 'Teken'
												) THEN
													CREATE TABLE Teken ( InTake NVARCHAR(250) NULL, Total_Take FLOAT DEFAULT 0, Take_To NVARCHAR(250) NULL, ThroughBy_Take NVARCHAR(250) NULL, Take_Date datetime, Remarks_Take NVARCHAR(250) NULL, TDT_V NVARCHAR(250) NULL, TDT_V_Date datetime, DDT_V_Date datetime, T_Insrt_Person NVARCHAR(250) NULL, T_Updt_Person NVARCHAR(250) NULL, T_Del_Person NVARCHAR(250) NULL );
												END IF;
												IF NOT EXISTS (
													SELECT * FROM Teken WHERE InTake = p_InTake
												) THEN         
													INSERT INTO Teken ( InTake,Total_Take,Take_To,ThroughBy_Take,Take_Date,Remarks_Take,TDT_V,TDT_V_Date,DDT_V_Date,T_Insrt_Person,T_Updt_Person,T_Del_Person )			
															   VALUES ( p_InTake,p_Total_Take,p_Take_To,p_ThroughBy_Take,p_Take_Date,p_Remarks_Take,p_TDT_V,p_TDT_V_Date,p_DDT_V_Date,p_T_Insrt_Person,p_T_Updt_Person,p_T_Del_Person );															
											ELSE
													UPDATE Teken SET InTake = p_InTake, Total_Take = p_Total_Take, Take_To = p_Take_To, ThroughBy_Take = p_ThroughBy_Take, Take_Date = p_Take_Date, Remarks_Take = p_Remarks_Take, TDT_V = p_TDT_V, TDT_V_Date = p_TDT_V_Date, DDT_V_Date = p_DDT_V_Date, T_Insrt_Person = p_T_Insrt_Person, T_Updt_Person  = p_T_Updt_Person, T_Del_Person = p_T_Del_Person ;        
											END IF;
										END";
					using (MySqlCommand procCmd = new MySqlCommand(crtProc, mysqlConn))
					{
						procCmd.ExecuteNonQuery();
					}
					while (reader.Read())
					{
						using (MySqlCommand mysqlCmd = new MySqlCommand("sp_takenSync", mysqlConn))
						{
							mysqlCmd.Parameters.AddWithValue("p_InTake", reader["InTake"]);
							mysqlCmd.Parameters.AddWithValue("p_Total_Take", reader["Total_Take"]);
							mysqlCmd.Parameters.AddWithValue("p_Take_To", reader["Take_To"]);
							mysqlCmd.Parameters.AddWithValue("p_ThroughBy_Take", reader["ThroughBy_Take"]);
							mysqlCmd.Parameters.AddWithValue("p_Take_Date", reader["Take_Date"]);
							mysqlCmd.Parameters.AddWithValue("p_Remarks_Take", reader["Remarks_Take"]);
							mysqlCmd.Parameters.AddWithValue("p_TDT_V", reader["TDT_V"]);
							mysqlCmd.Parameters.AddWithValue("p_TDT_V_Date", reader["TDT_V_Date"]);
							mysqlCmd.Parameters.AddWithValue("p_DDT_V_Date", reader["DDT_V_Date"]);
							mysqlCmd.Parameters.AddWithValue("p_T_Insrt_Person", reader["T_Insrt_Person"]);
							mysqlCmd.Parameters.AddWithValue("p_T_Updt_Person", reader["T_Updt_Person"]);
							mysqlCmd.Parameters.AddWithValue("p_T_Del_Person", reader["T_Del_Person"]);
							mysqlCmd.ExecuteNonQuery();
						}
					}
					mysqlConn.Close();
				}
				accConn.Close();
			}
		}
		private void sp_tekenUpdtSync()
		{
			using (OleDbConnection accConn = new OleDbConnection(connAcc))
			{
				accConn.Open();
				string selCom = "SELECT * FROM TekenUpdt";
				OleDbCommand command = new OleDbCommand(selCom, accConn);
				OleDbDataReader reader = command.ExecuteReader();
				using (MySqlConnection mysqlConn = new MySqlConnection(connMySql))
				{
					mysqlConn.Open();
					string crtProc = @"CREATE PROCEDURE IF NOT EXISTS sp_tekenUpdtSync ( IN p_InTake varchar(250), IN p_Was_Take float, IN p_Now_Take float, IN p_Total_Take float, IN p_Take_To varchar(250), IN p_TDT_V_Date datetime )
										BEGIN
											IF NOT EXISTS (
													SELECT * FROM information_schema.tables WHERE table_name = 'TekenUpdt'
												) THEN
													CREATE TABLE TekenUpdt ( InTake NVARCHAR(250) NULL, Was_Take FLOAT DEFAULT 0, Now_Take FLOAT DEFAULT 0, Total_Take FLOAT DEFAULT 0, Take_To NVARCHAR(250) NULL, TDT_V_Date datetime );
												END IF;
												IF NOT EXISTS (
													SELECT * FROM TekenUpdt WHERE InTake = p_InTake
												) THEN         
													INSERT INTO TekenUpdt ( InTake,Total_Take,Take_To,ThroughBy_Take,Take_Date,Remarks_Take,TDT_V,TDT_V_Date,DDT_V_Date,T_Insrt_Person,T_Updt_Person,T_Del_Person )			
																   VALUES ( p_InTake,p_Total_Take,p_Take_To,p_ThroughBy_Take,p_Take_Date,p_Remarks_Take,p_TDT_V,p_TDT_V_Date,p_DDT_V_Date,p_T_Insrt_Person,p_T_Updt_Person,p_T_Del_Person );															
											ELSE
													UPDATE TekenUpdt SET InTake = p_InTake, Was_Take = p_Was_Take, Now_Take = p_Now_Take, Total_Take = p_Total_Take, Take_To = p_Take_To, TDT_V_Date = p_TDT_V_Date ;        
											END IF;
										END";
					using (MySqlCommand procCmd = new MySqlCommand(crtProc, mysqlConn))
					{
						procCmd.ExecuteNonQuery();
					}
					while (reader.Read())
					{
						using (MySqlCommand mysqlCmd = new MySqlCommand("sp_takenUpdt", mysqlConn))
						{
							mysqlCmd.Parameters.AddWithValue("p_InTake", reader["InTake"]);
							mysqlCmd.Parameters.AddWithValue("p_Was_Take", reader["Was_Take"]);
							mysqlCmd.Parameters.AddWithValue("p_Now_Take", reader["Now_Take"]);
							mysqlCmd.Parameters.AddWithValue("p_Total_Take", reader["Total_Take"]);
							mysqlCmd.Parameters.AddWithValue("p_Take_To", reader["Take_To"]);
							mysqlCmd.Parameters.AddWithValue("p_TDT_V_Date", reader["TDT_V_Date"]);
							mysqlCmd.ExecuteNonQuery();
						}
					}
					mysqlConn.Close();
				}
				accConn.Close();
			}
		}
		private void sp_expenseSync()
		{
			using (OleDbConnection accConn = new OleDbConnection(connAcc))
			{
				accConn.Open();
				string selCom = "SELECT * FROM TariffAmt";
				OleDbCommand command = new OleDbCommand(selCom, accConn);
				OleDbDataReader reader = command.ExecuteReader();
				using (MySqlConnection mysqlConn = new MySqlConnection(connMySql))
				{
					mysqlConn.Open();
					string crtProc = @"CREATE PROCEDURE IF NOT EXISTS sp_tariffAmtSync ( IN p_InExpense varchar(250), IN p_Expense_Amount float, IN p_Expense_To varchar(250), IN p_ThroughBy_Expense varchar(250), IN p_Expense_Date datetime, IN p_Remarks_Expense varchar(250), IN p_EDT_V varchar(250), IN p_EDT_V_Date datetime, IN p_DDT_V_Date datetime, IN p_E_Insrt_Person varchar(250), IN p_E_Updt_Person varchar(250), IN p_E_Del_Person varchar(250) )
										BEGIN
											IF NOT EXISTS (
													SELECT * FROM information_schema.tables WHERE table_name = 'TariffAmt'
												) THEN
													CREATE TABLE TariffAmt ( InExpense NVARCHAR(250) NULL, Expense_Amount FLOAT DEFAULT 0, Expense_To NVARCHAR(250) NULL, ThroughBy_Expense NVARCHAR(250) NULL, Expense_Date datetime, Remarks_Expense NVARCHAR(250) NULL, EDT_V NVARCHAR(250) NULL, EDT_V_Date datetime, DDT_V_Date datetime, E_Insrt_Person NVARCHAR(250) NULL, E_Updt_Person NVARCHAR(250) NULL, E_Del_Person NVARCHAR(250) NULL );
												END IF;
												IF NOT EXISTS (
													SELECT * FROM TariffAmt WHERE InExpense = p_InExpense
												) THEN         
													INSERT INTO TariffAmt ( InExpense,Expense_Amount,Expense_To,ThroughBy_Expense,Expense_Date,Remarks_Expense,EDT_V,EDT_V_Date,DDT_V_Date,E_Insrt_Person,E_Updt_Person,E_Del_Person )			
																   VALUES ( p_InExpense,p_Expense_Amount,p_Expense_To,p_ThroughBy_Expense,p_Expense_Date,p_Remarks_Expense,p_EDT_V,p_EDT_V_Date,p_DDT_V_Date,p_E_Insrt_Person,p_E_Updt_Person,p_E_Del_Person );															
											ELSE
													UPDATE TariffAmt SET InExpense = p_InExpense, Expense_Amount = p_Expense_Amount, Expense_To = p_Expense_To, ThroughBy_Expense = p_ThroughBy_Expense, Expense_Date = p_Expense_Date, Remarks_Expense = p_Remarks_Expense, EDT_V = p_EDT_V, EDT_V_Date = p_EDT_V_Date, DDT_V_Date = p_DDT_V_Date, E_Insrt_Person = p_E_Insrt_Person, E_Updt_Person = p_E_Updt_Person, E_Del_Person = p_E_Del_Person ;        
											END IF;
										END";
					using (MySqlCommand procCmd = new MySqlCommand(crtProc, mysqlConn))
					{
						procCmd.ExecuteNonQuery();
					}
					while (reader.Read())
					{
						using (MySqlCommand mysqlCmd = new MySqlCommand("sp_tariffAmtSync", mysqlConn))
						{
							mysqlCmd.Parameters.AddWithValue("p_InExpense", reader["InExpense"]);
							mysqlCmd.Parameters.AddWithValue("p_Expense_Amount", reader["Expense_Amount"]);
							mysqlCmd.Parameters.AddWithValue("p_Expense_To", reader["Expense_To"]);
							mysqlCmd.Parameters.AddWithValue("p_ThroughBy_Expense", reader["ThroughBy_Expense"]);
							mysqlCmd.Parameters.AddWithValue("p_Expense_Date", reader["Expense_Date"]);
							mysqlCmd.Parameters.AddWithValue("p_Remarks_Expense", reader["Remarks_Expense"]);
							mysqlCmd.Parameters.AddWithValue("p_EDT_V", reader["EDT_V"]);
							mysqlCmd.Parameters.AddWithValue("p_EDT_V_Date", reader["EDT_V_Date"]);
							mysqlCmd.Parameters.AddWithValue("p_DDT_V_Date", reader["DDT_V_Date"]);
							mysqlCmd.Parameters.AddWithValue("p_E_Insrt_Person", reader["E_Insrt_Person"]);
							mysqlCmd.Parameters.AddWithValue("p_E_Updt_Person", reader["E_Updt_Person"]);
							mysqlCmd.Parameters.AddWithValue("p_E_Del_Person", reader["E_Del_Person"]);
							mysqlCmd.ExecuteNonQuery();
						}
					}
					mysqlConn.Close();
				}
				accConn.Close();
			}
		}
		private void sp_expenseUpdtSync()
		{
			using (OleDbConnection accConn = new OleDbConnection(connAcc))
			{
				accConn.Open();
				string selCom = "SELECT * FROM TariffAmtUpdt";
				OleDbCommand command = new OleDbCommand(selCom, accConn);
				OleDbDataReader reader = command.ExecuteReader();
				using (MySqlConnection mysqlConn = new MySqlConnection(connMySql))
				{
					mysqlConn.Open();
					string crtProc = @"CREATE PROCEDURE IF NOT EXISTS sp_tariffAmtUpdtSync ( IN p_InTake varchar(250), IN p_Was_Take float, IN p_Now_Take float, IN p_Total_Take float, IN p_Take_To varchar(250), IN p_TDT_V_Date datetime )
										BEGIN
											IF NOT EXISTS (
													SELECT * FROM information_schema.tables WHERE table_name = 'TariffAmtUpdt'
												) THEN
													CREATE TABLE TariffAmtUpdt ( InTake NVARCHAR(250) NULL, Was_Take FLOAT DEFAULT 0, Now_Take FLOAT DEFAULT 0, Total_Take FLOAT DEFAULT 0, Take_To NVARCHAR(250) NULL, TDT_V_Date datetime );
												END IF;
												IF NOT EXISTS (
													SELECT * FROM TariffAmtUpdt WHERE InTake = p_InTake
												) THEN         
													INSERT INTO TariffAmtUpdt ( InExpense,Was_Expense,Now_Expense,Expense_Amount,Expense_To,EDT_V_Date )			
																	   VALUES ( p_InTake,p_Was_Take,p_Now_Take,p_Total_Take,p_Take_To,p_TDT_V_Date );															
											ELSE
													UPDATE TariffAmtUpdt SET InTake = p_InTake, Was_Take = p_Was_Take, Now_Take = p_Now_Take, Total_Take = p_Total_Take, Take_To = p_Take_To, TDT_V_Date = p_TDT_V_Date ;        
											END IF;
										END";
					using (MySqlCommand procCmd = new MySqlCommand(crtProc, mysqlConn))
					{
						procCmd.ExecuteNonQuery();
					}
					while (reader.Read())
					{
						using (MySqlCommand mysqlCmd = new MySqlCommand("sp_tariffAmtUpdtSync", mysqlConn))
						{
							mysqlCmd.Parameters.AddWithValue("p_InExpense", reader["InExpense"]);
							mysqlCmd.Parameters.AddWithValue("p_Was_Expense", reader["Was_Expense"]);
							mysqlCmd.Parameters.AddWithValue("p_Now_Expense", reader["Now_Expense"]);
							mysqlCmd.Parameters.AddWithValue("p_Expense_Amount", reader["Expense_Amount"]);
							mysqlCmd.Parameters.AddWithValue("p_Expense_To", reader["Expense_To"]);
							mysqlCmd.Parameters.AddWithValue("p_EDT_V_Date", reader["EDT_V_Date"]);
							mysqlCmd.ExecuteNonQuery();
						}
					}
					mysqlConn.Close();
				}
				accConn.Close();
			}
		}
		private void sp_savingSync()
		{
			using (OleDbConnection accConn = new OleDbConnection(connAcc))
			{
				accConn.Open();
				string selCom = "SELECT * FROM Saving";
				OleDbCommand command = new OleDbCommand(selCom, accConn);
				OleDbDataReader reader = command.ExecuteReader();
				using (MySqlConnection mysqlConn = new MySqlConnection(connMySql))
				{
					mysqlConn.Open();
					string crtProc = @"CREATE PROCEDURE IF NOT EXISTS sp_savingSync ( IN p_Saving_Amount float, IN p_Saving_To varchar(250), IN p_ThroughBy_Saving varchar(250), IN p_Saving_Date datetime, IN p_Remarks_Saving varchar(250), IN p_SDT_V varchar(250), IN p_SDT_V_Date datetime, IN p_DDT_V_Date datetime, IN p_Saving_Bank varchar(250), IN p_S_Insrt_Person varchar(250), IN p_S_Updt_Person varchar(250), IN p_S_Del_Person varchar(250) )
										BEGIN
											IF NOT EXISTS (
													SELECT * FROM information_schema.tables WHERE table_name = 'Saving'
												) THEN
													CREATE TABLE Saving ( InSaving NVARCHAR(250) NULL, Saving_Amount float, Saving_To NVARCHAR(250) NULL, ThroughBy_Saving NVARCHAR(250) NULL, Saving_Date datetime, Remarks_Saving NVARCHAR(250) NULL, SDT_V NVARCHAR(250) NULL, SDT_V_Date datetime, DDT_V_Date datetime, Saving_Bank NVARCHAR(250) NULL, S_Insrt_Person NVARCHAR(250) NULL, S_Updt_Person NVARCHAR(250) NULL, S_Del_Person NVARCHAR(250) NULL );
												END IF;
												IF NOT EXISTS (
													SELECT * FROM Saving WHERE InSaving = p_InSaving
												) THEN         
													INSERT INTO Saving ( InSaving,Saving_Amount,Saving_To,ThroughBy_Saving,Saving_Date,Remarks_Saving,SDT_V,SDT_V_Date,DDT_V_Date,Saving_Bank,S_Insrt_Person,S_Updt_Person,S_Del_Person )			
																VALUES ( p_InSaving,p_Saving_Amount,p_Saving_To,p_ThroughBy_Saving,p_Saving_Date,p_Remarks_Saving,p_SDT_V,p_SDT_V_Date,p_DDT_V_Date,p_Saving_Bank,p_S_Insrt_Person,p_S_Updt_Person,p_S_Del_Person );															
											ELSE
													UPDATE Saving SET InSaving = p_InSaving, Saving_Amount = p_Saving_Amount, Saving_To = p_Saving_To, ThroughBy_Saving = p_ThroughBy_Saving, Saving_Date = p_Saving_Date, Remarks_Saving = p_Remarks_Saving, SDT_V = p_SDT_V, SDT_V_Date = p_SDT_V_Date, DDT_V_Date = p_DDT_V_Date, Saving_Bank = p_Saving_Bank, S_Insrt_Person = p_S_Insrt_Person, S_Updt_Person = p_S_Updt_Person, S_Del_Person = p_S_Del_Person ;        
											END IF;
										END";
					using (MySqlCommand procCmd = new MySqlCommand(crtProc, mysqlConn))
					{
						procCmd.ExecuteNonQuery();
					}
					while (reader.Read())
					{
						using (MySqlCommand mysqlCmd = new MySqlCommand("sp_savingSync", mysqlConn))
						{
							mysqlCmd.Parameters.AddWithValue("p_InSaving", reader["InSaving"]);
							mysqlCmd.Parameters.AddWithValue("p_Saving_Amount", reader["Saving_Amount"]);
							mysqlCmd.Parameters.AddWithValue("p_Saving_To", reader["Saving_To"]);
							mysqlCmd.Parameters.AddWithValue("p_ThroughBy_Saving", reader["ThroughBy_Saving"]);
							mysqlCmd.Parameters.AddWithValue("p_Saving_Date", reader["Saving_Date"]);
							mysqlCmd.Parameters.AddWithValue("p_Remarks_Saving", reader["Remarks_Saving"]);
							mysqlCmd.Parameters.AddWithValue("p_SDT_V", reader["SDT_V"]);
							mysqlCmd.Parameters.AddWithValue("p_SDT_V_Date", reader["SDT_V_Date"]);
							mysqlCmd.Parameters.AddWithValue("p_DDT_V_Date", reader["DDT_V_Date"]);
							mysqlCmd.Parameters.AddWithValue("p_Saving_Bank", reader["Saving_Bank"]);
							mysqlCmd.Parameters.AddWithValue("p_S_Insrt_Person", reader["S_Insrt_Person"]);
							mysqlCmd.Parameters.AddWithValue("p_S_Updt_Person", reader["S_Updt_Person"]);
							mysqlCmd.Parameters.AddWithValue("p_S_Del_Person", reader["S_Del_Person"]);
							mysqlCmd.ExecuteNonQuery();
						}
					}
					mysqlConn.Close();
				}
				accConn.Close();
			}
		}
		private void sp_savingUpdtSync()
		{
			using (OleDbConnection accConn = new OleDbConnection(connAcc))
			{
				accConn.Open();
				string selCom = "SELECT * FROM SavingUpdt";
				OleDbCommand command = new OleDbCommand(selCom, accConn);
				OleDbDataReader reader = command.ExecuteReader();
				using (MySqlConnection mysqlConn = new MySqlConnection(connMySql))
				{
					mysqlConn.Open();
					string crtProc = @"CREATE PROCEDURE IF NOT EXISTS sp_savingUpdtSync ( IN p_InSaving varchar(250), IN p_Was_Saving float, IN p_Now_Saving float, IN p_Saving_Amount float, IN p_Saving_To varchar(250), IN p_SDT_V_Date datetime )
										BEGIN
											IF NOT EXISTS (
													SELECT * FROM information_schema.tables WHERE table_name = 'SavingUpdt'
												) THEN
													CREATE TABLE SavingUpdt ( InSaving NVARCHAR(250) NULL, Was_Saving FLOAT DEFAULT 0, Now_Saving FLOAT DEFAULT 0, Saving_Amount FLOAT DEFAULT 0, Saving_To NVARCHAR(250) NULL, SDT_V_Date datetime );
												END IF;
												IF NOT EXISTS (
													SELECT * FROM SavingUpdt WHERE InSaving = p_InSaving
												) THEN         
													INSERT INTO SavingUpdt ( InSaving,Was_Saving,Now_Saving,Saving_Amount,Saving_To,SDT_V_Date )			
																	VALUES ( p_InSaving,p_Was_Saving,p_Now_Saving,p_Saving_Amount,p_Saving_To,p_SDT_V_Date );															
											ELSE
													UPDATE SavingUpdt SET InSaving = p_InSaving, Was_Saving = p_Was_Saving, Now_Saving = p_Now_Saving, Saving_Amount = p_Saving_Amount, Saving_To = p_Saving_To, SDT_V_Date = p_SDT_V_Date ;        
											END IF;
										END";
					using (MySqlCommand procCmd = new MySqlCommand(crtProc, mysqlConn))
					{
						procCmd.ExecuteNonQuery();
					}
					while (reader.Read())
					{
						using (MySqlCommand mysqlCmd = new MySqlCommand("sp_savingUpdtSync", mysqlConn))
						{
							mysqlCmd.Parameters.AddWithValue("p_InSaving", reader["InSaving"]);
							mysqlCmd.Parameters.AddWithValue("p_Was_Saving", reader["Was_Saving"]);
							mysqlCmd.Parameters.AddWithValue("p_Now_Saving", reader["Now_Saving"]);
							mysqlCmd.Parameters.AddWithValue("p_Saving_Amount", reader["Saving_Amount"]);
							mysqlCmd.Parameters.AddWithValue("p_Saving_To", reader["Saving_To"]);
							mysqlCmd.Parameters.AddWithValue("p_SDT_V_Date", reader["SDT_V_Date"]);
							mysqlCmd.ExecuteNonQuery();
						}
					}
					mysqlConn.Close();
				}
				accConn.Close();
			}
		}
		private void sp_unratedSync()
		{
			using (OleDbConnection accConn = new OleDbConnection(connAcc))
			{
				accConn.Open();
				string selCom = "SELECT * FROM Unrated";
				OleDbCommand command = new OleDbCommand(selCom, accConn);
				OleDbDataReader reader = command.ExecuteReader();
				using (MySqlConnection mysqlConn = new MySqlConnection(connMySql))
				{
					mysqlConn.Open();
					string crtProc = @"CREATE PROCEDURE IF NOT EXISTS sp_undatedSync ( IN p_InUnrated varchar(250), IN p_Unrated_Amount float, IN p_Unrated_To varchar(250), IN p_ThroughBy_Unrated varchar(250), IN p_Unrated_Date datetime, IN p_Remarks_Unrated varchar(250), IN p_UDT_V varchar(250), IN p_UDT_V_Date datetime, IN p_DDT_V_Date datetime, IN p_U_Insrt_Person varchar(250), IN p_U_Updt_Person varchar(250), IN p_U_Del_Person varchar(250) )
										BEGIN
											IF NOT EXISTS (
													SELECT * FROM information_schema.tables WHERE table_name = 'Unrated'
												) THEN
													CREATE TABLE Unrated ( InUnrated NVARCHAR(250) NULL, Unrated_Amount FLOAT DEFAULT 0, Unrated_To NVARCHAR(250) NULL, ThroughBy_Unrated NVARCHAR(250) NULL, Unrated_Date datetime, Remarks_Unrated NVARCHAR(250) NULL, UDT_V NVARCHAR(250) NULL, UDT_V_Date datetime, DDT_V_Date datetime, U_Insrt_Person NVARCHAR(250) NULL, U_Updt_Person NVARCHAR(250) NULL, U_Del_Person NVARCHAR(250) NULL );
												END IF;
												IF NOT EXISTS (
													SELECT * FROM Undated WHERE InUnrated = p_InUnrated
												) THEN         
													INSERT INTO Undated ( InUnrated,Unrated_Amount,Unrated_To,ThroughBy_Unrated,Unrated_Date,Remarks_Unrated,UDT_V,UDT_V_Date,DDT_V_Date,U_Insrt_Person,U_Updt_Person,U_Del_Person )			
																 VALUES ( p_InUnrated,p_Unrated_Amount,p_Unrated_To,p_ThroughBy_Unrated,p_Unrated_Date,p_Remarks_Unrated,p_UDT_V,p_UDT_V_Date,p_DDT_V_Date,p_U_Insrt_Person,p_U_Updt_Person,p_U_Del_Person );															
											ELSE
													UPDATE Unrated SET InUnrated = p_InUnrated, Unrated_Amount = p_Unrated_Amount, Unrated_To = p_Unrated_To, ThroughBy_Unrated = p_ThroughBy_Unrated, Unrated_Date = p_Unrated_Date, Remarks_Unrated = p_Remarks_Unrated, UDT_V = p_UDT_V, UDT_V_Date = p_UDT_V_Date, DDT_V_Date = p_DDT_V_Date, U_Insrt_Person = p_U_Insrt_Person, U_Updt_Person = p_U_Updt_Person, U_Del_Person = p_U_Del_Person ;        
											END IF;
										END";
					using (MySqlCommand procCmd = new MySqlCommand(crtProc, mysqlConn))
					{
						procCmd.ExecuteNonQuery();
					}
					while (reader.Read())
					{
						using (MySqlCommand mysqlCmd = new MySqlCommand("sp_unratedSync", mysqlConn))
						{
							mysqlCmd.Parameters.AddWithValue("p_InUnrated", reader["InUnrated"]);
							mysqlCmd.Parameters.AddWithValue("p_Unrated_Amount", reader["Unrated_Amount"]);
							mysqlCmd.Parameters.AddWithValue("p_Unrated_To", reader["Unrated_To"]);
							mysqlCmd.Parameters.AddWithValue("p_ThroughBy_Unrated", reader["ThroughBy_Unrated"]);
							mysqlCmd.Parameters.AddWithValue("p_Unrated_Date", reader["Unrated_Date"]);
							mysqlCmd.Parameters.AddWithValue("p_Remarks_Unrated", reader["Remarks_Unrated"]);
							mysqlCmd.Parameters.AddWithValue("p_UDT_V", reader["UDT_V"]);
							mysqlCmd.Parameters.AddWithValue("p_UDT_V_Date", reader["UDT_V_Date"]);
							mysqlCmd.Parameters.AddWithValue("p_DDT_V_Date", reader["DDT_V_Date"]);
							mysqlCmd.Parameters.AddWithValue("p_U_Insrt_Person", reader["U_Insrt_Person"]);
							mysqlCmd.Parameters.AddWithValue("p_U_Updt_Person", reader["U_Updt_Person"]);
							mysqlCmd.Parameters.AddWithValue("p_U_Del_Person", reader["U_Del_Person"]);
							mysqlCmd.ExecuteNonQuery();
						}
					}
					mysqlConn.Close();
				}
				accConn.Close();
			}
		}
		private void sp_unratedUpdtSync()
		{
			using (OleDbConnection accConn = new OleDbConnection(connAcc))
			{
				accConn.Open();
				string selCom = "SELECT * FROM UnratedUpdt";
				OleDbCommand command = new OleDbCommand(selCom, accConn);
				OleDbDataReader reader = command.ExecuteReader();
				using (MySqlConnection mysqlConn = new MySqlConnection(connMySql))
				{
					mysqlConn.Open();
					string crtProc = @"CREATE PROCEDURE IF NOT EXISTS sp_undatedUpdtSync ( IN p_InUnrated varchar(250), IN p_Was_Unrated float, IN p_Now_Unrated float, IN p_Unrated_Amount float, IN p_Unrated_To varchar(250), IN p_UDT_V_Date datetime )
										BEGIN
											IF NOT EXISTS (
													SELECT * FROM information_schema.tables WHERE table_name = 'UnratedUpdt'
												) THEN
													CREATE TABLE UnratedUpdt ( InUnrated NVARCHAR(250) NULL, Was_Unrated FLOAT DEFAULT 0, Now_Unrated FLOAT DEFAULT 0, Unrated_Amount FLOAT DEFAULT 0, Unrated_To NVARCHAR(250) NULL, UDT_V_Date datetime );
												END IF;
												IF NOT EXISTS (
													SELECT * FROM UndatedUpdt WHERE InUnrated = p_InUnrated
												) THEN         
													INSERT INTO UndatedUpdt ( InUnrated,Was_Unrated,Now_Unrated,Unrated_Amount,Unrated_To,UDT_V_Date )			
																	 VALUES ( p_InUnrated,p_Was_Unrated,p_Now_Unrated,p_Unrated_Amount,p_Unrated_To,p_UDT_V_Date );															
											ELSE
													UPDATE UnratedUpdt SET InUnrated = p_InUnrated, Was_Unrated = p_Was_Unrated, Now_Unrated = p_Now_Unrated, Unrated_Amount = p_Unrated_Amount, Unrated_To = p_Unrated_To, UDT_V_Date = p_UDT_V_Date ;        
											END IF;
										END";
					using (MySqlCommand procCmd = new MySqlCommand(crtProc, mysqlConn))
					{
						procCmd.ExecuteNonQuery();
					}
					while (reader.Read())
					{
						using (MySqlCommand mysqlCmd = new MySqlCommand("sp_unratedUpdtSync", mysqlConn))
						{
							mysqlCmd.Parameters.AddWithValue("p_InUnrated", reader["InUnrated"]);
							mysqlCmd.Parameters.AddWithValue("p_Was_Unrated", reader["Was_Unrated"]);
							mysqlCmd.Parameters.AddWithValue("p_Now_Unrated", reader["Now_Unrated"]);
							mysqlCmd.Parameters.AddWithValue("p_Unrated_Amount", reader["Unrated_Amount"]);
							mysqlCmd.Parameters.AddWithValue("p_Unrated_To", reader["Unrated_To"]);
							mysqlCmd.Parameters.AddWithValue("p_UDT_V_Date", reader["UDT_V_Date"]);
							mysqlCmd.ExecuteNonQuery();
						}
					}
					mysqlConn.Close();
				}
				accConn.Close();
			}
		}
		private void sp_dailySync()
		{
			using (OleDbConnection accConn = new OleDbConnection(connAcc))
			{
				accConn.Open();
				string selCom = "SELECT * FROM Daily";
				OleDbCommand command = new OleDbCommand(selCom, accConn);
				OleDbDataReader reader = command.ExecuteReader();
				using (MySqlConnection mysqlConn = new MySqlConnection(connMySql))
				{
					mysqlConn.Open();
					string crtProc = @"CREATE PROCEDURE IF NOT EXISTS sp_dailySync ( IN p_D_ID varchar(250), IN p_D_Date datetime, IN p_D_FPAmount float, IN p_D_SPAmount float, IN p_NotTaken float, IN p_D_Data varchar(250), IN p_TakenDate datetime, IN p_D_Insrt_Person varchar(250), IN p_D_Updt_Person varchar(250), IN p_D_Del_Person varchar(250) )
										BEGIN
											IF NOT EXISTS (
													SELECT * FROM information_schema.tables WHERE table_name = 'Daily'
												) THEN
													CREATE TABLE Daily ( D_ID NVARCHAR(250) NULL, D_Date datetime, D_FPAmount FLOAT DEFAULT 0, D_SPAmount FLOAT DEFAULT 0, NotTaken FLOAT DEFAULT 0, D_Data NVARCHAR(250) NULL, TakenDate datetime, D_Insrt_Person NVARCHAR(250) NULL, D_Updt_Person NVARCHAR(250) NULL, D_Del_Person NVARCHAR(250) NULL );
												END IF;
												IF NOT EXISTS (
													SELECT * FROM Daily WHERE D_ID = p_D_ID
												) THEN         
													INSERT INTO Daily ( D_ID,D_Date,D_FPAmount,D_SPAmount,NotTaken,D_Data,TakenDate,D_Insrt_Person,D_Updt_Person,D_Del_Person )			
															   VALUES ( p_D_ID,p_D_Date,p_D_FPAmount,p_D_SPAmount,p_NotTaken,p_D_Data,p_TakenDate,p_D_Insrt_Person,p_D_Updt_Person,p_D_Del_Person);															
											ELSE
													UPDATE Daily SET D_ID = p_D_ID, D_Date = p_D_Date, D_FPAmount = p_D_FPAmount, D_SPAmount = p_D_SPAmount, NotTaken = p_NotTaken, D_Data = p_D_Data, TakenDate = p_TakenDate, D_Insrt_Person = p_D_Insrt_Person, D_Updt_Person = p_D_Updt_Person, D_Del_Person = p_D_Del_Person ;        
											END IF;
										END";
					using (MySqlCommand procCmd = new MySqlCommand(crtProc, mysqlConn))
					{
						procCmd.ExecuteNonQuery();
					}
					while (reader.Read())
					{
						using (MySqlCommand mysqlCmd = new MySqlCommand("sp_dailySync", mysqlConn))
						{
							mysqlCmd.Parameters.AddWithValue("p_D_ID", reader["D_ID"]);
							mysqlCmd.Parameters.AddWithValue("p_D_Date", reader["D_Date"]);
							mysqlCmd.Parameters.AddWithValue("p_D_FPAmount", reader["D_FPAmount"]);
							mysqlCmd.Parameters.AddWithValue("p_D_SPAmount", reader["D_SPAmount"]);
							mysqlCmd.Parameters.AddWithValue("p_NotTaken", reader["NotTaken"]);
							mysqlCmd.Parameters.AddWithValue("p_D_Data", reader["D_Data"]);
							mysqlCmd.Parameters.AddWithValue("p_TakenDate", reader["TakenDate"]);
							mysqlCmd.Parameters.AddWithValue("p_D_Insrt_Person", reader["D_Insrt_Person"]);
							mysqlCmd.Parameters.AddWithValue("p_D_Updt_Person", reader["D_Updt_Person"]);
							mysqlCmd.Parameters.AddWithValue("p_D_Del_Person", reader["D_Del_Person"]);
							mysqlCmd.ExecuteNonQuery();
						}
					}
					mysqlConn.Close();
				}
				accConn.Close();
			}
		}
		private void sp_dailyCutSync()
		{
			using (OleDbConnection accConn = new OleDbConnection(connAcc))
			{
				accConn.Open();
				string selCom = "SELECT * FROM DailyCut";
				OleDbCommand command = new OleDbCommand(selCom, accConn);
				OleDbDataReader reader = command.ExecuteReader();
				using (MySqlConnection mysqlConn = new MySqlConnection(connMySql))
				{
					mysqlConn.Open();
					string crtProc = @"CREATE PROCEDURE IF NOT EXISTS sp_dailyCutSync ( IN p_C_ID varchar(250), IN p_C_Date datetime, IN p_C_Amount float, IN p_C_Insrt_Person varchar(250), IN p_C_Updt_Person varchar(250), IN p_C_Del_Person varchar(250) )
										BEGIN
											IF NOT EXISTS (
													SELECT * FROM information_schema.tables WHERE table_name = 'DailyCut'
												) THEN
													CREATE TABLE DailyCut ( C_ID NVARCHAR(250) NULL, C_Date datetime, C_Amount FLOAT DEFAULT 0, C_Insrt_Person NVARCHAR(250) NULL, C_Updt_Person NVARCHAR(250) NULL, C_Del_Person NVARCHAR(250) NULL );
												END IF;
												IF NOT EXISTS (
													SELECT * FROM DailyCut WHERE C_ID = p_C_ID
												) THEN         
													INSERT INTO DailyCut ( C_ID,C_Date,C_Amount,C_Insrt_Person,C_Updt_Person,C_Del_Person )			
																  VALUES ( p_C_ID,p_C_Date,p_C_Amount,p_C_Insrt_Person,p_C_Updt_Person,p_C_Del_Person );															
											ELSE
													UPDATE DailyCut SET C_ID = p_C_ID, C_Date = p_C_Date, C_Amount = p_C_Amount, C_Insrt_Person = p_C_Insrt_Person, C_Updt_Person = p_C_Updt_Person, C_Del_Person = p_C_Del_Person ;        
											END IF;
										END";
					using (MySqlCommand procCmd = new MySqlCommand(crtProc, mysqlConn))
					{
						procCmd.ExecuteNonQuery();
					}
					while (reader.Read())
					{
						using (MySqlCommand mysqlCmd = new MySqlCommand("sp_dailyCutSync", mysqlConn))
						{
							mysqlCmd.Parameters.AddWithValue("p_C_ID", reader["C_ID"]);
							mysqlCmd.Parameters.AddWithValue("p_C_Date", reader["C_Date"]);
							mysqlCmd.Parameters.AddWithValue("p_C_Amount", reader["C_Amount"]);
							mysqlCmd.Parameters.AddWithValue("p_C_Insrt_Person", reader["C_Insrt_Person"]);
							mysqlCmd.Parameters.AddWithValue("p_C_Updt_Person", reader["C_Updt_Person"]);
							mysqlCmd.Parameters.AddWithValue("p_C_Del_Person", reader["C_Del_Person"]);
							mysqlCmd.ExecuteNonQuery();
						}
					}
					mysqlConn.Close();
				}
				accConn.Close();
			}
		}
		private void sp_dailyAntSync()
		{
			using (OleDbConnection accConn = new OleDbConnection(connAcc))
			{
				accConn.Open();
				string selCom = "SELECT * FROM DailyAnt";
				OleDbCommand command = new OleDbCommand(selCom, accConn);
				OleDbDataReader reader = command.ExecuteReader();
				using (MySqlConnection mysqlConn = new MySqlConnection(connMySql))
				{
					mysqlConn.Open();
					string crtProc = @"CREATE PROCEDURE IF NOT EXISTS sp_dailyAntSync ( IN p_DA_ID varchar(250), IN p_DA_Date datetime, IN p_DA_FPAmount float, IN p_DA_SPAmount float, IN p_NotTaken float, IN p_DA_Data varchar(250), IN p_TakenDate datetime, IN p_DA_Insrt_Person varchar(250), IN p_DA_Updt_Person varchar(250), IN p_DA_Del_Person varchar(250) )
										BEGIN
											IF NOT EXISTS (
													SELECT * FROM information_schema.tables WHERE table_name = 'DailyAnt'
												) THEN
													CREATE TABLE DailyAnt ( DA_ID NVARCHAR(250) NULL, DA_Date datetime, DA_FPAmount FLOAT DEFAULT 0, DA_SPAmount FLOAT DEFAULT 0, NotTaken FLOAT DEFAULT 0, DA_Data NVARCHAR(250) NULL, TakenDate datetime, DA_Insrt_Person NVARCHAR(250) NULL, DA_Updt_Person NVARCHAR(250) NULL, DA_Del_Person NVARCHAR(250) NULL );
												END IF;
												IF NOT EXISTS (
													SELECT * FROM DailyAnt WHERE DA_ID = p_DA_ID
												) THEN         
													INSERT INTO DailyAnt ( DA_ID,DA_Date,DA_FPAmount,DA_SPAmount,NotTaken,DA_Data,TakenDate,DA_Insrt_Person,DA_Updt_Person,DA_Del_Person )			
																  VALUES ( p_DA_ID,p_DA_Date,p_DA_FPAmount,p_DA_SPAmount,p_NotTaken,p_DA_Data,p_TakenDate,p_DA_Insrt_Person,p_DA_Updt_Person,p_DA_Del_Person );															
											ELSE
													UPDATE DailyAnt SET DA_ID = p_DA_ID, DA_Date = p_DA_Date, DA_FPAmount = p_DA_FPAmount, DA_SPAmount = p_DA_SPAmount, NotTaken = p_NotTaken, DA_Data = p_DA_Data, TakenDate = p_TakenDate, DA_Insrt_Person = p_DA_Insrt_Person, DA_Updt_Person = p_DA_Updt_Person, DA_Del_Person = p_DA_Del_Person ;        
											END IF;
										END";
					using (MySqlCommand procCmd = new MySqlCommand(crtProc, mysqlConn))
					{
						procCmd.ExecuteNonQuery();
					}
					while (reader.Read())
					{
						using (MySqlCommand mysqlCmd = new MySqlCommand("sp_dailyantSync", mysqlConn))
						{
							mysqlCmd.Parameters.AddWithValue("p_DA_ID", reader["DA_ID"]);
							mysqlCmd.Parameters.AddWithValue("p_DA_Date", reader["DA_Date"]);
							mysqlCmd.Parameters.AddWithValue("p_DA_FPAmount", reader["DA_FPAmount"]);
							mysqlCmd.Parameters.AddWithValue("p_DA_SPAmount", reader["DA_SPAmount"]);
							mysqlCmd.Parameters.AddWithValue("p_NotTaken", reader["NotTaken"]);
							mysqlCmd.Parameters.AddWithValue("p_DA_Data", reader["DA_Data"]);
							mysqlCmd.Parameters.AddWithValue("p_TakenDate", reader["TakenDate"]);
							mysqlCmd.Parameters.AddWithValue("p_DA_Insrt_Person", reader["DA_Insrt_Person"]);
							mysqlCmd.Parameters.AddWithValue("p_DA_Updt_Person", reader["DA_Updt_Person"]);
							mysqlCmd.Parameters.AddWithValue("p_DA_Del_Person", reader["DA_Del_Person"]);
							mysqlCmd.ExecuteNonQuery();
						}
					}
					mysqlConn.Close();
				}
				accConn.Close();
			}
		}
		private void sp_monthlySync()
		{
			using (OleDbConnection accConn = new OleDbConnection(connAcc))
			{
				accConn.Open();
				string selCom = "SELECT * FROM MonthlyTaken";
				OleDbCommand command = new OleDbCommand(selCom, accConn);
				OleDbDataReader reader = command.ExecuteReader();
				using (MySqlConnection mysqlConn = new MySqlConnection(connMySql))
				{
					mysqlConn.Open();
					string crtProc = @"CREATE PROCEDURE IF NOT EXISTS sp_monthlyTakenSync ( IN p_MT_ID varchar(250), IN p_MT_Date datetime, IN p_MT_TotalTK float, IN p_MT_Giv_TK float, IN p_MT_LS_TK float, IN p_T01 float, IN p_T02 float, IN p_T03 float, IN p_T04 float, IN p_T05 float, IN p_T06 float, IN p_T07 float, IN p_T08 float, IN p_T09 float, IN p_T10 float, IN p_T11 float, IN p_T12 float, IN p_T13 float, IN p_T14 float, IN p_T15 float, IN p_T16 float, IN p_T17 float, IN p_T18 float, IN p_T19 float, IN p_T20 float, IN p_T21 float, IN p_T22 float, IN p_T23 float, IN p_T24 float, IN p_T25 float, IN p_T26 float, IN p_T27 float, IN p_T28 float, IN p_T29 float, IN p_T30 float, IN p_T31 float, IN p_T32 float, IN p_T33 float, IN p_T34 float, IN p_T35 float, IN p_T36 float, IN p_T37 float, IN p_T38 float, IN p_T39 float, IN p_T40 float, IN p_T41 float, IN p_T42 float, IN p_T43 float, IN p_T44 float, IN p_T45 float, IN p_T46 float, IN p_T47 float, IN p_T48 float, IN p_T49 float, IN p_T50 float, IN p_T51 float, IN p_T52 float, IN p_T53 float, IN p_T54 float, IN p_T55 float, IN p_T56 float, IN p_T57 float, IN p_T58 float, IN p_T59 float, IN p_T60 float, IN p_T61 float, IN p_T62 float, IN p_T63 float, IN p_T64 float, IN p_T65 float, IN p_T66 float, IN p_T67 float, IN p_T68 float, IN p_T69 float, IN p_T70 float, IN p_T71 float, IN p_T72 float, IN p_T73 float, IN p_T74 float, IN p_T75 float, IN p_T76 float, IN p_T77 float, IN p_T78 float, IN p_T79 float, IN p_T80 float, IN p_T81 float, IN p_T82 float, IN p_T83 float, IN p_T84 float, IN p_T85 float, IN p_T86 float, IN p_T87 float, IN p_T88 float, IN p_T89 float, IN p_T90 float, IN p_T91 float, IN p_T92 float, IN p_T93 float, IN p_T94 float, IN p_T95 float, IN p_T96 float, IN p_T97 float, IN p_T98 float, IN p_T99 float, IN p_T100 float, IN p_T101 float, IN p_T102 float, IN p_T103 float, IN p_T104 float, IN p_T105 float, IN p_T106 float, IN p_T107 float, IN p_T108 float, IN p_T109 float, IN p_T110 float, IN p_T111 float, IN p_T112 float, IN p_T113 float, IN p_T114 float, IN p_T115 float, IN p_T116 float, IN p_T117 float, IN p_T118 float, IN p_T119 float, IN p_T120 float, IN p_MTDT_V varchar(250), IN p_MT_Insrt_Person varchar(250), IN p_MT_Updt_Person varchar(250), IN p_MT_Del_Person varchar(250) )
										BEGIN
											IF NOT EXISTS (
													SELECT * FROM information_schema.tables WHERE table_name = 'MonthlyTaken'
												) THEN
													CREATE TABLE MonthlyTaken ( MT_ID NVARCHAR(250) NULL,  MT_Date datetime, MT_TotalTK FLOAT DEFAULT 0, MT_Giv_TK FLOAT DEFAULT 0, MT_LS_TK FLOAT DEFAULT 0, T01 FLOAT DEFAULT 0, T02 FLOAT DEFAULT 0, T03 FLOAT DEFAULT 0, T04 FLOAT DEFAULT 0, T05 FLOAT DEFAULT 0, T06 FLOAT DEFAULT 0, T07 FLOAT DEFAULT 0, T08 FLOAT DEFAULT 0, T09 FLOAT DEFAULT 0, T10 FLOAT DEFAULT 0, T11 FLOAT DEFAULT 0, T12 FLOAT DEFAULT 0, T13 FLOAT DEFAULT 0, T14 FLOAT DEFAULT 0, T15 FLOAT DEFAULT 0, T16 FLOAT DEFAULT 0, T17 FLOAT DEFAULT 0, T18 FLOAT DEFAULT 0, T19 FLOAT DEFAULT 0, T20 FLOAT DEFAULT 0, T21 FLOAT DEFAULT 0, T22 FLOAT DEFAULT 0, T23 FLOAT DEFAULT 0, T24 FLOAT DEFAULT 0, T25 FLOAT DEFAULT 0, T26 FLOAT DEFAULT 0, T27 FLOAT DEFAULT 0, T28 FLOAT DEFAULT 0, T29 FLOAT DEFAULT 0, T30 FLOAT DEFAULT 0, T31 FLOAT DEFAULT 0, T32 FLOAT DEFAULT 0, T33 FLOAT DEFAULT 0, T34 FLOAT DEFAULT 0, T35 FLOAT DEFAULT 0, T36 FLOAT DEFAULT 0, T37 FLOAT DEFAULT 0, T38 FLOAT DEFAULT 0, T39 FLOAT DEFAULT 0, T40 FLOAT DEFAULT 0, T41 FLOAT DEFAULT 0, T42 FLOAT DEFAULT 0, T43 FLOAT DEFAULT 0, T44 FLOAT DEFAULT 0, T45 FLOAT DEFAULT 0, T46 FLOAT DEFAULT 0, T47 FLOAT DEFAULT 0, T48 FLOAT DEFAULT 0, T49 FLOAT DEFAULT 0, T50 FLOAT DEFAULT 0, T51 FLOAT DEFAULT 0, T52 FLOAT DEFAULT 0, T53 FLOAT DEFAULT 0, T54 FLOAT DEFAULT 0, T55 FLOAT DEFAULT 0, T56 FLOAT DEFAULT 0, T57 FLOAT DEFAULT 0, T58 FLOAT DEFAULT 0, T59 FLOAT DEFAULT 0, T60 FLOAT DEFAULT 0, T61 FLOAT DEFAULT 0, T62 FLOAT DEFAULT 0, T63 FLOAT DEFAULT 0, T64 FLOAT DEFAULT 0, T65 FLOAT DEFAULT 0, T66 FLOAT DEFAULT 0, T67 FLOAT DEFAULT 0, T68 FLOAT DEFAULT 0, T69 FLOAT DEFAULT 0, T70 FLOAT DEFAULT 0, T71 FLOAT DEFAULT 0, T72 FLOAT DEFAULT 0, T73 FLOAT DEFAULT 0, T74 FLOAT DEFAULT 0, T75 FLOAT DEFAULT 0, T76 FLOAT DEFAULT 0, T77 FLOAT DEFAULT 0, T78 FLOAT DEFAULT 0, T79 FLOAT DEFAULT 0, T80 FLOAT DEFAULT 0, T81 FLOAT DEFAULT 0, T82 FLOAT DEFAULT 0, T83 FLOAT DEFAULT 0, T84 FLOAT DEFAULT 0, T85 FLOAT DEFAULT 0, T86 FLOAT DEFAULT 0, T87 FLOAT DEFAULT 0, T88 FLOAT DEFAULT 0, T89 FLOAT DEFAULT 0, T90 FLOAT DEFAULT 0, T91 FLOAT DEFAULT 0, T92 FLOAT DEFAULT 0, T93 FLOAT DEFAULT 0, T94 FLOAT DEFAULT 0, T95 FLOAT DEFAULT 0, T96 FLOAT DEFAULT 0, T97 FLOAT DEFAULT 0, T98 FLOAT DEFAULT 0, T99 FLOAT DEFAULT 0, T100 FLOAT DEFAULT 0, T101 FLOAT DEFAULT 0, T102 FLOAT DEFAULT 0, T103 FLOAT DEFAULT 0, T104 FLOAT DEFAULT 0, T105 FLOAT DEFAULT 0, T106 FLOAT DEFAULT 0, T107 FLOAT DEFAULT 0, T108 FLOAT DEFAULT 0, T109 FLOAT DEFAULT 0, T110 FLOAT DEFAULT 0, T111 FLOAT DEFAULT 0, T112 FLOAT DEFAULT 0, T113 FLOAT DEFAULT 0, T114 FLOAT DEFAULT 0, T115 FLOAT DEFAULT 0, T116 FLOAT DEFAULT 0, T117 FLOAT DEFAULT 0, T118 FLOAT DEFAULT 0, T119 FLOAT DEFAULT 0, T120 FLOAT DEFAULT 0, MTDT_V NVARCHAR(250) NULL, MT_Insrt_Person NVARCHAR(250) NULL, MT_Updt_Person NVARCHAR(250) NULL, MT_Del_Person NVARCHAR(250) NULL );
												END IF;
												IF NOT EXISTS (
													SELECT * FROM MonthlyTaken WHERE MT_ID = p_MT_ID
												) THEN         
													INSERT INTO MonthlyTaken ( MT_ID,MT_Date,MT_TotalTK,MT_Giv_TK,MT_LS_TK,T01,T02,T03,T04,T05,T06,T07,T08,T09,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26,T27,T28,T29,T30,T31,T32,T33,T34,T35,T36,T37,T38,T39,T40,T41,T42,T43,T44,T45,T46,T47,T48,T49,T50,T51,T52,T53,T54,T55,T56,T57,T58,T59,T60,T61,T62,T63,T64,T65,T66,T67,T68,T69,T70,T71,T72,T73,T74,T75,T76,T77,T78,T79,T80,T81,T82,T83,T84,T85,T86,T87,T88,T89,T90,T91,T92,T93,T94,T95,T96,T97,T98,T99,T100,T101,T102,T103,T104,T105,T106,T107,T108,T109,T110,T111,T112,T113,T114,T115,T116,T117,T118,T119,T120,MTDT_V,MT_Insrt_Person,MT_Updt_Person,MT_Del_Person )			
																	  VALUES ( p_MT_ID,p_MT_Date,p_MT_TotalTK,p_MT_Giv_TK,p_MT_LS_TK,p_T01,p_T02,p_T03,p_T04,p_T05,p_T06,p_T07,p_T08,p_T09,p_T10,p_T11,p_T12,p_T13,p_T14,p_T15,p_T16,p_T17,p_T18,p_T19,p_T20,p_T21,p_T22,p_T23,p_T24,p_T25,p_T26,p_T27,p_T28,p_T29,p_T30,p_T31,p_T32,p_T33,p_T34,p_T35,p_T36,p_T37,p_T38,p_T39,p_T40,p_T41,p_T42,p_T43,p_T44,p_T45,p_T46,p_T47,p_T48,p_T49,p_T50,p_T51,p_T52,p_T53,p_T54,p_T55,p_T56,p_T57,p_T58,p_T59,p_T60,p_T61,p_T62,p_T63,p_T64,p_T65,p_T66,p_T67,p_T68,p_T69,p_T70,p_T71,p_T72,p_T73,p_T74,p_T75,p_T76,p_T77,p_T78,p_T79,p_T80,p_T81,p_T82,p_T83,p_T84,p_T85,p_T86,p_T87,p_T88,p_T89,p_T90,p_T91,p_T92,p_T93,p_T94,p_T95,p_T96,p_T97,p_T98,p_T99,p_T100,p_T101,p_T102,p_T103,p_T104,p_T105,p_T106,p_T107,p_T108,p_T109,p_T110,p_T111,p_T112,p_T113,p_T114,p_T115,p_T116,p_T117,p_T118,p_T119,p_T120,p_MTDT_V,p_MT_Insrt_Person,p_MT_Updt_Person,p_MT_Del_Person );
											ELSE
													UPDATE MonthlyTaken SET MT_ID = p_MT_ID,  MT_Date = p_MT_Date, MT_TotalTK = p_MT_TotalTK, MT_Giv_TK = p_MT_Giv_TK, MT_LS_TK = p_MT_LS_TK, T01 = p_T01, T02 = p_T02, T03 = p_T03, T04 = p_T04, T05 = p_T05, T06 = p_T06, T07 = p_T07, T08 = p_T08, T09 = p_T09, T10 = p_T10, T11 = p_T11, T12 = p_T12, T13 = p_T13, T14 = p_T14, T15 = p_T15, T16 = p_T16, T17 = p_T17, T18 = p_T18, T19 = p_T19, T20 = p_T20, T21 = p_T21, T22 = p_T22, T23 = p_T23, T24 = p_T24, T25 = p_T25, T26 = p_T26, T27 = p_T27, T28 = p_T28, T29 = p_T29, T30 = p_T30, T31 = p_T31, T32 = p_T32, T33 = p_T33, T34 = p_T34, T35 = p_T35, T36 = p_T36, T37 = p_T37, T38 = p_T38, T39 = p_T39, T40 = p_T40, T41 = p_T41, T42 = p_T42, T43 = p_T43, T44 = p_T44, T45 = p_T45, T46 = p_T46, T47 = p_T47, T48 = p_T48, T49 = p_T49, T50 = p_T50, T51 = p_T51, T52 = p_T52, T53 = p_T53, T54 = p_T54, T55 = p_T55, T56 = p_T56, T57 = p_T57, T58 = p_T58, T59 = p_T59, T60 = p_T60, T61 = p_T61, T62 = p_T62, T63 = p_T63, T64 = p_T64, T65 = p_T65, T66 = p_T66, T67 = p_T67, T68 = p_T68, T69 = p_T69, T70 = p_T70, T71 = p_T71, T72 = p_T72, T73 = p_T73, T74 = p_T74, T75 = p_T75, T76 = p_T76, T77 = p_T77, T78 = p_T78, T79 = p_T79, T80 = p_T80, T81 = p_T81, T82 = p_T82, T83 = p_T83, T84 = p_T84, T85 = p_T85, T86 = p_T86, T87 = p_T87, T88 = p_T88, T89 = p_T89, T90 = p_T90, T91 = p_T91, T92 = p_T92, T93 = p_T93, T94 = p_T94, T95 = p_T95, T96 = p_T96, T97 = p_T97, T98 = p_T98, T99 = p_T99, T100 = p_T100, T101 = p_T101, T102 = p_T102, T103 = p_T103, T104 = p_T104, T105 = p_T105, T106 = p_T106, T107 = p_T107, T108 = p_T108, T109 = p_T109, T110 = p_T110, T111 = p_T111, T112 = p_T112, T113 = p_T113, T114 = p_T114, T115 = p_T115, T116 = p_T116, T117 = p_T117, T118 = p_T118, T119 = p_T119, T120 = p_T120, MTDT_V = p_MTDT_V, MT_Insrt_Person = p_MT_Insrt_Person, MT_Updt_Person = p_MT_Updt_Person, MT_Del_Person = p_MT_Del_Person ;        
											END IF;
										END";
					using (MySqlCommand procCmd = new MySqlCommand(crtProc, mysqlConn))
					{
						procCmd.ExecuteNonQuery();
					}
					while (reader.Read())
					{
						using (MySqlCommand mysqlCmd = new MySqlCommand("sp_monthlyTakenSync", mysqlConn))
						{
							mysqlCmd.Parameters.AddWithValue("p_MT_ID", reader["MT_ID"]);
							mysqlCmd.Parameters.AddWithValue("p_MT_Date", reader["MT_Date"]);
							mysqlCmd.Parameters.AddWithValue("p_MT_TotalTK", reader["MT_TotalTK"]);
							mysqlCmd.Parameters.AddWithValue("p_MT_Giv_TK", reader["MT_Giv_TK"]);
							mysqlCmd.Parameters.AddWithValue("p_MT_LS_TK", reader["MT_LS_TK"]);
							mysqlCmd.Parameters.AddWithValue("p_T01", reader["T01"]);
							mysqlCmd.Parameters.AddWithValue("p_T02", reader["T02"]);
							mysqlCmd.Parameters.AddWithValue("p_T03", reader["T03"]);
							mysqlCmd.Parameters.AddWithValue("p_T04", reader["T04"]);
							mysqlCmd.Parameters.AddWithValue("p_T05", reader["T05"]);
							mysqlCmd.Parameters.AddWithValue("p_T06", reader["T06"]);
							mysqlCmd.Parameters.AddWithValue("p_T07", reader["T07"]);
							mysqlCmd.Parameters.AddWithValue("p_T08", reader["T08"]);
							mysqlCmd.Parameters.AddWithValue("p_T09", reader["T09"]);
							mysqlCmd.Parameters.AddWithValue("p_T10", reader["T10"]);
							mysqlCmd.Parameters.AddWithValue("p_T11", reader["T11"]);
							mysqlCmd.Parameters.AddWithValue("p_T12", reader["T12"]);
							mysqlCmd.Parameters.AddWithValue("p_T13", reader["T13"]);
							mysqlCmd.Parameters.AddWithValue("p_T14", reader["T14"]);
							mysqlCmd.Parameters.AddWithValue("p_T15", reader["T15"]);
							mysqlCmd.Parameters.AddWithValue("p_T16", reader["T16"]);
							mysqlCmd.Parameters.AddWithValue("p_T17", reader["T17"]);
							mysqlCmd.Parameters.AddWithValue("p_T18", reader["T18"]);
							mysqlCmd.Parameters.AddWithValue("p_T19", reader["T19"]);
							mysqlCmd.Parameters.AddWithValue("p_T20", reader["T20"]);
							mysqlCmd.Parameters.AddWithValue("p_T21", reader["T21"]);
							mysqlCmd.Parameters.AddWithValue("p_T22", reader["T22"]);
							mysqlCmd.Parameters.AddWithValue("p_T23", reader["T23"]);
							mysqlCmd.Parameters.AddWithValue("p_T24", reader["T24"]);
							mysqlCmd.Parameters.AddWithValue("p_T25", reader["T25"]);
							mysqlCmd.Parameters.AddWithValue("p_T26", reader["T26"]);
							mysqlCmd.Parameters.AddWithValue("p_T27", reader["T27"]);
							mysqlCmd.Parameters.AddWithValue("p_T28", reader["T28"]);
							mysqlCmd.Parameters.AddWithValue("p_T29", reader["T29"]);
							mysqlCmd.Parameters.AddWithValue("p_T30", reader["T30"]);
							mysqlCmd.Parameters.AddWithValue("p_T31", reader["T31"]);
							mysqlCmd.Parameters.AddWithValue("p_T32", reader["T32"]);
							mysqlCmd.Parameters.AddWithValue("p_T33", reader["T33"]);
							mysqlCmd.Parameters.AddWithValue("p_T34", reader["T34"]);
							mysqlCmd.Parameters.AddWithValue("p_T35", reader["T35"]);
							mysqlCmd.Parameters.AddWithValue("p_T36", reader["T36"]);
							mysqlCmd.Parameters.AddWithValue("p_T37", reader["T37"]);
							mysqlCmd.Parameters.AddWithValue("p_T38", reader["T38"]);
							mysqlCmd.Parameters.AddWithValue("p_T39", reader["T39"]);
							mysqlCmd.Parameters.AddWithValue("p_T40", reader["T40"]);
							mysqlCmd.Parameters.AddWithValue("p_T41", reader["T41"]);
							mysqlCmd.Parameters.AddWithValue("p_T42", reader["T42"]);
							mysqlCmd.Parameters.AddWithValue("p_T43", reader["T43"]);
							mysqlCmd.Parameters.AddWithValue("p_T44", reader["T44"]);
							mysqlCmd.Parameters.AddWithValue("p_T45", reader["T45"]);
							mysqlCmd.Parameters.AddWithValue("p_T46", reader["T46"]);
							mysqlCmd.Parameters.AddWithValue("p_T47", reader["T47"]);
							mysqlCmd.Parameters.AddWithValue("p_T48", reader["T48"]);
							mysqlCmd.Parameters.AddWithValue("p_T49", reader["T49"]);
							mysqlCmd.Parameters.AddWithValue("p_T50", reader["T50"]);
							mysqlCmd.Parameters.AddWithValue("p_T51", reader["T51"]);
							mysqlCmd.Parameters.AddWithValue("p_T52", reader["T52"]);
							mysqlCmd.Parameters.AddWithValue("p_T53", reader["T53"]);
							mysqlCmd.Parameters.AddWithValue("p_T54", reader["T54"]);
							mysqlCmd.Parameters.AddWithValue("p_T55", reader["T55"]);
							mysqlCmd.Parameters.AddWithValue("p_T56", reader["T56"]);
							mysqlCmd.Parameters.AddWithValue("p_T57", reader["T57"]);
							mysqlCmd.Parameters.AddWithValue("p_T58", reader["T58"]);
							mysqlCmd.Parameters.AddWithValue("p_T59", reader["T59"]);
							mysqlCmd.Parameters.AddWithValue("p_T60", reader["T60"]);
							mysqlCmd.Parameters.AddWithValue("p_T61", reader["T61"]);
							mysqlCmd.Parameters.AddWithValue("p_T62", reader["T62"]);
							mysqlCmd.Parameters.AddWithValue("p_T63", reader["T63"]);
							mysqlCmd.Parameters.AddWithValue("p_T64", reader["T64"]);
							mysqlCmd.Parameters.AddWithValue("p_T65", reader["T65"]);
							mysqlCmd.Parameters.AddWithValue("p_T66", reader["T66"]);
							mysqlCmd.Parameters.AddWithValue("p_T67", reader["T67"]);
							mysqlCmd.Parameters.AddWithValue("p_T68", reader["T68"]);
							mysqlCmd.Parameters.AddWithValue("p_T69", reader["T69"]);
							mysqlCmd.Parameters.AddWithValue("p_T70", reader["T70"]);
							mysqlCmd.Parameters.AddWithValue("p_T71", reader["T71"]);
							mysqlCmd.Parameters.AddWithValue("p_T72", reader["T72"]);
							mysqlCmd.Parameters.AddWithValue("p_T73", reader["T73"]);
							mysqlCmd.Parameters.AddWithValue("p_T74", reader["T74"]);
							mysqlCmd.Parameters.AddWithValue("p_T75", reader["T75"]);
							mysqlCmd.Parameters.AddWithValue("p_T76", reader["T76"]);
							mysqlCmd.Parameters.AddWithValue("p_T77", reader["T77"]);
							mysqlCmd.Parameters.AddWithValue("p_T78", reader["T78"]);
							mysqlCmd.Parameters.AddWithValue("p_T79", reader["T79"]);
							mysqlCmd.Parameters.AddWithValue("p_T80", reader["T80"]);
							mysqlCmd.Parameters.AddWithValue("p_T81", reader["T81"]);
							mysqlCmd.Parameters.AddWithValue("p_T82", reader["T82"]);
							mysqlCmd.Parameters.AddWithValue("p_T83", reader["T83"]);
							mysqlCmd.Parameters.AddWithValue("p_T84", reader["T84"]);
							mysqlCmd.Parameters.AddWithValue("p_T85", reader["T85"]);
							mysqlCmd.Parameters.AddWithValue("p_T86", reader["T86"]);
							mysqlCmd.Parameters.AddWithValue("p_T87", reader["T87"]);
							mysqlCmd.Parameters.AddWithValue("p_T88", reader["T88"]);
							mysqlCmd.Parameters.AddWithValue("p_T89", reader["T89"]);
							mysqlCmd.Parameters.AddWithValue("p_T90", reader["T90"]);
							mysqlCmd.Parameters.AddWithValue("p_T91", reader["T91"]);
							mysqlCmd.Parameters.AddWithValue("p_T92", reader["T92"]);
							mysqlCmd.Parameters.AddWithValue("p_T93", reader["T93"]);
							mysqlCmd.Parameters.AddWithValue("p_T94", reader["T94"]);
							mysqlCmd.Parameters.AddWithValue("p_T95", reader["T95"]);
							mysqlCmd.Parameters.AddWithValue("p_T96", reader["T96"]);
							mysqlCmd.Parameters.AddWithValue("p_T97", reader["T97"]);
							mysqlCmd.Parameters.AddWithValue("p_T98", reader["T98"]);
							mysqlCmd.Parameters.AddWithValue("p_T99", reader["T99"]);
							mysqlCmd.Parameters.AddWithValue("p_T100", reader["T100"]);
							mysqlCmd.Parameters.AddWithValue("p_T101", reader["T101"]);
							mysqlCmd.Parameters.AddWithValue("p_T102", reader["T102"]);
							mysqlCmd.Parameters.AddWithValue("p_T103", reader["T103"]);
							mysqlCmd.Parameters.AddWithValue("p_T104", reader["T104"]);
							mysqlCmd.Parameters.AddWithValue("p_T105", reader["T105"]);
							mysqlCmd.Parameters.AddWithValue("p_T106", reader["T106"]);
							mysqlCmd.Parameters.AddWithValue("p_T107", reader["T107"]);
							mysqlCmd.Parameters.AddWithValue("p_T108", reader["T108"]);
							mysqlCmd.Parameters.AddWithValue("p_T109", reader["T109"]);
							mysqlCmd.Parameters.AddWithValue("p_T110", reader["T110"]);
							mysqlCmd.Parameters.AddWithValue("p_T111", reader["T111"]);
							mysqlCmd.Parameters.AddWithValue("p_T112", reader["T112"]);
							mysqlCmd.Parameters.AddWithValue("p_T113", reader["T113"]);
							mysqlCmd.Parameters.AddWithValue("p_T114", reader["T114"]);
							mysqlCmd.Parameters.AddWithValue("p_T115", reader["T115"]);
							mysqlCmd.Parameters.AddWithValue("p_T116", reader["T116"]);
							mysqlCmd.Parameters.AddWithValue("p_T117", reader["T117"]);
							mysqlCmd.Parameters.AddWithValue("p_T118", reader["T118"]);
							mysqlCmd.Parameters.AddWithValue("p_T119", reader["T119"]);
							mysqlCmd.Parameters.AddWithValue("p_T120", reader["T120"]);
							mysqlCmd.Parameters.AddWithValue("p_MTDT_V", reader["MTDT_V"]);
							mysqlCmd.Parameters.AddWithValue("p_MT_Insrt_Person", reader["MT_Insrt_Person"]);
							mysqlCmd.Parameters.AddWithValue("p_MT_Updt_Person", reader["MT_Updt_Person"]);
							mysqlCmd.Parameters.AddWithValue("p_MT_Del_Person", reader["MT_Del_Person"]);
							mysqlCmd.ExecuteNonQuery();
						}
					}
					mysqlConn.Close();
				}
				accConn.Close();
			}
		}
		private void sp_imagesSync()
		{
			using (OleDbConnection accConn = new OleDbConnection(connAcc))
			{
				accConn.Open();
				string selCom = "SELECT * FROM Images";
				OleDbCommand command = new OleDbCommand(selCom, accConn);
				OleDbDataReader reader = command.ExecuteReader();
				using (MySqlConnection mysqlConn = new MySqlConnection(connMySql))
				{
					mysqlConn.Open();
					string crtProc = @"CREATE PROCEDURE IF NOT EXISTS sp_imagesSync ( IN p_Img_ID varchar(250), IN p_ImageData varchar(250) )
										BEGIN
											IF NOT EXISTS (
													SELECT * FROM information_schema.tables WHERE table_name = 'Images'
												) THEN
													CREATE TABLE Images ( Img_ID NVARCHAR(250) NULL, ImageData NVARCHAR(250) NULL );
												END IF;
												IF NOT EXISTS (
													SELECT * FROM Images WHERE Img_ID = p_Img_ID
												) THEN         
													INSERT INTO Images ( Img_ID,ImageData ) 
																VALUES ( p_Img_ID,p_ImageData );															
											ELSE
													UPDATE Images SET Img_ID = p_Img_ID, ImageData = p_ImageData ;        
											END IF;
										END";
					using (MySqlCommand procCmd = new MySqlCommand(crtProc, mysqlConn))
					{
						procCmd.ExecuteNonQuery();
					}
					while (reader.Read())
					{
						using (MySqlCommand mysqlCmd = new MySqlCommand("sp_imagesSync", mysqlConn))
						{
							mysqlCmd.Parameters.AddWithValue("p_Img_ID", reader["Img_ID"]);
							mysqlCmd.Parameters.AddWithValue("p_ImageData", reader["ImageData"]);
							mysqlCmd.ExecuteNonQuery();
						}
					}
					mysqlConn.Close();
				}
				accConn.Close();
			}
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
		public DataTable GetMonthTakeData(string mntTknId)
		{
			using (OleDbConnection conn = new OleDbConnection(this.conn.ConnectionString))
			{
				conn.Open();
				string query = "SELECT * FROM MonthlyTaken WHERE MT_ID = ?";
				using (OleDbDataAdapter oleDbDatadt = new OleDbDataAdapter(query, conn))
				{
					oleDbDatadt.SelectCommand.Parameters.AddWithValue("?", mntTknId);
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
