CREATE TABLE IF NOT EXISTS `Currency` (
  `id` int(11) NOT NULL,
  `username` varchar(128) NOT NULL,
  `balance` double NOT NULL,
  `realmoney` double NOT NULL,
  `status` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--

CREATE TABLE IF NOT EXISTS `Ban` (
  `id` int(11) NOT NULL,
  `name` varchar(128) NOT NULL,
  `reason` longtext,
  `admin` varchar(128) NOT NULL,
  `time` int(32) NOT NULL,
  `temptime` int(32) NOT NULL,
  `type` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--

CREATE TABLE IF NOT EXISTS `BanIp` (
  `id` int(11) NOT NULL,
  `name` varchar(128) NOT NULL,
  `lastip` longtext
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--

CREATE TABLE IF NOT EXISTS `News` (
  `Id` int(11) NOT NULL,
  `Title` varchar(128) NOT NULL,
  `Text` longtext,
  `TimeCreate` datetime NOT NULL,
  `Image` longblob,
  `ViewersCount` int(11) NOT NULL,
  `Author` varchar(128) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--

CREATE TABLE IF NOT EXISTS `Comment` (
  `Id` int(11) NOT NULL,
  `Text` varchar(255),
  `TimeCreate` datetime NOT NULL,
  `Author` varchar(128) NOT NULL,
  `News` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--

CREATE TABLE IF NOT EXISTS `IKTransaction` (
  `Id` int(11) NOT NULL,
  `TransactionId` varchar(128) NOT NULL,
  `Player` varchar(128) NOT NULL,
  `Time` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--

CREATE TABLE IF NOT EXISTS `Modification` (
  `Id` int(11) NOT NULL,
  `Name` varchar(128),
  `Description` longtext,
  `Version` varchar(32)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--

CREATE TABLE IF NOT EXISTS `Permissions` (
  `id` int(11) NOT NULL,
  `name` longtext,
  `permission` longtext,
  `type` int(11) NOT NULL,
  `value` longtext,
  `world` longtext
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--

CREATE TABLE IF NOT EXISTS `PexEntity` (
  `id` int(11) NOT NULL,
  `name` longtext,
  `type` int(11) NOT NULL,
  `isDefault` int(11) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--

CREATE TABLE IF NOT EXISTS `PexInheritance` (
  `id` int(11) NOT NULL,
  `parent` longtext,
  `type` int(11) NOT NULL,
  `world` longtext,
  `child` longtext
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--

CREATE TABLE IF NOT EXISTS `PlayerItem` (
  `id` int(11) NOT NULL,
  `nickname` varchar(128) DEFAULT NULL,
  `item_id` varchar(128) DEFAULT NULL,
  `item_amount` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--

CREATE TABLE IF NOT EXISTS `Player` (
  `Id` int(11) NOT NULL,
  `Name` varchar(128) NOT NULL,
  `Gender` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--

CREATE TABLE IF NOT EXISTS `PlayerSession` (
  `Id` int(11) NOT NULL,
  `Player` varchar(128) NOT NULL,
  `Session` varchar(128) NOT NULL,
  `Server` varchar(128) NOT NULL,
  `Token` varchar(128) NOT NULL,
  `Md5` varchar(128) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--

CREATE TABLE IF NOT EXISTS `PlayerSkin` (
  `Id` int(11) NOT NULL,
  `Player` varchar(128) DEFAULT NULL,
  `Image` longblob
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;

--

CREATE TABLE IF NOT EXISTS `ServerModification` (
  `Id` int(11) NOT NULL,
  `Server` int(11) NOT NULL,
  `Modification` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--

CREATE TABLE IF NOT EXISTS `Server` (
  `Id` int(11) NOT NULL,
  `Name` varchar(128) NOT NULL,
  `Description` longtext,
  `Image` longblob,
  `ClientVersion` varchar(32) NOT NULL,
  `IpAddress` varchar(16) NOT NULL,
  `Port` int(5) NOT NULL,
  `MapPort` int(5) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--

CREATE TABLE IF NOT EXISTS `ShopItem` (
  `Id` int(11) NOT NULL,
  `GameId` varchar(128) NOT NULL,
  `Name` varchar(128) NOT NULL,
  `Description` longtext,
  `Image` longblob,
  `Price` float NOT NULL,
  `Category` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--

CREATE TABLE IF NOT EXISTS `ShopItemCategory` (
  `Id` int(11) NOT NULL,
  `Name` varchar(128) NOT NULL,
  `Description` longtext
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--

CREATE TABLE IF NOT EXISTS `ShopSale` (
  `Id` int(11) NOT NULL,
  `Item` varchar(128) NOT NULL,
  `Amount` float NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--

--Индексы

--

ALTER TABLE `BanIp`
  ADD PRIMARY KEY (`id`),
  ADD KEY `IX_name` (`name`) USING HASH;

--

ALTER TABLE `Ban`
  ADD PRIMARY KEY (`id`),
  ADD KEY `IX_name` (`name`) USING HASH,
  ADD KEY `IX_admin` (`admin`) USING HASH;

--

ALTER TABLE `Comment`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Author` (`Author`) USING HASH,
  ADD KEY `IX_News` (`News`) USING HASH;

--

ALTER TABLE `Currency`
  ADD PRIMARY KEY (`id`),
  ADD KEY `IX_username` (`username`) USING HASH;

--

ALTER TABLE `IKTransaction`
  ADD UNIQUE KEY (`Id`),
  ADD PRIMARY KEY (`TransactionId`),
  ADD KEY `IX_Player` (`Player`) USING HASH;

--

ALTER TABLE `Modification`
  ADD PRIMARY KEY (`Id`);

--

ALTER TABLE `News`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Author` (`Author`) USING HASH;

--

ALTER TABLE `Permissions`
  ADD PRIMARY KEY (`id`);

--

ALTER TABLE `PexEntity`
  ADD PRIMARY KEY (`id`);

--

ALTER TABLE `PexInheritance`
  ADD PRIMARY KEY (`id`);

--

ALTER TABLE `PlayerItem`
  ADD PRIMARY KEY (`id`),
  ADD KEY `IX_nickname` (`nickname`) USING HASH,
  ADD KEY `IX_item_id` (`item_id`) USING HASH;

--

ALTER TABLE `Player`
  ADD UNIQUE KEY (`Id`),
  ADD PRIMARY KEY (`Name`);

--

ALTER TABLE `PlayerSession`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Player` (`Player`) USING HASH;

--

ALTER TABLE `ServerModification`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Modification` (`Modification`) USING HASH,
  ADD KEY `IX_Server` (`Server`) USING HASH;

--

ALTER TABLE `Server`
  ADD PRIMARY KEY (`Id`);

--

ALTER TABLE `ShopItemCategory`
  ADD PRIMARY KEY (`Id`);

--

ALTER TABLE `ShopItem`
  ADD UNIQUE KEY (`Id`),
  ADD PRIMARY KEY (`GameId`),
  ADD KEY `IX_Category` (`Category`) USING HASH;

--

ALTER TABLE `ShopSale`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Item` (`Item`) USING HASH;

--

ALTER TABLE `PlayerSkin`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Player` (`Player`) USING HASH;

--

--AutoIncrement

--

--

ALTER TABLE `BanIp`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--

ALTER TABLE `Ban`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--

ALTER TABLE `Comment`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--

ALTER TABLE `Currency`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--

ALTER TABLE `Modification`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--

ALTER TABLE `News`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--

ALTER TABLE `Permissions`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--

ALTER TABLE `PexEntity`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--

ALTER TABLE `PexInheritance`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--

ALTER TABLE `Player`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--

ALTER TABLE `PlayerItem`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--

ALTER TABLE `PlayerSession`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--

ALTER TABLE `ServerModification`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--

ALTER TABLE `Server`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--

ALTER TABLE `IKTransaction`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--

ALTER TABLE `ShopItemCategory`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--

ALTER TABLE `ShopItem`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--

ALTER TABLE `ShopSale`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--

ALTER TABLE `PlayerSkin`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;
  
--

ALTER TABLE `Player`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;
  
--

ALTER TABLE `ShopItem`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;
  
--

--Внешние ключи

--

--

ALTER TABLE `BanIp`
  ADD CONSTRAINT `FK_BanIp_Player` FOREIGN KEY (`name`) REFERENCES `Player` (`Name`);

--

ALTER TABLE `Ban`
  ADD CONSTRAINT `FK_Ban_admin` FOREIGN KEY (`admin`) REFERENCES `Player` (`Name`),
  ADD CONSTRAINT `FK_Ban_name` FOREIGN KEY (`name`) REFERENCES `Player` (`Name`);

--

ALTER TABLE `Comment`
  ADD CONSTRAINT `FK_Comment_News` FOREIGN KEY (`News`) REFERENCES `News` (`Id`),
  ADD CONSTRAINT `FK_Comment_Author` FOREIGN KEY (`Author`) REFERENCES `Player` (`Name`);

--

ALTER TABLE `Currency`
  ADD CONSTRAINT `FK_Currency_Player` FOREIGN KEY (`username`) REFERENCES `Player` (`Name`);

--

ALTER TABLE `IKTransaction`
  ADD CONSTRAINT `FK_IKTransaction_Player` FOREIGN KEY (`Player`) REFERENCES `Player` (`Name`);

--

ALTER TABLE `News`
  ADD CONSTRAINT `FK_News_Author` FOREIGN KEY (`Author`) REFERENCES `Player` (`Name`);

--

ALTER TABLE `PlayerItem`
  ADD CONSTRAINT `FK_PlayerItem_nickname` FOREIGN KEY (`nickname`) REFERENCES `Player` (`Name`),
  ADD CONSTRAINT `FK_PlayerItem_item_id` FOREIGN KEY (`item_id`) REFERENCES `ShopItem` (`GameId`);

--

ALTER TABLE `PlayerSession`
  ADD CONSTRAINT `FK_PlayerSession_Player` FOREIGN KEY (`Player`) REFERENCES `Player` (`Name`);

--

ALTER TABLE `ServerModification`
  ADD CONSTRAINT `FK_ServerModification_Modification` FOREIGN KEY (`Modification`) REFERENCES `Modification` (`Id`),
  ADD CONSTRAINT `FK_ServerModification_Server` FOREIGN KEY (`Server`) REFERENCES `Server` (`Id`);

--

ALTER TABLE `ShopItem`
  ADD CONSTRAINT `FK_ShopItem_Category` FOREIGN KEY (`Category`) REFERENCES `ShopItemCategory` (`Id`);

--

ALTER TABLE `ShopSale`
  ADD CONSTRAINT `FK_ShopSale_Item` FOREIGN KEY (`Item`) REFERENCES `ShopItem` (`GameId`);

--

ALTER TABLE `PlayerSkin`
  ADD CONSTRAINT `FK_UserSkin_Player` FOREIGN KEY (`Player`) REFERENCES `Player` (`Name`);

  