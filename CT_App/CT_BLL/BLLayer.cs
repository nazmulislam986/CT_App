using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CT_App.CT_DLL;
using CT_App.Models;

namespace CT_App.CT_BLL
{
    public class BLLayer
    {
        private DLLayer _dLLayer = new DLLayer();

        //--------------------- All Retrive Data In DataTable -------------------
        //-----------------------------------------------------------------------
        public List<DataTable> RetrieveMarketData()
        {
            return _dLLayer.GetMarketData();
        }
        public List<DataTable> RetrieveDataAllinstaTable()
        {
            return _dLLayer.GetDataAllInstaTable();
        }
        public List<DataTable> RetrieveDailySavTable()
        {
            return _dLLayer.GetDataAllDailySavTable();
        }
        public List<DataTable> RetrieveDataAllCrTable()
        {
            return _dLLayer.GetDataAllCrTable();
        }
        public List<DataTable> RetrieveDataAllCutGridTable()
        {
            return _dLLayer.GetDataAllCutGridTable();
        }
        public float GetTtlDaily()
        {
            return _dLLayer.GetTotalForDail();
        }
        public float GetTtlDailyCut()
        {
            return _dLLayer.GetTotalForDailCut();
        }
        public float GetTtlDailyAnt()
        {
            return _dLLayer.GetTotalForDailAnt();
        }
        public float GetTtlDailySave()
        {
            return _dLLayer.GetTotalForDailySav();
        }
        public float GetTotalMarket()
        {
            return _dLLayer.GetTotalMarketAmount();
        }
        public float GetTotalGiven()
        {
            return _dLLayer.GetSumOfGivenAmount();
        }
        public float GetTotalTeken()
        {
            return _dLLayer.GetSumOfTekenAmount();
        }
        public float GetTotalTariff()
        {
            return _dLLayer.GetSumOfTariffAmount();
        }
        public float GetTotalSaving()
        {
            return _dLLayer.GetSumOfSavingAmount();
        }
        public float GetTotalUnrated()
        {
            return _dLLayer.GetSumOfUnratedAmount();
        }
        public string GetTotalDaily()
        {
            return _dLLayer.GetSumOfDailyAmount();
        }
        public string GetTotalDailyAnt()
        {
            return _dLLayer.GetSumOfDailyAntAmount();
        }
        public string GetTotalDailySaving()
        {
            return _dLLayer.GetSumOfDailySavingAmount();
        }
        public float GetTotalInstl()
        {
            return _dLLayer.GetTotalForInstl();
        }

        //------------------------------ Market / Mamo --------------------------
        //-----------------------------------------------------------------------
        public bool InsMarket(Market market)
        {
            return _dLLayer.insrtMarket(market);
        }
        public bool UpdtMarket(Market market)
        {
            return _dLLayer.updtMarket(market);
        }
        public bool InsUtoM(Market market)
        {
            return _dLLayer.insrtUtoM(market);
        }

        public bool InsMarketMemos(MarketMemos marketMemos)
        {
            return _dLLayer.insrtMrktMemos(marketMemos);
        }
        public bool updtMarketMemos(MarketMemos marketMemos)
        {
            return _dLLayer.updtMrktMemos(marketMemos);
        }
        public bool DelMarketMemos(string Mem_ID, MarketMemos marketMemos)
        {
            return _dLLayer.delMrktMemos(Mem_ID, marketMemos);
        }

        //------------------------------- Installment ---------------------------
        //-----------------------------------------------------------------------
        public bool InsDailySaving(DailySaving dailySaving)
        {
            return _dLLayer.insrtDailySaving(dailySaving);
        }
        public bool UpdtDailySaving(DailySaving dailySaving)
        {
            return _dLLayer.updtDailySaving(dailySaving);
        }
        public bool DelDailySaving(DailySaving dailySaving)
        {
            return _dLLayer.delDailySaving(dailySaving);
        }
        public bool DelReDailySaving(DailySaving dailySaving)
        {
            return _dLLayer.delReDailySaving(dailySaving);
        }

