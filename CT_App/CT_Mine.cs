using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CT_App
{
    public partial class CT_Mine : Form
    {
        #region Comments
        OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\CT_DB.accdb;Jet OLEDB:Database Password=*3455*00;");
        OdbcConnection conne = new OdbcConnection(@"Dsn=RETAILMasterSHOPS;uid=sa;Pwd=Ajwahana$@$;");
        private string DltDate;
        private string tableName = "ImagesTable";
        private string selectedImagePath;
        string connAcc = (@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\CT_DB.accdb;Jet OLEDB:Database Password=*3455*00;");
        string connSql = (@"Dsn=RETAILMasterSHOPS;uid=sa;Pwd=Ajwahana$@$;");
        #endregion
        public CT_Mine()
        {
            InitializeComponent();
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            this.Text = string.Concat("T Mine - " + String.Format(this.lblVer.Text, version.Major, version.Minor, version.Build, version.Revision));
            this.tabControl1.Visible = false;
            this.DltDate = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            this.fillData();
            this.AmtDataView();
            this.fillGivenData();
            this.AmtCrDataView();
            this.fillDailyData();
            this.totalDailyData();
            this.fillDailyAntData();
            this.totalDailyAntData();
            this.fillInstData();
            this.totalInstData();
            this.fillMemo();
            this.fillDataBike();
            this.fillImageData();
            this.textBox1.ReadOnly = true;
            this.textBox39.ReadOnly = true;
            this.textBox33.ReadOnly = true;
            this.textBox32.ReadOnly = true;
            this.comboBox1.Enabled = false;
            this.dateTimePicker3.Enabled = false;
            this.textBox34.ReadOnly = true;
            this.radioButton1.Enabled = false;
            this.radioButton2.Enabled = false;
            this.radioButton3.Enabled = false;
            this.radioButton5.Enabled = false;
            this.radioButton4.Enabled = false;
            this.button7.Visible = false;
            this.textBox37.ReadOnly = true;
            this.textBox133.ReadOnly = true;
            this.textBox50.ReadOnly = true;
            this.textBox131.ReadOnly = true;
            this.panel9.Visible = false;
            this.panel12.Visible = false;
            this.button12.Visible = false;
            this.panel10.Visible = false;
            this.panel11.Visible = false;
            this.panel30.Visible = false;
            this.button21.Visible = false;
            this.button22.Visible = false;
            this.button33.Visible = false;
            this.button32.Visible = false;
            this.button25.Visible = false;
            this.button24.Visible = false;
            this.label231.Text = "";
            this.label233.Text = "";
            this.label235.Text = "";
            this.label250.Text = "";
            this.label237.Text = "";
            this.label252.Visible = false;
            this.dataGridView13.Visible = false;
        }

        //-----------------------------------------------------------------------
        //------------------------------All Classes------------------------------
        //-----------------------------------------------------------------------
        private void fillDataBike()
        {
            try
            {
                string query = "SELECT B_Next_ODO AS [ODO], B_Chng_Date AS [Date], B_ID AS [ID] FROM BikeInfo ORDER BY B_Chng_Date DESC";
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView12.DataSource = dataTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void fillData()
        {
            try
            {
                string query = "SELECT M_ID AS [ID], M_Date AS [Date], M_Amount AS [Amount] FROM Market ORDER BY M_Date DESC";
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void AmtDataView()
        {
            try
            {
                string query = "SELECT SUM(M_Amount) FROM Market";
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value)
                    {
                        label5.Text = dataTable.Rows[0][0].ToString();
                    }
                    else
                    {
                        label5.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void AmtCrDataView()
        {
            try
            {
                this.conn.Open();
                DataTable dataTableAmtGiven = new DataTable();
                string queryAmtGiven = "SELECT SUM(Total_Given) FROM Given WHERE [GDT_V] = 'NDV'";
                using (OleDbDataAdapter dataAdapterdltAmtG = new OleDbDataAdapter(queryAmtGiven, this.conn))
                {
                    dataAdapterdltAmtG.Fill(dataTableAmtGiven);
                    this.label87.Text = dataTableAmtGiven.Rows.Count > 0 ? dataTableAmtGiven.Rows[0][0].ToString() : "0";
                }
                DataTable dataTableAmtTake = new DataTable();
                string queryAmtTake = "SELECT SUM(Total_Take) FROM Teken WHERE [TDT_V] = 'NDV'";
                using (OleDbDataAdapter dataAdapterdltAmtT = new OleDbDataAdapter(queryAmtTake, this.conn))
                {
                    dataAdapterdltAmtT.Fill(dataTableAmtTake);
                    this.label92.Text = dataTableAmtTake.Rows.Count > 0 ? dataTableAmtTake.Rows[0][0].ToString() : "0";
                }
                DataTable dataTableAmtExp = new DataTable();
                string queryAmtExp = "SELECT SUM(Expense_Amount) FROM TariffAmt WHERE [EDT_V] = 'NDV'";
                using (OleDbDataAdapter dataAdapterdltAmtE = new OleDbDataAdapter(queryAmtExp, this.conn))
                {
                    dataAdapterdltAmtE.Fill(dataTableAmtExp);
                    this.label90.Text = dataTableAmtExp.Rows.Count > 0 ? dataTableAmtExp.Rows[0][0].ToString() : "0";
                }
                DataTable dataTableAmtSev = new DataTable();
                string queryAmtSev = "SELECT SUM(Saving_Amount) FROM Saving WHERE [SDT_V] = 'NDV'";
                using (OleDbDataAdapter dataAdapterdltSev = new OleDbDataAdapter(queryAmtSev, this.conn))
                {
                    dataAdapterdltSev.Fill(dataTableAmtSev);
                    this.label114.Text = dataTableAmtSev.Rows.Count > 0 ? dataTableAmtSev.Rows[0][0].ToString() : "0";
                }
                DataTable dataTableAmtUnr = new DataTable();
                string queryAmtUnr = "SELECT SUM(Unrated_Amount) FROM Unrated WHERE [UDT_V] = 'NDV'";
                using (OleDbDataAdapter dataAdapterdltAmtUnr = new OleDbDataAdapter(queryAmtUnr, this.conn))
                {
                    dataAdapterdltAmtUnr.Fill(dataTableAmtUnr);
                    this.label116.Text = dataTableAmtUnr.Rows.Count > 0 ? dataTableAmtUnr.Rows[0][0].ToString() : "0";
                }
                DataTable dataTableAmtCol = new DataTable();
                string queryAmtCol = "SELECT Max(TakenDate) FROM Daily WHERE [D_Data] = 'TKN'";
                using (OleDbDataAdapter dataAdapterdltAmtCol = new OleDbDataAdapter(queryAmtCol, this.conn))
                {
                    dataAdapterdltAmtCol.Fill(dataTableAmtCol);
                    this.label222.Text = dataTableAmtCol.Rows.Count > 0 ? dataTableAmtCol.Rows[0][0].ToString() : "";
                }
                DataTable dataTableAmtAntCol = new DataTable();
                string queryAmtAntCol = "SELECT Max(TakenDate) FROM DailyAnt WHERE [DA_Data] = 'TKN'";
                using (OleDbDataAdapter dataAdapterdltAmtAntCol = new OleDbDataAdapter(queryAmtAntCol, this.conn))
                {
                    dataAdapterdltAmtAntCol.Fill(dataTableAmtAntCol);
                    this.label261.Text = dataTableAmtAntCol.Rows.Count > 0 ? dataTableAmtAntCol.Rows[0][0].ToString() : "";
                }
                DataTable dataTableAmtSavCol = new DataTable();
                string queryAmtSavCol = "SELECT Max(DS_InBankDate) FROM DailySaving WHERE [DS_Data] = 'TKN'";
                using (OleDbDataAdapter dataAdapterdltAmtSavCol = new OleDbDataAdapter(queryAmtSavCol, this.conn))
                {
                    dataAdapterdltAmtSavCol.Fill(dataTableAmtSavCol);
                    this.label210.Text = dataTableAmtSavCol.Rows.Count > 0 ? dataTableAmtSavCol.Rows[0][0].ToString() : "";
                }
                this.conn.Close();
            }
            catch (Exception ex)
            {
                this.conn.Close();
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void fillGivenData()
        {
            try
            {
                string[] queries = {
                    "SELECT InGiven AS [ID], Given_To AS [Name], Total_Given AS [GTK], Given_Date AS [GDT] FROM Given WHERE [GDT_V] = 'NDV' ORDER BY [ID] DESC",
                    "SELECT InTake AS [ID], Take_To AS [Name], Total_Take AS [TTK], Take_Date AS [TDT] FROM Teken WHERE [TDT_V] = 'NDV' ORDER BY [ID] DESC",
                    "SELECT InExpense AS [ID], Expense_To AS [Name], Expense_Amount AS [ETK], Expense_Date AS [EDT] FROM TariffAmt WHERE [EDT_V] = 'NDV' ORDER BY [ID] DESC",
                    "SELECT InSaving AS [ID], Saving_To AS [Name], Saving_Amount AS [STK], Saving_Date AS [SDT] FROM Saving WHERE [SDT_V] = 'NDV' ORDER BY [ID] DESC",
                    "SELECT InUnrated AS [ID], Unrated_To AS [Name], Unrated_Amount AS [UTK], Unrated_Date AS [UDT] FROM Unrated WHERE [UDT_V] = 'NDV' ORDER BY [ID] DESC"
                };
                DataGridView[] dataGridViews = { dataGridView3, dataGridView7, dataGridView8, dataGridView9, dataGridView10 };
                for (int i = 0; i < queries.Length; i++)
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(queries[i], conn))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridViews[i].DataSource = dataTable.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void fillDailyData()
        {
            try
            {
                string[] queries = {
                    "SELECT D_ID AS [ID], D_Date AS [Date], NotTaken FROM Daily WHERE [D_Data] = 'NTKN' ORDER BY [D_Date] DESC",
                    "SELECT C_ID AS [ID], C_Date AS [Date], C_Amount AS [Amount] FROM DailyCut ORDER BY [C_Date] DESC",
                    "SELECT DS_ID AS [ID], DS_Date AS [Date], NotTaken FROM DailySaving WHERE [DS_Data] = 'NTKN' ORDER BY [DS_Date] DESC"
                };
                DataGridView[] dataGridViews = { dataGridView5, dataGridView4, dataGridView14 };
                for (int i = 0; i < queries.Length; i++)
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(queries[i], conn))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridViews[i].DataSource = dataTable.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void fillDailyAntData()
        {
            try
            {
                string query = "SELECT DA_ID AS [ID], DA_Date AS [Date], NotTaken FROM DailyAnt WHERE Da_Data = 'NTKN' ORDER BY Da_Date DESC";
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView17.DataSource = dataTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void fillInstData()
        {
            try
            {
                string[] queries = {
                    "SELECT I_ID AS [ID], InsPay_Date AS [Date], InsPay AS [PayAmt] FROM Installment WHERE Take_Data = 'INS' ORDER BY [ID] DESC",
                    "SELECT I_ID AS [ID], I_Date AS [Date], Take_Total AS [Total], Take_Anot AS [Anot], Take_Mine AS [Mine] FROM Installment WHERE Take_Data = 'NPD' ORDER BY [ID] DESC"
                };
                DataGridView[] dataGridViews = { dataGridView2, dataGridView6 };
                for (int i = 0; i < queries.Length; i++)
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(queries[i], conn))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridViews[i].DataSource = dataTable.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void BalankFld()
        {
            this.label117.Text   = "";
            this.label102.Text   = "";
            this.textBox36.Text  = "";
            this.textBox40.Text  = "";
            this.textBox41.Text  = "";
            this.textBox42.Text  = "";
            this.textBox118.Text = "";
            this.textBox119.Text = "";
            this.label111.Text   = "";
            this.textBox44.Text  = "";
            this.textBox45.Text  = "";
            this.textBox46.Text  = "";
            this.textBox47.Text  = "";
            this.textBox121.Text = "";
            this.textBox120.Text = "";
            this.label113.Text   = "";
            this.textBox104.Text = "";
            this.textBox103.Text = "";
            this.textBox93.Text  = "";
            this.textBox102.Text = "";
            this.textBox127.Text = "";
            this.textBox109.Text = "";
            this.label243.Text   = "";
            this.textBox105.Text = "";
            this.textBox43.Text  = "";
            this.textBox48.Text  = "";
            this.textBox49.Text  = "";
            this.textBox122.Text = "";
            this.textBox116.Text = "";
            this.textBox106.Text = "";
            this.textBox51.Text  = "";
            this.textBox52.Text  = "";
            this.textBox53.Text  = "";
            this.textBox123.Text = "";
            this.textBox117.Text = "";
        }
        private void BalankFldMarMem()
        {
            this.textBox72.Text  = "";
            this.textBox73.Text  = "";
            this.textBox78.Text  = "";
            this.textBox75.Text  = "";
            this.textBox76.Text  = "";
            this.textBox77.Text  = "";
            this.textBox79.Text  = "";
            this.textBox80.Text  = "";
            this.textBox81.Text  = "";
            this.textBox82.Text  = "";
            this.textBox83.Text  = "";
            this.textBox84.Text  = "";
            this.textBox85.Text  = "";
            this.textBox86.Text  = "";
            this.textBox87.Text  = "";
            this.textBox88.Text  = "";

            this.textBox2.Text   = "0";
            this.textBox3.Text   = "0";
            this.textBox4.Text   = "0";
            this.textBox5.Text   = "0";
            this.textBox6.Text   = "0";
            this.textBox7.Text   = "0";
            this.textBox8.Text   = "0";
            this.textBox9.Text   = "0";
            this.textBox10.Text  = "0";
            this.textBox11.Text  = "0";
            this.textBox12.Text  = "0";
            this.textBox13.Text  = "0";
            this.textBox14.Text  = "0";
            this.textBox15.Text  = "0";
            this.textBox16.Text  = "0";
            this.textBox17.Text  = "0";
            this.textBox18.Text  = "0";
            this.textBox19.Text  = "0";
            this.textBox20.Text  = "0";
            this.textBox21.Text  = "0";
            this.textBox22.Text  = "0";
            this.textBox23.Text  = "0";
            this.textBox24.Text  = "0";
            this.textBox25.Text  = "0";
            this.textBox26.Text  = "0";
            this.textBox27.Text  = "0";
            this.textBox28.Text  = "0";
            this.textBox29.Text  = "0";
            this.textBox30.Text  = "0";
            this.textBox31.Text  = "0";
            this.textBox54.Text  = "0";
            this.textBox38.Text  = "0";

            this.label9.Text     = "0";
            this.label13.Text    = "0";
            this.label17.Text    = "0";
            this.label24.Text    = "0";
            this.label28.Text    = "0";
            this.label32.Text    = "0";
            this.label36.Text    = "0";
            this.label40.Text    = "0";
            this.label44.Text    = "0";
            this.label48.Text    = "0";
            this.label52.Text    = "0";
            this.label56.Text    = "0";
            this.label60.Text    = "0";
            this.label64.Text    = "0";
            this.label68.Text    = "0";
            this.label76.Text    = "0";
            this.textBox90.Text  = "0";

            this.textBox56.Text  = "0";
            this.textBox57.Text  = "0";
            this.textBox58.Text  = "0";
            this.textBox59.Text  = "0";
            this.textBox60.Text  = "0";
            this.textBox61.Text  = "0";
            this.textBox62.Text  = "0";
            this.textBox63.Text  = "0";
            this.textBox64.Text  = "0";
            this.textBox65.Text  = "0";
            this.textBox66.Text  = "0";
            this.textBox67.Text  = "0";
            this.textBox68.Text  = "0";
            this.textBox69.Text  = "0";
            this.textBox70.Text  = "0";
            this.textBox71.Text  = "0";
            this.textBox89.Text  = "0";
            this.textBox91.Text  = "0";
            this.textBox110.Text = "0";
            this.textBox111.Text = "0";
            this.textBox112.Text = "0";
            this.textBox113.Text = "0";
            this.textBox114.Text = "0";
            this.textBox115.Text = "0";

            this.textBox90.Text  = "0";
            this.textBox55.Text  = "0";
            this.label147.Text   = "0";
            this.label10.Text    = "0";

            this.label179.Text   = "0";
            this.label172.Text   = "0";
            this.label171.Text   = "0";
            this.label170.Text   = "0";
            this.label165.Text   = "0";
            this.label164.Text   = "0";
            this.label163.Text   = "0";
            this.label162.Text   = "0";
            this.label157.Text   = "0";
            this.label156.Text   = "0";
            this.label155.Text   = "0";
            this.label154.Text   = "0";
            this.label169.Text   = "0";
            this.label168.Text   = "0";
            this.label167.Text   = "0";
            this.label166.Text   = "0";
            this.label161.Text   = "0";
            this.label160.Text   = "0";
            this.label159.Text   = "0";
            this.label158.Text   = "0";
            this.label153.Text   = "0";
            this.label152.Text   = "0";
            this.label151.Text   = "0";
            this.label150.Text   = "0";
        }
        private void AllItemAdd()
        {
            try
            {
                Label[] labels = {
                    label9, label13, label17, label24, label28, label32,
                    label36, label40, label44, label48, label52, label56,
                    label60, label64, label68, label76
                };
                int totalItemSum = 0;
                foreach (var label in labels)
                {
                    if (int.TryParse(label.Text.Trim(), out int value))
                    {
                        totalItemSum += value;
                    }
                    else
                    {
                        MessageBox.Show($"Error parsing value from {label.Name}: {label.Text}");
                    }
                }
                label10.Text = totalItemSum.ToString();
                textBox115.Text = label10.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void AllIinvAdd()
        {
            try
            {
                Label[] labels = {
                    label179, label172, label171, label170, label169, label168,
                    label167, label166, label165, label164, label163, label162,
                    label161, label160, label159, label158, label157, label156,
                    label155, label154, label153, label152, label151, label150
                };
                int sumNums = 0;
                foreach (var label in labels)
                {
                    if (int.TryParse(label.Text.Trim(), out int value))
                    {
                        sumNums += value;
                    }
                    else
                    {
                        MessageBox.Show($"Error parsing value from {label.Name}: {label.Text}");
                    }
                }
                textBox90.Text = sumNums.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void totalDailyData()
        {
            try
            {
                var queries = new[]
                {
                    new { Query = "SELECT SUM(NotTaken) FROM Daily WHERE [D_Data]='NTKN'", Label = label94 },
                    new { Query = "SELECT SUM(C_Amount) FROM DailyCut", Label = label121 },
                    new { Query = "SELECT SUM(NotTaken) FROM DailySaving WHERE [DS_Data]='NTKN'", Label = label254 }
                };
                foreach (var entry in queries)
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(entry.Query, conn))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        if (dataTable.Rows.Count > 0)
                        {
                            entry.Label.Text = dataTable.Rows[0][0].ToString();
                        }
                        else
                        {
                            entry.Label.Text = "0";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void totalDailyAntData()
        {
            try
            {
                string query = "SELECT SUM(NotTaken) FROM DailyAnt WHERE [DA_Data]='NTKN'";
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value)
                    {
                        this.label263.Text = dataTable.Rows[0][0].ToString();
                    }
                    else
                    {
                        this.label263.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void totalInstData()
        {
            try
            {
                string query = "SELECT SUM(InsPay) FROM Installment";
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value)
                    {
                        this.label211.Text = dataTable.Rows[0][0].ToString();
                    }
                    else
                    {
                        this.label211.Text = "0"; 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void fillMemo()
        {
            try
            {
                string query = "SELECT Mem_ID as [ID], Mem_Date as [Date], Giv_TK as [Given], R_InvTK as [Main], C_InvTK as [CAmt], Ret_TK as [Return] FROM MarketMemos ORDER BY Mem_Date DESC";
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView11.DataSource = dataTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void fillImageData()
        {
            try
            {
                //string query = "SELECT Img_ID as [ID] FROM Images";
                //using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                //{
                //    DataTable dataTable = new DataTable();
                //    adapter.Fill(dataTable);
                //    dataGridView14.DataSource = dataTable.DefaultView;
                //}

                //DataTable dataTabledltAmt = new DataTable();
                //OleDbDataAdapter odbcDataAdapterdltAmt = new OleDbDataAdapter(string.Concat("SELECT Img_ID as [ID] FROM Images"), this.conn);
                //odbcDataAdapterdltAmt.Fill(dataTabledltAmt);
                //dataGridView14.DataSource = dataTabledltAmt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void checkBoxClear()
        {
            this.checkBox1.Checked = false;
            this.checkBox2.Checked = false;
            this.checkBox3.Checked = false;
            this.checkBox4.Checked = false;
            this.checkBox5.Checked = false;
        }

        //-----------------------------------------------------------------------
        //------------------------------All Button Work--------------------------
        //-----------------------------------------------------------------------
        private void button2_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            this.button1.Text = "Add";
            this.BalankFldMarMem();
            this.label10.Text = "0";
            this.button15.Text = "New";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.button1.Text == "Add")
            {
                this.textBox1.ReadOnly = false;
                this.textBox1.Focus();
                TextBox textBox = this.textBox101;
                string[] strArrays = new string[] { "ME", null, null, null, null };
                int date = DateTime.Now.Day;
                int month = DateTime.Now.Month;
                int millis = DateTime.Now.Millisecond;
                strArrays[2] = date.ToString();
                strArrays[3] = month.ToString();
                strArrays[4] = millis.ToString();
                textBox.Text = string.Concat(strArrays);
                this.button1.Text = "Save";
                this.BalankFldMarMem();
            }
            else if (this.button1.Text == "Save")
            {
                if (!(this.textBox1.Text.Trim() != ""))
                {
                    this.textBox1.Focus();
                }
                else
                {
                    try
                    {
                        this.conn.Open();
                        string query = "INSERT INTO Market(M_ID, M_Date, M_Amount, M_Insrt_Person) VALUES (?, ?, ?, ?)";
                        using (OleDbCommand insComm = new OleDbCommand(query, this.conn))
                        {
                            insComm.Parameters.AddWithValue("@M_ID", textBox101.Text.Trim());
                            insComm.Parameters.AddWithValue("@M_Date", dateTimePicker1.Text.Trim());
                            insComm.Parameters.AddWithValue("@M_Amount", textBox1.Text.Trim());
                            insComm.Parameters.AddWithValue("@M_Insrt_Person", label249.Text.Trim());
                            insComm.ExecuteNonQuery();
                        }
                        this.conn.Close();
                        MessageBox.Show("Data added successfully");
                        this.fillData();
                        this.AmtDataView();
                        this.textBox1.ReadOnly = true;
                        this.textBox1.Text = "";
                        this.button1.Text = "Add";
                        this.BalankFldMarMem();
                    }
                    catch (Exception ex)
                    {
                        this.conn.Close();
                        MessageBox.Show("Error : " + ex.Message);
                    }
                }
            }
            else if (this.button1.Text == "Updt")
            {
                try
                {
                    this.conn.Open();
                    string query = "UPDATE Market SET M_Amount = ?, M_Date = ?, M_Updt_Person = ? WHERE M_ID = ?";
                    OleDbCommand updtComm = new OleDbCommand(query, this.conn);
                    updtComm.Parameters.AddWithValue("@M_Amount", this.textBox1.Text.Trim());
                    updtComm.Parameters.AddWithValue("@M_Date", this.dateTimePicker1.Text.Trim());
                    updtComm.Parameters.AddWithValue("@M_Updt_Person", this.label249.Text.Trim());
                    updtComm.Parameters.AddWithValue("@M_ID", this.label6.Text.Trim());
                    updtComm.ExecuteNonQuery();
                    this.conn.Close();
                    MessageBox.Show(string.Concat("Successfull Update - ", this.label6.Text));
                    this.fillData();
                    this.AmtDataView();
                    this.textBox1.ReadOnly = true;
                    this.textBox1.Text = "";
                    this.label6.Text = "";
                    this.button1.Text = "Add";
                    this.BalankFldMarMem();
                }
                catch (Exception ex)
                {
                    this.conn.Close();
                    MessageBox.Show("Error : " + ex.Message);
                }
            }
            else if (this.button1.Text == "U to M")
            {
                try
                {
                    using (OleDbConnection insrtconn = new OleDbConnection(connAcc))
                    {
                        insrtconn.Open();
                        string query = "INSERT INTO Market(M_ID, M_Date, M_Amount, M_Insrt_Person) VALUES (?, ?, ?, ?)";
                        using (OleDbCommand accInsComm = new OleDbCommand(query, insrtconn))
                        {
                            accInsComm.Parameters.AddWithValue("@M_ID", textBox108.Text.Trim());
                            accInsComm.Parameters.AddWithValue("@M_Date", dateTimePicker1.Text.Trim());
                            accInsComm.Parameters.AddWithValue("@M_Amount", label10.Text.Trim());
                            accInsComm.Parameters.AddWithValue("@M_Insrt_Person", label249.Text.Trim());
                            accInsComm.ExecuteNonQuery();
                        }
                        insrtconn.Close();
                        MessageBox.Show("Successfull Memo Amount Added");
                    }
                    this.fillData();
                    this.AmtDataView();
                    this.button1.Text = "Add";
                    this.BalankFldMarMem();
                }
                catch (Exception ex)
                {
                    this.conn.Close();
                    MessageBox.Show("Error : " + ex.Message);
                }
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {            
            if (this.button6.Text == "New")
            {
                this.textBox39.ReadOnly = false;
                this.textBox39.Text = "";
                this.textBox33.ReadOnly = false;
                this.textBox33.Text = "";
                this.comboBox1.Enabled = true;
                this.dateTimePicker3.Enabled = true;
                this.textBox34.ReadOnly = false;
                this.textBox34.Text = "";
                this.textBox39.Focus();
                this.button6.Text = "Save";
                this.radioButton1.Enabled = true;
                this.radioButton2.Enabled = true;
                this.radioButton3.Enabled = true;
                this.radioButton5.Enabled = true;
                this.radioButton4.Enabled = true;
            }
            else if (this.button6.Text == "Save")
            {
                if (this.radioButton5.Checked)
                {
                    try
                    {
                        this.conn.Open();
                        string query = "INSERT INTO Given (InGiven, Total_Given, Given_To, ThroughBy, Given_Date, Remarks_Given, GDT_V, G_Insrt_Person) VALUES (?, ?, ?, ?, ?, ?, 'NDV', ?)";
                        using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
                        {
                            cmd.Parameters.AddWithValue("@InGiven", this.textBox35.Text.Trim());
                            cmd.Parameters.AddWithValue("@Total_Given", this.textBox39.Text.Trim());
                            cmd.Parameters.AddWithValue("@Given_To", this.textBox33.Text.Trim());
                            cmd.Parameters.AddWithValue("@ThroughBy", this.comboBox1.Text.Trim());
                            cmd.Parameters.AddWithValue("@Given_Date", this.dateTimePicker3.Text.Trim()); 
                            cmd.Parameters.AddWithValue("@Remarks_Given", this.textBox34.Text.Trim());
                            cmd.Parameters.AddWithValue("@G_Insrt_Person", this.label249.Text.Trim());
                            cmd.ExecuteNonQuery();
                        }
                        this.conn.Close();
                        MessageBox.Show(string.Concat("Successfull Added to Given"));
                        this.fillGivenData();
                        this.AmtCrDataView();
                        this.button6.Text = "New";
                    }
                    catch (Exception ex)
                    {
                        this.conn.Close();
                        MessageBox.Show("Error : " + ex.Message);
                    }
                }
                else if(this.radioButton4.Checked)
                { 
                    try
                    {
                        this.conn.Open();
                        string query = "INSERT INTO Teken (InTake, Total_Take, Take_To, ThroughBy, Take_Date, Remarks_Take, TDT_V, T_Insrt_Person) VALUES (?, ?, ?, ?, ?, ?, 'NDV', ?)";
                        using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
                        {
                            cmd.Parameters.AddWithValue("@InTake", this.textBox35.Text.Trim());
                            cmd.Parameters.AddWithValue("@Total_Take", this.textBox39.Text.Trim());
                            cmd.Parameters.AddWithValue("@Take_To", this.textBox33.Text.Trim());
                            cmd.Parameters.AddWithValue("@ThroughBy", this.comboBox1.Text.Trim());
                            cmd.Parameters.AddWithValue("@Take_Date", this.dateTimePicker3.Text.Trim());
                            cmd.Parameters.AddWithValue("@Remarks_Take", this.textBox34.Text.Trim());
                            cmd.Parameters.AddWithValue("@T_Insrt_Person", this.label249.Text.Trim());
                            cmd.ExecuteNonQuery();
                        }
                        this.conn.Close();
                        MessageBox.Show(string.Concat("Successfull Added to Taken"));
                        this.fillGivenData();
                        this.AmtCrDataView();
                        this.button6.Text = "New";
                    }
                    catch (Exception ex)
                    {
                        this.conn.Close();
                        MessageBox.Show("Error : " + ex.Message);
                    }
                }
                else if (this.radioButton3.Checked)
                {
                    try
                    {
                        this.conn.Open();
                        string query = "INSERT INTO TariffAmt (InExpense, Expense_Amount, Expense_To, ThroughBy, Expense_Date, Remarks_Expense, EDT_V, E_Insrt_Person) VALUES (?, ?, ?, ?, ?, ?, 'NDV', ?)";
                        using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
                        {
                            cmd.Parameters.AddWithValue("@InExpense", this.textBox35.Text.Trim());
                            cmd.Parameters.AddWithValue("@Expense_Amount", this.textBox39.Text.Trim());
                            cmd.Parameters.AddWithValue("@Expense_To", this.textBox33.Text.Trim());
                            cmd.Parameters.AddWithValue("@ThroughBy", this.comboBox1.Text.Trim());
                            cmd.Parameters.AddWithValue("@Expense_Date", this.dateTimePicker3.Text.Trim());
                            cmd.Parameters.AddWithValue("@Remarks_Expense", this.textBox34.Text.Trim());
                            cmd.Parameters.AddWithValue("@E_Insrt_Person", this.label249.Text.Trim());
                            cmd.ExecuteNonQuery();
                        }
                        this.conn.Close();
                        MessageBox.Show(string.Concat("Successfull Added to Expense"));
                        this.fillGivenData();
                        this.AmtCrDataView();
                        this.button6.Text = "New";
                    }
                    catch (Exception ex)
                    {
                        this.conn.Close();
                        MessageBox.Show("Error : " + ex.Message);
                    }
                }
                else if (this.radioButton1.Checked)
                {
                    try
                    {
                        this.conn.Open();
                        string query = "INSERT INTO Saving (InSaving, Saving_Amount, Saving_To, ThroughBy, Saving_Date, Remarks_Saving, SDT_V, Saving_Bank, S_Insrt_Person) VALUES (?, ?, ?, ?, ?, ?, 'NDV', ?, ?)";
                        using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
                        {
                            cmd.Parameters.AddWithValue("@InSaving", this.textBox35.Text.Trim());
                            cmd.Parameters.AddWithValue("@Saving_Amount", this.textBox39.Text.Trim());
                            cmd.Parameters.AddWithValue("@Saving_To", this.textBox33.Text.Trim());
                            cmd.Parameters.AddWithValue("@ThroughBy", this.comboBox1.Text.Trim());
                            cmd.Parameters.AddWithValue("@Saving_Date", this.dateTimePicker3.Text.Trim());
                            cmd.Parameters.AddWithValue("@Remarks_Saving", this.textBox34.Text.Trim());
                            cmd.Parameters.AddWithValue("@Saving_Bank", this.comboBox1.Text.Trim());
                            cmd.Parameters.AddWithValue("@S_Insrt_Person", this.label249.Text.Trim());
                            cmd.ExecuteNonQuery();
                        }
                        this.conn.Close();
                        MessageBox.Show(string.Concat("Successfull Added to Saving"));
                        this.fillGivenData();
                        this.AmtCrDataView();
                        this.button6.Text = "New";
                    }
                    catch (Exception ex)
                    {
                        this.conn.Close();
                        MessageBox.Show("Error : " + ex.Message);
                    }
                }
                else if (this.radioButton2.Checked)
                {
                    try
                    {
                        this.conn.Open();
                        string query = "INSERT INTO Unrated (InUnrated, Unrated_Amount, Unrated_To, ThroughBy, Unrated_Date, Remarks_Unrated, UDT_V, U_Insrt_Person) VALUES (?, ?, ?, ?, ?, ?, 'NDV', ?)";
                        using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
                        {
                            cmd.Parameters.AddWithValue("@InUnrated", this.textBox35.Text.Trim());
                            cmd.Parameters.AddWithValue("@Unrated_Amount", this.textBox39.Text.Trim());
                            cmd.Parameters.AddWithValue("@Unrated_To", this.textBox33.Text.Trim());
                            cmd.Parameters.AddWithValue("@ThroughBy", this.comboBox1.Text.Trim());
                            cmd.Parameters.AddWithValue("@Unrated_Date", this.dateTimePicker3.Text.Trim());
                            cmd.Parameters.AddWithValue("@Remarks_Unrated", this.textBox34.Text.Trim());
                            cmd.Parameters.AddWithValue("@U_Insrt_Person", this.label249.Text.Trim());
                            cmd.ExecuteNonQuery();
                        }
                        this.conn.Close();
                        MessageBox.Show(string.Concat("Successfull Added to Unrated"));
                        this.fillGivenData();
                        this.AmtCrDataView();
                        this.button6.Text = "New";
                    }
                    catch (Exception ex)
                    {
                        this.conn.Close();
                        MessageBox.Show("Error : " + ex.Message);
                    }
                }
            }
        }
        private void button19_Click(object sender, EventArgs e)
        {
            try
            {
                this.conn.Open();
                string query = "UPDATE Given SET Total_Given = ?, GDT_V_Date = ?, G_Updt_Person = ? WHERE InGiven = ?";
                using (OleDbCommand command = new OleDbCommand(query, this.conn))
                {
                    command.Parameters.AddWithValue("@TotalGiven", this.textBox40.Text.Trim());
                    command.Parameters.AddWithValue("@GDT_V_Date", this.DltDate);
                    command.Parameters.AddWithValue("@G_Updt_Person", this.label249.Text.Trim());
                    command.Parameters.AddWithValue("@InGiven", this.label117.Text.Trim());
                    command.ExecuteNonQuery();
                }
                this.conn.Close();
                MessageBox.Show($"Successfully Given TK Update For - {this.label117.Text} ");
                this.AmtCrDataView();
                this.BalankFld();
                this.fillGivenData();
                this.checkBoxClear();
                this.textBox109.Text = "";
            }
            catch (Exception ex)
            {
                this.conn.Close();
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void button20_Click(object sender, EventArgs e)
        {
            try
            {
                this.conn.Open();
                string query = "UPDATE Teken SET Total_Take = ?, TDT_V_Date = ?, T_Updt_Person = ? WHERE InTake = ?";
                using (OleDbCommand command = new OleDbCommand(query, this.conn))
                {
                    command.Parameters.AddWithValue("@TotalTake", this.textBox45.Text.Trim());
                    command.Parameters.AddWithValue("@TDT_V_Date", this.DltDate);
                    command.Parameters.AddWithValue("@T_Updt_Person", this.label249.Text.Trim());
                    command.Parameters.AddWithValue("@InTake", this.label117.Text.Trim());
                    command.ExecuteNonQuery();
                }
                this.conn.Close();
                MessageBox.Show($"Successfully Teken TK Update For - {this.label117.Text} ");
                this.AmtCrDataView();
                this.BalankFld();
                this.fillGivenData();
                this.checkBoxClear();
                this.textBox109.Text = "";
            }
            catch (Exception ex)
            {
                this.conn.Close();
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void button16_Click(object sender, EventArgs e)
        {
            try
            {
                this.conn.Open();
                string query = "UPDATE TariffAmt SET Expense_Amount = ?, EDT_V_Date = ?, E_Updt_Person = ? WHERE InExpense = ?";
                using (OleDbCommand command = new OleDbCommand(query, this.conn))
                {
                    command.Parameters.AddWithValue("@ExpenseAmount", this.textBox103.Text.Trim());
                    command.Parameters.AddWithValue("@EDT_V_Date", this.DltDate);
                    command.Parameters.AddWithValue("@E_Updt_Person", this.label249.Text.Trim());
                    command.Parameters.AddWithValue("@InExpense", this.label117.Text.Trim());
                    command.ExecuteNonQuery();
                }
                this.conn.Close();
                MessageBox.Show($"Successfully Expance TK Update For - {this.label117.Text} ");
                this.AmtCrDataView();
                this.BalankFld();
                this.fillGivenData();
                this.checkBoxClear();
                this.textBox109.Text = "";
                this.button6.Text = "New";
            }
            catch (Exception ex)
            {
                this.conn.Close();
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void button17_Click(object sender, EventArgs e)
        {
            try
            {
                this.conn.Open();
                string query = "UPDATE Saving SET Saving_Amount = ?, SDT_V_Date = ?, S_Updt_Person = ? WHERE InSaving = ?";
                using (OleDbCommand command = new OleDbCommand(query, this.conn))
                {
                    command.Parameters.AddWithValue("@SavingAmount", this.textBox43.Text.Trim());
                    command.Parameters.AddWithValue("@SDT_V_Date", this.DltDate);
                    command.Parameters.AddWithValue("@S_Updt_Person", this.label249.Text.Trim());
                    command.Parameters.AddWithValue("@InSaving", this.label117.Text.Trim());
                    command.ExecuteNonQuery();
                }
                this.conn.Close();
                MessageBox.Show($"Successfully Saving TK Update For - {this.label117.Text} ");
                this.AmtCrDataView();
                this.BalankFld();
                this.fillGivenData();
                this.checkBoxClear();
                this.textBox116.Text = "";
                this.button6.Text = "New";
            }
            catch (Exception ex)
            {
                this.conn.Close();
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void button18_Click(object sender, EventArgs e)
        {
            try
            {
                this.conn.Open();
                string query = "UPDATE Unrated SET Unrated_Amount = ?, UDT_V_Date = ?, U_Updt_Person = ? WHERE InUnrated = ?";
                using (OleDbCommand command = new OleDbCommand(query, this.conn))
                {
                    command.Parameters.AddWithValue("@UnratedAmount", this.textBox51.Text.Trim());
                    command.Parameters.AddWithValue("@UDT_V_Date", this.DltDate);
                    command.Parameters.AddWithValue("@U_Updt_Person", this.label249.Text.Trim());
                    command.Parameters.AddWithValue("@InUnrated", this.label117.Text.Trim());
                    command.ExecuteNonQuery();
                }
                this.conn.Close();
                MessageBox.Show($"Successfully Unrated TK Update For - {this.label117.Text} ");
                this.AmtCrDataView();
                this.BalankFld();
                this.fillGivenData();
                this.checkBoxClear();
                this.textBox117.Text = "";
                this.button6.Text = "New";
            }
            catch (Exception ex)
            {
                this.conn.Close();
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            this.button6.Text = "New";
            this.radioButton1.Enabled = false;
            this.radioButton2.Enabled = false;
            this.radioButton3.Enabled = false;
            this.radioButton4.Enabled = false;
            this.radioButton5.Enabled = false;
            this.textBox39.ReadOnly = true;
            this.textBox33.ReadOnly = true;
            this.comboBox1.Enabled = false;
            this.dateTimePicker3.Enabled = false;
            this.textBox34.ReadOnly = true;
            this.button7.Visible = false;
            this.BalankFld();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            if (this.button7.Text == "Delete G.")
            {
                try
                {
                    this.conn.Open();
                    string query = "UPDATE Given SET GDT_V = 'DDV', DDT_V_Date = ?, G_Del_Person = ? WHERE InGiven = ?";
                    using (OleDbCommand command = new OleDbCommand(query, this.conn))
                    {
                        command.Parameters.AddWithValue("@DDT_V_Date", this.DltDate);
                        command.Parameters.AddWithValue("@G_Del_Person", this.label249.Text.Trim());
                        command.Parameters.AddWithValue("@InGiven", this.label117.Text.Trim());
                        command.ExecuteNonQuery();
                    }
                    this.conn.Close();
                    MessageBox.Show($"Successfull Deleted - [{this.label117.Text}]");
                    this.BalankFld();
                    this.AmtCrDataView();
                    this.fillGivenData();
                    this.button7.Visible = false;
                }
                catch (Exception ex)
                {
                    this.conn.Close();
                    MessageBox.Show("Error : " + ex.Message);
                }
            }
            else if (this.button7.Text == "Delete T.")
            {
                try
                {
                    this.conn.Open();
                    string query = "UPDATE Teken SET TDT_V = 'DDV', DDT_V_Date = ?, T_Del_Person = ? WHERE InTake = ?";
                    using (OleDbCommand command = new OleDbCommand(query, this.conn))
                    {
                        command.Parameters.AddWithValue("@DDT_V_Date", this.DltDate);
                        command.Parameters.AddWithValue("@T_Del_Person", this.label249.Text.Trim());
                        command.Parameters.AddWithValue("@InTake", this.label117.Text.Trim());
                        command.ExecuteNonQuery();
                    }
                    this.conn.Close();
                    MessageBox.Show($"Successfull Deleted - [{this.label117.Text}]");
                    this.BalankFld();
                    this.AmtCrDataView();
                    this.fillGivenData();
                    this.button7.Visible = false;
                }
                catch (Exception ex)
                {
                    this.conn.Close();
                    MessageBox.Show("Error : " + ex.Message);
                }
            }
            else if (this.button7.Text == "Delete E.")
            {
                try
                {
                    this.conn.Open();
                    string query = "UPDATE TariffAmt SET EDT_V = 'DDV', DDT_V_Date = ?, E_Del_Person = ? WHERE InExpense = ?";
                    using (OleDbCommand command = new OleDbCommand(query, this.conn))
                    {
                        command.Parameters.AddWithValue("@DDT_V_Date", this.DltDate);
                        command.Parameters.AddWithValue("@E_Del_Person", this.label249.Text.Trim());
                        command.Parameters.AddWithValue("@InExpense", this.label117.Text.Trim());
                        command.ExecuteNonQuery();
                    }
                    this.conn.Close();
                    MessageBox.Show($"Successfull Deleted - [{this.label117.Text}]");
                    this.BalankFld();
                    this.AmtCrDataView();
                    this.fillGivenData();
                    this.button7.Visible = false;
                }
                catch (Exception ex)
                {
                    this.conn.Close();
                    MessageBox.Show("Error : " + ex.Message);
                }
            }
            else if (this.button7.Text == "Delete S.")
            {
                try
                {
                    this.conn.Open();
                    string query = "UPDATE Saving SET SDT_V = 'DDV', DDT_V_Date = ?, S_Del_Person = ? WHERE InSaving = ?";
                    using (OleDbCommand command = new OleDbCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@DDT_V_Date", this.DltDate);
                        command.Parameters.AddWithValue("@S_Del_Person", this.label249.Text.Trim());
                        command.Parameters.AddWithValue("@InSaving", this.label117.Text.Trim());
                        command.ExecuteNonQuery();
                    }
                    this.conn.Close();
                    MessageBox.Show($"Successfull Deleted - [{this.label117.Text}]");
                    this.BalankFld();
                    this.AmtCrDataView();
                    this.fillGivenData();
                    this.button7.Visible = false;
                }
                catch (Exception ex)
                {
                    this.conn.Close();
                    MessageBox.Show("Error : " + ex.Message);
                }
            }
            else if (this.button7.Text == "Delete U.")
            {
                try
                {
                    this.conn.Open();
                    string query = "UPDATE Unrated SET UDT_V = 'DDV', DDT_V_Date = ?, U_Del_Person = ? WHERE InUnrated = ?";
                    using (OleDbCommand command = new OleDbCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@DDT_V_Date", this.DltDate);
                        command.Parameters.AddWithValue("@U_Del_Person", this.label249.Text.Trim());
                        command.Parameters.AddWithValue("@InUnrated", this.label117.Text.Trim());
                        command.ExecuteNonQuery();
                    }
                    this.conn.Close();
                    MessageBox.Show($"Successfull Deleted - [{this.label117.Text}]");
                    this.BalankFld();
                    this.AmtCrDataView();
                    this.fillGivenData();
                    this.button7.Visible = false;
                }
                catch (Exception ex)
                {
                    this.conn.Close();
                    MessageBox.Show("Error : " + ex.Message);
                }
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            if (this.button10.Text == "Add")
            {
                this.textBox37.ReadOnly = false;
                this.textBox37.Focus();
                TextBox textBox = this.textBox92;
                string[] strArrays = new string[] { "D", null, null, null, null };
                int date = DateTime.Now.Day;
                int month = DateTime.Now.Month;
                int millis = DateTime.Now.Millisecond;
                strArrays[2] = date.ToString();
                strArrays[3] = month.ToString();
                strArrays[4] = millis.ToString();
                textBox.Text = string.Concat(strArrays);
                this.button10.Text = "Save";
            }
            else if (this.button10.Text == "Save")
            {
                if (!(this.textBox37.Text.Trim() != ""))
                {
                    this.textBox37.Focus();
                }
                else
                {
                    try
                    {
                        this.conn.Open();
                        string query = "INSERT INTO Daily (D_ID, D_Date, D_FPAmount, D_SPAmount, NotTaken, D_Data, D_Insrt_Person) VALUES (?, ?, ?, ?, ?, 'NTKN', ?)";
                        using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
                        {
                            cmd.Parameters.AddWithValue("@D_ID", this.textBox92.Text.Trim());
                            cmd.Parameters.AddWithValue("@D_Date", this.dateTimePicker4.Text.Trim()); //.Value.ToString("yyyy-MM-dd"))
                            cmd.Parameters.AddWithValue("@D_FPAmount", this.textBox37.Text.Trim());
                            cmd.Parameters.AddWithValue("@D_SPAmount", this.label194.Text.Trim());
                            cmd.Parameters.AddWithValue("@NotTaken", this.label194.Text.Trim());
                            cmd.Parameters.AddWithValue("@D_Insrt_Person", this.label249.Text.Trim());
                            cmd.ExecuteNonQuery();
                        }
                        this.conn.Close();
                        MessageBox.Show(string.Concat("Successfull Daily Data Added"));
                        this.fillDailyData();
                        this.totalDailyData();
                        this.textBox37.ReadOnly = true;
                        this.textBox37.Text = "";
                        this.textBox92.Text = "";
                        this.button10.Text = "Add";
                    }
                    catch (Exception ex)
                    {
                        this.conn.Close();
                        MessageBox.Show("Error : " + ex.Message);
                    }
                }
            }
            else if (this.button10.Text == "Updt")
            {
                try
                {
                    this.conn.Open();
                    string query = "UPDATE Daily SET D_FPAmount = ?, D_SPAmount = ?, NotTaken = ?, D_Date = ?, D_Updt_Person = ? WHERE D_ID = ?";
                    using (OleDbCommand command = new OleDbCommand(query, this.conn))
                    {
                        command.Parameters.AddWithValue("@D_FPAmount", this.textBox37.Text.Trim());
                        command.Parameters.AddWithValue("@D_SPAmount", this.label194.Text.Trim());
                        command.Parameters.AddWithValue("@NotTaken", this.label194.Text.Trim());
                        command.Parameters.AddWithValue("@D_Date", this.dateTimePicker4.Text.Trim()); //.Value.ToString("yyyy-MM-dd"))
                        command.Parameters.AddWithValue("@D_Updt_Person", this.label249.Text.Trim());
                        command.Parameters.AddWithValue("@D_ID", this.label182.Text.Trim());
                        command.ExecuteNonQuery();
                    }
                    this.conn.Close();
                    MessageBox.Show(string.Concat("Successfull Update Daily Get Data"));
                    this.fillDailyData();
                    this.totalDailyData();
                    this.textBox37.ReadOnly = true;
                    this.textBox37.Text = "";
                    this.label182.Text = "0";
                    this.label185.Text = "0";
                    this.label187.Text = "0";
                    this.label189.Text = "0";
                    this.button10.Text = "Add";
                }
                catch (Exception ex)
                {
                    this.conn.Close();
                    MessageBox.Show("Error : " + ex.Message);
                }
            }
        }
        private void button14_Click(object sender, EventArgs e)
        {
            if (this.button14.Text == "Add")
            {
                this.textBox50.ReadOnly = false;
                this.textBox50.Focus();
                TextBox textBox = this.textBox92;
                string[] strArrays = new string[] { "C", null, null, null, null };
                int date = DateTime.Now.Day;
                int month = DateTime.Now.Month;
                int millis = DateTime.Now.Millisecond;
                strArrays[2] = date.ToString();
                strArrays[3] = month.ToString();
                strArrays[4] = millis.ToString();
                textBox.Text = string.Concat(strArrays);
                this.button14.Text = "Add Amt";
            }
            else if (this.button14.Text == "Add Amt")
            {
                if (!(this.textBox50.Text.Trim() != ""))
                {
                    this.textBox50.Focus();
                }
                else
                {
                    try
                    {
                        this.conn.Open();
                        string query = "INSERT INTO DailyCut (C_ID, C_Date, C_Amount, C_Insrt_Person) VALUES (?, ?, ?, ?)";
                        using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
                        {
                            cmd.Parameters.AddWithValue("@C_ID", this.textBox92.Text.Trim());
                            cmd.Parameters.AddWithValue("@C_Date", this.dateTimePicker5.Text.Trim());
                            cmd.Parameters.AddWithValue("@C_Amount", this.textBox50.Text.Trim());
                            cmd.Parameters.AddWithValue("@C_Insrt_Person", this.label249.Text.Trim());
                            cmd.ExecuteNonQuery();
                        }
                        MessageBox.Show(string.Concat("Successfull Added Total Daily Amount"));
                        this.fillDailyData();
                        this.totalDailyData();
                        this.textBox50.ReadOnly = true;
                        this.textBox50.Text = "";
                        this.textBox92.Text = "";
                        this.button14.Text = "Add";
                    }
                    catch (Exception ex)
                    {
                        this.conn.Close();
                        MessageBox.Show("Error : " + ex.Message);
                    }
                }
            }
            else if (this.button14.Text == "Updt")
            {
                try
                {
                    this.conn.Open();
                    string query = "UPDATE DailyCut SET C_Amount = ?, C_Date = ?, C_Updt_Person = ? WHERE C_ID = ?";
                    using (OleDbCommand command = new OleDbCommand(query, this.conn))
                    {
                        command.Parameters.AddWithValue("@C_Amount", this.textBox50.Text.Trim());
                        command.Parameters.AddWithValue("@C_Date", this.dateTimePicker5.Text.Trim());
                        command.Parameters.AddWithValue("@C_Updt_Person", this.label249.Text.Trim());
                        command.Parameters.AddWithValue("@C_ID", this.label182.Text.Trim());
                        command.ExecuteNonQuery();
                    }
                    this.conn.Close();
                    MessageBox.Show(string.Concat("Successfull Update Daily Gat"));
                    this.fillDailyData();
                    this.totalDailyData();
                    this.textBox50.ReadOnly = true;
                    this.textBox50.Text = "";
                    this.label182.Text = "0";
                    this.label191.Text = "0";
                    this.button14.Text = "Add";
                }
                catch (Exception ex)
                {
                    this.conn.Close();
                    MessageBox.Show("Error : " + ex.Message);
                }
            }
        }
        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                this.conn.Open();
                string query = "UPDATE Daily SET D_Data = 'TKN', TakenDate = ?, D_Del_Person = ? WHERE D_ID = ?";
                using (OleDbCommand command = new OleDbCommand(query, this.conn))
                {
                    command.Parameters.AddWithValue("@TakenDate", this.DltDate);
                    command.Parameters.AddWithValue("@D_Del_Person", this.label249.Text.Trim());
                    command.Parameters.AddWithValue("@D_ID", this.label182.Text.Trim());
                    command.ExecuteNonQuery();
                }
                this.conn.Close();
                MessageBox.Show($"Successfull Deleted - [{this.label182.Text}]");
                this.fillDailyData();
                this.AmtCrDataView();
                if (this.dataGridView5.RowCount > 0)
                {
                    this.totalDailyData();
                }
                else
                {
                    this.label94.Text = "00";
                }
                this.button12.Visible = false;
                this.button10.Text = "Add";
                this.textBox37.Text = "";
                this.label182.Text = "0";
                this.label185.Text = "0";
                this.label187.Text = "0";
                this.label189.Text = "0";
                this.label191.Text = "0";
            }
            catch (Exception ex)
            {
                this.conn.Close();
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void button25_Click_1(object sender, EventArgs e)
        {
            try
            {
                this.conn.Open();
                string query = "UPDATE DailySaving SET DS_Data = 'TKN', DS_InBankDate = ?, DS_Del_Person = ? WHERE DS_ID = ?";
                using (OleDbCommand command = new OleDbCommand(query, this.conn))
                {
                    command.Parameters.AddWithValue("@DS_InBankDate", this.DltDate);
                    command.Parameters.AddWithValue("@DS_Del_Person", this.label249.Text.Trim());
                    command.Parameters.AddWithValue("@DS_ID", this.label292.Text.Trim());
                    command.ExecuteNonQuery();
                }
                this.conn.Close();
                MessageBox.Show($"Successfull Deleted - [{this.label292.Text}]");
                this.fillDailyData();
                this.AmtCrDataView();
                if (this.dataGridView5.RowCount > 0)
                {
                    this.totalDailyData();
                }
                else
                {
                    this.label254.Text = "00";
                }
                this.button25.Visible = false;
                this.buttonS24.Text = "Add";
                this.textBox131.Text = "";
                this.label292.Text = "0";
                this.label289.Text = "0";
                this.label282.Text = "0";
                this.label293.Text = "0";
                this.label291.Text = "0";
            }
            catch (Exception ex)
            {
                this.conn.Close();
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void button22_Click(object sender, EventArgs e)
        {
            try
            {
                this.conn.Open();
                string query = "DELETE FROM Daily WHERE D_ID = ?";
                string query2 = "DELETE FROM DailyCut WHERE C_ID = ?";
                using (OleDbCommand commanda = new OleDbCommand(query, this.conn))
                {
                    commanda.Parameters.AddWithValue("?", this.label247.Text.Trim());
                    commanda.ExecuteNonQuery();
                }
                using (OleDbCommand commandb = new OleDbCommand(query2, this.conn))
                {
                    commandb.Parameters.AddWithValue("?", this.label248.Text.Trim());
                    commandb.ExecuteNonQuery();
                }
                this.conn.Close();
                MessageBox.Show($"Successfull Deleted - [{this.label247.Text}] & [{this.label248.Text}]");
                this.fillDailyData();
                this.button22.Visible = false;
            }
            catch (Exception ex)
            {
                this.conn.Close();
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void button24_Click_1(object sender, EventArgs e)
        {
            try
            {
                this.conn.Open();
                string query = "DELETE FROM DailySaving WHERE DS_ID = ?";
                using (OleDbCommand commanda = new OleDbCommand(query, this.conn))
                {
                    commanda.Parameters.AddWithValue("@DS_ID", this.label284.Text.Trim());
                    commanda.ExecuteNonQuery();
                }
                this.conn.Close();
                MessageBox.Show($"Successfull Deleted - [{this.label284.Text}]");
                this.fillDailyData();
                this.button22.Visible = false;
            }
            catch (Exception ex)
            {
                this.conn.Close();
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            this.textBox37.ReadOnly = true;
            this.textBox37.Text = "";
            this.label182.Text = "0";
            this.label185.Text = "0";
            this.label187.Text = "0";
            this.label189.Text = "0";
            this.button10.Text = "Add";
        }
        private void buttonS25_Click(object sender, EventArgs e)
        {
            this.textBox131.ReadOnly = true;
            this.textBox131.Text = "";
            this.label292.Text = "0";
            this.label282.Text = "0";
            this.label293.Text = "0";
            this.label291.Text = "0";
            this.buttonS24.Text = "Add";
        }
        private void button30_Click(object sender, EventArgs e)
        {
            this.textBox133.ReadOnly = true;
            this.textBox133.Text = "";
            this.label277.Text = "0";
            this.label279.Text = "0";
            this.label278.Text = "0";
            this.label276.Text = "0";
            this.button31.Text = "Add";
        }
        private void button9_Click(object sender, EventArgs e)
        {
            this.textBox50.ReadOnly = true;
            this.textBox50.Text = "";
            this.label182.Text = "0";
            this.label191.Text = "0";
            this.button14.Text = "Add";
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (this.button4.Text == "Add")
            {
                this.textBox32.ReadOnly = false;
                this.textBox32.Focus();
                TextBox textBox = this.textBox98;
                string[] strArrays = new string[] { "I", null, null, null, null };
                int date = DateTime.Now.Day;
                int month = DateTime.Now.Month;
                int millis = DateTime.Now.Millisecond;
                strArrays[2] = date.ToString();
                strArrays[3] = month.ToString();
                strArrays[4] = millis.ToString();
                textBox.Text = string.Concat(strArrays);
                this.button4.Text = "Save";
            }
            else if (this.button4.Text == "Save")
            {
                if (!(this.textBox32.Text.Trim() != ""))
                {
                    this.textBox32.Focus();
                }
                else
                {
                    try
                    {
                        this.conn.Open();
                        string query = "INSERT INTO Installment (I_ID, InsPay_Date, InsPay, Take_Data, I_Insrt_Person) VALUES (?, ?, ?, 'INS', ?)";
                        using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
                        {
                            cmd.Parameters.AddWithValue("@I_ID", this.textBox98.Text.Trim());
                            cmd.Parameters.AddWithValue("@InsPay_Date", this.dateTimePicker2.Text.Trim());
                            cmd.Parameters.AddWithValue("@InsPay", this.textBox32.Text.Trim());
                            cmd.Parameters.AddWithValue("@I_Insrt_Person", this.label249.Text.Trim());
                            cmd.ExecuteNonQuery();
                        }
                        this.conn.Close();
                        this.fillInstData();
                        this.totalInstData();
                        MessageBox.Show(string.Concat("Successfull Daily InstallPay Added"));
                        this.textBox32.ReadOnly = true;
                        this.textBox32.Text = "";
                        this.button4.Text = "Add";
                    }
                    catch (Exception ex)
                    {
                        this.conn.Close();
                        MessageBox.Show("Error : " + ex.Message);
                    }
                }
            }
            else if (this.button4.Text == "Updt")
            {
                try
                {
                    this.conn.Open();
                    string query = "UPDATE Installment SET InsPay_Date = ?, I_Updt_Person = ? WHERE I_ID = ?";
                    using (OleDbCommand command = new OleDbCommand(query, this.conn))
                    {
                        command.Parameters.AddWithValue("@InsPay_Date", this.dateTimePicker2.Text.Trim());
                        command.Parameters.AddWithValue("@I_Updt_Person", this.label249.Text.Trim());
                        command.Parameters.AddWithValue("@I_ID", this.label201.Text.Trim());
                        command.ExecuteNonQuery();
                    }
                    this.conn.Close();
                    MessageBox.Show(string.Concat("Successfull Update Instrallment Date"));
                    this.fillInstData();
                    this.textBox32.ReadOnly = true;
                    this.textBox32.Text = "";
                    this.button4.Text = "Add";
                }
                catch (Exception ex)
                {
                    this.conn.Close();
                    MessageBox.Show("Error : " + ex.Message);
                }
            }
        }
        private void button13_Click(object sender, EventArgs e)
        {
            if (this.button13.Text == "Add")
            {
                this.textBox94.ReadOnly = false;
                this.textBox94.Text = "";
                this.textBox95.ReadOnly = false;
                this.textBox95.Text = "";
                this.textBox96.ReadOnly = false;
                this.textBox96.Text = "";
                this.textBox97.ReadOnly = false;
                this.textBox97.Text = "";
                this.label195.Text = "0";
                TextBox textBox = this.textBox99;
                string[] strArrays = new string[] { "IS", null, null, null, null };
                int date = DateTime.Now.Day;
                int month = DateTime.Now.Month;
                int millis = DateTime.Now.Millisecond;
                strArrays[2] = date.ToString();
                strArrays[3] = month.ToString();
                strArrays[4] = millis.ToString();
                textBox.Text = string.Concat(strArrays);
                this.button13.Text = "Insert";
                this.textBox94.Focus();
            }
            else if (this.button13.Text == "Insert")
            {
                if (!(this.textBox94.Text.Trim() != ""))
                {
                    this.textBox94.Focus();
                }
                else
                {
                    try
                    {
                        this.conn.Open();
                        string query = "INSERT INTO Installment (I_ID, I_Date, Take_Total, Take_Anot, Take_Mine, InsPerMonth, PerMonthPay, Take_Data) VALUES (?, ?, ?, ?, ?, ?, ?, 'NPD')";
                        using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
                        {
                            cmd.Parameters.AddWithValue("@I_ID", this.textBox99.Text.Trim());
                            cmd.Parameters.AddWithValue("@I_Date", this.DltDate);
                            cmd.Parameters.AddWithValue("@Take_Total", this.textBox94.Text.Trim());
                            cmd.Parameters.AddWithValue("@Take_Anot", this.textBox96.Text.Trim());
                            cmd.Parameters.AddWithValue("@Take_Mine", this.textBox97.Text.Trim());
                            cmd.Parameters.AddWithValue("@InsPerMonth", this.textBox95.Text.Trim());
                            cmd.Parameters.AddWithValue("@PerMonthPay", this.label195.Text.Trim());
                            cmd.ExecuteNonQuery();
                        }
                        this.conn.Close();
                        this.fillInstData();
                        this.totalInstData();
                        MessageBox.Show(string.Concat("Successfull Inserted"));
                        this.textBox94.ReadOnly = false;
                        this.textBox94.Text = "";
                        this.textBox95.ReadOnly = false;
                        this.textBox95.Text = "";
                        this.textBox96.ReadOnly = false;
                        this.textBox96.Text = "";
                        this.textBox97.ReadOnly = false;
                        this.textBox97.Text = "";
                        this.label195.Text = "0";
                        this.button13.Text = "Add";
                    }
                    catch (Exception ex)
                    {
                        this.conn.Close();
                        MessageBox.Show("Error : " + ex.Message);
                    }
                }
            }
            else if (this.button13.Text == "Dlt")
            {
                try
                {
                    this.conn.Open();
                    string query = "UPDATE Installment SET Take_Data = 'TPD' WHERE I_ID = ?";
                    using (OleDbCommand command = new OleDbCommand(query, this.conn))
                    {
                        command.Parameters.AddWithValue("@I_ID", this.label218.Text.Trim());
                        command.ExecuteNonQuery();
                    }
                    this.conn.Close();
                    MessageBox.Show($"Successfull Deleted - [{this.label218.Text}]");
                    this.fillInstData();
                    if (this.dataGridView6.RowCount > 0)
                    {
                        this.totalInstData();
                    }
                    else
                    {
                        this.label203.Text = "00";
                        this.label72.Text  = "00";
                        this.label206.Text = "00";
                        this.label205.Text = "00";
                    }
                    this.button13.Text = "Add";
                }
                catch (Exception ex)
                {
                    this.conn.Close();
                    MessageBox.Show("Error : " + ex.Message);
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.textBox32.ReadOnly = false;
            this.textBox32.Text = "";
            this.textBox94.ReadOnly = false;
            this.textBox94.Text = "";
            this.textBox95.ReadOnly = false;
            this.textBox95.Text = "";
            this.textBox96.ReadOnly = false;
            this.textBox96.Text = "";
            this.textBox97.ReadOnly = false;
            this.textBox97.Text = "";
            this.label195.Text = "0";
            this.label201.Text = "0";
            this.label212.Text = "0";
            this.button4.Text = "Add";
            this.button13.Text = "Add";
        }
        private void button15_Click(object sender, EventArgs e)
        {
            if (this.button15.Text == "New")
            {
                this.BalankFldMarMem();
                TextBox textBox = this.textBox108;
                string[] strArrays = new string[] { "ME", null, null, null, null };
                int date = DateTime.Now.Day;
                int month = DateTime.Now.Month;
                int millis = DateTime.Now.Millisecond;
                strArrays[2] = date.ToString();
                strArrays[3] = month.ToString();
                strArrays[4] = millis.ToString();
                textBox.Text = string.Concat(strArrays);
                this.button15.Text = "Save";
                this.button1.Text = "U to M";
                this.textBox72.Focus();
            }
            else if (this.button15.Text == "Save")
            {
                try
                {
                    this.conn.Open();
                    string query = "INSERT INTO MarketMemos(Mem_ID,Mem_Date,R_InvTK,C_InvTK,Giv_TK,Ret_TK,I_N01,I_N02,I_N03,I_N04,I_N05,I_N06,I_N07,I_N08,I_N09,I_N10,I_N11,I_N12,I_N13,I_N14,I_N15,I_N16,I_P01,I_P02,I_P03,I_P04,I_P05,I_P06,I_P07,I_P08,I_P09,I_P10,I_P11,I_P12,I_P13,I_P14,I_P15,I_P16,I_Q01,I_Q02,I_Q03,I_Q04,I_Q05,I_Q06,I_Q07,I_Q08,I_Q09,I_Q10,I_Q11,I_Q12,I_Q13,I_Q14,I_Q15,I_Q16,I_ST01,I_ST02,I_ST03,I_ST04,I_ST05,I_ST06,I_ST07,I_ST08,I_ST09,I_ST10,I_ST11,I_ST12,I_ST13,I_ST14,I_ST15,I_ST16,R_Inv01,R_Inv02,R_Inv03,R_Inv04,R_Inv05,R_Inv06,R_Inv07,R_Inv08,R_Inv09,R_Inv10,R_Inv11,R_Inv12,R_Inv13,R_Inv14,R_Inv15,R_Inv16,R_Inv17,R_Inv18,R_Inv19,R_Inv20,R_Inv21,R_Inv22,R_Inv23,R_Inv24,Mem_Insrt_Person) " +
                                   "VALUES (@Mem_ID, @Mem_Date, @R_InvTK, @C_InvTK, @Giv_TK, @Ret_TK, @I_N01, @I_N02, @I_N03, @I_N04, @I_N05, @I_N06, @I_N07, @I_N08, @I_N09, @I_N10, @I_N11, @I_N12, @I_N13, @I_N14, @I_N15, @I_N16, @I_P01, @I_P02, @I_P03, @I_P04, @I_P05, @I_P06, @I_P07, @I_P08, @I_P09, @I_P10, @I_P11, @I_P12, @I_P13, @I_P14, @I_P15, @I_P16, @I_Q01, @I_Q02, @I_Q03, @I_Q04, @I_Q05, @I_Q06, @I_Q07, @I_Q08, @I_Q09, @I_Q10, @I_Q11, @I_Q12, @I_Q13, @I_Q14, @I_Q15, @I_Q16, @I_ST01, @I_ST02, @I_ST03, @I_ST04, @I_ST05, @I_ST06, @I_ST07, @I_ST08, @I_ST09, @I_ST10, @I_ST11, @I_ST12, @I_ST13, @I_ST14, @I_ST15, @I_ST16, @R_Inv01, @R_Inv02, @R_Inv03, @R_Inv04, @R_Inv05, @R_Inv06, @R_Inv07, @R_Inv08, @R_Inv09, @R_Inv10, @R_Inv11, @R_Inv12, @R_Inv13, @R_Inv14, @R_Inv15, @R_Inv16, @R_Inv17, @R_Inv18, @R_Inv19, @R_Inv20, @R_Inv21, @R_Inv22, @R_Inv23, @R_Inv24, @Mem_Insrt_Person)";
                    using (OleDbCommand insComm = new OleDbCommand(query, this.conn))
                    {
                        insComm.Parameters.AddWithValue("@Mem_ID", this.textBox108.Text.Trim());
                        insComm.Parameters.AddWithValue("@Mem_Date", this.DltDate);
                        insComm.Parameters.AddWithValue("@R_InvTK", this.textBox90.Text.Trim());
                        insComm.Parameters.AddWithValue("@C_InvTK", this.label10.Text.Trim());
                        insComm.Parameters.AddWithValue("@Giv_TK", this.textBox55.Text.Trim());
                        insComm.Parameters.AddWithValue("@Ret_TK", this.label147.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_N01", this.textBox72.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_N02", this.textBox73.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_N03", this.textBox78.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_N04", this.textBox75.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_N05", this.textBox76.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_N06", this.textBox77.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_N07", this.textBox79.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_N08", this.textBox80.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_N09", this.textBox81.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_N10", this.textBox82.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_N11", this.textBox83.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_N12", this.textBox84.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_N13", this.textBox85.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_N14", this.textBox86.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_N15", this.textBox87.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_N16", this.textBox88.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_P01", this.textBox3.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_P02", this.textBox5.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_P03", this.textBox7.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_P04", this.textBox9.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_P05", this.textBox11.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_P06", this.textBox13.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_P07", this.textBox15.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_P08", this.textBox17.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_P09", this.textBox19.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_P10", this.textBox21.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_P11", this.textBox23.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_P12", this.textBox25.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_P13", this.textBox27.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_P14", this.textBox29.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_P15", this.textBox31.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_P16", this.textBox38.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_Q01", this.textBox2.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_Q02", this.textBox4.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_Q03", this.textBox6.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_Q04", this.textBox8.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_Q05", this.textBox10.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_Q06", this.textBox12.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_Q07", this.textBox14.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_Q08", this.textBox16.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_Q09", this.textBox18.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_Q10", this.textBox20.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_Q11", this.textBox22.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_Q12", this.textBox24.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_Q13", this.textBox26.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_Q14", this.textBox28.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_Q15", this.textBox30.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_Q16", this.textBox54.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_ST01", this.label9.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_ST02", this.label13.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_ST03", this.label17.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_ST04", this.label24.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_ST05", this.label28.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_ST06", this.label32.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_ST07", this.label36.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_ST08", this.label40.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_ST09", this.label44.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_ST10", this.label48.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_ST11", this.label52.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_ST12", this.label56.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_ST13", this.label60.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_ST14", this.label64.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_ST15", this.label68.Text.Trim());
                        insComm.Parameters.AddWithValue("@I_ST16", this.label76.Text.Trim());
                        insComm.Parameters.AddWithValue("@R_Inv01", this.textBox56.Text.Trim());
                        insComm.Parameters.AddWithValue("@R_Inv02", this.textBox57.Text.Trim());
                        insComm.Parameters.AddWithValue("@R_Inv03", this.textBox58.Text.Trim());
                        insComm.Parameters.AddWithValue("@R_Inv04", this.textBox59.Text.Trim());
                        insComm.Parameters.AddWithValue("@R_Inv05", this.textBox60.Text.Trim());
                        insComm.Parameters.AddWithValue("@R_Inv06", this.textBox61.Text.Trim());
                        insComm.Parameters.AddWithValue("@R_Inv07", this.textBox62.Text.Trim());
                        insComm.Parameters.AddWithValue("@R_Inv08", this.textBox63.Text.Trim());
                        insComm.Parameters.AddWithValue("@R_Inv09", this.textBox64.Text.Trim());
                        insComm.Parameters.AddWithValue("@R_Inv10", this.textBox65.Text.Trim());
                        insComm.Parameters.AddWithValue("@R_Inv11", this.textBox66.Text.Trim());
                        insComm.Parameters.AddWithValue("@R_Inv12", this.textBox67.Text.Trim());
                        insComm.Parameters.AddWithValue("@R_Inv13", this.textBox68.Text.Trim());
                        insComm.Parameters.AddWithValue("@R_Inv14", this.textBox69.Text.Trim());
                        insComm.Parameters.AddWithValue("@R_Inv15", this.textBox70.Text.Trim());
                        insComm.Parameters.AddWithValue("@R_Inv16", this.textBox71.Text.Trim());
                        insComm.Parameters.AddWithValue("@R_Inv17", this.textBox89.Text.Trim());
                        insComm.Parameters.AddWithValue("@R_Inv18", this.textBox91.Text.Trim());
                        insComm.Parameters.AddWithValue("@R_Inv19", this.textBox110.Text.Trim());
                        insComm.Parameters.AddWithValue("@R_Inv20", this.textBox111.Text.Trim());
                        insComm.Parameters.AddWithValue("@R_Inv21", this.textBox112.Text.Trim());
                        insComm.Parameters.AddWithValue("@R_Inv22", this.textBox113.Text.Trim());
                        insComm.Parameters.AddWithValue("@R_Inv23", this.textBox114.Text.Trim());
                        insComm.Parameters.AddWithValue("@R_Inv24", this.textBox115.Text.Trim());
                        insComm.Parameters.AddWithValue("@Mem_Insrt_Person", this.label249.Text.Trim());
                        insComm.ExecuteNonQuery();
                    }
                    this.conn.Close();
                    MessageBox.Show(string.Concat("Successfull Memo Added"));
                    this.fillMemo();
                    this.button21.Visible = true;
                    this.button15.Text = "New";
                }
                catch (Exception ex)
                {
                    this.conn.Close();
                    MessageBox.Show("Error : " + ex.Message);
                }
            }
            else if (this.button15.Text == "Update")
            {
                try
                {
                    this.conn.Open();
                    string query = "UPDATE MarketMemos SET R_InvTK = ?,C_InvTK = ?,Giv_TK = ?,Ret_TK = ?,I_N01 = ? ,I_N02 = ? ,I_N03 = ? ,I_N04 = ? ,I_N05 = ? ,I_N06 = ? ,I_N07 = ? ,I_N08 = ? ,I_N09 = ? ,I_N10 = ? ,I_N11 = ? ,I_N12 = ? ,I_N13 = ? ,I_N14 = ? ,I_N15 = ? ,I_N16 = ? ,I_P01 = ? ,I_P02 = ? ,I_P03 = ? ,I_P04 = ? ,I_P05 = ? ,I_P06 = ? ,I_P07 = ? ,I_P08 = ? ,I_P09 = ? ,I_P10 = ? ,I_P11 = ? ,I_P12 = ? ,I_P13 = ? ,I_P14 = ? ,I_P15 = ? ,I_P16 = ? ,I_Q01 = ? ,I_Q02 = ? ,I_Q03 = ? ,I_Q04 = ? ,I_Q05 = ? ,I_Q06 = ? ,I_Q07 = ? ,I_Q08 = ? ,I_Q09 = ? ,I_Q10 = ? ,I_Q11 = ? ,I_Q12 = ? ,I_Q13 = ? ,I_Q14 = ? ,I_Q15 = ? ,I_Q16 = ? ,I_ST01 = ? ,I_ST02 = ? ,I_ST03 = ? ,I_ST04 = ? ,I_ST05 = ? ,I_ST06 = ? ,I_ST07 = ? ,I_ST08 = ? ,I_ST09 = ? ,I_ST10 = ? ,I_ST11 = ? ,I_ST12 = ? ,I_ST13 = ? ,I_ST14 = ? ,I_ST15 = ? ,I_ST16 = ? ,R_Inv01 = ? ,R_Inv02 = ? ,R_Inv03 = ? ,R_Inv04 = ? ,R_Inv05 = ? ,R_Inv06 = ? ,R_Inv07 = ? ,R_Inv08 = ? ,R_Inv09 = ? ,R_Inv10 = ? ,R_Inv11 = ? ,R_Inv12 = ? ,R_Inv13 = ? ,R_Inv14 = ? ,R_Inv15 = ? ,R_Inv16 = ? ,R_Inv17 = ? ,R_Inv18 = ? ,R_Inv19 = ? ,R_Inv20 = ? ,R_Inv21 = ? ,R_Inv22 = ? ,R_Inv23 = ? ,R_Inv24 = ? ,Mem_Updt_Person = ? WHERE Mem_ID = ?";
                    OleDbCommand updtComm = new OleDbCommand(query, this.conn);
                    updtComm.Parameters.AddWithValue("@R_InvTK", this.textBox90.Text.Trim());
                    updtComm.Parameters.AddWithValue("@C_InvTK", this.label10.Text.Trim());
                    updtComm.Parameters.AddWithValue("@Giv_TK", this.textBox55.Text.Trim());
                    updtComm.Parameters.AddWithValue("@Ret_TK", this.label147.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_N01", this.textBox72.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_N02", this.textBox73.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_N03", this.textBox78.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_N04", this.textBox75.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_N05", this.textBox76.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_N06", this.textBox77.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_N07", this.textBox79.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_N08", this.textBox80.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_N09", this.textBox81.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_N10", this.textBox82.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_N11", this.textBox83.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_N12", this.textBox84.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_N13", this.textBox85.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_N14", this.textBox86.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_N15", this.textBox87.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_N16", this.textBox88.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_P01", this.textBox3.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_P02", this.textBox5.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_P03", this.textBox7.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_P04", this.textBox9.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_P05", this.textBox11.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_P06", this.textBox13.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_P07", this.textBox15.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_P08", this.textBox17.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_P09", this.textBox19.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_P10", this.textBox21.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_P11", this.textBox23.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_P12", this.textBox25.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_P13", this.textBox27.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_P14", this.textBox29.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_P15", this.textBox31.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_P16", this.textBox38.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_Q01", this.textBox2.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_Q02", this.textBox4.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_Q03", this.textBox6.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_Q04", this.textBox8.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_Q05", this.textBox10.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_Q06", this.textBox12.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_Q07", this.textBox14.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_Q08", this.textBox16.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_Q09", this.textBox18.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_Q10", this.textBox20.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_Q11", this.textBox22.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_Q12", this.textBox24.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_Q13", this.textBox26.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_Q14", this.textBox28.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_Q15", this.textBox30.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_Q16", this.textBox54.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_ST01", this.label9.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_ST02", this.label13.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_ST03", this.label17.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_ST04", this.label24.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_ST05", this.label28.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_ST06", this.label32.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_ST07", this.label36.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_ST08", this.label40.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_ST09", this.label44.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_ST10", this.label48.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_ST11", this.label52.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_ST12", this.label56.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_ST13", this.label60.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_ST14", this.label64.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_ST15", this.label68.Text.Trim());
                    updtComm.Parameters.AddWithValue("@I_ST16", this.label76.Text.Trim());
                    updtComm.Parameters.AddWithValue("@R_Inv01", this.textBox56.Text.Trim());
                    updtComm.Parameters.AddWithValue("@R_Inv02", this.textBox57.Text.Trim());
                    updtComm.Parameters.AddWithValue("@R_Inv03", this.textBox58.Text.Trim());
                    updtComm.Parameters.AddWithValue("@R_Inv04", this.textBox59.Text.Trim());
                    updtComm.Parameters.AddWithValue("@R_Inv05", this.textBox60.Text.Trim());
                    updtComm.Parameters.AddWithValue("@R_Inv06", this.textBox61.Text.Trim());
                    updtComm.Parameters.AddWithValue("@R_Inv07", this.textBox62.Text.Trim());
                    updtComm.Parameters.AddWithValue("@R_Inv08", this.textBox63.Text.Trim());
                    updtComm.Parameters.AddWithValue("@R_Inv09", this.textBox64.Text.Trim());
                    updtComm.Parameters.AddWithValue("@R_Inv10", this.textBox65.Text.Trim());
                    updtComm.Parameters.AddWithValue("@R_Inv11", this.textBox66.Text.Trim());
                    updtComm.Parameters.AddWithValue("@R_Inv12", this.textBox67.Text.Trim());
                    updtComm.Parameters.AddWithValue("@R_Inv13", this.textBox68.Text.Trim());
                    updtComm.Parameters.AddWithValue("@R_Inv14", this.textBox69.Text.Trim());
                    updtComm.Parameters.AddWithValue("@R_Inv15", this.textBox70.Text.Trim());
                    updtComm.Parameters.AddWithValue("@R_Inv16", this.textBox71.Text.Trim());
                    updtComm.Parameters.AddWithValue("@R_Inv17", this.textBox89.Text.Trim());
                    updtComm.Parameters.AddWithValue("@R_Inv18", this.textBox91.Text.Trim());
                    updtComm.Parameters.AddWithValue("@R_Inv19", this.textBox110.Text.Trim());
                    updtComm.Parameters.AddWithValue("@R_Inv20", this.textBox111.Text.Trim());
                    updtComm.Parameters.AddWithValue("@R_Inv21", this.textBox112.Text.Trim());
                    updtComm.Parameters.AddWithValue("@R_Inv22", this.textBox113.Text.Trim());
                    updtComm.Parameters.AddWithValue("@R_Inv23", this.textBox114.Text.Trim());
                    updtComm.Parameters.AddWithValue("@R_Inv24", this.textBox115.Text.Trim());
                    updtComm.Parameters.AddWithValue("@Mem_Updt_Person", this.label249.Text.Trim());
                    updtComm.Parameters.AddWithValue("@Mem_ID", this.label224.Text.Trim());
                    updtComm.ExecuteNonQuery();
                    this.conn.Close();
                    MessageBox.Show(string.Concat("Successfull Update - ", this.label224.Text));
                    this.fillMemo();
                    this.button21.Visible = true;
                    this.button15.Text = "New";
                }
                catch (Exception ex)
                {
                    this.conn.Close();
                    MessageBox.Show("Error : " + ex.Message);
                }
            }
        }
        private void button21_Click(object sender, EventArgs e)
        {
            try
            {
                this.conn.Open();
                string updateQuery = "UPDATE MarketMemos SET Mem_Del_Person = ? WHERE Mem_ID = ?";
                OleDbCommand commandUpdtPerson = new OleDbCommand(updateQuery, this.conn);
                commandUpdtPerson.Parameters.AddWithValue("@Mem_Del_Person", this.label249.Text.Trim());
                commandUpdtPerson.Parameters.AddWithValue("@Mem_ID", this.label224.Text.Trim());
                commandUpdtPerson.ExecuteNonQuery();
                string insertQuery = "INSERT INTO MarketMemosDel SELECT * FROM MarketMemos WHERE Mem_ID = ?";
                OleDbCommand sendData = new OleDbCommand(insertQuery, this.conn);
                sendData.Parameters.AddWithValue("@Mem_ID", this.label224.Text.Trim());
                sendData.ExecuteNonQuery();
                string deleteQuery = "DELETE FROM MarketMemos WHERE Mem_ID = ?";
                OleDbCommand sendDData = new OleDbCommand(deleteQuery, this.conn);
                sendDData.Parameters.AddWithValue("@Mem_ID", this.label224.Text.Trim());
                sendDData.ExecuteNonQuery();
                this.conn.Close();
                MessageBox.Show($"Successfull Deleted - [{this.label224.Text}]");
                this.BalankFldMarMem();
                this.fillMemo();
                this.button15.Text = "New";
                this.button21.Visible = false;
            }
            catch (Exception ex)
            {
                this.conn.Close();
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void label217_DoubleClick(object sender, EventArgs e)
        {
            //code refresh for Due&Paid
        }
        private void button11_Click(object sender, EventArgs e)
        {
            if (!(this.textBox129.Text.Trim() != ""))
            {
                this.textBox129.Focus();
            }
            else
            {
                try
                {
                    this.conn.Open();
                    string query = "INSERT INTO BikeInfo (B_ID, B_Chng_Date, B_KM_ODO, B_Mobile_Go, B_Next_ODO, B_Insrt_Person) VALUES (?, ?, ?, ?, ?, ?)";
                    using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
                    {
                        cmd.Parameters.AddWithValue("@B_ID", this.textBox98.Text.Trim());
                        cmd.Parameters.AddWithValue("@B_Chng_Date", this.dateTimePicker6.Text.Trim());
                        cmd.Parameters.AddWithValue("@B_KM_ODO", this.textBox129.Text.Trim());
                        cmd.Parameters.AddWithValue("@B_Mobile_Go", this.textBox128.Text.Trim());
                        cmd.Parameters.AddWithValue("@B_Next_ODO", this.label257.Text.Trim());
                        cmd.Parameters.AddWithValue("@B_Insrt_Person", this.label249.Text.Trim());
                        cmd.ExecuteNonQuery();
                    }
                    this.conn.Close();
                    this.fillDataBike();
                    MessageBox.Show(string.Concat("Successfull Bike Info Added"));
                    this.textBox129.Text = "";
                    this.textBox128.Text = "";
                    this.label257.Text = "0";
                }
                catch (Exception ex)
                {
                    this.conn.Close();
                    MessageBox.Show("Error : " + ex.Message);
                }
            }
        }
        private void button24_Click(object sender, EventArgs e)
        {
            OpenFileDialog oFD = new OpenFileDialog();
            oFD.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp|All Files (*.*)|*.*";
            oFD.Title  = "Select an Image";
            if (oFD.ShowDialog() == DialogResult.OK)
            {
                //pictureBox1.Image = new Bitmap(oFD.FileName);
            }
        }
        private void button25_Click(object sender, EventArgs e)
        {/*            
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Please select an image first.");
                return;
            }
            try
            {
                this.conn.Open();
                MemoryStream ms = new MemoryStream();
                pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat); ;//(ms, pictureBox1.Image.RawFormat);   (ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = ms.ToArray();
                this.conn.Open();
                string query = "INSERT INTO Images (img_ID, ImageData) VALUES (?, ?)";
                using (OleDbCommand sendData = new OleDbCommand(query, this.conn))
                {
                    sendData.Parameters.AddWithValue("@img_ID", this.DltDate); // Assuming this.DltDate is the image ID
                    sendData.Parameters.AddWithValue("@ImageData", imageBytes);
                    sendData.ExecuteNonQuery();
                }
                this.conn.Close();
                MessageBox.Show("Image inserted successfully.");
                pictureBox1.Image = null;
                this.fillImageData();
            }
            catch (Exception ex)
            {
                this.conn.Close();
                MessageBox.Show("Error : " + ex.Message);
            }*/
        }
        private void button31_Click(object sender, EventArgs e)
        {
            if (this.button31.Text == "Add")
            {
                this.textBox133.ReadOnly = false;
                this.textBox133.Focus();
                TextBox textBox = this.textBox132;
                string[] strArrays = new string[] { "DA", null, null, null, null };
                int date = DateTime.Now.Day;
                int month = DateTime.Now.Month;
                int millis = DateTime.Now.Millisecond;
                strArrays[2] = date.ToString();
                strArrays[3] = month.ToString();
                strArrays[4] = millis.ToString();
                textBox.Text = string.Concat(strArrays);
                this.button31.Text = "Save";
            }
            else if (this.button31.Text == "Save")
            {
                if (!(this.textBox133.Text.Trim() != ""))
                {
                    this.textBox133.Focus();
                }
                else
                {
                    try
                    {
                        this.conn.Open();
                        string query = "INSERT INTO DailyAnt (DA_ID, DA_Date, DA_FPAmount, DA_SPAmount, NotTaken, DA_Data, DA_Insrt_Person) VALUES (?, ?, ?, ?, ?, 'NTKN', ?)";
                        using (OleDbCommand cmd = new OleDbCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@DA_ID", textBox132.Text.Trim());
                            cmd.Parameters.AddWithValue("@DA_Date", dateTimePicker8.Text.Trim());
                            cmd.Parameters.AddWithValue("@DA_FPAmount", textBox133.Text.Trim());
                            cmd.Parameters.AddWithValue("@DA_SPAmount", textBox134.Text.Trim());
                            cmd.Parameters.AddWithValue("@NotTaken", textBox134.Text.Trim());
                            cmd.Parameters.AddWithValue("@DA_Insrt_Person", label249.Text.Trim());
                            cmd.ExecuteNonQuery();
                        }
                        this.conn.Close();
                        MessageBox.Show(string.Concat("Successfull Daily AntData Added"));
                        this.fillDailyAntData();
                        this.totalDailyAntData();
                        this.textBox133.ReadOnly = true;
                        this.textBox133.Text = "";
                        this.textBox132.Text = "";
                        this.button31.Text = "Add";
                    }
                    catch (Exception ex)
                    {
                        this.conn.Close();
                        MessageBox.Show("Error : " + ex.Message);
                    }
                }
            }
            else if (this.button31.Text == "Updt")
            {
                try
                {
                    this.conn.Open();
                    string query = "UPDATE DailyAnt SET DA_FPAmount = ?, DA_SPAmount = ?, NotTaken = ?, DA_Date = ?, DA_Updt_Person = ? WHERE DA_ID = ?";
                    using (OleDbCommand command = new OleDbCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@DA_FPAmount", textBox133.Text.Trim());
                        command.Parameters.AddWithValue("@DA_SPAmount", textBox134.Text.Trim());
                        command.Parameters.AddWithValue("@NotTaken", textBox134.Text.Trim());
                        command.Parameters.AddWithValue("@DA_Date", dateTimePicker8.Text.Trim());
                        command.Parameters.AddWithValue("@DA_Updt_Person", label249.Text.Trim());
                        command.Parameters.AddWithValue("@DA_ID", label277.Text.Trim());
                        command.ExecuteNonQuery();
                    }
                    this.conn.Close();
                    MessageBox.Show(string.Concat("Successfull Update AntDaily Get"));
                    this.fillDailyAntData();
                    this.totalDailyAntData();
                    this.textBox133.ReadOnly = true;
                    this.textBox133.Text = "";
                    this.label277.Text = "0";
                    this.label279.Text = "0";
                    this.label278.Text = "0";
                    this.label276.Text = "0";
                    this.button31.Text = "Add";
                }
                catch (Exception ex)
                {
                    this.conn.Close();
                    MessageBox.Show("Error : " + ex.Message);
                }
            }
        }
        private void button33_Click(object sender, EventArgs e)
        {
            try
            {
                this.conn.Open();
                string query = "UPDATE DailyAnt SET DA_Data = 'TKN', TakenDate = ?, DA_Del_Person = ? WHERE DA_ID = ?";
                using (OleDbCommand command = new OleDbCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@TakenDate", DltDate);
                    command.Parameters.AddWithValue("@DA_Del_Person", label249.Text.Trim());
                    command.Parameters.AddWithValue("@DA_ID", label277.Text.Trim());
                    command.ExecuteNonQuery();
                }
                this.conn.Close();
                MessageBox.Show($"Successfull Deleted - [{this.label277.Text}]");
                this.fillDailyAntData();
                this.AmtCrDataView();
                if (this.dataGridView5.RowCount > 0)
                {
                    this.totalDailyAntData();
                }
                else
                {
                    this.label263.Text = "00";
                }
                this.button33.Visible = false;
                this.button31.Text = "Add";
                this.textBox133.Text = "";
                this.label277.Text = "0";
                this.label279.Text = "0";
                this.label278.Text = "0";
                this.label276.Text = "0";
            }
            catch (Exception ex)
            {
                this.conn.Close();
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void button32_Click(object sender, EventArgs e)
        {
            try
            {
                this.conn.Open();
                string query = "DELETE FROM DailyAnt WHERE DA_ID = ?";
                using (OleDbCommand command = new OleDbCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@DA_ID", label268.Text.Trim());
                    command.ExecuteNonQuery();
                }
                this.conn.Close();
                MessageBox.Show($"Successfull Deleted - [{this.label268.Text}]");
                this.fillDailyAntData();
                this.button32.Visible = false;
            }
            catch (Exception ex)
            {
                this.conn.Close();
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void buttonS24_Click(object sender, EventArgs e)
        {
            if (this.buttonS24.Text == "Add")
            {
                this.textBox131.ReadOnly = false;
                this.textBox131.Focus();
                TextBox textBox = this.textBox137;
                string[] strArrays = new string[] { "DS", null, null, null, null };
                int date = DateTime.Now.Day;
                int month = DateTime.Now.Month;
                int millis = DateTime.Now.Millisecond;
                strArrays[2] = date.ToString();
                strArrays[3] = month.ToString();
                strArrays[4] = millis.ToString();
                textBox.Text = string.Concat(strArrays);
                this.buttonS24.Text = "Add Amt";
            }
            else if (this.buttonS24.Text == "Add Amt")
            {
                if (!(this.textBox131.Text.Trim() != ""))
                {
                    this.textBox131.Focus();
                }
                else
                {
                    try
                    {
                        this.conn.Open();
                        string query = "INSERT INTO DailySaving (DS_ID, DS_Date, DS_FPAmount, DS_SPAmount, DS_TPAmount, NotTaken, DS_Data, DS_Insrt_Person) VALUES (?, ?, ?, ?, ?, ?, 'NTKN', ?)";
                        using (OleDbCommand cmd = new OleDbCommand(query, this.conn))
                        {
                            cmd.Parameters.AddWithValue("@DS_ID", this.textBox137.Text.Trim());
                            cmd.Parameters.AddWithValue("@DS_Date", this.dateTimePicker7.Text.Trim());
                            cmd.Parameters.AddWithValue("@DS_FPAmount", this.textBox131.Text.Trim());
                            cmd.Parameters.AddWithValue("@DS_SPAmount", this.textBox135.Text.Trim());
                            cmd.Parameters.AddWithValue("@DS_TPAmount", this.textBox135.Text.Trim());
                            cmd.Parameters.AddWithValue("@NotTaken", this.textBox135.Text.Trim());
                            cmd.Parameters.AddWithValue("@DS_Insrt_Person", this.label249.Text.Trim());
                            cmd.ExecuteNonQuery();
                        }
                        this.conn.Close();
                        MessageBox.Show(string.Concat("Successfull Added Daily Saving Amount"));
                        this.fillDailyData();
                        this.totalDailyData();
                        this.textBox131.ReadOnly = true;
                        this.textBox131.Text = "";
                        this.textBox137.Text = "";
                        this.buttonS24.Text = "Add";
                    }
                    catch (Exception ex)
                    {
                        this.conn.Close();
                        MessageBox.Show("Error : " + ex.Message);
                    }
                }
            }
            else if (this.buttonS24.Text == "Updt")
            {
                try
                {
                    this.conn.Open();
                    string query = "UPDATE DailySaving SET DS_FPAmount = ?, DS_Date = ?, DS_SPAmount = ?, DS_TPAmount = ?, NotTaken = ?, DS_Updt_Person = ? WHERE DS_ID = ?";
                    using (OleDbCommand command = new OleDbCommand(query, this.conn))
                    {
                        command.Parameters.AddWithValue("@DS_FPAmount", this.textBox131.Text.Trim());
                        command.Parameters.AddWithValue("@DS_Date", this.dateTimePicker7.Text.Trim());
                        command.Parameters.AddWithValue("@DS_SPAmount", this.textBox135.Text.Trim());
                        command.Parameters.AddWithValue("@DS_TPAmount", this.textBox135.Text.Trim());
                        command.Parameters.AddWithValue("@NotTaken", this.textBox135.Text.Trim());
                        command.Parameters.AddWithValue("@DS_Updt_Person", this.label249.Text.Trim());
                        command.Parameters.AddWithValue("@DS_ID", this.label292.Text.Trim());
                        command.ExecuteNonQuery();
                    }
                    this.conn.Close();
                    MessageBox.Show(string.Concat("Successfull Update Daily Saving"));
                    this.fillDailyData();
                    this.totalDailyData();
                    this.textBox131.ReadOnly = true;
                    this.textBox131.Text = "";
                    this.label292.Text = "0";
                    this.label289.Text = "0";
                    this.buttonS24.Text = "Add";
                }
                catch (Exception ex)
                {
                    this.conn.Close();
                    MessageBox.Show("Error : " + ex.Message);
                }
            }
        }
        private void button26_Click(object sender, EventArgs e)
        {
            this.marketSync();
            this.marketMemosSync();
            this.marketMemosDelSync();
            //this.imagesSync();
        }
        private void button27_Click(object sender, EventArgs e)
        {
            this.dailySavingSync();
            this.installmentSync();
            this.installmentPaySync();
            this.bikeInfoSync();
        }
        private void button36_Click(object sender, EventArgs e)
        {
            this.givenSync();
            this.tekenSync();
            this.expenseSync();
            this.savingSync();
            this.unratedSync();
        }
        private void button29_Click(object sender, EventArgs e)
        {
            this.dailySync();
            this.dailyAntSync();
            this.dailyCutSync();
        }
        private void button28_Click(object sender, EventArgs e)
        {
            try
            {
                string insCom = "BEGIN " +
                                "DELETE FROM BikeInfo; DELETE FROM Daily; DELETE FROM DailyAnt; DELETE FROM DailyCut; DELETE FROM DailySaving; DELETE FROM TariffAmt; DELETE FROM Given; DELETE FROM Images; DELETE FROM Installment; DELETE FROM Market; DELETE FROM MarketMemos; DELETE FROM MarketMemosDel; DELETE FROM Saving; DELETE FROM Teken; DELETE FROM Unrated; " +
                                "END;";
                using (OdbcConnection sqlConn = new OdbcConnection(connSql))
                {
                    if (MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        try
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
                            MessageBox.Show($"Successfully Data Synchronization", "Success");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"An Error Data Synchronization {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                
        }
        private void button35_Click(object sender, EventArgs e)
        {
            try
            {
                string folderPath = @"D:\BackupACC";
                string dbBackupPath = System.IO.Path.Combine(folderPath, "CT_DB.accdb");
                string dbRestorePath = System.IO.Path.Combine(folderPath, "CT_DB.accdb");
                if (System.IO.Directory.Exists(folderPath))
                {
                    System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(folderPath);
                    directoryInfo.Attributes &= ~System.IO.FileAttributes.Hidden;
                }
                DialogResult result = MessageBox.Show("Click 'Yes' To Create Backup\n\nClick 'No' To Restore.", "Backup / Restore", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string timestamp = DateTime.Now.ToString("ddMMyyyy_hhmmss_tt");
                    string defaultFileName = $"CT_DB_{timestamp}.accdb";
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "Access Database (*.accdb)|*.accdb";
                        saveFileDialog.Title = "Select Save Backup";
                        saveFileDialog.FileName = defaultFileName;
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            string backupPath = saveFileDialog.FileName;
                            if (System.IO.File.Exists(dbBackupPath))
                            {
                                System.IO.File.Copy(dbBackupPath, backupPath, true);
                                MessageBox.Show($"Backup Successful! \n\nLocation : [ {dbBackupPath} ] ", "Success");
                                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(folderPath);
                                di.Attributes |= System.IO.FileAttributes.Hidden;
                            }
                        }
                    }
                }
                else if (result == DialogResult.No)
                {
                    string restoreFileName = $"RCT_DB.accdb";
                    using (OpenFileDialog openFileDialog = new OpenFileDialog())
                    {
                        openFileDialog.Filter = "Access Database (*.accdb)|*.accdb";
                        openFileDialog.Title = "Select Backup to Restore";
                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            string restorePath = restoreFileName;
                            if (System.IO.File.Exists(dbRestorePath))
                            {
                                System.IO.File.Copy(dbRestorePath, restorePath, true);
                                MessageBox.Show($"Restore Successful! \n\nLocation : [ {dbRestorePath} ] ", "Success");
                                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(folderPath);
                                di.Attributes |= System.IO.FileAttributes.Hidden;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }

        //-----------------------------------------------------------------------
        //----------------Access to SQL Data Insert Event Work-------------------
        //-----------------------------------------------------------------------
        private void marketSync()
        {
            try
            {
                string insCom = "BEGIN " +
                                "IF NOT EXISTS (SELECT * FROM Market WHERE M_ID = ?) " +
                                    "BEGIN " +
                                        "INSERT INTO Market (M_ID,M_Date,M_Amount,M_Insrt_Person,M_Updt_Person,M_Del_Person) VALUES (?, ?, ?, ?, ?, ?) " +
                                    "END " +
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
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR: {ex.Message}");
            }
        }
        private void marketMemosSync()
        {
            try
            {
                string insCom = "BEGIN " +
                                "IF NOT EXISTS (SELECT * FROM MarketMemos WHERE Mem_ID = ?) " +
                                    "BEGIN " +
                                        "INSERT INTO MarketMemos (Mem_ID,Mem_Date,R_InvTK,C_InvTK,Giv_TK,Ret_TK,I_N01,I_N02,I_N03,I_N04,I_N05,I_N06,I_N07,I_N08,I_N09,I_N10,I_N11,I_N12,I_N13,I_N14,I_N15,I_N16,I_P01,I_P02,I_P03,I_P04,I_P05,I_P06,I_P07,I_P08,I_P09,I_P10,I_P11,I_P12,I_P13,I_P14,I_P15,I_P16,I_Q01,I_Q02,I_Q03,I_Q04,I_Q05,I_Q06,I_Q07,I_Q08,I_Q09,I_Q10,I_Q11,I_Q12,I_Q13,I_Q14,I_Q15,I_Q16,I_ST01,I_ST02,I_ST03,I_ST04,I_ST05,I_ST06,I_ST07,I_ST08,I_ST09,I_ST10,I_ST11,I_ST12,I_ST13,I_ST14,I_ST15,I_ST16,R_Inv01,R_Inv02,R_Inv03,R_Inv04,R_Inv05,R_Inv06,R_Inv07,R_Inv08,R_Inv09,R_Inv10,R_Inv11,R_Inv12,R_Inv13,R_Inv14,R_Inv15,R_Inv16,R_Inv17,R_Inv18,R_Inv19,R_Inv20,R_Inv21,R_Inv22,R_Inv23,R_Inv24,Mem_Insrt_Person,Mem_Updt_Person,Mem_Del_Person) " +
                                        "VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?) " +
                                    "END " +
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
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR: {ex.Message}");
            }
        }
        private void marketMemosDelSync()
        {
            try
            {
                string insCom = "BEGIN " +
                                "IF NOT EXISTS (SELECT * FROM MarketMemosDel WHERE Mem_ID = ?) " +
                                    "BEGIN " +
                                        "INSERT INTO MarketMemosDel (Mem_ID,Mem_Date,R_InvTK,C_InvTK,Giv_TK,Ret_TK,I_N01,I_N02,I_N03,I_N04,I_N05,I_N06,I_N07,I_N08,I_N09,I_N10,I_N11,I_N12,I_N13,I_N14,I_N15,I_N16,I_P01,I_P02,I_P03,I_P04,I_P05,I_P06,I_P07,I_P08,I_P09,I_P10,I_P11,I_P12,I_P13,I_P14,I_P15,I_P16,I_Q01,I_Q02,I_Q03,I_Q04,I_Q05,I_Q06,I_Q07,I_Q08,I_Q09,I_Q10,I_Q11,I_Q12,I_Q13,I_Q14,I_Q15,I_Q16,I_ST01,I_ST02,I_ST03,I_ST04,I_ST05,I_ST06,I_ST07,I_ST08,I_ST09,I_ST10,I_ST11,I_ST12,I_ST13,I_ST14,I_ST15,I_ST16,R_Inv01,R_Inv02,R_Inv03,R_Inv04,R_Inv05,R_Inv06,R_Inv07,R_Inv08,R_Inv09,R_Inv10,R_Inv11,R_Inv12,R_Inv13,R_Inv14,R_Inv15,R_Inv16,R_Inv17,R_Inv18,R_Inv19,R_Inv20,R_Inv21,R_Inv22,R_Inv23,R_Inv24,Mem_Insrt_Person,Mem_Updt_Person,Mem_Del_Person) " +
                                        "VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?) " +
                                    "END " +
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
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR: {ex.Message}");
            }
        }

        private void dailySavingSync()
        {
            try
            {
                string insCom = "BEGIN " +
                                "IF NOT EXISTS (SELECT * FROM DailySaving WHERE DS_ID = ?) " +
                                    "BEGIN " +
                                        "INSERT INTO DailySaving (DS_ID,DS_Date,DS_FPAmount,DS_SPAmount,DS_TPAmount,NotTaken,DS_Data,DS_InBankDate,DS_Insrt_Person,DS_Updt_Person,DS_Del_Person) VALUES (?,?,?,?,?,?,?,?,?,?,?)" +
                                    "END " +
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
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR: {ex.Message}");
            }
        }
        private void installmentSync()
        {
            try
            {
                string insCom = "BEGIN " +
                                "IF NOT EXISTS (SELECT * FROM Installment WHERE I_ID = ?) " +
                                    "BEGIN " +
                                        "INSERT INTO Installment (I_ID,I_Date,Take_Total,Take_Anot,Take_Mine,Take_Data,InsPerMonth,PerMonthPay,InsPay,InsPay_Date,I_Insrt_Person,I_Updt_Person,I_Del_Person) VALUES (?,?,?,?,?,?,?,?,?,?,?)" +
                                    "END " +
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
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR: {ex.Message}");
            }
        }
        private void installmentPaySync()
        {
            try
            {
                //Work Later
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR: {ex.Message}");
            }
        }
        private void bikeInfoSync()
        {
            try
            {
                string insCom = "BEGIN " +
                                "IF NOT EXISTS (SELECT * FROM BikeInfo WHERE B_ID = ?) " +
                                    "BEGIN " +
                                        "INSERT INTO BikeInfo (B_ID,B_Chng_Date,B_KM_ODO,B_Mobile_Go,B_Next_ODO,B_Insrt_Person,B_Updt_Person) VALUES (?, ?, ?, ?, ?, ?, ?) " +
                                    "END " +
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
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR: {ex.Message}");
            }
        }

        private void givenSync()
        {
            try
            {
                string insCom = "BEGIN " +
                                "IF NOT EXISTS (SELECT * FROM Given WHERE InGiven = ?) " +
                                    "BEGIN " +
                                        "INSERT INTO Given (InGiven,Total_Given,Given_To,ThroughBy,Given_Date,Remarks_Given,GDT_V,GDT_V_Date,DDT_V_Date,G_Insrt_Person,G_Updt_Person,G_Del_Person) VALUES (?,?,?,?,?,?,?,?,?,?,?,?)" +
                                    "END " +
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
                        while (reader.Read())
                        {
                            using (OdbcCommand sqlInsComm = new OdbcCommand(insCom, sqlConn))
                            {
                                sqlInsComm.Parameters.AddWithValue("?", reader["InGiven"]);
                                sqlInsComm.Parameters.AddWithValue("?", reader["InGiven"]); // For IF NOT EXISTS
                                sqlInsComm.Parameters.AddWithValue("?", reader["Total_Given"]);
                                sqlInsComm.Parameters.AddWithValue("?", reader["Given_To"]);
                                sqlInsComm.Parameters.AddWithValue("?", reader["ThroughBy"]);
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
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR: {ex.Message}");
            }
        }
        private void tekenSync()
        {
            try
            {
                string insCom = "BEGIN " +
                                "IF NOT EXISTS (SELECT * FROM Teken WHERE InTake = ?) " +
                                    "BEGIN " +
                                        "INSERT INTO Teken (InTake,Total_Take,Take_To,ThroughBy,Take_Date,Remarks_Take,TDT_V,TDT_V_Date,DDT_V_Date,T_Insrt_Person,T_Updt_Person,T_Del_Person) VALUES (?,?,?,?,?,?,?,?,?,?,?,?)" +
                                    "END " +
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
                        while (reader.Read())
                        {
                            using (OdbcCommand sqlInsComm = new OdbcCommand(insCom, sqlConn))
                            {
                                sqlInsComm.Parameters.AddWithValue("?", reader["InTake"]);
                                sqlInsComm.Parameters.AddWithValue("?", reader["InTake"]); // For IF NOT EXISTS
                                sqlInsComm.Parameters.AddWithValue("?", reader["Total_Take"]);
                                sqlInsComm.Parameters.AddWithValue("?", reader["Take_To"]);
                                sqlInsComm.Parameters.AddWithValue("?", reader["ThroughBy"]);
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
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR: {ex.Message}");
            }
        }
        private void expenseSync()
        {
            try
            {
                string insCom = "BEGIN " +
                                "IF NOT EXISTS (SELECT * FROM TariffAmt WHERE InExpense = ?) " +
                                    "BEGIN " +
                                        "INSERT INTO TariffAmt (InExpense,Expense_Amount,Expense_To,ThroughBy,Expense_Date,Remarks_Expense,EDT_V,EDT_V_Date,DDT_V_Date,E_Insrt_Person,E_Updt_Person,E_Del_Person) VALUES (?,?,?,?,?,?,?,?,?,?,?,?)" +
                                    "END " +
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
                        while (reader.Read())
                        {
                            using (OdbcCommand sqlInsComm = new OdbcCommand(insCom, sqlConn))
                            {
                                sqlInsComm.Parameters.AddWithValue("?", reader["InExpense"]);
                                sqlInsComm.Parameters.AddWithValue("?", reader["InExpense"]); // For IF NOT EXISTS
                                sqlInsComm.Parameters.AddWithValue("?", reader["Expense_Amount"]);
                                sqlInsComm.Parameters.AddWithValue("?", reader["Expense_To"]);
                                sqlInsComm.Parameters.AddWithValue("?", reader["ThroughBy"]);
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
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR: {ex.Message}");
            }
        }
        private void savingSync()
        {
            try
            {
                string insCom = "BEGIN " +
                                "IF NOT EXISTS (SELECT * FROM Saving WHERE InSaving = ?) " +
                                    "BEGIN " +
                                        "INSERT INTO Saving (InSaving,Saving_Amount,Saving_To,ThroughBy,Saving_Date,Remarks_Saving,SDT_V,SDT_V_Date,DDT_V_Date,Saving_Bank,S_Insrt_Person,S_Updt_Person,S_Del_Person) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?)" +
                                    "END " +
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
                        while (reader.Read())
                        {
                            using (OdbcCommand sqlInsComm = new OdbcCommand(insCom, sqlConn))
                            {
                                sqlInsComm.Parameters.AddWithValue("?", reader["InSaving"]);
                                sqlInsComm.Parameters.AddWithValue("?", reader["InSaving"]); // For IF NOT EXISTS
                                sqlInsComm.Parameters.AddWithValue("?", reader["Saving_Amount"]);
                                sqlInsComm.Parameters.AddWithValue("?", reader["Saving_To"]);
                                sqlInsComm.Parameters.AddWithValue("?", reader["ThroughBy"]);
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
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR: {ex.Message}");
            }
        }
        private void unratedSync()
        {
            try
            {
                string insCom = "BEGIN " +
                                "IF NOT EXISTS (SELECT * FROM Unrated WHERE InUnrated = ?) " +
                                    "BEGIN " +
                                        "INSERT INTO Unrated (InUnrated,Unrated_Amount,Unrated_To,ThroughBy,Unrated_Date,Remarks_Unrated,UDT_V,UDT_V_Date,DDT_V_Date,U_Insrt_Person,U_Updt_Person,U_Del_Person) VALUES (?,?,?,?,?,?,?,?,?,?,?,?)" +
                                    "END " +
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
                        while (reader.Read())
                        {
                            using (OdbcCommand sqlInsComm = new OdbcCommand(insCom, sqlConn))
                            {
                                sqlInsComm.Parameters.AddWithValue("?", reader["InUnrated"]);
                                sqlInsComm.Parameters.AddWithValue("?", reader["InUnrated"]); // For IF NOT EXISTS
                                sqlInsComm.Parameters.AddWithValue("?", reader["Unrated_Amount"]);
                                sqlInsComm.Parameters.AddWithValue("?", reader["Unrated_To"]);
                                sqlInsComm.Parameters.AddWithValue("?", reader["ThroughBy"]);
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
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR: {ex.Message}");
            }
        }

        private void dailySync()
        {
            try
            {
                string insCom = "BEGIN " +
                                "IF NOT EXISTS (SELECT * FROM Daily WHERE D_ID = ?) " +
                                    "BEGIN " +
                                        "INSERT INTO Daily (D_ID,D_Date,D_FPAmount,D_SPAmount,NotTaken,D_Data,TakenDate,D_Insrt_Person,D_Updt_Person,D_Del_Person) VALUES (?,?,?,?,?,?,?,?,?,?)" +
                                    "END " +
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
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR: {ex.Message}");
            }
        }
        private void dailyCutSync()
        {
            try
            {
                string insCom = "BEGIN " +
                                "IF NOT EXISTS (SELECT * FROM DailyCut WHERE C_ID = ?) " +
                                    "BEGIN " +
                                        "INSERT INTO DailyCut (C_ID,C_Date,C_Amount,C_Insrt_Person,C_Updt_Person,C_Del_Person) VALUES (?,?,?,?,?,?)" +
                                    "END " +
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
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR: {ex.Message}");
            }
        }
        private void dailyAntSync()
        {
            try
            {
                string insCom = "BEGIN " +
                                "IF NOT EXISTS (SELECT * FROM DailyAnt WHERE DA_ID = ?) " +
                                    "BEGIN " +
                                        "INSERT INTO DailyAnt (DA_ID,DA_Date,DA_FPAmount,DA_SPAmount,NotTaken,DA_Data,TakenDate,DA_Insrt_Person,DA_Updt_Person,DA_Del_Person) VALUES (?,?,?,?,?,?,?,?,?,?)" +
                                    "END " +
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
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR: {ex.Message}");
            }
        }
        private void imagesSync()
        {

        }

        //-----------------------------------------------------------------------
        //------------------------------Time Event Work--------------------------
        //-----------------------------------------------------------------------
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.label4.Text = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.conn.Open();
                string query = "SELECT M_ID, M_Amount FROM Market WHERE M_ID = ?";
                using (OleDbDataAdapter oleDbDatadt = new OleDbDataAdapter(query, this.conn))
                {
                    oleDbDatadt.SelectCommand.Parameters.AddWithValue("@M_ID", this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    DataTable dataTabledt = new DataTable();
                    oleDbDatadt.Fill(dataTabledt);
                    if (dataTabledt.Rows.Count > 0)
                    {
                        this.label6.Text = dataTabledt.Rows[0]["M_ID"].ToString();
                        this.textBox1.Text = dataTabledt.Rows[0]["M_Amount"].ToString();
                    }
                }
                this.conn.Close();
                this.textBox1.ReadOnly = false;
                this.textBox1.Focus();
                this.button1.Text = "Updt";
                this.dateTimePicker1.Visible = true;
            }
            catch (Exception ex)
            {
                this.conn.Close();
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.conn.Open();
                string query = "SELECT I_ID, InsPay FROM Installment WHERE I_ID = ?";
                using (OleDbDataAdapter oleDbData = new OleDbDataAdapter(query, this.conn))
                {
                    oleDbData.SelectCommand.Parameters.AddWithValue("@I_ID", this.dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
                    DataTable dataTable = new DataTable();
                    oleDbData.Fill(dataTable);
                    if (dataTable.Rows.Count > 0)
                    {
                        DataRow row = dataTable.Rows[0];
                        this.label201.Text = row[0].ToString();
                        this.label212.Text = row[1].ToString();
                        this.textBox32.Text = row[1].ToString();
                        this.textBox32.ReadOnly = false;
                        this.textBox32.Focus();
                    }
                }
                this.conn.Close();
                this.button4.Text = "Updt";
            }
            catch (Exception ex)
            {
                this.conn.Close();
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.conn.Open();
                string query = "SELECT * FROM Given WHERE InGiven = ?";
                using (OleDbDataAdapter oleDbData = new OleDbDataAdapter(query, this.conn))
                {
                    oleDbData.SelectCommand.Parameters.AddWithValue("@InGiven", this.dataGridView3.SelectedRows[0].Cells[0].Value.ToString());
                    DataTable dataTable = new DataTable();
                    oleDbData.Fill(dataTable);
                    if (dataTable.Rows.Count > 0)
                    {
                        this.label102.Text = dataTable.Rows[0][0].ToString();
                        this.label117.Text = dataTable.Rows[0][1].ToString();
                        this.textBox40.Text = dataTable.Rows[0][2].ToString();
                        this.label111.Text = dataTable.Rows[0][2].ToString();
                        this.textBox36.Text = dataTable.Rows[0][3].ToString();
                        this.label113.Text = dataTable.Rows[0][4].ToString();
                        this.textBox41.Text = dataTable.Rows[0][5].ToString();
                        this.textBox42.Text = dataTable.Rows[0][6].ToString();
                        this.textBox118.Text = dataTable.Rows[0][8].ToString();
                        this.button7.Visible = true;
                        this.button7.Text = "Delete G.";
                        this.textBox119.Focus();
                    }
                }
                this.conn.Close();
            }
            catch (Exception ex)
            {
                this.conn.Close();
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.button12.Visible = true;
                this.button22.Visible = true;
                this.button10.Text = "Updt";
                string query = "SELECT D_ID, D_FPAmount, D_SPAmount, D_Data, NotTaken FROM Daily WHERE D_ID = ?";
                this.conn.Open();
                using (OleDbDataAdapter oleDbData = new OleDbDataAdapter(query, this.conn))
                {
                    oleDbData.SelectCommand.Parameters.AddWithValue("@D_ID", this.dataGridView5.SelectedRows[0].Cells[0].Value.ToString());
                    DataTable dataTable = new DataTable();
                    oleDbData.Fill(dataTable);
                    if (dataTable.Rows.Count > 0)
                    {
                        this.label182.Text = dataTable.Rows[0][0].ToString();
                        this.label247.Text = dataTable.Rows[0][0].ToString();
                        this.label185.Text = dataTable.Rows[0][1].ToString();
                        this.label187.Text = dataTable.Rows[0][2].ToString();
                        this.label189.Text = dataTable.Rows[0][3].ToString();
                        this.textBox37.Text = dataTable.Rows[0][4].ToString();
                        this.textBox37.ReadOnly = false;
                        this.textBox37.Focus();
                    }
                }
                this.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.conn.Open();
                this.button22.Visible = true;
                this.button14.Text = "Updt";
                string query = "SELECT C_ID, C_Amount FROM DailyCut WHERE C_ID = ?";
                using (OleDbDataAdapter oleDbData = new OleDbDataAdapter(query, this.conn))
                {
                    oleDbData.SelectCommand.Parameters.AddWithValue("@C_ID", this.dataGridView4.SelectedRows[0].Cells[0].Value.ToString());
                    DataTable dataTable = new DataTable();
                    oleDbData.Fill(dataTable);
                    if (dataTable.Rows.Count > 0)
                    {
                        DataRow row = dataTable.Rows[0];
                        label182.Text = row[0].ToString();
                        label248.Text = row[0].ToString();
                        label191.Text = row[1].ToString();
                        textBox50.Text = row[1].ToString();
                        textBox50.ReadOnly = false;
                        textBox50.Focus();
                    }
                }
                this.conn.Close();
            }
            catch (Exception ex)
            {
                this.conn.Close();
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void dataGridView6_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.conn.Open();
                string query = "SELECT I_ID, Take_Anot, Take_Mine FROM Installment WHERE I_ID = ?";
                using (OleDbDataAdapter oleDbData = new OleDbDataAdapter(query, this.conn))
                {
                    oleDbData.SelectCommand.Parameters.AddWithValue("@I_ID", this.dataGridView6.SelectedRows[0].Cells[0].Value.ToString());
                    DataTable dataTable = new DataTable();
                    oleDbData.Fill(dataTable);
                    if (dataTable.Rows.Count > 0)
                    {
                        DataRow row = dataTable.Rows[0];
                        this.label218.Text = row[0].ToString();
                        this.label199.Text = row[1].ToString();
                        this.label198.Text = row[2].ToString();
                    }
                }
                this.conn.Close();
                this.button13.Text = "Dlt";
            }
            catch (Exception ex)
            {
                this.conn.Close();
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void dataGridView7_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.conn.Open();
                string query = "SELECT * FROM Teken WHERE InTake = ?";
                using (OleDbDataAdapter oleDbData = new OleDbDataAdapter(query, this.conn))
                {
                    oleDbData.SelectCommand.Parameters.AddWithValue("@InTake", this.dataGridView7.SelectedRows[0].Cells[0].Value.ToString());
                    DataTable dataTable = new DataTable();
                    oleDbData.Fill(dataTable);
                    if (dataTable.Rows.Count > 0)
                    {
                        this.label102.Text = dataTable.Rows[0][0].ToString();
                        this.label117.Text = dataTable.Rows[0][1].ToString();
                        this.textBox45.Text = dataTable.Rows[0][2].ToString();
                        this.label111.Text = dataTable.Rows[0][2].ToString();
                        this.textBox44.Text = dataTable.Rows[0][3].ToString();
                        this.label113.Text = dataTable.Rows[0][4].ToString();
                        this.textBox46.Text = dataTable.Rows[0][5].ToString();
                        this.textBox47.Text = dataTable.Rows[0][6].ToString();
                        this.textBox121.Text = dataTable.Rows[0][8].ToString();
                        this.button7.Visible = true;
                        this.button7.Text = "Delete T.";
                        this.textBox120.Focus();
                    }
                }
                this.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void dataGridView8_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.conn.Open();
                string query = "SELECT * FROM TariffAmt WHERE InExpense = ?";
                using (OleDbDataAdapter oleDbData = new OleDbDataAdapter(query, this.conn))
                {
                    oleDbData.SelectCommand.Parameters.AddWithValue("@InExpense", this.dataGridView8.SelectedRows[0].Cells[0].Value.ToString());
                    DataTable dataTable = new DataTable();
                    oleDbData.Fill(dataTable);
                    if (dataTable.Rows.Count > 0)
                    {
                        this.label102.Text = dataTable.Rows[0][0].ToString();
                        this.label117.Text = dataTable.Rows[0][1].ToString();
                        this.textBox103.Text = dataTable.Rows[0][2].ToString();
                        this.label111.Text = dataTable.Rows[0][2].ToString();
                        this.textBox104.Text = dataTable.Rows[0][3].ToString();
                        this.label113.Text = dataTable.Rows[0][4].ToString();
                        this.textBox93.Text = dataTable.Rows[0][5].ToString();
                        this.textBox102.Text = dataTable.Rows[0][6].ToString();
                        this.textBox127.Text = dataTable.Rows[0][8].ToString();
                        this.button7.Visible = true;
                        this.button7.Text = "Delete E.";
                        this.textBox109.Focus();
                    }
                }
                this.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void dataGridView9_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.conn.Open();
                string query = "SELECT * FROM Saving WHERE InSaving = ?";
                using (OleDbDataAdapter oleDbData = new OleDbDataAdapter(query, this.conn))
                {
                    oleDbData.SelectCommand.Parameters.AddWithValue("@InSaving", this.dataGridView9.SelectedRows[0].Cells[0].Value.ToString());
                    DataTable dataTable = new DataTable();
                    oleDbData.Fill(dataTable);
                    if (dataTable.Rows.Count > 0)
                    {
                        this.label102.Text = dataTable.Rows[0][0].ToString();
                        this.label117.Text = dataTable.Rows[0][1].ToString();
                        this.textBox43.Text = dataTable.Rows[0][2].ToString();
                        this.label111.Text = dataTable.Rows[0][2].ToString();
                        this.textBox105.Text = dataTable.Rows[0][3].ToString();
                        this.label113.Text = dataTable.Rows[0][4].ToString();
                        this.textBox48.Text = dataTable.Rows[0][5].ToString();
                        this.textBox49.Text = dataTable.Rows[0][6].ToString();
                        this.textBox122.Text = dataTable.Rows[0][8].ToString();
                        this.label243.Text = dataTable.Rows[0][9].ToString();
                        this.button7.Visible = true;
                        this.button7.Text = "Delete S.";
                        this.textBox116.Focus();
                    }
                }
                this.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void dataGridView10_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.conn.Open();
                string query = "SELECT * FROM Unrated WHERE InUnrated = ?";
                using (OleDbDataAdapter oleDbData = new OleDbDataAdapter(query, this.conn))
                {
                    oleDbData.SelectCommand.Parameters.AddWithValue("@InUnrated", this.dataGridView10.SelectedRows[0].Cells[0].Value.ToString());
                    DataTable dataTable = new DataTable();
                    oleDbData.Fill(dataTable);
                    if (dataTable.Rows.Count > 0)
                    {
                        this.label102.Text = dataTable.Rows[0][0].ToString();
                        this.label117.Text = dataTable.Rows[0][1].ToString();
                        this.textBox51.Text = dataTable.Rows[0][2].ToString();
                        this.label111.Text = dataTable.Rows[0][2].ToString();
                        this.textBox106.Text = dataTable.Rows[0][3].ToString();
                        this.label113.Text = dataTable.Rows[0][4].ToString();
                        this.textBox52.Text = dataTable.Rows[0][5].ToString();
                        this.textBox53.Text = dataTable.Rows[0][6].ToString();
                        this.textBox123.Text = dataTable.Rows[0][8].ToString();
                        this.button7.Visible = true;
                        this.button7.Text = "Delete U.";
                        this.textBox117.Focus();
                    }
                }
                this.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void dataGridView11_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.conn.Open();
                string query = "SELECT * FROM MarketMemos WHERE Mem_ID = ?";
                using (OleDbDataAdapter oleDbData = new OleDbDataAdapter(query, this.conn))
                {
                    oleDbData.SelectCommand.Parameters.AddWithValue("@Mem_ID", this.dataGridView11.SelectedRows[0].Cells[0].Value.ToString());
                    DataTable dataTable = new DataTable();
                    oleDbData.Fill(dataTable);
                    if (dataTable.Rows.Count > 0)
                    {
                        DataRow row = dataTable.Rows[0];
                        this.label224.Text = row[0].ToString();
                        this.textBox90.Text = row[2].ToString();
                        this.label10.Text = row[3].ToString();
                        this.textBox55.Text = row[4].ToString();
                        this.label147.Text = row[5].ToString();
                        this.textBox72.Text = row[6].ToString();
                        this.textBox73.Text = row[7].ToString();
                        this.textBox78.Text = row[8].ToString();
                        this.textBox75.Text = row[9].ToString();
                        this.textBox76.Text = row[10].ToString();
                        this.textBox77.Text = row[11].ToString();
                        this.textBox79.Text = row[12].ToString();
                        this.textBox80.Text = row[13].ToString();
                        this.textBox81.Text = row[14].ToString();
                        this.textBox82.Text = row[15].ToString();
                        this.textBox83.Text = row[16].ToString();
                        this.textBox84.Text = row[17].ToString();
                        this.textBox85.Text = row[18].ToString();
                        this.textBox86.Text = row[19].ToString();
                        this.textBox87.Text = row[20].ToString();
                        this.textBox88.Text = row[21].ToString();
                        this.textBox3.Text = row[22].ToString();
                        this.textBox5.Text = row[23].ToString();
                        this.textBox7.Text = row[24].ToString();
                        this.textBox9.Text = row[25].ToString();
                        this.textBox11.Text = row[26].ToString();
                        this.textBox13.Text = row[27].ToString();
                        this.textBox15.Text = row[28].ToString();
                        this.textBox17.Text = row[29].ToString();
                        this.textBox19.Text = row[30].ToString();
                        this.textBox21.Text = row[31].ToString();
                        this.textBox23.Text = row[32].ToString();
                        this.textBox25.Text = row[33].ToString();
                        this.textBox27.Text = row[34].ToString();
                        this.textBox29.Text = row[35].ToString();
                        this.textBox31.Text = row[36].ToString();
                        this.textBox38.Text = row[37].ToString();
                        this.textBox2.Text = row[38].ToString();
                        this.textBox4.Text = row[39].ToString();
                        this.textBox6.Text = row[40].ToString();
                        this.textBox8.Text = row[41].ToString();
                        this.textBox10.Text = row[42].ToString();
                        this.textBox12.Text = row[43].ToString();
                        this.textBox14.Text = row[44].ToString();
                        this.textBox16.Text = row[45].ToString();
                        this.textBox18.Text = row[46].ToString();
                        this.textBox20.Text = row[47].ToString();
                        this.textBox22.Text = row[48].ToString();
                        this.textBox24.Text = row[49].ToString();
                        this.textBox26.Text = row[50].ToString();
                        this.textBox28.Text = row[51].ToString();
                        this.textBox30.Text = row[52].ToString();
                        this.textBox54.Text = row[53].ToString();
                        this.label9.Text = row[54].ToString();
                        this.label13.Text = row[55].ToString();
                        this.label17.Text = row[56].ToString();
                        this.label24.Text = row[57].ToString();
                        this.label28.Text = row[58].ToString();
                        this.label32.Text = row[59].ToString();
                        this.label36.Text = row[60].ToString();
                        this.label40.Text = row[61].ToString();
                        this.label44.Text = row[62].ToString();
                        this.label48.Text = row[63].ToString();
                        this.label52.Text = row[64].ToString();
                        this.label56.Text = row[65].ToString();
                        this.label60.Text = row[66].ToString();
                        this.label64.Text = row[67].ToString();
                        this.label68.Text = row[68].ToString();
                        this.label76.Text = row[69].ToString();
                        this.textBox56.Text = row[70].ToString();
                        this.textBox57.Text = row[71].ToString();
                        this.textBox58.Text = row[72].ToString();
                        this.textBox59.Text = row[73].ToString();
                        this.textBox60.Text = row[74].ToString();
                        this.textBox61.Text = row[75].ToString();
                        this.textBox62.Text = row[76].ToString();
                        this.textBox63.Text = row[77].ToString();
                        this.textBox64.Text = row[78].ToString();
                        this.textBox65.Text = row[79].ToString();
                        this.textBox66.Text = row[80].ToString();
                        this.textBox67.Text = row[81].ToString();
                        this.textBox68.Text = row[82].ToString();
                        this.textBox69.Text = row[83].ToString();
                        this.textBox70.Text = row[84].ToString();
                        this.textBox71.Text = row[85].ToString();
                        this.textBox89.Text = row[86].ToString();
                        this.textBox91.Text = row[87].ToString();
                        this.textBox110.Text = row[88].ToString();
                        this.textBox111.Text = row[89].ToString();
                        this.textBox112.Text = row[90].ToString();
                        this.textBox113.Text = row[91].ToString();
                        this.textBox114.Text = row[92].ToString();
                        this.textBox115.Text = row[93].ToString();
                        // this.pictureBox1.Text = row[97].ToString();
                    }
                    else
                    {
                        this.conn.Close();
                        this.button15.Text = "New";
                    }
                    this.conn.Close();
                    this.button15.Text = "Update";
                    this.button21.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void dataGridView12_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.conn.Open();
                string query = "SELECT B_Next_ODO FROM BikeInfo WHERE B_ID = ?";
                using (OleDbDataAdapter oleDbData = new OleDbDataAdapter(query, this.conn))
                {
                    oleDbData.SelectCommand.Parameters.AddWithValue("@B_ID", this.dataGridView12.SelectedRows[0].Cells[2].Value.ToString());
                    DataTable dataTable = new DataTable();
                    oleDbData.Fill(dataTable);
                    if (dataTable.Rows.Count > 0)
                    {
                        this.textBox129.Text = dataTable.Rows[0][0].ToString();
                    }
                }
                //string uniqueString = $"OM{DateTime.Now.Day:D2}{DateTime.Now.Month:D2}{DateTime.Now.Millisecond:D4}";
                //this.textBox98.Text = uniqueString;
                this.conn.Close();
                TextBox textBox = this.textBox98;
                string[] strArrays = new string[] { "OM", null, null, null, null };
                int date = DateTime.Now.Day;
                int month = DateTime.Now.Month;
                int millis = DateTime.Now.Millisecond;
                strArrays[2] = date.ToString();
                strArrays[3] = month.ToString();
                strArrays[4] = millis.ToString();
                textBox.Text = string.Concat(strArrays);
                this.textBox129.Focus();
            }
            catch (Exception ex)
            {
                this.conn.Close();
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void dataGridView17_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.button33.Visible = true;
                this.button32.Visible = true;
                this.button31.Text = "Updt";
                this.conn.Open();
                string query = "SELECT DA_ID, DA_FPAmount, DA_SPAmount, DA_Data, NotTaken FROM DailyAnt WHERE DA_ID = ?";
                using (OleDbDataAdapter oleDbData = new OleDbDataAdapter(query, conn))
                {
                    oleDbData.SelectCommand.Parameters.AddWithValue("@DA_ID", dataGridView17.SelectedRows[0].Cells[0].Value.ToString());

                    DataTable dataTable = new DataTable();
                    oleDbData.Fill(dataTable);
                    if (dataTable.Rows.Count > 0)
                    {
                        this.label277.Text = dataTable.Rows[0][0].ToString();
                        this.label268.Text = dataTable.Rows[0][0].ToString();
                        this.label279.Text = dataTable.Rows[0][1].ToString();
                        this.label278.Text = dataTable.Rows[0][2].ToString();
                        this.label276.Text = dataTable.Rows[0][3].ToString();
                        this.textBox133.Text = dataTable.Rows[0][4].ToString();
                        textBox133.ReadOnly = false;
                        textBox133.Focus();
                    }
                }
                this.conn.Close();
            }
            catch (Exception ex)
            {
                this.conn.Close();
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void dataGridView14_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.button25.Visible = true;
                this.button24.Visible = true;
                this.buttonS24.Text = "Updt";
                string query = "SELECT DS_ID, DS_FPAmount, DS_TPAmount, DS_Data, NotTaken FROM DailySaving WHERE DS_ID = ?";
                using (OleDbDataAdapter oleDbData = new OleDbDataAdapter(query, this.conn))
                {
                    oleDbData.SelectCommand.Parameters.AddWithValue("@DS_ID", this.dataGridView14.SelectedRows[0].Cells[0].Value.ToString());
                    DataTable dataTable = new DataTable();
                    oleDbData.Fill(dataTable);
                    if (dataTable.Rows.Count > 0)
                    {
                        DataRow row = dataTable.Rows[0];
                        this.label292.Text = row[0].ToString();
                        this.label284.Text = row[0].ToString();
                        this.label282.Text = row[1].ToString();
                        this.label293.Text = row[2].ToString();
                        this.label291.Text = row[3].ToString();
                        this.textBox131.Text = row[4].ToString();
                        this.textBox131.ReadOnly = false;
                        this.textBox131.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                this.conn.Close();
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void dataGridView14_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.conn.Open();
                string query = "SELECT ImageData FROM Images";
                using (OleDbCommand oleDbCommand = new OleDbCommand(query, this.conn))
                {
                    using (OleDbDataReader reader = oleDbCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(reader.GetOrdinal("ImageData")))
                            {
                                byte[] imageBytes = (byte[])reader["ImageData"];
                                using (MemoryStream ms = new MemoryStream(imageBytes))
                                {
                                    //pictureBox1.Image = Image.FromStream(ms);
                                }
                            }
                        }
                    }
                }
                this.conn.Close();
            }
            catch (Exception ex)
            {
                this.conn.Close();
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        //-----------------------------------------------------------------------
        //------------------------------All Event Work---------------------------
        //-----------------------------------------------------------------------
        #region All_TextBox_Event_Work
        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.textBox.Text.Trim() == "*1355*" || this.textBox.Text.Trim() == "shohel")
                {
                    this.tabControl1.Visible = true;
                    this.panel27.Visible = false;
                    this.label249.Text = this.textBox.Text.Trim();
                }
                else if (this.textBox.Text.Trim() == "shamim")
                {
                    this.panel27.Visible = false;
                    this.tabControl1.Visible = true;
                    this.tabControl1.TabPages.Remove(tabPage1);
                    this.tabControl1.TabPages.Remove(tabPage2);
                    this.tabControl1.TabPages.Remove(tabPage3);
                    this.panel4.Visible = false;
                    this.panel19.Visible = false;
                    this.panel31.Visible = false;
                    this.dataGridView5.ReadOnly = true;
                    this.label249.Text = this.textBox.Text.Trim();
                }
                else
                {
                    this.textBox.Text = "";
                }
            }
        }
        private void textBox39_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox39.Text.Trim() != ""))
                    {
                        MessageBox.Show("Please Insert Amount.");
                        this.textBox39.Focus();
                    }
                    else
                    {
                        if (this.button6.Text == "Updt")
                        {
                            this.button6.Focus();
                        }
                        else
                        {
                            this.textBox33.Focus();
                        }
                    }
                }
            }
        }
        private void textBox33_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (!(this.textBox33.Text.Trim() != ""))
                {
                    MessageBox.Show("Insert Name");
                    this.textBox33.Focus();
                }
                else
                {
                    this.comboBox1.Focus();
                }
            }
        }
        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (!(this.comboBox1.Text.Trim() != ""))
                {
                    MessageBox.Show("Select Dropdoen List");
                    this.comboBox1.Focus();
                }
                else
                {
                    this.dateTimePicker3.Focus();
                }                
            }
        }
        private void dateTimePicker3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (!(this.dateTimePicker3.Text.Trim() != ""))
                {
                    this.dateTimePicker3.Focus();
                }
                else
                {
                    this.textBox34.Focus();
                }
            }
        }
        private void textBox107_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!(this.textBox107.Text.Trim() != ""))
                {
                    this.dataGridView13.DataSource = null;
                    this.label231.Text = "";
                    this.label252.Visible = false;
                    this.dataGridView13.Visible = false;
                }
                else
                {
                    this.label252.Visible = true;
                    this.label252.Text = "Given";
                    DataTable dataTable = new DataTable();
                    string query = "SELECT SUM(Total_Given) as Total, Given_To FROM Given WHERE Given_To LIKE ? AND GDT_V = 'NDV' GROUP BY Given_To";
                    using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, this.conn))
                    {
                        dataAdapter.SelectCommand.Parameters.AddWithValue("@GivenTo", "%" + this.textBox107.Text.Trim() + "%");
                        dataAdapter.Fill(dataTable);
                    }
                    this.dataGridView13.Visible = true;
                    DataTable dataTablegv = new DataTable();
                    string querygv = "SELECT TOP 500 Given_To as Name, Total_Given as GAmount, Given_Date as GDate, ThroughBy as GUsing, GDT_V_Date as LUpDT, Remarks_Given as Remarks FROM Given WHERE Given_To LIKE ? AND GDT_V = 'NDV' ORDER BY Given_Date DESC";
                    using (OleDbDataAdapter dataAdaptergv = new OleDbDataAdapter(querygv, this.conn))
                    {
                        dataAdaptergv.SelectCommand.Parameters.AddWithValue("@GivenTo", "%" + this.textBox107.Text.Trim() + "%");
                        dataAdaptergv.Fill(dataTablegv);
                    }
                    if (dataTable.Rows.Count > 0 && !string.IsNullOrWhiteSpace(this.textBox107.Text.Trim()) && dataTablegv.Rows.Count > 0)
                    {
                        this.label231.Text = dataTable.Rows[0]["Total"].ToString();
                        this.dataGridView13.DataSource = dataTablegv;
                    }
                    else
                    {
                        this.label231.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void textBox124_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!(this.textBox124.Text.Trim() != ""))
                {
                    this.dataGridView13.DataSource = null;
                    this.label233.Text = "";
                    this.label252.Visible = false;
                    this.dataGridView13.Visible = false;
                }
                else
                {
                    this.label252.Visible = true;
                    this.label252.Text = "Taken";
                    DataTable dataTable = new DataTable();
                    string query = "SELECT SUM(Total_Take) as Total, Take_To FROM Teken WHERE Take_To LIKE ? AND TDT_V = 'NDV' GROUP BY Take_To";
                    using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, this.conn))
                    {
                        dataAdapter.SelectCommand.Parameters.AddWithValue("@TakeTo", "%" + this.textBox124.Text.Trim() + "%");
                        dataAdapter.Fill(dataTable);
                    }
                    this.dataGridView13.Visible = true;
                    DataTable dataTablegv = new DataTable();
                    string querygv = "SELECT TOP 500 Take_To as Name, Total_Take as TAmount, Take_Date as TDate, ThroughBy as TUsing, TDT_V_Date as LUpDT, Remarks_Take as Remarks FROM Teken WHERE Take_To LIKE ? AND TDT_V = 'NDV' ORDER BY Take_Date DESC";
                    using (OleDbDataAdapter dataAdaptergv = new OleDbDataAdapter(querygv, this.conn))
                    {
                        dataAdaptergv.SelectCommand.Parameters.AddWithValue("@TakeTo", "%" + this.textBox124.Text.Trim() + "%");
                        dataAdaptergv.Fill(dataTablegv);
                    }
                    if (dataTable.Rows.Count > 0 && !string.IsNullOrWhiteSpace(this.textBox124.Text.Trim()) && dataTablegv.Rows.Count > 0)
                    {
                        this.label233.Text = dataTable.Rows[0]["Total"].ToString();
                        this.dataGridView13.DataSource = dataTablegv;
                    }
                    else
                    {
                        this.label233.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void textBox130_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!(this.textBox130.Text.Trim() != ""))
                {
                    this.dataGridView13.DataSource = null;
                    this.label250.Text = "";
                    this.label252.Visible = false;
                    this.dataGridView13.Visible = false;
                }
                else
                {
                    this.label252.Visible = true;
                    this.label252.Text = "Expense";
                    DataTable dataTable = new DataTable();
                    string query = "SELECT SUM(Expense_Amount) as Total, Expense_To FROM TariffAmt WHERE Expense_To LIKE ? AND EDT_V = 'NDV' GROUP BY Expense_To";
                    using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, this.conn))
                    {
                        dataAdapter.SelectCommand.Parameters.AddWithValue("@ExpenseTo", "%" + this.textBox130.Text.Trim() + "%");
                        dataAdapter.Fill(dataTable);
                    }
                    this.dataGridView13.Visible = true;
                    DataTable dataTablegv = new DataTable();
                    string querygv = "SELECT TOP 500 Expense_To as Name, Expense_Amount as EAmount, Expense_Date as EDate, ThroughBy as EUsing, EDT_V_Date as LUpDT, Remarks_Expense as Remarks FROM TariffAmt WHERE Expense_To LIKE ? AND EDT_V = 'NDV' ORDER BY Expense_Date DESC";
                    using (OleDbDataAdapter dataAdaptergv = new OleDbDataAdapter(querygv, this.conn))
                    {
                        dataAdaptergv.SelectCommand.Parameters.AddWithValue("@ExpenseTo", "%" + this.textBox130.Text.Trim() + "%");
                        dataAdaptergv.Fill(dataTablegv);
                    }
                    if (dataTable.Rows.Count > 0 && !string.IsNullOrWhiteSpace(this.textBox130.Text.Trim()) && dataTablegv.Rows.Count > 0)
                    {
                        this.label250.Text = dataTable.Rows[0]["Total"].ToString();
                        this.dataGridView13.DataSource = dataTablegv;
                    }
                    else
                    {
                        this.label250.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void textBox125_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!(this.textBox125.Text.Trim() != ""))
                {
                    this.dataGridView13.DataSource = null;
                    this.label235.Text = "";
                    this.label252.Visible = false;
                    this.dataGridView13.Visible = false;
                }
                else
                {
                    this.label252.Visible = true;
                    this.label252.Text = "Savings";
                    DataTable dataTable = new DataTable();
                    string query = "SELECT SUM(Saving_Amount) as Total, Saving_To FROM Saving WHERE Saving_To LIKE ? AND SDT_V = 'NDV' GROUP BY Saving_To";
                    using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, this.conn))
                    {
                        dataAdapter.SelectCommand.Parameters.AddWithValue("@SavingTo", "%" + this.textBox125.Text.Trim() + "%");
                        dataAdapter.Fill(dataTable);
                    }
                    this.dataGridView13.Visible = true;
                    DataTable dataTablegv = new DataTable();
                    string querygv = "SELECT TOP 500 Saving_To as Name, Saving_Amount as SAmount, Saving_Date as SDate, ThroughBy as SUsing, SDT_V_Date as LUpDT, Remarks_Saving as Remarks FROM Saving WHERE Saving_To LIKE ? AND SDT_V = 'NDV' ORDER BY Saving_Date DESC";
                    using (OleDbDataAdapter dataAdaptergv = new OleDbDataAdapter(querygv, this.conn))
                    {
                        dataAdaptergv.SelectCommand.Parameters.AddWithValue("@SavingTo", "%" + this.textBox125.Text.Trim() + "%");
                        dataAdaptergv.Fill(dataTablegv);
                    }
                    if (dataTable.Rows.Count > 0 && !string.IsNullOrWhiteSpace(this.textBox125.Text.Trim()) && dataTablegv.Rows.Count > 0)
                    {
                        this.label235.Text = dataTable.Rows[0]["Total"].ToString();
                        this.dataGridView13.DataSource = dataTablegv;
                    }
                    else
                    {
                        this.label235.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void textBox126_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!(this.textBox126.Text.Trim() != ""))
                {
                    this.dataGridView13.DataSource = null;
                    this.label237.Text = "";
                    this.label252.Visible = false;
                    this.dataGridView13.Visible = false;
                }
                else
                {
                    this.label252.Visible = true;
                    this.label252.Text = "Unrated";
                    DataTable dataTable = new DataTable();
                    string query = "SELECT SUM(Unrated_Amount) as Total, Unrated_To FROM Unrated WHERE Unrated_To LIKE ? AND UDT_V = 'NDV' GROUP BY Unrated_To";
                    using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, this.conn))
                    {
                        dataAdapter.SelectCommand.Parameters.AddWithValue("@UnratedTo", "%" + this.textBox126.Text.Trim() + "%");
                        dataAdapter.Fill(dataTable);
                    }
                    this.dataGridView13.Visible = true;
                    DataTable dataTablegv = new DataTable();
                    string querygv = "SELECT TOP 500 Unrated_To as Name, Unrated_Amount as UAmount, Unrated_Date as UDate, ThroughBy as TUsing, UDT_V_Date as LUpDT, Remarks_Unrated as Remarks FROM Unrated WHERE Unrated_To LIKE ? AND UDT_V = 'NDV' ORDER BY Unrated_Date DESC";
                    using (OleDbDataAdapter dataAdaptergv = new OleDbDataAdapter(querygv, this.conn))
                    {
                        dataAdaptergv.SelectCommand.Parameters.AddWithValue("@UnratedTo", "%" + this.textBox126.Text.Trim() + "%");
                        dataAdaptergv.Fill(dataTablegv);
                    }
                    if (dataTable.Rows.Count > 0 && !string.IsNullOrWhiteSpace(this.textBox126.Text.Trim()) && dataTablegv.Rows.Count > 0)
                    {
                        this.label237.Text = dataTable.Rows[0][0].ToString();
                        this.dataGridView13.DataSource = dataTablegv;
                    }
                    else
                    {
                        this.label237.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void textBox34_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (!(this.textBox34.Text.Trim() != ""))
                {
                    if (this.comboBox1.Text == "Hand")
                    {
                        this.textBox34.Text = "Through By Hand ()";
                        this.button6.Focus();
                    }
                    else if (this.comboBox1.Text == "Bkash")
                    {
                        this.textBox34.Text = "Through By Bkash ()";
                        this.button6.Focus();
                    }
                    else if (this.comboBox1.Text == "Nagad")
                    {
                        this.textBox34.Text = "Through By Nagad ()";
                        this.button6.Focus();
                    }
                    else if (this.comboBox1.Text == "Roket")
                    {
                        this.textBox34.Text = "Through By Roket ()";
                        this.button6.Focus();
                    }
                    else if (this.comboBox1.Text == "DBBL")
                    {
                        this.textBox34.Text = "Through By DBBL ()";
                        this.button6.Focus();
                    }
                    else if (this.comboBox1.Text == "City Bank")
                    {
                        this.textBox34.Text = "Through By City Bank Cr.Card / CityTouch ()";
                        this.button6.Focus();
                    }
                    else if (this.comboBox1.Text == "Expense")
                    {
                        this.textBox34.Text = "Through By  ()";
                        this.button6.Focus();
                    }
                    else if (this.comboBox1.Text == "Savings")
                    {
                        this.textBox34.Text = "Through By ()";
                        this.button6.Focus();
                    }
                }
                else
                {
                    this.button6.Focus();
                }
            }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox1.Text.Trim() != ""))
                    {
                        this.textBox1.Focus();
                    }
                    else
                    {
                        this.button1.Focus();
                    }
                }
            }
        }
        private void radioButton5_Click(object sender, EventArgs e)
        {
            TextBox textBox = this.textBox35;
            string[] strArrays = new string[] { "G", null, null, null, null };
            int date = DateTime.Now.Day;
            int month = DateTime.Now.Month;
            int millis = DateTime.Now.Millisecond;
            strArrays[2] = date.ToString();
            strArrays[3] = month.ToString();
            strArrays[4] = millis.ToString();
            textBox.Text = string.Concat(strArrays);
            this.textBox39.Focus();
        }
        private void radioButton4_Click(object sender, EventArgs e)
        {
            TextBox textBox = this.textBox35;
            string[] strArrays = new string[] { "T", null, null, null, null };
            int date = DateTime.Now.Day;
            int month = DateTime.Now.Month;
            int millis = DateTime.Now.Millisecond;
            strArrays[2] = date.ToString();
            strArrays[3] = month.ToString();
            strArrays[4] = millis.ToString();
            textBox.Text = string.Concat(strArrays);
            this.textBox39.Focus();
        }
        private void radioButton3_Click(object sender, EventArgs e)
        {
            TextBox textBox = this.textBox35;
            string[] strArrays = new string[] { "E", null, null, null, null };
            int date = DateTime.Now.Day;
            int month = DateTime.Now.Month;
            int millis = DateTime.Now.Millisecond;
            strArrays[2] = date.ToString();
            strArrays[3] = month.ToString();
            strArrays[4] = millis.ToString();
            textBox.Text = string.Concat(strArrays);
            this.textBox39.Focus();
        }
        private void radioButton1_Click(object sender, EventArgs e)
        {
            TextBox textBox = this.textBox35;
            string[] strArrays = new string[] { "S", null, null, null, null };
            int date = DateTime.Now.Day;
            int month = DateTime.Now.Month;
            int millis = DateTime.Now.Millisecond;
            strArrays[2] = date.ToString();
            strArrays[3] = month.ToString();
            strArrays[4] = millis.ToString();
            textBox.Text = string.Concat(strArrays);
            this.textBox39.Focus();
        }
        private void radioButton2_Click(object sender, EventArgs e)
        {
            TextBox textBox = this.textBox35;
            string[] strArrays = new string[] { "U", null, null, null, null };
            int date = DateTime.Now.Day;
            int month = DateTime.Now.Month;
            int millis = DateTime.Now.Millisecond;
            strArrays[2] = date.ToString();
            strArrays[3] = month.ToString();
            strArrays[4] = millis.ToString();
            textBox.Text = string.Concat(strArrays);
            this.textBox39.Focus();
        }
        private void textBox72_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (!(this.textBox72.Text.Trim() != ""))
                {
                    this.textBox72.Focus();
                }
                else
                {
                    if (this.button15.Text == "New")
                    {
                        MessageBox.Show(string.Concat("Please Press New Button"));
                        this.textBox72.Focus();
                    }
                    else
                    {
                        this.textBox3.Focus();
                    }
                }
            }
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox3.Text.Trim() != ""))
                    {
                        this.textBox3.Focus();
                    }
                    else
                    {
                        this.textBox2.Focus();
                    }
                }
            }
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    try
                    {
                        if (!(this.textBox2.Text.Trim() != ""))
                        {
                            this.textBox2.Focus();
                        }
                        else
                        {
                            int num1 = Convert.ToInt32(this.textBox3.Text.Trim());
                            int num2 = Convert.ToInt32(this.textBox2.Text.Trim());
                            int num3 = num1 * num2;
                            this.label9.Text = num3.ToString();
                            this.AllItemAdd();
                            this.textBox73.Focus();
                        }
                    }
                    catch (Exception)
                    {
                        this.textBox2.Focus();
                    }
                }
            }
        }
        private void textBox73_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (!(this.textBox73.Text.Trim() != ""))
                {
                    this.textBox73.Focus();
                }
                else
                {
                    this.textBox5.Focus();
                }
            }
        }
        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox5.Text.Trim() != ""))
                    {
                        this.textBox5.Focus();
                    }
                    else
                    {
                        this.textBox4.Focus();
                    }
                }
            }
        }
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    try
                    {
                        if (!(this.textBox4.Text.Trim() != ""))
                        {
                            this.textBox4.Focus();
                        }
                        else
                        {
                            int num1 = Convert.ToInt32(this.textBox5.Text.Trim());
                            int num2 = Convert.ToInt32(this.textBox4.Text.Trim());
                            int num3 = num1 * num2;
                            this.label13.Text = num3.ToString();
                            this.AllItemAdd();
                            this.textBox78.Focus();
                        }
                    }
                    catch (Exception)
                    {
                        this.textBox4.Focus();
                    }
                }
            }
        }
        private void textBox78_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (!(this.textBox78.Text.Trim() != ""))
                {
                    this.textBox78.Focus();
                }
                else
                {
                    this.textBox7.Focus();
                }
            }
        }
        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox7.Text.Trim() != ""))
                    {
                        this.textBox7.Focus();
                    }
                    else
                    {
                        this.textBox6.Focus();
                    }
                }
            }
        }
        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    try
                    {
                        if (!(this.textBox6.Text.Trim() != ""))
                        {
                            this.textBox6.Focus();
                        }
                        else
                        {
                            int num1 = Convert.ToInt32(this.textBox7.Text.Trim());
                            int num2 = Convert.ToInt32(this.textBox6.Text.Trim());
                            int num3 = num1 * num2;
                            this.label17.Text = num3.ToString();
                            this.AllItemAdd();
                            this.textBox75.Focus();
                        }
                    }
                    catch (Exception)
                    {
                        this.textBox6.Focus();
                    }
                }
            }
        }
        private void textBox75_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (!(this.textBox75.Text.Trim() != ""))
                {
                    this.textBox75.Focus();
                }
                else
                {
                    this.textBox9.Focus();
                }
            }
        }
        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox9.Text.Trim() != ""))
                    {
                        this.textBox9.Focus();
                    }
                    else
                    {
                        this.textBox8.Focus();
                    }
                }
            }
        }
        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    try
                    {
                        if (!(this.textBox8.Text.Trim() != ""))
                        {
                            this.textBox8.Focus();
                        }
                        else
                        {
                            int num1 = Convert.ToInt32(this.textBox9.Text.Trim());
                            int num2 = Convert.ToInt32(this.textBox8.Text.Trim());
                            int num3 = num1 * num2;
                            this.label24.Text = num3.ToString();
                            this.AllItemAdd();
                            this.textBox76.Focus();
                        }
                    }
                    catch (Exception)
                    {
                        this.textBox8.Focus();
                    }
                }
            }
        }
        private void textBox76_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (!(this.textBox76.Text.Trim() != ""))
                {
                    this.textBox76.Focus();
                }
                else
                {
                    this.textBox11.Focus();
                }
            }
        }
        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox11.Text.Trim() != ""))
                    {
                        this.textBox11.Focus();
                    }
                    else
                    {
                        this.textBox10.Focus();
                    }
                }
            }
        }
        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    try
                    {
                        if (!(this.textBox10.Text.Trim() != ""))
                        {
                            this.textBox10.Focus();
                        }
                        else
                        {
                            int num1 = Convert.ToInt32(this.textBox11.Text.Trim());
                            int num2 = Convert.ToInt32(this.textBox10.Text.Trim());
                            int num3 = num1 * num2;
                            this.label28.Text = num3.ToString();
                            this.AllItemAdd();
                            this.textBox77.Focus();
                        }
                    }
                    catch (Exception)
                    {
                        this.textBox10.Focus();
                    }
                }
            }
        }
        private void textBox77_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (!(this.textBox77.Text.Trim() != ""))
                {
                    this.textBox77.Focus();
                }
                else
                {
                    this.textBox13.Focus();
                }
            }
        }
        private void textBox13_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox13.Text.Trim() != ""))
                    {
                        this.textBox13.Focus();
                    }
                    else
                    {
                        this.textBox12.Focus();
                    }
                }
            }
        }
        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    try
                    {
                        if (!(this.textBox12.Text.Trim() != ""))
                        {
                            this.textBox12.Focus();
                        }
                        else
                        {
                            int num1 = Convert.ToInt32(this.textBox13.Text.Trim());
                            int num2 = Convert.ToInt32(this.textBox12.Text.Trim());
                            int num3 = num1 * num2;
                            this.label32.Text = num3.ToString();
                            this.AllItemAdd();
                            this.textBox79.Focus();
                        }
                    }
                    catch (Exception)
                    {
                        this.textBox12.Focus();
                    }
                    
                }
            }
        }
        private void textBox79_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (!(this.textBox79.Text.Trim() != ""))
                {
                    this.textBox79.Focus();
                }
                else
                {
                    this.textBox15.Focus();
                }
            }
        }
        private void textBox15_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox15.Text.Trim() != ""))
                    {
                        this.textBox15.Focus();
                    }
                    else
                    {
                        this.textBox14.Focus();
                    }
                }
            }
        }
        private void textBox14_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    try
                    {
                        if (!(this.textBox14.Text.Trim() != ""))
                        {
                            this.textBox14.Focus();
                        }
                        else
                        {
                            int num1 = Convert.ToInt32(this.textBox15.Text.Trim());
                            int num2 = Convert.ToInt32(this.textBox14.Text.Trim());
                            int num3 = num1 * num2;
                            this.label36.Text = num3.ToString();
                            this.AllItemAdd();
                            this.textBox80.Focus();
                        }
                    }
                    catch (Exception)
                    {
                        this.textBox14.Focus();
                    }
                    
                }
            }
        }
        private void textBox80_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (!(this.textBox80.Text.Trim() != ""))//work here after rest
                {
                    this.textBox80.Focus();
                }
                else
                {
                    this.textBox17.Focus();
                }
            }
        }
        private void textBox17_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox17.Text.Trim() != ""))
                    {
                        this.textBox17.Focus();
                    }
                    else
                    {
                        this.textBox16.Focus();
                    }
                }
            }
        }
        private void textBox16_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    try
                    {
                        if (!(this.textBox16.Text.Trim() != ""))
                        {
                            this.textBox16.Focus();
                        }
                        else
                        {
                            int num1 = Convert.ToInt32(this.textBox17.Text.Trim());
                            int num2 = Convert.ToInt32(this.textBox16.Text.Trim());
                            int num3 = num1 * num2;
                            this.label40.Text = num3.ToString();
                            this.AllItemAdd();
                            this.textBox81.Focus();
                        }
                    }
                    catch (Exception ex)
                    {
                        this.textBox16.Focus();
                    }
                    
                }
            }
        }
        private void textBox81_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (!(this.textBox80.Text.Trim() != ""))
                {
                    this.textBox81.Focus();
                }
                else
                {
                    this.textBox19.Focus();
                }
            }
        }
        private void textBox19_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox19.Text.Trim() != ""))
                    {
                        this.textBox19.Focus();
                    }
                    else
                    {
                        this.textBox18.Focus();
                    }
                }
            }
        }
        private void textBox18_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    try
                    {
                        if (!(this.textBox18.Text.Trim() != ""))
                        {
                            this.textBox18.Focus();
                        }
                        else
                        {

                            int num1 = Convert.ToInt32(this.textBox19.Text.Trim());
                            int num2 = Convert.ToInt32(this.textBox18.Text.Trim());
                            int num3 = num1 * num2;
                            this.label44.Text = num3.ToString();
                            this.AllItemAdd();
                            this.textBox82.Focus();
                        }
                    }
                    catch (Exception)
                    {
                        this.textBox18.Focus();
                    }
                }
            }
        }
        private void textBox82_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (!(this.textBox82.Text.Trim() != ""))
                {
                    this.textBox82.Focus();
                }
                else
                {
                    this.textBox21.Focus();
                }
            }
        }
        private void textBox21_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox21.Text.Trim() != ""))
                    {
                        this.textBox21.Focus();
                    }
                    else
                    {
                        this.textBox20.Focus();
                    }
                }
            }
        }
        private void textBox20_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    try
                    {
                        if (!(this.textBox20.Text.Trim() != ""))
                        {
                            this.textBox20.Focus();
                        }
                        else
                        {
                            int num1 = Convert.ToInt32(this.textBox21.Text.Trim());
                            int num2 = Convert.ToInt32(this.textBox20.Text.Trim());
                            int num3 = num1 * num2;
                            this.label48.Text = num3.ToString();
                            this.AllItemAdd();
                            this.textBox83.Focus();
                        }
                    }
                    catch (Exception)
                    {
                        this.textBox20.Focus();
                    }
                }
            }
        }
        private void textBox83_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (!(this.textBox83.Text.Trim() != ""))
                {
                    this.textBox83.Focus();
                }
                else
                {
                    this.textBox23.Focus();
                }
            }
        }
        private void textBox23_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox23.Text.Trim() != ""))
                    {
                        this.textBox23.Focus();
                    }
                    else
                    {
                        this.textBox22.Focus();
                    }
                }
            }
        }
        private void textBox22_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    try
                    {
                        if (!(this.textBox22.Text.Trim() != ""))
                        {
                            this.textBox22.Focus();
                        }
                        else
                        {
                            int num1 = Convert.ToInt32(this.textBox23.Text.Trim());
                            int num2 = Convert.ToInt32(this.textBox22.Text.Trim());
                            int num3 = num1 * num2;
                            this.label52.Text = num3.ToString();
                            this.AllItemAdd();
                            this.textBox84.Focus();
                        }
                    }
                    catch (Exception)
                    {
                        this.textBox22.Focus();
                    }
                }
            }
        }
        private void textBox84_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (!(this.textBox84.Text.Trim() != ""))
                {
                    this.textBox84.Focus();
                }
                else
                {
                    this.textBox25.Focus();
                }
            }
        }
        private void textBox25_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox25.Text.Trim() != ""))
                    {
                        this.textBox25.Focus();
                    }
                    else
                    {
                        this.textBox24.Focus();
                    }
                }
            }
        }
        private void textBox24_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    try
                    {
                        if (!(this.textBox24.Text.Trim() != ""))
                        {
                            this.textBox24.Focus();
                        }
                        else
                        {
                            int num1 = Convert.ToInt32(this.textBox25.Text.Trim());
                            int num2 = Convert.ToInt32(this.textBox24.Text.Trim());
                            int num3 = num1 * num2;
                            this.label56.Text = num3.ToString();
                            this.AllItemAdd();
                            this.textBox85.Focus();
                        }
                    }
                    catch (Exception)
                    {
                        this.textBox24.Focus();
                    }
                }
            }
        }
        private void textBox85_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (!(this.textBox85.Text.Trim() != ""))
                {
                    this.textBox85.Focus();
                }
                else
                {
                    this.textBox27.Focus();
                }
            }
        }
        private void textBox27_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox27.Text.Trim() != ""))
                    {
                        this.textBox27.Focus();
                    }
                    else
                    {
                        this.textBox26.Focus();
                    }
                }
            }
        }
        private void textBox26_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    try
                    {
                        if (!(this.textBox26.Text.Trim() != ""))
                        {
                            this.textBox26.Focus();
                        }
                        else
                        {
                            int num1 = Convert.ToInt32(this.textBox27.Text.Trim());
                            int num2 = Convert.ToInt32(this.textBox26.Text.Trim());
                            int num3 = num1 * num2;
                            this.label60.Text = num3.ToString();
                            this.AllItemAdd();
                            this.textBox86.Focus();
                        }
                    }
                    catch (Exception)
                    {
                        this.textBox26.Focus();
                    }
                    
                }
            }
        }
        private void textBox86_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (!(this.textBox86.Text.Trim() != ""))
                {
                    this.textBox86.Focus();
                }
                else
                {
                    this.textBox29.Focus();
                }
            }
        }
        private void textBox29_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox29.Text.Trim() != ""))
                    {
                        this.textBox29.Focus();
                    }
                    else
                    {
                        this.textBox28.Focus();
                    }
                }
            }
        }
        private void textBox28_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    try
                    {
                        if (!(this.textBox28.Text.Trim() != ""))
                        {
                            this.textBox28.Focus();
                        }
                        else
                        {
                            int num1 = Convert.ToInt32(this.textBox29.Text.Trim());
                            int num2 = Convert.ToInt32(this.textBox28.Text.Trim());
                            int num3 = num1 * num2;
                            this.label64.Text = num3.ToString();
                            this.AllItemAdd();
                            this.textBox87.Focus();
                        }
                    }
                    catch (Exception)
                    {
                        this.textBox28.Focus();
                    }
                    
                }
            }
        }
        private void textBox87_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (!(this.textBox87.Text.Trim() != ""))
                {
                    this.textBox87.Focus();
                }
                else
                {
                    this.textBox31.Focus();
                }
            }
        }
        private void textBox31_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox31.Text.Trim() != ""))
                    {
                        this.textBox31.Focus();
                    }
                    else
                    {
                        this.textBox30.Focus();
                    }
                }
            }
        }
        private void textBox30_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    try
                    {
                        if (!(this.textBox30.Text.Trim() != ""))
                        {
                            this.textBox30.Focus();
                        }
                        else
                        {
                            int num1 = Convert.ToInt32(this.textBox31.Text.Trim());
                            int num2 = Convert.ToInt32(this.textBox30.Text.Trim());
                            int num3 = num1 * num2;
                            this.label68.Text = num3.ToString();
                            this.AllItemAdd();
                            this.textBox88.Focus();
                        }
                    }
                    catch (Exception)
                    {
                        this.textBox30.Focus();
                    }
                }
            }
        }
        private void textBox88_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (!(this.textBox88.Text.Trim() != ""))
                {
                    this.textBox88.Focus();
                }
                else
                {
                    this.textBox38.Focus();
                }
            }
        }
        private void textBox38_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox38.Text.Trim() != ""))
                    {
                        this.textBox38.Focus();
                    }
                    else
                    {
                        this.textBox54.Focus();
                    }
                }
            }
        }
        private void textBox54_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    try
                    {
                        if (!(this.textBox54.Text.Trim() != ""))
                        {
                            this.textBox54.Focus();
                        }
                        else
                        {
                            int num1 = Convert.ToInt32(this.textBox38.Text.Trim());
                            int num2 = Convert.ToInt32(this.textBox54.Text.Trim());
                            int num3 = num1 * num2;
                            this.label76.Text = num3.ToString();
                            this.AllItemAdd();
                        }
                    }
                    catch (Exception)
                    {
                        this.textBox54.Focus();
                    }
                }
            }
        }
        private void textBox56_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox56.Text.Trim() != ""))
                    {
                        this.textBox56.Focus();
                    }
                    else
                    {
                        if (this.button15.Text == "New")
                        {
                            MessageBox.Show(string.Concat("Please Press New Button"));
                            this.textBox56.Focus();
                        }
                        else
                        {
                            this.label179.Text = this.textBox56.Text.Trim();
                            this.AllIinvAdd();
                            this.textBox57.Focus();
                        }
                    }
                }
            }
        }
        private void textBox57_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox57.Text.Trim() != ""))
                    {
                        this.textBox57.Focus();
                    }
                    else
                    {
                        this.label172.Text = this.textBox57.Text.Trim();
                        this.AllIinvAdd();
                        this.textBox58.Focus();
                    }
                }
            }
        }        
        private void textBox58_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox58.Text.Trim() != ""))
                    {
                        this.textBox58.Focus();
                    }
                    else
                    {
                        this.label171.Text = this.textBox58.Text.Trim();
                        this.AllIinvAdd();
                        this.textBox59.Focus();
                    }
                }
            }
        }
        private void textBox59_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox59.Text.Trim() != ""))
                    {
                        this.textBox59.Focus();
                    }
                    else
                    {
                        this.label170.Text = this.textBox59.Text.Trim();
                        this.AllIinvAdd();
                        this.textBox60.Focus();
                    }
                }
            }
        }
        private void textBox60_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox60.Text.Trim() != ""))
                    {
                        this.textBox60.Focus();
                    }
                    else
                    {
                        this.label169.Text = this.textBox60.Text.Trim();
                        this.AllIinvAdd();
                        this.textBox61.Focus();
                    }
                }
            }
        }
        private void textBox61_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox61.Text.Trim() != ""))
                    {
                        this.textBox61.Focus();
                    }
                    else
                    {
                        this.label168.Text = this.textBox61.Text.Trim();
                        this.AllIinvAdd();
                        this.textBox62.Focus();
                    }
                }
            }
        }
        private void textBox62_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox62.Text.Trim() != ""))
                    {
                        this.textBox62.Focus();
                    }
                    else
                    {
                        this.label167.Text = this.textBox62.Text.Trim();
                        this.AllIinvAdd();
                        this.textBox63.Focus();
                    }
                }
            }
        }
        private void textBox63_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox63.Text.Trim() != ""))
                    {
                        this.textBox63.Focus();
                    }
                    else
                    {
                        this.label166.Text = this.textBox63.Text.Trim();
                        this.AllIinvAdd();
                        this.textBox64.Focus();
                    }
                }
            }
        }
        private void textBox64_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox64.Text.Trim() != ""))
                    {
                        this.textBox64.Focus();
                    }
                    else
                    {
                        this.label165.Text = this.textBox64.Text.Trim();
                        this.AllIinvAdd();
                        this.textBox65.Focus();
                    }
                }
            }
        }
        private void textBox65_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox65.Text.Trim() != ""))
                    {
                        this.textBox65.Focus();
                    }
                    else
                    {
                        this.label164.Text = this.textBox65.Text.Trim();
                        this.AllIinvAdd();
                        this.textBox66.Focus();
                    }
                }
            }
        }
        private void textBox66_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox66.Text.Trim() != ""))
                    {
                        this.textBox66.Focus();
                    }
                    else
                    {
                        this.label163.Text = this.textBox66.Text.Trim();
                        this.AllIinvAdd();
                        this.textBox67.Focus();
                    }
                }
            }
        }
        private void textBox67_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox67.Text.Trim() != ""))
                    {
                        this.textBox67.Focus();
                    }
                    else
                    {
                        this.label162.Text = this.textBox67.Text.Trim();
                        this.AllIinvAdd();
                        this.textBox68.Focus();
                    }
                }
            }
        }
        private void textBox68_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox68.Text.Trim() != ""))
                    {
                        this.textBox68.Focus();
                    }
                    else
                    {
                        this.label161.Text = this.textBox68.Text.Trim();
                        this.AllIinvAdd();
                        this.textBox69.Focus();
                    }
                }
            }
        }
        private void textBox69_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox69.Text.Trim() != ""))
                    {
                        this.textBox69.Focus();
                    }
                    else
                    {
                        this.label160.Text = this.textBox69.Text.Trim();
                        this.AllIinvAdd();
                        this.textBox70.Focus();
                    }
                }
            }
        }
        private void textBox70_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox70.Text.Trim() != ""))
                    {
                        this.textBox70.Focus();
                    }
                    else
                    {
                        this.label159.Text = this.textBox70.Text.Trim();
                        this.AllIinvAdd();
                        this.textBox71.Focus();
                    }
                }
            }
        }
        private void textBox71_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox71.Text.Trim() != ""))
                    {
                        this.textBox71.Focus();
                    }
                    else
                    {
                        this.label158.Text = this.textBox71.Text.Trim();
                        this.AllIinvAdd();
                        this.textBox89.Focus();
                    }
                }
            }
        }
        private void textBox89_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox89.Text.Trim() != ""))
                    {
                        this.textBox89.Focus();
                    }
                    else
                    {
                        this.label157.Text = this.textBox89.Text.Trim();
                        this.AllIinvAdd();
                        this.textBox91.Focus();
                    }
                }
            }
        }
        private void textBox91_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox91.Text.Trim() != ""))
                    {
                        this.textBox91.Focus();
                    }
                    else
                    {
                        this.label156.Text = this.textBox91.Text.Trim();
                        this.AllIinvAdd();
                        this.textBox110.Focus();
                    }
                }
            }
        }
        private void textBox110_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox110.Text.Trim() != ""))
                    {
                        this.textBox110.Focus();
                    }
                    else
                    {
                        this.label155.Text = this.textBox110.Text.Trim();
                        this.AllIinvAdd();
                        this.textBox111.Focus();
                    }
                }
            }
        }
        private void textBox111_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox111.Text.Trim() != ""))
                    {
                        this.textBox111.Focus();
                    }
                    else
                    {
                        this.label154.Text = this.textBox111.Text.Trim();
                        this.AllIinvAdd();
                        this.textBox112.Focus();
                    }
                }
            }
        }
        private void textBox112_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox112.Text.Trim() != ""))
                    {
                        this.textBox112.Focus();
                    }
                    else
                    {
                        this.label153.Text = this.textBox112.Text.Trim();
                        this.AllIinvAdd();
                        this.textBox113.Focus();
                    }
                }
            }
        }
        private void textBox113_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox113.Text.Trim() != ""))
                    {
                        this.textBox113.Focus();
                    }
                    else
                    {
                        this.label152.Text = this.textBox113.Text.Trim();
                        this.AllIinvAdd();
                        this.textBox114.Focus();
                    }
                }
            }
        }
        private void textBox114_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox114.Text.Trim() != ""))
                    {
                        this.textBox114.Focus();
                    }
                    else
                    {
                        this.label151.Text = this.textBox114.Text.Trim();
                        this.AllIinvAdd();
                        this.textBox115.Focus();
                    }
                }
            }
        }
        private void textBox115_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox115.Text.Trim() != ""))
                    {
                        this.textBox115.Focus();
                    }
                    else
                    {
                        this.label150.Text = this.textBox115.Text.Trim();
                        this.AllIinvAdd();
                        this.textBox55.Focus();
                    }
                }
            }
        }
        private void textBox55_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.textBox55.Text.Trim() != "")
                {
                    double num   = double.Parse(this.textBox55.Text.Trim());
                    double num1  = double.Parse(this.textBox90.Text.Trim());
                    double num3  = num1 - num;
                    decimal num2 = Convert.ToDecimal(num3.ToString());
                    Label str1 = this.label147;
                    decimal num4 = Math.Round(num2, 4);
                    num3 = double.Parse(num4.ToString());
                    str1.Text = num3.ToString();
                }
                else
                {
                    this.label147.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void textBox55_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox55.Text.Trim() != ""))
                    {
                        this.textBox55.Focus();
                    }
                    else
                    {
                        this.button15.Focus();
                    }
                }
            }
        }
        private void textBox37_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox37.Text.Trim() != ""))
                    {
                        this.textBox37.Focus();
                    }
                    else
                    {
                        int dev = Convert.ToInt32(this.textBox37.Text.Trim()) / 2;
                        this.label194.Text = dev.ToString();
                        this.button10.Focus();
                    }
                }
            }
        }
        private void textBox50_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox50.Text.Trim() != ""))
                    {
                        this.textBox50.Focus();
                    }
                    else
                    {
                        this.button14.Focus();
                    }
                }
            }
        }
        private void textBox32_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox32.Text.Trim() != ""))
                    {
                        this.textBox32.Focus();
                    }
                    else
                    {
                        this.button4.Focus();
                    }
                }
            }
        }
        private void textBox94_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox94.Text.Trim() != ""))
                    {
                        MessageBox.Show("Please Insert Amount.");
                        this.textBox32.Focus();
                    }
                    else
                    {
                        this.textBox95.Focus();
                    }
                }
            }
        }
        private void textBox95_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox95.Text.Trim() != ""))
                    {
                        MessageBox.Show("Please Insert Amount.");
                        this.textBox35.Focus();
                    }
                    else
                    {
                        this.textBox96.Focus();
                    }
                }
            }
        }
        private void textBox95_TextChanged(object sender, EventArgs e)
        {
            if (!(this.textBox95.Text.Trim() != ""))
            {
                MessageBox.Show("Please Insert Amount.");
                this.textBox35.Focus();
            }
            else
            {
                int dev = Convert.ToInt32(this.textBox94.Text.Trim()) / Convert.ToInt32(this.textBox95.Text.Trim());
                this.label195.Text = dev.ToString();
            }
        }
        private void textBox96_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox96.Text.Trim() != ""))
                    {
                        MessageBox.Show("Please Insert Amount.");
                        this.textBox96.Focus();
                    }
                    else
                    {
                        this.textBox97.Focus();
                    }
                }
            }
        }
        private void textBox97_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox97.Text.Trim() != ""))
                    {
                        MessageBox.Show("Please Insert Amount.");
                        this.textBox97.Focus();
                    }
                    else
                    {
                        this.button13.Focus();
                    }
                }
            }
        }
        private void textBox109_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox109.Text.Trim() != ""))
                    {
                        this.textBox109.Focus();
                    }
                    else
                    {
                        this.button16.Focus();
                    }
                }
            }
        }
        private void textBox119_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox119.Text.Trim() != ""))
                    {
                        this.textBox119.Focus();
                    }
                    else
                    {
                        this.button19.Focus();
                    }
                }
            }
        }
        private void textBox129_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox129.Text.Trim() != ""))
                    {
                        this.textBox129.Focus();
                    }
                    else
                    {
                        TextBox textBox = this.textBox98;
                        string[] strArrays = new string[] { "OM", null, null, null, null };
                        int date = DateTime.Now.Day;
                        int month = DateTime.Now.Month;
                        int millis = DateTime.Now.Millisecond;
                        strArrays[2] = date.ToString();
                        strArrays[3] = month.ToString();
                        strArrays[4] = millis.ToString();
                        textBox.Text = string.Concat(strArrays);
                        this.textBox128.Focus();
                    }
                }
            }
        }
        private void textBox128_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox128.Text.Trim() != ""))
                    {
                        this.textBox128.Focus();
                    }
                    else
                    {
                        this.button11.Focus();
                    }
                }
            }
        }
        private void textBox128_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.textBox128.Text.Trim() != "")
                {
                    double num = double.Parse(this.textBox129.Text.Trim());
                    double num1 = double.Parse(this.textBox128.Text.Trim());
                    double num3 = num1 + num;
                    decimal num2 = Convert.ToDecimal(num3.ToString());
                    Label str1 = this.label257;
                    decimal num4 = Math.Round(num2, 4);
                    num3 = double.Parse(num4.ToString());
                    str1.Text = num3.ToString();
                }
                else
                {
                    this.label257.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void textBox120_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox120.Text.Trim() != ""))
                    {
                        this.textBox120.Focus();
                    }
                    else
                    {
                        this.button20.Focus();
                    }
                }
            }
        }
        private void textBox116_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox116.Text.Trim() != ""))
                    {
                        this.textBox116.Focus();
                    }
                    else
                    {
                        this.button17.Focus();
                    }
                }
            }
        }
        private void textBox117_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox117.Text.Trim() != ""))
                    {
                        this.textBox117.Focus();
                    }
                    else
                    {
                        this.button18.Focus();
                    }
                }
            }
        }
        private void dateTimePicker4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (!(this.dateTimePicker4.Text.Trim() != ""))
                {
                    this.dateTimePicker4.Focus();
                }
                else
                {
                    this.textBox37.Focus();
                }
            }
        }
        private void dateTimePicker5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (!(this.dateTimePicker5.Text.Trim() != ""))
                {
                    this.dateTimePicker5.Focus();
                }
                else
                {
                    this.textBox50.Focus();
                }
            }
        }
        private void dateTimePicker2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (!(this.dateTimePicker2.Text.Trim() != ""))
                {
                    this.dateTimePicker2.Focus();
                }
                else
                {
                    this.textBox32.Focus();
                }
            }
        }
        private void dateTimePicker1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (!(this.dateTimePicker1.Text.Trim() != ""))
                {
                    this.dateTimePicker1.Focus();
                }
                else
                {
                    this.textBox1.Focus();
                }
            }
        }
        private void dateTimePicker6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (!(this.dateTimePicker6.Text.Trim() != ""))
                {
                    this.dateTimePicker6.Focus();
                }
                else
                {
                    this.textBox129.Focus();
                }
            }
        }
        private void textBox117_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.textBox117.Text.Trim() != "")
                {
                    if (!this.checkBox5.Checked)
                    {
                        double num = double.Parse(this.label111.Text.Trim());
                        double num1 = double.Parse(this.textBox117.Text.Trim());
                        double num3 = num1 + num;
                        decimal num2 = Convert.ToDecimal(num3.ToString());
                        TextBox str1 = this.textBox51;
                        decimal num4 = Math.Round(num2, 4);
                        num3 = double.Parse(num4.ToString());
                        str1.Text = num3.ToString();
                    }
                    else
                    {
                        double num = double.Parse(this.label111.Text.Trim());
                        double num1 = double.Parse(this.textBox117.Text.Trim());
                        double num3 = num - num1;
                        decimal num2 = Convert.ToDecimal(num3.ToString());
                        TextBox str1 = this.textBox51;
                        decimal num4 = Math.Round(num2, 4);
                        num3 = double.Parse(num4.ToString());
                        str1.Text = num3.ToString();
                    }
                }
                else
                {
                    this.textBox51.Text = this.label111.Text.Trim();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void textBox116_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.textBox116.Text.Trim() != "")
                {
                    if (!this.checkBox4.Checked)
                    {
                        double num = double.Parse(this.label111.Text.Trim());
                        double num1 = double.Parse(this.textBox116.Text.Trim());
                        double num3 = num1 + num;
                        decimal num2 = Convert.ToDecimal(num3.ToString());
                        TextBox str1 = this.textBox43;
                        decimal num4 = Math.Round(num2, 4);
                        num3 = double.Parse(num4.ToString());
                        str1.Text = num3.ToString();
                    }
                    else
                    {
                        double num = double.Parse(this.label111.Text.Trim());
                        double num1 = double.Parse(this.textBox116.Text.Trim());
                        double num3 = num - num1;
                        decimal num2 = Convert.ToDecimal(num3.ToString());
                        TextBox str1 = this.textBox43;
                        decimal num4 = Math.Round(num2, 4);
                        num3 = double.Parse(num4.ToString());
                        str1.Text = num3.ToString();
                    }
                }
                else
                {
                    this.textBox43.Text = this.label111.Text.Trim();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void textBox109_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.textBox109.Text.Trim() != "")
                {
                    if (!this.checkBox3.Checked)
                    {
                        double num = double.Parse(this.label111.Text.Trim());
                        double num1 = double.Parse(this.textBox109.Text.Trim());
                        double num3 = num1 + num;
                        decimal num2 = Convert.ToDecimal(num3.ToString());
                        TextBox str1 = this.textBox103;
                        decimal num4 = Math.Round(num2, 4);
                        num3 = double.Parse(num4.ToString());
                        str1.Text = num3.ToString();
                    }
                    else
                    {
                        double num = double.Parse(this.label111.Text.Trim());
                        double num1 = double.Parse(this.textBox109.Text.Trim());
                        double num3 = num - num1;
                        decimal num2 = Convert.ToDecimal(num3.ToString());
                        TextBox str1 = this.textBox103;
                        decimal num4 = Math.Round(num2, 4);
                        num3 = double.Parse(num4.ToString());
                        str1.Text = num3.ToString();
                    }
                }
                else
                {
                    this.textBox103.Text = this.label111.Text.Trim();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void textBox120_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (this.textBox120.Text.Trim() != "")
                {
                    if (!this.checkBox2.Checked)
                    {
                        double num = double.Parse(this.label111.Text.Trim());
                        double num1 = double.Parse(this.textBox120.Text.Trim());
                        double num3 = num1 + num;
                        decimal num2 = Convert.ToDecimal(num3.ToString());
                        TextBox str1 = this.textBox45;
                        decimal num4 = Math.Round(num2, 4);
                        num3 = double.Parse(num4.ToString());
                        str1.Text = num3.ToString();
                    }
                    else
                    {
                        double num = double.Parse(this.label111.Text.Trim());
                        double num1 = double.Parse(this.textBox120.Text.Trim());
                        double num3 = num - num1;
                        decimal num2 = Convert.ToDecimal(num3.ToString());
                        TextBox str1 = this.textBox45;
                        decimal num4 = Math.Round(num2, 4);
                        num3 = double.Parse(num4.ToString());
                        str1.Text = num3.ToString();
                    }
                }
                else
                {
                    this.textBox45.Text = this.label111.Text.Trim();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void textBox119_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.textBox119.Text.Trim() != "")
                {
                    if (!this.checkBox1.Checked)
                    {
                        double num = double.Parse(this.label111.Text.Trim());
                        double num1 = double.Parse(this.textBox119.Text.Trim());
                        double num3 = num1 + num;
                        decimal num2 = Convert.ToDecimal(num3.ToString());
                        TextBox str1 = this.textBox40;
                        decimal num4 = Math.Round(num2, 4);
                        num3 = double.Parse(num4.ToString());
                        str1.Text = num3.ToString();
                    }
                    else
                    {
                        double num = double.Parse(this.label111.Text.Trim());
                        double num1 = double.Parse(this.textBox119.Text.Trim());
                        double num3 = num - num1;
                        decimal num2 = Convert.ToDecimal(num3.ToString());
                        TextBox str1 = this.textBox40;
                        decimal num4 = Math.Round(num2, 4);
                        num3 = double.Parse(num4.ToString());
                        str1.Text = num3.ToString();
                    }
                }
                else
                {
                    this.textBox40.Text = this.label111.Text.Trim();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void textBox133_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox133.Text.Trim() != ""))
                    {
                        this.textBox133.Focus();
                    }
                    else
                    {
                        int dev = Convert.ToInt32(this.textBox133.Text.Trim()) / 2;
                        this.textBox134.Text = dev.ToString();
                        this.button31.Focus();
                    }
                }
            }
        }
        private void textBox131_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox131.Text.Trim() != ""))
                    {
                        this.textBox131.Focus();
                    }
                    else
                    {
                        int dev = Convert.ToInt32(this.textBox131.Text.Trim()) / 3;
                        this.textBox135.Text = dev.ToString();
                        this.buttonS24.Focus();
                    }
                }
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.textBox119.Focus();
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.textBox120.Focus();
        }
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            this.textBox109.Focus();
        }
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            this.textBox116.Focus();
        }
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            this.textBox117.Focus();
        }

        #endregion
        //-----------------------------------------------------------------------
        //------------------------------If Query Needed--------------------------
        //-----------------------------------------------------------------------
    }
}
