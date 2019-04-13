-- MySQL dump 10.13  Distrib 5.7.12, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: db_estacionamento
-- ------------------------------------------------------
-- Server version	5.7.16-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `dbe_estacionamento`
--

DROP TABLE IF EXISTS `dbe_estacionamento`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `dbe_estacionamento` (
  `dbs_id` int(11) NOT NULL AUTO_INCREMENT,
  `dbs_estado` varchar(1) NOT NULL COMMENT 'A = Em aberto\nP = Pago',
  `dbs_id_vaga` int(11) NOT NULL,
  `dbs_id_veiculo` int(11) NOT NULL,
  `dbs_datahora_entrada` datetime NOT NULL,
  `dbs_datahora_saida` datetime DEFAULT NULL,
  PRIMARY KEY (`dbs_id`),
  KEY `fk_id_vaga_idx` (`dbs_id_vaga`),
  KEY `fk_id_tiket_pagamento_idx` (`dbs_id`),
  KEY `fk_estacionamento_id_veiculo_idx` (`dbs_id_veiculo`),
  CONSTRAINT `fk_estacionamento_id_veiculo` FOREIGN KEY (`dbs_id_veiculo`) REFERENCES `dbe_veiculo` (`dbv_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dbe_estacionamento`
--

LOCK TABLES `dbe_estacionamento` WRITE;
/*!40000 ALTER TABLE `dbe_estacionamento` DISABLE KEYS */;
INSERT INTO `dbe_estacionamento` VALUES (1,'P',107,1,'2019-04-10 06:15:09','2019-04-10 11:40:32'),(2,'P',108,10,'2019-04-10 14:59:33','2019-04-11 11:14:59'),(3,'P',110,16,'2019-04-11 00:57:44','2019-04-11 14:30:09'),(4,'P',109,1,'2019-04-11 11:24:25','2019-04-11 21:19:03'),(5,'P',113,10,'2019-04-11 11:29:29','2019-04-11 21:43:21'),(6,'P',114,17,'2019-04-11 11:30:55','2019-04-11 11:34:25'),(7,'P',115,24,'2019-04-11 21:30:19','2019-04-11 21:30:55'),(8,'P',110,16,'2019-04-11 21:43:07','2019-04-12 07:31:49'),(9,'P',111,26,'2019-04-12 07:18:51','2019-04-12 07:28:56'),(10,'P',109,12,'2019-04-12 07:32:01','2019-04-12 07:32:46'),(11,'P',107,1,'2019-04-12 09:08:29','2019-04-12 09:18:37'),(13,'A',113,17,'2019-04-12 09:20:10',NULL),(14,'A',115,12,'2019-04-12 10:49:55',NULL),(15,'A',110,16,'2019-04-12 11:06:33',NULL);
/*!40000 ALTER TABLE `dbe_estacionamento` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-04-12 12:51:52
