CREATE TABLE IF NOT EXISTS `AspNetRoles` (
  `Id` varchar(128) NOT NULL,
  `Name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--

CREATE TABLE IF NOT EXISTS `AspNetUserClaims` (
  `Id` int(11) NOT NULL,
  `UserId` varchar(128) NOT NULL,
  `ClaimType` longtext,
  `ClaimValue` longtext
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--

CREATE TABLE IF NOT EXISTS `AspNetUserLogins` (
  `LoginProvider` varchar(128) NOT NULL,
  `ProviderKey` varchar(128) NOT NULL,
  `UserId` varchar(128) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--

CREATE TABLE IF NOT EXISTS `AspNetUserRoles` (
  `UserId` varchar(128) NOT NULL,
  `RoleId` varchar(128) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--

CREATE TABLE IF NOT EXISTS `AspNetUsers` (
  `Id` varchar(128) NOT NULL,
  `Email` varchar(255) DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext,
  `SecurityStamp` longtext,
  `PhoneNumber` longtext,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEndDateUtc` datetime DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int(11) NOT NULL,
  `UserName` varchar(255) NOT NULL,
  `Gender` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--

--

ALTER TABLE `AspNetRoles`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `RoleNameIndex` (`Name`) USING HASH;

--

ALTER TABLE `AspNetUserClaims`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_UserId` (`UserId`) USING HASH;

ALTER TABLE `AspNetUserClaims`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;
  
ALTER TABLE `AspNetUserClaims`
  ADD CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
 
ALTER TABLE `AspNetUserLogins`
  ADD PRIMARY KEY (`LoginProvider`,`ProviderKey`,`UserId`),
  ADD KEY `IX_UserId` (`UserId`) USING HASH;
  
ALTER TABLE `AspNetUserLogins`
  ADD CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE;

--

ALTER TABLE `AspNetUserRoles`
  ADD PRIMARY KEY (`UserId`,`RoleId`),
  ADD KEY `IX_UserId` (`UserId`) USING HASH,
  ADD KEY `IX_RoleId` (`RoleId`) USING HASH;

ALTER TABLE `AspNetUserRoles`
  ADD CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE;

--

ALTER TABLE `AspNetUsers`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `UserNameIndex` (`UserName`) USING HASH;