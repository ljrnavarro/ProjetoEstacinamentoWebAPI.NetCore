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
-- Table structure for table `dbe_tiket_pagamento`
--

DROP TABLE IF EXISTS `dbe_tiket_pagamento`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `dbe_tiket_pagamento` (
  `dbp_id` int(11) NOT NULL AUTO_INCREMENT,
  `dbp_dataPagamento` datetime NOT NULL,
  `dbp_pago` varchar(1) NOT NULL,
  `dbp_valor` decimal(4,2) NOT NULL,
  `dbp_id_estacionamento` int(11) DEFAULT NULL,
  PRIMARY KEY (`dbp_id`),
  KEY `fk_id_estacionamento_idx` (`dbp_id_estacionamento`),
  CONSTRAINT `fk_id_estacionamento` FOREIGN KEY (`dbp_id_estacionamento`) REFERENCES `dbe_estacionamento` (`dbs_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dbe_tiket_pagamento`
--

LOCK TABLES `dbe_tiket_pagamento` WRITE;
/*!40000 ALTER TABLE `dbe_tiket_pagamento` DISABLE KEYS */;
INSERT INTO `dbe_tiket_pagamento` VALUES (1,'2019-04-10 11:40:36','S',13.00,1),(2,'2019-04-11 11:14:59','S',58.00,2),(3,'2019-04-11 11:34:25','S',7.00,6),(4,'2019-04-11 14:30:09','S',37.00,3),(5,'2019-04-11 21:19:03','S',25.00,4),(6,'2019-04-11 21:30:55','S',7.00,7),(7,'2019-04-11 21:43:21','S',28.00,5),(8,'2019-04-12 07:28:56','S',7.00,9),(9,'2019-04-12 07:31:49','S',25.00,8),(10,'2019-04-12 07:32:46','S',7.00,10),(11,'2019-04-12 09:18:37','S',7.00,11);
/*!40000 ALTER TABLE `dbe_tiket_pagamento` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-04-12 12:51:53
