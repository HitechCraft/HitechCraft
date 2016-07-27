CREATE TABLE IF NOT EXISTS `AuthLog` (
  `Id` int(11) NOT NULL,
  `Player` int(11) NOT NULL,
  `Ip` varchar(128) NOT NULL,
  `Browser` varchar(128) NOT NULL,
  `Type` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

ALTER TABLE `AuthLog`
  ADD PRIMARY KEY (`Id`);

ALTER TABLE `AuthLog`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

ALTER TABLE `AuthLog`
  ADD CONSTRAINT `FK_AuthLog_Player` FOREIGN KEY (`Player`) REFERENCES `Player` (`Id`);