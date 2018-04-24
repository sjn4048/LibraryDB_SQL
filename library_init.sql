--declare @i int declare cur cursor for select spid from sysprocesses where db_name(dbid)= 'Exp5_LibraryDB' open cur fetch next from cur into @i while @@fetch_status=0 begin exec('kill '+@i) fetch next from cur into @i end close cur deallocate cur

--DROP DATABASE Exp5_LibraryDB

--CREATE DATABASE Exp5_LibraryDB

USE Exp5_LibraryDB

CREATE TABLE administrator (
  admin_id int NOT NULL IDENTITY,
  admin_name varchar(15) NOT NULL,
  passwd varchar(15) DEFAULT NULL,
  PRIMARY KEY (admin_id)
);

INSERT INTO administrator(admin_name, passwd) VALUES ('ShiJianing', '123456'), ('admin','admin');

CREATE TABLE book_info(
  book_id bigint NOT NULL IDENTITY(10000001,1),
  book_name varchar(50) NOT NULL,
  author varchar(50) NOT NULL,
  price decimal(10,2) NOT NULL,
  left_num int NOT NULL,
  PRIMARY KEY (book_id)
);

INSERT INTO book_info (book_name, author, price, left_num) VALUES
('活着', '余华', 20.00, 100),
('人类简史', '尤瓦尔·赫拉利', 68.00, 0),
( '明朝那些事儿（1-9）', '当年明月',358.20, 124),
('经济学原理（上下）', '曼昆', 88.00, 1),
('控方证人', '阿加莎·克里斯蒂',35.00, 44),
('少有人走的路', 'M·斯科特·派克',26.0,92);

CREATE TABLE lend_list (
  sernum bigint NOT NULL IDENTITY(20180001,1),
  book_id bigint NOT NULL,
  reader_id int NOT NULL,
  lend_date date DEFAULT NULL,
  back_date date DEFAULT NULL,
  PRIMARY KEY (sernum)
);

INSERT INTO lend_list (book_id, reader_id, lend_date, back_date) VALUES
(10000001, 10001, '2017-03-15', '2017-06-16'),
(10000003, 10003, '2017-06-10', '2017-09-02'),
(10000006, 10002, '2017-06-12', '2017-09-02'),
(10000004, 10004, '2017-03-15', '2017-09-03'),
(10000005, 10001, '2017-06-15', NULL),
(10000010, 10001, '2017-06-15', NULL),
(10000001, 10002, '2017-09-02', '2017-09-02');

CREATE TABLE reader_card (
  reader_id int NOT NULL IDENTITY(10001,1),
  reader_name varchar(16) NOT NULL,
  passwd varchar(15) NOT NULL DEFAULT '111111',
  card_state tinyint DEFAULT 1,
  PRIMARY KEY (reader_id)
);

INSERT INTO reader_card (reader_name, passwd, card_state) VALUES
('张华', '111111', 1),
('王大锤', '111111', 1),
('李明', '111111', 1),
('赵昭', '111111', 1),
('李云龙', '111111', 1),
('周志华', '111111', 1);