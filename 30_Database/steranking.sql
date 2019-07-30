-- phpMyAdmin SQL Dump
-- version 4.9.0.1
-- https://www.phpmyadmin.net/
--
-- ホスト: 127.0.0.1
-- 生成日時: 
-- サーバのバージョン： 10.3.16-MariaDB
-- PHP のバージョン: 7.2.19

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- データベース: `techtest`
--

-- --------------------------------------------------------

--
-- テーブルの構造 `steranking`
--

CREATE TABLE `steranking` (
  `Id` int(11) NOT NULL,
  `Name` varchar(20) NOT NULL,
  `Score` int(11) NOT NULL,
  `Date` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- テーブルのデータのダンプ `steranking`
--

INSERT INTO `steranking` (`Id`, `Name`, `Score`, `Date`) VALUES
(1, 'TESTMAN', 100, '2019-07-18 12:07:54'),
(2, 'TESTGIRL', 200, '2019-07-18 12:07:54'),
(3, 'TEST', 300, '2019-07-18 16:33:02'),
(4, 'TANAKA', 1000, '2019-07-23 10:26:34'),
(5, 'MIDI', 400, '2019-07-23 10:26:34'),
(6, 'OCELL', 500, '2019-07-23 10:26:34'),
(7, 'LEFT', 600, '2019-07-23 10:26:34'),
(8, 'MUGICHA', 700, '2019-07-23 10:26:34'),
(9, 'GROUP', 800, '2019-07-23 10:26:34'),
(10, 'ZETMAN', 900, '2019-07-23 10:26:34'),
(11, 'YAMADA', 700, '2019-07-23 05:26:53'),
(12, 'YAMADA', 1400, '2019-07-23 05:28:01'),
(21, 'NONAME', 3950, '2019-07-23 23:20:29'),
(22, 'NONAME', 0, '2019-07-24 01:41:57'),
(23, 'NONAME', 4100, '2019-07-24 02:41:01'),
(24, 'NONAME', 8600, '2019-07-24 02:43:15'),
(25, 'NONAME', 11850, '2019-07-24 02:46:16');

--
-- ダンプしたテーブルのインデックス
--

--
-- テーブルのインデックス `steranking`
--
ALTER TABLE `steranking`
  ADD PRIMARY KEY (`Id`);

--
-- ダンプしたテーブルのAUTO_INCREMENT
--

--
-- テーブルのAUTO_INCREMENT `steranking`
--
ALTER TABLE `steranking`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=26;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
