CREATE TABLE IF NOT EXISTS `PlayerInfo` (
  `Id` int(11) NOT NULL,
  `Email` varchar(128) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

ALTER TABLE `PlayerInfo`
  ADD PRIMARY KEY (`Id`);

ALTER TABLE `Player`
  ADD COLUMN `PlayerInfo` int(11) NOT NULL;

ALTER TABLE `PlayerInfo`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

ALTER TABLE `Player`
  ADD CONSTRAINT `FK_Player_PlayerInfo` FOREIGN KEY (`PlayerInfo`) REFERENCES `PlayerInfo` (`Id`);