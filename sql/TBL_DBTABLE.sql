/*
 Navicat Premium Data Transfer

 Source Server         : localhost mysql
 Source Server Type    : MySQL
 Source Server Version : 80027
 Source Host           : localhost:3306
 Source Schema         : mydb1

 Target Server Type    : MySQL
 Target Server Version : 80027
 File Encoding         : 65001

 Date: 30/10/2021 08:34:19
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for TBL_DBTABLE
-- ----------------------------
DROP TABLE IF EXISTS `TBL_DBTABLE`;
CREATE TABLE `TBL_DBTABLE`  (
  `ID` decimal(11, 0) NOT NULL,
  `ISDELETE` decimal(4, 0) NULL DEFAULT NULL,
  `DBTABLENAME` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `DBTABLENO` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `DBCONNECTIONID` decimal(11, 0) NULL DEFAULT NULL,
  `SORT` decimal(11, 0) NULL DEFAULT NULL,
  `STATE` decimal(4, 0) NULL DEFAULT NULL,
  `CREATETIME` datetime(0) NULL DEFAULT NULL,
  `REMARK` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
