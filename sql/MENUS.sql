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

 Date: 30/10/2021 08:33:48
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for MENUS
-- ----------------------------
DROP TABLE IF EXISTS `MENUS`;
CREATE TABLE `MENUS`  (
  `ID` decimal(11, 0) NOT NULL,
  `MENUNAME` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `URL` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `ICON` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `PARENTID` decimal(11, 0) NULL DEFAULT NULL,
  `ORDER` decimal(4, 0) NULL DEFAULT NULL,
  `ISDELETE` decimal(4, 0) NULL DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of MENUS
-- ----------------------------
INSERT INTO `MENUS` VALUES (1, '采集配置', '#', NULL, 0, 0, 0);
INSERT INTO `MENUS` VALUES (9, '数据库连接', '/DB/DBConnection/Index', NULL, 1, 7, 0);
INSERT INTO `MENUS` VALUES (10, '数据库表', '/DB/DBTable/Index', NULL, 1, 8, 0);
INSERT INTO `MENUS` VALUES (11, '数据库表字段', '/DB/DBField/Index', NULL, 1, 9, 0);

SET FOREIGN_KEY_CHECKS = 1;
