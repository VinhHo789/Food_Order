create database food
go

use food


-- Create KHACHHANG (Customer) table
CREATE TABLE KHACHHANG (
    MAKH int identity(1,1) PRIMARY KEY,
    HO NVARCHAR(255),
    TEN NVARCHAR(255),
    NGAYSINH DATE,
    DIACHI NVARCHAR(255),
    SDT NVARCHAR(20)
);

drop table Khachhang

-- Create MONAN (Food Item) table
CREATE TABLE MONAN (
    MAMA int identity(1,1) PRIMARY KEY,
    TENMA NVARCHAR(255),
    DONGIA MONEY,
    LOAIMA NVARCHAR(50)
);

-- Create HOADON (Invoice) table
CREATE TABLE HOADON (
    MAHD int identity(1,1) PRIMARY KEY,
    MAKH int,
    THT MONEY,
    NGAYHD DATE,
    FOREIGN KEY (MAKH) REFERENCES KHACHHANG(MAKH)
);

drop table cthd
-- Create CTHD (Invoice Detail) table
CREATE TABLE CTHD (
    MAHD int,
    MAMA int,
    MAK nvarchar(255),
    SL INT,
    PRIMARY KEY (MAHD, MAMA),
    FOREIGN KEY (MAHD) REFERENCES HOADON(MAHD),
    FOREIGN KEY (MAMA) REFERENCES MONAN(MAMA)
);

-- Create ACCOUNT table
CREATE TABLE ACCOUNT (
    USERNAME NVARCHAR(255) PRIMARY KEY,
    MAKH int,
    PASSWORD NVARCHAR(255),
    FOREIGN KEY (MAKH) REFERENCES KHACHHANG(MAKH)
);

-- Insert data into KHACHHANG (Customer) table
INSERT INTO KHACHHANG ( HO, TEN, NGAYSINH, DIACHI, SDT)
VALUES 
    ( 'Nguyen', 'Van A', '1990-01-15', '123 Main Street, City', '0123456789'),
    ( 'Tran', 'Thi B', '1985-05-20', '456 Oak Street, Town', '0987654321'),
    ( 'Le', 'Quang C', '1998-11-03', '789 Pine Street, Village', '0369852147');

-- Insert data into MONAN (Food Item) table
INSERT INTO MONAN ( TENMA, DONGIA, LOAIMA)
VALUES 
    ( 'Pho Bo', 5.99, 'Món ăn'),
    ( 'Banh Mi', 3.50, 'Món ăn'),
    ( 'Che Suong Sa', 2.00, 'Nước uống');

-- Insert data into HOADON (Invoice) table
INSERT INTO HOADON ( MAKH, THT, NGAYHD)
VALUES 
    ( 1, 10.99, '2023-01-10'),
    ( 2, 8.50, '2023-02-15'),
    ( 3, 4.00, '2023-03-20');

-- Insert data into CTHD (Invoice Detail) table
INSERT INTO CTHD (MAHD, MAMA, MAK, SL)
VALUES 
    (1, 1, 'Topping1', 2),
    (1, 2, 'Topping2', 1),
    (2, 3, 'Topping3', 3);

-- Insert data into ACCOUNT table
INSERT INTO ACCOUNT (USERNAME, MAKH, PASSWORD)
VALUES 
    ('user1', 1, 'pass123'),
    ('user2', 2, 'pass456'),
    ('user3', 3, 'pass789');