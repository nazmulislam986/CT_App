using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
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
        private string DltDate;
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
            this.fillInstData();
            this.totalInstData();
            this.fillMemo();
            this.fillDataBike();
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
            this.textBox50.ReadOnly = true;
            this.panel9.Visible = false;
            this.panel12.Visible = false;
            this.button12.Visible = false;
            this.panel10.Visible = false;
            this.panel11.Visible = false;
            this.button21.Visible = false;
            this.button22.Visible = false;
            this.label231.Text = "";
            this.label233.Text = "";
            this.label235.Text = "";
            this.label237.Text = "";
        }

        //-----------------------------------------------------------------------
        //------------------------------All Classes------------------------------
        //-----------------------------------------------------------------------
        private void fillDataBike()
        {
            try
            {
                DataTable dataTabledltAmt = new DataTable();
                OleDbDataAdapter odbcDataAdapterdltAmt = new OleDbDataAdapter(string.Concat("SELECT B_Next_ODO as [ODO],B_Chng_Date as [Date],B_ID as [ID] FROM BikeInfo ORDER BY [ID] DESC"), this.conn);
                odbcDataAdapterdltAmt.Fill(dataTabledltAmt);
                dataGridView12.DataSource = dataTabledltAmt.DefaultView;
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
                DataTable dataTabledltAmt = new DataTable();
                OleDbDataAdapter odbcDataAdapterdltAmt = new OleDbDataAdapter(string.Concat("SELECT M_ID as [ID],M_Date as [Date],M_Amount as [Amount] FROM Market ORDER BY [M_Date] DESC"), this.conn);
                odbcDataAdapterdltAmt.Fill(dataTabledltAmt);
                dataGridView1.DataSource = dataTabledltAmt.DefaultView;
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
                DataTable dataTableAmt = new DataTable();
                OleDbDataAdapter dataAdapterdltAmtT = new OleDbDataAdapter(string.Concat(" SELECT SUM(M_Amount) FROM Market "), this.conn);
                dataAdapterdltAmtT.Fill(dataTableAmt);
                if (dataTableAmt.Rows.Count > 0)
                {
                    this.label5.Text = dataTableAmt.Rows[0][0].ToString();
                }
                else
                {
                    this.label5.Text = "0";
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
                DataTable dataTableAmtGiven = new DataTable();
                OleDbDataAdapter dataAdapterdltAmtG = new OleDbDataAdapter(string.Concat("SELECT SUM(Total_Given) FROM Given WHERE [GDT_V]='NDV' "), this.conn);
                dataAdapterdltAmtG.Fill(dataTableAmtGiven);
                this.label87.Text = dataTableAmtGiven.Rows[0][0].ToString();
                DataTable dataTableAmtTake = new DataTable();
                OleDbDataAdapter dataAdapterdltAmtT = new OleDbDataAdapter(string.Concat("SELECT SUM(Total_Take) FROM Teken WHERE [TDT_V]='NDV' "), this.conn);
                dataAdapterdltAmtT.Fill(dataTableAmtTake);
                this.label92.Text = dataTableAmtTake.Rows[0][0].ToString();
                DataTable dataTableAmtExp = new DataTable();
                OleDbDataAdapter dataAdapterdltAmtE = new OleDbDataAdapter(string.Concat("SELECT SUM(Expense_Amount) FROM Expense WHERE [EDT_V]='NDV' "), this.conn);
                dataAdapterdltAmtE.Fill(dataTableAmtExp);
                this.label90.Text = dataTableAmtExp.Rows[0][0].ToString();
                DataTable dataTableAmtSev = new DataTable();
                OleDbDataAdapter dataAdapterdltSev = new OleDbDataAdapter(string.Concat("SELECT SUM(Saving_Amount) FROM Saving WHERE [SDT_V]='NDV' "), this.conn);
                dataAdapterdltSev.Fill(dataTableAmtSev);
                this.label114.Text = dataTableAmtSev.Rows[0][0].ToString();
                DataTable dataTableAmtUnr = new DataTable();
                OleDbDataAdapter dataAdapterdltAmtUnr = new OleDbDataAdapter(string.Concat("SELECT SUM(Unrated_Amount) FROM Unrated WHERE [UDT_V]='NDV' "), this.conn);
                dataAdapterdltAmtUnr.Fill(dataTableAmtUnr);
                this.label116.Text = dataTableAmtUnr.Rows[0][0].ToString();
                DataTable dataTableAmtCol = new DataTable();
                OleDbDataAdapter dataAdapterdltAmtCol = new OleDbDataAdapter(string.Concat("SELECT Max(TakenDate) FROM Daily WHERE [D_Data]='TKN' "), this.conn);
                dataAdapterdltAmtCol.Fill(dataTableAmtCol);
                this.label222.Text = dataTableAmtCol.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void fillGivenData()
        {
            try
            {
                DataTable dataTableGivenAmt = new DataTable();
                OleDbDataAdapter odbcDataAdapterGivenAmt = new OleDbDataAdapter(string.Concat("SELECT InGiven as [ID],Given_To as [Name],Total_Given as [GTK],Given_Date as [GDT] FROM Given WHERE [GDT_V]='NDV' ORDER BY [ID] DESC"), this.conn);
                odbcDataAdapterGivenAmt.Fill(dataTableGivenAmt);
                dataGridView3.DataSource = dataTableGivenAmt.DefaultView;
                DataTable dataTableTekenAmt = new DataTable();
                OleDbDataAdapter odbcDataAdapterTekenAmt = new OleDbDataAdapter(string.Concat("SELECT InTake as [ID],Take_To as [Name],Total_Take as [TTK],Take_Date as [TDT] FROM Teken WHERE [TDT_V]='NDV' ORDER BY [ID] DESC"), this.conn);
                odbcDataAdapterTekenAmt.Fill(dataTableTekenAmt);
                dataGridView7.DataSource = dataTableTekenAmt.DefaultView;
                DataTable dataTableExpenAmt = new DataTable();
                OleDbDataAdapter odbcDataAdapterExpenAmt = new OleDbDataAdapter(string.Concat("SELECT InExpense as [ID],Expense_To as [Name],Expense_Amount as [ETK],Expense_Date as [EDT] FROM Expense WHERE [EDT_V]='NDV' ORDER BY [ID] DESC"), this.conn);
                odbcDataAdapterExpenAmt.Fill(dataTableExpenAmt);
                dataGridView8.DataSource = dataTableExpenAmt.DefaultView;
                DataTable dataTableSaveAmt = new DataTable();
                OleDbDataAdapter odbcDataAdapterSavenAmt = new OleDbDataAdapter(string.Concat("SELECT InSaving as [ID],Saving_To as [Name],Saving_Amount as [STK],Saving_Date as [SDT] FROM Saving WHERE [SDT_V]='NDV' ORDER BY [ID] DESC"), this.conn);
                odbcDataAdapterSavenAmt.Fill(dataTableSaveAmt);
                dataGridView9.DataSource = dataTableSaveAmt.DefaultView;
                DataTable dataTableUntatAmt = new DataTable();
                OleDbDataAdapter odbcDataAdapterUntaAmt = new OleDbDataAdapter(string.Concat("SELECT InUnrated as [ID],Unrated_To as [Name],Unrated_Amount as [UTK],Unrated_Date as [UDT] FROM Unrated WHERE [UDT_V]='NDV' ORDER BY [ID] DESC"), this.conn);
                odbcDataAdapterUntaAmt.Fill(dataTableUntatAmt);
                dataGridView10.DataSource = dataTableUntatAmt.DefaultView;
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
                DataTable dataTableDaiAmt = new DataTable();
                OleDbDataAdapter odbcDataAdapterDaiAmt = new OleDbDataAdapter(string.Concat("SELECT D_ID as [ID],D_Date as [Date],NotTaken FROM Daily WHERE [D_Data]='NTKN' ORDER BY [D_Date] DESC "), this.conn);
                odbcDataAdapterDaiAmt.Fill(dataTableDaiAmt);
                dataGridView5.DataSource = dataTableDaiAmt.DefaultView;
                DataTable dataTableCutAmt = new DataTable();
                OleDbDataAdapter odbcDataAdapterCutAmt = new OleDbDataAdapter(string.Concat("SELECT C_ID as [ID],C_Date as [Date],C_Amount as [Amount] FROM DailyCut ORDER BY [C_Date] DESC "), this.conn);
                odbcDataAdapterCutAmt.Fill(dataTableCutAmt);
                dataGridView4.DataSource = dataTableCutAmt.DefaultView;
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
                DataTable dataTableDaiAmt = new DataTable();
                OleDbDataAdapter odbcDataAdapterDaiAmt = new OleDbDataAdapter(string.Concat("SELECT I_ID as [ID],InsPay_Date as [Date],InsPay as [PayAmt] FROM Installment WHERE Take_Data='INS' ORDER BY [ID] DESC "), this.conn);
                odbcDataAdapterDaiAmt.Fill(dataTableDaiAmt);
                dataGridView2.DataSource = dataTableDaiAmt.DefaultView;
                DataTable dataTableTakeiAmt = new DataTable();
                OleDbDataAdapter odbcDataAdapterTakiAmt = new OleDbDataAdapter(string.Concat("SELECT I_ID as [ID],I_Date as [Date],Take_Total as [Total],Take_Anot as [Anot],Take_Mine as [Mine] FROM Installment WHERE Take_Data='NPD' ORDER BY [ID] DESC "), this.conn);
                odbcDataAdapterTakiAmt.Fill(dataTableTakeiAmt);
                dataGridView6.DataSource = dataTableTakeiAmt.DefaultView;
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
                int num1  = Convert.ToInt32(this.label9.Text.Trim());
                int num2  = Convert.ToInt32(this.label13.Text.Trim());
                int num3  = Convert.ToInt32(this.label17.Text.Trim());
                int num4  = Convert.ToInt32(this.label24.Text.Trim());
                int num5  = Convert.ToInt32(this.label28.Text.Trim());
                int num6  = Convert.ToInt32(this.label32.Text.Trim());
                int num7  = Convert.ToInt32(this.label36.Text.Trim());
                int num8  = Convert.ToInt32(this.label40.Text.Trim());
                int num9  = Convert.ToInt32(this.label44.Text.Trim());
                int num10 = Convert.ToInt32(this.label48.Text.Trim());
                int num11 = Convert.ToInt32(this.label52.Text.Trim());
                int num12 = Convert.ToInt32(this.label56.Text.Trim());
                int num13 = Convert.ToInt32(this.label60.Text.Trim());
                int num14 = Convert.ToInt32(this.label64.Text.Trim());
                int num15 = Convert.ToInt32(this.label68.Text.Trim());
                int num16 = Convert.ToInt32(this.label76.Text.Trim());
                int totalItemSum = num1 + num2 + num3 + num4 + num5 + num6 + num7 + num8 + num9 + num10 + num11 + num12 + num13 + num14 + num15 + num16;
                this.label10.Text = totalItemSum.ToString();
                this.textBox115.Text = this.label10.Text;
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
                int num21 = Convert.ToInt32(this.label179.Text.Trim());
                int num22 = Convert.ToInt32(this.label172.Text.Trim());
                int num23 = Convert.ToInt32(this.label171.Text.Trim());
                int num24 = Convert.ToInt32(this.label170.Text.Trim());
                int num25 = Convert.ToInt32(this.label169.Text.Trim());
                int num26 = Convert.ToInt32(this.label168.Text.Trim());
                int num27 = Convert.ToInt32(this.label167.Text.Trim());
                int num28 = Convert.ToInt32(this.label166.Text.Trim());
                int num29 = Convert.ToInt32(this.label165.Text.Trim());
                int num30 = Convert.ToInt32(this.label164.Text.Trim());
                int num31 = Convert.ToInt32(this.label163.Text.Trim());
                int num32 = Convert.ToInt32(this.label162.Text.Trim());
                int num33 = Convert.ToInt32(this.label161.Text.Trim());
                int num34 = Convert.ToInt32(this.label160.Text.Trim());
                int num35 = Convert.ToInt32(this.label159.Text.Trim());
                int num36 = Convert.ToInt32(this.label158.Text.Trim());
                int num37 = Convert.ToInt32(this.label157.Text.Trim());
                int num38 = Convert.ToInt32(this.label156.Text.Trim());
                int num39 = Convert.ToInt32(this.label155.Text.Trim());
                int num40 = Convert.ToInt32(this.label154.Text.Trim());
                int num41 = Convert.ToInt32(this.label153.Text.Trim());
                int num42 = Convert.ToInt32(this.label152.Text.Trim());
                int num43 = Convert.ToInt32(this.label151.Text.Trim());
                int num44 = Convert.ToInt32(this.label150.Text.Trim());
                int sumNums = num21 + num22 + num23 + num24 + num25 + num26 + num27 + num28 + num29 + num30 + num31 + num32 + num33 + num34 + num35 + num36 + num37 + num38 + num39 + num40 + num41 + num42 + num43 + num44;
                this.textBox90.Text = sumNums.ToString();
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
                DataTable dataTableDailAmt = new DataTable();
                OleDbDataAdapter dataAdapterAmtUnr = new OleDbDataAdapter(string.Concat("SELECT SUM(NotTaken) FROM Daily WHERE [D_Data]='NTKN' "), this.conn);
                dataAdapterAmtUnr.Fill(dataTableDailAmt);
                this.label94.Text = dataTableDailAmt.Rows[0][0].ToString();
                DataTable dataTableDaiylAmt = new DataTable();
                OleDbDataAdapter dataAdapterAmUnr = new OleDbDataAdapter(string.Concat("SELECT SUM(C_Amount) FROM DailyCut"), this.conn);
                dataAdapterAmUnr.Fill(dataTableDaiylAmt);
                this.label121.Text = dataTableDaiylAmt.Rows[0][0].ToString();
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
                DataTable dataTableDailAmt = new DataTable();
                OleDbDataAdapter dataAdapterAmtUnr = new OleDbDataAdapter(string.Concat("SELECT SUM(InsPay) FROM Installment"), this.conn);
                dataAdapterAmtUnr.Fill(dataTableDailAmt);
                this.label211.Text = dataTableDailAmt.Rows[0][0].ToString();
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
                DataTable dataTabledltAmt = new DataTable();
                OleDbDataAdapter odbcDataAdapterdltAmt = new OleDbDataAdapter(string.Concat("SELECT Mem_ID as [ID],Mem_Date as [Date],Giv_TK as [Given],R_InvTK as [Main],C_InvTK as [CAmt],Ret_TK as [Return] FROM MarketMemos ORDER BY Mem_Date DESC"), this.conn);
                odbcDataAdapterdltAmt.Fill(dataTabledltAmt);
                dataGridView11.DataSource = dataTabledltAmt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
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
                try
                {
                    this.conn.Open();
                    OleDbCommand cmd = new OleDbCommand("INSERT INTO Market(M_ID,M_Date,M_Amount,M_Insrt_Person) VALUES('" + this.textBox101.Text.Trim() + "','" + this.dateTimePicker1.Text.Trim() + "','" + this.textBox1.Text.Trim() + "','" + this.label249.Text.Trim() + "')", this.conn);
                    cmd.ExecuteNonQuery();
                    this.conn.Close();
                    MessageBox.Show(string.Concat("Successfull Data Added"));
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
            else if (this.button1.Text == "Updt")
            {
                try
                {
                    this.conn.Open();
                    OleDbCommand command = new OleDbCommand("UPDATE Market SET M_Amount= '" + this.textBox1.Text.Trim() + "',M_Date= '" + this.dateTimePicker1.Text.Trim() + "',M_Updt_Person= '" + this.label249.Text.Trim() + "' WHERE M_ID= '" + this.label6.Text.Trim() + "' ", this.conn);
                    command.ExecuteNonQuery();
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
                    this.conn.Open();
                    OleDbCommand cmd = new OleDbCommand("INSERT INTO Market(M_ID,M_Date,M_Amount,M_Insrt_Person) VALUES('" + this.textBox108.Text.Trim() + "','" + this.dateTimePicker1.Text.Trim() + "','" + this.label10.Text.Trim() + "','" + this.label249.Text.Trim() + "')", this.conn);
                    cmd.ExecuteNonQuery();
                    this.conn.Close();
                    MessageBox.Show(string.Concat("Successfull Memo Amount Added"));
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
                        OleDbCommand cmd = new OleDbCommand("INSERT INTO Given (InGiven,Total_Given,Given_To,ThroughBy,Given_Date,Remarks_Given,GDT_V,G_Insrt_Person) VALUES('" + this.textBox35.Text.Trim() + "','" + this.textBox39.Text.Trim() + "','" + this.textBox33.Text.Trim() + "','" + this.comboBox1.Text.Trim() + "','" + this.dateTimePicker3.Text.Trim() + "','" + this.textBox34.Text.Trim() + "','NDV','" + this.label249.Text.Trim() + "')", this.conn);
                        cmd.ExecuteNonQuery();
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
                        OleDbCommand cmd = new OleDbCommand("INSERT INTO Teken (InTake,Total_Take,Take_To,ThroughBy,Take_Date,Remarks_Take,TDT_V,T_Insrt_Person) VALUES('" + this.textBox35.Text.Trim() + "','" + this.textBox39.Text.Trim() + "','" + this.textBox33.Text.Trim() + "','" + this.comboBox1.Text.Trim() + "','" + this.dateTimePicker3.Text.Trim() + "','" + this.textBox34.Text.Trim() + "','NDV','" + this.label249.Text.Trim() + "')", this.conn);
                        cmd.ExecuteNonQuery();
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
                        OleDbCommand cmd = new OleDbCommand("INSERT INTO Expense (InExpense,Expense_Amount,Expense_To,ThroughBy,Expense_Date,Remarks_Expense,EDT_V,E_Insrt_Person) VALUES('" + this.textBox35.Text.Trim() + "','" + this.textBox39.Text.Trim() + "','" + this.textBox33.Text.Trim() + "','" + this.comboBox1.Text.Trim() + "','" + this.dateTimePicker3.Text.Trim() + "','" + this.textBox34.Text.Trim() + "','NDV','" + this.label249.Text.Trim() + "')", this.conn);
                        cmd.ExecuteNonQuery();
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
                        OleDbCommand cmd = new OleDbCommand("INSERT INTO Saving (InSaving,Saving_Amount,Saving_To,ThroughBy,Saving_Date,Remarks_Saving,SDT_V,Saving_Bank,S_Insrt_Person) VALUES('" + this.textBox35.Text.Trim() + "','" + this.textBox39.Text.Trim() + "','" + this.textBox33.Text.Trim() + "','" + this.comboBox1.Text.Trim() + "','" + this.dateTimePicker3.Text.Trim() + "','" + this.textBox34.Text.Trim() + "','NDV','" + this.comboBox1.Text.Trim() + "','" + this.label249.Text.Trim() + "')", this.conn);
                        cmd.ExecuteNonQuery();
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
                        OleDbCommand cmd = new OleDbCommand("INSERT INTO Unrated (InUnrated,Unrated_Amount,Unrated_To,ThroughBy,Unrated_Date,Remarks_Unrated,UDT_V,U_Insrt_Person) VALUES('" + this.textBox35.Text.Trim() + "','" + this.textBox39.Text.Trim() + "','" + this.textBox33.Text.Trim() + "','" + this.comboBox1.Text.Trim() + "','" + this.dateTimePicker3.Text.Trim() + "','" + this.textBox34.Text.Trim() + "','NDV','" + this.label249.Text.Trim() + "')", this.conn);
                        cmd.ExecuteNonQuery();
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
                OleDbCommand command = new OleDbCommand("UPDATE Given SET Total_Given= '" + this.textBox119.Text.Trim() + "',GDT_V_Date= '" + this.DltDate + "',G_Updt_Person= '" + this.label249.Text.Trim() + "' WHERE InGiven= '" + this.label117.Text.Trim() + "' ", this.conn);
                command.ExecuteNonQuery();
                this.conn.Close();
                MessageBox.Show(string.Concat("Successfull Given TK Update - ", this.label117.Text));
                this.AmtCrDataView();
                this.BalankFld();
                this.fillGivenData();
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
                OleDbCommand command = new OleDbCommand("UPDATE Teken SET Total_Take= '" + this.textBox120.Text.Trim() + "',TDT_V_Date= '" + this.DltDate + "',T_Updt_Person= '" + this.label249.Text.Trim() + "' WHERE InTake= '" + this.label117.Text.Trim() + "' ", this.conn);
                command.ExecuteNonQuery();
                this.conn.Close();
                MessageBox.Show(string.Concat("Successfull Teken TK Update - ", this.label117.Text));
                this.AmtCrDataView();
                this.BalankFld();
                this.fillGivenData();
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
                OleDbCommand command = new OleDbCommand("UPDATE Expense SET Expense_Amount= '" + this.textBox109.Text.Trim() + "',EDT_V_Date= '" + this.DltDate + "',E_Updt_Person= '" + this.label249.Text.Trim() + "' WHERE InExpense= '" + this.label117.Text.Trim() + "' ", this.conn);
                command.ExecuteNonQuery();
                this.conn.Close();
                MessageBox.Show(string.Concat("Successfull Expance TK Update - ", this.label117.Text));
                this.AmtCrDataView();
                this.BalankFld();
                this.fillGivenData();
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
                OleDbCommand command = new OleDbCommand("UPDATE Saving SET Saving_Amount= '" + this.textBox116.Text.Trim() + "',SDT_V_Date= '" + this.DltDate + "',S_Updt_Person= '" + this.label249.Text.Trim() + "' WHERE InSaving= '" + this.label117.Text.Trim() + "' ", this.conn);
                command.ExecuteNonQuery();
                this.conn.Close();
                MessageBox.Show(string.Concat("Successfull Saving TK Update - ", this.label117.Text));
                this.AmtCrDataView();
                this.BalankFld();
                this.fillGivenData();
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
                OleDbCommand command = new OleDbCommand("UPDATE Unrated SET Unrated_Amount= '" + this.textBox117.Text.Trim() + "',UDT_V_Date= '" + this.DltDate + "',U_Updt_Person= '" + this.label249.Text.Trim() + "' WHERE InUnrated= '" + this.label117.Text.Trim() + "' ", this.conn);
                command.ExecuteNonQuery();
                this.conn.Close();
                MessageBox.Show(string.Concat("Successfull Unrated TK Update - ", this.label117.Text));
                this.AmtCrDataView();
                this.BalankFld();
                this.fillGivenData();
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
                    OleDbCommand command = new OleDbCommand("UPDATE Given SET GDT_V='DDV',DDT_V_Date= '" + this.DltDate + "',G_Del_Person= '" + this.label249.Text.Trim() + "' WHERE InGiven= '" + this.label117.Text.Trim() + "' ", this.conn);
                    command.ExecuteNonQuery();
                    this.conn.Close();
                    MessageBox.Show(string.Concat("Successfull Deleted - [", this.label117.Text + "] "));
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
                    OleDbCommand command = new OleDbCommand("UPDATE Teken SET TDT_V='DDV',DDT_V_Date= '" + this.DltDate + "',T_Del_Person= '" + this.label249.Text.Trim() + "' WHERE InTake= '" + this.label117.Text.Trim() + "' ", this.conn);
                    command.ExecuteNonQuery();
                    this.conn.Close();
                    MessageBox.Show(string.Concat("Successfull Deleted - [", this.label117.Text + "] "));
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
                    OleDbCommand command = new OleDbCommand("UPDATE Expense SET EDT_V='DDV',DDT_V_Date= '" + this.DltDate + "',E_Del_Person= '" + this.label249.Text.Trim() + "' WHERE InExpense= '" + this.label117.Text.Trim() + "' ", this.conn);
                    command.ExecuteNonQuery();
                    this.conn.Close();
                    MessageBox.Show(string.Concat("Successfull Deleted - [", this.label117.Text + "] "));
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
                    OleDbCommand command = new OleDbCommand("UPDATE Saving SET SDT_V='DDV',DDT_V_Date= '" + this.DltDate + "',S_Del_Person= '" + this.label249.Text.Trim() + "' WHERE InSaving= '" + this.label117.Text.Trim() + "' ", this.conn);
                    command.ExecuteNonQuery();
                    this.conn.Close();
                    MessageBox.Show(string.Concat("Successfull Deleted - [", this.label117.Text + "] "));
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
                    OleDbCommand command = new OleDbCommand("UPDATE Unrated SET UDT_V='DDV',DDT_V_Date= '" + this.DltDate + "',U_Del_Person= '" + this.label249.Text.Trim() + "' WHERE InUnrated= '" + this.label117.Text.Trim() + "' ", this.conn);
                    command.ExecuteNonQuery();
                    this.conn.Close();
                    MessageBox.Show(string.Concat("Successfull Deleted - [", this.label117.Text + "] "));
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
                try
                {
                    this.conn.Open();
                    OleDbCommand cmd = new OleDbCommand("INSERT INTO Daily(D_ID,D_Date,D_FPAmount,D_SPAmount,NotTaken,D_Data,D_Insrt_Person) VALUES('" + this.textBox92.Text.Trim() + "','" + this.dateTimePicker4.Text.Trim() + "','" + this.textBox37.Text.Trim() + "','" + this.label194.Text.Trim() + "','" + this.label194.Text.Trim() + "','NTKN','" + this.label249.Text.Trim() + "')", this.conn);
                    cmd.ExecuteNonQuery();
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
            else if (this.button10.Text == "Updt")
            {
                try
                {
                    this.conn.Open();
                    OleDbCommand command = new OleDbCommand("UPDATE Daily SET D_FPAmount = '" + this.textBox37.Text.Trim() + "',D_SPAmount = '" + this.label194.Text.Trim() + "',NotTaken = '" + this.label194.Text.Trim() + "',D_Date='" + this.dateTimePicker4.Text.Trim() + "',D_Updt_Person='" + this.label249.Text.Trim() + "' WHERE D_ID= '" + this.label182.Text.Trim() + "' ", this.conn);
                    command.ExecuteNonQuery();
                    this.conn.Close();
                    MessageBox.Show(string.Concat("Successfull Update Daily Gat"));
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
                try
                {
                    this.conn.Open();
                    OleDbCommand cmd = new OleDbCommand("INSERT INTO DailyCut(C_ID,C_Date,C_Amount,C_Insrt_Person) VALUES('" + this.textBox92.Text.Trim() + "','" + this.dateTimePicker5.Text.Trim() + "','" + this.textBox50.Text.Trim() + "','" + this.label249.Text.Trim() + "')", this.conn);
                    cmd.ExecuteNonQuery();
                    this.conn.Close();
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
            else if (this.button14.Text == "Updt")
            {
                try
                {
                    this.conn.Open();
                    OleDbCommand command = new OleDbCommand("UPDATE DailyCut SET C_Amount = '" + this.textBox50.Text.Trim() + "',C_Date='" + this.dateTimePicker5.Text.Trim() + "',C_Updt_Persone='" + this.label249.Text.Trim() + "' WHERE C_ID= '" + this.label182.Text.Trim() + "' ", this.conn);
                    command.ExecuteNonQuery();
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
                OleDbCommand command = new OleDbCommand("UPDATE Daily SET [D_Data]='TKN',[TakenDate]='" + this.DltDate + "',[D_Del_Person]='" + this.label249.Text.Trim() + "' WHERE D_ID= '" + this.label182.Text.Trim() + "' ", this.conn);
                command.ExecuteNonQuery();
                this.conn.Close();
                MessageBox.Show(string.Concat("Successfull Deleted - [", this.label182.Text + "] "));
                this.fillDailyData();
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
        private void button22_Click(object sender, EventArgs e)
        {
            try
            {
                this.conn.Open();
                OleDbCommand commanda = new OleDbCommand("DELETE FROM Daily WHERE D_ID= '" + this.label247.Text.Trim() + "' ", this.conn);
                commanda.ExecuteNonQuery();
                OleDbCommand commandb = new OleDbCommand("DELETE FROM DailyCut WHERE C_ID= '" + this.label248.Text.Trim() + "' ", this.conn);
                commandb.ExecuteNonQuery();
                this.conn.Close();
                MessageBox.Show(string.Concat("Successfull Deleted - [", this.label247.Text + "] & [", this.label248.Text + "] "));
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
                try
                {
                    this.conn.Open();
                    OleDbCommand cmd = new OleDbCommand("INSERT INTO Installment(I_ID,InsPay_Date,InsPay,Take_Data,I_Insrt_Person) VALUES('" + this.textBox98.Text.Trim() + "','" + this.dateTimePicker2.Text.Trim() + "','" + this.textBox32.Text.Trim() + "','INS','" + this.label249.Text.Trim() + "')", this.conn);
                    cmd.ExecuteNonQuery();
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
            else if (this.button4.Text == "Updt")
            {
                try
                {
                    this.conn.Open();
                    OleDbCommand command = new OleDbCommand("UPDATE Installment SET InsPay_Date= '" + this.dateTimePicker2.Text.Trim() + "',I_Updt_Person= '" + this.label249.Text.Trim() + "' WHERE I_ID= '" + this.label201.Text.Trim() + "' ", this.conn);
                    command.ExecuteNonQuery();
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
                try
                {
                    this.conn.Open();
                    OleDbCommand cmd = new OleDbCommand("INSERT INTO Installment(I_ID,I_Date,Take_Total,Take_Anot,Take_Mine,InsPerMonth,PerMonthPay,Take_Data) VALUES('" + this.textBox99.Text.Trim() + "','" + this.DltDate + "','" + this.textBox94.Text.Trim() + "','" + this.textBox96.Text.Trim() + "','" + this.textBox97.Text.Trim() + "','" + this.textBox95.Text.Trim() + "','" + this.label195.Text.Trim() + "','NPD')", this.conn);
                    cmd.ExecuteNonQuery();
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
            else if (this.button13.Text == "Dlt")
            {
                try
                {
                    this.conn.Open();
                    OleDbCommand command = new OleDbCommand("UPDATE Installment SET [Take_Data]='TPD' WHERE I_ID= '" + this.label218.Text.Trim() + "' ", this.conn);
                    command.ExecuteNonQuery();
                    this.conn.Close();
                    MessageBox.Show(string.Concat("Successfull Deleted - [", this.label218.Text + "] "));
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
                    object[] longString = new object[191];
                    longString[0] = "INSERT INTO MarketMemos(Mem_ID,Mem_Date,R_InvTK,C_InvTK,Giv_TK,Ret_TK,I_N01,I_N02,I_N03,I_N04,I_N05,I_N06,I_N07,I_N08,I_N09,I_N10,I_N11,I_N12,I_N13,I_N14,I_N15,I_N16,I_P01,I_P02,I_P03,I_P04,I_P05,I_P06,I_P07,I_P08,I_P09,I_P10,I_P11,I_P12,I_P13,I_P14,I_P15,I_P16,I_Q01,I_Q02,I_Q03,I_Q04,I_Q05,I_Q06,I_Q07,I_Q08,I_Q09,I_Q10,I_Q11,I_Q12,I_Q13,I_Q14,I_Q15,I_Q16,I_ST01,I_ST02,I_ST03,I_ST04,I_ST05,I_ST06,I_ST07,I_ST08,I_ST09,I_ST10,I_ST11,I_ST12,I_ST13,I_ST14,I_ST15,I_ST16,R_Inv01,R_Inv02,R_Inv03,R_Inv04,R_Inv05,R_Inv06,R_Inv07,R_Inv08,R_Inv09,R_Inv10,R_Inv11,R_Inv12,R_Inv13,R_Inv14,R_Inv15,R_Inv16,R_Inv17,R_Inv18,R_Inv19,R_Inv20,R_Inv21,R_Inv22,R_Inv23,R_Inv24,Mem_Insrt_Person) Values('";
                    longString[1] = this.textBox108.Text.Trim();
                    longString[2] = "','";
                    longString[3] = this.DltDate;
                    longString[4] = "','";
                    longString[5] = this.textBox90.Text.Trim();
                    longString[6] = "','";
                    longString[7] = this.label10.Text.Trim();
                    longString[8] = "','";
                    longString[9] = this.textBox55.Text.Trim();
                    longString[10] = "','";
                    longString[11] = this.label147.Text.Trim();
                    longString[12] = "','";
                    longString[13] = this.textBox72.Text.Trim();
                    longString[14] = "','";
                    longString[15] = this.textBox73.Text.Trim();
                    longString[16] = "','";
                    longString[17] = this.textBox78.Text.Trim();
                    longString[18] = "','";
                    longString[19] = this.textBox75.Text.Trim();
                    longString[20] = "','";
                    longString[21] = this.textBox76.Text.Trim();
                    longString[22] = "','";
                    longString[23] = this.textBox77.Text.Trim();
                    longString[24] = "','";
                    longString[25] = this.textBox79.Text.Trim();
                    longString[26] = "','";
                    longString[27] = this.textBox80.Text.Trim();
                    longString[28] = "','";
                    longString[29] = this.textBox81.Text.Trim();
                    longString[30] = "','";
                    longString[31] = this.textBox82.Text.Trim();
                    longString[32] = "','";
                    longString[33] = this.textBox83.Text.Trim();
                    longString[34] = "','";
                    longString[35] = this.textBox84.Text.Trim();
                    longString[36] = "','";
                    longString[37] = this.textBox85.Text.Trim();
                    longString[38] = "','";
                    longString[39] = this.textBox86.Text.Trim();
                    longString[40] = "','";
                    longString[41] = this.textBox87.Text.Trim();
                    longString[42] = "','";
                    longString[43] = this.textBox88.Text.Trim();
                    longString[44] = "','";
                    longString[45] = this.textBox3.Text.Trim();
                    longString[46] = "','";
                    longString[47] = this.textBox5.Text.Trim();
                    longString[48] = "','";
                    longString[49] = this.textBox7.Text.Trim();
                    longString[50] = "','";
                    longString[51] = this.textBox9.Text.Trim();
                    longString[52] = "','";
                    longString[53] = this.textBox11.Text.Trim();
                    longString[54] = "','";
                    longString[55] = this.textBox13.Text.Trim();
                    longString[56] = "','";
                    longString[57] = this.textBox15.Text.Trim();
                    longString[58] = "','";
                    longString[59] = this.textBox17.Text.Trim();
                    longString[60] = "','";
                    longString[61] = this.textBox19.Text.Trim();
                    longString[62] = "','";
                    longString[63] = this.textBox21.Text.Trim();
                    longString[64] = "','";
                    longString[65] = this.textBox23.Text.Trim();
                    longString[66] = "','";
                    longString[67] = this.textBox25.Text.Trim();
                    longString[68] = "','";
                    longString[69] = this.textBox27.Text.Trim();
                    longString[70] = "','";
                    longString[71] = this.textBox29.Text.Trim();
                    longString[72] = "','";
                    longString[73] = this.textBox31.Text.Trim();
                    longString[74] = "','";
                    longString[75] = this.textBox38.Text.Trim();
                    longString[76] = "','";
                    longString[77] = this.textBox2.Text.Trim();
                    longString[78] = "','";
                    longString[79] = this.textBox4.Text.Trim();
                    longString[80] = "','";
                    longString[81] = this.textBox6.Text.Trim();
                    longString[82] = "','";
                    longString[83] = this.textBox8.Text.Trim();
                    longString[84] = "','";
                    longString[85] = this.textBox10.Text.Trim();
                    longString[86] = "','";
                    longString[87] = this.textBox12.Text.Trim();
                    longString[88] = "','";
                    longString[89] = this.textBox14.Text.Trim();
                    longString[90] = "','";
                    longString[91] = this.textBox16.Text.Trim();
                    longString[92] = "','";
                    longString[93] = this.textBox18.Text.Trim();
                    longString[94] = "','";
                    longString[95] = this.textBox20.Text.Trim();
                    longString[96] = "','";
                    longString[97] = this.textBox22.Text.Trim();
                    longString[98] = "','";
                    longString[99] = this.textBox24.Text.Trim();
                    longString[100] = "','";
                    longString[101] = this.textBox26.Text.Trim();
                    longString[102] = "','";
                    longString[103] = this.textBox28.Text.Trim();
                    longString[104] = "','";
                    longString[105] = this.textBox30.Text.Trim();
                    longString[106] = "','";
                    longString[107] = this.textBox54.Text.Trim();
                    longString[108] = "','";
                    longString[109] = this.label9.Text.Trim();
                    longString[110] = "','";
                    longString[111] = this.label13.Text.Trim();
                    longString[112] = "','";
                    longString[113] = this.label17.Text.Trim();
                    longString[114] = "','";
                    longString[115] = this.label24.Text.Trim();
                    longString[116] = "','";
                    longString[117] = this.label28.Text.Trim();
                    longString[118] = "','";
                    longString[119] = this.label32.Text.Trim();
                    longString[120] = "','";
                    longString[121] = this.label36.Text.Trim();
                    longString[122] = "','";
                    longString[123] = this.label40.Text.Trim();
                    longString[124] = "','";
                    longString[125] = this.label44.Text.Trim();
                    longString[126] = "','";
                    longString[127] = this.label48.Text.Trim();
                    longString[128] = "','";
                    longString[129] = this.label52.Text.Trim();
                    longString[130] = "','";
                    longString[131] = this.label56.Text.Trim();
                    longString[132] = "','";
                    longString[133] = this.label60.Text.Trim();
                    longString[134] = "','";
                    longString[135] = this.label64.Text.Trim();
                    longString[136] = "','";
                    longString[137] = this.label68.Text.Trim();
                    longString[138] = "','";
                    longString[139] = this.label76.Text.Trim();
                    longString[140] = "','";
                    longString[141] = this.textBox56.Text.Trim();
                    longString[142] = "','";
                    longString[143] = this.textBox57.Text.Trim();
                    longString[144] = "','";
                    longString[145] = this.textBox58.Text.Trim();
                    longString[146] = "','";
                    longString[147] = this.textBox59.Text.Trim();
                    longString[148] = "','";
                    longString[149] = this.textBox60.Text.Trim();
                    longString[150] = "','";
                    longString[151] = this.textBox61.Text.Trim();
                    longString[152] = "','";
                    longString[153] = this.textBox62.Text.Trim();
                    longString[154] = "','";
                    longString[155] = this.textBox63.Text.Trim();
                    longString[156] = "','";
                    longString[157] = this.textBox64.Text.Trim();
                    longString[158] = "','";
                    longString[159] = this.textBox65.Text.Trim();
                    longString[160] = "','";
                    longString[161] = this.textBox66.Text.Trim();
                    longString[162] = "','";
                    longString[163] = this.textBox67.Text.Trim();
                    longString[164] = "','";
                    longString[165] = this.textBox68.Text.Trim();
                    longString[166] = "','";
                    longString[167] = this.textBox69.Text.Trim();
                    longString[168] = "','";
                    longString[169] = this.textBox70.Text.Trim();
                    longString[170] = "','";
                    longString[171] = this.textBox71.Text.Trim();
                    longString[172] = "','";
                    longString[173] = this.textBox89.Text.Trim();
                    longString[174] = "','";
                    longString[175] = this.textBox91.Text.Trim();
                    longString[176] = "','";
                    longString[177] = this.textBox110.Text.Trim();
                    longString[178] = "','";
                    longString[179] = this.textBox111.Text.Trim();
                    longString[180] = "','";
                    longString[181] = this.textBox112.Text.Trim();
                    longString[182] = "','";
                    longString[183] = this.textBox113.Text.Trim();
                    longString[184] = "','";
                    longString[185] = this.textBox114.Text.Trim();
                    longString[186] = "','";
                    longString[187] = this.textBox115.Text.Trim();
                    longString[188] = "','";
                    longString[189] = this.label249.Text.Trim();
                    longString[190] = "')";
                    OleDbCommand cmd = new OleDbCommand(string.Concat(longString), this.conn);
                    cmd.ExecuteNonQuery();
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
                    OleDbCommand command = new OleDbCommand("UPDATE MarketMemos SET R_InvTK= '" + this.textBox90.Text.Trim() + "',C_InvTK= '" + this.label10.Text.Trim() + "',Giv_TK= '" + this.textBox55.Text.Trim() + "',Ret_TK= '" + this.label147.Text.Trim() + "',I_N01= '" + this.textBox72.Text.Trim() + "',I_N02= '" + this.textBox73.Text.Trim() + "',I_N03= '" + this.textBox78.Text.Trim() + "',I_N04= '" + this.textBox75.Text.Trim() + "',I_N05= '" + this.textBox76.Text.Trim() + "',I_N06= '" + this.textBox77.Text.Trim() + "',I_N07= '" + this.textBox79.Text.Trim() + "',I_N08= '" + this.textBox80.Text.Trim() + "',I_N09= '" + this.textBox81.Text.Trim() + "',I_N10= '" + this.textBox82.Text.Trim() + "',I_N11= '" + this.textBox83.Text.Trim() + "',I_N12= '" + this.textBox84.Text.Trim() + "',I_N13= '" + this.textBox85.Text.Trim() + "',I_N14= '" + this.textBox86.Text.Trim() + "',I_N15= '" + this.textBox87.Text.Trim() + "',I_N16= '" + this.textBox88.Text.Trim() + "',I_P01= '" + this.textBox3.Text.Trim() + "',I_P02= '" + this.textBox5.Text.Trim() + "',I_P03= '" + this.textBox7.Text.Trim() + "',I_P04= '" + this.textBox9.Text.Trim() + "',I_P05= '" + this.textBox11.Text.Trim() + "',I_P06= '" + this.textBox13.Text.Trim() + "',I_P07= '" + this.textBox15.Text.Trim() + "',I_P08= '" + this.textBox17.Text.Trim() + "',I_P09= '" + this.textBox19.Text.Trim() + "',I_P10= '" + this.textBox21.Text.Trim() + "',I_P11= '" + this.textBox23.Text.Trim() + "',I_P12= '" + this.textBox25.Text.Trim() + "',I_P13= '" + this.textBox27.Text.Trim() + "',I_P14= '" + this.textBox29.Text.Trim() + "',I_P15= '" + this.textBox31.Text.Trim() + "',I_P16= '" + this.textBox38.Text.Trim() + "',I_Q01= '" + this.textBox2.Text.Trim() + "',I_Q02= '" + this.textBox4.Text.Trim() + "',I_Q03= '" + this.textBox6.Text.Trim() + "',I_Q04= '" + this.textBox8.Text.Trim() + "',I_Q05= '" + this.textBox10.Text.Trim() + "',I_Q06= '" + this.textBox12.Text.Trim() + "',I_Q07= '" + this.textBox14.Text.Trim() + "',I_Q08= '" + this.textBox16.Text.Trim() + "',I_Q09= '" + this.textBox18.Text.Trim() + "',I_Q10= '" + this.textBox20.Text.Trim() + "',I_Q11= '" + this.textBox22.Text.Trim() + "',I_Q12= '" + this.textBox24.Text.Trim() + "',I_Q13= '" + this.textBox26.Text.Trim() + "',I_Q14= '" + this.textBox28.Text.Trim() + "',I_Q15= '" + this.textBox30.Text.Trim() + "',I_Q16= '" + this.textBox54.Text.Trim() + "',I_ST01= '" + this.label9.Text.Trim() + "',I_ST02= '" + this.label13.Text.Trim() + "',I_ST03= '" + this.label17.Text.Trim() + "',I_ST04= '" + this.label24.Text.Trim() + "',I_ST05= '" + this.label28.Text.Trim() + "',I_ST06= '" + this.label32.Text.Trim() + "',I_ST07= '" + this.label36.Text.Trim() + "',I_ST08= '" + this.label40.Text.Trim() + "',I_ST09= '" + this.label44.Text.Trim() + "',I_ST10= '" + this.label48.Text.Trim() + "',I_ST11= '" + this.label52.Text.Trim() + "',I_ST12= '" + this.label56.Text.Trim() + "',I_ST13= '" + this.label60.Text.Trim() + "',I_ST14= '" + this.label64.Text.Trim() + "',I_ST15= '" + this.label68.Text.Trim() + "',I_ST16= '" + this.label76.Text.Trim() + "',R_Inv01= '" + this.textBox56.Text.Trim() + "',R_Inv02= '" + this.textBox57.Text.Trim() + "',R_Inv03= '" + this.textBox58.Text.Trim() + "',R_Inv04= '" + this.textBox59.Text.Trim() + "',R_Inv05= '" + this.textBox60.Text.Trim() + "',R_Inv06= '" + this.textBox61.Text.Trim() + "',R_Inv07= '" + this.textBox62.Text.Trim() + "',R_Inv08= '" + this.textBox63.Text.Trim() + "',R_Inv09= '" + this.textBox64.Text.Trim() + "',R_Inv10= '" + this.textBox65.Text.Trim() + "',R_Inv11= '" + this.textBox66.Text.Trim() + "',R_Inv12= '" + this.textBox67.Text.Trim() + "',R_Inv13= '" + this.textBox68.Text.Trim() + "',R_Inv14= '" + this.textBox69.Text.Trim() + "',R_Inv15= '" + this.textBox70.Text.Trim() + "',R_Inv16= '" + this.textBox71.Text.Trim() + "',R_Inv17= '" + this.textBox89.Text.Trim() + "',R_Inv18= '" + this.textBox91.Text.Trim() + "',R_Inv19= '" + this.textBox110.Text.Trim() + "',R_Inv20= '" + this.textBox111.Text.Trim() + "',R_Inv21= '" + this.textBox112.Text.Trim() + "',R_Inv22= '" + this.textBox113.Text.Trim() + "',R_Inv23= '" + this.textBox114.Text.Trim() + "',R_Inv24= '" + this.textBox115.Text.Trim() + "',Mem_Updt_Person= '" + this.label249.Text.Trim() + "' WHERE Mem_ID = '" + this.label224.Text.Trim() + "' ", this.conn);
                    command.ExecuteNonQuery();
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
                OleDbCommand commandUpdtPerson = new OleDbCommand("UPDATE MarketMemos SET Mem_Del_Person= '" + this.label249.Text.Trim() + "' WHERE Mem_ID = '" + this.label224.Text.Trim() + "' ", this.conn);
                commandUpdtPerson.ExecuteNonQuery();
                OleDbCommand sendData = new OleDbCommand(string.Concat("INSERT INTO MarketMemosDel SELECT * FROM MarketMemos WHERE Mem_ID = '" + this.label224.Text.Trim() + "' "), this.conn);
                sendData.ExecuteNonQuery();
                OleDbCommand sendDData = new OleDbCommand(string.Concat("DELETE FROM MarketMemos WHERE Mem_ID = '" + this.label224.Text.Trim() + "' "), this.conn);
                sendDData.ExecuteNonQuery();
                this.conn.Close();
                MessageBox.Show(string.Concat("Successfull Deleted - [", this.label224.Text + "] "));
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
            try
            {
                this.conn.Open();
                OleDbCommand cmd = new OleDbCommand("INSERT INTO BikeInfo(B_ID,B_Chng_Date,B_KM_ODO,B_Mobile_Go,B_Next_ODO,B_Insrt_Person) VALUES('" + this.textBox98.Text.Trim() + "','" + this.dateTimePicker6.Text.Trim() + "','" + this.textBox129.Text.Trim() + "','" + this.textBox128.Text.Trim() + "','" + this.label257.Text.Trim() + "','" + this.label249.Text.Trim() + "')", this.conn);
                cmd.ExecuteNonQuery();
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
                DataTable dataTabledt = new DataTable();
                OleDbDataAdapter oleDbDatadt = new OleDbDataAdapter(String.Concat("SELECT M_ID,M_Amount FROM Market WHERE M_ID='", this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), "' "), this.conn);
                oleDbDatadt.Fill(dataTabledt);
                this.label6.Text = dataTabledt.Rows[0][0].ToString();
                this.textBox1.Text = dataTabledt.Rows[0][1].ToString();
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
                this.button4.Text = "Updt";
                DataTable dataTable = new DataTable();
                OleDbDataAdapter oleDbData = new OleDbDataAdapter(String.Concat("SELECT I_ID,InsPay FROM Installment WHERE I_ID='", this.dataGridView2.SelectedRows[0].Cells[0].Value.ToString(), "' "), this.conn);
                oleDbData.Fill(dataTable);
                this.label201.Text = dataTable.Rows[0][0].ToString();
                this.label212.Text = dataTable.Rows[0][1].ToString();
                this.textBox32.Text = dataTable.Rows[0][1].ToString();
                this.textBox32.ReadOnly = false;
                this.textBox32.Focus();
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
                DataTable dataTable = new DataTable();
                OleDbDataAdapter oleDbData = new OleDbDataAdapter(String.Concat("SELECT * FROM Given WHERE InGiven='", this.dataGridView3.SelectedRows[0].Cells[0].Value.ToString(), "' "), this.conn);
                oleDbData.Fill(dataTable);
                this.label102.Text   = dataTable.Rows[0][0].ToString();
                this.label117.Text   = dataTable.Rows[0][1].ToString();
                this.textBox40.Text  = dataTable.Rows[0][2].ToString();
                this.label111.Text   = dataTable.Rows[0][2].ToString();
                this.textBox36.Text  = dataTable.Rows[0][3].ToString();
                this.label113.Text   = dataTable.Rows[0][4].ToString();
                this.textBox41.Text  = dataTable.Rows[0][5].ToString();
                this.textBox42.Text  = dataTable.Rows[0][6].ToString();
                this.textBox118.Text = dataTable.Rows[0][8].ToString();
                this.conn.Close();
                this.textBox119.Text = this.label111.Text.Trim();
                this.button7.Visible = true;
                this.button7.Text = "Delete G.";
                this.textBox119.Focus();
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
                DataTable dataTable = new DataTable();
                OleDbDataAdapter oleDbData = new OleDbDataAdapter(String.Concat("SELECT D_ID,D_FPAmount,D_SPAmount,D_Data,NotTaken FROM Daily WHERE D_ID='", this.dataGridView5.SelectedRows[0].Cells[0].Value.ToString(), "' "), this.conn);
                oleDbData.Fill(dataTable);
                this.label182.Text = dataTable.Rows[0][0].ToString();
                this.label247.Text = dataTable.Rows[0][0].ToString();
                this.label185.Text = dataTable.Rows[0][1].ToString();
                this.label187.Text = dataTable.Rows[0][2].ToString();
                this.label189.Text = dataTable.Rows[0][3].ToString();
                this.textBox37.Text = dataTable.Rows[0][4].ToString();
                this.textBox37.ReadOnly = false;
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
                this.button22.Visible = true;
                this.button14.Text = "Updt";
                DataTable dataTable = new DataTable();
                OleDbDataAdapter oleDbData = new OleDbDataAdapter(String.Concat("SELECT C_ID,C_Amount FROM DailyCut WHERE C_ID='", this.dataGridView4.SelectedRows[0].Cells[0].Value.ToString(), "' "), this.conn);
                oleDbData.Fill(dataTable);
                this.label182.Text = dataTable.Rows[0][0].ToString();
                this.label248.Text = dataTable.Rows[0][0].ToString();
                this.label191.Text = dataTable.Rows[0][1].ToString();
                this.textBox50.Text = dataTable.Rows[0][1].ToString();
                this.textBox50.ReadOnly = false;
                this.textBox50.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }

        }
        private void dataGridView6_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.button13.Text = "Dlt";
                DataTable dataTable = new DataTable();
                OleDbDataAdapter oleDbData = new OleDbDataAdapter(String.Concat("SELECT I_ID,Take_Anot,Take_Mine FROM Installment WHERE I_ID='", this.dataGridView6.SelectedRows[0].Cells[0].Value.ToString(), "' "), this.conn);
                oleDbData.Fill(dataTable);
                this.label218.Text = dataTable.Rows[0][0].ToString();
                this.label199.Text = dataTable.Rows[0][1].ToString();
                this.label198.Text = dataTable.Rows[0][2].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void dataGridView7_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.conn.Open();
                DataTable dataTable = new DataTable();
                OleDbDataAdapter oleDbData = new OleDbDataAdapter(String.Concat("SELECT * FROM Teken WHERE InTake='", this.dataGridView7.SelectedRows[0].Cells[0].Value.ToString(), "' "), this.conn);
                oleDbData.Fill(dataTable);
                this.label102.Text   = dataTable.Rows[0][0].ToString();
                this.label117.Text   = dataTable.Rows[0][1].ToString();
                this.textBox45.Text  = dataTable.Rows[0][2].ToString();
                this.label111.Text   = dataTable.Rows[0][2].ToString();
                this.textBox44.Text  = dataTable.Rows[0][3].ToString();
                this.label113.Text   = dataTable.Rows[0][4].ToString();
                this.textBox46.Text  = dataTable.Rows[0][5].ToString();
                this.textBox47.Text  = dataTable.Rows[0][6].ToString();
                this.textBox121.Text = dataTable.Rows[0][8].ToString();
                this.conn.Close();
                this.textBox120.Text = this.label111.Text.Trim();
                this.button7.Visible = true;
                this.button7.Text = "Delete T.";
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
                DataTable dataTable = new DataTable();
                OleDbDataAdapter oleDbData = new OleDbDataAdapter(String.Concat("SELECT * FROM Expense WHERE InExpense='", this.dataGridView8.SelectedRows[0].Cells[0].Value.ToString(), "' "), this.conn);
                oleDbData.Fill(dataTable);
                this.label102.Text   = dataTable.Rows[0][0].ToString();
                this.label117.Text   = dataTable.Rows[0][1].ToString();
                this.textBox103.Text = dataTable.Rows[0][2].ToString();
                this.label111.Text   = dataTable.Rows[0][2].ToString();
                this.textBox104.Text = dataTable.Rows[0][3].ToString();
                this.label113.Text   = dataTable.Rows[0][4].ToString();
                this.textBox93.Text  = dataTable.Rows[0][5].ToString();
                this.textBox102.Text = dataTable.Rows[0][6].ToString();
                this.textBox127.Text = dataTable.Rows[0][8].ToString();
                this.conn.Close();
                this.textBox109.Text = this.label111.Text.Trim();
                this.button7.Visible = true;
                this.button7.Text = "Delete E.";
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
                DataTable dataTable = new DataTable();
                OleDbDataAdapter oleDbData = new OleDbDataAdapter(String.Concat("SELECT * FROM Saving WHERE InSaving='", this.dataGridView9.SelectedRows[0].Cells[0].Value.ToString(), "' "), this.conn);
                oleDbData.Fill(dataTable);
                this.label102.Text   = dataTable.Rows[0][0].ToString();
                this.label117.Text   = dataTable.Rows[0][1].ToString();
                this.textBox43.Text  = dataTable.Rows[0][2].ToString();
                this.label111.Text   = dataTable.Rows[0][2].ToString();
                this.textBox105.Text = dataTable.Rows[0][3].ToString();
                this.label113.Text   = dataTable.Rows[0][4].ToString();
                this.textBox48.Text  = dataTable.Rows[0][5].ToString();
                this.textBox49.Text  = dataTable.Rows[0][6].ToString();
                this.textBox112.Text = dataTable.Rows[0][8].ToString();
                this.label243.Text = dataTable.Rows[0][9].ToString();
                this.conn.Close();
                this.textBox109.Text = this.label111.Text.Trim();
                this.button7.Visible = true;
                this.button7.Text = "Delete S.";
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
                DataTable dataTable = new DataTable();
                OleDbDataAdapter oleDbData = new OleDbDataAdapter(String.Concat("SELECT * FROM Unrated WHERE InUnrated='", this.dataGridView10.SelectedRows[0].Cells[0].Value.ToString(), "' "), this.conn);
                oleDbData.Fill(dataTable);
                this.label102.Text = dataTable.Rows[0][0].ToString(); 
                this.label117.Text = dataTable.Rows[0][1].ToString();
                this.textBox51.Text = dataTable.Rows[0][2].ToString();
                this.label111.Text = dataTable.Rows[0][2].ToString();
                this.textBox106.Text = dataTable.Rows[0][3].ToString();
                this.label113.Text = dataTable.Rows[0][4].ToString();
                this.textBox52.Text = dataTable.Rows[0][5].ToString(); 
                this.textBox53.Text = dataTable.Rows[0][6].ToString();
                this.textBox123.Text = dataTable.Rows[0][8].ToString();
                this.conn.Close();
                this.textBox117.Text = this.label111.Text.Trim();
                this.button7.Visible = true;
                this.button7.Text = "Delete U.";
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
                DataTable dataTable = new DataTable();
                OleDbDataAdapter oleDbData = new OleDbDataAdapter(String.Concat("SELECT * FROM MarketMemos WHERE Mem_ID='", this.dataGridView11.SelectedRows[0].Cells[0].Value.ToString(), "' "), this.conn);
                oleDbData.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {                                                           
                    this.label224.Text = dataTable.Rows[0][0].ToString();
                    this.textBox90.Text = dataTable.Rows[0][2].ToString();
                    this.label10.Text = dataTable.Rows[0][3].ToString();
                    this.textBox55.Text = dataTable.Rows[0][4].ToString();
                    this.label147.Text = dataTable.Rows[0][5].ToString();  
                    this.textBox72.Text = dataTable.Rows[0][6].ToString();
                    this.textBox73.Text = dataTable.Rows[0][7].ToString();
                    this.textBox78.Text = dataTable.Rows[0][8].ToString();
                    this.textBox75.Text = dataTable.Rows[0][9].ToString();
                    this.textBox76.Text = dataTable.Rows[0][10].ToString();
                    this.textBox77.Text = dataTable.Rows[0][11].ToString();
                    this.textBox79.Text = dataTable.Rows[0][12].ToString();
                    this.textBox80.Text = dataTable.Rows[0][13].ToString();
                    this.textBox81.Text = dataTable.Rows[0][14].ToString();
                    this.textBox82.Text = dataTable.Rows[0][15].ToString();
                    this.textBox83.Text = dataTable.Rows[0][16].ToString();
                    this.textBox84.Text = dataTable.Rows[0][17].ToString();
                    this.textBox85.Text = dataTable.Rows[0][18].ToString();
                    this.textBox86.Text = dataTable.Rows[0][19].ToString();
                    this.textBox87.Text = dataTable.Rows[0][20].ToString();
                    this.textBox88.Text = dataTable.Rows[0][21].ToString();
                    this.textBox3.Text = dataTable.Rows[0][22].ToString();
                    this.textBox5.Text = dataTable.Rows[0][23].ToString();
                    this.textBox7.Text = dataTable.Rows[0][24].ToString();
                    this.textBox9.Text = dataTable.Rows[0][25].ToString();
                    this.textBox11.Text = dataTable.Rows[0][26].ToString();
                    this.textBox13.Text = dataTable.Rows[0][27].ToString();
                    this.textBox15.Text = dataTable.Rows[0][28].ToString();
                    this.textBox17.Text = dataTable.Rows[0][29].ToString();
                    this.textBox19.Text = dataTable.Rows[0][30].ToString();
                    this.textBox21.Text = dataTable.Rows[0][31].ToString();
                    this.textBox23.Text = dataTable.Rows[0][32].ToString();
                    this.textBox25.Text = dataTable.Rows[0][33].ToString();
                    this.textBox27.Text = dataTable.Rows[0][34].ToString();
                    this.textBox29.Text = dataTable.Rows[0][35].ToString();
                    this.textBox31.Text = dataTable.Rows[0][36].ToString();
                    this.textBox38.Text = dataTable.Rows[0][37].ToString();
                    this.textBox2.Text = dataTable.Rows[0][38].ToString();
                    this.textBox4.Text = dataTable.Rows[0][39].ToString();
                    this.textBox6.Text = dataTable.Rows[0][40].ToString();
                    this.textBox8.Text = dataTable.Rows[0][41].ToString();
                    this.textBox10.Text = dataTable.Rows[0][42].ToString();
                    this.textBox12.Text = dataTable.Rows[0][43].ToString();
                    this.textBox14.Text = dataTable.Rows[0][44].ToString();
                    this.textBox16.Text = dataTable.Rows[0][45].ToString();
                    this.textBox18.Text = dataTable.Rows[0][46].ToString();
                    this.textBox20.Text = dataTable.Rows[0][47].ToString();
                    this.textBox22.Text = dataTable.Rows[0][48].ToString();
                    this.textBox24.Text = dataTable.Rows[0][49].ToString();
                    this.textBox26.Text = dataTable.Rows[0][50].ToString();
                    this.textBox28.Text = dataTable.Rows[0][51].ToString();
                    this.textBox30.Text = dataTable.Rows[0][52].ToString();
                    this.textBox54.Text = dataTable.Rows[0][53].ToString();
                    this.label9.Text = dataTable.Rows[0][54].ToString();
                    this.label13.Text = dataTable.Rows[0][55].ToString();
                    this.label17.Text = dataTable.Rows[0][56].ToString();
                    this.label24.Text = dataTable.Rows[0][57].ToString();
                    this.label28.Text = dataTable.Rows[0][58].ToString();
                    this.label32.Text = dataTable.Rows[0][59].ToString();
                    this.label36.Text = dataTable.Rows[0][60].ToString();
                    this.label40.Text = dataTable.Rows[0][61].ToString();
                    this.label44.Text = dataTable.Rows[0][62].ToString();
                    this.label48.Text = dataTable.Rows[0][63].ToString();
                    this.label52.Text = dataTable.Rows[0][64].ToString();
                    this.label56.Text = dataTable.Rows[0][65].ToString();
                    this.label60.Text = dataTable.Rows[0][66].ToString();
                    this.label64.Text = dataTable.Rows[0][67].ToString();
                    this.label68.Text = dataTable.Rows[0][68].ToString();
                    this.label76.Text = dataTable.Rows[0][69].ToString();
                    this.textBox56.Text = dataTable.Rows[0][70].ToString();
                    this.textBox57.Text = dataTable.Rows[0][71].ToString();
                    this.textBox58.Text = dataTable.Rows[0][72].ToString();
                    this.textBox59.Text = dataTable.Rows[0][73].ToString();
                    this.textBox60.Text = dataTable.Rows[0][74].ToString();
                    this.textBox61.Text = dataTable.Rows[0][75].ToString();
                    this.textBox62.Text = dataTable.Rows[0][76].ToString();
                    this.textBox63.Text = dataTable.Rows[0][77].ToString();
                    this.textBox64.Text = dataTable.Rows[0][78].ToString();
                    this.textBox65.Text = dataTable.Rows[0][79].ToString();
                    this.textBox66.Text = dataTable.Rows[0][80].ToString();
                    this.textBox67.Text = dataTable.Rows[0][81].ToString();
                    this.textBox68.Text = dataTable.Rows[0][82].ToString();
                    this.textBox69.Text = dataTable.Rows[0][83].ToString();
                    this.textBox70.Text = dataTable.Rows[0][84].ToString();
                    this.textBox71.Text = dataTable.Rows[0][85].ToString();
                    this.textBox89.Text = dataTable.Rows[0][86].ToString();
                    this.textBox91.Text = dataTable.Rows[0][87].ToString();
                    this.textBox110.Text = dataTable.Rows[0][88].ToString();
                    this.textBox111.Text = dataTable.Rows[0][89].ToString();
                    this.textBox112.Text = dataTable.Rows[0][90].ToString();
                    this.textBox113.Text = dataTable.Rows[0][91].ToString();
                    this.textBox114.Text = dataTable.Rows[0][92].ToString();
                    this.textBox115.Text = dataTable.Rows[0][93].ToString();
                    this.conn.Close();
                    this.button15.Text = "Update";
                    this.button21.Visible = true;
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
                DataTable dataTable = new DataTable();
                OleDbDataAdapter oleDbData = new OleDbDataAdapter(String.Concat("SELECT B_Next_ODO FROM BikeInfo WHERE B_ID='", this.dataGridView12.SelectedRows[0].Cells[2].Value.ToString(), "' "), this.conn);
                oleDbData.Fill(dataTable);
                this.textBox129.Text = dataTable.Rows[0][0].ToString();
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
                DataTable dataTable = new DataTable();
                string[] strArrays = new string[] { "SELECT SUM(Total_Given) as Total,Given_To FROM Given where Given_To like '%" + this.textBox107.Text.Trim() + "%' Group By Given_To" };
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(string.Concat(strArrays), this.conn);
                dataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0 && this.textBox107.Text.Trim() != "")
                {
                    this.label231.Text = dataTable.Rows[0][0].ToString();
                }
                else
                {
                    this.label231.Text = "";
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
                DataTable dataTable = new DataTable();
                string[] strArrays = new string[] { "SELECT SUM(Total_Take) as Total,Take_To FROM Teken where Take_To like '%" + this.textBox124.Text.Trim() + "%' Group By Take_To" };
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(string.Concat(strArrays), this.conn);
                dataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0 && this.textBox124.Text.Trim() != "")
                {
                    this.label233.Text = dataTable.Rows[0][0].ToString();
                }
                else
                {
                    this.label233.Text = "";
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
                DataTable dataTable = new DataTable();
                string[] strArrays = new string[] { "SELECT SUM(Saving_Amount) as Total,Saving_To FROM Saving where Saving_To like '%" + this.textBox125.Text.Trim() + "%' Group By Saving_To" };
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(string.Concat(strArrays), this.conn);
                dataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0 && this.textBox125.Text.Trim() != "")
                {
                    this.label235.Text = dataTable.Rows[0][0].ToString();
                }
                else
                {
                    this.label235.Text = "";
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
                DataTable dataTable = new DataTable();
                string[] strArrays = new string[] { "SELECT SUM(Unrated_Amount) as Total,Unrated_To FROM Unrated where Unrated_To like '%" + this.textBox126.Text.Trim() + "%' Group By Unrated_To" };
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(string.Concat(strArrays), this.conn);
                dataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0 && this.textBox126.Text.Trim() != "")
                {
                    this.label237.Text = dataTable.Rows[0][0].ToString();
                }
                else
                {
                    this.label237.Text = "";
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
                    this.textBox34.Text = "Through By Bkash";
                    this.button6.Focus();
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
                    if (!(this.textBox18.Text.Trim() != ""))
                    {
                        this.textBox18.Focus();
                    }
                    else
                    {
                        int num1 = Convert.ToInt32(this.textBox19.Text.Trim());
                        int num2 = Convert.ToInt32(this.textBox18.Text.Trim());
                        int num3 = num1 * num2;
                        this.AllItemAdd();
                        this.textBox82.Focus();
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

        #endregion

        //-----------------------------------------------------------------------
        //------------------------------If Query Needed--------------------------
        //-----------------------------------------------------------------------
    }
}
