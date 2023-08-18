using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
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
            this.DltDate = DateTime.Now.ToString("MM/dd/yyyy");
            this.textBox1.ReadOnly = true;
            this.button1.Text = "Add";
            this.dateTimePicker1.Visible = false;
            this.fillData();
            this.AmtDataView();
            this.fillGivenData();
            this.AmtCrDataView();
            this.textBox39.ReadOnly = true;
            this.textBox33.ReadOnly = true;
            this.comboBox1.Enabled = false;
            this.dateTimePicker3.Enabled = false;
            this.textBox34.ReadOnly = true;
            this.checkBox1.Enabled = false;
            this.radioButton1.Enabled = false;
            this.radioButton2.Enabled = false;
            this.button7.Visible = false;
        }


        //-----------------------------------------------------------------------
        //------------------------------All Classes------------------------------
        //-----------------------------------------------------------------------
        private void fillData()
        {
            try
            {
                DataTable dataTabledltAmt = new DataTable();
                OleDbDataAdapter odbcDataAdapterdltAmt = new OleDbDataAdapter(string.Concat("SELECT ID,M_Date as [Date],Amount as [Amount] FROM Market ORDER BY [ID] DESC "), this.conn);
                odbcDataAdapterdltAmt.Fill(dataTabledltAmt);
                dataGridView1.DataSource = dataTabledltAmt.DefaultView;
            }
            catch (Exception)
            {
            }
        }
        private void AmtDataView()
        {
            try
            {
                DataTable dataTableAmt = new DataTable();
                OleDbDataAdapter dataAdapterdltAmtT = new OleDbDataAdapter(string.Concat(" SELECT SUM(Amount) FROM Market "), this.conn);
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
            catch (Exception)
            {
            }
        }
        private void AmtCrDataView()
        {
            try
            {
                DataTable dataTableAmtGiven = new DataTable();
                OleDbDataAdapter dataAdapterdltAmtG = new OleDbDataAdapter(string.Concat(" SELECT SUM(Total_Given) FROM Credit WHERE [DT_V]='NDV' "), this.conn);
                dataAdapterdltAmtG.Fill(dataTableAmtGiven);
                this.label87.Text = dataTableAmtGiven.Rows[0][0].ToString();

                DataTable dataTableAmtTake = new DataTable();
                OleDbDataAdapter dataAdapterdltAmtT = new OleDbDataAdapter(string.Concat("SELECT SUM(Total_Take) FROM Credit WHERE [DT_V]='NDV' "), this.conn);
                dataAdapterdltAmtT.Fill(dataTableAmtTake);
                this.label92.Text = dataTableAmtTake.Rows[0][0].ToString();

                DataTable dataTableAmtExp = new DataTable();
                OleDbDataAdapter dataAdapterdltAmtE = new OleDbDataAdapter(string.Concat("SELECT SUM(Amount) FROM Credit WHERE [DT_V]='NDV' "), this.conn);
                dataAdapterdltAmtE.Fill(dataTableAmtExp);
                this.label90.Text = dataTableAmtExp.Rows[0][0].ToString();

                DataTable dataTableAmtSev = new DataTable();
                OleDbDataAdapter dataAdapterdltSev = new OleDbDataAdapter(string.Concat("SELECT SUM(Saving_Amount) FROM Credit WHERE [DT_V]='NDV' "), this.conn);
                dataAdapterdltSev.Fill(dataTableAmtSev);
                this.label114.Text = dataTableAmtSev.Rows[0][0].ToString();

                DataTable dataTableAmtUnr = new DataTable();
                OleDbDataAdapter dataAdapterdltAmtUnr = new OleDbDataAdapter(string.Concat("SELECT SUM(Unrated_Amount) FROM Credit WHERE [DT_V]='NDV' "), this.conn);
                dataAdapterdltAmtUnr.Fill(dataTableAmtUnr);
                this.label116.Text = dataTableAmtUnr.Rows[0][0].ToString();

            }
            catch (Exception)
            {
            }
        }
        private void fillGivenData()
        {
            try
            {
                DataTable dataTableGivenAmt = new DataTable();
                OleDbDataAdapter odbcDataAdapterGivenAmt = new OleDbDataAdapter(string.Concat("SELECT ID,Amount,Given_To as [GName],Total_Given as [GTK],Given_Date as [GDate],Take_To as [TName],Total_Take as [TTK],Take_Date as [TDate],ThroughBy as [Through],Saving_Amount as [STK],Unrated_Amount as [UTK] FROM Credit WHERE [DT_V]='NDV' ORDER BY [ID] DESC "), this.conn);
                odbcDataAdapterGivenAmt.Fill(dataTableGivenAmt);
                dataGridView3.DataSource = dataTableGivenAmt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BalankFld()
        {
            this.label117.Text = "";
            this.label102.Text = "";
            this.textBox36.Text = "";
            this.textBox40.Text = "";
            this.textBox41.Text = "";
            this.textBox42.Text = "";
            this.textBox44.Text = "";
            this.textBox45.Text = "";
            this.textBox46.Text = "";
            this.textBox47.Text = "";
            this.label113.Text = "";
            this.label111.Text = "";
            this.label108.Text = "";
        }
       

        //---------------------------------------------------------------------------
        //------------------------------All Button Work------------------------------
        //---------------------------------------------------------------------------
        private void button2_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            this.button1.Text = "Add";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.button1.Text == "Add")
            {
                this.textBox1.ReadOnly = false;
                this.textBox1.Focus();
                this.button1.Text = "Save";
            }
            else if (this.button1.Text == "Updt")
            {
                try
                {
                    this.conn.Open();
                    OleDbCommand command = new OleDbCommand("UPDATE Market SET Amount= '" + this.textBox1.Text.Trim() + "',M_Date= '" + this.dateTimePicker1.Text.Trim() + "' WHERE ID= " + this.label6.Text.Trim() + " ", this.conn);
                    command.ExecuteNonQuery();
                    this.conn.Close();
                    MessageBox.Show(string.Concat("Successfull Update - ", this.label6.Text));
                    this.fillData();
                    this.AmtDataView();
                    this.textBox1.ReadOnly = true;
                    this.textBox1.Text = "";
                    this.label6.Text = "";
                    this.dateTimePicker1.Visible = false;
                    this.button1.Text = "Add";

                }
                catch (Exception exception)
                {
                }
            }
            else if (this.button1.Text == "Save")
            {
                try
                {
                    this.conn.Open();
                    OleDbCommand cmd = new OleDbCommand("INSERT INTO Market(M_Date,Amount) VALUES('" + this.DltDate + "','" + this.textBox1.Text.Trim() + "')", this.conn);
                    cmd.ExecuteNonQuery();
                    this.conn.Close();
                    MessageBox.Show(string.Concat("Successfull Data Added"));
                    this.fillData();
                    this.textBox1.ReadOnly = true;
                    this.textBox1.Text = "";
                    this.button1.Text = "Add";
                }
                catch (Exception exception)
                {
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
                this.checkBox1.Enabled = true;
                this.radioButton1.Enabled = true;
                this.radioButton2.Enabled = true;
            }
            else if (this.button6.Text == "Save")
            {
                if (!this.checkBox1.Checked)
                {
                    try
                    {
                        this.conn.Open();
                        OleDbCommand cmd = new OleDbCommand("INSERT INTO [Credit] ([Given_To],[Total_Given],[Given_Date],[Remarks_Given],[ThroughBy],[Amount],[Bank_Name],[InDel]) VALUES('" + this.textBox33.Text.Trim() + "','" + this.textBox39.Text.Trim() + "','" + this.DltDate + "','" + this.textBox34.Text.Trim() + "','" + this.comboBox1.Text.Trim() + "','" + this.textBox39.Text.Trim() + "','" + this.comboBox1.Text.Trim() + "','" + this.textBox35.Text.Trim() + "')", this.conn);
                        cmd.ExecuteNonQuery();
                        this.conn.Close();
                        MessageBox.Show(string.Concat("Successfull Added to Given"));
                        this.fillGivenData();
                        this.button6.Text = "New";
                    }
                    catch (Exception exception)
                    {
                    }
                }
                else
                {
                    try
                    {
                        this.conn.Open();
                        OleDbCommand cmd = new OleDbCommand("INSERT INTO [Credit] ([Take_To],[Total_Take],[Take_Date],[Remarks_Take],[ThroughBy],[Amount],[Bank_Name],[InDel]) VALUES('" + this.textBox33.Text.Trim() + "','" + this.textBox39.Text.Trim() + "','" + this.DltDate + "','" + this.textBox34.Text.Trim() + "','" + this.comboBox1.Text.Trim() + "','" + this.textBox39.Text.Trim() + "','" + this.comboBox1.Text.Trim() + "','" + this.textBox35.Text.Trim() + "')", this.conn);
                        cmd.ExecuteNonQuery();
                        this.conn.Close();
                        MessageBox.Show(string.Concat("Successfull Added to Taken"));
                        this.fillGivenData();
                        this.button6.Text = "New";
                    }
                    catch (Exception exception)
                    {
                    }
                }

            }
            else if (this.button6.Text == "Updt")
            {
                try
                {
                    if (!this.checkBox1.Checked)
                    {
                        double num1 = Convert.ToDouble(this.textBox39.Text.Trim());
                        double num2 = Convert.ToDouble(this.textBox40.Text.Trim());
                        double num3 = num1 + num2;//G.Amount
                        this.label83.Text = num3.ToString();
                        this.conn.Open();
                        OleDbCommand command = new OleDbCommand("UPDATE Credit SET Amount= '" + this.label83.Text.Trim() + "',Total_Given= '" + this.label83.Text.Trim() + "',Given_Date= '" + this.DltDate + "' WHERE InDel= '" + this.label117.Text.Trim() + "' ", this.conn);
                        command.ExecuteNonQuery();
                        this.conn.Close();
                        MessageBox.Show(string.Concat("Successfull Update - ", this.textBox36.Text));
                        this.AmtCrDataView();
                        this.BalankFld();
                        this.fillGivenData();
                        this.textBox1.ReadOnly = true;
                        this.textBox1.Text = "";
                        this.label6.Text = "";
                        this.dateTimePicker1.Visible = false;
                        this.button1.Text = "Add";
                    }
                    else
                    {
                        double num4 = Convert.ToDouble(this.textBox39.Text.Trim());
                        double num5 = Convert.ToDouble(this.textBox45.Text.Trim());
                        double num6 = num4 + num5;//T.Amount
                        this.label101.Text = num6.ToString();
                        this.conn.Open();
                        OleDbCommand command = new OleDbCommand("UPDATE Credit SET Amount= '" + this.label101.Text.Trim() + "',Total_Take= '" + this.label101.Text.Trim() + "',Take_Date= '" + this.DltDate + "' WHERE InDel= " + this.label117.Text.Trim() + " ", this.conn);
                        command.ExecuteNonQuery();
                        this.conn.Close();
                        MessageBox.Show(string.Concat("Successfull Update - ", this.textBox44.Text));
                        this.AmtCrDataView();
                        this.BalankFld();
                        this.fillGivenData();
                        this.textBox1.ReadOnly = true;
                        this.textBox1.Text = "";
                        this.label6.Text = "";
                        this.dateTimePicker1.Visible = false;
                        this.button1.Text = "Add";
                        
                    }
                }
                catch (Exception exception)
                {
                }
            }
            else if (this.button6.Text == "Sav")
            {
                try
                {
                    this.conn.Open();
                    OleDbCommand cmd = new OleDbCommand("INSERT INTO [Credit] ([Saving_Amount],[Saving_Date],[Remarks_Saving],[Bank_Name],[Amount],[ThroughBy],[InDel]) VALUES('" + this.textBox39.Text.Trim() + "','" + this.DltDate + "','" + this.textBox34.Text.Trim() + "','" + this.comboBox1.Text.Trim() + "','" + this.textBox39.Text.Trim() + "','" + this.comboBox1.Text.Trim() + "','" + this.textBox35.Text.Trim() + "')", this.conn);
                    cmd.ExecuteNonQuery();
                    this.conn.Close();
                    MessageBox.Show(string.Concat("Successfull Added to Saving Amount"));
                    this.fillGivenData();
                    this.AmtCrDataView();
                    this.button6.Text = "New";
                }
                catch (Exception exception)
                {
                }
            }
            else if (this.button6.Text == "Unr")
            {
                try
                {
                    this.conn.Open();
                    OleDbCommand cmd = new OleDbCommand("INSERT INTO [Credit] ([Unrated_Amount],[Unrated_Date],[Remarks_Unrated],[Bank_Name],[Amount],[ThroughBy],[InDel]) VALUES('" + this.textBox39.Text.Trim() + "','" + this.DltDate + "','" + this.textBox34.Text.Trim() + "','" + this.comboBox1.Text.Trim() + "','" + this.textBox39.Text.Trim() + "','" + this.comboBox1.Text.Trim() + "','" + this.textBox35.Text.Trim() + "')", this.conn);
                    cmd.ExecuteNonQuery();
                    this.conn.Close();
                    MessageBox.Show(string.Concat("Successfull Added to Unrated Amount"));
                    this.fillGivenData();
                    this.AmtCrDataView();
                    this.button6.Text = "New";
                }
                catch (Exception exception)
                {
                }
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                this.conn.Open();
                OleDbCommand command = new OleDbCommand("UPDATE CREDIT SET DT_V='DDV' WHERE InDel= '" + this.label117.Text.Trim() + "' ", this.conn);
                command.ExecuteNonQuery();
                this.conn.Close();
                MessageBox.Show(string.Concat("Successfull Deleted - [" , this.label117.Text + "] "));                
                this.BalankFld();
                this.AmtCrDataView();
                this.fillGivenData();
                this.button7.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.label4.Text = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
        }
        private void dataGridView3_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.radioButton1.Enabled = false;
                this.radioButton2.Enabled = false;
                this.conn.Open();
                DataTable dataTable = new DataTable();
                OleDbDataAdapter oleDbData = new OleDbDataAdapter(String.Concat("SELECT * FROM Credit WHERE ID=", this.dataGridView3.SelectedRows[0].Cells[0].Value.ToString(), " "), this.conn);
                oleDbData.Fill(dataTable);
                this.label102.Text  = dataTable.Rows[0][0].ToString();
                this.label117.Text  = dataTable.Rows[0][1].ToString();
                this.textBox36.Text = dataTable.Rows[0][2].ToString();
                this.textBox40.Text = dataTable.Rows[0][3].ToString();
                this.textBox41.Text = dataTable.Rows[0][4].ToString();
                this.textBox42.Text = dataTable.Rows[0][5].ToString();
                this.textBox44.Text = dataTable.Rows[0][6].ToString();
                this.textBox45.Text = dataTable.Rows[0][7].ToString();
                this.textBox46.Text = dataTable.Rows[0][8].ToString();
                this.textBox47.Text = dataTable.Rows[0][9].ToString();
                this.label113.Text  = dataTable.Rows[0][10].ToString();
                this.label111.Text  = dataTable.Rows[0][11].ToString();
                this.label108.Text  = dataTable.Rows[0][12].ToString();
                this.textBox43.Text = dataTable.Rows[0][13].ToString();
                this.textBox48.Text = dataTable.Rows[0][14].ToString();
                this.textBox49.Text = dataTable.Rows[0][15].ToString();
                this.textBox51.Text = dataTable.Rows[0][16].ToString();
                this.textBox52.Text = dataTable.Rows[0][17].ToString();
                this.textBox53.Text = dataTable.Rows[0][18].ToString();
                this.conn.Close();
                this.button7.Visible = true;
                this.textBox39.ReadOnly = false;
                this.dateTimePicker3.Enabled = true;
                this.checkBox1.Enabled = true;
                this.textBox39.Focus();
                this.button6.Text = "Updt";
            }
            catch (Exception exception)
            {
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.conn.Open();
                DataTable dataTabledt = new DataTable();
                OleDbDataAdapter oleDbDatadt = new OleDbDataAdapter(String.Concat("SELECT ID,Amount FROM Market WHERE ID=", this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), " "), this.conn);
                oleDbDatadt.Fill(dataTabledt);
                this.label6.Text   = dataTabledt.Rows[0][0].ToString();
                this.textBox1.Text = dataTabledt.Rows[0][1].ToString();
                this.conn.Close();
                this.textBox1.ReadOnly = false;
                this.textBox1.Focus();
                this.button1.Text = "Updt";
            }
            catch (Exception exception)
            {
            }
        }


        //---------------------------------------------------------------------------
        //------------------------------All Event Work-------------------------------
        //---------------------------------------------------------------------------
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
                            TextBox textBox = this.textBox35;
                            string[] strArrays = new string[] { "A", null, null, null, null };
                            int date = DateTime.Now.Day;
                            int month = DateTime.Now.Month;
                            int millis = DateTime.Now.Millisecond;
                            strArrays[2] = date.ToString();
                            strArrays[3] = month.ToString();
                            strArrays[4] = millis.ToString();
                            textBox.Text = string.Concat(strArrays);
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
                    this.textBox34.Focus();
                }

                
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
                        MessageBox.Show("Please Insert Amount.");
                        this.textBox1.Focus();
                    }
                    else
                    {
                        this.button1.Focus();
                    }
                }
            }
        }
        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (!this.checkBox1.Checked)
            {
                this.checkBox1.Text = "G. Amount :";
                this.textBox39.Focus();
            }
            else
            {
                this.checkBox1.Text = "T. Amount :";
                this.textBox39.Focus();
            }
        }
        private void radioButton1_Click(object sender, EventArgs e)
        {
            this.button6.Text = "Sav";
            this.textBox39.ReadOnly = false;
            this.comboBox1.Enabled = true;
            this.textBox34.ReadOnly = false;
        }
        private void radioButton2_Click(object sender, EventArgs e)
        {
            this.button6.Text = "Unr";
            this.textBox33.ReadOnly = false;
            this.textBox39.ReadOnly = false;
            this.comboBox1.Enabled = true;
            this.textBox34.ReadOnly = false;
        }        
    }
}
