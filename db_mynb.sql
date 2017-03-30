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
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

-- Дамп данных таблицы story.story: ~4 rows (приблизительно)
/*!40000 ALTER TABLE `story` DISABLE KEYS */;
INSERT IGNORE INTO `story` (`id`, `title`, `story`, `email`, `post_date`) VALUES
	(1, 'Sad money', 'Money is the root all evil', 'sob@gmail.com', '2016-12-26 17:04:34'),
	(2, 'Go', 'Money makes the world go round!', 'sobaka@gmail.com', '2016-12-26 17:07:29'),
	(3, 'Who i am?', 'Ich bin wanderbar!', 'sob@sob.ru', '2017-03-30 10:14:57'),
	(4, 'Where i\'am going?', 'Ich gehe zum Erfolg!', 'psy@na.ru', '2017-03-30 10:18:17');
/*!40000 ALTER TABLE `story` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
