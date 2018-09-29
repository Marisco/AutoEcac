CREATE DATABASE `catalogo` /*!40100 DEFAULT CHARACTER SET utf8 */;

CREATE TABLE `arquivo` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `Tipo` varchar(4) NOT NULL,
  `Nome` varchar(45) NOT NULL,
  `Descricao` varchar(255) DEFAULT NULL,
  `Imagem` blob NOT NULL,
  `Tam` bigint(20) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

CREATE TABLE `items` (
  `seq` int(11) NOT NULL AUTO_INCREMENT,
  `codigo` varchar(25) DEFAULT NULL,
  `descricao` varchar(3700) DEFAULT NULL,
  `cnpjRaiz` varchar(8) NOT NULL,
  `situacao` varchar(10) NOT NULL,
  `modalidade` varchar(10) NOT NULL,
  `ncm` int(8) NOT NULL,
  `codigoNaladi` int(8) DEFAULT NULL,
  `codigoGPC` int(10) DEFAULT NULL,
  `codigoGPCBrick` int(10) DEFAULT NULL,
  `codigoUNSPSC` int(10) DEFAULT NULL,
  `fabricanteConhecido` tinyint(4) NOT NULL,
  `paisOrigem` varchar(2) DEFAULT NULL,
  `cpfCnpjFabricante` varchar(14) DEFAULT NULL,
  `codigoOperadorEstrangeiro` varchar(35) DEFAULT NULL,
  `codigoInternoId` int(11) DEFAULT NULL,
  `atributoId` int(11) DEFAULT NULL,
  PRIMARY KEY (`seq`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
