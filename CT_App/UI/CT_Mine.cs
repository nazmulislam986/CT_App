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
using CT_App.CT_BLL;
using CT_App.Models;

namespace CT_App
{
    public partial class CT_Mine : Form
    {
        #region Comments
        private BLLayer _bLLayer = new BLLayer();
        private string DltDate;
        private string tableName = "ImagesTable";
        private string selectedImagePath;
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
            this.fillDataBike();
            this.fillGivenData();
            this.AmtCrDataView();
            this.fillDailyData();
            this.totalDailyData();
            this.fillImageData();
            this.BalankFld();
            this.DailySavin();
            this.BalankFldMonthly();
            this.fillyMonthlyData();
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
            this.panel39.Visible = false;
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
            this.label247.Text = "";
            this.label248.Text = "";
            this.label268.Text = "";
            this.label252.Visible = false;
            this.dataGridView13.Visible = false;
        }

        //------------------------------All Classes------------------------------
        //-----------------------------------------------------------------------
        private void fillData()
        {
            try
            {
                List<DataTable> dataTables = _bLLayer.RetrieveMarketData();
                DataGridView[] dataGridViews = { dataGridView1, dataGridView11 };
                for (int i = 0; i < dataGridViews.Length; i++)
                {
                    dataGridViews[i].DataSource = dataTables[i].DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void fillDataBike()
        {
            try
            {
                List<DataTable> dataTables = _bLLayer.RetrieveDataAllinstaTable();
                DataGridView[] dataGridViews = {  dataGridView2, dataGridView6, dataGridView12 };
                for (int i = 0; i < dataGridViews.Length; i++)
                {
                    dataGridViews[i].DataSource = dataTables[i].DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void DailySavin()
        {
            try
            {
                List<DataTable> dataTables = _bLLayer.RetrieveDailySavTable();
                DataGridView[] dataGridViews = { dataGridView14 };
                for (int i = 0; i < dataGridViews.Length; i++)
                {
                    dataGridViews[i].DataSource = dataTables[i].DefaultView;
                }
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
                List<DataTable> dataTables = _bLLayer.RetrieveDataAllCrTable();
                DataGridView[] dataGridViews = { dataGridView3, dataGridView7, dataGridView8, dataGridView9, dataGridView10 };
                for (int i = 0; i < dataGridViews.Length; i++)
                {
                    dataGridViews[i].DataSource = dataTables[i].DefaultView;
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
                List<DataTable> dataTables = _bLLayer.RetrieveDataAllCutGridTable();
                DataGridView[] dataGridViews = { dataGridView5, dataGridView4, dataGridView17 };
                for (int i = 0; i < dataGridViews.Length; i++)
                {
                    dataGridViews[i].DataSource = dataTables[i].DefaultView;
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
                float totalAmount = _bLLayer.GetTotalMarket();
                this.label5.Text = totalAmount.ToString();
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
                float totalGiven = _bLLayer.GetTotalGiven();
                this.label87.Text = totalGiven.ToString();

                float totalTeken = _bLLayer.GetTotalTeken();
                this.label92.Text = totalTeken.ToString();

                float totalTariffAmt = _bLLayer.GetTotalTariff();
                this.label90.Text = totalTariffAmt.ToString();

                float totalSaving = _bLLayer.GetTotalSaving();
                this.label114.Text = totalSaving.ToString();

                float totalUnrated = _bLLayer.GetTotalUnrated();
                this.label116.Text = totalUnrated.ToString();

                this.label222.Text = _bLLayer.GetTotalDaily();

                this.label261.Text = _bLLayer.GetTotalDailyAnt();

                this.label210.Text = _bLLayer.GetTotalDailySaving();
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
            this.textBox41.Text  = "";
            this.textBox42.Text  = "";
            this.textBox118.Text = "";
            this.textBox119.Text = "";
            this.label111.Text   = "0";
            this.textBox44.Text  = "";
            this.textBox46.Text  = "";
            this.textBox47.Text  = "";
            this.textBox121.Text = "";
            this.textBox120.Text = "";
            this.label113.Text   = "";
            this.textBox104.Text = "";
            this.textBox93.Text  = "";
            this.textBox102.Text = "";
            this.textBox127.Text = "";
            this.textBox109.Text = "";
            this.label243.Text   = "";
            this.textBox105.Text = "";
            this.textBox48.Text  = "";
            this.textBox49.Text  = "";
            this.textBox122.Text = "";
            this.textBox116.Text = "";
            this.textBox106.Text = "";
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
                    label179, label172, label171, label170, label169, label168, label167, label166, label165, label164, label163, label162, label161, label160, label159, label158, label157, label156, label155, label154, label153, label152, label151, label150
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
                this.textBox90.Text = sumNums.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void AllTakenAdd()
        {
            try
            {
                Label[] labels = {
                      label426 ,label418 ,label410 ,label422 ,label414 ,label406 ,label425 ,label417 ,label409 ,label421 ,label413 ,label402 ,label424 ,label416 ,label408 ,label420 ,label412 ,label401 ,label423 ,label415 
                     ,label407 ,label419 ,label411 ,label400 ,label399, label434 ,label442 ,label430 ,label438 ,label446 ,label427 ,label435 ,label443 ,label431 ,label439 ,label447 ,label428 ,label436 ,label444 ,label432 
                     ,label440 ,label448 ,label429 ,label437 ,label445 ,label433 ,label441 ,label449 ,label473 ,label465 ,label457 ,label469 ,label461 ,label453 ,label472 ,label464 ,label456 ,label468 ,label460 ,label452 
                     ,label471 ,label463 ,label455 ,label467 ,label459 ,label451 ,label470 ,label462 ,label454 ,label466 ,label458 ,label450 ,label497 ,label489 ,label481 ,label493 ,label485 ,label477 ,label496 ,label488 
                     ,label480 ,label492 ,label484 ,label476 ,label495 ,label487 ,label479 ,label494 ,label483 ,label475 ,label494 ,label486 ,label478 ,label490 ,label482 ,label474 ,label521 ,label513 ,label505 ,label517
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
                this.textBox163.Text = sumNums.ToString();
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
                float totalDaily = _bLLayer.GetTtlDaily();
                this.label94.Text = totalDaily.ToString();

                float totalDailyCut = _bLLayer.GetTtlDailyCut();
                this.label121.Text = totalDailyCut.ToString();

                float totalDailyAnt = _bLLayer.GetTtlDailyAnt();
                this.label263.Text = totalDailyAnt.ToString();

                float totalDailySave = _bLLayer.GetTtlDailySave();
                this.label254.Text = totalDailySave.ToString();

                float totalInst = _bLLayer.GetTotalInstl(); ;
                this.label211.Text = totalInst.ToString();
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
                DataTable imagesData = _bLLayer.GetImagesData();
                dataGridView14.DataSource = imagesData.DefaultView;
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
        private void fillyMonthlyData()
        {
            try
            {
                List<DataTable> dataTables = _bLLayer.RetrieveMonthlyData();
                DataGridView[] dataGridViews = { dataGridView15 };
                for (int i = 0; i < dataGridViews.Length; i++)
                {
                    dataGridViews[i].DataSource = dataTables[i].DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void BalankFldMonthly()
        {
            this.textBox163.Text = "0";
            this.textBox162.Text = "0";
            this.label294.Text = "0";
            this.textBox146.Text = "0";
            this.textBox145.Text = "0";
            this.textBox144.Text = "0";
            this.textBox143.Text = "0";
            this.textBox142.Text = "0";
            this.textBox141.Text = "0";
            this.textBox140.Text = "0";
            this.textBox139.Text = "0";
            this.textBox138.Text = "0";
            this.textBox161.Text = "0";
            this.textBox160.Text = "0";
            this.textBox159.Text = "0";
            this.textBox158.Text = "0";
            this.textBox157.Text = "0";
            this.textBox156.Text = "0";
            this.textBox155.Text = "0";
            this.textBox154.Text = "0";
            this.textBox153.Text = "0";
            this.textBox152.Text = "0";
            this.textBox151.Text = "0";
            this.textBox172.Text = "0";
            this.textBox173.Text = "0";
            this.textBox174.Text = "0";
            this.textBox175.Text = "0";
            this.textBox176.Text = "0";
            this.textBox177.Text = "0";
            this.textBox178.Text = "0";
            this.textBox179.Text = "0";
            this.textBox180.Text = "0";
            this.textBox148.Text = "0";
            this.textBox149.Text = "0";
            this.textBox150.Text = "0";
            this.textBox164.Text = "0";
            this.textBox165.Text = "0";
            this.textBox166.Text = "0";
            this.textBox167.Text = "0";
            this.textBox168.Text = "0";
            this.textBox169.Text = "0";
            this.textBox170.Text = "0";
            this.textBox171.Text = "0";
            this.textBox192.Text = "0";
            this.textBox193.Text = "0";
            this.textBox194.Text = "0";
            this.textBox195.Text = "0";
            this.textBox196.Text = "0";
            this.textBox197.Text = "0";
            this.textBox198.Text = "0";
            this.textBox199.Text = "0";
            this.textBox200.Text = "0";
            this.textBox181.Text = "0";
            this.textBox182.Text = "0";
            this.textBox183.Text = "0";
            this.textBox184.Text = "0";
            this.textBox185.Text = "0";
            this.textBox186.Text = "0";
            this.textBox187.Text = "0";
            this.textBox188.Text = "0";
            this.textBox189.Text = "0";
            this.textBox190.Text = "0";
            this.textBox191.Text = "0";
            this.textBox212.Text = "0";
            this.textBox213.Text = "0";
            this.textBox214.Text = "0";
            this.textBox215.Text = "0";
            this.textBox216.Text = "0";
            this.textBox217.Text = "0";
            this.textBox218.Text = "0";
            this.textBox219.Text = "0";
            this.textBox220.Text = "0";
            this.textBox201.Text = "0";
            this.textBox202.Text = "0";
            this.textBox203.Text = "0";
            this.textBox204.Text = "0";
            this.textBox205.Text = "0";
            this.textBox206.Text = "0";
            this.textBox207.Text = "0";
            this.textBox208.Text = "0";
            this.textBox209.Text = "0";
            this.textBox210.Text = "0";
            this.textBox211.Text = "0";
            this.textBox232.Text = "0";
            this.textBox233.Text = "0";
            this.textBox234.Text = "0";
            this.textBox235.Text = "0";
            this.textBox236.Text = "0";
            this.textBox237.Text = "0";
            this.textBox238.Text = "0";
            this.textBox239.Text = "0";
            this.textBox240.Text = "0";
            this.textBox221.Text = "0";
            this.textBox222.Text = "0";
            this.textBox223.Text = "0";
            this.textBox224.Text = "0";
            this.textBox225.Text = "0";
            this.textBox226.Text = "0";
            this.textBox227.Text = "0";
            this.textBox228.Text = "0";
            this.textBox229.Text = "0";
            this.textBox230.Text = "0";
            this.textBox231.Text = "0";
        }

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
                        Market market = new Market();
                        market.M_ID = this.textBox101.Text.Trim();
                        market.M_Date = Convert.ToDateTime(this.dateTimePicker1.Text.Trim());
                        market.M_Amount = Convert.ToSingle(this.textBox1.Text.Trim());
                        market.M_Insrt_Person = this.label249.Text.Trim();
                        bool isInserted = _bLLayer.InsMarket(market);
                        if (isInserted)
                        {
                            MessageBox.Show("Data added successfully");
                            this.fillData();
                            this.AmtDataView();
                            this.textBox1.ReadOnly = true;
                            this.textBox1.Text = "";
                            this.button1.Text = "Add";
                            this.BalankFldMarMem();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error : " + ex.Message);
                    }
                }
            }
            else if (this.button1.Text == "Updt")
            {
                try
                {
                    Market market = new Market();
                    market.M_Amount = Convert.ToSingle(this.textBox1.Text.Trim());
                    market.M_Date = Convert.ToDateTime(this.dateTimePicker1.Text.Trim());
                    market.M_Updt_Person = this.label249.Text.Trim();
                    market.M_ID = this.label6.Text.Trim();
                    bool isInserted = _bLLayer.UpdtMarket(market);
                    if (isInserted)
                    {
                        MessageBox.Show(string.Concat("Successfull Update - ", this.label6.Text));
                        this.fillData();
                        this.AmtDataView();
                        this.textBox1.ReadOnly = true;
                        this.textBox1.Text = "";
                        this.label6.Text = "";
                        this.button1.Text = "Add";
                        this.BalankFldMarMem();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error : " + ex.Message);
                }
            }
            else if (this.button1.Text == "U to M")
            {
                try
                {
                    Market market = new Market();
                    market.M_ID = this.textBox108.Text.Trim();
                    market.M_Date = Convert.ToDateTime(this.dateTimePicker1.Text.Trim());
                    market.M_Amount = Convert.ToSingle(this.label10.Text.Trim());
                    market.M_Insrt_Person = this.label249.Text.Trim();
                    bool isInserted = _bLLayer.InsUtoM(market);
                    if (isInserted)
                    {
                        MessageBox.Show("Successfull Memo Amount Added");
                        this.fillData();
                        this.AmtDataView();
                        this.button1.Text = "Add";
                        this.BalankFldMarMem();
                    }
                }
                catch (Exception ex)
                {
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
                        Given insGiven = new Given();
                        insGiven.InGiven = this.textBox35.Text.Trim();
                        insGiven.Total_Given = Convert.ToSingle(this.textBox39.Text.Trim());
                        insGiven.Given_To = this.textBox33.Text.Trim();
                        insGiven.ThroughBy_Given = this.comboBox1.Text.Trim();
                        insGiven.Given_Date = Convert.ToDateTime(this.dateTimePicker3.Text.Trim());
                        insGiven.Remarks_Given = this.textBox34.Text.Trim();
                        insGiven.GDT_V = "NDV";
                        insGiven.G_Insrt_Person = this.label249.Text.Trim();
                        bool isInserted = _bLLayer.InsGiven(insGiven);
                        if (isInserted)
                        {
                            MessageBox.Show(string.Concat("Successfull Added to Given"));
                            this.fillGivenData();
                            this.AmtCrDataView();
                            this.button6.Text = "New";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error : " + ex.Message);
                    }
                }
                else if(this.radioButton4.Checked)
                { 
                    try
                    {
                        Teken insTeken = new Teken();
                        insTeken.InTake = this.textBox35.Text.Trim();
                        insTeken.Total_Take = Convert.ToSingle(this.textBox39.Text.Trim());
                        insTeken.Take_To = this.textBox33.Text.Trim();
                        insTeken.ThroughBy_Take = this.comboBox1.Text.Trim();
                        insTeken.Take_Date = Convert.ToDateTime(this.dateTimePicker3.Text.Trim());
                        insTeken.Remarks_Take = this.textBox34.Text.Trim();
                        insTeken.TDT_V = "NDV";
                        insTeken.T_Insrt_Person = this.label249.Text.Trim();
                        bool isInserted = _bLLayer.InsTeken(insTeken);
                        if (isInserted)
                        {
                            MessageBox.Show(string.Concat("Successfull Added to Taken"));
                            this.fillGivenData();
                            this.AmtCrDataView();
                            this.button6.Text = "New";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error : " + ex.Message);
                    }
                }
                else if (this.radioButton3.Checked)
                {
                    try
                    {
                        TariffAmt instariff = new TariffAmt();
                        instariff.InExpense = this.textBox35.Text.Trim();
                        instariff.Expense_Amount = Convert.ToSingle(this.textBox39.Text.Trim());
                        instariff.Expense_To = this.textBox33.Text.Trim();
                        instariff.ThroughBy_Expense = this.comboBox1.Text.Trim();
                        instariff.Expense_Date = Convert.ToDateTime(this.dateTimePicker3.Text.Trim());
                        instariff.Remarks_Expense = this.textBox34.Text.Trim();
                        instariff.EDT_V = "NDV";
                        instariff.E_Insrt_Person = this.label249.Text.Trim();
                        bool isInserted = _bLLayer.InsTariffAmt(instariff);
                        if (isInserted)
                        {
                            MessageBox.Show(string.Concat("Successfull Added to Expense"));
                            this.fillGivenData();
                            this.AmtCrDataView();
                            this.button6.Text = "New";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error : " + ex.Message);
                    }
                }
                else if (this.radioButton1.Checked)
                {
                    try
                    {
                        Saving saving = new Saving();
                        saving.InSaving = this.textBox35.Text.Trim();
                        saving.Saving_Amount = Convert.ToSingle(this.textBox39.Text.Trim());
                        saving.Saving_To = this.textBox33.Text.Trim();
                        saving.ThroughBy_Saving = this.comboBox1.Text.Trim();
                        saving.Saving_Date = Convert.ToDateTime(this.dateTimePicker3.Text.Trim());
                        saving.Remarks_Saving = this.textBox34.Text.Trim();
                        saving.SDT_V = "NDV";
                        saving.Saving_Bank = this.comboBox1.Text.Trim();
                        saving.S_Insrt_Person = this.label249.Text.Trim();
                        bool isInserted = _bLLayer.InsSaving(saving);
                        if (isInserted)
                        {
                            MessageBox.Show(string.Concat("Successfull Added to Saving"));
                            this.fillGivenData();
                            this.AmtCrDataView();
                            this.button6.Text = "New";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error : " + ex.Message);
                    }
                }
                else if (this.radioButton2.Checked)
                {
                    try
                    {
                        Unrated unrated = new Unrated();
                        unrated.InUnrated = this.textBox35.Text.Trim();
                        unrated.Unrated_Amount = Convert.ToSingle(this.textBox39.Text.Trim());
                        unrated.Unrated_To = this.textBox33.Text.Trim();
                        unrated.ThroughBy_Unrated = this.comboBox1.Text.Trim();
                        unrated.Unrated_Date = Convert.ToDateTime(this.dateTimePicker3.Text.Trim());
                        unrated.Remarks_Unrated = this.textBox34.Text.Trim();
                        unrated.UDT_V = "NDV";
                        unrated.U_Insrt_Person = this.label249.Text.Trim();
                        bool isInserted = _bLLayer.InsUnrated(unrated);
                        if (isInserted)
                        {
                            MessageBox.Show(string.Concat("Successfull Added to Unrated"));
                            this.fillGivenData();
                            this.AmtCrDataView();
                            this.button6.Text = "New";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error : " + ex.Message);
                    }
                }
            }
        }
        private void button19_Click(object sender, EventArgs e)
        {
            try
            {
                Given insGiven = new Given();
                insGiven.Total_Given = Convert.ToSingle(this.textBox40.Text.Trim());
                insGiven.GDT_V_Date = Convert.ToDateTime(this.DltDate);
                insGiven.G_Updt_Person = this.label249.Text.Trim();
                insGiven.InGiven = this.label117.Text.Trim();
                insGiven.InGiven = this.label117.Text.Trim();
                insGiven.Was_Given_UD = Convert.ToSingle(this.label111.Text.Trim());
                insGiven.Now_Given_UD = Convert.ToSingle(this.textBox119.Text.Trim());
                insGiven.Total_Given_UD = Convert.ToSingle(this.textBox40.Text.Trim());
                insGiven.Given_To_UD = this.textBox36.Text.Trim();
                insGiven.GDT_V_Date_UD = Convert.ToDateTime(this.DltDate);
                bool isInserted = _bLLayer.InsUpdtGiven(insGiven);
                if (isInserted)
                {
                    MessageBox.Show($"Successfully Given TK Update For - {this.label117.Text} ");
                    this.AmtCrDataView();
                    this.BalankFld();
                    this.fillGivenData();
                    this.checkBoxClear();
                    this.textBox109.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void button20_Click(object sender, EventArgs e)
        {
            try
            {
                Teken insTeken = new Teken();
                insTeken.Total_Take = Convert.ToSingle(this.textBox45.Text.Trim());
                insTeken.TDT_V_Date = Convert.ToDateTime(this.DltDate);
                insTeken.T_Updt_Person = this.label249.Text.Trim();
                insTeken.InTake = this.label117.Text.Trim();
                insTeken.InTake = this.label117.Text.Trim();
                insTeken.Was_Take_UD = Convert.ToSingle(this.label111.Text.Trim());
                insTeken.Now_Take_UD = Convert.ToSingle(this.textBox120.Text.Trim());
                insTeken.Total_Take_UD = Convert.ToSingle(this.textBox45.Text.Trim());
                insTeken.Take_To_UD = this.textBox44.Text.Trim();
                insTeken.TDT_V_Date_UD = Convert.ToDateTime(this.DltDate);
                bool isInserted = _bLLayer.InsUpdtTeken(insTeken);
                if (isInserted)
                {
                    MessageBox.Show($"Successfully Teken TK Update For - {this.label117.Text} ");
                    this.AmtCrDataView();
                    this.BalankFld();
                    this.fillGivenData();
                    this.checkBoxClear();
                    this.textBox109.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void button16_Click(object sender, EventArgs e)
        {
            try
            {
                TariffAmt insTariff = new TariffAmt();
                insTariff.Expense_Amount = Convert.ToSingle(this.textBox103.Text.Trim());
                insTariff.EDT_V_Date = Convert.ToDateTime(this.DltDate);
                insTariff.E_Updt_Person = this.label249.Text.Trim();
                insTariff.InExpense = this.label117.Text.Trim();
                insTariff.InExpense = this.label117.Text.Trim();
                insTariff.Was_Expense_UD = Convert.ToSingle(this.label111.Text.Trim());
                insTariff.Now_Expense_UD = Convert.ToSingle(this.textBox109.Text.Trim());
                insTariff.Expense_Amount_UD = Convert.ToSingle(this.textBox103.Text.Trim());
                insTariff.Expense_To_UD = this.textBox104.Text.Trim();
                insTariff.EDT_V_Date_UD = Convert.ToDateTime(this.DltDate);
                bool isInserted = _bLLayer.InsUpdtTariffAmt(insTariff);
                if (isInserted)
                {
                    MessageBox.Show($"Successfully Expance TK Update For - {this.label117.Text} ");
                    this.AmtCrDataView();
                    this.BalankFld();
                    this.fillGivenData();
                    this.checkBoxClear();
                    this.textBox109.Text = "";
                    this.button6.Text = "New";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void button17_Click(object sender, EventArgs e)
        {
            try
            {
                Saving insSaving = new Saving();
                insSaving.Saving_Amount = Convert.ToSingle(this.textBox43.Text.Trim());
                insSaving.SDT_V_Date = Convert.ToDateTime(this.DltDate);
                insSaving.S_Updt_Person = this.label249.Text.Trim();
                insSaving.InSaving = this.label117.Text.Trim();
                insSaving.InSaving = this.label117.Text.Trim();
                insSaving.Was_Saving_UD = Convert.ToSingle(this.label111.Text.Trim());
                insSaving.Now_Saving_UD = Convert.ToSingle(this.textBox116.Text.Trim());
                insSaving.Saving_Amount_UD = Convert.ToSingle(this.textBox43.Text.Trim());
                insSaving.Saving_To_UD = this.textBox105.Text.Trim();
                insSaving.SDT_V_Date_UD = Convert.ToDateTime(this.DltDate);
                bool isInserted = _bLLayer.InsUpdtSaving(insSaving);
                if (isInserted)
                {
                    MessageBox.Show($"Successfully Saving TK Update For - {this.label117.Text} ");
                    this.AmtCrDataView();
                    this.BalankFld();
                    this.fillGivenData();
                    this.checkBoxClear();
                    this.textBox116.Text = "";
                    this.button6.Text = "New";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void button18_Click(object sender, EventArgs e)
        {
            try
            {
                Unrated insUnrated = new Unrated();
                insUnrated.Unrated_Amount = Convert.ToSingle(this.textBox51.Text.Trim());
                insUnrated.UDT_V_Date = Convert.ToDateTime(this.DltDate);
                insUnrated.U_Updt_Person = this.label249.Text.Trim();
                insUnrated.InUnrated = this.label117.Text.Trim();
                insUnrated.InUnrated = this.label117.Text.Trim();
                insUnrated.Was_Unrated_UD = Convert.ToSingle(this.label111.Text.Trim());
                insUnrated.Now_Unrated_UD = Convert.ToSingle(this.textBox117.Text.Trim());
                insUnrated.Unrated_Amount_UD = Convert.ToSingle(this.textBox51.Text.Trim());
                insUnrated.Unrated_To_UD = this.textBox106.Text.Trim();
                insUnrated.UDT_V_Date_UD = Convert.ToDateTime(this.DltDate);
                bool isInserted = _bLLayer.InsUpdtUnrated(insUnrated);
                if (isInserted)
                {
                    MessageBox.Show($"Successfully Unrated TK Update For - {this.label117.Text} ");
                    this.AmtCrDataView();
                    this.BalankFld();
                    this.fillGivenData();
                    this.checkBoxClear();
                    this.textBox117.Text = "";
                    this.button6.Text = "New";
                }
            }
            catch (Exception ex)
            {
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
                    Given delGiven = new Given();
                    delGiven.GDT_V = "DDV";
                    delGiven.DDT_V_Date = Convert.ToDateTime(this.DltDate);
                    delGiven.G_Del_Person = this.label249.Text.Trim();
                    delGiven.InGiven = this.label117.Text.Trim();
                    bool isInserted = _bLLayer.DelGiven(delGiven);
                    if (isInserted)
                    {
                        MessageBox.Show($"Successfull Deleted - [{this.label117.Text}]");
                        this.BalankFld();
                        this.AmtCrDataView();
                        this.fillGivenData();
                        this.button7.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error : " + ex.Message);
                }
            }
            else if (this.button7.Text == "Delete T.")
            {
                try
                {
                    Teken delTeken = new Teken();
                    delTeken.TDT_V = "DDV";
                    delTeken.DDT_V_Date = Convert.ToDateTime(this.DltDate);
                    delTeken.T_Del_Person = this.label249.Text.Trim();
                    delTeken.InTake = this.label117.Text.Trim();
                    bool isInserted = _bLLayer.DelTeken(delTeken);
                    if (isInserted)
                    {
                        MessageBox.Show($"Successfull Deleted - [{this.label117.Text}]");
                        this.BalankFld();
                        this.AmtCrDataView();
                        this.fillGivenData();
                        this.button7.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error : " + ex.Message);
                }
            }
            else if (this.button7.Text == "Delete E.")
            {
                try
                {
                    TariffAmt delTariff = new TariffAmt();
                    delTariff.EDT_V = "DDV";
                    delTariff.DDT_V_Date = Convert.ToDateTime(this.DltDate);
                    delTariff.E_Del_Person = this.label249.Text.Trim();
                    delTariff.InExpense = this.label117.Text.Trim();
                    bool isInserted = _bLLayer.DelTariffAmt(delTariff);
                    if (isInserted)
                    {
                        MessageBox.Show($"Successfull Deleted - [{this.label117.Text}]");
                        this.BalankFld();
                        this.AmtCrDataView();
                        this.fillGivenData();
                        this.button7.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error : " + ex.Message);
                }
            }
            else if (this.button7.Text == "Delete S.")
            {
                try
                {
                    Saving delSaving = new Saving();
                    delSaving.SDT_V = "DDV";
                    delSaving.DDT_V_Date = Convert.ToDateTime(this.DltDate);
                    delSaving.S_Del_Person = this.label249.Text.Trim();
                    delSaving.InSaving = this.label117.Text.Trim();
                    bool isInserted = _bLLayer.DelSaving(delSaving);
                    if (isInserted)
                    {
                        MessageBox.Show($"Successfull Deleted - [{this.label117.Text}]");
                        this.BalankFld();
                        this.AmtCrDataView();
                        this.fillGivenData();
                        this.button7.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error : " + ex.Message);
                }
            }
            else if (this.button7.Text == "Delete U.")
            {
                try
                {
                    Unrated delUnrated = new Unrated();
                    delUnrated.UDT_V = "DDV";
                    delUnrated.DDT_V_Date = Convert.ToDateTime(this.DltDate);
                    delUnrated.U_Del_Person = this.label249.Text.Trim();
                    delUnrated.InUnrated = this.label117.Text.Trim();
                    bool isInserted = _bLLayer.DelUnrated(delUnrated);
                    if (isInserted)
                    {
                        MessageBox.Show($"Successfull Deleted - [{this.label117.Text}]");
                        this.BalankFld();
                        this.AmtCrDataView();
                        this.fillGivenData();
                        this.button7.Visible = false;
                    }
                }
                catch (Exception ex)
                {
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
                        Daily insdaily = new Daily();
                        insdaily.D_ID = this.textBox92.Text.Trim();
                        insdaily.D_Date = Convert.ToDateTime(this.dateTimePicker4.Text.Trim());
                        insdaily.D_FPAmount = Convert.ToSingle(this.textBox37.Text.Trim());
                        insdaily.D_SPAmount = Convert.ToSingle(this.label194.Text.Trim());
                        insdaily.NotTaken = Convert.ToSingle(this.label194.Text.Trim());
                        insdaily.D_Data = "NTKN";
                        insdaily.D_Insrt_Person = this.label249.Text.Trim();
                        bool isInserted = _bLLayer.InsDaily(insdaily);
                        if (isInserted)
                        {
                            MessageBox.Show(string.Concat("Successfull Daily Data Added"));
                            this.fillDailyData();
                            this.totalDailyData();
                            this.textBox37.ReadOnly = true;
                            this.textBox37.Text = "";
                            this.textBox92.Text = "";
                            this.button10.Text = "Add";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error : " + ex.Message);
                    }
                }
            }
            else if (this.button10.Text == "Updt")
            {
                try
                {
                    Daily updtdaily = new Daily();
                    updtdaily.D_FPAmount = Convert.ToSingle(this.textBox37.Text.Trim());
                    updtdaily.D_SPAmount = Convert.ToSingle(this.label194.Text.Trim());
                    updtdaily.NotTaken = Convert.ToSingle(this.label194.Text.Trim());
                    updtdaily.D_Date = Convert.ToDateTime(this.dateTimePicker4.Text.Trim());
                    updtdaily.D_Updt_Person = this.label249.Text.Trim();
                    updtdaily.D_ID = this.label182.Text.Trim();
                    bool isUpdated = _bLLayer.UpdtDaily(updtdaily);
                    if (isUpdated)
                    {
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
                }
                catch (Exception ex)
                {
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
                        DailyCut insdailyCut = new DailyCut();
                        insdailyCut.C_ID = this.textBox92.Text.Trim();
                        insdailyCut.C_Date = Convert.ToDateTime(this.dateTimePicker5.Text.Trim());
                        insdailyCut.C_Amount = Convert.ToSingle(this.textBox50.Text.Trim());
                        insdailyCut.C_Insrt_Person = this.label249.Text.Trim();
                        bool isInserted = _bLLayer.AddDailyCut(insdailyCut);
                        if (isInserted)
                        {
                            MessageBox.Show(string.Concat("Successfull Added Total Daily Amount"));
                            this.fillDailyData();
                            this.totalDailyData();
                            this.textBox50.ReadOnly = true;
                            this.textBox50.Text = "";
                            this.textBox92.Text = "";
                            this.button14.Text = "Add";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error : " + ex.Message);
                    }
                }
            }
            else if (this.button14.Text == "Updt")
            {
                try
                {
                    DailyCut updtdailyCut = new DailyCut();
                    updtdailyCut.C_Amount = Convert.ToSingle(this.textBox50.Text.Trim());
                    updtdailyCut.C_Date = Convert.ToDateTime(this.dateTimePicker5.Text.Trim());
                    updtdailyCut.C_Updt_Person = this.label249.Text.Trim();
                    updtdailyCut.C_ID = this.label182.Text.Trim();
                    bool isUpdated = _bLLayer.UpdateDailyCut(updtdailyCut);
                    if (isUpdated)
                    {
                        MessageBox.Show(string.Concat("Successfull Update Daily Gat"));
                        this.fillDailyData();
                        this.totalDailyData();
                        this.textBox50.ReadOnly = true;
                        this.textBox50.Text = "";
                        this.label182.Text = "0";
                        this.label191.Text = "0";
                        this.button14.Text = "Add";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error : " + ex.Message);
                }
            }
        }
        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                Daily deldaily = new Daily();
                deldaily.D_Data = "TKN";
                deldaily.D_Date = Convert.ToDateTime(this.DltDate);
                deldaily.D_Insrt_Person = this.label249.Text.Trim();
                deldaily.D_ID = this.label182.Text.Trim();
                bool isDeleted = _bLLayer.DelDaily(deldaily);
                if (isDeleted)
                {
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void button25_Click_1(object sender, EventArgs e)
        {
            try
            {
                DailySaving deldailySav = new DailySaving();
                deldailySav.DS_Data = "TKN";
                deldailySav.DS_InBankDate = Convert.ToDateTime(this.DltDate);
                deldailySav.DS_Del_Person = this.label249.Text.Trim();
                deldailySav.DS_ID = this.label292.Text.Trim();
                bool isUpdated = _bLLayer.DelDailySaving(deldailySav);
                if (isUpdated)
                {
                    MessageBox.Show($"Successfull Deleted - [{this.label292.Text}]");
                    this.DailySavin();
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void button22_Click(object sender, EventArgs e)
        {
            try
            {
                string D_ID = this.label247.Text.Trim();
                string C_ID = this.label248.Text.Trim();
                if (string.IsNullOrWhiteSpace(D_ID) || string.IsNullOrWhiteSpace(C_ID))
                {
                    MessageBox.Show($"R. Del ID1 :[{this.label247.Text}]\n\nR. Del ID2 :[{this.label248.Text}]\n\nBoth Selection Needed", "Error Delete");
                    return;
                }
                DailyCut deldailyCut = new DailyCut();
                deldailyCut.C_Del_Person = this.label249.Text.Trim();
                bool isdeleted = _bLLayer.DelDailyAndDailyCut(D_ID, C_ID, deldailyCut);
                if (isdeleted)
                {
                    MessageBox.Show($"Successfull Deleted - [{this.label247.Text}] & [{this.label248.Text}]");
                    this.fillDailyData();
                    this.button22.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void button24_Click_1(object sender, EventArgs e)
        {
            try
            {
                DailySaving delredailySav = new DailySaving();
                delredailySav.DS_ID = this.label284.Text.Trim();
                bool isUpdated = _bLLayer.DelReDailySaving(delredailySav);
                if (isUpdated)
                {
                    MessageBox.Show($"Successfull Deleted - [{this.label284.Text}]");
                    this.DailySavin();
                    this.button24.Visible = false;
                }
            }
            catch (Exception ex)
            {
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
                        Installment insinstalSav = new Installment();
                        insinstalSav.I_ID = this.textBox98.Text.Trim();
                        insinstalSav.InsPay_Date = Convert.ToDateTime(this.dateTimePicker2.Text.Trim());
                        insinstalSav.InsPay = Convert.ToSingle(this.textBox32.Text.Trim());
                        insinstalSav.Take_Data = "INS";
                        insinstalSav.I_Insrt_Person = this.label249.Text.Trim();
                        bool isInserted = _bLLayer.InsInstallment(insinstalSav);
                        if (isInserted)
                        {
                            this.fillDataBike();
                            this.totalDailyData();
                            MessageBox.Show(string.Concat("Successfull Daily InstallPay Added"));
                            this.textBox32.ReadOnly = true;
                            this.textBox32.Text = "";
                            this.button4.Text = "Add";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error : " + ex.Message);
                    }
                }
            }
            else if (this.button4.Text == "Updt")
            {
                try
                {
                    Installment updtinstalSav = new Installment();
                    updtinstalSav.InsPay_Date = Convert.ToDateTime(this.dateTimePicker2.Text.Trim());
                    updtinstalSav.I_Updt_Person = this.label249.Text.Trim();
                    updtinstalSav.I_ID = this.label201.Text.Trim();
                    bool isInserted = _bLLayer.UpdtInstallment(updtinstalSav);
                    if (isInserted)
                    {
                        MessageBox.Show(string.Concat("Successfull Update Instrallment Date"));
                        this.fillDataBike();
                        this.textBox32.ReadOnly = true;
                        this.textBox32.Text = "";
                        this.button4.Text = "Add";
                    }
                }
                catch (Exception ex)
                {
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
                        Installment insinstalSav = new Installment();
                        insinstalSav.I_ID = this.textBox99.Text.Trim();
                        insinstalSav.I_Date = Convert.ToDateTime(this.DltDate);
                        insinstalSav.Take_Total = Convert.ToSingle(this.textBox94.Text.Trim());
                        insinstalSav.Take_Anot = Convert.ToSingle(this.textBox96.Text.Trim());
                        insinstalSav.Take_Mine = Convert.ToSingle(this.textBox97.Text.Trim());
                        insinstalSav.InsPerMonth = Convert.ToSingle(this.textBox95.Text.Trim());
                        insinstalSav.PerMonthPay = Convert.ToSingle(this.textBox99.Text.Trim());
                        insinstalSav.Take_Data = "NPD";
                        bool isInserted = _bLLayer.InsrInstallment(insinstalSav);
                        if (isInserted)
                        {
                            this.fillDataBike();
                            this.totalDailyData();
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
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error : " + ex.Message);
                    }
                }
            }
            else if (this.button13.Text == "Dlt")
            {
                try
                {
                    Installment insinstalSav = new Installment();
                    insinstalSav.Take_Data = "TPD";
                    insinstalSav.I_ID = this.label218.Text.Trim();
                    bool isUpdated = _bLLayer.UdtInstallment(insinstalSav);
                    if (isUpdated)
                    {
                        MessageBox.Show($"Successfull Deleted - [{this.label218.Text}]");
                        this.fillDataBike();
                        if (this.dataGridView6.RowCount > 0)
                        {
                            this.totalDailyData();
                        }
                        else
                        {
                            this.label203.Text = "00";
                            this.label72.Text = "00";
                            this.label206.Text = "00";
                            this.label205.Text = "00";
                        }
                        this.button13.Text = "Add";
                    }
                }
                catch (Exception ex)
                {
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
                    MarketMemos marketMemos = new MarketMemos();
                    marketMemos.Mem_ID = this.textBox108.Text.Trim();
                    marketMemos.Mem_Date = Convert.ToDateTime(this.DltDate);
                    marketMemos.R_InvTK = Convert.ToSingle(this.textBox90.Text.Trim());
                    marketMemos.C_InvTK = Convert.ToSingle(this.label10.Text.Trim());
                    marketMemos.Giv_TK = Convert.ToSingle(this.textBox55.Text.Trim());
                    marketMemos.Ret_TK = Convert.ToSingle(this.label147.Text.Trim());

                    marketMemos.I_N01 = this.textBox72.Text.Trim();
                    marketMemos.I_N02 = this.textBox73.Text.Trim();
                    marketMemos.I_N03 = this.textBox78.Text.Trim();
                    marketMemos.I_N04 = this.textBox75.Text.Trim();
                    marketMemos.I_N05 = this.textBox76.Text.Trim();
                    marketMemos.I_N06 = this.textBox77.Text.Trim();
                    marketMemos.I_N07 = this.textBox79.Text.Trim();
                    marketMemos.I_N08 = this.textBox80.Text.Trim();
                    marketMemos.I_N09 = this.textBox81.Text.Trim();
                    marketMemos.I_N10 = this.textBox82.Text.Trim();
                    marketMemos.I_N11 = this.textBox83.Text.Trim();
                    marketMemos.I_N12 = this.textBox84.Text.Trim();
                    marketMemos.I_N13 = this.textBox85.Text.Trim();
                    marketMemos.I_N14 = this.textBox86.Text.Trim();
                    marketMemos.I_N15 = this.textBox87.Text.Trim();
                    marketMemos.I_N16 = this.textBox88.Text.Trim();

                    marketMemos.I_P01 = Convert.ToSingle(this.textBox3.Text.Trim());
                    marketMemos.I_P02 = Convert.ToSingle(this.textBox5.Text.Trim());
                    marketMemos.I_P03 = Convert.ToSingle(this.textBox7.Text.Trim());
                    marketMemos.I_P04 = Convert.ToSingle(this.textBox9.Text.Trim());
                    marketMemos.I_P05 = Convert.ToSingle(this.textBox11.Text.Trim());
                    marketMemos.I_P06 = Convert.ToSingle(this.textBox13.Text.Trim());
                    marketMemos.I_P07 = Convert.ToSingle(this.textBox15.Text.Trim());
                    marketMemos.I_P08 = Convert.ToSingle(this.textBox17.Text.Trim());
                    marketMemos.I_P09 = Convert.ToSingle(this.textBox19.Text.Trim());
                    marketMemos.I_P10 = Convert.ToSingle(this.textBox21.Text.Trim());
                    marketMemos.I_P11 = Convert.ToSingle(this.textBox23.Text.Trim());
                    marketMemos.I_P12 = Convert.ToSingle(this.textBox25.Text.Trim());
                    marketMemos.I_P13 = Convert.ToSingle(this.textBox27.Text.Trim());
                    marketMemos.I_P14 = Convert.ToSingle(this.textBox29.Text.Trim());
                    marketMemos.I_P15 = Convert.ToSingle(this.textBox31.Text.Trim());
                    marketMemos.I_P16 = Convert.ToSingle(this.textBox38.Text.Trim());

                    marketMemos.I_Q01 = Convert.ToSingle(this.textBox2.Text.Trim());
                    marketMemos.I_Q02 = Convert.ToSingle(this.textBox4.Text.Trim());
                    marketMemos.I_Q03 = Convert.ToSingle(this.textBox6.Text.Trim());
                    marketMemos.I_Q04 = Convert.ToSingle(this.textBox8.Text.Trim());
                    marketMemos.I_Q05 = Convert.ToSingle(this.textBox10.Text.Trim());
                    marketMemos.I_Q06 = Convert.ToSingle(this.textBox12.Text.Trim());
                    marketMemos.I_Q07 = Convert.ToSingle(this.textBox14.Text.Trim());
                    marketMemos.I_Q08 = Convert.ToSingle(this.textBox16.Text.Trim());
                    marketMemos.I_Q09 = Convert.ToSingle(this.textBox18.Text.Trim());
                    marketMemos.I_Q10 = Convert.ToSingle(this.textBox20.Text.Trim());
                    marketMemos.I_Q11 = Convert.ToSingle(this.textBox22.Text.Trim());
                    marketMemos.I_Q12 = Convert.ToSingle(this.textBox24.Text.Trim());
                    marketMemos.I_Q13 = Convert.ToSingle(this.textBox26.Text.Trim());
                    marketMemos.I_Q14 = Convert.ToSingle(this.textBox28.Text.Trim());
                    marketMemos.I_Q15 = Convert.ToSingle(this.textBox30.Text.Trim());
                    marketMemos.I_Q16 = Convert.ToSingle(this.textBox54.Text.Trim());

                    marketMemos.I_ST01 = Convert.ToSingle(this.label9.Text.Trim());
                    marketMemos.I_ST02 = Convert.ToSingle(this.label13.Text.Trim());
                    marketMemos.I_ST03 = Convert.ToSingle(this.label17.Text.Trim());
                    marketMemos.I_ST04 = Convert.ToSingle(this.label24.Text.Trim());
                    marketMemos.I_ST05 = Convert.ToSingle(this.label28.Text.Trim());
                    marketMemos.I_ST06 = Convert.ToSingle(this.label32.Text.Trim());
                    marketMemos.I_ST07 = Convert.ToSingle(this.label36.Text.Trim());
                    marketMemos.I_ST08 = Convert.ToSingle(this.label40.Text.Trim());
                    marketMemos.I_ST09 = Convert.ToSingle(this.label44.Text.Trim());
                    marketMemos.I_ST10 = Convert.ToSingle(this.label48.Text.Trim());
                    marketMemos.I_ST11 = Convert.ToSingle(this.label52.Text.Trim());
                    marketMemos.I_ST12 = Convert.ToSingle(this.label56.Text.Trim());
                    marketMemos.I_ST13 = Convert.ToSingle(this.label60.Text.Trim());
                    marketMemos.I_ST14 = Convert.ToSingle(this.label64.Text.Trim());
                    marketMemos.I_ST15 = Convert.ToSingle(this.label68.Text.Trim());
                    marketMemos.I_ST16 = Convert.ToSingle(this.label76.Text.Trim());

                    marketMemos.R_Inv01 = Convert.ToSingle(this.textBox56.Text.Trim());
                    marketMemos.R_Inv02 = Convert.ToSingle(this.textBox57.Text.Trim());
                    marketMemos.R_Inv03 = Convert.ToSingle(this.textBox58.Text.Trim());
                    marketMemos.R_Inv04 = Convert.ToSingle(this.textBox59.Text.Trim());
                    marketMemos.R_Inv05 = Convert.ToSingle(this.textBox60.Text.Trim());
                    marketMemos.R_Inv06 = Convert.ToSingle(this.textBox61.Text.Trim());
                    marketMemos.R_Inv07 = Convert.ToSingle(this.textBox62.Text.Trim());
                    marketMemos.R_Inv08 = Convert.ToSingle(this.textBox63.Text.Trim());
                    marketMemos.R_Inv09 = Convert.ToSingle(this.textBox64.Text.Trim());
                    marketMemos.R_Inv10 = Convert.ToSingle(this.textBox65.Text.Trim());
                    marketMemos.R_Inv11 = Convert.ToSingle(this.textBox66.Text.Trim());
                    marketMemos.R_Inv12 = Convert.ToSingle(this.textBox67.Text.Trim());
                    marketMemos.R_Inv13 = Convert.ToSingle(this.textBox68.Text.Trim());
                    marketMemos.R_Inv14 = Convert.ToSingle(this.textBox69.Text.Trim());
                    marketMemos.R_Inv15 = Convert.ToSingle(this.textBox70.Text.Trim());
                    marketMemos.R_Inv16 = Convert.ToSingle(this.textBox71.Text.Trim());
                    marketMemos.R_Inv17 = Convert.ToSingle(this.textBox89.Text.Trim());
                    marketMemos.R_Inv18 = Convert.ToSingle(this.textBox91.Text.Trim());
                    marketMemos.R_Inv19 = Convert.ToSingle(this.textBox110.Text.Trim());
                    marketMemos.R_Inv20 = Convert.ToSingle(this.textBox111.Text.Trim());
                    marketMemos.R_Inv21 = Convert.ToSingle(this.textBox112.Text.Trim());
                    marketMemos.R_Inv22 = Convert.ToSingle(this.textBox113.Text.Trim());
                    marketMemos.R_Inv23 = Convert.ToSingle(this.textBox114.Text.Trim());
                    marketMemos.R_Inv24 = Convert.ToSingle(this.textBox115.Text.Trim());
                    marketMemos.Mem_Insrt_Person = this.label249.Text.Trim();
                    bool isInserted = _bLLayer.InsMarketMemos(marketMemos);
                    if (isInserted)
                    {
                        MessageBox.Show(string.Concat("Successfull Memo Added"));
                        this.fillData();
                        this.button21.Visible = true;
                        this.button15.Text = "New";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error : " + ex.Message);
                }
            }
            else if (this.button15.Text == "Update")
            {
                try
                {
                    MarketMemos marketMemos = new MarketMemos();
                    marketMemos.R_InvTK = Convert.ToSingle(this.textBox90.Text.Trim());
                    marketMemos.C_InvTK = Convert.ToSingle(this.label10.Text.Trim());
                    marketMemos.Giv_TK = Convert.ToSingle(this.textBox55.Text.Trim());
                    marketMemos.Ret_TK = Convert.ToSingle(this.label147.Text.Trim());

                    marketMemos.I_N01 = this.textBox72.Text.Trim();
                    marketMemos.I_N02 = this.textBox73.Text.Trim();
                    marketMemos.I_N03 = this.textBox78.Text.Trim();
                    marketMemos.I_N04 = this.textBox75.Text.Trim();
                    marketMemos.I_N05 = this.textBox76.Text.Trim();
                    marketMemos.I_N06 = this.textBox77.Text.Trim();
                    marketMemos.I_N07 = this.textBox79.Text.Trim();
                    marketMemos.I_N08 = this.textBox80.Text.Trim();
                    marketMemos.I_N09 = this.textBox81.Text.Trim();
                    marketMemos.I_N10 = this.textBox82.Text.Trim();
                    marketMemos.I_N11 = this.textBox83.Text.Trim();
                    marketMemos.I_N12 = this.textBox84.Text.Trim();
                    marketMemos.I_N13 = this.textBox85.Text.Trim();
                    marketMemos.I_N14 = this.textBox86.Text.Trim();
                    marketMemos.I_N15 = this.textBox87.Text.Trim();
                    marketMemos.I_N16 = this.textBox88.Text.Trim();

                    marketMemos.I_P01 = Convert.ToSingle(this.textBox3.Text.Trim());
                    marketMemos.I_P02 = Convert.ToSingle(this.textBox5.Text.Trim());
                    marketMemos.I_P03 = Convert.ToSingle(this.textBox7.Text.Trim());
                    marketMemos.I_P04 = Convert.ToSingle(this.textBox9.Text.Trim());
                    marketMemos.I_P05 = Convert.ToSingle(this.textBox11.Text.Trim());
                    marketMemos.I_P06 = Convert.ToSingle(this.textBox13.Text.Trim());
                    marketMemos.I_P07 = Convert.ToSingle(this.textBox15.Text.Trim());
                    marketMemos.I_P08 = Convert.ToSingle(this.textBox17.Text.Trim());
                    marketMemos.I_P09 = Convert.ToSingle(this.textBox19.Text.Trim());
                    marketMemos.I_P10 = Convert.ToSingle(this.textBox21.Text.Trim());
                    marketMemos.I_P11 = Convert.ToSingle(this.textBox23.Text.Trim());
                    marketMemos.I_P12 = Convert.ToSingle(this.textBox25.Text.Trim());
                    marketMemos.I_P13 = Convert.ToSingle(this.textBox27.Text.Trim());
                    marketMemos.I_P14 = Convert.ToSingle(this.textBox29.Text.Trim());
                    marketMemos.I_P15 = Convert.ToSingle(this.textBox31.Text.Trim());
                    marketMemos.I_P16 = Convert.ToSingle(this.textBox38.Text.Trim());

                    marketMemos.I_Q01 = Convert.ToSingle(this.textBox2.Text.Trim());
                    marketMemos.I_Q02 = Convert.ToSingle(this.textBox4.Text.Trim());
                    marketMemos.I_Q03 = Convert.ToSingle(this.textBox6.Text.Trim());
                    marketMemos.I_Q04 = Convert.ToSingle(this.textBox8.Text.Trim());
                    marketMemos.I_Q05 = Convert.ToSingle(this.textBox10.Text.Trim());
                    marketMemos.I_Q06 = Convert.ToSingle(this.textBox12.Text.Trim());
                    marketMemos.I_Q07 = Convert.ToSingle(this.textBox14.Text.Trim());
                    marketMemos.I_Q08 = Convert.ToSingle(this.textBox16.Text.Trim());
                    marketMemos.I_Q09 = Convert.ToSingle(this.textBox18.Text.Trim());
                    marketMemos.I_Q10 = Convert.ToSingle(this.textBox20.Text.Trim());
                    marketMemos.I_Q11 = Convert.ToSingle(this.textBox22.Text.Trim());
                    marketMemos.I_Q12 = Convert.ToSingle(this.textBox24.Text.Trim());
                    marketMemos.I_Q13 = Convert.ToSingle(this.textBox26.Text.Trim());
                    marketMemos.I_Q14 = Convert.ToSingle(this.textBox28.Text.Trim());
                    marketMemos.I_Q15 = Convert.ToSingle(this.textBox30.Text.Trim());
                    marketMemos.I_Q16 = Convert.ToSingle(this.textBox54.Text.Trim());

                    marketMemos.I_ST01 = Convert.ToSingle(this.label9.Text.Trim());
                    marketMemos.I_ST02 = Convert.ToSingle(this.label13.Text.Trim());
                    marketMemos.I_ST03 = Convert.ToSingle(this.label17.Text.Trim());
                    marketMemos.I_ST04 = Convert.ToSingle(this.label24.Text.Trim());
                    marketMemos.I_ST05 = Convert.ToSingle(this.label28.Text.Trim());
                    marketMemos.I_ST06 = Convert.ToSingle(this.label32.Text.Trim());
                    marketMemos.I_ST07 = Convert.ToSingle(this.label36.Text.Trim());
                    marketMemos.I_ST08 = Convert.ToSingle(this.label40.Text.Trim());
                    marketMemos.I_ST09 = Convert.ToSingle(this.label44.Text.Trim());
                    marketMemos.I_ST10 = Convert.ToSingle(this.label48.Text.Trim());
                    marketMemos.I_ST11 = Convert.ToSingle(this.label52.Text.Trim());
                    marketMemos.I_ST12 = Convert.ToSingle(this.label56.Text.Trim());
                    marketMemos.I_ST13 = Convert.ToSingle(this.label60.Text.Trim());
                    marketMemos.I_ST14 = Convert.ToSingle(this.label64.Text.Trim());
                    marketMemos.I_ST15 = Convert.ToSingle(this.label68.Text.Trim());
                    marketMemos.I_ST16 = Convert.ToSingle(this.label76.Text.Trim());

                    marketMemos.R_Inv01 = Convert.ToSingle(this.textBox56.Text.Trim());
                    marketMemos.R_Inv02 = Convert.ToSingle(this.textBox57.Text.Trim());
                    marketMemos.R_Inv03 = Convert.ToSingle(this.textBox58.Text.Trim());
                    marketMemos.R_Inv04 = Convert.ToSingle(this.textBox59.Text.Trim());
                    marketMemos.R_Inv05 = Convert.ToSingle(this.textBox60.Text.Trim());
                    marketMemos.R_Inv06 = Convert.ToSingle(this.textBox61.Text.Trim());
                    marketMemos.R_Inv07 = Convert.ToSingle(this.textBox62.Text.Trim());
                    marketMemos.R_Inv08 = Convert.ToSingle(this.textBox63.Text.Trim());
                    marketMemos.R_Inv09 = Convert.ToSingle(this.textBox64.Text.Trim());
                    marketMemos.R_Inv10 = Convert.ToSingle(this.textBox65.Text.Trim());
                    marketMemos.R_Inv11 = Convert.ToSingle(this.textBox66.Text.Trim());
                    marketMemos.R_Inv12 = Convert.ToSingle(this.textBox67.Text.Trim());
                    marketMemos.R_Inv13 = Convert.ToSingle(this.textBox68.Text.Trim());
                    marketMemos.R_Inv14 = Convert.ToSingle(this.textBox69.Text.Trim());
                    marketMemos.R_Inv15 = Convert.ToSingle(this.textBox70.Text.Trim());
                    marketMemos.R_Inv16 = Convert.ToSingle(this.textBox71.Text.Trim());
                    marketMemos.R_Inv17 = Convert.ToSingle(this.textBox89.Text.Trim());
                    marketMemos.R_Inv18 = Convert.ToSingle(this.textBox91.Text.Trim());
                    marketMemos.R_Inv19 = Convert.ToSingle(this.textBox110.Text.Trim());
                    marketMemos.R_Inv20 = Convert.ToSingle(this.textBox111.Text.Trim());
                    marketMemos.R_Inv21 = Convert.ToSingle(this.textBox112.Text.Trim());
                    marketMemos.R_Inv22 = Convert.ToSingle(this.textBox113.Text.Trim());
                    marketMemos.R_Inv23 = Convert.ToSingle(this.textBox114.Text.Trim());
                    marketMemos.R_Inv24 = Convert.ToSingle(this.textBox115.Text.Trim());
                    marketMemos.Mem_Updt_Person = this.label249.Text.Trim();
                    marketMemos.Mem_ID = this.label224.Text.Trim();
                    bool isInserted = _bLLayer.updtMarketMemos(marketMemos);
                    if (isInserted)
                    {
                        MessageBox.Show(string.Concat("Successfull Update - ", this.label224.Text));
                        this.fillData();
                        this.button21.Visible = true;
                        this.button15.Text = "New";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error : " + ex.Message);
                }
            }
        }
        private void button21_Click(object sender, EventArgs e)
        {
            try
            {
                string Mem_ID = this.label224.Text.Trim();
                if (string.IsNullOrWhiteSpace(Mem_ID))
                {
                    MessageBox.Show($"Memo ID :[{this.label224.Text}]\n\nSelection Needed", "Error Delete");
                    return;
                }
                MarketMemos marketMemos = new MarketMemos();
                marketMemos.Mem_Del_Person = this.label249.Text.Trim();
                marketMemos.Mem_ID = this.label224.Text.Trim();
                bool isInserted = _bLLayer.DelMarketMemos(Mem_ID, marketMemos);
                if (isInserted)
                {
                    MessageBox.Show($"Successfull Deleted - [{this.label224.Text}]");
                    this.BalankFldMarMem();
                    this.fillData();
                    this.button15.Text = "New";
                    this.button21.Visible = false;
                }
            }
            catch (Exception ex)
            {
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
                    BikeInfo insBikeInfo = new BikeInfo();
                    insBikeInfo.B_ID = this.textBox98.Text.Trim();
                    insBikeInfo.B_Chng_Date = Convert.ToDateTime(this.dateTimePicker6.Text.Trim());
                    insBikeInfo.B_KM_ODO = Convert.ToSingle(this.textBox129.Text.Trim());
                    insBikeInfo.B_Mobile_Go = Convert.ToSingle(this.textBox128.Text.Trim());
                    insBikeInfo.B_Next_ODO = Convert.ToSingle(this.label257.Text.Trim());
                    insBikeInfo.B_Insrt_Person = this.label249.Text.Trim();
                    bool isInserted = _bLLayer.InsBikeInfo(insBikeInfo);
                    if (isInserted)
                    {
                        this.fillDataBike();
                        MessageBox.Show(string.Concat("Successfull Bike Info Added"));
                        this.textBox129.Text = "";
                        this.textBox128.Text = "";
                        this.label257.Text = "0";
                    }
                }
                catch (Exception ex)
                {
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
                string query = $"INSERT INTO Images (img_ID, ImageData) VALUES (?, ?)";
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
                        DailyAnt dailyAnt = new DailyAnt();
                        dailyAnt.DA_ID = textBox132.Text.Trim();
                        dailyAnt.DA_Date = Convert.ToDateTime(this.dateTimePicker8.Text.Trim());
                        dailyAnt.DA_FPAmount = Convert.ToSingle(this.textBox133.Text.Trim());
                        dailyAnt.DA_SPAmount = Convert.ToSingle(this.textBox134.Text.Trim());
                        dailyAnt.NotTaken = Convert.ToSingle(this.textBox134.Text.Trim());
                        dailyAnt.DA_Data = "NTKN";
                        dailyAnt.DA_Insrt_Person = this.label249.Text.Trim();
                        bool isInserted = _bLLayer.InsDailyAnt(dailyAnt);
                        if (isInserted)
                        {
                            MessageBox.Show(string.Concat("Successfull Daily AntData Added"));
                            this.fillDailyData();
                            this.totalDailyData();
                            this.textBox133.ReadOnly = true;
                            this.textBox133.Text = "";
                            this.textBox132.Text = "";
                            this.button31.Text = "Add";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error : " + ex.Message);
                    }
                }
            }
            else if (this.button31.Text == "Updt")
            {
                try
                {
                    DailyAnt dailyAnt = new DailyAnt();
                    dailyAnt.DA_FPAmount = Convert.ToSingle(this.textBox133.Text.Trim());
                    dailyAnt.DA_SPAmount = Convert.ToSingle(this.textBox134.Text.Trim());
                    dailyAnt.NotTaken = Convert.ToSingle(this.textBox134.Text.Trim());
                    dailyAnt.DA_Date = Convert.ToDateTime(this.dateTimePicker8.Text.Trim());
                    dailyAnt.DA_Updt_Person = this.label249.Text.Trim();
                    dailyAnt.DA_ID = this.label277.Text.Trim();
                    bool isUpdated = _bLLayer.UpdtDailyAnt(dailyAnt);
                    if (isUpdated)
                    {
                        MessageBox.Show(string.Concat("Successfull Update AntDaily Get"));
                        this.fillDailyData();
                        this.totalDailyData();
                        this.textBox133.ReadOnly = true;
                        this.textBox133.Text = "";
                        this.label277.Text = "0";
                        this.label279.Text = "0";
                        this.label278.Text = "0";
                        this.label276.Text = "0";
                        this.button31.Text = "Add";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error : " + ex.Message);
                }
            }
        }
        private void button33_Click(object sender, EventArgs e)
        {
            try
            {
                DailyAnt dailyAnt = new DailyAnt();
                dailyAnt.DA_Data = "TKN";
                dailyAnt.TakenDate = Convert.ToDateTime(this.DltDate);
                dailyAnt.DA_Del_Person = this.label249.Text.Trim();
                dailyAnt.DA_ID = this.label277.Text.Trim();
                bool isDeleted = _bLLayer.delDailyAnt(dailyAnt);
                if (isDeleted)
                {
                    MessageBox.Show($"Successfull Deleted - [{this.label277.Text}]");
                    this.fillDailyData();
                    this.AmtCrDataView();
                    if (this.dataGridView5.RowCount > 0)
                    {
                        this.totalDailyData();
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void button32_Click(object sender, EventArgs e)
        {
            try
            {
                DailyAnt dailyAnt = new DailyAnt();
                dailyAnt.DA_ID = this.label268.Text.Trim();
                bool isDeleted = _bLLayer.delReDailyAnt(dailyAnt);
                if (isDeleted)
                {
                    MessageBox.Show($"Successfull Deleted - [{this.label268.Text}]");
                    this.fillDailyData();
                    this.button32.Visible = false;
                }
            }
            catch (Exception ex)
            {
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
                        DailySaving insdailySav = new DailySaving();
                        insdailySav.DS_ID = this.textBox137.Text.Trim();
                        insdailySav.DS_Date = Convert.ToDateTime(this.dateTimePicker7.Text.Trim());
                        insdailySav.DS_FPAmount = Convert.ToSingle(this.textBox131.Text.Trim());
                        insdailySav.DS_SPAmount = Convert.ToSingle(this.textBox135.Text.Trim());
                        insdailySav.DS_TPAmount = Convert.ToSingle(this.textBox135.Text.Trim());
                        insdailySav.NotTaken = Convert.ToSingle(this.textBox135.Text.Trim());
                        insdailySav.DS_Data = "NTKN";
                        insdailySav.DS_Insrt_Person = this.label249.Text.Trim();
                        bool isInserted = _bLLayer.InsDailySaving(insdailySav);
                        if (isInserted)
                        {
                            MessageBox.Show(string.Concat("Successfull Added Daily Saving Amount"));
                            this.DailySavin();
                            this.totalDailyData();
                            this.textBox131.ReadOnly = true;
                            this.textBox131.Text = "";
                            this.textBox137.Text = "";
                            this.buttonS24.Text = "Add";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error : " + ex.Message);
                    }
                }
            }
            else if (this.buttonS24.Text == "Updt")
            {
                try
                {
                    DailySaving updtdailySav = new DailySaving();
                    updtdailySav.DS_FPAmount = Convert.ToSingle(this.textBox131.Text.Trim());
                    updtdailySav.DS_Date = Convert.ToDateTime(this.dateTimePicker7.Text.Trim());
                    updtdailySav.DS_SPAmount = Convert.ToSingle(this.textBox135.Text.Trim());
                    updtdailySav.DS_TPAmount = Convert.ToSingle(this.textBox135.Text.Trim());
                    updtdailySav.NotTaken = Convert.ToSingle(this.textBox135.Text.Trim());
                    updtdailySav.DS_Updt_Person = this.label249.Text.Trim();
                    updtdailySav.DS_ID = this.label292.Text.Trim();
                    bool isUpdated = _bLLayer.UpdtDailySaving(updtdailySav);
                    if (isUpdated)
                    {
                        MessageBox.Show(string.Concat("Successfull Update Daily Saving"));
                        this.DailySavin();
                        this.totalDailyData();
                        this.textBox131.ReadOnly = true;
                        this.textBox131.Text = "";
                        this.label292.Text = "0";
                        this.label289.Text = "0";
                        this.buttonS24.Text = "Add";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error : " + ex.Message);
                }
            }
        }
        private void button26_Click(object sender, EventArgs e)
        {
            try
            {
                _bLLayer.SynchronizeMarkMemData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An Error Data Synchronization {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button27_Click(object sender, EventArgs e)
        {
            try
            {
                _bLLayer.SynchronizeInstallData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An Error Data Synchronization {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button36_Click(object sender, EventArgs e)
        {
            try
            {
                _bLLayer.SynchronizeCrCardData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An Error Data Synchronization {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button29_Click(object sender, EventArgs e)
        {
            try
            {
                _bLLayer.SynchronizeDailyAchiveData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An Error Data Synchronization {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button34_Click(object sender, EventArgs e)
        {
            try
            {
                _bLLayer.SynchronizeMonthlyData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An Error Data Synchronization {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button28_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                try
                {
                    _bLLayer.SynchronizeData();
                    MessageBox.Show($"Successfully Data Synchronization", "Success");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An Error Data Synchronization {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
        private void button38_Click(object sender, EventArgs e)
        {
            if (this.button38.Text == "Add")
            {
                this.textBox162.ReadOnly = false;
                this.textBox146.Focus();
                TextBox textBox = this.textBox241;
                string[] strArrays = new string[] { "MT", null, null, null, null };
                int date = DateTime.Now.Day;
                int month = DateTime.Now.Month;
                int millis = DateTime.Now.Millisecond;
                strArrays[2] = date.ToString();
                strArrays[3] = month.ToString();
                strArrays[4] = millis.ToString();
                textBox.Text = string.Concat(strArrays);
                this.button38.Text = "Save";
                this.BalankFldMonthly();
            }
            else if (this.button38.Text == "Save")
            {
                MonthlyTake monthlyTake = new MonthlyTake();
                monthlyTake.MT_ID = this.textBox241.Text;
                monthlyTake.MT_Date = Convert.ToDateTime(this.dateTimePicker9.Text);
                monthlyTake.MT_TotalTK = Convert.ToSingle(this.textBox163.Text);
                monthlyTake.MT_Giv_TK = Convert.ToSingle(this.textBox162.Text);
                monthlyTake.MT_LS_TK = Convert.ToSingle(this.label294.Text);
                monthlyTake.T01 = Convert.ToSingle(this.textBox146.Text.Trim());
                monthlyTake.T02 = Convert.ToSingle(this.textBox145.Text.Trim());
                monthlyTake.T03 = Convert.ToSingle(this.textBox144.Text.Trim());
                monthlyTake.T04 = Convert.ToSingle(this.textBox143.Text.Trim());
                monthlyTake.T05 = Convert.ToSingle(this.textBox142.Text.Trim());
                monthlyTake.T06 = Convert.ToSingle(this.textBox141.Text.Trim());
                monthlyTake.T07 = Convert.ToSingle(this.textBox140.Text.Trim());
                monthlyTake.T08 = Convert.ToSingle(this.textBox139.Text.Trim());
                monthlyTake.T09 = Convert.ToSingle(this.textBox138.Text.Trim());
                monthlyTake.T10 = Convert.ToSingle(this.textBox161.Text.Trim());
                monthlyTake.T11 = Convert.ToSingle(this.textBox160.Text.Trim());
                monthlyTake.T12 = Convert.ToSingle(this.textBox159.Text.Trim());
                monthlyTake.T13 = Convert.ToSingle(this.textBox158.Text.Trim());
                monthlyTake.T14 = Convert.ToSingle(this.textBox157.Text.Trim());
                monthlyTake.T15 = Convert.ToSingle(this.textBox156.Text.Trim());
                monthlyTake.T16 = Convert.ToSingle(this.textBox155.Text.Trim());
                monthlyTake.T17 = Convert.ToSingle(this.textBox154.Text.Trim());
                monthlyTake.T18 = Convert.ToSingle(this.textBox153.Text.Trim());
                monthlyTake.T19 = Convert.ToSingle(this.textBox152.Text.Trim());
                monthlyTake.T20 = Convert.ToSingle(this.textBox151.Text.Trim());
                monthlyTake.T21 = Convert.ToSingle(this.textBox172.Text.Trim());
                monthlyTake.T22 = Convert.ToSingle(this.textBox173.Text.Trim());
                monthlyTake.T23 = Convert.ToSingle(this.textBox174.Text.Trim());
                monthlyTake.T24 = Convert.ToSingle(this.textBox175.Text.Trim());
                monthlyTake.T25 = Convert.ToSingle(this.textBox176.Text.Trim());
                monthlyTake.T26 = Convert.ToSingle(this.textBox177.Text.Trim());
                monthlyTake.T27 = Convert.ToSingle(this.textBox178.Text.Trim());
                monthlyTake.T28 = Convert.ToSingle(this.textBox179.Text.Trim());
                monthlyTake.T29 = Convert.ToSingle(this.textBox180.Text.Trim());
                monthlyTake.T30 = Convert.ToSingle(this.textBox148.Text.Trim());
                monthlyTake.T31 = Convert.ToSingle(this.textBox149.Text.Trim());
                monthlyTake.T32 = Convert.ToSingle(this.textBox150.Text.Trim());
                monthlyTake.T33 = Convert.ToSingle(this.textBox164.Text.Trim());
                monthlyTake.T34 = Convert.ToSingle(this.textBox165.Text.Trim());
                monthlyTake.T35 = Convert.ToSingle(this.textBox166.Text.Trim());
                monthlyTake.T36 = Convert.ToSingle(this.textBox167.Text.Trim());
                monthlyTake.T37 = Convert.ToSingle(this.textBox168.Text.Trim());
                monthlyTake.T38 = Convert.ToSingle(this.textBox169.Text.Trim());
                monthlyTake.T39 = Convert.ToSingle(this.textBox170.Text.Trim());
                monthlyTake.T40 = Convert.ToSingle(this.textBox171.Text.Trim());
                monthlyTake.T41 = Convert.ToSingle(this.textBox192.Text.Trim());
                monthlyTake.T42 = Convert.ToSingle(this.textBox193.Text.Trim());
                monthlyTake.T43 = Convert.ToSingle(this.textBox194.Text.Trim());
                monthlyTake.T44 = Convert.ToSingle(this.textBox195.Text.Trim());
                monthlyTake.T45 = Convert.ToSingle(this.textBox196.Text.Trim());
                monthlyTake.T46 = Convert.ToSingle(this.textBox197.Text.Trim());
                monthlyTake.T47 = Convert.ToSingle(this.textBox198.Text.Trim());
                monthlyTake.T48 = Convert.ToSingle(this.textBox199.Text.Trim());
                monthlyTake.T49 = Convert.ToSingle(this.textBox200.Text.Trim());
                monthlyTake.T50 = Convert.ToSingle(this.textBox181.Text.Trim());
                monthlyTake.T51 = Convert.ToSingle(this.textBox182.Text.Trim());
                monthlyTake.T52 = Convert.ToSingle(this.textBox183.Text.Trim());
                monthlyTake.T53 = Convert.ToSingle(this.textBox184.Text.Trim());
                monthlyTake.T54 = Convert.ToSingle(this.textBox185.Text.Trim());
                monthlyTake.T55 = Convert.ToSingle(this.textBox186.Text.Trim());
                monthlyTake.T56 = Convert.ToSingle(this.textBox187.Text.Trim());
                monthlyTake.T57 = Convert.ToSingle(this.textBox188.Text.Trim());
                monthlyTake.T58 = Convert.ToSingle(this.textBox189.Text.Trim());
                monthlyTake.T59 = Convert.ToSingle(this.textBox190.Text.Trim());
                monthlyTake.T60 = Convert.ToSingle(this.textBox191.Text.Trim());
                monthlyTake.T61 = Convert.ToSingle(this.textBox212.Text.Trim());
                monthlyTake.T62 = Convert.ToSingle(this.textBox213.Text.Trim());
                monthlyTake.T63 = Convert.ToSingle(this.textBox214.Text.Trim());
                monthlyTake.T64 = Convert.ToSingle(this.textBox215.Text.Trim());
                monthlyTake.T65 = Convert.ToSingle(this.textBox216.Text.Trim());
                monthlyTake.T66 = Convert.ToSingle(this.textBox217.Text.Trim());
                monthlyTake.T67 = Convert.ToSingle(this.textBox218.Text.Trim());
                monthlyTake.T68 = Convert.ToSingle(this.textBox219.Text.Trim());
                monthlyTake.T69 = Convert.ToSingle(this.textBox220.Text.Trim());
                monthlyTake.T70 = Convert.ToSingle(this.textBox201.Text.Trim());
                monthlyTake.T71 = Convert.ToSingle(this.textBox202.Text.Trim());
                monthlyTake.T72 = Convert.ToSingle(this.textBox203.Text.Trim());
                monthlyTake.T73 = Convert.ToSingle(this.textBox204.Text.Trim());
                monthlyTake.T74 = Convert.ToSingle(this.textBox205.Text.Trim());
                monthlyTake.T75 = Convert.ToSingle(this.textBox206.Text.Trim());
                monthlyTake.T76 = Convert.ToSingle(this.textBox207.Text.Trim());
                monthlyTake.T77 = Convert.ToSingle(this.textBox208.Text.Trim());
                monthlyTake.T78 = Convert.ToSingle(this.textBox209.Text.Trim());
                monthlyTake.T79 = Convert.ToSingle(this.textBox210.Text.Trim());
                monthlyTake.T80 = Convert.ToSingle(this.textBox211.Text.Trim());
                monthlyTake.T81 = Convert.ToSingle(this.textBox232.Text.Trim());
                monthlyTake.T82 = Convert.ToSingle(this.textBox233.Text.Trim());
                monthlyTake.T83 = Convert.ToSingle(this.textBox234.Text.Trim());
                monthlyTake.T84 = Convert.ToSingle(this.textBox235.Text.Trim());
                monthlyTake.T85 = Convert.ToSingle(this.textBox236.Text.Trim());
                monthlyTake.T86 = Convert.ToSingle(this.textBox237.Text.Trim());
                monthlyTake.T87 = Convert.ToSingle(this.textBox238.Text.Trim());
                monthlyTake.T88 = Convert.ToSingle(this.textBox239.Text.Trim());
                monthlyTake.T89 = Convert.ToSingle(this.textBox240.Text.Trim());
                monthlyTake.T90 = Convert.ToSingle(this.textBox221.Text.Trim());
                monthlyTake.T91 = Convert.ToSingle(this.textBox222.Text.Trim());
                monthlyTake.T92 = Convert.ToSingle(this.textBox223.Text.Trim());
                monthlyTake.T93 = Convert.ToSingle(this.textBox224.Text.Trim());
                monthlyTake.T94 = Convert.ToSingle(this.textBox225.Text.Trim());
                monthlyTake.T95 = Convert.ToSingle(this.textBox226.Text.Trim());
                monthlyTake.T96 = Convert.ToSingle(this.textBox227.Text.Trim());
                monthlyTake.T97 = Convert.ToSingle(this.textBox228.Text.Trim());
                monthlyTake.T98 = Convert.ToSingle(this.textBox229.Text.Trim());
                monthlyTake.T99 = Convert.ToSingle(this.textBox230.Text.Trim());
                monthlyTake.T100 = Convert.ToSingle(this.textBox231.Text.Trim());
                monthlyTake.MTDT_V = "NTKN";
                monthlyTake.MT_Insrt_Person = this.label249.Text.Trim();
                bool isInserted = _bLLayer.InsMonthlyTake(monthlyTake);
                if (isInserted)
                {
                    MessageBox.Show(string.Concat("Successfull Monthly Take Added"));
                    this.fillyMonthlyData();
                    this.textBox162.ReadOnly = true;
                    this.button38.Text = "Add";
                }
            }
            else if (this.button38.Text == "Updt")
            {
                MonthlyTake monthlyTake = new MonthlyTake();
                monthlyTake.MT_TotalTK = Convert.ToSingle(this.textBox163.Text);
                monthlyTake.MT_Giv_TK = Convert.ToSingle(this.textBox162.Text);
                monthlyTake.MT_LS_TK = Convert.ToSingle(this.label294.Text);
                monthlyTake.T01 = Convert.ToSingle(this.textBox146.Text.Trim());
                monthlyTake.T02 = Convert.ToSingle(this.textBox145.Text.Trim());
                monthlyTake.T03 = Convert.ToSingle(this.textBox144.Text.Trim());
                monthlyTake.T04 = Convert.ToSingle(this.textBox143.Text.Trim());
                monthlyTake.T05 = Convert.ToSingle(this.textBox142.Text.Trim());
                monthlyTake.T06 = Convert.ToSingle(this.textBox141.Text.Trim());
                monthlyTake.T07 = Convert.ToSingle(this.textBox140.Text.Trim());
                monthlyTake.T08 = Convert.ToSingle(this.textBox139.Text.Trim());
                monthlyTake.T09 = Convert.ToSingle(this.textBox138.Text.Trim());
                monthlyTake.T10 = Convert.ToSingle(this.textBox161.Text.Trim());
                monthlyTake.T11 = Convert.ToSingle(this.textBox160.Text.Trim());
                monthlyTake.T12 = Convert.ToSingle(this.textBox159.Text.Trim());
                monthlyTake.T13 = Convert.ToSingle(this.textBox158.Text.Trim());
                monthlyTake.T14 = Convert.ToSingle(this.textBox157.Text.Trim());
                monthlyTake.T15 = Convert.ToSingle(this.textBox156.Text.Trim());
                monthlyTake.T16 = Convert.ToSingle(this.textBox155.Text.Trim());
                monthlyTake.T17 = Convert.ToSingle(this.textBox154.Text.Trim());
                monthlyTake.T18 = Convert.ToSingle(this.textBox153.Text.Trim());
                monthlyTake.T19 = Convert.ToSingle(this.textBox152.Text.Trim());
                monthlyTake.T20 = Convert.ToSingle(this.textBox151.Text.Trim());
                monthlyTake.T21 = Convert.ToSingle(this.textBox172.Text.Trim());
                monthlyTake.T22 = Convert.ToSingle(this.textBox173.Text.Trim());
                monthlyTake.T23 = Convert.ToSingle(this.textBox174.Text.Trim());
                monthlyTake.T24 = Convert.ToSingle(this.textBox175.Text.Trim());
                monthlyTake.T25 = Convert.ToSingle(this.textBox176.Text.Trim());
                monthlyTake.T26 = Convert.ToSingle(this.textBox177.Text.Trim());
                monthlyTake.T27 = Convert.ToSingle(this.textBox178.Text.Trim());
                monthlyTake.T28 = Convert.ToSingle(this.textBox179.Text.Trim());
                monthlyTake.T29 = Convert.ToSingle(this.textBox180.Text.Trim());
                monthlyTake.T30 = Convert.ToSingle(this.textBox148.Text.Trim());
                monthlyTake.T31 = Convert.ToSingle(this.textBox149.Text.Trim());
                monthlyTake.T32 = Convert.ToSingle(this.textBox150.Text.Trim());
                monthlyTake.T33 = Convert.ToSingle(this.textBox164.Text.Trim());
                monthlyTake.T34 = Convert.ToSingle(this.textBox165.Text.Trim());
                monthlyTake.T35 = Convert.ToSingle(this.textBox166.Text.Trim());
                monthlyTake.T36 = Convert.ToSingle(this.textBox167.Text.Trim());
                monthlyTake.T37 = Convert.ToSingle(this.textBox168.Text.Trim());
                monthlyTake.T38 = Convert.ToSingle(this.textBox169.Text.Trim());
                monthlyTake.T39 = Convert.ToSingle(this.textBox170.Text.Trim());
                monthlyTake.T40 = Convert.ToSingle(this.textBox171.Text.Trim());
                monthlyTake.T41 = Convert.ToSingle(this.textBox192.Text.Trim());
                monthlyTake.T42 = Convert.ToSingle(this.textBox193.Text.Trim());
                monthlyTake.T43 = Convert.ToSingle(this.textBox194.Text.Trim());
                monthlyTake.T44 = Convert.ToSingle(this.textBox195.Text.Trim());
                monthlyTake.T45 = Convert.ToSingle(this.textBox196.Text.Trim());
                monthlyTake.T46 = Convert.ToSingle(this.textBox197.Text.Trim());
                monthlyTake.T47 = Convert.ToSingle(this.textBox198.Text.Trim());
                monthlyTake.T48 = Convert.ToSingle(this.textBox199.Text.Trim());
                monthlyTake.T49 = Convert.ToSingle(this.textBox200.Text.Trim());
                monthlyTake.T50 = Convert.ToSingle(this.textBox181.Text.Trim());
                monthlyTake.T51 = Convert.ToSingle(this.textBox182.Text.Trim());
                monthlyTake.T52 = Convert.ToSingle(this.textBox183.Text.Trim());
                monthlyTake.T53 = Convert.ToSingle(this.textBox184.Text.Trim());
                monthlyTake.T54 = Convert.ToSingle(this.textBox185.Text.Trim());
                monthlyTake.T55 = Convert.ToSingle(this.textBox186.Text.Trim());
                monthlyTake.T56 = Convert.ToSingle(this.textBox187.Text.Trim());
                monthlyTake.T57 = Convert.ToSingle(this.textBox188.Text.Trim());
                monthlyTake.T58 = Convert.ToSingle(this.textBox189.Text.Trim());
                monthlyTake.T59 = Convert.ToSingle(this.textBox190.Text.Trim());
                monthlyTake.T60 = Convert.ToSingle(this.textBox191.Text.Trim());
                monthlyTake.T61 = Convert.ToSingle(this.textBox212.Text.Trim());
                monthlyTake.T62 = Convert.ToSingle(this.textBox213.Text.Trim());
                monthlyTake.T63 = Convert.ToSingle(this.textBox214.Text.Trim());
                monthlyTake.T64 = Convert.ToSingle(this.textBox215.Text.Trim());
                monthlyTake.T65 = Convert.ToSingle(this.textBox216.Text.Trim());
                monthlyTake.T66 = Convert.ToSingle(this.textBox217.Text.Trim());
                monthlyTake.T67 = Convert.ToSingle(this.textBox218.Text.Trim());
                monthlyTake.T68 = Convert.ToSingle(this.textBox219.Text.Trim());
                monthlyTake.T69 = Convert.ToSingle(this.textBox220.Text.Trim());
                monthlyTake.T70 = Convert.ToSingle(this.textBox201.Text.Trim());
                monthlyTake.T71 = Convert.ToSingle(this.textBox202.Text.Trim());
                monthlyTake.T72 = Convert.ToSingle(this.textBox203.Text.Trim());
                monthlyTake.T73 = Convert.ToSingle(this.textBox204.Text.Trim());
                monthlyTake.T74 = Convert.ToSingle(this.textBox205.Text.Trim());
                monthlyTake.T75 = Convert.ToSingle(this.textBox206.Text.Trim());
                monthlyTake.T76 = Convert.ToSingle(this.textBox207.Text.Trim());
                monthlyTake.T77 = Convert.ToSingle(this.textBox208.Text.Trim());
                monthlyTake.T78 = Convert.ToSingle(this.textBox209.Text.Trim());
                monthlyTake.T79 = Convert.ToSingle(this.textBox210.Text.Trim());
                monthlyTake.T80 = Convert.ToSingle(this.textBox211.Text.Trim());
                monthlyTake.T81 = Convert.ToSingle(this.textBox232.Text.Trim());
                monthlyTake.T82 = Convert.ToSingle(this.textBox233.Text.Trim());
                monthlyTake.T83 = Convert.ToSingle(this.textBox234.Text.Trim());
                monthlyTake.T84 = Convert.ToSingle(this.textBox235.Text.Trim());
                monthlyTake.T85 = Convert.ToSingle(this.textBox236.Text.Trim());
                monthlyTake.T86 = Convert.ToSingle(this.textBox237.Text.Trim());
                monthlyTake.T87 = Convert.ToSingle(this.textBox238.Text.Trim());
                monthlyTake.T88 = Convert.ToSingle(this.textBox239.Text.Trim());
                monthlyTake.T89 = Convert.ToSingle(this.textBox240.Text.Trim());
                monthlyTake.T90 = Convert.ToSingle(this.textBox221.Text.Trim());
                monthlyTake.T91 = Convert.ToSingle(this.textBox222.Text.Trim());
                monthlyTake.T92 = Convert.ToSingle(this.textBox223.Text.Trim());
                monthlyTake.T93 = Convert.ToSingle(this.textBox224.Text.Trim());
                monthlyTake.T94 = Convert.ToSingle(this.textBox225.Text.Trim());
                monthlyTake.T95 = Convert.ToSingle(this.textBox226.Text.Trim());
                monthlyTake.T96 = Convert.ToSingle(this.textBox227.Text.Trim());
                monthlyTake.T97 = Convert.ToSingle(this.textBox228.Text.Trim());
                monthlyTake.T98 = Convert.ToSingle(this.textBox229.Text.Trim());
                monthlyTake.T99 = Convert.ToSingle(this.textBox230.Text.Trim());
                monthlyTake.T100 = Convert.ToSingle(this.textBox231.Text.Trim());
                monthlyTake.MTDT_V = "TKN";
                monthlyTake.MT_Updt_Person = this.label249.Text.Trim();
                monthlyTake.MT_ID = this.label404.Text.Trim();
                bool isInserted = _bLLayer.UpdtMonthlyTake(monthlyTake);
                if (isInserted)
                {
                    MessageBox.Show(string.Concat($"Successfull Updated - {this.label404.Text.Trim()}"));
                    this.fillyMonthlyData();
                    this.textBox163.Text = "";
                    this.textBox162.Text = "";
                    this.textBox162.ReadOnly = true;
                    this.button38.Text = "Add";
                }
            }
        }
        private void button37_Click(object sender, EventArgs e)
        {
            this.BalankFldMonthly();
            this.button38.Text = "Add";
        }

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
                string marketId = this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                DataTable dataTabledt = _bLLayer.GetMarketDataById(marketId);
                if (dataTabledt.Rows.Count > 0)
                {
                    this.label6.Text = dataTabledt.Rows[0]["M_ID"].ToString();
                    this.textBox1.Text = dataTabledt.Rows[0]["M_Amount"].ToString();
                }
                this.textBox1.ReadOnly = false;
                this.textBox1.Focus();
                this.button1.Text = "Updt";
                this.dateTimePicker1.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string installmentId = this.dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                DataTable dataTable = _bLLayer.GetInstallmentDataById(installmentId);
                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    this.label201.Text = row["I_ID"].ToString();
                    this.label212.Text = row["InsPay"].ToString();
                    this.textBox32.Text = row["InsPay"].ToString();
                    this.textBox32.ReadOnly = false;
                    this.textBox32.Focus();
                }
                this.button4.Text = "Updt";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string givenId = this.dataGridView3.SelectedRows[0].Cells[0].Value.ToString();
                DataTable dataTable = _bLLayer.GetGivenDataById(givenId);
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
            catch (Exception ex)
            {
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
                string dailyId = this.dataGridView5.SelectedRows[0].Cells[0].Value.ToString();
                DataTable dataTable = _bLLayer.GetDailyDataById(dailyId);
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
                string dailycutId = this.dataGridView4.SelectedRows[0].Cells[0].Value.ToString();
                DataTable dataTable = _bLLayer.GetDailyCutById(dailycutId);
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
                string installmntId = this.dataGridView6.SelectedRows[0].Cells[0].Value.ToString();
                DataTable dataTablein = _bLLayer.GetInstallmntById(installmntId);
                if (dataTablein.Rows.Count > 0)
                {
                    DataRow row = dataTablein.Rows[0];
                    this.label218.Text = row[0].ToString();
                    this.label199.Text = row[1].ToString();
                    this.label198.Text = row[2].ToString();
                }
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
                string takeId = this.dataGridView7.SelectedRows[0].Cells[0].Value.ToString();
                DataTable dataTable = _bLLayer.GetIntakeById(takeId);
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
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void dataGridView8_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string expenseId = this.dataGridView8.SelectedRows[0].Cells[0].Value.ToString();
                DataTable dataTable = _bLLayer.GetExpenseById(expenseId);
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
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void dataGridView9_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string savingId = this.dataGridView9.SelectedRows[0].Cells[0].Value.ToString();
                DataTable dataTable = _bLLayer.GetSavingById(savingId);
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
                    this.label243.Text = dataTable.Rows[0][10].ToString();
                    this.button7.Visible = true;
                    this.button7.Text = "Delete S.";
                    this.textBox116.Focus();
                }
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
                string unratedId = this.dataGridView10.SelectedRows[0].Cells[0].Value.ToString();
                DataTable dataTable = _bLLayer.GetUnratedById(unratedId);
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
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void dataGridView11_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string memoId = this.dataGridView11.SelectedRows[0].Cells[0].Value.ToString();
                DataTable dataTable = _bLLayer.GetMarketMemoById(memoId);
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
                    this.button15.Text = "New";
                }
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
                string bikeinfoId = this.dataGridView12.SelectedRows[0].Cells[0].Value.ToString();
                DataTable dataTable = _bLLayer.GetBikeInfoById(bikeinfoId);
                if (dataTable.Rows.Count > 0)
                {
                    this.textBox129.Text = dataTable.Rows[0][0].ToString();
                }
                //string uniqueString = $"OM{DateTime.Now.Day:D2}{DateTime.Now.Month:D2}{DateTime.Now.Millisecond:D4}";
                //this.textBox98.Text = uniqueString;
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
                string dailyAntId = this.dataGridView17.SelectedRows[0].Cells[0].Value.ToString();
                DataTable dataTable = _bLLayer.GetDailyAntById(dailyAntId);
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
            catch (Exception ex)
            {
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
                string dailySaviId = this.dataGridView14.SelectedRows[0].Cells[0].Value.ToString();
                DataTable dataTable = _bLLayer.GetDailySaviById(dailySaviId);
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
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void dataGridView14_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                /*
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
                */
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void dataGridView15_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.textBox162.ReadOnly = false;
                string mntTknId = this.dataGridView15.SelectedRows[0].Cells[0].Value.ToString();  
                DataTable dataTable = _bLLayer.GetMonthDataById(mntTknId);
                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    this.label404.Text = row[0].ToString();

                    this.textBox163.Text = row[2].ToString();
                    this.textBox162.Text = row[3].ToString();

                    this.label10.Text = row[4].ToString();
                    this.textBox146.Text = row[5].ToString();
                    this.textBox145.Text = row[6].ToString();
                    this.textBox144.Text = row[7].ToString();
                    this.textBox143.Text = row[8].ToString();
                    this.textBox142.Text = row[9].ToString();
                    this.textBox141.Text = row[10].ToString();
                    this.textBox140.Text = row[11].ToString();
                    this.textBox139.Text = row[12].ToString();
                    this.textBox138.Text = row[13].ToString();
                    this.textBox161.Text = row[14].ToString();
                    this.textBox160.Text = row[15].ToString();
                    this.textBox159.Text = row[16].ToString();
                    this.textBox158.Text = row[17].ToString();
                    this.textBox157.Text = row[18].ToString();
                    this.textBox156.Text = row[19].ToString();
                    this.textBox155.Text = row[20].ToString();
                    this.textBox154.Text = row[21].ToString();
                    this.textBox153.Text = row[22].ToString();
                    this.textBox152.Text = row[23].ToString();
                    this.textBox151.Text = row[24].ToString();
                    this.textBox172.Text = row[25].ToString();
                    this.textBox173.Text = row[26].ToString();
                    this.textBox174.Text = row[27].ToString();
                    this.textBox175.Text = row[28].ToString();
                    this.textBox176.Text = row[29].ToString();
                    this.textBox177.Text = row[30].ToString();
                    this.textBox178.Text = row[31].ToString();
                    this.textBox179.Text = row[32].ToString();
                    this.textBox180.Text = row[33].ToString();
                    this.textBox148.Text = row[34].ToString();
                    this.textBox149.Text = row[35].ToString();
                    this.textBox150.Text = row[36].ToString();
                    this.textBox164.Text = row[37].ToString();
                    this.textBox165.Text = row[38].ToString();
                    this.textBox166.Text = row[39].ToString();
                    this.textBox167.Text = row[40].ToString();
                    this.textBox168.Text = row[41].ToString();
                    this.textBox169.Text = row[42].ToString();
                    this.textBox170.Text = row[43].ToString();
                    this.textBox171.Text = row[44].ToString();
                    this.textBox192.Text = row[45].ToString();
                    this.textBox193.Text = row[46].ToString();
                    this.textBox194.Text = row[47].ToString();
                    this.textBox195.Text = row[48].ToString();
                    this.textBox196.Text = row[49].ToString();
                    this.textBox197.Text = row[50].ToString();
                    this.textBox198.Text = row[51].ToString();
                    this.textBox199.Text = row[52].ToString();
                    this.textBox200.Text = row[53].ToString();
                    this.textBox181.Text = row[54].ToString();
                    this.textBox182.Text = row[55].ToString();
                    this.textBox183.Text = row[56].ToString();
                    this.textBox184.Text = row[57].ToString();
                    this.textBox185.Text = row[58].ToString();
                    this.textBox186.Text = row[59].ToString();
                    this.textBox187.Text = row[60].ToString();
                    this.textBox188.Text = row[61].ToString();
                    this.textBox189.Text = row[62].ToString();
                    this.textBox190.Text = row[63].ToString();
                    this.textBox191.Text = row[64].ToString();
                    this.textBox212.Text = row[65].ToString();
                    this.textBox213.Text = row[66].ToString();
                    this.textBox214.Text = row[67].ToString();
                    this.textBox215.Text = row[68].ToString();
                    this.textBox216.Text = row[69].ToString();
                    this.textBox217.Text = row[70].ToString();
                    this.textBox218.Text = row[71].ToString();
                    this.textBox219.Text = row[72].ToString();
                    this.textBox220.Text = row[73].ToString();
                    this.textBox201.Text = row[74].ToString();
                    this.textBox202.Text = row[75].ToString();
                    this.textBox203.Text = row[76].ToString();
                    this.textBox204.Text = row[77].ToString();
                    this.textBox205.Text = row[78].ToString();
                    this.textBox206.Text = row[79].ToString();
                    this.textBox207.Text = row[80].ToString();
                    this.textBox208.Text = row[81].ToString();
                    this.textBox209.Text = row[82].ToString();
                    this.textBox210.Text = row[83].ToString();
                    this.textBox211.Text = row[84].ToString();
                    this.textBox232.Text = row[85].ToString();
                    this.textBox233.Text = row[86].ToString();
                    this.textBox234.Text = row[87].ToString();
                    this.textBox235.Text = row[88].ToString();
                    this.textBox236.Text = row[89].ToString();
                    this.textBox237.Text = row[90].ToString();
                    this.textBox238.Text = row[91].ToString();
                    this.textBox239.Text = row[92].ToString();
                    this.textBox240.Text = row[93].ToString();
                    this.textBox221.Text = row[94].ToString();
                    this.textBox222.Text = row[95].ToString();
                    this.textBox223.Text = row[96].ToString();
                    this.textBox224.Text = row[97].ToString();
                    this.textBox225.Text = row[98].ToString();
                    this.textBox226.Text = row[99].ToString();
                    this.textBox227.Text = row[100].ToString();
                    this.textBox228.Text = row[101].ToString();
                    this.textBox229.Text = row[102].ToString();
                    this.textBox230.Text = row[103].ToString();
                    this.textBox231.Text = row[104].ToString();
                }
                else
                {
                    this.button38.Text = "Add";
                }
                this.button38.Text = "Updt";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        //------------------------------All Event Work---------------------------
        //-----------------------------------------------------------------------
        #region All_TextBox_Event_Work
        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.textBox.Text.Trim() == "*1355*" || this.textBox.Text.Trim() == "shohel" || this.textBox.Text.Trim() == "13")
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
                    string givenTo = this.textBox107.Text.Trim();
                    DataSet savingsData = _bLLayer.GetGivenByReceiver(givenTo);
                    if (savingsData.Tables["TotalGiven"].Rows.Count > 0 && savingsData.Tables["GivenDetails"].Rows.Count > 0)
                    {
                        this.label231.Text = savingsData.Tables["TotalGiven"].Rows[0]["Total"].ToString();
                        this.dataGridView13.DataSource = savingsData.Tables["GivenDetails"].DefaultView;
                    }
                    else
                    {
                        this.label231.Text = string.Empty;
                    }
                    this.dataGridView13.Visible = true;
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
                    string takeTo = this.textBox124.Text.Trim();
                    DataSet savingsData = _bLLayer.GetTakenByReceiver(takeTo);
                    if (savingsData.Tables["TotalTaken"].Rows.Count > 0 && savingsData.Tables["TakenDetails"].Rows.Count > 0)
                    {
                        this.label233.Text = savingsData.Tables["TotalTaken"].Rows[0]["Total"].ToString();
                        this.dataGridView13.DataSource = savingsData.Tables["TakenDetails"].DefaultView;
                    }
                    else
                    {
                        this.label233.Text = string.Empty;
                    }
                    this.dataGridView13.Visible = true;
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
                    string expenseTo = this.textBox130.Text.Trim();
                    DataSet savingsData = _bLLayer.GetExpenseByReceiver(expenseTo);
                    if (savingsData.Tables["TotalExpense"].Rows.Count > 0 && savingsData.Tables["ExpenseDetails"].Rows.Count > 0)
                    {
                        this.label250.Text = savingsData.Tables["TotalExpense"].Rows[0]["Total"].ToString();
                        this.dataGridView13.DataSource = savingsData.Tables["ExpenseDetails"].DefaultView;
                    }
                    else
                    {
                        this.label250.Text = string.Empty;
                    }
                    this.dataGridView13.Visible = true;
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
                    string savingTo = this.textBox125.Text.Trim();
                    DataSet savingsData = _bLLayer.GetSavingsByReceiver(savingTo);
                    if (savingsData.Tables["TotalSavings"].Rows.Count > 0 && savingsData.Tables["SavingsDetails"].Rows.Count > 0)
                    {
                        this.label235.Text = savingsData.Tables["TotalSavings"].Rows[0]["Total"].ToString();
                        this.dataGridView13.DataSource = savingsData.Tables["SavingsDetails"].DefaultView;
                    }
                    else
                    {
                        this.label235.Text = string.Empty;
                    }
                    this.dataGridView13.Visible = true;
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
                    string unratedTo = this.textBox126.Text.Trim();
                    DataSet savingsData = _bLLayer.GetUnrateByReceiver(unratedTo);
                    if (savingsData.Tables["TotalUnrated"].Rows.Count > 0 && savingsData.Tables["UnratedDetails"].Rows.Count > 0)
                    {
                        this.label237.Text = savingsData.Tables["TotalUnrated"].Rows[0]["Total"].ToString();
                        this.dataGridView13.DataSource = savingsData.Tables["UnratedDetails"].DefaultView;
                    }
                    else
                    {
                        this.label237.Text = string.Empty;
                    }
                    this.dataGridView13.Visible = true;
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
                    catch (Exception)
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
                if (!string.IsNullOrWhiteSpace(this.textBox117.Text))
                {
                    double num = double.Parse(this.label111.Text.Trim());
                    double num1 = double.Parse(this.textBox117.Text.Trim());
                    double result = this.checkBox5.Checked ? num - num1 : num + num1;
                    this.textBox51.Text = Math.Round(result, 4).ToString();
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
                if (!string.IsNullOrWhiteSpace(this.textBox116.Text))
                {
                    double num = double.Parse(this.label111.Text.Trim());
                    double num1 = double.Parse(this.textBox116.Text.Trim());
                    double result = this.checkBox4.Checked ? num - num1 : num + num1;
                    this.textBox43.Text = Math.Round(result, 4).ToString();
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
                if (!string.IsNullOrWhiteSpace(this.textBox109.Text))
                {
                    double num = double.Parse(this.label111.Text.Trim());
                    double num1 = double.Parse(this.textBox109.Text.Trim());
                    double result = this.checkBox3.Checked ? num - num1 : num + num1;
                    this.textBox103.Text = Math.Round(result, 4).ToString();
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
                if (!string.IsNullOrWhiteSpace(this.textBox120.Text))
                {
                    double num = double.Parse(this.label111.Text.Trim());
                    double num1 = double.Parse(this.textBox120.Text.Trim());
                    double result = this.checkBox2.Checked ? num - num1 : num + num1;
                    this.textBox45.Text = Math.Round(result, 4).ToString();
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
                if (!string.IsNullOrWhiteSpace(this.textBox119.Text))
                {
                    double num = double.Parse(this.label111.Text.Trim());
                    double num1 = double.Parse(this.textBox119.Text.Trim());
                    double result = this.checkBox1.Checked ? num - num1 : num + num1;
                    this.textBox40.Text = Math.Round(result, 4).ToString();
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
        private void textBox146_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox146.Text.Trim() != ""))
                    {
                        this.textBox146.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox146.Focus();
                        }
                        else
                        {
                            this.label426.Text = this.textBox146.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox145.Focus();
                        }
                    }
                }
            }
        }
        private void textBox145_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox145.Text.Trim() != ""))
                    {
                        this.textBox145.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox145.Focus();
                        }
                        else
                        {
                            this.label418.Text = this.textBox145.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox144.Focus();
                        }
                    }
                }
            }
        }
        private void textBox144_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox144.Text.Trim() != ""))
                    {
                        this.textBox144.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox144.Focus();
                        }
                        else
                        {
                            this.label410.Text = this.textBox144.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox143.Focus();
                        }
                    }
                }
            }
        }
        private void textBox143_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox143.Text.Trim() != ""))
                    {
                        this.textBox143.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox143.Focus();
                        }
                        else
                        {
                            this.label422.Text = this.textBox143.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox142.Focus();
                        }
                    }
                }
            }
        }
        private void textBox142_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox142.Text.Trim() != ""))
                    {
                        this.textBox142.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox142.Focus();
                        }
                        else
                        {
                            this.label414.Text = this.textBox142.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox141.Focus();
                        }
                    }
                }
            }
        }
        private void textBox141_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox141.Text.Trim() != ""))
                    {
                        this.textBox141.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox141.Focus();
                        }
                        else
                        {
                            this.label406.Text = this.textBox141.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox140.Focus();
                        }
                    }
                }
            }
        }
        private void textBox140_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox140.Text.Trim() != ""))
                    {
                        this.textBox140.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox140.Focus();
                        }
                        else
                        {
                            this.label425.Text = this.textBox140.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox139.Focus();
                        }
                    }
                }
            }
        }
        private void textBox139_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox139.Text.Trim() != ""))
                    {
                        this.textBox139.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox139.Focus();
                        }
                        else
                        {
                            this.label417.Text = this.textBox139.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox138.Focus();
                        }
                    }
                }
            }
        }
        private void textBox138_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox138.Text.Trim() != ""))
                    {
                        this.textBox138.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox138.Focus();
                        }
                        else
                        {
                            this.label409.Text = this.textBox138.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox161.Focus();
                        }
                    }
                }
            }
        }
        private void textBox161_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox161.Text.Trim() != ""))
                    {
                        this.textBox161.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox161.Focus();
                        }
                        else
                        {
                            this.label421.Text = this.textBox161.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox160.Focus();
                        }
                    }
                }
            }
        }
        private void textBox160_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox160.Text.Trim() != ""))
                    {
                        this.textBox160.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox160.Focus();
                        }
                        else
                        {
                            this.label413.Text = this.textBox160.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox159.Focus();
                        }
                    }
                }
            }
        }
        private void textBox159_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox159.Text.Trim() != ""))
                    {
                        this.textBox159.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox159.Focus();
                        }
                        else
                        {
                            this.label402.Text = this.textBox159.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox158.Focus();
                        }
                    }
                }
            }
        }
        private void textBox158_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox158.Text.Trim() != ""))
                    {
                        this.textBox158.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox158.Focus();
                        }
                        else
                        {
                            this.label424.Text = this.textBox158.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox157.Focus();
                        }
                    }
                }
            }
        }
        private void textBox157_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox157.Text.Trim() != ""))
                    {
                        this.textBox157.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox157.Focus();
                        }
                        else
                        {
                            this.label416.Text = this.textBox157.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox156.Focus();
                        }
                    }
                }
            }
        }
        private void textBox156_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox156.Text.Trim() != ""))
                    {
                        this.textBox156.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox156.Focus();
                        }
                        else
                        {
                            this.label408.Text = this.textBox156.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox155.Focus();
                        }
                    }
                }
            }
        }
        private void textBox155_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox155.Text.Trim() != ""))
                    {
                        this.textBox155.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox155.Focus();
                        }
                        else
                        {
                            this.label420.Text = this.textBox155.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox154.Focus();
                        }
                    }
                }
            }
        }
        private void textBox154_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox154.Text.Trim() != ""))
                    {
                        this.textBox154.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox154.Focus();
                        }
                        else
                        {
                            this.label412.Text = this.textBox154.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox153.Focus();
                        }
                    }
                }
            }
        }
        private void textBox153_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox153.Text.Trim() != ""))
                    {
                        this.textBox153.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox153.Focus();
                        }
                        else
                        {
                            this.label401.Text = this.textBox153.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox152.Focus();
                        }
                    }
                }
            }
        }
        private void textBox152_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox152.Text.Trim() != ""))
                    {
                        this.textBox152.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox152.Focus();
                        }
                        else
                        {
                            this.label423.Text = this.textBox152.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox151.Focus();
                        }
                    }
                }
            }
        }
        private void textBox151_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox151.Text.Trim() != ""))
                    {
                        this.textBox151.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox151.Focus();
                        }
                        else
                        {
                            this.label415.Text = this.textBox151.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox172.Focus();
                        }
                    }
                }
            }
        }
        private void textBox172_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox172.Text.Trim() != ""))
                    {
                        this.textBox172.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox172.Focus();
                        }
                        else
                        {
                            this.label407.Text = this.textBox172.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox173.Focus();
                        }
                    }
                }
            }
        }
        private void textBox173_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox173.Text.Trim() != ""))
                    {
                        this.textBox173.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox173.Focus();
                        }
                        else
                        {
                            this.label419.Text = this.textBox173.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox174.Focus();
                        }
                    }
                }
            }
        }
        private void textBox174_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox174.Text.Trim() != ""))
                    {
                        this.textBox174.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox174.Focus();
                        }
                        else
                        {
                            this.label411.Text = this.textBox174.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox175.Focus();
                        }
                    }
                }
            }
        }
        private void textBox175_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox175.Text.Trim() != ""))
                    {
                        this.textBox175.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox175.Focus();
                        }
                        else
                        {
                            this.label400.Text = this.textBox175.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox176.Focus();
                        }
                    }
                }
            }
        }
        private void textBox176_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox176.Text.Trim() != ""))
                    {
                        this.textBox176.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox176.Focus();
                        }
                        else
                        {
                            this.label399.Text = this.textBox176.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox177.Focus();
                        }
                    }
                }
            }
        }
        private void textBox177_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox177.Text.Trim() != ""))
                    {
                        this.textBox177.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox177.Focus();
                        }
                        else
                        {
                            this.label434.Text = this.textBox177.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox178.Focus();
                        }
                    }
                }
            }
        }
        private void textBox178_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox178.Text.Trim() != ""))
                    {
                        this.textBox178.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox178.Focus();
                        }
                        else
                        {
                            this.label442.Text = this.textBox178.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox179.Focus();
                        }
                    }
                }
            }
        }
        private void textBox179_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox179.Text.Trim() != ""))
                    {
                        this.textBox179.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox179.Focus();
                        }
                        else
                        {
                            this.label430.Text = this.textBox179.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox180.Focus();
                        }
                    }
                }
            }
        }
        private void textBox180_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox180.Text.Trim() != ""))
                    {
                        this.textBox180.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox180.Focus();
                        }
                        else
                        {
                            this.label438.Text = this.textBox180.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox148.Focus();
                        }
                    }
                }
            }
        }
        private void textBox148_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox148.Text.Trim() != ""))
                    {
                        this.textBox148.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox148.Focus();
                        }
                        else
                        {
                            this.label446.Text = this.textBox148.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox149.Focus();
                        }
                    }
                }
            }
        }
        private void textBox149_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox149.Text.Trim() != ""))
                    {
                        this.textBox149.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox149.Focus();
                        }
                        else
                        {
                            this.label427.Text = this.textBox149.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox150.Focus();
                        }
                    }
                }
            }
        }
        private void textBox150_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox150.Text.Trim() != ""))
                    {
                        this.textBox150.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox150.Focus();
                        }
                        else
                        {
                            this.label435.Text = this.textBox150.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox164.Focus();
                        }
                    }
                }
            }
        }
        private void textBox164_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox164.Text.Trim() != ""))
                    {
                        this.textBox164.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox164.Focus();
                        }
                        else
                        {
                            this.label443.Text = this.textBox164.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox165.Focus();
                        }
                    }
                }
            }
        }
        private void textBox165_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox165.Text.Trim() != ""))
                    {
                        this.textBox165.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox165.Focus();
                        }
                        else
                        {
                            this.label431.Text = this.textBox165.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox166.Focus();
                        }
                    }
                }
            }
        }
        private void textBox166_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox166.Text.Trim() != ""))
                    {
                        this.textBox166.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox166.Focus();
                        }
                        else
                        {
                            this.label439.Text = this.textBox166.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox167.Focus();
                        }
                    }
                }
            }
        }
        private void textBox167_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox167.Text.Trim() != ""))
                    {
                        this.textBox167.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox167.Focus();
                        }
                        else
                        {
                            this.label447.Text = this.textBox167.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox168.Focus();
                        }
                    }
                }
            }
        }
        private void textBox168_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox168.Text.Trim() != ""))
                    {
                        this.textBox168.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox168.Focus();
                        }
                        else
                        {
                            this.label428.Text = this.textBox168.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox169.Focus();
                        }
                    }
                }
            }
        }
        private void textBox169_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox169.Text.Trim() != ""))
                    {
                        this.textBox169.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox169.Focus();
                        }
                        else
                        {
                            this.label436.Text = this.textBox169.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox170.Focus();
                        }
                    }
                }
            }
        }
        private void textBox170_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox170.Text.Trim() != ""))
                    {
                        this.textBox170.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox170.Focus();
                        }
                        else
                        {
                            this.label444.Text = this.textBox170.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox171.Focus();
                        }
                    }
                }
            }
        }
        private void textBox171_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox171.Text.Trim() != ""))
                    {
                        this.textBox171.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox171.Focus();
                        }
                        else
                        {
                            this.label432.Text = this.textBox171.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox192.Focus();
                        }
                    }
                }
            }
        }
        private void textBox192_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox192.Text.Trim() != ""))
                    {
                        this.textBox192.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox192.Focus();
                        }
                        else
                        {
                            this.label440.Text = this.textBox192.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox193.Focus();
                        }
                    }
                }
            }
        }
        private void textBox193_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox193.Text.Trim() != ""))
                    {
                        this.textBox193.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox193.Focus();
                        }
                        else
                        {
                            this.label448.Text = this.textBox193.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox194.Focus();
                        }
                    }
                }
            }
        }
        private void textBox194_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox194.Text.Trim() != ""))
                    {
                        this.textBox194.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox194.Focus();
                        }
                        else
                        {
                            this.label429.Text = this.textBox194.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox195.Focus();
                        }
                    }
                }
            }
        }
        private void textBox195_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox195.Text.Trim() != ""))
                    {
                        this.textBox195.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox195.Focus();
                        }
                        else
                        {
                            this.label437.Text = this.textBox195.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox196.Focus();
                        }
                    }
                }
            }
        }
        private void textBox196_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox196.Text.Trim() != ""))
                    {
                        this.textBox196.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox196.Focus();
                        }
                        else
                        {
                            this.label445.Text = this.textBox196.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox197.Focus();
                        }
                    }
                }
            }
        }
        private void textBox197_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox197.Text.Trim() != ""))
                    {
                        this.textBox197.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox197.Focus();
                        }
                        else
                        {
                            this.label433.Text = this.textBox197.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox198.Focus();
                        }
                    }
                }
            }
        }
        private void textBox198_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox198.Text.Trim() != ""))
                    {
                        this.textBox198.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox198.Focus();
                        }
                        else
                        {
                            this.label441.Text = this.textBox198.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox199.Focus();
                        }
                    }
                }
            }
        }
        private void textBox199_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox199.Text.Trim() != ""))
                    {
                        this.textBox199.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox199.Focus();
                        }
                        else
                        {
                            this.label449.Text = this.textBox199.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox200.Focus();
                        }
                    }
                }
            }
        }
        private void textBox200_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox200.Text.Trim() != ""))
                    {
                        this.textBox200.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox200.Focus();
                        }
                        else
                        {
                            this.label473.Text = this.textBox200.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox181.Focus();
                        }
                    }
                }
            }
        }
        private void textBox181_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox181.Text.Trim() != ""))
                    {
                        this.textBox181.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox181.Focus();
                        }
                        else
                        {
                            this.label465.Text = this.textBox181.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox182.Focus();
                        }
                    }
                }
            }
        }
        private void textBox182_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox182.Text.Trim() != ""))
                    {
                        this.textBox182.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox182.Focus();
                        }
                        else
                        {
                            this.label457.Text = this.textBox182.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox183.Focus();
                        }
                    }
                }
            }
        }
        private void textBox183_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox183.Text.Trim() != ""))
                    {
                        this.textBox183.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox183.Focus();
                        }
                        else
                        {
                            this.label469.Text = this.textBox183.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox184.Focus();
                        }
                    }
                }
            }
        }
        private void textBox184_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox184.Text.Trim() != ""))
                    {
                        this.textBox184.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox184.Focus();
                        }
                        else
                        {
                            this.label461.Text = this.textBox184.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox185.Focus();
                        }
                    }
                }
            }
        }
        private void textBox185_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox185.Text.Trim() != ""))
                    {
                        this.textBox185.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox185.Focus();
                        }
                        else
                        {
                            this.label453.Text = this.textBox185.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox186.Focus();
                        }
                    }
                }
            }
        }
        private void textBox186_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox186.Text.Trim() != ""))
                    {
                        this.textBox186.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox186.Focus();
                        }
                        else
                        {
                            this.label472.Text = this.textBox186.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox187.Focus();
                        }
                    }
                }
            }
        }
        private void textBox187_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox187.Text.Trim() != ""))
                    {
                        this.textBox187.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox187.Focus();
                        }
                        else
                        {
                            this.label464.Text = this.textBox187.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox188.Focus();
                        }
                    }
                }
            }
        }
        private void textBox188_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox188.Text.Trim() != ""))
                    {
                        this.textBox188.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox188.Focus();
                        }
                        else
                        {
                            this.label456.Text = this.textBox188.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox189.Focus();
                        }
                    }
                }
            }
        }
        private void textBox189_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox189.Text.Trim() != ""))
                    {
                        this.textBox189.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox189.Focus();
                        }
                        else
                        {
                            this.label468.Text = this.textBox189.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox190.Focus();
                        }
                    }
                }
            }
        }
        private void textBox190_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox190.Text.Trim() != ""))
                    {
                        this.textBox190.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox190.Focus();
                        }
                        else
                        {
                            this.label460.Text = this.textBox190.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox191.Focus();
                        }
                    }
                }
            }
        }
        private void textBox191_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox191.Text.Trim() != ""))
                    {
                        this.textBox191.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox191.Focus();
                        }
                        else
                        {
                            this.label452.Text = this.textBox191.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox212.Focus();
                        }
                    }
                }
            }
        }
        private void textBox212_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox212.Text.Trim() != ""))
                    {
                        this.textBox212.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox212.Focus();
                        }
                        else
                        {
                            this.label471.Text = this.textBox212.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox213.Focus();
                        }
                    }
                }
            }
        }
        private void textBox213_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox213.Text.Trim() != ""))
                    {
                        this.textBox213.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox213.Focus();
                        }
                        else
                        {
                            this.label463.Text = this.textBox213.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox214.Focus();
                        }
                    }
                }
            }
        }
        private void textBox214_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox214.Text.Trim() != ""))
                    {
                        this.textBox214.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox214.Focus();
                        }
                        else
                        {
                            this.label455.Text = this.textBox214.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox215.Focus();
                        }
                    }
                }
            }
        }
        private void textBox215_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox215.Text.Trim() != ""))
                    {
                        this.textBox215.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox215.Focus();
                        }
                        else
                        {
                            this.label467.Text = this.textBox215.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox216.Focus();
                        }
                    }
                }
            }
        }
        private void textBox216_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox216.Text.Trim() != ""))
                    {
                        this.textBox216.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox216.Focus();
                        }
                        else
                        {
                            this.label459.Text = this.textBox216.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox217.Focus();
                        }
                    }
                }
            }
        }
        private void textBox217_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox217.Text.Trim() != ""))
                    {
                        this.textBox217.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox217.Focus();
                        }
                        else
                        {
                            this.label451.Text = this.textBox217.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox218.Focus();
                        }
                    }
                }
            }
        }
        private void textBox218_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox218.Text.Trim() != ""))
                    {
                        this.textBox218.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox218.Focus();
                        }
                        else
                        {
                            this.label470.Text = this.textBox218.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox219.Focus();
                        }
                    }
                }
            }
        }
        private void textBox219_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox219.Text.Trim() != ""))
                    {
                        this.textBox219.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox219.Focus();
                        }
                        else
                        {
                            this.label462.Text = this.textBox219.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox220.Focus();
                        }
                    }
                }
            }
        }
        private void textBox220_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox220.Text.Trim() != ""))
                    {
                        this.textBox220.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox220.Focus();
                        }
                        else
                        {
                            this.label454.Text = this.textBox220.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox201.Focus();
                        }
                    }
                }
            }
        }
        private void textBox201_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox201.Text.Trim() != ""))
                    {
                        this.textBox201.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox201.Focus();
                        }
                        else
                        {
                            this.label466.Text = this.textBox201.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox202.Focus();
                        }
                    }
                }
            }
        }
        private void textBox202_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox202.Text.Trim() != ""))
                    {
                        this.textBox202.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox202.Focus();
                        }
                        else
                        {
                            this.label458.Text = this.textBox202.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox203.Focus();
                        }
                    }
                }
            }
        }
        private void textBox203_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox203.Text.Trim() != ""))
                    {
                        this.textBox203.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox203.Focus();
                        }
                        else
                        {
                            this.label450.Text = this.textBox203.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox204.Focus();
                        }
                    }
                }
            }
        }
        private void textBox204_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox204.Text.Trim() != ""))
                    {
                        this.textBox204.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox204.Focus();
                        }
                        else
                        {
                            this.label497.Text = this.textBox204.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox205.Focus();
                        }
                    }
                }
            }
        }
        private void textBox205_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox205.Text.Trim() != ""))
                    {
                        this.textBox205.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox205.Focus();
                        }
                        else
                        {
                            this.label489.Text = this.textBox205.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox206.Focus();
                        }
                    }
                }
            }
        }
        private void textBox206_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox206.Text.Trim() != ""))
                    {
                        this.textBox206.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox206.Focus();
                        }
                        else
                        {
                            this.label481.Text = this.textBox206.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox207.Focus();
                        }
                    }
                }
            }
        }
        private void textBox207_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox207.Text.Trim() != ""))
                    {
                        this.textBox207.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox207.Focus();
                        }
                        else
                        {
                            this.label493.Text = this.textBox207.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox208.Focus();
                        }
                    }
                }
            }
        }
        private void textBox208_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox208.Text.Trim() != ""))
                    {
                        this.textBox208.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox208.Focus();
                        }
                        else
                        {
                            this.label485.Text = this.textBox208.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox209.Focus();
                        }
                    }
                }
            }
        }
        private void textBox209_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox209.Text.Trim() != ""))
                    {
                        this.textBox209.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox209.Focus();
                        }
                        else
                        {
                            this.label477.Text = this.textBox209.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox210.Focus();
                        }
                    }
                }
            }
        }
        private void textBox210_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox210.Text.Trim() != ""))
                    {
                        this.textBox210.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox210.Focus();
                        }
                        else
                        {
                            this.label496.Text = this.textBox210.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox211.Focus();
                        }
                    }
                }
            }
        }
        private void textBox211_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox211.Text.Trim() != ""))
                    {
                        this.textBox211.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox211.Focus();
                        }
                        else
                        {
                            this.label488.Text = this.textBox211.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox232.Focus();
                        }
                    }
                }
            }
        }
        private void textBox232_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox232.Text.Trim() != ""))
                    {
                        this.textBox232.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox232.Focus();
                        }
                        else
                        {
                            this.label480.Text = this.textBox232.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox233.Focus();
                        }
                    }
                }
            }
        }
        private void textBox233_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox233.Text.Trim() != ""))
                    {
                        this.textBox233.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox233.Focus();
                        }
                        else
                        {
                            this.label492.Text = this.textBox233.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox234.Focus();
                        }
                    }
                }
            }
        }
        private void textBox234_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox234.Text.Trim() != ""))
                    {
                        this.textBox234.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox234.Focus();
                        }
                        else
                        {
                            this.label484.Text = this.textBox234.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox235.Focus();
                        }
                    }
                }
            }
        }
        private void textBox235_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox235.Text.Trim() != ""))
                    {
                        this.textBox235.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox235.Focus();
                        }
                        else
                        {
                            this.label476.Text = this.textBox235.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox236.Focus();
                        }
                    }
                }
            }
        }
        private void textBox236_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox236.Text.Trim() != ""))
                    {
                        this.textBox236.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox236.Focus();
                        }
                        else
                        {
                            this.label495.Text = this.textBox236.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox237.Focus();
                        }
                    }
                }
            }
        }
        private void textBox237_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox237.Text.Trim() != ""))
                    {
                        this.textBox237.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox237.Focus();
                        }
                        else
                        {
                            this.label487.Text = this.textBox237.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox238.Focus();
                        }
                    }
                }
            }
        }
        private void textBox238_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox238.Text.Trim() != ""))
                    {
                        this.textBox238.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox238.Focus();
                        }
                        else
                        {
                            this.label479.Text = this.textBox238.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox239.Focus();
                        }
                    }
                }
            }
        }
        private void textBox239_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox239.Text.Trim() != ""))
                    {
                        this.textBox239.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox239.Focus();
                        }
                        else
                        {
                            this.label494.Text = this.textBox239.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox240.Focus();
                        }
                    }
                }
            }
        }
        private void textBox240_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox240.Text.Trim() != ""))
                    {
                        this.textBox240.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox240.Focus();
                        }
                        else
                        {
                            this.label483.Text = this.textBox240.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox221.Focus();
                        }
                    }
                }
            }
        }
        private void textBox221_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox221.Text.Trim() != ""))
                    {
                        this.textBox221.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox221.Focus();
                        }
                        else
                        {
                            this.label475.Text = this.textBox221.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox222.Focus();
                        }
                    }
                }
            }
        }
        private void textBox222_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox222.Text.Trim() != ""))
                    {
                        this.textBox222.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox222.Focus();
                        }
                        else
                        {
                            this.label494.Text = this.textBox222.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox223.Focus();
                        }
                    }
                }
            }
        }
        private void textBox223_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox223.Text.Trim() != ""))
                    {
                        this.textBox223.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox223.Focus();
                        }
                        else
                        {
                            this.label486.Text = this.textBox223.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox224.Focus();
                        }
                    }
                }
            }
        }
        private void textBox224_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox224.Text.Trim() != ""))
                    {
                        this.textBox224.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox224.Focus();
                        }
                        else
                        {
                            this.label478.Text = this.textBox224.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox225.Focus();
                        }
                    }
                }
            }
        }
        private void textBox225_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox225.Text.Trim() != ""))
                    {
                        this.textBox225.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox225.Focus();
                        }
                        else
                        {
                            this.label490.Text = this.textBox225.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox226.Focus();
                        }
                    }
                }
            }
        }
        private void textBox226_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox226.Text.Trim() != ""))
                    {
                        this.textBox226.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox226.Focus();
                        }
                        else
                        {
                            this.label482.Text = this.textBox226.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox227.Focus();
                        }
                    }
                }
            }
        }
        private void textBox227_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox227.Text.Trim() != ""))
                    {
                        this.textBox227.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox227.Focus();
                        }
                        else
                        {
                            this.label474.Text = this.textBox227.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox229.Focus();
                        }
                    }
                }
            }
        }
        private void textBox229_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox229.Text.Trim() != ""))
                    {
                        this.textBox229.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox229.Focus();
                        }
                        else
                        {
                            this.label521.Text = this.textBox229.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox230.Focus();
                        }
                    }
                }
            }
        }
        private void textBox230_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox230.Text.Trim() != ""))
                    {
                        this.textBox230.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox230.Focus();
                        }
                        else
                        {
                            this.label513.Text = this.textBox230.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox231.Focus();
                        }
                    }
                }
            }
        }
        private void textBox231_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox231.Text.Trim() != ""))
                    {
                        this.textBox231.Focus();
                    }
                    else
                    {
                        if (this.button38.Text == "Add")
                        {
                            MessageBox.Show(string.Concat("Please Press Add Button"));
                            this.textBox231.Focus();
                        }
                        else
                        {
                            this.label505.Text = this.textBox231.Text.Trim();
                            this.AllTakenAdd();
                            this.textBox162.Focus();
                        }
                    }
                }
            }
        }
        private void textBox162_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((char.IsDigit(keyChar) || keyChar == 0 || keyChar == '\b' ? false : keyChar != '.'))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    if (!(this.textBox162.Text.Trim() != ""))
                    {
                        this.textBox162.Focus();
                    }
                    else
                    {
                        this.button38.Focus();
                    }
                }
            }
        }
        private void textBox162_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.textBox162.Text.Trim() != "")
                {
                    double num = double.Parse(this.textBox162.Text.Trim());
                    double num1 = double.Parse(this.textBox163.Text.Trim());
                    double num3 = num - num1;
                    decimal num2 = Convert.ToDecimal(num3.ToString());
                    Label str1 = this.label294;
                    decimal num4 = Math.Round(num2, 4);
                    num3 = double.Parse(num4.ToString());
                    str1.Text = num3.ToString();
                }
                else
                {
                    this.label294.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        #endregion

        //------------------------------If Query Needed--------------------------
        //-----------------------------------------------------------------------
    }
}
