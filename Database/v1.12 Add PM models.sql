CREATE TABLE IF NOT EXISTS `PMPlayerBox` (
  `Id` int(11) NOT NULL,
  `Message` int(11) NOT NULL,
  `Player` varchar(128) NOT NULL,
  `PlayerType` int(11) DEFAULT 0 NOT NULL,
  `PmType` int(11) DEFAULT 0 NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE IF NOT EXISTS `PrivateMessage` (
  `Id` int(11) NOT NULL,
  `Title` varchar(128) NOT NULL,
  `Text` varchar(1000) NOT NULL,
  `TimeCreate` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

ALTER TABLE `PMPlayerBox`
  ADD PRIMARY KEY (`Id`);

ALTER TABLE `PMPlayerBox`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;
  
ALTER TABLE `PrivateMessage`
  ADD PRIMARY KEY (`Id`);

ALTER TABLE `PrivateMessage`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;
  
ALTER TABLE `PMPlayerBox`
  ADD CONSTRAINT `FK_PMPlayerBox_Player` FOREIGN KEY (`Player`) REFERENCES `Player` (`Name`);
  
ALTER TABLE `PMPlayerBox`
  ADD CONSTRAINT `FK_PMPlayerBox_Message` FOREIGN KEY (`Message`) REFERENCES `PrivateMessage` (`Id`);