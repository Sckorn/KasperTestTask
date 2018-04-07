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

Date: 2018-04-07 22:09:38
*/


-- ----------------------------
-- Table structure for t_file_list
-- ----------------------------
DROP TABLE [KasperTestTask].[t_file_list]
GO
CREATE TABLE [KasperTestTask].[t_file_list] (
[id] int NOT NULL ,
[basename] varchar(255) NULL ,
[fullname] varchar(255) NULL ,
[size] float(53) NULL ,
[recstatus] int NULL 
)


GO

-- ----------------------------
-- Indexes structure for table t_file_list
-- ----------------------------
CREATE INDEX [id] ON [KasperTestTask].[t_file_list]
([id] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table t_file_list
-- ----------------------------
ALTER TABLE [KasperTestTask].[t_file_list] ADD PRIMARY KEY ([id])
GO
