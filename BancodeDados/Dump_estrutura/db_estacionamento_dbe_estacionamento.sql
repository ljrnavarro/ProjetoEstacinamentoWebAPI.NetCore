CREATE DATABASE  IF NOT EXISTS `db_estacionamento` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `db_estacionamento`;
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
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-04-12 12:51:08