        public bool InsInstallment(Installment installment)
        {
            return _dLLayer.insrtInstallment(installment);
        }
        public bool UpdtInstallment(Installment installment)
        {
            return _dLLayer.updtInstallment(installment);
        }

        public bool InsrInstallment(Installment installment)
        {
            return _dLLayer.insrInstallment(installment);
        }
        public bool UdtInstallment(Installment installment)
        {
            return _dLLayer.udtInstallment(installment);
        }

        public bool InsBikeInfo(BikeInfo bikeInfo)
        {
            return _dLLayer.insrtBikeInfo(bikeInfo);
        }

        //-------------------------------- Cr. Card -----------------------------
        //-----------------------------------------------------------------------
        public bool InsGiven(Given given)
        {
            return _dLLayer.insrtGiven(given);
        }
        public bool InsTeken(Teken teken)
        {
            return _dLLayer.insrtTeken(teken);
        }
        public bool InsTariffAmt(TariffAmt tariff)
        {
            return _dLLayer.insrtTariffAmt(tariff);
        }
        public bool InsSaving(Saving saving)
        {
            return _dLLayer.insrtSaving(saving);
        }
        public bool InsUnrated(Unrated unrated)
        {
            return _dLLayer.insrtUnrated(unrated);
        }
        
        public bool InsUpdtGiven(Given given)
        {
            return _dLLayer.insrtupdtGiven(given);
        }
        public bool InsUpdtTeken(Teken teken)
        {
            return _dLLayer.insrtupdtTeken(teken);
        }
        public bool InsUpdtTariffAmt(TariffAmt tariff)
        {
            return _dLLayer.insrtupdtTariffAmt(tariff);
        }
        public bool InsUpdtSaving(Saving saving)
        {
            return _dLLayer.insrtupdtSaving(saving);
        }
        public bool InsUpdtUnrated(Unrated unrated)
        {
            return _dLLayer.insrtupdtUnrated(unrated);
        }

        public bool DelGiven(Given given)
        {
            return _dLLayer.delGiven(given);
        }
        public bool DelTeken(Teken teken)
        {
            return _dLLayer.delTeken(teken);
        }
        public bool DelTariffAmt(TariffAmt tariff)
        {
            return _dLLayer.delTariffAmt(tariff);
        }
        public bool DelSaving(Saving saving)
        {
            return _dLLayer.delSaving(saving);
        }
        public bool DelUnrated(Unrated unrated)
        {
            return _dLLayer.delUnrated(unrated);
        }

        //------------------------------ Daily / Achive -------------------------
        //-----------------------------------------------------------------------
        public bool InsDaily(Daily daily)
        {
            return _dLLayer.insrtDaily(daily);
        }
        public bool UpdtDaily(Daily daily)
        {
            return _dLLayer.updtDaily(daily);
        }
        public bool DelDaily(Daily daily)
        {
            return _dLLayer.delDaily(daily);
        }

        public bool AddDailyCut(DailyCut dailyCut)
        {
            return _dLLayer.InsertDailyCut(dailyCut);
        }
        public bool UpdateDailyCut(DailyCut dailyCut)
        {
            return _dLLayer.UpdateDailyCut(dailyCut);
        }
        public bool DelDailyAndDailyCut(string D_ID, string C_ID, DailyCut deldailyCut)
        {
            return _dLLayer.deleteDailyCut(D_ID, C_ID, deldailyCut);
        }

        public bool InsDailyAnt(DailyAnt dailyAnt)
        {
            return _dLLayer.insrtDailyAnt(dailyAnt);
        }
        public bool UpdtDailyAnt(DailyAnt dailyAnt)
        {
            return _dLLayer.updtDailyAnt(dailyAnt);
        }
        public bool delDailyAnt(DailyAnt dailyAnt)
        {
            return _dLLayer.delDailyAnt(dailyAnt);
        }
        public bool delReDailyAnt(DailyAnt dailyAnt)
        {
            return _dLLayer.delReDailyAnt(dailyAnt);
        }

