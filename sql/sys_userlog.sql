/*
 Navicat Premium Data Transfer

 Source Server         : 192.168.110.252
 Source Server Type    : MySQL
 Source Server Version : 80029
 Source Host           : 192.168.110.252:3306
 Source Schema         : cmtmc

 Target Server Type    : MySQL
 Target Server Version : 80029
 File Encoding         : 65001

 Date: 27/09/2022 11:00:12
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for sys_userlog
-- ----------------------------
DROP TABLE IF EXISTS `sys_userlog`;
CREATE TABLE `sys_userlog`  (
  `ID` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键ID',
  `LogInfo` varchar(2000) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '日志内容',
  `UserId` int(0) NULL DEFAULT NULL COMMENT '用户Id',
  `LogModule` int(0) NULL DEFAULT NULL COMMENT '日志发生模块',
  `CreateTime` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0),
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 5768226 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = DYNAMIC;

SET FOREIGN_KEY_CHECKS = 1;
