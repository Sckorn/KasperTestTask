/*
Navicat SQL Server Data Transfer

Source Server         : MSSQL
Source Server Version : 110000
Source Host           : localhost\SQLEXPRESS:1433
Source Database       : KasperTestTask
Source Schema         : KasperTestTask

Target Server Type    : SQL Server
Target Server Version : 110000
File Encoding         : 65001

Date: 2018-04-08 00:36:57
*/


-- ----------------------------
-- Table structure for t_file_list
-- ----------------------------
DROP TABLE [KasperTestTask].[t_file_list]
GO
CREATE TABLE [KasperTestTask].[t_file_list] (
[id] int NOT NULL IDENTITY(1,1),
[basename] varchar(255) NULL ,
[fullname] varchar(1024) NULL ,
[size] int NULL ,
[recstatus] int NULL 
)


GO

-- ----------------------------
-- Indexes structure for table t_file_list
-- ----------------------------
CREATE INDEX [id] ON [KasperTestTask].[t_file_list]
([id] ASC) 
GO
