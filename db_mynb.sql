-- --------------------------------------------------------
-- Хост:                         127.0.0.1
-- Версия сервера:               5.7.16-log - MySQL Community Server (GPL)
-- Операционная система:         Win32
-- HeidiSQL Версия:              9.4.0.5125
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Дамп структуры базы данных story
CREATE DATABASE IF NOT EXISTS `story` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `story`;

-- Дамп структуры для таблица story.comments
CREATE TABLE IF NOT EXISTS `comments` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `id_story` int(10) unsigned NOT NULL,
  `id_comment` int(10) unsigned NOT NULL,
  `comment` text NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Дамп данных таблицы story.comments: ~0 rows (приблизительно)
/*!40000 ALTER TABLE `comments` DISABLE KEYS */;
/*!40000 ALTER TABLE `comments` ENABLE KEYS */;

-- Дамп структуры для таблица story.story
CREATE TABLE IF NOT EXISTS `story` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(255) DEFAULT NULL,
  `story` text,
  `email` varchar(255) DEFAULT NULL,
  `post_date` datetime DEFAULT NULL,
  `status` enum('wait','show','drop') DEFAULT 'wait',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Дамп данных таблицы story.story: ~0 rows (приблизительно)
/*!40000 ALTER TABLE `story` DISABLE KEYS */;
/*!40000 ALTER TABLE `story` ENABLE KEYS */;

-- Дамп структуры для таблица story.user
CREATE TABLE IF NOT EXISTS `user` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `login` varchar(50) DEFAULT NULL,
  `passw` varchar(50) DEFAULT NULL,
  `status` int(11) DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- Дамп данных таблицы story.user: ~1 rows (приблизительно)
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT IGNORE INTO `user` (`id`, `login`, `passw`, `status`) VALUES
	(1, 'admin', 'qwas', 1);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
