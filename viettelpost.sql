/*
 Navicat Premium Dump SQL

 Source Server         : MySqlH2q
 Source Server Type    : MySQL
 Source Server Version : 80031 (8.0.31)
 Source Host           : 103.163.215.105:3306
 Source Schema         : viettelpost

 Target Server Type    : MySQL
 Target Server Version : 80031 (8.0.31)
 File Encoding         : 65001

 Date: 22/04/2026 02:56:11
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for candidates
-- ----------------------------
DROP TABLE IF EXISTS `candidates`;
CREATE TABLE `candidates`  (
  `candidate_id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `candidate_name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `candidate_dob` date NULL DEFAULT NULL,
  `candidate_gender` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `candidate_region` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `candidate_phone_number` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `candidate_country` int NULL DEFAULT NULL,
  `candidate_province` int NULL DEFAULT NULL,
  `candidate_ward` int NULL DEFAULT NULL,
  `candidate_address_detail` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `candidate_email` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  INDEX `fk_country`(`candidate_country` ASC) USING BTREE,
  INDEX `fk_province`(`candidate_province` ASC) USING BTREE,
  INDEX `fk_ward`(`candidate_ward` ASC) USING BTREE,
  CONSTRAINT `fk_country` FOREIGN KEY (`candidate_country`) REFERENCES `region2` (`RegionID`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_province` FOREIGN KEY (`candidate_province`) REFERENCES `region2` (`RegionID`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_ward` FOREIGN KEY (`candidate_ward`) REFERENCES `region2` (`RegionID`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for employees
-- ----------------------------
DROP TABLE IF EXISTS `employees`;
CREATE TABLE `employees`  (
  `employee_id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `employee_name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `employee_age` int NOT NULL,
  `create_date` datetime NULL DEFAULT NULL,
  `create_by` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `modified_date` datetime NULL DEFAULT NULL,
  `modified_by` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  PRIMARY KEY (`employee_id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for region2
-- ----------------------------
DROP TABLE IF EXISTS `region2`;
CREATE TABLE `region2`  (
  `RegionID` int NOT NULL AUTO_INCREMENT,
  `ParentID` int(10) UNSIGNED ZEROFILL NULL DEFAULT NULL,
  `RegionCode` varchar(8) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `RegionName` varchar(120) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `RegionNameNotMark` varchar(120) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `RegionNameAlias` varchar(120) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `RegionLevel` int NULL DEFAULT NULL,
  `IsActive` tinyint NULL DEFAULT NULL,
  `Order` int NULL DEFAULT NULL,
  `RegionCodeVHD` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `CreateUser` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `CreateDate` datetime NULL DEFAULT CURRENT_TIMESTAMP,
  `CreateDate_Tick` bigint NULL DEFAULT NULL,
  `UpdateUser` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `LastUpdate` datetime NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `LastUpdate_Tick` bigint NULL DEFAULT NULL,
  `IsDelete` bit(1) NULL DEFAULT b'0',
  PRIMARY KEY (`RegionID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3597 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Table structure for systemkey
-- ----------------------------
DROP TABLE IF EXISTS `systemkey`;
CREATE TABLE `systemkey`  (
  `ID` int NOT NULL AUTO_INCREMENT,
  `ParentID` int UNSIGNED NULL DEFAULT NULL,
  `CodeKey` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `CodeValue` int NULL DEFAULT 0,
  `Description` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `SortOrder` int UNSIGNED NULL DEFAULT NULL,
  `IsDelete` tinyint(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`ID`) USING BTREE,
  INDEX `Index_Id`(`ID` ASC) USING BTREE,
  INDEX `Index_CodeValue`(`ParentID` ASC, `CodeValue` ASC) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 251 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Procedure structure for usp_logerror_InsertError
-- ----------------------------
DROP PROCEDURE IF EXISTS `usp_logerror_InsertError`;
delimiter ;;
CREATE DEFINER=`aiviewems`@`%` PROCEDURE `usp_logerror_InsertError`(
    IN pLogContent TEXT,
    IN pTypeLog INT
)
BEGIN
  INSERT INTO LogError (LogContent, TypeLog, LogTime)
    VALUES (pLogContent, pTypeLog, NOW());
    SELECT LAST_INSERT_ID();
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for usp_userotp_GetByEmail
-- ----------------------------
DROP PROCEDURE IF EXISTS `usp_userotp_GetByEmail`;
delimiter ;;
CREATE DEFINER=`aiviewems`@`%` PROCEDURE `usp_userotp_GetByEmail`(
  IN pEmail VARCHAR(255)
)
BEGIN
  SELECT *
    FROM userotp
    WHERE email = pEmail;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for usp_userprofiles_GetByUserId
-- ----------------------------
DROP PROCEDURE IF EXISTS `usp_userprofiles_GetByUserId`;
delimiter ;;
CREATE DEFINER=`aiviewems`@`%` PROCEDURE `usp_userprofiles_GetByUserId`(
IN pUserId VARCHAR(36)
  )
BEGIN
  SELECT *
    FROM userprofiles
    WHERE userprofiles.userId = pUserId;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for usp_users_CheckExistExternalUser
-- ----------------------------
DROP PROCEDURE IF EXISTS `usp_users_CheckExistExternalUser`;
delimiter ;;
CREATE DEFINER=`aiviewems`@`%` PROCEDURE `usp_users_CheckExistExternalUser`(pExternalLoginID varchar(100))
BEGIN
		Select *
		From `users`
		Where ExternalLoginID = pExternalLoginID;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for usp_users_GetUserByEmail
-- ----------------------------
DROP PROCEDURE IF EXISTS `usp_users_GetUserByEmail`;
delimiter ;;
CREATE DEFINER=`aiviewems`@`%` PROCEDURE `usp_users_GetUserByEmail`(
   IN pEmail VARCHAR(255)
)
BEGIN
  SELECT *
    FROM users
    WHERE email = pEmail;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for usp_User_Token_DeleteByToken
-- ----------------------------
DROP PROCEDURE IF EXISTS `usp_User_Token_DeleteByToken`;
delimiter ;;
CREATE DEFINER=`aiviewems`@`%` PROCEDURE `usp_User_Token_DeleteByToken`(pToken varchar(50) CHARACTER SET utf8mb4 collate utf8mb4_unicode_ci)
BEGIN
	DELETE  FROM UserToken WHERE Token = pToken;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for usp_User_Token_Insert
-- ----------------------------
DROP PROCEDURE IF EXISTS `usp_User_Token_Insert`;
delimiter ;;
CREATE DEFINER=`aiviewems`@`%` PROCEDURE `usp_User_Token_Insert`(pID varchar(36) ,
pUserID varchar(36) ,
pIsRememberPassword BIT(1),
pToken varchar(50) ,
pExpiredDate datetime ,
pCreateDate datetime)
BEGIN
	INSERT  INTO UserToken
					( ID,
						UserID,
						IsRememberPassword,
						Token ,
						ExpiredDate ,
						CreateDate
					)
	VALUES  ( pID,
						pUserID ,
						pIsRememberPassword,
						pToken ,
						pExpiredDate ,
						pCreateDate
					);
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for usp_User_Token_SelectbyToken
-- ----------------------------
DROP PROCEDURE IF EXISTS `usp_User_Token_SelectbyToken`;
delimiter ;;
CREATE DEFINER=`aiviewems`@`%` PROCEDURE `usp_User_Token_SelectbyToken`(pToken varchar(50) CHARACTER SET utf8mb4 collate utf8mb4_unicode_ci)
BEGIN
		Select  
					ut.*,
					u.UserName Username,
					u.FullName,
					u.UserCode,
					u.AccountType,
					u.AccountID,
					u.Avatar,
					u.Email,
					u.WardID,
					u.DistrictID,
					u.ProvinceID,
					u.Address,
					u.PhoneNumber
		From UserToken ut
		INNER JOIN `User` u on ut.UserID = u.UserID
		Where  ut.Token = pToken;
		-- And ut.ExpiredDate > CURRENT_TIMESTAMP;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for usp_user_Token_UpdateExpiredDate
-- ----------------------------
DROP PROCEDURE IF EXISTS `usp_user_Token_UpdateExpiredDate`;
delimiter ;;
CREATE DEFINER=`aiviewems`@`%` PROCEDURE `usp_user_Token_UpdateExpiredDate`(pID varchar(36)  CHARACTER SET utf8mb4 collate utf8mb4_unicode_ci,
    pExpiredDate DATETIME)
BEGIN
        UPDATE  UserToken
        SET     ExpiredDate = pExpiredDate
        WHERE   ID = pID;
END
;;
delimiter ;

SET FOREIGN_KEY_CHECKS = 1;