        //---------------------------------- Monthly ------------------------------
        //-------------------------------------------------------------------------
        public List<DataTable> RetrieveMonthlyData()
        {
            return _dLLayer.GetMonthlyData();
        }
        public bool InsMonthlyTake(MonthlyTake monthlyTake)
        {
            return _dLLayer.insrtMonthlyTake(monthlyTake);
        }
        public bool UpdtMonthlyTake(MonthlyTake monthlyTake)
        {
            return _dLLayer.updtMonthlyTake(monthlyTake);
        }
        public DataTable GetMonthDataById(string mntTknId)
        {
            return _dLLayer.GetMonthTakeData(mntTknId);
        }

        //------------------------------ Sync Data to SQL -------------------------
        //-------------------------------------------------------------------------
        public void SynchronizeData()
        {
            _dLLayer.DeleteAllDataInSQL();
        }

        public void SynchronizeMarkMemData()
        {
            _dLLayer.SyncMarkMemData();
        }
        public void SynchronizeInstallData()
        {
            _dLLayer.SyncInstallData();
        }
        public void SynchronizeCrCardData()
        {
            _dLLayer.SyncCrCardData();
        }
        public void SynchronizeDailyAchiveData()
        {
            _dLLayer.SyncDailyAchiveData();
        }
        public void SynchronizeMonthlyData()
        {
            _dLLayer.SyncMonthlyData();
        }

        //------------------------------DataGridView Events----------------------
        //-----------------------------------------------------------------------
        public DataTable GetMarketDataById(string marketId)
        {
            return _dLLayer.GetMarketData(marketId);
        }
        public DataTable GetInstallmentDataById(string installmentId)
        {
            return _dLLayer.GetInstallmentData(installmentId);
        }
        public DataTable GetGivenDataById(string givenId)
        {
            return _dLLayer.GetGivenData(givenId);
        }
        public DataTable GetDailyDataById(string dailyId)
        {
            return _dLLayer.GetDailyData(dailyId);
        }
        public DataTable GetDailyCutById(string dailycutId)
        {
            return _dLLayer.GetDailyCutData(dailycutId);
        }
        public DataTable GetInstallmntById(string installmntId)
        {
            return _dLLayer.GetInstallmntData(installmntId);
        }
        public DataTable GetIntakeById(string takeId)
        {
            return _dLLayer.GetIntakeData(takeId);
        }
        public DataTable GetExpenseById(string expenseId)
        {
            return _dLLayer.GetExpenseData(expenseId);
        }
        public DataTable GetSavingById(string savingId)
        {
            return _dLLayer.GetSavingData(savingId);
        }
        public DataTable GetUnratedById(string unratedId)
        {
            return _dLLayer.GetUnratedData(unratedId);
        }
        public DataTable GetMarketMemoById(string memoId)
        {
            return _dLLayer.GetMarketMemoData(memoId);
        }
        public DataTable GetBikeInfoById(string bikeinfoId)
        {
            return _dLLayer.GetbikeInfoData(bikeinfoId);
        }
        public DataTable GetDailyAntById(string dailyAntId)
        {
            return _dLLayer.GetDailyAntData(dailyAntId);
        }
        public DataTable GetDailySaviById(string dailySaviId)
        {
            return _dLLayer.GetDailySaviData(dailySaviId);
        }
        public DataTable GetImagesData()
        {
            return _dLLayer.GetImageData();
        }

        //--------------------------All Search Query Events----------------------
        //-----------------------------------------------------------------------
        public DataSet GetGivenByReceiver(string givenTo)
        {
            return _dLLayer.GetGivenDetailData(givenTo);
        }
        public DataSet GetTakenByReceiver(string takeTo)
        {
            return _dLLayer.GetTakenDetailData(takeTo);
        }
        public DataSet GetExpenseByReceiver(string expenseTo)
        {
            return _dLLayer.GetExpenseDetailData(expenseTo);
        }
        public DataSet GetSavingsByReceiver(string savingTo)
        {
            return _dLLayer.GetSavingsDetailData(savingTo);
        }
        public DataSet GetUnrateByReceiver(string unratedTo)
        {
            return _dLLayer.GetUnratedDetailData(unratedTo);
        }
    }
}
