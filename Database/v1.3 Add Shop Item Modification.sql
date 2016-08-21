ALTER TABLE `ShopItem`
  ADD COLUMN `Modification` int(11) NOT NULL;

ALTER TABLE `ShopItem`
  ADD CONSTRAINT `FK_ShopItem_Modification` FOREIGN KEY (`Modification`) REFERENCES `Modification` (`Id`);