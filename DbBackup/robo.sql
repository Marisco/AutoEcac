-- MySQL dump 10.13  Distrib 8.0.12, for Win64 (x86_64)
--
-- Host: 52.91.45.143    Database: robo
-- ------------------------------------------------------
-- Server version	8.0.12

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
 SET NAMES utf8 ;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `tsiscomexweb_robo`
--

DROP TABLE IF EXISTS `tsiscomexweb_robo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `tsiscomexweb_robo` (
  `nr_sequencia` bigint(20) NOT NULL AUTO_INCREMENT,
  `nr_processo` bigint(20) NOT NULL,
  `nr_registro_di` bigint(20) DEFAULT NULL,
  `nr_seq` tinyint(2) DEFAULT NULL,
  `tp_consulta` varchar(2) NOT NULL,
  `tp_acao` varchar(15) NOT NULL,
  `dt_agendamento` datetime DEFAULT NULL,
  `dt_realizacao` datetime NOT NULL,
  `dt_registro_di` datetime DEFAULT NULL,
  `nr_tentativas` tinyint(1) NOT NULL DEFAULT '0',
  `tx_erro` text,
  `cd_usuario` bigint(20) DEFAULT NULL,
  `cpf_certificado` varchar(11) NOT NULL,
  `in_rodando` tinyint(1) NOT NULL DEFAULT '1',
  `in_desembaraco` tinyint(1) DEFAULT NULL,
  `tp_anuencia` varchar(45) DEFAULT NULL,
  `in_situacao` tinyint(1) DEFAULT NULL,
  `xml_comando` text,
  `xml_retorno` text,
  PRIMARY KEY (`nr_sequencia`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tsiscomexweb_robo`
--

LOCK TABLES `tsiscomexweb_robo` WRITE;
/*!40000 ALTER TABLE `tsiscomexweb_robo` DISABLE KEYS */;
INSERT INTO `tsiscomexweb_robo` VALUES (1,12344,1815260647,0,'DI','consulta',NULL,'2018-08-25 22:59:58','2018-08-21 22:59:58',0,NULL,NULL,'00487543904',0,1,NULL,NULL,NULL,NULL),(2,12345,1815141460,0,'DI','consulta',NULL,'2018-08-25 22:59:58','2018-08-20 22:59:58',0,NULL,NULL,'00487543904',0,1,NULL,NULL,NULL,NULL),(3,12346,1815141508,0,'DI','consulta',NULL,'2018-08-25 22:59:58','2018-08-20 22:59:58',0,NULL,NULL,'00487543904',0,1,NULL,NULL,NULL,NULL),(4,12347,1810561193,0,'DI','consulta',NULL,'2018-08-26 14:17:09','2018-08-20 22:59:58',1,NULL,NULL,'00487543904',0,0,NULL,NULL,NULL,'xml inteiro da DI'),(5,12347,1810561193,0,'DI','acompanha',NULL,'2018-08-26 14:50:34','2018-08-20 22:59:58',0,NULL,NULL,'00487543904',1,0,NULL,NULL,NULL,'guardar xml do desembaraco concluido'),(6,12349,1815715989,0,'DI','consulta',NULL,'2018-08-27 20:50:34','2018-08-27 10:59:58',0,NULL,NULL,'00487543904',0,0,NULL,NULL,NULL,NULL),(7,12343,1815715644,0,'DI','consulta',NULL,'2018-08-27 20:51:34','2018-08-27 10:59:50',0,NULL,NULL,'00487543904',0,0,NULL,NULL,NULL,NULL),(8,12342,1815714923,0,'DI','consulta',NULL,'2018-08-27 20:51:35','2018-08-27 12:00:50',0,NULL,NULL,'00487543904',0,0,NULL,NULL,NULL,NULL),(9,12341,1815805465,0,'DI','consulta',NULL,'2018-08-28 20:51:35','2018-08-28 12:00:50',0,NULL,NULL,'00487543904',1,0,NULL,NULL,NULL,NULL),(10,12340,1816040470,0,'DI','consulta',NULL,'2018-08-31 22:00:00','2018-08-31 17:00:00',0,NULL,NULL,'00487543904',1,0,NULL,NULL,NULL,NULL),(11,12348,1829588774,0,'LI','consulta',NULL,'2018-08-31 17:00:00','2018-08-31 17:00:00',0,NULL,NULL,'00487543904',1,0,NULL,NULL,NULL,NULL);
/*!40000 ALTER TABLE `tsiscomexweb_robo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'robo'
--

--
-- Dumping routines for database 'robo'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-09-02  6:31:07
