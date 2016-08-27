ALTER TABLE `PlayerInfo`
  ADD COLUMN `Refer` varchar(128) NULL;

ALTER TABLE `PlayerInfo`
  ADD CONSTRAINT `FK_PlayerInfo_Player` FOREIGN KEY (`Refer`) REFERENCES `Player` (`Name`);