-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Generation Time: Aug 31, 2024 at 02:29 PM
-- Server version: 8.0.39
-- PHP Version: 8.2.13

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `ct_db`
--

-- --------------------------------------------------------

--
-- Table structure for table `bikeinfo`
--

DROP TABLE IF EXISTS `bikeinfo`;
CREATE TABLE IF NOT EXISTS `bikeinfo` (
  `B_ID` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `B_Chng_Date` datetime DEFAULT NULL,
  `B_KM_ODO` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `B_Mobile_Go` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `B_Next_ODO` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `B_Insrt_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `B_Updt_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `daily`
--

DROP TABLE IF EXISTS `daily`;
CREATE TABLE IF NOT EXISTS `daily` (
  `D_ID` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `D_Date` datetime DEFAULT NULL,
  `D_FPAmount` float DEFAULT '0',
  `D_SPAmount` float DEFAULT '0',
  `NotTaken` float DEFAULT '0',
  `D_Data` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `TakenDate` datetime DEFAULT NULL,
  `D_Insrt_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `D_Updt_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `D_Del_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `dailyant`
--

DROP TABLE IF EXISTS `dailyant`;
CREATE TABLE IF NOT EXISTS `dailyant` (
  `DA_ID` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `DA_Date` datetime DEFAULT NULL,
  `DA_FPAmount` float DEFAULT '0',
  `DA_SPAmount` float DEFAULT '0',
  `NotTaken` float DEFAULT '0',
  `DA_Data` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `TakenDate` datetime DEFAULT NULL,
  `DA_Insrt_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `DA_Updt_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `DA_Del_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `dailycut`
--

DROP TABLE IF EXISTS `dailycut`;
CREATE TABLE IF NOT EXISTS `dailycut` (
  `C_ID` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `C_Date` datetime DEFAULT NULL,
  `C_Amount` float DEFAULT '0',
  `C_Insrt_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `C_Updt_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `C_Del_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `dailysaving`
--

DROP TABLE IF EXISTS `dailysaving`;
CREATE TABLE IF NOT EXISTS `dailysaving` (
  `DS_ID` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `DS_Date` datetime DEFAULT NULL,
  `DS_FPAmount` float DEFAULT '0',
  `DS_SPAmount` float DEFAULT '0',
  `DS_TPAmount` float DEFAULT '0',
  `NotTaken` float DEFAULT '0',
  `DS_Data` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `DS_InBankDate` datetime DEFAULT NULL,
  `DS_Insrt_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `DS_Updt_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `DS_Del_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `given`
--

DROP TABLE IF EXISTS `given`;
CREATE TABLE IF NOT EXISTS `given` (
  `InGiven` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `Total_Given` float DEFAULT '0',
  `Given_To` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `ThroughBy_Given` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `Given_Date` datetime DEFAULT NULL,
  `Remarks_Given` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `GDT_V` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `GDT_V_Date` datetime DEFAULT NULL,
  `DDT_V_Date` datetime DEFAULT NULL,
  `G_Insrt_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `G_Updt_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `G_Del_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `givenupdt`
--

DROP TABLE IF EXISTS `givenupdt`;
CREATE TABLE IF NOT EXISTS `givenupdt` (
  `InGiven` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `Was_Given` float DEFAULT '0',
  `Now_Given` float DEFAULT '0',
  `Total_Given` float DEFAULT '0',
  `Given_To` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `GDT_V_Date` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `images`
--

DROP TABLE IF EXISTS `images`;
CREATE TABLE IF NOT EXISTS `images` (
  `Img_ID` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `ImageData` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `installment`
--

DROP TABLE IF EXISTS `installment`;
CREATE TABLE IF NOT EXISTS `installment` (
  `I_ID` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `I_Date` datetime DEFAULT NULL,
  `Take_Total` float DEFAULT '0',
  `Take_Anot` float DEFAULT '0',
  `Take_Mine` float DEFAULT '0',
  `Take_Data` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `InsPerMonth` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `PerMonthPay` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `InsPay` float DEFAULT '0',
  `InsPay_Date` datetime DEFAULT NULL,
  `I_Insrt_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `I_Updt_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `I_Del_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `market`
--

DROP TABLE IF EXISTS `market`;
CREATE TABLE IF NOT EXISTS `market` (
  `M_ID` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `M_Date` datetime DEFAULT NULL,
  `M_Amount` float DEFAULT '0',
  `M_Insrt_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `M_Updt_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `M_Del_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `marketmemos`
--

DROP TABLE IF EXISTS `marketmemos`;
CREATE TABLE IF NOT EXISTS `marketmemos` (
  `Mem_ID` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `Mem_Date` datetime DEFAULT NULL,
  `R_InvTK` float DEFAULT '0',
  `C_InvTK` float DEFAULT '0',
  `Giv_TK` float DEFAULT '0',
  `Ret_TK` float DEFAULT '0',
  `I_N01` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `I_N02` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `I_N03` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `I_N04` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `I_N05` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `I_N06` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `I_N07` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `I_N08` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `I_N09` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `I_N10` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `I_N11` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `I_N12` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `I_N13` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `I_N14` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `I_N15` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `I_N16` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `I_P01` float DEFAULT '0',
  `I_P02` float DEFAULT '0',
  `I_P03` float DEFAULT '0',
  `I_P04` float DEFAULT '0',
  `I_P05` float DEFAULT '0',
  `I_P06` float DEFAULT '0',
  `I_P07` float DEFAULT '0',
  `I_P08` float DEFAULT '0',
  `I_P09` float DEFAULT '0',
  `I_P10` float DEFAULT '0',
  `I_P11` float DEFAULT '0',
  `I_P12` float DEFAULT '0',
  `I_P13` float DEFAULT '0',
  `I_P14` float DEFAULT '0',
  `I_P15` float DEFAULT '0',
  `I_P16` float DEFAULT '0',
  `I_Q01` float DEFAULT '0',
  `I_Q02` float DEFAULT '0',
  `I_Q03` float DEFAULT '0',
  `I_Q04` float DEFAULT '0',
  `I_Q05` float DEFAULT '0',
  `I_Q06` float DEFAULT '0',
  `I_Q07` float DEFAULT '0',
  `I_Q08` float DEFAULT '0',
  `I_Q09` float DEFAULT '0',
  `I_Q10` float DEFAULT '0',
  `I_Q11` float DEFAULT '0',
  `I_Q12` float DEFAULT '0',
  `I_Q13` float DEFAULT '0',
  `I_Q14` float DEFAULT '0',
  `I_Q15` float DEFAULT '0',
  `I_Q16` float DEFAULT '0',
  `I_ST01` float DEFAULT '0',
  `I_ST02` float DEFAULT '0',
  `I_ST03` float DEFAULT '0',
  `I_ST04` float DEFAULT '0',
  `I_ST05` float DEFAULT '0',
  `I_ST06` float DEFAULT '0',
  `I_ST07` float DEFAULT '0',
  `I_ST08` float DEFAULT '0',
  `I_ST09` float DEFAULT '0',
  `I_ST10` float DEFAULT '0',
  `I_ST11` float DEFAULT '0',
  `I_ST12` float DEFAULT '0',
  `I_ST13` float DEFAULT '0',
  `I_ST14` float DEFAULT '0',
  `I_ST15` float DEFAULT '0',
  `I_ST16` float DEFAULT '0',
  `R_Inv01` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv02` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv03` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv04` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv05` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv06` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv07` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv08` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv09` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv10` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv11` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv12` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv13` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv14` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv15` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv16` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv17` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv18` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv19` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv20` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv21` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv22` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv23` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv24` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `Mem_Insrt_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `Mem_Updt_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `Mem_Del_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `marketmemosdel`
--

DROP TABLE IF EXISTS `marketmemosdel`;
CREATE TABLE IF NOT EXISTS `marketmemosdel` (
  `Mem_ID` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `Mem_Date` datetime DEFAULT NULL,
  `R_InvTK` float DEFAULT '0',
  `C_InvTK` float DEFAULT '0',
  `Giv_TK` float DEFAULT '0',
  `Ret_TK` float DEFAULT '0',
  `I_N01` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `I_N02` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `I_N03` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `I_N04` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `I_N05` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `I_N06` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `I_N07` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `I_N08` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `I_N09` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `I_N10` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `I_N11` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `I_N12` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `I_N13` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `I_N14` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `I_N15` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `I_N16` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `I_P01` float DEFAULT '0',
  `I_P02` float DEFAULT '0',
  `I_P03` float DEFAULT '0',
  `I_P04` float DEFAULT '0',
  `I_P05` float DEFAULT '0',
  `I_P06` float DEFAULT '0',
  `I_P07` float DEFAULT '0',
  `I_P08` float DEFAULT '0',
  `I_P09` float DEFAULT '0',
  `I_P10` float DEFAULT '0',
  `I_P11` float DEFAULT '0',
  `I_P12` float DEFAULT '0',
  `I_P13` float DEFAULT '0',
  `I_P14` float DEFAULT '0',
  `I_P15` float DEFAULT '0',
  `I_P16` float DEFAULT '0',
  `I_Q01` float DEFAULT '0',
  `I_Q02` float DEFAULT '0',
  `I_Q03` float DEFAULT '0',
  `I_Q04` float DEFAULT '0',
  `I_Q05` float DEFAULT '0',
  `I_Q06` float DEFAULT '0',
  `I_Q07` float DEFAULT '0',
  `I_Q08` float DEFAULT '0',
  `I_Q09` float DEFAULT '0',
  `I_Q10` float DEFAULT '0',
  `I_Q11` float DEFAULT '0',
  `I_Q12` float DEFAULT '0',
  `I_Q13` float DEFAULT '0',
  `I_Q14` float DEFAULT '0',
  `I_Q15` float DEFAULT '0',
  `I_Q16` float DEFAULT '0',
  `I_ST01` float DEFAULT '0',
  `I_ST02` float DEFAULT '0',
  `I_ST03` float DEFAULT '0',
  `I_ST04` float DEFAULT '0',
  `I_ST05` float DEFAULT '0',
  `I_ST06` float DEFAULT '0',
  `I_ST07` float DEFAULT '0',
  `I_ST08` float DEFAULT '0',
  `I_ST09` float DEFAULT '0',
  `I_ST10` float DEFAULT '0',
  `I_ST11` float DEFAULT '0',
  `I_ST12` float DEFAULT '0',
  `I_ST13` float DEFAULT '0',
  `I_ST14` float DEFAULT '0',
  `I_ST15` float DEFAULT '0',
  `I_ST16` float DEFAULT '0',
  `R_Inv01` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv02` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv03` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv04` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv05` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv06` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv07` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv08` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv09` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv10` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv11` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv12` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv13` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv14` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv15` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv16` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv17` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv18` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv19` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv20` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv21` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv22` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv23` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `R_Inv24` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `Mem_Insrt_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `Mem_Updt_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `Mem_Del_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `saving`
--

DROP TABLE IF EXISTS `saving`;
CREATE TABLE IF NOT EXISTS `saving` (
  `InSaving` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `Saving_Amount` float DEFAULT '0',
  `Saving_To` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `ThroughBy_Saving` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `Saving_Date` datetime DEFAULT NULL,
  `Remarks_Saving` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `SDT_V` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `SDT_V_Date` datetime DEFAULT NULL,
  `DDT_V_Date` datetime DEFAULT NULL,
  `Saving_Bank` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `S_Insrt_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `S_Updt_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `S_Del_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `savingupdt`
--

DROP TABLE IF EXISTS `savingupdt`;
CREATE TABLE IF NOT EXISTS `savingupdt` (
  `InSaving` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `Was_Saving` float DEFAULT '0',
  `Now_Saving` float DEFAULT '0',
  `Saving_Amount` float DEFAULT '0',
  `Saving_To` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `SDT_V_Date` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `tariffamt`
--

DROP TABLE IF EXISTS `tariffamt`;
CREATE TABLE IF NOT EXISTS `tariffamt` (
  `InExpense` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `Expense_Amount` float DEFAULT '0',
  `Expense_To` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `ThroughBy_Expense` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `Expense_Date` datetime DEFAULT NULL,
  `Remarks_Expense` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `EDT_V` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `EDT_V_Date` datetime DEFAULT NULL,
  `DDT_V_Date` datetime DEFAULT NULL,
  `E_Insrt_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `E_Updt_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `E_Del_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `tariffamtupdt`
--

DROP TABLE IF EXISTS `tariffamtupdt`;
CREATE TABLE IF NOT EXISTS `tariffamtupdt` (
  `InExpense` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `Was_Expense` float DEFAULT '0',
  `Now_Expense` float DEFAULT '0',
  `Expense_Amount` float DEFAULT '0',
  `Expense_To` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `EDT_V_Date` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `teken`
--

DROP TABLE IF EXISTS `teken`;
CREATE TABLE IF NOT EXISTS `teken` (
  `InTake` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `Total_Take` float DEFAULT '0',
  `Take_To` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `ThroughBy_Take` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `Take_Date` datetime DEFAULT NULL,
  `Remarks_Take` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `TDT_V` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `TDT_V_Date` datetime DEFAULT NULL,
  `DDT_V_Date` datetime DEFAULT NULL,
  `T_Insrt_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `T_Updt_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `T_Del_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `tekenupdt`
--

DROP TABLE IF EXISTS `tekenupdt`;
CREATE TABLE IF NOT EXISTS `tekenupdt` (
  `InTake` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `Was_Take` float DEFAULT '0',
  `Now_Take` float DEFAULT '0',
  `Total_Take` float DEFAULT '0',
  `Take_To` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `TDT_V_Date` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `unrated`
--

DROP TABLE IF EXISTS `unrated`;
CREATE TABLE IF NOT EXISTS `unrated` (
  `InUnrated` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `Unrated_Amount` float DEFAULT '0',
  `Unrated_To` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `ThroughBy_Unrated` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `Unrated_Date` datetime DEFAULT NULL,
  `Remarks_Unrated` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `UDT_V` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `UDT_V_Date` datetime DEFAULT NULL,
  `DDT_V_Date` datetime DEFAULT NULL,
  `U_Insrt_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `U_Updt_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `U_Del_Person` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `unratedupdt`
--

DROP TABLE IF EXISTS `unratedupdt`;
CREATE TABLE IF NOT EXISTS `unratedupdt` (
  `InUnrated` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `Was_Unrated` float DEFAULT '0',
  `Now_Unrated` float DEFAULT '0',
  `Unrated_Amount` float DEFAULT '0',
  `Unrated_To` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `UDT_V_Date` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
