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

 Date: 12/05/2026 22:50:57
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
  `create_date` datetime NULL DEFAULT NULL,
  `create_by` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `modified_date` datetime NULL DEFAULT NULL,
  `modified_by` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
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
-- Table structure for pa_grid_config
-- ----------------------------
DROP TABLE IF EXISTS `pa_grid_config`;
CREATE TABLE `pa_grid_config`  (
  `grid_config_id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT 'Khóa chính',
  `orgnization_id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT 'ID Đơn vị (nếu config áp dụng chung cho đơn vị)',
  `user_id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT 'ID Người dùng (nếu config lưu theo từng user cụ thể)',
  `grid_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT 'Mã định danh của Grid (VD: SalaryCompositionList)',
  `column_field` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT 'Tên trường dữ liệu (VD: CompositionCode, Property)',
  `is_visible` tinyint(1) NULL DEFAULT 1 COMMENT 'Cột có hiển thị hay không (1: Hiện, 0: Ẩn)',
  `is_pinned` tinyint(1) NULL DEFAULT 0 COMMENT 'Cho phép ghim cột (1: Ghim, 0: Không)',
  `pin_postion` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT 'Vị trí ghim (left, right)',
  `column_order` int NULL DEFAULT 0 COMMENT 'Thứ tự hiển thị của cột',
  `column_width` int NULL DEFAULT NULL COMMENT 'Độ rộng của cột (px)',
  `created_by` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT 'Người tạo',
  `created_date` datetime NULL DEFAULT CURRENT_TIMESTAMP,
  `modified_date` datetime NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `modified_by` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT 'Người sửa đổi',
  PRIMARY KEY (`grid_config_id`) USING BTREE,
  UNIQUE INDEX `idx_user_grid_col`(`user_id` ASC, `grid_id` ASC, `column_field` ASC) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = 'Bảng cấu hình hiển thị cột của Data Grid' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for pa_organization
-- ----------------------------
DROP TABLE IF EXISTS `pa_organization`;
CREATE TABLE `pa_organization`  (
  `orgnization_id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT 'Khóa chính, ID Đơn vị/Công ty',
  `parent_id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT 'ID của đơn vị cha (NULL nếu là đơn vị cấp cao nhất)',
  `orgnization_name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT 'Tên đơn vị (VD: Công ty Thí điểm AgentWork)',
  `created_date` datetime NULL DEFAULT CURRENT_TIMESTAMP,
  `modified_date` datetime NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `modified_by` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT 'Người thay đổi',
  `created_by` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT 'Người tạo',
  PRIMARY KEY (`orgnization_id`) USING BTREE,
  INDEX `idx_parent_id`(`parent_id` ASC) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = 'Bảng danh sách đơn vị công tác' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for pa_salary_composition
-- ----------------------------
DROP TABLE IF EXISTS `pa_salary_composition`;
CREATE TABLE `pa_salary_composition`  (
  `composition_id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT 'Khóa chính, ID Thành phần lương',
  `orgnization_id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT 'ID Đơn vị áp dụng (FK)',
  `system_composition_id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT 'ID TPL Hệ thống (nếu được nhân bản từ hệ thống)',
  `composition_code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT 'Mã thành phần (VD: __HT_DS)',
  `composition_name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT 'Tên thành phần (VD: % HT DS)',
  `composition_type` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT 'Loại thành phần (Lương, Khác, Doanh số, Thông tin nhân viên...)',
  `property` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT 'Tính chất (Thu nhập, Khác...)',
  `taxable_type` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT 'Chịu thuế (Chịu thuế, -)',
  `tax_deduction_type` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT 'Giảm trừ khi tính thuế (Không, -)',
  `norm` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT 'Định mức',
  `value_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT 'Kiểu giá trị (Tiền tệ, ...)',
  `value_expression` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL COMMENT 'Giá trị hoặc Công thức (VD: 1.000.000 hoặc =15%*THU_NHAP...)',
  `description` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL COMMENT 'Mô tả',
  `show_on_payslip` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT 'Hiển thị trên phiếu lương (Chỉ hiển thị nếu giá trị khác 0, Có)',
  `creation_source` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT 'Nguồn tạo (Tự thêm, Hệ thống...)',
  `status` tinyint(1) NULL DEFAULT 1 COMMENT 'Trạng thái (1: Đang theo dõi, 0: Ngừng theo dõi)',
  `created_date` datetime NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `modified_date` datetime NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `modified_by` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`composition_id`) USING BTREE,
  INDEX `idx_org_id`(`orgnization_id` ASC) USING BTREE,
  INDEX `idx_sys_comp_id`(`system_composition_id` ASC) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = 'Bảng danh sách thành phần lương của đơn vị' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for pa_salary_composition_system
-- ----------------------------
DROP TABLE IF EXISTS `pa_salary_composition_system`;
CREATE TABLE `pa_salary_composition_system`  (
  `system_composition_id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT 'Khóa chính, ID TPL Hệ thống',
  `composition_code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT 'Mã thành phần (VD: LCB, BHXH...)',
  `composition_name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT 'Tên thành phần',
  `composition_type` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT 'Loại thành phần (Lương, Khác, Doanh số, Thông tin nhân viên...)',
  `property` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT 'Tính chất (Thu nhập, Khác...)',
  `taxable_type` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT 'Chịu thuế (Chịu thuế, Không chịu thuế, -)',
  `tax_deduction_type` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT 'Giảm trừ khi tính thuế (Không, -)',
  `norm` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT 'Định mức',
  `value_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT 'Kiểu giá trị (Tiền tệ, Phần trăm...)',
  `value_expression` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL COMMENT 'Giá trị/Công thức tính (VD: =PHU_CAP_01*KHAU_TRU_BHXH)',
  `description` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL COMMENT 'Mô tả',
  `show_on_payslip` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT 'Hiển thị trên phiếu lương (Chỉ hiển thị nếu giá trị khác 0, Có, Không)',
  `status` tinyint(1) NULL DEFAULT 1 COMMENT 'Trạng thái (1: Đang theo dõi, 0: Ngừng theo dõi)',
  `created_date` datetime NULL DEFAULT CURRENT_TIMESTAMP,
  `modified_date` datetime NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `created_by` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `modified_by` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`system_composition_id`) USING BTREE,
  UNIQUE INDEX `idx_system_comp_code`(`composition_code` ASC) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = 'Bảng danh mục thành phần lương hệ thống' ROW_FORMAT = Dynamic;

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
-- View structure for candidateswithregion
-- ----------------------------
DROP VIEW IF EXISTS `candidateswithregion`;
CREATE ALGORITHM = UNDEFINED SQL SECURITY DEFINER VIEW `candidateswithregion` AS select `c`.`candidate_id` AS `candidate_id`,`c`.`candidate_name` AS `candidate_name`,`c`.`candidate_dob` AS `candidate_dob`,`c`.`candidate_phone_number` AS `candidate_phone_number`,`c`.`candidate_email` AS `candidate_email`,`c`.`candidate_gender` AS `candidate_gender`,`country`.`RegionName` AS `country_name`,`province`.`RegionName` AS `province_name`,`ward`.`RegionName` AS `ward_name`,`c`.`candidate_address_detail` AS `candidate_address_detail` from (((`candidates` `c` left join `region2` `country` on((`country`.`RegionID` = `c`.`candidate_country`))) left join `region2` `province` on((`province`.`RegionID` = `c`.`candidate_province`))) left join `region2` `ward` on((`ward`.`RegionID` = `c`.`candidate_ward`)));

-- ----------------------------
-- View structure for view_salary_composition_after_join
-- ----------------------------
DROP VIEW IF EXISTS `view_salary_composition_after_join`;
CREATE ALGORITHM = UNDEFINED SQL SECURITY DEFINER VIEW `view_salary_composition_after_join` AS select `sc`.`CompositionId` AS `CompositionId`,`sc`.`OrganizationId` AS `OrganizationId`,`sc`.`SystemCompositionId` AS `SystemCompositionId`,`sc`.`CompositionCode` AS `CompositionCode`,`sc`.`CompositionName` AS `CompositionName`,`sc`.`CompositionType` AS `CompositionType`,`sc`.`Property` AS `Property`,`sc`.`TaxableType` AS `TaxableType`,`sc`.`TaxDeductionType` AS `TaxDeductionType`,`sc`.`Norm` AS `Norm`,`sc`.`ValueType` AS `ValueType`,`sc`.`ValueExpression` AS `ValueExpression`,`sc`.`Description` AS `Description`,`sc`.`ShowOnPayslip` AS `ShowOnPayslip`,`sc`.`CreationSource` AS `CreationSource`,`sc`.`Status` AS `Status`,`sc`.`CreatedDate` AS `CreatedDate`,`sc`.`CreatedBy` AS `CreatedBy`,`sc`.`ModifiedDate` AS `ModifiedDate`,`sc`.`ModifiedBy` AS `ModifiedBy`,`o`.`OrganizationName` AS `OrganizationName` from (`pa_salary_composition` `sc` left join `pa_organization` `o` on((`sc`.`OrganizationId` = `o`.`OrganizationId`)));

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
