CREATE TABLE IF NOT EXISTS `Referal` (
  `Id` int(11) NOT NULL,
  `Refer` varchar(128) NOT NULL,
  `Referer` varchar(128) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

ALTER TABLE `Referal`
  ADD PRIMARY KEY (`Id`);

ALTER TABLE `Referal`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;
  
ALTER TABLE `Referal`
  DROP FOREIGN KEY `FK_PlayerInfo_Player`;
  
ALTER TABLE `PlayerInfo`
  DROP COLUMN `Refer`;

ALTER TABLE `Referal`
  ADD CONSTRAINT `FK_Referal_Refer` FOREIGN KEY (`Refer`) REFERENCES `Player` (`Name`);
  
ALTER TABLE `Referal`
  ADD CONSTRAINT `FK_Referal_Referer` FOREIGN KEY (`Referer`) REFERENCES `Player` (`Name`);